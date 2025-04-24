using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        private Dictionary<string, Tuple<string, int>> printerMappings = new Dictionary<string, Tuple<string, int>>();
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
            
        }

        private void LoadPrinterMappings()
        {
             
                      
                try
                {
                    SqlConnection connection = Database.getConnection();
                    connection.Open();
                    string query = "SELECT ID, PrinterName, PrinterIPAddress FROM PrinterMapping"; // Adjust query and table/column names

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string ID = reader.GetString(0);
                                string PrinterName = reader.GetString(1);
                                string PrinterIPAddress = reader.GetString(2);
                                printerMappings[PrinterName.ToLower()] = Tuple.Create(PrinterIPAddress, 9100);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading printer mappings: {ex.Message}");
                    // Handle the error appropriately (e.g., use default mappings or log)
                }
            

            //// Example fallback mappings if database loading fails or for categories not in the DB
            //if (!printerMappings.ContainsKey("beverages"))
            //{
            //    printerMappings["beverages"] = Tuple.Create("192.168.0.101", 9100);
            //}
            //if (!printerMappings.ContainsKey("grill"))
            //{
            //    printerMappings["grill"] = Tuple.Create("192.168.0.102", 9100);
            //}
            // Add more fallback mappings as needed
        }

        public void PrintOrderDynamic(int orderId)
        {
            Dictionary<string, StringBuilder> categoryReceipts = new Dictionary<string, StringBuilder>();

            SqlConnection connection = Database.getConnection();
            connection.Open();
            string query = "SELECT ItemName, ItemType FROM OrderItems WHERE OrderId = @OrderId"; // Adjust your query

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string itemName = reader.GetString(0);
                            string itemType = reader.GetString(1).ToLower(); // Use lowercase for consistent matching
                            string itemPrintData = GeneratePrintData(itemName);

                            if (printerMappings.ContainsKey(itemType))
                            {
                                if (!categoryReceipts.ContainsKey(itemType))
                                {
                                    categoryReceipts[itemType] = new StringBuilder();
                                    categoryReceipts[itemType].AppendLine($"Order ID: {orderId}");
                                    categoryReceipts[itemType].AppendLine($"----- {itemType.ToUpper()} -----");
                                }
                                categoryReceipts[itemType].AppendLine(itemPrintData);
                            }
                            else
                            {
                                Console.WriteLine($"No printer mapping found for category '{itemType}' for item '{itemName}'. Not printed.");
                                // Handle unmapped categories (e.g., log, print to a default printer)
                            }
                        }
                    }
                }

                // Send the accumulated data for each category to its respective printer
                foreach (var category in categoryReceipts.Keys)
                {
                    if (categoryReceipts[category].Length > ($"Order ID: {orderId}\n----- {category.ToUpper()} -----\n").Length && printerMappings.ContainsKey(category))
                    {
                        var printerInfo = printerMappings[category];
                        SendRawData(printerInfo.Item1, printerInfo.Item2, categoryReceipts[category].ToString());
                        Console.WriteLine($"{category.ToUpper()} receipt sent for Order ID: {orderId} to {printerInfo.Item1}:{printerInfo.Item2}");
                    }
                }
            
        }

        private string GeneratePrintData(string itemName)
        {
            return itemName; // Replace with your actual formatting
        }
    }
}
