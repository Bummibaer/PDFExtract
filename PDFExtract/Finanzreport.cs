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

        Dictionary<string, int> dFiles = new Dictionary<string, int>();

        TextWriter tw = File.CreateText("ertrag.csv");

        struct sLine
        {
            public string filename, datum, kauf, wert, gewinn;
        };


        public Finanzreport()
        {
            debug = 0;
        }

        public void TestDoWork()
        {
            ExtractPDF ep = new ExtractPDF();
            ep.SetLineSpacing(5);
            char[] c = new char[] { '|', '\\', '-', '/' };
            int cindex = 0;

            tw.WriteLine("File;Datum;Wert;Kaufwert;Gewinn");

            string dir = @"F:\Benutzer\PapaNetz\Dokumente\comdirect\";
            //string dir = @"P:\privat\comdirect\";
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
                if (dFiles.ContainsKey(filename))
                {
                    Trace.WriteLine("Why!");
                }
                dFiles.Add(filename, 1);

                string text = ep.getText(filename, 0);
                if (text == "") continue;
                ParseText(filename, text);
            }

            tw.Close();

        }

        private void ParseText(string file, string text)
        {
            string[] newline = { "\n" };
            Trace.Write(file + '\t');
            // Gesamtbestand 2.193,14 6.156,94 -3.963,80
            Regex reBedingung = new Regex(
                @"^\s*Gesamt\w+\s+(?<Wert>[+-]*[\d.]+,\d+[+-]*)\s+(?<Kauf>[+-]*[\d.]+,\d+[+-]*)\s+(?<Gewinn>[+-]*[\d.]+,\d+[+-]*)");
            Regex reDatum = new Regex(
                @"Finanzreport Nr. \d+ per (?<Datum>\d+.\d+.\d+)\s*$");

            sLine line = new sLine();

            string[] lines = text.Split(newline, StringSplitOptions.None);

            // if (file.IndexOf("12_vom_01.12.2003") > 0) debug = 3;

            for (int index = 0; index < lines.Length; index++)
            {
                if ((debug > 2) && (lines[index].IndexOf("Finanzreport Nr.") >= 0))
                {
                    Trace.WriteLine("");
                }
                Trace.WriteLineIf(debug > 2, lines[index]);
                if (reBedingung.IsMatch(lines[index]))
                {
                    Match m = reBedingung.Match(lines[index]);
                    line.filename = file;
                    line.kauf = m.Groups["Kauf"].Value;
                    line.wert = m.Groups["Wert"].Value;
                    line.gewinn = m.Groups["Gewinn"].Value;

                    if (line.datum == null)
                    {
                        Regex re = new Regex(@"_(?<Datum>\d{2}\.\d{2}\.\d{4})");
                        Match mr = re.Match(file);
                        if (mr.Length > 0)
                        {
                            line.datum = mr.Groups["Datum"].Value;
                        }
                        else
                        {
                            line.datum  = "Not Known";
                        }
                    }

                }
                if (reDatum.IsMatch(lines[index]))
                {
                    Match m = reDatum.Match(lines[index]);
                    line.datum = m.Groups["Datum"].Value;
                }
            }

            Trace.WriteLine(
               "Datum:" + line.datum
               + "\tWert:" + line.wert
               + "\tKaufwert : " + line.kauf
               + "\tGewinn : " + line.gewinn
               );
            tw.WriteLine(file + ';'
                + line.datum + ';'
                + line.wert + ';'
                + line.kauf + ';'
                + line.gewinn+ ';'
                );

        }
    }
}
