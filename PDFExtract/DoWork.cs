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
        Template template;

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

        List<string> lColumns = new List<string>();
        Dictionary<string, string> dStock = new Dictionary<string, string>();
        List<Dictionary<string, string>> lStocks = new List<Dictionary<string, string>>();

        TextWriter tw;

        public DoWork()
        {
            // Regexes für Tabelle
            //
            template = new Template();

            tw = File.CreateText("test.csv");

            lColumns.Add("Fonds");
        }

        bool first = true;
        /// <summary>
        /// Parse text from PDF for 
        /// </summary>
        /// <param name="text"></param>
        public void ParseText(string text)
        {

            string[] newline = { "\n" };

            Regex reBedingung = new Regex(@"Wertpapier-Bezeichnung\s+WPKNR/ISIN");
            Regex reValuta = new Regex(@"IBAN\s+Valuta\s+Zu\s+Ihren\s+(\w+)");
            Regex reLasten = new Regex(@"(\w{3}\s+-*[.\d]+,[.\d]+-*)");
            Regex reTest = new Regex(@"(.+)(?! {2,}) (.+)");
            MatchCollection mc;
            dStock = new Dictionary<string, string>();

            string[] lines = text.Split(newline, StringSplitOptions.None);
            int textIndex = 0;
            for (int index = 0; index < lines.Length; index++)
            {
                //if (index == 38)
                //{
                //    Trace.WriteLine("");
                //}
                Trace.WriteLineIf(debug > 1, index.ToString("D3") + "\t: L=" + lines[index].Length + "\t<" + lines[index] + '>', "TEST");
                //## Next Line, how to template ?
                if (reBedingung.IsMatch(lines[index]))
                {
                    Trace.WriteLineIf(debug > 0, "Found:" + lines[index], "TEST");
                    // Goto Next Line
                    GetNextLine(newline, lines, ref textIndex, ref index);
                    mc = reTest.Matches(lines[index]);

                    if (mc.Count == 1)
                    {
                        writeValues(mc[0].Groups[1].Index + textIndex, mc[0].Groups[1].Length, "Fonds", mc[0].Groups[1].Value.Trim());
                        writeValues(mc[0].Groups[2].Index + textIndex, mc[0].Groups[2].Length, "WPKNR", mc[0].Groups[2].Value.Trim());
                    }
                    else if (mc.Count == 0)
                    {
                        Trace.WriteLine("10.:" + lines[index], "ERROR");
                        throw new Exception("Fehler");
                    }

                    GetNextLine(newline, lines, ref textIndex, ref index);
                    mc = reTest.Matches(lines[index]);

                    if (mc.Count == 1)
                    {
                        dStock["Fonds"] += ' ' + mc[0].Groups[1].Value.Trim();
                        writeValues(mc[0].Groups[2].Index + textIndex, mc[0].Groups[2].Length, "ISIN", mc[0].Groups[2].Value.Trim());
                    }
                    else if (mc.Count == 0)
                    {
                        Trace.WriteLine("10.:" + lines[index], "ERROR");
                        throw new Exception("Fehler");
                    }
                }
                else if (reValuta.IsMatch(lines[index]))
                {
                    GetNextLine(newline, lines, ref textIndex, ref index);

                    if (reLasten.IsMatch(lines[index]))
                    {
                        mc = reLasten.Matches(lines[index]);
                        writeValues(mc[0].Groups[2].Index + textIndex, mc[0].Groups[1].Length, "DebitValue", mc[0].Groups[1].Value.Trim());
                    }
                    else
                    {
                        Trace.WriteLine("Was:" + lines[index], "TEST");
                        Trace.WriteLine("Wrong Lasten : " + lines[index]);
                    }
                }
                else foreach (Template.sRule re in template.LRules)
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
            if (first)
            {
                foreach (string name in lColumns)
                {
                    Trace.Write(name + "\t;");
                    tw.Write(name + ";");
                }
                Trace.WriteLine("");
                tw.WriteLine("");
                first = false;
            }
            foreach (string name in lColumns)
            {
                if (dStock.ContainsKey(name))
                {
                    tw.Write(dStock[name] + ';');
                }
                else
                {
                    tw.Write("-;");
                }

            }
            tw.WriteLine("");
            tw.Flush();
        }

        private void GetNextLine(string[] newline, string[] lines, ref int textIndex, ref int index)
        {
            textIndex += (lines[index].Length + newline.Length);
            index++;
            Trace.WriteLineIf(debug > 0, "Found:" + lines[index], "TEST");
        }

        private void writeValues(int index, int length, string name, string value)
        {
            dStock[name] = value;

            if (!lColumns.Contains(name))
            {
                lColumns.Add(name);
                Trace.WriteLine(name + '\t' + value, "New Property");
            }
            sResult r = new sResult(index, value.Length, name, value);
            Results.Add(r);


        }

        static string dir = @"F:\Benutzer\PapaNetz\Dokumente\comdirect\cominvest\";
        public static void TestDoWork()
        {
            ExtractPDF ep = new ExtractPDF();
            DoWork dw = new DoWork();
            char[] c = new char[] { '|', '\\', '-', '/' };
            int cindex = 0;
            int debug = 0;

            Trace.WriteLine("Work on : "
                + dir
                + "Wertpapierabrechnung_*.pdf"
                + "\t"
                + Directory.Exists(dir)
                );

            int line = 0;
            foreach (string filename in Directory.EnumerateFiles(dir, "Wertpapierabrechnung_*.pdf"))
            {
                if (debug == 0)
                {
                    Trace.Write(c[cindex++].ToString() + '\b');
                    if (cindex >= c.Length)
                    {
                        cindex = 0;
                    }
                    if (line++ == 64)
                    {
                        Trace.WriteLine("");
                        line = 0;
                    }
                }
                else
                {
                    Trace.WriteLine("Read : " + filename);
                }
                string text = ep.getText(filename);
                //if (filename.Contains("NORDEA"))
                //{
                //    dw.debug = 3;
                //}
                dw.ParseText(text);
            }
            dw.Close();
        }
        public static void TestDoWork(string filename)
        {
            ExtractPDF ep = new ExtractPDF();
            DoWork dw = new DoWork();

            string text = ep.getText(dir + filename);
            dw.debug = 3;

            dw.ParseText(text);
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
