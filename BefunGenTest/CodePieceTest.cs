using BefunGen.AST;
using BefunGen.AST.CodeGen;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace BefunGenTest
{
	[TestClass]
	public class CodePieceTest
	{//TODO Automatic Test resulting pros -> like BefungExec DebugMode -> Random input (perhaps multiple times)
		#region Helper Methods

		private TextFungeParser GParser = new TextFungeParser();

		private Expression parseExpression(string expr)
		{
			string txt = String.Format("program b var  bool a; begin a = (bool)({0}); end end", expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return ((Expression_Cast)((Statement_Assignment)((Statement_StatementList)p.MainStatement.Body).List[0]).Expr).Expr;
		}

		private Statement parseStatement(string stmt)
		{
			string txt = String.Format("program b var  bool a; begin {0} end end", stmt);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return ((Statement_StatementList)p.MainStatement.Body).List[0];
		}

		private Method parseMethod(string meth)
		{
			string txt = String.Format("program b begin end {0} end", meth);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			return p.MethodList[1];
		}

		private void debugExpression(string expr)
		{
			expr = expr.Replace(@"''", "\"");

			Expression e = parseExpression(expr);
			CodePiece pc = e.generateCode(false);
		}

		private void debugStatement(string stmt)
		{
			stmt = stmt.Replace(@"''", "\"");

			Statement e = parseStatement(stmt);
			CodePiece pc = e.generateCode(false);
		}

		private void debugMethod(string meth)
		{
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]+[\r\n]{1,2}", "\r\n");
			meth = Regex.Replace(meth, @"^[ \t]*[\r\n]{1,2}", "");
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]*$", "");
			meth = meth.Replace(@"''", "\"");

			Method e = parseMethod(meth);
			CodePiece pc = e.generateCode(0, 0);
		}

		#endregion

		[TestMethod]
		public void setTest()
		{
			CodePiece cp = new CodePiece();

			cp[0, 0] = new BefungeCommand(BefungeCommandType.Add);
			Assert.AreEqual(1, cp.Width);
			Assert.AreEqual(1, cp.Height);

			cp[0, 2] = new BefungeCommand(BefungeCommandType.Add);
			Assert.AreEqual(1, cp.Width);
			Assert.AreEqual(3, cp.Height);

			cp[2, 0] = new BefungeCommand(BefungeCommandType.Add);
			Assert.AreEqual(3, cp.Width);
			Assert.AreEqual(3, cp.Height);

			cp[2, 2] = new BefungeCommand(BefungeCommandType.Add);
			Assert.AreEqual(3, cp.Width);
			Assert.AreEqual(3, cp.Height);

			cp[0, -2] = new BefungeCommand(BefungeCommandType.Add);
			Assert.AreEqual(3, cp.Width);
			Assert.AreEqual(5, cp.Height);

			cp[-2, 0] = new BefungeCommand(BefungeCommandType.Add);
			Assert.AreEqual(5, cp.Width);
			Assert.AreEqual(5, cp.Height);

			cp[-2, -2] = new BefungeCommand(BefungeCommandType.Add);
			Assert.AreEqual(5, cp.Width);
			Assert.AreEqual(5, cp.Height);
		}

		[TestMethod]
		public void codeGenTest()
		{
			debugExpression("40*(-50+(int)rand)");

			debugExpression("100");

			debugExpression("-100");

			debugExpression("137");

			debugExpression("true && (false ^ true)");

			debugExpression("true || false");

			debugMethod(@"
			int doFiber(int max)
			var
				int a := 4;
				bool b;	
				char cc := 'o';
				int[4] e := {40, 48, 60, -20};
				bool c;
				bool d := 10;
				int[8] h;
			begin
				
			end
			");

			debugMethod(@"
			int doIt()
			var
				char cr;
				char lf;
				int i := 48;
			begin
				cr = (char)13;
				lf = (char)10;	
			
				out (char)i;
				out cr;
				out lf;
				i++;
				out (char)i;
				out cr;
				out lf;
				i++;
				out (char)i;
				out cr;
				out lf;
				i++;
				out (char)i;
				out cr;
				out lf;
				i++;
				out (char)i;
				out cr;
				out lf;
				i++;
			    out (char)(48+(int)RAND);
				out ''Hello'';
				out '' ... '';
				out '' World'';
				out (char)(48+(int)RAND);
				QUIT;
			end
			");

			debugMethod(@"
			int doIt()
			begin
				out (char)(48+(int)RAND);
				out (char)(48+(int)RAND);
				out (char)(48+(int)RAND);
				out (char)(48+(int)RAND);
				QUIT;
			end
			");

			debugStatement(@"
			while (true) do
			begin
				out (char)(48+(int)RAND);
				begin
					out (char)(50+(int)RAND);
					out (char)(50+(int)RAND);
				end
				begin
					out (char)(50+(int)RAND);
					out (char)(50+(int)RAND);
				end
				out (char)(48+(int)RAND);
				out (char)(48+(int)RAND);
				out (char)(48+(int)RAND);
			end
			");

			debugMethod(@"
			int doIt()
			begin
				out (char) 54;
				out (char) 55;
			end
			");

			debugExpression("true || false");

			debugStatement("out ''blub:fasel'';");

			debugMethod(@"
			void calc()
			var
				int i := 60;
				char[2] lb;
			begin
				lb[0] = (char)13;
				lb[1] = (char)10;

				while (i <= 66) do
				begin
					out i;
					out '' = '';
					out (char)i;
					out lb[0];
					out lb[1];
					i++;
				end
				
				QUIT;
			end
			");

			debugMethod(@"
			void calc()
			var
				int i := 0;
				char[2] lb;
			begin
				lb[0] = (char)13;
				lb[1] = (char)10;
				out '>';
				while (i <= 128) do
				begin
					out i;
					out '' = '';
					out (char)i;
					out lb[0];
					out lb[1];
					i++;
				end
				
				QUIT;
			end
			");
		}
	}
}
