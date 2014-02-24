using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefungExec.Logic
{
	public static class RunOptions
	{
		public static bool INIT_PAUSED = true;
		public static Rect2i INIT_ZOOM = null;

		public static int SLEEP_TIME = 1;	// Time (ms) per Cycle
		public static int DECAY_TIME = 500; // TIme until decay
	}
}
