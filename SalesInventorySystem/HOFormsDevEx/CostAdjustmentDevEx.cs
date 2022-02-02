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
    public partial class CostAdjustmentDevEx : DevExpress.XtraEditors.XtraForm
    {
        public CostAdjustmentDevEx()
        {
            InitializeComponent();
        }

        private void txtorderno_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(isfound())
                {
                    populateProducts();
                    //txtsupplier.Text = getSupplier();
                    //if(!isQtyComplete())
                    //{
                    //    XtraMessageBox.Show("This Inventory is already have a movement!.. Not Advisable to Adjust the Cost..");
                    //    return;
                    //}
                }
                else
                {
                    XtraMessageBox.Show("Order No not found");
                }
            }
        }

        bool isfound()
        {
            bool ok = false;
            if (Database.checkifExist("select ShipmentNo from ShipmentOrder where ShipmentNo='" + txtorderno.Text + "'") == true)
            {
                ok = true;
            }
            else
            {
                ok = false;
            }
            return ok;
        }

        void populateProducts()
        {
            Database.displaySearchlookupEdit("Select distinct Description FROM Inventory WHERE ShipmentNo='" + txtorderno.Text + "'", txtproduct, "Description", "Description");
        }

        String getSupplier()
        {
            string str = "";
            str= Database.getSingleQuery("ShipmentOrder", "ShipmentNo='" + txtorderno.Text + "'", "SupplierName");
            return str;
        }

        String getSupplierID()
        {
            string str = "";
            str = Database.getSingleQuery("ShipmentOrder", "ShipmentNo='" + txtorderno.Text + "'", "SupplierID");
            return str;
        }

        String getProductCode()
        {
            string str = "";
            str = Database.getSingleQuery("Products", "Description='" + txtproduct.Text + "' And BranchCode='"+Login.assignedBranch+"'", "ProductCode");
            return str;
        }

        Double getTotalQuantity()
        {
            string prodcatcode = Classes.Product.getProductCategoryCode(txtproduct.Text);
            double total = 0.0;
            total = Database.getTotalSummation2("Inventory", "ShipmentNo='" + txtorderno.Text + "' AND Product='" + getProductCode() + "' and IsStock=1 and Available > 0", "Quantity");
            return total;
        }

        Double getTotalAvailable()
        {
            string prodcatcode = Classes.Product.getProductCategoryCode(txtproduct.Text);
            double total = 0.0;
            total = Database.getTotalSummation2("Inventory", "ShipmentNo='" + txtorderno.Text + "' AND Product='" + getProductCode() + "' and IsStock=1 and Available > 0", "Available");
            return total;
        }

        private void txtproduct_EditValueChanged(object sender, EventArgs e)
        {
            double totalqty = 0.0,totalcost=0.0;
            string costkg = "";
            string prodcatcode = Classes.Product.getProductCategoryCode(txtproduct.Text);
            totalqty = getTotalAvailable();
            costkg = Database.getSingleQuery("Inventory", "ShipmentNo='" + txtorderno.Text + "' AND Product='" + getProductCode() + "' ", "Cost");
            totalcost = totalqty * Convert.ToDouble(costkg);
            txttotalqty.Text = totalqty.ToString();
            txtcostperqty.Text = costkg;
            txttotalcost.Text = totalcost.ToString();
        }

        private void txtcostadj_EditValueChanged(object sender, EventArgs e)
        {
            double total = 0.0;
            total = getTotalAvailable() * Convert.ToDouble(txtcostadj.Text);
            txtnewcostvalue.Text = total.ToString();
        }

        bool isQtyComplete()
        {
            bool ok = false;
            double qty = getTotalQuantity();
            double available = getTotalAvailable();
            if(qty == available)
            {
                ok = true;
            }
            else
            {
                ok = false;
            }
            return ok;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        String getInventoryBranch()
        {
            string str = "";
            str = Database.getSingleQuery("Inventory", "ShipmentNo='" + txtorderno.Text + "'", "Branch");
            return str;
        }

        String isVatable()
        {
            string str = "0";
            str = Database.getSingleQuery("Inventory", "ShipmentNo = '" + txtorderno.Text + "' AND Description='" + txtproduct.Text + "'", "IsVat");
            return str;
        }

        void save()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                double oldcostvalue = 0.0;
                oldcostvalue = Convert.ToDouble(txtcostperqty.Text) * getTotalAvailable();
                string prodcatcode = Classes.Product.getProductCategoryCode(txtproduct.Text);
                string query = "sp_InvCostAdjustment";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", getInventoryBranch());
                com.Parameters.AddWithValue("@parmprodcode", getProductCode());
                com.Parameters.AddWithValue("@parmdesc", txtproduct.Text);
                com.Parameters.AddWithValue("@parmqty", getTotalAvailable().ToString());
                com.Parameters.AddWithValue("@parmcost", txtcostperqty.Text);
                com.Parameters.AddWithValue("@parmcostadj", txtcostadj.Text);
                com.Parameters.AddWithValue("@parmoldcostvalue", oldcostvalue.ToString());
                com.Parameters.AddWithValue("@parmnewcostvalue", txtnewcostvalue.Text);
                com.Parameters.AddWithValue("@parmseqrefnum", txtorderno.Text); //shipmentno
                com.Parameters.AddWithValue("@parmstat", "FULL");
                com.Parameters.AddWithValue("@parmisvat", isVatable());
                //com.Parameters.AddWithValue("@parmrefno",);
                com.Parameters.AddWithValue("@parmuser", Login.userid);
                com.Parameters.AddWithValue("@parmtype", cmbtype.Text);
                com.Parameters.AddWithValue("@parmsupplierid", getSupplierID());
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(cmbtype.Text == "Add / DeductSupplier")
            {
                if (!isQtyComplete())
                {
                    XtraMessageBox.Show("This Inventory is already have a movement and some items are already out of Stock!.. Not Advisable to Adjust the Cost... ");
                    return;
                }
                else
                {
                    save();
                    XtraMessageBox.Show("Successfully Adjusted");
                    this.Dispose();
                }
            }
            else
            {
                save();
                XtraMessageBox.Show("Successfully Adjusted");
                this.Dispose();
            }
            
        }

        private void CostAdjustmentDevEx_Load(object sender, EventArgs e)
        {

        }

        private void cmbtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbtype.Text == "Add/DeductSupplier")
            {
                lblsupplier.Visible = true;
                txtsupplier.Visible = true;
                txtsupplier.Text = getSupplier();
            }
            else
            {
                lblsupplier.Visible = false;
                txtsupplier.Visible = false;
            }
        }
    }
}