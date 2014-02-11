using System;
using System.IO;

namespace BefunGen.AST
{
	public class TextFungeParser
	{
		private GOLD.Parser parser;

		public string FailMessage { get; private set; }
		public long ParseTime { get; private set; }

		public TextFungeParser()
		{
			parser = new GOLD.Parser();
		}

		public TextFungeParser(BinaryReader tables)
		{
			parser = new GOLD.Parser();

			loadTables(tables);
		}

		public bool loadTables(BinaryReader r)
		{
			return parser.LoadTables(r);
		}

		public bool loadTables(string p)
		{
			return loadTables(new BinaryReader(new FileStream(p, FileMode.Open)));
		}

		public Program generateAST(string txt)
		{
			ParseTime = Environment.TickCount;

			Program result = null;

			try
			{
				result = (Program)parse(txt);
			}
			catch (Exception e)
			{
				FailMessage = e.ToString();
				return null;
			}

			if (result == null)
				return null;

			try
			{
				result.link();
			}
			catch (Exception e)
			{
				FailMessage = e.ToString();
				return null;
			}

			ParseTime = Environment.TickCount - ParseTime;

			return result;
		}

		private object parse(string txt)
		{
			object result = null;

			parser.Open(ref txt);
			parser.TrimReductions = false;

			bool done = false;
			while (!done)
			{
				GOLD.ParseMessage response = parser.Parse();

				switch (response)
				{
					case GOLD.ParseMessage.LexicalError:
					case GOLD.ParseMessage.SyntaxError:
					case GOLD.ParseMessage.InternalError:
					case GOLD.ParseMessage.NotLoadedError:
					case GOLD.ParseMessage.GroupError:
						fail(response);
						done = true;
						break;

					case GOLD.ParseMessage.Reduction: // Reduction
						parser.CurrentReduction = GrammarTableMap.CreateNewASTObject(parser.CurrentReduction as GOLD.Reduction);
						break;

					case GOLD.ParseMessage.Accept: //Accepted!
						result = parser.CurrentReduction;
						done = true;
						break;

					case GOLD.ParseMessage.TokenRead: //You don't have to do anything here.
						break;
				}
			}

			return result;
		}

		private void fail(GOLD.ParseMessage msg)
		{
			switch (msg)
			{
				case GOLD.ParseMessage.LexicalError: //Cannot recognize token
					FailMessage = "Lexical Error:" + Environment.NewLine +
								  "Line: " + (parser.CurrentPosition().Line + 1) + ":" + parser.CurrentPosition().Column + Environment.NewLine +
								  "Read: " + parser.CurrentToken().Data;
					break;

				case GOLD.ParseMessage.SyntaxError: //Expecting a different token
					FailMessage = "Syntax Error:" + Environment.NewLine +
								  "Line: " + (parser.CurrentPosition().Line + 1) + ":" + parser.CurrentPosition().Column + Environment.NewLine +
								  "Read: " + parser.CurrentToken().Data + Environment.NewLine +
								  "Expecting: " + parser.ExpectedSymbols().Text();
					break;

				case GOLD.ParseMessage.InternalError: //INTERNAL ERROR! Something is horribly wrong.
					FailMessage = "Internal Error";
					break;

				case GOLD.ParseMessage.NotLoadedError: //This error occurs if the CGT was not loaded.
					FailMessage = "Tables not loaded";
					break;

				case GOLD.ParseMessage.GroupError: //GROUP ERROR! Unexpected end of file
					FailMessage = "Runaway group";
					break;
			}
		}
	}
}