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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.Reporting
{
    public partial class DeliverySummaryReports : DevExpress.XtraEditors.XtraForm
    {
        public DeliverySummaryReports()
        {
            InitializeComponent();
        }
        void exporttoexcel(GridView view, string title)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {

                string filepath = "C:\\MyFiles\\";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string filename = "DELIVERYSUMMARY" + DateTime.Now.ToShortDateString().Replace(@"/", "-");
            exporttoexcel(gridView1, filename);
        }
    }
}