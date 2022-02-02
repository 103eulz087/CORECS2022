using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Accounting
{
    public partial class AddNewTicket : Form
    {
        List<string> list = new List<string>();
        DataTable table;
        string acctcode, accttitle, deb, cred;
        public AddNewTicket()
        {
            InitializeComponent();
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
        }

        private void AddNewTicket_Load(object sender, EventArgs e)
        {
            displayVendor();
            displayCheckersList();
            displayApproversList();
            string checker = Database.getSingleQuery("Users", "isChecker='1'", "FullName");
            string approver = Database.getSingleQuery("Users", "isApprover='1'", "FullName");
            txtmaker.Text = Login.isglobalUserID;
            txtticketno.Text = getTicketNumber(); //IDGenerator.getLastTicketNumber().ToString();//getTicketNo();
            txtchecker.Text = "";//checker;
            txtapprover.Text = "";// approver;
            txtticketdate.Text = DateTime.Now.ToShortDateString();
            txtorigin.Text = Login.assignedBranch;
            txtbrcode.Text = Login.assignedBranch;
            txtsuppno.Text = "0";
            table = new DataTable();
            table.Columns.Add("AccountCode");
            table.Columns.Add("AccountTitle");
            table.Columns.Add("Debit");
            table.Columns.Add("Credit");
            gridControl1.DataSource = table;
            gridView1.Columns["Debit"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Debit", "{0:n2}");
            gridView1.Columns["Credit"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Credit", "{0:n2}");
        }

        void displayVendor()
        {
            Database.displaySearchlookupEdit("Select SupplierID,SupplierName From Supplier", searchLookUpEdit1, "SupplierID", "SupplierID");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
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

        private String getTicketNo()
        {
            string ticketno = "";
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_GetTicketNumber";
            SqlCommand com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                ticketno = reader["ticketnumber"].ToString();
            }
            return ticketno;
        }

        String getUserID(string fullname)
        {
            string str = "";
            str = Database.getSingleQuery("Users", "FullName='" + fullname + "'", "UserID");
            return str;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string approver = Database.getSingleQuery("Approvers", "UserID <> ''", "UserID");
            
            if (memoEdit1.Text=="" || txtbrcode.Text=="")
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
                    //list.Add(acctcode);
                    //foreach(var ss in list)
                    //{
                    //    if (list.Contains(ss)==true)
                    //    {
                    //        isduplicate = true;
                    //        break;
                    //    }
                    //}
                    //if (isduplicate == true)
                    //{
                    //    XtraMessageBox.Show("The System Found out that AccountCode is more than 1");
                    //    return;
                    //}
                    //Database.ExecuteQuery("INSERT INTO TempTicketDetails VALUES ('" + txtbrcode.Text + "'" +
                    //    ",'" + txtticketdate.Text + "'" +
                    //    ",'" + txtsuppno.Text + "'" +
                    //    ",'" + txtticketno.Text + "'" +
                    //    ",'" + txtorigin.Text + "'" +
                    //    ",'" + acctcode + "'" +
                    //    ",'" + accttitle + "'" +
                    //    ",'" + deb + "'" +
                    //    ",'" + cred + "'" +
                    //    ",'0')");
                    Database.ExecuteQuery("INSERT INTO TicketDetails VALUES ('" + txtticketdate.Text + "'" +
                        ",'0'" +
                        ",'" + txtbrcode.Text + "'" +
                        ",'" + txtrefno.Text + "'" +
                        ",'" + txtticketno.Text + "'" +
                        ",'" + txtrefno.Text + "'" +
                        ",'" + acctcode + "'" +
                        //",'" + accttitle + "'" +
                        ",'" + deb + "'" +
                        ",'" + cred + "'" +
                        ",'0')");
                }
                //Database.ExecuteQuery("INSERT INTO TempTicketMaster " +
                //    "VALUES ('" + txtbrcode.Text + "'" +
                //    ",'" + txtticketdate.Text + "'" +
                //    ",'" + txtsuppno.Text + "'" +
                //    ",'" + txtticketno.Text + "'" +
                //    ",'" + txtorigin.Text + "'" +
                //    ",' '" +
                //    ",'" + memoEdit1.Text + "'" +
                //    ",'" + txtmaker.Text + "'" +
                //    ",'" + DateTime.Now.ToShortDateString() + "'" +
                //    ",'" + getUserID(txtchecker.Text) + "'" +
                //    ",' '" +
                //    ",'FOR APPROVAL'" +
                //    ",'"+ approver + "'" +
                //    ",' ','FOR APPROVAL'" +
                //    ",'PENDING'" +
                //    ",''" +
                //    ",'"+txtrefno.Text+"'" +
                //    ",'" + searchLookUpEdit1.Text+"')"
                //    , "Successfully Added!");
                Database.ExecuteQuery("INSERT INTO TicketMaster " +
                    "VALUES ('" + txtticketdate.Text + "'" +
                    ",'0'" +
                    ",'" + txtbrcode.Text + "'" +
                    ",'" + txtbrcode.Text + "'" +
                    ",'" + txtticketno.Text + "'" +
                    ",'"+  txtrefno.Text+"'" +
                    ",'" + txtrefno.Text + "'" +
                    ",'" + searchLookUpEdit1.Text + "'" +
                    ",'" + memoEdit1.Text+ "'" +
                    ",'" + Login.Fullname + "'" +
                    ",'*'" +
                    ",'*'" +
                    ",'UPDATED'" +
                    ",'MANUALENTRY'" +
                    ",' ')"
                    , "Successfully Added!");

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

        private void txtbrcode_Click(object sender, EventArgs e)
        {
            Database.displayDevComboBoxItems("SELECT * FROM Branches", "BranchCode", txtbrcode);
        }

     
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            SearchAccountCode sacForm = new SearchAccountCode();
            sacForm.FormClosed += new FormClosedEventHandler(sacForm_FormClosed);
            sacForm.Show();
        }
        void sacForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "AccountCode", SearchAccountCode.acctcode);
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "AccountTitle", SearchAccountCode.acctdesc);
            gridView1.FocusedColumn = gridView1.Columns["Debit"];
        }


        void displayCheckersList()
        {
            Database.displayDevComboBoxItems("SELECT * FROM Users WHERE isChecker='1'", "FullName", txtchecker);
        }

        void displayApproversList()
        {
            Database.displayDevComboBoxItems("SELECT * FROM Users WHERE isApprover='1'", "FullName", txtapprover);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            Database.displaySearchlookupEdit("Select SupplierID,SupplierName From Supplier", searchLookUpEdit1, "SupplierID", "SupplierID");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
                Database.displaySearchlookupEdit("Select CustomerID,CustomerName From Customers", searchLookUpEdit1, "CustomerID", "CustomerID");
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                searchLookUpEdit1.Text = "";
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

        private void gridControl1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }
    }
}
