using BefunGen.AST;
using System;
using System.IO;
using System.Windows.Forms;

namespace BefunGen
{
	public partial class frmMain : Form
	{
		private DemoParser MyParser = new DemoParser();
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

		private void txtSource_TextChanged(object sender, EventArgs e)
		{
			doParse();
		}

		private void doParse()
		{
			if (loaded)
			{
				string refout1 = "";
				MyParser.Parse(new StringReader(txtSource.Document.Text), ref refout1, false);
				txtParseTree.Text = refout1;

				string refout2 = "";
				MyParser.Parse(new StringReader(txtSource.Document.Text), ref refout2, true);
				txtParseTrimTree.Text = refout2;

				BefunGen.AST.Program p = GParser.generateAST(txtSource.Document.Text);
				if (p == null)
					txtAST.Text = GParser.FailMessage;
				else
					txtAST.Text = p.getDebugString().Replace("\n", Environment.NewLine);
			}
			else
			{
				txtParseTree.Text = "Grammar not loaded";
				txtAST.Text = "Grammar not loaded";
			}
		}

		private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
		{
			doParse();
		}
	}
} //Form