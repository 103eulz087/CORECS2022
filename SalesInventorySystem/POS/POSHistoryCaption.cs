using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSHistoryCaption : Form
    {
        public static bool transactiondone = false;
        SerialPort _serialPort=null;
        private delegate void SetTextDeleg(string text);
        public POSHistoryCaption()
        {
            InitializeComponent();
        }

        private void POSHistoryCaption_Load(object sender, EventArgs e)
        {
            //_serialPort = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
            //_serialPort.Handshake = Handshake.None;
            //_serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            //_serialPort.ReadTimeout = 500;
            //_serialPort.WriteTimeout = 500;
            //_serialPort.Open();

           

            button1.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
           
            if (keyData == Keys.Escape)
            {
                transactiondone = true;
                this.Dispose();
            }
            else if (keyData == Keys.Enter)
            {
                transactiondone = true;
                this.Dispose();
            }
           
            return functionReturnValue;
        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = _serialPort.ReadLine();
            //data = "";
            //data = _serialPort.ReadLine();
            //data = _serialPort.ReadExisting();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }
        private void si_DataReceived(string data)
        {
            txtamountchangecap.Text = data.Trim();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            transactiondone = true;
            this.Dispose();
        }
    }
}
