
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

	public class CodeGenOptions
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

		//TODO (Optional) When in STatementList and have Left->Right Statement test if next STatement is mirrorable (no If_Horiz) - the mirror it and append it directly beneath --> no empty line
		// Expression Methodcall before codegen convert in AST to multiple statements (call methods->assign to var->use var in expr) (??? perhaps ???)

		//TODO Reusable CodePoints can be "tagged" in the CodePiece class with special -unique- Tags (with Params) to find them later (like method enter, stack back etc)

		//TODO Differentitate between real empty and walked empty cells (for debugging fill realempty with @)
		//     --> Later better optimizing etc etc
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