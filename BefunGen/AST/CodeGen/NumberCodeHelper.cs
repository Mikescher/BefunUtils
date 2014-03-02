using BefunGen.AST.Exceptions;

namespace BefunGen.AST.CodeGen
{
	public class NumberCodeHelper
	{
		public static CodePiece generateCode(int Value, bool reversed)
		{
			CodePiece p;

			if (CodeGenOptions.AutoDigitizeNumberLiterals && Value >= 0 && Value <= 9)
			{
				p = generateCode_Digit((byte)Value);
			}
			else
			{
				if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.CharConstant)
				{
					p = generateCode_Stringmode(Value, reversed);
				}
				else if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.Base9)
				{
					p = Base9Converter.generateCodeForLiteral(Value);
					if (reversed)
						p.reverseX(false);
				}
				else if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.Factorization)
				{
					p = NumberFactorization.generateCodeForLiteral(Value);
					if (reversed)
						p.reverseX(false);
				}
				else
				{
					throw new WTFException();
				}
			}

			return p;
		}

		public static CodePiece generateCode_Digit(byte d)
		{
			CodePiece p = new CodePiece();
			p[0, 0] = BCHelper.dig(d);
			return p;
		}

		public static CodePiece generateCode_Stringmode(int Value, bool reversed)
		{
			CodePiece p = new CodePiece();

			if (Value >= 0 && Value <= 9)
			{
				p[0, 0] = BCHelper.chr(Value);
			}
			else if (Value == '"' || Value == '\r' || Value == '\n' || Value == '\a' || Value == '\0' || Value == '\b' || Value == '\v' || Value == '\f' || Value == '\0')
			{
				p = generateCode(Value, reversed);
			}
			else
			{
				p[0, 0] = BCHelper.Stringmode;
				p[1, 0] = BCHelper.chr(Value);
				p[2, 0] = BCHelper.Stringmode;
			}

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
