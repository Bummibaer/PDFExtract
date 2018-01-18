using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PDFExtract
{
    class Finanzreport
    {

        int debug { get; set; }

        public Finanzreport()
        {
            debug = 2;
        }
        public void TestDoWork()
        {
            ExtractPDF ep = new ExtractPDF();
            ep.SetLineSpacing(5);
            TextWriter tw = File.CreateText("ertrag.csv");
            char[] c = new char[] { '|', '\\', '-', '/' };
            int cindex = 0;

            tw.WriteLine("File;Datum;Sonder;Art;Geschäftsnummer;Geschäftstag;WPKNR;Name;ISIN;Test1;Test2;Rest");

            //string dir = @"F:\Benutzer\PapaNetz\Dokumente\comdirect\";
            string dir = @"P:\privat\comdirect\";
            string files = dir + "Finanzreport_ *.pdf";
            Trace.WriteLine("Work on : " + files + "\t" + Directory.Exists(dir));


            foreach (string filename in Directory.EnumerateFiles(dir, "Finanzreport_*.pdf"))
            {
                if (debug == 0)
                {
                    Trace.Write(c[cindex++].ToString() + '\b');
                    if (cindex >= c.Length)
                    {
                        cindex = 0;
                    }
                }
                else
                {
                    Trace.WriteLine("Read : " + filename);
                }
                string text = ep.getText(filename,0);
                if (text == "") continue;
                ParseText(text);
            }



        }

        private void ParseText(string text)
        {
            string[] newline = { "\n" };

            // Gesamtbestand 2.193,14 6.156,94 -3.963,80
            Regex reBedingung = new Regex(@"^\s+Gesamt\w+\s+(<?Kauf>[+-]*[\d.]+,\d+[+-]*)\s+(<?Wert>[+-]*[\d.]+,\d+[+-]*)\s+(<?Gewinn>[+-]*[\d.]+,\d+[+-]*)");
            string[] lines = text.Split(newline, StringSplitOptions.None);

            for (int index = 0; index < lines.Length; index++)
            {
                Trace.WriteLineIf(debug > 1, lines[index]);
                if (lines[index].IndexOf("Gesamtkurs") >= 0)
                {
                    Trace.WriteLine(lines[index]);
                }
                if (reBedingung.IsMatch(lines[index]))
                {
                    Match m = reBedingung.Match(lines[index]);
                    Trace.WriteLine(m.Groups["Kauf"].Value, "TEST");
                }
            }
        }
    }
}
