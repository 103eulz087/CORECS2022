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
using DevExpress.Utils.Layout;

namespace SalesInventorySystem.LiveTrends
{
    public partial class STSMonitoring : DevExpress.XtraEditors.XtraForm
    {
        public STSMonitoring()
        {
            InitializeComponent();
            //ApplyRoundedStyleToLabels(this);
            gridControlSalesNotDeducted.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControlSalesNotDeducted.LookAndFeel.SetSkinStyle("Metropolis Dark");
            gridControlUploadedSales.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControlUploadedSales.LookAndFeel.SetSkinStyle("Metropolis Dark");
            gridControlForRcvng.LookAndFeel.UseDefaultLookAndFeel = false;
            gridControlForRcvng.LookAndFeel.SetSkinStyle("Metropolis Dark");
            tablePanel1.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            tablePanel2.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            tablePanel3.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
        }

        private async void STSMonitoring_Load(object sender, EventArgs e)
        {
            this.Text = "Connecting...";
            this.ForeColor = Color.Orange;

            // 1. Load data immediately (using await so UI doesn't freeze on load)
            //await LoadLiveCountsAsync();
            // Run BOTH queries at the exact same time
            await Task.WhenAll(LoadLiveCountsAsync(), LoadLiveCountsStsAsync(), LoadTopPendingDeliveriesAsync(),LoadSalesUploadButNotDeductedAsync(), LoadBranchUploadedSalesAsync());
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
                gridControlForRcvng.DataSource = dt;

                // Optional but recommended: Auto-resize the DevExpress columns to fit the new data
                gridViewForRcvng.BestFitColumns();
            }
            catch (Exception)
            {
                // If the network drops, we just fail silently here. 
                // Your LoadLiveCountsAsync() method is already handling the red "Offline" status label.
            }
        }

        private async Task LoadSalesUploadButNotDeductedAsync()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = Database.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM [view_SalesUploadButNotDeducted]", conn))
                    {
                        cmd.CommandType = CommandType.Text;

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
                gridControlSalesNotDeducted.DataSource = dt;

                // Optional but recommended: Auto-resize the DevExpress columns to fit the new data
                gridViewSalesNotDeducted.BestFitColumns();
            }
            catch (Exception)
            {
                // If the network drops, we just fail silently here. 
                // Your LoadLiveCountsAsync() method is already handling the red "Offline" status label.
            }
        }


        private async Task LoadBranchUploadedSalesAsync()
        {
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection conn = Database.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM [view_BranchUploadedSales] ORDER BY BranchCode", conn))
                    {
                        cmd.CommandType = CommandType.Text;

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
                gridControlUploadedSales.DataSource = dt;

                // Optional but recommended: Auto-resize the DevExpress columns to fit the new data
                gridViewUploadedSales.BestFitColumns();
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
            await Task.WhenAll(LoadLiveCountsAsync(), LoadLiveCountsStsAsync(), LoadTopPendingDeliveriesAsync(), LoadSalesUploadButNotDeductedAsync(), LoadBranchUploadedSalesAsync());

            timerLiveSync.Start();
        }

        private void lblforapproval_Paint(object sender, PaintEventArgs e)
        {
            //LabelControl lbl = sender as LabelControl;
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //using (Brush b = new SolidBrush(lbl.BackColor))
            //using (Pen p = new Pen(Color.Gray, 1))
            //{
            //    int radius = 10; // corner radius
            //    Rectangle rect = lbl.ClientRectangle;

            //    using (System.Drawing.Drawing2D.GraphicsPath path = RoundedRect(rect, radius))
            //    {
            //        e.Graphics.FillPath(b, path);
            //        e.Graphics.DrawPath(p, path);
            //    }
            //}

            //// Draw the text
            //TextRenderer.DrawText(e.Graphics, lbl.Text, lbl.Font, lbl.ClientRectangle, lbl.ForeColor,
            //    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

        }
      

        private void tablePanel3_Paint(object sender, PaintEventArgs e)
        {
            TablePanel panel = sender as TablePanel;
            // Get the grid info to find cell boundaries
            var grid = panel.GetViewInfo().Layout.Grid;

            // Example: Draw a red border around the entire panel
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);

        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {
            TablePanel panel = sender as TablePanel;
            // Get the grid info to find cell boundaries
            var grid = panel.GetViewInfo().Layout.Grid;

            // Example: Draw a red border around the entire panel
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);

        }

        private void tablePanel2_Paint(object sender, PaintEventArgs e)
        {
            TablePanel panel = sender as TablePanel;
            // Get the grid info to find cell boundaries
            var grid = panel.GetViewInfo().Layout.Grid;

            // Example: Draw a red border around the entire panel
            ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);

        }

        //private void ApplyRoundedStyleToLabels(Control parent)
        //{
        //    foreach (Control ctrl in parent.Controls)
        //    {
        //        if (ctrl is LabelControl lbl)
        //        {
        //            lbl.Paint -= RoundedLabel_Paint; // avoid duplicate subscription
        //            lbl.Paint += RoundedLabel_Paint;
        //        }

        //        // recurse into child containers (like panels, groupboxes)
        //        if (ctrl.HasChildren)
        //            ApplyRoundedStyleToLabels(ctrl);
        //    }
        //}
        //private void RoundedLabel_Paint(object sender, PaintEventArgs e)
        //{
        //    LabelControl lbl = sender as LabelControl;
        //    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        //    using (Brush b = new SolidBrush(lbl.BackColor))
        //    using (Pen p = new Pen(Color.Gray, 1))
        //    {
        //        int radius = 10;
        //        Rectangle rect = lbl.ClientRectangle;
        //        using (System.Drawing.Drawing2D.GraphicsPath path = RoundedRect(rect, radius))
        //        {
        //            e.Graphics.FillPath(b, path);
        //            e.Graphics.DrawPath(p, path);
        //        }
        //    }

        //    TextRenderer.DrawText(e.Graphics, lbl.Text, lbl.Font, lbl.ClientRectangle, lbl.ForeColor,
        //        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        //}

        //private System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        //{
        //    int diameter = radius * 2;
        //    var path = new System.Drawing.Drawing2D.GraphicsPath();
        //    path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
        //    path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
        //    path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
        //    path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
        //    path.CloseFigure();
        //    return path;
        //}

    }
    public class RoundedLabel : LabelControl
    {
        public int CornerRadius { get; set; } = 10;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Brush b = new SolidBrush(this.BackColor))
            using (Pen p = new Pen(Color.Gray, 1))
            {
                Rectangle rect = this.ClientRectangle;
                using (System.Drawing.Drawing2D.GraphicsPath path = RoundedRect(rect, CornerRadius))
                {
                    e.Graphics.FillPath(b, path);
                    e.Graphics.DrawPath(p, path);
                }
            }

            // Draw the text
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private System.Drawing.Drawing2D.GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
      
    }

}