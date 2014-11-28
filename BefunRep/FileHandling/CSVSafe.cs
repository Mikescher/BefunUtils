using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BefunRep.FileHandling
{
	public class CSVSafe : RepresentationSafe
	{
		private readonly string filepath;

		private SortedDictionary<int, string> representations;

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
							.Select(p => p.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(q => q.Trim()))
							.Select(p => p.ToList())
							.Where(p => p.Count == 2)
							.Where(p => int.TryParse(p[0], out tmp))
							.Where(p => p[1] != "");

			representations = new SortedDictionary<int, string>();
			foreach (var item in elements)
			{
				representations.Add(int.Parse(item[0]), item[1]);
			}
		}

		private void safe()
		{
			string txt = String.Join(Environment.NewLine, representations.Select(p => p.Key + ", " + p.Value));

			File.WriteAllText(filepath, txt);
		}

		public override string get(int key)
		{
			if (representations.ContainsKey(key))
				return representations[key];
			else
				return null;
		}

		public override void put(int key, string representation)
		{
			representations.Add(key, representation);

			safe();
		}

	}
}
