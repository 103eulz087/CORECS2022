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
    public partial class InventoryQtyAdjustmentDevEx : DevExpress.XtraEditors.XtraForm
    {
        double newqty = 0.0, costkg = 0.0;
        bool islinktosupplier = false;
        object productcode,productcost;
        string adjustmenttype = "", adjustmentmethod="";
        public InventoryQtyAdjustmentDevEx()
        {
            InitializeComponent();
        }

        private void InventoryQtyAdjustmentDevEx_Load(object sender, EventArgs e)
        {
            populate();
        }

        void populate()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");
        }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            //GridView view = txtbranch.Properties.View;
            //int rowHandle = view.FocusedRowHandle;
            ////string fieldName = "Name"; // or other field name
            //object branchcode = view.GetRowCellValue(rowHandle, "BranchCode");
            //txtseqno.Text = value.ToString();
            //txtweight.Text = valueAvailable.ToString();
            //txtweight.Focus();
            
        }

        private void radlinktosupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (radlinktosupplier.Checked==true)
            {
                adjustmentmethod = "LINKTOSUPPLIER";
                Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", txtsupplier, "SupplierID", "SupplierID");
                txtsupplier.Enabled = true;
                txtshipmentno.Enabled = true;
            }
            else
            {
                adjustmentmethod = "INTRANSIT";
                Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='888'", txtproduct, "Description", "Description");
                txtcostkg.Enabled = true;
                txtsupplier.Enabled = false;
                txtshipmentno.Enabled = false;
            }

        }

        private void txtsupplier_EditValueChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT ShipmentNo,BranchCode,TargetDate FROM POSUMMARY", txtshipmentno, "ShipmentNo", "ShipmentNo");
            //Database.displaySearchlookupEdit("SELECT ShipmentNo,Branch,OrderDate FROM ShipmentOrder", txtshipmentno, "ShipmentNo", "ShipmentNo");
            islinktosupplier = true;
        }

        private void txtshipmentno_EditValueChanged(object sender, EventArgs e)
        {
            if (islinktosupplier)
            {
                Database.displaySearchlookupEdit("SELECT distinct Product as ProductCode,Description,Cost FROM Inventory WHERE Branch='"+txtbranch.Text+"' and ShipmentNo='" + txtshipmentno.Text + "'", txtproduct, "Description", "Description");
            }
            else
            {
                Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='888'", txtproduct, "Description", "Description");
               
            }
        }

        private void txtqtyadj_EditValueChanged(object sender, EventArgs e)
        {
            if (radadd.Checked==true)
            {
                newqty = Convert.ToDouble(txtqty.Text) + Convert.ToDouble(txtqtyadj.Text);
                costkg = Convert.ToDouble(txtcostkg.Text) * Convert.ToDouble(txtqtyadj.Text);
                txtnewqty.Text = newqty.ToString();
            }
            else if (raddeduct.Checked==true)
            {
                newqty = Convert.ToDouble(txtqty.Text) - Convert.ToDouble(txtqtyadj.Text);
                costkg = Convert.ToDouble(txtcostkg.Text) * Convert.ToDouble(txtqtyadj.Text);
                txtnewqty.Text = newqty.ToString();
            }
        }

        private void radadd_CheckedChanged(object sender, EventArgs e)
        {
            if (radadd.Checked == true)
                adjustmenttype = "ADD";
            else
                adjustmenttype = "DEDUCT";
        }

        private void radintransit_CheckedChanged(object sender, EventArgs e)
        {
            if (radintransit.Checked == true)
            {
                adjustmentmethod = "INTRANSIT";
                Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='888'", txtproduct, "Description", "Description");
                txtcostkg.Enabled = true;
                txtsupplier.Enabled = false;
                txtshipmentno.Enabled = false;
            }
            else
            {
                adjustmentmethod = "LINKTOSUPPLIER";
                txtsupplier.Enabled = true;
                txtshipmentno.Enabled = true;
            }
               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double totalavailable = Database.getTotalSummation2("Inventory", "Product='" + productcode + "' and isStock=1 and Available > 0 and Branch='" + txtbranch.Text + "'", "Available");
            if(raddeduct.Checked==true)
            {
                if (totalavailable < Convert.ToDouble(txtqtyadj.Text))
                {
                    XtraMessageBox.Show("Cant Deduct Inventory.. Available Quantity must not less than Qty Adjustment!");
                    return;
                }
                else
                {
                    save();
                    XtraMessageBox.Show("Successfully Adjusted!");
                    this.Dispose();
                }
            }
            else
            {
                save();
                XtraMessageBox.Show("Successfully Adjusted!");
                this.Dispose();
            }
        }

        private void raddeduct_CheckedChanged(object sender, EventArgs e)
        {
            if (radadd.Checked == false)
                adjustmenttype = "DEDUCT";
            else
                adjustmenttype = "ADD";
        }

        private void txtproduct_EditValueChanged(object sender, EventArgs e)
        {
            //GridView view = txtproduct.Properties.View;
            //int rowHandle = view.FocusedRowHandle;
            //productcode = view.GetRowCellValue(rowHandle, "ProductCode");
            //productcost = view.GetRowCellValue(rowHandle, "Cost");
            productcode = SearchLookUpClass.getSingleValue(txtproduct, "ProductCode");
            productcost = SearchLookUpClass.getSingleValue(txtproduct, "Cost");
            txtcostkg.Text = productcost.ToString();
            double totalorigqty = Database.getTotalSummation2("Inventory", "Product='" + productcode + "' and isWarehouse=1 and Branch='" + txtbranch.Text + "' and ShipmentNo='"+txtshipmentno.Text+"'", "Quantity");
            double totalavailable = Database.getTotalSummation2("Inventory", "Product='"+productcode+"' and isStock=1 and Available > 0 and isWarehouse=1 and Branch='" + txtbranch.Text + "'  and ShipmentNo='" + txtshipmentno.Text + "'", "Available");
            txtqty.Text = totalavailable.ToString();
            txtorigqty.Text = totalorigqty.ToString();
        }

        void save()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_InvQtyAdjustment";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
                com.Parameters.AddWithValue("@parmprodcode", productcode.ToString());
                com.Parameters.AddWithValue("@parmdesc", txtproduct.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplier.Text);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmqty", txtqty.Text);
                com.Parameters.AddWithValue("@parmcost", txtcostkg.Text);
                com.Parameters.AddWithValue("@parmqtyadj", txtqtyadj.Text);
                com.Parameters.AddWithValue("@parmnewqty", txtnewqty.Text);
                com.Parameters.AddWithValue("@parmadjustmenttype", adjustmenttype);
                com.Parameters.AddWithValue("@parmadjustmentmethod", adjustmentmethod);
                com.Parameters.AddWithValue("@parmamountadjusted", costkg);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
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