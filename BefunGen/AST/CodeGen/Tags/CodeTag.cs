using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.Tags
{
	public abstract class CodeTag
	{
		public readonly string UUID;
		public readonly string name;
		public readonly object param;

		public CodeTag(string name) 
			: this(name, null)
		{
			//-
		}

		public CodeTag(string name, object param)
		{
			this.UUID = System.Guid.NewGuid().ToString("D");
			this.name = name;
			this.param = param;
		}

		public bool hasParam()
		{
			return param != null;
		}

		public override string ToString()
		{
			return (hasParam()) ? (string.Format("{0} ({1}) <{2}>", name, param, UUID)) : (string.Format("{0} <{2}>", name, UUID));
		}
	}
}
