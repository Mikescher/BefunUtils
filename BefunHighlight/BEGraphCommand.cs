using System;

namespace BefunHighlight
{
	public enum BeGraphCommandType
	{
		NOP,
		Add,
		Sub,
		Mult,
		Div,
		Modulo,
		Not,
		GreaterThan,
		PC_Right,
		PC_Left,
		PC_Up,
		PC_Down,
		PC_Random,
		If_Horizontal,
		If_Vertical,
		Stringmode,
		Stack_Dup,
		Stack_Swap,
		Stack_Pop,
		Out_Int,
		Out_ASCII,
		PC_Jump,
		Reflect_Set,
		Reflect_Get,
		In_Int,
		In_ASCII,
		Stop,
		Other
	}

	// Immutable Object
	public class BeGraphCommand
	{
		public readonly BeGraphCommandType Type;

		public BeGraphCommand(BeGraphCommandType _t)
		{
			Type = _t;
		}

		public char getCommandCode()
		{
			switch (Type)
			{
				case BeGraphCommandType.NOP:
					return ' ';
				case BeGraphCommandType.Add:
					return '+';
				case BeGraphCommandType.Sub:
					return '-';
				case BeGraphCommandType.Mult:
					return '*';
				case BeGraphCommandType.Div:
					return '/';
				case BeGraphCommandType.Modulo:
					return '%';
				case BeGraphCommandType.Not:
					return '!';
				case BeGraphCommandType.GreaterThan:
					return '`';
				case BeGraphCommandType.PC_Right:
					return '>';
				case BeGraphCommandType.PC_Left:
					return '<';
				case BeGraphCommandType.PC_Up:
					return '^';
				case BeGraphCommandType.PC_Down:
					return 'v';
				case BeGraphCommandType.PC_Random:
					return '?';
				case BeGraphCommandType.If_Horizontal:
					return '_';
				case BeGraphCommandType.If_Vertical:
					return '|';
				case BeGraphCommandType.Stringmode:
					return '"';
				case BeGraphCommandType.Stack_Dup:
					return ':';
				case BeGraphCommandType.Stack_Swap:
					return '\\';
				case BeGraphCommandType.Stack_Pop:
					return '$';
				case BeGraphCommandType.Out_Int:
					return '.';
				case BeGraphCommandType.Out_ASCII:
					return ',';
				case BeGraphCommandType.PC_Jump:
					return '#';
				case BeGraphCommandType.Reflect_Set:
					return 'p';
				case BeGraphCommandType.Reflect_Get:
					return 'g';
				case BeGraphCommandType.In_Int:
					return '&';
				case BeGraphCommandType.In_ASCII:
					return '~';
				case BeGraphCommandType.Stop:
					return '@';
				case BeGraphCommandType.Other:
					return 'X';
				default:
					throw new Exception();
			}
		}

		public static BeGraphCommand getCommand(long code)
		{
			switch (code)
			{
				case ' ':
					return new BeGraphCommand(BeGraphCommandType.NOP);
				case '+':
					return new BeGraphCommand(BeGraphCommandType.Add);
				case '-':
					return new BeGraphCommand(BeGraphCommandType.Sub);
				case '*':
					return new BeGraphCommand(BeGraphCommandType.Mult);
				case '/':
					return new BeGraphCommand(BeGraphCommandType.Div);
				case '%':
					return new BeGraphCommand(BeGraphCommandType.Modulo);
				case '!':
					return new BeGraphCommand(BeGraphCommandType.Not);
				case '`':
					return new BeGraphCommand(BeGraphCommandType.GreaterThan);
				case '>':
					return new BeGraphCommand(BeGraphCommandType.PC_Right);
				case '<':
					return new BeGraphCommand(BeGraphCommandType.PC_Left);
				case '^':
					return new BeGraphCommand(BeGraphCommandType.PC_Up);
				case 'v':
					return new BeGraphCommand(BeGraphCommandType.PC_Down);
				case '?':
					return new BeGraphCommand(BeGraphCommandType.PC_Random);
				case '_':
					return new BeGraphCommand(BeGraphCommandType.If_Horizontal);
				case '|':
					return new BeGraphCommand(BeGraphCommandType.If_Vertical);
				case '"':
					return new BeGraphCommand(BeGraphCommandType.Stringmode);
				case ':':
					return new BeGraphCommand(BeGraphCommandType.Stack_Dup);
				case '\\':
					return new BeGraphCommand(BeGraphCommandType.Stack_Swap);
				case '$':
					return new BeGraphCommand(BeGraphCommandType.Stack_Pop);
				case '.':
					return new BeGraphCommand(BeGraphCommandType.Out_Int);
				case ',':
					return new BeGraphCommand(BeGraphCommandType.Out_ASCII);
				case '#':
					return new BeGraphCommand(BeGraphCommandType.PC_Jump);
				case 'p':
					return new BeGraphCommand(BeGraphCommandType.Reflect_Set);
				case 'g':
					return new BeGraphCommand(BeGraphCommandType.Reflect_Get);
				case '&':
					return new BeGraphCommand(BeGraphCommandType.In_Int);
				case '~':
					return new BeGraphCommand(BeGraphCommandType.In_ASCII);
				case '@':
					return new BeGraphCommand(BeGraphCommandType.Stop);
				default:
					return new BeGraphCommand(BeGraphCommandType.Other);
			}
		}

		public BeGraphOpType getGraphOpType()
		{
			switch (Type)
			{
				case BeGraphCommandType.NOP:
				case BeGraphCommandType.Add:
				case BeGraphCommandType.Sub:
				case BeGraphCommandType.Mult:
				case BeGraphCommandType.Div:
				case BeGraphCommandType.Modulo:
				case BeGraphCommandType.Not:
				case BeGraphCommandType.GreaterThan:
				case BeGraphCommandType.Stack_Dup:
				case BeGraphCommandType.Stack_Swap:
				case BeGraphCommandType.Stack_Pop:
				case BeGraphCommandType.Out_Int:
				case BeGraphCommandType.Out_ASCII:
				case BeGraphCommandType.Reflect_Set:
				case BeGraphCommandType.Reflect_Get:
				case BeGraphCommandType.In_Int:
				case BeGraphCommandType.In_ASCII:
				case BeGraphCommandType.Other:
					return BeGraphOpType.SimpleCommand;
				case BeGraphCommandType.PC_Right:
				case BeGraphCommandType.PC_Left:
				case BeGraphCommandType.PC_Up:
				case BeGraphCommandType.PC_Down:
					return BeGraphOpType.DirectionChange;
				case BeGraphCommandType.PC_Random:
				case BeGraphCommandType.If_Horizontal:
				case BeGraphCommandType.If_Vertical:
					return BeGraphOpType.Descision;
				case BeGraphCommandType.Stringmode:
					return BeGraphOpType.Stringmode;
				case BeGraphCommandType.PC_Jump:
					return BeGraphOpType.Jump;
				case BeGraphCommandType.Stop:
					return BeGraphOpType.Stop;
				default:
					throw new Exception();
			}
		}
	}
}
