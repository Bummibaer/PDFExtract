using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFExtract
{
    public class Data 
    {
        public struct sData
        {
            public string regex;
            public string result;
 
            public sData(string regex,string result) 
            {
                this.regex = regex;
                this.result = result;
            }
        };

        public BindingList<sData> blData = new BindingList<sData>();

         public Data()
        {
            blData.Add(new sData("test", "Hallo"));
        }

    }
}
