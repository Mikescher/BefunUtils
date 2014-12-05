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
			new Base9Algorithm(),			// [0]
			new FactorizationAlgorithm(),	// [1]
			new CharAlgorithm(),			// [2]
			new StringifyAlgorithm(),		// [3]
			new BaseNAlgorithm(),	// [4]
		};

		public static string[] algorithmNames { get { return algorithms.Select(p => p.GetType().Name.Replace("Algorithm", "")).ToArray(); } }

		private readonly long lowerB;
		private readonly long upperB;

		private readonly RepresentationSafe safe;
		private readonly ResultTester tester;

		public RepCalculator(long low, long high, bool test, RepresentationSafe rsafe)
		{
			this.lowerB = low;
			this.upperB = high;
			this.safe = rsafe;

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

			RepAlgorithm algo = algorithms[algonum];

			for (long v = lowerB; v < upperB; v++)
			{
				calculateSingle(algo, v);
			}

			//#################################################################

			safe.stop();
		}

		public void calculate()
		{
			safe.start();

			//#################################################################

			foreach (RepAlgorithm algo in algorithms)
			{
				for (long v = lowerB; v < upperB; v++)
				{
					calculateSingle(algo, v);
				}
			}

			//#################################################################

			safe.stop();
		}

		private void calculateSingle(RepAlgorithm algo, long v)
		{
			string outerror;
			string result = algo.calculate(v);

			if (result != null)
			{
				Console.Out.WriteLine(
					String.Format("[{0:HH:mm:ss}] {1,16} Found: {2,11}  ->  {3}",
						DateTime.Now,
						algo.GetType().Name.Replace("Algorithm", ""),
						v,
						result)
					);

				if (!tester.test(result, v, out outerror))
				{
					Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] TEST RESULT ERROR", DateTime.Now));
					Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] #################", DateTime.Now));
					Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Program = {1}", DateTime.Now, result));
					Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Algorithm = {1}", DateTime.Now, algo.GetType().Name));
					Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Expected Result = {1}", DateTime.Now, v));
					Console.Out.WriteLine(String.Format("[{0:HH:mm:ss}] TEST Problem = \"{1}\"", DateTime.Now, outerror));

					Thread.Sleep(5 * 1000);
				}
			}
		}
	}
}
