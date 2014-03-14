using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.NumberCode
{
	public class NumberCodeFactory_StringmodeChar
	{
		public static CodePiece generateCode(int Value, bool reversed)
		{
			CodePiece p = generateCode(Value);
			if (reversed)
				p.reverseX(false);
			return p;
		}

		public static CodePiece generateCode(int Value)
		{
			CodePiece p = new CodePiece();

			if (Value == -(int)'"')
			{
				p[0, 0] = BCHelper.Digit_1;
				p[1, 0] = BCHelper.Stringmode;
				p[2, 0] = BCHelper.chr(-Value + 1);
				p[3, 0] = BCHelper.Stringmode;
				p[4, 0] = BCHelper.Sub;

				return p;
			} 
			else if (Value == (int)'"')
			{
				p[0, 0] = BCHelper.Digit_1;
				p[1, 0] = BCHelper.Stringmode;
				p[2, 0] = BCHelper.chr(Value - 1);
				p[3, 0] = BCHelper.Stringmode;
				p[4, 0] = BCHelper.Add;

				return p;
			} 
			else if (Value <= -(int)' ' && Value >= -(int)'~')
			{
				p[0, 0] = BCHelper.Digit_0;
				p[1, 0] = BCHelper.Stringmode;
				p[2, 0] = BCHelper.chr(-Value);
				p[3, 0] = BCHelper.Stringmode;
				p[4, 0] = BCHelper.Sub;

				return p;
			}
			else if (Value >= (int)' ' && Value <= (int)'~')
			{
				p[0, 0] = BCHelper.Stringmode;
				p[1, 0] = BCHelper.chr(Value);
				p[2, 0] = BCHelper.Stringmode;

				return p;
			}
			else
			{
				return null;
			}
		}
	}
}
