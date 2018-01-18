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

        float spacing = 2f;
        float linespacing = 1f;
        bool bSpacingChanged = false;
        public Dictionary<string, string> dTexte { get; set; }


        public ExtractPDF()
        {
            dTexte = new Dictionary<string, string>();
        }
        public bool getText(string[] filenames)
        {
            bool rc = true;
            foreach (string file in filenames)
            {
                getText(file,1);
            }
            return rc;
        }

        /// <summary>
        /// Extrahiert Text aus filename
        /// read only "page" if pages != 0 
        ///
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="pages"></param>
        /// <returns></returns>
        public string getText(string filename, int pages)
        {
            string text;
            if (!bSpacingChanged && dTexte.ContainsKey(filename))
            {
                Trace.WriteLine("Schon gelesen", "PDF");
            }
            else
            {
                try
                {
                    using (PdfReader pr = new PdfReader(filename))
                    {

                        //Trace.WriteLine(filename + "\tspacing = " + spacing + "\tlinespacing = " + linespacing, "PDF");
                        ITextExtractionStrategy tes = new snSimpleTextExtractionStrategy(linespacing, spacing);
                        if (pages != 0)
                        {
                            text = PdfTextExtractor.GetTextFromPage(pr, 1, tes);
                        }
                        else
                        {
                            StringBuilder sbText = new StringBuilder();
                            for (int i = 1; i <= pr.NumberOfPages; i++)
                                sbText.Append(
                                    PdfTextExtractor.GetTextFromPage(pr, i,tes )
                                    );
                            text = sbText.ToString();
                        }

                        if (dTexte.ContainsKey(filename))
                        {
                            dTexte[filename] = text;
                        }
                        else
                        {
                            dTexte.Add(filename, text);
                        }
                        bSpacingChanged = false;
                    }
                }
                catch (iTextSharp.text.exceptions.InvalidPdfException inv)
                {
                    Trace.WriteLine(inv.Message, "WARNING");
                    Trace.WriteLine("Go further");
                    return "";
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
                    Trace.WriteLine("End");
                    throw e;
                }
            }
            return dTexte[filename];

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
    }
}