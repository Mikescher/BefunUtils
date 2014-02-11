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

		private void btnLoadSYN_Click(object sender, EventArgs e)
		{
			doLoadSYN();
		}

		private void doLoadSYN()
		{
			try
			{
				txtSource.Document.SyntaxFile = txtSynFile.Text;
				txtSource.Document.ParseAll();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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
			if (path.Contains("BefunGen"))
				path = path.Substring(0, path.LastIndexOf("BefunGen"));
			path = Path.Combine(path, "BefunGen");
			path = Path.Combine(path, "TextFunge");

			txtTableFile.Text = Path.Combine(path, "TextFunge.egt");

			if (File.Exists(txtTableFile.Text))
				doLoad();
			else
				txtLog.AppendText(string.Format("EGT not found: {0} \r\n", txtTableFile.Text));

			//#########

			txtSynFile.Text = Path.Combine(path, "TextFunge.syn");
			if (File.Exists(txtSynFile.Text))
				doLoadSYN();
			else
				txtLog.AppendText(string.Format("Syntaxfile not found: {0} \r\n", txtSynFile.Text));

			string path_tf = Path.Combine(path, "example_00.tf");
			if (File.Exists(path_tf))
				txtSource.Document.Text = File.ReadAllText(path_tf);
			else
				txtLog.AppendText(string.Format("Example not found: {0} \r\n", path_tf));
		}

		private void txtSource_TextChanged(object sender, EventArgs e)
		{
			doParse();
		}

		private void doParse()
		{
			if (loaded)
			{
				txtLog.Clear();

				string refout1 = "";
				MyParser.Parse(new StringReader(txtSource.Document.Text), ref refout1, false);
				txtParseTree.Text = refout1;
				txtLog.AppendText(string.Format("Reduction Tree: {0}ms\r\n", MyParser.Time));

				string refout2 = "";
				MyParser.Parse(new StringReader(txtSource.Document.Text), ref refout2, true);
				txtParseTrimTree.Text = refout2;
				txtLog.AppendText(string.Format("Trimmed Reduction Tree: {0}ms\r\n", MyParser.Time));

				BefunGen.AST.Program p = GParser.generateAST(txtSource.Document.Text);
				if (p == null)
					txtAST.Text = GParser.FailMessage;
				else
				{
					txtLog.AppendText(string.Format("AST-Gen: {0}ms\r\n", MyParser.Time));

					long gdst = Environment.TickCount;
					string debug = p.getDebugString().Replace("\n", Environment.NewLine);
					gdst = Environment.TickCount - gdst;

					txtAST.Text = debug;

					txtLog.AppendText(string.Format("AST-DebugOut: {0}ms\r\n", gdst));
				}
			}
			else
			{
				txtParseTree.Text = "Grammar not loaded";
				txtParseTrimTree.Text = "Grammar not loaded";
				txtAST.Text = "Grammar not loaded";
			}
		}

		private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
		{
			doParse();
		}
	}
} //Form