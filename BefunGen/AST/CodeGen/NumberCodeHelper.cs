using BefunGen.AST.Exceptions;

namespace BefunGen.AST.CodeGen
{
	public static class NumberCodeHelper
	{
		public static NumberRep lastRep;

		public static CodePiece generateCode(int Value, bool reversed)
		{
			CodePiece p;

			if (CodeGenOptions.AutoDigitizeNumberLiterals && Value >= 0 && Value <= 9)
			{
				p = generateCode_Digit((byte)Value);
				lastRep = NumberRep.Digit;
			}
			else if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.Best)
			{
				if (Value >= 0 && Value <= 9)
				{
					p = generateCode_Digit((byte)Value);
					lastRep = NumberRep.Digit;
				}
				else
				{
					CodePiece cp_b9 = Base9Converter.generateCodeForLiteral(Value);
					CodePiece cp_nf = NumberFactorization.generateCodeForLiteral(Value);
					CodePiece cp_sm = generateCode_Stringmode(Value, reversed);

					if (cp_b9.Width > 3 && cp_nf.Width > 3 && cp_sm.Width == 3 && Value >= 32 && Value <= 126)
					{
						p = cp_sm; // Displayable as ASCII
						lastRep = NumberRep.CharConstant;
					}
					else if (cp_b9.Width > 5 && cp_nf.Width > 5 && cp_sm.Width == 5 && (-Value) >= 32 && (-Value) <= 126)
					{
						p = cp_sm; // Displayable as 0 - ASCII
						lastRep = NumberRep.CharConstant;
					}
					else if (cp_nf.Width < cp_b9.Width)
					{
						p = cp_nf; // NumbreFactorization is superior
						lastRep = NumberRep.Factorization;

						if (reversed)
							p.reverseX(false);
					}
					else
					{
						p = cp_b9; // Base-9 is superior/equals NumberFactorization
						lastRep = NumberRep.Base9;

						if (reversed)
							p.reverseX(false);
					}
				}
			}
			else if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.CharConstant)
			{
				p = generateCode_Stringmode(Value, reversed);
				lastRep = NumberRep.CharConstant;
			}
			else if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.Base9)
			{
				p = Base9Converter.generateCodeForLiteral(Value);
				lastRep = NumberRep.Base9;

				if (reversed)
					p.reverseX(false);
			}
			else if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.Factorization)
			{
				p = NumberFactorization.generateCodeForLiteral(Value);
				lastRep = NumberRep.Factorization;

				if (reversed)
					p.reverseX(false);
			}
			else if (CodeGenOptions.NumberLiteralRepresentation == NumberRep.Digit)
			{
				p = generateCode_Digit((byte)Value);
				lastRep = NumberRep.Digit;
			}
			else
			{
				throw new WTFException();
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
				p = Base9Converter.generateCodeForLiteral(Value);

				if (reversed)
					p.reverseX(false);
			}
			else if (Value < 0)
			{
				p[0, 0] = BCHelper.Digit_0;
				p[1, 0] = BCHelper.Stringmode;
				p[2, 0] = BCHelper.chr(-Value);
				p[3, 0] = BCHelper.Stringmode;
				p[4, 0] = BCHelper.Sub;

				if (reversed)
					p.reverseX(false);
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
