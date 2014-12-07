
using System;
using System.Linq;
namespace BefunRep.FileHandling
{
	public struct SafeInfo
	{
		public long low;
		public long high;

		public long count;

		public long nonNullCount;
		public long[] nonNullPerAlgorithm;

		public long totalLen;
		public long[] totalLenPerAlgorithm;

		public double avgLen;
		public double[] avgLenPerAlgorithm;

		public int minLen;
		public int[] minLenPerAlgorithm;

		public int maxLen;
		public int[] maxLenPerAlgorithm;
	}

	public abstract class RepresentationSafe
	{
		public abstract string get(long key);
		public abstract byte? getAlgorithm(long key);
		public abstract void put(long key, string representation, byte algorithm);

		public abstract void start();
		public abstract void stop();

		public abstract long getLowestValue();
		public abstract long getHighestValue();

		public SafeInfo getInformations()
		{
			long low = getLowestValue();
			long high = getHighestValue();

			long nonNullCount = 0;
			long[] nonNullPerAlgorithm = Enumerable.Repeat(0L, RepCalculator.algorithms.Length).ToArray();

			long totalLen = 0;
			long[] totalLenPerAlgorithm = Enumerable.Repeat(0L, RepCalculator.algorithms.Length).ToArray();

			int minLen = int.MaxValue;
			int[] minLenPerAlgorithm = Enumerable.Repeat(int.MaxValue, RepCalculator.algorithms.Length).ToArray();

			int maxLen = int.MinValue;
			int[] maxLenPerAlgorithm = Enumerable.Repeat(int.MinValue, RepCalculator.algorithms.Length).ToArray();

			for (long i = low; i < high; i++)
			{
				string rep = get(i);

				if (rep == null)
					continue;

				byte? _algo = getAlgorithm(i).Value;

				if (_algo == null)
					continue;

				byte algo = _algo.Value;

				nonNullCount++;
				nonNullPerAlgorithm[algo]++;

				totalLen += rep.Length;
				totalLenPerAlgorithm[algo] += rep.Length;

				minLen = Math.Min(minLen, rep.Length);
				minLenPerAlgorithm[algo] = Math.Min(minLenPerAlgorithm[algo], rep.Length);

				maxLen = Math.Max(maxLen, rep.Length);
				maxLenPerAlgorithm[algo] = Math.Max(maxLenPerAlgorithm[algo], rep.Length);
			}


			if (minLen == int.MaxValue || maxLen == int.MinValue)
			{
				minLen = 0;
				maxLen = 0;
			}

			double avgLen;
			double[] avgLenPerAlgorithm = new double[RepCalculator.algorithms.Length];

			for (int i = 0; i < totalLenPerAlgorithm.Length; i++)
			{
				if (nonNullPerAlgorithm[i] == 0)
					avgLenPerAlgorithm[i] = 0;
				else
					avgLenPerAlgorithm[i] = totalLenPerAlgorithm[i] * 1.0 / nonNullPerAlgorithm[i];

				if (minLenPerAlgorithm[i] == int.MaxValue || maxLenPerAlgorithm[i] == int.MinValue)
				{
					minLenPerAlgorithm[i] = 0;
					maxLenPerAlgorithm[i] = 0;
				}
			}



			if (nonNullCount == 0)
				avgLen = 0;
			else
				avgLen = totalLen * 1.0 / nonNullCount;

			return new SafeInfo
			{
				low = low,
				high = high,
				count = high - low,
				nonNullCount = nonNullCount,
				nonNullPerAlgorithm = nonNullPerAlgorithm,
				totalLen = totalLen,
				totalLenPerAlgorithm = totalLenPerAlgorithm,
				minLen = minLen,
				minLenPerAlgorithm = minLenPerAlgorithm,
				maxLen = maxLen,
				maxLenPerAlgorithm = maxLenPerAlgorithm,
				avgLen = avgLen,
				avgLenPerAlgorithm = avgLenPerAlgorithm,
			};
		}

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
