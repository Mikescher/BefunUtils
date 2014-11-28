using BefunRep.FileHandling;
using System;
using System.Text;

namespace BefunRep.Algorithms
{
	public class Base9Algorithm : RepAlgorithm
	{

		public Base9Algorithm(RepresentationSafe s)
			: base(s)
		{
			//
		}

		public override string get(int lit)
		{
			StringBuilder p = new StringBuilder();

			if (lit < 0)
			{
				return "0" + getPositive(-lit) + "-";
			}
			else
			{
				return getPositive(lit);
			}
		}

		private string getPositive(int lit)
		{
			StringBuilder p = new StringBuilder();

			string rep = ConvertToBase(lit, 9);

			for (int i = 0; i < rep.Length; i++)
			{
				p.Append(rep[rep.Length - i - 1]);

				if (i + 1 != rep.Length)
					p.Append('9');
			}

			int count = rep.Length - 1;

			for (int i = 0; i < count; i++)
			{
				p.Append('*');
				p.Append('+');
			}

			return p.ToString();
		}

		private string ConvertToBase(long decimalNumber, int radix)
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
