using System;

namespace BefunRep.Algorithms
{
	/// <summary>
	/// Represents numbers as 
	/// othernumber [+-] [0-9] 
	/// Not possible for all numbers
	/// </summary>
	public class DigitAddAlgorithm : RepAlgorithm
	{
		public DigitAddAlgorithm(byte aid)
			: base(aid)
		{
			// NOP
		}

		public override string get(long value)
		{
			string best = null;

			for (int i = -9; i <= 9; i++)
			{
				if (i == 0)
					continue;

				string other = representations.get(value + i);
				if (other == null)
					continue;

				if (best == null || other.Length + 2 < best.Length)
					best = other + dig(Math.Abs(i)) + chrsign(-i);
			}
			return best;
		}

	}
}
