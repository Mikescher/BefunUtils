using System;
using System.Collections.Generic;
using System.Text;

namespace BefunHighlight
{
	public class BeGraph
	{
		public readonly int Width;
		public readonly int Height;

		BeGraphCommand[,] last_cmds;

		public HighlightField[,] fields;

		public BeGraph(int w, int h)
		{
			Width = w;
			Height = h;

			fields = new HighlightField[w, h];
		}

		private void Reset(BeGraphCommand[,] cmds)
		{
			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
					fields[x, y] = new HighlightField(cmds[x,y]);
		}

		public void Calculate(BeGraphCommand[,] cmds)
		{
			Calculate(0, 0, BeGraphDirection.LeftRight, cmds);
		}

		public void Calculate(int _x, int _y, BeGraphDirection _d, BeGraphCommand[,] cmds, bool reset = true)
		{
			if (reset)
				Reset(cmds);

			last_cmds = cmds;

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
							}
							else if (cmd.Type == BeGraphCommandType.If_Vertical)
							{
								ops.Push(op.next(Width, Height, BeGraphDirection.TopBottom));
								ops.Push(op.next(Width, Height, BeGraphDirection.BottomTop));
							}
							else if (cmd.Type == BeGraphCommandType.PC_Random)
							{
								ops.Push(op.next(Width, Height, BeGraphDirection.TopBottom));
								ops.Push(op.next(Width, Height, BeGraphDirection.BottomTop));
								ops.Push(op.next(Width, Height, BeGraphDirection.LeftRight));
								ops.Push(op.next(Width, Height, BeGraphDirection.RightLeft));
							}
							else
								throw new Exception();
							break;
						case BeGraphOpType.DirectionChange:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;
							ops.Push(op.next(Width, Height, op.getDC(cmd)));
							break;
						case BeGraphOpType.Jump:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;
							ops.Push(op.next(Width, Height, op.D, 2));
							break;
						case BeGraphOpType.SimpleCommand:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;
							ops.Push(op.next(Width, Height, op.D));
							break;
						case BeGraphOpType.Stop:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;
							break;
						case BeGraphOpType.Stringmode:
							fields[op.X, op.Y].information[(int)op.D].hl_command = true;
							ops.Push(op.next_sm(Width, Height, op.D));
							break;
					}
				}
			}
		}

		public bool Update(int _x, int _y, BeGraphCommand cmd, int pos_x, int pos_y, int delta_x, int delta_y)
		{
			BeGraphDirection d = BeGraphHelper.getBeGraphDir(delta_x, delta_y);

			return Update(_x, _y, cmd, pos_x, pos_y, d);
		}

		public bool Update(int _x, int _y, BeGraphCommand cmd, int pos_x, int pos_y, BeGraphDirection d)
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
	}
}
