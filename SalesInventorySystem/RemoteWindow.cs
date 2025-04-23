using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Win;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Popup;

namespace SalesInventorySystem
{
    public partial class RemoteWindow : Form
    {
        delegate void SetTextCallback(string text);
        TcpListener listener;
        TcpClient client;
        NetworkStream ns;
        Thread t = null;
       
        public RemoteWindow()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
        }
        public void DoWork()
        {
            byte[] bytes = new byte[1024];
            while (true)
            {
                Thread.Sleep(10);
                int bytesRead = ns.Read(bytes, 0, bytes.Length);
                this.SetText(Encoding.ASCII.GetString(bytes, 0, bytesRead));
                //XtraMessageBox.Show(Encoding.ASCII.GetString(bytes, 0, bytesRead));
            }
        }
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = this.textBox1.Text + text;
            }
        }

        private void RemoteWindow_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewBranchOrder viewBrd = new ViewBranchOrder();
            viewBrd.Show();
            this.Hide();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            listener = new TcpListener(System.Net.IPAddress.Any, 8888);
            listener.Start();
            client = listener.AcceptTcpClient();
            ns = client.GetStream();
            t = new Thread(DoWork);
            t.Start();
        }

        private void dateEdit1_Popup(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = monthEdit1.Text + " " + comboBoxEdit1.Text;
        }

        public static void SendRawData(string printerIpAddress, int printerPort, string dataToSend)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(printerIpAddress, printerPort);

                // Convert the string data to bytes using a suitable encoding (e.g., ASCII, UTF-8)
                byte[] dataBytes = Encoding.ASCII.GetBytes(dataToSend);

                // Get the network stream
                NetworkStream stream = client.GetStream();

                // Send the data
                stream.Write(dataBytes, 0, dataBytes.Length);
                Console.WriteLine($"Data sent successfully to {printerIpAddress}:{printerPort}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Close the connection
                client?.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string printerIp = "192.168.0.103"; // Replace with your printer's IP address
            int printerPort = 9100;              // Typically 9100 for raw printing
            string receiptData = "This is a test receipt.\n\x1D\x56\x41\x03"; // Example with ESC/POS cut command

            SendRawData(printerIp, printerPort, receiptData);
            Console.ReadKey();
        }
    }
}
