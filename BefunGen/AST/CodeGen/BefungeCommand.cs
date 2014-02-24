using BefunGen.AST.Exceptions;

namespace BefunGen.AST.CodeGen
{
	public enum BefungeCommandType
	{
		NOP,
		Walkway, // Like NOP - But PC can appear here
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

	public class BefungeCommand
	{
		public readonly BefungeCommandType Type;

		public readonly int Param;

		public readonly object Tag;

		public BefungeCommand(BefungeCommandType _t)
			: this(_t, 0)
		{
			//--
		}

		public BefungeCommand(BefungeCommandType _t, object _p)
			: this(_t, 0, _p)
		{
			//--
		}

		public BefungeCommand(BefungeCommandType _t, int _p)
			: this(_t, _p, null)
		{
			//--
		}

		public BefungeCommand(BefungeCommandType _t, int _p, object _g)
		{
			Type  = _t;
			Param = _p;
			Tag   = _g;
		}

		public char getCommandCode()
		{
			switch (Type)
			{
				case BefungeCommandType.NOP:
					return CodeGenOptions.SetNOPCellsToExit ? '@' : ' ';

				case BefungeCommandType.Walkway:
					return ' ';

				case BefungeCommandType.Add:
					return '+';

				case BefungeCommandType.Sub:
					return '-';

				case BefungeCommandType.Mult:
					return '*';

				case BefungeCommandType.Div:
					return '/';

				case BefungeCommandType.Modulo:
					return '%';

				case BefungeCommandType.Not:
					return '!';

				case BefungeCommandType.GreaterThan:
					return '`';

				case BefungeCommandType.PC_Right:
					return '>';

				case BefungeCommandType.PC_Left:
					return '<';

				case BefungeCommandType.PC_Up:
					return '^';

				case BefungeCommandType.PC_Down:
					return 'v';

				case BefungeCommandType.PC_Random:
					return '?';

				case BefungeCommandType.If_Horizontal:
					return '_';

				case BefungeCommandType.If_Vertical:
					return '|';

				case BefungeCommandType.Stringmode:
					return '"';

				case BefungeCommandType.Stack_Dup:
					return ':';

				case BefungeCommandType.Stack_Swap:
					return '\\';

				case BefungeCommandType.Stack_Pop:
					return '$';

				case BefungeCommandType.Out_Int:
					return '.';

				case BefungeCommandType.Out_ASCII:
					return ',';

				case BefungeCommandType.PC_Jump:
					return '#';

				case BefungeCommandType.Reflect_Set:
					return 'p';

				case BefungeCommandType.Reflect_Get:
					return 'g';

				case BefungeCommandType.In_Int:
					return '&';

				case BefungeCommandType.In_ASCII:
					return '~';

				case BefungeCommandType.Stop:
					return '@';

				case BefungeCommandType.Other:
					return (char)Param;

				default:
					throw new InvalidBefungeCommandTypeException(new SourceCodePosition());
			}
		}
	}
}