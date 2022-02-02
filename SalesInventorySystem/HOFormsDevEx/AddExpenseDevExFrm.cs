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
using SalesInventorySystem.Accounting;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AddExpenseDevExFrm : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        string acctcode, accttitle, deb, cred;
        bool ok = false;
        public AddExpenseDevExFrm()
        {
            InitializeComponent();
        }

        private void AddExpenseDevExFrm_Load(object sender, EventArgs e)
        {
            txtticketno.Text = getTicketNumber(); //IDGenerator.getLastTicketNumber().ToString();//getTicketNo();
            txtrefno.Text = IDGenerator.getIDNumberSP("sp_GetExpenseNumber", "expenseno");//IDGenerator.getExpenseNumber();
            displayvendor();
            populateBranches();
            populateBranches2();
            table = new DataTable();
            table.Columns.Add("AccountCode");
            table.Columns.Add("AccountTitle");
            table.Columns.Add("Debit");
            table.Columns.Add("Credit");
            gridControl1.DataSource = table;
            gridView1.Columns["Debit"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Debit", "{0:n2}");
            gridView1.Columns["Credit"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Credit", "{0:n2}");
        }
        void populateBranches()
        {
            Database.displayDevComboBoxItems("SELECT BranchCode FROM Branches", "BranchCode", txtbrcode);
        }
        void populateBranches2()
        {
            Database.displayDevComboBoxItems("SELECT BranchCode FROM Branches", "BranchCode", txtbrcodesum);
        }
        String getTicketNumber()
        {
            string num = "";
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_GetTicketNumber";
            SqlCommand com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    num = reader["TicketNumber"].ToString();
                }
            }
            return num;
            //  con.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["Debit"] = 0;
            newRow["Credit"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "AccountCode")
                e.RepositoryItem = repositoryItemButtonEdit1;
            if (e.Column.FieldName == "Debit")
                e.RepositoryItem = spindebit;
            if (e.Column.FieldName == "Credit")
                e.RepositoryItem = spincredit;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtrefno.Text == "" || txtremakrs.Text == "" || txtexpname.Text == "")
                {
                    XtraMessageBox.Show("Please Input All Valid Fields");
                }
                else if (Convert.ToDouble(lbltotaldebit.Text) != Convert.ToDouble(lbltotalcredit.Text))
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
                        Database.ExecuteQuery("INSERT INTO TempTicketDetails VALUES ('" + txtbrcode.Text + "','" + txtexpdate.Text + "','0','" + txtticketno.Text + "','" + txtbrcode.Text + "','" + acctcode + "','" + accttitle + "','" + deb + "','" + cred + "','0')");
                    }
                    Database.ExecuteQuery("INSERT INTO TempTicketMaster VALUES ('" + txtbrcode.Text + "'" +
                        ",'" + txtexpdate.Text + "'" +
                        ",'0'" +
                        ",'" + txtticketno.Text + "'" +
                        ",'" + txtbrcode.Text + "'" +
                        ",' '" +
                        ",'" + txtremakrs.Text + "'" +
                        ",'" + Login.Fullname + "'" +
                        ",'" + DateTime.Now.ToShortDateString() + "'" +
                        ",'*'" +
                        ",' '" +
                        ",'Approved'" +
                        ",'*'" +
                        ",' '" +
                        ",'Approved'" +
                        ",'PENDING'" +
                        ",'" + txtremakrs.Text + "'" +
                        ",' '" +
                        ",'" + txtvendor.Text + "')");
                    Database.ExecuteQuery("INSERT INTO ExpenseMaster " +
                        "VALUES('1' "+ // TRN_SEQ_NO
                        ",'" + txtbrcode.Text + "'" + // BranchCode
                        ",'" + Classes.Suppliers.getSupplierID(txtvendor.Text) + "'" + // SupplierID
                        ",'" + txtrefno.Text + "'" + // ReferenceNumber
                        ",'" + txtinvoiceno.Text + "'" + // InvoiceNo
                        ",'" + txtexpname.Text + "'" + // ExpenseName
                        ",'" + txtexpdate.Text + "'" + // ExpenseDate
                        ",'" + textBox4.Text + "'" + // Amount
                        ",'" + txtremakrs.Text + "'" + // Remarks
                        ",'UNPAID'" + // Status
                        ",'" + textBox4.Text + "'" + // Balance
                        ",0" + // AmountPaid
                        ",0" + // EWTAmount
                        ",0" + // DiscountAmount
                        ",0" + // OffsetAmount
                        ",0)" // isErrorCorrect
                        , "Successfully Added!");
                    postExpense();
                    XtraMessageBox.Show("Successfully Added!");
                    this.Close();
                }
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            
        }

        void postExpense()
        {
            string mode = "";
            if(creditmode.Checked==true)
            {
                mode = "CREDITMODE";
            }
            else
            {
                mode = "DEBITMODE";
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spu_PostExpense";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranch", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmticketno", txtticketno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                com.Parameters.AddWithValue("@parmexpensename", txtexpname.Text);
                com.Parameters.AddWithValue("@parmexpdate", txtexpdate.Text);
                com.Parameters.AddWithValue("@parmexpamount", textBox4.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtvendor.Text);
                com.Parameters.AddWithValue("@parmremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.Parameters.AddWithValue("@parmmode", mode);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
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

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            string val = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountTitle").ToString();
            e.Cancel = gridView1.FocusedColumn.FieldName == "AccountTitle";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                ok = true;
                txtbrcodesum.Text = "";
                txtbrcodesum.Enabled = false;
            }
            else
            {
                ok = false;
                txtbrcodesum.Enabled = true;
                Database.displayDevComboBoxItems("SELECT BranchCode from Branches", "BranchCode", txtbrcodesum);
            }
        }

        private void btnget_Click(object sender, EventArgs e)
        {
            getquery();
        }

        void getquery()
        {
            
            if (checkBox1.Checked == true) //if all branch
            {
                ok = true;
            }
            else
            {
                ok = false;
            }
            if(ok)
            {
                Database.display("SELECT * FROM view_ExpenseMaster WHERE ExpenseDate >= '" + datefrom.Text + "' and ExpenseDate <= '" + dateto.Text + "'", gridControl2, gridView2);
            }
            else
            {
                Database.display("SELECT * FROM view_ExpenseMaster WHERE ExpenseDate >= '" + datefrom.Text + "' and ExpenseDate <= '" + dateto.Text + "' AND BranchCode='"+txtbrcodesum.Text+"'", gridControl2, gridView2);
            }
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
  

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        void displayvendor()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", txtvendor, "SupplierID", "SupplierID");
        }



    }
}