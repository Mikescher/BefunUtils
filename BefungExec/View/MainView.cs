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

		private Rect2i selection = null;
		private Rect2i zoom = null;

		public MainView(BefunProg model)
			: base(Math.Min((int)(((model.Width / (model.Height * 1d)) * 480) * (8.0 / 12.0)) + 250, 1024), 480)
		{
			Title = "-- BefungExec --";

			prog = model;

			zoom = RunOptions.INIT_ZOOM;
			if (zoom == null || zoom.bl.X < 0 || zoom.bl.Y < 0 || zoom.tr.X > prog.Width ||zoom.tr.Y > prog.Height) 
				zoom = new Rect2i(0, 0, prog.Width, prog.Height);

			Load += new EventHandler<EventArgs>(OnLoad);
			Resize += new EventHandler<EventArgs>(OnResize);
			UpdateFrame += new EventHandler<FrameEventArgs>(OnUpdate);
			RenderFrame += new EventHandler<FrameEventArgs>(OnRender);
			Closed += new EventHandler<EventArgs>(OnClose);
			KeyPress += new EventHandler<KeyPressEventArgs>(OnKeyPress);
			Mouse.ButtonDown += new EventHandler<MouseButtonEventArgs>(OnMouseDown);
			Mouse.ButtonUp += new EventHandler<MouseButtonEventArgs>(OnMouseUp);
			Mouse.Move += new EventHandler<MouseMoveEventArgs>(OnMouseMove);

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

		private bool lastState_Esc = false;
		private bool lastState_Space = false;
		private bool lastState_BSpace = false;
		private bool lastState_Enter = false;
		private bool lastState_Right = false;
		private void OnUpdate(object o, FrameEventArgs arg)
		{
			if (Keyboard[Key.Escape] && !lastState_Esc)
			{
				if (zoom != null)
				{
					zoom = new Rect2i(0, 0, prog.Width, prog.Height);
				}
				else
				{
					Exit();
				}
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
			lastState_Esc = Keyboard[Key.Escape];
		}

		private void OnMouseDown(object o, MouseButtonEventArgs arg)
		{
			if (!Mouse[MouseButton.Left])
				return;

			int selx, sely;
			getPointInProgram(arg.X, arg.Y, out selx, out sely);

			if (selx != -1 && sely != -1)
			{
				selection = new Rect2i(selx, sely, 1, 1);
			}
		}

		private void getPointInProgram(int px, int py, out int selx, out int sely)
		{
			double offx;
			double offy;
			double w;
			double h;
			calcProgPos(out offx, out offy, out w, out h);

			selx = -1;
			sely = -1;

			for (int x = 0; x < prog.Width; x++)
			{
				for (int y = 0; y < prog.Height; y++)
				{
					if (new Rect2d(offx + x * w, offy + y * h, w, h).Includes(new Vec2d(px, py)))
					{
						selx = x;
						sely = y;
					}
				}
			}
		}

		private void OnMouseUp(object o, MouseButtonEventArgs arg)
		{
			if (arg.Button == MouseButton.Left)
			{
				if (selection != null)
				{
					if (Math.Abs(selection.Width) > 1 && Math.Abs(selection.Height) > 1)
					{
						selection.normalize();
						zoom = selection;
					}
					else if (selection.Width == 1 && selection.Height == 1)
					{
						prog.breakpoints[selection.bl.X, selection.bl.Y] = !prog.breakpoints[selection.bl.X, selection.bl.Y];
					}
				}

				selection = null;
			}
		}

		private void OnMouseMove(object o, MouseMoveEventArgs arg)
		{
			if (!Mouse[MouseButton.Left])
				selection = null;


			if (selection != null)
			{
				selection.normalize();

				int selx, sely;
				getPointInProgram(arg.X, arg.Y, out selx, out sely);

				selection = new Rect2i(selection.bl, new Vec2i(selx + 1, sely + 1));
			}
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

			double offx;
			double offy;
			double w;
			double h;
			calcProgPos(out offx, out offy, out w, out h);

			#endregion

			#region PROG

			long now = Environment.TickCount;


			for (int x = zoom.bl.X; x < zoom.tr.X; x++)
			{
				for (int y = zoom.bl.Y; y < zoom.tr.Y; y++)
				{
					double decay_perc = (RunOptions.DECAY_TIME != 0) ? (1 - (now - prog.decay_raster[x, y] * 1d) / RunOptions.DECAY_TIME) : (prog.decay_raster[x, y]);
					decay_perc = Math.Min(1, decay_perc);

					double r = prog.breakpoints[x, y] ? decay_perc : 1;
					double g = prog.breakpoints[x, y] ? 0 : (1 - decay_perc);
					double b = prog.breakpoints[x, y] ? (1 - decay_perc) : (1 - decay_perc);

					GL.Color3(r, g, b);

					font.Render(new Rect2d(offx + (x - zoom.bl.X) * w, offy + ((zoom.Height - 1) - (y - zoom.bl.Y)) * h, w, h), -4, prog[x, y]);

				}
			}

			#endregion

			#region SELECTION

			if (selection != null)
			{
				Rect2d rect = new Rect2d(offx + (selection.tl.X) * w, offy + (zoom.Height - (selection.tl.Y - 0 * zoom.bl.Y)) * h, selection.Width * w, selection.Height * h);

				GL.Disable(EnableCap.Texture2D);

				GL.Begin(BeginMode.LineLoop);
				GL.Translate(0, 0, -3);
				GL.Color4(Color.Black);
				GL.Vertex3(rect.tl.X, rect.tl.Y, 0);
				GL.Vertex3(rect.bl.X, rect.bl.Y, 0);
				GL.Vertex3(rect.br.X, rect.br.Y, 0);
				GL.Vertex3(rect.tr.X, rect.tr.Y, 0);
				GL.Color3(1.0, 1.0, 1.0);
				GL.Translate(0, 0, 3);
				GL.End();

				GL.Enable(EnableCap.Texture2D);
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

			RenderFont(new Vec2d(Width - 350, 0f), String.Format("FPS: {0} || SPEED: {1} Hz", (int)fps.Frequency, (int)prog.freq.Frequency), -1, DebugFont, true);

			#endregion

			#region FINISH

			SwapBuffers();

			#endregion
		}

		private void calcProgPos(out double offx, out double offy, out double w, out double h)
		{
			int th = zoom.Height - 1;

			offx = 0;
			offy = 0;

			int stackwidth = 250;

			w = ((Width - stackwidth) * 1.0) / zoom.Width;
			h = (Height * 1.0) / zoom.Height;

			if ((w / h) < (8.0 / 12.0))
			{
				offy = h * zoom.Height;
				h = (12.0 * w) / (8.0);
				offy -= h * zoom.Height;
				offy /= 2;
				offx = stackwidth;
			}
			else if ((w / h) > (8.0 / 12.0))
			{
				offx = w * zoom.Width - stackwidth;
				w = (8.0 * h) / (12.0);
				offx -= w * zoom.Width - stackwidth;
				offx /= 2;
				offx += stackwidth;
			}
			else
			{
				offx = stackwidth;
			}
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
