using System;

namespace BefunGen.AST.Exceptions
{
	public class UnresolvableReferenceException : Exception
	{
		public UnresolvableReferenceException(string s)
			: base("Could not resolve reference: '" + s + "'")
		{
		}
	}
}
