using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.NumberCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BefunGenTest
{
	[TestClass]
	public class Test_Programs
	{

		[TestMethod]
		public void codeGenTest_Program_MethodCalls()
		{
			BFTestHelper.debugProgram_Terminate(@"
			program testprog
				VAR
					int i;
				BEGIN

					OUT ''\r\nSTART\r\n'';

					ma();
					mb();
					mc();

					OUT ''\r\nFIN\r\n'';

					QUIT;
				END

				VOID ma()
				BEGIN
				
					OUT ''A1'';
					OUT ''A2'';
					OUT ''A3'';
					OUT ''\r\n'';

					RETURN;

				END

				VOID mb()
				BEGIN

					OUT ''B1'';
					OUT ''B2'';
					OUT ''B3'';
					OUT ''\r\n'';

					RETURN;

				END

				VOID mc()
				BEGIN

					OUT ''C1'';
					OUT ''C2'';
					OUT ''C3'';
					OUT ''\r\n'';

					RETURN;

				END
			END
			");
		}

		[TestMethod]
		public void codeGenTest_Program_ParameterMethodCalls()
		{
			BFTestHelper.debugProgram_Terminate(@"
program example
	begin
		out euclid(44, 12);
	end

	int euclid(int a, int b) 
	begin
		OUT a;
		OUT ''  '';
		OUT b;
		OUT ''  '';
		return 1337;
	end
end
			");
		}

		[TestMethod]
		public void codeGenTest_Program_ArrayReturn()
		{
			BFTestHelper.debugProgram_Output("Hello", @"
			program example
				begin
					out blub();
				end
			
				char[5] blub()
				var
					char[5] result;
				begin
					result[0] = 'H';
					result[1] = 'e';
					result[2] = 'l';
					result[3] = 'l';
					result[4] = 'o';
					return result;
				end
			end
			");
		}
		
		[TestMethod]
		public void codeGenTest_Program_ArrayReturn_2()
		{
			BFTestHelper.debugProgram_Output("Hello", @"
			program example
				begin
					out blub();
				end
			
				char[5] blub()
				var
					char[5] result;
				begin
					result[0] = 'H';
					result[1] = 'e';
					result[2] = 'l';
					result[3] = 'l';
					result[4] = 'o';
					OUT '''';
					return result;
				end
			end
			");
		}

		[TestMethod]
		public void codeGenTest_Program_Euklid()
		{
			BFTestHelper.debugProgram_Output("4", @"
program example
	begin
		out euclid(44, 12);
	end

	int euclid(int a, int b) 
	begin
		if (a == 0) then
			return b;
		else 
			if (b == 0) then
				return a;
			else 
				if (a > b) then
					return euclid(a - b, b);
				else
					return euclid(a, b - a);
				end
			end
		end
	end
end
			");
		}

		[TestMethod]
		public void codeGenTest_Program_GlobalVar()
		{
			BFTestHelper.debugProgram_Output("99", @"
program example
	global
	 int i;
	begin
		i = 0;
		
		doodle();
		
		OUT i;
	end

	void doodle() 
	begin
		i = 10;
		
		doodle2();
	end
	 
	void doodle2() 
	begin
		i = i * 10;
		
		doodle3();
	end
	 
	void doodle3() 
	begin
		i--;
	end
end
			");
		}

		[TestMethod]
		public void codeGenTest_Program_Constants()
		{
			BFTestHelper.debugProgram_Output("99", @"
program example
	const
		int FALSCH := 0;
		int WAHR   := 1;
	global
		int i;
	begin
		i = FALSCH;
		
		doodle();
		
		OUT i;
	end

	void doodle() 
	begin
		i = 10;
		
		doodle2();
	end
	 
	void doodle2() 
	begin
		i = i * 10;
		
		doodle3();
	end
	 
	void doodle3() 
	begin
		i = i - WAHR;
	end
end
			");
		}

		[TestMethod]
		public void codeGenTest_Program_Modulo_Display_Access()
		{
			CodeGenOptions.DisplayModuloAccess = true;

			BFTestHelper.debugProgram(@"
program example : display[64, 16]
	begin
		FOR(;;) DO
			paintR();
		END
	end

	void paintR() 
	var
	 int x;
	 int y;
	begin
		x = ((((((((int)RAND)*2) + ((int)RAND))*2 + ((int)RAND) ) * 2 + ((int)RAND)*2) + ((int)RAND))*2 + ((int)RAND) ) * 2 + ((int)RAND);
		y = ((((((((int)RAND)*2) + ((int)RAND))*2 + ((int)RAND) ) * 2 + ((int)RAND)*2) + ((int)RAND))*2 + ((int)RAND) ) * 2 + ((int)RAND);

		OUT x;
		OUT '','';		
		OUT y;
		OUT ''\r\n'';		

		display[x, y] = '#';

		OUT ''\r\n'';

	end
end
");
		}
	}
}
