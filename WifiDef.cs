using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RTKModule
{
    // 20M = 0, 40M = 1, 80M = 2
    public enum BW
    {
        B_20MHZ,
        B_40MHZ,
        B_80MHZ
    }

    // Path A = a, Path B = b, Path C = c, Path D = d, Path AB(2x2) = ab,
    public enum ANT_PATH
    {
        PATH_A = 0,
        PATH_B = 1,
        //PATH_C = 2,
        //PATH_D = 3,
        PATH_AB = 4
    }

    // Packet Tx = 1, Continuous Tx = 2, OFDM Single Tone Tx = 3
    public enum TX_MODE
    {
        PACKET_TX = 1,
        CONTINUOUS_TX,
        OFDM_SINGLE_TONE_TX
    }

    public enum RATE_ID
    {
        R_1M,
        R_2M,
        R_5_5M,
        R_11M,
        R_6M,
        R_9M,
        R_12M,
        R_18M,
        R_24M,
        R_36M,
        R_48M,
        R_54M,
        HTMCS0,
        HTMCS1,
        HTMCS2,
        HTMCS3,
        HTMCS4,
        HTMCS5,
        HTMCS6,
        HTMCS7,
        HTMCS8,
        HTMCS9,
        HTMCS10,
        HTMCS11,
        HTMCS12,
        HTMCS13,
        HTMCS14,
        HTMCS15,
        HTMCS16,
        HTMCS17,
        HTMCS18,
        HTMCS19,
        HTMCS20,
        HTMCS21,
        HTMCS22,
        HTMCS23,
        HTMCS24,
        HTMCS25,
        HTMCS26,
        HTMCS27,
        HTMCS28,
        HTMCS29,
        HTMCS30,
        HTMCS31,
        VHT1MCS0,
        VHT1MCS1,
        VHT1MCS2,
        VHT1MCS3,
        VHT1MCS4,
        VHT1MCS5,
        VHT1MCS6,
        VHT1MCS7,
        VHT1MCS8,
        VHT1MCS9,
        VHT2MCS0,
        VHT2MCS1,
        VHT2MCS2,
        VHT2MCS3,
        VHT2MCS4,
        VHT2MCS5,
        VHT2MCS6,
        VHT2MCS7,
        VHT2MCS8,
        VHT2MCS9
    }

    public enum CH
    {
        CH1 = 2412,
        CH2 = 2417,
        CH3 = 2422,
        CH4 = 2427,
        CH5 = 2432,
        CH6 = 2437,
        CH7 = 2442,
        CH8 = 2447,
        CH9 = 2452,
        CH10 = 2457,
        CH11 = 2462,
        CH12 = 2467,
        CH13 = 2472,
        CH14 = 2484,
        CH36 = 5180,
        CH38 = 5190,
        CH40 = 5200,
        CH42 = 5210,
        CH44 = 5220,
        CH46 = 5230,
        CH48 = 5240,
        CH52 = 5260,
        CH54 = 5270,
        CH56 = 5280,
        CH58 = 5290,
        CH60 = 5300,
        CH62 = 5310,
        CH64 = 5320,
        CH68 = 5340,
        CH96 = 5480,
        CH100 = 5500,
        CH102 = 5510,
        CH104 = 5520,
        CH106 = 5530,
        CH108 = 5540,
        CH110 = 5550,
        CH112 = 5560,
        CH116 = 5580,
        CH118 = 5590,
        CH120 = 5600,
        CH122 = 5610,
        CH124 = 5620,
        CH126 = 5630,
        CH128 = 5640,
        CH132 = 5660,
        CH134 = 5670,
        CH136 = 5680,
        CH138 = 5690,
        CH140 = 5700,
        CH142 = 5710,
        CH144 = 5720,
        CH149 = 5745,
        CH151 = 5755,
        CH153 = 5765,
        CH155 = 5775,
        CH157 = 5785,
        CH159 = 5795,
        CH161 = 5805,
        CH165 = 5825,
        CH167 = 5835,
        CH169 = 5845,
        CH171 = 5855,
        CH173 = 5865,
        CH175 = 5875,
        CH177 = 5885,
        //CH184 = 4920,
        //CH188 = 4940,
        //CH192 = 4960,
        //CH196 = 4980
    }

    public class Wifi
    {
        public static readonly Dictionary<BW, string> bwDic = new Dictionary<BW, string>()
        {
            { BW.B_20MHZ, "20M" },
            { BW.B_40MHZ, "40M" },
            { BW.B_80MHZ, "80M" }
        };

        public static readonly Dictionary<ANT_PATH, string> antPathDic = new Dictionary<ANT_PATH, string>()
        {
            { ANT_PATH.PATH_A, "a" },
            { ANT_PATH.PATH_B, "b" },
            //{ ANT_PATH.PATH_C, "c" },
            //{ ANT_PATH.PATH_D, "d" },
            { ANT_PATH.PATH_AB, "ab" }
        };

        public static readonly Dictionary<CH, string> chDic = new Dictionary<CH, string>()
        {
            { CH.CH1, "CH1" },
            { CH.CH2, "CH2" },
            { CH.CH3, "CH3" },
            { CH.CH4, "CH4" },
            { CH.CH5, "CH5" },
            { CH.CH6, "CH6" },
            { CH.CH7, "CH7" },
            { CH.CH8, "CH8" },
            { CH.CH9, "CH9" },
            { CH.CH10, "CH10" },
            { CH.CH11, "CH11" },
            { CH.CH12, "CH12" },
            { CH.CH13, "CH13" },
            { CH.CH14, "CH14" },
            { CH.CH36, "CH36" },
            { CH.CH38, "CH38" },
            { CH.CH40, "CH40" },
            { CH.CH42, "CH42" },
            { CH.CH44, "CH44" },
            { CH.CH46, "CH46" },
            { CH.CH48, "CH48" },
            { CH.CH52, "CH52" },
            { CH.CH54, "CH54" },
            { CH.CH56, "CH56" },
            { CH.CH58, "CH58" },
            { CH.CH60, "CH60" },
            { CH.CH62, "CH62" },
            { CH.CH64, "CH64" },
            { CH.CH68, "CH68" },
            { CH.CH96, "CH96" },
            { CH.CH100, "CH100" },
            { CH.CH102, "CH102" },
            { CH.CH104, "CH104" },
            { CH.CH106, "CH106" },
            { CH.CH108, "CH108" },
            { CH.CH110, "CH110" },
            { CH.CH112, "CH112" },
            { CH.CH116, "CH116" },
            { CH.CH118, "CH118" },
            { CH.CH120, "CH120" },
            { CH.CH124, "CH124" },
            { CH.CH122, "CH122" },
            { CH.CH126, "CH126" },
            { CH.CH128, "CH128" },
            { CH.CH132, "CH132" },
            { CH.CH134, "CH134" },
            { CH.CH136, "CH136" },
            { CH.CH138, "CH138" },
            { CH.CH140, "CH140" },
            { CH.CH142, "CH142" },
            { CH.CH144, "CH144" },
            { CH.CH149, "CH149" },
            { CH.CH151, "CH151" },
            { CH.CH153, "CH153" },
            { CH.CH155, "CH155" },
            { CH.CH157, "CH157" },
            { CH.CH159, "CH159" },
            { CH.CH161, "CH161" },
            { CH.CH165, "CH165" },
            { CH.CH167, "CH167" },
            { CH.CH169, "CH169" },
            { CH.CH171, "CH171" },
            { CH.CH173, "CH173" },
            { CH.CH175, "CH175" },
            { CH.CH177, "CH177" }
            //{ CH.CH184, "CH184" },
            //{ CH.CH188, "CH188" },
            //{ CH.CH192, "CH192" },
            //{ CH.CH196, "CH196" }
        };

        public static readonly Dictionary<RATE_ID, string> rateIdDic = new Dictionary<RATE_ID, string>()
        {
            { RATE_ID.R_1M, "1M" },
            { RATE_ID.R_2M, "2M" },
            { RATE_ID.R_5_5M, "5.5M" },
            { RATE_ID.R_11M, "11M" },
            { RATE_ID.R_6M, "6M" },
            { RATE_ID.R_9M, "9M" },
            { RATE_ID.R_12M, "12M" },
            { RATE_ID.R_18M, "18M" },
            { RATE_ID.R_24M, "24M" },
            { RATE_ID.R_36M, "36M" },
            { RATE_ID.R_48M, "48M" },
            { RATE_ID.R_54M, "54M" },
            { RATE_ID.HTMCS0, "HTMCS0" },
            { RATE_ID.HTMCS1, "HTMCS1" },
            { RATE_ID.HTMCS2, "HTMCS2" },
            { RATE_ID.HTMCS3, "HTMCS3" },
            { RATE_ID.HTMCS4, "HTMCS4" },
            { RATE_ID.HTMCS5, "HTMCS5" },
            { RATE_ID.HTMCS6, "HTMCS6" },
            { RATE_ID.HTMCS7, "HTMCS7" },
            { RATE_ID.HTMCS8, "HTMCS8" },
            { RATE_ID.HTMCS9, "HTMCS9" },
            { RATE_ID.HTMCS10, "HTMCS10" },
            { RATE_ID.HTMCS11, "HTMCS11" },
            { RATE_ID.HTMCS12, "HTMCS12" },
            { RATE_ID.HTMCS13, "HTMCS13" },
            { RATE_ID.HTMCS14, "HTMCS14" },
            { RATE_ID.HTMCS15, "HTMCS15" },
            { RATE_ID.HTMCS16, "HTMCS16" },
            { RATE_ID.HTMCS17, "HTMCS17" },
            { RATE_ID.HTMCS18, "HTMCS18" },
            { RATE_ID.HTMCS19, "HTMCS19" },
            { RATE_ID.HTMCS20, "HTMCS20" },
            { RATE_ID.HTMCS21, "HTMCS21" },
            { RATE_ID.HTMCS22, "HTMCS22" },
            { RATE_ID.HTMCS23, "HTMCS23" },
            { RATE_ID.HTMCS24, "HTMCS24" },
            { RATE_ID.HTMCS25, "HTMCS25" },
            { RATE_ID.HTMCS26, "HTMCS26" },
            { RATE_ID.HTMCS27, "HTMCS27" },
            { RATE_ID.HTMCS28, "HTMCS28" },
            { RATE_ID.HTMCS29, "HTMCS29" },
            { RATE_ID.HTMCS30, "HTMCS30" },
            { RATE_ID.HTMCS31, "HTMCS31" },
            { RATE_ID.VHT1MCS0, "VHT1MCS0" },
            { RATE_ID.VHT1MCS1, "VHT1MCS1" },
            { RATE_ID.VHT1MCS2, "VHT1MCS2" },
            { RATE_ID.VHT1MCS3, "VHT1MCS3" },
            { RATE_ID.VHT1MCS4, "VHT1MCS4" },
            { RATE_ID.VHT1MCS5, "VHT1MCS5" },
            { RATE_ID.VHT1MCS6, "VHT1MCS6" },
            { RATE_ID.VHT1MCS7, "VHT1MCS7" },
            { RATE_ID.VHT1MCS8, "VHT1MCS8" },
            { RATE_ID.VHT1MCS9, "VHT1MCS9" },
            { RATE_ID.VHT2MCS0, "VHT2MCS0" },
            { RATE_ID.VHT2MCS1, "VHT2MCS1" },
            { RATE_ID.VHT2MCS2, "VHT2MCS2" },
            { RATE_ID.VHT2MCS3, "VHT2MCS3" },
            { RATE_ID.VHT2MCS4, "VHT2MCS4" },
            { RATE_ID.VHT2MCS5, "VHT2MCS5" },
            { RATE_ID.VHT2MCS6, "VHT2MCS6" },
            { RATE_ID.VHT2MCS7, "VHT2MCS7" },
            { RATE_ID.VHT2MCS8, "VHT2MCS8" },
            { RATE_ID.VHT2MCS9, "VHT2MCS9" },
        };

        public static int ChannelMapping(int freq)
        {
            int channel;
            CH ch = (CH)freq;
            switch (ch)
            {
                case CH.CH1:
                    channel = 1;
                    break;
                case CH.CH2:
                    channel = 2;
                    break;
                case CH.CH3:
                    channel = 3;
                    break;
                case CH.CH4:
                    channel = 4;
                    break;
                case CH.CH5:
                    channel = 5;
                    break;
                case CH.CH6:
                    channel = 6;
                    break;
                case CH.CH7:
                    channel = 7;
                    break;
                case CH.CH8:
                    channel = 8;
                    break;
                case CH.CH9:
                    channel = 9;
                    break;
                case CH.CH10:
                    channel = 10;
                    break;
                case CH.CH11:
                    channel = 11;
                    break;
                case CH.CH12:
                    channel = 12;
                    break;
                case CH.CH13:
                    channel = 13;
                    break;
                case CH.CH14:
                    channel = 14;
                    break;
                case CH.CH36:
                    channel = 36;
                    break;
                case CH.CH38:
                    channel = 38;
                    break;
                case CH.CH40:
                    channel = 40;
                    break;
                case CH.CH42:
                    channel = 42;
                    break;
                case CH.CH44:
                    channel = 44;
                    break;
                case CH.CH46:
                    channel = 46;
                    break;
                case CH.CH48:
                    channel = 48;
                    break;
                case CH.CH52:
                    channel = 52;
                    break;
                case CH.CH54:
                    channel = 54;
                    break;
                case CH.CH56:
                    channel = 56;
                    break;
                case CH.CH58:
                    channel = 58;
                    break;
                case CH.CH60:
                    channel = 60;
                    break;
                case CH.CH62:
                    channel = 62;
                    break;
                case CH.CH64:
                    channel = 64;
                    break;
                case CH.CH68:
                    channel = 68;
                    break;
                case CH.CH96:
                    channel = 96;
                    break;
                case CH.CH100:
                    channel = 100;
                    break;
                case CH.CH102:
                    channel = 102;
                    break;
                case CH.CH104:
                    channel = 104;
                    break;
                case CH.CH106:
                    channel = 106;
                    break;
                case CH.CH108:
                    channel = 108;
                    break;
                case CH.CH110:
                    channel = 110;
                    break;
                case CH.CH112:
                    channel = 112;
                    break;
                case CH.CH116:
                    channel = 116;
                    break;
                case CH.CH118:
                    channel = 118;
                    break;
                case CH.CH120:
                    channel = 120;
                    break;
                case CH.CH122:
                    channel = 122;
                    break;
                case CH.CH124:
                    channel = 124;
                    break;
                case CH.CH126:
                    channel = 126;
                    break;
                case CH.CH128:
                    channel = 128;
                    break;
                case CH.CH132:
                    channel = 132;
                    break;
                case CH.CH134:
                    channel = 134;
                    break;
                case CH.CH136:
                    channel = 136;
                    break;
                case CH.CH138:
                    channel = 138;
                    break;
                case CH.CH140:
                    channel = 140;
                    break;
                case CH.CH142:
                    channel = 142;
                    break;
                case CH.CH144:
                    channel = 144;
                    break;
                case CH.CH149:
                    channel = 149;
                    break;
                case CH.CH151:
                    channel = 151;
                    break;
                case CH.CH153:
                    channel = 153;
                    break;
                case CH.CH155:
                    channel = 155;
                    break;
                case CH.CH157:
                    channel = 157;
                    break;
                case CH.CH159:
                    channel = 159;
                    break;
                case CH.CH161:
                    channel = 161;
                    break;
                case CH.CH165:
                    channel = 165;
                    break;
                case CH.CH167:
                    channel = 167;
                    break;
                case CH.CH169:
                    channel = 169;
                    break;
                case CH.CH171:
                    channel = 171;
                    break;
                case CH.CH173:
                    channel = 173;
                    break;
                case CH.CH175:
                    channel = 175;
                    break;
                case CH.CH177:
                    channel = 177;
                    break;
                //case CH.CH184:
                //    channel = 184;
                //    break;
                //case CH.CH188:
                //    channel = 188;
                //    break;
                //case CH.CH192:
                //    channel = 192;
                //    break;
                //case CH.CH196:
                //    channel = 196;
                //    break;
                default:
                    channel = 0;
                    break;
            }
            return channel;
        }
    }
}
