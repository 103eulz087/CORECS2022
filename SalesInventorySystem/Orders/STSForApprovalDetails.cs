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

namespace SalesInventorySystem.Orders
{
    public partial class STSForApprovalDetails : DevExpress.XtraEditors.XtraForm
    {
        public static string refernceno;
        public static bool isdone = false;
        public STSForApprovalDetails()
        {
            InitializeComponent();
        }

        private void STSForApprovalDetails_Load(object sender, EventArgs e)
        {
            if (POForApprovalSTS.menu == "approvedrequest")
            {
                btnadd.Visible = false;
                simpleButton9.Visible = false;
            }
            else
            {
                btnadd.Visible = true;
                simpleButton9.Visible = true;
            }
        }

        private void btnapprove_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                refernceno = POForApproval.refno;//gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
                Database.ExecuteQuery("UPDATE TransferOrderSummary SET Status='APPROVED',Remarks='" + richTextBox1.Text.Trim() + "',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                isdone = true;
                this.Close();
            }
        }

        private void btnreject_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Reject this Transaction?", "Rejected!!");
                if (ok)
                {
                    //refernceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
                    Database.ExecuteQuery("UPDATE TransferOrderSummary SET Status='REJECTED',Remarks='" + richTextBox1.Text.Trim() + "',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                    this.Close();
                }
            }
        }
        void printSts()
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();
           
            gridView1.FocusedRowHandle = 2;
            DevExReportTemplate.StockOrderRep xct = new DevExReportTemplate.StockOrderRep();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));

            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
          
            var rowz = Database.getMultipleQuery("TransferOrderSummary", "PONumber='" + txtpono.Text + "' ", "RequestedBy,InitiatingBranch,BranchCode,EffectivityDate");

            string requestedby = rowz["RequestedBy"].ToString();
            string brcode = rowz["BranchCode"].ToString();
            string InitiatingBranch = rowz["InitiatingBranch"].ToString();
            string effecitivitydate = rowz["EffectivityDate"].ToString();
            string branchname = Database.getSingleQuery("Branches", "BranchCode='" + InitiatingBranch + "'", "BranchName");
            string branchaddress = Database.getSingleQuery("Branches", "BranchCode='" + InitiatingBranch + "'", "Address");


            xct.xrbranchname.Text = branchname;
            xct.xrbranchaddress.Text = branchaddress;
            xct.xrdate.Text = Convert.ToDateTime(effecitivitydate).ToShortDateString();//dt.ToShortDateString();
            xct.xrrequestedby.Text = requestedby;//POForApproval.requestedBy;
            xct.xrpono.Text = txtpono.Text;
            xct.xrpreparedby.Text = Login.Fullname;

            //this.gridView1.Columns["BranchCode"].Visible = false;
            this.gridView1.Columns["PONumber"].Visible = false;
            this.gridView1.Columns["Category"].Visible = false;
            this.gridView1.Columns["ProductCode"].Visible = false;
            //this.gridView1.Columns["Units"].Visible = false;
            //this.gridView1.Columns["DateRequested"].Visible = false;
            //this.gridView1.Columns["EffectivityDate"].Visible = false; 
            //this.gridView1.Columns["SeqNo"].Visible = false;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 9);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void printStsDelivered()
        {
            DevExReportTemplate.StockOrderRep xct = new DevExReportTemplate.StockOrderRep();
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            //   DateTime dt = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString());
            //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 10);
            xct.xrbranchname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            xct.xrdate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString();//dt.ToShortDateString();
            xct.xrrequestedby.Text = POForApproval.requestedBy;
            xct.xrpono.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();

            //this.gridView1.Columns["BranchCode"].Visible = false;
            this.gridView1.Columns["PONumber"].Visible = false;
            this.gridView1.Columns["Category"].Visible = false;
            this.gridView1.Columns["ProductCode"].Visible = false;
            this.gridView1.Columns["Units"].Visible = false;
            //this.gridView1.Columns["DateRequested"].Visible = false;
            //this.gridView1.Columns["EffectivityDate"].Visible = false; 
            //this.gridView1.Columns["SeqNo"].Visible = false;


            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printSts();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                refernceno = POForApproval.refno;//gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
                Database.ExecuteQuery("UPDATE TransferOrderSummary SET Status='APPROVED',Remarks='" + richTextBox1.Text.Trim() + "',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                isdone = true;
                this.Close();
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                XtraMessageBox.Show("Please Input Remarks");
            }
            else
            {
                DateTime dt = DateTime.Now;
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Reject this Transaction?", "Rejected!!");
                if (ok)
                {
                    //refernceno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
                    Database.ExecuteQuery("UPDATE TransferOrderSummary SET Status='REJECTED',Remarks='" + richTextBox1.Text.Trim() + "',ApprovedBy='" + Login.Fullname + "',DateApproved='" + String.Format("{0:MM/dd/yyyy}", dt) + "' WHERE PONumber='" + txtpono.Text + "'", "Success");
                    this.Close();
                }
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            printSts();
        }
    }
}