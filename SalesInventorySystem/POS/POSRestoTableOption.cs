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

namespace SalesInventorySystem.POS
{
    public partial class POSRestoTableOption : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false, iseditOrder = false;
        public static string existingor = "", status = "";
        public POSRestoTableOption()
        {
            InitializeComponent();
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            Database.displaySearchlookupEdit("Select TableNo FROM dbo.RestaurantTable WHERE TableStatus='Available'", searchLookUpEdit1, "TableNo", "TableNo");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string getLimit = Database.getSingleResultSet($"SELECT TOP(1) Limit FROM dbo.RestaurantTable WHERE TableNo='{POSRestoTables.buttonname}'");
            POSRestoTableLimit tablelimit = new POSRestoTableLimit();
            tablelimit.txttableno.Text = POSRestoTables.buttonname;
            tablelimit.txtlimitamount.Text = getLimit;
            tablelimit.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE BatchSalesSummary Set TableNo='" + searchLookUpEdit1.Text + "' WHERE TableNo='" + POSRestoTables.buttonname + "' and Status='Pending' and isFloat=1 And BranchCode='" + Login.assignedBranch + "' And MachineUsed='" + Environment.MachineName + "' ");
            Database.ExecuteQuery("UPDATE RestaurantTable set TableStatus='Available' WHERE TableNo='" + POSRestoTables.buttonname + "'");
            Database.ExecuteQuery("UPDATE RestaurantTable set TableStatus='Occupied' WHERE TableNo='" + searchLookUpEdit1.Text + "'");
            status = "Transfer";
            this.Dispose();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            existingor = Database.getSingleQuery("BatchSalesSummary", "TableNo='" + POSRestoTables.buttonname + "' And BranchCode='"+Login.assignedBranch+"' And MachineUsed='"+Environment.MachineName+ "' And isVoid=0 And Status<>'SOLD' AND CAST(TransDate as date)='" + DateTime.Now.ToShortDateString() + "' ", "ReferenceNo");
            iseditOrder = true;
            isdone = true;
            status = "AddOrder";
            this.Close();
        }
    }
}