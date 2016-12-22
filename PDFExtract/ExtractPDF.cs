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
        public string  getText(string filename)
        {
            if ( !bSpacingChanged && dTexte.ContainsKey(filename) )
            {
                Trace.WriteLine("Schon gelesen", "PDF");
                return dTexte[filename];
            }
            else
            {
                try
                {
                    using (PdfReader pr = new PdfReader(filename) ) { 
                    
                        Trace.WriteLine(filename + "\tspacing = " + spacing + "\tlinespacing = " + linespacing, "PDF");
                        string text = PdfTextExtractor.GetTextFromPage(pr, 1, new snSimpleTextExtractionStrategy(linespacing,spacing));
                        if (dTexte.ContainsKey(filename))
                        {
                            dTexte[filename] = text;
                        }
                        else {
                            dTexte.Add(filename,text);
                        }
                        bSpacingChanged = false;
                        return dTexte[filename];
                    }
               }
                catch (iTextSharp.text.exceptions.InvalidPdfException inv)
                {
                    Trace.WriteLine(inv.Message ,"WARNING");
                    Trace.WriteLine("Go further");
                    return "";
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

                return dTexte;
            }

            set
            {
                dTexte = value;
            }
        }
    }
}
