using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using BefunGen.Properties;
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

			loadTables(new BinaryReader(new MemoryStream(getGrammar())));
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
			FailMessage = "";

			ParseTime = Environment.TickCount;

			Program result = null;

			try
			{
				result = (Program)parse(txt);
			}
			catch (BefunGenException e)
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
				result.prepare();
			}
			catch (BefunGenException e)
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

		public bool TryParse(string txt, out BefunGenException err)
		{
			FailMessage = "";

			ParseTime = Environment.TickCount;

			Program result = null;

			try
			{
				result = (Program)parse(txt);
			}
			catch (BefunGenException e)
			{
				err = e;
				return false;
			}
			catch (Exception e)
			{
				err = new NativeException(e);
				return false;
			}

			if (result == null)
			{
				err = new WTFException();
				return false;
			}

			try
			{
				result.prepare();
			}
			catch (BefunGenException e)
			{
				err = e;
				return false;
			}
			catch (Exception e)
			{
				err = new NativeException(e);
				return false;
			}

			ParseTime = Environment.TickCount - ParseTime;

			err = null;
			return true;
		}

		private object parse(string txt)
		{
			object result = null;

			txt = txt.Replace("\r\n", "\n") + "\n";

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

		public string getGrammarDefinition()
		{
			return Resources.TextFunge_grm;
		}

		public byte[] getGrammar()
		{
			return Resources.TextFunge_egt;
		}
	}
}