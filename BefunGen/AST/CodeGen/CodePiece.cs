using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen
{
	public class CodePiece
	{
		public int MinX { get; private set; } // Minimal

		public int MinY { get; private set; }

		public int MaxX { get; private set; } // Maximal + 1

		public int MaxY { get; private set; }

		public int Width { get { return MaxX - MinX; } }

		public int Height { get { return MaxY - MinY; } }

		private List<List<BefungeCommand>> commandArr = new List<List<BefungeCommand>>();

		public BefungeCommand this[int x, int y] { get { return get(x, y); } set { set(x, y, value); } }

		public CodePiece()
		{
			MinX = 0;
			MinY = 0;

			MaxX = 0;
			MaxY = 0;
		}

		private bool IsIncluded(int x, int y)
		{
			return x >= MinX && y >= MinY && x < MaxX && y < MaxY;
		}

		private bool expand(int x, int y)
		{
			bool ex = expandX(x);
			bool ey = expandY(y);

			return ex && ey;
		}

		private bool expandX(int x)
		{
			if (x >= MaxX) // expand Right
			{
				int newMaxX = x + 1;

				while (MaxX < newMaxX)
				{
					commandArr.Add(Enumerable.Repeat((BefungeCommand)null, Height).ToList());

					MaxX++;
				}

				return true;
			}
			else if (x < MinX)
			{
				int newMinX = x;

				while (MinX > newMinX)
				{
					commandArr.Insert(0, Enumerable.Repeat((BefungeCommand)null, Height).ToList());

					MinX--;
				}
			}

			return false;
		}

		private bool expandY(int y)
		{
			if (y >= MaxY) // expand Right
			{
				int newMaxY = y + 1;

				while (MaxY < newMaxY)
				{
					for (int xw = 0; xw < Width; xw++)
					{
						commandArr[xw].Add(null);
					}

					MaxY++;
				}

				return true;
			}
			else if (y < MinY)
			{
				int newMinY = y;

				while (MinY > newMinY)
				{
					for (int xw = 0; xw < Width; xw++)
					{
						commandArr[xw].Insert(0, null);
					}

					MinY--;
				}
			}

			return false;
		}

		public void set(int x, int y, BefungeCommand value)
		{
			if (!IsIncluded(x, y))
				expand(x, y);

			if (commandArr[x - MinX][y - MinY] != null)
				throw new Exception("[DBG] WARNING: Modification of CodePiece : " + x + "|" + y);

			commandArr[x - MinX][y - MinY] = value;
		}

		public BefungeCommand get(int x, int y)
		{
			if (IsIncluded(x, y))
				return commandArr[x - MinX][y - MinY];
			else
				return null;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.AppendLine(string.Format("{0}: [{1} - {2}, {3} - {4}] ({5}, {6})", this.GetType().Name, MinX, MaxX, MinY, MaxY, Width, Height));

			builder.AppendLine("{");
			for (int y = MinY; y < MaxY; y++)
			{
				for (int x = MinX; x < MaxX; x++)
				{
					BefungeCommand bc = this[x, y];
					if (bc == null)
						builder.Append("X");
					else
						builder.Append(bc.getCommandCode());
				}
				builder.AppendLine();
			}
			builder.AppendLine("}");

			return builder.ToString();
		}

		public void normalize()
		{
			int ox = -MinX;
			int oy = -MinY;

			MinX += ox;
			MinY += oy;

			MaxX += ox;
			MaxY += oy;
		}

		public CodePiece copy()
		{
			CodePiece result = new CodePiece();
			for (int x = 0; x < commandArr.Count; x++)
			{
				for (int y = 0; y < commandArr[x].Count; y++)
				{
					result[x, y] = commandArr[x][y];
				}
			}

			result.MinX = MinX;
			result.MinY = MinY;

			result.MaxX = MaxX;
			result.MaxY = MaxY;

			return result;
		}

		public CodePiece copyNormalized()
		{
			CodePiece result = copy();
			result.normalize();
			return result;
		}

		public static CodePiece CombineHorizontal(CodePiece left, CodePiece right)
		{
			// TODO Dont normalize - Add resprective to Y-Offset (IS IMPORTANT !!)
			CodePiece c_l = left.copyNormalized();
			CodePiece c_r = right.copyNormalized();

			int offset = c_l.Width;

			for (int x = 0; x < c_r.Width; x++)
			{
				for (int y = 0; y < c_r.Height; y++)
				{
					c_l[offset + x, y] = c_r[x, y];
				}
			}

			return c_l;
		}

		public static CodePiece CombineVertical(CodePiece top, CodePiece bottom)
		{
			// TODO Dont normalize - Add resprective to X-Offset (IS IMPORTANT !!)
			CodePiece c_t = top.copyNormalized();
			CodePiece c_b = bottom.copyNormalized();

			int offset = c_t.Height;

			for (int x = 0; x < c_b.Width; x++)
			{
				for (int y = 0; y < c_b.Height; y++)
				{
					c_t[x, offset + y] = c_b[x, y];
				}
			}

			return c_t;
		}
	}
}