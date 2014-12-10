using System;
using System.Collections.Generic;
using System.Text;

namespace BefunHighlight
{
	public class BeGraph
	{
		public readonly int Width;
		public readonly int Height;

		public int EffectiveWidth { get; private set; }
		public int EffectiveHeight { get; private set; }

		BeGraphCommand[,] last_cmds;

		public HighlightField[,] fields;

		public BeGraph(int w, int h)
		{
			Width = w;
			Height = h;

			EffectiveWidth = -1;
			EffectiveHeight = -1;

			fields = new HighlightField[w, h];
		}

		private void Reset(BeGraphCommand[,] cmds)
		{
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					fields[x, y] = new HighlightField(cmds[x, y]);

			EffectiveWidth = -1;
			EffectiveHeight = -1;
		}

		public void Calculate(BeGraphCommand[,] cmds)
		{
			Calculate(0, 0, BeGraphDirection.LeftRight, cmds);
		}

		public void Calculate(long _x, long _y, BeGraphDirection _d, BeGraphCommand[,] cmds, bool reset = true)
		{
			if (reset)
				Reset(cmds);

			last_cmds = cmds;

			if (cmds.GetLength(0) * cmds.GetLength(1) == 0)
				return;

			Stack<BeGraphCalculateOperation> ops = new Stack<BeGraphCalculateOperation>();
			ops.Push(new BeGraphCalculateOperation() { X = _x, Y = _y, D = _d });

			while (ops.Count > 0)
			{
				BeGraphCalculateOperation op = ops.Pop();
				BeGraphCommand cmd = cmds[op.X, op.Y];

				if (fields[op.X, op.Y].information[(int)op.D].is_set)
					continue;

				if (op.isDirectionStringMode)
				{
					if (cmd.getGraphOpType() == BeGraphOpType.Stringmode)
					{
						fields[op.X, op.Y].information[(int)op.D].hl_command = true;
						ops.Push(op.next_sm(Width, Height, op.D));
					}
					else
					{
						fields[op.X, op.Y].information[(int)op.D].hl_string = true;
						ops.Push(op.next(Width, Height, op.D));
					}

					fields[op.X, op.Y].information[(int)op.D].setDirection(op.D, true);
				}
				else
				{
					switch (cmd.getGraphOpType())
					{
						case BeGraphOpType.Descision:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;

							if (cmd.Type == BeGraphCommandType.If_Horizontal)
							{
								ops.Push(op.next(Width, Height, BeGraphDirection.LeftRight));
								ops.Push(op.next(Width, Height, BeGraphDirection.RightLeft));

								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.LeftRight, true);
								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.RightLeft, true);
							}
							else if (cmd.Type == BeGraphCommandType.If_Vertical)
							{
								ops.Push(op.next(Width, Height, BeGraphDirection.TopBottom));
								ops.Push(op.next(Width, Height, BeGraphDirection.BottomTop));

								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.TopBottom, true);
								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.BottomTop, true);
							}
							else if (cmd.Type == BeGraphCommandType.PC_Random)
							{
								ops.Push(op.next(Width, Height, BeGraphDirection.TopBottom));
								ops.Push(op.next(Width, Height, BeGraphDirection.BottomTop));
								ops.Push(op.next(Width, Height, BeGraphDirection.LeftRight));
								ops.Push(op.next(Width, Height, BeGraphDirection.RightLeft));

								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.LeftRight, true);
								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.RightLeft, true);
								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.TopBottom, true);
								fields[op.X, op.Y].information[(int)op.D].setDirection(BeGraphDirection.BottomTop, true);
							}
							else
								throw new Exception();
							break;
						case BeGraphOpType.DirectionChange:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;

							ops.Push(op.next(Width, Height, op.getDC(cmd)));

							fields[op.X, op.Y].information[(int)op.D].setDirection(op.getDC(cmd), true);
							break;
						case BeGraphOpType.Jump:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;

							ops.Push(op.next(Width, Height, op.D, 2));

							fields[op.X, op.Y].information[(int)op.D].setDirection(op.D, true);
							break;
						case BeGraphOpType.SimpleCommand:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;

							ops.Push(op.next(Width, Height, op.D));

							fields[op.X, op.Y].information[(int)op.D].setDirection(op.D, true);
							break;
						case BeGraphOpType.Stop:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;

							break;
						case BeGraphOpType.Stringmode:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;

							ops.Push(op.next_sm(Width, Height, op.D));

							fields[op.X, op.Y].information[(int)op.D].setDirection(op.D, true);
							break;
					}
				}
			}

			CalculateEffectiveSize();
		}

		public bool Update(long _x, long _y, BeGraphCommand cmd, long pos_x, long pos_y, int delta_x, int delta_y)
		{
			BeGraphDirection d = BeGraphHelper.getBeGraphDir(delta_x, delta_y);

			return Update(_x, _y, cmd, pos_x, pos_y, d);
		}

		public bool Update(long _x, long _y, BeGraphCommand cmd, long pos_x, long pos_y, BeGraphDirection d)
		{
			if (fields[_x, _y].getType() == HighlightType.NOP)
			{
				fields[_x, _y].command = cmd;
				last_cmds[_x, _y] = cmd;
				return false;
			}
			else
			{
				last_cmds[_x, _y] = cmd;

				Calculate(pos_x, pos_y, d, last_cmds, true);

				return true;
			}
		}

		public string toDebugString()
		{
			StringBuilder sb = new StringBuilder();

			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					sb.Append(fields[x, y].toDebugString());
				}
				sb.AppendLine();
			}

			return sb.ToString();
		}

		public void CalculateMid(BeGraphCommand[,] cmds, int _x, int _y, int delta_x, int delta_y)
		{
			Calculate(cmds);
			Calculate(_x, _y, BeGraphHelper.getBeGraphDir(delta_x, delta_y), cmds, false);
		}

		private void CalculateEffectiveSize()
		{
			EffectiveWidth = -1;
			EffectiveHeight = -1;

			for (int y = 0; y < Height; y++)
				for (int x = 0; x < Width; x++)
					if (fields[x, y].getType() != HighlightType.NOP)
					{
						EffectiveHeight = Math.Max(EffectiveHeight, y + 1);
						EffectiveWidth = Math.Max(EffectiveWidth, x + 1);
					}
		}

		public bool isEffectiveSizeCalculated()
		{
			return EffectiveHeight > 0 && EffectiveWidth > 0;
		}
	}
}
