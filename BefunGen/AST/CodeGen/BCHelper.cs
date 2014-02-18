
using System;
namespace BefunGen.AST.CodeGen
{
	public static class BCHelper
	{
		public static BefungeCommand Empty
		{
			get { return new BefungeCommand(BefungeCommandType.NOP); }
		}

		public static BefungeCommand Add
		{
			get { return new BefungeCommand(BefungeCommandType.Add); }
		}

		public static BefungeCommand Sub
		{
			get { return new BefungeCommand(BefungeCommandType.Sub); }
		}

		public static BefungeCommand Mult
		{
			get { return new BefungeCommand(BefungeCommandType.Mult); }
		}

		public static BefungeCommand Div
		{
			get { return new BefungeCommand(BefungeCommandType.Div); }
		}

		public static BefungeCommand Modulo
		{
			get { return new BefungeCommand(BefungeCommandType.Modulo); }
		}

		public static BefungeCommand Not
		{
			get { return new BefungeCommand(BefungeCommandType.Not); }
		}

		public static BefungeCommand GreaterThan
		{
			get { return new BefungeCommand(BefungeCommandType.GreaterThan); }
		}

		public static BefungeCommand PC_Right
		{
			get { return new BefungeCommand(BefungeCommandType.PC_Right); }
		}

		public static BefungeCommand PC_Left
		{
			get { return new BefungeCommand(BefungeCommandType.PC_Left); }
		}

		public static BefungeCommand PC_Up
		{
			get { return new BefungeCommand(BefungeCommandType.PC_Up); }
		}

		public static BefungeCommand PC_Down
		{
			get { return new BefungeCommand(BefungeCommandType.PC_Down); }
		}

		public static BefungeCommand PC_Random
		{
			get { return new BefungeCommand(BefungeCommandType.PC_Random); }
		}

		public static BefungeCommand If_Horizontal
		{
			get { return new BefungeCommand(BefungeCommandType.If_Horizontal); }
		}

		public static BefungeCommand If_Vertical
		{
			get { return new BefungeCommand(BefungeCommandType.If_Vertical); }
		}

		public static BefungeCommand Stringmode
		{
			get { return new BefungeCommand(BefungeCommandType.Stringmode); }
		}

		public static BefungeCommand Stack_Dup
		{
			get { return new BefungeCommand(BefungeCommandType.Stack_Dup); }
		}

		public static BefungeCommand Stack_Swap
		{
			get { return new BefungeCommand(BefungeCommandType.Stack_Swap); }
		}

		public static BefungeCommand Stack_Pop
		{
			get { return new BefungeCommand(BefungeCommandType.Stack_Pop); }
		}

		public static BefungeCommand Out_Int
		{
			get { return new BefungeCommand(BefungeCommandType.Out_Int); }
		}

		public static BefungeCommand Out_ASCII
		{
			get { return new BefungeCommand(BefungeCommandType.Out_ASCII); }
		}

		public static BefungeCommand PC_Jump
		{
			get { return new BefungeCommand(BefungeCommandType.PC_Jump); }
		}

		public static BefungeCommand Reflect_Set
		{
			get { return new BefungeCommand(BefungeCommandType.Reflect_Set); }
		}

		public static BefungeCommand Reflect_Get
		{
			get { return new BefungeCommand(BefungeCommandType.Reflect_Get); }
		}

		public static BefungeCommand In_Int
		{
			get { return new BefungeCommand(BefungeCommandType.In_Int); }
		}

		public static BefungeCommand In_ASCII
		{
			get { return new BefungeCommand(BefungeCommandType.In_ASCII); }
		}

		public static BefungeCommand Stop
		{
			get { return new BefungeCommand(BefungeCommandType.Stop); }
		}

		public static BefungeCommand Digit_0
		{
			get { return dig(0); }
		}

		public static BefungeCommand Digit_1
		{
			get { return dig(1); }
		}

		public static BefungeCommand Digit_2
		{
			get { return dig(2); }
		}

		public static BefungeCommand Digit_3
		{
			get { return dig(3); }
		}

		public static BefungeCommand Digit_4
		{
			get { return dig(4); }
		}

		public static BefungeCommand Digit_5
		{
			get { return dig(5); }
		}

		public static BefungeCommand Digit_6
		{
			get { return dig(6); }
		}

		public static BefungeCommand Digit_7
		{
			get { return dig(7); }
		}

		public static BefungeCommand Digit_8
		{
			get { return dig(8); }
		}

		public static BefungeCommand Digit_9
		{
			get { return dig(9); }
		}

		public static BefungeCommand chr(int v)
		{
			return new BefungeCommand(BefungeCommandType.Other, v);
		}

		public static BefungeCommand dig(byte v)
		{
			if (v < 10)
				return new BefungeCommand(BefungeCommandType.Other, '0' + v);
			else
				throw new ArgumentException();
		}
	}
}
