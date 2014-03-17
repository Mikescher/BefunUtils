using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.Exceptions
{
	public abstract class BefunGenInternalException : BefunGenException
	{
		public BefunGenInternalException(string msg, SourceCodePosition pos)
			: base("INTERNAL", msg, pos)
		{
		}

		public BefunGenInternalException(string msg)
			: base("INTERNAL", msg)
		{
		}
	}
}
