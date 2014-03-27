
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
		@Percenteq = 13,                           // '%='
		@Ampamp = 14,                              // '&&'
		@Ampeq = 15,                               // '&='
		@Lparen = 16,                              // '('
		@Rparen = 17,                              // ')'
		@Times = 18,                               // '*'
		@Timeseq = 19,                             // '*='
		@Comma = 20,                               // ','
		@Div = 21,                                 // '/'
		@Diveq = 22,                               // '/='
		@Colon = 23,                               // ':'
		@Coloneq = 24,                             // ':='
		@Semi = 25,                                // ';'
		@Lbracket = 26,                            // '['
		@Rbracket = 27,                            // ']'
		@Caret = 28,                               // '^'
		@Careteq = 29,                             // '^='
		@Lbrace = 30,                              // '{'
		@Pipepipe = 31,                            // '||'
		@Pipeeq = 32,                              // '|='
		@Rbrace = 33,                              // '}'
		@Plus = 34,                                // '+'
		@Plusplus = 35,                            // '++'
		@Pluseq = 36,                              // '+='
		@Lt = 37,                                  // '<'
		@Lteq = 38,                                // '<='
		@Eq = 39,                                  // '='
		@Minuseq = 40,                             // '-='
		@Eqeq = 41,                                // '=='
		@Gt = 42,                                  // '>'
		@Gteq = 43,                                // '>='
		@Begin = 44,                               // begin
		@Bool = 45,                                // bool
		@Boolean = 46,                             // boolean
		@Case = 47,                                // case
		@Char = 48,                                // char
		@Character = 49,                           // Character
		@Charliteral = 50,                         // CharLiteral
		@Close = 51,                               // close
		@Const = 52,                               // const
		@Decliteral = 53,                          // DecLiteral
		@Default = 54,                             // default
		@Digit = 55,                               // digit
		@Digitliteral = 56,                        // DigitLiteral
		@Display = 57,                             // display
		@Do = 58,                                  // do
		@Else = 59,                                // else
		@Elsif = 60,                               // elsif
		@End = 61,                                 // end
		@False = 62,                               // false
		@For = 63,                                 // for
		@Global = 64,                              // global
		@Goto = 65,                                // goto
		@Hexliteral = 66,                          // HexLiteral
		@Identifier = 67,                          // Identifier
		@If = 68,                                  // if
		@In = 69,                                  // in
		@Int = 70,                                 // int
		@Integer = 71,                             // integer
		@Out = 72,                                 // out
		@Program = 73,                             // program
		@Quit = 74,                                // quit
		@Rand = 75,                                // rand
		@Repeat = 76,                              // repeat
		@Return = 77,                              // return
		@Stop = 78,                                // stop
		@Stringliteral = 79,                       // StringLiteral
		@Switch = 80,                              // switch
		@Then = 81,                                // then
		@True = 82,                                // true
		@Until = 83,                               // until
		@Var = 84,                                 // var
		@Void = 85,                                // void
		@While = 86,                               // while
		@Array_literal = 87,                       // <Array_Literal>
		@Constants = 88,                           // <Constants>
		@Expadd = 89,                              // <Exp Add>
		@Expcomp = 90,                             // <Exp Comp>
		@Expmult = 91,                             // <Exp Mult>
		@Exprand = 92,                             // <Exp Rand>
		@Expunary = 93,                            // <Exp Unary>
		@Exprbool = 94,                            // <Expr Bool>
		@Expreq = 95,                              // <Expr Eq>
		@Expression = 96,                          // <Expression>
		@Expressionlist = 97,                      // <ExpressionList>
		@Footer = 98,                              // <Footer>
		@Globalvars = 99,                          // <GlobalVars>
		@Header = 100,                             // <Header>
		@Literal = 101,                            // <Literal>
		@Literal_bool = 102,                       // <Literal_Bool>
		@Literal_bool_list = 103,                  // <Literal_Bool_List>
		@Literal_boolarr = 104,                    // <Literal_BoolArr>
		@Literal_char = 105,                       // <Literal_Char>
		@Literal_char_list = 106,                  // <Literal_Char_List>
		@Literal_digit = 107,                      // <Literal_Digit>
		@Literal_digit_list = 108,                 // <Literal_Digit_List>
		@Literal_digitarr = 109,                   // <Literal_DigitArr>
		@Literal_int = 110,                        // <Literal_Int>
		@Literal_int_list = 111,                   // <Literal_Int_List>
		@Literal_intarr = 112,                     // <Literal_IntArr>
		@Literal_string = 113,                     // <Literal_String>
		@Mainstatements = 114,                     // <MainStatements>
		@Method = 115,                             // <Method>
		@Methodbody = 116,                         // <MethodBody>
		@Methodheader = 117,                       // <MethodHeader>
		@Methodlist = 118,                         // <MethodList>
		@Optionalexpression = 119,                 // <OptionalExpression>
		@Optionalsimstatement = 120,               // <OptionalSimStatement>
		@Param = 121,                              // <Param>
		@Paramdecl = 122,                          // <ParamDecl>
		@Paramlist = 123,                          // <ParamList>
		@Program2 = 124,                           // <Program>
		@Simplestatement = 125,                    // <SimpleStatement>
		@Statement = 126,                          // <Statement>
		@Statementlist = 127,                      // <StatementList>
		@Stmt_assignment = 128,                    // <Stmt_Assignment>
		@Stmt_call = 129,                          // <Stmt_Call>
		@Stmt_elseiflist = 130,                    // <Stmt_ElseIfList>
		@Stmt_for = 131,                           // <Stmt_For>
		@Stmt_goto = 132,                          // <Stmt_Goto>
		@Stmt_if = 133,                            // <Stmt_If>
		@Stmt_in = 134,                            // <Stmt_In>
		@Stmt_inc = 135,                           // <Stmt_Inc>
		@Stmt_label = 136,                         // <Stmt_Label>
		@Stmt_modassignment = 137,                 // <Stmt_ModAssignment>
		@Stmt_out = 138,                           // <Stmt_Out>
		@Stmt_quit = 139,                          // <Stmt_Quit>
		@Stmt_repeat = 140,                        // <Stmt_Repeat>
		@Stmt_return = 141,                        // <Stmt_Return>
		@Stmt_switch = 142,                        // <Stmt_Switch>
		@Stmt_switch_caselist = 143,               // <Stmt_Switch_CaseList>
		@Stmt_while = 144,                         // <Stmt_While>
		@Type = 145,                               // <Type>
		@Type_bool = 146,                          // <Type_Bool>
		@Type_boolarr = 147,                       // <Type_BoolArr>
		@Type_char = 148,                          // <Type_Char>
		@Type_digit = 149,                         // <Type_Digit>
		@Type_digitarr = 150,                      // <Type_DigitArr>
		@Type_int = 151,                           // <Type_Int>
		@Type_intarr = 152,                        // <Type_IntArr>
		@Type_string = 153,                        // <Type_String>
		@Type_void = 154,                          // <Type_Void>
		@Value = 155,                              // <Value>
		@Value_literal = 156,                      // <Value_Literal>
		@Valuepointer = 157,                       // <ValuePointer>
		@Vardecl = 158,                            // <VarDecl>
		@Vardeclbody = 159,                        // <VarDeclBody>
		@Varlist = 160                             // <VarList>
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
		@Statement6 = 35,                          // <Statement> ::= <Stmt_Switch>
		@Simplestatement = 36,                     // <SimpleStatement> ::= <Stmt_Quit>
		@Simplestatement2 = 37,                    // <SimpleStatement> ::= <Stmt_Return>
		@Simplestatement3 = 38,                    // <SimpleStatement> ::= <Stmt_Out>
		@Simplestatement4 = 39,                    // <SimpleStatement> ::= <Stmt_In>
		@Simplestatement5 = 40,                    // <SimpleStatement> ::= <Stmt_Inc>
		@Simplestatement6 = 41,                    // <SimpleStatement> ::= <Stmt_Assignment>
		@Simplestatement7 = 42,                    // <SimpleStatement> ::= <Stmt_Call>
		@Simplestatement8 = 43,                    // <SimpleStatement> ::= <Stmt_ModAssignment>
		@Statementlist = 44,                       // <StatementList> ::= <StatementList> <Statement>
		@Statementlist2 = 45,                      // <StatementList> ::= 
		@Stmt_inc_Plusplus = 46,                   // <Stmt_Inc> ::= <ValuePointer> '++'
		@Stmt_inc_Minusminus = 47,                 // <Stmt_Inc> ::= <ValuePointer> '--'
		@Stmt_quit_Quit = 48,                      // <Stmt_Quit> ::= quit
		@Stmt_quit_Stop = 49,                      // <Stmt_Quit> ::= stop
		@Stmt_quit_Close = 50,                     // <Stmt_Quit> ::= close
		@Stmt_out_Out = 51,                        // <Stmt_Out> ::= out <Expression>
		@Stmt_out_Out2 = 52,                       // <Stmt_Out> ::= out <Literal_String>
		@Stmt_in_In = 53,                          // <Stmt_In> ::= in <ValuePointer>
		@Stmt_assignment_Eq = 54,                  // <Stmt_Assignment> ::= <ValuePointer> '=' <Expression>
		@Stmt_modassignment_Pluseq = 55,           // <Stmt_ModAssignment> ::= <ValuePointer> '+=' <Expression>
		@Stmt_modassignment_Minuseq = 56,          // <Stmt_ModAssignment> ::= <ValuePointer> '-=' <Expression>
		@Stmt_modassignment_Timeseq = 57,          // <Stmt_ModAssignment> ::= <ValuePointer> '*=' <Expression>
		@Stmt_modassignment_Diveq = 58,            // <Stmt_ModAssignment> ::= <ValuePointer> '/=' <Expression>
		@Stmt_modassignment_Percenteq = 59,        // <Stmt_ModAssignment> ::= <ValuePointer> '%=' <Expression>
		@Stmt_modassignment_Ampeq = 60,            // <Stmt_ModAssignment> ::= <ValuePointer> '&=' <Expression>
		@Stmt_modassignment_Pipeeq = 61,           // <Stmt_ModAssignment> ::= <ValuePointer> '|=' <Expression>
		@Stmt_modassignment_Careteq = 62,          // <Stmt_ModAssignment> ::= <ValuePointer> '^=' <Expression>
		@Stmt_return_Return = 63,                  // <Stmt_Return> ::= return <Expression>
		@Stmt_return_Return2 = 64,                 // <Stmt_Return> ::= return
		@Stmt_call_Identifier_Lparen_Rparen = 65,  // <Stmt_Call> ::= Identifier '(' <ExpressionList> ')'
		@Stmt_call_Identifier_Lparen_Rparen2 = 66,  // <Stmt_Call> ::= Identifier '(' ')'
		@Stmt_if_If_Then_End = 67,                 // <Stmt_If> ::= if <Expression> then <StatementList> <Stmt_ElseIfList> end
		@Stmt_elseiflist_Elsif_Then = 68,          // <Stmt_ElseIfList> ::= elsif <Expression> then <StatementList> <Stmt_ElseIfList>
		@Stmt_elseiflist_Else = 69,                // <Stmt_ElseIfList> ::= else <StatementList>
		@Stmt_elseiflist = 70,                     // <Stmt_ElseIfList> ::= 
		@Stmt_while_While_Do_End = 71,             // <Stmt_While> ::= while <Expression> do <StatementList> end
		@Stmt_for_For_Lparen_Semi_Semi_Rparen_Do_End = 72,  // <Stmt_For> ::= for '(' <OptionalSimStatement> ';' <OptionalExpression> ';' <OptionalSimStatement> ')' do <StatementList> end
		@Stmt_repeat_Repeat_Until_Lparen_Rparen = 73,  // <Stmt_Repeat> ::= repeat <StatementList> until '(' <Expression> ')'
		@Stmt_switch_Switch_Begin_End = 74,        // <Stmt_Switch> ::= switch <Expression> begin <Stmt_Switch_CaseList> end
		@Stmt_switch_caselist_Case_Colon_End = 75,  // <Stmt_Switch_CaseList> ::= case <Value_Literal> ':' <StatementList> end <Stmt_Switch_CaseList>
		@Stmt_switch_caselist_Default_Colon_End = 76,  // <Stmt_Switch_CaseList> ::= default ':' <StatementList> end
		@Stmt_switch_caselist = 77,                // <Stmt_Switch_CaseList> ::= 
		@Stmt_goto_Goto_Identifier = 78,           // <Stmt_Goto> ::= goto Identifier
		@Stmt_label_Identifier_Colon = 79,         // <Stmt_Label> ::= Identifier ':'
		@Type = 80,                                // <Type> ::= <Type_Int>
		@Type2 = 81,                               // <Type> ::= <Type_Digit>
		@Type3 = 82,                               // <Type> ::= <Type_Char>
		@Type4 = 83,                               // <Type> ::= <Type_Bool>
		@Type5 = 84,                               // <Type> ::= <Type_Void>
		@Type6 = 85,                               // <Type> ::= <Type_IntArr>
		@Type7 = 86,                               // <Type> ::= <Type_String>
		@Type8 = 87,                               // <Type> ::= <Type_DigitArr>
		@Type9 = 88,                               // <Type> ::= <Type_BoolArr>
		@Type_int_Int = 89,                        // <Type_Int> ::= int
		@Type_int_Integer = 90,                    // <Type_Int> ::= integer
		@Type_char_Char = 91,                      // <Type_Char> ::= char
		@Type_char_Character = 92,                 // <Type_Char> ::= Character
		@Type_digit_Digit = 93,                    // <Type_Digit> ::= digit
		@Type_bool_Bool = 94,                      // <Type_Bool> ::= bool
		@Type_bool_Boolean = 95,                   // <Type_Bool> ::= boolean
		@Type_void_Void = 96,                      // <Type_Void> ::= void
		@Type_intarr_Lbracket_Rbracket = 97,       // <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
		@Type_string_Lbracket_Rbracket = 98,       // <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
		@Type_digitarr_Lbracket_Rbracket = 99,     // <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
		@Type_boolarr_Lbracket_Rbracket = 100,     // <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
		@Literal = 101,                            // <Literal> ::= <Array_Literal>
		@Literal2 = 102,                           // <Literal> ::= <Value_Literal>
		@Array_literal = 103,                      // <Array_Literal> ::= <Literal_IntArr>
		@Array_literal2 = 104,                     // <Array_Literal> ::= <Literal_String>
		@Array_literal3 = 105,                     // <Array_Literal> ::= <Literal_DigitArr>
		@Array_literal4 = 106,                     // <Array_Literal> ::= <Literal_BoolArr>
		@Value_literal = 107,                      // <Value_Literal> ::= <Literal_Int>
		@Value_literal2 = 108,                     // <Value_Literal> ::= <Literal_Char>
		@Value_literal3 = 109,                     // <Value_Literal> ::= <Literal_Bool>
		@Value_literal4 = 110,                     // <Value_Literal> ::= <Literal_Digit>
		@Literal_int_Decliteral = 111,             // <Literal_Int> ::= DecLiteral
		@Literal_int_Hexliteral = 112,             // <Literal_Int> ::= HexLiteral
		@Literal_char_Charliteral = 113,           // <Literal_Char> ::= CharLiteral
		@Literal_bool_True = 114,                  // <Literal_Bool> ::= true
		@Literal_bool_False = 115,                 // <Literal_Bool> ::= false
		@Literal_digit_Digitliteral = 116,         // <Literal_Digit> ::= DigitLiteral
		@Literal_intarr_Lbrace_Rbrace = 117,       // <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
		@Literal_string_Lbrace_Rbrace = 118,       // <Literal_String> ::= '{' <Literal_Char_List> '}'
		@Literal_string_Stringliteral = 119,       // <Literal_String> ::= StringLiteral
		@Literal_digitarr_Lbrace_Rbrace = 120,     // <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
		@Literal_boolarr_Lbrace_Rbrace = 121,      // <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
		@Literal_int_list_Comma = 122,             // <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
		@Literal_int_list = 123,                   // <Literal_Int_List> ::= <Literal_Int>
		@Literal_char_list_Comma = 124,            // <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
		@Literal_char_list = 125,                  // <Literal_Char_List> ::= <Literal_Char>
		@Literal_digit_list_Comma = 126,           // <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
		@Literal_digit_list = 127,                 // <Literal_Digit_List> ::= <Literal_Digit>
		@Literal_bool_list_Comma = 128,            // <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
		@Literal_bool_list = 129,                  // <Literal_Bool_List> ::= <Literal_Bool>
		@Optionalexpression = 130,                 // <OptionalExpression> ::= <Expression>
		@Optionalexpression2 = 131,                // <OptionalExpression> ::= 
		@Expression = 132,                         // <Expression> ::= <Expr Bool>
		@Exprbool_Ampamp = 133,                    // <Expr Bool> ::= <Expr Bool> '&&' <Expr Eq>
		@Exprbool_Pipepipe = 134,                  // <Expr Bool> ::= <Expr Bool> '||' <Expr Eq>
		@Exprbool_Caret = 135,                     // <Expr Bool> ::= <Expr Bool> '^' <Expr Eq>
		@Exprbool = 136,                           // <Expr Bool> ::= <Expr Eq>
		@Expreq_Eqeq = 137,                        // <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
		@Expreq_Exclameq = 138,                    // <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
		@Expreq = 139,                             // <Expr Eq> ::= <Exp Comp>
		@Expcomp_Lt = 140,                         // <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
		@Expcomp_Gt = 141,                         // <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
		@Expcomp_Lteq = 142,                       // <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
		@Expcomp_Gteq = 143,                       // <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
		@Expcomp = 144,                            // <Exp Comp> ::= <Exp Add>
		@Expadd_Plus = 145,                        // <Exp Add> ::= <Exp Add> '+' <Exp Mult>
		@Expadd_Minus = 146,                       // <Exp Add> ::= <Exp Add> '-' <Exp Mult>
		@Expadd = 147,                             // <Exp Add> ::= <Exp Mult>
		@Expmult_Times = 148,                      // <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
		@Expmult_Div = 149,                        // <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
		@Expmult_Percent = 150,                    // <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
		@Expmult = 151,                            // <Exp Mult> ::= <Exp Unary>
		@Expunary_Exclam = 152,                    // <Exp Unary> ::= '!' <Value>
		@Expunary_Minus = 153,                     // <Exp Unary> ::= '-' <Value>
		@Expunary_Lparen_Rparen = 154,             // <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
		@Expunary = 155,                           // <Exp Unary> ::= <Value>
		@Value = 156,                              // <Value> ::= <Value_Literal>
		@Value2 = 157,                             // <Value> ::= <Exp Rand>
		@Value3 = 158,                             // <Value> ::= <ValuePointer>
		@Value_Identifier_Lparen_Rparen = 159,     // <Value> ::= Identifier '(' <ExpressionList> ')'
		@Value_Identifier_Lparen_Rparen2 = 160,    // <Value> ::= Identifier '(' ')'
		@Value_Lparen_Rparen = 161,                // <Value> ::= '(' <Expression> ')'
		@Value_Plusplus = 162,                     // <Value> ::= <ValuePointer> '++'
		@Value_Minusminus = 163,                   // <Value> ::= <ValuePointer> '--'
		@Value_Plusplus2 = 164,                    // <Value> ::= '++' <ValuePointer>
		@Value_Minusminus2 = 165,                  // <Value> ::= '--' <ValuePointer>
		@Exprand_Rand = 166,                       // <Exp Rand> ::= rand
		@Exprand_Rand_Lbracket_Rbracket = 167,     // <Exp Rand> ::= rand '[' <Expression> ']'
		@Expressionlist_Comma = 168,               // <ExpressionList> ::= <ExpressionList> ',' <Expression>
		@Expressionlist = 169,                     // <ExpressionList> ::= <Expression>
		@Valuepointer_Identifier = 170,            // <ValuePointer> ::= Identifier
		@Valuepointer_Identifier_Lbracket_Rbracket = 171,  // <ValuePointer> ::= Identifier '[' <Expression> ']'
		@Valuepointer_Display_Lbracket_Comma_Rbracket = 172   // <ValuePointer> ::= display '[' <Expression> ',' <Expression> ']'
	}
}
