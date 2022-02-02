using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.DevExReportTemplate
{
    public partial class StorageReceivingForm : DevExpress.XtraReports.UI.XtraReport
    {
        int i=0;
        public StorageReceivingForm()
        {
            InitializeComponent();
        }

        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //i++;
            //if (i == 1) e.Cancel = true;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            i++;
            if (i != 1) e.Cancel = true;
        }
    }
}
