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
			this.btnLoad = new System.Windows.Forms.Button();
			this.txtTableFile = new System.Windows.Forms.TextBox();
			this.txtParseTree = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.lineNumbers_For_RichTextBox1 = new LineNumbers.LineNumbers_For_RichTextBox();
			this.txtSource = new System.Windows.Forms.RichTextBox();
			this.panel1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(578, 5);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(96, 32);
			this.btnLoad.TabIndex = 9;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// txtTableFile
			// 
			this.txtTableFile.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTableFile.Location = new System.Drawing.Point(8, 11);
			this.txtTableFile.Name = "txtTableFile";
			this.txtTableFile.Size = new System.Drawing.Size(564, 21);
			this.txtTableFile.TabIndex = 8;
			// 
			// txtParseTree
			// 
			this.txtParseTree.BackColor = System.Drawing.SystemColors.Window;
			this.txtParseTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtParseTree.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtParseTree.Location = new System.Drawing.Point(0, 0);
			this.txtParseTree.Multiline = true;
			this.txtParseTree.Name = "txtParseTree";
			this.txtParseTree.ReadOnly = true;
			this.txtParseTree.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtParseTree.Size = new System.Drawing.Size(684, 272);
			this.txtParseTree.TabIndex = 6;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtTableFile);
			this.panel1.Controls.Add(this.btnLoad);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(684, 45);
			this.panel1.TabIndex = 10;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 45);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtParseTree);
			this.splitContainer1.Size = new System.Drawing.Size(684, 551);
			this.splitContainer1.SplitterDistance = 275;
			this.splitContainer1.TabIndex = 11;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.lineNumbers_For_RichTextBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtSource, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 275);
			this.tableLayoutPanel1.TabIndex = 7;
			// 
			// lineNumbers_For_RichTextBox1
			// 
			this.lineNumbers_For_RichTextBox1._SeeThroughMode_ = false;
			this.lineNumbers_For_RichTextBox1.AutoSizing = true;
			this.lineNumbers_For_RichTextBox1.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lineNumbers_For_RichTextBox1.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
			this.lineNumbers_For_RichTextBox1.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
			this.lineNumbers_For_RichTextBox1.BorderLines_Color = System.Drawing.Color.SlateGray;
			this.lineNumbers_For_RichTextBox1.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
			this.lineNumbers_For_RichTextBox1.BorderLines_Thickness = 1F;
			this.lineNumbers_For_RichTextBox1.DockSide = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Left;
			this.lineNumbers_For_RichTextBox1.GridLines_Color = System.Drawing.Color.SlateGray;
			this.lineNumbers_For_RichTextBox1.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
			this.lineNumbers_For_RichTextBox1.GridLines_Thickness = 1F;
			this.lineNumbers_For_RichTextBox1.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
			this.lineNumbers_For_RichTextBox1.LineNrs_AntiAlias = true;
			this.lineNumbers_For_RichTextBox1.LineNrs_AsHexadecimal = false;
			this.lineNumbers_For_RichTextBox1.LineNrs_ClippedByItemRectangle = true;
			this.lineNumbers_For_RichTextBox1.LineNrs_LeadingZeroes = true;
			this.lineNumbers_For_RichTextBox1.LineNrs_Offset = new System.Drawing.Size(0, 0);
			this.lineNumbers_For_RichTextBox1.Location = new System.Drawing.Point(14, 0);
			this.lineNumbers_For_RichTextBox1.Margin = new System.Windows.Forms.Padding(0);
			this.lineNumbers_For_RichTextBox1.MarginLines_Color = System.Drawing.Color.SlateGray;
			this.lineNumbers_For_RichTextBox1.MarginLines_Side = LineNumbers.LineNumbers_For_RichTextBox.LineNumberDockSide.Right;
			this.lineNumbers_For_RichTextBox1.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
			this.lineNumbers_For_RichTextBox1.MarginLines_Thickness = 1F;
			this.lineNumbers_For_RichTextBox1.Name = "lineNumbers_For_RichTextBox1";
			this.lineNumbers_For_RichTextBox1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.lineNumbers_For_RichTextBox1.ParentRichTextBox = this.txtSource;
			this.lineNumbers_For_RichTextBox1.Show_BackgroundGradient = true;
			this.lineNumbers_For_RichTextBox1.Show_BorderLines = true;
			this.lineNumbers_For_RichTextBox1.Show_GridLines = true;
			this.lineNumbers_For_RichTextBox1.Show_LineNrs = true;
			this.lineNumbers_For_RichTextBox1.Show_MarginLines = true;
			this.lineNumbers_For_RichTextBox1.Size = new System.Drawing.Size(17, 275);
			this.lineNumbers_For_RichTextBox1.TabIndex = 6;
			// 
			// txtSource
			// 
			this.txtSource.AcceptsTab = true;
			this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSource.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSource.Location = new System.Drawing.Point(32, 0);
			this.txtSource.Margin = new System.Windows.Forms.Padding(0);
			this.txtSource.Name = "txtSource";
			this.txtSource.Size = new System.Drawing.Size(652, 275);
			this.txtSource.TabIndex = 5;
			this.txtSource.Text = "";
			this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
			this.txtSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSource_KeyPress);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 596);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
			this.Name = "frmMain";
			this.Text = "Draw Parse Tree";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnLoad;
		internal System.Windows.Forms.TextBox txtTableFile;
        internal System.Windows.Forms.TextBox txtParseTree;
		internal System.Windows.Forms.RichTextBox txtSource;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private LineNumbers.LineNumbers_For_RichTextBox lineNumbers_For_RichTextBox1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

