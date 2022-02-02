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
    public partial class ViewRecoveredTransactionsFrm : DevExpress.XtraEditors.XtraForm
    {
        public static string refno="";
        public static bool isdone = false;
        public ViewRecoveredTransactionsFrm()
        {
            InitializeComponent();
        }

        private void ViewRecoveredTransactionsFrm_Load(object sender, EventArgs e)
        {
            //display();
        }

        void display()
        {
            Database.display("SELECT ReferenceNo" +
                                ",OnHoldName AS CustomerName" +
                                ",TotalItem" +
                                ",SubTotal" +
                                ",TotalAmount" +
                                ",AdvancePayment" +
                                ",Transdate AS DateHold" +
                                ",PreparedBy as TransactedBy " +
                                "FROM BatchSalesSummary " +
                                "WHERE isFloat='1' " +
                                "and isHold='1' " +
                                "and isVoid='0'", gridControl1, gridView1);
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Restore this Transaction?", "Restore Transaction!");
                if (ok)
                {
                    refno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNo").ToString();
                }
                isdone = true;
                this.Close();
            }
        }

        private void ViewRecoveredTransactionsFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                gridView1.Focus();
            }
        }
    }
}