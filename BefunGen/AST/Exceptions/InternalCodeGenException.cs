using BefunGen.AST.CodeGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.Exceptions
{
	public class InternalCodeGenException : Exception
	{
		public InternalCodeGenException()
			: base("Internal CodeGen Exception")
		{

		}
	}
}
