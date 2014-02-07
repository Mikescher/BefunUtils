﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BefunGen.AST
{
	static class GrammarTableMap
	{
		public static enum SymbolIndex
		{
			@Eof = 0,                                  // (EOF)
			@Error = 1,                                // (Error)
			@Comment = 2,                              // Comment
			@Newline = 3,                              // NewLine
			@Whitespace = 4,                           // Whitespace
			@Timesdiv = 5,                             // '*/'
			@Divtimes = 6,                             // '/*'
			@Divdiv = 7,                               // '//'
			@Minus = 8,                                // '-'
			@Minusminus = 9,                           // '--'
			@Exclam = 10,                              // '!'
			@Exclameq = 11,                            // '!='
			@Percent = 12,                             // '%'
			@Lparen = 13,                              // '('
			@Rparen = 14,                              // ')'
			@Times = 15,                               // '*'
			@Comma = 16,                               // ','
			@Div = 17,                                 // '/'
			@Colon = 18,                               // ':'
			@Coloneq = 19,                             // ':='
			@Semi = 20,                                // ';'
			@Lbracket = 21,                            // '['
			@Rbracket = 22,                            // ']'
			@Lbrace = 23,                              // '{'
			@Rbrace = 24,                              // '}'
			@Plus = 25,                                // '+'
			@Plusplus = 26,                            // '++'
			@Lt = 27,                                  // '<'
			@Lteq = 28,                                // '<='
			@Eq = 29,                                  // '='
			@Eqeq = 30,                                // '=='
			@Gt = 31,                                  // '>'
			@Gteq = 32,                                // '>='
			@Begin = 33,                               // begin
			@Bool = 34,                                // bool
			@Boolean = 35,                             // boolean
			@Char = 36,                                // char
			@Character = 37,                           // Character
			@Charliteral = 38,                         // CharLiteral
			@Close = 39,                               // close
			@Decliteral = 40,                          // DecLiteral
			@Digit = 41,                               // digit
			@Digitliteral = 42,                        // DigitLiteral
			@Do = 43,                                  // do
			@Else = 44,                                // else
			@End = 45,                                 // end
			@False = 46,                               // false
			@Goto = 47,                                // goto
			@Hexliteral = 48,                          // HexLiteral
			@Identifier = 49,                          // Identifier
			@If = 50,                                  // if
			@In = 51,                                  // in
			@Int = 52,                                 // int
			@Integer = 53,                             // integer
			@Out = 54,                                 // out
			@Program = 55,                             // program
			@Quit = 56,                                // quit
			@Rand = 57,                                // rand
			@Repeat = 58,                              // repeat
			@Return = 59,                              // return
			@Stop = 60,                                // stop
			@Stringliteral = 61,                       // StringLiteral
			@Then = 62,                                // then
			@True = 63,                                // true
			@Until = 64,                               // until
			@Var = 65,                                 // var
			@Void = 66,                                // void
			@While = 67,                               // while
			@Array_literal = 68,                       // <Array_Literal>
			@Expadd = 69,                              // <Exp Add>
			@Expcomp = 70,                             // <Exp Comp>
			@Expmult = 71,                             // <Exp Mult>
			@Expunary = 72,                            // <Exp Unary>
			@Expreq = 73,                              // <Expr Eq>
			@Expression = 74,                          // <Expression>
			@Expressionlist = 75,                      // <ExpressionList>
			@Footer = 76,                              // <Footer>
			@Header = 77,                              // <Header>
			@Literal = 78,                             // <Literal>
			@Literal_bool = 79,                        // <Literal_Bool>
			@Literal_bool_list = 80,                   // <Literal_Bool_List>
			@Literal_boolarr = 81,                     // <Literal_BoolArr>
			@Literal_char = 82,                        // <Literal_Char>
			@Literal_char_list = 83,                   // <Literal_Char_List>
			@Literal_digit = 84,                       // <Literal_Digit>
			@Literal_digit_list = 85,                  // <Literal_Digit_List>
			@Literal_digitarr = 86,                    // <Literal_DigitArr>
			@Literal_int = 87,                         // <Literal_Int>
			@Literal_int_list = 88,                    // <Literal_Int_List>
			@Literal_intarr = 89,                      // <Literal_IntArr>
			@Literal_string = 90,                      // <Literal_String>
			@Mainstatements = 91,                      // <MainStatements>
			@Method = 92,                              // <Method>
			@Methodbody = 93,                          // <MethodBody>
			@Methodheader = 94,                        // <MethodHeader>
			@Methodlist = 95,                          // <MethodList>
			@Param = 96,                               // <Param>
			@Paramdecl = 97,                           // <ParamDecl>
			@Paramlist = 98,                           // <ParamList>
			@Program2 = 99,                            // <Program>
			@Statement = 100,                          // <Statement>
			@Statementlist = 101,                      // <StatementList>
			@Stmt_assignment = 102,                    // <Stmt_Assignment>
			@Stmt_goto = 103,                          // <Stmt_Goto>
			@Stmt_if = 104,                            // <Stmt_If>
			@Stmt_in = 105,                            // <Stmt_In>
			@Stmt_inc = 106,                           // <Stmt_Inc>
			@Stmt_label = 107,                         // <Stmt_Label>
			@Stmt_out = 108,                           // <Stmt_Out>
			@Stmt_quit = 109,                          // <Stmt_Quit>
			@Stmt_repeat = 110,                        // <Stmt_Repeat>
			@Stmt_return = 111,                        // <Stmt_Return>
			@Stmt_while = 112,                         // <Stmt_While>
			@Type = 113,                               // <Type>
			@Type_bool = 114,                          // <Type_Bool>
			@Type_boolarr = 115,                       // <Type_BoolArr>
			@Type_char = 116,                          // <Type_Char>
			@Type_digit = 117,                         // <Type_Digit>
			@Type_digitarr = 118,                      // <Type_DigitArr>
			@Type_int = 119,                           // <Type_Int>
			@Type_intarr = 120,                        // <Type_IntArr>
			@Type_string = 121,                        // <Type_String>
			@Type_void = 122,                          // <Type_Void>
			@Value = 123,                              // <Value>
			@Value_literal = 124,                      // <Value_Literal>
			@Valuepointer = 125,                       // <ValuePointer>
			@Vardecl = 126,                            // <VarDecl>
			@Vardeclbody = 127,                        // <VarDeclBody>
			@Varlist = 128                             // <VarList>
		}

		public static enum ProductionIndex
		{
			@Program = 0,                              // <Program> ::= <Header> <MainStatements> <MethodList> <Footer>
			@Header_Program_Identifier = 1,            // <Header> ::= program Identifier
			@Footer_End = 2,                           // <Footer> ::= end
			@Methodlist = 3,                           // <MethodList> ::= <Method> <MethodList>
			@Methodlist2 = 4,                          // <MethodList> ::= 
			@Mainstatements = 5,                       // <MainStatements> ::= <MethodBody>
			@Method = 6,                               // <Method> ::= <MethodHeader> <MethodBody>
			@Methodbody = 7,                           // <MethodBody> ::= <VarDeclBody> <Statement>
			@Methodheader_Identifier_Lparen_Rparen = 8,  // <MethodHeader> ::= <Type> Identifier '(' <ParamDecl> ')'
			@Vardeclbody_Var = 9,                      // <VarDeclBody> ::= var <VarList>
			@Vardeclbody = 10,                         // <VarDeclBody> ::= 
			@Paramdecl = 11,                           // <ParamDecl> ::= <ParamList>
			@Paramdecl2 = 12,                          // <ParamDecl> ::= 
			@Paramlist_Comma = 13,                     // <ParamList> ::= <ParamList> ',' <Param>
			@Paramlist = 14,                           // <ParamList> ::= <Param>
			@Param_Identifier = 15,                    // <Param> ::= <Type> Identifier
			@Varlist_Semi = 16,                        // <VarList> ::= <VarList> <VarDecl> ';'
			@Varlist_Semi2 = 17,                       // <VarList> ::= <VarDecl> ';'
			@Vardecl_Identifier = 18,                  // <VarDecl> ::= <Type> Identifier
			@Vardecl_Identifier_Coloneq = 19,          // <VarDecl> ::= <Type> Identifier ':=' <Literal>
			@Statement_Semi = 20,                      // <Statement> ::= <Stmt_Quit> ';'
			@Statement_Semi2 = 21,                     // <Statement> ::= <Stmt_Return> ';'
			@Statement_Semi3 = 22,                     // <Statement> ::= <Stmt_Out> ';'
			@Statement_Semi4 = 23,                     // <Statement> ::= <Stmt_In> ';'
			@Statement_Semi5 = 24,                     // <Statement> ::= <Stmt_Inc> ';'
			@Statement_Semi6 = 25,                     // <Statement> ::= <Stmt_Assignment> ';'
			@Statement_Begin_End = 26,                 // <Statement> ::= begin <StatementList> end
			@Statement = 27,                           // <Statement> ::= <Stmt_If>
			@Statement2 = 28,                          // <Statement> ::= <Stmt_While>
			@Statement3 = 29,                          // <Statement> ::= <Stmt_Repeat>
			@Statement4 = 30,                          // <Statement> ::= <Stmt_Goto>
			@Statement5 = 31,                          // <Statement> ::= <Stmt_Label>
			@Statementlist = 32,                       // <StatementList> ::= <Statement> <StatementList>
			@Statementlist2 = 33,                      // <StatementList> ::= 
			@Stmt_inc_Identifier_Plusplus = 34,        // <Stmt_Inc> ::= Identifier '++'
			@Stmt_inc_Identifier_Minusminus = 35,      // <Stmt_Inc> ::= Identifier '--'
			@Stmt_quit_Quit = 36,                      // <Stmt_Quit> ::= quit
			@Stmt_quit_Stop = 37,                      // <Stmt_Quit> ::= stop
			@Stmt_quit_Close = 38,                     // <Stmt_Quit> ::= close
			@Stmt_out_Out = 39,                        // <Stmt_Out> ::= out <Expression>
			@Stmt_in_In = 40,                          // <Stmt_In> ::= in <ValuePointer>
			@Stmt_assignment_Eq = 41,                  // <Stmt_Assignment> ::= <ValuePointer> '=' <Expression>
			@Stmt_return_Return = 42,                  // <Stmt_Return> ::= return <Expression>
			@Stmt_return_Return2 = 43,                 // <Stmt_Return> ::= return
			@Stmt_if_If_Lparen_Rparen_Then_End = 44,   // <Stmt_If> ::= if '(' <Expression> ')' then <Statement> end
			@Stmt_if_If_Lparen_Rparen_Then_Else_End = 45,  // <Stmt_If> ::= if '(' <Expression> ')' then <Statement> else <Statement> end
			@Stmt_while_While_Lparen_Rparen_Do = 46,   // <Stmt_While> ::= while '(' <Expression> ')' do <Statement>
			@Stmt_repeat_Repeat_Until_Lparen_Rparen = 47,  // <Stmt_Repeat> ::= repeat <Statement> until '(' <Expression> ')'
			@Stmt_goto_Goto_Identifier = 48,           // <Stmt_Goto> ::= goto Identifier
			@Stmt_label_Identifier_Colon = 49,         // <Stmt_Label> ::= Identifier ':'
			@Type = 50,                                // <Type> ::= <Type_Int>
			@Type2 = 51,                               // <Type> ::= <Type_Digit>
			@Type3 = 52,                               // <Type> ::= <Type_Char>
			@Type4 = 53,                               // <Type> ::= <Type_Bool>
			@Type5 = 54,                               // <Type> ::= <Type_Void>
			@Type6 = 55,                               // <Type> ::= <Type_IntArr>
			@Type7 = 56,                               // <Type> ::= <Type_String>
			@Type8 = 57,                               // <Type> ::= <Type_DigitArr>
			@Type9 = 58,                               // <Type> ::= <Type_BoolArr>
			@Type_int_Int = 59,                        // <Type_Int> ::= int
			@Type_int_Integer = 60,                    // <Type_Int> ::= integer
			@Type_char_Char = 61,                      // <Type_Char> ::= char
			@Type_char_Character = 62,                 // <Type_Char> ::= Character
			@Type_digit_Digit = 63,                    // <Type_Digit> ::= digit
			@Type_bool_Bool = 64,                      // <Type_Bool> ::= bool
			@Type_bool_Boolean = 65,                   // <Type_Bool> ::= boolean
			@Type_void_Void = 66,                      // <Type_Void> ::= void
			@Type_intarr_Lbracket_Rbracket = 67,       // <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
			@Type_string_Lbracket_Rbracket = 68,       // <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
			@Type_digitarr_Lbracket_Rbracket = 69,     // <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
			@Type_boolarr_Lbracket_Rbracket = 70,      // <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
			@Literal = 71,                             // <Literal> ::= <Array_Literal>
			@Literal2 = 72,                            // <Literal> ::= <Value_Literal>
			@Array_literal = 73,                       // <Array_Literal> ::= <Literal_IntArr>
			@Array_literal2 = 74,                      // <Array_Literal> ::= <Literal_String>
			@Array_literal3 = 75,                      // <Array_Literal> ::= <Literal_DigitArr>
			@Array_literal4 = 76,                      // <Array_Literal> ::= <Literal_BoolArr>
			@Value_literal = 77,                       // <Value_Literal> ::= <Literal_Int>
			@Value_literal2 = 78,                      // <Value_Literal> ::= <Literal_Char>
			@Value_literal3 = 79,                      // <Value_Literal> ::= <Literal_Bool>
			@Value_literal4 = 80,                      // <Value_Literal> ::= <Literal_Digit>
			@Literal_int_Decliteral = 81,              // <Literal_Int> ::= DecLiteral
			@Literal_int_Hexliteral = 82,              // <Literal_Int> ::= HexLiteral
			@Literal_char_Charliteral = 83,            // <Literal_Char> ::= CharLiteral
			@Literal_bool_True = 84,                   // <Literal_Bool> ::= true
			@Literal_bool_False = 85,                  // <Literal_Bool> ::= false
			@Literal_digit_Digitliteral = 86,          // <Literal_Digit> ::= DigitLiteral
			@Literal_intarr_Lbrace_Rbrace = 87,        // <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
			@Literal_string_Lbrace_Rbrace = 88,        // <Literal_String> ::= '{' <Literal_Char_List> '}'
			@Literal_string_Stringliteral = 89,        // <Literal_String> ::= StringLiteral
			@Literal_digitarr_Lbrace_Rbrace = 90,      // <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
			@Literal_boolarr_Lbrace_Rbrace = 91,       // <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
			@Literal_int_list_Comma = 92,              // <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
			@Literal_int_list = 93,                    // <Literal_Int_List> ::= <Literal_Int>
			@Literal_char_list_Comma = 94,             // <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
			@Literal_char_list = 95,                   // <Literal_Char_List> ::= <Literal_Char>
			@Literal_digit_list_Comma = 96,            // <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
			@Literal_digit_list = 97,                  // <Literal_Digit_List> ::= <Literal_Digit>
			@Literal_bool_list_Comma = 98,             // <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
			@Literal_bool_list = 99,                   // <Literal_Bool_List> ::= <Literal_Bool>
			@Expression = 100,                         // <Expression> ::= <Expr Eq>
			@Expreq_Eqeq = 101,                        // <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
			@Expreq_Exclameq = 102,                    // <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
			@Expreq = 103,                             // <Expr Eq> ::= <Exp Comp>
			@Expcomp_Lt = 104,                         // <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
			@Expcomp_Gt = 105,                         // <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
			@Expcomp_Lteq = 106,                       // <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
			@Expcomp_Gteq = 107,                       // <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
			@Expcomp = 108,                            // <Exp Comp> ::= <Exp Add>
			@Expadd_Plus = 109,                        // <Exp Add> ::= <Exp Add> '+' <Exp Mult>
			@Expadd_Minus = 110,                       // <Exp Add> ::= <Exp Add> '-' <Exp Mult>
			@Expadd = 111,                             // <Exp Add> ::= <Exp Mult>
			@Expmult_Times = 112,                      // <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
			@Expmult_Div = 113,                        // <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
			@Expmult_Percent = 114,                    // <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
			@Expmult = 115,                            // <Exp Mult> ::= <Exp Unary>
			@Expunary_Exclam = 116,                    // <Exp Unary> ::= '!' <Value>
			@Expunary_Minus = 117,                     // <Exp Unary> ::= '-' <Value>
			@Expunary_Lparen_Rparen = 118,             // <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
			@Expunary = 119,                           // <Exp Unary> ::= <Value>
			@Value = 120,                              // <Value> ::= <Value_Literal>
			@Value_Rand = 121,                         // <Value> ::= rand
			@Value2 = 122,                             // <Value> ::= <ValuePointer>
			@Value_Identifier_Lparen_Rparen = 123,     // <Value> ::= Identifier '(' <ExpressionList> ')'
			@Value_Identifier_Lparen_Rparen2 = 124,    // <Value> ::= Identifier '(' ')'
			@Value_Lparen_Rparen = 125,                // <Value> ::= '(' <Expression> ')'
			@Expressionlist_Comma = 126,               // <ExpressionList> ::= <ExpressionList> ',' <Expression>
			@Expressionlist = 127,                     // <ExpressionList> ::= <Expression>
			@Valuepointer_Identifier = 128,            // <ValuePointer> ::= Identifier
			@Valuepointer_Identifier_Lbracket_Rbracket = 129   // <ValuePointer> ::= Identifier '[' <Expression> ']'
		}

		private ASTObject CreateNewASTObject(GOLD.Reduction r)
		{
			ASTObject result = null;

			switch ((ProductionIndex)r.Parent.TableIndex())
			{
				case ProductionIndex.Program:
					// <Program> ::= <Header> <MainStatements> <MethodList> <Footer>
					break;

				case ProductionIndex.Header_Program_Identifier:
					// <Header> ::= program Identifier
					break;

				case ProductionIndex.Footer_End:
					// <Footer> ::= end
					break;

				case ProductionIndex.Methodlist:
					// <MethodList> ::= <Method> <MethodList>
					break;

				case ProductionIndex.Methodlist2:
					// <MethodList> ::= 
					break;

				case ProductionIndex.Mainstatements:
					// <MainStatements> ::= <MethodBody>
					break;

				case ProductionIndex.Method:
					// <Method> ::= <MethodHeader> <MethodBody>
					break;

				case ProductionIndex.Methodbody:
					// <MethodBody> ::= <VarDeclBody> <Statement>
					break;

				case ProductionIndex.Methodheader_Identifier_Lparen_Rparen:
					// <MethodHeader> ::= <Type> Identifier '(' <ParamDecl> ')'
					break;

				case ProductionIndex.Vardeclbody_Var:
					// <VarDeclBody> ::= var <VarList>
					break;

				case ProductionIndex.Vardeclbody:
					// <VarDeclBody> ::= 
					break;

				case ProductionIndex.Paramdecl:
					// <ParamDecl> ::= <ParamList>
					break;

				case ProductionIndex.Paramdecl2:
					// <ParamDecl> ::= 
					break;

				case ProductionIndex.Paramlist_Comma:
					// <ParamList> ::= <ParamList> ',' <Param>
					break;

				case ProductionIndex.Paramlist:
					// <ParamList> ::= <Param>
					break;

				case ProductionIndex.Param_Identifier:
					// <Param> ::= <Type> Identifier
					break;

				case ProductionIndex.Varlist_Semi:
					// <VarList> ::= <VarList> <VarDecl> ';'
					break;

				case ProductionIndex.Varlist_Semi2:
					// <VarList> ::= <VarDecl> ';'
					break;

				case ProductionIndex.Vardecl_Identifier:
					// <VarDecl> ::= <Type> Identifier
					break;

				case ProductionIndex.Vardecl_Identifier_Coloneq:
					// <VarDecl> ::= <Type> Identifier ':=' <Literal>
					break;

				case ProductionIndex.Statement_Semi:
					// <Statement> ::= <Stmt_Quit> ';'
					break;

				case ProductionIndex.Statement_Semi2:
					// <Statement> ::= <Stmt_Return> ';'
					break;

				case ProductionIndex.Statement_Semi3:
					// <Statement> ::= <Stmt_Out> ';'
					break;

				case ProductionIndex.Statement_Semi4:
					// <Statement> ::= <Stmt_In> ';'
					break;

				case ProductionIndex.Statement_Semi5:
					// <Statement> ::= <Stmt_Inc> ';'
					break;

				case ProductionIndex.Statement_Semi6:
					// <Statement> ::= <Stmt_Assignment> ';'
					break;

				case ProductionIndex.Statement_Begin_End:
					// <Statement> ::= begin <StatementList> end
					break;

				case ProductionIndex.Statement:
					// <Statement> ::= <Stmt_If>
					break;

				case ProductionIndex.Statement2:
					// <Statement> ::= <Stmt_While>
					break;

				case ProductionIndex.Statement3:
					// <Statement> ::= <Stmt_Repeat>
					break;

				case ProductionIndex.Statement4:
					// <Statement> ::= <Stmt_Goto>
					break;

				case ProductionIndex.Statement5:
					// <Statement> ::= <Stmt_Label>
					break;

				case ProductionIndex.Statementlist:
					// <StatementList> ::= <Statement> <StatementList>
					break;

				case ProductionIndex.Statementlist2:
					// <StatementList> ::= 
					break;

				case ProductionIndex.Stmt_inc_Identifier_Plusplus:
					// <Stmt_Inc> ::= Identifier '++'
					break;

				case ProductionIndex.Stmt_inc_Identifier_Minusminus:
					// <Stmt_Inc> ::= Identifier '--'
					break;

				case ProductionIndex.Stmt_quit_Quit:
					// <Stmt_Quit> ::= quit
					break;

				case ProductionIndex.Stmt_quit_Stop:
					// <Stmt_Quit> ::= stop
					break;

				case ProductionIndex.Stmt_quit_Close:
					// <Stmt_Quit> ::= close
					break;

				case ProductionIndex.Stmt_out_Out:
					// <Stmt_Out> ::= out <Expression>
					break;

				case ProductionIndex.Stmt_in_In:
					// <Stmt_In> ::= in <ValuePointer>
					break;

				case ProductionIndex.Stmt_assignment_Eq:
					// <Stmt_Assignment> ::= <ValuePointer> '=' <Expression>
					break;

				case ProductionIndex.Stmt_return_Return:
					// <Stmt_Return> ::= return <Expression>
					break;

				case ProductionIndex.Stmt_return_Return2:
					// <Stmt_Return> ::= return
					break;

				case ProductionIndex.Stmt_if_If_Lparen_Rparen_Then_End:
					// <Stmt_If> ::= if '(' <Expression> ')' then <Statement> end
					break;

				case ProductionIndex.Stmt_if_If_Lparen_Rparen_Then_Else_End:
					// <Stmt_If> ::= if '(' <Expression> ')' then <Statement> else <Statement> end
					break;

				case ProductionIndex.Stmt_while_While_Lparen_Rparen_Do:
					// <Stmt_While> ::= while '(' <Expression> ')' do <Statement>
					break;

				case ProductionIndex.Stmt_repeat_Repeat_Until_Lparen_Rparen:
					// <Stmt_Repeat> ::= repeat <Statement> until '(' <Expression> ')'
					break;

				case ProductionIndex.Stmt_goto_Goto_Identifier:
					// <Stmt_Goto> ::= goto Identifier
					break;

				case ProductionIndex.Stmt_label_Identifier_Colon:
					// <Stmt_Label> ::= Identifier ':'
					break;

				case ProductionIndex.Type:
					// <Type> ::= <Type_Int>
					break;

				case ProductionIndex.Type2:
					// <Type> ::= <Type_Digit>
					break;

				case ProductionIndex.Type3:
					// <Type> ::= <Type_Char>
					break;

				case ProductionIndex.Type4:
					// <Type> ::= <Type_Bool>
					break;

				case ProductionIndex.Type5:
					// <Type> ::= <Type_Void>
					break;

				case ProductionIndex.Type6:
					// <Type> ::= <Type_IntArr>
					break;

				case ProductionIndex.Type7:
					// <Type> ::= <Type_String>
					break;

				case ProductionIndex.Type8:
					// <Type> ::= <Type_DigitArr>
					break;

				case ProductionIndex.Type9:
					// <Type> ::= <Type_BoolArr>
					break;

				case ProductionIndex.Type_int_Int:
					// <Type_Int> ::= int
					break;

				case ProductionIndex.Type_int_Integer:
					// <Type_Int> ::= integer
					break;

				case ProductionIndex.Type_char_Char:
					// <Type_Char> ::= char
					break;

				case ProductionIndex.Type_char_Character:
					// <Type_Char> ::= Character
					break;

				case ProductionIndex.Type_digit_Digit:
					// <Type_Digit> ::= digit
					break;

				case ProductionIndex.Type_bool_Bool:
					// <Type_Bool> ::= bool
					break;

				case ProductionIndex.Type_bool_Boolean:
					// <Type_Bool> ::= boolean
					break;

				case ProductionIndex.Type_void_Void:
					// <Type_Void> ::= void
					break;

				case ProductionIndex.Type_intarr_Lbracket_Rbracket:
					// <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
					break;

				case ProductionIndex.Type_string_Lbracket_Rbracket:
					// <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
					break;

				case ProductionIndex.Type_digitarr_Lbracket_Rbracket:
					// <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
					break;

				case ProductionIndex.Type_boolarr_Lbracket_Rbracket:
					// <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
					break;

				case ProductionIndex.Literal:
					// <Literal> ::= <Array_Literal>
					break;

				case ProductionIndex.Literal2:
					// <Literal> ::= <Value_Literal>
					break;

				case ProductionIndex.Array_literal:
					// <Array_Literal> ::= <Literal_IntArr>
					break;

				case ProductionIndex.Array_literal2:
					// <Array_Literal> ::= <Literal_String>
					break;

				case ProductionIndex.Array_literal3:
					// <Array_Literal> ::= <Literal_DigitArr>
					break;

				case ProductionIndex.Array_literal4:
					// <Array_Literal> ::= <Literal_BoolArr>
					break;

				case ProductionIndex.Value_literal:
					// <Value_Literal> ::= <Literal_Int>
					break;

				case ProductionIndex.Value_literal2:
					// <Value_Literal> ::= <Literal_Char>
					break;

				case ProductionIndex.Value_literal3:
					// <Value_Literal> ::= <Literal_Bool>
					break;

				case ProductionIndex.Value_literal4:
					// <Value_Literal> ::= <Literal_Digit>
					break;

				case ProductionIndex.Literal_int_Decliteral:
					// <Literal_Int> ::= DecLiteral
					break;

				case ProductionIndex.Literal_int_Hexliteral:
					// <Literal_Int> ::= HexLiteral
					break;

				case ProductionIndex.Literal_char_Charliteral:
					// <Literal_Char> ::= CharLiteral
					break;

				case ProductionIndex.Literal_bool_True:
					// <Literal_Bool> ::= true
					break;

				case ProductionIndex.Literal_bool_False:
					// <Literal_Bool> ::= false
					break;

				case ProductionIndex.Literal_digit_Digitliteral:
					// <Literal_Digit> ::= DigitLiteral
					break;

				case ProductionIndex.Literal_intarr_Lbrace_Rbrace:
					// <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
					break;

				case ProductionIndex.Literal_string_Lbrace_Rbrace:
					// <Literal_String> ::= '{' <Literal_Char_List> '}'
					break;

				case ProductionIndex.Literal_string_Stringliteral:
					// <Literal_String> ::= StringLiteral
					break;

				case ProductionIndex.Literal_digitarr_Lbrace_Rbrace:
					// <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
					break;

				case ProductionIndex.Literal_boolarr_Lbrace_Rbrace:
					// <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
					break;

				case ProductionIndex.Literal_int_list_Comma:
					// <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
					break;

				case ProductionIndex.Literal_int_list:
					// <Literal_Int_List> ::= <Literal_Int>
					break;

				case ProductionIndex.Literal_char_list_Comma:
					// <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
					break;

				case ProductionIndex.Literal_char_list:
					// <Literal_Char_List> ::= <Literal_Char>
					break;

				case ProductionIndex.Literal_digit_list_Comma:
					// <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
					break;

				case ProductionIndex.Literal_digit_list:
					// <Literal_Digit_List> ::= <Literal_Digit>
					break;

				case ProductionIndex.Literal_bool_list_Comma:
					// <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
					break;

				case ProductionIndex.Literal_bool_list:
					// <Literal_Bool_List> ::= <Literal_Bool>
					break;

				case ProductionIndex.Expression:
					// <Expression> ::= <Expr Eq>
					break;

				case ProductionIndex.Expreq_Eqeq:
					// <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
					break;

				case ProductionIndex.Expreq_Exclameq:
					// <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
					break;

				case ProductionIndex.Expreq:
					// <Expr Eq> ::= <Exp Comp>
					break;

				case ProductionIndex.Expcomp_Lt:
					// <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
					break;

				case ProductionIndex.Expcomp_Gt:
					// <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
					break;

				case ProductionIndex.Expcomp_Lteq:
					// <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
					break;

				case ProductionIndex.Expcomp_Gteq:
					// <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
					break;

				case ProductionIndex.Expcomp:
					// <Exp Comp> ::= <Exp Add>
					break;

				case ProductionIndex.Expadd_Plus:
					// <Exp Add> ::= <Exp Add> '+' <Exp Mult>
					break;

				case ProductionIndex.Expadd_Minus:
					// <Exp Add> ::= <Exp Add> '-' <Exp Mult>
					break;

				case ProductionIndex.Expadd:
					// <Exp Add> ::= <Exp Mult>
					break;

				case ProductionIndex.Expmult_Times:
					// <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
					break;

				case ProductionIndex.Expmult_Div:
					// <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
					break;

				case ProductionIndex.Expmult_Percent:
					// <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
					break;

				case ProductionIndex.Expmult:
					// <Exp Mult> ::= <Exp Unary>
					break;

				case ProductionIndex.Expunary_Exclam:
					// <Exp Unary> ::= '!' <Value>
					break;

				case ProductionIndex.Expunary_Minus:
					// <Exp Unary> ::= '-' <Value>
					break;

				case ProductionIndex.Expunary_Lparen_Rparen:
					// <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
					break;

				case ProductionIndex.Expunary:
					// <Exp Unary> ::= <Value>
					break;

				case ProductionIndex.Value:
					// <Value> ::= <Value_Literal>
					break;

				case ProductionIndex.Value_Rand:
					// <Value> ::= rand
					break;

				case ProductionIndex.Value2:
					// <Value> ::= <ValuePointer>
					break;

				case ProductionIndex.Value_Identifier_Lparen_Rparen:
					// <Value> ::= Identifier '(' <ExpressionList> ')'
					break;

				case ProductionIndex.Value_Identifier_Lparen_Rparen2:
					// <Value> ::= Identifier '(' ')'
					break;

				case ProductionIndex.Value_Lparen_Rparen:
					// <Value> ::= '(' <Expression> ')'
					break;

				case ProductionIndex.Expressionlist_Comma:
					// <ExpressionList> ::= <ExpressionList> ',' <Expression>
					break;

				case ProductionIndex.Expressionlist:
					// <ExpressionList> ::= <Expression>
					break;

				case ProductionIndex.Valuepointer_Identifier:
					// <ValuePointer> ::= Identifier
					break;

				case ProductionIndex.Valuepointer_Identifier_Lbracket_Rbracket:
					// <ValuePointer> ::= Identifier '[' <Expression> ']'
					break;

			}  //switch

			return result;
		}
	}

}
