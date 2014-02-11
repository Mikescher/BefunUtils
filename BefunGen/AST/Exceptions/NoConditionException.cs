using BefunGen.AST.CodeGen;

namespace BefunGen.AST.Exceptions
{
	public class NoConditionException : ASTException
	{
		public NoConditionException(SourceCodePosition pos)
			: base("Expression is not a valid Condition", pos)
		{
		}
	}
}
