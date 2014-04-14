using BefunGen.AST.CodeGen;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BefunWrite
{
	public class TextFungeProjectWrapper
	{
		public bool Project_isDirty { get; private set; }
		public bool Sourcecode_isDirty { get; private set; }

		public string ProjectConfigPath;
		public TextFungeProject ProjectConfig;

		public string Sourcecode;

		private TextFungeProjectWrapper(string fp, TextFungeProject p)
		{
			ProjectConfig = p;
			ProjectConfigPath = fp;

			Project_isDirty = false;
			Sourcecode_isDirty = false;

			if (File.Exists(getAbsoluteSourceCodePath()))
				Sourcecode = File.ReadAllText(getAbsoluteSourceCodePath());
		}

		public static TextFungeProjectWrapper CreateNew()
		{
			TextFungeProjectWrapper w = new TextFungeProjectWrapper("", new TextFungeProject());

			w.ProjectConfig.SelectedConfiguration = 0;
			w.ProjectConfig.Configurations.Add(new ProjectCodeGenOptions
			{
				Name = "Debug",
				Options = CodeGenOptions.getCGO_Debug()
			});
			w.ProjectConfig.Configurations.Add(new ProjectCodeGenOptions
			{
				Name = "Release",
				Options = CodeGenOptions.getCGO_Release()
			});

			w.Project_isDirty = true;
			w.Sourcecode_isDirty = true;

			return w;
		}

		public static TextFungeProjectWrapper LoadFromFile(string path)
		{
			try
			{
				return new TextFungeProjectWrapper(path, JsonConvert.DeserializeObject<TextFungeProject>(File.ReadAllText(path)));
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
				return null;
			}
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

		public bool TrySave()
		{
			try
			{
				if (Project_isDirty)
					Save_projectfile();

				if (Sourcecode_isDirty)
					Save_sourcecode();

				return true;
			}
			catch (IOException)
			{
				MessageBox.Show("Error: File could not be saved", "Error while saving", MessageBoxButton.OK, MessageBoxImage.Error);

				return false;
			}

		}

		public void Save_projectfile()
		{
			if (!string.IsNullOrWhiteSpace(ProjectConfigPath))
			{
				string json = JsonConvert.SerializeObject(ProjectConfig, Formatting.Indented);

				File.WriteAllText(ProjectConfigPath, json);

				Project_isDirty = false;
			}
		}

		public void Save_sourcecode()
		{
			if (!string.IsNullOrWhiteSpace(getAbsoluteSourceCodePath()))
			{
				File.WriteAllText(getAbsoluteSourceCodePath(), Sourcecode);

				Sourcecode_isDirty = false;
			}
		}

		public bool isDirty()
		{
			return Sourcecode_isDirty || Project_isDirty;
		}

		public void DirtyProject()
		{
			Project_isDirty = true;
		}

		public void DirtySourcecode()
		{
			Sourcecode_isDirty = true;
		}

		public void ClearDirty()
		{
			Sourcecode_isDirty = false;
			Project_isDirty = false;
		}
	}
}
