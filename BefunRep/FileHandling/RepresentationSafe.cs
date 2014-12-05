
namespace BefunRep.FileHandling
{
	public abstract class RepresentationSafe
	{
		public abstract string get(long key);
		public abstract byte? getAlgorithm(long key);
		public abstract void put(long key, string representation, byte algorithm);

		public abstract void start();
		public abstract void stop();

		public abstract long getLowestValue();
		public abstract long getHighestValue();

		public long getNonNullRepresentations()
		{
			long low = getLowestValue();
			long high = getHighestValue();

			long count = 0;

			for (long i = low; i < high; i++)
			{
				if (get(i) != null)
					count++;
			}

			return count;
		}

		public long countRepresentationsPerAlgorithm(int p)
		{
			long low = getLowestValue();
			long high = getHighestValue();

			long count = 0;

			for (long i = low; i < high; i++)
			{
				if (get(i) != null && getAlgorithm(i) == p)
					count++;
			}

			return count;
		}

		public double getAverageRepresentationWidth()
		{
			long low = getLowestValue();
			long high = getHighestValue();

			long count = 0;
			long len = 0;

			for (long i = low; i < high; i++)
			{
				string rep = get(i);
				if (get(i) != null)
				{
					count++;
					len += rep.Length;
				}
			}

			if (count == 0)
				return 0;

			return len / count;
		}

		public double getAverageRepresentationWidthPerAlgorithm(int p)
		{
			long low = getLowestValue();
			long high = getHighestValue();

			long count = 0;
			long len = 0;

			for (long i = low; i < high; i++)
			{
				string rep = get(i);
				if (get(i) != null && getAlgorithm(i) == p)
				{
					count++;
					len += rep.Length;
				}
			}

			if (count == 0)
				return 0;

			return len / count;
		}
	}
}
