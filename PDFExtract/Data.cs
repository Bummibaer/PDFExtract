using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace PDFExtract
{
    public class Data : List<string>, INotifyPropertyChanged
    {
        List<String> lHeader = new List<string>();
        List<String> lTexts = new List<string>();
        string tre;

  
        public Data()
        {
        }

        public Data(int capacity) : base(capacity)
        {
        }

        public Data(IEnumerable<string> collection) : base(collection)
        {
        }

        public Data(string tre)
        {
            this.tre = tre;

        }


  
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            Trace.WriteLine("NotifyProperty : " + propertyName, "DATA");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
