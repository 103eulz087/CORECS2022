using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace SalesInventorySystem
{
    public partial class RibbonForm2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        
        public RibbonForm2()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Samples.SampleBonus adhipma = new Samples.SampleBonus();
            adhipma.Show();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Samples.SampleAccounts adhipma = new Samples.SampleAccounts();
            adhipma.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Samples.SampleCashierTransactions adhipma = new Samples.SampleCashierTransactions();
            adhipma.Show();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            Samples.SampleSalesSummary adhipma = new Samples.SampleSalesSummary();
            adhipma.Show();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}