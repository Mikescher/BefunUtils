using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BefunRep.FileHandling
{
	public class JSONSafe : RepresentationSafe
	{
		private readonly string filepath;

		private SortedDictionary<long, string> representations;

		public JSONSafe(string path)
		{
			this.filepath = path;

			load();
		}

		private void load()
		{
			if (!File.Exists(filepath))
			{
				File.CreateText(filepath).Close();
				representations = new SortedDictionary<long, string>();
				return;
			}

			string file = File.ReadAllText(filepath);

			representations = JsonConvert.DeserializeObject<SortedDictionary<long, string>>(file);

			if (representations == null)
				representations = new SortedDictionary<long, string>();
		}

		private void safe()
		{
			string txt = JsonConvert.SerializeObject(representations, Formatting.Indented);

			File.WriteAllText(filepath, txt);
		}

		public override string get(long key)
		{
			if (representations.ContainsKey(key))
				return representations[key];
			else
				return null;
		}

		public override void put(long key, string representation)
		{
			representations[key] = representation;

			safe();
		}

		public override void start()
		{
			//
		}

		public override void stop()
		{
			safe();
		}

		public override long getLowestValue()
		{
			return (representations.Count == 0) ? 0 : representations.Keys.Min();
		}

		public override long getHighestValue()
		{
			return (representations.Count == 0) ? 0 : representations.Keys.Max();
		}

	}
}
