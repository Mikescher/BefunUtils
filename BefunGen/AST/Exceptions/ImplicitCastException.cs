using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class ImplicitCastException : ASTException
	{
		public ImplicitCastException(BType from, BType to, SourceCodePosition pos)
			: base(String.Format("Cannot implicitly cast from {0} to {1}", from, to), pos)
		{
		}
	}
}
