namespace BefunGen
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.btnLoad = new System.Windows.Forms.Button();
			this.txtTableFile = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.txtSource = new Alsing.Windows.Forms.SyntaxBoxControl();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.txtParseTree = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.txtParseTrimTree = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtAST = new System.Windows.Forms.TextBox();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.txtGrammar = new System.Windows.Forms.TextBox();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.btnGen = new System.Windows.Forms.Button();
			this.btnRun = new System.Windows.Forms.Button();
			this.tabPage7 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtDebug = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.btnExecuteDebug = new System.Windows.Forms.Button();
			this.btnSendToRun = new System.Windows.Forms.Button();
			this.btnQCircle = new System.Windows.Forms.Button();
			this.btnFocus = new System.Windows.Forms.Button();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.btnDebugNumberRep = new System.Windows.Forms.Button();
			this.edNumberRep = new System.Windows.Forms.NumericUpDown();
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.btnSingleRep = new System.Windows.Forms.Button();
			this.edSingleRep = new System.Windows.Forms.NumericUpDown();
			this.tabPage8 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnHighlight = new System.Windows.Forms.Button();
			this.tcHighlight = new System.Windows.Forms.TabControl();
			this.tabPage9 = new System.Windows.Forms.TabPage();
			this.edHighlightCode = new System.Windows.Forms.TextBox();
			this.tabPage10 = new System.Windows.Forms.TabPage();
			this.edHighlighted = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.btnLoadSYN = new System.Windows.Forms.Button();
			this.txtSynFile = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.tabPage5.SuspendLayout();
			this.tabPage6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tabPage7.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.edNumberRep)).BeginInit();
			this.tableLayoutPanel8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.edSingleRep)).BeginInit();
			this.tabPage8.SuspendLayout();
			this.tableLayoutPanel7.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tcHighlight.SuspendLayout();
			this.tabPage9.SuspendLayout();
			this.tabPage10.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLoad
			// 
			this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoad.Enabled = false;
			this.btnLoad.Location = new System.Drawing.Point(589, 3);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(94, 26);
			this.btnLoad.TabIndex = 9;
			this.btnLoad.Text = "Load EGT";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// txtTableFile
			// 
			this.txtTableFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTableFile.Location = new System.Drawing.Point(3, 3);
			this.txtTableFile.Name = "txtTableFile";
			this.txtTableFile.Size = new System.Drawing.Size(580, 20);
			this.txtTableFile.TabIndex = 8;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 73);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.txtSource);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(686, 466);
			this.splitContainer1.SplitterDistance = 211;
			this.splitContainer1.TabIndex = 11;
			// 
			// txtSource
			// 
			this.txtSource.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
			this.txtSource.AutoListPosition = null;
			this.txtSource.AutoListSelectedText = "a123";
			this.txtSource.AutoListVisible = false;
			this.txtSource.BackColor = System.Drawing.Color.White;
			this.txtSource.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
			this.txtSource.CopyAsRTF = false;
			this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSource.FontName = "Courier new";
			this.txtSource.HighLightActiveLine = true;
			this.txtSource.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.txtSource.Indent = Alsing.Windows.Forms.SyntaxBox.IndentStyle.Smart;
			this.txtSource.InfoTipCount = 1;
			this.txtSource.InfoTipPosition = null;
			this.txtSource.InfoTipSelectedIndex = 1;
			this.txtSource.InfoTipVisible = false;
			this.txtSource.Location = new System.Drawing.Point(0, 0);
			this.txtSource.LockCursorUpdate = false;
			this.txtSource.Name = "txtSource";
			this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtSource.ShowGutterMargin = false;
			this.txtSource.ShowScopeIndicator = false;
			this.txtSource.ShowTabGuides = true;
			this.txtSource.Size = new System.Drawing.Size(686, 211);
			this.txtSource.SmoothScroll = true;
			this.txtSource.SplitView = false;
			this.txtSource.SplitviewH = -4;
			this.txtSource.SplitviewV = -4;
			this.txtSource.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
			this.txtSource.TabIndex = 5;
			this.txtSource.TabSize = 2;
			this.txtSource.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
			this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage5);
			this.tabControl1.Controls.Add(this.tabPage6);
			this.tabControl1.Controls.Add(this.tabPage7);
			this.tabControl1.Controls.Add(this.tabPage8);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(686, 251);
			this.tabControl1.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtParseTree);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(678, 225);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Reduction Tree";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// txtParseTree
			// 
			this.txtParseTree.BackColor = System.Drawing.SystemColors.Window;
			this.txtParseTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtParseTree.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtParseTree.Location = new System.Drawing.Point(3, 3);
			this.txtParseTree.Multiline = true;
			this.txtParseTree.Name = "txtParseTree";
			this.txtParseTree.ReadOnly = true;
			this.txtParseTree.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtParseTree.Size = new System.Drawing.Size(672, 219);
			this.txtParseTree.TabIndex = 6;
			this.txtParseTree.WordWrap = false;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.txtParseTrimTree);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(678, 225);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Trimmed Tree";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// txtParseTrimTree
			// 
			this.txtParseTrimTree.BackColor = System.Drawing.SystemColors.Window;
			this.txtParseTrimTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtParseTrimTree.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtParseTrimTree.Location = new System.Drawing.Point(3, 3);
			this.txtParseTrimTree.Multiline = true;
			this.txtParseTrimTree.Name = "txtParseTrimTree";
			this.txtParseTrimTree.ReadOnly = true;
			this.txtParseTrimTree.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtParseTrimTree.Size = new System.Drawing.Size(672, 219);
			this.txtParseTrimTree.TabIndex = 7;
			this.txtParseTrimTree.WordWrap = false;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtAST);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(678, 225);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "AST";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtAST
			// 
			this.txtAST.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAST.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAST.Location = new System.Drawing.Point(3, 3);
			this.txtAST.Multiline = true;
			this.txtAST.Name = "txtAST";
			this.txtAST.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAST.Size = new System.Drawing.Size(672, 219);
			this.txtAST.TabIndex = 0;
			this.txtAST.WordWrap = false;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.txtLog);
			this.tabPage4.Location = new System.Drawing.Point(4, 4);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(678, 225);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Log";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// txtLog
			// 
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Location = new System.Drawing.Point(3, 3);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.Size = new System.Drawing.Size(672, 219);
			this.txtLog.TabIndex = 0;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.txtGrammar);
			this.tabPage5.Location = new System.Drawing.Point(4, 4);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage5.Size = new System.Drawing.Size(678, 225);
			this.tabPage5.TabIndex = 4;
			this.tabPage5.Text = "Grammar";
			this.tabPage5.UseVisualStyleBackColor = true;
			// 
			// txtGrammar
			// 
			this.txtGrammar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtGrammar.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGrammar.Location = new System.Drawing.Point(3, 3);
			this.txtGrammar.Multiline = true;
			this.txtGrammar.Name = "txtGrammar";
			this.txtGrammar.ReadOnly = true;
			this.txtGrammar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtGrammar.Size = new System.Drawing.Size(672, 219);
			this.txtGrammar.TabIndex = 0;
			// 
			// tabPage6
			// 
			this.tabPage6.Controls.Add(this.splitContainer2);
			this.tabPage6.Location = new System.Drawing.Point(4, 4);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage6.Size = new System.Drawing.Size(678, 225);
			this.tabPage6.TabIndex = 5;
			this.tabPage6.Text = "Code";
			this.tabPage6.UseVisualStyleBackColor = true;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.txtCode);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel4);
			this.splitContainer2.Size = new System.Drawing.Size(672, 219);
			this.splitContainer2.SplitterDistance = 488;
			this.splitContainer2.TabIndex = 1;
			// 
			// txtCode
			// 
			this.txtCode.BackColor = System.Drawing.Color.White;
			this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtCode.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCode.Location = new System.Drawing.Point(0, 0);
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtCode.Size = new System.Drawing.Size(488, 219);
			this.txtCode.TabIndex = 0;
			this.txtCode.WordWrap = false;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.btnGen, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.btnRun, 0, 1);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 3;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.Size = new System.Drawing.Size(180, 219);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// btnGen
			// 
			this.btnGen.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnGen.Location = new System.Drawing.Point(3, 3);
			this.btnGen.Name = "btnGen";
			this.btnGen.Size = new System.Drawing.Size(174, 26);
			this.btnGen.TabIndex = 0;
			this.btnGen.Text = "Generate";
			this.btnGen.UseVisualStyleBackColor = true;
			this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
			// 
			// btnRun
			// 
			this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnRun.Location = new System.Drawing.Point(3, 35);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(174, 26);
			this.btnRun.TabIndex = 1;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// tabPage7
			// 
			this.tabPage7.Controls.Add(this.tableLayoutPanel1);
			this.tabPage7.Location = new System.Drawing.Point(4, 4);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage7.Size = new System.Drawing.Size(678, 225);
			this.tabPage7.TabIndex = 6;
			this.tabPage7.Text = "Debug";
			this.tabPage7.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.97619F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.02381F));
			this.tableLayoutPanel1.Controls.Add(this.txtDebug, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(672, 219);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// txtDebug
			// 
			this.txtDebug.BackColor = System.Drawing.Color.White;
			this.txtDebug.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDebug.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDebug.Location = new System.Drawing.Point(3, 3);
			this.txtDebug.Multiline = true;
			this.txtDebug.Name = "txtDebug";
			this.txtDebug.ReadOnly = true;
			this.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDebug.Size = new System.Drawing.Size(517, 213);
			this.txtDebug.TabIndex = 0;
			this.txtDebug.WordWrap = false;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Controls.Add(this.btnExecuteDebug, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.btnSendToRun, 0, 3);
			this.tableLayoutPanel5.Controls.Add(this.btnQCircle, 0, 4);
			this.tableLayoutPanel5.Controls.Add(this.btnFocus, 0, 5);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 2);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel8, 0, 1);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(526, 3);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 7;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(143, 213);
			this.tableLayoutPanel5.TabIndex = 1;
			// 
			// btnExecuteDebug
			// 
			this.btnExecuteDebug.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnExecuteDebug.Location = new System.Drawing.Point(3, 3);
			this.btnExecuteDebug.Name = "btnExecuteDebug";
			this.btnExecuteDebug.Size = new System.Drawing.Size(137, 29);
			this.btnExecuteDebug.TabIndex = 1;
			this.btnExecuteDebug.Text = "Execute";
			this.btnExecuteDebug.UseVisualStyleBackColor = true;
			this.btnExecuteDebug.Click += new System.EventHandler(this.btnExecuteDebug_Click);
			// 
			// btnSendToRun
			// 
			this.btnSendToRun.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnSendToRun.Location = new System.Drawing.Point(3, 108);
			this.btnSendToRun.Name = "btnSendToRun";
			this.btnSendToRun.Size = new System.Drawing.Size(137, 29);
			this.btnSendToRun.TabIndex = 3;
			this.btnSendToRun.Text = "Send To Run";
			this.btnSendToRun.UseVisualStyleBackColor = true;
			this.btnSendToRun.Click += new System.EventHandler(this.btnSendToRun_Click);
			// 
			// btnQCircle
			// 
			this.btnQCircle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnQCircle.Location = new System.Drawing.Point(3, 143);
			this.btnQCircle.Name = "btnQCircle";
			this.btnQCircle.Size = new System.Drawing.Size(137, 29);
			this.btnQCircle.TabIndex = 4;
			this.btnQCircle.Text = "Quarter Circle";
			this.btnQCircle.UseVisualStyleBackColor = true;
			this.btnQCircle.Click += new System.EventHandler(this.btnQCircle_Click);
			// 
			// btnFocus
			// 
			this.btnFocus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnFocus.Location = new System.Drawing.Point(3, 178);
			this.btnFocus.Name = "btnFocus";
			this.btnFocus.Size = new System.Drawing.Size(137, 29);
			this.btnFocus.TabIndex = 5;
			this.btnFocus.Text = "Select All";
			this.btnFocus.UseVisualStyleBackColor = true;
			this.btnFocus.Click += new System.EventHandler(this.btnFocus_Click);
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 2;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.Controls.Add(this.btnDebugNumberRep, 1, 0);
			this.tableLayoutPanel6.Controls.Add(this.edNumberRep, 0, 0);
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 73);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 1;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(137, 29);
			this.tableLayoutPanel6.TabIndex = 6;
			// 
			// btnDebugNumberRep
			// 
			this.btnDebugNumberRep.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnDebugNumberRep.Location = new System.Drawing.Point(71, 3);
			this.btnDebugNumberRep.Name = "btnDebugNumberRep";
			this.btnDebugNumberRep.Size = new System.Drawing.Size(63, 23);
			this.btnDebugNumberRep.TabIndex = 2;
			this.btnDebugNumberRep.Text = "Bench";
			this.btnDebugNumberRep.UseVisualStyleBackColor = true;
			this.btnDebugNumberRep.Click += new System.EventHandler(this.btnDebugNumberRep_Click);
			// 
			// edNumberRep
			// 
			this.edNumberRep.Location = new System.Drawing.Point(3, 3);
			this.edNumberRep.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.edNumberRep.Name = "edNumberRep";
			this.edNumberRep.Size = new System.Drawing.Size(62, 20);
			this.edNumberRep.TabIndex = 3;
			this.edNumberRep.Value = new decimal(new int[] {
            16384,
            0,
            0,
            0});
			// 
			// tableLayoutPanel8
			// 
			this.tableLayoutPanel8.ColumnCount = 2;
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel8.Controls.Add(this.btnSingleRep, 1, 0);
			this.tableLayoutPanel8.Controls.Add(this.edSingleRep, 0, 0);
			this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 38);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 1;
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(137, 29);
			this.tableLayoutPanel8.TabIndex = 7;
			// 
			// btnSingleRep
			// 
			this.btnSingleRep.Location = new System.Drawing.Point(71, 3);
			this.btnSingleRep.Name = "btnSingleRep";
			this.btnSingleRep.Size = new System.Drawing.Size(63, 23);
			this.btnSingleRep.TabIndex = 0;
			this.btnSingleRep.Text = "NumRep";
			this.btnSingleRep.UseVisualStyleBackColor = true;
			this.btnSingleRep.Click += new System.EventHandler(this.btnSingleRep_Click);
			// 
			// edSingleRep
			// 
			this.edSingleRep.Location = new System.Drawing.Point(3, 3);
			this.edSingleRep.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
			this.edSingleRep.Name = "edSingleRep";
			this.edSingleRep.Size = new System.Drawing.Size(62, 20);
			this.edSingleRep.TabIndex = 1;
			this.edSingleRep.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			// 
			// tabPage8
			// 
			this.tabPage8.Controls.Add(this.tableLayoutPanel7);
			this.tabPage8.Location = new System.Drawing.Point(4, 4);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage8.Size = new System.Drawing.Size(678, 225);
			this.tabPage8.TabIndex = 7;
			this.tabPage8.Text = "Highlight";
			this.tabPage8.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 2;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
			this.tableLayoutPanel7.Controls.Add(this.flowLayoutPanel1, 1, 0);
			this.tableLayoutPanel7.Controls.Add(this.tcHighlight, 0, 0);
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(672, 219);
			this.tableLayoutPanel7.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.btnHighlight);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(595, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(74, 213);
			this.flowLayoutPanel1.TabIndex = 0;
			// 
			// btnHighlight
			// 
			this.btnHighlight.Location = new System.Drawing.Point(3, 3);
			this.btnHighlight.Name = "btnHighlight";
			this.btnHighlight.Size = new System.Drawing.Size(65, 23);
			this.btnHighlight.TabIndex = 0;
			this.btnHighlight.Text = "Highlight";
			this.btnHighlight.UseVisualStyleBackColor = true;
			this.btnHighlight.Click += new System.EventHandler(this.btnHighlight_Click);
			// 
			// tcHighlight
			// 
			this.tcHighlight.Controls.Add(this.tabPage9);
			this.tcHighlight.Controls.Add(this.tabPage10);
			this.tcHighlight.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcHighlight.Location = new System.Drawing.Point(3, 3);
			this.tcHighlight.Name = "tcHighlight";
			this.tcHighlight.SelectedIndex = 0;
			this.tcHighlight.Size = new System.Drawing.Size(586, 213);
			this.tcHighlight.TabIndex = 1;
			// 
			// tabPage9
			// 
			this.tabPage9.Controls.Add(this.edHighlightCode);
			this.tabPage9.Location = new System.Drawing.Point(4, 22);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage9.Size = new System.Drawing.Size(578, 187);
			this.tabPage9.TabIndex = 0;
			this.tabPage9.Text = "Code";
			this.tabPage9.UseVisualStyleBackColor = true;
			// 
			// edHighlightCode
			// 
			this.edHighlightCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edHighlightCode.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edHighlightCode.Location = new System.Drawing.Point(3, 3);
			this.edHighlightCode.Multiline = true;
			this.edHighlightCode.Name = "edHighlightCode";
			this.edHighlightCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.edHighlightCode.Size = new System.Drawing.Size(572, 181);
			this.edHighlightCode.TabIndex = 1;
			// 
			// tabPage10
			// 
			this.tabPage10.Controls.Add(this.edHighlighted);
			this.tabPage10.Location = new System.Drawing.Point(4, 22);
			this.tabPage10.Name = "tabPage10";
			this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage10.Size = new System.Drawing.Size(578, 187);
			this.tabPage10.TabIndex = 1;
			this.tabPage10.Text = "Highlight";
			this.tabPage10.UseVisualStyleBackColor = true;
			// 
			// edHighlighted
			// 
			this.edHighlighted.Dock = System.Windows.Forms.DockStyle.Fill;
			this.edHighlighted.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edHighlighted.Location = new System.Drawing.Point(3, 3);
			this.edHighlighted.Multiline = true;
			this.edHighlighted.Name = "edHighlighted";
			this.edHighlighted.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.edHighlighted.Size = new System.Drawing.Size(572, 181);
			this.edHighlighted.TabIndex = 2;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(692, 542);
			this.tableLayoutPanel2.TabIndex = 10;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel3.Controls.Add(this.btnLoad, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.txtTableFile, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.btnLoadSYN, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.txtSynFile, 0, 1);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 2;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(686, 64);
			this.tableLayoutPanel3.TabIndex = 12;
			// 
			// btnLoadSYN
			// 
			this.btnLoadSYN.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoadSYN.Enabled = false;
			this.btnLoadSYN.Location = new System.Drawing.Point(589, 35);
			this.btnLoadSYN.Name = "btnLoadSYN";
			this.btnLoadSYN.Size = new System.Drawing.Size(94, 26);
			this.btnLoadSYN.TabIndex = 10;
			this.btnLoadSYN.Text = "Load SYN";
			this.btnLoadSYN.UseVisualStyleBackColor = true;
			this.btnLoadSYN.Click += new System.EventHandler(this.btnLoadSYN_Click);
			// 
			// txtSynFile
			// 
			this.txtSynFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSynFile.Location = new System.Drawing.Point(3, 35);
			this.txtSynFile.Name = "txtSynFile";
			this.txtSynFile.Size = new System.Drawing.Size(580, 20);
			this.txtSynFile.TabIndex = 11;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(692, 542);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMain";
			this.Text = "Draw Parse Tree";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.tabPage3.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.tabPage5.ResumeLayout(false);
			this.tabPage5.PerformLayout();
			this.tabPage6.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tabPage7.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.edNumberRep)).EndInit();
			this.tableLayoutPanel8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.edSingleRep)).EndInit();
			this.tabPage8.ResumeLayout(false);
			this.tableLayoutPanel7.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.tcHighlight.ResumeLayout(false);
			this.tabPage9.ResumeLayout(false);
			this.tabPage9.PerformLayout();
			this.tabPage10.ResumeLayout(false);
			this.tabPage10.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.Button btnLoad;
		internal System.Windows.Forms.TextBox txtTableFile;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button btnLoadSYN;
		private System.Windows.Forms.TextBox txtSynFile;
		internal Alsing.Windows.Forms.SyntaxBoxControl txtSource;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		internal System.Windows.Forms.TextBox txtParseTree;
		private System.Windows.Forms.TabPage tabPage3;
		internal System.Windows.Forms.TextBox txtParseTrimTree;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox txtAST;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.TabPage tabPage5;
		private System.Windows.Forms.TextBox txtGrammar;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.TextBox txtDebug;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button btnGen;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.Button btnExecuteDebug;
		private System.Windows.Forms.Button btnDebugNumberRep;
		private System.Windows.Forms.Button btnSendToRun;
		private System.Windows.Forms.Button btnQCircle;
		private System.Windows.Forms.Button btnFocus;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.NumericUpDown edNumberRep;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button btnHighlight;
		private System.Windows.Forms.TextBox edHighlightCode;
		private System.Windows.Forms.TabControl tcHighlight;
		private System.Windows.Forms.TabPage tabPage9;
		private System.Windows.Forms.TabPage tabPage10;
		private System.Windows.Forms.TextBox edHighlighted;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.Button btnSingleRep;
		internal System.Windows.Forms.NumericUpDown edSingleRep;
	}
}

