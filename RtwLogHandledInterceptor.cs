using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTKModule
{
    public class RtwLogHandledInterceptor : IInterceptor
    {
        private ILog log;

        public RtwLogHandledInterceptor(ILog log)
        {
            this.log = log;
        }

        public void Intercept(IInvocation invocation)
        {
            PreProceed(invocation);
            invocation.Proceed();
            PostProceed(invocation);
        }
        public void PreProceed(IInvocation invocation)
        {
            if (log == null)
                return;

            object[] args = invocation.Arguments;
            string logTime = DateTime.Now.ToString("[yyyyMMdd HH:mm:ss.fff]");

            if (invocation.Method.Name == "Send" || invocation.Method.Name == "WaitFor")
            {
                log.WriteLine(logTime + " >> " + args[0]);
            }
            else if (invocation.Method.Name == "Receive")
            {
                log.WriteLine(logTime + " <<<< " + args[0]);
            }
        }

        public void PostProceed(IInvocation invocation)
        {

        }
    }
}
