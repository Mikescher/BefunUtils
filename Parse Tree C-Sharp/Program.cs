using BefunGen.AST.CodeGen;
using System;
using System.Diagnostics;

namespace BefunGen
{
	static class Program
	{

		[STAThread]
		static void Main()
		{
			DoTest();

			//Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new frmMain());
		}

		static void DoTest()
		{
			CodePiece cp = new CodePiece();

			cp.set(0, 0, new BefungeCommand(BefungeCommandType.Add));
			Debug.WriteLine(cp.ToString());
			Debug.WriteLine("");

			cp.set(0, 2, new BefungeCommand(BefungeCommandType.Add));
			Debug.WriteLine(cp.ToString());
			Debug.WriteLine("");

			cp.set(2, 0, new BefungeCommand(BefungeCommandType.Add));
			Debug.WriteLine(cp.ToString());
			Debug.WriteLine("");

			cp.set(2, 2, new BefungeCommand(BefungeCommandType.Add));
			Debug.WriteLine(cp.ToString());
			Debug.WriteLine("");

			cp.set(0, 0, new BefungeCommand(BefungeCommandType.Mult));
			Debug.WriteLine(cp.ToString());
			Debug.WriteLine("");

			//cp.set(0, -2, new BefungeCommand(BefungeCommandType.Add));
			//Console.WriteLine(cp.ToString());
			//Console.WriteLine("");

			//cp.set(-2, 0, new BefungeCommand(BefungeCommandType.Add));
			//Console.WriteLine(cp.ToString());
			//Console.WriteLine("");

			//cp.set(-2, -2, new BefungeCommand(BefungeCommandType.Add));
			//Console.WriteLine(cp.ToString());
			//Console.WriteLine("");
		}
	}
}
