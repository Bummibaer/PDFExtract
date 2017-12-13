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
            public int lineIndex { get; }
            public int length { get; }
            public string name { get; }
            public string value { get; }


            public sResult(int index, int length, string name, string value)
            {
                lineIndex = index;
                this.length = length;
                this.name = name;
                this.value = value;
            }
        };

        public List<sResult> Results = new List<sResult>();

        public int debug { get; set; }
        Dictionary<string, string> dStock = new Dictionary<string, string>();
        List<Dictionary<string, string>> lStocks = new List<Dictionary<string, string>>();

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


        /// <summary>
        /// Parse text from PDF for 
        /// </summary>
        /// <param name="text"></param>
        public void ParseText(string text)
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


            string[] newline = { "\n" };

            Regex reBedingung = new Regex(@"Wertpapier-Bezeichnung\s+WPKNR/ISIN");
            Regex reLast = new Regex(@"Zu Ihren Lasten");
            Regex reLasten = new Regex(@"(\w{3})\s+(-*[.\d]+,[.\d]+-*)");
            Regex reTest = new Regex(@"(.+)(?! {2,}) (.+)");

            string[] lines = text.Split(newline, StringSplitOptions.None);
            int textIndex = 0;
            for (int index = 0; index < lines.Length; index++)
            {
                Trace.WriteLineIf(debug > 1, index.ToString("D3") + ":" + lines[index], "TEST");
                //## Next Line, how to template ?
                if (reBedingung.IsMatch(lines[index]))
                {
                    dStock = new Dictionary<string, string>();
                    Trace.WriteLineIf(debug > 0, "Found:" + lines[index++], "TEST");
                    // hint
                    Trace.WriteLineIf(debug > 0, "\t" + lines[index], "TEST");
                    MatchCollection mc = reTest.Matches(lines[index++]);

                    if (mc.Count == 1)
                    {
                        writeValues(mc[0].Groups[1].Index + textIndex, mc[0].Groups[1].Length, "Fonds", mc[0].Groups[1].Value.Trim());
                        writeValues(mc[0].Groups[2].Index + textIndex, mc[0].Groups[2].Length, "WPKNR", mc[0].Groups[2].Value.Trim());
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
                        writeValues(mc[0].Groups[2].Index + textIndex, mc[0].Groups[2].Length, "ISIN", mc[0].Groups[2].Value.Trim());
                        writeValues(mc[0].Groups[1].Index + textIndex, mc[0].Groups[1].Length, "Name", mc[0].Groups[1].Value.Trim());
                    }
                    else if (mc.Count == 0)
                    {
                        Trace.WriteLine("10.:" + lines[index], "TEST");
                        throw new Exception("Fehler");
                    }
                    continue;
                }

                else if (reLast.IsMatch(lines[index]))
                {
                    index++;
                    Trace.WriteLineIf(debug > 0, "Found:" + lines[index], "TEST");
                    if (reLasten.IsMatch(lines[index]))
                    {
                        MatchCollection mc = reLasten.Matches(lines[index]);
                        writeValues(mc[0].Groups[1].Index + textIndex, mc[0].Groups[1].Length, "DebitCurrency", mc[0].Groups[1].Value.Trim());
                        writeValues(mc[0].Groups[2].Index + textIndex, mc[0].Groups[2].Length, "DebitValue", mc[0].Groups[2].Value.Trim());
                    }
                    else
                    {
                        Trace.WriteLine("Was:" + lines[index], "TEST");
                        Trace.WriteLine("Wrong Lasten : " + lines[index]);
                    }
                }
                else foreach (sLine re in lRegexes)
                    {
                        if (re.regex.IsMatch(lines[index]))
                        {
                            Match m = re.regex.Match(lines[index]);
                            Trace.WriteLineIf(debug > 1, lines[index], "DEBUG");
                            Trace.WriteLineIf(debug > 2, (re.names == null ? 0 : re.names.Length) + " Names\tRegEx: /" + re.regex.ToString() + "/\tGroups:" + m.Groups.Count);
                            if (re.names == null)
                            {
                                writeValues(m.Groups[1].Index + textIndex, m.Groups[1].Length, m.Groups[1].Value.Trim(), m.Groups[3].Value.Trim());
                            }
                            else
                            if (re.names.Length == (m.Groups.Count - 1))
                            {
                                for (int i = 1; i < re.names.Length + 1; i++)
                                {
                                    writeValues(m.Groups[i].Index + textIndex, m.Groups[i].Length, re.names[i - 1], m.Groups[i].Value.Trim());
                                }
                            }
                            else
                            {
                                Trace.WriteLine("Wrong Name count", "ERROR");
                            }
                            break;
                        }

                    }
                textIndex += (lines[index].Length + newline.Length);

            } // foreach line
            lStocks.Add(dStock);

            Trace.WriteLine(sbLine);
            tw.WriteLine(sbLine);
        }

        private StringBuilder sbLine = new StringBuilder();

        private void writeValues(int index, int length, string name, string value)
        {
            dStock[name] = value;
            sbLine.Append(value + ";");

            sResult r = new sResult(index, value.Length, name, value);
            Results.Add(r);

            Trace.WriteLineIf(debug > 0, name + ":" + value, "VALUES");

        }
        public static void TestDoWork()
        {
            ExtractPDF ep = new ExtractPDF();
            DoWork dw = new DoWork();
            foreach (string file in Directory.EnumerateFiles(@"F:\Benutzer\PapaNetz\Dokumente\comdirect", "Wertpapierabrechnung_Kauf*.pdf", SearchOption.TopDirectoryOnly))
            {
                string text = ep.getText(file);
                if (text == "") continue;
                dw.ParseText(text);
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
