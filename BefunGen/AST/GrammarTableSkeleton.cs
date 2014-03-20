
namespace BefunGen.AST
{
	enum SymbolIndex
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
		@Const = 43,                               // const
		@Decliteral = 44,                          // DecLiteral
		@Digit = 45,                               // digit
		@Digitliteral = 46,                        // DigitLiteral
		@Display = 47,                             // display
		@Do = 48,                                  // do
		@Else = 49,                                // else
		@Elsif = 50,                               // elsif
		@End = 51,                                 // end
		@False = 52,                               // false
		@For = 53,                                 // for
		@Global = 54,                              // global
		@Goto = 55,                                // goto
		@Hexliteral = 56,                          // HexLiteral
		@Identifier = 57,                          // Identifier
		@If = 58,                                  // if
		@In = 59,                                  // in
		@Int = 60,                                 // int
		@Integer = 61,                             // integer
		@Out = 62,                                 // out
		@Program = 63,                             // program
		@Quit = 64,                                // quit
		@Rand = 65,                                // rand
		@Repeat = 66,                              // repeat
		@Return = 67,                              // return
		@Stop = 68,                                // stop
		@Stringliteral = 69,                       // StringLiteral
		@Then = 70,                                // then
		@True = 71,                                // true
		@Until = 72,                               // until
		@Var = 73,                                 // var
		@Void = 74,                                // void
		@While = 75,                               // while
		@Array_literal = 76,                       // <Array_Literal>
		@Constants = 77,                           // <Constants>
		@Expadd = 78,                              // <Exp Add>
		@Expcomp = 79,                             // <Exp Comp>
		@Expmult = 80,                             // <Exp Mult>
		@Expunary = 81,                            // <Exp Unary>
		@Exprbool = 82,                            // <Expr Bool>
		@Expreq = 83,                              // <Expr Eq>
		@Expression = 84,                          // <Expression>
		@Expressionlist = 85,                      // <ExpressionList>
		@Footer = 86,                              // <Footer>
		@Globalvars = 87,                          // <GlobalVars>
		@Header = 88,                              // <Header>
		@Literal = 89,                             // <Literal>
		@Literal_bool = 90,                        // <Literal_Bool>
		@Literal_bool_list = 91,                   // <Literal_Bool_List>
		@Literal_boolarr = 92,                     // <Literal_BoolArr>
		@Literal_char = 93,                        // <Literal_Char>
		@Literal_char_list = 94,                   // <Literal_Char_List>
		@Literal_digit = 95,                       // <Literal_Digit>
		@Literal_digit_list = 96,                  // <Literal_Digit_List>
		@Literal_digitarr = 97,                    // <Literal_DigitArr>
		@Literal_int = 98,                         // <Literal_Int>
		@Literal_int_list = 99,                    // <Literal_Int_List>
		@Literal_intarr = 100,                     // <Literal_IntArr>
		@Literal_string = 101,                     // <Literal_String>
		@Mainstatements = 102,                     // <MainStatements>
		@Method = 103,                             // <Method>
		@Methodbody = 104,                         // <MethodBody>
		@Methodheader = 105,                       // <MethodHeader>
		@Methodlist = 106,                         // <MethodList>
		@Optionalexpression = 107,                 // <OptionalExpression>
		@Optionalsimstatement = 108,               // <OptionalSimStatement>
		@Param = 109,                              // <Param>
		@Paramdecl = 110,                          // <ParamDecl>
		@Paramlist = 111,                          // <ParamList>
		@Program2 = 112,                           // <Program>
		@Simplestatement = 113,                    // <SimpleStatement>
		@Statement = 114,                          // <Statement>
		@Statementlist = 115,                      // <StatementList>
		@Stmt_assignment = 116,                    // <Stmt_Assignment>
		@Stmt_call = 117,                          // <Stmt_Call>
		@Stmt_elseiflist = 118,                    // <Stmt_ElseIfList>
		@Stmt_for = 119,                           // <Stmt_For>
		@Stmt_goto = 120,                          // <Stmt_Goto>
		@Stmt_if = 121,                            // <Stmt_If>
		@Stmt_in = 122,                            // <Stmt_In>
		@Stmt_inc = 123,                           // <Stmt_Inc>
		@Stmt_label = 124,                         // <Stmt_Label>
		@Stmt_out = 125,                           // <Stmt_Out>
		@Stmt_quit = 126,                          // <Stmt_Quit>
		@Stmt_repeat = 127,                        // <Stmt_Repeat>
		@Stmt_return = 128,                        // <Stmt_Return>
		@Stmt_while = 129,                         // <Stmt_While>
		@Type = 130,                               // <Type>
		@Type_bool = 131,                          // <Type_Bool>
		@Type_boolarr = 132,                       // <Type_BoolArr>
		@Type_char = 133,                          // <Type_Char>
		@Type_digit = 134,                         // <Type_Digit>
		@Type_digitarr = 135,                      // <Type_DigitArr>
		@Type_int = 136,                           // <Type_Int>
		@Type_intarr = 137,                        // <Type_IntArr>
		@Type_string = 138,                        // <Type_String>
		@Type_void = 139,                          // <Type_Void>
		@Value = 140,                              // <Value>
		@Value_literal = 141,                      // <Value_Literal>
		@Valuepointer = 142,                       // <ValuePointer>
		@Vardecl = 143,                            // <VarDecl>
		@Vardeclbody = 144,                        // <VarDeclBody>
		@Varlist = 145                             // <VarList>
	}

	enum ProductionIndex
	{
		@Program = 0,                              // <Program> ::= <Header> <Constants> <GlobalVars> <MainStatements> <MethodList> <Footer>
		@Header_Program_Identifier = 1,            // <Header> ::= program Identifier
		@Header_Program_Identifier_Colon_Display_Lbracket_Comma_Rbracket = 2,  // <Header> ::= program Identifier ':' display '[' <Literal_Int> ',' <Literal_Int> ']'
		@Footer_End = 3,                           // <Footer> ::= end
		@Constants_Const = 4,                      // <Constants> ::= const <VarList>
		@Constants = 5,                            // <Constants> ::= 
		@Globalvars_Global = 6,                    // <GlobalVars> ::= global <VarList>
		@Globalvars = 7,                           // <GlobalVars> ::= 
		@Methodlist = 8,                           // <MethodList> ::= <MethodList> <Method>
		@Methodlist2 = 9,                          // <MethodList> ::= 
		@Mainstatements = 10,                      // <MainStatements> ::= <MethodBody>
		@Method = 11,                              // <Method> ::= <MethodHeader> <MethodBody>
		@Methodbody = 12,                          // <MethodBody> ::= <VarDeclBody> <Statement>
		@Methodheader_Identifier_Lparen_Rparen = 13,  // <MethodHeader> ::= <Type> Identifier '(' <ParamDecl> ')'
		@Vardeclbody_Var = 14,                     // <VarDeclBody> ::= var <VarList>
		@Vardeclbody = 15,                         // <VarDeclBody> ::= 
		@Paramdecl = 16,                           // <ParamDecl> ::= <ParamList>
		@Paramdecl2 = 17,                          // <ParamDecl> ::= 
		@Paramlist_Comma = 18,                     // <ParamList> ::= <ParamList> ',' <Param>
		@Paramlist = 19,                           // <ParamList> ::= <Param>
		@Param_Identifier = 20,                    // <Param> ::= <Type> Identifier
		@Varlist_Semi = 21,                        // <VarList> ::= <VarList> <VarDecl> ';'
		@Varlist_Semi2 = 22,                       // <VarList> ::= <VarDecl> ';'
		@Vardecl_Identifier = 23,                  // <VarDecl> ::= <Type> Identifier
		@Vardecl_Identifier_Coloneq = 24,          // <VarDecl> ::= <Type> Identifier ':=' <Literal>
		@Optionalsimstatement = 25,                // <OptionalSimStatement> ::= <SimpleStatement>
		@Optionalsimstatement2 = 26,               // <OptionalSimStatement> ::= 
		@Statement_Semi = 27,                      // <Statement> ::= <SimpleStatement> ';'
		@Statement_Begin_End = 28,                 // <Statement> ::= begin <StatementList> end
		@Statement = 29,                           // <Statement> ::= <Stmt_If>
		@Statement2 = 30,                          // <Statement> ::= <Stmt_While>
		@Statement3 = 31,                          // <Statement> ::= <Stmt_For>
		@Statement4 = 32,                          // <Statement> ::= <Stmt_Repeat>
		@Statement_Semi2 = 33,                     // <Statement> ::= <Stmt_Goto> ';'
		@Statement5 = 34,                          // <Statement> ::= <Stmt_Label>
		@Simplestatement = 35,                     // <SimpleStatement> ::= <Stmt_Quit>
		@Simplestatement2 = 36,                    // <SimpleStatement> ::= <Stmt_Return>
		@Simplestatement3 = 37,                    // <SimpleStatement> ::= <Stmt_Out>
		@Simplestatement4 = 38,                    // <SimpleStatement> ::= <Stmt_In>
		@Simplestatement5 = 39,                    // <SimpleStatement> ::= <Stmt_Inc>
		@Simplestatement6 = 40,                    // <SimpleStatement> ::= <Stmt_Assignment>
		@Simplestatement7 = 41,                    // <SimpleStatement> ::= <Stmt_Call>
		@Statementlist = 42,                       // <StatementList> ::= <StatementList> <Statement>
		@Statementlist2 = 43,                      // <StatementList> ::= 
		@Stmt_inc_Plusplus = 44,                   // <Stmt_Inc> ::= <ValuePointer> '++'
		@Stmt_inc_Minusminus = 45,                 // <Stmt_Inc> ::= <ValuePointer> '--'
		@Stmt_quit_Quit = 46,                      // <Stmt_Quit> ::= quit
		@Stmt_quit_Stop = 47,                      // <Stmt_Quit> ::= stop
		@Stmt_quit_Close = 48,                     // <Stmt_Quit> ::= close
		@Stmt_out_Out = 49,                        // <Stmt_Out> ::= out <Expression>
		@Stmt_out_Out2 = 50,                       // <Stmt_Out> ::= out <Literal_String>
		@Stmt_in_In = 51,                          // <Stmt_In> ::= in <ValuePointer>
		@Stmt_assignment_Eq = 52,                  // <Stmt_Assignment> ::= <ValuePointer> '=' <Expression>
		@Stmt_return_Return = 53,                  // <Stmt_Return> ::= return <Expression>
		@Stmt_return_Return2 = 54,                 // <Stmt_Return> ::= return
		@Stmt_call_Identifier_Lparen_Rparen = 55,  // <Stmt_Call> ::= Identifier '(' <ExpressionList> ')'
		@Stmt_call_Identifier_Lparen_Rparen2 = 56,  // <Stmt_Call> ::= Identifier '(' ')'
		@Stmt_if_If_Lparen_Rparen_Then_End = 57,   // <Stmt_If> ::= if '(' <Expression> ')' then <StatementList> <Stmt_ElseIfList> end
		@Stmt_elseiflist_Elsif_Lparen_Rparen_Then = 58,  // <Stmt_ElseIfList> ::= elsif '(' <Expression> ')' then <StatementList> <Stmt_ElseIfList>
		@Stmt_elseiflist_Else = 59,                // <Stmt_ElseIfList> ::= else <StatementList>
		@Stmt_elseiflist = 60,                     // <Stmt_ElseIfList> ::= 
		@Stmt_while_While_Lparen_Rparen_Do_End = 61,  // <Stmt_While> ::= while '(' <Expression> ')' do <StatementList> end
		@Stmt_for_For_Lparen_Semi_Semi_Rparen_Do_End = 62,  // <Stmt_For> ::= for '(' <OptionalSimStatement> ';' <OptionalExpression> ';' <OptionalSimStatement> ')' do <StatementList> end
		@Stmt_repeat_Repeat_Until_Lparen_Rparen = 63,  // <Stmt_Repeat> ::= repeat <StatementList> until '(' <Expression> ')'
		@Stmt_goto_Goto_Identifier = 64,           // <Stmt_Goto> ::= goto Identifier
		@Stmt_label_Identifier_Colon = 65,         // <Stmt_Label> ::= Identifier ':'
		@Type = 66,                                // <Type> ::= <Type_Int>
		@Type2 = 67,                               // <Type> ::= <Type_Digit>
		@Type3 = 68,                               // <Type> ::= <Type_Char>
		@Type4 = 69,                               // <Type> ::= <Type_Bool>
		@Type5 = 70,                               // <Type> ::= <Type_Void>
		@Type6 = 71,                               // <Type> ::= <Type_IntArr>
		@Type7 = 72,                               // <Type> ::= <Type_String>
		@Type8 = 73,                               // <Type> ::= <Type_DigitArr>
		@Type9 = 74,                               // <Type> ::= <Type_BoolArr>
		@Type_int_Int = 75,                        // <Type_Int> ::= int
		@Type_int_Integer = 76,                    // <Type_Int> ::= integer
		@Type_char_Char = 77,                      // <Type_Char> ::= char
		@Type_char_Character = 78,                 // <Type_Char> ::= Character
		@Type_digit_Digit = 79,                    // <Type_Digit> ::= digit
		@Type_bool_Bool = 80,                      // <Type_Bool> ::= bool
		@Type_bool_Boolean = 81,                   // <Type_Bool> ::= boolean
		@Type_void_Void = 82,                      // <Type_Void> ::= void
		@Type_intarr_Lbracket_Rbracket = 83,       // <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
		@Type_string_Lbracket_Rbracket = 84,       // <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
		@Type_digitarr_Lbracket_Rbracket = 85,     // <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
		@Type_boolarr_Lbracket_Rbracket = 86,      // <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
		@Literal = 87,                             // <Literal> ::= <Array_Literal>
		@Literal2 = 88,                            // <Literal> ::= <Value_Literal>
		@Array_literal = 89,                       // <Array_Literal> ::= <Literal_IntArr>
		@Array_literal2 = 90,                      // <Array_Literal> ::= <Literal_String>
		@Array_literal3 = 91,                      // <Array_Literal> ::= <Literal_DigitArr>
		@Array_literal4 = 92,                      // <Array_Literal> ::= <Literal_BoolArr>
		@Value_literal = 93,                       // <Value_Literal> ::= <Literal_Int>
		@Value_literal2 = 94,                      // <Value_Literal> ::= <Literal_Char>
		@Value_literal3 = 95,                      // <Value_Literal> ::= <Literal_Bool>
		@Value_literal4 = 96,                      // <Value_Literal> ::= <Literal_Digit>
		@Literal_int_Decliteral = 97,              // <Literal_Int> ::= DecLiteral
		@Literal_int_Hexliteral = 98,              // <Literal_Int> ::= HexLiteral
		@Literal_char_Charliteral = 99,            // <Literal_Char> ::= CharLiteral
		@Literal_bool_True = 100,                  // <Literal_Bool> ::= true
		@Literal_bool_False = 101,                 // <Literal_Bool> ::= false
		@Literal_digit_Digitliteral = 102,         // <Literal_Digit> ::= DigitLiteral
		@Literal_intarr_Lbrace_Rbrace = 103,       // <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
		@Literal_string_Lbrace_Rbrace = 104,       // <Literal_String> ::= '{' <Literal_Char_List> '}'
		@Literal_string_Stringliteral = 105,       // <Literal_String> ::= StringLiteral
		@Literal_digitarr_Lbrace_Rbrace = 106,     // <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
		@Literal_boolarr_Lbrace_Rbrace = 107,      // <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
		@Literal_int_list_Comma = 108,             // <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
		@Literal_int_list = 109,                   // <Literal_Int_List> ::= <Literal_Int>
		@Literal_char_list_Comma = 110,            // <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
		@Literal_char_list = 111,                  // <Literal_Char_List> ::= <Literal_Char>
		@Literal_digit_list_Comma = 112,           // <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
		@Literal_digit_list = 113,                 // <Literal_Digit_List> ::= <Literal_Digit>
		@Literal_bool_list_Comma = 114,            // <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
		@Literal_bool_list = 115,                  // <Literal_Bool_List> ::= <Literal_Bool>
		@Optionalexpression = 116,                 // <OptionalExpression> ::= <Expression>
		@Optionalexpression2 = 117,                // <OptionalExpression> ::= 
		@Expression = 118,                         // <Expression> ::= <Expr Bool>
		@Exprbool_Ampamp = 119,                    // <Expr Bool> ::= <Expr Bool> '&&' <Expr Eq>
		@Exprbool_Pipepipe = 120,                  // <Expr Bool> ::= <Expr Bool> '||' <Expr Eq>
		@Exprbool_Caret = 121,                     // <Expr Bool> ::= <Expr Bool> '^' <Expr Eq>
		@Exprbool = 122,                           // <Expr Bool> ::= <Expr Eq>
		@Expreq_Eqeq = 123,                        // <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
		@Expreq_Exclameq = 124,                    // <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
		@Expreq = 125,                             // <Expr Eq> ::= <Exp Comp>
		@Expcomp_Lt = 126,                         // <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
		@Expcomp_Gt = 127,                         // <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
		@Expcomp_Lteq = 128,                       // <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
		@Expcomp_Gteq = 129,                       // <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
		@Expcomp = 130,                            // <Exp Comp> ::= <Exp Add>
		@Expadd_Plus = 131,                        // <Exp Add> ::= <Exp Add> '+' <Exp Mult>
		@Expadd_Minus = 132,                       // <Exp Add> ::= <Exp Add> '-' <Exp Mult>
		@Expadd = 133,                             // <Exp Add> ::= <Exp Mult>
		@Expmult_Times = 134,                      // <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
		@Expmult_Div = 135,                        // <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
		@Expmult_Percent = 136,                    // <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
		@Expmult = 137,                            // <Exp Mult> ::= <Exp Unary>
		@Expunary_Exclam = 138,                    // <Exp Unary> ::= '!' <Value>
		@Expunary_Minus = 139,                     // <Exp Unary> ::= '-' <Value>
		@Expunary_Lparen_Rparen = 140,             // <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
		@Expunary = 141,                           // <Exp Unary> ::= <Value>
		@Value = 142,                              // <Value> ::= <Value_Literal>
		@Value_Rand = 143,                         // <Value> ::= rand
		@Value2 = 144,                             // <Value> ::= <ValuePointer>
		@Value_Identifier_Lparen_Rparen = 145,     // <Value> ::= Identifier '(' <ExpressionList> ')'
		@Value_Identifier_Lparen_Rparen2 = 146,    // <Value> ::= Identifier '(' ')'
		@Value_Lparen_Rparen = 147,                // <Value> ::= '(' <Expression> ')'
		@Expressionlist_Comma = 148,               // <ExpressionList> ::= <ExpressionList> ',' <Expression>
		@Expressionlist = 149,                     // <ExpressionList> ::= <Expression>
		@Valuepointer_Identifier = 150,            // <ValuePointer> ::= Identifier
		@Valuepointer_Identifier_Lbracket_Rbracket = 151,  // <ValuePointer> ::= Identifier '[' <Expression> ']'
		@Valuepointer_Display_Lbracket_Comma_Rbracket = 152   // <ValuePointer> ::= display '[' <Expression> ',' <Expression> ']'
	}
}
