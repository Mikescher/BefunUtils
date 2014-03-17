using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.Exceptions
{
	public abstract class BefunGenUserException : BefunGenException
	{
		public BefunGenUserException(string msg, SourceCodePosition pos)
			: base("USER", msg, pos)
		{
		}
	}
}
