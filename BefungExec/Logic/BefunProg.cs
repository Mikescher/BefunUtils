using BefungExec.View;
using SuperBitBros.OpenGL.OGLMath;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BefungExec.Logic
{
	public class BefunProg
	{
		private static int[,] randDelta = { { 1, 0 }, { 0, -1 }, { -1, 0 }, { 0, 1 } };

		public bool running;
		public bool doSingleStep = false;
		public bool paused;
		public int mode = 0;

		public FrequencyCounter freq = new FrequencyCounter();

		public int[,] raster;
		public long[,] decay_raster;
		public bool[,] breakpoints;

		public int Width { get { return raster.GetLength(0); } }
		public int Height { get { return raster.GetLength(1); } }

		public int this[int x, int y] { get { return raster[x, y]; } }

		public Vec2i PC = new Vec2i(0, 0);
		public Vec2i delta = new Vec2i(1, 0);
		public bool stringmode = false;

		public Stack<int> Stack = new Stack<int>();

		private Vec2i dimension;

		Random rnd = new Random();

		public const int MODE_RUN = 0;
		public const int MODE_IN_INT = 1;
		public const int MODE_IN_CHAR = 2;
		public const int MODE_MOVEANDRUN = 3;

		public int curr_lvl_sleeptime;

		public bool reset_freeze_request = false;
		public bool reset_freeze_answer = false;

		public string err = null;

		public BefunProg(int[,] iras)
		{
			raster = iras;
			decay_raster = new long[Width, Height];
			breakpoints = new bool[Width, Height];

			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
				{
					decay_raster[x, y] = 0;
					breakpoints[x, y] = false;
				}

			dimension = new Vec2i(Width, Height);

			paused = RunOptions.INIT_PAUSED;

			curr_lvl_sleeptime = RunOptions.SLEEP_TIME;
		}

		public void run()
		{
			int skipcount;

			running = true;

			long start = Environment.TickCount;
			int sleeptime;

			while (running)
			{
				if ((paused && !doSingleStep) || mode != MODE_RUN)
				{
					if (mode == MODE_MOVEANDRUN)
					{
						move();
						mode = MODE_RUN;
					}
					else
					{
						Thread.Sleep(curr_lvl_sleeptime);
						decay();

						testForFreeze();

						start = Environment.TickCount;
					}
					continue;
				}
				freq.Inc();

				if (mode == MODE_RUN)
				{
					calc();
					debug();

					if (mode == MODE_RUN && (!paused || doSingleStep))
					{
						skipcount = 0;
						do
						{
							move();
							decay();
							conditionalbreak();
							debug();

							skipcount++;
							if (skipcount > Width * 2)
							{
								err = "Program entered infinite NOP-Loop";
								debug();
								break; // Even when no debug - no infinite loop in this thread
							}
						}
						while (RunOptions.SKIP_NOP && raster[PC.X, PC.Y] == ' ' && !stringmode && (!paused || doSingleStep));


					}
				}

				doSingleStep = false;

				sleeptime = (int)Math.Max(0, curr_lvl_sleeptime - (Environment.TickCount - start));
				if (curr_lvl_sleeptime != 0)
				{
					Thread.Sleep(sleeptime);
				}

				testForFreeze();

				start = Environment.TickCount;
			}
		}

		private void testForFreeze()
		{
			while (reset_freeze_request)
			{
				reset_freeze_answer = true;
				Thread.Sleep(0);
			}
			reset_freeze_answer = false;
		}

		private void conditionalbreak()
		{
			paused = paused || breakpoints[PC.X, PC.Y];
		}

		private void debug()
		{
			if (err != null && RunOptions.DEBUGRUN)
			{
				Console.WriteLine();
				Console.WriteLine("Debug Break: " + err);

				paused = true;
				err = null;
			}
		}

		private int pop()
		{
			lock (Stack)
			{
				if (Stack.Count == 0)
				{
					err = "Trying to pop an empty stack";
					return 0;
				}
				else
					return Stack.Pop();
			}
		}

		private int peek()
		{
			lock (Stack)
			{
				if (Stack.Count == 0)
				{
					err = "Trying to pop an empty stack"; // Yes, pop, not peek - no peek OP in Befunge
					return 0;
				}
				else
					return Stack.Peek();
			}
		}

		private bool popBool()
		{
			lock (Stack)
			{
				if (Stack.Count == 0)
				{
					err = "Trying to pop an empty stack"; // Yes, pop, not peek - no peek OP in Befunge
					return false;
				}
				else
					return (Stack.Pop() != 0);
			}
		}

		public void push(int a)
		{
			lock (Stack)
			{
				Stack.Push(a);
			}
		}

		public void push(bool a)
		{
			lock (Stack)
			{
				Stack.Push(a ? 1 : 0);
			}
		}

		private void calc()
		{
			int curr = raster[PC.X, PC.Y];

			if (stringmode && curr != '"')
			{
				push(curr);
			}
			else
			{
				int tmp, tmp2;

				switch (curr)
				{
					case ' ':
						break; // NOP
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						push(curr - '0');
						break;
					case '+':
						push(pop() + pop());
						break;
					case '-':
						tmp = pop();
						push(pop() - tmp);
						break;
					case '*':
						push(pop() * pop());
						break;
					case '/':
						tmp = pop();
						push(tmp != 0 ? (pop() / tmp) : 0);
						break;
					case '%':
						tmp = pop();
						push(tmp != 0 ? (pop() % tmp) : 0);
						break;
					case '!':
						push(!popBool());
						break;
					case '`':
						tmp = pop();
						push(pop() > tmp);
						break;
					case '>':
						delta.Set(1, 0);
						break;
					case '<':
						delta.Set(-1, 0);
						break;
					case '^':
						delta.Set(0, -1);
						break;
					case 'v':
						delta.Set(0, 1);
						break;
					case '?':
						tmp = rnd.Next(4);
						delta.Set(randDelta[tmp, 0], randDelta[tmp, 1]);
						break;
					case '_':
						tmp = popBool() ? 2 : 0;
						delta.Set(randDelta[tmp, 0], randDelta[tmp, 1]);
						break;
					case '|':
						tmp = popBool() ? 1 : 3;
						delta.Set(randDelta[tmp, 0], randDelta[tmp, 1]);
						break;
					case '"':
						stringmode = !stringmode;
						break;
					case ':':
						push(peek());
						break;
					case '\\':
						tmp = pop();
						tmp2 = pop();
						push(tmp);
						push(tmp2);
						break;
					case '$':
						pop();
						break;
					case '.':
						Console.Out.Write(pop());
						break;
					case ',':
						Console.Out.Write((char)pop());
						break;
					case '#':
						move();
						break;
					case 'g':
						tmp = pop();
						tmp2 = pop();
						if (tmp >= 0 && tmp2 >= 0 && tmp2 < Width && tmp < Height)
							push(raster[tmp2, tmp]);
						else
							push(0);
						break;
					case 'p':
						tmp = pop();
						tmp2 = pop();
						if (tmp >= 0 && tmp2 >= 0 && tmp2 < Width && tmp < Height)
							raster[tmp2, tmp] = pop();
						else
							pop();
						break;
					case '&':
						mode = MODE_IN_INT;
						break;
					case '~':
						mode = MODE_IN_CHAR;
						break;
					case '@':
						delta.Set(0, 0);
						break;
					default:
						err = String.Format("Unknown Operation at {0}|{1}: {2}({3})", PC.X, PC.Y, curr, (char)curr);
						// NOP
						break;
				}
			}
		}

		public void move()
		{
			PC += delta;

			int bx = PC.X;
			int by = PC.Y;

			PC += dimension;

			PC %= dimension;

			if (bx != PC.X || by != PC.Y)
				err = "PC wrapped around ledge (" + bx + "|" + by + ") - (" + PC.X + "|" + PC.Y + ")";
		}

		private void decay()
		{
			if (RunOptions.DECAY_TIME == 0)
			{
				for (int x = 0; x < Width; x++)
				{
					for (int y = 0; y < Height; y++)
					{
						decay_raster[x, y] = (PC.X == x && PC.Y == y) ? 1 : 0;
					}
				}
			}
			else
			{
				long now = Environment.TickCount;

				if (PC.X >= 0 && PC.Y >= 0)
					decay_raster[PC.X, PC.Y] = Environment.TickCount;
			}
		}

		public void full_reset(string code)
		{
			raster = Program.GetProg(code);
			PC = new Vec2i(0, 0);
			paused = true;
			doSingleStep = false;

			for (int x = 0; x < Width; x++)
				for (int y = 0; y < Height; y++)
				{
					decay_raster[x, y] = 0;
				}
			Stack.Clear();
			stringmode = false;
			delta = new Vec2i(1, 0);
			mode = MODE_RUN;
			running = true;
			dimension = new Vec2i(Width, Height);
		}
	}
}
