using BefunGen.MathExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST.CodeGen
{
	public static class CodeGenConstants
	{
		// Every code can manipulate this field for its own purpose
		public static MathExt.Point TMP_FIELD = new MathExt.Point(1, 0);
		// TopLeft of temporary Field for ReturnValue caching
		public static MathExt.Point TMP_FIELD_RETURNVAL = new MathExt.Point(3, 0);

		public const int VERTICAL_METHOD_DISTANCE = 4;	//TODO Set to 0 for optimal mash-code
		public const int LANE_VERTICAL_MARGIN = 4;		//TODO Set to 0 for optimal mash-code

		public const int MIN_METHOD_HEIGHT = 10;
	}
}
