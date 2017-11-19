using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace PDFExtract
{
    
    public class Data 
    {


        DataTable dt = new DataTable("Data");

        public DataTable Dt
        {
            get
            {
                return dt;
            }

            set
            {
                dt = value;
            }
        }

        public Data()
        {
            Trace.WriteLine("Initialize Data !", "DATA");
            dt.Columns.Add("Name", typeof(String));
            dt.Columns.Add("Test", typeof(String));
            //dt.
        }

       }


}
