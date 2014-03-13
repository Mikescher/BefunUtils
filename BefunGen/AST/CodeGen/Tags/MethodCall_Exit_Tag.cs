using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.Tags
{
	public class MethodCall_Exit_Tag : CodeTag
	{
		public MethodCall_Exit_Tag(Method target)
			: base("MethodCall_Exit", target)
		{
			//NOP
		}
	}
}
