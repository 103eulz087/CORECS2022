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
    public partial class HotelFrmUpdateForCleaning : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public HotelFrmUpdateForCleaning()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtattendant.Text) || String.IsNullOrEmpty(txtstatus.Text) )
            {
                XtraMessageBox.Show("Please input all Fields!..");
            }
            else
            {
                Database.ExecuteQuery("UPDATE RoomCleanedReport SET Status='"+txtstatus.Text+"',CleanedBy='"+txtattendant.Text+"',Remarks='"+txtremarks.Text+"',DateCleaned='"+DateTime.Now.ToString()+"' WHERE RoomNumber='"+txtroomno.Text+"' AND Status='FOR CLEANING'","Successfully Updated!",Database.getCustomizeConnection());
                isdone = true;
                this.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            txtattendant.Text = "";
            txtstatus.Text = "";
            txtremarks.Text = "";
        }

        private void HotelFrmUpdateForCleaning_Load(object sender, EventArgs e)
        {

        }
    }
}