using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.NumberCode
{
	public class NumberCodeFactory_Digit
	{
		public static CodePiece generateCode(int Value, bool reversed)
		{
			CodePiece p = generateCode(Value);
			if (reversed)
				p.reverseX(false);
			return p;
		}

		public static CodePiece generateCode(int d)
		{
			if (d < 0 || d > 9)
				return null;

			CodePiece p = new CodePiece();
			p[0, 0] = BCHelper.dig((byte)d);
			return p;
		}
	}
}
