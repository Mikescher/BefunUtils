using BefunRep.Algorithms;
using BefunRep.FileHandling;
using BefunRep.Test;
using System;
using System.Linq;
using System.Threading;

namespace BefunRep
{
	public class RepCalculator
	{
		public static readonly RepAlgorithm[] algorithms = new RepAlgorithm[]
		{
			new Base9Algorithm(0),			// [0]
			new FactorizationAlgorithm(1),	// [1]
			new CharAlgorithm(2),			// [2]
			new StringifyAlgorithm(3),		// [3]
			new BaseNAlgorithm(4),			// [4]
			new PowerAlgorithm(5),			// [5]
			new DigitAddAlgorithm(6),		// [6]
			new SimpleNegativeAlgorithm(7),	// [7]
			new DigitMultAlgorithm(8),		// [8]
		};

		public static string[] algorithmNames { get { return algorithms.Select(p => p.GetType().Name.Replace("Algorithm", "")).ToArray(); } }
		public static long[] algorithmTime = new long[algorithms.Length];

		private readonly long lowerB;
		private readonly long upperB;
		private readonly bool quiet;

		private readonly RepresentationSafe safe;
		private readonly ResultTester tester;

		public RepCalculator(long low, long high, bool test, RepresentationSafe rsafe, bool isQuiet)
		{
			this.lowerB = low;
			this.upperB = high;
			this.safe = rsafe;
			this.quiet = isQuiet;

			if (test)
				tester = new ExecuteResultTester();
			else
				tester = new DummyResultTester();

			foreach (var algo in algorithms)
				algo.representations = rsafe;
		}

		public void calculate(int algonum)
		{
			if (algonum < 0)
			{
				calculate();
				return;
			}

			safe.start();

			//#################################################################

			algorithmTime[algonum] = Environment.TickCount;
			RepAlgorithm algo = algorithms[algonum];

			for (long v = lowerB; v < upperB; v++)
			{
				calculateSingle(algo, v);
			}
			algorithmTime[algonum] = Environment.TickCount - algorithmTime[algonum];

			//#################################################################

			safe.stop();
		}

		public void calculate()
		{
			safe.start();

			//#################################################################

			for (int algonum = 0; algonum < algorithms.Length; algonum++)
			{
				algorithmTime[algonum] = Environment.TickCount;
				for (long v = lowerB; v < upperB; v++)
				{
					calculateSingle(algorithms[algonum], v);
				}
				algorithmTime[algonum] = Environment.TickCount - algorithmTime[algonum];
			}

			//#################################################################

			safe.stop();
		}

		private void calculateSingle(RepAlgorithm algo, long v)
		{
			string outerror;
			string before = safe.get(v);
			string beforeAlgo = before == null ? null : algorithmNames[safe.getAlgorithm(v).Value];
			if (before == null)
				before = "";
			if (beforeAlgo == null)
				beforeAlgo = "";
			string result = algo.calculate(v);

			if (result != null)
			{
				if (!quiet)
				{
					if (before != "")
						Console.Out.WriteLine(
							String.Format("[{0:HH:mm:ss}] {1,16} Found: {2,11}  ->  {3,-24} (before {4,-16} {5,-24} = +{6})",
								DateTime.Now,
								algo.GetType().Name.Replace("Algorithm", ""),
								v,
								result,
								beforeAlgo,
								before,
								before.Length - result.Length)
							);
					else
						Console.Out.WriteLine(
							String.Format("[{0:HH:mm:ss}] {1,16} Found: {2,11}  ->  {3,-24}",
								DateTime.Now,
								algo.GetType().Name.Replace("Algorithm", ""),
								v,
								result)
							);
				}

				if (!tester.test(result, v, out outerror))
				{
					Console.Error.WriteLine(String.Format("[{0:HH:mm:ss}] TEST RESULT ERROR", DateTime.Now));
					Console.Error.WriteLine(String.Format("[{0:HH:mm:ss}] #################", DateTime.Now));
					Console.Error.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Program = {1}", DateTime.Now, result));
					Console.Error.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Algorithm = {1}", DateTime.Now, algo.GetType().Name));
					Console.Error.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Expected Result = {1}", DateTime.Now, v));
					Console.Error.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Problem = \"{1}\"", DateTime.Now, outerror));

					Thread.Sleep(5 * 1000);
				}
			}
		}
	}
}
