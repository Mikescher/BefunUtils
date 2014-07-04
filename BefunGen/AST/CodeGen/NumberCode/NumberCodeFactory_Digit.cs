
namespace BefunGen.AST.CodeGen.NumberCode
{
	public class NumberCodeFactory_Digit
	{
		public static CodePiece generateCode(long Value, bool reversed)
		{
			CodePiece p = generateCode(Value);

			if (p == null)
				return null;

			if (reversed)
				p.reverseX(false);
			return p;
		}

		public static CodePiece generateCode(long d)
		{
			if (d < 0 || d > 9)
				return null;

			CodePiece p = new CodePiece();
			p[0, 0] = BCHelper.dig((byte)d);
			return p;
		}
	}
}
