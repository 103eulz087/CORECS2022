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

namespace SalesInventorySystem.Reporting
{
    public partial class BranchInventoryTransfer : XtraForm
    {
        public BranchInventoryTransfer()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            extract();
        }
        void extract()
        {
            Database.display($"SELECT * FROM dbo.TransferBatch WHERE CreatedAt >= '{datefrom.Text}' and CreatedAt <= '{dateto.Text}' and Status='Committed' ORDER BY BatchNo DESC", gridControl1, gridView1);
        }
    }
}
