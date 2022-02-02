using DevExpress.XtraEditors;
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

namespace SalesInventorySystem.Accounting
{
    public partial class ViewCheckVoucherDetails : Form
    {
        public ViewCheckVoucherDetails()
        {
            InitializeComponent();
        }

        private void ViewCheckVoucherDetails_Load(object sender, EventArgs e)
        {
            
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                string companyname = row["Heading"].ToString();
                string imagewidth = row["ImageWidth"].ToString();
                string imageheight = row["ImageHeight"].ToString();
                string caption1 = row["Caption1"].ToString();
                string caption2 = row["Caption2"].ToString();

                DevExReportTemplate.CheckVoucher xct = new DevExReportTemplate.CheckVoucher();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='CheckVoucher'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
                xct.xrcheckno.Text = ViewCheckVoucher.checkno;
                xct.xrcheckdate.Text = Convert.ToDateTime(ViewCheckVoucher.checkdate).ToShortDateString();
                xct.xrpaidto.Text = ViewCheckVoucher.paidto;
                xct.xrparticular.Text = ViewCheckVoucher.pariculars;
                xct.xramount.Text = ViewCheckVoucher.amount;
                //decimal amountinwords1 = 0;
                //amountinwords1 = Convert.ToDecimal(ViewCheckVoucher.amount);
                //long.Parse(Convert.ToInt64(txtamount.Text));
                string str = Classes.CurrencyConversion.ConvertToWords(xct.xramount.Text);
                //string str = HelperFunction.NumWords(amountinwords1);
                xct.xramountinwords.Text = str.ToUpper();
                xct.xrpreparedby.Text = ViewCheckVoucher.preparedby;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();
            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
