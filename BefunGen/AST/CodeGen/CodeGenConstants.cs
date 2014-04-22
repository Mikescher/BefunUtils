using BefunGen.MathExtensions;

namespace BefunGen.AST.CodeGen
{
	public static class CodeGenConstants
	{

		public static MathExt.Point TMP_FIELD_IO_ARR = new MathExt.Point(1, 0);
		public static MathExt.Point TMP_FIELD_OUT_ARR = new MathExt.Point(2, 0);
		public static MathExt.Point TMP_FIELD_JMP_ADDR = new MathExt.Point(3, 0);
		// TopLeft of temporary Field for ReturnValue caching
		public static MathExt.Point TMP_ARRFIELD_RETURNVAL = new MathExt.Point(4, 0);

		public const int VERTICAL_METHOD_DISTANCE = 0;
		public const int LANE_VERTICAL_MARGIN = 0;
	}
}
