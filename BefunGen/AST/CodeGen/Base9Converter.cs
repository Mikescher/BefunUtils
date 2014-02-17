using System;

namespace BefunGen.AST.CodeGen
{
	class Base9Converter
	{
		public static CodePiece generateCodeForLiteral(int lit)
		{
			CodePiece p = new CodePiece();

			bool isneg;
			if (isneg = lit < 0)
			{
				lit *= -1;
			}

			string rep = ConvertToBase(lit, 9);
			int pos = 0;

			if (isneg)
			{
				p[pos++, 0] = BCHelper.Digit_0;
			}

			for (int i = 0; i < rep.Length; i++)
			{
				p[pos++, 0] = BCHelper.dig((byte)(rep[rep.Length - i - 1] - '0'));

				if (i + 1 != rep.Length)
					p[pos++, 0] = BCHelper.dig(9);
			}

			int count = rep.Length - 1;

			for (int i = 0; i < count; i++)
			{
				p[pos++, 0] = BCHelper.Mult;
				p[pos++, 0] = BCHelper.Add;
			}

			if (isneg)
			{
				p[pos++, 0] = BCHelper.Sub;
			}

			return p;
		}

		public static string ConvertToBase(long decimalNumber, int radix)
		{
			const int BitsInLong = 64;
			const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			if (radix < 2 || radix > Digits.Length)
				throw new ArgumentException("The radix must be >= 2 and <= " + Digits.Length.ToString());

			if (decimalNumber == 0)
				return "0";

			int index = BitsInLong - 1;
			long currentNumber = Math.Abs(decimalNumber);
			char[] charArray = new char[BitsInLong];

			while (currentNumber != 0)
			{
				int remainder = (int)(currentNumber % radix);
				charArray[index--] = Digits[remainder];
				currentNumber = currentNumber / radix;
			}

			string result = new String(charArray, index + 1, BitsInLong - index - 1);
			if (decimalNumber < 0)
			{
				result = "-" + result;
			}

			return result;
		}
	}
}
