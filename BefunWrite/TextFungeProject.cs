using BefunExec.Logic;
using BefunGen.AST.CodeGen;
using System.Collections.Generic;

namespace BefunWrite
{
	public enum SH_Mode
	{
		AUTOMATIC = 0,
		NONE = 1,
		SIMPLE = 2,
		EXTENDED = 3,
	}

	/// <summary>
	/// Class in JSON Config File
	/// </summary>
	public class BefunExecSettings
	{
		public bool startPaused;
		public SH_Mode syntaxHighlight;
		public bool asciistack;
		public bool follocursormode;
		public bool skipnop;
		public bool IsDebug;

		public int initialSpeedIndex;
		public int[] simuSpeeds = new int[5];

		public int decaytime;
		public bool dodecay;
		public bool zoomToDisplay;

		public static BefunExecSettings getBES_Debug()
		{
			return new BefunExecSettings()
			{
				startPaused = true,
				syntaxHighlight = SH_Mode.AUTOMATIC,
				asciistack = true,
				follocursormode = false,
				skipnop = true,
				IsDebug = true,

				initialSpeedIndex = 3,
				simuSpeeds = new int[5]
				{
					RunOptions.SLEEP_TIME_1,
					RunOptions.SLEEP_TIME_2,
					RunOptions.SLEEP_TIME_3,
					RunOptions.SLEEP_TIME_4,
					RunOptions.SLEEP_TIME_5,
				},

				decaytime = RunOptions.DECAY_TIME,
				dodecay = true,
				zoomToDisplay = false,
			};
		}

		public static BefunExecSettings getBES_Release()
		{
			return new BefunExecSettings()
			{
				startPaused = false,
				syntaxHighlight = SH_Mode.AUTOMATIC,
				asciistack = true,
				follocursormode = false,
				skipnop = true,
				IsDebug = false,

				initialSpeedIndex = 5,
				simuSpeeds = new int[5]
				{
					RunOptions.SLEEP_TIME_1,
					RunOptions.SLEEP_TIME_2,
					RunOptions.SLEEP_TIME_3,
					RunOptions.SLEEP_TIME_4,
					RunOptions.SLEEP_TIME_5,
				},

				decaytime = RunOptions.DECAY_TIME,
				dodecay = false,
				zoomToDisplay = false,
			};
		}
	}

	/// <summary>
	/// Class in JSON Config File
	/// </summary>
	public class ProjectCodeGenOptions
	{
		public string Name;
		public CodeGenOptions Options;

		public BefunExecSettings ExecSettings = new BefunExecSettings();

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
		public string DisplayValuePath = "";
		public string OutputPath = "";

		public int SelectedConfiguration = -1;
		public List<ProjectCodeGenOptions> Configurations = new List<ProjectCodeGenOptions>();
	}
}
