
namespace BefunGen.AST.CodeGen
{
	public enum NumberRep
	{
		CharKonstant,
		Base9
	}

	public class CodeGenOptions
	{
		public readonly static NumberRep NumberLiteralRepresentation = NumberRep.Base9;
		public readonly static bool AutoDigitizeNumberLiterals = true; // If 0 <= NumberLiteral <= 9 then generate as digit (only codegen - not in AST)
	}
}
