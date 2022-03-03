using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TPM;

namespace PCI_L122DSF_D122
{
    public partial class Form1 : Form
    {
        short ret = 0;
        ushort m_Baudrate0 = 0;
        ushort m_CardNo = 0;
        ushort m_RingNo = 0;//only one ring
        ushort m_IP = 0;
        uint[] lDevTable = new uint[2];
        ///////////////////////////
        Label[,] lb_DIp = new Label[4, 8];
        Label[,] lb_DOp = new Label[4, 8];
        Label[] lb_DIO = new Label[16];
        byte[] Value = new byte[4];

        public Form1()
        {
            InitializeComponent();

            int x, y;
            for (int i = 0; i < 3; i++)
            {
                x = gBox_DI.Width / 12;
                y = gBox_DI.Height / 10 + i * (gBox_DI.Width / 12 + 5);//i * 32;

                for (int j = 0; j < 8; j++)
                {
                    if (i == 0)
                    {
                        lb_DIO[j] = new Label();
                        lb_DIO[j].Text = j.ToString();
                        lb_DIO[j].TextAlign = ContentAlignment.MiddleCenter;
                        lb_DIO[j].Location = new System.Drawing.Point(x, y);
                        lb_DIO[j].Size = new System.Drawing.Size(gBox_DI.Width / 12, gBox_DI.Width / 12);
                        x += gBox_DI.Width / 10;
                        gBox_DI.Controls.Add(lb_DIO[j]);
                    }
                    else
                    {
                        lb_DIp[i - 1, j] = new Label();
                        lb_DIp[i - 1, j].Location = new System.Drawing.Point(x, y);
                        lb_DIp[i - 1, j].Size = new System.Drawing.Size(gBox_DI.Width / 12, gBox_DI.Width / 12);
                        lb_DIp[i - 1, j].Tag = (i - 1) * 10 + j;
                        lb_DIp[i - 1, j].BackColor = Color.Black;
                        lb_DIp[i - 1, j].ForeColor = Color.White;
                        lb_DIp[i - 1, j].TextAlign = ContentAlignment.MiddleCenter;
                        lb_DIp[i - 1, j].Text = string.Format("DI{0}", (i - 1) * 8 + j);

                        x += gBox_DI.Width / 10;
                        gBox_DI.Controls.Add(lb_DIp[i - 1, j]);

                    }
                }
            }//end of for i

            for (int i = 0; i < 3; i++)
            {
                x = gBox_DO.Width / 12;
                y = gBox_DO.Height / 10 + i * (gBox_DO.Width / 12 + 5);//i * 32;

                for (int j = 0; j < 8; j++)
                {
                    if (i == 0)
                    {
                        lb_DIO[j] = new Label();
                        lb_DIO[j].Text = j.ToString();
                        lb_DIO[j].TextAlign = ContentAlignment.MiddleCenter;
                        lb_DIO[j].Location = new System.Drawing.Point(x, y);
                        lb_DIO[j].Size = new System.Drawing.Size(gBox_DO.Width / 12, gBox_DO.Width / 12);
                        x += gBox_DO.Width / 10;
                        gBox_DO.Controls.Add(lb_DIO[j]);
                    }
                    else
                    {
                        lb_DOp[i - 1, j] = new Label();
                        lb_DOp[i - 1, j].Location = new System.Drawing.Point(x, y);
                        lb_DOp[i - 1, j].Size = new System.Drawing.Size(gBox_DO.Width / 12, gBox_DO.Width / 12);
                        lb_DOp[i - 1, j].Tag = (i - 1) * 10 + j;
                        lb_DOp[i - 1, j].BackColor = Color.Black;
                        lb_DOp[i - 1, j].ForeColor = Color.White;
                        lb_DOp[i - 1, j].TextAlign = ContentAlignment.MiddleCenter;
                        lb_DOp[i - 1, j].Text = string.Format("DO{0}", (i - 1) * 8 + j);

                        x += gBox_DO.Width / 10;
                        gBox_DO.Controls.Add(lb_DOp[i - 1, j]);
                        lb_DOp[i - 1, j].Click += new EventHandler(DO_Click);
                    }
                }
            }//end of for i

            cBox_cardno.SelectedIndex = 0;
            cBox_baudrate.SelectedIndex = 2;
            timer1.Interval = 100;
        }


        private void cBox_baudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Baudrate0 = (ushort)cBox_baudrate.SelectedIndex;
        }
        private void btn_Initial_Click(object sender, EventArgs e)
        {
            ret = 0;
            ushort existcards = 0;


            ret += Master.PCI_L122_DSF._l122_dsf_open(ref existcards);
            if (ret != 0)
            {
                MessageBox.Show("No any PCI_L122_DSF!!!");
                return;
            }
            else
            {
                ret = Master.PCI_L122_DSF._l122_dsf_get_switch_card_num((ushort)cBox_cardno.SelectedIndex, ref m_CardNo);
                ret = Master.PCI_L122_DSF._l122_dsf_get_start_ring_num(m_CardNo, ref m_RingNo);
            }



            if (ret == 0)
            {
                btn_Initial.BackColor = Color.Lime;
                btn_CheckSlave.Enabled = true;
                btn_Initial.Enabled = false;
            }
            else
            {
                btn_Initial.BackColor = Color.Red;
            }
        }

        private void btn_CheckSlave_Click(object sender, EventArgs e)
        {
            if (Master.PCI_L122_DSF._l122_dsf_set_ring_config(m_CardNo, m_RingNo, m_Baudrate0) != 0)
            {
                MessageBox.Show("Set ring config fail !!");
                return;
            }

            if (MNet.Basic._mnet_reset_ring(m_RingNo) != 0)
            {
                MessageBox.Show("Reset Ring fail!!");
                return;
            }

            ret = MNet.Basic._mnet_start_ring(m_RingNo);

            ret = MNet.Basic._mnet_get_ring_active_table(m_RingNo, lDevTable);

            if (ret == -74)
            {
                MessageBox.Show("No Device!");
                return;
            }

            MNet.SlaveType slavetype = 0;

            for (ushort ip = 0; ip < 64; ip++)
            {
                if ((lDevTable[ip / 32] & (0x01 << (ip % 32))) != 0)
                {
                    ret += MNet.Basic._mnet_get_slave_type(m_RingNo, ip, ref slavetype);
                    if (slavetype == MNet.SlaveType.DIO_I16Q16)//EZM-D122
                    {
                        gBox_DI.Text += string.Format(" SlaveIP: {0}", ip);
                        gBox_DO.Text += string.Format(" SlaveIP: {0}", ip);
                        m_IP = ip;
                        btn_CheckSlave.BackColor = Color.Lime;
                        btn_CheckSlave.Enabled = false;
                        gBox_DI.Enabled = true;
                        gBox_DO.Enabled = true;
                        timer1.Enabled = true;
                    }

                }
            }

            if (btn_CheckSlave.BackColor != Color.Lime)
            {
                btn_CheckSlave.BackColor = Color.Red;
                MessageBox.Show("Cann't find 107-D122!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DO_Click(object sender, EventArgs e)
        {
            int index = (int)(sender as Label).Tag;
            byte port = Convert.ToByte(index / 10+2);
            Value[port] ^= Convert.ToByte(0x01 << (index % 10));

            MNet.Basic._mnet_io_output(0, m_IP, port, Value[port]);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (byte port = 0; port < 4; port++)
            {
                Value[port] = Convert.ToByte(MNet.Basic._mnet_io_input(0, m_IP, port));

                if (port < 2)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((Value[port] & (0x01 << i)) == 0x01 << i)
                            lb_DIp[port, i].BackColor = Color.Lime;

                        else
                            lb_DIp[port, i].BackColor = Color.Black;
                    }

                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((Value[port] & (0x01 << i)) == 0x01 << i)
                            lb_DOp[port-2, i].BackColor = Color.Red;

                        else
                            lb_DOp[port-2, i].BackColor = Color.Black;
                    }
                }
            }


        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            byte[] DIO = new byte[2] { 0x00, 0x00 };

            ret = MNet.Basic._mnet_io_output_all(0, m_IP, DIO);

            ret = MNet.Basic._mnet_close();

            ret = Master.PCI_L122_DSF._l122_dsf_close((ushort)(cBox_cardno.SelectedIndex));
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}