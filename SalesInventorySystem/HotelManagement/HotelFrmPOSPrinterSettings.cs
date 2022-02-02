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
using System.Drawing.Printing;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmPOSPrinterSettings : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmPOSPrinterSettings()
        {
            InitializeComponent();
        }

        private void HotelFrmPOSPrinterSettings_Load(object sender, EventArgs e)
        {
            GetAllPrinterList();
        }


        private void GetAllPrinterList()
        {
            foreach (string printname in PrinterSettings.InstalledPrinters)
            {
                comboBoxEdit1.Properties.Items.Add(printname);
            }
        }
    }
}