using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class InvalidFormatSpecifierException : ASTException
	{
		public InvalidFormatSpecifierException(string fs, SourceCodePosition pos)
			: base(String.Format("Formatspecifier '{0}' is unknown", fs), pos)
		{
		}
	}
}
