using BefungExec.Logic;
using BefungExec.View;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace BefungExec
{
	class Program
	{
		private static string demo = Properties.Resources.demoProg;

		static void Main(string[] args)
		{
			string code;

			parseParams(args, out code);

			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("########## KEYS ##########");
			Console.WriteLine();
			Console.WriteLine("Space:   Pause | Resume");
			Console.WriteLine("Right:   Step Forward");
			Console.WriteLine("Mouse:   Zoom in | Breakpoint");
			Console.WriteLine("Esc:     Zoom out | Exit");
			Console.WriteLine("R:       Reset");
			Console.WriteLine("1:       Debug speed");
			Console.WriteLine("2:       Normal speed");
			Console.WriteLine("3:       High speed");

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("########## OUTPUT ##########");

			//###########

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			BefunProg bp = new BefunProg(GetProg(code));
			new Thread(new ThreadStart(bp.run)).Start();

			MainView mv = new MainView(bp, code);
		}

		private static CommandLineArguments parseParams(string[] args, out string code)
		{
			CommandLineArguments cmda = new CommandLineArguments(args);

			if (cmda.isEmpty())
			{
				Console.WriteLine("########## Parameter ##########");
				Console.WriteLine();
				Console.WriteLine("pause | no_pause           : Start Interpreter paused");
				Console.WriteLine("highlight | no_highlight   : Enable Syntax-Highlighting");
				Console.WriteLine("skipnop | no_skipnop       : Skip NOP's");
				Console.WriteLine("debug | no_debug           : Activates additional debug-messages");
				Console.WriteLine("topspeed=?                 : Set the speed (ms) for speed-3");
				Console.WriteLine("speed=?                    : Set the speed (ms) for speed-2");
				Console.WriteLine("lowspeed=?                 : Set the speed (ms) for speed-1");
				Console.WriteLine("decay=?                    : Time (ms) for the decay effect");
				Console.WriteLine("zoom=?,?,?,?               : Initial zoom position (x1, y1, x2, y2)");
				Console.WriteLine("file=?                     : The file to execute");
				Console.WriteLine();
			}

			//############################

			if (cmda.IsSet("no_pause"))
				RunOptions.INIT_PAUSED = false;
			if (cmda.IsSet("pause"))
				RunOptions.INIT_PAUSED = true;

			//##############

			if (cmda.IsSet("highlight"))
				RunOptions.SYNTAX_HIGHLIGHTING = true;
			if (cmda.IsSet("no_highlight"))
				RunOptions.SYNTAX_HIGHLIGHTING = false;

			//##############

			if (cmda.IsSet("no_skipnop") || cmda.IsSet("executenop"))
				RunOptions.SKIP_NOP = false;
			if (cmda.IsSet("skipnop"))
				RunOptions.SKIP_NOP = true;

			//##############

			if (cmda.IsSet("no_debug") || cmda.IsSet("no_debugrun"))
				RunOptions.DEBUGRUN = false;
			if (cmda.IsSet("debug") || cmda.IsSet("debugrun"))
				RunOptions.DEBUGRUN = true;

			//##############

			if (cmda.IsInt("topspeed"))
			{
				RunOptions.TOP_SLEEP_TIME = int.Parse(cmda["topspeed"]);
			}

			if (cmda.IsInt("speed"))
			{
				RunOptions.SLEEP_TIME = int.Parse(cmda["speed"]);
			}

			if (cmda.IsInt("lowspeed"))
			{
				RunOptions.LOW_SLEEP_TIME = int.Parse(cmda["lowspeed"]);
			}

			//##############

			if (cmda.IsInt("decay"))
				RunOptions.DECAY_TIME = int.Parse(cmda["decay"]);

			//##############

			if (cmda.IsSet("zoom"))
			{
				int tmp;
				string[] zooms = cmda["zoom"].Split(','); // zoom=X1,Y1,X2,Y2
				if (zooms.Length == 4 && zooms.All(p => int.TryParse(p, out tmp)))
				{
					RunOptions.INIT_ZOOM = new Rect2i(
						int.Parse(zooms[0]),
						int.Parse(zooms[1]),
						int.Parse(zooms[2]) - int.Parse(zooms[0]),
						int.Parse(zooms[3]) - int.Parse(zooms[1]));
				}
			}

			//##############

			if (cmda.IsSet("file"))
			{
				try
				{
					string fn = cmda["file"].Trim('"');
					code = File.ReadAllText(fn);
				}
				catch (Exception e)
				{
					Console.Out.WriteLine(e.ToString());
					code = "";
				}
			}
			else
			{
				Console.WriteLine("########## FILE NOT FOUND ##########");

				Console.WriteLine("Please pass a BefungeFile with the parameter '-file'");

				Console.WriteLine("Using Demo ...");

				code = demo;
				//RunOptions.DEBUGRUN = false; // Please do not debug demo :/
			}

			Console.WriteLine();
			Console.WriteLine("Actual arguments:");
			Array.ForEach(args, p => Console.WriteLine(p));
			Console.WriteLine();
			return cmda;
		}

		private static int GetProgWidth(string pg)
		{
			return Regex.Split(pg, @"\r\n").Max(s => s.Length);
		}

		private static int GetProgHeight(string pg)
		{
			return Regex.Split(pg, @"\r\n").Length;
		}

		public static int[,] GetProg(string pg)
		{
			int w, h;

			int[,] prog = new int[w = GetProgWidth(pg), h = GetProgHeight(pg)];

			string[] split = Regex.Split(pg, @"\r\n");

			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					prog[x, y] = (x < split[y].Length) ? split[y][x] : (int)' ';
				}
			}

			return prog;
		}
	}
}
