using BefungExec.Logic;
using BefungExec.View.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Drawing;

namespace BefungExec.View
{
	public class MainView : GameWindow
	{
		private FrequencyCounter fps = new FrequencyCounter();
		private FontRasterSheet font;
		private QFont DebugFont;
		private BefunProg prog;


		public MainView(BefunProg model)
			: base(1280, 480)
		{
			prog = model;

			Load += new EventHandler<EventArgs>(OnLoad);
			Resize += new EventHandler<EventArgs>(OnResize);
			UpdateFrame += new EventHandler<FrameEventArgs>(OnUpdate);
			RenderFrame += new EventHandler<FrameEventArgs>(OnRender);
			Closed += new EventHandler<EventArgs>(OnClose);

			Run();
		}

		private void OnLoad(object o, EventArgs arg)
		{
			GL.Enable(EnableCap.Texture2D);
			GL.Disable(EnableCap.CullFace);
			GL.Disable(EnableCap.DepthTest);

			QFontBuilderConfiguration builderConfig = new QFontBuilderConfiguration(true);
			builderConfig.ShadowConfig.blurRadius = 1; //reduce blur radius because font is very small
			builderConfig.TextGenerationRenderHint = TextGenerationRenderHint.ClearTypeGridFit; //best render hint for this font
			DebugFont = new QFont(new Font("Arial", 8));
			DebugFont.Options.DropShadowActive = true;
			DebugFont.Options.Colour = Color4.Black;

			font = FontRasterSheet.create();
		}

		private void OnResize(object o, EventArgs arg)
		{
			GL.Viewport(0, 0, Width, Height);
			QFont.InvalidateViewport();
		}

		private bool lastState_Space = false;
		private void OnUpdate(object o, FrameEventArgs arg)
		{
			if (Keyboard[Key.Escape])
			{
				Exit();
			}
			else if (Keyboard[Key.Space] && !lastState_Space)
			{
				prog.paused = !prog.paused;
			}
			lastState_Space = Keyboard[Key.Space];

		}

		private void OnRender(object o, FrameEventArgs arg)
		{
			fps.Inc();

			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0.0, Width, 0.0, Height, 0.0, 4.0);

			GL.Color3(1.0, 1.0, 1.0);

			// ################

			font.bind();

			int th = prog.Height - 1;

			double offx = 0;
			double offy = 0;

			double w = (Width * 1.0) / prog.Width;
			double h = (Height * 1.0) / prog.Height;

			if ((w / h) < (8.0 / 12.0))
			{
				offy = h * prog.Height;
				h = (12.0 * w) / (8.0);
				offy -= h * prog.Height;
				offy /= 2;
			}
			else if ((w / h) > (8.0 / 12.0))
			{
				w = (8.0 * h) / (12.0);
			}


			for (int x = 0; x < prog.Width; x++)
			{
				for (int y = 0; y < prog.Height; y++)
				{
					GL.Color3(1, 1 - prog.decay_raster[x, y], 1 - prog.decay_raster[x, y]);
					font.Render(new Rect2d(offx + x * w, offy + (th - y) * h, w, h), -4, prog[x, y]);
				}
			}

			// ################

			RenderFont(new Vec2d(0f, 0f), String.Format("FPS: {0} || SPEED: {1}", fps.Frequency, (int)prog.freq.Frequency), -1);

			SwapBuffers();
		}

		public void RenderFont(Vec2d pos, string text, int distance)
		{
			float w = DebugFont.Measure(text).Width;
			float h = DebugFont.Measure(text).Height;

			Rect2d rect = new Rect2d(pos.X, pos.Y + Height - h, w, h);

			GL.Disable(EnableCap.Texture2D);
			GL.Begin(BeginMode.Quads);
			GL.Translate(0, 0, -4);
			GL.Color4(Color.FromArgb(192, 255, 255, 255));
			GL.Vertex3(rect.tl.X, rect.tl.Y, 0);
			GL.Vertex3(rect.bl.X, rect.bl.Y, 0);
			GL.Vertex3(rect.br.X, rect.br.Y, 0);
			GL.Vertex3(rect.tr.X, rect.tr.Y, 0);
			GL.Color3(1.0, 1.0, 1.0);
			GL.Translate(0, 0, 4);
			GL.End();
			GL.Enable(EnableCap.Texture2D);

			QFont.Begin();
			GL.PushMatrix();

			GL.Translate(0, 0, distance);
			DebugFont.Print(text, new Vector2((float)pos.X, (float)pos.Y));

			GL.PopMatrix();
			QFont.End();

			GL.Color3(1.0, 1.0, 1.0);
		}

		private void OnClose(object o, EventArgs arg)
		{
			prog.running = false;
		}
	}
}
