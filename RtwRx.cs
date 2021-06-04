using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTKModule
{
    public class RtwRx : Rtw
    {
        public RtwRx() { }

        public RtwRx(int freq, BW bw, ANT_PATH antPath, RATE_ID rateID)
        {
            this.freq = freq;
            this.bw = bw;
            this.antPath = antPath;
            this.rateID = rateID;
        }
    }
}
