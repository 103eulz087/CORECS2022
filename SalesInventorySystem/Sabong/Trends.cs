using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Net.WebSockets;
using System.Threading;

namespace SalesInventorySystem.Sabong
{
    public partial class Trends : DevExpress.XtraEditors.XtraForm
    {
        private Classes.WebSocketDashboardClient dashboardClient;
        private ClientWebSocket ws;
        private string serverUri = "ws://127.0.0.1:7990/ws/"; // Change to your VM IP
        //private string connectionString = "Server=127.0.0.1:7990;Database=ITCOREPOSDEMO_ERP;User Id=eulz;Password=123123;";

        public Trends()
        {
            InitializeComponent();
            Task.Run(async () => await ConnectWebSocket());
        }
        private async Task ConnectWebSocket()
        {
            ws = new ClientWebSocket();
            await ws.ConnectAsync(new Uri(serverUri), CancellationToken.None);
            _ = ListenForUpdates();
        }
        private async Task ListenForUpdates()
        {
            var buffer = new byte[1024];

            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                if (message == "update")
                {
                    UpdateDashboardFromDatabase();
                }
            }
        }

        private void UpdateDashboardFromDatabase()
        {
            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT SUM(Amount) FROM Bets", conn);
                    var result = cmd.ExecuteScalar();
                    decimal totalAmount = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                    label1.Invoke((MethodInvoker)(() =>
                    {
                        label1.Text = totalAmount.ToString("C");
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating dashboard: " + ex.Message);
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Trends_Load(object sender, EventArgs e)
        {
            //dashboardClient = new Classes.WebSocketDashboardClient(
            //        "ws://127.0.0.1:7990/ws/",
            //        label1
            //    );

            //Task.Run(async () => await dashboardClient.ConnectAsync());

        }
    }
}