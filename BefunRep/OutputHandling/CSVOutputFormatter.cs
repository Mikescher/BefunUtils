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

					if (rep == null)
						continue;

					writer.WriteLine(String.Format("{0, -11} {1}", v, rep));
				}
			}
		}
	}
}
