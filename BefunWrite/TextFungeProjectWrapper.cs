using BefunGen.AST.CodeGen;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace BefunWrite
{
	public class TextFungeProjectWrapper
	{
		public bool Project_isDirty { get; private set; }
		public bool Sourcecode_isDirty { get; private set; }
		public bool DisplayVal_isDirty { get; private set; }

		public string ProjectConfigPath;
		public TextFungeProject ProjectConfig;

		public bool HasConfigSelected { get { return ProjectConfig.Configurations.ElementAtOrDefault(ProjectConfig.SelectedConfiguration) != null; } }
		public ProjectCodeGenOptions SelectedConfig { get { return ProjectConfig.Configurations.ElementAtOrDefault(ProjectConfig.SelectedConfiguration); } }

		public string Sourcecode;
		public string DisplayValue;

		private TextFungeProjectWrapper(string fp, TextFungeProject p)
		{
			ProjectConfig = p;
			ProjectConfigPath = fp;

			Project_isDirty = false;
			Sourcecode_isDirty = false;
			DisplayVal_isDirty = false;

			if (File.Exists(getAbsoluteSourceCodePath()))
				Sourcecode = File.ReadAllText(getAbsoluteSourceCodePath());

			if (File.Exists(getAbsoluteDisplayValuePath()))
				DisplayValue = File.ReadAllText(getAbsoluteDisplayValuePath());
		}

		public static TextFungeProjectWrapper CreateNew()
		{
			TextFungeProjectWrapper w = new TextFungeProjectWrapper("", new TextFungeProject());

			w.ProjectConfig.SelectedConfiguration = 0;
			w.ProjectConfig.Configurations.Add(new ProjectCodeGenOptions
			{
				Name = "Debug",
				ExecSettings = BefunExecSettings.getBES_Debug(),
				Options = CodeGenOptions.getCGO_Debug(),
			});
			w.ProjectConfig.Configurations.Add(new ProjectCodeGenOptions
			{
				Name = "Release",
				ExecSettings = BefunExecSettings.getBES_Release(),
				Options = CodeGenOptions.getCGO_Release()
			});

			w.Project_isDirty = true;
			w.Sourcecode_isDirty = true;
			w.DisplayVal_isDirty = true;

			return w;
		}

		public static TextFungeProjectWrapper LoadFromFile(string path) //throws Exception
		{
			return new TextFungeProjectWrapper(path, JsonConvert.DeserializeObject<TextFungeProject>(File.ReadAllText(path)));
		}

		public string getAbsoluteSourceCodePath()
		{
			if (String.IsNullOrWhiteSpace(ProjectConfigPath))
			{
				return "";
			}

			if (String.IsNullOrWhiteSpace(ProjectConfig.SourceCodePath))
			{
				return "";
			}

			return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(ProjectConfigPath), ProjectConfig.SourceCodePath));
		}

		public string getAbsoluteDisplayValuePath()
		{
			if (String.IsNullOrWhiteSpace(ProjectConfigPath))
			{
				return "";
			}

			if (String.IsNullOrWhiteSpace(ProjectConfig.DisplayValuePath))
			{
				return "";
			}

			return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(ProjectConfigPath), ProjectConfig.DisplayValuePath));
		}

		public string GetProjectName()
		{
			if (String.IsNullOrWhiteSpace(ProjectConfigPath))
			{
				return "";
			}
			else
			{
				return Path.GetFileNameWithoutExtension(ProjectConfigPath);
			}
		}

		public bool TrySave(bool forcenew = false)
		{
			try
			{

				bool s_p = true;
				if (Project_isDirty || forcenew)
					s_p = Save_projectfile(forcenew);

				if (!s_p)
					return false;

				bool s_s = true;
				if (Sourcecode_isDirty || forcenew)
					s_s = Save_sourcecode(forcenew);

				if (!s_s)
					return false;

				bool s_d = true;
				if (DisplayVal_isDirty || forcenew)
					s_s = Save_displayval(forcenew);

				if (!s_d)
					return false;

				bool s_p2 = true;
				if (Project_isDirty) // save_SC / save_DV Could make Projectfile dirty
					s_p = Save_projectfile(false);

				if (!s_p2)
					return false;

				return s_p && s_s && s_d && s_p2;
			}
			catch (IOException)
			{
				MessageBox.Show("Error: File could not be saved", "Error while saving", MessageBoxButton.OK, MessageBoxImage.Error);

				return false;
			}

		}

		public bool Save_projectfile(bool forcenew = false)
		{
			if (!string.IsNullOrWhiteSpace(ProjectConfigPath) && !forcenew)
			{
				string json = JsonConvert.SerializeObject(ProjectConfig, Formatting.Indented);

				File.WriteAllText(ProjectConfigPath, json);

				Project_isDirty = false;

				return true;
			}
			else
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.AddExtension = true;
				sfd.DefaultExt = ".tfp";
				sfd.Filter = "TextFungeProject |*.tfp";

				if (sfd.ShowDialog().GetValueOrDefault(false))
				{
					if (File.Exists(sfd.FileName))
					{
						if (MessageBox.Show("File already Exists. Override ?", "File exists", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) != MessageBoxResult.Yes)
						{
							return false;
						}
					}

					ProjectConfigPath = sfd.FileName;

					string json = JsonConvert.SerializeObject(ProjectConfig, Formatting.Indented);
					File.WriteAllText(ProjectConfigPath, json);

					DirtyProject();

					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public bool Save_sourcecode(bool forcenew = false)
		{
			if (!string.IsNullOrWhiteSpace(getAbsoluteSourceCodePath()) && !forcenew)
			{
				File.WriteAllText(getAbsoluteSourceCodePath(), Sourcecode);

				Sourcecode_isDirty = false;

				return true;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(ProjectConfigPath))
					return false;

				string prev = ProjectConfig.SourceCodePath;

				string relativepath = Path.GetFileNameWithoutExtension(ProjectConfigPath) + ".tf";
				ProjectConfig.SourceCodePath = relativepath;

				if (File.Exists(getAbsoluteSourceCodePath()))
				{
					if (MessageBox.Show(String.Format("File '{0}' already Exists. Override ?", Path.GetFileName(getAbsoluteSourceCodePath())), "File exists", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) != MessageBoxResult.Yes)
					{
						ProjectConfig.SourceCodePath = prev;
						return false;
					}
				}

				File.WriteAllText(getAbsoluteSourceCodePath(), Sourcecode);
				Sourcecode_isDirty = false;

				Project_isDirty = true;

				return true;
			}
		}

		public bool Save_displayval(bool forcenew = false)
		{
			if (!string.IsNullOrWhiteSpace(getAbsoluteDisplayValuePath()) && !forcenew)
			{
				File.WriteAllText(getAbsoluteDisplayValuePath(), DisplayValue);

				DisplayVal_isDirty = false;

				return true;
			}
			else
			{
				if (string.IsNullOrWhiteSpace(ProjectConfigPath))
					return false;

				string prev = ProjectConfig.DisplayValuePath;

				string relativepath = Path.GetFileNameWithoutExtension(ProjectConfigPath) + ".tfdv";
				ProjectConfig.DisplayValuePath = relativepath;

				if (File.Exists(getAbsoluteDisplayValuePath()))
				{
					if (MessageBox.Show(String.Format("File '{0}' already Exists. Override ?", Path.GetFileName(getAbsoluteDisplayValuePath())), "File exists", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) != MessageBoxResult.Yes)
					{
						ProjectConfig.DisplayValuePath = prev;
						return false;
					}
				}

				File.WriteAllText(getAbsoluteDisplayValuePath(), DisplayValue);
				DisplayVal_isDirty = false;

				Project_isDirty = true;

				return true;
			}
		}

		public bool isDirty()
		{
			return Sourcecode_isDirty || Project_isDirty || DisplayVal_isDirty;
		}

		public void DirtyProject()
		{
			Project_isDirty = true;
		}

		public void DirtySourcecode()
		{
			Sourcecode_isDirty = true;
		}

		public void DirtyDisplayValue()
		{
			DisplayVal_isDirty = true;
		}

		public void ClearDirty()
		{
			DisplayVal_isDirty = false;
			Sourcecode_isDirty = false;
			Project_isDirty = false;
		}
	}
}
