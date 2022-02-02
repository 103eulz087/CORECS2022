using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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

namespace SalesInventorySystem.Reporting
{
    public partial class DeliveryDetailsReportsFrm : Form
    {
        public DeliveryDetailsReportsFrm()
        {
            InitializeComponent();
        }

        private void DeliveryDetailsReportsFrm_Load(object sender, EventArgs e)
        {
            //display();
        }

        void display()
        {
           // Database.displayLocalGrid("SELECT * FROM view_DeliveryDetailsReportSummary WHERE PONumber='" + Reporting.DeliveryReportsFrm.ponum + "'", dataGridView1);
            if (radsummaryview.Checked == true && radsts.Checked == true)
            {
                //use analyze
                analyze("spview_DeliveryReports","STS",txtpo.Text, gridControl1, gridView1);
            }
            else if (radsummaryview.Checked == true && radsalesorder.Checked == true)
            {
                //use analyze
                analyze("spview_DeliveryReports", "SALES", txtpo.Text, gridControl1, gridView1);
            }
            else if (raddetailedview.Checked == true && radsts.Checked == true)
            {
                Database.display("SELECT * FROM view_DeliveryReciept WHERE PONumber='" + txtpo.Text + "'", gridControl1, gridView1);
            }
            else if (raddetailedview.Checked == true && radsalesorder.Checked == true)
            {
                Database.display("SELECT * FROM view_DeliveryReciept WHERE PONumber='" + txtpo.Text + "'", gridControl1, gridView1);
            }
        }


        private void simpleButton10_Click(object sender, EventArgs e)
        {
           
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.StockOrderRep xct = new DevExReportTemplate.StockOrderRep();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //   DateTime dt = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString());
                //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 10);
                string branchname = Branch.getBranchName(txtbrcode.Text);
                string branchaddress = Branch.getBranchAddress(txtbrcode.Text);
                string requestedby = Database.getSingleQuery("TransferOrderSummary", "PONumber='" + txtpo.Text + "' ", "RequestedBy");
                
                xct.xrbranchname.Text = branchname;
                xct.xrdate.Text = Convert.ToDateTime(txteffectivitydate.Text).ToShortDateString() ; //Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateProcessed").ToString()).ToShortDateString();dt.ToShortDateString();
                xct.xrrequestedby.Text = requestedby;
                xct.xrpono.Text = txtpo.Text;
                xct.xrbranchaddress.Text = branchaddress;
                xct.xrpreparedby.Text = Login.Fullname;

                if (raddetailedview.Checked == true)
                {
                    //this.gridView1.Columns["BranchCode"].Visible = false;
                    this.gridView1.Columns["PONumber"].Visible = false;
                    this.gridView1.Columns["DeliveryNo"].Visible = false; 
                    this.gridView1.Columns["ProductNo"].Visible = false;

                    //this.gridView1.Columns["DateProcessed"].Visible = false;
                    this.gridView1.Columns["isVat"].Visible = false;
                    this.gridView1.Columns["Status"].Visible = false;
                    //this.gridView1.Columns["ProcessedBy"].Visible = false;
                }

                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();
            //}
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        void analyze(string spname,string type, string pono, GridControl cont, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            cont.BeginUpdate();
            try
            {
                //spname = "spview_DeliveryReports";
                string sp = spname;
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmpono", pono);
                com.Parameters.AddWithValue("@parmtype", type);
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
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (raddetailedview.Checked == true)
            //{
            //    Database.display("SELECT * FROM view_DeliveryDetailsReportSummary WHERE PONumber='" + txtpo.Text + "'", gridControl1, gridView1);
            //}
            //else
            //{
            //    Database.display("SELECT COUNT(ProductName) as TotalBox,SUM(QtyDelivered) as Quantity,ProductName,'' as UnitPrice,'' as Amount FROM view_DeliveryDetailsReportSummary WHERE PONumber='" + txtpo.Text + "' GROUP BY ProductName", gridControl1, gridView1);
            //}
            display();
        }

        private void raddetailedview_CheckedChanged(object sender, EventArgs e)
        {
            //if (raddetailedview.Checked == true)
            //{
            //    Database.display("SELECT * FROM view_DeliveryDetailsReportSummary WHERE PONumber='" + txtpo.Text + "'", gridControl1, gridView1);
            //}
            //else
            //{
            //    Database.display("SELECT COUNT(ProductName) as TotalBox,SUM(QtyDelivered) as Quantity,ProductName,'' as UnitPrice,'' as Amount FROM view_DeliveryDetailsReportSummary WHERE PONumber='" + txtpo.Text + "' GROUP BY ProductName", gridControl1, gridView1);
            //}
            display();
        }
    }
}
