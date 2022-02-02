using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace SalesInventorySystem.HOForms
{
    public partial class ViewForDeliveryDetails : Form
    {
        //string referenceNumber = "";
        public static bool isupdated = false;
        public ViewForDeliveryDetails()
        {
            InitializeComponent();
        }

        void updateInvoice()
        {
            //if (Convert.ToBoolean(Login.isglobalOfficer) == true)
            //{
            //    referenceNumber = POForApproval.refno1;
            //}
            //else if (Convert.ToBoolean(Login.isglobalWarehouseOfficer) == true)
            //{
            //    referenceNumber = ViewBranchOrder.refno;
            //}
            if (String.IsNullOrEmpty(txtsino.Text))
            {
                XtraMessageBox.Show("Invoice Number must not Empty");
            }
            else
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure that the Invoice you Enter is Correct?", "Confirm Invoice Number");
                if (confirm == true)
                {
                    Database.ExecuteQuery("UPDATE DeliverySummary SET InvoiceNo='" + txtsino.Text + "',isInvoiceUpdate='1' WHERE PONumber = '" + txtpono.Text + "' ");
                    //forchecking i think it is not used
                    //because it is not yet inserted in Sales.. it will inserted after Confirm Order
                    //Database.ExecuteQuery("UPDATE BatchSalesSummary SET Invoice='" + txtsino.Text + "' WHERE ReferenceNo = '" + oldrefno + "' ");
                    //Database.ExecuteQuery("UPDATE TransactionChargeSales SET Reference='" + txtsino.Text + "' WHERE OrderNo = '" + oldrefno + "' ");
                    //Database.ExecuteQuery("UPDATE TransactionChargeSalesDetails SET ReferenceNo='" + txtsino.Text + "' WHERE OrderNo = '" + oldrefno + "' ");

                    //-----------------------------------------USED FOR PRINTING DR--------------------------------------------------------
                    //-----------------------------------------MUST TRANSFER TO REPORTING--------------------------------------------------
                    string deliveryNum = Database.getSingleQuery("DeliverySummary", "PONumber = '" + txtpono.Text + "'", "DeliveryNo");

                    var row = Database.getMultipleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Customer");

                    string custkey = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Customer");
                    string notes = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "Notes");
                    string custname = Database.getSingleQuery("Customers", "CustomerKey = '" + custkey + "'", "CustomerName");
                    string address = Database.getSingleQuery("Customers", "CustomerKey = '" + custkey + "'", "CustomerAddress");
                    string contactno = Database.getSingleQuery("Customers", "CustomerKey = '" + custkey + "'", "CustomerContactNo");
                    string requestedby = Database.getSingleQuery("PurchaseOrderSummary", "PONumber = '" + txtpono.Text + "'", "RequestedBy");
                    string Barcode = Database.getSingleQuery("view_DeliveryReciept", "PONumber = '" + txtpono.Text + "'", "BarcodeNo");

                    var rowz = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='DeliveryReceiptFrm'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                    string companyname = rowz["Heading"].ToString();
                    string imagewidth = rowz["ImageWidth"].ToString();
                    string imageheight = rowz["ImageHeight"].ToString();
                    string caption1 = rowz["Caption1"].ToString();
                    string caption2 = rowz["Caption2"].ToString();
                    
                    if (checkprint.Checked == false)
                    {
                        DeliveryReceiptFrm xct = new DeliveryReceiptFrm();

                        Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='DeliveryReceiptFrm'", "ImageLogo");
                        xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth, CultureInfo.InvariantCulture.NumberFormat), float.Parse(imageheight, CultureInfo.InvariantCulture.NumberFormat));

                        xct.xrcompanyname.Text = companyname;
                        xct.xrcaption1.Text = caption1;
                        xct.xrcaption2.Text = caption2;

                        xct.Landscape = false;
                        xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
                        xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
                        xct.xrdate.Text = DateTime.Now.ToShortDateString();
                        xct.xrorderno.Text = txtpono.Text;
                        xct.xrdevno.Text = deliveryNum;
                        xct.xcustname.Text = custname;
                        xct.xraddress.Text = address;
                        xct.xrsino.Text = txtsino.Text;

                        string fullname = Database.getSingleQuery("Users", "UserID = '" + requestedby + "'", "FullName");
                        xct.preparedby.Text = fullname;//xct.xrdeliveredby.Text = ap

                        this.gridView4.Columns["BarcodeNo"].Visible = false;
                        //this.gridView4.Columns["ActualQtyDelivered"].Visible = false;
                        this.gridView4.Columns["ProductNo"].Visible = false;

                        xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl4));
                        xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                        ReportPrintTool report = new ReportPrintTool(xct);

                        report.ShowRibbonPreviewDialog();
                    }
                    //-----------------------------------------END OF USED FOR PRINTING DR--------------------------------------------------------
                    //-----------------------------------------END OF MUST TRANSFER TO REPORTING--------------------------------------------------

                }
                else
                {
                    return;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ViewForDeliveryDetails_Load(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            updateInvoice();
            XtraMessageBox.Show("Invoice Number Successfully Updated!");
            this.Dispose();
        }
        void analyze(string spname, string pono, GridControl cont, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            cont.BeginUpdate();
            try
            {
                string sp = spname;
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmpono", pono);

                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                view.Columns.Clear();
                cont.DataSource = null;
                adapter.Fill(table);
                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cont.EndUpdate();
                con.Close();
            }
        }
        void printSalesInvoice(GridView view)
        {
            string custkey="",custname = "", custaddress = "", custterm = "";
            //get customername from purchaseordersummary
            custkey = Database.getSingleQuery("PurchaseOrderSummary", "PONumber='" + txtpono.Text + "'", "Customer");

            var row = Database.getMultipleQuery("Customers", "CustomerKey='" + custkey + "'", "CustomerName,CustomerAddress,Term");
            custname = row["CustomerName"].ToString();
            custaddress = row["CustomerAddress"].ToString();//Database.getSingleQuery("Customers", "CustomerName='" + custname + "'", "CustomerAddress");
            custterm = row["Term"].ToString();// Database.getSingleQuery("Customers", "CustomerName='" + custname + "'", "Term");
            
            Reporting.SalesInvoiceDexEx viewdet = new Reporting.SalesInvoiceDexEx();
            viewdet.Show();

            analyze("spview_SalesInvoice", txtpono.Text, viewdet.gridControl4, viewdet.gridView4);

            viewdet.txtpono.Text = txtpono.Text;
            double vatablesales = 0.0, vatexemptsale = 0.0, vatamount = 0.0, totalsales = 0.0, lessvat = 0.0, netofvat = 0.0, amountdue = 0.0, addvat = 0.0, vatsales = 0.0, totalamountdue = 0.0;
            for (int i = 0; i <= viewdet.gridView4.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(viewdet.gridView4.GetRowCellValue(i, "isVat").ToString()) == true)
                {
                    vatablesales += Convert.ToDouble(viewdet.gridView4.GetRowCellValue(i, "Amount").ToString());
                }
                if (Convert.ToBoolean(viewdet.gridView4.GetRowCellValue(i, "isVat").ToString()) == false)
                {
                    vatexemptsale += Convert.ToDouble(viewdet.gridView4.GetRowCellValue(i, "Amount").ToString());
                }
            }
            vatsales = Math.Round(vatablesales / 1.12, 2);
            vatamount = Math.Round(vatsales * 0.12, 2);
            totalsales = Math.Round(vatablesales + vatexemptsale, 2);
            lessvat = vatamount;
            netofvat = totalsales - vatamount;
            amountdue = netofvat;
            addvat = vatamount;
            totalamountdue = totalsales;

            viewdet.txtcustname.Text = custname;
            viewdet.txtcustaddress.Text = custaddress;
            viewdet.txtterm.Text = custterm;

            viewdet.txtvatablesale.Text = vatsales.ToString();
            viewdet.txtvatexemptsale.Text = vatexemptsale.ToString();
            viewdet.txtvatamount.Text = vatamount.ToString();
            viewdet.txttotalsales.Text = totalsales.ToString();
            viewdet.txtlessvat.Text = lessvat.ToString();
            viewdet.txtamountnetofvat.Text = netofvat.ToString();
            viewdet.txtamountdue.Text = amountdue.ToString();
            viewdet.txtaddvat.Text = addvat.ToString();
            viewdet.txttotalamountdue.Text = totalamountdue.ToString();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            printSalesInvoice(gridView4);
        }
    }
}
