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
using DevExpress.XtraReports.UI;
using System.Media;
using System.IO;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class TransferByBarcode : DevExpress.XtraEditors.XtraForm
    {
        string source = "", destination = "";
        public TransferByBarcode()
        {
            InitializeComponent();
        }

        private void TransferByBarcode_Load(object sender, EventArgs e)
        {
            txtbatchno.Text = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber"); //IDGenerator.getTransferedNumber();
            txtbarcodeno.Focus();
        }

        private void btnadd_Click(object sender, EventArgs e) => StageBarcode();

        private void RefreshGrid()
        {
        //    string sql = @"
        //SELECT d.SequenceNo, d.Barcode, d.SequenceInventoryNumber, d.Status, d.ErrorMessage, d.CreatedAt
        //FROM dbo.TransferBatchDetail d
        //WHERE d.BatchNo = @BatchNo
        //ORDER BY d.SequenceNo";

            string sql = @"SELECT * FROM dbo.funcview_TransferInventoryByBarcode(@Batchno) order by SequenceNo DESC";

            using (var con = Database.getConnection())
            using (var da = new SqlDataAdapter(sql, con))
            {
                da.SelectCommand.Parameters.Add("@BatchNo", SqlDbType.Int).Value = int.Parse(txtbatchno.Text);
                var dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                

            }
        }
        public static async Task PlayNotificationSoundAsync(string soundFilePath)
        {
            // Use Task.Run to offload the sound loading and playback logic to a background thread.
            // This prevents the main UI thread from becoming unresponsive, even if the sound file
            // is large or there are delays in accessing it.
            await Task.Run(() =>
            {
                try
                {
                    // 1. Check if the specified sound file actually exists.
                    if (!File.Exists(soundFilePath))
                    {
                        Console.WriteLine($"Error: Sound file not found at '{soundFilePath}'. Please verify the path.");
                        // Optionally, you could play a default system sound here if the file is missing.
                        // SystemSounds.Exclamation.Play();
                        return; // Exit the method if the file is not found.
                    }

                    // 2. Create a new SoundPlayer instance with the provided file path.
                    // The 'using' statement ensures that the SoundPlayer object is properly
                    // disposed of after it's no longer needed, releasing system resources.
                    using (SoundPlayer player = new SoundPlayer(soundFilePath))
                    {
                        // 3. Load the sound into memory. This operation can be synchronous
                        // but since it's inside Task.Run, it won't block the main thread.
                        player.Load();

                        // 4. Play the sound. The Play() method plays the sound asynchronously
                        // on an internal thread managed by SoundPlayer, and returns immediately.
                        player.Play();

                        //Console.WriteLine($"Notification: Playing sound from '{soundFilePath}'");
                    }
                }
                // 5. Implement robust error handling for common issues.
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Error: The sound file was not found at '{soundFilePath}'. Double-check the file path and ensure it's accessible.");
                }
                catch (InvalidOperationException ex)
                {
                    // This typically occurs if the sound file is not a valid .wav format
                    // or if there's an issue with the audio device.
                    Console.WriteLine($"Error playing sound from '{soundFilePath}': {ex.Message}. Ensure the file is a valid .wav format and your audio device is working.");
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected errors during sound playback.
                    Console.WriteLine($"An unexpected error occurred while attempting to play sound from '{soundFilePath}': {ex.Message}");
                }
            });
        }
        private async void StageBarcode()
        {
            if (string.IsNullOrWhiteSpace(txtbarcodeno.Text))
            {
                XtraMessageBox.Show("Please scan or enter a barcode.");
                txtbarcodeno.Focus();
                return;
            }

            // Infer source/destination
            source = radtobigblue.Checked ? "Commissary" : "BigBlue";
            destination = radtobigblue.Checked ? "BigBlue" : "Commissary";

            using (var con = Database.getConnection()) // assumes your helper returns SqlConnection
            using (var cmd = new SqlCommand("dbo.sp_StageBarcodeForTransfer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BatchNo", SqlDbType.Int).Value = int.Parse(txtbatchno.Text);
                cmd.Parameters.Add("@Barcode", SqlDbType.VarChar, 100).Value = txtbarcodeno.Text.Trim();
                cmd.Parameters.Add("@Source", SqlDbType.VarChar, 20).Value = source;
                cmd.Parameters.Add("@Destination", SqlDbType.VarChar, 20).Value = destination;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 50).Value = Login.assignedBranch;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = Login.userid; // or your user id

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    RefreshGrid(); // reload staged items
                    gridView1.BestFitColumns();
                    gridView1.Columns["SequenceNo"].Summary.Clear();
                    gridView1.Columns["SequenceNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "SequenceNo", "{0}");
                    gridView1.Columns["Qty"].Summary.Clear();
                    gridView1.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0}");

                }
                catch (SqlException ex)
                {

                    await PlayNotificationSoundAsync(Application.StartupPath + "\\error.wav");
                    XtraMessageBox.Show(ex.Message, "Stage failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            txtbarcodeno.Text = "";
            txtbarcodeno.Focus();
        }

        private void btnsave_Click(object sender, EventArgs e) => CommitBatch();

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (gridView1.FocusedRowHandle < 0) return;

            var seq = Convert.ToInt64(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo"));
            var batchNo = int.Parse(txtbatchno.Text);

            string sql = @"DELETE FROM dbo.TransferBatchDetail WHERE BatchNo = @BatchNo AND SequenceNo = @Seq AND Status = 'Pending'";

            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@BatchNo", SqlDbType.Int).Value = batchNo;
                cmd.Parameters.Add("@Seq", SqlDbType.BigInt).Value = seq;
                con.Open();
                cmd.ExecuteNonQuery();
            }
            RefreshGrid();

        }

        private void txtbarcodeno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnadd.PerformClick();
        }
        void BigBlueTemplate()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption2");

            //DevExReportTemplate.CustomerRequest xct = new DevExReportTemplate.CustomerRequest();
            DevExReportTemplate.TransferInventory xct = new DevExReportTemplate.TransferInventory();

            xct.Landscape = false;
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);

            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtransfertype.Text = "Transfer to BigBlue";
            xct.xrdispatchno.Text = txtdispatchno.Text;

            //xct.xrdateneeded.Text = DateTime.Now.ToShortDateString();
            //xct.xrrequestedby.Text = Login.Fullname;
            //xct.xrdaterequest.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now);
            //xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);

            gridView1.Columns["SequenceNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["PalletNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["Status"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["Barcode"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["ShipmentNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["CreatedAt"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;

            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void CommissaryTemplate()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption2");

            DevExReportTemplate.StorageReceivingForm xct = new DevExReportTemplate.StorageReceivingForm();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
            //xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);

            gridView1.Columns["SequenceNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["PalletNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["Status"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["Barcode"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["ShipmentNo"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            gridView1.Columns["CreatedAt"].OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;

            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("No Data to print");
                return;
            }
            if (radtobigblue.Checked == true)
            {
                BigBlueTemplate();
            }
            else
            {
                CommissaryTemplate();
            }
        }

        private void CommitBatch()
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Nothing to save.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtdispatchno.Text))
            {
                XtraMessageBox.Show("Please provide Dispatch Number.");
                txtdispatchno.Focus();
                return;
            }

            // Ensure header/source/destination set
            source = radtobigblue.Checked ? "Commissary" : "BigBlue";
            destination = radtobigblue.Checked ? "BigBlue" : "Commissary";

            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("dbo.sp_CommitTransferBatch", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BatchNo", SqlDbType.Int).Value = int.Parse(txtbatchno.Text);
                cmd.Parameters.Add("@Source", SqlDbType.VarChar, 20).Value = source;
                cmd.Parameters.Add("@Destination", SqlDbType.VarChar, 20).Value = destination;
                cmd.Parameters.Add("@DispatchNo", SqlDbType.VarChar, 50).Value = txtdispatchno.Text.Trim();
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = Login.userid;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    XtraMessageBox.Show("Inventory successfully transferred.");
                    this.Dispose();
                }
                catch (SqlException ex)
                {
                    XtraMessageBox.Show(ex.Message, "Commit failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RefreshGrid(); // show which lines are error/processed
                }
            }
        }

       
    }
}