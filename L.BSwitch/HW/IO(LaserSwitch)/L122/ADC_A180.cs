using System;
using System.Text;
using System.Runtime.InteropServices;

namespace ADC_A180
{
    public class CADC_A180
    {



        // A180 device control definition
        // DAC channel control definition
        // Channel Definition
        // Gain value definition

        //Instruction Set of EEPROM AT25040A


        //===== A180 ADC module functions definition =============

        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_initial")]
        public static extern short CS_mnet_ai8_initial(ushort RingNo, ushort SlaveIP);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_hardware_info")]
        public static extern short CS_mnet_ai8_get_hardware_info(ushort RingNo, ushort SlaveIP, ref byte DeviceID, ref byte VHDL_Version);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_set_cycle_time")]
        public static extern short CS_mnet_ai8_set_cycle_time(ushort RingNo, ushort SlaveIP, byte SetValue);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_enable_device")]
        public static extern short CS_mnet_ai8_enable_device(ushort RingNo, ushort SlaveIP, byte EnableDevice);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_enable_channel")]
        public static extern short CS_mnet_ai8_enable_channel(ushort RingNo, ushort SlaveIP, ushort ChannelNo, byte Enable);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_set_channel_gain")]
        public static extern short CS_mnet_ai8_set_channel_gain(ushort RingNo, ushort SlaveIP, ushort ChannelNo, byte Gain);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_channel_gain")]
        public static extern short CS_mnet_ai8_get_channel_gain(ushort RingNo, ushort SlaveIP, ushort ChannelNo, ref byte Gain);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_value")]
        public static extern short CS_mnet_ai8_get_value(ushort RingNo, ushort SlaveIP, ushort ChannelNo, ref short GetValue);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_value_all")]
        public static extern short CS_mnet_ai8_get_value_all(ushort RingNo, ushort SlaveIP, ref short GetValue);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_voltage")]
        public static extern short CS_mnet_ai8_get_voltage(ushort RingNo, ushort SlaveIP, ushort ChannelNo, ref double Voltage);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_voltage_all")]
        public static extern short CS_mnet_ai8_get_voltage_all(ushort RingNo, ushort SlaveIP, ref double Voltage);

        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_voltage_gain")]
        public static extern short CS_mnet_ai8_get_voltage_gain(ushort RingNo, ushort SlaveIP,ushort ChannelNo, ref double Voltage,ref byte Gain);
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_voltage_gain_all")]
        public static extern short CS_mnet_ai8_get_voltage_gain_all(ushort RingNo, ushort SlaveIP, ref double Voltage,ref byte Gain);
//A180-03
        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_current")]
        public static extern short CS_mnet_ai8_get_current(ushort RingNo, ushort SlaveIP,ushort ChannelNo, ref double Current);

        [DllImport("CMNet.dll", EntryPoint = "_mnet_ai8_get_adjustment_offset")]
        public static extern short CS_mnet_ai8_get_adjustment_offset(ushort RingNo, ushort SlaveIP, ref short Offset);




    }
}
