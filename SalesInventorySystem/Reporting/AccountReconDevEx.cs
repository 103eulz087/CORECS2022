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

namespace SalesInventorySystem.Reporting
{
    public partial class AccountReconDevEx : DevExpress.XtraEditors.XtraForm
    {
        public AccountReconDevEx()
        {
            InitializeComponent();
        }

        private void AccountReconDevEx_Load(object sender, EventArgs e)
        {
            populateCOA();
           
        }
        void populateCOA()
        {
            Database.displaySearchlookupEdit("SELECT * FROM view_GLAccounts", searchLookUpEdit1, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("Select * FROM PassbookBalances",txtstatementbalance,"EndingBalance","EndingBalance");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
            
        }

        void display()
        {
            //DETIALS
            if (raddet.Checked == true)
            {
                if (comboBoxEdit1.Text == "CLEARED")
                {
                    Database.display("select * from view_AccountReconSample WHERE AccountCode='" + searchLookUpEdit1.Text + "' and TicketDate between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' AND CostCenter='1' ORDER BY Debit,Credit", gridControl1, gridView1);
                }
                else if (comboBoxEdit1.Text == "UNCLEARED")
                {
                    Database.display("select * from view_AccountReconSample WHERE AccountCode='" + searchLookUpEdit1.Text + "' and TicketDate between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' AND CostCenter <> '1' ORDER BY Debit,Credit", gridControl1, gridView1);
                }
                else if (comboBoxEdit1.Text == "ALL")
                {
                    Database.display("select * from view_AccountReconSample WHERE AccountCode='" + searchLookUpEdit1.Text + "' and TicketDate between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' ORDER BY Debit,Credit", gridControl1, gridView1);
                }
            }
            else  //MASTER
            {
                if (comboBoxEdit1.Text == "CLEARED")
                {
                    Database.display("select * from view_AccountReconMaster WHERE AccountCode='" + searchLookUpEdit1.Text + "' and TicketDate between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' AND CostCenter='1' ORDER BY Debit,Credit", gridControl1, gridView1);
                }
                else if (comboBoxEdit1.Text == "UNCLEARED")
                {
                    Database.display("select * from view_AccountReconMaster WHERE AccountCode='" + searchLookUpEdit1.Text + "' and TicketDate between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' AND CostCenter <> '1' ORDER BY Debit,Credit", gridControl1, gridView1);
                }
                else if (comboBoxEdit1.Text == "ALL")
                {
                    Database.display("select * from view_AccountReconMaster WHERE AccountCode='" + searchLookUpEdit1.Text + "' and TicketDate between '" + dateFrom.Text + "' AND '" + dateTo.Text + "' ORDER BY Debit,Credit", gridControl1, gridView1);
                }
            }

            //Database.display("select * from view_AccountReconSample WHERE AccountCode='"+searchLookUpEdit1.Text+"' and TicketDate <= '"+dateFrom.Text+"' ORDER BY Debit,Credit", gridControl1, gridView1);
            txtdepositintransit.Text = getDepositInTransit().ToString();
            txtoutstandingchecks.Text = getOutStandingCheck().ToString();
            txtglsystembal.Text = getStatementEndingBalance().ToString();
            double diff = 0.0;
            diff = Convert.ToDouble(txtstatementbalance.Text) - Convert.ToDouble(txtoutstandingchecks.Text) + Convert.ToDouble(txtdepositintransit.Text) - Convert.ToDouble(txtglsystembal.Text);
            txtdiff.Text = diff.ToString();
        }

        void refreshComputation()
        {
            txtdepositintransit.Text = getDepositInTransit().ToString();
            txtoutstandingchecks.Text = getOutStandingCheck().ToString();
            txtglsystembal.Text = getStatementEndingBalance().ToString();
            double diff = 0.0;
            diff = Convert.ToDouble(txtstatementbalance.Text) - Convert.ToDouble(txtoutstandingchecks.Text) + Convert.ToDouble(txtdepositintransit.Text) - Convert.ToDouble(txtglsystembal.Text);
            txtdiff.Text = diff.ToString();
        }

        //Double getStatementEndingBalance()
        //{
        //    double bal = 0.0,asofbalance=0.0,previousendingbalance=0.0;
        //    DateTime cutoffdate;
        //    cutoffdate = Convert.ToDateTime(dateFrom.Text).AddDays(-1);

        //    previousendingbalance = Database.getTotalSummation2("GLSummary", "PostingDate = '" + cutoffdate + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");
        //    asofbalance = Database.getTotalSummation2("GLSummary", "PostingDate = '" + dateTo.Text + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");

        //    if (chckasofbalance.Checked==true)
        //    {
        //        // bal = Database.getTotalSummation2("GLSummary", "PostingDate = '" + dateFrom.Text + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");
        //        bal = asofbalance;
        //    }
        //    else if (chckasofbalance.Checked == false)
        //    {
        //        //asofbalance
        //        bal = asofbalance - previousendingbalance;
        //    }
        //    return bal;
        //}
        Decimal getStatementEndingBalance()
        {
            decimal bal = 0, asofbalance = 0, previousendingbalance = 0;
            DateTime cutoffdate;
            cutoffdate = Convert.ToDateTime(dateFrom.Text).AddDays(-1);

            previousendingbalance = Database.getTotalSummation2Dec("GLSummary", "PostingDate = '" + cutoffdate + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");
            asofbalance = Database.getTotalSummation2Dec("GLSummary", "PostingDate = '" + dateTo.Text + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");

            if (chckasofbalance.Checked == true)
            {
                // bal = Database.getTotalSummation2("GLSummary", "PostingDate = '" + dateFrom.Text + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");
                bal = asofbalance;
            }
            else if (chckasofbalance.Checked == false)
            {
                //asofbalance
                bal = asofbalance - previousendingbalance;
            }
            return bal;
        }
        Decimal getDepositInTransit()
        {
            decimal val = 0;
            if (chckasofbalance.Checked == true)
            {
                // bal = Database.getTotalSummation2("GLSummary", "PostingDate = '" + dateFrom.Text + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");
                val = Database.getTotalSummation2Dec("TicketDetails", "AccountCode= '" + searchLookUpEdit1.Text + "' and TicketDate <= '" + dateTo.Text + "' and CostCenter <> '1' and BranchCode in (Select BranchCode FROM Branches) ", "Debit");
            }
            else if (chckasofbalance.Checked == false)
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    //string mark = gridView1.GetRowCellValue(i, "CostCenter").ToString();
                    if (gridView1.GetRowCellValue(i, "Status").ToString() != "1")
                    {
                        val += Convert.ToDecimal(gridView1.GetRowCellValue(i, "Debit").ToString());
                    }
                }
            }
            return val;
        }
        
        Decimal getOutStandingCheck()
        {
            decimal val = 0;
            if (chckasofbalance.Checked == true)
            {
                // bal = Database.getTotalSummation2("GLSummary", "PostingDate = '" + dateFrom.Text + "' and AccountCode='" + searchLookUpEdit1.Text + "' ", "EndingBalance");
                val = Database.getTotalSummation2Dec("TicketDetails", "AccountCode= '" + searchLookUpEdit1.Text + "'  and TicketDate <= '" + dateTo.Text + "' and CostCenter <> '1' and BranchCode in (Select BranchCode FROM Branches) ", "Credit");
            }
            else if (chckasofbalance.Checked == false)
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "Status").ToString() != "1")
                    {
                        val += Convert.ToDecimal(gridView1.GetRowCellValue(i, "Credit").ToString());
                    }
                }
            }
            return val;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {
                string filepath = "C:\\MyFiles\\";
                string filename = "BankRecon_" + dateFrom.Text.Replace('/', '-') + '-' + dateFrom.Text.Replace('/', '-') + ".xls";
                string file = filepath + filename;
                gridView1.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
                contextMenuStrip1.Items[0].Visible = false;
            }
                
        }

        private void clearedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE TicketDetails set CostCenter='1' WHERE TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' and Debit='"+gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Debit").ToString()+ "' and Credit='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Credit").ToString() + "' ", "Cleared");
            display();
        }

        private void unclearedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE TicketDetails set CostCenter='0' WHERE TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "' and Debit='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Debit").ToString() + "' and Credit='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Credit").ToString() + "' ", "UnCleared");
            display();
        }

        private void txtoutstandingchecks_TextChanged(object sender, EventArgs e)
        {
            decimal diff = 0;
            diff = Convert.ToDecimal(txtstatementbalance.Text) - Convert.ToDecimal(txtoutstandingchecks.Text) + Convert.ToDecimal(txtdepositintransit.Text) - Convert.ToDecimal(txtglsystembal.Text);
            txtdiff.Text = diff.ToString();
        }

        //private void txtstatementbalance_TextChanged(object sender, EventArgs e)
        //{
        //    decimal diff = 0;
        //    diff = Convert.ToDecimal(txtstatementbalance.Text) - Convert.ToDecimal(txtoutstandingchecks.Text) + Convert.ToDecimal(txtdepositintransit.Text) - Convert.ToDecimal(txtglsystembal.Text);
        //    txtdiff.Text = diff.ToString();
        //}

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
                {
                    string costcenter = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Status"]);
                    if (costcenter == "1")
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                    }
                }
        }

        private void txtdepositintransit_TextChanged(object sender, EventArgs e)
        {
            decimal diff = 0;
            diff = Convert.ToDecimal(txtstatementbalance.Text) - Convert.ToDecimal(txtoutstandingchecks.Text) + Convert.ToDecimal(txtdepositintransit.Text) - Convert.ToDecimal(txtglsystembal.Text);
            txtdiff.Text = diff.ToString();
        }

        private void txtstatementbalance_EditValueChanged(object sender, EventArgs e)
        {
            decimal diff = 0;
            diff = Convert.ToDecimal(txtstatementbalance.Text) - Convert.ToDecimal(txtoutstandingchecks.Text) + Convert.ToDecimal(txtdepositintransit.Text) - Convert.ToDecimal(txtglsystembal.Text);
            txtdiff.Text = diff.ToString();
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Status")
                // e.RepositoryItem = repositoryItemComboBox5;
                e.RepositoryItem = repositoryItemCheckEdit1;
        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit edit = (DevExpress.XtraEditors.CheckEdit)sender;
            //if(edit.Checked==true)
            //if (Convert.ToBoolean(repositoryItemCheckEdit1.GetValueByState(CheckState.Checked).ToString()) ==true)
            if (edit.Checked == true)//if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Status").ToString() == "True")
            {
                Database.ExecuteQuery("UPDATE TicketDetails SET CostCenter='1' WHERE TicketNumber='" + gridView1.GetFocusedRowCellValue("TicketNumber").ToString() + "' AND Debit='"+ gridView1.GetFocusedRowCellValue("Debit").ToString() + "' AND Credit='"+ gridView1.GetFocusedRowCellValue("Credit").ToString() + "' ");
                refreshComputation(); 
            }
            else
            {
                Database.ExecuteQuery("UPDATE TicketDetails SET CostCenter='0' WHERE TicketNumber='" + gridView1.GetFocusedRowCellValue("TicketNumber").ToString() + "' AND Debit='" + gridView1.GetFocusedRowCellValue("Debit").ToString() + "' AND Credit='" + gridView1.GetFocusedRowCellValue("Credit").ToString() + "' ");
                refreshComputation();
            }
        }

        //private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //{
        //    if(e.Column.FieldName == "CostCenter")
        //    {
        //        //DevExpress.XtraEditors.CheckEdit edit = (DevExpress.XtraEditors.CheckEdit)sender;
        //        if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CostCenter").ToString() == "True") //if (edit.Checked == true)//
        //            Database.ExecuteQuery("UPDATE TicketDetails SET CostCenter='1' WHERE TicketNumber='" + gridView1.GetFocusedRowCellValue("TicketNumber").ToString() + "'");
        //        else
        //            Database.ExecuteQuery("UPDATE TicketDetails SET CostCenter='0' WHERE TicketNumber='" + gridView1.GetFocusedRowCellValue("TicketNumber").ToString() + "'");

        //    }
        //}

        private void repositoryItemCheckEdit1_QueryCheckStateByValue(object sender, DevExpress.XtraEditors.Controls.QueryCheckStateByValueEventArgs e)
        {
            string val = e.Value.ToString();
            switch (val)
            {
                case "True":
                case "Yes":
                case "1":
                    e.CheckState = CheckState.Checked;
                    break;
                case "False":
                case "No":
                case "0":
                    e.CheckState = CheckState.Unchecked;
                    break;
                default:
                    e.CheckState = CheckState.Indeterminate;
                    break;
            }
            e.Handled = true;
        }

        private void viewTicketDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Accounting.ViewTicketMasterDetails asds = new Accounting.ViewTicketMasterDetails();
            Database.GridMasterDetail("TicketMaster", "TicketDetails", "TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "'", "TicketNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString() + "'", "TicketNumber", "TicketNumber", "TicketMasterDetails", asds.gridControl1);
            asds.ShowDialog(this);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {
                string filepath = "C:\\MyFiles\\";
                string filename = "BankRecon_" + dateFrom.Text.Replace('/', '-') + '-' + dateFrom.Text.Replace('/', '-') + ".xls";
                string file = filepath + filename;
                gridView1.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }
    }
}