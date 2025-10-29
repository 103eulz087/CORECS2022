using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    public class WebSocketDashboardClient
    {

        private ClientWebSocket ws;
        private string serverUri;
        private Label lblTotalAmount;
        private string connectionString;

        public WebSocketDashboardClient(string uri, Label totalLabel)
        {
            serverUri = uri;
            lblTotalAmount = totalLabel;
            //connectionString = dbConnection;
            ws = new ClientWebSocket();
        }

        public async Task ConnectAsync()
        {
            await ws.ConnectAsync(new Uri(serverUri), CancellationToken.None);
            _ = ListenForUpdates(); // fire and forget
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
                SqlConnection conn = Database.getConnection();
                
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT SUM(Amount) FROM GameDetails", conn);
                    var result = cmd.ExecuteScalar();
                    decimal totalAmount = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                    lblTotalAmount.Invoke((MethodInvoker)(() =>
                    {
                        lblTotalAmount.Text = totalAmount.ToString("C");
                    }));
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating dashboard: " + ex.Message);
            }
        }

    }
}
