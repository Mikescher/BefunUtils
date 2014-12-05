using BefunRep.FileHandling;
using System;
using System.IO;

namespace BefunRep.OutputHandling
{
	public class CSVOutputFormatter : OutputFormatter
	{
		public CSVOutputFormatter(string path)
			: base(path)
		{
			//
		}

		public override void Output(RepresentationSafe safe)
		{
			using (StreamWriter writer = new StreamWriter(filepath))
			{
				long min = safe.getLowestValue();
				long max = safe.getHighestValue();
				for (long v = min; v < max; v++)
				{
					string rep = safe.get(v);
					byte? algo = safe.getAlgorithm(v);

					if (rep == null || algo == null)
						continue;

					writer.WriteLine(String.Format("{0, -11}   {1,-30}   {2}",
						v,
						RepCalculator.algorithmNames[algo.Value] + "-Algorithm",
						rep));
				}
			}
		}
	}
}
