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
using System.Threading;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.POSDevEx
{
    public partial class ManipulateDataDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string brcode = "",machinename="",petsa="";
        public ManipulateDataDevEx()
        {
            InitializeComponent();
        }

        private void ManipulateDataDevEx_Load(object sender, EventArgs e)
        {
            double total = 0.0;
            for (int i = 0; i <= gridView3.RowCount - 1; i++)
            {
                total += Convert.ToDouble(gridView3.GetRowCellValue(i, "TotalAmount").ToString());
            }
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                double newtotalamount = 0.0, newqty = 0.0, diff = 0.0, total = 0.0;
                newtotalamount = Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SellingPrice").ToString()) * Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NewQty").ToString());
                newqty = Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NewTotalAmount").ToString()) / Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SellingPrice").ToString());
                diff = Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "TotalAmount").ToString()) - Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "NewTotalAmount").ToString());
                for (int i = 0; i <= gridView3.RowCount - 1; i++)
                {
                    total += Convert.ToDouble(gridView3.GetRowCellValue(i, "NewTotalAmount").ToString());
                }
                if (e.Column.FieldName == "NewTotalAmount")
                {
                    gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "NewQty", newqty.ToString());
                    gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "Difference", diff.ToString());
                    txtnewtotalamount.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + " Please Input Valid Fields (numeric).");
            }

            //try
            //{
            //    if (e.Column.FieldName != "NewTotalAmount" && e.Column.FieldName != "NewQty") return;

            //    decimal price = Convert.ToDecimal(gridView3.GetRowCellValue(e.RowHandle, "SellingPrice"), CultureInfo.InvariantCulture);
            //    if (price <= 0) return;

            //    decimal newQty, newTotal;

            //    if (e.Column.FieldName == "NewTotalAmount")
            //    {
            //        newTotal = Convert.ToDecimal(gridView3.GetRowCellValue(e.RowHandle, "NewTotalAmount"), CultureInfo.InvariantCulture);
            //        newQty = newTotal / price;
            //        gridView3.SetRowCellValue(e.RowHandle, "NewQty", newQty);
            //    }
            //    else
            //    {
            //        newQty = Convert.ToDecimal(gridView3.GetRowCellValue(e.RowHandle, "NewQty"), CultureInfo.InvariantCulture);
            //        newTotal = newQty * price;
            //        gridView3.SetRowCellValue(e.RowHandle, "NewTotalAmount", newTotal);
            //    }

            //    decimal oldTotal = Convert.ToDecimal(gridView3.GetRowCellValue(e.RowHandle, "TotalAmount"), CultureInfo.InvariantCulture);
            //    gridView3.SetRowCellValue(e.RowHandle, "Difference", oldTotal - newTotal);

            //    // Use grid summary instead of manual looping for total
            //    gridView3.UpdateSummary();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message + " Please input valid numeric fields.");
            //}


        }

        //private void btnanalyze_Click(object sender, EventArgs e)
        //{
        //    progressBar1.Maximum = 9;
        //    progressBar1.Step = 1;
        //    backgroundWorker1.RunWorkerAsync();
        //    backgroundWorker1.ReportProgress(1);
        //    Thread.Sleep(100);
        //}

        private CancellationTokenSource _cts;

        private async void btnanalyze_Click(object sender, EventArgs e)
        {

            btnanalyze.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;

            try
            {
                var tvp = BuildManipItemsTvpFromGrid();

                if (tvp.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No changes detected.");
                    return;
                }

                await ApplyManipulationAndRecalcAsync(tvp);

                XtraMessageBox.Show("Successfully Updated & Recalculated.");
                this.Dispose();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
                btnanalyze.Enabled = true;
            }

        }

        private DataTable BuildManipItemsTvpFromGrid()
        {
            var dt = new DataTable();
            dt.Columns.Add("BranchCode", typeof(string));
            dt.Columns.Add("MachineUsed", typeof(string));
            dt.Columns.Add("Petsa", typeof(DateTime));
            dt.Columns.Add("ReferenceNo", typeof(string));
            dt.Columns.Add("CashierTransNo", typeof(string));
            dt.Columns.Add("SequenceNumber", typeof(int));
            dt.Columns.Add("NewQty", typeof(decimal));
            dt.Columns.Add("NewTotalAmount", typeof(decimal));

            var petsaDate = Convert.ToDateTime(petsa); // ensure this is a valid date

            for (int i = 0; i < gridView3.RowCount; i++)
            {
                string referenceNo = Convert.ToString(gridView3.GetRowCellValue(i, "ReferenceNo"));
                string cashierTransNo = Convert.ToString(gridView3.GetRowCellValue(i, "CashierTransNo"));
                string machineUsed = Convert.ToString(gridView3.GetRowCellValue(i, "MachineUsed"));
                int seq = Convert.ToInt32(gridView3.GetRowCellValue(i, "SequenceNumber"));

                // Prefer decimals for money/qty to avoid float rounding
                decimal newQty = Convert.ToDecimal(gridView3.GetRowCellValue(i, "NewQty"), CultureInfo.InvariantCulture);
                decimal newTotal = Convert.ToDecimal(gridView3.GetRowCellValue(i, "NewTotalAmount"), CultureInfo.InvariantCulture);

                decimal oldQty = Convert.ToDecimal(gridView3.GetRowCellValue(i, "QtySold"), CultureInfo.InvariantCulture);
                decimal oldTotal = Convert.ToDecimal(gridView3.GetRowCellValue(i, "TotalAmount"), CultureInfo.InvariantCulture);

                // Only send changed rows (best practice; reduces SQL work)
                if (newQty == oldQty && newTotal == oldTotal) continue;

                dt.Rows.Add(brcode, machineUsed, petsaDate, referenceNo, cashierTransNo, seq, newQty, newTotal);
            }

            return dt;
        }

        private async Task ApplyManipulationAndRecalcAsync(DataTable tvp)
        {
            using (SqlConnection con = Database.getConnection())
            using (SqlCommand cmd = new SqlCommand("dbo.spman_apply_and_calculate", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("@brcode", SqlDbType.Char, 3).Value = brcode;
                cmd.Parameters.Add("@petsa", SqlDbType.Date).Value = Convert.ToDateTime(petsa);
                cmd.Parameters.Add("@machinename", SqlDbType.VarChar, 20).Value = machinename;

                var p = cmd.Parameters.AddWithValue("@Items", tvp);
                p.SqlDbType = SqlDbType.Structured;
                p.TypeName = "dbo.ManipItemType";

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }



        void execSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spman_calculate";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@brcode", brcode);
                com.Parameters.AddWithValue("@petsa", petsa);
                com.Parameters.AddWithValue("@machinename", machinename);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
                com.ExecuteNonQuery();

            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally { con.Close(); }
            
        }
        void updateManipulation()
        {
            string mark = brcode;
            try
            {
                for (int i = 0; i <= gridView3.RowCount - 1; i++)
                {
                    //Database.ExecuteQuery("UPDATE BatchSalesDetails2 SET isCosting=1, QtySold='" + gridView3.GetRowCellValue(i, "NewQty").ToString() + "',TotalAmount='" + gridView3.GetRowCellValue(i, "NewTotalAmount").ToString() + "' WHERE SequenceNumber='" + gridView3.GetRowCellValue(i, "SequenceNumber").ToString() + "' AND BranchCode='" + brcode + "' AND ReferenceNo='" + gridView3.GetRowCellValue(i, "ReferenceNo").ToString() + "' AND MachineUsed='" + gridView3.GetRowCellValue(i, "MachineUsed").ToString() + "'");
                    //Database.ExecuteQuery("UPDATE BatchSalesDetails2 SET isCosting=1, SubTotal='" + gridView3.GetRowCellValue(i, "NewTotalAmount").ToString() + "' WHERE SequenceNumber='" + gridView3.GetRowCellValue(i, "SequenceNumber").ToString() + "' AND BranchCode='" + brcode + "' AND ReferenceNo='" + gridView3.GetRowCellValue(i, "ReferenceNo").ToString() + "' AND MachineUsed='" + gridView3.GetRowCellValue(i, "MachineUsed").ToString() + "'");
                    if (Convert.ToDouble(gridView3.GetRowCellValue(i, "NewQty").ToString()) == 0)
                    {
                        Database.ExecuteQuery($"INSERT INTO dbo.ManipOR VALUES('{brcode}','{gridView3.GetRowCellValue(i, "ReferenceNo").ToString()}','{gridView3.GetRowCellValue(i, "CashierTransNo").ToString()}','{gridView3.GetRowCellValue(i, "MachineUsed").ToString()}','{gridView3.GetRowCellValue(i, "SequenceNumber").ToString()}','{gridView3.GetRowCellValue(i, "NewQty").ToString()}','{gridView3.GetRowCellValue(i, "NewTotalAmount").ToString()}')");
                    }
                }
                //Database.ExecuteQuery("UPDATE BatchSalesDetails2 SET isCosting=1, QtySold=0 ,TotalAmount=0 WHERE SequenceNumber='" + gridView3.GetRowCellValue(i, "SequenceNumber").ToString() + "' AND BranchCode='" + brcode + "' AND ReferenceNo='" + gridView3.GetRowCellValue(i, "ReferenceNo").ToString() + "' AND MachineUsed='" + gridView3.GetRowCellValue(i, "MachineUsed").ToString() + "'");
                //Database.ExecuteQuery("UPDATE BatchSalesDetails2 SET isCosting=1, SubTotal=0 WHERE SequenceNumber='" + gridView3.GetRowCellValue(i, "SequenceNumber").ToString() + "' AND BranchCode='" + brcode + "' AND ReferenceNo='" + gridView3.GetRowCellValue(i, "ReferenceNo").ToString() + "' AND MachineUsed='" + gridView3.GetRowCellValue(i, "MachineUsed").ToString() + "'");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            //XtraMessageBox.Show("Successfully Updated");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(2);
            Thread.Sleep(100);
            updateManipulation();
            backgroundWorker1.ReportProgress(5);
            Thread.Sleep(100);
            execSP();
            backgroundWorker1.ReportProgress(6);
            Thread.Sleep(100);
            backgroundWorker1.ReportProgress(9);
            Thread.Sleep(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            this.Text = e.ProgressPercentage.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            XtraMessageBox.Show("Successfully Updated");
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Database.display($"SELECT * FROM dbo.func_vatexmanip('{brcode}','{petsa}','{machinename}') ORDER BY TotalAmount DESC", gridControl2, gridView3);
            gridView3.BestFitColumns();
            gridView3.Columns["CategoryCode"].Visible = false;
            gridView3.Columns["QtySold"].Summary.Clear();
            gridView3.Columns["TotalAmount"].Summary.Clear();
            gridView3.Columns["QtySold"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "QtySold", "{0}");
            gridView3.Columns["TotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount", "{0}");
            gridView3.Columns["NewQty"].Summary.Clear();
            gridView3.Columns["NewTotalAmount"].Summary.Clear();
            gridView3.Columns["NewQty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NewQty", "{0}");
            gridView3.Columns["NewTotalAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NewTotalAmount", "{0}");
            gridView3.Columns["Difference"].Summary.Clear();
            gridView3.Columns["Difference"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Difference", "{0}");
            //string newqty = "";
            //string newtotal = "";

            //for (int i=0;i<=gridView3.RowCount-1;i++)
            //{
            //    newqty = gridView3.GetRowCellValue(i, "NewQty").ToString();
            //    newtotal = gridView3.GetRowCellValue(i, "NewTotalAmount").ToString();
            //    gridView3.SetRowCellValue(i, "NewQty", newqty);
            //    gridView3.SetRowCellValue(i, "NewTotalAmount", newtotal);
            //}
        }

        private void gridView3_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //if (e.Column.FieldName == "TotalItem")
            //{
            //    if (Convert.ToDouble(e.CellValue) == 1)
            //    {
            //        e.Appearance.ForeColor = Color.Red;
            //    }
            //}
           
        }

        private void gridView3_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (e.RowHandle >= 0)
            //{
            //    string totalitems = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TotalItems"]);
            //    string totalvatableitems = view.GetRowCellDisplayText(e.RowHandle, view.Columns["TotalVatableItems"]);
            //    if (totalitems == "1")
            //    {
            //        // e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //        e.Appearance.BackColor = Color.Salmon;
            //        e.Appearance.BackColor2 = Color.SeaShell;
            //    }
            //    if (totalitems != "1" && totalvatableitems == "0")
            //    {
            //        //   e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //        e.Appearance.BackColor = Color.LightCyan;
            //        e.Appearance.BackColor2 = Color.LightBlue;
            //    }
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btncalc_Click(object sender, EventArgs e)
        {
            double total = 0.0;
            int gridctr = 0;
            for(int i=0;i<=gridView3.RowCount-1;i++)
            {
                if(Convert.ToInt32(gridView3.GetRowCellValue(i,"Ctr").ToString()) > Convert.ToInt32(txtcttrto.Text))
                {
                    //gridctr = Convert.ToInt32(gridView3.GetRowCellValue(i, "Ctr").ToString());
                    //gridView3.SetRowCellValue(gridctr, "NewTotalAmount", 0);
                    total += Convert.ToDouble(gridView3.GetRowCellValue(i, "TotalAmount").ToString());
                }
                if (Convert.ToInt32(gridView3.GetRowCellValue(i,"Ctr").ToString()) <= Convert.ToInt32(txtcttrto.Text))
                {
                    gridctr = Convert.ToInt32(gridView3.GetRowCellValue(i, "Ctr").ToString());
                    gridView3.SetRowCellValue(i, "NewQty", 0);
                    gridView3.SetRowCellValue(i, "NewTotalAmount", 0);
                    gridView3.SetRowCellValue(i, "Difference", 0);
                    
                }
            }
            txtcalcres.Text = total.ToString();            
        }
    }
}