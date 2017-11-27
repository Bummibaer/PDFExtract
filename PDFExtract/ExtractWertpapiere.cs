﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PDFExtract
{
    class ExtractWertpapiere
    {

        // x ref(qr($p))
        ////                       GESCHï¿½FTSABRECHNUNG VOM 17.08.2010
        ////                    *   Wertpapierkauf
        ////                        Geschï¿½ftsnummer          : 92 006773
        ////                                                   225608597130        Rechnungsnummer          : 209151660613DF35
        ////                        Geschï¿½ftstag             : 16.08.2010          Ausfï¿½hrungsplatz         : FRANKFURT SCOACH
        ////                                                                                                  (Kommissionsgeschï¿½ft)
        ////                        Wertpapier-Bezeichnung                                               WPKNR/ISIN
        ////                        Commerzbank AG                                                           CB2458
        ////                        Gold Qanto Zert.(2005/unlim.)                                      DE000CB24589
        ////                        Fï¿½lligkeit        : LAUFZEIT UNBEFRISTET/AUSLOSUNG/KUENDIGUNG ODER RUECKKAUF
        ////                                            MOEGLICH
        ////
        ////                        Nennwert                           Zum Kurs von
        ////                        St. 0,93                           EUR 105,90
        ////                                                  Kurswert                    : EUR               98,49
        ////                        --------------------------------------------------------------------------------
        ////                        Eigene Entgelte
        ////                                                  Provision                   : EUR                1,48
        ////                        --------------------------------------------------------------------------------
        ////
        ////                        Verrechnung ï¿½ber Konto            BLZ          Valuta               Zu Ihren Lasten vor Steuern
        ////                             4202040 00 EUR               200 411 33   18.08.2010              EUR               99,97
        ////                        Verwahrungs-Art: GIROSAMMELDEPOT
        ////

        ////                                           Umrechnung zum Devisenkurs 1,477000 : EUR               99,94
        ////                                                         2,62500% Bonifikation : USD                3,98
        //-

        //////Wertpapier-Bezeichnung                                               WPKNR/ISIN
        //////                                                                              SFL9ZM
        //////29,25000% Sal.Oppenheim jr. & Cie. KGaA
        //////                                                                 DE000SFL9ZM1
        //////         PROTECT-IHS v.08(09) ALV

        // MAIN//

        internal struct sRules
        {
            internal Regex re;
            internal string[] names;
            public sRules(Regex re, string[] names)
            {
                this.re = re;
                this.names = names;
            }
        }

        internal struct sStocks
        {
            public string Name;
            public string WPKNR;
            public string Bank;
            public string ISIN;
            public string DebitCurrency;
            public decimal DebitValue;

        }

        List<sRules> lRules = new List<sRules>();

        private int debug = 0;

        public ExtractWertpapiere()
        {
            ReadParser();
            DoWork();
        }
        void ReadParser()
        {
            Trace.WriteLine("Read Parser ");
            using (TextReader tr = File.OpenText("rules.txt"))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    string[] parts = line.Split(new char[] { ':' }, 2);
                    if (debug > 1) Trace.WriteLine(parts[0] + "\t-\t" + parts[1], "PARSE");
                    string[] names = parts[0].Split(new char[] { ',' });
                    try
                    {
                        Regex re = new Regex(parts[1]);
                        re.IsMatch("test");
                        sRules sRule = new sRules(re, names);
                        lRules.Add(sRule);
                    }
                    catch (ArgumentException)
                    {
                        Trace.WriteLine("Wrong RegEx : " + parts[1] + '!');
                        Environment.Exit(1);
                    }
                }
            }

        }

        void DoWork()
        {
            TextWriter tw = File.CreateText("ertrag.csv");
            tw.WriteLine("File;Datum;Sonder;Art;Geschäftsnummer;Geschäftstag;WPKNR;Name;ISIN;Test1;Test2;Rest");

            string dir = @"F:\Benutzer\PapaNetz\Dokumente\comdirect\";
            string pdfs = "Wertpapierabrechnung_*.pdf";
            string text;
            // $Folder = "P:\\privat\\comdirect\\";

            Trace.WriteLine("Read from " + pdfs + "!");

            char[] c = new char[] { '|', '\\', '-', '/' };
            int cindex = 0;

            foreach (string filename in Directory.GetFiles(dir, pdfs))
            {
                ////Trace.WriteLine( "$file\n";
                tw.Write(filename + ';');

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
                /*
                                if (filename.Contains("973347"))
                                {
                                    Trace.WriteLine(filename);
                                    debug = 2;
                                }

                  */
                try
                {
                    ExtractPDF ep = new ExtractPDF();
                    text = ep.getText(filename);
                }
                catch (iTextSharp.text.exceptions.InvalidPdfException ipe)
                {
                    Trace.WriteLine(Environment.NewLine + "Could not read " + filename, "WARNING");
                    continue;
                }
                if (text.Length == 0)
                {
                    Trace.WriteLine(Environment.NewLine + "Could not read " + filename, "WARNING");
                    continue;
                }
                string[] lines = text.Split(new char[] { '\n' });
                sStocks stock = new sStocks();
                int lindex = 0;
                do
                {
                    string line = getNextLine(lines, ref lindex);

                    if (debug > 1) Trace.WriteLine(line);
                    //// Next Line, how to template ?
                    ////                        Wertpapier-Bezeichnung                                               WPKNR/ISIN
                    ////                        Commerzbank AG                                                           CB2458
                    ////                        Gold Qanto Zert.(2005/unlim.)                                      DE000CB24589
                    Regex re = new Regex(@"Wertpapier-Bezeichnung\s+WPKNR/ISIN");
                    if (re.IsMatch(line))
                    {
                        if (debug > 1) Trace.WriteLine(line);
                        line = getNextLine(lines, ref lindex);
                        //// hint
                        re = new Regex(@"\s{2,}");
                        string[] n = re.Split(line);

                        switch (n.Length)
                        {
                            case 2:
                                stock.Name = n[0];
                                stock.WPKNR = n[1];
                                break;
                            default:
                                Trace.WriteLine("Canot read Name + WPNKNR ", "ERROR");
                                Trace.WriteLine(line);
                                break;
                        }

                        tw.Write(stock.WPKNR + ';' + stock.ISIN + ';');
                        if (debug > 0) Trace.WriteLine("Name;WPKNR>>>>>>>>>>>>>>>>>>>>>>>>>>> " +
                            stock.Name + ';' + stock.WPKNR);


                        line = getNextLine(lines, ref lindex);

                        n = re.Split(line);
                        switch (n.Length)
                        {
                            case 2:
                                stock.Name += ' ' + n[0];
                                stock.ISIN = n[1];
                                break;
                            default:
                                Trace.WriteLine("Cannot read Name + ISIN ", "ERROR");
                                Trace.WriteLine(line);
                                break;
                        }

                        tw.Write(stock.WPKNR + ';' + stock.ISIN + ';');
                        if (debug > 0) Trace.WriteLine("Name;ISIN>>>>>>>>>>>>>>>>>>>>>>>>>>> " +
                            stock.Name + ';' + stock.ISIN);
                    }

                    // Test Valuta 
                    if (line.Contains("Valuta"))
                    {
                        Trace.WriteLine("");
                    }
                    re = new Regex(@"Valuta\s+Zu Ihren");
                    if (re.IsMatch(line))
                    {
                        line = getNextLine(lines, ref lindex);

                        re = new Regex(@"(\w{3})\s+(-*[\d.]+,\d+-*)");
                        if (re.IsMatch(line))
                        {
                            MatchCollection mc = re.Matches(line);
                            stock.DebitCurrency = mc[0].Groups[1].Value;
                            decimal.TryParse(mc[0].Groups[2].Value, out stock.DebitValue);

                        }
                        else
                        {
                            Trace.WriteLine("Could not extract Valuta", "ERROR");
                            tw.Write(stock.DebitCurrency + ';' + stock.DebitValue);
                            if (debug > 0) Trace.WriteLine("Debit:>>>>>>>>>>>>>>>>>>>>>>>>>>> ;" + stock.DebitCurrency + ';' + stock.DebitValue);
                        }
                    }
                    // Test Rules
                    foreach (sRules rule in lRules)
                    {
                        if (rule.re.IsMatch(line))
                        {
                            MatchCollection mc = rule.re.Matches(line);
                            if (debug > 0) Trace.WriteLine("Matches :" + rule.re.ToString() + "\t Matches:" + mc[0].Groups.Count);
                            int index = 1;

                            foreach (string name in rule.names)
                            {

                                if (debug > 0) Trace.WriteLine("M:" + name + " -> " + mc[0].Groups[index].Value);

                                tw.Write(mc[0].Groups[index].Value + ';');
                                if (debug > 0) Trace.WriteLine("M:>>>>>>>>>>>>>>>>>>>>>>>>>>> " + mc[0].Groups[index].Value + ';');

                                index++;
                            }
                            break;
                        }
                    }

                } while (lindex < lines.Length);
                tw.WriteLine("");
                if (debug > 0) Trace.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>> \n");
                tw.Flush();
                if (debug > 0) Trace.WriteLine(" Ende ");
            }
            tw.Close();
            Trace.WriteLine("Ende");
        }

        private string getNextLine(string[] lines, ref int lindex)
        {
            string line;
            do
            {

                line = lines[lindex++];
                if (debug > 1) Trace.WriteLine(line);
            } while (line.Length < 2);
            line = line.Trim(new char[] { '\n', '\r', ' ' });

            return line;
        }


        //Verrechnung ï¿½ber Konto Valuta Zu Ihren Lasten
        //                            4202040 00 EUR               200 411 33   18.08.2010              EUR               99,97
        // IBAN Valuta Zu Ihren Lasten vor Steuern
        // DE02 2004 1133 0420 2040 00                                                                  EUR 05.07.2017 EUR 39,93
        //'                     4202040 00 EUR                       21.04.2006                  EUR                     49,77'

    }
}