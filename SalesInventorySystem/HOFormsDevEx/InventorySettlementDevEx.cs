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
    public partial class InventorySettlementDevEx : DevExpress.XtraEditors.XtraForm
    {
        string devno, pono;
        public static string brcode;
        public InventorySettlementDevEx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void InventorySettlementDevEx_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            Database.display("SELECT * FROM view_InventoryForSettlement WHERE (TotalVarianceVat <> 0 OR TotalVarianceVatExempt <> 0) and isSettled=0 AND DateAdded >= '"+datefromforapproval.Text+"' and DateAdded <= '"+datetoforapproval.Text+"'", gridControl1,gridView1);
        }
        void displaySettled()
        {
            Database.display("SELECT * FROM view_SettledInventory WHERE isSettled=1 AND DateAdded >= '" + datefromsettled.Text + "' and DateAdded <= '" + datetosettled.Text + "'", gridControl2, gridView2);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1,e.Location);
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            HOFormsDevEx.InventorySettlementUpdate det = new InventorySettlementUpdate();
            det.Show();
            det.txtpono.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            det.txtdevno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            displaySettled();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            double totalvatexemptqtydeliv = 0.0, totalvatexemptqtyrcv=0.0,totalvatqtydeliv=0.0, totalvatqtyrcv=0.0;
            double totalvatexemptgain = 0.0, totalvatexemptloss = 0.0;
            //double totalcostvariancevat = 0.0, totalcostvariancevatexempt=0.0;
            double totalvatgain = 0.0, totalvatloss = 0.0;

            devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            pono = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            HOFormsDevEx.InventorySettlementDetailsDevEx det = new InventorySettlementDetailsDevEx();
            det.Show();
            Database.display("SELECT * FROM DeliveryDetails WHERE DeliveryNo='" + devno + "' AND PONumber='" + pono + "' and Variance <> 0 and isSettled=0", det.gridControl1, det.gridView1);

            totalvatexemptqtydeliv = Database.getTotalSummation2("DeliveryDetails", " DeliveryNo='" + devno + "' AND PONumber='" + pono + "' and isVat=0  and isSettled=0", "QtyDelivered");
            totalvatexemptqtyrcv = Database.getTotalSummation2("DeliveryDetails", " DeliveryNo='" + devno + "' AND PONumber='" + pono + "' and isVat=0  and isSettled=0", "ActualQty");

            
            if (totalvatexemptqtyrcv > totalvatexemptqtydeliv)
            {
                totalvatexemptgain = totalvatexemptqtydeliv - totalvatexemptqtyrcv;
            }
            else if (totalvatexemptqtyrcv < totalvatexemptqtydeliv)
            {
                totalvatexemptloss = totalvatexemptqtydeliv - totalvatexemptqtyrcv;
            }

            totalvatqtydeliv = Database.getTotalSummation2("DeliveryDetails", " DeliveryNo='" + devno + "' AND PONumber='" + pono + "' and isVat=1  and isSettled=0", "QtyDelivered");
            totalvatqtyrcv = Database.getTotalSummation2("DeliveryDetails", " DeliveryNo='" + devno + "' AND PONumber='" + pono + "' and isVat=1  and isSettled=0", "ActualQty");

            if (totalvatqtyrcv > totalvatqtydeliv)
            {
                totalvatgain = totalvatqtydeliv - totalvatqtyrcv;
            }
            else if (totalvatqtyrcv < totalvatqtydeliv)
            {
                totalvatloss = totalvatqtydeliv - totalvatqtyrcv;
            }
            det.txtvatexemptgain.Text = HelperFunction.numericFormat(totalvatexemptgain);
            det.txtvatexemptloss.Text = HelperFunction.numericFormat(totalvatexemptloss);
            det.txtvatgain.Text = HelperFunction.numericFormat(totalvatgain);
            det.txtvatloss.Text = HelperFunction.numericFormat(totalvatloss);
        }
    }
}