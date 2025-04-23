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
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.Reporting
{
    public partial class SalesInvoiceDexEx : DevExpress.XtraEditors.XtraForm
    {
        public SalesInvoiceDexEx()
        {
            InitializeComponent();
            //this.gridView4.Columns["Quantity"].Width = 125;
            //this.gridView4.Columns["Unit"].Width = 75;
            //this.gridView4.Columns["ProductName"].Width = 300;
            //this.gridView4.Columns["Price"].Width = 150;
            //this.gridView4.Columns["Amount"].Width = 150;
        }

        private void SalesInvoiceDexEx_Load(object sender, EventArgs e)
        {

        }

        void print()
        {
            //string vatablesales, vatexemptsales, vatamount, addvat;
            string remarks = Database.getSingleQuery($"SELECT TOP(1) Remarks FROM dbo.TransactionChargeSales WHERE ReferenceNo='{txtpono.Text}' AND BranchCode='{Login.assignedBranch}'","Remarks");
            bool isOnetimeDiscount = Database.checkifExist($"SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='{txtpono.Text}' and isErrorCorrect=0 and BranchCode='{Login.assignedBranch}'");
            string controlno = "";
            if (isOnetimeDiscount==true)
            {
                controlno = Database.getSingleQuery($"SELECT TOP(1) DiscIDNo FROM dbo.SalesDiscount WHERE OrderNo='{txtpono.Text}' AND BranchCode='{Login.assignedBranch}'", "DiscIDNo");
            }
            this.gridView4.Columns["isVat"].Visible = false;

            DevExReportTemplate.SalesInvoice xct = new DevExReportTemplate.SalesInvoice();
            //this.gridView4.Columns["TotalBox"].Width = 5;
            this.gridView4.Columns["Quantity"].Width = 125;
            this.gridView4.Columns["Unit"].Width = 78;
            this.gridView4.Columns["ProductName"].Width = 300;
            this.gridView4.Columns["Price"].Width = 110;
            this.gridView4.Columns["Amount"].Width = 145;

            this.gridView4.Columns["Unit"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            xct.xrcustname.Text = txtcustname.Text;
            xct.xrcontrolno.Text = controlno;
            xct.xraddress.Text = txtcustaddress.Text;
            xct.xrbusinessstyle.Text = remarks;
            xct.xrtin.Text = txtcusttin.Text;

            string term = "", trm = "";
            term = txtterm.Text;
            if (term == "0")
            {
                trm = "C.O.D";
            }
            else
            {
                trm = term + " days";
            }
            xct.xrterms.Text = trm;
            xct.xrpreparedby.Text = Login.Fullname;
            //xct.xrdate.Text = String.Format(DateTime.Now.ToShortDateString();
           
            xct.xrdate.Text = String.Format("{0:MMMM dd yyyy}", DateTime.Now);

            xct.xrlblvatablesales.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtvatablesale.Text));
            xct.xrlblvatexemptsales.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtvatexemptsale.Text));
            //zero rated
            xct.xrlblzeroratedsales.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtzeroratedsale.Text));
            xct.xrlblvatamount.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtvatamount.Text));
            xct.xrlbltotalsales.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txttotalsales.Text));
            xct.xrlbllessvat.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtlessvat.Text));
            xct.xrlblnetofvat.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtamountnetofvat.Text));
            xct.xrlblamountdue.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtamountdue.Text));
            xct.xrlbladdvat.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txtaddvat.Text));
            xct.xrlbltotalamountdue.Text = String.Format("{0:0,0.00}", Convert.ToDouble(txttotalamountdue.Text));

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl4));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();

          
        }
     
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            print();
        }
    }
}