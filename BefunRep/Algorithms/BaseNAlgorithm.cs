using System;
using System.Linq;
using System.Text;

namespace BefunRep.Algorithms
{
	/// <summary>
	/// Represents the numbers in (optimized) Base representation 
	/// => Not always "normalized" base representation 
	/// 
	/// Includes Base_2 - Base_9
	/// </summary>
	public class BaseNAlgorithm : RepAlgorithm
	{

		public override string get(long value)
		{
			return Enumerable.Range(2, 8).Select(p => get(value, p)).OrderBy(p => p.Length).First();
		}

		private string get(long value, int befbase)
		{
			StringBuilder p = new StringBuilder();

			if (value < 0)
				return getNegative(-value, befbase);
			else
				return getPositive(value, befbase);
		}

		private string getPositive(long value, int befbase)
		{
			StringBuilder p_num = new StringBuilder();

			StringBuilder p_op = new StringBuilder();

			string rep = ConvertToBase(value, befbase);

			bool skipM_base = false;
			for (int i = 0; i < rep.Length; i++)
			{
				int digit = rep[i] - '0';
				bool last = i == (rep.Length - 1);
				bool first = i == 0;

				if (first)
				{
					if (digit == 1 && !last)
					{
						p_num.Append(dig(befbase)); // Don't calculate 1 * $befbase ... directly write $befbase
						skipM_base = true;
					}
					else
					{
						p_num.Append(dig(digit));
					}
				}
				else
				{
					if (skipM_base)
					{
						skipM_base = false;
					}
					else
					{
						p_num.Append(dig(befbase));
						p_op.Append("*");
					}

					if (digit != 0) // if digit == 0 dont calculate +0
					{
						p_num.Append(dig(digit));
						p_op.Append("+");
					}
				}
			}

			return new String(p_num.ToString().ToCharArray().Reverse().ToArray()) + p_op.ToString();
		}

		private string getNegative(long value, int befbase)
		{
			return "0" + getPositive(value, befbase) + "-";
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
