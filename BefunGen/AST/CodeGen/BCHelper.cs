﻿
using System;
namespace BefunGen.AST.CodeGen
{
	public static class BCHelper
	{
		#region Normal

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

		#endregion

		#region Other

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

		#endregion

		#region Normal (Tagged)

		public static BefungeCommand Empty_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.NOP, tag);
		}

		public static BefungeCommand Add_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Add, tag);
		}

		public static BefungeCommand Sub_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Sub, tag);
		}

		public static BefungeCommand Mult_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Mult, tag);
		}

		public static BefungeCommand Div_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Div, tag);
		}

		public static BefungeCommand Modulo_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Modulo, tag);
		}

		public static BefungeCommand Not_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Not, tag);
		}

		public static BefungeCommand GreaterThan_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.GreaterThan, tag);
		}

		public static BefungeCommand PC_Right_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Right, tag);
		}

		public static BefungeCommand PC_Left_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Left, tag);
		}

		public static BefungeCommand PC_Up_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Up, tag);
		}

		public static BefungeCommand PC_Down_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Down, tag);
		}

		public static BefungeCommand PC_Random_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Random, tag);
		}

		public static BefungeCommand If_Horizontal_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.If_Horizontal, tag);
		}

		public static BefungeCommand If_Vertical_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.If_Vertical, tag);
		}

		public static BefungeCommand Stringmode_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Stringmode, tag);
		}

		public static BefungeCommand Stack_Dup_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Stack_Dup, tag);
		}

		public static BefungeCommand Stack_Swap_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Stack_Swap, tag);
		}

		public static BefungeCommand Stack_Pop_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Stack_Pop, tag);
		}

		public static BefungeCommand Out_Int_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Out_Int, tag);
		}

		public static BefungeCommand Out_ASCII_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Out_ASCII, tag);
		}

		public static BefungeCommand PC_Jump_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Jump, tag);
		}

		public static BefungeCommand Reflect_Set_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Reflect_Set, tag);
		}

		public static BefungeCommand Reflect_Get_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Reflect_Get, tag);
		}

		public static BefungeCommand In_Int_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.In_Int, tag);
		}

		public static BefungeCommand In_ASCII_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.In_ASCII, tag);
		}

		public static BefungeCommand Stop_tagged(string tag)
		{
			return new BefungeCommand(BefungeCommandType.Stop, tag);
		}

		public static BefungeCommand Digit_0_tagged(string tag)
		{
			return dig(0, tag);
		}

		public static BefungeCommand Digit_1_tagged(string tag)
		{
			return dig(1, tag);
		}

		public static BefungeCommand Digit_2_tagged(string tag)
		{
			return dig(2, tag);
		}

		public static BefungeCommand Digit_3_tagged(string tag)
		{
			return dig(3, tag);
		}

		public static BefungeCommand Digit_4_tagged(string tag)
		{
			return dig(4, tag);
		}

		public static BefungeCommand Digit_5_tagged(string tag)
		{
			return dig(5, tag);
		}

		public static BefungeCommand Digit_6_tagged(string tag)
		{
			return dig(6, tag);
		}

		public static BefungeCommand Digit_7_tagged(string tag)
		{
			return dig(7, tag);
		}

		public static BefungeCommand Digit_8_tagged(string tag)
		{
			return dig(8, tag);
		}

		public static BefungeCommand Digit_9_tagged(string tag)
		{
			return dig(9, tag);
		}

		#endregion

		#region Other (Tagged)

		public static BefungeCommand chr(int v, string tag)
		{
			return new BefungeCommand(BefungeCommandType.Other, v, tag);
		}

		public static BefungeCommand dig(byte v, string tag)
		{
			if (v < 10)
				return new BefungeCommand(BefungeCommandType.Other, '0' + v, tag);
			else
				throw new ArgumentException();
		}

		#endregion
	}
}
