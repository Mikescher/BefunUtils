using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class WrongParameterCountException : ASTException
	{
		public WrongParameterCountException(int actual, int expected, SourceCodePosition pos)
			: base(String.Format("Parametercount ({0}) differs from expected ({1})", actual, expected), pos)
		{
		}
	}
}
