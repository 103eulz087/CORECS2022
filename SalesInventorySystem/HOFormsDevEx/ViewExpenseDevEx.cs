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
    public partial class ViewExpenseDevEx : DevExpress.XtraEditors.XtraForm
    {
        string action = "";
        public ViewExpenseDevEx()
        {
            InitializeComponent();
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripForApproval.Show(gridControl2,e.Location);
        }

        private void ViewExpenseDevEx_Load(object sender, EventArgs e)
        {
            filtertab();
        }

        private void filtertab()
        {
            //FOR APPROVAL
            if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPageForApproval)) //FOR APPROVAL
            {
                Database.display($"SELECT * FROM view_ExpenseSummary WHERE Status='FOR APPROVAL'", gridControl2, gridView2);
            }
            else if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPageApproved))  //APPROVED
            {
                Database.display($"SELECT * FROM view_ExpenseSummary WHERE Status='APPROVED' AND CAST(ExpenseDate as date) between '{datefromapproved.Text}' AND '{datetoapproved.Text}' ", gridControl1, gridView1);
            }
            else if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPageCancelled))  //APPROVED
            {
                Database.display($"SELECT * FROM view_ExpenseSummary WHERE Status='CANCELLED'", gridControl3, gridView3);
            }
            else if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPagePaid))  //APPROVED
            {
                Database.display($"SELECT * FROM view_ExpenseSummary WHERE Status='PAID' AND CAST(ExpenseDate as date) between '{dateFromPaid.Text}' AND '{dateToPaid.Text}' ", gridControl4, gridView4);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            displayDetails();
        }

        void displayDetails()
        {
            string invoiceno = "", refno = "",suppid="";
            ViewExpenseDetailsDevEx viewdetdevex = new ViewExpenseDetailsDevEx();
            if (Convert.ToBoolean(Login.isglobalBranchOfficer) == true)
            {
                viewdetdevex.btnApproved.Visible = true;
                viewdetdevex.btncancel.Visible = true;
            }
            else if (Convert.ToBoolean(Login.isglobalBranchOfficer) == false)
            {
                viewdetdevex.btnApproved.Visible = false;
                viewdetdevex.btncancel.Visible = false;
            }

            if (action == "APPROVED")
            {
                invoiceno= gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InvoiceNo").ToString();
                refno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNumber").ToString();
                suppid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
               
              
                viewdetdevex.txtinvoiceno.Text = invoiceno;
                viewdetdevex.txtrefno.Text = refno;
                viewdetdevex.txtsuppid.Text = suppid;
                viewdetdevex.btnApproved.Visible = false;
                viewdetdevex.btncancel.Visible = false;
            }
            else if (action == "CANCELLED")
            {
                invoiceno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "InvoiceNo").ToString();
                refno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ReferenceNumber").ToString();
                suppid = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "SupplierID").ToString();
             
                viewdetdevex.txtinvoiceno.Text = invoiceno;
                viewdetdevex.txtrefno.Text = refno;
                viewdetdevex.txtsuppid.Text = suppid;
                viewdetdevex.btnApproved.Visible = false;
                viewdetdevex.btncancel.Visible = false;
            }
            else if (action == "PAID")
            {
                invoiceno = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "InvoiceNo").ToString();
                refno = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "ReferenceNumber").ToString();
                suppid = gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SupplierID").ToString();

                viewdetdevex.txtinvoiceno.Text = invoiceno;
                viewdetdevex.txtrefno.Text = refno;
                viewdetdevex.txtsuppid.Text = suppid;
                viewdetdevex.btnApproved.Visible = false;
                viewdetdevex.btncancel.Visible = false;
            }
            else
            {
                invoiceno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceNo").ToString();
                refno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ReferenceNumber").ToString();
                suppid = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString();

                viewdetdevex.txtinvoiceno.Text = invoiceno;
                viewdetdevex.txtrefno.Text = refno;
                viewdetdevex.txtsuppid.Text = suppid;

            }

            Database.display($"SELECT * FROM dbo.ExpenseDetails " +
               $"WHERE ReferenceNumber='{refno}' " +
               $"AND InvoiceNo='{invoiceno}' ", viewdetdevex.gridControl2, viewdetdevex.gridView2);

            Classes.DevXGridViewSettings.ShowFooterTotal(viewdetdevex.gridView2, "Amount");
            viewdetdevex.ShowDialog(this);
            if (ViewExpenseDetailsDevEx.isdone == true)
            {
                filtertab();
                ViewExpenseDetailsDevEx.isdone = false;
                viewdetdevex.Dispose();

            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            filtertab();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripApproved.Show(gridControl1, e.Location);
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripCancelled.Show(gridControl3, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            action = "APPROVED";
            displayDetails();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            action = "CANCELLED";
            displayDetails();
        }

        private void btnPendingGenerate_Click(object sender, EventArgs e)
        {
            Database.display($"SELECT * FROM view_ExpenseSummary WHERE Status='APPROVED' AND CAST(ExpenseDate as date) between '{datefromapproved.Text}' AND '{datetoapproved.Text}' ", gridControl1, gridView1);
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Amount");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Database.display($"SELECT * FROM view_ExpenseSummary WHERE Status='PAID' AND CAST(ExpenseDate as date) between '{dateFromPaid.Text}' AND '{dateToPaid.Text}' ", gridControl4, gridView4);
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView4, "Amount");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            action = "PAID";
            displayDetails();
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditExpenseDevEx editxpns = new EditExpenseDevEx();
            editxpns.txtrefno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ReferenceNumber").ToString();
            editxpns.txtinvoiceno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "InvoiceNo").ToString();
            editxpns.txtexpdate.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ExpenseDate").ToString();
            editxpns.txtremarks.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Description").ToString();
            editxpns.txtvendor.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierName").ToString();
            Database.display($"Select ReferenceNumber,InvoiceNo,ExpenseName,Remarks,Amount " +
                $"FROM dbo.ExpenseDetails " +
                $"WHERE ReferenceNumber='{editxpns.txtrefno.Text}' " +
                $"AND InvoiceNo='{editxpns.txtinvoiceno.Text}'", editxpns.gridControl1, editxpns.gridView1);

            editxpns.ShowDialog(this);
        }

        private void gridControl4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripPaid.Show(gridControl4, e.Location);
            
        }

        private void errorCorrectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void errorCorrect()
        {
            //try
            //{

            //    SqlConnection con = Database.getConnection();
            //    con.Open();
            //    string query = "sp_UpdateExpense";
            //    SqlCommand com = new SqlCommand(query, con);
            //    //com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
            //    //com.Parameters.AddWithValue("@parmsupplierid", suppid.ToString());
            //    //com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
            //    //com.Parameters.AddWithValue("@parmexpensedate", txtexpdate.Text);
            //    //com.Parameters.AddWithValue("@parmremarks", txtremarks.Text);
            //    //com.Parameters.AddWithValue("@parmuser", Login.Fullname);

            //    com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
            //    com.Parameters.AddWithValue("@parmsupplierid", suppid.ToString());
            //    com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
            //    com.Parameters.AddWithValue("@parmexpensedate", txtexpdate.Text);
            //    com.Parameters.AddWithValue("@parmremarks", txtremarks.Text); //DESCRIPTION
            //    com.Parameters.AddWithValue("@parmuser", Login.Fullname);
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.CommandText = query;
            //    com.ExecuteNonQuery();
            //    con.Close();
            //}
            //catch (SqlException ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }
    }
}