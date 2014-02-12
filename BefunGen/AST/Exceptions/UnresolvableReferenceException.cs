using BefunGen.AST.CodeGen;

namespace BefunGen.AST.Exceptions
{
	public class UnresolvableReferenceException : ASTException
	{
		public UnresolvableReferenceException(string s, SourceCodePosition pos)
			: base("Could not resolve reference: \"" + s + "\"", pos)
		{
		}
	}
}
