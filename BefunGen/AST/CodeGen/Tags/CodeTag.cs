using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen.Tags
{
	public abstract class CodeTag
	{
		public readonly string UUID;
		public readonly string TagName;
		public readonly object TagParam;

		public CodeTag(string name) 
			: this(name, null)
		{
			//-
		}

		public CodeTag(string name, object param)
		{
			this.UUID = System.Guid.NewGuid().ToString("D");
			this.TagName = name;
			this.TagParam = param;
		}

		public bool hasParam()
		{
			return TagParam != null;
		}

		public override string ToString()
		{
			return (hasParam()) ? (string.Format("{0} ({1}) <{2}>", TagName, TagParam, UUID)) : (string.Format("{0} <{2}>", TagName, UUID));
		}
	}
}
