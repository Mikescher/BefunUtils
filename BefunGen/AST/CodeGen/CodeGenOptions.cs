
namespace BefunGen.AST.CodeGen
{
	public enum NumberRep
	{
		CharConstant,
		Base9
	}

	public class CodeGenOptions
	{
		public readonly static NumberRep NumberLiteralRepresentation = NumberRep.Base9; //TODO Was funktioniert bei neg Zahlen ??
		public readonly static bool AutoDigitizeNumberLiterals = true; // If 0 <= NumberLiteral <= 9 then generate as digit (only codegen - not in AST)
		public readonly static bool OptimizeDoubleStringmodeToogle = true; // Removes 2x STringmodetoogle after each other rfrom Expression (eg "" )
	}
}
