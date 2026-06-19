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
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.Branches
{
    public partial class BranchGenInventoryUpdateCost : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public BranchGenInventoryUpdateCost()
        {
            InitializeComponent();
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtcost.Text))
            {
                BigAlert.Show("MUST NOT EMPTY", "Cost must not be empty", MessageBoxIcon.Warning);
            }
            else
            {
                Database.ExecuteQuery($"UPDATE dbo.Inventory SET Cost='{txtcost.Text}' WHERE SequenceNumber='{txtseqno.Text.Trim()}'");
                BigAlert.Show("SUCCESS", "Cost Successfully Updated!.", MessageBoxIcon.Information);
                isdone = true;
                this.Close();
            }
        }
    }
}