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
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.HOForms
{
    public partial class POSTransactions : DevExpress.XtraEditors.XtraForm
    {
        public static string refno;
        object branchcode;
        object branchcodedet;
        public POSTransactions()
        {
            InitializeComponent();
        }

         

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

      
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void POSTransactions_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranch, "BranchName", "BranchName");
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcodedetails, "BranchName", "BranchName");
        }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
           
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_batchTransactionSummary " +
                   "WHERE BranchCode='" + branchcode + "' " +
                   "AND CAST(TransDate as Date) >= '" + dateEdit1.Text + "' AND CAST(TransDate as Date) <= '" + dateEdit2.Text + "' ORDER BY ReferenceNo", gridControl3, gridView3);
        }

        private void btnextractdet_Click(object sender, EventArgs e)
        {
            gridControl4.BeginUpdate();
            gridView4.GroupSummary.Clear();
            gridView4.Columns.Clear();
            Database.display("SELECT * FROM view_detailTransactionHistory " +
                  "WHERE BranchCode='" + branchcodedet + "' " +
                  "AND CAST(DateOrder as date) >= '" + txtdatefromdet.Text+ "' AND CAST(DateOrder as date) <= '" + txtdatetodet.Text + "' ORDER BY ReferenceNo", gridControl4, gridView4);

            //GridView viewz = gridControl4.FocusedView as GridView;
            //viewz.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] 
            //{
            //    new GridColumnSortInfo(viewz.Columns["MachineUsed"],DevExpress.Data.ColumnSortOrder.Ascending),
            //    new GridColumnSortInfo(viewz.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            //},2);

            //viewz.ExpandAllGroups();

            Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView4, "BranchCode");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView4, "QtySold");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView4, "TotalAmount");
            gridControl4.EndUpdate();
        }

        private void txtbranch_EditValueChanged_1(object sender, EventArgs e)
        {
            branchcode = SearchLookUpClass.getSingleValue(txtbranch, "BranchCode");
        }

        private void txtbrcodedetails_EditValueChanged(object sender, EventArgs e)
        {
            branchcodedet = SearchLookUpClass.getSingleValue(txtbrcodedetails, "BranchCode");
        }
        void exporttoexcel(GridView view, string title)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {

                string filepath = "C:\\MyFiles\\SALESREPORT\\";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string filename = "SALESSUMMARY_" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridView3, filename);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = "SALESDETAILS_" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridView4, filename);
        }

        void sendMailNotification()
        {
            try
            {
                string subject = "POS Sales Report [" + Branch.getBranchName(Login.assignedBranch) + "]";
                string body = "";
                Classes.EmailSetup mailsetup = new Classes.EmailSetup();
                mailsetup.setupEmailParam(subject, body, false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void viewJournalTicketsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.CreditMemoHRIDevEx creddv = new HOFormsDevEx.CreditMemoHRIDevEx();
            Database.display($"SELECT * FROM vw_CreditMemoHRI WHERE ReferenceNo='{gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ReferenceNo").ToString()}' order by ReferenceNo,SequenceNumber", creddv.gridControl4, creddv.gridView4);
            creddv.txtpono.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ReferenceNo").ToString();
            creddv.ShowDialog(this);
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl3, e.Location);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Database.display($"SELECT * FROM vw_CreditMemoRep WHERE DateOrder BETWEEN '{txtcredmemodatefrom.Text}' AND '{txtcredmemodateto.Text}' order by ReferenceNo", gridControl1, gridView5);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(gridControl1, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.CreditMemoHRIDevEx credd = new HOFormsDevEx.CreditMemoHRIDevEx();
            Database.display($"SELECT * FROM vw_CreditMemoHRIReport WHERE ReferenceNo='{gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ReferenceNo").ToString()}' order by ReferenceNo,SequenceNumber", credd.gridControl4, credd.gridView4);
            credd.txtpono.Text = gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "ReferenceNo").ToString();
            credd.btnsubmit.Visible = false;
            credd.ShowDialog(this);
        }
    }
}