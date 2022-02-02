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

namespace SalesInventorySystem
{
    public partial class ReInventoryInEditLine : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public ReInventoryInEditLine()
        {
            InitializeComponent();
        }

        private void txtqty1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtcost.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void txtcost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add();
            }
        }

        void add()
        {
            Database.ExecuteQuery("UPDATE TempInventoryIN SET Quantity='" + txtqty1.Text + "',Cost='" + txtcost.Text + "' WHERE SequenceNumber='" + ReInventoryIn.seqno + "'");
            isdone = true;
            this.Close();
        }

        private void ReInventoryInEditLine_Load(object sender, EventArgs e)
        {

        }
    }
}