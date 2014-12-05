using System;
using System.IO;
using System.Security;

namespace BefunRep.OutputHandling
{
	public class XMLOutputFormatter : OutputFormatter
	{
		public XMLOutputFormatter(string path)
			: base(path)
		{
			//
		}

		public override void Output(FileHandling.RepresentationSafe safe)
		{
			using (StreamWriter writer = new StreamWriter(filepath))
			{
				long min = safe.getLowestValue();
				long max = safe.getHighestValue();

				writer.WriteLine("<data>");

				for (long v = min; v < max; v++)
				{
					string rep = safe.get(v);
					byte? algo = safe.getAlgorithm(v);

					if (rep == null || algo == null)
						continue;

					writer.WriteLine(String.Format("  <value v=\"{0}\" aID=\"{1}\" algorithm=\"{2}\">{3}</value>",
						v,
						algo,
						RepCalculator.algorithmNames[algo.Value],
						SecurityElement.Escape(rep)));
				}

				writer.WriteLine("</data>");
			}
		}
	}
}
