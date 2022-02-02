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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventoryCostEdit : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public InventoryCostEdit()
        {
            InitializeComponent();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtcost.Text))
            {
                XtraMessageBox.Show("Cost must not Empty");
            }
            else
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Update this Product?..","Confirm Update");
                if(confirm)
                {
                    Database.ExecuteQuery("UPDATE InventoryCost set CostKg='" + txtcost.Text + "' " +
                      "WHERE SupplierID='" + txtsuppkey.Text + "' " +
                      "AND ProductCode='" + txtprodcode.Text + "' ", "Successfully Updated!...");
                    isdone = true;
                    this.Close();
                }
                else
                {
                    return;
                }
               
            }
        }
    }
}