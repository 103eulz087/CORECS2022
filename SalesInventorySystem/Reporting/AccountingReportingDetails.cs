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
    public partial class AccountingReportingDetails : DevExpress.XtraEditors.XtraForm
    {
        public AccountingReportingDetails()
        {
            InitializeComponent();
        }

        private void AccountingReportingDetails_Load(object sender, EventArgs e)
        {

        }
        void exporttoexcel(GridView view, string title)
        {

            string filepath = "C:\\MyFiles\\";
            Classes.Utilities.createDirectoryFolder(filepath);
            string filename = title + ".xls";
            string file = filepath + filename;
            view.ExportToXls(file);
            XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string filename = "COST_SALES_ATTACHMENT_" + lblsalesdate.Text.Replace(@"/", "-");
            exporttoexcel(gridView1, filename);
        }
    }
}