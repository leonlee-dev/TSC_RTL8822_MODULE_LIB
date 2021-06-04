using Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RTKModule
{
    public class Adb : AbstractRtwProcessor
    {
        private StreamWriter adbWriter;
        private StreamReader adbReader;

        private Process adb;
        private string path;

        // Declare the delegate (if using non-generic pattern)
        public delegate void ReceiveAdbMessageEventHandler(object sender, DataReceivedEventArgs e);
        public delegate void ExitAdbEventHandler(object sender, EventArgs e);
        // Declare the event
        public event ReceiveAdbMessageEventHandler ReceiveAdbMessageEvent;
        public event ExitAdbEventHandler ExitAdbEvent;

        //public bool isConnected = false;

        public Adb(string execPath)
        {
            path = execPath;
        }

        public void Close()
        {
            if (adbWriter != null)
            {
                adbWriter.Close();
                adbWriter = null;
            }

            if (adbReader != null)
            {
                adbReader.Close();
                adbReader = null;
            }

            if (adb != null)
            {
                adb.Close();
                adb = null;
            }
        }

        //private async Task<bool> ConnectAdbAsync(string ip)
        //{
        //    try
        //    {
        //        adb = new Process();
        //        ProcessStartInfo psi = new ProcessStartInfo();
        //        psi.CreateNoWindow = true;
        //        psi.UseShellExecute = false;
        //        psi.FileName = path;
        //        psi.Arguments = "connect " + ip; // connect
        //        psi.RedirectStandardOutput = true;
        //        psi.RedirectStandardError = true;
        //        psi.RedirectStandardInput = true;
        //        adb.StartInfo = psi;
        //        adb.Start();
        //        adbReader = adb.StandardOutput;
        //        //Task<string> task = adbReader.ReadToEndAsync();
        //        string connect = await adbReader.ReadToEndAsync().ConfigureAwait(false);
        //        Console.WriteLine("connect:" + connect);
        //        if (connect.Contains("cannot"))
        //            isConnected = false;
        //        else
        //            isConnected = true;
        //    }
        //    finally
        //    {
        //        if (adb != null)
        //            adb.Close();
        //    }

        //    return isConnected;
        //}

        //public bool ConnectAdb(string ip)
        //{
        //    return ConnectAdbAsync(ip).Result;
        //}

        //private async Task<bool> OpenAdbShellAsync()
        //{
        //    try
        //    {
        //        adb = new Process();
        //        ProcessStartInfo psi = new ProcessStartInfo();
        //        psi.CreateNoWindow = true;
        //        psi.UseShellExecute = false;
        //        psi.FileName = path;
        //        psi.Arguments = "shell"; // enter shell mode
        //        psi.RedirectStandardOutput = true;
        //        psi.RedirectStandardError = true;
        //        psi.RedirectStandardInput = true;
        //        adb.StartInfo = psi;
        //        adb.Start();
        //        adbReader = adb.StandardOutput;
        //        string open = await adbReader.ReadToEndAsync().ConfigureAwait(false);
        //        Console.WriteLine("open:" + open);
        //        if (!open.Contains("error"))
        //            return false;
        //        return true;
        //    }
        //    finally
        //    {
        //        //if (adb != null)
        //        //    adb.Close();
        //    }
        //}

        public bool OpenAdbShell()
        {
            try
            {
                //if (!isConnected)
                //    return false;

                //if (!OpenAdbShellAsync().Result)
                //    return false;

                adb = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                //psi.WorkingDirectory = adbDir;
                psi.FileName = path;
                psi.Arguments = "shell"; // enter shell mode
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.RedirectStandardInput = true;
                adb.StartInfo = psi;
                adb.EnableRaisingEvents = true;
                if (ExitAdbEvent != null) adb.Exited += new EventHandler(ExitAdbEvent);
                if (ReceiveAdbMessageEvent != null) adb.OutputDataReceived += new DataReceivedEventHandler(ReceiveAdbMessageEvent);
                adb.Start();
                adb.BeginOutputReadLine();
                adb.BeginErrorReadLine();

                // redirect standard input to adbWriter
                adbWriter = adb.StandardInput;

                return true;
            }
            finally
            {
                
            }
        }

        public override void SendRtwCommand(string cmd, int timeoutMs = 0)
        {
            if (adbWriter != null) adbWriter.WriteLine(cmd);
            if (timeoutMs > 0) Thread.Sleep(timeoutMs);
        }

        public override void ReceiveRtwCommand(string text)
        {
            receiveBuffer.Append(text);
        }
    }
}
