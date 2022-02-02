using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class SetBatchCodeFrm : Form
    {
        public static string batchcode = "";
        public SetBatchCodeFrm()
        {
            InitializeComponent();
        }
 
        void submit()
        {
            string shipno = Database.getSingleQuery("Inventory", "BatchCode='" + txtbatchcode.Text + "' and ShipmentNo <> ''", "ShipmentNo");
            if (txtbatchcode.Text == "0" || txtbatchcode.Text == "")
            {
                XtraMessageBox.Show("Invalid BatchCode Number!");
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure that you are going to process this BatchCode? ", "BatchCode Process");
                if (ok)
                {
                    batchcode = txtbatchcode.Text;
                    AddPrimalCutInventory addprim = new AddPrimalCutInventory();
                    addprim.Show();
                    addprim.txtshipmentno.Text = shipno;
                    this.Close();
                }
                else
                {
                    return;
                }

            }
        }


        private void btnsubmit_Click(object sender, EventArgs e)
        {
            submit();
        }

        private void txtbatchcode_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void txtbatchcode_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsubmit.PerformClick();
            }
        }
    }
}
