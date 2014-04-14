using System;

namespace BefunWrite.Helper
{
	public static class MathHelper
	{
		public static uint ParseFormattedCharString(string s)
		{
			if (s == null)
			{
				return 0;
			}

			if (s.Length == 0)
			{
				return 0;
			}

			if (s.Length == 1)
			{
				return s[0];
			}

			try
			{
				return Convert.ToUInt32(s);
			}
			catch
			{
				/**/
			}

			return 0;
		}
	}
}
