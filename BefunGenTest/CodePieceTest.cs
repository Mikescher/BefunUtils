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
		public void CodePieceTest_Set()
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
		public void codeGenTest_Expr()
		{
			debugExpression("40*(-50+(int)rand)");

			debugExpression("100");

			debugExpression("-100");

			debugExpression("137");

			debugExpression("true && (false ^ true)");

			debugExpression("true || false");
		}

		[TestMethod]
		public void codeGenTest_Method_VarInitializer()
		{
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
		}

		[TestMethod]
		public void codeGenTest_Method_CharCast()
		{
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
		}

		[TestMethod]
		public void codeGenTest_Method_OutExpression()
		{
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
		}

		[TestMethod]
		public void codeGenTest_Method_NestedStatementLists()
		{
			debugStatement(@"
			while (true) do
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
		}

		[TestMethod]
		public void codeGenTest_Method_ReversedOut()
		{
			debugMethod(@"
			int doIt()
			begin
				out (char) 54;
				out (char) 55;
			end
			");
		}

		[TestMethod]
		public void codeGenTest_Method_ArrayIndexing()
		{
			debugMethod(@"
			void calc()
			var
				int i := 60;
				char[2] lb;
			begin
				lb[0] = (char)13;
				lb[1] = (char)10;

				while (i <= 66) do
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

		[TestMethod]
		public void codeGenTest_Method_ASCII_Table()
		{
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

		[TestMethod]
		public void codeGenTest_Method_StringEscaping()
		{
			debugMethod(@"
			void calc()
			begin
				OUT ''A \r\n\r\n'';
			END
			");
		}

		[TestMethod]
		public void codeGenTest_Method_StringEscaping_2()
		{
			debugMethod(@"
			void calc()
			var
				int i := 0;
				char[2] lb := { '\r', '\n' };
			begin
				OUT lb[0];
				OUT lb[1];
				
				OUT ''A \r\n\r\n'';

				OUT lb[0];
				OUT lb[1];

				OUT ''B'';

				OUT lb[0];
				OUT lb[1];

				OUT ''C'';

				QUIT;
			END
			");
		}

		[TestMethod]
		public void codeGenTest_Method_BoolCasting()
		{
			debugMethod(@"
			void calc()
			var
				int i := 0;
				char[2] lb;
			begin
				lb[0] = (char)13;
				lb[1] = (char)10;

				while (i <= 128) do
					out (int)(bool)(i % 3);
					out lb[0];
					out lb[1];
					i++;
				end
				
				QUIT;
			END
			");
		}

		[TestMethod]
		public void codeGenTest_Method_Random()
		{
			debugMethod(@"
			void calc()
			var
				int i := 0;
				char[2] lb := { '\r', '\n' };
			BEGIN
	
				out ''d'';

				BEGIN
					OUT lb[(int)RAND];
					OUT lb[(int)RAND];
					
					OUT ''A \r\n\r\n'';

					OUT lb[(int)RAND];
					OUT lb[(int)RAND];

					OUT ''B'';

					OUT lb[(int)RAND];
					OUT lb[(int)RAND];

					OUT ''C'';
					
					OUT lb[(int)RAND];
					OUT lb[(int)RAND];

				END

				QUIT;

			END
			");
		}

		[TestMethod]
		public void codeGenTest_Method_Random_2()
		{
			debugMethod(@"
			void calc()
			var
				int i := 0;
				char[2] lb := { '\r', '\n' };
			BEGIN
				BEGIN
					OUT lb[(int)RAND];
					OUT lb[(int)RAND];
					
					OUT ''A \r\n\r\n'';

					OUT lb[(int)RAND];
					OUT lb[(int)RAND];

					OUT ''B'';

					OUT lb[(int)RAND];
					OUT lb[(int)RAND];

					OUT '''';
					
					OUT lb[(int)RAND];
					OUT lb[(int)RAND];

				END

				QUIT;

			END
			");
		}

		[TestMethod]
		public void codeGenTest_Method_OutputArray()
		{
			debugMethod(@"
			void calc()
			var
				char[4] c;
				char[4] d;
				char[4] e;
			BEGIN
				c[0] = 'A';
				c[1] = 'B';
				c[2] = 'C';
				c[3] = 'D';

				d = c;
				e = d;

				OUT d[0];
				OUT d[1];
				OUT d[2];
				OUT d[3];

				OUT ''  -  '';

				OUT e[0];
				OUT e[1];
				OUT e[2];
				OUT e[3];

				OUT ''  -  '';
				
				OUT d;
				OUT ''::'';
				OUT ''::'';
				OUT e;

				QUIT;
			END
			");
		}

		[TestMethod]
		public void codeGenTest_Method_InputArray()
		{
			debugMethod(@"
			void calc()
			var
				char[5] c;
				char[5] d;
				char[5] e;

				int[4] x;
			BEGIN
				IN c;
				
				d = c;

				e[0] = d[4];
				e[1] = d[3];
				e[2] = d[2];
				e[3] = d[1];
				e[4] = d[0];

				OUT c;
				OUT '' -> '';
				OUT e;

				QUIT;
			END
			");
		}

		[TestMethod]
		public void codeGenTest_Statements()
		{
			debugStatement("out ''blub:fasel'';");

			debugStatement("out (char)50;");

			debugStatement("QUIT;");

			debugStatement("STOP;");

			debugStatement("OUT '''';");
		}

		[TestMethod]
		public void codeGenTest_FizzBuzz()
		{
			debugMethod(@"
			void calc()
			var
				int i := 1;
				char[2] lb;
			BEGIN
				lb[0] = (char)13;
				lb[1] = (char)10;

				WHILE (i < 100) DO
					IF (i % 3 == 0) THEN
						out ''Fizz'';
					END
					IF (i % 5 == 0) THEN
						out ''Buzz'';
					END
					IF (i % 3 != 0 && i % 5 != 0) THEN
						out i;
					END
					OUT lb[0];
					OUT lb[1];

					i++;
				END

				OUT ''Let's FizzBuzz''; // Reverse It
				OUT lb[0];
				OUT lb[1];
				i = 1;
				
				WHILE (i < 100) DO
					IF (i % 3 == 0) THEN
						out ''Fizz'';
					END
					IF (i % 5 == 0) THEN
						out ''Buzz'';
					END
					IF (i % 3 != 0 && i % 5 != 0) THEN
						out i;
					END
					OUT lb[0];
					OUT lb[1];

					i++;
				END

				QUIT;
			END
			");

			debugMethod(@"
			void calc()
			var
				int i := 0;
				char[2] lb;
			begin
				lb[0] = (char)13;
				lb[1] = (char)10;
				i = 1;				

				WHILE (i < 100) DO
					IF (i % 3 == 0 && i % 5 == 0) THEN
						out ''FizzBuzz'';
					ELSIF (i % 3 == 0) THEN
						out ''Fizz'';
					ELSIF (i % 5 == 0) THEN
						out ''Buzz'';
					ELSE
						out i;
					END

					OUT lb[0];
					OUT lb[1];

					i++;
				END

				lb[0] = (char)13;
				lb[1] = (char)10;
				out ''>> FizzBuzz <<''; //Reverse
				lb[0] = (char)13;
				lb[1] = (char)10;
				i = 1;

				WHILE (i < 100) DO
					IF (i % 3 == 0 && i % 5 == 0) THEN
						out ''FizzBuzz'';
					ELSIF (i % 3 == 0) THEN
						out ''Fizz'';
					ELSIF (i % 5 == 0) THEN
						out ''Buzz'';
					ELSE
						out i;
					END

					OUT lb[0];
					OUT lb[1];

					i++;
				END

				QUIT;
			END
			");
		}
	}
}
