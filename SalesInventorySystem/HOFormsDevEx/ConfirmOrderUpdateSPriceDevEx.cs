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
    public partial class ConfirmOrderUpdateSPriceDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ConfirmOrderUpdateSPriceDevEx()
        {
            InitializeComponent();
        }

        private void ConfirmOrderUpdateSPriceDevEx_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtsprice;
            txtsprice.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(checkapplytoall.Checked==true)
            {
                Database.ExecuteQuery("UPDATE DeliveryDetails set SellingPrice='" + txtsprice.Text + "' WHERE PONumber='" + txtpono.Text + "' And ProductName='"+txtdesc.Text+"'", "Successfully Updated!");
            }
            else
            {
                Database.ExecuteQuery("UPDATE DeliveryDetails set SellingPrice='" + txtsprice.Text + "' WHERE SeqNo='" + txtseqno.Text + "' and PONumber='" + txtpono.Text + "'", "Successfully Updated!");
            }
            isdone = true;
            this.Close();
        }

        private void txtsprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                simpleButton1.PerformClick();
        }
    }
}