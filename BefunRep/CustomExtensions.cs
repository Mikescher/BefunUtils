using System.Collections.Generic;

namespace BefunRep
{
	public static class CustomExtensions
	{
		public static void Fill<T>(this T[] originalArray, T with)
		{
			for (int i = 0; i < originalArray.Length; i++)
			{
				originalArray[i] = with;
			}
		}

		public static IEnumerable<long> LongRange(long start, long end)
		{
			for (long i = start; i < end; i++)
			{
				yield return i;
			}
		}
	}
}
