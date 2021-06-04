using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RTKModule
{
    public interface IRtwProcessor
    {
        void SendRtwCommand(string cmd, int timeoutMs = 0);
        void ReceiveRtwCommand(string text);
        bool WaitForReply(string strCmd, Regex pattern, ref string strReturn, int timeoutMs);
        bool WaitForReply(string strCmd, string strWaitFor, int timeoutMs);
    }
}
