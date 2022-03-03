using System;
using System.Collections.Generic;
using MainFrm.Hardware.IO.ErrCode;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainFrm.Hardware.IO.L112
{
    class IO_L112:IIO
    {

        #region Method

        public override RetErr Open()
        {
            RetErr ret = new RetErr();
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }
        public override RetErr Close()
        {
            RetErr ret = new RetErr();
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }
        public override RetErr CheckOpen(out Boolean isOpenOrNot)
        {
            RetErr ret = new RetErr();
            bool b = false ;
            isOpenOrNot = b;
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }
        public override RetErr SetOutPut(uint index, Boolean state)
        {
            RetErr ret = new RetErr();
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }
        public override RetErr GetOutPut(int index, out Boolean state)
        {
            RetErr ret = new RetErr();
            bool b = false;
            state = b;
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }
        public override RetErr GetInPut(int index, out Boolean state)
        {
            RetErr ret = new RetErr();
            bool b = false;
            state = b;
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }
        public override RetErr SetAnalogOutput(int index, double value)
        {
            RetErr ret = new RetErr();
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }
        public override RetErr GetAnalogInput(int index, out double value)
        {
            RetErr ret = new RetErr();
            double d = 0;
            value = d;
            try
            {
            }
            catch (Exception ee)
            {
            }
            return ret;
        }

        #endregion
    }
}
