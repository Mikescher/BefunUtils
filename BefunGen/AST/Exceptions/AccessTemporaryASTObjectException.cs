using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class AccessTemporaryASTObjectException : ASTException
	{
		public AccessTemporaryASTObjectException(SourceCodePosition pos)
			: base("Trying to access temporary AST-object", pos)
		{
		}
	}
}
