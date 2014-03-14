using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunGen.AST.CodeGen.NumberCode
{
	public class NumberCodeFactory_Stringify
	{
		private const int MIN_ASCII = (int)' '; // 32
		private const int MAX_ASCII = (int)'~'; // 126

		public static CodePiece generateCode(int Value, bool reversed)
		{
			CodePiece p = generateCode(Value);
			if (reversed)
				p.reverseX(false);
			return p;
		}

		public static CodePiece generateCode(int lit)
		{
			if (lit < 0)
			{
				CodePiece p = generateCode(-lit);
				if (p == null) return null;
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
					if (p == null) return null;
					p.AppendRight(BCHelper.Digit_8);
					p.AppendRight(BCHelper.Sub);
					return p;
				}
				else
				{
					CodePiece p = generateCode(lit + 9);
					if (p == null) return null;
					p.AppendRight(BCHelper.Digit_9);
					p.AppendRight(BCHelper.Sub);
					return p;
				}
				
			}

			if (lit < (MIN_ASCII - 9))
			{
				return null;
			}

			for (int currASCII = MIN_ASCII; currASCII <= MAX_ASCII; currASCII++)
			{
				if (currASCII * currASCII == lit)
				{
					CodePiece p = new CodePiece();
					p[0, 0] = BCHelper.Stringmode;
					p[1, 0] = BCHelper.chr(currASCII);
					p[2, 0] = BCHelper.chr(currASCII);
					p[3, 0] = BCHelper.Stringmode;
					p[4, 0] = BCHelper.Mult;
					return p;
				}
			}

			List<char> str;
	
			if (calculateStringOps(out str, lit))
			{
				CodePiece p = new CodePiece();
	
				p.AppendRight(BCHelper.Stringmode);
				foreach (char c in str)
					p.AppendRight(BCHelper.chr(c));
				p.AppendRight(BCHelper.Stringmode);
	
				for (int i = 1; i < str.Count; i++)
					p.AppendRight(BCHelper.Add);

				return p;
			}

			return null;
		}

		private static bool calculateStringOps(out List<char> str, int val) 
		{
			if (val < MIN_ASCII)
			{
				str = null;
				return false;
			} 
			else if (val >= MIN_ASCII && val <= MAX_ASCII && val != '"')
			{
				str = new List<char>() { (char)val };
				return true;
			}
			else
			{
				for (int curr = MAX_ASCII; curr >= MIN_ASCII; curr--)
				{
					if (curr == '"')
						continue;

					List<char> o_str;

					if (calculateStringOps(out o_str, val - curr))
					{
						str = o_str.ToList(); ;
						str.Insert(0, (char)curr);
						return true;
					}
				}

				str = null;
				return false;
			}
		}
	}
}
