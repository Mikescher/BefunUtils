using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class VoidObjectCallException : ASTException
	{
		public VoidObjectCallException(SourceCodePosition pos)
			: base("Operation not possible on <void>", pos)
		{
		}
	}
}
