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

namespace SalesInventorySystem.Barcode
{
    public partial class BarcodePrintingOptions : Form
    {
        public BarcodePrintingOptions()
        {
            InitializeComponent();
        }

        private void BarcodePrintingOptions_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    printNormalBarcode();
                }
                else
                {
                    printExpandedBarcode();
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void printNormalBarcode()
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblprodtype.Text = "MikeShit";
            bprint.lbltotalkilos.Text ="12.123";
            bprint.xrBarCode2.Text = "1009910015122340001";//productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            //report.ShowRibbonPreviewDialog();
            report.Print();
        }

        void printExpandedBarcode()
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblprodtype.Text = "MikeShit";
            bprint.lbltotalkilos.Text = "12.123";
            bprint.xrBarCode2.Text = "100990001";//productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            //report.ShowRibbonPreviewDialog();
            report.Print();
            //Barcode.BarcodePrintingExpanded bprint = new Barcode.BarcodePrintingExpanded();
            //bprint.lblprodtype.Text = HOForms.AddPrimalCutInventory.prodprint;
            //bprint.lbltotalkilos.Text = HOForms.AddPrimalCutInventory.weightprint;
            //bprint.xrBarCode2.Text = HOForms.AddPrimalCutInventory.barcodeprint;//productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            //bprint.xrBarCode2.Text = HOForms.AddPrimalCutInventory.barcodeprint;
            //ReportPrintTool report = new ReportPrintTool(bprint);
            //report.Print();
        }

        private void BarcodePrintingOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }
    }
}
