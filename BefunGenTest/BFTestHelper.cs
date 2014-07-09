using BefunGen.AST;
using BefunGen.AST.CodeGen;
using System;
using System.Text.RegularExpressions;

namespace BefunGenTest
{
	public static class BFTestHelper
	{
		private static TextFungeParser GParser = new TextFungeParser();

		#region Parsing

		public static Program parseExpression_lr(string type, string expr)
		{
			string txt = String.Format("program b var {0} a; begin a = {1}; QUIT; end end", type, expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p;
		}

		public static Program parseExpression_rl(string type, string expr)
		{
			string txt = String.Format("program b var {0} a; begin OUT \"\"; a = {1}; QUIT; end end", type, expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p;
		}

		public static Program parseExpression_o_lr(string type, string expr)
		{
			string txt = String.Format("program b var {0} a; begin a = {1}; OUT a; end end", type, expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p;
		}

		public static Program parseExpression_o_rl(string type, string expr)
		{
			string txt = String.Format("program b var {0} a; begin OUT \"\"; a = {1}; OUT a; end end", type, expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p;
		}

		public static Program parseStatement(string stmt)
		{
			string txt = String.Format("program b var  bool a; begin {0} QUIT; end end", stmt);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p;
		}

		public static Program parseMethod(string call, string meth)
		{
			string txt = String.Format("program b : display[4,4] begin {0}; end {1} end", call, meth);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p;
		}

		public static Program parseProgram(string meth)
		{
			BefunGen.AST.Program p = GParser.generateAST(meth);

			return p;
		}

		#endregion

		#region Debugging

		public static void debugExpression(string type, string expr)
		{
			expr = expr.Replace(@"''", "\"");

			Program e_lr = parseExpression_lr(type, expr);
			Program e_rl = parseExpression_rl(type, expr);

			CodePiece pc_lr = e_lr.generateCode();
			CodePiece pc_rl = e_rl.generateCode();

			TestCP(pc_lr);
			TestCP(pc_rl);
		}

		public static void debugExpression_Output(string o, string type, string expr)
		{
			expr = expr.Replace(@"''", "\"");

			Program e_lr = parseExpression_o_lr(type, expr);
			Program e_rl = parseExpression_o_rl(type, expr);

			CodePiece pc_lr = e_lr.generateCode();
			CodePiece pc_rl = e_rl.generateCode();

			TestCP_Output(pc_lr, o);
			TestCP_Output(pc_rl, o);
		}

		public static void debugStatement(string stmt)
		{
			stmt = stmt.Replace(@"''", "\"");

			Program e = parseStatement(stmt);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		public static void debugStatement_Output(string stmt, string op)
		{
			stmt = stmt.Replace(@"''", "\"");

			Program e = parseStatement(stmt);
			CodePiece pc = e.generateCode();

			TestCP_Output(pc, op);
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

		public static void debugMethod_Output(string o, string call, string meth)
		{
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]+[\r\n]{1,2}", "\r\n");
			meth = Regex.Replace(meth, @"^[ \t]*[\r\n]{1,2}", "");
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]*$", "");
			meth = meth.Replace(@"''", "\"");

			Program e = parseMethod(call, meth);
			CodePiece pc = e.generateCode();

			TestCP_Output(pc, o);
		}

		public static void debugProgram(string prog)
		{
			prog = prog.Replace(@"''", "\"");

			Program e = parseProgram(prog);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		public static void debugProgram_Terminate(string prog)
		{
			prog = prog.Replace(@"''", "\"");

			Program e = parseProgram(prog);
			CodePiece pc = e.generateCode();

			TestCP_Terminate(pc);
		}

		public static void debugProgram_Output(string p_out, string prog)
		{
			prog = prog.Replace(@"''", "\"");

			Program e = parseProgram(prog);
			CodePiece pc = e.generateCode();

			TestCP_Output(pc, p_out);
		}

		#endregion

		#region Testing

		public static void TestCP(CodePiece p)
		{
			MultiCPTester.Test_Common(p.ToSimpleString());
		}

		public static void TestCP_Terminate(CodePiece p)
		{
			MultiCPTester.Test_Terminate(p.ToSimpleString());
		}

		public static void TestCP_Output(CodePiece p, string p_out)
		{
			MultiCPTester.Test_Output(p.ToSimpleString(), p_out);
		}

		#endregion
	}
}
