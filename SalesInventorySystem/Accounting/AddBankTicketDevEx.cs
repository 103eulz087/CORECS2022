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
    public partial class AddBankTicketDevEx : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        string acctcode, accttitle, deb, cred;

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "AccountCode")
                e.RepositoryItem = repositoryItemButtonEdit1;
            if (e.Column.FieldName == "Debit")
                e.RepositoryItem = spindebit;
            if (e.Column.FieldName == "Credit")
                e.RepositoryItem = spincredit;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(lbltotaldebit.Text) != Convert.ToDouble(lbltotalcredit.Text))
            {
                XtraMessageBox.Show("Debit / Credit must Equal");
            }
            else
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    acctcode = gridView1.GetRowCellValue(i, "AccountCode").ToString();
                    accttitle = gridView1.GetRowCellValue(i, "AccountTitle").ToString();
                    deb = gridView1.GetRowCellValue(i, "Debit").ToString();
                    cred = gridView1.GetRowCellValue(i, "Credit").ToString();
                    Database.ExecuteQuery("INSERT INTO TicketDetails VALUES ('" + txtticketdate.Text + "','0','"+txtbrcode.Text+"','"+txtbrcode.Text+"','" + txtticketno.Text + "','','"+ acctcode + "','"+ deb + "','" + cred + "','')");
                }
                string mark1 = deb;
                string mark2 = cred;
                Database.ExecuteQuery("INSERT INTO TicketMaster VALUES ('" + txtticketdate.Text + "','0','" + txtbrcode.Text + "','" + txtbrcode.Text + "','" + txtticketno.Text + "','','BANK RECON','Cleared','" + memoEdit1.Text + "','" + Login.Fullname + "','*','*','Updated','','')");
                Database.ExecuteQuery("INSERT INTO BankReconLedger VALUES ('"+txtticketdate.Text+"','Cleared',0,'"+deb+"','0','"+memoEdit1.Text+"',0,0,'','"+txtticketno.Text+"')");
                this.Close();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double debittt = 0.0, creditt = 0.0;
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                acctcode = gridView1.GetRowCellValue(i, "AccountCode").ToString();
                accttitle = gridView1.GetRowCellValue(i, "AccountTitle").ToString();
                deb = gridView1.GetRowCellValue(i, "Debit").ToString();
                cred = gridView1.GetRowCellValue(i, "Credit").ToString();
                debittt += Convert.ToDouble(deb);
                creditt += Convert.ToDouble(cred);
            }
            lbltotaldebit.Text = debittt.ToString();
            lbltotalcredit.Text = creditt.ToString();
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            SearchAccountCode sacForm = new SearchAccountCode();
            sacForm.FormClosed += new FormClosedEventHandler(SacForm_FormClosed);
            sacForm.Show();
        }

        private void SacForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "AccountCode", SearchAccountCode.acctcode);
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "AccountTitle", SearchAccountCode.acctdesc);
            gridView1.FocusedColumn = gridView1.Columns["Debit"];
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
            double debittt = 0.0, creditt = 0.0;
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                acctcode = gridView1.GetRowCellValue(i, "AccountCode").ToString();
                accttitle = gridView1.GetRowCellValue(i, "AccountTitle").ToString();
                deb = gridView1.GetRowCellValue(i, "Debit").ToString();
                cred = gridView1.GetRowCellValue(i, "Credit").ToString();
                debittt += Convert.ToDouble(deb);
                creditt += Convert.ToDouble(cred);
            }
            lbltotaldebit.Text = debittt.ToString();
            lbltotalcredit.Text = creditt.ToString();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            string val = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountTitle").ToString();
            e.Cancel = gridView1.FocusedColumn.FieldName == "AccountTitle";
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["Debit"] = 0;
            newRow["Credit"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
        }

        public AddBankTicketDevEx()
        {
            InitializeComponent();
        }

        private void AddBankTicketDevEx_Load(object sender, EventArgs e)
        {
            txtticketno.Text = IDGenerator.getIDNumberSP("sp_GetTicketNumber", "TicketNumber");//getTicketNumberSP();
            txtbrcode.Text = Login.assignedBranch;
            table = new DataTable();
            table.Columns.Add("AccountCode");
            table.Columns.Add("AccountTitle");
            table.Columns.Add("Debit");
            table.Columns.Add("Credit");
            gridControl1.DataSource = table;
            gridView1.Columns["Debit"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Debit", "{0:n2}");
            gridView1.Columns["Credit"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Credit", "{0:n2}");
        }
    }
}