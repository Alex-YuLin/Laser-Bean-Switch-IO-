using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CMNet
{
    public class CCMNet
    {

        // Interrupt Types
        public enum EInterrupt
        {
            EINT_Unknown = -1, // Unknown
            EINT_CEND = 0x01, // transmitting FIFO buffer ready
            EINT_BRKF = 0x02, // communication break
            EINT_IOPC = 0x04, // the monitored input port changed
            EINT_EIOE = 0x08, // I/O communication error
            EINT_EDTE = 0x10, // date communication error
            EINT_ERAE = 0x20, // local device reception process error
            EINT_CAER = 0x40, // CPU access error
            EINT_ALL = 0x7F // all
        };
        // Index of Callback Function Array
        public enum EN_INT_EVENT
        {
            ECbk_Unknown = -1,	// Unknown
            ECbk_CEND = 0,	// CEND
            ECbk_BRKF = 1,	// BRKF
            ECbk_IOPC = 2,	// IOPC
            ECbk_EIOE = 3,	// EIOE
            ECbk_EDTE = 4, 	// EDTE
            ECbk_ERAE = 5, 	// ERAE
            ECbk_CAER = 6 	// CAER
        };

        public enum EN_SWD_EVENT // Soft Watch Dog Event
        {
            SWD_Cbk_Unknown = -1,	// Unknown
            SWD_Cbk_IOPC = 2,	// IOPC
            SWD_Cbk_EIOE = 3,	// EIOE
            SWD_Cbk_EDTE = 4, 	// EDTE
            SWD_Cbk_ERAE = 5, 	// ERAE
            SWD_Cbk_CAER = 6 	// CAER
        };
        // Error Code passed to user callback when error INT occur
        public enum EErrorCode
        {
            EErr_Unknown = -1, // Unknown
            EErr_EIOE = 0x30, // EIOE
            EErr_EDTE = 0x40, // EDTE
            EErr_ERAE_1 = 0x51, // ERAE case 1
            EErr_ERAE_2 = 0x52, // ERAE case 2
            EErr_ERAE_3 = 0x53, // ERAE case 3
            EErr_CAER_1 = 0x61, // CAER case 1
            EErr_CAER_2 = 0x62, // CAER case 2
            EErr_CAER_3 = 0x63, // CAER case 3
            EErr_CAER_4 = 0x64 // CAER case 4
        };

        //
        // MotionNet Slave Type Definition
        //










        // Message Mode Device
        // Others
        //M1 type
        //
        // PPCIE-8601
        [DllImport("CMnet.dll", EntryPoint = "_pcie8601_open")]
        public static extern short CS_pcie8601_open(ref short existcard);
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //
        // USB_L111
        [DllImport("CMnet.dll", EntryPoint = "_USB_L111_initial")]
        public static extern short CS_USB_L111_initial(ref ushort exist_usb_l111);
        [DllImport("CMnet.dll", EntryPoint = "_USB_L111_mnet_initial")]
        public static extern short CS_USB_L111_mnet_initial(ushort USB_IP);
        [DllImport("CMnet.dll", EntryPoint = "_USB_L111_mnet_GetRing")]
        public static extern short CS_USB_L111_mnet_GetRing(ushort USB_IP);
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        [DllImport("CMnet.dll", EntryPoint = "_mnet_close")]
        public static extern short CS_mnet_close();
        //
        // Ring Operation
        [DllImport("CMnet.dll", EntryPoint = "_mnet_reset_ring")]
        public static extern short CS_mnet_reset_ring(ushort RingNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_start_ring")]
        public static extern short CS_mnet_start_ring(ushort RingNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_stop_ring")]
        public static extern short CS_mnet_stop_ring(ushort RingNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_set_dll_close_ring")]
        public static extern short CS_mnet_set_dll_close_ring(ushort RingNo, ushort On_Off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_ring_status")]
        public static extern short CS_mnet_get_ring_status(ushort RingNo, ref ushort Status);

        // Soft Watch Dog
         //typedef void  (WINAPI *CallbackSoftWatchDog)(U16,U16,U16);
         // ===== Soft Watch Dog Event ==========

        [DllImport("CMnet.dll", EntryPoint = "_mnet_enable_soft_watchdog")]
        public static extern short CS_mnet_enable_soft_watchdog(ushort RingNo, ref IntPtr User_hEvent);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_disable_soft_watchdog")]
        public static extern short CS_mnet_disable_soft_watchdog(ushort RingNo);
        // I16 PASCAL _mnet_link_soft_watchdog_callback(U16 RingNo, CallbackSoftWatchDog CbkFunc);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_set_ring_quality_param")]
        public static extern short CS_mnet_set_ring_quality_param(ushort RingNo, ushort ContinueErr, ushort ErrorRate);
        //[DllImport("CMnet.dll", EntryPoint = "_mnet_watchdog_link")]
        //public static extern short CS_mnet_watchdog_link(ushort RingNo, ushort CbkEvent);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_com_status")]
        public static extern short CS_mnet_get_com_status(ushort RingNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_clear_ring_error")]
        public static extern short CS_mnet_clear_ring_error(ushort RingNo);

        //public delegate void CbkFunc(short RingNo, short SlaveIP, short Code);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_watchdog_link")]
        //public static extern short CS_mnet_watchdog_link(ushort RingNo, ushort CbkEvent, short CbkFunc);
        public static extern short CS_mnet_watchdog_link(ushort RingNo, ushort CbkEvent, MulticastDelegate CbkFunc);
        //
        // Baud Rate Setting
        [DllImport("CMnet.dll", EntryPoint = "_mnet_set_ring_config")]
        public static extern short CS_mnet_set_ring_config(ushort RingNO, ushort BaudRate);
        //

        // Slaves
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_ring_active_table")]
        public static extern short CS_mnet_get_ring_active_table(ushort RingNo, ref uint DevTable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_slave_type")]
        public static extern short CS_mnet_get_slave_type(ushort RingNo, ushort SlaveIP, ref byte SlaveType);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_slave_type_mem")]
        public static extern short CS_mnet_get_slave_type_mem(ushort RingNo, ushort SlaveIP, ref byte SlaveType);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_error_device")]
        public static extern short CS_mnet_get_error_device(ushort RingNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_slave_error_table")]
        public static extern short CS_mnet_get_slave_error_table(ushort RingNo, ref uint ErrorTable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_clear_slave_error_flag")]
        public static extern short CS_mnet_clear_slave_error_flag(ushort RingNo, ref uint ErrorTable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_save_ring_active_table")]
        public static extern short CS_mnet_save_ring_active_table(ushort RingNo, string FilePath);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_load_ring_active_table")]
        public static extern short CS_mnet_load_ring_active_table(ushort RingNo, ref uint DevTable, string FilePath);


        [DllImport("CMnet.dll", EntryPoint = "_mnet_auto_clear_slave_error_flag")]
        public static extern short CS_mnet_auto_clear_slave_error_flag(ushort RingNo, byte Enable, int ReTryTimes);

        //
        // Others
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_DLL_path")]
        public static extern short CS_mnet_get_DLL_path(string lpFilePath, uint nSize, ref uint nLength);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_DLL_version")]
        public static extern short CS_mnet_get_DLL_version(string lpBuf, uint nSize, ref uint nLength);

        // ==== G9001A New Functions ============================================
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_com_error_counter")]
        public static extern short CS_mnet_get_com_error_counter(ushort RingNo, ref ushort GetValue);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_get_cyclic_cycle_counter")]
        public static extern short CS_mnet_get_cyclic_cycle_counter(ushort RingNo, ref ushort GetValue);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_reset_com_error_counter")]
        public static extern short CS_mnet_reset_com_error_counter(ushort RingNo);
        //===== Broadcast Commands support by G9001A==============================
        [DllImport("CMnet.dll", EntryPoint = "_mnet_multi_sd_stop")]
        public static extern short CS_mnet_multi_sd_stop(ushort RingNo, byte Group);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_multi_emg_stop")]
        public static extern short CS_mnet_multi_emg_stop(ushort RingNo, byte Group);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_multi_imd_stop")]
        public static extern short CS_mnet_multi_imd_stop(ushort RingNo, byte Group);
        //
        // Interrupts
        //
        //
        // Interrupt Types
        //
        // Index of Callback Function Array


        //
        // Error Code passed to user callback when error INT occurs
        	//====== local device reception process error ===========
        	//====== CPU access error ===========
        //
        // User callback function prototype template
        // void PASCAL UserCbk_CEND(U16 RingNo, U16 Useless1, U16 Useless2);
        // void PASCAL UserCbk_BRKF(U16 RingNo, U16 Useless1, U16 Useless2);
        [DllImport("CMnet.dll", EntryPoint = "UserCbk_IOPC")]
        public static extern  short   CSUserCbk_IOPC(ushort RingNo, ushort SlaveIP, ushort Port);
        [DllImport("CMnet.dll", EntryPoint = "UserCbk_EIOE")]
        public static extern  short   CSUserCbk_EIOE(ushort RingNo, ushort SlaveIP, ushort Code);
        [DllImport("CMnet.dll", EntryPoint = "UserCbk_EDTE")]
        public static extern  short   CSUserCbk_EDTE(ushort RingNo, ushort SlaveIP, ushort Code);
        [DllImport("CMnet.dll", EntryPoint = "UserCbk_ERAE")]
        public static extern  short   CSUserCbk_ERAE(ushort RingNo, ushort SlaveIP, ushort Code);
        [DllImport("CMnet.dll", EntryPoint = "UserCbk_CAER")]
        public static extern short  CSUserCbk_CAER(ushort RingNo, ushort SlaveIP, ushort Code);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_int_enable")]
        public static extern short CS_mnet_int_enable(ushort RingNo, ushort Enable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_int_link")]
        public static extern short CS_mnet_int_link(ushort RingNo, ushort IntEvent);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_int_clear")]
        public static extern short CS_mnet_int_clear(ushort RingNo, ushort IntBits);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_int_mask")]
        public static extern short CS_mnet_int_mask(ushort RingNo, ushort IntBits);
        // I16 PASCAL _mnet_int_auto_break( U16 RingNo, U16 Enable );
        // I16 PASCAL _mnet_int_auto_clear( U16 RingNo, U16 Enable );
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////                      //////////////////////////////
        ////////////////////////////  Digital I/O Slaves  //////////////////////////////
        ////////////////////////////                      //////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_io_output")]
        public static extern short CS_mnet_io_output(ushort RingNo, ushort SlaveIP, byte PortNo, byte Val);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_io_input")]
        public static extern short CS_mnet_io_input(ushort RingNo, ushort SlaveIP, byte PortNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_bit_io_output")]
        public static extern short CS_mnet_bit_io_output(ushort RingNo, ushort SlaveIP, byte PortNo, byte BitNo, byte On_Off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_bit_io_input")]
        public static extern short CS_mnet_bit_io_input(ushort RingNo, ushort SlaveIP, byte PortNo, byte BitNo, ref byte On_Off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_io_input_monitor")]
        public static extern short CS_mnet_io_input_monitor(ushort RingNo, ushort SlaveIP, byte PortNo, ushort Enable);
        //
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////                      //////////////////////////////
        //////////////////////////// 1-Axis Motion Slaves //////////////////////////////
        ////////////////////////////                      //////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //
        // Initial
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_initial")]
        public static extern short CS_mnet_m1_initial(ushort RingNo, ushort SlaveIP);
        //
        // Pulse I/O Configuration
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_pls_outmode")]
        public static extern short CS_mnet_m1_set_pls_outmode(ushort RingNo, ushort SlaveIP, ushort pls_outmode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_pls_iptmode")]
        public static extern short CS_mnet_m1_set_pls_iptmode(ushort RingNo, ushort SlaveIP, ushort pls_iptmode, ushort pls_iptdir);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_feedback_src")]
        public static extern short CS_mnet_m1_set_feedback_src(ushort RingNo, ushort SlaveIP, ushort FbkSrc);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_move_ratio")]
        public static extern short CS_mnet_m1_set_move_ratio(ushort RingNo, ushort SlaveIP, double move_ratio);
        //
        // Interface I/O Configuration
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_alm")]
        public static extern short CS_mnet_m1_set_alm(ushort RingNo, ushort SlaveIP, ushort alm_logic, ushort alm_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_inp")]
        public static extern short CS_mnet_m1_set_inp(ushort RingNo, ushort SlaveIP, ushort inp_enable, ushort inp_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_erc")]
        public static extern short CS_mnet_m1_set_erc(ushort RingNo, ushort SlaveIP, ushort erc_logic, ushort erc_on_time, ushort erc_off_time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_erc_on")]
        public static extern short CS_mnet_m1_set_erc_on(ushort RingNo, ushort SlaveIP, short on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_svon")]
        public static extern short CS_mnet_m1_set_svon(ushort RingNo, ushort SlaveIP, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_ralm")]
        public static extern short CS_mnet_m1_set_ralm(ushort RingNo, ushort SlaveIP, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_sd")]
        public static extern short CS_mnet_m1_set_sd(ushort RingNo, ushort SlaveIP, short sd_enable, short sd_logic, short sd_latch, short sd_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_ltc_logic")]
        public static extern short CS_mnet_m1_set_ltc_logic(ushort RingNo, ushort SlaveIP, ushort ltc_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_pcs")]
        public static extern short CS_mnet_m1_set_pcs(ushort RingNo, ushort SlaveIP, ushort pcs_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_enable_pcs")]
        public static extern short CS_mnet_m1_enable_pcs(ushort RingNo, ushort SlaveIP, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_clr")]
        public static extern short CS_mnet_m1_set_clr(ushort RingNo, ushort SlaveIP, ushort clr_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_el_mode")]
        public static extern short CS_mnet_m1_set_el_mode(ushort RingNo, ushort SlaveIP, short el_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_dio_output")]
        public static extern short CS_mnet_m1_dio_output(ushort RingNo, ushort SlaveIP, ushort DoNo, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_dio_input")]
        public static extern short CS_mnet_m1_dio_input(ushort RingNo, ushort SlaveIP, ushort DiNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_mechanical_input_filter")]
        public static extern short CS_mnet_m1_set_mechanical_input_filter(ushort RingNo, ushort SlaveIP, ushort on_off);
        // I/O Status
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_io_status")]
        public static extern short CS_mnet_m1_get_io_status(ushort RingNo, ushort SlaveIP, ref uint IO_status);
        // INT Status
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_error_status")]
        public static extern short CS_mnet_m1_get_error_status(ushort RingNo, ushort SlaveIP, ref uint ErrorStatus);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_event_status")]
        public static extern short CS_mnet_m1_get_event_status(ushort RingNo, ushort SlaveIP, ref uint EventStatus);
        //
        // Speed Pattern
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_speed_pattern")]
        public static extern short CS_mnet_m1_set_speed_pattern(ushort RingNo, ushort SlaveIP, byte Pattern);
        //
        // Velocity Control
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_v_move")]
        public static extern short CS_mnet_m1_v_move(ushort RingNo, ushort SlaveIP, byte Dir);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_v_change")]
        public static extern short CS_mnet_m1_v_change(ushort RingNo, ushort SlaveIP, double NewVel, double Time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_fix_speed_range")]
        public static extern short CS_mnet_m1_fix_speed_range(ushort RingNo, ushort SlaveIP, double MaxVel);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_unfix_speed_range")]
        public static extern short CS_mnet_m1_unfix_speed_range(ushort RingNo, ushort SlaveIP);
        //
        // Position Control
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_tmove_speed")]
        public static extern short CS_mnet_m1_set_tmove_speed(ushort RingNo, ushort SlaveIP, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_smove_speed")]
        public static extern short CS_mnet_m1_set_smove_speed(ushort RingNo, ushort SlaveIP, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_r_move")]
        public static extern short CS_mnet_m1_start_r_move(ushort RingNo, ushort SlaveIP, int Distance);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_a_move")]
        public static extern short CS_mnet_m1_start_a_move(ushort RingNo, ushort SlaveIP, int Pos);
        //
        // Stop
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_sd_stop")]
        public static extern short CS_mnet_m1_sd_stop(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_sd_stop_time")]
        public static extern short CS_mnet_m1_sd_stop_time(ushort RingNo, ushort SlaveIP, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_emg_stop")]
        public static extern short CS_mnet_m1_emg_stop(ushort RingNo, ushort SlaveIP);
        //
        // Motion Status
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_motion_done")]
        public static extern short CS_mnet_m1_motion_done(ushort RingNo, ushort SlaveIP, ref ushort MoSt);
        //
        // Home
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_home_move")]
        public static extern short CS_mnet_m1_start_home_move(ushort RingNo, ushort SlaveIP, byte Dir);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_home_config")]
        public static extern short CS_mnet_m1_set_home_config(ushort RingNo, ushort SlaveIP, ushort home_mode, ushort org_logic, ushort ez_logic, ushort ez_count, ushort erc_out);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_home_search")]
        public static extern short CS_mnet_m1_start_home_search(ushort RingNo, ushort SlaveIP, byte Dir, double ORGOffset);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_home_escape")]
        public static extern short CS_mnet_m1_start_home_escape(ushort RingNo, ushort SlaveIP, byte Dir);
        //
        // Counter Operating
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_command")]
        public static extern short CS_mnet_m1_set_command(ushort RingNo, ushort SlaveIP, int Cmd);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_command")]
        public static extern short CS_mnet_m1_get_command(ushort RingNo, ushort SlaveIP, ref int Cmd);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_reset_command")]
        public static extern short CS_mnet_m1_reset_command(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_position")]
        public static extern short CS_mnet_m1_get_position(ushort RingNo, ushort SlaveIP, ref int Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_position")]
        public static extern short CS_mnet_m1_set_position(ushort RingNo, ushort SlaveIP, int Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_reset_position")]
        public static extern short CS_mnet_m1_reset_position(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_error_counter")]
        public static extern short CS_mnet_m1_set_error_counter(ushort RingNo, ushort SlaveIP, int ErrCnt);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_error_counter")]
        public static extern short CS_mnet_m1_get_error_counter(ushort RingNo, ushort SlaveIP, ref int ErrCnt);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_reset_error_counter")]
        public static extern short CS_mnet_m1_reset_error_counter(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_current_speed")]
        public static extern short CS_mnet_m1_get_current_speed(ushort RingNo, ushort SlaveIP, ref double speed);
        //
        // Position Compare and Latch
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_comparator_mode")]
        public static extern short CS_mnet_m1_set_comparator_mode(ushort RingNo, ushort SlaveIP, short CompNo, short CmpSrc, short CmpMethod, short CmpAction);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_comparator_data")]
        public static extern short CS_mnet_m1_set_comparator_data(ushort RingNo, ushort SlaveIP, short CompNo, double Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_trigger_comparator")]
        public static extern short CS_mnet_m1_set_trigger_comparator(ushort RingNo, ushort SlaveIP, ushort CmpSrc, ushort CmpMethod);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_trigger_comparator_data")]
        public static extern short CS_mnet_m1_set_trigger_comparator_data(ushort RingNo, ushort SlaveIP, double Data);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_comparator_data")]
        public static extern short CS_mnet_m1_get_comparator_data(ushort RingNo, ushort SlaveIP, short CompNo, ref double Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_soft_limit")]
        public static extern short CS_mnet_m1_set_soft_limit(ushort RingNo, ushort SlaveIP, int PLimit, int MLimit);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_enable_soft_limit")]
        public static extern short CS_mnet_m1_enable_soft_limit(ushort RingNo, ushort SlaveIP, byte Action);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_disable_soft_limit")]
        public static extern short CS_mnet_m1_disable_soft_limit(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_latch_data")]
        public static extern short CS_mnet_m1_get_latch_data(ushort RingNo, ushort SlaveIP, short LatchNo, ref double Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_auto_trigger")]
        public static extern short CS_mnet_m1_set_auto_trigger(ushort RingNo, ushort SlaveIP, ushort CmpSrc, ushort CmpMethod, ushort Interval, ushort on_off);
        //
        // Load Configuration File
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_load_motion_file")]
        public static extern short CS_mnet_m1_load_motion_file(ushort RingNo, ushort SlaveIP, string FilePath);
        //
        // Destination Change
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_p_change")]
        public static extern short CS_mnet_m1_start_p_change(ushort RingNo, ushort SlaveIP, int NewPos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_d_change")]
        public static extern short CS_mnet_m1_start_d_change(ushort RingNo, ushort SlaveIP, int NewDist);
        //
        // Backlash Correction
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_backlash")]
        public static extern short CS_mnet_m1_set_backlash(ushort RingNo, ushort SlaveIP, ushort Value, ushort Enable, ushort CntSrc);
        //
        // Synchronize
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_sta_trigger")]
        public static extern short CS_mnet_m1_set_sta_trigger(ushort RingNo, ushort SlaveIP, ushort TriType);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_sync_v_move")]
        public static extern short CS_mnet_m1_sync_v_move(ushort RingNo, ushort SlaveIP, byte Dir);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_sync_r_move")]
        public static extern short CS_mnet_m1_start_sync_r_move(ushort RingNo, ushort SlaveIP, int Distance);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_sync_a_move")]
        public static extern short CS_mnet_m1_start_sync_a_move(ushort RingNo, ushort SlaveIP, int Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_sync_tr_line")]
        public static extern short CS_mnet_m1_start_sync_tr_line(ushort RingNo, ref ushort SlaveIP, int DistX, int DistY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_sync_ta_line")]
        public static extern short CS_mnet_m1_start_sync_ta_line(ushort RingNo, ref ushort SlaveIP, int PosX, int PosY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_sync_sr_line")]
        public static extern short CS_mnet_m1_start_sync_sr_line(ushort RingNo, ref ushort SlaveIP, int DistX, int DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_start_sync_sa_line")]
        public static extern short CS_mnet_m1_start_sync_sa_line(ushort RingNo, ref ushort SlaveIP, int PosX, int PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_sync_sd_stop")]
        public static extern short CS_mnet_m1_sync_sd_stop(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_sync_imd_stop")]
        public static extern short CS_mnet_m1_sync_imd_stop(ushort RingNo, ushort SlaveIP);
        //
        // FUA3
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_fua3_sync")]
        public static extern short CS_mnet_m1_fua3_sync(ushort RingNo, ushort SlaveIP, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_fua3_powerdown")]
        public static extern short CS_mnet_m1_fua3_powerdown(ushort RingNo, ushort SlaveIP, ushort on_off);
        //
        // Others
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_speed_range")]
        public static extern short CS_mnet_m1_get_speed_range(ushort RingNo, ushort SlaveIP, ref double MinVel, ref double MaxVel);
        //m1 new type Line and Arc
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_set_group")]
        public static extern short CS_mnet_m1_set_group(ushort RingNo, ushort GroupNo, ushort[] AxisArray, ushort AxisCount);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_preregister_depth")]
        public static extern short CS_mnet_m1_get_preregister_depth(ushort RingNo, ushort SlaveIP, ref byte Depth);
        // I16 PASCAL _mnet_m1_group_lineN(U16 RingNo, U16 GroupNo, ECurve Curve, EMoveMode Mode, I32 *DistArray, U32 StrVel, U32 MaxVel, F32 Tacc, F32 Tdec, U8 bContinuous);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_tr_lineN")]
        public static extern short CS_mnet_m1_group_tr_lineN(ushort RingNo, ushort GroupNo,  double[] DistArray, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_ta_lineN")]
        public static extern short CS_mnet_m1_group_ta_lineN(ushort RingNo, ushort GroupNo, double[] PosArray, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sr_lineN")]
        public static extern short CS_mnet_m1_group_sr_lineN(ushort RingNo, ushort GroupNo, double[] DistArray, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sa_lineN")]
        public static extern short CS_mnet_m1_group_sa_lineN(ushort RingNo, ushort GroupNo, double[] PosArray, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_tr_lineN_continuous")]
        public static extern short CS_mnet_m1_group_tr_lineN_continuous(ushort RingNo, ushort GroupNo, double[] DistArray, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_ta_lineN_continuous")]
        public static extern short CS_mnet_m1_group_ta_lineN_continuous(ushort RingNo, ushort GroupNo, double[] PosArray, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sr_lineN_continuous")]
        public static extern short CS_mnet_m1_group_sr_lineN_continuous(ushort RingNo, ushort GroupNo, double[] DistArray, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sa_lineN_continuous")]
        public static extern short CS_mnet_m1_group_sa_lineN_continuous(ushort RingNo, ushort GroupNo, double[] PosArray, double StrVel, double MaxVel, double Tacc, double Tdec);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_tr_arc2")]
        public static extern short CS_mnet_m1_group_tr_arc2(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_ta_arc2")]
        public static extern short CS_mnet_m1_group_ta_arc2(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double Cx, double Cy, double Ex, double Ey, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sr_arc2")]
        public static extern short CS_mnet_m1_group_sr_arc2(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sa_arc2")]
        public static extern short CS_mnet_m1_group_sa_arc2(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double Cx, double Cy, double Ex, double Ey, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_tr_arc2_continuous")]
        public static extern short CS_mnet_m1_group_tr_arc2_continuous(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_ta_arc2_continuous")]
        public static extern short CS_mnet_m1_group_ta_arc2_continuous(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double Cx, double Cy, double Ex, double Ey, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sr_arc2_continuous")]
        public static extern short CS_mnet_m1_group_sr_arc2_continuous(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sa_arc2_continuous")]
        public static extern short CS_mnet_m1_group_sa_arc2_continuous(ushort RingNo, ushort GroupNo, ushort AxIP, ushort AyIP, double Cx, double Cy, double Ex, double Ey, short Dir, double StrVel, double MaxVel, double Tacc, double Tdec);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_continuous_move")]
        public static extern short CS_mnet_m1_group_continuous_move(ushort RingNo, ushort GroupNo, ushort Enable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_emg_stop")]
        public static extern short CS_mnet_m1_group_emg_stop(ushort RingNo, ushort GroupNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_group_sd_stop")]
        public static extern short CS_mnet_m1_group_sd_stop(ushort RingNo, ushort GroupNo);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m1_get_type")]
        public static extern short CS_mnet_m1_get_type(ushort RingNo, ushort SlaveIP, ref byte Type);

        ////////////////////////////////////////////////////////////////////////////////
        /////////////////////////                            ///////////////////////////
        ///////////////////////// 4-Axis Motion Slaves(M1X4) ///////////////////////////
        /////////////////////////                            ///////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //
        // Initial
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_initial")]
        public static extern short CS_mnet_m4_initial(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_initial_no_reset")]
        public static extern short CS_mnet_m4_initial_no_reset(ushort RingNo, ushort SlaveIP);
        //
        // Pulse I/O Configuration
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_pls_outmode")]
        public static extern short CS_mnet_m4_set_pls_outmode(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort pls_outmode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_pls_iptmode")]
        public static extern short CS_mnet_m4_set_pls_iptmode(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort pls_iptmode, ushort pls_iptdir);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_feedback_src")]
        public static extern short CS_mnet_m4_set_feedback_src(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort FbkSrc);
        //
        // Interface I/O Configuration
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_alm")]
        public static extern short CS_mnet_m4_set_alm(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort alm_logic, ushort alm_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_inp")]
        public static extern short CS_mnet_m4_set_inp(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort inp_enable, ushort inp_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_erc")]
        public static extern short CS_mnet_m4_set_erc(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort erc_logic, ushort erc_on_time, ushort erc_off_time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_erc_on")]
        public static extern short CS_mnet_m4_set_erc_on(ushort RingNo, ushort SlaveIP, ushort AxisNo, short on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_svon")]
        public static extern short CS_mnet_m4_set_svon(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_ralm")]
        public static extern short CS_mnet_m4_set_ralm(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_sd")]
        public static extern short CS_mnet_m4_set_sd(ushort RingNo, ushort SlaveIP, ushort AxisNo, short sd_enable, short sd_logic, short sd_latch, short sd_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_ell")]
        public static extern short CS_mnet_m4_set_ell(ushort RingNo, ushort SlaveIP, ushort AxisNo, short ell_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_mechanical_input_filter")]
        public static extern short CS_mnet_m4_set_mechanical_input_filter(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort on_off, ushort FilterLength);
        // I/O Status
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_get_io_status")]
        public static extern short CS_mnet_m4_get_io_status(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref uint IO_status);
        //
        // Velocity Control
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_v_move")]
        public static extern short CS_mnet_m4_v_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, byte Dir);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_fix_speed_range")]
        public static extern short CS_mnet_m4_fix_speed_range(ushort RingNo, ushort SlaveIP, ushort AxisNo, double MaxVel);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_unfix_speed_range")]
        public static extern short CS_mnet_m4_unfix_speed_range(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        //I16 PASCAL _mnet_m4_v_change( U16 RingNo, U16 SlaveIP, U16 AxisNo, F64 NewVel, F64 Time );
        //
        // Position Control
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_tmove_speed")]
        public static extern short CS_mnet_m4_set_tmove_speed(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_smove_speed")]
        public static extern short CS_mnet_m4_set_smove_speed(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_r_move")]
        public static extern short CS_mnet_m4_start_r_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Distance);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_a_move")]
        public static extern short CS_mnet_m4_start_a_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_p_change")]
        public static extern short CS_mnet_m4_start_p_change(ushort RingNo, ushort SlaveIP, short AxisNo, double NewPos);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_tr_move_xy")]
        public static extern short CS_mnet_m4_tr_move_xy(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_ta_move_xy")]
        public static extern short CS_mnet_m4_ta_move_xy(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_sr_move_xy")]
        public static extern short CS_mnet_m4_sr_move_xy(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_sa_move_xy")]
        public static extern short CS_mnet_m4_sa_move_xy(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_tr_move_zu")]
        public static extern short CS_mnet_m4_tr_move_zu(ushort RingNo, ushort SlaveIP, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_ta_move_zu")]
        public static extern short CS_mnet_m4_ta_move_zu(ushort RingNo, ushort SlaveIP, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_sr_move_zu")]
        public static extern short CS_mnet_m4_sr_move_zu(ushort RingNo, ushort SlaveIP, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_sa_move_zu")]
        public static extern short CS_mnet_m4_sa_move_zu(ushort RingNo, ushort SlaveIP, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_tr_line2")]
        public static extern short CS_mnet_m4_start_tr_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_ta_line2")]
        public static extern short CS_mnet_m4_start_ta_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_sr_line2")]
        public static extern short CS_mnet_m4_start_sr_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_sa_line2")]
        public static extern short CS_mnet_m4_start_sa_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_tr_line3")]
        public static extern short CS_mnet_m4_start_tr_line3(ushort RingNo, ushort SlaveIP, ref short AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_ta_line3")]
        public static extern short CS_mnet_m4_start_ta_line3(ushort RingNo, ushort SlaveIP, ref short AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_sr_line3")]
        public static extern short CS_mnet_m4_start_sr_line3(ushort RingNo, ushort SlaveIP, ref short AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_sa_line3")]
        public static extern short CS_mnet_m4_start_sa_line3(ushort RingNo, ushort SlaveIP, ref short AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_tr_line4")]
        public static extern short CS_mnet_m4_start_tr_line4(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_ta_line4")]
        public static extern short CS_mnet_m4_start_ta_line4(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_sr_line4")]
        public static extern short CS_mnet_m4_start_sr_line4(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_sa_line4")]
        public static extern short CS_mnet_m4_start_sa_line4(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        // Stop
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_sd_stop")]
        public static extern short CS_mnet_m4_sd_stop(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_emg_stop")]
        public static extern short CS_mnet_m4_emg_stop(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        //
        // Motion Status
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_motion_done")]
        public static extern short CS_mnet_m4_motion_done(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref ushort MoSt);
        //
        // Home
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_home_config")]
        public static extern short CS_mnet_m4_set_home_config(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort home_mode, ushort org_logic, ushort ez_logic, ushort ez_count, ushort erc_out);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_start_home_move")]
        public static extern short CS_mnet_m4_start_home_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, byte Dir);
        //
        // Counter Operating
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_get_command")]
        public static extern short CS_mnet_m4_get_command(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref int Cmd);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_command")]
        public static extern short CS_mnet_m4_set_command(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Cmd);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_reset_command")]
        public static extern short CS_mnet_m4_reset_command(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_get_position")]
        public static extern short CS_mnet_m4_get_position(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref int Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_position")]
        public static extern short CS_mnet_m4_set_position(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_reset_position")]
        public static extern short CS_mnet_m4_reset_position(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_get_current_speed")]
        public static extern short CS_mnet_m4_get_current_speed(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref double speed);
        //
        // Position Compare and Latch
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_ltc_logic")]
        public static extern short CS_mnet_m4_set_ltc_logic(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort ltc_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_get_latch_data")]
        public static extern short CS_mnet_m4_get_latch_data(ushort RingNo, ushort SlaveIP, ushort AxisNo, short LatchNo, ref double Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_comparator_data")]
        public static extern short CS_mnet_m4_set_comparator_data(ushort RingNo, ushort SlaveIP, ushort AxisNo, short CompNo, double Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_comparator_mode")]
        public static extern short CS_mnet_m4_set_comparator_mode(ushort RingNo, ushort SlaveIP, ushort AxisNo, short CompNo, short CmpMethod);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_trigger_comparator_mode")]
        public static extern short CS_mnet_m4_set_trigger_comparator_mode(ushort RingNo, ushort SlaveIP, ushort AxisNo, short CmpMethod);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_trigger_comparator_data")]
        public static extern short CS_mnet_m4_set_trigger_comparator_data(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Data);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_trigger_output_time")]
        public static extern short CS_mnet_m4_set_trigger_output_time(ushort RingNo, ushort SlaveIP, ushort Time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_auto_trigger")]
        public static extern short CS_mnet_m4_set_auto_trigger(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort CmpMethod, ushort Interval, ushort Logic, ushort on_off);
        //
        // Load Configuration File
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_load_motion_file")]
        public static extern short CS_mnet_m4_load_motion_file(ushort RingNo, ushort SlaveIP, string FilePath);
        //
        // MOF
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_pulser_iptmode")]
        public static extern short CS_mnet_m4_set_pulser_iptmode(ushort RingNo, ushort SlaveIP, ushort AxisNo, short InputMode, short Inverse);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_mof_config")]
        public static extern short CS_mnet_m4_set_mof_config(ushort RingNo, ushort SlaveIP, short MOF_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_disable_mof")]
        public static extern short CS_mnet_m4_disable_mof(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_get_mof_status")]
        public static extern short CS_mnet_m4_get_mof_status(ushort RingNo, ushort SlaveIP, ref ushort MOF_sts);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_manual_mpg")]
        public static extern short CS_mnet_m4_manual_mpg(ushort RingNo, ushort SlaveIP, short MPG_axis_sel);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_mof_jog_velocity")]
        public static extern short CS_mnet_m4_set_mof_jog_velocity(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc, double Tdec, short feedrate);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_mof_step_distance")]
        public static extern short CS_mnet_m4_set_mof_step_distance(ushort RingNo, ushort SlaveIP, ushort AxisNo, double step_dist);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_mof_step_velocity")]
        public static extern short CS_mnet_m4_set_mof_step_velocity(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc, double Tdec, short feedrate);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_mof_mpg_velocity")]
        public static extern short CS_mnet_m4_set_mof_mpg_velocity(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc, double Tdec);
        //
        // Synchronous Move - STA
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_sync_r_move")]
        public static extern short CS_mnet_m4_sync_r_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Distance);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_sync_a_move")]
        public static extern short CS_mnet_m4_sync_a_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Pos);
        //
        // Synchronous Move - internal
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_triggered_r_move")]
        public static extern short CS_mnet_m4_set_triggered_r_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Distance, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_triggered_a_move")]
        public static extern short CS_mnet_m4_set_triggered_a_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, int Pos, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_triggered_tr_line2")]
        public static extern short CS_mnet_m4_set_triggered_tr_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_triggered_ta_line2")]
        public static extern short CS_mnet_m4_set_triggered_ta_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_triggered_sr_line2")]
        public static extern short CS_mnet_m4_set_triggered_sr_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_triggered_sa_line2")]
        public static extern short CS_mnet_m4_set_triggered_sa_line2(ushort RingNo, ushort SlaveIP, ref short AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte timing);
        //
        // Others
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_continuous_move")]
        public static extern short CS_mnet_m4_set_continuous_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, short Enable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_set_velocity_limit")]
        public static extern short CS_mnet_m4_set_velocity_limit(ushort RingNo, ushort SlaveIP, ushort AxisNo, double MaxVel);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m4_open_move_check")]
        public static extern short CS_mnet_m4_open_move_check(short Enable);
        ////////////////////////////////////////////////////////////////////////////////
        /////////////////////////                            ///////////////////////////
        ///////////////////////// 4-Axis Motion Slaves(M204) ///////////////////////////
        /////////////////////////                            ///////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        //
        // Initial
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_initial")]
        public static extern short CS_mnet_m204_initial(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_initial_no_reset")]
        public static extern short CS_mnet_m204_initial_no_reset(ushort RingNo, ushort SlaveIP);
        //
        // Pulse I/O Configuration
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_pls_outmode")]
        public static extern short CS_mnet_m204_set_pls_outmode(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort pls_outmode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_pls_iptmode")]
        public static extern short CS_mnet_m204_set_pls_iptmode(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort pls_iptmode, ushort pls_iptdir);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_feedback_src")]
        public static extern short CS_mnet_m204_set_feedback_src(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort FbkSrc);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_move_ratio")]
        public static extern short CS_mnet_m204_set_move_ratio(ushort RingNo, ushort SlaveIP, ushort AxisNo, double move_ratio);
        //
        // Interface I/O Configuration
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_alm")]
        public static extern short CS_mnet_m204_set_alm(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort alm_logic, ushort alm_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_inp")]
        public static extern short CS_mnet_m204_set_inp(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort inp_enable, ushort inp_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_erc")]
        public static extern short CS_mnet_m204_set_erc(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort erc_logic, ushort erc_on_time, ushort erc_off_time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_erc_on")]
        public static extern short CS_mnet_m204_set_erc_on(ushort RingNo, ushort SlaveIP, ushort AxisNo, short on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_sd")]
        public static extern short CS_mnet_m204_set_sd(ushort RingNo, ushort SlaveIP, ushort AxisNo, short sd_enable, short sd_logic, short sd_latch, short sd_mode);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_svon")]
        public static extern short CS_mnet_m204_set_svon(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_ralm")]
        public static extern short CS_mnet_m204_set_ralm(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_ltc_logic")]
        public static extern short CS_mnet_m204_set_ltc_logic(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort ltc_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_el")]
        public static extern short CS_mnet_m204_set_el(ushort RingNo, ushort SlaveIP, ushort AxisNo, short el_mode, short el_logic);

        // I/O Status
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_io_status")]
        public static extern short CS_mnet_m204_get_io_status(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref ushort io_sts);
        //
        // Velocity Control
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_tv_move")]
        public static extern short CS_mnet_m204_tv_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sv_move")]
        public static extern short CS_mnet_m204_sv_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc, double SVacc);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_v_change")]
        public static extern short CS_mnet_m204_v_change(ushort RingNo, ushort SlaveIP, ushort AxisNo, double NewVel, double Time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_cmp_v_change")]
        public static extern short CS_mnet_m204_cmp_v_change(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Res_Dist, double OldVel, double NewVel, double Time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_fix_speed_range")]
        public static extern short CS_mnet_m204_fix_speed_range(ushort RingNo, ushort SlaveIP, ushort AxisNo, double MaxVel);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_unfix_speed_range")]
        public static extern short CS_mnet_m204_unfix_speed_range(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        //
        // Position Control
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_move")]
        public static extern short CS_mnet_m204_start_tr_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Dist, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_move")]
        public static extern short CS_mnet_m204_start_ta_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Pos, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_move")]
        public static extern short CS_mnet_m204_start_sr_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Dist, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_move")]
        public static extern short CS_mnet_m204_start_sa_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Pos, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_p_change")]
        public static extern short CS_mnet_m204_p_change(ushort RingNo, ushort SlaveIP, ushort AxisNo, double NewPos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_d_change")]
        public static extern short CS_mnet_m204_d_change(ushort RingNo, ushort SlaveIP, ushort AxisNo, double NewDist);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_line2")]
        public static extern short CS_mnet_m204_start_tr_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_line2")]
        public static extern short CS_mnet_m204_start_ta_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_line2")]
        public static extern short CS_mnet_m204_start_sr_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_line2")]
        public static extern short CS_mnet_m204_start_sa_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_line3")]
        public static extern short CS_mnet_m204_start_tr_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_line3")]
        public static extern short CS_mnet_m204_start_ta_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_line3")]
        public static extern short CS_mnet_m204_start_sr_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_line3")]
        public static extern short CS_mnet_m204_start_sa_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_line4")]
        public static extern short CS_mnet_m204_start_tr_line4(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_line4")]
        public static extern short CS_mnet_m204_start_ta_line4(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_line4")]
        public static extern short CS_mnet_m204_start_sr_line4(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_line4")]
        public static extern short CS_mnet_m204_start_sa_line4(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_arc2")]
        public static extern short CS_mnet_m204_start_tr_arc2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_arc2")]
        public static extern short CS_mnet_m204_start_ta_arc2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double Cx, double Cy, double Ex, double Ey, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_arc2")]
        public static extern short CS_mnet_m204_start_sr_arc2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_arc2")]
        public static extern short CS_mnet_m204_start_sa_arc2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double Cx, double Cy, double Ex, double Ey, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_arc_xyz")]
        public static extern short CS_mnet_m204_start_tr_arc_xyz(ushort RingNo, ushort SlaveIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, double DistZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_arc_xyz")]
        public static extern short CS_mnet_m204_start_ta_arc_xyz(ushort RingNo, ushort SlaveIP, double Cx, double Cy, double Ex, double Ey, double PosZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_arc_xyz")]
        public static extern short CS_mnet_m204_start_sr_arc_xyz(ushort RingNo, ushort SlaveIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, double DistZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_arc_xyz")]
        public static extern short CS_mnet_m204_start_sa_arc_xyz(ushort RingNo, ushort SlaveIP, double Cx, double Cy, double Ex, double Ey, double PosZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        // Conti Move
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_line2_conti")]
        public static extern short CS_mnet_m204_start_tr_line2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_line2_conti")]
        public static extern short CS_mnet_m204_start_ta_line2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_line2_conti")]
        public static extern short CS_mnet_m204_start_sr_line2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_line2_conti")]
        public static extern short CS_mnet_m204_start_sa_line2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_arc2_conti")]
        public static extern short CS_mnet_m204_start_tr_arc2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_arc2_conti")]
        public static extern short CS_mnet_m204_start_ta_arc2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double Cx, double Cy, double Ex, double Ey, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_arc2_conti")]
        public static extern short CS_mnet_m204_start_sr_arc2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_arc2_conti")]
        public static extern short CS_mnet_m204_start_sa_arc2_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double Cx, double Cy, double Ex, double Ey, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_arc_xyz_conti")]
        public static extern short CS_mnet_m204_start_tr_arc_xyz_conti(ushort RingNo, ushort SlaveIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, double DistZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_arc_xyz_conti")]
        public static extern short CS_mnet_m204_start_sr_arc_xyz_conti(ushort RingNo, ushort SlaveIP, double OffsetCx, double OffsetCy, double OffsetEx, double OffsetEy, double DistZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_arc_xyz_conti")]
        public static extern short CS_mnet_m204_start_ta_arc_xyz_conti(ushort RingNo, ushort SlaveIP, double Cx, double Cy, double Ex, double Ey, double PosZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_arc_xyz_conti")]
        public static extern short CS_mnet_m204_start_sa_arc_xyz_conti(ushort RingNo, ushort SlaveIP, double Cx, double Cy, double Ex, double Ey, double PosZ, short DIR, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_line3_conti")]
        public static extern short CS_mnet_m204_start_tr_line3_conti(ushort RingNo, ushort SlaveIP,  short[] AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_line3_conti")]
        public static extern short CS_mnet_m204_start_ta_line3_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_line3_conti")]
        public static extern short CS_mnet_m204_start_sr_line3_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_line3_conti")]
        public static extern short CS_mnet_m204_start_sa_line3_conti(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_tr_line4_conti")]
        public static extern short CS_mnet_m204_start_tr_line4_conti(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_ta_line4_conti")]
        public static extern short CS_mnet_m204_start_ta_line4_conti(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sr_line4_conti")]
        public static extern short CS_mnet_m204_start_sr_line4_conti(ushort RingNo, ushort SlaveIP, double DistX, double DistY, double DistZ, double DistU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_sa_line4_conti")]
        public static extern short CS_mnet_m204_start_sa_line4_conti(ushort RingNo, ushort SlaveIP, double PosX, double PosY, double PosZ, double PosU, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        //
        // Stop
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sd_stop")]
        public static extern short CS_mnet_m204_sd_stop(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_emg_stop")]
        public static extern short CS_mnet_m204_emg_stop(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        //
        // Motion Status
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_motion_done")]
        public static extern short CS_mnet_m204_motion_done(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref ushort McSts);
        //
        // Home
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_home_config")]
        public static extern short CS_mnet_m204_set_home_config(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort home_mode, ushort org_logic, ushort ez_logic, ushort ez_count, ushort erc_out);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_start_home_move")]
        public static extern short CS_mnet_m204_start_home_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_escape_home")]
        public static extern short CS_mnet_m204_escape_home(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_home_search")]
        public static extern short CS_mnet_m204_home_search(ushort RingNo, ushort SlaveIP, ushort AxisNo, double StrVel, double MaxVel, double Tacc, double ORG_Offset);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_home_auto_reset_counter")]
        public static extern short CS_mnet_m204_set_home_auto_reset_counter(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort reset_mode);

        //
        // Counter Operating
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_command")]
        public static extern short CS_mnet_m204_get_command(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref int cmd);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_position")]
        public static extern short CS_mnet_m204_get_position(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref int pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_current_speed")]
        public static extern short CS_mnet_m204_get_current_speed(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref double speed);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_error_counter")]
        public static extern short CS_mnet_m204_get_error_counter(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref short error);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_command")]
        public static extern short CS_mnet_m204_set_command(ushort RingNo, ushort SlaveIP, ushort AxisNo, int cmd);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_position")]
        public static extern short CS_mnet_m204_set_position(ushort RingNo, ushort SlaveIP, ushort AxisNo, int pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_reset_error_counter")]
        public static extern short CS_mnet_m204_reset_error_counter(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_rest_command")]
        public static extern short CS_mnet_m204_get_rest_command(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref int rest_command);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_reset_command")]
        public static extern short CS_mnet_m204_reset_command(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_reset_position")]
        public static extern short CS_mnet_m204_reset_position(ushort RingNo, ushort SlaveIP, ushort AxisNo);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_general_counter")]
        public static extern short CS_mnet_m204_set_general_counter(ushort RingNo, ushort SlaveIP, ushort AxisNo, short CntSrc, double SetValue);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_general_counter")]
        public static extern short CS_mnet_m204_get_general_counter(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref double GetValue);

        //
        // Others
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_sts")]
        public static extern short CS_mnet_m204_get_sts(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref uint sts);

        //Compare Function
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_general_comparator")]
        public static extern short CS_mnet_m204_set_general_comparator(ushort RingNo, ushort SlaveIP, ushort AxisNo, short CmpSrc, short CmpMethod, short CmpAction, double Data);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_trigger_comparator")]
        public static extern short CS_mnet_m204_set_trigger_comparator(ushort RingNo, ushort SlaveIP, ushort AxisNo, short CmpSrc, short CmpMethod, double Data);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_trigger_type")]
        public static extern short CS_mnet_m204_set_trigger_type(ushort RingNo, ushort SlaveIP, ushort AxisNo, short TriggerType);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_auto_compare")]
        public static extern short CS_mnet_m204_set_auto_compare(ushort RingNo, ushort SlaveIP, ushort CompareNo, ushort AxisCounterNo, short DIR, int Start, ushort Interval, int TriggerNumber);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_output_pulse_width")]
        public static extern short CS_mnet_m204_trigger_output_pulse_width(ushort RingNo, ushort SlaveIP, ushort CompareNo, ushort Time);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_enable_auto_compare")]
        public static extern short CS_mnet_m204_enable_auto_compare(ushort RingNo, ushort SlaveIP, ushort CompareNo, ushort Enable);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_soft_limit")]
        public static extern short CS_mnet_m204_set_soft_limit(ushort RingNo, ushort SlaveIP, ushort AxisNo, int PLimit, int NLimit);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_enable_soft_limit")]
        public static extern short CS_mnet_m204_enable_soft_limit(ushort RingNo, ushort SlaveIP, ushort AxisNo, short Action);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_disable_soft_limit")]
        public static extern short CS_mnet_m204_disable_soft_limit(ushort RingNo, ushort SlaveIP, ushort AxisNo);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_check_compare_data")]
        public static extern short CS_mnet_m204_check_compare_data(ushort RingNo, ushort SlaveIP, ushort AxisNo, short CmpType, ref double Pos);
        //SYNC
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_tr_move")]
        public static extern short CS_mnet_m204_sync_tr_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Dist, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_ta_move")]
        public static extern short CS_mnet_m204_sync_ta_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Pos, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_sr_move")]
        public static extern short CS_mnet_m204_sync_sr_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Dist, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_sa_move")]
        public static extern short CS_mnet_m204_sync_sa_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Pos, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_tr_line2")]
        public static extern short CS_mnet_m204_sync_tr_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_ta_line2")]
        public static extern short CS_mnet_m204_sync_ta_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_sr_line2")]
        public static extern short CS_mnet_m204_sync_sr_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_sync_sa_line2")]
        public static extern short CS_mnet_m204_sync_sa_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec);

        //Trigger Move
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_tr_move")]
        public static extern short CS_mnet_m204_trigger_tr_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Dist, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte Timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_ta_move")]
        public static extern short CS_mnet_m204_trigger_ta_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Pos, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte Timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_tirgger_sr_move")]
        public static extern short CS_mnet_m204_tirgger_sr_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Dist, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte Timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_tirgger_sa_move")]
        public static extern short CS_mnet_m204_tirgger_sa_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, double Pos, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte Timing);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_tr_line2")]
        public static extern short CS_mnet_m204_trigger_tr_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_ta_line2")]
        public static extern short CS_mnet_m204_trigger_ta_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_sr_line2")]
        public static extern short CS_mnet_m204_trigger_sr_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_sa_line2")]
        public static extern short CS_mnet_m204_trigger_sa_line2(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte timing);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_tr_line3")]
        public static extern short CS_mnet_m204_trigger_tr_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte Timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_ta_line3")]
        public static extern short CS_mnet_m204_trigger_ta_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec, ushort SrcAxisNo, byte Timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_sr_line3")]
        public static extern short CS_mnet_m204_trigger_sr_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double DistX, double DistY, double DistZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte Timing);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_trigger_sa_line3")]
        public static extern short CS_mnet_m204_trigger_sa_line3(ushort RingNo, ushort SlaveIP, short[] AxisArray, double PosX, double PosY, double PosZ, double StrVel, double MaxVel, double Tacc, double Tdec, double SVacc, double SVdec, ushort SrcAxisNo, byte Timing);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_continuous_move")]
        public static extern short CS_mnet_m204_set_continuous_move(ushort RingNo, ushort SlaveIP, ushort AxisNo, short Conti_logic);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_check_continuous_buffer")]
        public static extern short CS_mnet_m204_check_continuous_buffer(ushort RingNo, ushort SlaveIP, ushort AxisNo, ref ushort on_off);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_load_motion_file")]
        public static extern short CS_mnet_m204_load_motion_file(ushort RingNo, ushort SlaveIP, string FilePath);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_soft_sta_signal")]
        public static extern short CS_mnet_m204_set_soft_sta_signal(ushort RingNo, ushort SlaveIP, short on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_soft_stp_signal")]
        public static extern short CS_mnet_m204_set_soft_stp_signal(ushort RingNo, ushort SlaveIP, short on_off);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_stp_mode")]
        public static extern short CS_mnet_m204_set_stp_mode(ushort RingNo, ushort SlaveIP, ushort AxisNo, short stp_mode);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_FIFO_latch_src")]
        public static extern short CS_mnet_m204_set_FIFO_latch_src(ushort RingNo, ushort SlaveIP, ushort AxisCounterNo, ushort LatchInputNo, ushort Enable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_latch_data")]
        public static extern short CS_mnet_m204_get_latch_data(ushort RingNo, ushort SlaveIP, ushort AxisNo, short LatchNo, ref int Pos);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_latch_data_from_buffer")]
        public static extern short CS_mnet_m204_get_latch_data_from_buffer(ushort RingNo, ushort SlaveIP, ushort BufferNo, ref ushort AxisCounterNo, ref ushort LatchDataCnt, ref int LatchDataTable);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_reset_latch_FIFO")]
        public static extern short CS_mnet_m204_reset_latch_FIFO(ushort RingNo, ushort SlaveIP);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_latch_FIFO_length")]
        public static extern short CS_mnet_m204_get_latch_FIFO_length(ushort RingNo, ushort SlaveIP, ref ushort length);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_axis_counter")]
        public static extern short CS_mnet_m204_set_axis_counter(ushort RingNo, ushort SlaveIP, ushort AxisCounterNo, ushort CntMode, ushort CntDir, int SetValue);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_axis_counter")]
        public static extern short CS_mnet_m204_get_axis_counter(ushort RingNo, ushort SlaveIP, ushort AxisCounterNo, ref int GetValue);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_axis_counter_value")]
        public static extern short CS_mnet_m204_set_axis_counter_value(ushort RingNo, ushort SlaveIP, ushort AxisCounterNo, int SetValue);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_axis_counter_param")]
        public static extern short CS_mnet_m204_set_axis_counter_param(ushort RingNo, ushort SlaveIP, ushort AxisCounterNo, ushort CntMode, ushort CntDir);


        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_velocity_limit")]
        public static extern short CS_mnet_m204_set_velocity_limit(ushort RingNo, ushort SlaveIP, ushort AxisNo, double MaxVel);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_hardware_info")]
        public static extern short CS_mnet_m204_get_hardware_info(ushort RingNo, ushort SlaveIP, ref byte Version);

        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_get_type")]
        public static extern short CS_mnet_m204_get_type(ushort RingNo, ushort SlaveIP, ref byte Type);

        // Ring Count
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_set_ring_count")]
        public static extern short CS_mnet_m204_set_ring_count(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort src, double data);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_m204_enable_ring_count")]
        public static extern short CS_mnet_m204_enable_ring_count(ushort RingNo, ushort SlaveIP, ushort AxisNo, ushort src, ushort On_Off);


        [DllImport("CMnet.dll", EntryPoint = "_mnet_io_save_all_output_status")]
        public static extern short CS_mnet_io_save_all_output_status(ushort RingNo, string FilePath);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_io_restore_all_output_status")]
        public static extern short CS_mnet_io_restore_all_output_status(ushort RingNo, string FilePath);


        //Error Log
        [DllImport("CMnet.dll", EntryPoint = "_mnet_error_log_enable")]
        public static extern short CS_mnet_error_log_enable(ref string pFileName);
        [DllImport("CMnet.dll", EntryPoint = "_mnet_error_log_disable")]
        public static extern short CS_mnet_error_log_disable();

        ///////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////





    }
}
