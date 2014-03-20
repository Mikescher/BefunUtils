﻿using BefunGen.AST;
using BefunGen.AST.CodeGen;
using BefunGen.AST.CodeGen.NumberCode;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace BefunGen
{
	public partial class frmMain : Form
	{
		private DemoParser MyParser = new DemoParser();
		private TextFungeParser GParser = new TextFungeParser();

		private bool loaded = false;

		private string currentSC = "";
		private Thread parseThread;
		bool threadRunning = true;

		public frmMain() //TODO Use AvalonEdit And the whole SharpCode Library for these things ... (http://www.codeproject.com/Articles/42490/Using-AvalonEdit-WPF-Text-Editor)
		{
			InitializeComponent();
			tabControl1.SelectedIndex = 6;
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
				if (MyParser.Setup(new BinaryReader(new MemoryStream(GParser.getGrammar()))))
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
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			txtTableFile.Text = Path.Combine(Application.StartupPath, "TextFunge.egt");

			if (File.Exists(txtTableFile.Text))
				doLoad();
			else
				txtLog.AppendText(string.Format("EGT not found: {0} \r\n", txtTableFile.Text));

			//#########

			txtSynFile.Text = Path.Combine(Application.StartupPath, "TextFunge.syn");
			if (File.Exists(txtSynFile.Text))
				doLoadSYN();
			else
				txtLog.AppendText(string.Format("Syntaxfile not found: {0} \r\n", txtSynFile.Text));

			string path_tf = Path.Combine(Application.StartupPath, "example_00.tf");
			if (File.Exists(path_tf))
			{
				txtSource.Document.Text = File.ReadAllText(path_tf);
				currentSC = txtSource.Document.Text;
			}
			else
				txtLog.AppendText(string.Format("Example not found: {0} \r\n", path_tf));

			//##########

			doLoad();

			//##########

			parseThread = new Thread(work);
			parseThread.Start();
		}

		private void txtSource_TextChanged(object sender, EventArgs e)
		{
			currentSC = txtSource.Document.Text;
		}

		private Tuple<string, string, string, string, string> doParse(string txt)
		{
			long time_full = Environment.TickCount;

			string redtree = "";
			string trimtree = "";
			string asttree = "";
			string log = "";
			string grammar = "";

			if (loaded)
			{
				string refout1 = "";
				MyParser.Parse(new StringReader(txt), ref refout1, false);
				redtree = refout1;
				log += (string.Format("Reduction Tree: {0}ms\r\n", MyParser.Time));

				string refout2 = "";
				MyParser.Parse(new StringReader(txt), ref refout2, true);
				trimtree = refout2;
				log += (string.Format("Trimmed Reduction Tree: {0}ms\r\n", MyParser.Time));

				BefunGen.AST.Program p = GParser.generateAST(txt);
				if (p == null)
					asttree = GParser.FailMessage;
				else
				{
					log += (string.Format("AST-Gen: {0}ms\r\n", MyParser.Time));

					long gdst = Environment.TickCount;
					string debug = p.getDebugString().Replace("\n", Environment.NewLine);
					gdst = Environment.TickCount - gdst;

					asttree = debug;

					log += (string.Format("AST-DebugOut: {0}ms\r\n", gdst));
				}

				grammar = GParser.getGrammarDefinition();
			}
			else
			{
				redtree = "Grammar not loaded";
				trimtree = "Grammar not loaded";
				asttree = "Grammar not loaded";
			}

			time_full = Environment.TickCount - time_full;

			log += "\r\n----------\r\n";
			log += (string.Format("Parse: {0}ms\r\n", time_full));

			return Tuple.Create(redtree, trimtree, asttree, log, grammar);
		}

		private void txtSource_KeyPress(object sender, KeyPressEventArgs e)
		{
			currentSC = txtSource.Document.Text;
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			threadRunning = false;
		}

		private void work()
		{
			string currentTxt = null;

			while (threadRunning)
			{
				string newtxt = currentSC;

				if (newtxt != currentTxt)
				{
					bool hasChangedAgain = true;
					while (hasChangedAgain)
					{
						Thread.Sleep(500);
						hasChangedAgain = (currentSC != newtxt);
						newtxt = currentSC;
					}

					var result = doParse(newtxt);

					txtParseTree.SetPropertyThreadSafe(() => txtParseTree.Text, result.Item1);
					txtParseTrimTree.SetPropertyThreadSafe(() => txtParseTrimTree.Text, result.Item2);
					txtAST.SetPropertyThreadSafe(() => txtAST.Text, result.Item3);
					txtLog.SetPropertyThreadSafe(() => txtLog.Text, result.Item4);
					txtGrammar.SetPropertyThreadSafe(() => txtGrammar.Text, result.Item5);

					currentTxt = newtxt;
				}
				else
				{
					Thread.Sleep(100);
				}
			}
		}

		private Expression parseExpression(string expr)
		{
			string txt = String.Format("program b var  bool a; begin a = (bool)({0}); end end", expr);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return ((Expression_Cast)((Statement_Assignment)((Statement_StatementList)p.MainMethod.Body).List[0]).Expr).Expr;
		}

		private Statement parseStatement(string stmt)
		{
			string txt = String.Format("program b var  bool a; begin {0} end end", stmt);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return ((Statement_StatementList)p.MainMethod.Body).List[0];
		}

		private Method parseMethod(string meth)
		{
			string txt = String.Format("program b begin end {0} end", meth);
			BefunGen.AST.Program p = GParser.generateAST(txt);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p.MethodList[1];
		}

		private BefunGen.AST.Program parseProgram(string prog)
		{
			BefunGen.AST.Program p = GParser.generateAST(prog);

			if (p == null)
				throw new Exception(GParser.FailMessage);

			return p;
		}

		private void debugExpression(string expr)
		{
			txtDebug.Clear();

			try
			{
				expr = expr.Replace(@"''", "\"");

				txtDebug.Text += expr + Environment.NewLine;

				Expression e = parseExpression(expr);
				CodePiece pc = e.generateCode(false);
				txtDebug.Text += pc.ToString() + Environment.NewLine;
			}
			catch (Exception e)
			{
				txtDebug.Text += e.ToString();
			}
		}

		private void debugStatement(string stmt)
		{
			txtDebug.Clear();

			try
			{
				stmt = stmt.Replace(@"''", "\"");

				txtDebug.Text += stmt + Environment.NewLine;

				Statement e = parseStatement(stmt);
				CodePiece pc = e.generateCode(false);
				txtDebug.Text += pc.ToString() + Environment.NewLine;
			}
			catch (Exception e)
			{
				txtDebug.Text += e.ToString();
			}
		}

		private void debugMethod(string meth)
		{
			txtDebug.Clear();

			try
			{
				meth = meth.Replace(@"''", "\"");

				meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]+[\r\n]{1,2}", "\r\n");
				meth = Regex.Replace(meth, @"^[ \t]*[\r\n]{1,2}", "");
				meth = Regex.Replace(meth, @"[\r\n]{1,2}[ \t]*$", "");

				Method e = parseMethod(meth);
				txtDebug.Text += "[METHOD] " + e.Identifier + ":" + e.MethodAddr + Environment.NewLine;
				CodePiece pc = e.generateCode(0, 0);
				txtDebug.Text += pc.ToString() + Environment.NewLine;
			}
			catch (Exception e)
			{
				txtDebug.Text += e.ToString();
			}
		}

		private void debugProgram(string prog)
		{
			txtDebug.Clear();

			try
			{
				prog = prog.Replace(@"''", "\"");

				BefunGen.AST.Program e = parseProgram(prog);
				txtDebug.Text += "[PROGRAM] " + e.Identifier + Environment.NewLine;
				CodePiece pc = e.generateCode();
				txtDebug.Text += pc.ToString() + Environment.NewLine;
			}
			catch (Exception e)
			{
				txtDebug.Text += e.ToString();
				//txtDebug.Text += Regex.Replace(e.ToString().Replace(" in ", Environment.NewLine + "      in "), @"in.*BefunGen\\", "in ");
				//txtDebug.Text += e.ToString().Replace("in", Environment.NewLine + "      in").Replace(@"e:\Eigene Dateien\Dropbox\Eigene EDV\Visual Studio\Projects\BefunGen\BefunGen\", "");
			}
		}

		private void btnExecuteDebug_Click(object sender, EventArgs earg) //TODO Allow '_' in identifier
		{
			debugProgram(@"
program example
	const
		int FALSCH := 0;
		int WAHR   := 1;
	global
		int i;
	begin
		i = FALSCH;
		
		doodle();
		
		OUT i;
	end

	void doodle() 
	begin
		i = 10;
		
		doodle2();
	end
	 
	void doodle2() 
	begin
		i = i * 10;
		
		doodle3();
	end
	 
	void doodle3() 
	begin
		i = i - WAHR;
	end
end
");
		}

		private void btnDebugNumberRep_Click(object sender, EventArgs e)
		{
			string bench = NumberCodeHelper.generateBenchmark(16384, true);

			txtDebug.Text = bench;

			txtDebug.Focus();
			txtDebug.SelectAll();
		}

		private void btnRun_Click(object sender, EventArgs e)
		{
			string txt = txtCode.Text;

			int bstart = txt.IndexOf('{');
			int bend = txt.LastIndexOf('}');

			if (!(bend > bstart && bend >= 0 && bstart >= 0))
			{
				return;
			}
			
			txt = txt.Substring(bstart + 1, bend - bstart - 1);
			txt = txt.Trim(' ', '\r', '\n', '\t');

			try
			{
				string code = txt;

				string path = Path.Combine(Application.StartupPath, "code_tmp.b93");

				File.WriteAllText(path, code);

				//-----

				ProcessStartInfo start = new ProcessStartInfo();

				start.Arguments = String.Format("-file=\"{0}\"", path);
				start.FileName = Path.Combine(Application.StartupPath, "BefungExec.exe");

				Process.Start(start);
			}
			catch (Exception ex)
			{
				txtCode.Text = ex.ToString();
			}
		}

		private void btnGen_Click(object sender, EventArgs e)
		{
			BefunGen.AST.Program p = GParser.generateAST(txtSource.Document.Text);
			if (p == null)
			{
				txtCode.Text = GParser.FailMessage;
			}
			else
			{
				try
				{
					txtCode.Text = p.generateCode().ToString();
				}
				catch (Exception ex)
				{
					txtCode.Text = ex.ToString();
				}
			}
		}

		private void btnSendToRun_Click(object sender, EventArgs e)
		{
			string txt = txtDebug.Text;

			int start = txt.IndexOf('{');
			int end = txt.LastIndexOf('}');

			if (end > start && end >= 0 && start >= 0)
			{
				txtCode.Text = txtDebug.Text;
				txtCode.Select(0, 0);

				tabControl1.SelectedIndex = 5;
			}
		}
	}
} //Form