
namespace BefunRep.Algorithms
{
	/// <summary>
	/// Represents numbers as 
	/// othernumber [*/] [0-9] 
	/// Not possible for all numbers
	/// </summary>
	public class DigitMultAlgorithm : RepAlgorithm
	{
		public DigitMultAlgorithm(byte aid)
			: base(aid)
		{
			// NOP
		}

		public override string get(long value)
		{
			string best = null;

			for (int i = 2; i <= 9; i++)
			{

				string other = representations.get(value * i);
				if (other == null)
					continue;

				if (best == null || other.Length + 2 < best.Length)
					best = other + dig(i) + "/";
			}

			for (int i = 2; i <= 9; i++)
			{
				if (value % i != 0)
					continue;

				string other = representations.get(value / i);
				if (other == null)
					continue;

				if (best == null || other.Length + 2 < best.Length)
					best = other + dig(i) + "*";
			}

			return best;
		}

	}
}
