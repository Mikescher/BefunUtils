using Newtonsoft.Json;
using System;
using System.IO;

namespace BefunRep.OutputHandling
{
	public class JSONOutputFormatter : OutputFormatter
	{
		public JSONOutputFormatter(string path)
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

				writer.WriteLine("{");

				for (long v = min; v < max; v++)
				{
					string rep = safe.get(v);

					if (rep == null)
						continue;

					writer.WriteLine(String.Format("  \"{0}\": {1},", v, JsonConvert.SerializeObject(rep)));
				}

				writer.WriteLine("}");
			}
		}
	}
}
