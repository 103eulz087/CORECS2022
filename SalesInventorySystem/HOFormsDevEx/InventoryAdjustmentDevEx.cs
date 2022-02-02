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
    public partial class InventoryAdjustmentDevEx : DevExpress.XtraEditors.XtraForm
    {
        double newqty = 0.0, costkg = 0.0;
        public InventoryAdjustmentDevEx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE Inventory SET Available='" + txtnewqty.Text + "' WHERE SequenceNumber='" + txtrefnum.Text + "' ");
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
                string query = "sp_InvAdjustment";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode",txtbranch.Text);
                com.Parameters.AddWithValue("@parmprodcode",txtprodcode.Text);
                com.Parameters.AddWithValue("@parmdesc",txtitemname.Text);
                com.Parameters.AddWithValue("@parmqty",txtqty.Text);
                com.Parameters.AddWithValue("@parmcost",txtcostkg.Text);
                com.Parameters.AddWithValue("@parmqtyadj",txtqtyadj.Text);
                com.Parameters.AddWithValue("@parmnewqty",txtnewqty.Text);
                com.Parameters.AddWithValue("@parmseqrefnum",txtrefnum.Text);
                com.Parameters.AddWithValue("@parmstat",ViewGeneralInventory.status);
                //com.Parameters.AddWithValue("@parmstat", lblstatus.Text);
                com.Parameters.AddWithValue("@parmamountadjusted",costkg);
                com.Parameters.AddWithValue("@parmisvat", Convert.ToBoolean(ViewGeneralInventory.isvat));
                //com.Parameters.AddWithValue("@parmrefno",);
                com.Parameters.AddWithValue("@parmuser",Login.userid);
                com.Parameters.AddWithValue("@parmtype", cmbtype.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplier.Text);
                //Gain / Loss
                //Returned
                //Add / Deduct Supplier
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void txtqtyadj_EditValueChanged(object sender, EventArgs e)
        {

            // if(ViewGeneralInventory.status == "AddOtherIncome" || ViewGeneralInventory.status == "AddInTransit")
            if (ViewGeneralInventory.status == "AddOtherIncome" || ViewGeneralInventory.status == "AddInTransit" || ViewGeneralInventory.status == "AddAddtoSupplier")
            {
                newqty = Convert.ToDouble(txtqty.Text) + Convert.ToDouble(txtqtyadj.Text);
                costkg = Convert.ToDouble(txtcostkg.Text) * Convert.ToDouble(txtqtyadj.Text);
                txtnewqty.Text = newqty.ToString();
            }
            else if (ViewGeneralInventory.status == "DeductOtherExpense" || ViewGeneralInventory.status == "DeductInTransit" || ViewGeneralInventory.status== "DeductDeducttoSupplier")
            {
                newqty = Convert.ToDouble(txtqty.Text) - Convert.ToDouble(txtqtyadj.Text);
                costkg = Convert.ToDouble(txtcostkg.Text) * Convert.ToDouble(txtqtyadj.Text);
                txtnewqty.Text = newqty.ToString();
            }
        }

        private void InventoryAdjustmentDevEx_Load(object sender, EventArgs e)
        {
            populateSupplier();
            //cmbtype.Text = cmbtype.SelectedIndex = 0;
            //if(txtbranch.Text != "888") //cmbtype default GAINLOSS
            //{
            //    lbltype.Visible = false;
            //    lblsupplier.Visible = false;
            //    txtsupplier.Visible = false;
            //    cmbtype.Visible = false;
            //}
            //else
            //{
            //    lbltype.Visible = true;
            //    lblsupplier.Visible = true;
            //    txtsupplier.Visible = true;
            //    cmbtype.Visible = true;
            //}
            string mark = lblstatus.Text;
            if (ViewGeneralInventory.status == "AddAddtoSupplier" || ViewGeneralInventory.status == "DeductDeducttoSupplier" )
            {
                //lbltype.Visible = true;
                lblsupplier.Visible = true;
                txtsupplier.Visible = true;
                //cmbtype.Visible = true;
            }
        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbtype.Text == "Add/DeductSupplier")
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

        void populateSupplier()
        {
            Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", txtsupplier, "SupplierID", "SupplierID");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}