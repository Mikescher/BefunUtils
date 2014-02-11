using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
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
			resetCounter();

			ParseTime = Environment.TickCount;

			Program result = null;

			try
			{
				result = (Program)parse(txt);
			}
			catch (ASTException e)
			{
				FailMessage = e.ToString();
				return null;
			}
			catch (Exception e)
			{
				FailMessage = "FATAL EXCEPTION:\r\n" + e.ToString();
				return null;
			}

			if (result == null)
				return null;

			try
			{
				result.link();
			}
			catch (ASTException e)
			{
				FailMessage = e.ToString();
				return null;
			}
			catch (Exception e)
			{
				FailMessage = "FATAL EXCEPTION:\r\n" + e.ToString();
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
						break;

					case GOLD.ParseMessage.Reduction: // Reduction
						parser.CurrentReduction = GrammarTableMap.CreateNewASTObject(parser.CurrentReduction as GOLD.Reduction, parser.CurrentPosition());
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
					throw new LexicalErrorException(parser.CurrentToken().Data, new SourceCodePosition(parser));

				case GOLD.ParseMessage.SyntaxError: //Expecting a different token
					throw new SyntaxErrorException(parser.CurrentToken().Data, parser.ExpectedSymbols().Text(), new SourceCodePosition(parser));

				case GOLD.ParseMessage.InternalError: //INTERNAL ERROR! Something is horribly wrong.
					throw new InternalErrorException(new SourceCodePosition(parser));

				case GOLD.ParseMessage.NotLoadedError: //This error occurs if the CGT was not loaded.
					throw new NotLoadedErrorException(new SourceCodePosition(parser));

				case GOLD.ParseMessage.GroupError: //GROUP ERROR! Unexpected end of file
					throw new GroupErrorException(new SourceCodePosition(parser));
			}
		}

		private void resetCounter()
		{
			Method.resetCounter();
			VarDeclaration.resetCounter();
		}
	}
}