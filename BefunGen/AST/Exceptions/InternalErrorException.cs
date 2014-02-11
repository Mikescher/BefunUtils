using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class InternalErrorException : ASTException
	{
		public InternalErrorException(SourceCodePosition pos)
			: base(pos)
		{
		}
	}
}
