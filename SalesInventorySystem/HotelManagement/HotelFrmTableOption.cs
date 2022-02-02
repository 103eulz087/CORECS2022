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
    public partial class HotelFrmTableOption : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false, iseditOrder = false;
        public static string existingor = "",status="";
        public HotelFrmTableOption()
        {
            InitializeComponent();
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            Database.displaySearchlookupEdit("Select TableNo FROM RestaurantTable WHERE TableStatus='Available'", searchLookUpEdit1, "TableNo", "TableNo");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE OrderSummary Set TableNo='" + searchLookUpEdit1.Text + "' WHERE TableNo='" + HotelFrmTables.buttonname + "' and Status='Pending' and isFloat=1");
            Database.ExecuteQuery("UPDATE RestaurantTable set TableStatus='Available' WHERE TableNo='" + HotelFrmTables.buttonname + "'");
            Database.ExecuteQuery("UPDATE RestaurantTable set TableStatus='Occupied' WHERE TableNo='" + searchLookUpEdit1.Text + "'");
            status = "Transfer";
            this.Dispose();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            existingor = Database.getSingleQuery("OrderSummary", "TableNo='" + HotelFrmTables.buttonname + "'", "ReferenceNo");
            iseditOrder = true;
            isdone = true;
            status = "AddOrder";
            this.Close();
        }
    }
}