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
    public partial class VIEWPODETAILSUpdateCost : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public VIEWPODETAILSUpdateCost()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtshipmentno.Text) || String.IsNullOrEmpty(txtsupplierid.Text) || String.IsNullOrEmpty(txtprodcode.Text))
            {
                XtraMessageBox.Show("There are empty fields..");
                return;
            }
            else
            {
                Decimal totalcost = 0;
                totalcost = Convert.ToDecimal(txtqty.Text) * Convert.ToDecimal(txtcost.Text);
                Database.ExecuteQuery($"UPDATE dbo.PODETAILS SET Cost='{txtcost.Text}',TotalCost='{totalcost}' " +
                    $"WHERE ShipmentNo='{txtshipmentno.Text}' " +
                    $"AND SupplierID='{txtsupplierid.Text}' " +
                    $"AND OrderCode='{txtprodcode.Text}'","Succesfully Updated");
                isdone = true;
                this.Close();
            }
        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void txtcost_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}