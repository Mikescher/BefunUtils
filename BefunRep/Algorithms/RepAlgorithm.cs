using BefunRep.FileHandling;

namespace BefunRep.Algorithms
{
	public abstract class RepAlgorithm
	{
		protected readonly RepresentationSafe representations;

		public RepAlgorithm(RepresentationSafe safe)
		{
			this.representations = safe;
		}

		public string calculate(int value)
		{
			string old = representations.get(value);
			string result = get(value);

			if (result == null || result == "" || result == old || (old != null && old.Length <= result.Length))
			{
				return null;
			}
			else
			{
				representations.put(value, result);

				return result;
			}

		}

		protected char dig(long v)
		{
			return (char)(v + '0');
		}

		public abstract string get(int value);
	}
}
