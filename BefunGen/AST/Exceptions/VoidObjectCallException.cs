using System;

namespace BefunGen.AST.Exceptions
{
	public class VoidObjectCallException : Exception
	{
		public VoidObjectCallException()
			: base("Operation not possible on <void>")
		{
		}
	}
}
