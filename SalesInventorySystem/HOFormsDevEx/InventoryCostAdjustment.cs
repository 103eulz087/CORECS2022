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
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventoryCostAdjustment : DevExpress.XtraEditors.XtraForm
    {
        bool islinktosupplier = false;
        object productcode, productcost, oldqty,supplierid;

        public InventoryCostAdjustment()
        {
            InitializeComponent();
        }

        private void txtshipmentno_EditValueChanged(object sender, EventArgs e)
        {
            if (islinktosupplier)
            {
                //Database.displaySearchlookupEdit("SELECT ProductCode,Description,CostKg,ActualQty,ActualCost FROM OrderDetails WHERE ShipmentNo='" + txtshipmentno.Text + "' And ActualQty <> 0", txtproduct, "Description", "Description");
                Database.displaySearchlookupEdit("SELECT Product as ProductCode,Description,SUM(Quantity) as ActualQty,Cost,SUM(Quantity)*Cost as TotalCost " +
                    "FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' GROUP BY Product,Description,Cost", txtproduct, "Description", "Description");
            }
        }

        private void txtproduct_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtproduct.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            productcode = view.GetRowCellValue(rowHandle, "ProductCode");
            oldqty = view.GetRowCellValue(rowHandle, "ActualQty");
            productcost = view.GetRowCellValue(rowHandle, "Cost");
            txtorigcost.Text = productcost.ToString();
            txtorigqty.Text = oldqty.ToString();
        }
        void save()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_InvCostAdjustment";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", "888");
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmprodcode", productcode.ToString()); 
                com.Parameters.AddWithValue("@parmoldqty", txtorigqty.Text);
                com.Parameters.AddWithValue("@parmoldcostkg", txtorigcost.Text);
                com.Parameters.AddWithValue("@parmnewcostvalue", txtnewcostvalue.Text);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplier.Text);
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
        private void button1_Click(object sender, EventArgs e)
        {
            save();
            XtraMessageBox.Show("Successfully Adjusted!");
            this.Dispose();
        }

        private void radlinktosupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (radlinktosupplier.Checked == true)
            {
                Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", txtsupplier, "SupplierID", "SupplierID");
                txtsupplier.Enabled = true;
                txtshipmentno.Enabled = true;
            }
           
        }

        private void txtsupplier_EditValueChanged(object sender, EventArgs e)
        {
            //GridView view = txtproduct.Properties.View;
            //int rowHandle = view.FocusedRowHandle;
            //supplierid = view.GetRowCellValue(rowHandle, "SupplierID");
            supplierid = SearchLookUpClass.getSingleValue(txtsupplier, "SupplierID");
            Database.displaySearchlookupEdit("SELECT ShipmentNo,BranchCode,Cast(DateOrder as date) as DateOrder FROM POSUMMARY WHERE SupplierID='"+ supplierid + "' ", txtshipmentno, "ShipmentNo", "ShipmentNo");
            islinktosupplier = true;
        }

        private void InventoryCostAdjustment_Load(object sender, EventArgs e)
        {

        }
    }
}