using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Diagnostics;

namespace PDFExtract    
{
    class ExtractPDF
    {
        string text;
        float spacing = 2f;
        float linespacing = 1f;
        bool bSpacingChanged = false;
        Dictionary<string, string> dTexte = new Dictionary<string, string>();


        public ExtractPDF()
        {

        }
        public bool getText(string[] filenames)
        {
            bool rc = true;
            foreach(string file in filenames)
            {
                if (getText(file) == false) return false;
            }
            return rc;
        }


        public bool  getText(string filename)
        {
            bool rc = true;
            if ( !bSpacingChanged && DTexte1.ContainsKey(filename) )
            {
                Trace.WriteLine("Schon gelesen", "PDF");
                return rc;
            }
            else
            {
                try
                {
                    using (PdfReader pr = new PdfReader(filename) ) { 
                    
                        Trace.WriteLine(filename + "\tspacing = " + spacing + "\tlinespacing = " + linespacing, "PDF");
                        string text = PdfTextExtractor.GetTextFromPage(pr, 1, new snSimpleTextExtractionStrategy(linespacing,spacing));
                        if (DTexte1.ContainsKey(filename))
                        {
                            DTexte1[filename] = text;
                        }
                        else {
                            DTexte1.Add(filename,text);
                        }
                        bSpacingChanged = false;
                        return true;
                    }
               }
                catch (iTextSharp.text.exceptions.InvalidPdfException inv)
                {
                    Trace.WriteLine(inv.Message ,"WARNING");
                    Trace.WriteLine("Go further");
                    return false;
                }
                catch ( Exception e)
                {
                    Trace.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
                    Trace.WriteLine("End");
                    throw e;
                }
            }
        }

        internal void SetSpacing(decimal value)
        {
            spacing = (float)value;
            bSpacingChanged = true;
        }

        internal void SetLineSpacing(decimal value)
        {
            linespacing = (float)value;
            bSpacingChanged = true;
        }

        /*
               currentText =
            Encoding.UTF8.GetString(Encoding.Convert(
                Encoding.Default,
                Encoding.UTF8,
                Encoding.Default.GetBytes(currentText)));
*/
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public Dictionary<string, string> DTexte
        {
            get
            {

                return DTexte1;
            }

            set
            {
                DTexte1 = value;
            }
        }

        public Dictionary<string, string> DTexte1
        {
            get
            {
                return dTexte;
            }

            set
            {
                dTexte = value;
            }
        }
    }
}
