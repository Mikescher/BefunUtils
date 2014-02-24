﻿using BefunGen.AST.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen
{
	public class CodePiece
	{
		public int MinX { get; private set; } // Minimal ::> Inclusive

		public int MinY { get; private set; }

		public int MaxX { get; private set; } // Maximal + 1 ::> Exclusive

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
					commandArr.Add(Enumerable.Repeat(BCHelper.Unused, Height).ToList());

					MaxX++;
				}

				return true;
			}
			else if (x < MinX)
			{
				int newMinX = x;

				while (MinX > newMinX)
				{
					commandArr.Insert(0, Enumerable.Repeat(BCHelper.Unused, Height).ToList());

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
						commandArr[xw].Add(BCHelper.Unused);
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
						commandArr[xw].Insert(0, BCHelper.Unused);
					}

					MinY--;
				}
			}

			return false;
		}

		private void set(int x, int y, BefungeCommand value)
		{
			if (!IsIncluded(x, y))
				expand(x, y);

			if (commandArr[x - MinX][y - MinY].Type != BefungeCommandType.NOP)
				throw new InvalidCodeManipulationException("Modification of CodePiece : " + x + "|" + y);

			if (hasTag(value.Tag))
				throw new InvalidCodeManipulationException(string.Format("Duplicate Tag in CodePiece : [{0},{1}] = '{2}' = [{3},{4}])",x, y, value.Tag.ToString(), findTag(value.Tag).Item2, findTag(value.Tag).Item3));

			commandArr[x - MinX][y - MinY] = value;
		}

		private BefungeCommand get(int x, int y)
		{
			if (IsIncluded(x, y))
				return commandArr[x - MinX][y - MinY];
			else 
				return BCHelper.Unused;
		}

		public Tuple<BefungeCommand, int, int> findTag(object tag)
		{
			for (int x = MinX; x < MaxX; x++)
			{
				for (int y = MinY; y < MaxY; y++)
				{
					if (this[x, y].Tag == tag)
						return Tuple.Create(this[x, y], x, y);
				}
			}

			return null;
		}

		public bool hasTag(object tag)
		{
			return tag != null && findTag(tag) != null;
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

		public void normalizeY()
		{
			int oy = -MinY;
			MinY += oy;
			MaxY += oy;
		}

		public void normalizeX()
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

			c_l.AppendRight(c_r);

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

		public void AppendLeft(BefungeCommand c)
		{
			AppendLeft(0, c);
		}

		public void AppendLeft(int row, BefungeCommand c)
		{
			CodePiece p = new CodePiece();
			p[0, row] = c;

			AppendLeft(p);
		}

		public void AppendLeft(CodePiece left)
		{
			left = left.copy();

			CodePiece compress_conn;
			if (CodeGenOptions.CompressHorizontalCombining && (compress_conn = doCompressHorizontally(left, this)) != null)
			{
				this.RemoveColumn(this.MinX);
				left.RemoveColumn(left.MaxX - 1);

				this.AppendLeftDirect(compress_conn);
			}

			AppendLeftDirect(left);
		}

		private void AppendLeftDirect(CodePiece left)
		{
			left.normalizeX();

			int offset = MinX - left.MaxX;

			for (int x = left.MinX; x < left.MaxX; x++)
			{
				for (int y = left.MinY; y < left.MaxY; y++)
				{
					this[offset + x, y] = left[x, y];
				}
			}
		}

		public void AppendRight(BefungeCommand c)
		{
			AppendRight(0, c);
		}

		public void AppendRight(int row, BefungeCommand c)
		{
			CodePiece p = new CodePiece();
			p[0, row] = c;

			AppendRight(p);
		}

		public void AppendRight(CodePiece right)
		{
			right = right.copy();

			CodePiece compress_conn;
			if (CodeGenOptions.CompressHorizontalCombining && (compress_conn = doCompressHorizontally(this, right)) != null)
			{
				this.RemoveColumn(this.MaxX - 1);
				right.RemoveColumn(right.MinX);

				this.AppendRightDirect(compress_conn);
			}

			AppendRightDirect(right);
		}

		private void AppendRightDirect(CodePiece right)
		{
			right.normalizeX();

			int offset = MaxX;

			for (int x = right.MinX; x < right.MaxX; x++)
			{
				for (int y = right.MinY; y < right.MaxY; y++)
				{
					this[offset + x, y] = right[x, y];
				}
			}
		}

		public CodePiece doCompressHorizontally(CodePiece l, CodePiece r)
		{
			if (l.Width == 0 || r.Width == 0)
				return null;

			CodePiece connect = new CodePiece();

			int x_l = l.MaxX - 1;
			int x_r = r.MinX;

			for (int y = Math.Min(l.MinY, r.MinY); y < Math.Max(l.MaxY, r.MaxY); y++)
			{
				object Tag = null;

				if (l[x_l, y].Tag != null && r[x_r, y].Tag != null)
				{
					return null; // Can't compress - two tags would need to be merged
				}

				Tag = l[x_l, y].Tag ?? r[x_r, y].Tag;

				if (l[x_l, y].Type == BefungeCommandType.NOP && r[x_r, y].Type == BefungeCommandType.NOP)
				{
					connect[0, y] = new BefungeCommand(BefungeCommandType.NOP, Tag);
				}
				else if (l[x_l, y].Type != BefungeCommandType.NOP && r[x_r, y].Type != BefungeCommandType.NOP) 
				{
					return null; // Can't compress - two commands are colliding
					// Wouldn't even work when they are the same (eg stringmode_toogle ord stack-manipulation can't be merged)
				}
				else if (l[x_l, y].Type != BefungeCommandType.NOP)
				{
					connect[0, y] = new BefungeCommand(l[x_l, y].Type, l[x_l, y].Param, Tag);
				}
				else if (r[x_r, y].Type != BefungeCommandType.NOP)
				{
					connect[0, y] = new BefungeCommand(r[x_r, y].Type, r[x_r, y].Param, Tag);
				}
				else
				{
					throw new WTFException();
				}
			}

			return connect;
		}
	}
}