
namespace BefunGen.AST.CodeGen.Tags
{
	public class MethodCall_VerticalExit_Tag : CodeTag
	{
		public MethodCall_VerticalExit_Tag(Method target)
			: base("Vertical_MethodCall_Exit (Method)", target)
		{
			//NOP
		}

		public MethodCall_VerticalExit_Tag(Statement_Label target)
			: base("Vertical_MethodCall_Exit (Label)", target)
		{
			//NOP
		}

		public MethodCall_VerticalExit_Tag(object target)
			: base("Vertical_MethodCall_Exit (" + target.GetType().Name + ")", target)
		{
			//NOP
		}

		public MethodCall_VerticalExit_Tag()
			: base("Vertical_MethodCall_Exit (PARAMLESS)")
		{
			//NOP
		}
	}
}
