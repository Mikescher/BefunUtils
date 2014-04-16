using BefunGen.AST;
using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.Tags;
using BefunGen.AST.Exceptions;
using BefunWrite.Controls;
using BefunWrite.Dialogs;
using BefunWrite.Helper;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;

namespace BefunWrite
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class EditorWindow : Window
	{
		private const int PARSE_WAIT_TIME = 666; // Minimal Time (in ms) without Button Press for Parsing

		private TextFungeProjectWrapper project = null; // Is Set in constructor

		private IconBarMargin IconBar;

		private bool supressComboBoxChangedEvent = false;

		private Thread parseThread;
		private bool parseThreadRunning = true;
		private bool hasErrors = true;

		private TextFungeParser Parser;

		#region Konstruktor & Init

		public EditorWindow()
		{
			project = TextFungeProjectWrapper.CreateNew();

			InitializeComponent();

			Init();

			codeEditor.Text = Properties.Resources.example;
		}

		private void Init()
		{
			using (XmlReader reader = new XmlTextReader(new StringReader(Properties.Resources.TextFunge)))
			{
				IHighlightingDefinition customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
				codeEditor.SyntaxHighlighting = customHighlighting;
			}

			codeEditor.ShowLineNumbers = true;
			codeEditor.Options.CutCopyWholeLine = true;
			codeEditor.Options.ShowTabs = true;

			codeEditor.TextArea.LeftMargins.Insert(0, IconBar = new IconBarMargin(this, codeEditor));

			//######################

			parseThread = new Thread(work);
			parseThread.IsBackground = true;
			parseThread.Start();

			//######################

			Parser = new TextFungeParser();
		}

		#endregion

		#region Command Events

		#region New

		private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (project.isDirty())
			{
				MessageBoxResult r = MessageBox.Show("There are unsaved changes. Save now ?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

				switch (r)
				{
					case MessageBoxResult.Yes:
						if (!project.TrySave())
							return;
						break;
					case MessageBoxResult.Cancel:
						return;
				}
			}

			project = TextFungeProjectWrapper.CreateNew();

			updateUI();
		}

		private void NewEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;

			e.Handled = true;
		}

		#endregion

		#region Save

		private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (project.TrySave())
			{
				updateUI();
			}
		}

		private void SaveEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = project.isDirty();

			e.Handled = true;
		}

		#endregion

		#region Save As

		private void SaveAsExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (project.TrySave(true))
			{
				updateUI();
			}
		}

		private void SaveAsEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;

			e.Handled = true;
		}

		#endregion

		#region Open

		private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (project.isDirty())
			{
				MessageBoxResult r = MessageBox.Show("There are unsaved changes. Save now ?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

				switch (r)
				{
					case MessageBoxResult.Yes:
						if (!project.TrySave())
							return;
						break;
					case MessageBoxResult.Cancel:
						return;
				}
			}

			DoOpen();
		}

		private void OpenEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;

			e.Handled = true;
		}

		#endregion

		#region Build

		private void BuildExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (!project.TrySave())
				return;

			if (!project.HasConfigSelected)
			{
				MessageBox.Show("No Config selected", ">> Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			//#############################################

			string buildDir = Path.Combine(Path.GetDirectoryName(project.ProjectConfigPath), "build-" + project.GetProjectName(), DirectoryHelper.PrepareStringAsPath(project.SelectedConfig.Name));

			string filename;
			if (project.SelectedConfig.ExecSettings.IsDebug)
				filename = DirectoryHelper.PrepareStringAsPath(project.SelectedConfig.Name) + ".tfd";
			else
				filename = DirectoryHelper.PrepareStringAsPath(project.SelectedConfig.Name) + ".b98";

			string target = Path.Combine(buildDir, filename);

			Directory.CreateDirectory(buildDir);

			//#############################################

			string code;
			try
			{
				ASTObject.CGO = project.SelectedConfig.Options;

				Program tmp;
				code = Parser.generateCode(codeEditor.Text, project.SelectedConfig.ExecSettings.IsDebug);
			}
			catch (BefunGenException ex)
			{
				MessageBox.Show(ex.ToString(), ">> BefunGen Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), ">> Internal Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			//#############################################

			try
			{
				File.WriteAllText(target, code);
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.ToString(), ">> Filesystem Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
		}

		private void BuildEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = !hasErrors;

			e.Handled = true;
		}

		#endregion

		#region Start

		private void StartExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (!project.HasConfigSelected)
			{
				MessageBox.Show("No Config selected", ">> Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			//#############################################

			string target;
			if (project.SelectedConfig.ExecSettings.IsDebug)
				target = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString("B") + ".tfd";
			else
				target = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString("B") + ".b98";

			string code;
			Program prog;
			CodePiece cpiece;
			try
			{
				ASTObject.CGO = project.SelectedConfig.Options;

				code = Parser.generateCode(codeEditor.Text, project.SelectedConfig.ExecSettings.IsDebug, out prog, out cpiece);
			}
			catch (BefunGenException ex)
			{
				MessageBox.Show(ex.ToString(), ">> BefunGen Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), ">> Internal Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			//#############################################

			try
			{
				File.WriteAllText(target, code);
			}
			catch (IOException ex)
			{
				MessageBox.Show(ex.ToString(), ">> Filesystem Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			//#############################################

			ProcessStartInfo start = new ProcessStartInfo();

			start.Arguments = "";
			start.Arguments += String.Format("-file=\"{0}\"", target) + " ";

			if (project.SelectedConfig.ExecSettings.IsDebug)
				start.Arguments += "-debug" + " ";
			else
				start.Arguments += "-no_debug" + " ";

			if (project.SelectedConfig.ExecSettings.startPaused)
				start.Arguments += "-pause" + " ";
			else
				start.Arguments += "-no_pause" + " ";

			if (project.SelectedConfig.ExecSettings.syntaxHighlight)
				start.Arguments += "-highlight" + " ";
			else
				start.Arguments += "-no_highlight" + " ";

			if (project.SelectedConfig.ExecSettings.asciistack)
				start.Arguments += "-asciistack" + " ";
			else
				start.Arguments += "-no_asciistack" + " ";

			if (project.SelectedConfig.ExecSettings.skipnop)
				start.Arguments += "-skipnop" + " ";
			else
				start.Arguments += "-no_skipnop" + " ";

			start.Arguments += "-speed=" + project.SelectedConfig.ExecSettings.initialSpeedIndex + " ";

			start.Arguments += "-speed_1=" + project.SelectedConfig.ExecSettings.simuSpeeds[0] + " ";
			start.Arguments += "-speed_2=" + project.SelectedConfig.ExecSettings.simuSpeeds[1] + " ";
			start.Arguments += "-speed_3=" + project.SelectedConfig.ExecSettings.simuSpeeds[2] + " ";
			start.Arguments += "-speed_4=" + project.SelectedConfig.ExecSettings.simuSpeeds[3] + " ";
			start.Arguments += "-speed_5=" + project.SelectedConfig.ExecSettings.simuSpeeds[4] + " ";

			start.Arguments += "-decay=" + project.SelectedConfig.ExecSettings.decaytime + " ";

			if (project.SelectedConfig.ExecSettings.dodecay)
				start.Arguments += "-dodecay" + " ";
			else
				start.Arguments += "-no_dodecay" + " ";

			if (project.SelectedConfig.ExecSettings.zoomToDisplay && prog.HasDisplay)
			{
				TagLocation tagloc = cpiece.findTagSingle(typeof(Display_TopLeft_Tag));
				Display_TopLeft_Tag tag = tagloc.Tag as Display_TopLeft_Tag;

				start.Arguments += "-zoom=" + tagloc.X + "," + tagloc.Y + "," + (tagloc.X + tag.Width) + "," + (tagloc.Y + tag.Height) + " ";
			}

			start.FileName = "BefungExec.exe";

			try
			{
				Process.Start(start);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), ">> Excecution Error <<", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
		}

		private void StartEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			bool result = !hasErrors;

			e.CanExecute = result;

			if (cbxConfiguration != null)
			{
				cbxConfiguration.IsEnabled = result;
			}

			e.Handled = true;
		}

		#endregion

		#region Stop

		private void StopExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			//
		}

		private void StopEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;

			e.Handled = true;
		}

		#endregion

		#region ShowRunConfig

		private void ShowRunConfigExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			RunConfigurationManager rcm = new RunConfigurationManager(project);

			rcm.ShowDialog();

			updateUI();
		}

		private void ShowRunConfigEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;

			e.Handled = true;
		}

		#endregion

		#endregion

		#region Events

		private void codeEditor_TextChanged(object sender, EventArgs e)
		{
			if (project == null)
				return;

			project.Sourcecode = codeEditor.Text;
			project.DirtySourcecode();

			updateUI();
		}

		private void cbxConfiguration_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (project == null || supressComboBoxChangedEvent)
				return;

			project.ProjectConfig.SelectedConfiguration = cbxConfiguration.SelectedIndex;
			project.DirtyProject();

			updateUI();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (project.isDirty())
			{
				MessageBoxResult r = MessageBox.Show("There are unsaved changes. Save now ?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Information);

				switch (r)
				{
					case MessageBoxResult.Yes:
						if (!project.TrySave())
							e.Cancel = true;
						break;
					case MessageBoxResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			parseThreadRunning = false;
		}

		#endregion

		#region Loading

		private void DoOpen()
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Filter = "TextFungeProject (.tfp)|*.tfp";
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;

			if (ofd.ShowDialog().GetValueOrDefault(false))
			{
				TextFungeProjectWrapper pw = TextFungeProjectWrapper.LoadFromFile(ofd.FileName);

				if (pw == null)
				{
					MessageBox.Show("Could not load ProjectFile", "Error while loading", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				if (!File.Exists(pw.getAbsoluteSourceCodePath()))
				{
					MessageBox.Show("Sourcecodefile not found", "Error while loading", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				project = pw;

				updateUI();
				pw.ClearDirty();
				updateUI();

			}
		}

		#endregion

		#region UpdateUI

		private void updateUI()
		{
			supressComboBoxChangedEvent = true;
			cbxConfiguration.Items.Clear();
			project.ProjectConfig.Configurations.ForEach(p => cbxConfiguration.Items.Add(p));
			cbxConfiguration.SelectedIndex = project.ProjectConfig.SelectedConfiguration;
			supressComboBoxChangedEvent = false;


			if (codeEditor.Text != project.Sourcecode)
				codeEditor.Text = project.Sourcecode;

			this.Title = (String.IsNullOrWhiteSpace(project.ProjectConfigPath) ? "New" : Path.GetFileNameWithoutExtension(project.ProjectConfigPath)) + " - BefunWrite";

			dockCodeEditor.Title = (String.IsNullOrWhiteSpace(project.ProjectConfig.SourceCodePath) ? "New" : Path.GetFileName(project.ProjectConfig.SourceCodePath)) + (project.isDirty() ? "*" : "");
		}

		#endregion

		#region Background Parsing

		private string getTSCode()
		{
			string t = "<>";
			System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate { t = codeEditor.Text; });
			return t;
		}

		private void work()
		{
			string currentTxt = null;

			while (parseThreadRunning)
			{
				string newtxt = getTSCode();

				if (newtxt != currentTxt)
				{
					bool hasChangedAgain = true;
					while (hasChangedAgain)
					{
						Thread.Sleep(PARSE_WAIT_TIME);
						string newnewtxt = getTSCode();
						hasChangedAgain = (newnewtxt != newtxt);
						newtxt = newnewtxt;
					}

					DoThreadedErrorParse(newtxt);

					currentTxt = newtxt;
				}
				else
				{
					Thread.Sleep(100);
				}
			}
		}

		private void DoThreadedErrorParse(string code)
		{
			Debug.WriteLine("DoThreadedErrorParse(..)");

			BefunGenException error;
			Program program;

			if (Parser.TryParse(code, out error, out program))
			{
				System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
				{
					txtErrorList.Text = "";
					IconBar.SetError(-1, "");
					hasErrors = false;
				});
			}
			else
			{
				System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
				{
					txtErrorList.Text = error.getWellFormattedString();
					IconBar.SetError(error.Position.Line, error.ToPopupString());
					hasErrors = true;
				});

			}

			System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
			{
				PopulateSourceTree(program);
			});
		}

		private void PopulateSourceTree(Program p)
		{
			ClickableTreeViewItem root;

			if (p == null)
			{
				root = new ClickableTreeViewItem(IconBar, "<Error>", null, false);
			}
			else
			{
				root = new ClickableTreeViewItem(IconBar, p.getWellFormattedHeader(), null, true);

				if (p.Variables.Count > 0)
				{
					ClickableTreeViewItem globVars = new ClickableTreeViewItem(IconBar, String.Format("Global Variables ({0})", p.Variables.Count), null, true);
					globVars.Header = "Global Variables";
					globVars.IsExpanded = true;
					{
						foreach (VarDeclaration v in p.Variables)
						{
							globVars.Items.Add(new ClickableTreeViewItem(IconBar, v.getWellFormattedDecalaration(), v.Position, true));
						}
					}
					root.Items.Add(globVars);
				}

				if (p.Constants.Count > 0)
				{
					ClickableTreeViewItem constants = new ClickableTreeViewItem(IconBar, String.Format("Constants ({0})", p.Constants.Count), null, true);
					{
						foreach (VarDeclaration v in p.Constants)
						{
							constants.Items.Add(new ClickableTreeViewItem(IconBar, String.Format("{0} = {1}", v.getWellFormattedDecalaration(), v.Initial.getDebugString()), v.Position, true));
						}
					}
					root.Items.Add(constants);
				}

				ClickableTreeViewItem methods = new ClickableTreeViewItem(IconBar, String.Format("Methods ({0})", p.MethodList.Count), null, true);
				{
					foreach (Method v in p.MethodList)
					{
						ClickableTreeViewItem meth = new ClickableTreeViewItem(IconBar, v.getWellFormattedHeader(), v == p.MainMethod ? p.Position : v.Position, false);
						{
							List<VarDeclaration> vars = v.Variables.Where(pp => !v.Parameter.Contains(pp)).ToList();

							if (vars.Count > 0)
							{
								ClickableTreeViewItem varitem = new ClickableTreeViewItem(IconBar, "Variables", null, true);
								{
									foreach (VarDeclaration vv in vars)
									{
										varitem.Items.Add(new ClickableTreeViewItem(IconBar, vv.getWellFormattedDecalaration(), vv.Position, true));
									}
								}
								meth.Items.Add(varitem);
							}
						}
						methods.Items.Add(meth);
					}
				}
				root.Items.Add(methods);
			}

			sourceTreeView.Items.Clear();
			sourceTreeView.Items.Add(root);
		}

		#endregion

	}
}


//TODO Check Licenses:
// AvalonEdit
// AvalonDock
// GOLDTools
// OpenTK
// QuickFont
// SyntaxBox (?)
// Fugue Icon Set
// TwoColumnGrid (http://www.codeproject.com/Articles/238307/A-Two-Column-Grid-for-WPF)