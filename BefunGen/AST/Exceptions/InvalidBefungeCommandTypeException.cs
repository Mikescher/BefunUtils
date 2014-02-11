using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class InvalidBefungeCommandTypeException : Exception
	{
		public InvalidBefungeCommandTypeException()
			: base("BefungeCMD-Enum is in an impossible State")
		{
		}
	}
}
