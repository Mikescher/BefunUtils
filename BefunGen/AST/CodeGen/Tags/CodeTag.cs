
namespace BefunGen.AST.CodeGen.Tags
{
	public abstract class CodeTag
	{
		public readonly string UUID;
		public readonly string TagName;
		public readonly object TagParam;

		private bool _Active = true;
		public bool Active { get { return _Active; } }


		public CodeTag(string name)
			: this(name, null)
		{
			//-
		}

		public void deactivate()
		{
			_Active = false;
		}

		public bool isActive()
		{
			return Active;
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
			return 
				(hasParam()) 
				? 
				(string.Format("[{0}] {1} ({2}) <{3}>", Active ? "+" : "-", TagName, TagParam, UUID))
				:
				(string.Format("[{0}] {1} <{2}>",  Active ? "+" : "-", TagName, UUID));
		}
	}
}
