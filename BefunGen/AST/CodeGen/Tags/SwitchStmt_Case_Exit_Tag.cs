
namespace BefunGen.AST.CodeGen.Tags
{
	public class SwitchStmt_Case_Exit_Tag : CodeTag
	{
		public SwitchStmt_Case_Exit_Tag(bool active = true)
			: base("SwitchStmt_Case_Exit")
		{
			if (!active)
				deactivate();
		}
	}
}
