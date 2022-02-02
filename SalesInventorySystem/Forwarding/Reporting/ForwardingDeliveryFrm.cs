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

namespace SalesInventorySystem.Forwarding.Reporting
{
    public partial class ForwardingDeliveryFrm : DevExpress.XtraEditors.XtraForm
    {
        public ForwardingDeliveryFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                Database.display("select TripID,DateAdded,ContainerNo,Consignee,ShippingLines,Numdays,Rate,TotalAmount FROM Monitoring WHERE Consignee='" + searchLookUpEdit1.Text + "' AND DateAdded between '" + datefrompending.Text + "' and '" + datetopending.Text + "' And Balance > 0", gridControl1, gridView1, Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
                gridView1.Columns["TripID"].Visible = false;
            }
            else
            {
                Database.display("select TripNo as TripID,DateAdded,Consignee,Amount,TotalKilos,TotalHours,OverWeightCharge,OvertimeCharge FROM TripTicketMaster WHERE Consignee='" + searchLookUpEdit1.Text + "' AND DateAdded between '" + datefrompending.Text + "' and '" + datetopending.Text + "' And Balance > 0", gridControl1, gridView1, Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
                gridView1.Columns["TripID"].Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for(int i=0;i<=gridView1.RowCount-1;i++)
            {
                if (radioButton1.Checked == true)
                {
                    Database.ExecuteLocalQuery("UPDATE Monitoring Set InvoiceNo='" + txtinvoiceno.Text + "' WHERE TripID='" + gridView1.GetRowCellValue(i, "TripID").ToString() + "'", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
                }
                else
                {
                    Database.ExecuteLocalQuery("UPDATE TripTicketMaster Set InvoiceNo='" + txtinvoiceno.Text + "' WHERE TripNo='" + gridView1.GetRowCellValue(i, "TripID").ToString() + "'", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
                }
            }
            Forwarding.ReportingReports.DeliveryReceipt xct = new Forwarding.ReportingReports.DeliveryReceipt();
            xct.xrinvoiceno.Text = txtinvoiceno.Text;
            xct.xrdategenerate.Text = DateTime.Now.ToShortDateString();
            string custname = Database.getSingleQuery("Customers", "CustomerID='" + searchLookUpEdit1.Text + "'", "CustomerName");
            string address = Database.getSingleQuery("Customers", "CustomerID='" + searchLookUpEdit1.Text + "'", "CustomerAddress");
            xct.xrcustname.Text = custname;
            xct.xraddress.Text = address;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void ForwardingDeliveryFrm_Load(object sender, EventArgs e)
        {
            loadCustomers();
        }
        void loadCustomers()
        {
            Database.displaySearchlookupEdit("SELECT distinct CustomerID,CustomerName FROM Customers", searchLookUpEdit1, "CustomerID", "CustomerID");
        }
    }
}