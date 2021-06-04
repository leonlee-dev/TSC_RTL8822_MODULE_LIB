using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace RTKModule
{
    public class ComPort : AbstractRtwProcessor
    {
        // Declare the delegate (if using non-generic pattern)
        public delegate void ReceiveSerialMessageEventHandler(object sendor, SerialDataReceivedEventArgs args);
        // Declare the event
        public event ReceiveSerialMessageEventHandler ReceiveSerialMessageEvent;

        private SerialPort sp;

        public ComPort(string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            sp = new SerialPort();
            sp.PortName = portName;
            sp.BaudRate = baudRate;
            sp.DataBits = dataBits;
            sp.StopBits = stopBits;
            sp.Parity = parity;
        }

        public void Open()
        {
            if (ReceiveSerialMessageEvent != null) sp.DataReceived += new SerialDataReceivedEventHandler(ReceiveSerialMessageEvent);
            if (!sp.IsOpen)
                sp.Open();   
        }

        public void Close()
        {
            if(sp.IsOpen)
                sp.Close();
        }

        public override void ReceiveRtwCommand(string text)
        {
            receiveBuffer.Append(text);
        }

        public override void SendRtwCommand(string cmd, int timeoutMs = 0)
        {
            sp.WriteLine(cmd);
            if (timeoutMs > 0) Thread.Sleep(timeoutMs);
        }
    }
}
