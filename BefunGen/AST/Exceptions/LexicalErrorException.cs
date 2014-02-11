using BefunGen.AST.CodeGen;
using System;

namespace BefunGen.AST.Exceptions
{
	public class LexicalErrorException : ASTException
	{
		public LexicalErrorException(object data, SourceCodePosition pos)
			: base("Lexical Error on data='"+data+"'", pos)
		{
		}
	}
}
