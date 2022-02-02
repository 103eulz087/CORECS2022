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

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmAddCharges : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmAddCharges()
        {
            InitializeComponent();
        }

        private void txtservices_EditValueChanged(object sender, EventArgs e)
        {
            string rate="";
            rate=Database.getSingleQuery("ChargesRate", "Charges='" + txtservices.Text + "'", "Rate",Database.getCustomizeConnection());
            txtrate.Text = rate;

        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            double totalamount = 0.0;
            totalamount = Convert.ToDouble(txtrate.Text) * Convert.ToDouble(txtqty.Text);
            txttotalamount.Text = totalamount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Object objrate=SearchLookUpClass.getSingleValue(txtservices, "Rate", Database.getCustomizeConnection());
            Database.ExecuteQuery("INSERT INTO Charges VALUES('"+lblbookingid.Text+"','" + lblroomno.Text + "','" + lblguestid.Text + "','" + txtservices.Text + "','" + txtqty.Text + "','" + objrate.ToString() + "','" + txttotalamount.Text + "','UNPAID','"+DateTime.Now.ToString()+"','','" + Login.Fullname + "')","Successfully Added!",Database.getCustomizeConnection());
            Database.ExecuteQuery("INSERT INTO ClientLedger VALUES ('"+lblguestid.Text+"','"+DateTime.Now.ToString()+"','888','"+txtservices.Text+ "','SRVC CHRG','" + DateTime.Now.ToString() + "','',0,'"+txttotalamount.Text+"',0,0,'','eulz','*','','"+txttotalamount.Text+"',0)","",Database.getCustomizeConnection());
            this.Dispose();
        }

        private void HotelFrmAddCharges_Load(object sender, EventArgs e)
        {

        }
    }
}