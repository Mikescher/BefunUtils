﻿//Generated by the GOLD Parser Builder

using System;
using System.IO;
using System.Text;

internal class DemoParser
{
	private GOLD.Parser parser = new GOLD.Parser();

	public GOLD.Reduction Root;     //Store the top of the tree
	public string FailMessage;

	public long Time;

	public bool Setup(BinaryReader FilePath)
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

	public bool Parse(TextReader reader, ref string tree, bool Trim)
	{
		Time = Environment.TickCount;

		GOLD.ParseMessage response;
		bool done;                      //Controls when we leave the loop
		bool accepted = false;          //Was the parse successful?

		parser.Open(reader);
		parser.TrimReductions = Trim;  //Please read about this feature before enabling

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
					Root = (GOLD.Reduction)parser.CurrentReduction;    //The root node!
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

		if (accepted)
		{
			tree = DrawReductionTree(Root);
		}
		else
		{
			tree = FailMessage;
		}

		Time = Environment.TickCount - Time;

		return accepted;
	}

	private string DrawReductionTree(GOLD.Reduction Root)
	{
		//This procedure starts the recursion that draws the parse tree.
		StringBuilder tree = new StringBuilder();

		tree.AppendLine("+-" + Root.Parent.Text(false));
		DrawReduction(tree, Root, 1);

		return tree.ToString();
	}

	private void DrawReduction(StringBuilder tree, GOLD.Reduction reduction, int indent)
	{
		//This is a simple recursive procedure that draws an ASCII version of the parse
		//tree

		int n;
		string indentText = "";

		for (n = 1; n <= indent; n++)
		{
			indentText += "| ";
		}

		//=== Display the children of the reduction
		for (n = 0; n < reduction.Count(); n++)
		{
			switch (reduction[n].Type())
			{
				case GOLD.SymbolType.Nonterminal:
					GOLD.Reduction branch = (GOLD.Reduction)reduction[n].Data;

					tree.AppendLine(indentText + "+-" + branch.Parent.Text(false));
					DrawReduction(tree, branch, indent + 1);
					break;

				default:
					string leaf = (string)reduction[n].Data;

					tree.AppendLine(indentText + "+-" + leaf);
					break;
			}
		}
	}
}; //MyParser