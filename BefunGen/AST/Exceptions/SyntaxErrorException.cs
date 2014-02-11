using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class SyntaxErrorException : ASTException
	{
		public SyntaxErrorException(object data, string expect, SourceCodePosition pos)
			: base("Lexical Error on data='" + data + "', expected='" + expect + "'", pos)
		{
		}
	}
}
