using System;

namespace BefunGen.AST.Exceptions
{
	public class InvalidEnumStateException : Exception
	{
		public InvalidEnumStateException(Enum m)
			: base("Enum is in an impossible State: " + m.ToString())
		{
		}
	}
}
