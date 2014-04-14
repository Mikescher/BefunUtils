using BefunWrite.Dialogs;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace BefunWrite
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class EditorWindow : Window
	{
		private TextFungeProjectWrapper project = null; // Is Set in constructor

		private bool supressComboBoxChangedEvent = false;

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
		}

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
						if (!DoSave(false))
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
			if (DoSave(false))
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
			if (DoSave(true))
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
						if (!DoSave(false))
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
			//
		}

		private void BuildEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;

			e.Handled = true;
		}

		#endregion

		#region Start

		private void StartExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			//
		}

		private void StartEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;

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

		#endregion

		private bool DoSave(bool savenew)
		{
			if (savenew || String.IsNullOrWhiteSpace(project.ProjectConfigPath))
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.AddExtension = true;
				sfd.DefaultExt = ".tfp";
				sfd.Filter = "TextFungeProject (.tfp)|*.tfp";

				if (sfd.ShowDialog().GetValueOrDefault(false))
				{
					if (File.Exists(sfd.FileName))
					{
						if (MessageBox.Show("File already Exists. Override ?", "File exists", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) != MessageBoxResult.Yes)
						{
							return false;
						}
					}

					project.ProjectConfigPath = sfd.FileName;
				}
				else
				{
					return false;
				}
			}

			if (savenew || String.IsNullOrWhiteSpace(project.ProjectConfig.SourceCodePath))
			{
				string prev = project.ProjectConfig.SourceCodePath;

				string relativepath = Path.GetFileNameWithoutExtension(project.ProjectConfigPath) + ".tf";
				project.ProjectConfig.SourceCodePath = relativepath;

				if (File.Exists(project.getAbsoluteSourceCodePath()))
				{
					if (MessageBox.Show(String.Format("File '{0}' already Exists. Override ?", Path.GetFileName(project.getAbsoluteSourceCodePath())), "File exists", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) != MessageBoxResult.Yes)
					{
						project.ProjectConfig.SourceCodePath = prev;
						return false;
					}
				}
			}

			return project.TrySave();
		}

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