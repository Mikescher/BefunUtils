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
			string code = demo;

			parseParams(args, ref code);

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

			MainView mv = new MainView(bp);
		}

		private static CommandLineArguments parseParams(string[] args, ref string code)
		{
			CommandLineArguments cmda = new CommandLineArguments(args);

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

			if (cmda.IsInt("speed"))
			{
				RunOptions.SLEEP_TIME = int.Parse(cmda["speed"]);
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
				}
			}
			else
			{
				Console.WriteLine("Please pass a BefungeFile with the parameter '-file'");

				Console.WriteLine("Using Demo ...");
			}

			Console.WriteLine();
			Console.WriteLine("Actual arguments:");
			Array.ForEach(args, p => Console.WriteLine(p));
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

		private static int[,] GetProg(string pg)
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
