using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTKModule
{
    public abstract class Rtw
    {
        public int freq { get; set; }
        public BW bw { get; set; }
        public ANT_PATH antPath { get; set; }
        public RATE_ID rateID { get; set; }
    }
}
