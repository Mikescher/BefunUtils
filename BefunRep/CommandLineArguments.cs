using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace BefunRep
{
	public class CommandLineArguments
	{
		private StringDictionary Parameters;

		public CommandLineArguments(string[] Args)
		{
			Parameters = new StringDictionary();
			Regex Spliter = new Regex(@"^-{1,2}|^/|=|:",
				RegexOptions.IgnoreCase | RegexOptions.Compiled);

			Regex Remover = new Regex(@"^['""]?(.*?)['""]?$",
				RegexOptions.IgnoreCase | RegexOptions.Compiled);

			string Parameter = null;
			string[] Parts;

			foreach (string Txt in Args)
			{
				Parts = Spliter.Split(Txt, 3);

				switch (Parts.Length)
				{
					case 1:
						if (Parameter != null)
						{
							if (!Parameters.ContainsKey(Parameter))
							{
								Parts[0] =
									Remover.Replace(Parts[0], "$1");

								Parameters.Add(Parameter, Parts[0]);
							}
							Parameter = null;
						}
						break;

					case 2:
						if (Parameter != null)
						{
							if (!Parameters.ContainsKey(Parameter))
								Parameters.Add(Parameter, "true");
						}
						Parameter = Parts[1];
						break;

					case 3:
						if (Parameter != null)
						{
							if (!Parameters.ContainsKey(Parameter))
								Parameters.Add(Parameter, "true");
						}

						Parameter = Parts[1];

						if (!Parameters.ContainsKey(Parameter))
						{
							Parts[2] = Remover.Replace(Parts[2], "$1");
							Parameters.Add(Parameter, Parts[2]);
						}

						Parameter = null;
						break;
				}
			}
			if (Parameter != null)
			{
				if (!Parameters.ContainsKey(Parameter))
					Parameters.Add(Parameter, "true");
			}
		}

		public bool Contains(string key)
		{
			return Parameters.ContainsKey(key);
		}

		public bool IsSet(string key)
		{
			return Parameters.ContainsKey(key) && Parameters[key] != null;
		}

		public string this[string Param]
		{
			get
			{
				return (Parameters[Param]);
			}
		}

		public bool isEmpty()
		{
			return Parameters.Count == 0;
		}

		public bool IsLong(string p)
		{
			long a;
			return IsSet(p) && long.TryParse(Parameters[p], out a);
		}

		public long GetLong(string p)
		{
			return long.Parse(this[p]);
		}

		public long GetLongDefault(string p, long def)
		{
			return IsLong(p) ? GetLong(p) : def;
		}

		public long GetLongDefaultRange(string p, long def, long min, long max)
		{
			return Math.Min(max - 1, Math.Max(min, (IsLong(p) ? GetLong(p) : def)));
		}

		public bool IsInt(string p)
		{
			int a;
			return IsSet(p) && int.TryParse(Parameters[p], out a);
		}

		public int GetInt(string p)
		{
			return int.Parse(this[p]);
		}

		public int GetIntDefault(string p, int def)
		{
			return IsInt(p) ? GetInt(p) : def;
		}

		public int GetIntDefaultRange(string p, int def, int min, int max)
		{
			return Math.Min(max - 1, Math.Max(min, (IsInt(p) ? GetInt(p) : def)));
		}

		public string GetStringDefault(string p, string def)
		{
			return Contains(p) ? this[p] : def;
		}
	}
}
