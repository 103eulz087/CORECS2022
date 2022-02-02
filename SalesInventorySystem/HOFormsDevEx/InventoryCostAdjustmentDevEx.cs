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
    public partial class InventoryCostAdjustmentDevEx : DevExpress.XtraEditors.XtraForm
    {
        double newcostadj = 0.0;
        public InventoryCostAdjustmentDevEx()
        {
            InitializeComponent();
        }

        private void txtcostadj_EditValueChanged(object sender, EventArgs e)
        {
            //if (ViewGeneralInventory.status == "AdjustCost")
            //{
                newcostadj = Convert.ToDouble(txtqty.Text) * Convert.ToDouble(txtcostadj.Text);
                txtnewcostvalue.Text = newcostadj.ToString();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
            XtraMessageBox.Show("Successfully Adjusted");
            this.Dispose();
        }

        void save()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_InvCostAdjustment";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
                com.Parameters.AddWithValue("@parmprodcode", txtprodcode.Text);
                com.Parameters.AddWithValue("@parmdesc", txtitemname.Text);
                com.Parameters.AddWithValue("@parmqty", txtqty.Text);
                com.Parameters.AddWithValue("@parmcost", txtcostkg.Text);
                com.Parameters.AddWithValue("@parmcostadj",txtcostadj.Text);
                com.Parameters.AddWithValue("@parmoldcostvalue", txtoldcostvalue.Text);
                com.Parameters.AddWithValue("@parmnewcostvalue", txtnewcostvalue.Text);
                com.Parameters.AddWithValue("@parmseqrefnum", txtrefnum.Text);
                com.Parameters.AddWithValue("@parmstat", ViewGeneralInventory.status);
                com.Parameters.AddWithValue("@parmisvat", Convert.ToBoolean(ViewGeneralInventory.isvat));
                //com.Parameters.AddWithValue("@parmrefno",);
                com.Parameters.AddWithValue("@parmuser", Login.userid);
                com.Parameters.AddWithValue("@parmtype", cmbtype.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplier.Text);
                //Gain / Loss
                //Returned
                //Add / Deduct Supplier
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

        private void InventoryCostAdjustmentDevEx_Load(object sender, EventArgs e)
        {
            populateSupplier();
        }

        void populateSupplier()
        {
            Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", txtsupplier, "SupplierID", "SupplierID");
        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbtype.Text == "Add/DeductSupplier")
            {
                lblsupplier.Visible = true;
                txtsupplier.Visible = true;
                populateSupplier();
            }
            else
            {
                lblsupplier.Visible = false;
                txtsupplier.Visible = false;
            }
        }
    }
}