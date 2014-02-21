using BefungExec.Logic;
using BefungExec.View;
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

			CommandLineArguments cmda = new CommandLineArguments(args);

			if (cmda.IsSet("speed") && cmda.IsInt("speed"))
				BefunProg.SLEEP_TIME = int.Parse(cmda["speed"]);

			if (cmda.IsSet("decay") && cmda.IsInt("decay"))
				BefunProg.DECAY_SPEED = int.Parse(cmda["decay"]);

			if (cmda.IsSet("file"))
			{
				try
				{
					code = File.ReadAllText(cmda["file"]);
				}
				catch (Exception e)
				{
					Console.Out.WriteLine(e.ToString());
				}
			}

			BefunProg.INIT_PAUSED = !cmda.IsSet("no_pause");

			//###########

			BefunProg bp = new BefunProg(GetProg(demo));
			new Thread(new ThreadStart(bp.run)).Start();

			MainView mv = new MainView(bp);
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
