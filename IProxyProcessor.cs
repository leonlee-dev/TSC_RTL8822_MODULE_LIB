using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RTKModule
{
    // proxy interface
    public interface IProxyProcessor
    {
        void Send(string cmd, int timeoutMs = 0);
        void Receive(string text);
        bool WaitFor(string strCmd, Regex pattern, ref string strReturn, int timeoutMs);
        bool WaitFor(string strCmd, string strWaitFor, int timeoutMs);
    }
}
