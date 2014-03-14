using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.NumberCode
{
	public class NumberCodeFactory_Boolean
	{
		public static CodePiece generateCode(int Value)
		{
			return generateCode(Value, false);
		}

		public static CodePiece generateCode(int Value, bool reversed)
		{
			if (Value == 0 || Value == 1)
			{
				return generateCode(Value == 1, reversed);
			}
			else 
			{
				return null;
			}
		}

		public static CodePiece generateCode(bool Value, bool reversed)
		{
			CodePiece p = generateCode(Value);
			if (reversed)
				p.reverseX(false);
			return p;
		}

		public static CodePiece generateCode(bool Value)
		{
			CodePiece p = new CodePiece();
			p[0, 0] = BCHelper.dig(Value ? (byte)1 : (byte)0);
			return p;
		}
	}
}
