using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class NotLoadedErrorException : ASTException
	{
		public NotLoadedErrorException(SourceCodePosition pos)
			: base(pos)
		{
		}
	}
}
