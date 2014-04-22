using System;
using System.Diagnostics;

namespace BefunGen
{
	public class StaticStopWatch
	{
		private static Stopwatch sw = new Stopwatch();

		public static void Start()
		{
			sw.Restart();
		}

		public static long Stop()
		{
			sw.Stop();
			return sw.ElapsedMilliseconds;
		}

		public static void StopPrint()
		{
			Console.Out.WriteLine("Time elapsed: {0} ms", Stop());
		}

		public static void StopPrint(int min)
		{
			sw.Stop();
			if (sw.ElapsedMilliseconds > min)
				Console.Out.WriteLine("Time elapsed: {0} ms ( gt {1})", sw.ElapsedMilliseconds, min);
		}
	}
}
