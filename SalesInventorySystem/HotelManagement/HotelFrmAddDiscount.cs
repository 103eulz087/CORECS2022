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
    public partial class HotelFrmAddDiscount : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmAddDiscount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            authfrm.ShowDialog(this);
            if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            {
                Database.ExecuteQuery("INSERT INTO Discounts VALUES('" + lblbookingno.Text + "','" + lblroomno.Text + "','" + lblguestid.Text + "','" + txtRemarks.Text + "','" + txtamount.Text + "','" + DateTime.Now.ToString() + "','','" + Login.Fullname + "')", "Successfully Added!", Database.getCustomizeConnection());
                AuthorizedConfirmationFrm.isconfirmedLogin = false;
                authfrm.Dispose();
                this.Dispose();
            }
            else
            {
                return;
            }
           
        }
    }
}