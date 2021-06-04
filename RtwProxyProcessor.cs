using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RTKModule
{
    public class RtwProxyProcessor : IProxyProcessor
    {
        private IRtwProcessor rtwProcessor;

        public RtwProxyProcessor(IRtwProcessor rtwProcessor)
        {
            this.rtwProcessor = rtwProcessor;
        }

        public virtual void Send(string cmd, int timeoutMs = 0)
        {
            rtwProcessor.SendRtwCommand(cmd, timeoutMs);
        }

        public virtual void Receive(string text)
        {
            rtwProcessor.ReceiveRtwCommand(text);
        }

        public virtual bool WaitFor(string strCmd, Regex pattern, ref string strReturn, int timeoutMs)
        {
            return rtwProcessor.WaitForReply(strCmd, pattern, ref strReturn, timeoutMs);
        }

        public virtual bool WaitFor(string strCmd, string strWaitFor, int timeoutMs)
        {
            return rtwProcessor.WaitForReply(strCmd, strWaitFor, timeoutMs);
        }
    }
}
