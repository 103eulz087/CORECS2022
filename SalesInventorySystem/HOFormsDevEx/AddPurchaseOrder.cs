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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AddPurchaseOrder : DevExpress.XtraEditors.XtraForm
    {
        object suppkey;

        string suppid = "";
        public AddPurchaseOrder()
        {
            InitializeComponent();
        }

        private void AddPurchaseOrder_Load(object sender, EventArgs e)
        {
            displaySupplier();
            displayBranch();
            //btnnew.Enabled = true;
            //if (HOForms.VIEWPO.action == "")
            //{

            //    btnnew.Enabled = true;
             
            //}
            //else
            //{
            //    txtbranch.Enabled = true;
            //    txtdate.Enabled = true;
            
            //    searchLookUpEdit1.Enabled = true;
                
            //    searchLookUpEdit1.Text = HOForms.VIEWPO.suppliername;
            //    displayBranch();
              
            //    btnnew.Visible = false;
                
            //}
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            suppkey = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "SupplierKey");
            if(radProducts.Checked==true)
                Database.display("SELECT * FROM func_viewPurchaseOrder('" + Login.assignedBranch + "','" + suppkey + "')", gridControl1, gridView1);
            else
                Database.display("SELECT * FROM func_viewServicesOrder('" + Login.assignedBranch + "','" + suppkey + "')", gridControl1, gridView1);
            //Database.display("SELECT a.ProductCode" +
            //       ",a.ProductName" +
            //       ",a.CostKg as Cost " +
            //       ",0 as Quantity " +
            //       ",' ' as Units " +
            //       ",b. " +
            //       "FROM InventoryCost as a " +
            //       "LEFT OUTER JOIN Inventory as b " +
            //       "ON a.ProductCode=b.Product " +
            //       "AND b.Branch='"+Login.assignedBranch+"' " +
            //       "WHERE a.SupplierID='" + suppkey + "'", gridControl1, gridView1);
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtshipmentno.Text = IDGenerator.getIDNumberSP("sp_GetShipmentNo", "ShipmentNo");

            displaySupplier();
            displayBranch();
            populateRepositoryMetrics();
            txtshipmentno.Enabled = true;
            txtbranch.Enabled = true;
            txtdate.Enabled = true;

            searchLookUpEdit1.Enabled = true;
            btnnew.Enabled = false;
         
        }
        void populateRepositoryMetrics()
        {
            Database.displayRepositoryComboBoxItems("SELECT * FROM Metrics", "Metrics", repoMetrics);
        }

        private DataTable BuildPODetailsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ShipmentNo", typeof(string));
            dt.Columns.Add("SupplierID", typeof(string));
            dt.Columns.Add("OrderType", typeof(string));
            dt.Columns.Add("OrderCode", typeof(string));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("Cost", typeof(decimal));
            dt.Columns.Add("TotalCost", typeof(decimal));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("ActualQuantity", typeof(decimal));
            dt.Columns.Add("ActualCost", typeof(decimal));
            dt.Columns.Add("ActualTotalCost", typeof(decimal));
            dt.Columns.Add("isVat", typeof(bool));

            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                DataRow dr = dt.NewRow();
                dr["ShipmentNo"] = txtshipmentno.Text;
                dr["SupplierID"] = suppkey;
                dr["OrderType"] = radProducts.Checked ? "P" : "S";
                dr["OrderCode"] = radProducts.Checked
                    ? gridView1.GetRowCellValue(rowHandle, "ProductCode").ToString()
                    : gridView1.GetRowCellValue(rowHandle, "ServiceCode").ToString();
                dr["Quantity"] = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Quantity"));
                dr["Cost"] = Convert.ToDecimal(gridView1.GetRowCellValue(rowHandle, "Cost"));
                dr["TotalCost"] = 0;
                dr["Unit"] = radProducts.Checked
                    ? gridView1.GetRowCellValue(rowHandle, "Units").ToString()
                    : " ";
                dr["ActualQuantity"] = 0;
                dr["ActualCost"] = 0;
                dr["ActualTotalCost"] = 0;
                dr["isVat"] = true;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        private DataTable BuildPODetailsTable_ByQty()
        {
            // Ensure any in-place editor value is committed to the datasource
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();

            DataTable dt = new DataTable();
            dt.Columns.Add("ShipmentNo", typeof(string));
            dt.Columns.Add("SupplierID", typeof(string));
            dt.Columns.Add("OrderType", typeof(string));
            dt.Columns.Add("OrderCode", typeof(string));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("Cost", typeof(decimal));
            dt.Columns.Add("TotalCost", typeof(decimal));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("ActualQuantity", typeof(decimal));
            dt.Columns.Add("ActualCost", typeof(decimal));
            dt.Columns.Add("ActualTotalCost", typeof(decimal));
            dt.Columns.Add("isVat", typeof(bool));

            bool isProduct = radProducts.Checked;

            // Loop through data rows (ignore group rows, filter rows, etc.)
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                int rowHandle = gridView1.GetVisibleRowHandle(i);
                if (!gridView1.IsDataRow(rowHandle)) continue;

                // Safely read Quantity
                object qtyObj = gridView1.GetRowCellValue(rowHandle, "Quantity");
                decimal qty = 0m;
                if (qtyObj != null && qtyObj != DBNull.Value)
                    decimal.TryParse(qtyObj.ToString(), out qty);

                if (qty <= 0m) continue; // <-- THE MAIN RULE

                // Safely read Cost
                object costObj = gridView1.GetRowCellValue(rowHandle, "Cost");
                decimal cost = 0m;
                if (costObj != null && costObj != DBNull.Value)
                    decimal.TryParse(costObj.ToString(), out cost);

                string orderCode = isProduct
                    ? Convert.ToString(gridView1.GetRowCellValue(rowHandle, "ProductCode"))
                    : Convert.ToString(gridView1.GetRowCellValue(rowHandle, "ServiceCode"));

                // Skip if no code
                if (string.IsNullOrWhiteSpace(orderCode))
                    continue;

                DataRow dr = dt.NewRow();
                dr["ShipmentNo"] = txtshipmentno.Text;
                dr["SupplierID"] = suppkey;
                dr["OrderType"] = isProduct ? "P" : "S";
                dr["OrderCode"] = orderCode;
                dr["Quantity"] = qty;
                dr["Cost"] = cost;

                // If you want to calculate TotalCost client-side:
                dr["TotalCost"] = qty * cost;  // or 0 if server computes

                dr["Unit"] = isProduct
                    ? Convert.ToString(gridView1.GetRowCellValue(rowHandle, "Units"))
                    : " ";

                dr["ActualQuantity"] = 0m;
                dr["ActualCost"] = 0m;
                dr["ActualTotalCost"] = 0m;
                dr["isVat"] = true; // you can map this from grid too if you have a column

                dt.Rows.Add(dr);
            }

            return dt;
        }
        private void insert()
        {
            try
            {
                DataTable dtDetails = BuildPODetailsTable_ByQty();

                if (dtDetails.Rows.Count == 0)
                {
                    XtraMessageBox.Show("No items to insert. Please enter Quantity > 0.");
                    return;
                }

                using (SqlConnection conn = Database.getConnection())
                using (SqlCommand cmd = new SqlCommand("sp_BulkInsertPODetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Items", dtDetails);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.PODetailsType";

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        //private void insert()
        //{
        //    try
        //    {
        //        DataTable dtDetails = BuildPODetailsTable();

        //        using (SqlConnection conn = Database.getConnection())
        //        using (SqlCommand cmd = new SqlCommand("sp_BulkInsertPODetails", conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Items", dtDetails);
        //            tvpParam.SqlDbType = SqlDbType.Structured;
        //            tvpParam.TypeName = "dbo.PODetailsType";

        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}
        
        void ExecuteSP(string anaction)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SP_ADDPOSUM";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                com.Parameters.AddWithValue("@parmtargetdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmorderedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmaction", anaction);
                com.Parameters.AddWithValue("@parmsuppid", suppkey);
                com.Parameters.AddWithValue("@parmordertype", "");
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Quantity" && view.FocusedColumn.FieldName != "Units")
                e.Cancel = true;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
            if (e.Column.FieldName == "Units")
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }

            if (!gridView1.IsDataRow(e.RowHandle))
                return;

            object qtyObj = gridView1.GetRowCellValue(e.RowHandle, "Quantity");
            if (qtyObj == null || qtyObj == DBNull.Value)
                return;

            if (decimal.TryParse(qtyObj.ToString(), out decimal qty) && qty > 0)
            {
                e.Appearance.BackColor = Color.LightGoldenrodYellow;
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }

        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Units")
                e.RepositoryItem = repoMetrics;
        }
        void save()
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to save this Purchase Order?", "Confirm Purchase Order");
            if (!confirm) return;

            insert();
            ExecuteSP("SAVE"); // Pass action explicitly
            XtraMessageBox.Show("PO Successfully Created!...");
        }
        
        void showSaveAndSubmitForm()
        {
            AddPurchaseOrderSubmit addposub = new AddPurchaseOrderSubmit();
            Database.display($"SELECT ShipmentNo,ProductCategory,OrderCode,Description,Quantity,Cost,TotalCost " +
                $"FROM view_PODetails WHERE ShipmentNo='{txtshipmentno.Text}'",gridControl1,gridView1);
            addposub.ShowDialog(this);

        }
     
        private void btnsave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtshipmentno.Text) || String.IsNullOrEmpty(txtdate.Text) || String.IsNullOrEmpty(searchLookUpEdit1.Text))
            {
                XtraMessageBox.Show("Fields must not Empty...");
                return;
            }
            else
            {
                save();
                this.Dispose();
            }
            
        }

        void displaySupplier()
        {
            Database.displaySearchlookupEdit("select SupplierKey,SupplierID,SupplierName FROM Supplier", searchLookUpEdit1, "SupplierName", "SupplierName");
        }
        void displayBranch()
        {
            Database.displaySearchlookupEdit("select BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");
        }
       
        void ExecuteSP()
        {
            string ordertype = "";
            if(radProducts.Checked==true)
            {
                ordertype = "P";
            }
            else
            {
                ordertype = "S";
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SP_ADDPOSUM";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                com.Parameters.AddWithValue("@parmtargetdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmorderedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmaction", "");
                com.Parameters.AddWithValue("@parmsuppid", suppid);
                com.Parameters.AddWithValue("@parmordertype", ordertype);
                //  com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                //  com.Parameters.AddWithValue("@parmcoshpy", txtcoshpy.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text == "Preview")
            {
                label3.Text = "Add More Items";
                simpleButton1.Text = "AddMore";
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();

                // Hide rows where Quantity <= 0
                gridView1.ActiveFilter.Clear();
                gridView1.ActiveFilterString = "[Quantity] > 0";

            }
            else
            {
                gridView1.ActiveFilter.Clear();
                simpleButton1.Text = "Preview";
                label3.Text = "PREVIEW MODE – Only Quantity > 0 shown";
            }
           

        }
    }
}