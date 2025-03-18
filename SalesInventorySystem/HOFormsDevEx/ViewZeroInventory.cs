using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewZeroInventory : Form
    {
        object brcode = null;
        public ViewZeroInventory()
        {
            InitializeComponent();
        }

        private void ViewZeroInventory_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName From Branches", searchLookUpEdit1, "BranchCode", "BranchCode");
        }


        void generate()
        {
            if(radzero.Checked==true)
            {
                Database.display($"Select * FROM vw_ZeroInv WHERE Available <= 0  and BranchCode='{brcode}' order by ProductCode,Available", gridControl1, gridView1);
            }
            else
            {
                Database.display($"Select * FROM vw_InvLessThanReorderLevel WHERE Available > 0 and Available < ReOrderLevel  and BranchCode='{brcode}' ", gridControl1, gridView1);
            }

        }

        private void btnForApprovalSalesOrder_Click(object sender, EventArgs e)
        {
            generate();
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            brcode = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "BranchCode");
        }

        private void btnforapprovalsalesorderexcel_Click(object sender, EventArgs e)
        {
            string filename = "Inventory_Monitoring" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            HelperFunction.exporttoexcel(gridView1, filename);
        }
    }
}
