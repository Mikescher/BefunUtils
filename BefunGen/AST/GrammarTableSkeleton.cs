
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
		@Outf = 73,                                // outf
		@Program = 74,                             // program
		@Quit = 75,                                // quit
		@Rand = 76,                                // rand
		@Repeat = 77,                              // repeat
		@Return = 78,                              // return
		@Stop = 79,                                // stop
		@Stringliteral = 80,                       // StringLiteral
		@Switch = 81,                              // switch
		@Then = 82,                                // then
		@True = 83,                                // true
		@Until = 84,                               // until
		@Var = 85,                                 // var
		@Void = 86,                                // void
		@While = 87,                               // while
		@Array_literal = 88,                       // <Array_Literal>
		@Constants = 89,                           // <Constants>
		@Expadd = 90,                              // <Exp Add>
		@Expcomp = 91,                             // <Exp Comp>
		@Expmult = 92,                             // <Exp Mult>
		@Exprand = 93,                             // <Exp Rand>
		@Expunary = 94,                            // <Exp Unary>
		@Exprbool = 95,                            // <Expr Bool>
		@Expreq = 96,                              // <Expr Eq>
		@Expression = 97,                          // <Expression>
		@Expressionlist = 98,                      // <ExpressionList>
		@Footer = 99,                              // <Footer>
		@Globalvars = 100,                         // <GlobalVars>
		@Header = 101,                             // <Header>
		@Literal = 102,                            // <Literal>
		@Literal_bool = 103,                       // <Literal_Bool>
		@Literal_bool_list = 104,                  // <Literal_Bool_List>
		@Literal_boolarr = 105,                    // <Literal_BoolArr>
		@Literal_char = 106,                       // <Literal_Char>
		@Literal_char_list = 107,                  // <Literal_Char_List>
		@Literal_digit = 108,                      // <Literal_Digit>
		@Literal_digit_list = 109,                 // <Literal_Digit_List>
		@Literal_digitarr = 110,                   // <Literal_DigitArr>
		@Literal_int = 111,                        // <Literal_Int>
		@Literal_int_list = 112,                   // <Literal_Int_List>
		@Literal_intarr = 113,                     // <Literal_IntArr>
		@Literal_string = 114,                     // <Literal_String>
		@Mainstatements = 115,                     // <MainStatements>
		@Method = 116,                             // <Method>
		@Methodbody = 117,                         // <MethodBody>
		@Methodheader = 118,                       // <MethodHeader>
		@Methodlist = 119,                         // <MethodList>
		@Optionalexpression = 120,                 // <OptionalExpression>
		@Optionalsimstatement = 121,               // <OptionalSimStatement>
		@Outflist = 122,                           // <OutfList>
		@Param = 123,                              // <Param>
		@Paramdecl = 124,                          // <ParamDecl>
		@Paramlist = 125,                          // <ParamList>
		@Program2 = 126,                           // <Program>
		@Simplestatement = 127,                    // <SimpleStatement>
		@Statement = 128,                          // <Statement>
		@Statementlist = 129,                      // <StatementList>
		@Stmt_assignment = 130,                    // <Stmt_Assignment>
		@Stmt_call = 131,                          // <Stmt_Call>
		@Stmt_elseiflist = 132,                    // <Stmt_ElseIfList>
		@Stmt_for = 133,                           // <Stmt_For>
		@Stmt_goto = 134,                          // <Stmt_Goto>
		@Stmt_if = 135,                            // <Stmt_If>
		@Stmt_in = 136,                            // <Stmt_In>
		@Stmt_inc = 137,                           // <Stmt_Inc>
		@Stmt_label = 138,                         // <Stmt_Label>
		@Stmt_modassignment = 139,                 // <Stmt_ModAssignment>
		@Stmt_out = 140,                           // <Stmt_Out>
		@Stmt_outf = 141,                          // <Stmt_Outf>
		@Stmt_quit = 142,                          // <Stmt_Quit>
		@Stmt_repeat = 143,                        // <Stmt_Repeat>
		@Stmt_return = 144,                        // <Stmt_Return>
		@Stmt_switch = 145,                        // <Stmt_Switch>
		@Stmt_switch_caselist = 146,               // <Stmt_Switch_CaseList>
		@Stmt_while = 147,                         // <Stmt_While>
		@Type = 148,                               // <Type>
		@Type_bool = 149,                          // <Type_Bool>
		@Type_boolarr = 150,                       // <Type_BoolArr>
		@Type_char = 151,                          // <Type_Char>
		@Type_digit = 152,                         // <Type_Digit>
		@Type_digitarr = 153,                      // <Type_DigitArr>
		@Type_int = 154,                           // <Type_Int>
		@Type_intarr = 155,                        // <Type_IntArr>
		@Type_string = 156,                        // <Type_String>
		@Type_void = 157,                          // <Type_Void>
		@Value = 158,                              // <Value>
		@Value_literal = 159,                      // <Value_Literal>
		@Valuepointer = 160,                       // <ValuePointer>
		@Vardecl = 161,                            // <VarDecl>
		@Vardeclbody = 162,                        // <VarDeclBody>
		@Varlist = 163                             // <VarList>
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
		@Simplestatement9 = 44,                    // <SimpleStatement> ::= <Stmt_Outf>
		@Statementlist = 45,                       // <StatementList> ::= <StatementList> <Statement>
		@Statementlist2 = 46,                      // <StatementList> ::= 
		@Stmt_inc_Plusplus = 47,                   // <Stmt_Inc> ::= <ValuePointer> '++'
		@Stmt_inc_Minusminus = 48,                 // <Stmt_Inc> ::= <ValuePointer> '--'
		@Stmt_quit_Quit = 49,                      // <Stmt_Quit> ::= quit
		@Stmt_quit_Stop = 50,                      // <Stmt_Quit> ::= stop
		@Stmt_quit_Close = 51,                     // <Stmt_Quit> ::= close
		@Stmt_out_Out = 52,                        // <Stmt_Out> ::= out <Expression>
		@Stmt_out_Out2 = 53,                       // <Stmt_Out> ::= out <Literal_String>
		@Stmt_in_In = 54,                          // <Stmt_In> ::= in <ValuePointer>
		@Stmt_assignment_Eq = 55,                  // <Stmt_Assignment> ::= <ValuePointer> '=' <Expression>
		@Stmt_modassignment_Pluseq = 56,           // <Stmt_ModAssignment> ::= <ValuePointer> '+=' <Expression>
		@Stmt_modassignment_Minuseq = 57,          // <Stmt_ModAssignment> ::= <ValuePointer> '-=' <Expression>
		@Stmt_modassignment_Timeseq = 58,          // <Stmt_ModAssignment> ::= <ValuePointer> '*=' <Expression>
		@Stmt_modassignment_Diveq = 59,            // <Stmt_ModAssignment> ::= <ValuePointer> '/=' <Expression>
		@Stmt_modassignment_Percenteq = 60,        // <Stmt_ModAssignment> ::= <ValuePointer> '%=' <Expression>
		@Stmt_modassignment_Ampeq = 61,            // <Stmt_ModAssignment> ::= <ValuePointer> '&=' <Expression>
		@Stmt_modassignment_Pipeeq = 62,           // <Stmt_ModAssignment> ::= <ValuePointer> '|=' <Expression>
		@Stmt_modassignment_Careteq = 63,          // <Stmt_ModAssignment> ::= <ValuePointer> '^=' <Expression>
		@Stmt_return_Return = 64,                  // <Stmt_Return> ::= return <Expression>
		@Stmt_return_Return2 = 65,                 // <Stmt_Return> ::= return
		@Stmt_call_Identifier_Lparen_Rparen = 66,  // <Stmt_Call> ::= Identifier '(' <ExpressionList> ')'
		@Stmt_call_Identifier_Lparen_Rparen2 = 67,  // <Stmt_Call> ::= Identifier '(' ')'
		@Stmt_outf_Outf = 68,                      // <Stmt_Outf> ::= outf <OutfList>
		@Outflist = 69,                            // <OutfList> ::= <Expression>
		@Outflist2 = 70,                           // <OutfList> ::= <Literal_String>
		@Outflist_Comma = 71,                      // <OutfList> ::= <OutfList> ',' <Expression>
		@Outflist_Comma2 = 72,                     // <OutfList> ::= <OutfList> ',' <Literal_String>
		@Stmt_if_If_Then_End = 73,                 // <Stmt_If> ::= if <Expression> then <StatementList> <Stmt_ElseIfList> end
		@Stmt_elseiflist_Elsif_Then = 74,          // <Stmt_ElseIfList> ::= elsif <Expression> then <StatementList> <Stmt_ElseIfList>
		@Stmt_elseiflist_Else = 75,                // <Stmt_ElseIfList> ::= else <StatementList>
		@Stmt_elseiflist = 76,                     // <Stmt_ElseIfList> ::= 
		@Stmt_while_While_Do_End = 77,             // <Stmt_While> ::= while <Expression> do <StatementList> end
		@Stmt_for_For_Lparen_Semi_Semi_Rparen_Do_End = 78,  // <Stmt_For> ::= for '(' <OptionalSimStatement> ';' <OptionalExpression> ';' <OptionalSimStatement> ')' do <StatementList> end
		@Stmt_repeat_Repeat_Until_Lparen_Rparen = 79,  // <Stmt_Repeat> ::= repeat <StatementList> until '(' <Expression> ')'
		@Stmt_switch_Switch_Begin_End = 80,        // <Stmt_Switch> ::= switch <Expression> begin <Stmt_Switch_CaseList> end
		@Stmt_switch_caselist_Case_Colon_End = 81,  // <Stmt_Switch_CaseList> ::= case <Value_Literal> ':' <StatementList> end <Stmt_Switch_CaseList>
		@Stmt_switch_caselist_Default_Colon_End = 82,  // <Stmt_Switch_CaseList> ::= default ':' <StatementList> end
		@Stmt_switch_caselist = 83,                // <Stmt_Switch_CaseList> ::= 
		@Stmt_goto_Goto_Identifier = 84,           // <Stmt_Goto> ::= goto Identifier
		@Stmt_label_Identifier_Colon = 85,         // <Stmt_Label> ::= Identifier ':'
		@Type = 86,                                // <Type> ::= <Type_Int>
		@Type2 = 87,                               // <Type> ::= <Type_Digit>
		@Type3 = 88,                               // <Type> ::= <Type_Char>
		@Type4 = 89,                               // <Type> ::= <Type_Bool>
		@Type5 = 90,                               // <Type> ::= <Type_Void>
		@Type6 = 91,                               // <Type> ::= <Type_IntArr>
		@Type7 = 92,                               // <Type> ::= <Type_String>
		@Type8 = 93,                               // <Type> ::= <Type_DigitArr>
		@Type9 = 94,                               // <Type> ::= <Type_BoolArr>
		@Type_int_Int = 95,                        // <Type_Int> ::= int
		@Type_int_Integer = 96,                    // <Type_Int> ::= integer
		@Type_char_Char = 97,                      // <Type_Char> ::= char
		@Type_char_Character = 98,                 // <Type_Char> ::= Character
		@Type_digit_Digit = 99,                    // <Type_Digit> ::= digit
		@Type_bool_Bool = 100,                     // <Type_Bool> ::= bool
		@Type_bool_Boolean = 101,                  // <Type_Bool> ::= boolean
		@Type_void_Void = 102,                     // <Type_Void> ::= void
		@Type_intarr_Lbracket_Rbracket = 103,      // <Type_IntArr> ::= <Type_Int> '[' <Literal_Int> ']'
		@Type_string_Lbracket_Rbracket = 104,      // <Type_String> ::= <Type_Char> '[' <Literal_Int> ']'
		@Type_digitarr_Lbracket_Rbracket = 105,    // <Type_DigitArr> ::= <Type_Digit> '[' <Literal_Int> ']'
		@Type_boolarr_Lbracket_Rbracket = 106,     // <Type_BoolArr> ::= <Type_Bool> '[' <Literal_Int> ']'
		@Literal = 107,                            // <Literal> ::= <Array_Literal>
		@Literal2 = 108,                           // <Literal> ::= <Value_Literal>
		@Array_literal = 109,                      // <Array_Literal> ::= <Literal_IntArr>
		@Array_literal2 = 110,                     // <Array_Literal> ::= <Literal_String>
		@Array_literal3 = 111,                     // <Array_Literal> ::= <Literal_DigitArr>
		@Array_literal4 = 112,                     // <Array_Literal> ::= <Literal_BoolArr>
		@Value_literal = 113,                      // <Value_Literal> ::= <Literal_Int>
		@Value_literal2 = 114,                     // <Value_Literal> ::= <Literal_Char>
		@Value_literal3 = 115,                     // <Value_Literal> ::= <Literal_Bool>
		@Value_literal4 = 116,                     // <Value_Literal> ::= <Literal_Digit>
		@Literal_int_Decliteral = 117,             // <Literal_Int> ::= DecLiteral
		@Literal_int_Hexliteral = 118,             // <Literal_Int> ::= HexLiteral
		@Literal_char_Charliteral = 119,           // <Literal_Char> ::= CharLiteral
		@Literal_bool_True = 120,                  // <Literal_Bool> ::= true
		@Literal_bool_False = 121,                 // <Literal_Bool> ::= false
		@Literal_digit_Digitliteral = 122,         // <Literal_Digit> ::= DigitLiteral
		@Literal_intarr_Lbrace_Rbrace = 123,       // <Literal_IntArr> ::= '{' <Literal_Int_List> '}'
		@Literal_string_Lbrace_Rbrace = 124,       // <Literal_String> ::= '{' <Literal_Char_List> '}'
		@Literal_string_Stringliteral = 125,       // <Literal_String> ::= StringLiteral
		@Literal_digitarr_Lbrace_Rbrace = 126,     // <Literal_DigitArr> ::= '{' <Literal_Digit_List> '}'
		@Literal_boolarr_Lbrace_Rbrace = 127,      // <Literal_BoolArr> ::= '{' <Literal_Bool_List> '}'
		@Literal_int_list_Comma = 128,             // <Literal_Int_List> ::= <Literal_Int_List> ',' <Literal_Int>
		@Literal_int_list = 129,                   // <Literal_Int_List> ::= <Literal_Int>
		@Literal_char_list_Comma = 130,            // <Literal_Char_List> ::= <Literal_Char_List> ',' <Literal_Char>
		@Literal_char_list = 131,                  // <Literal_Char_List> ::= <Literal_Char>
		@Literal_digit_list_Comma = 132,           // <Literal_Digit_List> ::= <Literal_Digit_List> ',' <Literal_Digit>
		@Literal_digit_list = 133,                 // <Literal_Digit_List> ::= <Literal_Digit>
		@Literal_bool_list_Comma = 134,            // <Literal_Bool_List> ::= <Literal_Bool_List> ',' <Literal_Bool>
		@Literal_bool_list = 135,                  // <Literal_Bool_List> ::= <Literal_Bool>
		@Optionalexpression = 136,                 // <OptionalExpression> ::= <Expression>
		@Optionalexpression2 = 137,                // <OptionalExpression> ::= 
		@Expression = 138,                         // <Expression> ::= <Expr Bool>
		@Exprbool_Ampamp = 139,                    // <Expr Bool> ::= <Expr Bool> '&&' <Expr Eq>
		@Exprbool_Pipepipe = 140,                  // <Expr Bool> ::= <Expr Bool> '||' <Expr Eq>
		@Exprbool_Caret = 141,                     // <Expr Bool> ::= <Expr Bool> '^' <Expr Eq>
		@Exprbool = 142,                           // <Expr Bool> ::= <Expr Eq>
		@Expreq_Eqeq = 143,                        // <Expr Eq> ::= <Expr Eq> '==' <Exp Comp>
		@Expreq_Exclameq = 144,                    // <Expr Eq> ::= <Expr Eq> '!=' <Exp Comp>
		@Expreq = 145,                             // <Expr Eq> ::= <Exp Comp>
		@Expcomp_Lt = 146,                         // <Exp Comp> ::= <Exp Comp> '<' <Exp Add>
		@Expcomp_Gt = 147,                         // <Exp Comp> ::= <Exp Comp> '>' <Exp Add>
		@Expcomp_Lteq = 148,                       // <Exp Comp> ::= <Exp Comp> '<=' <Exp Add>
		@Expcomp_Gteq = 149,                       // <Exp Comp> ::= <Exp Comp> '>=' <Exp Add>
		@Expcomp = 150,                            // <Exp Comp> ::= <Exp Add>
		@Expadd_Plus = 151,                        // <Exp Add> ::= <Exp Add> '+' <Exp Mult>
		@Expadd_Minus = 152,                       // <Exp Add> ::= <Exp Add> '-' <Exp Mult>
		@Expadd = 153,                             // <Exp Add> ::= <Exp Mult>
		@Expmult_Times = 154,                      // <Exp Mult> ::= <Exp Mult> '*' <Exp Unary>
		@Expmult_Div = 155,                        // <Exp Mult> ::= <Exp Mult> '/' <Exp Unary>
		@Expmult_Percent = 156,                    // <Exp Mult> ::= <Exp Mult> '%' <Exp Unary>
		@Expmult = 157,                            // <Exp Mult> ::= <Exp Unary>
		@Expunary_Exclam = 158,                    // <Exp Unary> ::= '!' <Value>
		@Expunary_Minus = 159,                     // <Exp Unary> ::= '-' <Value>
		@Expunary_Lparen_Rparen = 160,             // <Exp Unary> ::= '(' <Type> ')' <Exp Unary>
		@Expunary = 161,                           // <Exp Unary> ::= <Value>
		@Value = 162,                              // <Value> ::= <Value_Literal>
		@Value2 = 163,                             // <Value> ::= <Exp Rand>
		@Value3 = 164,                             // <Value> ::= <ValuePointer>
		@Value_Identifier_Lparen_Rparen = 165,     // <Value> ::= Identifier '(' <ExpressionList> ')'
		@Value_Identifier_Lparen_Rparen2 = 166,    // <Value> ::= Identifier '(' ')'
		@Value_Lparen_Rparen = 167,                // <Value> ::= '(' <Expression> ')'
		@Value_Plusplus = 168,                     // <Value> ::= <ValuePointer> '++'
		@Value_Minusminus = 169,                   // <Value> ::= <ValuePointer> '--'
		@Value_Plusplus2 = 170,                    // <Value> ::= '++' <ValuePointer>
		@Value_Minusminus2 = 171,                  // <Value> ::= '--' <ValuePointer>
		@Exprand_Rand = 172,                       // <Exp Rand> ::= rand
		@Exprand_Rand_Lbracket_Rbracket = 173,     // <Exp Rand> ::= rand '[' <Expression> ']'
		@Expressionlist_Comma = 174,               // <ExpressionList> ::= <ExpressionList> ',' <Expression>
		@Expressionlist = 175,                     // <ExpressionList> ::= <Expression>
		@Valuepointer_Identifier = 176,            // <ValuePointer> ::= Identifier
		@Valuepointer_Identifier_Lbracket_Rbracket = 177,  // <ValuePointer> ::= Identifier '[' <Expression> ']'
		@Valuepointer_Display_Lbracket_Comma_Rbracket = 178   // <ValuePointer> ::= display '[' <Expression> ',' <Expression> ']'
	}
}
