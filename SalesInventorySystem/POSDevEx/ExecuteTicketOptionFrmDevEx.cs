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

namespace SalesInventorySystem.POSDevEx
{
    public partial class ExecuteTicketOptionFrmDevEx : DevExpress.XtraEditors.XtraForm
    {
        public bool isdone = false;
        public string option = "";
        public ExecuteTicketOptionFrmDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(tickettype.Text == "")
            {
                XtraMessageBox.Show("Please Select Options");
                return;
            }
            else
            {
                option = tickettype.Text;
                isdone = true;
            }
            this.Close();
        }
    }
}