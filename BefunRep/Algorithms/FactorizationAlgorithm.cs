using BefunRep.Exceptions;
using System.Collections.Generic;
using System.Text;

namespace BefunRep.Algorithms
{
	/// <summary>
	/// Represents numbers as multiple multiplications
	/// If this is not possible it tries to get as near as possible and then use an "add"
	/// </summary>
	public class FactorizationAlgorithm : RepAlgorithm
	{
		public FactorizationAlgorithm(byte aid)
			: base(aid)
		{
			// NOP
		}

		public override string get(long lit)
		{
			if (lit < 0)
			{
				StringBuilder p = new StringBuilder();
				p.Append('0');
				getFactors(p, -lit);
				p.Append('-');

				return p.ToString();
			}
			else if (lit == 0)
			{
				return "0";
			}
			else
			{
				return getFactors(new StringBuilder(), lit).ToString();
			}
		}

		private StringBuilder getFactors(StringBuilder p, long a)
		{
			List<int> result = new List<int>();

			if (a < 10)
			{
				p.Append(dig(a));
				return p;
			}

			for (byte i = 9; i > 1; i--)
			{
				if (a % i == 0)
				{
					getFactors(p, a / i);
					p.Append(dig(i));
					p.Append('*');
					return p;
				}
			}

			for (byte i = 1; i < 10; i++)
			{
				if ((a - i) % 9 == 0)
				{
					getFactors(p, a - i);
					p.Append(dig(i));
					p.Append('+');
					return p;
				}
			}

			throw new WTFException();
		}
	}
}
