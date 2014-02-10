using System;

namespace BefunGen.AST.Exceptions
{
	public class IndexOperatorNotDefiniedException : Exception
	{
		public IndexOperatorNotDefiniedException()
			: base("Cant perform index operation here")
		{
		}
	}
}
