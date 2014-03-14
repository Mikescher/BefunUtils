using BefunGen.AST;
using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.NumberCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BefunGenTest
{
	[TestClass]
	public class CodePieceTest
	{
		private const int NC_RANGE_MIN = -16384;
		private const int NC_RANGE_MAX = +16384;

		#region Helper Methods

		private TextFungeParser GParser = new TextFungeParser();

		#region Parsing

		private Program parseExpression(string type, string expr)
		{
			string txt = String.Format("program b var {0} a; begin a = {1}; QUIT; end end", type, expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		private Program parseStatement(string stmt)
		{
			string txt = String.Format("program b var  bool a; begin {0} QUIT; end end", stmt);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		private Program parseMethod(string call, string meth)
		{
			string txt = String.Format("program b begin {0}; end {1} end", call, meth);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		private Program parseProgram(string meth)
		{
			BefunGen.AST.Program p = GParser.generateAST(meth);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		#endregion

		#region Debugging

		private void debugExpression(string type, string expr)
		{
			expr = expr.Replace(@"''", "\"");

			Program e = parseExpression(type, expr);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		private void debugStatement(string stmt)
		{
			stmt = stmt.Replace(@"''", "\"");

			Program e = parseStatement(stmt);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		private void debugMethod(string call, string meth)
		{
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]+[\r\n]{1,2}", "\r\n");
			meth = Regex.Replace(meth, @"^[ \t]*[\r\n]{1,2}", "");
			meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]*$", "");
			meth = meth.Replace(@"''", "\"");

			Program e = parseMethod(call, meth);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		private void debugProgram(string prog)
		{
			prog = prog.Replace(@"''", "\"");

			Program e = parseProgram(prog);
			CodePiece pc = e.generateCode();

			TestCP(pc);
		}

		#endregion

		#region Testing

		public void TestCP(CodePiece p)
		{
			MultiCPTester.Test_Common(p.ToSimpleString());
		}

		#endregion

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
			debugExpression("int", "5+5");

			debugExpression("int", "40*(-50+(int)rand)");

			debugExpression("int", "100");

			debugExpression("int", "-100");

			debugExpression("int", "137");

			debugExpression("bool", "true && (false ^ true)");

			debugExpression("bool", "true || false");
		}

		[TestMethod]
		public void codeGenTest_Method_VarInitializer()
		{
			debugMethod("doFiber(8)",
			@"
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
			debugMethod("doIt()",
			@"
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
			debugMethod("doIt()",
			@"
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
			debugMethod("doIt()",
			@"
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
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
			void calc()
			begin
				OUT ''A \r\n\r\n'';
			END
			");
		}

		[TestMethod]
		public void codeGenTest_Method_StringEscaping_2()
		{
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
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
			debugMethod("calc()",
			@"
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

			debugMethod("calc()",
			@"
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

		[TestMethod]
		public void numberCodeFactoryTest_Normal()
		{
			for (int i = NC_RANGE_MIN; i < NC_RANGE_MAX; i++)
			{
				List<Tuple<NumberRep, CodePiece>> vs = NumberCodeHelper.generateAllCode(i, true);

				foreach (var val in vs)
					MultiCPTester.Test_ForStackValue(val.Item2.ToSimpleString() + "@", i);
			}
		}

		[TestMethod]
		public void numberCodeFactoryTest_Reverse()
		{
			for (int i = NC_RANGE_MIN; i < NC_RANGE_MAX; i++)
			{
				List<Tuple<NumberRep, CodePiece>> vs = NumberCodeHelper.generateAllCode(i, true, true);

				foreach (var val in vs)
					MultiCPTester.Test_ForStackValueReverse("@" + val.Item2.ToSimpleString(), i);
			}
		}
	}
}
