
using BefunGen.AST.Exceptions;
using System;
namespace BefunGen.AST.CodeGen
{
	public static class BCHelper
	{
		#region Normal

		public static BefungeCommand Unused
		{
			get { return new BefungeCommand(BefungeCommandType.NOP); }
		}

		public static BefungeCommand Walkway
		{
			get { return new BefungeCommand(BefungeCommandType.Walkway); }
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

		public static BefungeCommand Unused_tagged(object tag)
		{
			throw new InternalCodeGenException(); // There is nothing like an tagged unused ...
		}

		public static BefungeCommand Walkway_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Walkway, tag);
		}

		public static BefungeCommand Add_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Add, tag);
		}

		public static BefungeCommand Sub_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Sub, tag);
		}

		public static BefungeCommand Mult_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Mult, tag);
		}

		public static BefungeCommand Div_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Div, tag);
		}

		public static BefungeCommand Modulo_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Modulo, tag);
		}

		public static BefungeCommand Not_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Not, tag);
		}

		public static BefungeCommand GreaterThan_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.GreaterThan, tag);
		}

		public static BefungeCommand PC_Right_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Right, tag);
		}

		public static BefungeCommand PC_Left_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Left, tag);
		}

		public static BefungeCommand PC_Up_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Up, tag);
		}

		public static BefungeCommand PC_Down_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Down, tag);
		}

		public static BefungeCommand PC_Random_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Random, tag);
		}

		public static BefungeCommand If_Horizontal_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.If_Horizontal, tag);
		}

		public static BefungeCommand If_Vertical_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.If_Vertical, tag);
		}

		public static BefungeCommand Stringmode_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Stringmode, tag);
		}

		public static BefungeCommand Stack_Dup_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Stack_Dup, tag);
		}

		public static BefungeCommand Stack_Swap_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Stack_Swap, tag);
		}

		public static BefungeCommand Stack_Pop_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Stack_Pop, tag);
		}

		public static BefungeCommand Out_Int_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Out_Int, tag);
		}

		public static BefungeCommand Out_ASCII_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Out_ASCII, tag);
		}

		public static BefungeCommand PC_Jump_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.PC_Jump, tag);
		}

		public static BefungeCommand Reflect_Set_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Reflect_Set, tag);
		}

		public static BefungeCommand Reflect_Get_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Reflect_Get, tag);
		}

		public static BefungeCommand In_Int_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.In_Int, tag);
		}

		public static BefungeCommand In_ASCII_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.In_ASCII, tag);
		}

		public static BefungeCommand Stop_tagged(object tag)
		{
			return new BefungeCommand(BefungeCommandType.Stop, tag);
		}

		public static BefungeCommand Digit_0_tagged(object tag)
		{
			return dig(0, tag);
		}

		public static BefungeCommand Digit_1_tagged(object tag)
		{
			return dig(1, tag);
		}

		public static BefungeCommand Digit_2_tagged(object tag)
		{
			return dig(2, tag);
		}

		public static BefungeCommand Digit_3_tagged(object tag)
		{
			return dig(3, tag);
		}

		public static BefungeCommand Digit_4_tagged(object tag)
		{
			return dig(4, tag);
		}

		public static BefungeCommand Digit_5_tagged(object tag)
		{
			return dig(5, tag);
		}

		public static BefungeCommand Digit_6_tagged(object tag)
		{
			return dig(6, tag);
		}

		public static BefungeCommand Digit_7_tagged(object tag)
		{
			return dig(7, tag);
		}

		public static BefungeCommand Digit_8_tagged(object tag)
		{
			return dig(8, tag);
		}

		public static BefungeCommand Digit_9_tagged(object tag)
		{
			return dig(9, tag);
		}

		#endregion

		#region Other (Tagged)

		public static BefungeCommand chr(int v, object tag)
		{
			return new BefungeCommand(BefungeCommandType.Other, v, tag);
		}

		public static BefungeCommand dig(byte v, object tag)
		{
			if (v < 10)
				return new BefungeCommand(BefungeCommandType.Other, '0' + v, tag);
			else
				throw new ArgumentException();
		}

		#endregion

		#region Helper

		public static BefungeCommand FindCommand(char c)
		{
			switch (c)
			{
				case ' ': return BCHelper.Walkway;
				case '+': return BCHelper.Add;
				case '-': return BCHelper.Sub;
				case '*': return BCHelper.Mult;
				case '/': return BCHelper.Div;
				case '%': return BCHelper.Modulo;
				case '!': return BCHelper.Not;
				case '`': return BCHelper.GreaterThan;
				case '>': return BCHelper.PC_Right;
				case '<': return BCHelper.PC_Left;
				case '^': return BCHelper.PC_Up;
				case 'v': return BCHelper.PC_Down;
				case '?': return BCHelper.PC_Random;
				case '_': return BCHelper.If_Horizontal;
				case '|': return BCHelper.If_Vertical;
				case '"': return BCHelper.Stringmode;
				case ':': return BCHelper.Stack_Dup;
				case '\\': return BCHelper.Stack_Swap;
				case '$': return BCHelper.Stack_Pop;
				case '.': return BCHelper.Out_Int;
				case ',': return BCHelper.Out_ASCII;
				case '#': return BCHelper.PC_Jump;
				case 'p': return BCHelper.Reflect_Set;
				case 'g': return BCHelper.Reflect_Get;
				case '&': return BCHelper.In_Int;
				case '~': return BCHelper.In_ASCII;
				case '@': return BCHelper.Stop;
				case '0': return BCHelper.Digit_0;
				case '1': return BCHelper.Digit_1;
				case '2': return BCHelper.Digit_2;
				case '3': return BCHelper.Digit_3;
				case '4': return BCHelper.Digit_4;
				case '5': return BCHelper.Digit_5;
				case '6': return BCHelper.Digit_6;
				case '7': return BCHelper.Digit_7;
				case '8': return BCHelper.Digit_8;
				case '9': return BCHelper.Digit_9;
				default:  return BCHelper.chr(c);
			}
		}

		#endregion
	}
}
