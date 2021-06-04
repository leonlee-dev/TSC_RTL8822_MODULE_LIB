using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTKModule
{
    public class RtwTx : Rtw
    {
        public TX_MODE txMode { get; set; }

        public RtwTx() { }

        public RtwTx(int freq, BW bw, ANT_PATH antPath, RATE_ID rateID, TX_MODE txMode)
        {
            this.freq = freq;
            this.bw = bw;
            this.antPath = antPath;
            this.rateID = rateID;
            this.txMode = txMode;
        }
    }
}
