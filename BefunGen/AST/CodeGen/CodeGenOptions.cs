
namespace BefunGen.AST.CodeGen
{
	public enum NumberRep
	{
		StringmodeChar,	// CANT REPRESENT EVERYTHING !!
		Base9,
		Factorization,
		Stringify,		// CANT REPRESENT EVERYTHING !!
		Digit,			// CANT REPRESENT EVERYTHING !!
		Boolean,		// CANT REPRESENT EVERYTHING !!
		Best
	}

	public class CodeGenOptions
	{
		// Defines how Number Literals get represented
		public NumberRep NumberLiteralRepresentation;

		// Removes 2x Stringmodetoogle after each other from Expression (eg "" )
		public bool StripDoubleStringmodeToogle;

		// Every NOP-Command is displayed as an Exit-Command
		public bool SetNOPCellsToCustom;
		public char CustomNOPSymbol;

		// When combining two CodePieces try to combine the two connecting columns/rows to a single one
		public bool CompressHorizontalCombining;
		public bool CompressVerticalCombining;

		// The aimed width of variable declarations - this is only estimated, long arrays can extend the width
		public int DefaultVarDeclarationWidth;

		// The Value of reserved for variable fields before initialization 
		public BefungeCommand DefaultVarDeclarationSymbol;

		// The Value of reserved for temp_field before initialization 
		public BefungeCommand DefaultTempSymbol;

		// The Value of reserved for temp_field_returnval before initialization 
		public BefungeCommand DefaultResultTempSymbol;

		// When hard casting to bool force the value to be '0' or '1'
		public bool ExtendedBooleanCast;

		// Default Values for Init operations
		public byte DefaultNumeralValue;
		public char DefaultCharacterValue;
		public bool DefaultBooleanValue;

		// Values for the Display
		public BefungeCommand DefaultDisplayValue;
		public BefungeCommand DisplayBorder;
		public int DisplayBorderThickness;

		// If set to true you can't Out-Of-Bounce the Display - Set&Get is put into a modulo Width before
		public bool DisplayModuloAccess;

		// Calculate/Optimize Expressions at CompileTime
		public bool CompileTimeEvaluateExpressions; //TODO Add to BefunWrite Config
		/* EvaluateExpression - Optimizations:
		 * 
		 * RAND[0]  --> 0
		 * 1 * EXPR --> EXPR
		 * 0 * EXPR --> 0
		 * 0 [+,-] EXPR --> EXPR
		 * LITERAL [+,*,-,/] LITERAL --> LITERAL
		 * LITERAL [==,!=,>,<,>=,<=] LITERAL --> BOOL
		*/

		public static CodeGenOptions getCGO_Debug()
		{
			CodeGenOptions c = new CodeGenOptions();

			c.NumberLiteralRepresentation = NumberRep.Base9;
			c.StripDoubleStringmodeToogle = true;

			c.SetNOPCellsToCustom = true;
			c.CustomNOPSymbol = (char)164;

			c.CompressHorizontalCombining = true;
			c.CompressVerticalCombining = true;

			c.DefaultVarDeclarationWidth = 16;

			c.DefaultVarDeclarationSymbol = BCHelper.chr('V');
			c.DefaultTempSymbol = BCHelper.chr('R');
			c.DefaultResultTempSymbol = BCHelper.chr('T');

			c.ExtendedBooleanCast = false;

			c.DefaultNumeralValue = 0;
			c.DefaultCharacterValue = ' ';
			c.DefaultBooleanValue = false;

			c.DefaultDisplayValue = BCHelper.chr(' ');
			c.DisplayBorder = BCHelper.chr('#');
			c.DisplayBorderThickness = 1;

			c.DisplayModuloAccess = false;

			c.CompileTimeEvaluateExpressions = true;

			return c;
		}

		public static CodeGenOptions getCGO_Release()
		{
			CodeGenOptions c = new CodeGenOptions();

			c.NumberLiteralRepresentation = NumberRep.Best;
			c.StripDoubleStringmodeToogle = true;

			c.SetNOPCellsToCustom = false;
			c.CustomNOPSymbol = '@';

			c.CompressHorizontalCombining = true;
			c.CompressVerticalCombining = true;

			c.DefaultVarDeclarationWidth = 16;

			c.DefaultVarDeclarationSymbol = BCHelper.chr(' ');
			c.DefaultTempSymbol = BCHelper.chr(' ');
			c.DefaultResultTempSymbol = BCHelper.chr(' ');

			c.ExtendedBooleanCast = false;

			c.DefaultNumeralValue = 0;
			c.DefaultCharacterValue = ' ';
			c.DefaultBooleanValue = false;

			c.DefaultDisplayValue = BCHelper.chr(' ');
			c.DisplayBorder = BCHelper.chr('#');
			c.DisplayBorderThickness = 1;

			c.DisplayModuloAccess = false;

			c.CompileTimeEvaluateExpressions = true;

			return c;
		}

		public static int NumberRepToUINumber(NumberRep r)
		{
			switch (r)
			{
				case NumberRep.StringmodeChar:
					return -4;
				case NumberRep.Stringify:
					return -3;
				case NumberRep.Digit:
					return -2;
				case NumberRep.Boolean:
					return -1;
				case NumberRep.Base9:
					return 0;
				case NumberRep.Factorization:
					return 1;
				case NumberRep.Best:
					return 2;
				default:
					return int.MinValue;
			}
		}

		public static NumberRep UINumberToNumberRep(int r, NumberRep def)
		{
			switch (r)
			{
				case 0:
					return NumberRep.Base9;
				case 1:
					return NumberRep.Factorization;
				case 2:
					return NumberRep.Best;
				default:
					return def;
			}
		}
	}

	//Expressions are Left,0 in ... Right,0 out

	//Statements are Left,0 in ... Right,0 out
	//StatementLists order statements under each other (Y-0-Axis nearly top)

	//Methods enter at Left,0 in ... They can exit at multiple places
}

/*
  ::Jumping and the stack::
#############################

RIGHT LANE :> Jump to Codepoint - jump back - Stack flooded
LEFT LANE  :> Jump to Methodaddr - jump in - Stack unflooded


EXIT::JUMP_IN ( doMethod(a, b) )
########

[STACK]  OWN_VARIABLES
[STACK]  OWN_CODEPOINTADDR
[STACK]  TARGET_PARAMETER
[STACK]  TARGET_ADRESS ("Left Lane Adress" -> MethodAdress)
[STACK]  1 (FOR JUMP_IN -> "Left Lane")



EXIT::JUMP_BACK ( Return x )
#########

IF (RETURN VALUE) 
	[STACK]  CALC RETURN VALUE TO STACK (ALWAYS -> VOID is also value (0) );
	[STACK]  Stack_Swap (Switch BackJumpAddr and ReturnVal)
	[STACK]  0 (FOR JUMP_BACK -> "Right Lane")
IF (RETURN ARRAY)
	[STACK]  CALC RETURN VALUE TO STACK (ALWAYS -> VOID is also value (0) );
	PUT RETURNVAL to TMP_RETURN_FIELD
	PUT BACKJUMPADDR TO TMP_JMP_ADDR
	READ RETURNVAL BACK AGAIN
	READ BACKJUMPADDR BACK AGAIN
	[STACK]  0 (FOR JUMP_BACK -> "Right Lane")



EXIT::JUMP_LABEL ( GOTO lbl1 )
##########

[STACK]  TARGET_ADRESS ("Right Lane Adress" -> CodePointAdress)
[STACK]  0 (FOR JUMP_BACK -> "Right Lane") (little "hacky")



ROUNDABOUT_PATH
###############

SWITCH BETWEEN LEFT/RIGHT LANE

  /-- RIGHT LANE: FLOOD STACK BINARY FROM TARGETADRESS
-|
  \-- LEFT LANE: Do Nothing
 
GO THROUGH LANES (Right: Flooded, Left: sub..sub..sub)



ENTRY::JUMP_IN (From a jump in -> Left Lane)
##########

INIT VARS (STANDARD VALUES)
INIT PARAMS [FROM STACK]
{DO METHOD}



ENTRY::JUMP_BACK (From a jump back -> Right Lane)
##########

SAVE RETURN VALUE FROM STACK SOMEWHERE ELSE (perhaps calculate max array size -> reserve global return value tmp space)
RE-GET VARS FROM STACK
PUT RETURN VALUE BACK TO STACK
(If Statement_Call pop return value)



ENTRY::JUMP_LABEL ( lbl1: )
#################

Do Nothing ... just go on ... stack is fine

*/

/*

########### RPOGRAM IDEAS #######

Find way through Labyrinth thats written on display (recursive / Bruteforce)
Maze Generation
Solve Sudoku Thats inputted on Display (recursive Bruteforce)
Generate Sudoku on Display
Quicksort input
Befunge Interpreter
Generate  Math Question and check answers
Befunge Interpreter :D	  
PrimzahlCalcer
*/