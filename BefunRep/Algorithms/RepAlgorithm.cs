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
			string result = get(value);

			if (result == null || result == "" || result == representations.get(value))
			{
				return null;
			}
			else
			{
				representations.put(value, result);

				return result;
			}

		}

		public abstract string get(int value);
	}
}
