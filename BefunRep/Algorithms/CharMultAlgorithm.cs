
using System;
namespace BefunRep.Algorithms
{
	/// <summary>
	/// Tries to represent numbers as stringmode multiplications
	/// Not possible for all numbers
	/// </summary>
	public class CharMultAlgorithm : RepAlgorithm
	{
		public CharMultAlgorithm(byte aid)
			: base(aid)
		{
			// NOP
		}

		public override string get(long value)
		{
			if (value >= 0)
			{
				return getPositive(value);
			}
			else
			{
				string neg = getPositive(-value);

				if (neg == null)
					return null;

				return "0" + neg + "-";
			}
		}

		private string getPositive(long value)
		{
			string current = getMultiplicands(value);
			if (current != null)
				current = "" + '"' + current + '"' + "*".Repeat(current.Length - 1);


			for (int i = -9; i <= +9; i++)
			{
				if (i == 0)
					continue;

				string result = getMultiplicands(value + i);
				if (result == null)
					continue;

				if (current == null || (result.Length * (result.Length - 1) + 4) < current.Length)
					current = "" + '"' + result + '"' + "*".Repeat(result.Length - 1) + dig(Math.Abs(i)) + chrsign(-i);
			}

			for (int i = ' '; i <= '~'; i++)
			{
				if (i == '"')
					continue;

				string result = getMultiplicands(value + i);
				if (result == null)
					continue;

				if (current == null || (result.Length * (result.Length - 1) + 5) < current.Length)
					current = "" + '"' + ((char)i) + result + '"' + "*".Repeat(result.Length - 1) + "\\" + "-";
			}

			for (int i = ' '; i <= '~'; i++)
			{
				if (i == '"')
					continue;

				string result = getMultiplicands(value - i);
				if (result == null)
					continue;

				if (current == null || (result.Length * (result.Length - 1) + 4) < current.Length)
					current = "" + '"' + ((char)i) + result + '"' + "*".Repeat(result.Length - 1) + '+';
			}

			return current;
		}

		private string getMultiplicands(long value)
		{
			if (value == 0)
				return null;

			for (int chr = ' '; chr <= '~'; chr++)
			{
				if (chr == '"')
					continue;
				if (value % chr != 0)
					continue;
				if (value == chr)
					return ((char)chr) + "";

				string mpcands = getMultiplicands(value / chr);
				if (mpcands != null)
					return mpcands + ((char)chr);
			}

			return null;
		}
	}
}
