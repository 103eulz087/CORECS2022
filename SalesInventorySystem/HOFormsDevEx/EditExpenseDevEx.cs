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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class EditExpenseDevEx : DevExpress.XtraEditors.XtraForm
    {
         
        object suppid;
        public EditExpenseDevEx()
        {
            InitializeComponent();
        }

        private void EditExpenseDevEx_Load(object sender, EventArgs e)
        {
            displayvendor();
            loadRepositoryItem();
        }
        //void populateBranches2()
        //{
        //    Database.displayDevComboBoxItems("SELECT BranchCode FROM Branches", "BranchCode", txtbrcodesum);
        //}
        void displayvendor()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", txtvendor, "SupplierName", "SupplierName");
        }
        void loadRepositoryItem()
        {
            Database.displayRepositorySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", repbrcode, "BranchCode", "BranchCode");
            Database.displayRepositorySearchlookupEdit("SELECT ExpenseID,ExpenseName FROM ExpensesList", reptypeofexpense, "ExpenseName", "ExpenseName");
            //gridView2.BestFitColumns();
            gridView3.BestFitColumns();
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //Database.display($"Select ReferenceNumber,InvoiceNo,ExpenseName,Remarks,Amount " +
            //      $"FROM dbo.ExpenseDetails " +
            //      $"WHERE ReferenceNumber='{editxpns.txtrefno.Text}' " +
            //      $"AND InvoiceNo='{editxpns.txtinvoiceno.Text}'", editxpns.gridControl1, editxpns.gridView1);

            if (e.Column.FieldName == "ExpenseName")
                e.RepositoryItem = reptypeofexpense;
            if (e.Column.FieldName == "Remarks")
                e.RepositoryItem = repparticulars;
            if (e.Column.FieldName == "Amount")
                e.RepositoryItem = repamount;
        }

        private void btnsummary_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to update this Expense Summary", "Update Expense Summary");
            if (confirm)
            {
                Database.ExecuteQuery($"Update dbo.ExpenseSummary SET InvoiceNo='{txtinvoiceno.Text}'" +
                $",ExpenseDate='{txtexpdate.Text}'" +
                $",SupplierID='{suppid.ToString()}'" +
                $",Description='{txtremarks.Text}' WHERE ReferenceNumber='{txtrefno.Text}' ", "Successfully Updated");
            }
            else { return; }
                
        }

        private void txtvendor_EditValueChanged(object sender, EventArgs e)
        {
            suppid = SearchLookUpClass.getSingleValue(txtvendor, "SupplierID");
        }

        private void btndetails_Click(object sender, EventArgs e)
        {
            double total = 0.0;
            //int ctr = 0;
            //bool noDetails = false;
            //noDetails = Database.checkifExist($"SELECT TOP(1) ReferenceNumber FROM dbo.ExpenseDetails WHERE ReferenceNumber='{txtrefno.Text}'");
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to update this Expense Details", "Update Expense Details");
            if (confirm)
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    //if (noDetails)
                    //{
                    //    Database.ExecuteQuery($"INSERT INTO dbo.ExpenseDetails VALUES('{ctr}','888','{txtrefno.Text}','{txtinvoiceno.Text}','{gridView1.GetRowCellValue(i, "ExpenseName").ToString()}','{gridView1.GetRowCellValue(i, "Remarks").ToString()}','{gridView1.GetRowCellValue(i, "Amount").ToString()}')");
                    //    ctr += 1;
                    //}
                    //else
                    //{

                    //}
                    Database.ExecuteQuery($"Update dbo.ExpenseDetails SET InvoiceNo='{gridView1.GetRowCellValue(i, "InvoiceNo").ToString()}'" +
                                      $",ExpenseName='{gridView1.GetRowCellValue(i, "ExpenseName").ToString()}'" +
                                      $",Amount='{gridView1.GetRowCellValue(i, "Amount").ToString()}'" +
                                      $",Remarks='{gridView1.GetRowCellValue(i, "Remarks").ToString()}' " +
                                      $"WHERE ReferenceNumber='{txtrefno.Text}' ");
                    total += Convert.ToDouble(gridView1.GetRowCellValue(i, "Amount").ToString());

                }
            }
            else
            {
                return;
            }
            Database.ExecuteQuery($"Update dbo.ExpenseSummary Set Amount='{total}' WHERE ReferenceNumber='{txtrefno.Text}'", "Successfully Updated");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

          
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }
    }
}