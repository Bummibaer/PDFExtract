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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvData = new System.Windows.Forms.DataGridView();
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
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRegex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regexDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsData = new System.Windows.Forms.BindingSource(this.components);
            this.frmMainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineSPacing)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frmMainBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(878, 24);
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
            this.oPenToolStripMenuItem.Click += new System.EventHandler(this.oPenToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgvData, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(878, 531);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.richTextBox1, 2);
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(510, 326);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.SelectionChanged += new System.EventHandler(this.richTextBox1_SelectionChanged);
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
            this.richTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(519, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Nochmal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnNochmal_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cName,
            this.cRegex,
            this.cResult,
            this.regexDataGridViewTextBoxColumn1,
            this.resultDataGridViewTextBoxColumn1,
            this.nameDataGridViewTextBoxColumn});
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView1, 2);
            this.dataGridView1.DataSource = this.bsData;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataGridView1.Location = new System.Drawing.Point(2, 334);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(511, 195);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // dgvData
            // 
            this.dgvData.AutoGenerateColumns = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.DataSource = this.frmMainBindingSource;
            this.dgvData.Location = new System.Drawing.Point(600, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(240, 150);
            this.dgvData.TabIndex = 4;
            this.dgvData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView2_DataBindingComplete);
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
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
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
            this.nudLineSPacing.Size = new System.Drawing.Size(120, 20);
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
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRegexMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 533);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(878, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsRegexMessage
            // 
            this.tsRegexMessage.Name = "tsRegexMessage";
            this.tsRegexMessage.Size = new System.Drawing.Size(118, 17);
            this.tsRegexMessage.Text = "toolStripStatusLabel1";
            // 
            // cName
            // 
            this.cName.DataPropertyName = "Name";
            this.cName.FillWeight = 20F;
            this.cName.HeaderText = "Name";
            this.cName.MinimumWidth = 100;
            this.cName.Name = "cName";
            // 
            // cRegex
            // 
            this.cRegex.DataPropertyName = "Regex";
            this.cRegex.FillWeight = 30F;
            this.cRegex.HeaderText = "Regex";
            this.cRegex.MinimumWidth = 150;
            this.cRegex.Name = "cRegex";
            this.cRegex.Width = 150;
            // 
            // cResult
            // 
            this.cResult.DataPropertyName = "Result";
            this.cResult.FillWeight = 50F;
            this.cResult.HeaderText = "Result";
            this.cResult.MinimumWidth = 200;
            this.cResult.Name = "cResult";
            this.cResult.Width = 200;
            // 
            // regexDataGridViewTextBoxColumn1
            // 
            this.regexDataGridViewTextBoxColumn1.DataPropertyName = "Regex";
            this.regexDataGridViewTextBoxColumn1.HeaderText = "Regex";
            this.regexDataGridViewTextBoxColumn1.Name = "regexDataGridViewTextBoxColumn1";
            this.regexDataGridViewTextBoxColumn1.Width = 63;
            // 
            // resultDataGridViewTextBoxColumn1
            // 
            this.resultDataGridViewTextBoxColumn1.DataPropertyName = "Result";
            this.resultDataGridViewTextBoxColumn1.HeaderText = "Result";
            this.resultDataGridViewTextBoxColumn1.Name = "resultDataGridViewTextBoxColumn1";
            this.resultDataGridViewTextBoxColumn1.Width = 62;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 60;
            // 
            // bsData
            // 
            this.bsData.DataMember = "LRegEx";
            this.bsData.DataSource = typeof(PDFExtract.frmMain);
            this.bsData.BindingComplete += new System.Windows.Forms.BindingCompleteEventHandler(this.bindingSource1_BindingComplete);
            this.bsData.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            this.bsData.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bindingSource1_ListChanged);
            // 
            // frmMainBindingSource
            // 
            this.frmMainBindingSource.DataMember = "LData";
            this.frmMainBindingSource.DataSource = typeof(PDFExtract.frmMain);
            this.frmMainBindingSource.DataSourceChanged += new System.EventHandler(this.frmMainBindingSource_DataSourceChanged);
            this.frmMainBindingSource.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.frmMainBindingSource_ListChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(878, 555);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.nudLineSPacing);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLineSPacing)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frmMainBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPenToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown nudLineSPacing;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bsData;
        private System.Windows.Forms.DataGridViewTextBoxColumn regexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn raiseListChangedEventsDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowNewDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowEditDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowRemoveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsRegexMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRegex;
        private System.Windows.Forms.DataGridViewTextBoxColumn cResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn regexDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource frmMainBindingSource;
    }
}

