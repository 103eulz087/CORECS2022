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

namespace SalesInventorySystem.Reporting.BIR
{
    public partial class E_SalesReports : DevExpress.XtraEditors.XtraForm
    {
        public E_SalesReports()
        {
            InitializeComponent();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\BIRReports\\";
            string filename = "ESalesReport_" + DateTime.Now.ToShortDateString().Replace(@"/", "-");
            HelperFunction.exporttoexcel(gridView1, filename, filepath);
        }

        private void E_SalesReports_Load(object sender, EventArgs e)
        {

        }
    }
}