using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class InvalidCodeManipulationException : Exception
	{
		public InvalidCodeManipulationException(string msg)
			: base("Invalid Codemanipulation: " + msg)
		{
		}
	}
}
