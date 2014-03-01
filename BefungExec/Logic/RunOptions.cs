using SuperBitBros.OpenGL.OGLMath;

namespace BefungExec.Logic
{
	public static class RunOptions
	{
		public static bool INIT_PAUSED = true;
		public static Rect2i INIT_ZOOM = null;

		public static int SLEEP_TIME = 0;	// Time (ms) per Cycle
		public static int DECAY_TIME = 500; // TIme until decay

		public static bool SYNTAX_HIGHLIGHTING = false;
	}
}
