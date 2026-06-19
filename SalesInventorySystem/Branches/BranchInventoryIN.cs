using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SalesInventorySystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Branches
{
    public partial class BranchInventoryIN : Form
    {
        public BranchInventoryIN()
        {
            InitializeComponent();
        }

        private void BranchInventoryIN_Load(object sender, EventArgs e)
        {
            loadInvNum();
            display();
            ConfigureNewQtyEditor();
        }



    // Call this from your form load after grid is created/bound
        private void ConfigureNewQtyEditor()
        {
            var qtyEdit = new RepositoryItemTextEdit();

            // Numeric mask: allow decimals
            qtyEdit.Mask.MaskType = MaskType.Numeric;
            qtyEdit.Mask.EditMask = "n3";              // 3 decimals (change if needed)
            qtyEdit.Mask.UseMaskAsDisplayFormat = true;

            // Optional: prevent negative numbers
            qtyEdit.AllowNullInput = DefaultBoolean.False;

            // Assign editor to the column
            gridControlRcvd.RepositoryItems.Add(qtyEdit);
            gridViewRcvd.Columns["NewQty"].ColumnEdit = qtyEdit;
        }

        void loadInvNum()
        {
            txtid.Text = IDGenerator.getIDNumberSP("sp_GetInventoryINNumber", "InventoryID");
        }
        void display()
        {
            Database.display($"SELECT * FROM dbo.funcview_BranchInventoryIN('{Login.assignedBranch}') ORDER BY Category,ProductCode,Ending DESC", gridControlRcvd, gridViewRcvd);
            DevXGridViewSettings.ShowFooterCountTotal(gridViewRcvd, "BranchCode");
            DevXGridViewSettings.ShowFooterTotal(gridViewRcvd, "Ending");
            DevXGridViewSettings.ShowFooterTotal(gridViewRcvd, "NewQty");
            DevXGridViewSettings.ShowFooterTotal(gridViewRcvd, "Variance");
        }

        private void gridViewRcvd_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "NewQty")
                e.Cancel = true;
        }

        private void gridViewRcvd_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "NewQty")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }

           
            if (!gridViewRcvd.IsDataRow(e.RowHandle))
                return;

            object qtyObj = gridViewRcvd.GetRowCellValue(e.RowHandle, "NewQty");
            if (qtyObj == null || qtyObj == DBNull.Value)
                return;

            if (decimal.TryParse(qtyObj.ToString(), out decimal qty) && qty > 0)
            {
                e.Appearance.BackColor = Color.LightGoldenrodYellow;
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }
        private DataTable BuildPODetailsTable_ByQty()
        {
            // Ensure any in-place editor value is committed to the datasource
            gridViewRcvd.CloseEditor();
            gridViewRcvd.UpdateCurrentRow();

            DataTable dt = new DataTable();
            dt.Columns.Add("BatchID", typeof(string));
            dt.Columns.Add("BranchCode", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("ProductCode", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Ending", typeof(decimal));
            dt.Columns.Add("NewQty", typeof(decimal));
            dt.Columns.Add("Variance", typeof(decimal));


            // Loop through data rows (ignore group rows, filter rows, etc.)
            for (int i = 0; i < gridViewRcvd.DataRowCount; i++)
            {
                int rowHandle = gridViewRcvd.GetVisibleRowHandle(i);
                if (!gridViewRcvd.IsDataRow(rowHandle))
                    continue;

                // --- Read NewQty safely ---
                object newQtyObj = gridViewRcvd.GetRowCellValue(rowHandle, "NewQty");
                if (newQtyObj == null || newQtyObj == DBNull.Value)
                    continue;

                if (!decimal.TryParse(newQtyObj.ToString(), out decimal newQty))
                    continue;

                // ✅ MAIN BUSINESS RULE
                if (newQty <= 0m)
                    continue;

                // --- Read Ending safely ---
                decimal endingQty = 0m;
                object endingObj = gridViewRcvd.GetRowCellValue(rowHandle, "Ending");
                if (endingObj != null && endingObj != DBNull.Value)
                    decimal.TryParse(endingObj.ToString(), out endingQty);

                // ✅ Calculate variance (do NOT trust grid)
                decimal variance = newQty - endingQty;

                string productCode = Convert.ToString(
                    gridViewRcvd.GetRowCellValue(rowHandle, "ProductCode"));

                if (string.IsNullOrWhiteSpace(productCode))
                    continue;

                DataRow dr = dt.NewRow();
                dr["BatchID"] = txtid.Text;
                dr["BranchCode"] = Login.assignedBranch;
                dr["Category"] = Convert.ToString(gridViewRcvd.GetRowCellValue(rowHandle, "Category"));
                dr["ProductCode"] = productCode;
                dr["Description"] = Convert.ToString(gridViewRcvd.GetRowCellValue(rowHandle, "Description"));
                dr["Ending"] = endingQty;
                dr["NewQty"] = newQty;
                dr["Variance"] = variance;

                dt.Rows.Add(dr);
            }

            return dt;
        }
        //private void insert()
        //{
        //    try
        //    {
        //        DataTable dtDetails = BuildPODetailsTable_ByQty();

        //        if (dtDetails.Rows.Count == 0)
        //        {
        //            XtraMessageBox.Show("No items to insert. Please enter Quantity > 0.");
        //            return;
        //        }

        //        using (SqlConnection conn = Database.getConnection())
        //        using (SqlCommand cmd = new SqlCommand("sp_BulkInsertInventoryIN", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Items", dtDetails);
        //            tvpParam.SqlDbType = SqlDbType.Structured;
        //            tvpParam.TypeName = "dbo.InventoryINType";

        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message);
        //        throw; // VERY IMPORTANT
        //    }
        //}
        //void ExecuteSP()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    try
        //    {
        //        string query = "sp_UploadBranchInventoryIN";
        //        SqlCommand com = new SqlCommand(query, con);
        //        com.Parameters.AddWithValue("@parmbatchid", txtid.Text);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.CommandText = query;
        //        com.ExecuteNonQuery();
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //        throw; // VERY IMPORTANT
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //}

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dtDetails = BuildPODetailsTable_ByQty();

                if (dtDetails.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No items to insert. Please enter Quantity > 0.");
                    return;
                }

                bool ok;
                string msg;

                using (SqlConnection conn = Database.getConnection())
                using (SqlCommand cmd = new SqlCommand("dbo.sp_BranchInventoryINProcess", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var tvpParam = cmd.Parameters.AddWithValue("@Items", dtDetails);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.InventoryINType";

                    cmd.Parameters.Add("@EncodeBy", SqlDbType.VarChar, 50).Value = Login.isglobalUserID;

                    var pResult = new SqlParameter("@Result", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                    var pMsg = new SqlParameter("@ResultMessage", SqlDbType.NVarChar, 4000) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(pResult);
                    cmd.Parameters.Add(pMsg);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    ok = Convert.ToBoolean(pResult.Value);
                    msg = Convert.ToString(pMsg.Value);

                    if (!ok) throw new Exception(msg);
                }


                if (!ok)
                {
                    BigAlert.Show("Error: ", msg + "Operation failed.", MessageBoxIcon.Error);
                    return; // ✅ do NOT show success, do NOT close
                }

                BigAlert.Show("Success:", msg ?? "Successfully uploaded. Please check your Inventory.", MessageBoxIcon.Information);
                this.Dispose();
            }
            catch (Exception ex)
            {
                BigAlert.Show("Error: ", ex.Message , MessageBoxIcon.Error);
               
                // ✅ no success message, no dispose
            }

            //try
            //{
            //    insert();       // must THROW on error
            //    ExecuteSP();    // must THROW on error

            //    BigAlert.Show(
            //        "Success",
            //        "Successfully Uploaded. Please check your Inventory.",
            //        MessageBoxIcon.Information
            //    );

            //    this.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(
            //        $"Operation failed.\n\n{ex.Message}",
            //        "Error",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error
            //    );
            //}

        }

        private void gridViewRcvd_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if(e.Column.FieldName=="NewQty")
            {
                decimal variance;
                variance = Convert.ToDecimal(gridViewRcvd.GetRowCellValue(gridViewRcvd.FocusedRowHandle,"Ending")) - Convert.ToDecimal(gridViewRcvd.GetRowCellValue(gridViewRcvd.FocusedRowHandle, "NewQty"));
                gridViewRcvd.SetRowCellValue(gridViewRcvd.FocusedRowHandle, "Variance", variance.ToString());
            }
        }

        private void btnforapprovalstsexcel_Click(object sender, EventArgs e)
        {
            string filename = "BranchInventoryIN_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            HelperFunction.exporttoexcel(gridViewRcvd, filename);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text == "Preview")
            {
               
                simpleButton1.Text = "AddMore";
                gridViewRcvd.CloseEditor();
                gridViewRcvd.UpdateCurrentRow();

                // Hide rows where Quantity <= 0
                gridViewRcvd.ActiveFilter.Clear();
                gridViewRcvd.ActiveFilterString = "[NewQty] > 0";

            }
            else
            {
                gridViewRcvd.ActiveFilter.Clear();
                simpleButton1.Text = "Preview";
            }
        }
    }
}
