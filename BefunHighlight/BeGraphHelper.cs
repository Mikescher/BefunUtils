using System;
using System.Linq;

namespace BefunHighlight
{
	public static class BeGraphHelper
	{
		private static int GetProgWidth(string pg)
		{
			return pg.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).Max(s => s.Length);
		}

		private static int GetProgHeight(string pg)
		{
			return pg.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).Length;
		}

		public static BeGraphCommand[,] parse(string pg, out int w, out int h)
		{
			BeGraphCommand[,] prog = new BeGraphCommand[w = GetProgWidth(pg), h = GetProgHeight(pg)];

			string[] split = pg.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					prog[x, y] = (x < split[y].Length) ? BeGraphCommand.getCommand(split[y][x]) : new BeGraphCommand(BeGraphCommandType.NOP);
				}
			}

			return prog;
		}

		public static BeGraphCommand[,] parse(int[,] raster)
		{
			int w;
			int h;
			BeGraphCommand[,] prog = new BeGraphCommand[w = raster.GetLength(0), h = raster.GetLength(1)];

			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					prog[x, y] = BeGraphCommand.getCommand(raster[x, y]);
				}
			}

			return prog;
		}

		public static BeGraphCommand[,] parse(long[,] raster)
		{
			int w;
			int h;
			BeGraphCommand[,] prog = new BeGraphCommand[w = raster.GetLength(0), h = raster.GetLength(1)];

			for (int y = 0; y < h; y++)
			{
				for (int x = 0; x < w; x++)
				{
					prog[x, y] = BeGraphCommand.getCommand(raster[x, y]);
				}
			}

			return prog;
		}

		public static BeGraphDirection getBeGraphDir(int delta_x, int delta_y)
		{
			BeGraphDirection d = BeGraphDirection.LeftRight;

			if (delta_x == -1)
				d = BeGraphDirection.RightLeft;
			else if (delta_x == 1)
				d = BeGraphDirection.LeftRight;
			else if (delta_y == -1)
				d = BeGraphDirection.BottomTop;
			else if (delta_y == 1)
				d = BeGraphDirection.TopBottom;

			return d;
		}
	}
}
