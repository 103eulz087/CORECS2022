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

        private void insert()
        {
            try
            {
                DataTable dtDetails = BuildPODetailsTable();

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
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        //void insert()
        //{
        //    try
        //    {
        //        GridView view = gridControl1.FocusedView as GridView;
        //        view.SortInfo.Clear();

        //        int[] selectedRows = gridView1.GetSelectedRows();

        //        if (radProducts.Checked == true)
        //        {
        //            foreach (int rowHandle in selectedRows)
        //            {

        //                string productcode = gridView1.GetRowCellValue(rowHandle, "ProductCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
        //                string quantity = gridView1.GetRowCellValue(rowHandle, "Quantity").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //                string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //                string units = gridView1.GetRowCellValue(rowHandle, "Units").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();

        //                if (rowHandle >= 0)
        //                {

        //                    Database.ExecuteQuery("INSERT INTO PODETAILS (ShipmentNo" +
        //                            ",SupplierID" +
        //                            ",OrderType" +
        //                            ",OrderCode" +
        //                            ",Quantity" +
        //                            ",Cost" +
        //                            ",TotalCost" +
        //                            ",Unit" +
        //                            ",ActualQuantity" +
        //                            ",ActualCost" +
        //                            ",ActualTotalCost" +
        //                            ",isVat) " +
        //                        "VALUES ('" + txtshipmentno.Text + "'" +
        //                            ",'" + suppkey + "'" +
        //                            ",'P'" +
        //                            ",'" + productcode + "'" +
        //                            ",'" + quantity + "'" +
        //                            ",'" + cost + "'" +
        //                            ",'0'" +
        //                            ",'" + units + "'" +
        //                            ",'0'" +
        //                            ",'0'" +
        //                            ",'0'" +
        //                            ",'1') ");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            foreach (int rowHandle in selectedRows)
        //            {

        //                string servicecode = gridView1.GetRowCellValue(rowHandle, "ServiceCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
        //                string quantity = gridView1.GetRowCellValue(rowHandle, "Quantity").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
        //                string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();

        //                if (rowHandle >= 0)
        //                {

        //                    Database.ExecuteQuery("INSERT INTO PODETAILS (ShipmentNo" +
        //                            ",SupplierID" +
        //                            ",OrderType" +
        //                            ",OrderCode" +
        //                            ",Quantity" +
        //                            ",Cost" +
        //                            ",TotalCost" +
        //                            ",Unit" +
        //                            ",ActualQuantity" +
        //                            ",ActualCost" +
        //                            ",ActualTotalCost" +
        //                            ",isVat) " +
        //                        "VALUES ('" + txtshipmentno.Text + "'" +
        //                            ",'" + suppkey + "'" +
        //                            ",'S'" +
        //                            ",'" + servicecode + "'" +
        //                            ",'" + quantity + "'" +
        //                            ",'" + cost + "'" +
        //                            ",'0'" +
        //                            ",' '" +
        //                            ",'0'" +
        //                            ",'0'" +
        //                            ",'0'" +
        //                            ",'1') ");
        //                }
        //            }
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

    }
}