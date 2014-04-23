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

		public string generateCode(string txt, string initialDisplay, bool debug)
		{
			Program p;
			CodePiece c;
			return generateCode(txt, initialDisplay, debug, out p, out c);
		}

		public string generateCode(string txt, string initialDisplay, bool debug, out Program p, out CodePiece cp)
		{
			p = generateAST(txt) as Program;

			cp = p.generateCode(initialDisplay);

			string result;

			if (debug)
				result = cp.ToString();
			else
				result = cp.ToSimpleString();

			return result;
		}

		public Program generateAST(string txt)
		{
			ParseTime = Environment.TickCount;

			Program result = null;

			result = (Program)parse(txt);

			if (result == null)
				throw new Exception("Result == null");

			result.prepare();

			ParseTime = Environment.TickCount - ParseTime;

			return result;
		}

		public bool TryParse(string txt, string disp, out BefunGenException err, out Program prog)
		{
			ParseTime = Environment.TickCount;

			Program result = null;

			try
			{
				result = (Program)parse(txt);
			}
			catch (BefunGenException e)
			{
				err = e;
				prog = null;
				return false;
			}
			catch (Exception e)
			{
				err = new NativeException(e);
				prog = null;
				return false;
			}

			if (result == null)
			{
				err = new WTFException();
				prog = null;
				return false;
			}

			try
			{
				result.prepare();
			}
			catch (BefunGenException e)
			{
				err = e;
				prog = null;
				return false;
			}
			catch (Exception e)
			{
				err = new NativeException(e);
				prog = null;
				return false;
			}

			try
			{
				result.generateCode(disp);
			}
			catch (BefunGenException e)
			{
				err = e;
				prog = null;
				return false;
			}
			catch (Exception e)
			{
				err = new NativeException(e);
				prog = null;
				return false;
			}

			ParseTime = Environment.TickCount - ParseTime;

			err = null;
			prog = result;
			return true;
		}

		private object parse(string txt)
		{
			lock (this)
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