
namespace BefunGen.AST
{
	public enum SymbolIndex
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






















		@Ampamp = 13,                              // '&&'
		@Lparen = 14,                              // '('
		@Rparen = 15,                              // ')'
		@Times = 16,                               // '*'
		@Comma = 17,                               // ','
		@Div = 18,                                 // '/'
		@Colon = 19,                               // ':'
		@Coloneq = 20,                             // ':='
		@Semi = 21,                                // ';'
		@Lbracket = 22,                            // '['
		@Rbracket = 23,                            // ']'
		@Caret = 24,                               // '^'
		@Lbrace = 25,                              // '{'
		@Pipepipe = 26,                            // '||'
		@Rbrace = 27,                              // '}'
		@Plus = 28,                                // '+'
		@Plusplus = 29,                            // '++'
		@Lt = 30,                                  // '<'
		@Lteq = 31,                                // '<='
		@Eq = 32,                                  // '='
		@Eqeq = 33,                                // '=='
		@Gt = 34,                                  // '>'
		@Gteq = 35,                                // '>='
		@Begin = 36,                               // begin
		@Bool = 37,                                // bool
		@Boolean = 38,                             // boolean

		@Char = 39,                                // char
		@Character = 40,                           // Character
		@Charliteral = 41,                         // CharLiteral

		@Close = 42,                               // close
		@Decliteral = 43,                          // DecLiteral

		@Digit = 44,                               // digit
		@Digitliteral = 45,                        // DigitLiteral





		@Do = 46,                                  // do
		@Else = 47,                                // else
		@End = 48,                                 // end
		@False = 49,                               // false
		@Goto = 50,                                // goto
		@Hexliteral = 51,                          // HexLiteral
		@Identifier = 52,                          // Identifier



		@If = 53,                                  // if
		@In = 54,                                  // in
		@Int = 55,                                 // int
		@Integer = 56,                             // integer

		@Out = 57,                                 // out
		@Program = 58,                             // program





		@Quit = 59,                                // quit
		@Rand = 60,                                // rand
		@Repeat = 61,                              // repeat
		@Return = 62,                              // return
		@Stop = 63,                                // stop
		@Stringliteral = 64,                       // StringLiteral






		@Then = 65,                                // then
		@True = 66,                                // true
		@Until = 67,                               // until
		@Var = 68,                                 // var
		@Void = 69,                                // void
		@While = 70,                               // while
		@Array_literal = 71,                       // <Array_Literal>

		@Expadd = 72,                              // <Exp Add>
		@Expcomp = 73,                             // <Exp Comp>
		@Expmult = 74,                             // <Exp Mult>
		@Expunary = 75,                            // <Exp Unary>

		@Exprbool = 76,                            // <Expr Bool>
		@Expreq = 77,                              // <Expr Eq>
		@Expression = 78,                          // <Expression>
		@Expressionlist = 79,                      // <ExpressionList>
		@Footer = 80,                              // <Footer>
		@Header = 81,                              // <Header>
		@Literal = 82,                             // <Literal>
		@Literal_bool = 83,                        // <Literal_Bool>
		@Literal_bool_list = 84,                   // <Literal_Bool_List>
		@Literal_boolarr = 85,                     // <Literal_BoolArr>
		@Literal_char = 86,                        // <Literal_Char>
		@Literal_char_list = 87,                   // <Literal_Char_List>
		@Literal_digit = 88,                       // <Literal_Digit>
		@Literal_digit_list = 89,                  // <Literal_Digit_List>
		@Literal_digitarr = 90,                    // <Literal_DigitArr>
		@Literal_int = 91,                         // <Literal_Int>
		@Literal_int_list = 92,                    // <Literal_Int_List>
		@Literal_intarr = 93,                      // <Literal_IntArr>
		@Literal_string = 94,                      // <Literal_String>
		@Mainstatements = 95,                      // <MainStatements>
		@Method = 96,                              // <Method>
		@Methodbody = 97,                          // <MethodBody>
		@Methodheader = 98,                        // <MethodHeader>
		@Methodlist = 99,                          // <MethodList>

		@Param = 100,                              // <Param>
		@Paramdecl = 101,                          // <ParamDecl>
		@Paramlist = 102,                          // <ParamList>
		@Program2 = 103,                           // <Program>
		@Statement = 104,                          // <Statement>
		@Statementlist = 105,                      // <StatementList>
		@Stmt_assignment = 106,                    // <Stmt_Assignment>

		@Stmt_call = 107,                          // <Stmt_Call>
		@Stmt_goto = 108,                          // <Stmt_Goto>
		@Stmt_if = 109,                            // <Stmt_If>
		@Stmt_in = 110,                            // <Stmt_In>
		@Stmt_inc = 111,                           // <Stmt_Inc>
		@Stmt_label = 112,                         // <Stmt_Label>
		@Stmt_out = 113,                           // <Stmt_Out>
		@Stmt_quit = 114,                          // <Stmt_Quit>
		@Stmt_repeat = 115,                        // <Stmt_Repeat>
		@Stmt_return = 116,                        // <Stmt_Return>
		@Stmt_while = 117,                         // <Stmt_While>

		@Type = 118,                               // <Type>
		@Type_bool = 119,                          // <Type_Bool>
		@Type_boolarr = 120,                       // <Type_BoolArr>
		@Type_char = 121,                          // <Type_Char>
		@Type_digit = 122,                         // <Type_Digit>
		@Type_digitarr = 123,                      // <Type_DigitArr>
		@Type_int = 124,                           // <Type_Int>
		@Type_intarr = 125,                        // <Type_IntArr>
		@Type_string = 126,                        // <Type_String>
		@Type_void = 127,                          // <Type_Void>

		@Value = 128,                              // <Value>
		@Value_literal = 129,                      // <Value_Literal>
		@Valuepointer = 130,                       // <ValuePointer>
		@Vardecl = 131,                            // <VarDecl>
		@Vardeclbody = 132,                        // <VarDeclBody>
		@Varlist = 133                             // <VarList>
	}

	public enum ProductionIndex
	{
		@Program = 0,                              // <Program> ::= <Header> <MainStatements> <MethodList> <Footer>
		@Header_Program_Identifier = 1,            // <Header> ::= program Identifier
		@Footer_End = 2,                           // <Footer> ::= end
		@Methodlist = 3,                           // <MethodList> ::= <MethodList> <Method>
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
		@Statement6 = 32,                          // <Statement> ::= <Stmt_Call> ';'
		@Statementlist = 33,                       // <StatementList> ::= <StatementList> <Statement>
		@Statementlist2 = 34,                      // <StatementList> ::=
		@Stmt_inc_Plusplus = 35,                   // <Stmt_Inc> ::= <ValuePointer> '++'
		@Stmt_inc_Minusminus = 36,                 // <Stmt_Inc> ::= <ValuePointer> '--'
		@Stmt_quit_Quit = 37,                      // <Stmt_Quit> ::= quit
		@Stmt_quit_Stop = 38,                      // <Stmt_Quit> ::= stop
		@Stmt_quit_Close = 39,                     // <Stmt_Quit> ::= close
		@Stmt_out_Out = 40,                        // <Stmt_Out> ::= out <Expression>
		@Stmt_out_Out2 = 41,                       // <Stmt_Out> ::= out <Literal_String>
		@Stmt_in_In = 42,                          // <Stmt_In> ::= in <ValuePointer>
		@Stmt_assignment_Eq = 43,                  // <Stmt_Assignment> ::= <ValuePointer> '=' <Expression>
		@Stmt_return_Return = 44,                  // <Stmt_Return> ::= return <Expression>
		@Stmt_return_Return2 = 45,                 // <Stmt_Return> ::= return
		@Stmt_call_Identifier_Lparen_Rparen = 46,  // <Stmt_Call> ::= Identifier '(' <ExpressionList> ')'
		@Stmt_call_Identifier_Lparen_Rparen2 = 47,  // <Stmt_Call> ::= Identifier '(' ')'
		@Stmt_if_If_Lparen_Rparen_Then_End = 48,   // <Stmt_If> ::= if '(' <Expression> ')' then <Statement> end
		@Stmt_if_If_Lparen_Rparen_Then_Else_End = 49,  // <Stmt_If> ::= if '(' <Expression> ')' then <Statement> else <Statement> end
		@Stmt_while_While_Lparen_Rparen_Do = 50,   // <Stmt_While> ::= while '(' <Expression> ')' do <Statement>
		@Stmt_repeat_Repeat_Until_Lparen_Rparen = 51,  // <Stmt_Repeat> ::= repeat <Statement> until '(' <Expression> ')'
		@Stmt_goto_Goto_Identifier = 52,           // <Stmt_Goto> ::= goto Identifier
		@Stmt_label_Identifier_Colon = 53,         // <Stmt_Label> ::= Identifier ':'
		@Type = 54,                                // <Type> ::= <Type_Int>
		@Type2 = 55,                               // <Type> ::= <Type_Digit>
		@Type3 = 56,                               // <Type> ::= <Type_Char>
		@Type4 = 57,                               // <Type> ::= <Type_Bool>
		@Type5 = 58,                               // <Type> ::= <Type_Void>
		@Type6 = 59,                               // <Type> ::= <Type_IntArr>
		@Type7 = 60,                               // <Type> ::= <Type_String>
		@Type8 = 61,                               // <Type> ::= <Type_DigitArr>
		@Type9 = 62,                               // <Type> ::= <Type_BoolArr>
		@Type_int_Int = 63,                        // <Type_Int> ::= int
		@Type_int_Integer = 64,                    // <Type_Int> ::= integer
		@Type_char_Char = 65,                      // <Type_Char> ::= char
		@Type_char_Character = 66,                 // <Type_Char> ::= Character
		@Type_digit_Digit = 67,                    // <Type_Digit> ::= digit
		@Type_bool_Bool = 68,                      // <Type_Bool> ::= bool
		@Type_bool_Boolean = 69,                   // <Type_Bool> ::= boolean
		@Type_void_Void = 70,                      // <Type_Void> ::= void
		@Type_intarr_Lbracket_Rbracket = 71,       // <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
		@Type_string_Lbracket_Rbracket = 72,       // <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
		@Type_digitarr_Lbracket_Rbracket = 73,     // <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
		@Type_boolarr_Lbracket_Rbracket = 74,      // <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
		@Literal = 75,                             // <Literal> ::= <Array_Literal>
		@Literal2 = 76,                            // <Literal> ::= <Value_Literal>
		@Array_literal = 77,                       // <Array_Literal> ::= <Literal_IntArr>
		@Array_literal2 = 78,                      // <Array_Literal> ::= <Literal_String>
		@Array_literal3 = 79,                      // <Array_Literal> ::= <Literal_DigitArr>
		@Array_literal4 = 80,                      // <Array_Literal> ::= <Literal_BoolArr>
		@Value_literal = 81,                       // <Value_Literal> ::= <Literal_Int>
		@Value_literal2 = 82,                      // <Value_Literal> ::= <Literal_Char>
		@Value_literal3 = 83,                      // <Value_Literal> ::= <Literal_Bool>
		@Value_literal4 = 84,                      // <Value_Literal> ::= <Literal_Digit>
		@Literal_int_Decliteral = 85,              // <Literal_Int> ::= DecLiteral
		@Literal_int_Hexliteral = 86,              // <Literal_Int> ::= HexLiteral
		@Literal_char_Charliteral = 87,            // <Literal_Char> ::= CharLiteral
		@Literal_bool_True = 88,                   // <Literal_Bool> ::= true
		@Literal_bool_False = 89,                  // <Literal_Bool> ::= false
		@Literal_digit_Digitliteral = 90,          // <Literal_Digit> ::= DigitLiteral
		@Literal_intarr_Lbrace_Rbrace = 91,        // <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
		@Literal_string_Lbrace_Rbrace = 92,        // <Literal_String> ::= '{' <Literal_Char_List> '}'
		@Literal_string_Stringliteral = 93,        // <Literal_String> ::= StringLiteral
		@Literal_digitarr_Lbrace_Rbrace = 94,      // <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
		@Literal_boolarr_Lbrace_Rbrace = 95,       // <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
		@Literal_int_list_Comma = 96,              // <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
		@Literal_int_list = 97,                    // <Literal_Int_List> ::= <Literal_Int>
		@Literal_char_list_Comma = 98,             // <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
		@Literal_char_list = 99,                   // <Literal_Char_List> ::= <Literal_Char>
		@Literal_digit_list_Comma = 100,           // <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
		@Literal_digit_list = 101,                 // <Literal_Digit_List> ::= <Literal_Digit>
		@Literal_bool_list_Comma = 102,            // <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
		@Literal_bool_list = 103,                  // <Literal_Bool_List> ::= <Literal_Bool>
		@Expression = 104,                         // <Expression> ::= <Expr Bool>
		@Exprbool_Ampamp = 105,                    // <Expr Bool> ::= <Expr Bool> '&&' <Expr Eq>
		@Exprbool_Pipepipe = 106,                  // <Expr Bool> ::= <Expr Bool> '||' <Expr Eq>
		@Exprbool_Caret = 107,                     // <Expr Bool> ::= <Expr Bool> '^' <Expr Eq>
		@Exprbool = 108,                           // <Expr Bool> ::= <Expr Eq>
		@Expreq_Eqeq = 109,                        // <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
		@Expreq_Exclameq = 110,                    // <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
		@Expreq = 111,                             // <Expr Eq> ::= <Exp Comp>
		@Expcomp_Lt = 112,                         // <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
		@Expcomp_Gt = 113,                         // <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
		@Expcomp_Lteq = 114,                       // <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
		@Expcomp_Gteq = 115,                       // <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
		@Expcomp = 116,                            // <Exp Comp> ::= <Exp Add>
		@Expadd_Plus = 117,                        // <Exp Add> ::= <Exp Add> '+' <Exp Mult>
		@Expadd_Minus = 118,                       // <Exp Add> ::= <Exp Add> '-' <Exp Mult>
		@Expadd = 119,                             // <Exp Add> ::= <Exp Mult>
		@Expmult_Times = 120,                      // <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
		@Expmult_Div = 121,                        // <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
		@Expmult_Percent = 122,                    // <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
		@Expmult = 123,                            // <Exp Mult> ::= <Exp Unary>
		@Expunary_Exclam = 124,                    // <Exp Unary> ::= '!' <Value>
		@Expunary_Minus = 125,                     // <Exp Unary> ::= '-' <Value>
		@Expunary_Lparen_Rparen = 126,             // <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
		@Expunary = 127,                           // <Exp Unary> ::= <Value>
		@Value = 128,                              // <Value> ::= <Value_Literal>
		@Value_Rand = 129,                         // <Value> ::= rand
		@Value2 = 130,                             // <Value> ::= <ValuePointer>
		@Value_Identifier_Lparen_Rparen = 131,     // <Value> ::= Identifier '(' <ExpressionList> ')'
		@Value_Identifier_Lparen_Rparen2 = 132,    // <Value> ::= Identifier '(' ')'
		@Value_Lparen_Rparen = 133,                // <Value> ::= '(' <Expression> ')'
		@Expressionlist_Comma = 134,               // <ExpressionList> ::= <ExpressionList> ',' <Expression>
		@Expressionlist = 135,                     // <ExpressionList> ::= <Expression>
		@Valuepointer_Identifier = 136,            // <ValuePointer> ::= Identifier
		@Valuepointer_Identifier_Lbracket_Rbracket = 137   // <ValuePointer> ::= Identifier '[' <Expression> ']'
	}
}
