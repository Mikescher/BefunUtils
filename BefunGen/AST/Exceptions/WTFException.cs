using System;

namespace BefunGen.AST.Exceptions
{
	public class WTFException : Exception
	{
		public WTFException()
			: base("This should not be possible to happen ... I'm confused")
		{
		}
	}
}
