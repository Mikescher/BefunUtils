using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BefunGenTest
{
	[TestClass]
	public class Test_Methods
	{

		[TestMethod]
		public void codeGenTest_Method_VarInitializer()
		{
			BFTestHelper.debugMethod("doFiber(8)",
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
				return max;
			end
			");
		}

		[TestMethod]
		public void codeGenTest_Method_CharCast()
		{
			BFTestHelper.debugMethod("doIt()",
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
			BFTestHelper.debugMethod("doIt()",
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
			BFTestHelper.debugStatement(@"
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
			BFTestHelper.debugMethod("doIt()",
			@"
			int doIt()
			begin
				out (char) 54;
				out (char) 55;
				return 0;
			end
			");
		}

		[TestMethod]
		public void codeGenTest_Method_ArrayIndexing()
		{
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
			BFTestHelper.debugMethod("calc()",
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
		public void codeGenTest_Method_FizzBuzz()
		{
			BFTestHelper.debugMethod("calc()",
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

			BFTestHelper.debugMethod("calc()",
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
		public void codeGenTest_Method_GotoHell()
		{
			BFTestHelper.debugMethod("blub()",
			@"
			void blub()
			var
				int i := 10;
			begin

				lblstart:

				OUT i;

				i--;

				IF (i != 0) THEN
					OUT ''\r\n'';
					GOTO lblstart;
				ELSE
					GOTO lblend;
				END

				OUT ''WADWAD'';

				lblend:
				QUIT;

			end
			");
		}

		[TestMethod]
		public void codeGenTest_Method_SetDisplay()
		{
			BFTestHelper.debugMethod_Output("1", "a()",
			@"
				void a()
				begin
					display[0, 0] = '0';
					OUT (int)(display[0, 0] == '0');
				end
			");
		}

		[TestMethod]
		public void codeGenTest_Method_For()
		{
			BFTestHelper.debugMethod_Output("ABCDEFGHIJKLMNOPQRSTUVWXYZ", "a()",
			@"
				void a()
				var
					int i;
				begin
					FOR(i = (int)'A'; i <= (int)'Z'; i++) DO
						OUT (char)i;
					END
				end
			");
		}

		[TestMethod]
		public void codeGenTest_PostIncrement()
		{
			BFTestHelper.debugMethod_Output("5", "a()",
			@"
				void a()
				var
					int i;
				begin
					i = 5;

					OUT i++;
				end
			");
		}

		[TestMethod]
		public void codeGenTest_PostDecrement()
		{
			BFTestHelper.debugMethod_Output("5", "a()",
			@"
				void a()
				var
					int i;
				begin
					i = 5;

					OUT i--;
				end
			");
		}

		[TestMethod]
		public void codeGenTest_PreIncrement()
		{
			BFTestHelper.debugMethod_Output("6", "a()",
			@"
				void a()
				var
					int i;
				begin
					i = 5;

					OUT ++i;
				end
			");
		}

		[TestMethod]
		public void codeGenTest_PreDecrement()
		{
			BFTestHelper.debugMethod_Output("4", "a()",
			@"
				void a()
				var
					int i;
				begin
					i = 5;

					OUT --i;
				end
			");
		}
	}
}
