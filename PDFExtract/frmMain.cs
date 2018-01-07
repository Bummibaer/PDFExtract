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

        Template template;

        string currentText;
        int currentLength, currentIndex;
        Properties.Settings settings = new Properties.Settings();
        XmlSerializer xmlSerializer;

        public List<Template> lRegEx;

        private List<Data> lData = new List<Data>();

        DoWork dw = new DoWork();


        public frmMain()
        {
            InitializeComponent();
            ep = new ExtractPDF();
            ep.SetSpacing(numericUpDown1.Value);
            Trace.WriteLine(Properties.Settings.Default.RegExData, "DEBUG");
            if (File.Exists(Properties.Settings.Default.RegExData))
            {
                xmlSerializer = new XmlSerializer(typeof(Template));
                lRegEx.AddRange(((List<Template>)xmlSerializer.Deserialize(new StreamReader(Properties.Settings.Default.RegExData))));

            }

            lData.Add(new Data());
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Trace.WriteLine("Writing Data", "FORM");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Template>));
            xmlSerializer.Serialize(new StreamWriter("regexes.xml"), lRegEx);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFileDialog1.DefaultExt = "Acrobat PDF|*.pdf";

            DialogResult dr = openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != null)
            {
                fileNames = openFileDialog1.FileNames;
                rtbParsedText.Text = ep.getText(fileNames[0]);
                StringCollection sc = new StringCollection();
                dw.ParseText(rtbParsedText.Text);
            }
        }

        bool shiftPressed, controlPressed;

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            if (shiftPressed)
            {
                rtbParsedText.SelectionBackColor = Color.Azure;
                currentText = rtbParsedText.SelectedText;
                currentLength = rtbParsedText.SelectionLength;
                currentIndex = rtbParsedText.SelectionStart;
                Trace.WriteLine("SHIFT: " + currentIndex + "\t" + currentLength + "\t" + currentText, "SELECT");
            }
            else if (controlPressed)
            {
                rtbParsedText.SelectionBackColor = Color.Yellow;
                Trace.WriteLine("CTRL: " + rtbParsedText.SelectionStart, "SELECT");
            }
            else
            {
                Trace.WriteLine("Changed: " +
                    rtbParsedText.SelectionStart +
                    ":" +
                     rtbParsedText.SelectionLength +
                     " - " +
                     rtbParsedText.SelectionBackColor
                    , "SELECT");

            }
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            shiftPressed = controlPressed = false;
        }

        private void nudSetSpacing_ValueChanged(object sender, EventArgs e)
        {
            ep.SetSpacing(numericUpDown1.Value);
            rtbParsedText.Text = ep.getText(fileNames[0]);
        }


        private void nudLineSPacing_ValueChanged(object sender, EventArgs e)
        {
            ep.SetLineSpacing(nudLineSPacing.Value);
        }



        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Trace.WriteLine("CellEdit: " + e.ColumnIndex + "," + e.RowIndex, "DGT");
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Trace.WriteLine("DataBindingComplete ", "DGVData");
        }

        private void sRuleBindingSource_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            Trace.WriteLine(e.Binding.PropertyName, "sRuleBindingSource_BindingComplete");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            template = new Template();
            rtbParsedText.Text = ep.getText(@"F:\Benutzer\PapaNetz\Dokumente\comdirect\Wertpapierabrechnung_Kauf_0477_St._WKN_ETF090(CS.CO.C.EX-AG.EWT.U.ETF_I)_vom_01.12.2017930236.pdf");
            StringCollection sc = new StringCollection();
            dw.debug = 3;
            dw.ParseText(rtbParsedText.Text);
            for (int i = 0; i < dw.Results.Count; i++) HiglightText(i);
        }

        private void sRuleBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private int rindex = 0;
        bool cswitch = false;

        private void button1_Click(object sender, EventArgs e)
        {
            HiglightText(rindex);
            rindex++;
        }

        private void HiglightText(int index)
        {
            if (index >= dw.Results.Count) return;
            rtbParsedText.SelectionStart = dw.Results[index].lineIndex;
            rtbParsedText.SelectionLength = dw.Results[index].length;
            rtbParsedText.SelectionBackColor = cswitch ? Color.Gold : Color.Green;
            rtbParsedText.Select(rtbParsedText.SelectionStart, rtbParsedText.SelectionLength);
            textBox1.Text += dw.Results[index].lineIndex + "\t" 
                + dw.Results[index].length + "\t:" 
                + dw.Results[index].name + "\t:" 
                + dw.Results[index].value + "\t:"
                + rtbParsedText.SelectedText + "\t|" 
                + rtbParsedText.SelectionBackColor 
                + Environment.NewLine;
            cswitch = !cswitch;
        }

        private bool CalcRegEx(string sRegex)
        {
            bool rc = true;
            try
            {
                currentText = rtbParsedText.Text;
                Regex re = new Regex(sRegex);

                if (re.IsMatch(currentText))
                {
                    Trace.Write(currentText + "\t", "REGEX");
                    foreach (Match m in re.Matches(currentText))
                    {
                        Trace.Write(m.Value + "|" + m.Index + "," + m.Length + "/");
                        //((TemplateRegEx)bsRegEx.Current).Result = m.Value;
                    }
                    Trace.WriteLine("");
                    tsRegexMessage.Text = "RegEx Matches";
                }
                else
                {
                    //((TemplateRegEx)bsRegEx.Current).Result = "------------";
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
