
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace BefunGenTest
{
	public class MultiCPTester
	{
		private const int RAND_RUN_COUNT = 64;
		private const int MAX_STEP = 8192;

		public static void Test_Common(string prog)
		{
			CPTester tester = new CPTester(prog);

			tester.run(MAX_STEP);

			if (tester.hadRandomElements())
			{
				for (int i = 0; i < RAND_RUN_COUNT; i++)
				{
					tester = new CPTester(prog);
					tester.run(MAX_STEP);
				}
			}
		}

		public static void Test_Terminate(string prog)
		{
			CPTester tester = new CPTester(prog);

			tester.run(MAX_STEP);

			if (tester.StepCount >= (MAX_STEP - 8))
				Assert.Fail();

			if (tester.Stack.Count > 0)
				Assert.Fail();

			if (tester.hadRandomElements())
			{
				for (int i = 0; i < RAND_RUN_COUNT; i++)
				{
					tester = new CPTester(prog);
					tester.run(MAX_STEP);

					if (tester.StepCount >= (MAX_STEP - 8))
						Assert.Fail();

					if (tester.Stack.Count > 0)
						Assert.Fail();
				}
			}
		}

		public static void Test_ForStackValue(string prog, int val)
		{
			CPTester tester = new CPTester(prog);

			tester.run(MAX_STEP);

			if (!(tester.Stack.Count == 1 && tester.Stack.Peek() == val))
			{
				throw new BFRunException("Unexpected Stack Value: " + tester.Stack.Peek() + "  ->  Expected: " + val);
			}

			if (tester.hadRandomElements())
			{
				for (int i = 0; i < RAND_RUN_COUNT; i++)
				{
					tester = new CPTester(prog);
					tester.run(MAX_STEP);
				}
			}
		}

		public static void Test_ForStackValueReverse(string prog, int val)
		{
			CPTester tester = new CPTester(prog, true);

			tester.run(MAX_STEP);

			if (!(tester.Stack.Count == 1 && tester.Stack.Peek() == val))
			{
				throw new BFRunException("Unexpected Stack Value: " + tester.Stack.Peek() + "  ->  Expected: " + val);
			}

			if (tester.hadRandomElements())
			{
				for (int i = 0; i < RAND_RUN_COUNT; i++)
				{
					tester = new CPTester(prog, true);
					tester.run(MAX_STEP);
				}
			}
		}
	}
}
