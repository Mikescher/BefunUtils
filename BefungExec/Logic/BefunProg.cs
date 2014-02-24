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
		private Vec2i delta = new Vec2i(1, 0);
		private bool stringmode = false;

		public Stack<int> Stack = new Stack<int>();

		private Vec2i dimension;

		Random rnd = new Random();

		public const int MODE_RUN = 0;
		public const int MODE_IN_INT = 1;
		public const int MODE_IN_CHAR = 2;

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
		}

		public void run()
		{
			running = true;

			long start = Environment.TickCount;
			int sleeptime;

			while (running)
			{
				if ((paused && !doSingleStep) || mode != MODE_RUN)
				{
					Thread.Sleep(RunOptions.SLEEP_TIME);
					decay();
					continue;
				}
				freq.Inc();

				calc();
				if (mode == MODE_RUN)
				{
					move();
					decay();

					paused = paused || breakpoints[PC.X, PC.Y];
				}

				doSingleStep = false;

				if (RunOptions.SLEEP_TIME != 0)
				{
					sleeptime = (int)Math.Max(0, RunOptions.SLEEP_TIME - (Environment.TickCount - start));
					start = Environment.TickCount;

					Thread.Sleep(sleeptime);
				}
			}
		}

		private int pop()
		{
			lock (Stack)
			{
				return Stack.Count == 0 ? 0 : Stack.Pop();
			}
		}

		private int peek()
		{
			lock (Stack)
			{
				return Stack.Count == 0 ? 0 : Stack.Peek();
			}
		}

		private bool popBool()
		{
			lock (Stack)
			{
				return Stack.Count == 0 ? false : (Stack.Pop() != 0);
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
						// NOP
						break;
				}
			}
		}

		public void move()
		{
			PC += delta;

			PC += dimension;

			PC %= dimension;
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
	}
}
