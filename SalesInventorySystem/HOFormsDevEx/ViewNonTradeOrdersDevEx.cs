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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewNonTradeOrdersDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string shipmentno,tabtype;
        public ViewNonTradeOrdersDevEx()
        {
            InitializeComponent();
        }

        private void ViewNonTradeOrdersDevEx_Load(object sender, EventArgs e)
        {
            filtertab();
        }

        private void filtertab()
        {
            if (tabControl1.SelectedTab.Equals(forapproval))
            {
                Database.display("SELECT * FROM view_ShipmentOrderNonTrade WHERE Status='FOR APPROVAL' and NonTrade='1'", gridControl2, gridView2);
            }
            else if (tabControl1.SelectedTab.Equals(approved))
            {
                Database.display("SELECT * FROM view_ShipmentOrderNonTrade WHERE Status='Updated' and NonTrade='1'", gridControl1, gridView1);
            }
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            HOFormsDevEx.ViewNonTradeOrderDetailsDevEx voerord = new HOFormsDevEx.ViewNonTradeOrderDetailsDevEx();
            voerord.Show();
            Database.display("Select * FROM view_OrderDetailsNonTrade WHERE ShipmentNo='" + shipmentno + "'", voerord.gridControl2,voerord.gridView2);
            if(HOFormsDevEx.ViewNonTradeOrderDetailsDevEx.isdone==true)
            {
                filtertab();
                HOFormsDevEx.ViewNonTradeOrderDetailsDevEx.isdone = false;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            tabtype = tabControl1.SelectedTab.Text;
            shipmentno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            HOFormsDevEx.ViewNonTradeOrderDetailsDevEx voerord = new HOFormsDevEx.ViewNonTradeOrderDetailsDevEx();
            voerord.Show();
            Database.display("Select * FROM view_OrderDetailsNonTrade WHERE ShipmentNo='" + shipmentno + "'", voerord.gridControl2, voerord.gridView2);
            if (HOFormsDevEx.ViewNonTradeOrderDetailsDevEx.isdone == true)
            {
                filtertab();
                HOFormsDevEx.ViewNonTradeOrderDetailsDevEx.isdone = false;
            }
        }
    }
}