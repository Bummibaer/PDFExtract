using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PDFExtract
{
    class DoWork
    {

        struct sLine
        {
            internal string[] names;
            internal Regex regex;
            public sLine(string[] names, string regex)
            {
                this.names = names;
                this.regex = new Regex(regex);
            }
        }

        public struct sResult
        {
            public int lineIndex;
            public int begin;
            public int end;
            public string name;

            private string value;

            public sResult(int index, int begin, int end, string name, string value) : this()
            {
                this.lineIndex = index;
                this.begin = begin;
                this.end = end;
                this.name = name;
                this.value = value;
            }
        };

        public List<sResult> result = new List<sResult>();
        
        sLine[] sRegex =
        {
           new sLine(  new string[] { "Count" , "Value" } ,  @"\s+St\.\s+(\d+,\d+)\s+(\w{3})\s+([.\d]+,[.\d]+)" ),
           new sLine(  new string[] {"date" }, @"ABRECHNUNG VOM (\d+\.\d+\.\d+)"),
           new sLine(  new string[] { "number" } ,  @"Geschäftsnummer\s+:\s+([\d ]+)"),
           new sLine(  new string[] { "day" , "local" }, @"Geschäftstag\s+:\s+(\d+\.\d+\.\d+)\s+\w+\s+:*\s+([\w\d ]+)"),
           new sLine(  null ,  @"^\s+([^:]+):\s+([A-Z]{3})\s+(-*[.\d]+,[.\d]+-*)" )
        };
        TextWriter tw;

        List<sLine> lRegexes = new List<sLine>();
        public DoWork()
        {
            foreach (sLine line in sRegex)
            {

                try
                {
                    line.regex.IsMatch(" ");
                    lRegexes.Add(line);
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Wrong Regex : " + sRegex + "\t" + e.Message, "REGEX ERROR");
                    if (e.InnerException != null)
                    {
                        Trace.WriteLine(e.InnerException.Message, "REGEX ERROR");
                    }
                }
            }
            tw = File.CreateText("test.csv");
        }

        public int debug { get; set; }
        List<Dictionary<string, string>> lStocks = new List<Dictionary<string, string>>();
        public void Test(string text)
        {
            string[] mustHaves =
            {
                "Fonds",
                "Name",
                "ISIN",
                "WPKNR",
                "DebitCurrency",
                "DebitValue",
                "date"
            };

            Dictionary<string, string> dStock = new Dictionary<string, string>();
            string[] newline = { "\n" };
            StringBuilder sbLine = new StringBuilder();

            Regex reBedingung = new Regex(@"Wertpapier-Bezeichnung\s+WPKNR/ISIN");
            Regex reLast = new Regex(@"Zu Ihren Lasten");
            Regex reLasten = new Regex(@"(\w{3})\s+(-*[.\d]+,[.\d]+-*)");
            Regex reTest = new Regex(@"(.+)(?! {2,}) (.+)");

            string[] lines = text.Split(newline, StringSplitOptions.None);
            for (int index = 0; index < lines.Length; index++)
            {
                Trace.WriteLineIf(debug > 1, index.ToString("D3") + ":" + lines[index], "TEST");
                //## Next Line, how to template ?
                if (reBedingung.IsMatch(lines[index]))
                {
                    Trace.WriteLineIf(debug > 0, "Found:" + lines[index++], "TEST");
                    // hint
                    Trace.WriteLineIf(debug > 0, "\t" + lines[index], "TEST");
                    MatchCollection mc = reTest.Matches(lines[index++]);

                    if (mc.Count == 1)
                    {
                        dStock["Fonds"] = mc[0].Groups[1].Value.Trim();
                        dStock["WPKNR"] = mc[0].Groups[2].Value.Trim();
                    }
                    else if (mc.Count == 0)
                    {
                        Trace.WriteLine("10.:" + lines[index++], "TEST");
                        throw new Exception("Fehler");
                    }

                    Trace.WriteLineIf(debug > 0, "\t" + lines[index], "TEST");
                    mc = reTest.Matches(lines[index++]);

                    if (mc.Count == 1)
                    {
                        dStock["Name"] = mc[0].Groups[1].Value.Trim();
                        dStock["ISIN"] = mc[0].Groups[2].Value.Trim();
                        Trace.WriteLineIf(debug > 0, "Name: " + dStock["Name"] + "\tISIN: " + dStock["ISIN"]);
                    }
                    else if (mc.Count == 0)
                    {
                        Trace.WriteLine("10.:" + lines[index], "TEST");
                        throw new Exception("Fehler");
                    }
                    continue;

                }

                if (reLast.IsMatch(lines[index]))
                {
                    index++;
                    Trace.WriteLineIf(debug > 0, "Found:" + lines[index], "TEST");
                    if (reLasten.IsMatch(lines[index]))
                    {
                        MatchCollection mc = reLasten.Matches(lines[index]);

                        dStock["DebitCurrency"] = mc[0].Groups[1].Value;
                        dStock["DebitValue"] = mc[0].Groups[2].Value;
                        Trace.WriteLineIf(debug > 0, "DebitCurrency: " + dStock["DebitCurrency"] + "\tDebitValue: " + dStock["DebitValue"]);
                    }
                    else
                    {
                        Trace.WriteLine("Was:" + lines[index], "TEST");
                        Trace.WriteLine("Wrong Lasten : " + lines[index]);
                    }
                    continue;
                }
                foreach (sLine re in lRegexes)
                {
                    if (re.regex.IsMatch(lines[index]))
                    {
                        Match m = re.regex.Match(lines[index]);
                        if (re.names == null)
                        {
                            dStock[m.Groups[1].Value] = m.Groups[3].Value.Trim();
                            Trace.WriteLineIf(debug > 0, m.Groups[1].Value + "\t" + m.Groups[3].Value, "TEST");
                        }
                        else if (re.names.Length == (m.Groups.Count - 1))
                        {
                            for (int i = 1; i < re.names.Length + 1; i++)
                            {
                                dStock[re.names[i - 1]] = m.Groups[i].Value.Trim();
                                Trace.WriteLineIf(debug > 0, re.names[i - 1] + "\t" + m.Groups[i].Value, "TEST");
                            }
                        }
                        break;
                    }

                }
            }
            foreach (string key in mustHaves)
                if (dStock.ContainsKey(key))
                {
                    sbLine.Append(dStock[key] + ';');
                    dStock.Remove(key);
                }
                else
                {
                    sbLine.Append("UNDEFINED;");
                }
            foreach (string key in dStock.Keys)
            {
                //writeValues(index,1,10,"Stock" , key + ":" + dStock[key] + ';');
            }
            Trace.WriteLine(sbLine);
            tw.WriteLine(sbLine);
        }
        StringBuilder sbLine = new StringBuilder();
        private void writeValues(int index, int begin, int end, string name, string value)
        {
            sbLine.Append(value + ";");
            sResult r = new sResult(index, begin, end, name, value);
            result.Add(r);

        }
        public static void TestDoWork()
        {
            ExtractPDF ep = new ExtractPDF();
            DoWork dw = new DoWork();
            foreach (string file in Directory.EnumerateFiles(@"F:\Benutzer\PapaNetz\Dokumente\comdirect", "Wertpapierabrechnung_Kauf*.pdf", SearchOption.TopDirectoryOnly))
            {
                string text = ep.getText(file);
                if (text == "") continue;
                dw.Test(text);
            }
            dw.Close();
        }

        private void Close()
        {
            tw.Close();
        }

        private void PrintMatch(MatchCollection mc)
        {
            Trace.WriteLine("MatchCount: " + mc.Count, "RE");
            foreach (Match m in mc)
            {
                Trace.WriteLine("\tMATCH: " + m.Value, "RE");
                Trace.WriteLine("\t\tGroupCount: " + m.Groups.Count, "RE");
                foreach (Group g in m.Groups)
                {
                    Trace.WriteLine("\t\t\tGroup: " + g.Value, "RE");

                }
                Trace.WriteLine("\t\tCaptureCount: " + m.Captures.Count, "RE");
                foreach (Capture c in m.Captures)
                {
                    Trace.WriteLine("\t\t\tCapture: " + c.Value, "RE");
                }
            }
        }
    }
}
