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

					if (rep == null)
						continue;

					writer.WriteLine(String.Format("  <value v=\"{0}\">{1}</value>", v, SecurityElement.Escape(rep)));
				}

				writer.WriteLine("</data>");
			}
		}
	}
}
