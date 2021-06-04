using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace RTKModule
{
    public class AbstractRtwProcessor : IRtwProcessor
    {
        public StringBuffer receiveBuffer = new StringBuffer(1024);

        public virtual void ReceiveRtwCommand(string text)
        {
            // user must self-implement this function
            Console.WriteLine("Recv: " + text);
        }

        public virtual void SendRtwCommand(string cmd, int timeoutMs = 0)
        {
            // user must self-implement this function
            Console.WriteLine("Send: " + cmd);
        }

        public virtual bool WaitForReply(string strCmd, Regex pattern, ref string strReturn, int timeoutMs)
        {
            // clear
            receiveBuffer.Clear();

            DateTime startTime;
            int retryCount = 3;
            while (retryCount > 0)
            {
                if (strCmd != "") SendRtwCommand(strCmd);
                startTime = DateTime.Now;
                while (true)
                {
                    Thread.Sleep(10);
                    strReturn = receiveBuffer.ToString();
                    if (pattern.IsMatch(strReturn))
                    {
                        strReturn = pattern.Match(strReturn).Value;
                        return true;
                    }

                    TimeSpan tDiff = (DateTime.Now).Subtract(startTime);
                    if (tDiff.TotalMilliseconds >= timeoutMs)
                    {
                        break; // timeout --> to retry again
                    }
                }
                retryCount--;
            }
            return false;
        }

        public virtual bool WaitForReply(string strCmd, string strWaitFor, int timeoutMs)
        {
            // clear
            receiveBuffer.Clear();

            DateTime startTime;
            int retryCount = 3;
            while (retryCount > 0)
            {
                if (strCmd != "") SendRtwCommand(strCmd);
                startTime = DateTime.Now;
                while (true)
                {
                    Thread.Sleep(10);
                    string strTemp = receiveBuffer.ToString();
                    int i = strTemp.IndexOf(strWaitFor, 0);
                    if (i >= 0)
                        return true;

                    TimeSpan tDiff = (DateTime.Now).Subtract(startTime);
                    if (tDiff.TotalMilliseconds >= timeoutMs)
                    {
                        break; // timeout --> to retry again
                    }
                }
                retryCount--;
            }
            return false;
        }
    }
}
