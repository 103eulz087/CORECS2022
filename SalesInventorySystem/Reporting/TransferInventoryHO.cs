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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.Reporting
{
    public partial class TransferInventoryHO : DevExpress.XtraEditors.XtraForm
    {
        public TransferInventoryHO()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            extract();
        }

        void extract()
        {
            Database.display($"SELECT * FROM dbo.TransferBatch WHERE CreatedAt >= '{datefrom.Text}' and CreatedAt <= '{dateto.Text}' and Status='Committed' ORDER BY BatchNo DESC",gridControl1, gridView1);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reporting.TransferInventoryDetailsHO repo = new TransferInventoryDetailsHO();
            Database.display($"SELECT * FROM dbo.funcview_TransferInventoryByBarcode('{gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BatchNo").ToString()}') ORDER BY SequenceNo ASC ", repo.gridControl1, repo.gridView1);

            GridView view = repo.gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
            //podetails.gridView1.Columns["SeqNo"].Visible = false;
            repo.gridView1.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Qty";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = repo.gridView1.Columns["Qty"];
            repo.gridView1.GroupSummary.Add(ite);

            // Count for another field (e.g., ItemCode)
            GridGroupSummaryItem countItemCode = new GridGroupSummaryItem();
            countItemCode.FieldName = "SequenceNo"; // field to count
            countItemCode.SummaryType = DevExpress.Data.SummaryItemType.Count;
            countItemCode.ShowInGroupColumnFooter = repo.gridView1.Columns["SequenceNo"];
            countItemCode.DisplayFormat = "{0}"; // optional formatting
            repo.gridView1.GroupSummary.Add(countItemCode);


            repo.gridView1.Focus();
            repo.ShowDialog(this);
        }
    }
}