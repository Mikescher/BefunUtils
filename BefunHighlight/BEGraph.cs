using System.Collections.Generic;

namespace BefunHighlight
{
	public class BEGraph
	{
		public readonly int Width;
		public readonly int Height;

		public HighlightField[,] fields;

		public BEGraph(int w, int h)
		{
			Width = w;
			Height = h;

			fields = new HighlightField[w, h];
		}

		private void Reset()
		{
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					fields[x, y] = new HighlightField();
		}

		public void Calculate(int _x, int _y, BeGraphDirection _d, BeGraphCommand[,] cmds)
		{
			Reset();

			Stack<BeGraphCalculateOperation> ops = new Stack<BeGraphCalculateOperation>();
			ops.Push(new BeGraphCalculateOperation() { X = _x, Y = _y, D = _d });

			while (ops.Count > 0)
			{

			}
		}
	}
}
