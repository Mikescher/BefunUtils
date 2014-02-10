using System;

namespace BefunGen.AST.Exceptions
{
	public class AccessTemporaryASTObjectException : Exception
	{
		public AccessTemporaryASTObjectException()
			: base("Trying to access temporary AST-object")
		{
		}
	}
}
