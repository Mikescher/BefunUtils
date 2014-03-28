using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BefunGenTest
{
	[TestClass]
	public class Test_Expressions
	{

		[TestMethod]
		public void codeGenTest_Expr_Add()
		{
			BFTestHelper.debugExpression("int", "5+5");
		}

		[TestMethod]
		public void codeGenTest_Expr_rand()
		{
			BFTestHelper.debugExpression("int", "40*(-50+(int)rand)");
		}

		[TestMethod]
		public void codeGenTest_Expr_Literal()
		{
			BFTestHelper.debugExpression("int", "100");
		}

		[TestMethod]
		public void codeGenTest_Expr_negative()
		{
			BFTestHelper.debugExpression("int", "-100");
		}

		[TestMethod]
		public void codeGenTest_Expr_Literal_2()
		{
			BFTestHelper.debugExpression("int", "137");
		}

		[TestMethod]
		public void codeGenTest_Expr_bool_1()
		{
			BFTestHelper.debugExpression("bool", "true && (false ^ true)");
		}

		[TestMethod]
		public void codeGenTest_Expr_bool_or()
		{
			BFTestHelper.debugExpression("bool", "true || false");
		}

		[TestMethod]
		public void codeGenTest_Expr_b4_rand()
		{
			BFTestHelper.debugExpression("int", "RAND[4]");
		}

		[TestMethod]
		public void codeGenTest_Expr_b4_rand_rec()
		{
			BFTestHelper.debugExpression("int", "RAND[RAND[4]+1]");
		}

		[TestMethod]
		public void codeGenTest_Expr_b4_rand_negative()
		{
			BFTestHelper.debugExpression("int", "RAND[-4]");
		}
	}
}
