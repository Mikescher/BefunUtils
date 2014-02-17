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
					commandArr.Add(Enumerable.Repeat(BCHelper.Empty, Height).ToList());

					MaxX++;
				}

				return true;
			}
			else if (x < MinX)
			{
				int newMinX = x;

				while (MinX > newMinX)
				{
					commandArr.Insert(0, Enumerable.Repeat(BCHelper.Empty, Height).ToList());

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
						commandArr[xw].Add(BCHelper.Empty);
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
						commandArr[xw].Insert(0, BCHelper.Empty);
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

			if (commandArr[x - MinX][y - MinY].Type != BefungeCommandType.NOP)
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
			normalizeX();
			normalizeY();
		}

		private void normalizeY()
		{
			int oy = -MinY;
			MinY += oy;
			MaxY += oy;
		}

		private void normalizeX()
		{
			int ox = -MinX;
			MinX += ox;
			MaxX += ox;
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
			CodePiece c_l = left.copy();
			CodePiece c_r = right.copy();

			c_l.normalizeX();
			c_r.normalizeX();

			int offset = c_l.Width;

			for (int x = c_r.MinX; x < c_r.MaxX; x++)
			{
				for (int y = c_r.MinY; y < c_r.MaxY; y++)
				{
					c_l[offset + x, y] = c_r[x, y];
				}
			}

			return c_l;
		}

		public static CodePiece CombineVertical(CodePiece top, CodePiece bottom)
		{
			CodePiece c_t = top.copy();
			CodePiece c_b = bottom.copy();

			c_t.normalizeY();
			c_b.normalizeY();

			int offset = c_t.Height;

			for (int x = c_b.MinX; x < c_b.MaxX; x++)
			{
				for (int y = c_b.MinY; y < c_b.MaxY; y++)
				{
					c_t[x, offset + y] = c_b[x, y];
				}
			}

			return c_t;
		}

		public bool IsHFlat() // Is Horizontal Flat
		{
			return Height == 1;
		}

		public bool IsVFlat() // Is Vertical Flat
		{
			return Width == 1;
		}

		public void RemoveColumn(int col)
		{
			int abs = col - MinX;

			commandArr.RemoveAt(abs);

			MaxX = MaxX - 1;
		}

		public void RemoveRow(int row)
		{
			int abs = row - MinY;

			for (int i = 0; i < Width; i++)
			{
				commandArr[i].RemoveAt(abs);
			}

			MaxY = MaxY - 1;
		}

		public bool lastRowIsSingle()
		{
			return IsRowSingle(Width - 1);
		}

		public bool firstRowIsSingle()
		{
			return IsRowSingle(0);
		}

		public bool IsRowSingle(int r)
		{
			return commandArr[r].Count(p => p.Type != BefungeCommandType.NOP) == 1;
		}
	}
}