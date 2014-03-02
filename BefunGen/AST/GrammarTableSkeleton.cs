
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
		@Elsif = 48,                               // elsif
		@End = 49,                                 // end
		@False = 50,                               // false
		@Goto = 51,                                // goto
		@Hexliteral = 52,                          // HexLiteral
		@Identifier = 53,                          // Identifier
		@If = 54,                                  // if
		@In = 55,                                  // in
		@Int = 56,                                 // int
		@Integer = 57,                             // integer
		@Out = 58,                                 // out
		@Program = 59,                             // program
		@Quit = 60,                                // quit
		@Rand = 61,                                // rand
		@Repeat = 62,                              // repeat
		@Return = 63,                              // return
		@Stop = 64,                                // stop
		@Stringliteral = 65,                       // StringLiteral
		@Then = 66,                                // then
		@True = 67,                                // true
		@Until = 68,                               // until
		@Var = 69,                                 // var
		@Void = 70,                                // void
		@While = 71,                               // while
		@Array_literal = 72,                       // <Array_Literal>
		@Expadd = 73,                              // <Exp Add>
		@Expcomp = 74,                             // <Exp Comp>
		@Expmult = 75,                             // <Exp Mult>
		@Expunary = 76,                            // <Exp Unary>
		@Exprbool = 77,                            // <Expr Bool>
		@Expreq = 78,                              // <Expr Eq>
		@Expression = 79,                          // <Expression>
		@Expressionlist = 80,                      // <ExpressionList>
		@Footer = 81,                              // <Footer>
		@Header = 82,                              // <Header>
		@Literal = 83,                             // <Literal>
		@Literal_bool = 84,                        // <Literal_Bool>
		@Literal_bool_list = 85,                   // <Literal_Bool_List>
		@Literal_boolarr = 86,                     // <Literal_BoolArr>
		@Literal_char = 87,                        // <Literal_Char>
		@Literal_char_list = 88,                   // <Literal_Char_List>
		@Literal_digit = 89,                       // <Literal_Digit>
		@Literal_digit_list = 90,                  // <Literal_Digit_List>
		@Literal_digitarr = 91,                    // <Literal_DigitArr>
		@Literal_int = 92,                         // <Literal_Int>
		@Literal_int_list = 93,                    // <Literal_Int_List>
		@Literal_intarr = 94,                      // <Literal_IntArr>
		@Literal_string = 95,                      // <Literal_String>
		@Mainstatements = 96,                      // <MainStatements>
		@Method = 97,                              // <Method>
		@Methodbody = 98,                          // <MethodBody>
		@Methodheader = 99,                        // <MethodHeader>
		@Methodlist = 100,                         // <MethodList>
		@Param = 101,                              // <Param>
		@Paramdecl = 102,                          // <ParamDecl>
		@Paramlist = 103,                          // <ParamList>
		@Program2 = 104,                           // <Program>
		@Statement = 105,                          // <Statement>
		@Statementlist = 106,                      // <StatementList>
		@Stmt_assignment = 107,                    // <Stmt_Assignment>
		@Stmt_call = 108,                          // <Stmt_Call>
		@Stmt_elseiflist = 109,                    // <Stmt_ElseIfList>
		@Stmt_goto = 110,                          // <Stmt_Goto>
		@Stmt_if = 111,                            // <Stmt_If>
		@Stmt_in = 112,                            // <Stmt_In>
		@Stmt_inc = 113,                           // <Stmt_Inc>
		@Stmt_label = 114,                         // <Stmt_Label>
		@Stmt_out = 115,                           // <Stmt_Out>
		@Stmt_quit = 116,                          // <Stmt_Quit>
		@Stmt_repeat = 117,                        // <Stmt_Repeat>
		@Stmt_return = 118,                        // <Stmt_Return>
		@Stmt_while = 119,                         // <Stmt_While>
		@Type = 120,                               // <Type>
		@Type_bool = 121,                          // <Type_Bool>
		@Type_boolarr = 122,                       // <Type_BoolArr>
		@Type_char = 123,                          // <Type_Char>
		@Type_digit = 124,                         // <Type_Digit>
		@Type_digitarr = 125,                      // <Type_DigitArr>
		@Type_int = 126,                           // <Type_Int>
		@Type_intarr = 127,                        // <Type_IntArr>
		@Type_string = 128,                        // <Type_String>
		@Type_void = 129,                          // <Type_Void>
		@Value = 130,                              // <Value>
		@Value_literal = 131,                      // <Value_Literal>
		@Valuepointer = 132,                       // <ValuePointer>
		@Vardecl = 133,                            // <VarDecl>
		@Vardeclbody = 134,                        // <VarDeclBody>
		@Varlist = 135                             // <VarList>
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
		@Stmt_if_If_Lparen_Rparen_Then_End = 48,   // <Stmt_If> ::= if '(' <Expression> ')' then <StatementList> <Stmt_ElseIfList> end
		@Stmt_elseiflist_Elsif_Lparen_Rparen_Then = 49,  // <Stmt_ElseIfList> ::= elsif '(' <Expression> ')' then <StatementList> <Stmt_ElseIfList>
		@Stmt_elseiflist_Else = 50,                // <Stmt_ElseIfList> ::= else <StatementList>
		@Stmt_elseiflist = 51,                     // <Stmt_ElseIfList> ::= 
		@Stmt_while_While_Lparen_Rparen_Do_End = 52,  // <Stmt_While> ::= while '(' <<Expression>> ')' do <StatementList> end
		@Stmt_repeat_Repeat_Until_Lparen_Rparen = 53,  // <Stmt_Repeat> ::= repeat <Statement> until '(' <Expression> ')'
		@Stmt_goto_Goto_Identifier = 54,           // <Stmt_Goto> ::= goto Identifier
		@Stmt_label_Identifier_Colon = 55,         // <Stmt_Label> ::= Identifier ':'
		@Type = 56,                                // <Type> ::= <Type_Int>
		@Type2 = 57,                               // <Type> ::= <Type_Digit>
		@Type3 = 58,                               // <Type> ::= <Type_Char>
		@Type4 = 59,                               // <Type> ::= <Type_Bool>
		@Type5 = 60,                               // <Type> ::= <Type_Void>
		@Type6 = 61,                               // <Type> ::= <Type_IntArr>
		@Type7 = 62,                               // <Type> ::= <Type_String>
		@Type8 = 63,                               // <Type> ::= <Type_DigitArr>
		@Type9 = 64,                               // <Type> ::= <Type_BoolArr>
		@Type_int_Int = 65,                        // <Type_Int> ::= int
		@Type_int_Integer = 66,                    // <Type_Int> ::= integer
		@Type_char_Char = 67,                      // <Type_Char> ::= char
		@Type_char_Character = 68,                 // <Type_Char> ::= Character
		@Type_digit_Digit = 69,                    // <Type_Digit> ::= digit
		@Type_bool_Bool = 70,                      // <Type_Bool> ::= bool
		@Type_bool_Boolean = 71,                   // <Type_Bool> ::= boolean
		@Type_void_Void = 72,                      // <Type_Void> ::= void
		@Type_intarr_Lbracket_Rbracket = 73,       // <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
		@Type_string_Lbracket_Rbracket = 74,       // <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
		@Type_digitarr_Lbracket_Rbracket = 75,     // <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
		@Type_boolarr_Lbracket_Rbracket = 76,      // <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
		@Literal = 77,                             // <Literal> ::= <Array_Literal>
		@Literal2 = 78,                            // <Literal> ::= <Value_Literal>
		@Array_literal = 79,                       // <Array_Literal> ::= <Literal_IntArr>
		@Array_literal2 = 80,                      // <Array_Literal> ::= <Literal_String>
		@Array_literal3 = 81,                      // <Array_Literal> ::= <Literal_DigitArr>
		@Array_literal4 = 82,                      // <Array_Literal> ::= <Literal_BoolArr>
		@Value_literal = 83,                       // <Value_Literal> ::= <Literal_Int>
		@Value_literal2 = 84,                      // <Value_Literal> ::= <Literal_Char>
		@Value_literal3 = 85,                      // <Value_Literal> ::= <Literal_Bool>
		@Value_literal4 = 86,                      // <Value_Literal> ::= <Literal_Digit>
		@Literal_int_Decliteral = 87,              // <Literal_Int> ::= DecLiteral
		@Literal_int_Hexliteral = 88,              // <Literal_Int> ::= HexLiteral
		@Literal_char_Charliteral = 89,            // <Literal_Char> ::= CharLiteral
		@Literal_bool_True = 90,                   // <Literal_Bool> ::= true
		@Literal_bool_False = 91,                  // <Literal_Bool> ::= false
		@Literal_digit_Digitliteral = 92,          // <Literal_Digit> ::= DigitLiteral
		@Literal_intarr_Lbrace_Rbrace = 93,        // <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
		@Literal_string_Lbrace_Rbrace = 94,        // <Literal_String> ::= '{' <Literal_Char_List> '}'
		@Literal_string_Stringliteral = 95,        // <Literal_String> ::= StringLiteral
		@Literal_digitarr_Lbrace_Rbrace = 96,      // <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
		@Literal_boolarr_Lbrace_Rbrace = 97,       // <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
		@Literal_int_list_Comma = 98,              // <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
		@Literal_int_list = 99,                    // <Literal_Int_List> ::= <Literal_Int>
		@Literal_char_list_Comma = 100,            // <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
		@Literal_char_list = 101,                  // <Literal_Char_List> ::= <Literal_Char>
		@Literal_digit_list_Comma = 102,           // <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
		@Literal_digit_list = 103,                 // <Literal_Digit_List> ::= <Literal_Digit>
		@Literal_bool_list_Comma = 104,            // <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
		@Literal_bool_list = 105,                  // <Literal_Bool_List> ::= <Literal_Bool>
		@Expression = 106,                         // <Expression> ::= <Expr Bool>
		@Exprbool_Ampamp = 107,                    // <Expr Bool> ::= <Expr Bool> '&&' <Expr Eq>
		@Exprbool_Pipepipe = 108,                  // <Expr Bool> ::= <Expr Bool> '||' <Expr Eq>
		@Exprbool_Caret = 109,                     // <Expr Bool> ::= <Expr Bool> '^' <Expr Eq>
		@Exprbool = 110,                           // <Expr Bool> ::= <Expr Eq>
		@Expreq_Eqeq = 111,                        // <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
		@Expreq_Exclameq = 112,                    // <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
		@Expreq = 113,                             // <Expr Eq> ::= <Exp Comp>
		@Expcomp_Lt = 114,                         // <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
		@Expcomp_Gt = 115,                         // <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
		@Expcomp_Lteq = 116,                       // <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
		@Expcomp_Gteq = 117,                       // <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
		@Expcomp = 118,                            // <Exp Comp> ::= <Exp Add>
		@Expadd_Plus = 119,                        // <Exp Add> ::= <Exp Add> '+' <Exp Mult>
		@Expadd_Minus = 120,                       // <Exp Add> ::= <Exp Add> '-' <Exp Mult>
		@Expadd = 121,                             // <Exp Add> ::= <Exp Mult>
		@Expmult_Times = 122,                      // <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
		@Expmult_Div = 123,                        // <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
		@Expmult_Percent = 124,                    // <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
		@Expmult = 125,                            // <Exp Mult> ::= <Exp Unary>
		@Expunary_Exclam = 126,                    // <Exp Unary> ::= '!' <Value>
		@Expunary_Minus = 127,                     // <Exp Unary> ::= '-' <Value>
		@Expunary_Lparen_Rparen = 128,             // <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
		@Expunary = 129,                           // <Exp Unary> ::= <Value>
		@Value = 130,                              // <Value> ::= <Value_Literal>
		@Value_Rand = 131,                         // <Value> ::= rand
		@Value2 = 132,                             // <Value> ::= <ValuePointer>
		@Value_Identifier_Lparen_Rparen = 133,     // <Value> ::= Identifier '(' <ExpressionList> ')'
		@Value_Identifier_Lparen_Rparen2 = 134,    // <Value> ::= Identifier '(' ')'
		@Value_Lparen_Rparen = 135,                // <Value> ::= '(' <Expression> ')'
		@Expressionlist_Comma = 136,               // <ExpressionList> ::= <ExpressionList> ',' <Expression>
		@Expressionlist = 137,                     // <ExpressionList> ::= <Expression>
		@Valuepointer_Identifier = 138,            // <ValuePointer> ::= Identifier
		@Valuepointer_Identifier_Lbracket_Rbracket = 139   // <ValuePointer> ::= Identifier '[' <Expression> ']'
	}
}
