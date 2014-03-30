
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
		public NumberRep NumberLiteralRepresentation = NumberRep.Best;

		// Removes 2x Stringmodetoogle after each other from Expression (eg "" )
		public bool StripDoubleStringmodeToogle = true;

		// Every NOP-Command is displayed as an Exit-Command
		public bool SetNOPCellsToCustom = true;
		public char CustomNOPSymbol = (char)164;//'@'; //'\u00F8';

		// When combining two CodePieces try to combine the two connecting columns/rows to a single one
		public bool CompressHorizontalCombining = true;
		public bool CompressVerticalCombining = true;

		// The aimed width of variable declarations - this is only estimated, long arrays can extend the width
		public int DefaultVarDeclarationWidth = 16;

		// The Value of reserved for variable fields before initialization 
		public BefungeCommand DefaultVarDeclarationSymbol = BCHelper.chr('V');

		// The Value of reserved for temp_field before initialization 
		public BefungeCommand DefaultTempSymbol = BCHelper.chr('T');

		// The Value of reserved for temp_field_returnval before initialization 
		public BefungeCommand DefaultResultTempSymbol = BCHelper.chr('R');

		// When hard casting to bool force the value to be '0' or '1'
		public bool ExtendedBooleanCast = false;

		// Default Values for Init operations
		public byte DefaultNumeralValue = 0;
		public char DefaultCharacterValue = ' ';
		public bool DefaultBooleanValue = false;

		// Values for the Display
		public BefungeCommand DefaultDisplayValue = BCHelper.chr(' ');
		public BefungeCommand DisplayBorder = BCHelper.chr('#');
		public int DisplayBorderThickness = 2;

		// If set to true you can't Out-Of-Bounce the Display - Set&Get is put into a modulo Width before
		public static bool DisplayModuloAccess = true; //TODO Standard = false
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
 
*/