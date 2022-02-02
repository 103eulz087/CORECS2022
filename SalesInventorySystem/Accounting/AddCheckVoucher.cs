using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
    public partial class AddCheckVoucher : Form
    {
        DataTable table;
        public AddCheckVoucher()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["InvoiceNo"] ="";
            newRow["InvoiceDate"] = DateTime.Now.ToShortDateString();
            newRow["AccountCode"] = "";
            newRow["Description"] = "";
            newRow["Amount"] = 0;
            newRow["AmountPaid"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
        }

        private void AddCheckVoucher_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("InvoiceNo");
            table.Columns.Add("InvoiceDate");
            table.Columns.Add("AccountCode");
            table.Columns.Add("Description");
            table.Columns.Add("Amount");
            table.Columns.Add("AmountPaid");
            gridControl1.DataSource = table;
            gridView1.Columns["Amount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Debit", "{0:n2}");
            gridView1.Columns["AmountPaid"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Credit", "{0:n2}");
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "InvoiceDate")
                e.RepositoryItem = datepaid;
            if (e.Column.FieldName == "Description")
                e.RepositoryItem = buttonaccountcode;
            if (e.Column.FieldName == "Amount")
                e.RepositoryItem = invoiceamount;
            if (e.Column.FieldName == "AmountPaid")
                e.RepositoryItem = amountpaid;
        }

        private void buttonaccountcode_Click(object sender, EventArgs e)
        {
            SearchAccountCode sacForm = new SearchAccountCode();
            sacForm.FormClosed += new FormClosedEventHandler(SacForm_FormClosed);
            //sacForm.FormClosed += new FormClosedEventHandler(sacForm_FormClosed);
            sacForm.Show();
        }

        private void SacForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "AccountCode", SearchAccountCode.acctcode);
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Description", SearchAccountCode.acctdesc);
            gridView1.FocusedColumn = gridView1.Columns["Amount"];
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DevExReportTemplate.CheckVoucher xct = new DevExReportTemplate.CheckVoucher();
                xct.Landscape = false;
                xct.PaperKind = System.Drawing.Printing.PaperKind.Legal;
                //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
                xct.xrcheckno.Text = txtcheckno.Text;
                xct.xrcheckdate.Text = txtcheckdate.Text;
                xct.xrpaidto.Text = txtpaidto.Text;
                xct.xrparticular.Text = txtremarks.Text;
                xct.xramount.Text = txtamount.Text;

                //long amountinwords = 0;
                //amountinwords = Convert.ToInt64(txtamount.Text);//long.Parse(Convert.ToInt64(txtamount.Text));

                double amountinwords1 = 0.0;
                amountinwords1 = Convert.ToDouble(txtamount.Text);//long.Parse(Convert.ToInt64(txtamount.Text));


                //string str = Classes.Utilities.IntegerToWords(amountinwords);
                string str = HelperFunction.NumWords(amountinwords1);
                xct.xramountinwords.Text = str.ToUpper();
                xct.xrpreparedby.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();
            }
            catch(FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //getVoucherNumberSP
            try
            {
                string id = IDGenerator.getIDNumberSP("sp_GetVoucherNumber", "TicketNumber");//getVoucherNumberSP();
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("INSERT INTO CheckVoucherDetails values(" + Convert.ToInt32(id) + ",'" + gridView1.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridView1.GetRowCellValue(i, "InvoiceDate").ToString() + "','" + gridView1.GetRowCellValue(i, "AccountCode").ToString() + "','" + gridView1.GetRowCellValue(i, "Description").ToString() + "','" + gridView1.GetRowCellValue(i, "Amount").ToString() + "','" + gridView1.GetRowCellValue(i, "AmountPaid").ToString() + "')");
                }
                Database.ExecuteQuery("INSERT INTO CheckVoucher values(" + Convert.ToInt32(id) + ",'" + txtpaidto.Text + "','" + txtcheckno.Text + "','" + txtcheckdate.Text + "','" + txtremarks.Text + "','" + txtamount.Text + "','" + Login.Fullname + "','','','','','','','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "')", "Successfully Added");
                Classes.Utilities.ClearFields(groupBox1);
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                XtraMessageBox.Show("Success");
                this.Dispose();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
           
        }


        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }
    }
}
