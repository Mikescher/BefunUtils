using BefungExec.Logic;
using BefungExec.View.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using QuickFont;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BefungExec.View
{
	public class MainView : GameWindow
	{
		private FrequencyCounter fps = new FrequencyCounter();
		private FontRasterSheet font;
		private QFont DebugFont;
		private QFont StackFont;
		private QFont BoxFont;
		private BefunProg prog;

		private List<int> currStack = new List<int>();

		private char? lastInput = null;

		private string currInput = "";

		public MainView(BefunProg model)
			: base(1280 + 250, 480)
		{
			prog = model;

			Load += new EventHandler<EventArgs>(OnLoad);
			Resize += new EventHandler<EventArgs>(OnResize);
			UpdateFrame += new EventHandler<FrameEventArgs>(OnUpdate);
			RenderFrame += new EventHandler<FrameEventArgs>(OnRender);
			Closed += new EventHandler<EventArgs>(OnClose);
			KeyPress += new EventHandler<KeyPressEventArgs>(OnKeyPress);

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

			StackFont = new QFont(new Font("Arial", 24));
			StackFont.Options.DropShadowActive = true;
			StackFont.Options.Colour = Color4.White;

			BoxFont = new QFont(new Font("Arial", 16));
			BoxFont.Options.DropShadowActive = true;
			BoxFont.Options.Colour = Color4.Black;

			font = FontRasterSheet.create();
		}

		private void OnResize(object o, EventArgs arg)
		{
			GL.Viewport(0, 0, Width, Height);
			QFont.InvalidateViewport();
		}

		private void OnKeyPress(object o, KeyPressEventArgs arg)
		{
			lastInput = arg.KeyChar;
		}

		private bool lastState_Space = false;
		private bool lastState_BSpace = false;
		private bool lastState_Enter = false;
		private bool lastState_Right = false;
		private void OnUpdate(object o, FrameEventArgs arg)
		{
			if (Keyboard[Key.Escape])
			{
				Exit();
			}
			else if (Keyboard[Key.Space] && !lastState_Space && prog.mode == BefunProg.MODE_RUN)
			{
				prog.paused = !prog.paused;
			}
			else if (Keyboard[Key.BackSpace] && !lastState_BSpace)
			{
				if (currInput.Length > 0)
					currInput = currInput.Substring(0, currInput.Length - 1);
			}
			else if (Keyboard[Key.Enter] && !lastState_Enter)
			{
				if (prog.mode == BefunProg.MODE_IN_INT && currInput.Length > 0 && currInput != "-")
				{
					prog.push(int.Parse(currInput));
					currInput = "";
					prog.mode = BefunProg.MODE_RUN;
					prog.move();
				}
				if (prog.mode == BefunProg.MODE_IN_CHAR && currInput.Length == 0)
				{
					prog.push(0);
					currInput = "";
					prog.mode = BefunProg.MODE_RUN;
					prog.move();
				}
			}
			else if (Keyboard[Key.Right] && !lastState_Right)
			{
				prog.doSingleStep = true;
			}

			lastState_Space = Keyboard[Key.Space];
			lastState_BSpace = Keyboard[Key.BackSpace];
			lastState_BSpace = Keyboard[Key.Enter];
			lastState_Right = Keyboard[Key.Right];
		}

		private void OnRender(object o, FrameEventArgs arg)
		{
			#region INIT


			fps.Inc();

			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0.0, Width, 0.0, Height, 0.0, 4.0);

			GL.Color3(1.0, 1.0, 1.0);

			#endregion

			#region SIZE

			font.bind();

			int th = prog.Height - 1;

			double offx = 0;
			double offy = 0;

			int stackwidth = 250;

			double w = ((Width - stackwidth) * 1.0) / prog.Width;
			double h = (Height * 1.0) / prog.Height;

			if ((w / h) < (8.0 / 12.0))
			{
				offy = h * prog.Height;
				h = (12.0 * w) / (8.0);
				offy -= h * prog.Height;
				offy /= 2;
				offx = stackwidth;
			}
			else if ((w / h) > (8.0 / 12.0))
			{
				offx = w * prog.Width - stackwidth;
				w = (8.0 * h) / (12.0);
				offx -= w * prog.Width - stackwidth;
				offx /= 2;
				offx += stackwidth;
			}
			else
			{
				offx = stackwidth;
			}

			#endregion

			#region PROG

			for (int x = 0; x < prog.Width; x++)
			{
				for (int y = 0; y < prog.Height; y++)
				{
					GL.Color3(1, 1 - prog.decay_raster[x, y], 1 - prog.decay_raster[x, y]);
					font.Render(new Rect2d(offx + x * w, offy + (th - y) * h, w, h), -4, prog[x, y]);
				}
			}

			#endregion

			#region STACK

			currStack.Clear();

			lock (prog.Stack)
			{
				currStack.AddRange(prog.Stack);
			}

			float fh = 15 + RenderFont(new Vec2d(10f, 15f), "Stack<" + currStack.Count + ">", -1, StackFont, false) * 1.15f;
			for (int i = 0; i < currStack.Count; i++)
			{
				fh += RenderFont(new Vec2d(10f, fh), "" + currStack[i], -1, StackFont, false) * 1.15f;
				if (h > 2 * Width)
					break;
			}

			#endregion

			#region INPUT

			if (prog.mode != BefunProg.MODE_RUN)
			{
				int bw = 512;
				int bh = 128;

				int box = (Width - bw) / 2;
				int boy = (Height - bh) / 2;

				if (lastInput != null)
				{
					if (prog.mode == BefunProg.MODE_IN_INT && (char.IsDigit(lastInput.Value) || (currInput.Length == 0 && lastInput.Value == '-')))
					{
						currInput += lastInput;
					}
					if (prog.mode == BefunProg.MODE_IN_CHAR && currInput.Length == 0)
					{
						currInput += lastInput;

						prog.push(currInput[0]);
						currInput = "";
						prog.mode = BefunProg.MODE_RUN;
						prog.move();
					}
				}

				Rect2d rect = new Rect2d(box, boy, bw, bh);

				GL.Disable(EnableCap.Texture2D);
				GL.Begin(BeginMode.Quads);
				GL.Translate(0, 0, -3);
				GL.Color4(Color.FromArgb(255, 128, 128, 128));
				GL.Vertex3(rect.tl.X, rect.tl.Y, 0);
				GL.Vertex3(rect.bl.X, rect.bl.Y, 0);
				GL.Vertex3(rect.br.X, rect.br.Y, 0);
				GL.Vertex3(rect.tr.X, rect.tr.Y, 0);
				GL.Color3(1.0, 1.0, 1.0);
				GL.Translate(0, 0, 3);
				GL.End();
				GL.Enable(EnableCap.Texture2D);

				RenderFont(new Vec2d(box, boy), "Please enter a " + ((prog.mode == BefunProg.MODE_IN_INT) ? "number" : "character"), -1, BoxFont, true);

				RenderFont(new Vec2d(box, boy + 64), currInput, -1, BoxFont, true);
			}
			else
			{
				currInput = "";
			}
			lastInput = null;

			#endregion

			#region DEBUG

			RenderFont(new Vec2d(Width-350, 0f), String.Format("FPS: {0} || SPEED: {1}", fps.Frequency, (int)prog.freq.Frequency), -1, DebugFont, true);

			#endregion

			#region FINISH

			SwapBuffers();

			#endregion
		}

		public float RenderFont(Vec2d pos, string text, int distance, QFont fnt, bool backg)
		{
			float w = fnt.Measure(text).Width;
			float h = fnt.Measure(text).Height;

			Rect2d rect = new Rect2d(pos.X, pos.Y + Height - h, w, h);

			if (backg)
			{
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
			}

			QFont.Begin();
			GL.PushMatrix();

			GL.Translate(0, 0, distance);
			fnt.Print(text, new Vector2((float)pos.X, (float)pos.Y));

			GL.PopMatrix();
			QFont.End();

			GL.Color3(1.0, 1.0, 1.0);

			return h;
		}

		private void OnClose(object o, EventArgs arg)
		{
			prog.running = false;
		}
	}
}
