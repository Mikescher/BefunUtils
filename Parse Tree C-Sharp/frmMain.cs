using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Parse_Tree_C_Sharp
{
    public partial class frmMain : Form
    {
        MyParserClass MyParser = new MyParserClass();

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
				if (MyParser.Setup(txtTableFile.Text))
				{
					loaded = true;
				}
				else
				{
					txtParseTree.Text = MyParser.FailMessage + Environment.NewLine + "CGT failed to load";
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

			String path_egt = Application.StartupPath;
			path_egt = path_egt.Substring(0, path_egt.LastIndexOf("BefunGen"));
			path_egt = Path.Combine(path_egt, "BefunGen");
			path_egt = Path.Combine(path_egt, "TextFunge");
			path_egt = Path.Combine(path_egt, "TextFunge.egt");

			txtTableFile.Text = path_egt;

			if (File.Exists(path_egt))
				doLoad();

			//#########

			String path_tf = Application.StartupPath;
			path_tf = path_tf.Substring(0, path_tf.LastIndexOf("BefunGen"));
			path_tf = Path.Combine(path_tf, "BefunGen");
			path_tf = Path.Combine(path_tf, "TextFunge");
			path_tf = Path.Combine(path_tf, "example_00.tf");

			if (File.Exists(path_tf))
				txtSource.Text = File.ReadAllText(path_tf);
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
				if (MyParser.Parse(new StringReader(txtSource.Text)))
				{
					DrawReductionTree(MyParser.Root);
				}
				else
				{
					txtParseTree.Text = MyParser.FailMessage;
				}

				BefunGen.AST.GOLDParser gp = new BefunGen.AST.GOLDParser(new BinaryReader(new FileStream(txtTableFile.Text, FileMode.Open)));
				BefunGen.AST.Program p = gp.generateAST(txtSource.Text);
				if (p == null)
				{
					txtParseTree.Text += Environment.NewLine;
					txtParseTree.Text += Environment.NewLine;
					txtParseTree.Text += new string('#', 16);
					txtParseTree.Text += Environment.NewLine;
					txtParseTree.Text += Environment.NewLine;
					txtParseTree.Text += gp.FailMessage;
				}
			}
			else
			{
				txtParseTree.Text = "Grammar not loaded";
			}
        }

		private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Tab)
			{
				e.Handled = true;
				txtSource.SelectedText = new string(' ', 2);
			}

			doParse();
		}

    }
} //Form
