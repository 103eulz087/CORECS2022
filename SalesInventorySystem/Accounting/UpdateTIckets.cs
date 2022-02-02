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

namespace SalesInventorySystem.Accounting
{
    public partial class UpdateTIckets : DevExpress.XtraEditors.XtraForm
    {
        public UpdateTIckets()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void gridControlDetails_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlDetails, e.Location);
        }

        private void editValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.UpdateTicketValue upval = new UpdateTicketValue();
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", upval.txtdebitacctcode, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", upval.txtcreditacctcode, "AccountCode", "AccountCode");

            //string debitacctcode = Database.getSingleQuery("TicketDetails");
            //upval.txtdebitacctcode.Text = gridViewDetails.GetRowCellValue(gridViewDetails.FocusedRowHandle, "AccountCode").ToString();
            //upval.txtcreditacctcode.Text = gridViewDetails.GetRowCellValue(gridViewDetails.FocusedRowHandle, "AccountCode").ToString();
            //upval.txtdebitvalue.Text = gridViewDetails.GetRowCellValue(gridViewDetails.FocusedRowHandle, "Debit").ToString();
            //upval.txtcreditvalue.Text = gridViewDetails.GetRowCellValue(gridViewDetails.FocusedRowHandle, "Credit").ToString();
        }

        private void UpdateTIckets_Load(object sender, EventArgs e)
        {
            Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditdiscountglcode, "AccountCode", "AccountCode");
        }

        private void gridViewDetails_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "AccountCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditdiscountglcode;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
          
            double totalamountdebit = 0.0,totalamountcrdit=0.0;
            for (int k = 0; k <= gridViewDetails.RowCount - 1; k++)
            {

                string deb = gridViewDetails.GetRowCellValue(k, "Debit").ToString();
                string crd = gridViewDetails.GetRowCellValue(k, "Credit").ToString();
              
                totalamountdebit += Convert.ToDouble(deb);
                totalamountcrdit += Convert.ToDouble(crd);
            }
            if(totalamountcrdit != totalamountdebit)
            {
                XtraMessageBox.Show("Debit and Credit Value must equal");
                return;
            }
            else
            {
                for (int i = 0; i <= gridViewDetails.RowCount - 1; i++)
                {

                    string deb = gridViewDetails.GetRowCellValue(i, "Debit").ToString();
                    string crd = gridViewDetails.GetRowCellValue(i, "Credit").ToString();
                    string acct = gridViewDetails.GetRowCellValue(i, "AccountCode").ToString();
                    string tdate = Convert.ToDateTime(gridViewDetails.GetRowCellValue(i, "TicketDate").ToString()).ToShortDateString();
                    string refkey = gridViewDetails.GetRowCellValue(i, "ReferenceKey").ToString();
                    string brcode = gridViewDetails.GetRowCellValue(i, "BranchCode").ToString();
                    string tnum = gridViewDetails.GetRowCellValue(i, "TicketNumber").ToString();

                    totalamountdebit += Convert.ToDouble(deb);
                    totalamountcrdit += Convert.ToDouble(crd);
                    //string cttt = gridViewDetails.GetRowCellValue(i, "ctr").ToString();
                    Database.ExecuteQuery("UPDATE TicketDetails Set Debit='" + deb + "',Credit='" + crd + "',AccountCode='" + acct + "' WHERE TicketDate='" + tdate + "' AND BranchCode='" + brcode + "' AND TicketNumber='" + tnum + "' AND ReferenceKey='" + refkey + "' and Debit='" + gridViewDetails.GetRowCellValue(i, "OrigDebit").ToString() + "' AND Credit='" + gridViewDetails.GetRowCellValue(i, "OrigCredit").ToString() + "' and AccountCode='" + gridViewDetails.GetRowCellValue(i, "OrigAcctCode").ToString() + "' ");
                    //Database.ExecuteQuery("UPDATE viewtest Set Credit='" + gridViewDetails.GetRowCellValue(i, "Credit").ToString() + "',AccountCode='" + gridViewDetails.GetRowCellValue(i, "AccountCode").ToString() + "' WHERE TicketDate='" + gridViewDetails.GetRowCellValue(i, "TicketDate").ToString() + "' AND BranchCode='" + gridViewDetails.GetRowCellValue(i, "BranchCode").ToString() + "' AND TicketNumber='" + gridViewDetails.GetRowCellValue(i, "TicketNumber").ToString() + "' AND ReferenceKey='" + gridViewDetails.GetRowCellValue(i, "ReferenceKey").ToString() + "' and ctr='" + gridViewDetails.GetRowCellValue(i, "ctr").ToString() + "' ");
                }
                XtraMessageBox.Show("Successfully Updated");
            }
            
          
        }

        private void gridViewDetails_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Column.FieldName == "Debit")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            if (e.Column.FieldName == "AccountCode")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            if (e.Column.FieldName == "Credit")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
        }

        private void gridViewDetails_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Debit" || view.FocusedColumn.FieldName != "Credit" || view.FocusedColumn.FieldName != "AccountCode")
                e.Cancel = false;
        }
    }
}