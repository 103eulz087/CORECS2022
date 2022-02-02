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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.POSDevEx
{
    public partial class POSCreditCardMonitoring : DevExpress.XtraEditors.XtraForm
    {
        public POSCreditCardMonitoring()
        {
            InitializeComponent();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        void display()
        {
            if(rad1.Checked==true)
            {
                Database.display("SELECT * FROM POSCreditCardTransactions WHERE BranchCode='" + txtbranch.Text + "' AND DateAdded between '" + txtsalesdatefrom.Text + "' and '" + txtsalesdateto.Text + "'", gridControl1, gridView1);
            }
            else if(rad2.Checked==true)
            {
                Database.display("SELECT * FROM POSMerchantTransactions WHERE BranchCode='" + txtbranch.Text + "' AND CAST(DateAdded as date) between '" + txtsalesdatefrom.Text + "' and '" + txtsalesdateto.Text + "'", gridControl1, gridView1);
            }
        }
        private void clearedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rad1.Checked == true)
            {
                Database.ExecuteQuery("UPDATE POSCreditCardTransactions set isCleared=1,DateCleared='" + DateTime.Now.ToString() + "' " +
                    "WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'"
                    , "Successfully Updated!");
            }
            else if (rad2.Checked == true)
            {
                Database.ExecuteQuery("UPDATE POSMerchantTransactions set isCleared=1,DateCleared='" + DateTime.Now.ToString() + "' " +
                        "WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'"
                        , "Successfully Updated!");
            }
            display();
        }

        private void unclearedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rad1.Checked == true)
            {
                Database.ExecuteQuery("UPDATE POSCreditCardTransactions set isCleared=0,DateCleared='" + DateTime.Now.ToString() + "' " +
                    "WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'"
                    , "Successfully Updated!");
            }
            else if (rad2.Checked == true)
            {
                Database.ExecuteQuery("UPDATE POSMerchantTransactions set isCleared=0,DateCleared='" + DateTime.Now.ToString() + "' " +
                        "WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'"
                        , "Successfully Updated!");
            }
            display();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void POSCreditCardMonitoring_Load(object sender, EventArgs e)
        {
            populate();
        }

        void populate()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string status = view.GetRowCellDisplayText(e.RowHandle, view.Columns["isCleared"]);
                if (status == "Unchecked")
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                    e.Appearance.ForeColor = Color.Red;
                }
                else
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                    e.Appearance.ForeColor = Color.Blue;

                }
            }
        }
    }
}