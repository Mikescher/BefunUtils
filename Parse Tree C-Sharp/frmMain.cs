using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Parse_Tree_C_Sharp
{
    public partial class frmMain : Form
    {
        MyParserClass MyParser = new MyParserClass();

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //This procedure can be called to load the parse tables. The class can
            //read tables using a BinaryReader.
            try
            {
                if (MyParser.Setup(txtTableFile.Text))
                {
                    //Change button enable/disable for the user
                    btnLoad.Enabled = false;
                    btnParse.Enabled = true;
                }
                else
                {
                    MessageBox.Show("CGT failed to load");
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
            btnParse.Enabled = false;

            txtTableFile.Text = @"E:\Eigene Dateien\Dropbox\Eigene EDV\Visual Studio\Projects\BefunGen\TextFunge\TextFunge.egt";
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
            if (!btnParse.Enabled)
                return;

            btnParse.Enabled = false;

            if (MyParser.Parse(new StringReader(txtSource.Text)))
            {
                DrawReductionTree(MyParser.Root);
            }
            else
            {
                txtParseTree.Text = MyParser.FailMessage;
            }

            btnParse.Enabled = true;
        }


    }
} //Form
