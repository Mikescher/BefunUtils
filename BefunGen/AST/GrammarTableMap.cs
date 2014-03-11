using BefunGen.AST.CodeGen;
using BefunGen.AST.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST
{
	public static class GrammarTableMap
	{
		public static ASTObject CreateNewASTObject(GOLD.Reduction r, GOLD.Position goldpos)
		{
			ASTObject result = null;

			SourceCodePosition p = new SourceCodePosition(goldpos);

			switch ((ProductionIndex)r.Parent.TableIndex()) // Regex for empty cases:     ^\s+//[^\r]+\r\n\s+break;
			{
				case ProductionIndex.Program:
					// <Program> ::= <Header> <MainStatements> <MethodList> <Footer>
					result = new Program(p, ((Program_Header)r.get_Data(0)).Identifier, (Method)r.get_Data(1), ((List_Methods)r.get_Data(2)).List);
					break;

				case ProductionIndex.Header_Program_Identifier:
					// <Header> ::= program Identifier
					result = new Program_Header(p, getStrData(1, r));
					break;

				case ProductionIndex.Footer_End:
					// <Footer> ::= end
					result = new Program_Footer(p);
					break;

				case ProductionIndex.Methodlist:
					// <MethodList> ::= <MethodList> <Method>
					result = ((List_Methods)r.get_Data(0)).Append((Method)r.get_Data(1));
					break;

				case ProductionIndex.Methodlist2:
					// <MethodList> ::=
					result = new List_Methods(p);
					break;

				case ProductionIndex.Mainstatements:
					// <MainStatements> ::= <MethodBody>
					result = new Method(p, new Method_Header(p, new BType_Void(p), "main", new List<VarDeclaration>()), (Method_Body)r.get_Data(0));
					break;

				case ProductionIndex.Method:
					// <Method> ::= <MethodHeader> <MethodBody>
					result = new Method(p, (Method_Header)r.get_Data(0), (Method_Body)r.get_Data(1));
					break;

				case ProductionIndex.Methodbody:
					// <MethodBody> ::= <VarDeclBody> <Statement>
					result = new Method_Body(p, ((List_VarDeclarations)r.get_Data(0)).List, StmtToStmtList((Statement)r.get_Data(1)));
					break;

				case ProductionIndex.Methodheader_Identifier_Lparen_Rparen:
					// <MethodHeader> ::= <Type> Identifier '(' <ParamDecl> ')'
					result = new Method_Header(p, (BType)r.get_Data(0), getStrData(1, r), ((List_VarDeclarations)r.get_Data(3)).List);
					break;

				case ProductionIndex.Vardeclbody_Var:
					// <VarDeclBody> ::= var <VarList>
					result = (List_VarDeclarations)r.get_Data(1);
					break;

				case ProductionIndex.Vardeclbody:
					// <VarDeclBody> ::=
					result = new List_VarDeclarations(p);
					break;

				case ProductionIndex.Paramdecl:
					// <ParamDecl> ::= <ParamList>
					result = (List_VarDeclarations)r.get_Data(0);
					break;

				case ProductionIndex.Paramdecl2:
					// <ParamDecl> ::=
					result = new List_VarDeclarations(p);
					break;

				case ProductionIndex.Paramlist_Comma:
					// <ParamList> ::= <ParamList> ',' <Param>
					result = ((List_VarDeclarations)r.get_Data(0)).Append((VarDeclaration)r.get_Data(2));
					break;

				case ProductionIndex.Paramlist:
					// <ParamList> ::= <Param>
					result = new List_VarDeclarations(p, (VarDeclaration)r.get_Data(0));
					break;

				case ProductionIndex.Param_Identifier:
					// <Param> ::= <Type> Identifier
					result = createASTDeclarationFromReduction(r, false, p);
					break;

				case ProductionIndex.Varlist_Semi:
					// <VarList> ::= <VarList> <VarDecl> ';'
					result = ((List_VarDeclarations)r.get_Data(0)).Append((VarDeclaration)r.get_Data(1));
					break;

				case ProductionIndex.Varlist_Semi2:
					// <VarList> ::= <VarDecl> ';'
					result = new List_VarDeclarations(p, (VarDeclaration)r.get_Data(0));
					break;

				case ProductionIndex.Vardecl_Identifier:
					// <VarDecl> ::= <Type> Identifier
					result = createASTDeclarationFromReduction(r, false, p);
					break;

				case ProductionIndex.Vardecl_Identifier_Coloneq:
					// <VarDecl> ::= <Type> Identifier ':=' <Literal>
					result = createASTDeclarationFromReduction(r, true, p);
					break;

				case ProductionIndex.Statement_Semi:
					// <Statement> ::= <Stmt_Quit> ';'
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement_Semi2:
					// <Statement> ::= <Stmt_Return> ';'
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement_Semi3:
					// <Statement> ::= <Stmt_Out> ';'
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement_Semi4:
					// <Statement> ::= <Stmt_In> ';'
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement_Semi5:
					// <Statement> ::= <Stmt_Inc> ';'
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement_Semi6:
					// <Statement> ::= <Stmt_Assignment> ';'
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement_Begin_End:
					// <Statement> ::= begin <StatementList> end
					result = getStmtListAsStatement(p, r, 1);
					break;

				case ProductionIndex.Statement:
					// <Statement> ::= <Stmt_If>
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement2:
					// <Statement> ::= <Stmt_While>
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement3:
					// <Statement> ::= <Stmt_Repeat>
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement4:
					// <Statement> ::= <Stmt_Goto>
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement5:
					// <Statement> ::= <Stmt_Label>
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statement6:
					// <Statement> ::= <Stmt_Call> ';'
					result = (Statement)r.get_Data(0);
					break;

				case ProductionIndex.Statementlist:
					// <StatementList> ::= <StatementList> <Statement>
					result = ((List_Statements)r.get_Data(0)).Append((Statement)r.get_Data(1));
					break;

				case ProductionIndex.Statementlist2:
					// <StatementList> ::=
					result = new List_Statements(p);
					break;

				case ProductionIndex.Stmt_inc_Plusplus:
					// <Stmt_Inc> ::= <ValuePointer> '++'
					result = new Statement_Inc(p, (Expression_ValuePointer)r.get_Data(0));
					break;

				case ProductionIndex.Stmt_inc_Minusminus:
					// <Stmt_Inc> ::= <ValuePointer> '--'
					result = new Statement_Dec(p, (Expression_ValuePointer)r.get_Data(0));
					break;

				case ProductionIndex.Stmt_quit_Quit:
					// <Stmt_Quit> ::= quit
					result = new Statement_Quit(p);
					break;

				case ProductionIndex.Stmt_quit_Stop:
					// <Stmt_Quit> ::= stop
					result = new Statement_Quit(p);
					break;

				case ProductionIndex.Stmt_quit_Close:
					// <Stmt_Quit> ::= close
					result = new Statement_Quit(p);
					break;

				case ProductionIndex.Stmt_out_Out:
					// <Stmt_Out> ::= out <Expression>
					result = new Statement_Out(p, (Expression)r.get_Data(1));
					break;

				case ProductionIndex.Stmt_out_Out2:
					// <Stmt_Out> ::= out <Literal_String>
					result = new Statement_Out_CharArrLiteral(p, (Literal_CharArr)r.get_Data(1));
					break;

				case ProductionIndex.Stmt_in_In:
					// <Stmt_In> ::= in <ValuePointer>
					result = new Statement_In(p, (Expression_ValuePointer)r.get_Data(1));
					break;

				case ProductionIndex.Stmt_assignment_Eq:
					// <Stmt_Assignment> ::= <ValuePointer> '=' <Expression>
					result = new Statement_Assignment(p, (Expression_ValuePointer)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Stmt_return_Return:
					// <Stmt_Return> ::= return <Expression>
					result = new Statement_Return(p, (Expression)r.get_Data(1));
					break;

				case ProductionIndex.Stmt_return_Return2:
					// <Stmt_Return> ::= return
					result = new Statement_Return(p);
					break;

				case ProductionIndex.Stmt_call_Identifier_Lparen_Rparen:
					// <Stmt_Call> ::= Identifier '(' <ExpressionList> ')'
					result = new Statement_MethodCall(p, getStrData(r), ((List_Expressions)r.get_Data(2)).List);
					break;

				case ProductionIndex.Stmt_call_Identifier_Lparen_Rparen2:
					// <Stmt_Call> ::= Identifier '(' ')'
					result = new Statement_MethodCall(p, getStrData(r));
					break;

				case ProductionIndex.Stmt_if_If_Lparen_Rparen_Then_End:
					// <Stmt_If> ::= if '(' <Expression> ')' then <StatementList> <Stmt_ElseIfList> end
					result = new Statement_If(p, (Expression)r.get_Data(2), getStmtListAsStatement(p, r, 5), (Statement)r.get_Data(6));
					break;

				case ProductionIndex.Stmt_elseiflist_Elsif_Lparen_Rparen_Then:
					// <Stmt_ElseIfList> ::= elsif '(' <Expression> ')' then <StatementList> <Stmt_ElseIfList>
					result = new Statement_If(p, (Expression)r.get_Data(2), getStmtListAsStatement(p, r, 5), (Statement)r.get_Data(6));
					break;

				case ProductionIndex.Stmt_elseiflist_Else:
					// <Stmt_ElseIfList> ::= else <StatementList>
					result = getStmtListAsStatement(p, r, 1);
					break;

				case ProductionIndex.Stmt_elseiflist:
					// <Stmt_ElseIfList> ::=
					result = new Statement_NOP(p);
					break;

				case ProductionIndex.Stmt_while_While_Lparen_Rparen_Do_End:
					// <Stmt_While> ::= while '(' <<Expression>> ')' do <StatementList> end
					result = new Statement_While(p, (Expression)r.get_Data(2), getStmtListAsStatement(p, r, 5));
					break;

				case ProductionIndex.Stmt_repeat_Repeat_Until_Lparen_Rparen:
					// <Stmt_Repeat> ::= repeat <StatementList> until '(' <Expression> ')'
					result = new Statement_RepeatUntil(p, (Expression)r.get_Data(4), getStmtListAsStatement(p, r, 1));
					break;

				case ProductionIndex.Stmt_goto_Goto_Identifier:
					// <Stmt_Goto> ::= goto Identifier
					result = new Statement_Goto(p, getStrData(1, r));
					break;

				case ProductionIndex.Stmt_label_Identifier_Colon:
					// <Stmt_Label> ::= Identifier ':'
					result = new Statement_Label(p, getStrData(r));
					break;

				case ProductionIndex.Type:
					// <Type> ::= <Type_Int>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type2:
					// <Type> ::= <Type_Digit>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type3:
					// <Type> ::= <Type_Char>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type4:
					// <Type> ::= <Type_Bool>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type5:
					// <Type> ::= <Type_Void>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type6:
					// <Type> ::= <Type_IntArr>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type7:
					// <Type> ::= <Type_String>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type8:
					// <Type> ::= <Type_DigitArr>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type9:
					// <Type> ::= <Type_BoolArr>
					result = (BType)r.get_Data(0);
					break;

				case ProductionIndex.Type_int_Int:
					// <Type_Int> ::= int
					result = new BType_Int(p);
					break;

				case ProductionIndex.Type_int_Integer:
					// <Type_Int> ::= integer
					result = new BType_Int(p);
					break;

				case ProductionIndex.Type_char_Char:
					// <Type_Char> ::= char
					result = new BType_Char(p);
					break;

				case ProductionIndex.Type_char_Character:
					// <Type_Char> ::= Character
					result = new BType_Char(p);
					break;

				case ProductionIndex.Type_digit_Digit:
					// <Type_Digit> ::= digit
					result = new BType_Digit(p);
					break;

				case ProductionIndex.Type_bool_Bool:
					// <Type_Bool> ::= bool
					result = new BType_Bool(p);
					break;

				case ProductionIndex.Type_bool_Boolean:
					// <Type_Bool> ::= boolean
					result = new BType_Bool(p);
					break;

				case ProductionIndex.Type_void_Void:
					// <Type_Void> ::= void
					result = new BType_Void(p);
					break;

				case ProductionIndex.Type_intarr_Lbracket_Rbracket:
					// <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
					result = new BType_IntArr(p, ((Literal_Int)r.get_Data(2)).Value);
					break;

				case ProductionIndex.Type_string_Lbracket_Rbracket:
					// <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
					result = new BType_CharArr(p, ((Literal_Int)r.get_Data(2)).Value);
					break;

				case ProductionIndex.Type_digitarr_Lbracket_Rbracket:
					// <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
					result = new BType_DigitArr(p, ((Literal_Int)r.get_Data(2)).Value);
					break;

				case ProductionIndex.Type_boolarr_Lbracket_Rbracket:
					// <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
					result = new BType_BoolArr(p, ((Literal_Int)r.get_Data(2)).Value);
					break;

				case ProductionIndex.Literal:
					// <Literal> ::= <Array_Literal>
					result = (Literal)r.get_Data(0);
					break;

				case ProductionIndex.Literal2:
					// <Literal> ::= <Value_Literal>
					result = (Literal)r.get_Data(0);
					break;

				case ProductionIndex.Array_literal:
					// <Array_Literal> ::= <Literal_IntArr>
					result = (Literal_Array)r.get_Data(0);
					break;

				case ProductionIndex.Array_literal2:
					// <Array_Literal> ::= <Literal_String>
					result = (Literal_Array)r.get_Data(0);
					break;

				case ProductionIndex.Array_literal3:
					// <Array_Literal> ::= <Literal_DigitArr>
					result = (Literal_Array)r.get_Data(0);
					break;

				case ProductionIndex.Array_literal4:
					// <Array_Literal> ::= <Literal_BoolArr>
					result = (Literal_Array)r.get_Data(0);
					break;

				case ProductionIndex.Value_literal:
					// <Value_Literal> ::= <Literal_Int>
					result = (Literal_Value)r.get_Data(0);
					break;

				case ProductionIndex.Value_literal2:
					// <Value_Literal> ::= <Literal_Char>
					result = (Literal_Value)r.get_Data(0);
					break;

				case ProductionIndex.Value_literal3:
					// <Value_Literal> ::= <Literal_Bool>
					result = (Literal_Value)r.get_Data(0);
					break;

				case ProductionIndex.Value_literal4:
					// <Value_Literal> ::= <Literal_Digit>
					result = (Literal_Value)r.get_Data(0);
					break;

				case ProductionIndex.Literal_int_Decliteral:
					// <Literal_Int> ::= DecLiteral
					result = new Literal_Int(p, Convert.ToInt32(getStrData(r), 10));
					break;

				case ProductionIndex.Literal_int_Hexliteral:
					// <Literal_Int> ::= HexLiteral
					result = new Literal_Int(p, Convert.ToInt32(getStrData(r), 16));
					break;

				case ProductionIndex.Literal_char_Charliteral:
					// <Literal_Char> ::= CharLiteral
					result = new Literal_Char(p, unescapeChr(p, getStrTrimData(r)));
					break;

				case ProductionIndex.Literal_bool_True:
					// <Literal_Bool> ::= true
					result = new Literal_Bool(p, true);
					break;

				case ProductionIndex.Literal_bool_False:
					// <Literal_Bool> ::= false
					result = new Literal_Bool(p, false);
					break;

				case ProductionIndex.Literal_digit_Digitliteral:
					// <Literal_Digit> ::= DigitLiteral
					result = new Literal_Digit(p, Convert.ToByte(getStrData(r).Substring(1), 10));
					break;

				case ProductionIndex.Literal_intarr_Lbrace_Rbrace:
					// <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
					result = new Literal_IntArr(p, ((List_LiteralInts)r.get_Data(1)).List.Select(c => c.Value).ToList());
					break;

				case ProductionIndex.Literal_string_Lbrace_Rbrace:
					// <Literal_String> ::= '{' <Literal_Char_List> '}'
					result = new Literal_CharArr(p, ((List_LiteralChars)r.get_Data(1)).List.Select(c => c.Value).ToList());
					break;

				case ProductionIndex.Literal_string_Stringliteral:
					// <Literal_String> ::= StringLiteral
					result = new Literal_CharArr(p, unescapeStr(p, getStrTrimData(r)));
					break;

				case ProductionIndex.Literal_digitarr_Lbrace_Rbrace:
					// <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
					result = new Literal_DigitArr(p, ((List_LiteralDigits)r.get_Data(1)).List.Select(c => c.Value).ToList());
					break;

				case ProductionIndex.Literal_boolarr_Lbrace_Rbrace:
					// <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
					result = new Literal_BoolArr(p, ((List_LiteralBools)r.get_Data(1)).List.Select(c => c.Value).ToList());
					break;

				case ProductionIndex.Literal_int_list_Comma:
					// <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
					result = ((List_LiteralInts)r.get_Data(0)).Append((Literal_Int)r.get_Data(2));
					break;

				case ProductionIndex.Literal_int_list:
					// <Literal_Int_List> ::= <Literal_Int>
					result = new List_LiteralInts(p, (Literal_Int)r.get_Data(0));
					break;

				case ProductionIndex.Literal_char_list_Comma:
					// <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
					result = ((List_LiteralChars)r.get_Data(0)).Append((Literal_Char)r.get_Data(2));
					break;

				case ProductionIndex.Literal_char_list:
					// <Literal_Char_List> ::= <Literal_Char>
					result = new List_LiteralChars(p, (Literal_Char)r.get_Data(0));
					break;

				case ProductionIndex.Literal_digit_list_Comma:
					// <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
					result = ((List_LiteralDigits)r.get_Data(0)).Append((Literal_Digit)r.get_Data(2));
					break;

				case ProductionIndex.Literal_digit_list:
					// <Literal_Digit_List> ::= <Literal_Digit>
					result = new List_LiteralDigits(p, (Literal_Digit)r.get_Data(0));
					break;

				case ProductionIndex.Literal_bool_list_Comma:
					// <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
					result = ((List_LiteralBools)r.get_Data(0)).Append((Literal_Bool)r.get_Data(2));
					break;

				case ProductionIndex.Literal_bool_list:
					// <Literal_Bool_List> ::= <Literal_Bool>
					result = new List_LiteralBools(p, (Literal_Bool)r.get_Data(0));
					break;

				case ProductionIndex.Expression:
					// <Expression> ::= <Expr Bool>
					result = (Expression)r.get_Data(0);
					break;

				case ProductionIndex.Exprbool_Ampamp:
					// <Expr Bool> ::= <Expr Bool> '&&' <Expr Eq>
					result = new Expression_And(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Exprbool_Pipepipe:
					// <Expr Bool> ::= <Expr Bool> '||' <Expr Eq>
					result = new Expression_Or(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Exprbool_Caret:
					// <Expr Bool> ::= <Expr Bool> '^' <Expr Eq>
					result = new Expression_Xor(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Exprbool:
					// <Expr Bool> ::= <Expr Eq>
					result = (Expression)r.get_Data(0);
					break;

				case ProductionIndex.Expreq_Eqeq:
					// <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
					result = new Expression_Equals(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expreq_Exclameq:
					// <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
					result = new Expression_Unequals(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expreq:
					// <Expr Eq> ::= <Exp Comp>
					result = (Expression)r.get_Data(0);
					break;

				case ProductionIndex.Expcomp_Lt:
					// <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
					result = new Expression_Lesser(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expcomp_Gt:
					// <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
					result = new Expression_Greater(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expcomp_Lteq:
					// <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
					result = new Expression_LesserEquals(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expcomp_Gteq:
					// <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
					result = new Expression_GreaterEquals(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expcomp:
					// <Exp Comp> ::= <Exp Add>
					result = (Expression)r.get_Data(0);
					break;

				case ProductionIndex.Expadd_Plus:
					// <Exp Add> ::= <Exp Add> '+' <Exp Mult>
					result = new Expression_Add(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expadd_Minus:
					// <Exp Add> ::= <Exp Add> '-' <Exp Mult>
					result = new Expression_Sub(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expadd:
					// <Exp Add> ::= <Exp Mult>
					result = (Expression)r.get_Data(0);
					break;

				case ProductionIndex.Expmult_Times:
					// <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
					result = new Expression_Mult(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expmult_Div:
					// <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
					result = new Expression_Div(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expmult_Percent:
					// <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
					result = new Expression_Mod(p, (Expression)r.get_Data(0), (Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expmult:
					// <Exp Mult> ::= <Exp Unary>
					result = (Expression)r.get_Data(0);
					break;

				case ProductionIndex.Expunary_Exclam:
					// <Exp Unary> ::= '!' <Value>
					result = new Expression_Not(p, (Expression)r.get_Data(1));
					break;

				case ProductionIndex.Expunary_Minus:
					// <Exp Unary> ::= '-' <Value>
					result = new Expression_Negate(p, (Expression)r.get_Data(1));
					break;

				case ProductionIndex.Expunary_Lparen_Rparen:
					// <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
					result = new Expression_Cast(p, (BType)r.get_Data(1), (Expression)r.get_Data(3));
					break;

				case ProductionIndex.Expunary:
					// <Exp Unary> ::= <Value>
					result = (Expression)r.get_Data(0);
					break;

				case ProductionIndex.Value:
					// <Value> ::= <Value_Literal>
					result = new Expression_Literal(p, (Literal_Value)r.get_Data(0));
					break;

				case ProductionIndex.Value_Rand:
					// <Value> ::= rand
					result = new Expression_Rand(p);
					break;

				case ProductionIndex.Value2:
					// <Value> ::= <ValuePointer>
					result = (Expression_ValuePointer)r.get_Data(0);
					break;

				case ProductionIndex.Value_Identifier_Lparen_Rparen:
					// <Value> ::= Identifier '(' <ExpressionList> ')'
					result = new Expression_FunctionCall(p, new Statement_MethodCall(p, getStrData(r), ((List_Expressions)r.get_Data(2)).List));
					break;

				case ProductionIndex.Value_Identifier_Lparen_Rparen2:
					// <Value> ::= Identifier '(' ')'
					result = new Expression_FunctionCall(p, new Statement_MethodCall(p, getStrData(r)));
					break;

				case ProductionIndex.Value_Lparen_Rparen:
					// <Value> ::= '(' <Expression> ')'
					result = (Expression)r.get_Data(1);
					break;

				case ProductionIndex.Expressionlist_Comma:
					// <ExpressionList> ::= <ExpressionList> ',' <Expression>
					result = ((List_Expressions)r.get_Data(0)).Append((Expression)r.get_Data(2));
					break;

				case ProductionIndex.Expressionlist:
					// <ExpressionList> ::= <Expression>
					result = new List_Expressions(p, (Expression)r.get_Data(0));
					break;

				case ProductionIndex.Valuepointer_Identifier:
					// <ValuePointer> ::= Identifier
					result = new Expression_DirectValuePointer(p, getStrData(r));
					break;

				case ProductionIndex.Valuepointer_Identifier_Lbracket_Rbracket:
					// <ValuePointer> ::= Identifier '[' <Expression> ']'
					result = new Expression_ArrayValuePointer(p, getStrData(r), (Expression)r.get_Data(2));
					break;
			}  //switch

			if (result == null)
			{
				throw new Exception("Reduction not parsed: " + r.Parent);
			}

			return result;
		}

		private static VarDeclaration createASTDeclarationFromReduction(GOLD.Reduction r, bool hasInit, SourceCodePosition p)
		{
			if (hasInit)
			{
				if (r.get_Data(0) is BType_Array)
					return new VarDeclaration_Array(p, (BType_Array)r.get_Data(0), getStrData(1, r), (Literal_Array)r.get_Data(3));
				else if (r.get_Data(0) is BType_Value)
					return new VarDeclaration_Value(p, (BType_Value)r.get_Data(0), getStrData(1, r), (Literal_Value)r.get_Data(3));
				else
					return null;
			}
			else
			{
				if (r.get_Data(0) is BType_Array)
					return new VarDeclaration_Array(p, (BType_Array)r.get_Data(0), getStrData(1, r));
				else if (r.get_Data(0) is BType_Value)
					return new VarDeclaration_Value(p, (BType_Value)r.get_Data(0), getStrData(1, r));
				else
					return null;
			}
		}

		private static string getStrData(GOLD.Reduction r)
		{
			return getStrData(0, r);
		}

		private static string getStrData(int p, GOLD.Reduction r)
		{
			if (r.get_Data(p) == null)
			{
				Console.Beep();
			}
			return (string)r.get_Data(p);
		}

		private static string unescapeStr(SourceCodePosition p, string s)
		{
			StringBuilder outstr = new StringBuilder();

			bool esc = false;
			foreach (char chr in s)
			{
				if (esc)
				{
					outstr.Append(unescapeChr(p, "\\" + chr));

					esc = false;
				}
				else
				{
					if (chr == '\\')
						esc = true;
					else
						outstr.Append(chr);
				}
			}

			return outstr.ToString();
		}

		private static char unescapeChr(SourceCodePosition p, string s)
		{
			if (s.Length == 1)
			{
				return s[0];
			}
			else if (s.Length == 2 && s[0] == '\\')
			{
				switch (s[1])
				{
					case 'r':
						return '\r';
					case 'n':
						return '\n';
					case '0':
						return '\0';
					case 'b':
						return '\b';
					case 'v':
						return '\v';
					case 'f':
						return '\f';
					case 'a':
						return '\a';
					case '\\':
						return '\\';
					default:
						throw new InvalidFormatSpecifierException(s, p);
				}
			}
			else
			{
				throw new InvalidFormatSpecifierException(s, p);
			}
		}

		private static string getStrTrimData(GOLD.Reduction r)
		{
			string s = getStrData(r);
			return s.Substring(1, s.Length - 2);
		}

		private static Statement_StatementList getStmtListAsStatement(SourceCodePosition p, GOLD.Reduction r, int pos)
		{
			return new Statement_StatementList(p, ((List_Statements)r.get_Data(pos)).List);
		}

		private static Statement_StatementList StmtToStmtList(Statement s)
		{
			if (s is Statement_StatementList)
				return s as Statement_StatementList;
			else
				return new Statement_StatementList(s.Position, new List<Statement>() { s });
		}
	}
}