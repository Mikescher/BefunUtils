﻿using BefunGen.AST.CodeGen;
using System.Collections.Generic;

namespace BefunWrite
{
	public class ProjectCodeGenOptions
	{
		public string Name;
		public CodeGenOptions Options;

		public override string ToString()
		{
			return Name;
		}
	}

	/// <summary>
	/// The Class that represents the JSON Config File
	/// </summary>
	public class TextFungeProject
	{
		public string SourceCodePath = "";
		public string OutputPath = "";

		public int SelectedConfiguration = -1;
		public List<ProjectCodeGenOptions> Configurations = new List<ProjectCodeGenOptions>();
	}
}