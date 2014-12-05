using System.Collections.Generic;
using System.Linq;

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

		public static string Repeat(this string str, int count)
		{
			return string.Concat(Enumerable.Repeat(str, count));
		}
	}
}
