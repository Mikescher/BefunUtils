﻿//Generated by the GOLD Parser Builder

using System;
using System.IO;

class MyParserClass
{
    private GOLD.Parser parser = new GOLD.Parser();

    public GOLD.Reduction Root;     //Store the top of the tree
    public string FailMessage;

    public bool Setup(string FilePath)
    {
		try
		{
			 return parser.LoadTables(FilePath);
		}
		catch (Exception e)
		{
			FailMessage = e.Message;
			return false;
		}
       
    }

    public bool Parse(TextReader reader)
    {
        //This procedure starts the GOLD Parser Engine and handles each of the
        //messages it returns. Each time a reduction is made, you can create new
        //custom object and reassign the .CurrentReduction property. Otherwise, 
        //the system will use the Reduction object that was returned.
        //
        //The resulting tree will be a pure representation of the language 
        //and will be ready to implement.

        GOLD.ParseMessage response;
        bool done;                      //Controls when we leave the loop
        bool accepted = false;          //Was the parse successful?
        
        parser.Open(reader);
        parser.TrimReductions = false;  //Please read about this feature before enabling  

        done = false;
        while (!done)
        {
            response = parser.Parse();

            switch (response)
            {
                case GOLD.ParseMessage.LexicalError:
                    //Cannot recognize token
					FailMessage = "Lexical Error:" + Environment.NewLine +
								  "Position: " + (parser.CurrentPosition().Line + 1) + ", " + parser.CurrentPosition().Column + Environment.NewLine + 
                                  "Read: " + parser.CurrentToken().Data;
                    done = true;
                    break;

                case GOLD.ParseMessage.SyntaxError:
                    //Expecting a different token
                    FailMessage = "Syntax Error:" + Environment.NewLine +
								  "Position: " + (parser.CurrentPosition().Line + 1) + ", " + parser.CurrentPosition().Column + Environment.NewLine +
								  "Read: " + parser.CurrentToken().Data + Environment.NewLine + 
                                  "Expecting: " + parser.ExpectedSymbols().Text();
                    done = true;
                    break;

                case GOLD.ParseMessage.Reduction:
					GOLD.Reduction r = (GOLD.Reduction)parser.CurrentReduction;
                    break;

                case GOLD.ParseMessage.Accept:
                    //Accepted!
                    Root = (GOLD.Reduction) parser.CurrentReduction;    //The root node!                                  
                    done = true;
                    accepted = true;
                    break;

                case GOLD.ParseMessage.TokenRead:
                    //You don't have to do anything here.
                    break;

                case GOLD.ParseMessage.InternalError:
                    //INTERNAL ERROR! Something is horribly wrong.
					FailMessage = "Internal Error";
                    done = true;
                    break;

                case GOLD.ParseMessage.NotLoadedError:
                    //This error occurs if the CGT was not loaded.                   
                    FailMessage = "Tables not loaded";
                    done = true;
                    break;

                case GOLD.ParseMessage.GroupError:
                    //GROUP ERROR! Unexpected end of file
                    FailMessage = "Runaway group";
                    done = true;
                    break;
            }
        } //while

        return accepted;
    }

}; //MyParser
