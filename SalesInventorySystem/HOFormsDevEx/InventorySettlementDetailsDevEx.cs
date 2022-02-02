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
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventorySettlementDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public InventorySettlementDetailsDevEx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add();
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                Database.ExecuteQuery("Update DeliveryDetails set isSettled=1 WHERE SequenceNo='" + gridView1.GetRowCellValue(i, "SequenceNo").ToString() + "'");
            }
            XtraMessageBox.Show("Successfully Added!");
            this.Dispose();
        }

        void add()
        {
            string devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            string pono = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_InventorySettlement";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmdelivno", devno);
                com.Parameters.AddWithValue("@parmpono", pono);
                com.Parameters.AddWithValue("@parmvatgain", txtvatgain.Text);
                com.Parameters.AddWithValue("@parmvatloss", txtvatloss.Text);
                com.Parameters.AddWithValue("@parmvatexemptgain", txtvatexemptgain.Text);
                com.Parameters.AddWithValue("@parmvatexemptloss", txtvatexemptloss.Text);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
          
        }

        private void InventorySettlementDetailsDevEx_Load(object sender, EventArgs e)
        {

        }

        private void updateToIsSettledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double totalvatexemptqtydeliv = 0.0, totalvatexemptqtyrcv = 0.0, totalvatqtydeliv = 0.0, totalvatqtyrcv = 0.0;
            double totalvatexemptgain = 0.0, totalvatexemptloss = 0.0;
            double totalvatgain = 0.0, totalvatloss = 0.0;
            string devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            string pono = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            Database.ExecuteQuery("UPDATE DeliveryDetails SET isSettled=1 WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "'", "Item Successfully Settled");
            Database.display("SELECT * FROM DeliveryDetails WHERE DeliveryNo='" + devno + "' AND PONumber='" + pono + "' and Variance <> 0 and isSettled=0", gridControl1, gridView1);
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
            txtvatexemptgain.Text = HelperFunction.numericFormat(totalvatexemptgain);
            txtvatexemptloss.Text = HelperFunction.numericFormat(totalvatexemptloss);
            txtvatgain.Text = HelperFunction.numericFormat(totalvatgain);
            txtvatloss.Text = HelperFunction.numericFormat(totalvatloss);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }
    }
}