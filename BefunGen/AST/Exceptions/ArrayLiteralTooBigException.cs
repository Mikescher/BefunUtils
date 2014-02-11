using System;

namespace BefunGen.AST.Exceptions
{
	public class ArrayLiteralTooBigException : Exception
	{
		public ArrayLiteralTooBigException()
			: base("Operation not possible on <void>")
		{
		}
	}
}
