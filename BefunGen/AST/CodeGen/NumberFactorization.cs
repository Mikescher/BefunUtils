using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen
{
	class NumberFactorization
	{
		public static CodePiece generateCodeForLiteral(int lit)
		{

		}

		private static List<int> getFactors(int a) // Wenn nicht möglich so gut wie mögl und am ende add
		{
			List<int> result = new List<int>();

			if (a < 10)
			{
				result.Add(a);
				return result;
			}

			for (int i = 9; i < ; i++)
			{
				
			}
		}
	}
}
