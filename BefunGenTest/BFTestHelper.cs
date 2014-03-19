using BefunGen.AST;
using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.NumberCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BefunGenTest
{
	public static class BFTestHelper
	{
		private static TextFungeParser GParser = new TextFungeParser();

		#region Parsing

		public static Program parseExpression(string type, string expr)
		{
			string txt = String.Format("program b var {0} a; begin a = {1}; QUIT; end end", type, expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		public static Program parseStatement(string stmt)
		{
			string txt = String.Format("program b var  bool a; begin {0} QUIT; end end", stmt);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		public static Program parseMethod(string call, string meth)
		{
			string txt = String.Format("program b begin {0}; end {1} end", call, meth);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		public static Program parseProgram(string meth)
		{
			BefunGen.AST.Program p = GParser.generateAST(meth);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		#endregion

		#region Debugging

		public static void debugExpression(string type, string expr)
		{
			expr = expr.Replace(@"''", "\"");

			Program e = parseExpression(type, expr);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		public static void debugStatement(string stmt)
		{
			stmt = stmt.Replace(@"''", "\"");

			Program e = parseStatement(stmt);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		public static void debugMethod(string call, string meth)
		{
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]+[\r\n]{1,2}", "\r\n");
			meth = Regex.Replace(meth, @"^[ \t]*[\r\n]{1,2}", "");
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]*$", "");
			meth = meth.Replace(@"''", "\"");

			Program e = parseMethod(call, meth);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		public static void debugProgram(string prog)
		{
			prog = prog.Replace(@"''", "\"");

			Program e = parseProgram(prog);
			CodePiece pc = e.generateCode();

			TestCP_Terminate(pc);
		}

		public static void debugProgram_Terminate(string prog)
		{
			prog = prog.Replace(@"''", "\"");

			Program e = parseProgram(prog);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		#endregion

		#region Testing

		public static void TestCP(CodePiece p)
		{
			MultiCPTester.Test_Common(p.ToSimpleString());
		}

		public static void TestCP_Terminate(CodePiece p)
		{
			MultiCPTester.Test_Common(p.ToSimpleString());
		}

		#endregion
	}
}
