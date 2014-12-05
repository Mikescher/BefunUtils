using System;
using System.Collections.Generic;
using System.Linq;

namespace BefunRep.Algorithms
{
	/// <summary>
	/// Tries to represent numbers as n^a (a power)
	/// Not possible for all numbers
	/// </summary>
	public class PowerAlgorithm : RepAlgorithm
	{
		public PowerAlgorithm(byte aid)
			: base(aid)
		{
			// NOP
		}

		public override string get(long value)
		{
			if (value > 0)
				return getPositive(value);
			else
			{
				string pos = getPositive(-value);
				if (pos == null)
					return null;
				else
					return "0" + getPositive(-value) + "-";
			}
		}

		public string getPositive(long value)
		{
			List<string> reps = new List<string>();

			long maxPower = (long)Math.Ceiling(Math.Log(value, 2));

			for (int power = 0; power < maxPower; power++)
			{
				long factor = (long)Math.Pow(value, 1.0 / power);

				if (PowL(factor, power) != value)
					continue;

				string factor_rep = representations.get(factor);

				if (factor_rep == null)
					continue;

				int fcount = 1;
				string p_op = "";
				while (fcount < power)
				{
					if (fcount * 2 <= power)
					{
						fcount *= 2;
						p_op += ":*";
					}
					else
					{
						fcount++;
						p_op = ":" + p_op + "*";
					}
				}
				reps.Add(factor_rep + p_op);
			}

			if (reps.Count == 0)
				return null;
			return reps.OrderBy(p => p.Length).First();
		}

		private long PowL(long v, long p)
		{
			long result = 1;
			for (int i = 0; i < p; i++)
			{
				result *= v;
			}
			return result;
		}
	}
}
