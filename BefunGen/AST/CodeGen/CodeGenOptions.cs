﻿
namespace BefunGen.AST.CodeGen
{
	public enum NumberRep
	{
		CharConstant, //TODO What when neg
		Base9,
		Factorization//TODO Option to intelligent use Best Option
	}

	public class CodeGenOptions
	{
		// Defines how Number Literals get represented
		public static NumberRep NumberLiteralRepresentation = NumberRep.Factorization;

		// If 0 <= NumberLiteral <= 9 then generate as digit (only codegen - not in AST)
		public static bool AutoDigitizeNumberLiterals = true;

		// Removes 2x STringmodetoogle after each other rfrom Expression (eg "" )
		public static bool OptimizeDoubleStringmodeToogle = true;

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
