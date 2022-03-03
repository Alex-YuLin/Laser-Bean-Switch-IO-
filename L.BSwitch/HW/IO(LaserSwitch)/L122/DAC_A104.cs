using System;
using System.Text;
using System.Runtime.InteropServices;

namespace DAC_A104
{
    public class CDAC_A104
    {




        //==== Define Coarse Gain Register =======



        //========= Function Definition =========

        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_get_hardware_info")]
        public static extern short CS_mnet_ao4_get_hardware_info(ushort RingNo, ushort SlaveIP, ref byte DeviceID, ref byte VHDL_Version);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_initial")]
        public static extern short CS_mnet_ao4_initial(ushort RingNo, ushort SlaveIP);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_initial_reset")]
        public static extern short CS_mnet_ao4_initial_reset(ushort RingNo, ushort SlaveIP ,byte Enable);

        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_reset_DAC")]
        public static extern short CS_mnet_ao4_reset_DAC(ushort RingNo, ushort SlaveIP);

        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_clear_output_all")]
        public static extern short CS_mnet_ao4_clear_output_all(ushort RingNo, ushort SlaveIP);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_output")]
        public static extern short CS_mnet_ao4_set_output(ushort RingNo, ushort SlaveIP, byte ChannelNo, short SetValue);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_voltage")]
        public static extern short CS_mnet_ao4_set_voltage(ushort RingNo, ushort SlaveIP, byte ChannelNo, double Voltage);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_voltage1")]
        public static extern short CS_mnet_ao4_set_voltage1(ushort RingNo, ushort SlaveIP, byte ChannelNo, double Voltage, ref short Value);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_output_all")]
        public static extern short CS_mnet_ao4_set_output_all(ushort RingNo, ushort SlaveIP, short SetValue1, short SetValue2, short SetValue3, short SetValue4);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_voltage_all")]
        public static extern short CS_mnet_ao4_set_voltage_all(ushort RingNo, ushort SlaveIP, double Voltage1, double Voltage2, double Voltage3, double Voltage4);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_voltage_all1")]
        public static extern short CS_mnet_ao4_set_voltage_all1(ushort RingNo, ushort SlaveIP, double Voltage1, double Voltage2, double Voltage3, double Voltage4, ref short Value1, ref short Value2, ref short Value3, ref short Value4);

        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_coarse_gain")]
        public static extern short CS_mnet_ao4_set_coarse_gain(ushort RingNo, ushort SlaveIP, byte ChannelNo, short SetValue);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_fine_gain")]
        public static extern short CS_mnet_ao4_set_fine_gain(ushort RingNo, ushort SlaveIP, byte ChannelNo, short SetValue);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_set_offset")]
        public static extern short CS_mnet_ao4_set_offset(ushort RingNo, ushort SlaveIP, byte ChannelNo, short SetValue);
         [DllImport("CMNet.dll", EntryPoint = "_mnet_ao4_get_ARM_code_version")]
        public static extern short CS_mnet_ao4_get_ARM_code_version(ushort RingNo, ushort SlaveIP,ref ushort ReadData);
       
//






    }
}
