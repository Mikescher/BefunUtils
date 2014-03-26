using BefungExec.Logic;
using BefungExec.View.OpenGL;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using QuickFont;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace BefungExec.View
{
	public partial class MainForm : Form
	{
		private bool loaded_sv = false;
		private bool loaded_pv = false;
		private bool loaded { get { return loaded_sv && loaded_pv; } }

		private BefunProg prog;
		private string init_code;

		private FrequencyCounter fps = new FrequencyCounter();
		private FontRasterSheet font;
		private FontRasterSheet bwfont;
		private QFont DebugFont;
		private QFont StackFont;
		private QFont BoxFont;
		private InteropKeyboard kb = new InteropKeyboard();

		private List<int> currStack = new List<int>();

		private char? lastInput = null;

		private string currInput = "";

		private Rect2i selection = null;
		private Stack<Rect2i> zoom = new Stack<Rect2i>();

		public MainForm(BefunProg bp, string code)
		{
			InitializeComponent();

			prog = bp;
			init_code = code;

			zoom.Push(new Rect2i(0, 0, prog.Width, prog.Height));

			zoom.Push(RunOptions.INIT_ZOOM);
			if (zoom.Peek() == null || zoom.Peek().bl.X < 0 || zoom.Peek().bl.Y < 0 || zoom.Peek().tr.X > prog.Width || zoom.Peek().tr.Y > prog.Height)
				zoom.Pop();

			Application.Idle += Application_Idle;
		}

		void Application_Idle(object sender, EventArgs e)
		{
			if (!loaded) // Play nice
				return;

			if (glProgramView.IsIdle)
			{
				glProgramView.MakeCurrent();

				QFont.InvalidateViewport();

				updateProgramView();

				RenderProgramView();

			}

			if (glStackView.IsIdle)
			{
				glStackView.MakeCurrent();

				QFont.InvalidateViewport();

				RenderStackView();

			}

			glProgramView.Invalidate();
			glStackView.Invalidate();
		}

		#region Events

		private void glStackView_Load(object sender, EventArgs e)
		{
			glStackView.MakeCurrent();

			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Disable(EnableCap.CullFace);
			GL.Disable(EnableCap.DepthTest);

			QFontBuilderConfiguration builderConfig = new QFontBuilderConfiguration(true);
			builderConfig.ShadowConfig.blurRadius = 1; //reduce blur radius because font is very small
			builderConfig.TextGenerationRenderHint = TextGenerationRenderHint.ClearTypeGridFit; //best render hint for this font

			StackFont = new QFont(new Font("Arial", 24));
			StackFont.Options.DropShadowActive = true;
			StackFont.Options.Colour = Color4.White;


			loaded_sv = true;
		}

		private void glProgramView_Load(object sender, System.EventArgs e)
		{
			glProgramView.MakeCurrent();

			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Disable(EnableCap.CullFace);
			GL.Disable(EnableCap.DepthTest);

			QFontBuilderConfiguration builderConfig = new QFontBuilderConfiguration(true);
			builderConfig.ShadowConfig.blurRadius = 1; //reduce blur radius because font is very small
			builderConfig.TextGenerationRenderHint = TextGenerationRenderHint.ClearTypeGridFit; //best render hint for this font

			DebugFont = new QFont(new Font("Arial", 8));
			DebugFont.Options.DropShadowActive = true;
			DebugFont.Options.Colour = Color4.Black;

			BoxFont = new QFont(new Font("Arial", 16));
			BoxFont.Options.DropShadowActive = true;
			BoxFont.Options.Colour = Color4.Black;

			font = FontRasterSheet.create(RunOptions.SYNTAX_HIGHLIGHTING);
			bwfont = FontRasterSheet.create(false);

			loaded_pv = true;
		}

		private void glProgramView_Resize(object sender, EventArgs e)
		{
			glProgramView.MakeCurrent();

			GL.Viewport(0, 0, glProgramView.Width, glProgramView.Height);
			QFont.InvalidateViewport();

			glProgramView.Invalidate();
		}

		private void glStackView_Resize(object sender, EventArgs e)
		{
			glStackView.MakeCurrent();

			GL.Viewport(0, 0, glStackView.Width, glStackView.Height);
			QFont.InvalidateViewport();

			glStackView.Invalidate();
		}

		private void glProgramView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			int selx, sely;
			getPointInProgram(e.X, e.Y, out selx, out sely);

			if (selx != -1 && sely != -1)
			{
				selection = new Rect2i(selx, sely, 1, 1);
			}
		}

		private void glProgramView_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				selection = null;


			if (selection != null)
			{
				selection.normalize();

				int selx, sely;
				getPointInProgram(e.X, e.Y, out selx, out sely);

				if (selx == -1 && sely == -1)
					return;

				selection = new Rect2i(selection.bl, new Vec2i(selx + 1, sely + 1));
			}
		}

		private void glProgramView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				selection = null;

			if (selection != null)
			{
				if (Math.Abs(selection.Width) > 1 && Math.Abs(selection.Height) > 1)
				{
					selection.normalize();
					if (selection != zoom.Peek())
						zoom.Push(selection);
				}
				else if (selection.Width == 1 && selection.Height == 1)
				{
					prog.breakpoints[selection.bl.X, selection.bl.Y] = !prog.breakpoints[selection.bl.X, selection.bl.Y];
				}
			}

			selection = null;
		}

		private void glProgramView_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			lastInput = e.KeyChar;
		}

		#endregion

		private void RenderProgramView()
		{
			#region INIT

			fps.Inc();

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(Color.White);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0.0, glProgramView.Width, 0.0, glProgramView.Height, 0.0, 4.0);

			GL.Color3(1.0, 1.0, 1.0);

			#endregion

			#region SIZE

			double offx;
			double offy;
			double w;
			double h;
			calcProgPos(out offx, out offy, out w, out h);

			#endregion

			#region PROG

			long now = Environment.TickCount;

			int f_binded = -1;
			for (int x = zoom.Peek().bl.X; x < zoom.Peek().tr.X; x++)
			{
				for (int y = zoom.Peek().bl.Y; y < zoom.Peek().tr.Y; y++)
				{
					double decay_perc = (RunOptions.DECAY_TIME != 0) ? (1 - (now - prog.decay_raster[x, y] * 1d) / RunOptions.DECAY_TIME) : (prog.decay_raster[x, y]);
					decay_perc = Math.Min(1, decay_perc);

					double r = prog.breakpoints[x, y] ? decay_perc : 1;
					double g = prog.breakpoints[x, y] ? 0 : (1 - decay_perc);
					double b = prog.breakpoints[x, y] ? (1 - decay_perc) : (1 - decay_perc);

					GL.Color3(r, g, b);

					if (prog.breakpoints[x, y] || decay_perc > 0.25)
					{
						if (f_binded != 1)
							bwfont.bind();
						bwfont.Render(new Rect2d(offx + (x - zoom.Peek().bl.X) * w, offy + ((zoom.Peek().Height - 1) - (y - zoom.Peek().bl.Y)) * h, w, h), -4, prog[x, y]);
						f_binded = 1;
					}
					else
					{
						if (f_binded != 2)
							font.bind();
						font.Render(new Rect2d(offx + (x - zoom.Peek().bl.X) * w, offy + ((zoom.Peek().Height - 1) - (y - zoom.Peek().bl.Y)) * h, w, h), -4, prog[x, y]);
						f_binded = 2;
					}
				}
			}

			#endregion

			#region SELECTION

			if (selection != null)
			{
				Rect2d rect = new Rect2d(offx + ((selection.tl.X) - zoom.Peek().bl.X) * w, offy + ((zoom.Peek().Height - 1) - ((selection.tl.Y - 1) - zoom.Peek().bl.Y)) * h, selection.Width * w, selection.Height * h);

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

				GL.Begin(BeginMode.Quads);
				GL.Translate(0, 0, -4);
				GL.Color4(0.0, 0.0, 0.0, 0.5);
				GL.Vertex3(rect.tl.X, rect.tl.Y, 0);
				GL.Vertex3(rect.bl.X, rect.bl.Y, 0);
				GL.Vertex3(rect.br.X, rect.br.Y, 0);
				GL.Vertex3(rect.tr.X, rect.tr.Y, 0);
				GL.Color3(1.0, 1.0, 1.0);
				GL.Translate(0, 0, 4);
				GL.End();

				GL.Enable(EnableCap.Texture2D);
			}

			#endregion

			#region INPUT

			if (prog.mode != BefunProg.MODE_RUN)
			{
				int bw = 512;
				int bh = 128;

				int box = (glProgramView.Width - bw) / 2;
				int boy = (glProgramView.Height - bh) / 2;

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
						lastInput = null;
						prog.mode = BefunProg.MODE_MOVEANDRUN;
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

				RenderFont(glProgramView.Height, new Vec2d(box, boy), "Please enter a " + ((prog.mode == BefunProg.MODE_IN_INT) ? "number" : "character"), -1, BoxFont, true);

				RenderFont(glProgramView.Height, new Vec2d(box, boy + 64), currInput, -1, BoxFont, true);
			}
			else
			{
				currInput = "";
			}
			lastInput = null;

			#endregion

			#region DEBUG

			if (kb.isDown(Keys.Tab))
				RenderFont(glProgramView.Height, new Vec2d(0f, 0f), String.Format("FPS: {0} || SPEED: {1}", (int)fps.Frequency, getFreqFormatted()), -1, DebugFont, true);

			#endregion

			#region FINISH

			glProgramView.SwapBuffers();

			#endregion
		}

		private void updateProgramView()
		{
			kb.update();


			bool isrun = (prog.mode == BefunProg.MODE_RUN);

			if (isrun && kb[Keys.Escape])
			{
				if (zoom.Count > 1)
				{
					zoom.Pop();
				}
				else
				{
					//Exit();
				}
			}

			if (isrun && kb[Keys.Space] && prog.mode == BefunProg.MODE_RUN)
			{
				prog.paused = !prog.paused;
			}

			if (kb[Keys.Back])
			{
				if (currInput.Length > 0)
					currInput = currInput.Substring(0, currInput.Length - 1);
			}

			if (kb[Keys.Enter])
			{
				if (prog.mode == BefunProg.MODE_IN_INT && currInput.Length > 0 && currInput != "-")
				{
					prog.push(int.Parse(currInput));
					currInput = "";
					lastInput = null;
					prog.mode = BefunProg.MODE_MOVEANDRUN;
				}
			}

			if (isrun && kb[Keys.Right])
			{
				prog.doSingleStep = true;
			}

			if (isrun && kb[Keys.D1])
			{
				prog.curr_lvl_sleeptime = RunOptions.LOW_SLEEP_TIME;
			}

			if (isrun && kb[Keys.D2])
			{
				prog.curr_lvl_sleeptime = RunOptions.SLEEP_TIME;
			}

			if (isrun && kb[Keys.D3])
			{
				prog.curr_lvl_sleeptime = RunOptions.TOP_SLEEP_TIME;
			}

			if (isrun && kb[Keys.R])
			{
				reset();
			}

			if (isrun && kb[Keys.C])
			{
				for (int x = 0; x < prog.Width; x++)
				{
					for (int y = 0; y < prog.Height; y++)
					{
						prog.breakpoints[x, y] = false;
					}
				}
			}
		}

		private void RenderStackView()
		{
			#region INIT

			GL.Clear(ClearBufferMask.ColorBufferBit);
			GL.ClearColor(Color.Black);

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(0.0, glStackView.Width, 0.0, glStackView.Height, 0.0, 4.0);

			GL.Color3(1.0, 1.0, 1.0);

			#endregion

			#region STACK

			currStack.Clear();

			lock (prog.Stack)
			{
				currStack.AddRange(prog.Stack);
			}

			float fh = 15 + RenderFont(glStackView.Height, new Vec2d(10f, 15f), "Stack<" + currStack.Count + ">", -1, StackFont, false) * 1.15f;
			for (int i = 0; i < currStack.Count; i++)
			{
				int val = currStack[i];

				string sval;
				if (val >= 32 && val <= 126)
					sval = string.Format("{0} <{1}>", val, (char)val);
				else
					sval = "" + val;

				fh += RenderFont(glStackView.Height, new Vec2d(10f, fh), sval, -1, StackFont, false) * 1.15f;
				if (fh > 2 * glStackView.Height)
					break;
			}

			#endregion

			#region FINISH

			glStackView.SwapBuffers();

			#endregion
		}

		#region Helper

		private void reset()
		{
			prog.reset_freeze_request = true;

			while (!prog.reset_freeze_answer)
				Thread.Sleep(0);

			prog.full_reset(init_code);

			Console.WriteLine();
			Console.WriteLine();

			Console.WriteLine("########## OUTPUT ##########");

			Console.WriteLine();
			Console.WriteLine();

			prog.reset_freeze_request = false;
		}

		private void calcProgPos(out double offx, out double offy, out double w, out double h)
		{
			int th = zoom.Peek().Height - 1;

			offx = 0;
			offy = 0;

			w = ((glProgramView.Width) * 1.0) / zoom.Peek().Width;
			h = (glProgramView.Height * 1.0) / zoom.Peek().Height;

			if ((w / h) < (8.0 / 12.0))
			{
				offy = h * zoom.Peek().Height;
				h = (12.0 * w) / (8.0);
				offy -= h * zoom.Peek().Height;
				offy /= 2;
				offx = 0;
			}
			else if ((w / h) > (8.0 / 12.0))
			{
				offx = w * zoom.Peek().Width;
				w = (8.0 * h) / (12.0);
				offx -= w * zoom.Peek().Width;
				offx /= 2;
			}
			else
			{
				offx = 0;
			}
		}

		public float RenderFont(int CompHeight, Vec2d pos, string text, int distance, QFont fnt, bool backg)
		{
			float w = fnt.Measure(text).Width;
			float h = fnt.Measure(text).Height;

			Rect2d rect = new Rect2d(pos.X, pos.Y + CompHeight - h, w, h);

			if (backg)
			{
				GL.Disable(EnableCap.Texture2D);
				GL.Begin(BeginMode.Quads);
				GL.Translate(0, 0, -4);
				GL.Color4(Color.FromArgb(235, 255, 255, 255));
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

		private String getFreqFormatted()
		{
			string pref = "";
			double freq = prog.freq.Frequency;

			if (freq > 1000)
			{
				freq /= 1000;
				pref = "k";

				if (freq > 1000)
				{
					freq /= 1000;
					pref = "M";

					if (freq > 1000)
					{
						freq /= 1000;
						pref = "G";
					}
				}
			}

			return String.Format(@"{0:0.00} {1}Hz", freq, pref);
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
					if (new Rect2d(offx + (x - zoom.Peek().bl.X) * w, offy + (y - zoom.Peek().bl.Y) * h, w, h).Includes(new Vec2d(px, py)))
					{
						selx = x;
						sely = y;
						return;
					}
				}
			}
		}

		#endregion
	}
}
