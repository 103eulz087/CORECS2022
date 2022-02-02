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
    public partial class ClientSalesOrderDiscount : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ClientSalesOrderDiscount()
        {
            InitializeComponent();
        }

        private void ClientSalesOrderDiscount_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtpercentage;
            txtpercentage.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string prodcode = Database.getSingleQuery("Products", "BranchCode='888' and Description='" + txtdesc.Text.Trim() + "'", "ProductCode");
            bool ifexists = Database.checkifExist("Select TOP 1 PONumber FROM SalesOrderDiscounts WHERE PONumber='" + txtpono.Text + "' and ProductCode='" + prodcode + "' and SeqNo='" + txtseqno.Text + "'");
            if (ifexists == true)
            {
                bool confirm = HelperFunction.ConfirmDialog("The System found out that this item is already have a discount.. Are you sure you want to change/update discount Amount?", "Update Discount");
                if (confirm)
                {
                    Database.ExecuteQuery("Update SalesOrderDiscounts SET Percentage='"+txtpercentage.Text+"',DiscountAmount='"+txtdiscountamount.Text+"',UpdateBy='"+Login.isglobalUserID+"',DateUpdated='"+DateTime.Now.ToString()+"' WHERE SeqNo='"+txtseqno.Text+"' and PONumber='"+txtpono.Text+"' and ProductCode='"+prodcode+"' ", "Successfully Updated!");
                }
                else
                {
                    return;
                }
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO SalesOrderDiscounts VALUES ('" + txtseqno.Text + "','" + txtpono.Text + "','" + prodcode + "','" + txtpercentage.Text + "','"+txtdiscountamount.Text+"','" + Login.isglobalUserID + "','" + DateTime.Now.ToString() + "',' ',' ') ", "Successfully Added!");
                isdone = true;
                this.Close();
            }
        }

        private void txtpercentage_EditValueChanged(object sender, EventArgs e)
        {
            double percentage = 0.0,discamount=0.0;
            percentage = Convert.ToDouble(txtpercentage.Text)/100;
            discamount = Convert.ToDouble(txttotalamount.Text)*percentage;
            txtdiscountamount.Text = discamount.ToString();
        }
    }
}