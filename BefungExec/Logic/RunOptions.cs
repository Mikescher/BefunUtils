using SuperBitBros.OpenGL.OGLMath;

namespace BefungExec.Logic
{
	public static class RunOptions
	{
		public static bool INIT_PAUSED = true;
		public static Rect2i INIT_ZOOM = null;

		public static int TOP_SLEEP_TIME = 0;	// Time (ms) per Cycle
		public static int SLEEP_TIME = 1;		// Time (ms) per Cycle
		public static int LOW_SLEEP_TIME = 250;	// Time (ms) per Cycle

		public static int DECAY_TIME = 500;		// Time until decay

		public static bool SYNTAX_HIGHLIGHTING = true;

		public static bool SKIP_NOP = true;
		public static bool DEBUGRUN = true;
	}
}
