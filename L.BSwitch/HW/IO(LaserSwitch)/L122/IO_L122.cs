#define Log
using System;
using System.Reflection;
using MainFrm.Hardware.IO.ErrCode;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADC_A180;
using CMNet;
using DAC_A104;
using PCI_L122;
using PCI_L122_Err;

namespace MainFrm.Hardware.IO.L122
{
    class IO_L122:IIO
    {
        ///////////////////////////////////////////////////////////
        ///                     Para                            ///
        ///////////////////////////////////////////////////////////

        #region Para

        ushort m_CardNo = 0;
        ushort m_RingNo = 0;
        uint m_BitAddr = 0;
        uint m_ControlID;
        uint m_SlaveNo;
        uint m_PortNo;
        uint m_ChanelNo;
        Boolean isOpen = false;
        UInt32 DevTable = 0;

        #endregion

        ///////////////////////////////////////////////////////////
        ///                     SubRouting                      ///
        ///////////////////////////////////////////////////////////

        #region Subrouting

        private byte CreactByte(int index, byte ori, Boolean state)
        {
            uint _byte = new byte();
            uint _base = 0b_0000_0001;
          
            _byte = state ? ori ^ (_base << index) : ori ^ ~(_base << index);

            return Convert.ToByte(_byte);
        }

        private Boolean GetByteValue(byte ori, int index)
        {
            return (ori & (1 << index)) >> index == 1 ? true : false;
        }

        byte ToByte(int value)
        {
            byte tt = Convert.ToByte(value);
            return tt;
        }

        void DecodeIOAddress(uint bitaddr, ref ushort CardID,
                                ref uint ControlID, ref uint SlaveNO, ref uint PortNo, ref uint ChanelNo)
        {
            CardID = (ushort)((bitaddr >> 12) & 0x7);
            ControlID = (bitaddr >> 11) & 0x1;
            SlaveNO = (bitaddr >> 5) & 0x3f;
            PortNo = (bitaddr % 32) / 8;
            ChanelNo = bitaddr % 8;
        }

        #endregion

        ///////////////////////////////////////////////////////////
        ///                     Method                          ///
        ///////////////////////////////////////////////////////////

        #region Method

        public override RetErr Open()
        {
            RetErr ret = new RetErr();
            CPCI_L122_Err L122_ret;
            try
            {
                short retr = 0;
                short existcards = 0;
                L122_ret = (CPCI_L122_Err) CPCI_L122.CS_l122_open(ref existcards);
                if(L122_ret == CPCI_L122_Err.ERR_OpenCardRpt)
                    throw new Exception("L122 Open Repeat");
                if (L122_ret != CPCI_L122_Err.ERR_NoError)
                    throw new Exception("L122 Open Fail");
                if (existcards == 0)
                    throw new Exception("No PCI_L122 Card!!!");

                L122_ret = (CPCI_L122_Err)CPCI_L122.CS_l122_set_ring_config(m_CardNo, m_RingNo, 3);
                if (L122_ret != CPCI_L122_Err.ERR_NoError)
                    throw new Exception("L122 Set Config Fail");

                CCMNet.CS_mnet_set_ring_config(m_RingNo, 3);
                //CCMNet.CS_mnet_reset_ring(m_RingNo);
                CCMNet.CS_mnet_get_ring_active_table(m_RingNo,ref DevTable);

                short nL122_ret = CCMNet.CS_mnet_start_ring(m_RingNo);
                if (nL122_ret != 0)
                    throw new Exception("Open CN? fail");


                L122_ret = (CPCI_L122_Err)CPCI_L122.CS_l122_get_start_ring_num(m_CardNo, ref m_RingNo);
                if (L122_ret != CPCI_L122_Err.ERR_NoError)
                    throw new Exception("L122 Set Config Fail");

                CCMNet.CS_mnet_set_ring_quality_param(m_RingNo, 50, 10);

                CDAC_A104.CS_mnet_ao4_initial(m_RingNo, 1);

                CADC_A180.CS_mnet_ai8_initial(m_RingNo, 2);
                CADC_A180.CS_mnet_ai8_set_cycle_time(m_RingNo, 2, 20);

                for(ushort i = 0; i < 8; i++)
                {
                    CADC_A180.CS_mnet_ai8_set_channel_gain(m_RingNo, 2, i, 0);
                    CADC_A180.CS_mnet_ai8_enable_channel(m_RingNo, 2, i, 0);
                }
                CADC_A180.CS_mnet_ai8_enable_device(m_RingNo, 2, 1);


                isOpen = true;
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._Open,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._Open;
                ret.Meg = "IO Open";
                return ret;
            }
            return ret;
        }
        public override RetErr Close()
        {
            RetErr ret = new RetErr();
            try
            {
                if (isOpen)
                {
                    CDAC_A104.CS_mnet_ao4_clear_output_all(m_RingNo, 1);
                    CADC_A180.CS_mnet_ai8_enable_device(m_RingNo, 2, 0);
                    CCMNet.CS_mnet_stop_ring(m_RingNo);
                    CPCI_L122.CS_l122_close(m_CardNo);
                    isOpen = !isOpen;
                }
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._Close,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._Close;
                ret.Meg = "IO Close";
                return ret;
            }
            return ret;
        }
        public override RetErr CheckOpen(out Boolean isOpenOrNot)
        {
            RetErr ret = new RetErr();
            Boolean isOp = false;
            try
            {
                if (!isOpen) throw new Exception("doesn't Open , please Open First! ");

                isOp = isOpen;
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._chkConnect,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._chkConnect;
                isOpenOrNot = isOp;
                ret.Meg = "IO Check Open or Not";
                return ret;
            }
            isOpenOrNot = isOp;
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"> 0 ~ n</param>
        /// <param name="state"></param>
        /// <returns></returns>
        public override RetErr SetOutPut(uint Pindex, Boolean state)
        {
            RetErr ret = new RetErr();
            try
            {
                if (!isOpen) throw new Exception("doesn't Open , please Open First! ");

                uint index = Pindex + 16;
                DecodeIOAddress(index, ref m_CardNo, ref m_ControlID, ref m_SlaveNo, ref m_PortNo, ref m_ChanelNo);
                short tt = CCMNet.CS_mnet_bit_io_output((ushort)m_ControlID, (ushort)m_SlaveNo,
                                                        (byte)m_PortNo, (byte)m_ChanelNo,
                                                        state ? (byte)0x01 : (byte)0x00);
                if (tt!=0)
                    throw new Exception("Set index:" + Pindex + " , State: " + state.ToString() + " fail");

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._setDOutput,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._setDOutput;
                ret.Meg = "IO Set OutPut";
                return ret;
            }
            return ret;
        }
        public override RetErr GetOutPut(int Pindex, out Boolean state)
        {
            RetErr ret = new RetErr();
            bool tamp = false;
            try
            {
                if (!isOpen) throw new Exception("doesn't Open , please Open First! ");

                uint index = (uint)Pindex + 16;
                byte isON = 0;
                DecodeIOAddress(index, ref m_CardNo, ref m_ControlID, ref m_SlaveNo, ref m_PortNo, ref m_ChanelNo);
                short tt = CCMNet.CS_mnet_bit_io_input((ushort)m_ControlID, (ushort)m_SlaveNo,
                                                        (byte)m_PortNo, (byte)m_ChanelNo,
                                                        ref isON);
                tamp = isON == 1 ? true : false;
                if (tt != 0)
                    throw new Exception("Get index:" + Pindex + " , State: " + tamp.ToString() );

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._GetOutPut,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._GetOutPut;
                ret.Meg = "IO Get OutPut";
                state = tamp;
                return ret;
            }
            state = tamp;
            return ret;
        }
        public override RetErr GetInPut(int Pindex, out Boolean state)
        {
            RetErr ret = new RetErr();
            bool tamp = false;
            try
            {
                if (!isOpen) throw new Exception("doesn't Open , please Open First! ");

                uint index = (uint)Pindex ;
                byte isON = 0;
                DecodeIOAddress(index, ref m_CardNo, ref m_ControlID, ref m_SlaveNo, ref m_PortNo, ref m_ChanelNo);
                short tt = CCMNet.CS_mnet_bit_io_input((ushort)m_ControlID, (ushort)m_SlaveNo,
                                                        (byte)m_PortNo, (byte)m_ChanelNo,
                                                        ref isON);
                tamp = isON == 1 ? true : false;
                if (tt != 0)
                    throw new Exception("Get index:" + Pindex + " , State: " + tamp.ToString());

            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._GetInPut,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._GetInPut;
                ret.Meg = "IO Get Input";
                state = tamp;
                return ret;
            }
            state = tamp;
            return ret;
        }
        public override RetErr SetAnalogOutput(int index, double value)
        {
            RetErr ret = new RetErr();
            try
            {
                if (!isOpen) throw new Exception("doesn't Open , please Open First! ");

                throw new Exception("SPI-L122-DSF not Support Analog Output");

                //PCE - L122 - DCO    ╳
                //PCE - L123 - DCO    ╳
                //LPE - L122          ╳
                //APE - L122          ╳
                //PCI - L122 - DSF    ╳
                //SPE - L122          ╳
                //SPE - L121          4
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._SetAnalogOutput,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._SetAnalogOutput;
                ret.Meg = "IO Set AnalogOutput";
                return ret;
            }
            return ret;
        }
        public override RetErr GetAnalogInput(int index, out double value)
        {
            RetErr ret = new RetErr();
            try
            {
                if (!isOpen) throw new Exception("doesn't Open , please Open First! ");

                throw new Exception("SPI-L122-DSF not Support Analog Input");

                //PCE - L122 - DCO    ╳
                //PCE - L123 - DCO    ╳
                //LPE - L122          ╳
                //APE - L122          ╳
                //PCI - L122 - DSF    ╳
                //SPE - L122          ╳
                //SPE - L121          4
            }
            catch (Exception ee)
            {
#if (Log)
                Log.Pushlist(Num._SetAnalogInput,
                                MethodBase.GetCurrentMethod().Name,
                                ee.Message);
#endif
                // 通知外面 NG
                ret.flag = false;
                ret.Num = Num._SetAnalogInput;
                value = 0;
                ret.Meg = "IO Get AnalogInput";
                return ret;
            }
            value = 0;
            return ret;
        }

        #endregion

    }


}
