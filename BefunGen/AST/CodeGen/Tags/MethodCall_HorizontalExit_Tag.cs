
namespace BefunGen.AST.CodeGen.Tags
{
	public class MethodCall_HorizontalExit_Tag : CodeTag
	{
		public MethodCall_HorizontalExit_Tag(Method target)
			: base("Horizontal_MethodCall_Exit", target)
		{
			//NOP
		}

		public MethodCall_HorizontalExit_Tag(Statement_Label target)
			: base("Horizontal_MethodCall_Exit (Label)", target)
		{
			//NOP
		}

		public MethodCall_HorizontalExit_Tag(object target)
			: base("Horizontal_MethodCall_Exit ( ??? )", target)
		{
			//NOP
		}

		public MethodCall_HorizontalExit_Tag()
			: base("Vertical_MethodCall_Exit (PARAMLESS)")
		{
			//NOP
		}
	}
}
