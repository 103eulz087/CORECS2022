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

namespace SalesInventorySystem.Sticker
{
    public partial class PrinterSettings : Form
    {
        public PrinterSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sticker.Sticker5x2 bprint = new Sticker.Sticker5x2();
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.ShowRibbonPreviewDialog();
            //report.Print();
        }
    }
}
