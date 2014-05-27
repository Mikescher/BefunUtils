
using System;
namespace BefunHighlight
{
	struct BeGraphCalculateOperation
	{
		public int X;
		public int Y;
		public BeGraphDirection D;

		public bool isDirectionStringMode
		{
			get
			{
				return
					D == BeGraphDirection.BottomTop_sm ||
					D == BeGraphDirection.LeftRight_sm ||
					D == BeGraphDirection.RightLeft_sm ||
					D == BeGraphDirection.TopBottom_sm;
			}
		}

		public BeGraphDirection getDC(BeGraphCommand cmd)
		{
			if (cmd.Type == BeGraphCommandType.PC_Up)
			{
				return BeGraphDirection.BottomTop;
			}
			else if (cmd.Type == BeGraphCommandType.PC_Down)
			{
				return BeGraphDirection.TopBottom;
			}
			else if (cmd.Type == BeGraphCommandType.PC_Left)
			{
				return BeGraphDirection.LeftRight;
			}
			else if (cmd.Type == BeGraphCommandType.PC_Right)
			{
				return BeGraphDirection.RightLeft;
			}
			else
			{
				throw new Exception();
			}
		}

		public BeGraphCalculateOperation next(int w, int h, BeGraphDirection dir, int jmp = 1)
		{
			switch (dir)
			{
				case BeGraphDirection.LeftRight:
				case BeGraphDirection.LeftRight_sm:
					return new BeGraphCalculateOperation()
					{
						X = (this.X + jmp) % w,
						Y = this.Y,
						D = dir,
					};
				case BeGraphDirection.TopBottom:
				case BeGraphDirection.TopBottom_sm:
					return new BeGraphCalculateOperation()
					{
						X = this.X,
						Y = (this.Y + jmp) % h,
						D = dir,
					};
				case BeGraphDirection.RightLeft:
				case BeGraphDirection.RightLeft_sm:
					return new BeGraphCalculateOperation()
					{
						X = (this.X + w - jmp) % w,
						Y = this.Y,
						D = dir,
					};
				case BeGraphDirection.BottomTop:
				case BeGraphDirection.BottomTop_sm:
					return new BeGraphCalculateOperation()
					{
						X = this.X,
						Y = (this.Y + h - jmp) % h,
						D = dir,
					};
				default:
					throw new Exception();
			}
		}

		public BeGraphCalculateOperation next_sm(int w, int h)
		{
			switch (D)
			{
				case BeGraphDirection.LeftRight:
					return new BeGraphCalculateOperation()
					{
						X = (this.X + 1) % w,
						Y = this.Y,
						D = BeGraphDirection.LeftRight_sm,
					};
				case BeGraphDirection.LeftRight_sm:
					return new BeGraphCalculateOperation()
					{
						X = (this.X + 1) % w,
						Y = this.Y,
						D = BeGraphDirection.LeftRight,
					};
				case BeGraphDirection.TopBottom:
					return new BeGraphCalculateOperation()
					{
						X = this.X,
						Y = (this.Y + 1) % h,
						D = BeGraphDirection.TopBottom_sm,
					};
				case BeGraphDirection.TopBottom_sm:
					return new BeGraphCalculateOperation()
					{
						X = this.X,
						Y = (this.Y + 1) % h,
						D = BeGraphDirection.TopBottom,
					};
				case BeGraphDirection.RightLeft:
					return new BeGraphCalculateOperation()
					{
						X = (this.X - 1) % w,
						Y = this.Y,
						D = BeGraphDirection.RightLeft_sm,
					};
				case BeGraphDirection.RightLeft_sm:
					return new BeGraphCalculateOperation()
					{
						X = (this.X - 1) % w,
						Y = this.Y,
						D = BeGraphDirection.RightLeft,
					};
				case BeGraphDirection.BottomTop:
					return new BeGraphCalculateOperation()
					{
						X = this.X,
						Y = (this.Y - 1) % h,
						D = BeGraphDirection.BottomTop_sm,
					};
				case BeGraphDirection.BottomTop_sm:
					return new BeGraphCalculateOperation()
					{
						X = this.X,
						Y = (this.Y - 1) % h,
						D = BeGraphDirection.BottomTop,
					};
				default:
					throw new Exception();
			}
		}
	}
}
