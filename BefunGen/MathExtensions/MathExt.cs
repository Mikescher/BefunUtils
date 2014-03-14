using System;

namespace BefunGen.MathExtensions
{
	public class MathExt
	{
		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T temp;
			temp = lhs;
			lhs = rhs;
			rhs = temp;
		}

		public static int Max(int v1, params int[] va)
		{
			foreach (int v in va)
			{
				v1 = Math.Max(v1, v);
			}
			return v1;
		}

		public static int Min(int v1, params int[] va)
		{
			foreach (int v in va)
			{
				v1 = Math.Min(v1, v);
			}
			return v1;
		}

		public static long IntPow(int x, short power)
		{
			if (power == 0)
				return 1;
			if (power == 1)
				return x;
			// ----------------------
			int n = 15;
			while ((power <<= 1) >= 0)
				n--;

			long tmp = x;
			while (--n > 0)
				tmp = tmp * tmp *
					 (((power <<= 1) < 0) ? x : 1);
			return tmp;
		}
	}
}
