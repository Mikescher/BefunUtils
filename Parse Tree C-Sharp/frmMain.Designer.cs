namespace Parse_Tree_C_Sharp
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
			this.btnLoad = new System.Windows.Forms.Button();
			this.txtTableFile = new System.Windows.Forms.TextBox();
			this.txtParseTree = new System.Windows.Forms.TextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtSource = new Alsing.Windows.Forms.SyntaxBoxControl();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtAST = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLoad
			// 
			this.btnLoad.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoad.Location = new System.Drawing.Point(651, 3);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(94, 25);
			this.btnLoad.TabIndex = 9;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// txtTableFile
			// 
			this.txtTableFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTableFile.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTableFile.Location = new System.Drawing.Point(3, 3);
			this.txtTableFile.Name = "txtTableFile";
			this.txtTableFile.Size = new System.Drawing.Size(642, 21);
			this.txtTableFile.TabIndex = 8;
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
			this.txtParseTree.Size = new System.Drawing.Size(734, 278);
			this.txtParseTree.TabIndex = 6;
			this.txtParseTree.WordWrap = false;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 40);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Size = new System.Drawing.Size(748, 625);
			this.splitContainer1.SplitterDistance = 311;
			this.splitContainer1.TabIndex = 11;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.txtSource, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(748, 311);
			this.tableLayoutPanel1.TabIndex = 7;
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
			this.txtSource.Location = new System.Drawing.Point(3, 3);
			this.txtSource.LockCursorUpdate = false;
			this.txtSource.Name = "txtSource";
			this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtSource.ShowGutterMargin = false;
			this.txtSource.ShowScopeIndicator = false;
			this.txtSource.ShowTabGuides = true;
			this.txtSource.Size = new System.Drawing.Size(742, 305);
			this.txtSource.SmoothScroll = false;
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
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(748, 310);
			this.tabControl1.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.txtParseTree);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(740, 284);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Reduction Tree";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.txtAST);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(740, 284);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "AST";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtAST
			// 
			this.txtAST.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtAST.Location = new System.Drawing.Point(3, 3);
			this.txtAST.Multiline = true;
			this.txtAST.Name = "txtAST";
			this.txtAST.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAST.Size = new System.Drawing.Size(734, 278);
			this.txtAST.TabIndex = 0;
			this.txtAST.WordWrap = false;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(754, 668);
			this.tableLayoutPanel2.TabIndex = 10;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel3.Controls.Add(this.btnLoad, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.txtTableFile, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(748, 31);
			this.tableLayoutPanel3.TabIndex = 12;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(754, 668);
			this.Controls.Add(this.tableLayoutPanel2);
			this.Name = "frmMain";
			this.Text = "Draw Parse Tree";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.Button btnLoad;
		internal System.Windows.Forms.TextBox txtTableFile;
		internal System.Windows.Forms.TextBox txtParseTree;
		internal Alsing.Windows.Forms.SyntaxBoxControl txtSource;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox txtAST;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	}
}

