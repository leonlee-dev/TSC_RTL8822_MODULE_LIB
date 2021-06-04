using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace RTKModule
{
    public class RtwCommand
    {
        private static IProxyProcessor rtwProxyProcessor;

        // search key word by regular expression

        //wlan0    mp_channel:Change channel 126 to channel 142
        private static Regex setChRegex = new Regex("wlan0\\s*mp_channel\\s*:\\s*Change\\s*channel\\s*\\d+\\s*to\\s*channel\\s*\\d+");
        //wlan0    mp_bandwidth:Change BW 1 to BW 1
        private static Regex setBwRegex = new Regex("wlan0\\s*mp_bandwidth\\s*:\\s*Change\\s*BW\\s*\\d+\\s*to\\s*BW\\s*\\d+");
        //wlan0    mp_rate:Set data rate to HTMCS7 index 19
        private static Regex setRateRegex = new Regex("wlan0\\s*mp_rate\\s*:\\s*Set\\s*data\\s*rate\\s*to\\s*\\w+\\s*index\\s*\\d+");
        //wlan0    mp_ant_tx:switch Tx antenna to b
        private static Regex setAntRegex = new Regex("wlan0\\s*mp_ant_tx\\s*:\\s*switch\\s*Tx\\s*antenna\\s*to\\s*[A-Da-d]+");
        //wlan0    mp_phypara:Set xcap=81
        private static Regex setCrystalRegex = new Regex("wlan0\\s*mp_phypara\\s*:\\s*Set\\s*xcap\\s*=\\s*\\d+");
        //wlan0    mp_txpower:Set power level path_A:0 path_B:77 path_C:0 path_D:0
        private static Regex setPowerLevelRegex = new Regex("wlan0\\s*mp_txpower\\s*:\\s*Set\\s*power\\s*level(\\s*path_[A-Da-d]{1}\\s*:\\s*\\d+){4}");
        //Start continuous DA=ffffffffffff len=1500 count=0
        private static Regex swTxSendRegex = new Regex("Start\\s*continuous\\s*DA\\s*=\\s*f{12}\\s*len\\s*=\\s*\\d+\\s*count\\s*=\\s*\\d+");
        
        //wlan0    mp_ant_rx:switch Rx antenna to a
        private static Regex setRxAntRegex = new Regex("wlan0\\s*mp_ant_rx\\s*:\\s*switch\\s*Rx\\s*antenna\\s*to\\s*[A-Da-d]+");
        //wlan0    mp_arx:start
        private static Regex rxSendRegex = new Regex("wlan0\\s*mp_arx\\s*:\\s*start");
        //wlan0    mp_reset_stats:mp_reset_stats ok
        private static Regex resetRxRegex = new Regex("wlan0\\s*mp_reset_stats\\s*:\\s*mp_reset_stats\\s*ok");

        private static Regex thermalRegex = new Regex("wlan0\\s*mp_ther\\s*:\\s*\\d+");

        //wlan0    mp_get_txpower:patha=84,pathb=88
        //wlan0    mp_get_txpower: 84
        private static Regex txPowerRegex = new Regex("wlan0\\s*mp_get_txpower\\s*:\\s*(\\d+|(,?path[a-d]\\s*=\\s*\\d+)+)");

        //wlan0    mp_pwrctldm:PwrCtlDM start
        private static Regex powerTrackingOnRegex = new Regex("wlan0\\s*mp_pwrctldm\\s*:\\s*PwrCtlDM\\s*start");
        //wlan0    mp_pwrctldm:PwrCtlDM stop
        private static Regex powerTrackingOffRegex = new Regex("wlan0\\s*mp_pwrctldm\\s*:\\s*PwrCtlDM\\s*stop");

        public static void SetRtwProxyProcessor(IProxyProcessor proxyProcessor)
        {
            if (proxyProcessor is RtwProxyProcessor)
                rtwProxyProcessor = (RtwProxyProcessor)proxyProcessor;
        }

        public static bool Init()
        {
            if (rtwProxyProcessor != null)
            {
                rtwProxyProcessor.Send("su");
                rtwProxyProcessor.Send("rmmod 8822cs.ko");
                rtwProxyProcessor.Send("insmod /vendor/lib/modules/8822cs.ko rtw_powertracking_type=0 rtw_RFE_type=4");
                rtwProxyProcessor.Send("ifconfig wlan0 up");
                return true;
            }
            return false;
        }

        public static bool EnableBT(bool enable)
        {
            if(rtwProxyProcessor != null)
            {
                string strWaitFor = "Result: Parcel(00000000 00000001   '........')";
                if (enable)
                    rtwProxyProcessor.WaitFor("service call bluetooth_manager 6", strWaitFor, 3000); // open BT
                else
                    rtwProxyProcessor.WaitFor("service call bluetooth_manager 8", strWaitFor, 3000); // close BT
                return true;
            }
            return false;
        }

        public static bool PowerTracking(int on = 0)
        {
            if (rtwProxyProcessor != null)
            {
                string strReturn = "";
                if (on == 0)
                {
                    if (rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_pwrctldm stop", powerTrackingOffRegex, ref strReturn, 3000))
                        return true;
                }
                else
                {
                    if (rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_pwrctldm start", powerTrackingOnRegex, ref strReturn, 3000))
                        return true;
                }
            }
            return false;  
        }

        public static bool StartMp()
        {
            if (rtwProxyProcessor != null)
            {
                if (rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_start", "mp_start ok", 3000))
                    return true;
            }
            return false;
        }

        public static bool StopMp()
        {
            if (rtwProxyProcessor != null)
            {
                //if (rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_stop", "mp_stop ok", 3000))
                rtwProxyProcessor.Send("rtwpriv wlan0 mp_stop");
                return true;
            }
            return false;
        }

        public static bool StartSwTxCommand(RtwTx tx)
        {
            if (rtwProxyProcessor != null)
            {
                int channel = Wifi.ChannelMapping(tx.freq);

                //rtwpriv wlan0 mp_channel 7
                //rtwpriv wlan0 mp_bandwidth 40M=1,shortGI=0
                //rtwpriv wlan0 mp_rate HTMCS7
                //rtwpriv wlan0 mp_get_txpower 0
                //rtwpriv wlan0 mp_ant_tx a
                //rtwpriv wlan0 mp_txpower patha=63,pathb=0
                //rtwpriv wlan0 mp_ctx background,pkt

                string strChReturn = "";
                string strBwReturn = "";
                string strRateReturn = "";
                string strAntReturn = "";
                string strSwTxSendReturn = "";

                if (!(rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_channel " + channel, setChRegex, ref strChReturn, 3000)
                   && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_bandwidth 40M=" + (int)tx.bw + ",shortGI=0", setBwRegex, ref strBwReturn, 3000)
                   && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_rate " + Wifi.rateIdDic[tx.rateID], setRateRegex, ref strRateReturn, 3000)
                   && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_ant_tx " + Wifi.antPathDic[tx.antPath], setAntRegex, ref strAntReturn, 3000)
                ))
                    return false;

                byte[] txPower = Array.ConvertAll<int, byte>(GetTxPower(tx.antPath), (integer) => { return (byte)integer; }/*delegate(int integer) { return (byte)integer; }*/);
                if (!SendTxPowerCommand(tx.antPath, txPower))
                    return false;

                //rtwProxyProcessor.Send("rtwpriv wlan0 mp_ctx background,pkt");
                if(!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_ctx background,pkt", swTxSendRegex, ref strSwTxSendReturn, 3000))
                    return false;
                return true;
            }
            return false;
        }

        public static bool StartHwTxCommand(RtwTx tx)
        {
            if (rtwProxyProcessor != null)
            {
                //rtwProxyProcessor.Send("rtwpriv wlan0 mp_setrfpath 1");

                int channel = Wifi.ChannelMapping(tx.freq);

                // rtwpriv wlan0 7 1 a HTMCS7 1
                string cmd = "rtwpriv wlan0 " + channel + " " + (int)tx.bw + " " + Wifi.antPathDic[tx.antPath] + " " + Wifi.rateIdDic[tx.rateID] + " 1";

                // wait key word : Set PMac Tx Mode OK to emit
                if (rtwProxyProcessor.WaitFor(cmd, "Set PMac Tx Mode OK", 3000)) // start HW Tx
                    return true;
            }
            return false;
        }

        public static bool StopSwTxCommand()
        {
            if (rtwProxyProcessor != null)
            {
                if (rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_ctx stop", "Stop continuous Tx", 3000)) // stop SW Tx
                    return true;
            }
            return false;
        }

        public static bool StopHwTxCommand()
        {
            if (rtwProxyProcessor != null)
            {
                if (rtwProxyProcessor.WaitFor("rtwpriv wlan0 stop", "Set PMac Tx Mode OK", 3000)) // stop HW Tx
                    return true;
            }
            return false;
        }

        public static bool StartRxCommand(RtwRx rx)
        {
            if (rtwProxyProcessor != null)
            {
                int channel = Wifi.ChannelMapping(rx.freq);

                if (channel == -1)
                    return false;

                string strChReturn = "";
                string strAntReturn = "";
                string strBwReturn = "";
                string strRxSendReturn = "";
                string strResetRxReturn = "";

                if (!(rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_channel " + channel, setChRegex, ref strChReturn, 3000)
                   && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_ant_rx " + Wifi.antPathDic[rx.antPath], setRxAntRegex, ref strAntReturn, 3000)
                   && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_bandwidth 40M=" + (int)rx.bw + ",shortGI=0", setBwRegex, ref strBwReturn, 3000)
                   && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_arx start", rxSendRegex, ref strRxSendReturn, 3000)
                   && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_reset_stats", resetRxRegex, ref strResetRxReturn, 3000)
                ))
                    return false;
                return true;
            }
            return false;
        }

        public static bool ResetRxStat()
        {
            if (rtwProxyProcessor != null)
            {
                string strResetRxReturn = "";
                if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_reset_stats", resetRxRegex, ref strResetRxReturn, 3000))
                    return false;
                return true;
            }
            return false;
        }

        public static int GetRxPacketCount()
        {
            int pktCount;
            //wlan0    mp_arx:Phy Received packet OK:1000 CRC error:0 FA Counter: 1
            Regex tempRegex = new Regex(@"wlan0\s*mp_arx\s*:\s*Phy\s*Received\s*packet\s*OK\s*:\s*\d+");
            string strReturn = "";
            if (rtwProxyProcessor != null)
            {
                if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_arx phy", tempRegex, ref strReturn, 2000))
                {
                    return -1;
                }

                int index1 = strReturn.IndexOf("packet OK:", 0);
                int index2 = strReturn.IndexOf("CRC error:", 0);
                int index3 = strReturn.IndexOf("FA Counter:", 0);

                if (index1 > 0/* && index2 > 0 && index3 > 0*/)
                {
                    index1 += 10;
                   if (int.TryParse(strReturn.Substring(index1), out pktCount))
                     //&& int.TryParse(strReturn.Substring(index1, index3 - index2 - 1), out pktCount[1])
                     //&& int.TryParse(strReturn.Substring(index3), out pktCount[2]))
                    {
                        return pktCount;
                    }
                }
            }
            return -1;
        }

        public static bool IndexCryNextCommand(byte indexCry)
        {
            if (rtwProxyProcessor != null)
            {
                string strCrystalReturn = "";
                if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_phypara xcap=" + indexCry, setCrystalRegex, ref strCrystalReturn, 3000))
                    return false;
                return true;
            }
            return false;
        }

        public static bool SendTxPowerCommand(ANT_PATH antPath, params byte[] txPower)
        {
            if (rtwProxyProcessor != null)
            {
                if (txPower == null)
                    return false;

                string strPowerLevelReturn = "";
                switch (antPath)
                {
                    case ANT_PATH.PATH_A:
                        if (!(txPower.Length > 0 && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_txpower patha=" + txPower[0] + ",pathb=0", setPowerLevelRegex, ref strPowerLevelReturn, 3000)))
                            return false;
                        break;
                    case ANT_PATH.PATH_B:
                        if (!(txPower.Length > 0 && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_txpower patha=0,pathb=" + txPower[0], setPowerLevelRegex, ref strPowerLevelReturn, 3000)))
                            return false;
                        break;
                    case ANT_PATH.PATH_AB:
                        if (!(txPower.Length > 1 && rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_txpower patha=" + txPower[0] + ",pathb=" + txPower[1], setPowerLevelRegex, ref strPowerLevelReturn, 3000)))
                            return false;
                        break;
                    default:
                        return false;
                }
                return true;
            }
            return false;
        }

        public static int[] GetTxPower(ANT_PATH antPath)
        {
            string strReturn = "";
            if (rtwProxyProcessor != null)
            {
                if(antPath == ANT_PATH.PATH_AB) // 2SS
                {
                    if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_get_txpower 0 1", txPowerRegex, ref strReturn, 3000))
                        return null;
                    string[] txPower = strReturn.Split(':')[1].Split(',');
                    int txPower0, txPower1;
                    if (!(int.TryParse(txPower[0].Split('=')[1].Trim(), out txPower0) 
                       && int.TryParse(txPower[1].Split('=')[1].Trim(), out txPower1)))
                        return null;
                    return new int[] { txPower0, txPower1 };
                }
                else // 1SS
                {
                    if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_get_txpower " + (int)antPath, txPowerRegex, ref strReturn, 3000))
                        return null;
                    int txPower;
                    if (int.TryParse(strReturn.Split(':')[1].Trim(), out txPower))
                        return new int[] { txPower };
                }
            }
            return null;
        }

        public static int GetThermalValue()
        {
            string strReturn = "";
            if (rtwProxyProcessor != null)
            {
                if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_ther", thermalRegex, ref strReturn, 2000))
                    return -1;

                int thermalValue;
                if (int.TryParse(strReturn.Split(':')[1].Trim(), out thermalValue))
                    return thermalValue;
            }
            return -1;
        }

        public static int GetThermalValue(ANT_PATH antPath)
        {
            string strReturn = "";
            if (rtwProxyProcessor != null)
            {
                switch (antPath)
                {
                    case ANT_PATH.PATH_A:
                        if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_ther 0", thermalRegex, ref strReturn, 3000))
                            return -1;
                        break;
                    case ANT_PATH.PATH_B:
                        if (!rtwProxyProcessor.WaitFor("rtwpriv wlan0 mp_ther 1", thermalRegex, ref strReturn, 3000))
                            return -1;
                        break;
                    default:
                        return -1;
                }

                int thermalValue;
                if (int.TryParse(strReturn.Split(':')[1].Trim(), out thermalValue))
                    return thermalValue;
            }
            return -1;
        }

        public static bool WriteFakeMap(int addr, byte value)
        {
            // rtwpriv wlan0 efuse_set wlwfake,Addr(HEX),value(HEX)
            if (rtwProxyProcessor != null)
            {
                string hexAddr = addr.ToString("X2");
                string hexValue = value.ToString("X2");
                string cmd = "rtwpriv wlan0 efuse_set wlwfake," + hexAddr + "," + hexValue;
                if (rtwProxyProcessor.WaitFor(cmd, "wlwfake OK", 2000))
                    return true;
            }
            return false;
        }

        public static bool WriteFakeMap(int addr, byte[] value)
        {
            // rtwpriv wlan0 efuse_set wlwfake,Addr(HEX),value(HEX)
            if (rtwProxyProcessor != null)
            {
                string hexAddr = addr.ToString("X2");
                string hexValue = "";
                for (int i = 0; i < value.Length; i++)
                {
                    hexValue += value[i].ToString("X2");
                }
                string cmd = "rtwpriv wlan0 efuse_set wlwfake," + hexAddr + "," + hexValue;
                if (rtwProxyProcessor.WaitFor(cmd, "wlwfake OK", 1000))
                    return true;
            }
            return false;
        }

        public static bool WriteToEfuse()
        {
            if (rtwProxyProcessor != null)
            {
                if (rtwProxyProcessor.WaitFor("rtwpriv wlan0 efuse_set wlfk2map", "WiFi write map compare OK", 1000))
                    return true;
            }
            return false;
        }
    }
}
