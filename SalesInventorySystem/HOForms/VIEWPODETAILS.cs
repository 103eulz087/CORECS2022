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

namespace SalesInventorySystem.HOForms
{
    public partial class VIEWPODETAILS : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public VIEWPODETAILS()
        {
            InitializeComponent();
        }

        void print()
        {
            string suppliername = Classes.Suppliers.getSupplierName(txtsupplierid.Text);
            string invoiceno = Database.getSingleQuery("APACCOUNTS", "ShipmentNo='" + txtshipmentno.Text + "' AND SupplierID='" + txtsupplierid.Text + "' ", "InvoiceNo");
            string branchname = Database.getSingleQuery("Branches", "BranchCode='" + txtbrcode.Text + "'", "BranchName");

            var row = Database.getMultipleQuery("POSUMMARY", "ShipmentNo='" + txtshipmentno.Text + "' " +
                "AND SupplierID='" + txtsupplierid.Text + "' " +
                "AND OrderType='" + txtordertype.Text + "' ", "TargetDate,Remarks,DateOrder,ShipmentNo,OrderedBy,ApprovedBy");

            string targetdate = row["TargetDate"].ToString();
            string remakrs = row["Remarks"].ToString();
            string dateorder = row["DateOrder"].ToString();
            string ponumber = row["ShipmentNo"].ToString();
            string orderby = row["OrderedBy"].ToString();
            string ApprovedBy = row["ApprovedBy"].ToString();

            var rows = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='PurchaseOrderRep'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");
            string companyname = rows["Heading"].ToString();
            string imagewidth = rows["ImageWidth"].ToString();
            string imageheight = rows["ImageHeight"].ToString();
            string caption1 = rows["Caption1"].ToString();
            string caption2 = rows["Caption2"].ToString();

            DevExReportTemplate.PurchaseOrderRepSupplier asdhk = new DevExReportTemplate.PurchaseOrderRepSupplier();

            Classes.Utilities.GetImageDevEx(asdhk.xrPictureBox1, "ReportHeaderSettings", "ReportName='PurchaseOrderRep'", "ImageLogo");
            asdhk.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            asdhk.xrcompanyname.Text = companyname;
            asdhk.xraddress1.Text = caption1;
            asdhk.xraddress2.Text = caption2;

            asdhk.lblcustname.Text = suppliername;
            asdhk.lblinvoiceno.Text = invoiceno;
            asdhk.xrdevdate.Text = Convert.ToDateTime(targetdate).ToShortDateString();
            asdhk.xrlblremarks.Text = remakrs;
            asdhk.lbldate.Text = Convert.ToDateTime(dateorder).ToShortDateString();
            asdhk.lblorderno.Text = ponumber;
            asdhk.xrbranch.Text = branchname;
            asdhk.xrorderedby.Text = orderby;
            asdhk.approvedby.Text = ApprovedBy;
            asdhk.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControl2));
            asdhk.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(asdhk);
            report.ShowRibbonPreviewDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool checkifApproved = Database.checkifExist($"SELECT TOP(1) Status " +
                $"FROM POSUMMARY " +
                $"WHERE ShipmentNo='{txtshipmentno.Text}' " +
                $"AND Status='FOR APPROVAL'");
            if(checkifApproved)
            {
                XtraMessageBox.Show("You must Approved the P.O First before Printing!..");
            }
            else
            {
                print();
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool checkifexists = Database.checkifExist("SELECT TOP(1) Status FROM dbo.POSUMMARY WHERE ShipmentNo='" + txtshipmentno.Text + "' and Status='FOR DELIVERY' ");
            if (!checkifexists)
            {
                Database.ExecuteQuery("UPDATE dbo.POSUMMARY " +
                    "SET ApprovedBy='" + Login.Fullname + "' " +
                    ",ApprovedDate='" + DateTime.Now.ToString() + "' " +
                    ",Status=case when OrderType='P' then 'FOR DELIVERY' when OrderType='S' then 'FOR CONFIRMATION' else ' ' end " +
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "'", "Approved");
                isdone = true;
                this.Close();
            }else
            {
                XtraMessageBox.Show("You Already Approved this Request");
                return;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            bool checkifexists = Database.checkifExist("SELECT TOP(1) Status FROM dbo.POSUMMARY WHERE ShipmentNo='" + txtshipmentno.Text + "' and Status='CANCELLED' ");
            if (!checkifexists)
            {
                Database.ExecuteQuery("UPDATE dbo.POSUMMARY Set Status='CANCELLED' " +
                    ",ApprovedDate='" + DateTime.Now.ToShortDateString() + "'" +
                    ",ApprovedBy='" + Login.isglobalUserID + "' " +
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "' ", "DisApproved!");
                isdone = true;
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("You Already Cancelled this Request");
                return;
            }
        }

        private void VIEWPODETAILS_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Login.isglobalApprover) == true)
            {
                simpleButton2.Visible = true;
                simpleButton3.Visible = true;
            }
        }
    }
}