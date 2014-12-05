using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BefunRep.FileHandling
{
	public class CSVSafe : RepresentationSafe
	{
		private readonly string filepath;

		private SortedDictionary<long, string> representations;

		public CSVSafe(string path)
		{
			this.filepath = path;

			load();
		}

		private void load()
		{
			if (!File.Exists(filepath))
				File.CreateText(filepath).Close();

			string file = File.ReadAllText(filepath);

			int tmp;
			var elements = file
							.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
							.Where(p => p.Contains(' '))
							.Select(p => new string[] { p.Substring(0, p.IndexOf(' ')), p.Substring(p.IndexOf(' ') + 1) })
							.Select(p => p.Select(q => q.Trim()))
							.Select(p => p.ToList())
							.Where(p => p.Count == 2)
							.Where(p => int.TryParse(p[0], out tmp))
							.Where(p => p[1] != "");

			representations = new SortedDictionary<long, string>();
			foreach (var item in elements)
			{
				representations.Add(int.Parse(item[0]), item[1]);
			}
		}

		private void safe()
		{
			string txt = String.Join(Environment.NewLine, representations.Select(p => String.Format("{0, -11} {1}", p.Key, p.Value)));

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
