using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class ArrayLiteralTooBigException : ASTException
	{
		public ArrayLiteralTooBigException(SourceCodePosition pos)
			: base("ArrayLiteral is too big for Variable", pos)
		{
		}
	}
}
