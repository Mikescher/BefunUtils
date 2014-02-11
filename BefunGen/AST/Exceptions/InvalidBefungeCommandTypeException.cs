using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class InvalidBefungeCommandTypeException : ASTException
	{
		public InvalidBefungeCommandTypeException(SourceCodePosition pos)
			: base("BefungeCMD-Enum is in an impossible State", pos)
		{
		}
	}
}
