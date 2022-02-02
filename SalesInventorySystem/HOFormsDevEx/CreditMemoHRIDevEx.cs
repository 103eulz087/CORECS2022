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
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class CreditMemoHRIDevEx : DevExpress.XtraEditors.XtraForm
    {
        public CreditMemoHRIDevEx()
        {
            InitializeComponent();
        }

        private void gridView4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double sellingprice = 0.0, qty = 0.0, newtotalamnt = 0.0, actualqty = 0.0, total = 0.0, totalamount = 0.0, discountamount = 0.0;
            bool isReturned = false;
            //isReturned = Convert.ToBoolean(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "isReturned").ToString());
            totalamount = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "TotalAmount").ToString());
            qty = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "QtySold").ToString());
            actualqty = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "NewQty").ToString());
            sellingprice = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SellingPrice").ToString());
            newtotalamnt = actualqty * sellingprice;

            discountamount = Math.Round(totalamount - newtotalamnt, 2);
            if (isReturned == true)
            {
                XtraMessageBox.Show("This item is Already Returned!..");
                return;
            }
            if (e.Column.FieldName == "NewQty")
            {
                gridView4.SetRowCellValue(gridView4.FocusedRowHandle, "NewTotalAmount", newtotalamnt.ToString());
                gridView4.SetRowCellValue(gridView4.FocusedRowHandle, "DiscountAmount", discountamount.ToString());
            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            submitCM();
            //printCM();
            this.Dispose();
        }

        void executeSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_CreditMemoHRI";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbrcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmrefno", txtpono.Text);
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

        private void gridView4_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "NewQty")
                e.Cancel = true;
        }

        void submitCM()
        {
            try
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure?", "Confirm Transaction");
                if (confirm)
                {
                    for (int i = 0; i <= gridView4.RowCount - 1; i++)
                    {
                        bool isvat = false;
                        if (gridView4.GetRowCellValue(i, "isVat").ToString() == "True") { isvat = true; } else { isvat = false; }

                        if (Convert.ToDouble(gridView4.GetRowCellValue(i, "DiscountAmount").ToString()) != 0)
                        {
                            Database.ExecuteQuery($"INSERT INTO dbo.CreditMemoHRI VALUES('{Login.assignedBranch}','{txtpono.Text}','{gridView4.GetRowCellValue(i, "SequenceNumber").ToString()}','{gridView4.GetRowCellValue(i, "ProductCode").ToString()}','{gridView4.GetRowCellValue(i, "Description").ToString()}','{gridView4.GetRowCellValue(i, "SellingPrice").ToString()}','{gridView4.GetRowCellValue(i, "QtySold").ToString()}','{gridView4.GetRowCellValue(i, "SubTotal").ToString()}','{gridView4.GetRowCellValue(i, "TotalAmount").ToString()}','{isvat}','{gridView4.GetRowCellValue(i, "NewQty").ToString()}','{gridView4.GetRowCellValue(i, "NewTotalAmount").ToString()}','{gridView4.GetRowCellValue(i, "DiscountAmount").ToString()}','{gridView4.GetRowCellValue(i, "MachineUsed").ToString()}','{DateTime.Now.ToString()}','{Login.Fullname}')");
                            Database.ExecuteQuery("Update dbo.BatchSalesDetails set isPosted=1,QtySold='" + gridView4.GetRowCellValue(i, "NewQty").ToString() + "'" +
                                ",TotalAmount='" + gridView4.GetRowCellValue(i, "NewTotalAmount").ToString() + "'  " +
                                "WHERE ReferenceNo='" + txtpono.Text + "' " +
                                "and SequenceNumber='" + gridView4.GetRowCellValue(i, "SequenceNumber").ToString() + "' " +
                                "and BranchCode='" + Login.assignedBranch + "' ");
                        }
                    }
                    executeSP();
                    XtraMessageBox.Show("Successfully Executed!..");
                    printCM();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void printCM()
        {
            try
            {
               
                DevExReportTemplate.Creditmemo xct = new DevExReportTemplate.Creditmemo();
                xct.Landscape = false;


                var rowz = Database.getMultipleQuery($"SELECT CustomerName,InvoiceNo,ReferenceNo FROM view_batchTransactionSummary WHERE ReferenceNo='{txtpono.Text}'", "CustomerName,InvoiceNo,ReferenceNo");

                string custname = rowz["CustomerName"].ToString();
                string refno = rowz["ReferenceNo"].ToString();
                string invoiceno = rowz["InvoiceNo"].ToString();
                //string refno = Database.getSingleQuery("DeliveryDetails", " PONumber='" + txtpono.Text + "'", "ReferenceNumber");

                xct.xrcustname.Text = custname;
                xct.xrinvoiceno.Text = invoiceno;
                xct.xrdate.Text = DateTime.Now.ToShortDateString();
                xct.xrPreparedBy.Text = Login.Fullname;

                xct.xrtitle.Text = "Credit Memo";

                gridView4.Columns["ReferenceNo"].Visible = false;
                gridView4.Columns["SequenceNumber"].Visible = false;
                gridView4.Columns["SubTotal"].Visible = false;
                gridView4.Columns["isVat"].Visible = false;
                gridView4.Columns["MachineUsed"].Visible = false;


                xct.xrPreparedBy.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl4));
                //xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControlCredMemo(gridControl4, gridView4, "DiscountAmount"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            printCM();
        }
    }
}