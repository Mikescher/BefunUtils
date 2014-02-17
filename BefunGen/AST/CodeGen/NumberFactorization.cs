using BefunGen.AST.Exceptions;
using System.Collections.Generic;

namespace BefunGen.AST.CodeGen
{
	class NumberFactorization
	{
		public static CodePiece generateCodeForLiteral(int lit)
		{
			bool isneg;
			if (isneg = lit < 0)
			{
				lit *= -1;
			}

			CodePiece p = new CodePiece();

			if (lit == 0)
			{
				p[0, 0] = BCHelper.Digit_0;
				return p;
			}

			if (isneg)
			{
				p.AppendRight(BCHelper.Digit_0);
			}

			getFactors(p, lit);

			if (isneg)
			{
				p.AppendRight(BCHelper.Sub);
			}

			return p;
		}

		private static void getFactors(CodePiece p, int a) // Wenn nicht möglich so gut wie mögl und am ende add
		{
			List<int> result = new List<int>();

			if (a < 10)
			{
				p.AppendRight(BCHelper.dig((byte)a));
				return;
			}

			for (byte i = 9; i > 1; i--)
			{
				if (a % i == 0)
				{
					getFactors(p, a / i);
					p.AppendRight(BCHelper.dig(i));
					p.AppendRight(BCHelper.Mult);
					return;
				}
			}

			for (byte i = 1; i < 10; i++)
			{
				if ((a - i) % 9 == 0)
				{
					getFactors(p, a - i);
					p.AppendRight(BCHelper.dig(i));
					p.AppendRight(BCHelper.Add);
					return;
				}
			}

			throw new WTFException();
		}
	}
}
