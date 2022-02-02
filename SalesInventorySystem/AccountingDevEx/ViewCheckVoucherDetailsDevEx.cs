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
using System.Data.SqlClient;

namespace SalesInventorySystem.AccountingDevEx
{
    public partial class ViewCheckVoucherDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public ViewCheckVoucherDetailsDevEx()
        {
            InitializeComponent();
        }

        void populate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spr_ReprintCheckVoucher";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmsupplierid", ViewCheckVoucherDevEx.supplierid);
            com.Parameters.AddWithValue("@voucherid", ViewCheckVoucherDevEx.vouchid);
            com.Parameters.AddWithValue("@parmvouchertype", ViewCheckVoucherDevEx.vouchertype);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            //com.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "AmountPaid");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "ActualCost");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Discount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "EWTAmount");
            con.Close();
        }

        void reprint()
        {
            populate();
            
            //Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "OffsetAmount");
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                populate();
                var rows = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher'","Heading,ImageWidth,ImageHeight,Caption1,Caption2");
              
                string companyname = rows["Heading"].ToString();  
                string imagewidth = rows["ImageWidth"].ToString(); 
                string imageheight = rows["ImageHeight"].ToString();  
                string caption1 = rows["Caption1"].ToString(); 
                string caption2 = rows["Caption2"].ToString(); 

                DevExReportTemplate.CheckVoucher xct = new DevExReportTemplate.CheckVoucher();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='CheckVoucher'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
                xct.xrcheckno.Text = AccountingDevEx.ViewCheckVoucherDevEx.checkno;
                xct.xrcheckdate.Text = Convert.ToDateTime(AccountingDevEx.ViewCheckVoucherDevEx.checkdate).ToShortDateString();
                xct.xrpaidto.Text = AccountingDevEx.ViewCheckVoucherDevEx.paidto;
                xct.xrparticular.Text = AccountingDevEx.ViewCheckVoucherDevEx.pariculars;
                xct.xramount.Text = AccountingDevEx.ViewCheckVoucherDevEx.amount;

                string str = Classes.DecimalToWordExtension.ToWords(Convert.ToDecimal(xct.xramount.Text));
                //string str = Classes.CurrencyConversion.ConvertToWords(xct.xramount.Text);
                //string str = HelperFunction.NumWords(amountinwords1);
                xct.xramountinwords.Text = str.ToUpper();
                xct.xrpreparedby.Text = AccountingDevEx.ViewCheckVoucherDevEx.preparedby;
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

        private void ViewCheckVoucherDetailsDevEx_Load(object sender, EventArgs e)
        {

        }
    }
}