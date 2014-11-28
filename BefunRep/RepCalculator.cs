using BefunRep.Algorithms;
using BefunRep.FileHandling;
using System;

namespace BefunRep
{
	public class RepCalculator
	{
		private readonly int lowerB;
		private readonly int upperB;

		private readonly RepresentationSafe safe;

		private readonly RepAlgorithm[] algorithms;

		public RepCalculator(int low, int high, RepresentationSafe rsafe)
		{
			this.lowerB = low;
			this.upperB = high;
			this.safe = rsafe;

			algorithms = new RepAlgorithm[]
			{
				new Base9Algorithm(safe),
				new FactorizationAlgorithm(safe),
				new CharAlgorithm(safe),
				new StringifyAlgorithm(safe),
			};
		}

		public void start()
		{
			foreach (RepAlgorithm algo in algorithms)
			{
				for (int v = lowerB; v < upperB; v++)
				{
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
					}
				}
			}
		}
	}
}
