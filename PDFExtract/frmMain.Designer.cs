﻿namespace PDFExtract
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbParsedText = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sRuleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.nudLineSPacing = new System.Windows.Forms.NumericUpDown();
            this.regexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.raiseListChangedEventsDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.allowNewDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.allowEditDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.allowRemoveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.countDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsRegexMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sRuleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineSPacing)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(920, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPenToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // oPenToolStripMenuItem
            // 
            this.oPenToolStripMenuItem.Name = "oPenToolStripMenuItem";
            this.oPenToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.oPenToolStripMenuItem.Text = "Open";
            this.oPenToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 683F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.tableLayoutPanel1.Controls.Add(this.rtbParsedText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(920, 770);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // rtbParsedText
            // 
            this.rtbParsedText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbParsedText.Location = new System.Drawing.Point(3, 3);
            this.rtbParsedText.Name = "rtbParsedText";
            this.rtbParsedText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbParsedText.Size = new System.Drawing.Size(677, 548);
            this.rtbParsedText.TabIndex = 0;
            this.rtbParsedText.Text = "";
            this.rtbParsedText.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            this.rtbParsedText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.rtbParsedText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lengthDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.sRuleBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(3, 557);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(432, 150);
            this.dataGridView1.TabIndex = 5;
            // 
            // lengthDataGridViewTextBoxColumn
            // 
            this.lengthDataGridViewTextBoxColumn.DataPropertyName = "Length";
            this.lengthDataGridViewTextBoxColumn.HeaderText = "Length";
            this.lengthDataGridViewTextBoxColumn.Name = "lengthDataGridViewTextBoxColumn";
            this.lengthDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sRuleBindingSource
            // 
            this.sRuleBindingSource.DataMember = "Names";
            this.sRuleBindingSource.DataSource = typeof(PDFExtract.Template.sRule);
            this.sRuleBindingSource.BindingComplete += new System.Windows.Forms.BindingCompleteEventHandler(this.sRuleBindingSource_BindingComplete);
            this.sRuleBindingSource.CurrentChanged += new System.EventHandler(this.sRuleBindingSource_CurrentChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PDF-Dateien|*.pdf";
            this.openFileDialog1.Title = "Wähle ein File aus";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(643, 4);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(119, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.nudSetSpacing_ValueChanged);
            // 
            // nudLineSPacing
            // 
            this.nudLineSPacing.Location = new System.Drawing.Point(784, 4);
            this.nudLineSPacing.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLineSPacing.Name = "nudLineSPacing";
            this.nudLineSPacing.Size = new System.Drawing.Size(119, 20);
            this.nudLineSPacing.TabIndex = 3;
            this.nudLineSPacing.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudLineSPacing.ValueChanged += new System.EventHandler(this.nudLineSPacing_ValueChanged);
            // 
            // regexDataGridViewTextBoxColumn
            // 
            this.regexDataGridViewTextBoxColumn.DataPropertyName = "Regex";
            this.regexDataGridViewTextBoxColumn.HeaderText = "Regex";
            this.regexDataGridViewTextBoxColumn.Name = "regexDataGridViewTextBoxColumn";
            this.regexDataGridViewTextBoxColumn.Width = 77;
            // 
            // resultDataGridViewTextBoxColumn
            // 
            this.resultDataGridViewTextBoxColumn.DataPropertyName = "Result";
            this.resultDataGridViewTextBoxColumn.HeaderText = "Result";
            this.resultDataGridViewTextBoxColumn.Name = "resultDataGridViewTextBoxColumn";
            this.resultDataGridViewTextBoxColumn.Width = 77;
            // 
            // raiseListChangedEventsDataGridViewCheckBoxColumn
            // 
            this.raiseListChangedEventsDataGridViewCheckBoxColumn.DataPropertyName = "RaiseListChangedEvents";
            this.raiseListChangedEventsDataGridViewCheckBoxColumn.HeaderText = "RaiseListChangedEvents";
            this.raiseListChangedEventsDataGridViewCheckBoxColumn.Name = "raiseListChangedEventsDataGridViewCheckBoxColumn";
            this.raiseListChangedEventsDataGridViewCheckBoxColumn.Width = 172;
            // 
            // allowNewDataGridViewCheckBoxColumn
            // 
            this.allowNewDataGridViewCheckBoxColumn.DataPropertyName = "AllowNew";
            this.allowNewDataGridViewCheckBoxColumn.HeaderText = "AllowNew";
            this.allowNewDataGridViewCheckBoxColumn.Name = "allowNewDataGridViewCheckBoxColumn";
            this.allowNewDataGridViewCheckBoxColumn.Width = 73;
            // 
            // allowEditDataGridViewCheckBoxColumn
            // 
            this.allowEditDataGridViewCheckBoxColumn.DataPropertyName = "AllowEdit";
            this.allowEditDataGridViewCheckBoxColumn.HeaderText = "AllowEdit";
            this.allowEditDataGridViewCheckBoxColumn.Name = "allowEditDataGridViewCheckBoxColumn";
            this.allowEditDataGridViewCheckBoxColumn.Width = 70;
            // 
            // allowRemoveDataGridViewCheckBoxColumn
            // 
            this.allowRemoveDataGridViewCheckBoxColumn.DataPropertyName = "AllowRemove";
            this.allowRemoveDataGridViewCheckBoxColumn.HeaderText = "AllowRemove";
            this.allowRemoveDataGridViewCheckBoxColumn.Name = "allowRemoveDataGridViewCheckBoxColumn";
            this.allowRemoveDataGridViewCheckBoxColumn.Width = 98;
            // 
            // countDataGridViewTextBoxColumn
            // 
            this.countDataGridViewTextBoxColumn.DataPropertyName = "Count";
            this.countDataGridViewTextBoxColumn.HeaderText = "Count";
            this.countDataGridViewTextBoxColumn.Name = "countDataGridViewTextBoxColumn";
            this.countDataGridViewTextBoxColumn.ReadOnly = true;
            this.countDataGridViewTextBoxColumn.Width = 74;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRegexMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 772);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(920, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsRegexMessage
            // 
            this.tsRegexMessage.Name = "tsRegexMessage";
            this.tsRegexMessage.Size = new System.Drawing.Size(118, 17);
            this.tsRegexMessage.Text = "toolStripStatusLabel1";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(686, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(231, 548);
            this.textBox1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(920, 794);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.nudLineSPacing);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sRuleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineSPacing)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox rtbParsedText;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown nudLineSPacing;
        private System.Windows.Forms.DataGridViewTextBoxColumn regexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn raiseListChangedEventsDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowNewDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowEditDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowRemoveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsRegexMessage;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource sRuleBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn lengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

