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
    class Template
    {
        public struct sRule
        {
            internal Regex re;
            internal string[] names;
            public string  reString {
                get {
                    return re.ToString();
                }
                }
            public sRule(Regex re, string[] names)
            {
                this.re = re;
                this.names = names;
            }
        }

         List<sRule> lRules = new List<sRule>();

        int debug = 0;
        public Template()
        {
            ReadParser();
        }

        public int Debug
        {
            get
            {
                return debug;
            }

            set
            {
                debug = value;
            }
        }

        public  List<sRule> LRules
        {
            get
            {
                return lRules;
            }

            set
            {
                lRules = value;
            }
        }

        private void ReadParser()
        {
            Trace.WriteLine("Read Parser ");
            using (TextReader tr = File.OpenText(Properties.Settings.Default.TemplateFIle))
            {
                string line;
                while ((line = tr.ReadLine()) != null)
                {
                    if (line[0] == '#') continue;
                    string[] parts = line.Split(new char[] { ':' }, 2);
                    if (debug > 1) Trace.WriteLine(parts[0] + "\t-\t" + parts[1], "PARSE");
                    string[] names = parts[0].Split(new char[] { ',' });
                    try
                    {
                        Regex re = new Regex(parts[1]);
                        re.IsMatch("test");
                        sRule sRule = new sRule(re, names);
                        LRules.Add(sRule);
                    }
                    catch (ArgumentException)
                    {
                        Trace.WriteLine("Wrong RegEx : " + parts[1] + '!');
                        Environment.Exit(1);
                    }
                }
            }

        }

    }
}