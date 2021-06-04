using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RTKModule
{
    public class EfuseMap
    {
        public const int BlOCK_SIZE = 768;
        public byte[,] efuse;

        public EfuseMap()
        {
            CreatMap();
        }

        public void LoadMap(string mapPath)
        {
            string[] lines = File.ReadAllLines(mapPath);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(' ');
                for (int j = 0; j < data.Length; j++)
                {
                    efuse[i, j] = Convert.ToByte(data[j], 16);
                }
            }
        }

        public void CreatMap()
        {
            int n = BlOCK_SIZE / 16;
            efuse = new byte[n, 16];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    efuse[i, j] = 0xFF;
                }
            }
        }

        public void WriteMap(int position, byte data)
        {
            if (position > BlOCK_SIZE - 1)
                throw new Exception("Over range of efuse");

            int i = position / 16;
            int j = position - 16 * i;
            efuse[i, j] = data;
        }

        public byte ReadMap(int position)
        {
            int i = position / 16;
            int j = position - 16 * i;
            return efuse[i, j];
        }

        public void Save(string path)
        {
            int d0 = efuse.GetLength(0);
            int d1 = efuse.GetLength(1);
            string content = "";
            for (int i = 0; i < d0; i++)
            {
                for (int j = 0; j < d1; j++)
                {
                    content += efuse[i, j].ToString("X2") + ((j == d1 - 1) ? "\r\n" : " ");
                }
            }
            File.WriteAllText(path, content);
        }

        public static int GetGroup(int freq)
        {
            CH ch = (CH)freq;
            switch (ch)
            {
                case CH.CH1:
                case CH.CH2:
                    return 1;
                case CH.CH3:
                case CH.CH4:
                case CH.CH5:
                    return 2;
                case CH.CH6:
                case CH.CH7:
                case CH.CH8:
                    return 3;
                case CH.CH9:
                case CH.CH10:
                case CH.CH11:
                    return 4;
                case CH.CH12:
                case CH.CH13:
                    return 5;
                case CH.CH14:
                    return 6;
                case CH.CH36:
                case CH.CH38:
                case CH.CH40:
                    return 7;
                case CH.CH44:
                case CH.CH46:
                case CH.CH48:
                    return 8;
                case CH.CH52:
                case CH.CH54:
                case CH.CH56:
                    return 9;
                case CH.CH60:
                case CH.CH62:
                case CH.CH64:
                    return 10;
                case CH.CH100:
                case CH.CH102:
                case CH.CH104:
                    return 11;
                case CH.CH108:
                case CH.CH112:
                    return 12;
                case CH.CH116:
                case CH.CH120:
                    return 13;
                case CH.CH124:
                case CH.CH126:
                case CH.CH128:
                    return 14;
                case CH.CH132:
                case CH.CH136:
                    return 15;
                case CH.CH140:
                case CH.CH142:
                case CH.CH144:
                    return 16;
                case CH.CH149:
                case CH.CH151:
                case CH.CH153:
                    return 17;
                case CH.CH157:
                case CH.CH161:
                    return 18;
                case CH.CH165:
                case CH.CH169:
                    return 19;
                case CH.CH173:
                case CH.CH175:
                case CH.CH177:
                    return 20;
                default:
                    return -1;
            }
        }

        public static int GetPosition(RATE_ID rateID, BW bw, ANT_PATH path, int freq)
        {
            int group = GetGroup(freq);

            if (rateID == RATE_ID.HTMCS7)
            {
                if (bw == BW.B_40MHZ)
                {
                    if (path == ANT_PATH.PATH_A)
                    {
                        switch (group)
                        {
                            case 1:
                                return 0x16;
                            case 2:
                                return 0x17;
                            case 3:
                                return 0x18;
                            case 4:
                                return 0x19;
                            case 5:
                                return 0x1A;
                            case 6:
                                return -1;
                            case 7:
                                return 0x22;
                            case 8:
                                return 0x23;
                            case 9:
                                return 0x24;
                            case 10:
                                return 0x25;
                            case 11:
                                return 0x26;
                            case 12:
                                return 0x27;
                            case 13:
                                return 0x28;
                            case 14:
                                return 0x29;
                            case 15:
                                return 0x2A;
                            case 16:
                                return 0x2B;
                            case 17:
                                return 0x2C;
                            case 18:
                                return 0x2D;
                            case 19:
                                return 0x2E;
                            case 20:
                                return 0x2F;
                        }
                    }
                    else if (path == ANT_PATH.PATH_B)
                    {
                        switch (group)
                        {
                            case 1:
                                return 0x40;
                            case 2:
                                return 0x41;
                            case 3:
                                return 0x42;
                            case 4:
                                return 0x43;
                            case 5:
                                return 0x44;
                            case 6:
                                return -1;
                            case 7:
                                return 0x4C;
                            case 8:
                                return 0x4D;
                            case 9:
                                return 0x4E;
                            case 10:
                                return 0x4F;
                            case 11:
                                return 0x50;
                            case 12:
                                return 0x51;
                            case 13:
                                return 0x52;
                            case 14:
                                return 0x53;
                            case 15:
                                return 0x54;
                            case 16:
                                return 0x55;
                            case 17:
                                return 0x56;
                            case 18:
                                return 0x57;
                            case 19:
                                return 0x58;
                            case 20:
                                return 0x59;
                        }
                    }
                }
            }
            else if (rateID == RATE_ID.R_11M)
            {
                if (bw == BW.B_20MHZ)
                {
                    if (path == ANT_PATH.PATH_A)
                    {
                        switch (group)
                        {
                            case 1:
                                return 0x10;
                            case 2:
                                return 0x11;
                            case 3:
                                return 0x12;
                            case 4:
                                return 0x13;
                            case 5:
                                return 0x14;
                            case 6:
                                return 0x15;
                        }
                    }
                    else if (path == ANT_PATH.PATH_B)
                    {
                        switch (group)
                        {
                            case 1:
                                return 0x3A;
                            case 2:
                                return 0x3B;
                            case 3:
                                return 0x3C;
                            case 4:
                                return 0x3D;
                            case 5:
                                return 0x3E;
                            case 6:
                                return 0x3F;
                        }
                    }
                }
            }

            return -1;
        }

        //public static string GetPosition(Realtek.RATE_ID rateID, Realtek.BW bw, Realtek.ANT_PATH path, int freq)
        //{
        //    int group = GetGroup(freq);

        //    if (rateID == Realtek.RATE_ID.HTMCS7)
        //    {
        //        if (bw == Realtek.BW.B_40MHZ)
        //        {
        //            if (path == Realtek.ANT_PATH.PATH_A)
        //            {
        //                switch (group)
        //                {
        //                    case 1:
        //                        return "16"; // 0x16
        //                    case 2:
        //                        return "17";
        //                    case 3:
        //                        return "18";
        //                    case 4:
        //                        return "19";
        //                    case 5:
        //                        return "1A";
        //                    case 6:
        //                        return "";
        //                    case 7:
        //                        return "22";
        //                    case 8:
        //                        return "23";
        //                    case 9:
        //                        return "24";
        //                    case 10:
        //                        return "25";
        //                    case 11:
        //                        return "26";
        //                    case 12:
        //                        return "27";
        //                    case 13:
        //                        return "28";
        //                    case 14:
        //                        return "29";
        //                    case 15:
        //                        return "2A";
        //                    case 16:
        //                        return "2B";
        //                    case 17:
        //                        return "2C";
        //                    case 18:
        //                        return "2D";
        //                    case 19:
        //                        return "2E";
        //                    case 20:
        //                        return "2F";
        //                }
        //            }
        //            else if (path == Realtek.ANT_PATH.PATH_B)
        //            {
        //                switch (group)
        //                {
        //                    case 1:
        //                        return "40"; // 0x40
        //                    case 2:
        //                        return "41";
        //                    case 3:
        //                        return "42";
        //                    case 4:
        //                        return "43";
        //                    case 5:
        //                        return "44";
        //                    case 6:
        //                        return "";
        //                    case 7:
        //                        return "4C";
        //                    case 8:
        //                        return "4D";
        //                    case 9:
        //                        return "4E";
        //                    case 10:
        //                        return "4F";
        //                    case 11:
        //                        return "50";
        //                    case 12:
        //                        return "51";
        //                    case 13:
        //                        return "52";
        //                    case 14:
        //                        return "53";
        //                    case 15:
        //                        return "54";
        //                    case 16:
        //                        return "55";
        //                    case 17:
        //                        return "56";
        //                    case 18:
        //                        return "57";
        //                    case 19:
        //                        return "58";
        //                    case 20:
        //                        return "59";
        //                }
        //            }
        //        }
        //    }
        //    else if (rateID == Realtek.RATE_ID.R_11M)
        //    {
        //        if (bw == Realtek.BW.B_20MHZ)
        //        {
        //            if (path == Realtek.ANT_PATH.PATH_A)
        //            {
        //                switch (group)
        //                {
        //                    case 1:
        //                        return "10"; // 0x10
        //                    case 2:
        //                        return "11";
        //                    case 3:
        //                        return "12";
        //                    case 4:
        //                        return "13";
        //                    case 5:
        //                        return "14";
        //                    case 6:
        //                        return "15";
        //                }
        //            }
        //            else if (path == Realtek.ANT_PATH.PATH_B)
        //            {
        //                switch (group)
        //                {
        //                    case 1:
        //                        return "3A"; // 0x3A
        //                    case 2:
        //                        return "3B";
        //                    case 3:
        //                        return "3C";
        //                    case 4:
        //                        return "3D";
        //                    case 5:
        //                        return "3E";
        //                    case 6:
        //                        return "3F";
        //                }
        //            }
        //        }
        //    }

        //    return "";
        //}
    }
}
