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
    public partial class CollectionList : DevExpress.XtraEditors.XtraForm
    {
        public CollectionList()
        {
            InitializeComponent();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            Database.display("SELECT * FROM view_CollectionListReports WHERE DueDate='" + datefrom.Text + "' and AccountOfficer = '"+txtao.Text+"' ", gridControl1, gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DevExReportTemplate.CollectionList xct = new DevExReportTemplate.CollectionList();
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.xrcollectiondate.Text = datefrom.Text;
            xct.xrpreparedby.Text = Login.Fullname;
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        void displayAO()
        {
            Database.displaySearchlookupEdit("SELECT * FROM AccountOfficers", txtao, "AccountOfficerName", "AccountOfficerName");
        }
        private void CollectionList_Load(object sender, EventArgs e)
        {
            displayAO();
        }
    }
}