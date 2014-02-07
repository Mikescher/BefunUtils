using BefunGen.AST;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Parse_Tree_C_Sharp
{
	public partial class frmMain : Form
	{
		private MyParserClass MyParser = new MyParserClass();
		private TextFungeParser GParser = new TextFungeParser();

		private bool loaded = false;

		public frmMain()
		{
			InitializeComponent();
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			doLoad();
		}

		private void doLoad()
		{
			try
			{
				if (MyParser.Setup(txtTableFile.Text) && GParser.loadTables(txtTableFile.Text))
				{
					loaded = true;
				}
				else
				{
					txtParseTree.Text = MyParser.FailMessage + Environment.NewLine + "CGT failed to load";
					txtAST.Text = GParser.FailMessage + Environment.NewLine + "CGT failed to load";
				}
			}
			catch (GOLD.ParserException ex)
			{
				MessageBox.Show(ex.Message);
			}

			doParse();
		}

		private void btnParse_Click(object sender, EventArgs e)
		{
			doParse();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			btnLoad.Enabled = true;

			String path = Application.StartupPath;
			path = path.Substring(0, path.LastIndexOf("BefunGen"));
			path = Path.Combine(path, "BefunGen");
			path = Path.Combine(path, "TextFunge");

			txtTableFile.Text = Path.Combine(path, "TextFunge.egt");

			if (File.Exists(txtTableFile.Text))
				doLoad();

			//#########

			txtSource.Document.SyntaxFile = Path.Combine(path, "TextFunge.syn");

			string path_tf = Path.Combine(path, "example_00.tf");
			if (File.Exists(path_tf))
				txtSource.Document.Text = File.ReadAllText(path_tf);
		}

		private void DrawReductionTree(GOLD.Reduction Root)
		{
			//This procedure starts the recursion that draws the parse tree.
			StringBuilder tree = new StringBuilder();

			tree.AppendLine("+-" + Root.Parent.Text(false));
			DrawReduction(tree, Root, 1);

			txtParseTree.Text = tree.ToString();
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

		private void txtSource_TextChanged(object sender, EventArgs e)
		{
			doParse();
		}

		private void doParse()
		{
			if (loaded)
			{
				if (MyParser.Parse(new StringReader(txtSource.Document.Text)))
				{
					DrawReductionTree(MyParser.Root);
				}
				else
				{
					txtParseTree.Text = MyParser.FailMessage;
				}

				BefunGen.AST.Program p = GParser.generateAST(txtSource.Document.Text);
				if (p == null)
				{
					txtAST.Text = GParser.FailMessage;
				}
				else
				{
					txtAST.Text = "Succesful";
				}
			}
			else
			{
				txtParseTree.Text = "Grammar not loaded";
				txtAST.Text       = "Grammar not loaded";
			}
		}

		private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
		{
			doParse();
		}

	}
} //Form
