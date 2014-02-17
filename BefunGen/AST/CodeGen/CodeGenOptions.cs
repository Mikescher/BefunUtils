
namespace BefunGen.AST.CodeGen
{
	public enum NumberRep
	{
		CharConstant,
		Base9,
		Factorization//TODO Option to intelligent use Best Option
	}

	public class CodeGenOptions
	{
		// Defines how Number Literals get represented
		public static NumberRep NumberLiteralRepresentation = NumberRep.Factorization;

		// If 0 <= NumberLiteral <= 9 then generate as digit (only codegen - not in AST)
		public static bool AutoDigitizeNumberLiterals = true;

		// Removes 2x STringmodetoogle after each other rfrom Expression (eg "" )
		public static bool OptimizeDoubleStringmodeToogle = true;
	}
}
