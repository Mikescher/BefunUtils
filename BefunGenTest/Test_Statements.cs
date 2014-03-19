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
	public class Test_Statements
	{

		[TestMethod]
		public void codeGenTest_Statement_Out_Literal()
		{
			BFTestHelper.debugStatement("out ''blub:fasel'';");
		}

		[TestMethod]
		public void codeGenTest_Statement_Cast()
		{
			BFTestHelper.debugStatement("out (char)50;");
		}

		[TestMethod]
		public void codeGenTest_Statement_Quit()
		{
			BFTestHelper.debugStatement("QUIT;");
		}

		[TestMethod]
		public void codeGenTest_Statement_Quit_2()
		{
			BFTestHelper.debugStatement("STOP;");
		}

		[TestMethod]
		public void codeGenTest_Statement_Out_Empty()
		{
			BFTestHelper.debugStatement("OUT '''';");
		}

	}
}
