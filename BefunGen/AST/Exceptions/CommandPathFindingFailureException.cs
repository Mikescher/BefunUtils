using System;

namespace BefunGen.AST.Exceptions
{
	public class CommandPathFindingFailureException : Exception
	{
		public CommandPathFindingFailureException(string msg)
			: base("Internal Pathfinding failure: " + msg)
		{
		}
	}
}
