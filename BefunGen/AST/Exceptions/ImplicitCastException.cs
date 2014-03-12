using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class ImplicitCastException : ASTException
	{
		public ImplicitCastException(SourceCodePosition pos, BType from, BType to)
			: base(String.Format("Cannot implicitly cast from {0} to {1}", from, to), pos)
		{
		}

		public ImplicitCastException(SourceCodePosition pos, BType from, params BType[] to)
			: base(String.Format("Cannot implicitly cast from {0} to {1}", from, string.Join(" or ", to.ToList())), pos)
		{
		}
	}
}
