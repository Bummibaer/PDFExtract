using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        public frmMain()
        {
            InitializeComponent();
            ep = new ExtractPDF();
            ep.SetSpacing(numericUpDown1.Value);
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
                richTextBox1.SelectionBackColor = Color.Azure;
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


        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Trace.WriteLine("DataBindingComplete: " + e.ListChangedType, "DGV");
        }

        private void bindingSource1_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            bindingSource1.Add(new Data("Hallo", "Ballo"));
            Trace.WriteLine("DataBindingComplete: " + e.BindingCompleteState, "BS");
        }

        private void bindingSource1_ListChanged(object sender, ListChangedEventArgs e)
        {
            Trace.WriteLine("ListChange: " +
                e.ListChangedType +
                "\t" + e.OldIndex +
                "\t" + e.NewIndex +
                "\t" + e.PropertyDescriptor, "BS");

            PropertyDescriptor pd = e.PropertyDescriptor;
            Trace.WriteLine(bindingSource1.Count, "BS");
            if (pd != null)
            {
                Trace.WriteLine(pd.Description);
            }
            if (bindingSource1.Current == null)
            {
                Trace.WriteLine("Current is null", "BS");
            }
            else
            {
                Trace.WriteLine("Current : " +
                    bindingSource1.Current + "\t" +
                    ((Data)bindingSource1.Current).Regex + "\t" +
                    ((Data)bindingSource1.Current).Result
                    , "BS");
                CalcRegEx(((Data)bindingSource1.Current).Regex);
            }

        }

 
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Trace.WriteLine("CellEdit: " + e.ColumnIndex + "," + e.RowIndex, "DGT");
        }

        private bool CalcRegEx(string sRegex)
        {
            bool rc = true;
            try
            {
                currentText = richTextBox1.Text;
                re = new Regex(sRegex);

                if (re.IsMatch(currentText))
                {
                    Trace.Write(currentText + "\t", "REGEX");
                    foreach (Match m in re.Matches(currentText))
                    {
                        Trace.Write(m.Value + "|" + m.Index + "," + m.Length + "/");
                    }
                    Trace.WriteLine("");
                }
            }
            catch (Exception)
            {
                Trace.WriteLine("Wrong Regex : " + sRegex);
                rc = false;
            }

            return rc;
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
