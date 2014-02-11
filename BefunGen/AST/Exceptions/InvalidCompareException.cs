using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class InvalidCompareException : ASTException
	{
		public InvalidCompareException(BType a, BType b, SourceCodePosition pos)
			: base(String.Format("Cannot compare Types of {0} and {1}", a, b), pos)
		{
		}
	}
}
