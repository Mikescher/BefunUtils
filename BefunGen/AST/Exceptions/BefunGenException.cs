using BefunGen.AST.CodeGen;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace BefunGen.AST.Exceptions
{
	public abstract class BefunGenException : Exception
	{
		public readonly string BefMessage;

		public readonly SourceCodePosition Position;

		public BefunGenException(string eid, SourceCodePosition pos)
			: base(String.Format("[{0}] Exception ({1})", eid, pos))
		{
			Position = pos;
			BefMessage = "";
		}

		public BefunGenException(string eid, string msg, SourceCodePosition pos)
			: base(String.Format("[{0}] Exception ({1}): \r\n   {2}", pos, eid, msg))
		{
			Position = pos;
			BefMessage = msg;
		}

		public BefunGenException(string eid, string msg)
			: base(String.Format("Exception ({0}): \r\n   {1}", eid, msg))
		{
			Position = new SourceCodePosition();
			BefMessage = msg;
		}

		public override string ToString()
		{
			return Regex.Replace(base.ToString().Replace(" in ", Environment.NewLine + "      in "), @"in.*BefunGen\\", "in ");
		}

		public string getWellFormattedString()
		{
			string result = null;
			bool done = false;

			System.Threading.Thread t = new System.Threading.Thread(() =>
			{
				result = base.ToString();

				done = true;
			});
			t.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
			t.Start();

			while (!done)
			{
				Thread.Sleep(0);
			}

			return Regex.Replace(result.Replace(" at ", "      at "), @"at.*BefunGen\\", "at ");
		}

		public abstract string ToPopupString();
	}
}
