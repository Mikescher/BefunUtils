using BefunRep.Exceptions;
using BefunRep.FileHandling;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunRep.Algorithms
{
	/// <summary>
	/// Tries to represent numbers as multiplication and addition of stringmode-representable characters
	/// Not possible for all numbers
	/// </summary>
	public class StringifyAlgorithm : RepAlgorithm
	{
		private enum StripOp { Add, Mult }

		private const char MIN_ASCII = ' '; // 32
		private const char MAX_ASCII = '~'; // 126

		public StringifyAlgorithm(RepresentationSafe s)
			: base(s)
		{
			//
		}

		public override string get(int lit)
		{
			if (lit < 0)
			{
				string p = get(-lit);

				if (p == null)
					return null;

				return "0" + p + "-";
			}

			if (lit >= 0 && lit <= 9)
			{
				return "" + dig(lit);
			}

			if (lit < MIN_ASCII && lit >= (MIN_ASCII - 9))
			{
				if (lit + 9 == '"')
				{
					string p = get(lit + 8);

					if (p == null)
						return null;

					return p + "8-";
				}
				else
				{
					string p = get(lit + 9);

					if (p == null)
						return null;

					return p + "9-";
				}

			}

			if (lit < (MIN_ASCII - 9))
			{
				return null;
			}

			List<char> str;
			List<StripOp> ops;

			if (calculateStringOps(out str, out ops, lit))
			{
				StringBuilder p = new StringBuilder();

				p.Append('"');
				foreach (char c in str)
					p.Append(c);
				p.Append('"');

				for (int i = 0; i < ops.Count; i++)
				{
					switch (ops[i])
					{
						case StripOp.Add:
							p.Append('+');
							break;
						case StripOp.Mult:
							p.Append('*');
							break;
						default:
							throw new WTFException();
					}
				}

				return p.ToString();
			}

			return null;
		}

		private static bool calculateStringOps(out List<char> str, out List<StripOp> ops, long val)
		{
			if (val < MIN_ASCII)
			{
				ops = null;
				str = null;
				return false;
			}

			//##########################################################################

			if (val >= MIN_ASCII && val <= MAX_ASCII && val != '"')
			{
				ops = new List<StripOp>();
				str = new List<char>() { (char)val };
				return true;
			}

			//##########################################################################

			for (char curr = MAX_ASCII; curr >= MIN_ASCII; curr--)
			{
				if (curr == '"')
					continue;

				if (val % curr == 0 && val / curr > MIN_ASCII)
				{
					List<char> o_str;
					List<StripOp> o_ops;

					if (calculateStringOps(out o_str, out o_ops, val / curr))
					{
						str = o_str.ToList();
						ops = o_ops.ToList();

						str.Insert(0, curr);
						ops.Add(StripOp.Mult);

						return true;
					}
				}
			}

			//##########################################################################


			for (char curr = MAX_ASCII; curr >= MIN_ASCII; curr--)
			{
				if (curr == '"')
					continue;

				List<char> o_str;
				List<StripOp> o_ops;

				if (calculateStringOps(out o_str, out o_ops, val - curr))
				{
					str = o_str.ToList();
					ops = o_ops.ToList();

					str.Insert(0, curr);
					ops.Add(StripOp.Add);

					return true;
				}
			}

			str = null;
			ops = null;
			return false;
		}
	}
}
