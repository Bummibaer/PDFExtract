using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace PDFExtract
{
    public class Data : BindingList<Data>
    {
        private string regex;
        private string result;

        public string Regex
        {
            get
            {
                return regex;
            }

            set
            {
                Trace.WriteLine("Set Regex to " + value, "DATA");
                regex = value;
            }
        }

        public string Result
        {
            get
            {
                return result;
            }

            set
            {
                Trace.WriteLine("Set Result to " + value, "DATA");
                result = value;
            }
        }

        public Data(string regex, string result)
        {
            this.Regex = regex;
            this.Result = result;
        }


        public Data()
        {
            Trace.WriteLine("Construct", "DATA");
            regex = "";
            result = "";
        }

    }
}
