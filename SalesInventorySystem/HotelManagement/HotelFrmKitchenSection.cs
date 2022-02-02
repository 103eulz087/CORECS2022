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
using System.Management;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmKitchenSection : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmKitchenSection()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void GetAllPrinterList()
        {
            //foreach (string printname in PrinterSettings.InstalledPrinters)
            //{
            //    comboBoxEdit1.Properties.Items.Add(printname);
            //}
            System.Management.ObjectQuery oquery =
              new System.Management.ObjectQuery("SELECT * FROM Win32_Printer");

            System.Management.ManagementObjectSearcher mosearcher =
                new System.Management.ManagementObjectSearcher(oquery);

            System.Management.ManagementObjectCollection moc = mosearcher.Get();

            foreach (ManagementObject mo in moc)
            {
                System.Management.PropertyDataCollection pdc = mo.Properties;
                foreach (System.Management.PropertyData pd in pdc)
                {
                    if ((bool)mo["Network"])
                    {
                        comboBoxEdit1.Properties.Items.Add(mo[pd.Name]);
                    }
                }
            }

        }

        private void HotelFrmKitchenSection_Load(object sender, EventArgs e)
        {
            GetAllPrinterList();
        }
    }
}