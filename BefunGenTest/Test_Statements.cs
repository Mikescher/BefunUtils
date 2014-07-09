using Microsoft.VisualStudio.TestTools.UnitTesting;

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

		[TestMethod]
		public void codeGenTest_Statement_For_NoCall()
		{
			BFTestHelper.debugStatement("FOR(;FALSE;) DO END");
		}

		[TestMethod]
		public void codeGenTest_Statement_While_NoCall()
		{
			BFTestHelper.debugStatement("WHILE(FALSE) DO END");
		}

		[TestMethod]
		public void codeGenTest_Statement_Outf()
		{
			BFTestHelper.debugStatement("OUTF ''a'', ''B'', 55;");
		}

		[TestMethod]
		public void codeGenTest_Statement_Out()
		{
			BFTestHelper.debugStatement_Output("OUT ''a'';", "a");
			BFTestHelper.debugStatement_Output("OUT ''ab'';", "ab");
			BFTestHelper.debugStatement_Output("OUT ''abcd'';", "abcd");
			BFTestHelper.debugStatement_Output("OUT ''abcdefgh'';", "abcdefgh");
			BFTestHelper.debugStatement_Output("OUT ''abcdefghijklmnop'';", "abcdefghijklmnop");
			BFTestHelper.debugStatement_Output("OUT ''abcdefghijklmnopqrstuvwxyz123456'';", "abcdefghijklmnopqrstuvwxyz123456");
		}
	}
}
