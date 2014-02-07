using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BefunGen.AST
{
	class GOLDParser
	{
		private GOLD.Parser parser;

		public string FailMessage { get; private set; }

		public GOLDParser(BinaryReader tables)
		{
			parser = new GOLD.Parser();

			parser.LoadTables(tables);
		}

		public Program generateAST(string txt)
		{
			Program result = null;

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
