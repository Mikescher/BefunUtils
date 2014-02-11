using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class IndexOperatorNotDefiniedException : ASTException
	{
		public IndexOperatorNotDefiniedException(SourceCodePosition pos)
			: base("Cant perform index operation here", pos)
		{
		}
	}
}
