using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.Tags
{
	public class MethodCall_ReEntry_Tag : CodeTag
	{
		public MethodCall_ReEntry_Tag(Method source)
			: base("MethodCall_ReEntry", source)
		{
			//NOP
		}
	}
}
