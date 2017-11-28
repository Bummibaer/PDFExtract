using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace PDFExtract
{
    public partial class frmMain : Form
    {
        ExtractPDF ep;
        String[] fileNames;
        Regex re;
        string currentText;
        int currentLength, currentIndex;
        Properties.Settings settings = new Properties.Settings();
        XmlSerializer xmlSerializer;

        private List<Template> lRegEx = new List<Template>();
        private List<Data> lData = new List<Data>();

        DoWork dw = new DoWork();

        Template template = new Template();

        public frmMain()
        {
            InitializeComponent();
            ep = new ExtractPDF();
            ep.SetSpacing(numericUpDown1.Value);

            if (File.Exists(Properties.Settings.Default.RegExData))
            {
                xmlSerializer = new XmlSerializer(typeof(Template));
                LRegEx.AddRange(((List<Template>)xmlSerializer.Deserialize(new StreamReader(Properties.Settings.Default.RegExData))));

            }

            lData.Add(new Data());
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trace.WriteLine("Writing Data", "FORM");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TemplateRegEx>));
            xmlSerializer.Serialize(new StreamWriter("regexes.xml"), LRegEx);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.DefaultExt = "Acrobat PDF|*.pdf";

            DialogResult dr = openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {
                fileNames = openFileDialog1.FileNames;
                richTextBox1.Text = ep.getText(fileNames[0]);
                StringCollection sc = new StringCollection();
                dw.Test(richTextBox1.Text);
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
                Trace.WriteLine("SHIFT: " + currentIndex + "\t" + currentLength + "\t" + currentText,"SELECT");
            }
            else if (controlPressed)
            {
                richTextBox1.SelectionBackColor = Color.Yellow;
                Trace.WriteLine("CTRL: " + richTextBox1.SelectionStart,"SELECT");
            }
            else
            {
                Trace.WriteLine("PRESS: " + richTextBox1.SelectionStart, "SELECT");

            }
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            shiftPressed = controlPressed = false;
        }

        private void nudSetSpacing_ValueChanged(object sender, EventArgs e)
        {
            ep.SetSpacing(numericUpDown1.Value);
            richTextBox1.Text = ep.getText(fileNames[0]);
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
            bsRegEx.Add(new TemplateRegEx("Hallo", "Ballo"));
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
            Trace.WriteLine(bsRegEx.Count, "BS");
            if (pd != null)
            {
                Trace.WriteLine(pd.Description);
            }
            if (bsRegEx.Current == null)
            {
                Trace.WriteLine("Current is null", "BS");
            }
            else
            {
                if (((TemplateRegEx)bsRegEx.Current).Regex.Length > 0)
                {
                    TemplateRegEx tre = ((TemplateRegEx)bsRegEx.Current);
                    CalcRegEx(tre.Regex);
                    Trace.WriteLine("Current : <" +
                        tre.Regex + ">\t<" +
                        tre.Result + ">"
                        , "BS");

                }
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Trace.WriteLine("CellEdit: " + e.ColumnIndex + "," + e.RowIndex, "DGT");
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            Trace.WriteLine("CurrentChanged ", "BS");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Trace.WriteLine("DataBindingComplete ", "DGVData");
        }

        private void dataBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }


        private void frmMainBindingSource_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void frmMainBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Trace.WriteLine("ListChanged " + e.ListChangedType, "BSData");
            if (bsData.Current == null) return;
            PropertyDescriptor pd = e.PropertyDescriptor;
            Trace.WriteLine("\tListChanged not NULL", "BSData");
            dgvData.DataSource = ((Data)bsData.Current).Dt;
        }

        private void bsData_PositionChanged(object sender, EventArgs e)
        {
            if (bsData.Current == null) return;
            Trace.WriteLine("PositionChanged", "BSData");
            dgvData.DataSource = ((Data)bsData.Current).Dt;
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
                        ((TemplateRegEx)bsRegEx.Current).Result = m.Value;
                    }
                    Trace.WriteLine("");
                    tsRegexMessage.Text = "RegEx Matches";
                }
                else
                {
                    ((TemplateRegEx)bsRegEx.Current).Result = "------------";
                    tsRegexMessage.Text = "RegEx No Match";
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Wrong Regex : " + sRegex + "\t" + e.Message, "REGEX ERROR");
                if (e.InnerException != null)
                {
                    Trace.WriteLine(e.InnerException.Message, "REGEX ERROR");
                }

                tsRegexMessage.Text = e.Message;
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
