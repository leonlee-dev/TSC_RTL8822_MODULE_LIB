using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTKModule
{
    public interface ILog
    {
        string Read();
        void Write(string text);
        void WriteLine(string text);
        void Clear();
        void Save(string path);
    }
}
