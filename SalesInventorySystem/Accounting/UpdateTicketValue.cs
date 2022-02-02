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

namespace SalesInventorySystem.Accounting
{
    public partial class UpdateTicketValue : DevExpress.XtraEditors.XtraForm
    {
        public UpdateTicketValue()
        {
            InitializeComponent();
        }

        private void UpdateTicketValue_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtdebitacctcode, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", txtcreditacctcode, "AccountCode", "AccountCode");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}