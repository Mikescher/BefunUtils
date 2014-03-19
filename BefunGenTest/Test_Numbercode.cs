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
	public class Test_Numbercode
	{
		private const int NC_RANGE_MIN = -16384;
		private const int NC_RANGE_MAX = +16384;

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
