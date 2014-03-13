
namespace BefunGen.AST.CodeGen
{
	public enum NumberRep
	{
		CharConstant,
		Base9,
		Factorization,
		Digit, // Throws Error when not 0 <= x <= 9
		Best
	}

	public static class CodeGenOptions
	{
		// Defines how Number Literals get represented
		public static NumberRep NumberLiteralRepresentation = NumberRep.Best;

		// If 0 <= NumberLiteral <= 9 then generate as digit (only codegen - not in AST)
		public static bool AutoDigitizeNumberLiterals = true;

		// Removes 2x STringmodetoogle after each other rfrom Expression (eg "" )
		public static bool StripDoubleStringmodeToogle = true;

		// Every NOP-Command is displayed as an Exit-Command
		public static bool SetNOPCellsToCustom = true;
		public static char CustomNOPSymbol = (char)164;//'@'; //'\u00F8';

		// When combining two CodePieces try to combine the two connecting columns/rows to a signle one
		public static bool CompressHorizontalCombining = true;
		public static bool CompressVerticalCombining = true;

		// The aimed width of variable declarations - this is only estimated, long arrays can extend the width
		public static int DefaultVarDeclarationWidth = 16;

		// The Value of reserved for variable fields before initialization 
		public static BefungeCommand DefaultVarDeclarationSymbol = BCHelper.chr('V');

		// When hard casting to bool force the value to be '0' or '1'
		public static bool ExtendedBooleanCast = false;

		// Default Values for Init operations
		public static byte DefaultNumeralValue   = 0;
		public static char DefaultCharacterValue = ' ';
		public static bool DefaultBooleanValue   = false;
	}

	//Expressions are Left,0 in ... Right,0 out

	//Statements are Left,0 in ... Right,0 out
	//StatementLists order <statements under each other (Y-0-Axis nearly top)

	//Methods enter ate Left,0 in ... They can exit at multiple places
}


/* PLAN FOR METHOD FINDING

2 Lanes Left : For Jump In and Jump Back
1 Lane Right from Code for Jump out (goes up -> and then left down)

On Finding Stack:[Target, Params, BackJump]
On Found   Stack:[Params, BackJump]
On Run     Stack:[???, BackJump]
On Leave   Stack:[Result, BackJump] --> then swap [BackJump, Result]
(Every Method becomes return 0; appended)
(Even normal return; return Value 0)

--> Extern Pipe and Method Intern pipe - first navigate to method on extern, then to re-enter pos on intern
--> Push ALL Vars on stack on eavign and re enter them on re-entering

For now no Inline Method Calls ... (only procedures - no functions) 

For Inline Method Calls: Mark Expression entry and exit Points with Tags - then after generating whole Expression move in and out of it from above - building walkways ... (would need to be done in Statement)

#################

[!] BackJump Locations MUST have Y-DIstance from >= 3
(Method In Jump also - but this should be always given (EVALUATE !!!, perhaps force MethodSize >= 3))

##### CODE ######

1
-
:
!
#
>
|

####################

v:-1<
>#v_>
  > v
  
  
#####################
  
v_v#   <<<<<<< // 0-> Right Lane || 1 -> Left Lane
|#|
|#|
|#|
L: BackJump (5x3 Jmpr)
R: InJump   (1x7 Jmpr)


##################### 

Alternative: flooding stack with all needed vals: (i think preferable)

$_v#!:-1<\1
  >0\   ^
  
==> Stack:[5] becomes Stack:[1,0,0,0,0]

==> kein 1-:! mehr 
nurnoch:

#
>@
|


################## EXAMPLE #############

v
#
>3v
?1v4
>2v
v <
> :."=",                   v

v          $_v#!:-1<\1     <
			 >0\   ^

v
v

#
>    "1",@
|

#
>    "2",@
|

#
>    "3",@
|

#
>    "4",@
|

#
>    "5",@
|


*/

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

SAVE BACKJUMPADDR (Current Stack::Top) to TMP_2 (TMP_2 Exclusive to this OP ( BackJumpAddr )) //TODO Every Tmp Addr exclusive ?

[STACK]  CALC RETURN VALUE TO STACK (ALWAYS -> VOID is also value (0) );
[STACK]  PUT BACKJUMPADDR BACK ON STACK ("Right Lane Adress" -> CodePointAdress)
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

// Encode dynamically Base-9 number

v      ########## >>#$>#<  @
>  9:* ## 5471 ## 0>\:9`|
	   ##########  ^+*9\<
			   
#### Digits in Base-9 ####
   
8Bit  = ~  3 B9
32Bit = ~ 10 B9
64Bit = ~ 20 B9


##########################
Generate Random Nmbr
##########################

v              ,,+55+49  <
v      >>v               .
v      21v      >>#$>#<  ^
> 59>#v?^>\1-:#v_>\:4`|
	  3>0^       ^+*4\<
	^          <
	  >  ^

########### RPOGRAM IDEAS #######

Find way through Labyrinth thats written on display (recursive / Bruteforce)
Maze Generation
Solve Sudoku Thats inputted on Display (recursive Bruteforce)
Generate Sudoku on Display
Quicksort input
Befunge Interpreter
Generate  Math Question and check answers
	  
*/