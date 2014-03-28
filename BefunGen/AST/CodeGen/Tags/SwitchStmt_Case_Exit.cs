
namespace BefunGen.AST.CodeGen.Tags
{
	public class SwitchStmt_Case_Exit : CodeTag
	{
		public SwitchStmt_Case_Exit(bool active = true)
			: base("SwitchStmt_Case_Exit")
		{
			if (!active)
				deactivate();
		}
	}
}
