using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFExtract
{
    public partial class frmMain : Form
    {
        ExtractPDF ep;
        string fileName;
        Regex re;
        string currentText;
        int currentLength, currentIndex;

        public List<Data.sData> lData = new List<Data.sData>();
        public Data d = new Data();

        public frmMain()
        {
            InitializeComponent();
            ep = new ExtractPDF();
            ep.SetSpacing(numericUpDown1.Value);
            dataGridView1.AutoGenerateColumns = true;
            bindingSource1.DataSource = d.blData;
            bindingSource1.Add(new Data.sData("h", "M"));
            Trace.WriteLine(bindingSource1.Current);
        }

        private void oPenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {
                fileName = openFileDialog1.FileName;
                string s = ep.getText(fileName);
                richTextBox1.Text = s;
            }
        }

        bool shiftPressed, controlPressed;

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (shiftPressed)
            {

                richTextBox1.SelectionBackColor  = Color.Azure;
                currentText = richTextBox1.SelectedText;
                currentLength = richTextBox1.SelectionLength;
                currentIndex = richTextBox1.SelectionStart;
                Trace.WriteLine(currentIndex + "\t" + currentLength + "\t" + currentText);
            }
            else if (controlPressed)
            {
                richTextBox1.SelectionBackColor = Color.Yellow;

            }
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            shiftPressed = controlPressed = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ep.SetSpacing(numericUpDown1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = ep.getText(fileName);
            richTextBox1.Text = s;
        }

        private void nudLineSPacing_ValueChanged(object sender, EventArgs e)
        {
            ep.SetLineSpacing(nudLineSPacing.Value);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Trace.WriteLine("DataBindingComplete: " + e.ListChangedType);
        }

        private void bindingSource1_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            Trace.WriteLine("DataBindingComplete: " + e.BindingCompleteState);

        }

        private void bindingSource1_ListChanged(object sender, ListChangedEventArgs e)
        {
            Trace.WriteLine("ListChange: " + e.ListChangedType + "\t" +e.NewIndex);
            dataGridView1.DataSource = bindingSource1;

        }

        private void tbRegEx_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //re = new Regex(tbRegEx.Text);

                //if ( re.IsMatch(currentText))
                //{
                //    Trace.Write(currentText + "\t", "REGEX");
                //    foreach(Match m in re.Matches(currentText))
                //    {
                //        Trace.Write(m.Value + "|");
                //    }
                //    Trace.WriteLine("");
                //}
            }
            catch (Exception)
            {
                Trace.WriteLine("Wrong Regex : "); // + tbRegEx.Text);
            }
         }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift)
            {
                Trace.WriteLine("ShiftKey pressed", "KEY");
                shiftPressed = true;
            }
            if (e.Control)
            {
                Trace.WriteLine("Control pressed", "KEY");
                controlPressed = true;

            }
        }
    }
}
