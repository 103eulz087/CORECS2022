using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSSettingTools : Form
    {
        public POSSettingTools()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                //   if (form.GetType() == typeof(HOForms.CustomersFrm))
                if (form.GetType() == typeof(HOFormsDevEx.CustomersInfoDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.CustomersInfoDevEx cstfmrs = new HOFormsDevEx.CustomersInfoDevEx();
            cstfmrs.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ProductCategory))
                {
                    form.Activate();
                    return;
                }
            }
            ProductCategory prodcat = new ProductCategory();
            prodcat.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.ProductSettings))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.ProductSettings prodsets = new HOForms.ProductSettings();
            prodsets.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.ClientAccounts))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.ClientAccounts pcusatfsmr = new HOForms.ClientAccounts();
            pcusatfsmr.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSDevEx.POSXReadReportDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            POSDevEx.POSXReadReportDevEx viewinv = new POSDevEx.POSXReadReportDevEx();
            viewinv.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Branches.SyncPrize))
                {
                    form.Activate();
                    return;
                }
            }
            Branches.SyncPrize pcusatfsmr = new Branches.SyncPrize();
            pcusatfsmr.ShowDialog(this);
        }
    }
}
