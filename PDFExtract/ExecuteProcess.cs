using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFExtract
{
    class ExecuteProcess
    {
        private string mCommand;
        private List<string> text;
        private int debug = 0;

        public List<string> Text
        {
            get
            {
                return text;
            }
       }

        public ExecuteProcess(string command)
        {
            mCommand = command;
            text = new List<string>();
        }
        public void Run(string filename)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = mCommand;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = " -layout " + filename + " -";
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;

            Trace.WriteLine("Execute " + startInfo.FileName + startInfo.Arguments, "PROCESS");
            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process process = new Process())
                {
                    process.OutputDataReceived += ExeProcess_OutputDataReceived;
                    process.ErrorDataReceived += ExeProcess_ErrorDataReceived;
                    process.StartInfo = startInfo;

                    process.Start();
                    process.BeginErrorReadLine();
                    process.BeginOutputReadLine();
                    process.WaitForExit();
                    Trace.WriteLine("Command finished after " + process.UserProcessorTime.Milliseconds + "ms", "PROCESS");
                }
            }
            catch
            {
                Trace.WriteLine("Cannot Process !?", "PROCESS");
                throw;
            }
        }

        private void ExeProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (debug > 0 ) Trace.WriteLine(e.Data, "PROCESS");
            text.Add(e.Data);
        }
        private void ExeProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Trace.WriteLine(e.Data, "PROCESSERROR");
        }
    }
}
