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
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.Reporting
{
    public partial class PurchaseOrderRepDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string supplierid, suppliername, dateorder, pono, approvedby, preparedby;
        public PurchaseOrderRepDevEx()
        {
            InitializeComponent();
        }

        void submit()
        {
            Database.display("SELECT * FROM view_POSUMMARYREP WHERE CAST(DateOrder as date) between '" + dateFrom.Text + "' and '" + dateTo.Text + "' ", gridControl1, gridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            supplierid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            //suppliername = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierName").ToString();
            dateorder = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateOrder").ToString();
            pono = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            preparedby = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderedBy").ToString();
            approvedby = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ApprovedBy").ToString();

            Reporting.PurchaseOrderRepDetailsDevEx purchdet = new PurchaseOrderRepDetailsDevEx();
            Database.display("SELECT * FROM view_PODETAILS WHERE ShipmentNo='" + pono + "' and SupplierID='" + supplierid + "'", purchdet.gridControl1, purchdet.gridView1);
            purchdet.ShowDialog(this);
       }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            submit();
        }
    }
}