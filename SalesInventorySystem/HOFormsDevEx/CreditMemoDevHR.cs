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
    public partial class CreditMemoDevHR : DevExpress.XtraEditors.XtraForm
    {
        public CreditMemoDevHR()
        {
            InitializeComponent();
        }

        private void gridView4_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double sellingprice = 0.0,qty=0.0,newtotalamnt=0.0,actualqty=0.0,total=0.0,totalamount=0.0,discountamount=0.0;
            bool isReturned = false;
            //isReturned = Convert.ToBoolean(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "isReturned").ToString());
            totalamount = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "TotalAmount").ToString());
            qty = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "QtySold").ToString());
            actualqty = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "NewQty").ToString());
            sellingprice = Convert.ToDouble(gridView4.GetRowCellValue(gridView4.FocusedRowHandle, "SellingPrice").ToString());
            newtotalamnt = actualqty * sellingprice;
            
            discountamount = Math.Round(totalamount - newtotalamnt, 2);
            if (isReturned==true)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool isInvoiceUpdated = false;
                isInvoiceUpdated = Database.checkifExist("Select isInvoiceUpdate FROM DeliverySummary WHERE PONumber='" + txtpono.Text + "' and isInvoiceUpdate=1");
                if(isInvoiceUpdated==false)
                {
                    XtraMessageBox.Show("Invoice Number must be updated first..please go to Print Delivery Receipt Option!...");
                    return;
                }
                //bool isUpdated = false;
                //for (int i = 0; i <= gridView4.RowCount - 1; i++)
                //{
                //    if (Convert.ToDouble(gridView4.GetRowCellValue(i, "Variance").ToString()) != 0)
                //    {
                //        isUpdated = true;
                //        break;
                //    }
                //}
                //if (isUpdated)
                //{
                //    XtraMessageBox.Show("The System found out that there are items that already executed");
                //    return;
                //}
                //bool checkReturned = Database.checkifExist("Select isReturned FROM DeliveryDetails WHERE isReturned=1 AND PONumber='" + txtpono.Text + "' and ProductNo='" + txtprodno.Text + "'");
                //if (checkReturned == true)
                //{
                //    XtraMessageBox.Show("This item is already executed as CreditMemo!");
                //    return;
                //}
                for (int i = 0; i <= gridView4.RowCount - 1; i++)
                {
                    //Database.ExecuteQuery("Update DeliveryDetails set ActualQty='" + gridView4.GetRowCellValue(i, "ActualQty").ToString() + "',isCreditMemo='1'  WHERE PONumber='" + txtpono.Text + "' and ProductNo='" + gridView4.GetRowCellValue(i, "ProductNo").ToString() + "' and SequenceNo='"+ gridView4.GetRowCellValue(i, "SequenceNo").ToString() + "' and Variance <> 0");
                    if (Convert.ToDouble(gridView4.GetRowCellValue(i, "Variance").ToString()) != 0)
                    {
                        Database.ExecuteQuery("Update DeliveryDetails set ActualQty='" + gridView4.GetRowCellValue(i, "ActualQty").ToString() + "',isCreditMemo='1'  WHERE PONumber='" + txtpono.Text + "' and ProductNo='" + gridView4.GetRowCellValue(i, "ProductNo").ToString() + "' and SeqNo='" + gridView4.GetRowCellValue(i, "SeqNo").ToString() + "' AND isCreditMemo=0");
                    }
                }
                    executeSP();
                XtraMessageBox.Show("Payment Successfully Posted");
                //this.Dispose();
                //button2.PerformClick();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
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

        private void CreditMemoDevEx_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Database.display("SELECT PONumber,QtyDelivered as Qty,ProductName as Description,SellingPrice as UnitCost,FORMAT(TotalAmount , 'N', 'en-us') as Amount FROM view_CreditMemoDetails WHERE Variance > 0 and PONumber='" + txtpono.Text + "' ", gridControl4, gridView4);
                Database.display("SELECT * FROM view_CreditMemoDetails WHERE PONumber='" + txtpono.Text + "' AND isCreditMemo='1'", gridControl4, gridView4);

                //button3.PerformClick();
                DevExReportTemplate.Creditmemo xct = new DevExReportTemplate.Creditmemo();
                xct.Landscape = false;

                string custname = Database.getSingleQuery("PurchaseOrderSummary", " PONumber='" + txtpono.Text + "'", "Customer");
                string refno = Database.getSingleQuery("DeliverySummary", " PONumber='" + txtpono.Text + "'", "ReferenceNumber");
                string invoiceno = Database.getSingleQuery("DeliverySummary", " PONumber='" + txtpono.Text + "'", "InvoiceNo");
                //string refno = Database.getSingleQuery("DeliveryDetails", " PONumber='" + txtpono.Text + "'", "ReferenceNumber");

                xct.xrcustname.Text = custname;
                xct.xrinvoiceno.Text = invoiceno;
                xct.xrdate.Text = DateTime.Now.ToShortDateString();
                xct.xrPreparedBy.Text = Login.Fullname;

                xct.xrtitle.Text = "Credit Memo";

                gridView4.Columns["PONumber"].Visible = false;
                gridView4.Columns["ProductNo"].Visible = false;
                gridView4.Columns["QtyDelivered"].Visible = false;
                gridView4.Columns["ActualQty"].Visible = false;
                gridView4.Columns["Cost"].Visible = false;
                gridView4.Columns["TotalAmount"].Visible = false;
                gridView4.Columns["Status"].Visible = false;
                gridView4.Columns["DateProcessed"].Visible = false;
                gridView4.Columns["isVat"].Visible = false;
                gridView4.Columns["ProcessedBy"].Visible = false;
                gridView4.Columns["SequenceNo"].Visible = false;
                gridView4.Columns["isSettled"].Visible = false;
                gridView4.Columns["isCreditMemo"].Visible = false;
                gridView4.Columns["isReturned"].Visible = false;

                xct.xrPreparedBy.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControlCredMemo(gridControl4,gridView4,"Variance"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.PreviewRibbonForm.FormClosed += PreviewRibbonForm_FormClosed;
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void PreviewRibbonForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //gridView4.Columns["PONumber"].Visible = true;
            //gridView4.Columns["ProductNo"].Visible = true;
            //gridView4.Columns["QtyDelivered"].Visible = true;
            //gridView4.Columns["ActualQty"].Visible = true;
            //gridView4.Columns["Cost"].Visible = true;
            //gridView4.Columns["TotalAmount"].Visible = true;
            //gridView4.Columns["Status"].Visible = true;
            //gridView4.Columns["DateProcessed"].Visible = true;
            //gridView4.Columns["isVat"].Visible = true;
            //gridView4.Columns["ProcessedBy"].Visible = true;
            //gridView4.Columns["SequenceNo"].Visible = true;
            //gridView4.Columns["isSettled"].Visible = true;
            //gridView4.Columns["isCreditMemo"].Visible = true;
            //gridView4.Columns["isReturned"].Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= gridView4.RowCount - 1; i++)
            {
                if (Convert.ToDouble(gridView4.GetRowCellValue(i, "Variance").ToString()) <= 0)
                {
                    gridView4.DeleteRow(i);
                }
            }
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
                        if(gridView4.GetRowCellValue(i, "isVat").ToString() == "True"){ isvat = true; } else { isvat = false; }
                        
                        if (Convert.ToDouble(gridView4.GetRowCellValue(i, "DiscountAmount").ToString()) != 0)
                        {
                            Database.ExecuteQuery($"INSERT INTO dbo.CreditMemoHRI VALUES('{Login.assignedBranch}','{txtpono.Text}','{gridView4.GetRowCellValue(i, "SequenceNumber").ToString()}','{gridView4.GetRowCellValue(i, "ProductCode").ToString()}','{gridView4.GetRowCellValue(i, "Description").ToString()}','{gridView4.GetRowCellValue(i, "SellingPrice").ToString()}','{gridView4.GetRowCellValue(i, "QtySold").ToString()}','{gridView4.GetRowCellValue(i, "SubTotal").ToString()}','{gridView4.GetRowCellValue(i, "TotalAmount").ToString()}','{isvat}','{gridView4.GetRowCellValue(i, "NewQty").ToString()}','{gridView4.GetRowCellValue(i, "NewTotalAmount").ToString()}','{gridView4.GetRowCellValue(i, "DiscountAmount").ToString()}','{gridView4.GetRowCellValue(i, "MachineUsed").ToString()}','{DateTime.Now.ToString()}','{Login.Fullname}')");
                            Database.ExecuteQuery("Update dbo.BatchSalesDetails set isPosted=1,QtySold='" + gridView4.GetRowCellValue(i, "NewQty").ToString() + "'" +
                                ",TotalAmount='" + gridView4.GetRowCellValue(i, "NewTotalAmount").ToString() + "'  " +
                                "WHERE ReferenceNo='" + txtpono.Text + "' " +
                                "and SequenceNumber='" + gridView4.GetRowCellValue(i, "SequenceNumber").ToString() + "' " +
                                "and BranchCode='"+Login.assignedBranch+"' ");
                        }
                    }
                    executeSP();
                    XtraMessageBox.Show("Successfully Executed!..");
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            submitCM();
            printCM();
            this.Dispose();
        }
        void printCM()
        {
            try
            {
                //Database.display("SELECT PONumber,QtyDelivered as Qty,ProductName as Description,SellingPrice as UnitCost,FORMAT(TotalAmount , 'N', 'en-us') as Amount FROM view_CreditMemoDetails WHERE Variance > 0 and PONumber='" + txtpono.Text + "' ", gridControl4, gridView4);
                //Database.display("SELECT * FROM vw_CreditMemoHRI WHERE PONumber='" + txtpono.Text + "' AND isCreditMemo='1'", gridControl4, gridView4);

                //button3.PerformClick();
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
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControlCredMemo(gridControl4, gridView4, "Variance"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
        }
    }
}