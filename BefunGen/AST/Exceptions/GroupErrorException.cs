using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class GroupErrorException : ASTException
	{
		public GroupErrorException(SourceCodePosition pos)
			: base(pos)
		{
		}
	}
}
