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
    public partial class ViewOnHoldTransaction : DevExpress.XtraEditors.XtraForm
    { 
        public static string refno="",machinename;
        public static bool isdone = false;
        public ViewOnHoldTransaction()
        {
            InitializeComponent();
        }

        private void ViewOnHoldTransaction_Load(object sender, EventArgs e)
        {
            //Database.display("SELECT ReferenceNo,OnHoldName AS CustomerName,TotalItem,SubTotal AS TotalAmount ,AdvancePayment,Transdate AS DateHold,PreparedBy as TransactedBy FROM BatchSalesSummary WHERE isHold='1' And isFloat='1'", gridControl1, gridView1);
        }

        private void ViewOnHoldTransaction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                gridView1.Focus();
            }
        }

        void selectItem()
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Restore this Transaction?", "Restore Transaction!");
            if (ok)
            {
                machinename = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MachineUsed").ToString();
                refno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNo").ToString();
                isdone = true;
                this.Close();
            }
            else
            {
                this.Dispose();
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectItem();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            selectItem();
            
        }
    }
}