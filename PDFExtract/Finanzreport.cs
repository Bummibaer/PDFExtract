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
            debug = 0;
        }
        TextWriter tw = File.CreateText("ertrag.csv");
        public void TestDoWork()
        {
            ExtractPDF ep = new ExtractPDF();
            ep.SetLineSpacing(5);
            char[] c = new char[] { '|', '\\', '-', '/' };
            int cindex = 0;

            tw.WriteLine("File;Datum;Wert;Kaufwert;Gewinn");

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
                string text = ep.getText(filename, 0);
                if (text == "") continue;
                ParseText(filename, text);
            }

            tw.Close();

        }

        private void ParseText(string file, string text)
        {
            string[] newline = { "\n" };

            // Gesamtbestand 2.193,14 6.156,94 -3.963,80
            Regex reBedingung = new Regex(
                @"^\s*Gesamt\w+\s+(?<Wert>[+-]*[\d.]+,\d+[+-]*)\s+(?<Kauf>[+-]*[\d.]+,\d+[+-]*)\s+(?<Gewinn>[+-]*[\d.]+,\d+[+-]*)");
            Regex reDatum = new Regex(
                @"Finanzreport Nr. \d+ per (?<Datum>\d+.\d+.\d+)\s*$");

            string Datum = "Unknown";

            string[] lines = text.Split(newline, StringSplitOptions.None);


            for (int index = 0; index < lines.Length; index++)
            {
                Trace.WriteLineIf(debug > 2, lines[index]);
                if (reBedingung.IsMatch(lines[index]))
                {
                    Match m = reBedingung.Match(lines[index]);
                    Trace.WriteLine(
                        "Datum:" + Datum
                        + "\tWert:" + m.Groups["Wert"].Value
                        + "\tKaufwert : " + m.Groups["Kauf"].Value
                        + "\tGewinn : " + m.Groups["Gewinn"].Value
                        );
                        tw.WriteLine(file + ';'
                            + Datum + ';'
                            + m.Groups["Wert"].Value + ';'
                            + m.Groups["Kauf"].Value + ';'
                            + m.Groups["Gewinn"].Value + ';'
                            );

                }
                if (reDatum.IsMatch(lines[index]))
                {
                    Match m = reDatum.Match(lines[index]);
                    Datum = m.Groups["Datum"].Value;
                }
            }
        }
    }
}
