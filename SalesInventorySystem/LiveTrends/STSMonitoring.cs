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

namespace SalesInventorySystem.LiveTrends
{
    public partial class STSMonitoring : DevExpress.XtraEditors.XtraForm
    {
        public STSMonitoring()
        {
            InitializeComponent();
        }

        private async void STSMonitoring_Load(object sender, EventArgs e)
        {
            this.Text = "Connecting...";
            this.ForeColor = Color.Orange;

            // 1. Load data immediately (using await so UI doesn't freeze on load)
            //await LoadLiveCountsAsync();
            // Run BOTH queries at the exact same time
            await Task.WhenAll(LoadLiveCountsAsync(), LoadLiveCountsStsAsync(), LoadTopPendingDeliveriesAsync());
            // 2. Setup the auto-refresh timer (30 seconds)
            timerLiveSync.Interval = 30000;
            timerLiveSync.Start();
        }

        private async Task LoadLiveCountsAsync()
        {
            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetDailyDeliveryMonitor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Asynchronous open prevents the UI from freezing
                        await conn.OpenAsync();

                        // Asynchronous execution
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                lblpending.Text = reader["PendingCount"].ToString();
                                lblreceiving.Text = reader["ReceivingCount"].ToString();
                                lblcompleted.Text = reader["CompletedCount"].ToString();

                                // Update Status to show it is healthy
                                this.Text = "Live (Online)";
                                this.ForeColor = Color.Green;
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Network dropped or SQL Server is unreachable
                this.Text = "Offline - Retrying...";
                this.ForeColor = Color.Red;

                // Note: We leave the old numbers on the screen, so they at least 
                // have the last known good data while it tries to reconnect.
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                this.Text = "Error - Retrying...";
                this.ForeColor = Color.Red;
            }
        }

        private async Task LoadLiveCountsStsAsync()
        {
            try
            {
                using (SqlConnection conn = Database.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("splive_GetDailySTSMonitor", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Asynchronous open prevents the UI from freezing
                        await conn.OpenAsync();

                        // Asynchronous execution
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                lblforapproval.Text = reader["ForApprovalCount"].ToString();
                                lblrejected.Text = reader["RejectedCount"].ToString();
                                lblapproved.Text = reader["ApprovedCount"].ToString();

                                // Update Status to show it is healthy
                                this.Text = "Live (Online)";
                                this.ForeColor = Color.Green;
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                // Network dropped or SQL Server is unreachable
                this.Text = "Offline - Retrying...";
                this.ForeColor = Color.Red;

                // Note: We leave the old numbers on the screen, so they at least 
                // have the last known good data while it tries to reconnect.
            }
            catch (Exception ex)
            {
                // Catch any other unexpected errors
                this.Text = "Error - Retrying...";
                this.ForeColor = Color.Red;
            }
        }

        private async Task LoadTopPendingDeliveriesAsync()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = Database.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetTopPendingDeliveries", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Open connection asynchronously
                        await conn.OpenAsync();

                        // Execute reader asynchronously
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            // Load the data into the DataTable
                            // (dt.Load is synchronous, but for 20 rows it takes 0.001 seconds, so it will not freeze the UI)
                            dt.Load(reader);
                        }
                    }
                }

                // Bind the DataTable to the DevExpress GridControl
                gridControl3.DataSource = dt;

                // Optional but recommended: Auto-resize the DevExpress columns to fit the new data
                gridView3.BestFitColumns();
            }
            catch (Exception)
            {
                // If the network drops, we just fail silently here. 
                // Your LoadLiveCountsAsync() method is already handling the red "Offline" status label.
            }
        }

        private void STSMonitoring_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerLiveSync.Stop();
        }

        private async void timerLiveSync_Tick(object sender, EventArgs e)
        {
            timerLiveSync.Stop();

            // Run BOTH queries at the exact same time during the 30-second tick
            await Task.WhenAll(LoadLiveCountsAsync(), LoadLiveCountsStsAsync(), LoadTopPendingDeliveriesAsync());

            timerLiveSync.Start();
        }

        private void lblforapproval_Paint(object sender, PaintEventArgs e)
        {
            LabelControl lbl = sender as LabelControl;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Brush b = new SolidBrush(lbl.BackColor))
            using (Pen p = new Pen(Color.Gray, 1))
            {
                int radius = 10; // corner radius
                Rectangle rect = lbl.ClientRectangle;

                using (System.Drawing.Drawing2D.GraphicsPath path = RoundedRect(rect, radius))
                {
                    e.Graphics.FillPath(b, path);
                    e.Graphics.DrawPath(p, path);
                }
            }

            // Draw the text
            TextRenderer.DrawText(e.Graphics, lbl.Text, lbl.Font, lbl.ClientRectangle, lbl.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

        }
        private System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

    }
}