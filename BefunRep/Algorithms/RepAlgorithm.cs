using BefunRep.FileHandling;

namespace BefunRep.Algorithms
{
	public abstract class RepAlgorithm
	{
		public RepresentationSafe representations = null;
		public readonly byte algorithmID;

		public RepAlgorithm(byte id)
		{
			this.algorithmID = id;
		}

		public string calculate(long value)
		{
			string old = representations.get(value);
			string result = get(value);

			if (result == null || result == "" || result == old || (old != null && old.Length <= result.Length))
			{
				return null;
			}
			else
			{
				representations.put(value, result, algorithmID);

				return result;
			}

		}

		protected char dig(long v)
		{
			return (char)(v + '0');
		}

		protected char chrsign(long i)
		{
			if (i < 0)
				return '-';
			else if (i > 0)
				return '+';
			else
				return '0';
		}

		public abstract string get(long value);
	}
}
