using System;
using System.Text;
using System.Runtime.InteropServices;

namespace PCI_L122
{
    public class CPCI_L122
    {
	public static uint INFINITE = 0xFFFFFFFF;
        public static Int32 WAIT_OBJECT_0 = 0;
		[DllImport("kernel32", SetLastError=true, ExactSpelling=true)]
		public static extern Int32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);

	
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateEvent(
            ref int lpEventAttributes,    // pointer to security attributes      
            bool bManualReset,  // flag for manual-reset event
            bool bInitialState, // flag for initial state
            string lpName       // pointer to event-object name
        );
        [DllImport("kernel32.dll")]
		public static extern bool ResetEvent(IntPtr hEvent);
		[DllImport("kernel32.dll")]
		public static extern bool SetEvent(IntPtr hEvent);


  	//======= Constant definition ===============
	//=====Int Setting constant ==========
        public const int C_INT_NONE_USED = 0x0000;
        public const int C_INT_RING0_IRQ = 0x0001;
        public const int C_INT_RING1_IRQ = 0x0002;
        public const int C_INT_GPIO_IRQ = 0x0004;
	//======= Baud Rate Of Ring ==========
        public const int C_BAUD_RATE_2P5MHZ = 0x00;
        public const int C_BAUD_RATE_5MHZ = 0x01;
        public const int C_BAUD_RATE_10MHZ = 0x02;
        public const int C_BAUD_RATE_20MHZ = 0x03;
	//======= Baud Rate Of Ring ==========
        public const int PCIL122_INT_DISABLE = 0x00;
        public const int PCIL122_INT_ENABLE = 0x01;
        	//======= End of Constant Definition ========

        	//======= Card Resource functions =======
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_open")]
        public static extern short CS_l122_open(ref short existcards);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_close")]
        public static extern short CS_l122_close(ushort CardSwitchNo);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_set_ring_config")]
        public static extern short CS_l122_set_ring_config(ushort CardSwitchNo, ushort RingOfCard, ushort BaudRate);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_get_start_ring_num")]
        public static extern short CS_l122_get_start_ring_num(ushort CardSwitchNo, ref ushort RingNo);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_get_hardware_info")]
        public static extern short CS_l122_get_hardware_info(ushort CardSwitchNo, ref byte Version);
        	//======= Local Io functions =========
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_lio_input_read")]
        public static extern short CS_l122_lio_input_read(ushort CardSwitchNo, ref ushort data);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_lio_output_write")]
        public static extern short CS_l122_lio_output_write(ushort CardSwitchNo, ushort data);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_lio_output_read")]
        public static extern short CS_l122_lio_output_read(ushort CardSwitchNo, ref ushort data);


        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_lio_output_bit_write")]
        public static extern short CS_l122_lio_output_bit_write(ushort CardSwitchNo, byte BitNo, ushort ON_OFF);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_lio_input_bit_read")]
        public static extern short CS_l122_lio_input_bit_read(ushort CardSwitchNo, byte BitNo, ref ushort ON_OFF);

        //======= Interrupt control functions ==========
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_set_lio_int_mask")]
        public static extern short CS_l122_set_lio_int_mask(ushort CardSwitchNo, ushort data);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_int_control")]
        public static extern short CS_l122_int_control(ushort CardSwitchNo, ushort IntType, ushort Enable);

        public delegate void L122GpioChangeUserCbk(UInt16 CardNo);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_int_link")]
        public static extern short CS_l122_int_link(ushort CardSwitchNo, L122GpioChangeUserCbk CbkFunc);

        public delegate void L122GpioChangeUserCbkWithFlag(UInt16 CardNo,ref UInt16 IntFlagStatus);
        [DllImport("PCI_L122C.dll", EntryPoint = "_l122_int_link_with_flag")]
        public static extern short CS_l122_int_link_with_flag(ushort CardSwitchNo, L122GpioChangeUserCbkWithFlag CbkFunc);




    }
}
