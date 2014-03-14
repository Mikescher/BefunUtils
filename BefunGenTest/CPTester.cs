using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGenTest
{
	public struct Vector
	{
		public int X;
		public int Y;

		public Vector(int xx, int yy) { X = xx; Y = yy; }

		public void set(int xx, int yy) { X = xx; Y = yy; }

		public void set(int d)
		{
			switch (d)
			{
				case 0:
					set(+1, 00);
					break;
				case 1:
					set(00, +1);
					break;
				case 2:
					set(-1, 00);
					break;
				case 3:
					set(00, -1);
					break;
			}
		}

		public bool isZero()
		{
			return X == 0 && Y == 0;
		}
	}

	public class CPTester
	{
		private static Random Rand = new Random();

		public int w;
		public int h;

		public int[,] raster;
		public Vector PC;
		public Vector Delta;
		public bool Stringmode;

		public Stack<int> Stack;

		public StringBuilder Output;

		public int StepCount;
		public bool RandLog;

		public CPTester(string s, bool reversed = false)
		{
			string[] lines = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			h = lines.Length;
			w = lines.Max(p => p.Length);

			raster = new int[w, h];

			for (int x = 0; x < w; x++)
				for (int y = 0; y < h; y++)
					raster[x, y] = ' ';

			PC = new Vector(0, 0);
			Delta = new Vector(1, 0);
			Stack = new Stack<int>();
			Output = new StringBuilder();
			StepCount = 0;
			Stringmode = false;
			RandLog = false;

			if (reversed)
			{
				Delta.set(-1, 0);
				PC.X = w - 1;
			}

			for (int y = 0; y < lines.Length; y++)
				for (int x = 0; x < lines[y].Length; x++)
					raster[x, y] = lines[y][x];
		}

		public bool hadRandomElements()
		{
			return RandLog;
		}

		public void run(int maxSteps)
		{
			while (StepCount < maxSteps && !Delta.isZero())
			{
				runSingle();
			}
		}

		private void runSingle()
		{
			executCmd(raster[PC.X, PC.Y]);

			move();

			StepCount++;
		}

		public void Set(int x, int y, int chr)
		{
			if (x < 0 || y < 0 || x >= w || y >= h)
				throw new BFRunException("Modification Out Of Raster");

			//if (chr < 0)
			//	throw new BFRunException("Modification to invalid char");

			raster[x, y] = chr;
		}

		public int Get(int x, int y)
		{
			if (x < 0 || y < 0 || x >= w || y >= h)
				throw new BFRunException("Reflection Out Of Raster");

			//if (chr < 0)
			//	throw new BFRunException("Modification to invalid char");

			return raster[x, y];
		}

		public void Push(int i)
		{
			Stack.Push(i);
		}

		public int Pop()
		{
			if (Stack.Count == 0)
			{
				throw new BFRunException("Popped an empty stack");
			}

			return Stack.Pop();
		}

		public int Peek()
		{
			if (Stack.Count == 0)
			{
				throw new BFRunException("Popped an empty stack");
			}

			return Stack.Peek();
		}

		public bool Pop_b()
		{
			return Pop() != 0;
		}

		public void Push(bool b)
		{
			Push(b ? 1 : 0);
		}

		private void executCmd(int cmd)
		{
			if (Stringmode)
			{
				if (cmd == '"')
				{
					Stringmode = false;
					return;
				}
				else
				{
					Push(cmd);
					return;
				}
			}

			int t1;
			int t2;
			int t3;

			switch (cmd)
			{
				case ' ':
					// NOP
					break;
				case '+':
					Push(Pop() + Pop());
					break;
				case '-':
					t1 = Pop();
					Push(Pop() - t1);
					break;
				case '*':
					t1 = Pop();
					Push(Pop() * t1);
					break;
				case '/':
					t1 = Pop();
					Push(Pop() / t1);
					break;
				case '%':
					t1 = Pop();
					Push(Pop() % t1);
					break;
				case '!':
					Push(!Pop_b());
					break;
				case '`':
					t1 = Pop();
					Push(Pop() > t1);
					break;
				case '>':
					Delta.set(1, 0);
					break;
				case '<':
					Delta.set(-1, 0);
					break;
				case '^':
					Delta.set(0, -1);
					break;
				case 'v':
					Delta.set(0, 1);
					break;
				case '?':
					RandLog = true;
					Delta.set(Rand.Next(4));
					break;
				case '_':
					Delta.set(Pop_b() ? 2 : 0);
					break;
				case '|':
					Delta.set(Pop_b() ? 3 : 1);
					break;
				case '"':
					Stringmode = true;
					break;
				case ':':
					Push(Peek());
					break;
				case '\\':
					t1 = Pop();
					t2 = Pop();
					Push(t1);
					Push(t2);
					break;
				case '$':
					Pop();
					break;
				case '.':
					Output.Append((int)Pop());
					break;
				case ',':
					Output.Append((char)Pop());
					break;
				case '#':
					move();
					break;
				case 'p':
					t1 = Pop();
					t2 = Pop();
					t3 = Pop();

					Set(t2, t1, t3);
					break;
				case 'g':
					t1 = Pop();
					t2 = Pop();

					Push(Get(t2, t1));
					break;
				case '&':
					RandLog = true;
					Push(Rand.Next(8192) - 4096);
					break;
				case '~':
					RandLog = true;
					Push(Rand.Next('~' - ' ') + ' ');
					break;
				case '@':
					Delta.set(0, 0);
					break;
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
					Push(cmd - '0');
					break;
				default:
					throw new BFRunException("Unknown Command:" + cmd + "(" + (char)cmd + ")");
			}
		}

		private void move()
		{
			PC.X += Delta.X;
			PC.Y += Delta.Y;

			if (PC.X < 0 || PC.Y < 0 || PC.X >= w || PC.Y >= h)
				throw new BFRunException("Moved Out Of Raster");
		}
	}
}
