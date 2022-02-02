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

namespace SalesInventorySystem.HOForms
{
    public partial class ChangePassword : DevExpress.XtraEditors.XtraForm
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtnewpass.Text) || String.IsNullOrEmpty(txtconfirmnewpass.Text))
            {
                XtraMessageBox.Show("Fields must not Empty");
                return;
            }
            else
            {
                if (txtconfirmnewpass.Text != txtnewpass.Text)
                {
                    XtraMessageBox.Show("Password Not Match!");
                    return;
                }
                update();
                this.Dispose();
            }
        }

        void update()
        {
            Database.ExecuteQuery("UPDATE Users SET Password='"+txtnewpass.Text+"' WHERE UserID='"+Login.isglobalUserID+"'","Successfully Updated!");
        }
    }
}