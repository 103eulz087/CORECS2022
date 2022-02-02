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
    public partial class ARFreight : DevExpress.XtraEditors.XtraForm
    {
        public ARFreight()
        {
            InitializeComponent();
            gridView1.MasterRowExpanded += new CustomMasterRowEventHandler(gridView1_MasterRowExpanded);
            gridView1.MasterRowGetLevelDefaultView += new MasterRowGetLevelDefaultViewEventHandler(gridView1_MasterRowGetLevelDefaultView);
        }
        void gridView1_MasterRowGetLevelDefaultView(object sender, MasterRowGetLevelDefaultViewEventArgs e)
        {
            e.DefaultView = sender as GridView;
        }

        void gridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView view = sender as GridView;
            ((GridView)view.GetVisibleDetailView(e.RowHandle)).BestFitColumns();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
           
        }

        void display()
        {
            //Database.display("SELECT * FROM ClientChargeSalesSummary WHERE DateAdded Between '"+txtdatefrom.Text+"' AND '"+txtdateto.Text+"'",gridControl1,gridView1);
            Database.GridMasterDetail("SELECT * FROM view_ClientChargeSalesSummary WHERE DateAdded Between '" + txtdatefrom.Text + "' AND '" + txtdateto.Text + "'", "Select * FROM view_ClientChargeSalesDetails", "ClientChargeSalesSummary", "ClientChargeSalesDetails", "ChargeNo", "ChargeNo", "ClientChargeDetails", gridControl1, "");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void reprintDebitMemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string refno1 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            //string brcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();

            HOFormsDevEx.DebitMemoDevEx viewdet = new HOFormsDevEx.DebitMemoDevEx();
            viewdet.Show();
            viewdet.txtpono.Text = refno1;
            //viewdet.txtbrcode.Text = brcode;
            Database.display("SELECT Description,Amount FROM view_ClientChargeSalesDetails WHERE PONumber='" + viewdet.txtpono.Text + "' ", viewdet.gridControl4, viewdet.gridView4);
        }

        private void ARFreight_Load(object sender, EventArgs e)
        {
            Database.GridMasterDetail("SELECT * FROM view_ClientChargeSalesSummary WHERE DateAdded Between '" + DateTime.Now.ToShortDateString() + "' AND '" + DateTime.Now.ToShortDateString() + "'", "Select * FROM view_ClientChargeSalesDetails", "ClientChargeSalesSummary", "ClientChargeSalesDetails", "ChargeNo", "ChargeNo", "ClientChargeDetails", gridControl1, "");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Balance");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "AmountPaid");
        }

    }
}