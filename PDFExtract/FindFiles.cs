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
    class FindFiles
    {
        private string directory = @"F:\Benutzer\PapaNetz\Dokumente\comdirect\";
        ExtractPDF epdf = new ExtractPDF();
        Regex re = new Regex("WKN_([^_]+)");
        Dictionary<string, int> dwkns = new Dictionary<string, int>();
        string command = @"C:\ProgramData\chocolatey\bin\pdftotext.exe";
        public FindFiles()
        {
            IEnumerable<string> fileenum = Directory.EnumerateFiles(directory, "Wertpapier*.pdf");
            foreach (string filename in fileenum)
            {
                Trace.Write(filename + "\t\t:");
                if (re.IsMatch(filename))
                {
                    Match m = re.Match(filename);
                    string wkn = m.Groups[1].Value;
                    Trace.WriteLine(wkn);
                    if (dwkns.ContainsKey(wkn))
                    {
                        dwkns[wkn]++;
                    }
                    else
                    {
                        ExecuteProcess ep = new ExecuteProcess(command);
                        ep.Run(filename);
                        List<string> plines = ep.Text;
                        string text = epdf.getText(filename);
                        string[] elines = text.Split(new char[] { '\n' });
                        int index = 0;
                        int count_ptext = plines.Count;
                        int count_etext = elines.Length;
                        bool plast = false, elast = false;
                        Trace.WriteLine("BEGIN");
                        TextWriter ptw = File.CreateText("p.txt");
                        TextWriter etw = File.CreateText("e.txt");
                        do
                        {
                            if (index < count_ptext)
                            {
                                ptw.WriteLine(plines[index]);
                            }
                            else
                            {
                                Trace.Write("----------------------------------------------------\t|");
                                plast = true;
                            }
                            if (index < count_etext)
                            {
                                etw.WriteLine(elines[index]);
                            }
                            else
                            {
                                Trace.Write("----------------------------------------------------\t");
                                elast = true;
                            }
                            index++;
                        } while (!(plast & elast) );
                        ptw.Close();
                        etw.Close();
                        Trace.WriteLine("ENDE");
                    }
                }
                else
                {
                    Trace.WriteLine("Not found");
                }

            }
        }
    }
}
