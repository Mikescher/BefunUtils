using System;
using System.Linq;
using System.Text;

namespace BefunGen
{
	class SquashHelper
	{
		private readonly char[,] commandGrid;
		private int width;
		private int height;

		public SquashHelper(string inputtext)
		{
			var input = inputtext.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

			width = input.Max(p => p.Length);
			height = input.Length;

			commandGrid = new char[width, height];

			for (int x = 0; x < width; x++)
				for (int y = 0; y < height; y++)
					commandGrid[x, y] = ' ';

			for (int i = 0; i < input.Length; i++)
				for (int j = 0; j < input[i].Length; j++)
					commandGrid[j, i] = input[i][j];
		}

		public void Squash()
		{
			for (int i = 0; i < width;)
			{
				if (ColIsEmpty(i))
				{
					DeleteCol(i);
					i = 0;
					continue;
				}
				i++;
			}

			for (int i = 0; i < height; )
			{
				if (RowIsEmpty(i))
				{
					DeleteRow(i);
					i = 0;
					continue;
				}
				i++;
			}
		}

		private bool ColIsEmpty(int x)
		{
			bool result = height > 0;

			for (int y = 0; y < height; y++)
			{
				result &= commandGrid[x, y] == ' ';
			}

			return result;
		}

		private bool RowIsEmpty(int y)
		{
			bool result = width > 0;

			for (int x = 0; x < width; x++)
			{
				result &= commandGrid[x, y] == ' ';
			}

			return result;
		}

		private void DeleteCol(int rep_x)
		{
			for (int x = rep_x; x < (width-1); x++)
				for (int y = 0; y < height; y++)
					commandGrid[x, y] = commandGrid[x + 1, y];

			width--;
		}

		private void DeleteRow(int rep_y)
		{
			for (int y = rep_y; y < (height - 1); y++)
				for (int x = 0; x < width; x++)
					commandGrid[x, y] = commandGrid[x, y + 1];

			height--;
		}

		public override string ToString()
		{
			StringBuilder builderAll = new StringBuilder();
			for (int y = 0; y < height; y++)
			{
				StringBuilder builderLine = new StringBuilder();
				for (int x = 0; x < width; x++)
					builderLine.Append(commandGrid[x, y]);
				builderAll.AppendLine(builderLine.ToString().TrimEnd());
			}

			return builderAll.ToString();
		}
	}
}
