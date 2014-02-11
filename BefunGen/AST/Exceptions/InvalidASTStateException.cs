using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class InvalidASTStateException : ASTException
	{
		public InvalidASTStateException(SourceCodePosition pos)
			: base("AST is currently in an invalid state", pos)
		{
		}
	}
}
