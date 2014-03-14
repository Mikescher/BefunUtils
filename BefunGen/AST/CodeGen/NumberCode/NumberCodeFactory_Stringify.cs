using BefunGen.AST.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST.CodeGen.NumberCode
{
	public class NumberCodeFactory_Stringify
	{
		private enum StripOp { Add, Mult }

		private const char MIN_ASCII = ' '; // 32
		private const char MAX_ASCII = '~'; // 126

		public static CodePiece generateCode(int Value, bool reversed)
		{
			CodePiece p = generateCode(Value);

			if (p == null)
				return null;

			if (reversed)
				p.reverseX(false);
			return p;
		}

		public static CodePiece generateCode(int lit)
		{
			if (lit < 0)
			{
				CodePiece p = generateCode(-lit);
				if (p == null)
					return null;
				p.AppendLeft(BCHelper.Digit_0);
				p.AppendRight(BCHelper.Sub);
				p.normalizeX();
				return p;
			}

			if (lit >= 0 && lit <= 9)
			{
				return new CodePiece(BCHelper.dig((byte)lit));
			}

			if (lit < MIN_ASCII && lit >= (MIN_ASCII - 9))
			{
				if (lit + 9 == '"')
				{
					CodePiece p = generateCode(lit + 8);
					if (p == null)
						return null;
					p.AppendRight(BCHelper.Digit_8);
					p.AppendRight(BCHelper.Sub);
					return p;
				}
				else
				{
					CodePiece p = generateCode(lit + 9);
					if (p == null)
						return null;
					p.AppendRight(BCHelper.Digit_9);
					p.AppendRight(BCHelper.Sub);
					return p;
				}

			}

			if (lit < (MIN_ASCII - 9))
			{
				return null;
			}

			List<char> str;
			List<StripOp> ops;

			if (calculateStringOps(out str, out ops, lit))
			{
				CodePiece p = new CodePiece();

				p.AppendRight(BCHelper.Stringmode);
				foreach (char c in str)
					p.AppendRight(BCHelper.chr(c));
				p.AppendRight(BCHelper.Stringmode);

				for (int i = 0; i < ops.Count; i++)
				{
					switch (ops[i])
					{
						case StripOp.Add:
							p.AppendRight(BCHelper.Add);
							break;
						case StripOp.Mult:
							p.AppendRight(BCHelper.Mult);
							break;
						default:
							throw new WTFException();
					}
				}

				return p;
			}

			return null;
		}

		private static bool calculateStringOps(out List<char> str, out List<StripOp> ops, int val)
		{
			if (val < MIN_ASCII)
			{
				ops = null;
				str = null;
				return false;
			}

			//##########################################################################

			if (val >= MIN_ASCII && val <= MAX_ASCII && val != '"')
			{
				ops = new List<StripOp>();
				str = new List<char>() { (char)val };
				return true;
			}

			//##########################################################################

			for (char curr = MAX_ASCII; curr >= MIN_ASCII; curr--)
			{
				if (val % curr == 0 && val / curr > MIN_ASCII)
				{
					List<char> o_str;
					List<StripOp> o_ops;

					if (calculateStringOps(out o_str, out o_ops, val / curr))
					{
						str = o_str.ToList();
						ops = o_ops.ToList();

						str.Insert(0, curr);
						ops.Add(StripOp.Mult);

						return true;
					}
				}
			}

			//##########################################################################


			for (char curr = MAX_ASCII; curr >= MIN_ASCII; curr--)
			{
				if (curr == '"')
					continue;

				List<char> o_str;
				List<StripOp> o_ops;

				if (calculateStringOps(out o_str, out o_ops, val - curr))
				{
					str = o_str.ToList();
					ops = o_ops.ToList();

					str.Insert(0, curr);
					ops.Add(StripOp.Add);

					return true;
				}
			}

			str = null;
			ops = null;
			return false;
		}
	}
}
