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
            string sql = @"
        SELECT d.SequenceNo, d.Barcode, d.SequenceInventoryNumber, d.Status, d.ErrorMessage, d.CreatedAt
        FROM dbo.TransferBatchDetail d
        WHERE d.BatchNo = @BatchNo
        ORDER BY d.SequenceNo";

            using (var con = Database.getConnection())
            using (var da = new SqlDataAdapter(sql, con))
            {
                da.SelectCommand.Parameters.Add("@BatchNo", SqlDbType.Int).Value = int.Parse(txtbatchno.Text);
                var dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
        }

        private void StageBarcode()
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
                }
                catch (SqlException ex)
                {
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

            var seq = Convert.ToInt64(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber"));
            var batchNo = int.Parse(txtbatchno.Text);

            string sql = @"DELETE FROM dbo.TransferBatchDetail WHERE BatchNo = @BatchNo AND SequenceInventoryNumber = @Seq AND Status = 'Pending'";

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

        private void btnprint_Click(object sender, EventArgs e)
        {

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