using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTKModule
{
    public class RtwProxyCreator
    {
        public static RtwProxyProcessor CreatProxy(IRtwProcessor rtwProcessor, params IInterceptor[] interceptors)
        {
            ProxyGenerator generator = new ProxyGenerator();
            return (RtwProxyProcessor)generator.CreateClassProxy(typeof(RtwProxyProcessor), new object[] { rtwProcessor }, interceptors);
        }
    }
}
