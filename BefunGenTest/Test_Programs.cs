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
			BFTestHelper.debugProgram_Terminate(@"
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
	}
}
