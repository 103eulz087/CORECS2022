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
    public partial class ClientChargesDevExUpdate : DevExpress.XtraEditors.XtraForm
    {
        double totalamount = 0.0;
        DataTable table;
        public ClientChargesDevExUpdate()
        {
            InitializeComponent();
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip1.Show(gridControl2, e.Location);
        }

        private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["Amount"] = 0;
            table.Rows.Add(newRow);
            gridControl2.DataSource = table;
            gridView2.BestFitColumns();
        }
        void add()
        {
            try
            {
                if (gridView2.RowCount == 0)
                {
                    XtraMessageBox.Show("No Additional Details Added!..Only The ReferenceNo and Remarks will Update.");
                    Database.ExecuteQuery("Update ClientChargeSalesSummary SET Description='" + txtremarks.Text + "',ReferenceNo='" + txtfreno.Text + "' WHERE ChargeNo='" + txtchargeno.Text + "'", "Successfully Updated");
                }
                else
                {
                    for (int i = 0; i <= gridView2.RowCount - 1; i++)
                    {
                        if (!String.IsNullOrEmpty(gridView2.GetRowCellValue(i, "Amount").ToString()))
                        {
                            totalamount += Convert.ToDouble(gridView2.GetRowCellValue(i, "Amount").ToString());
                            Database.ExecuteQuery("INSERT INTO ClientChargeSalesDetails VALUES('" + txtchargeno.Text + "','" + txtpono.Text + "','" + gridView2.GetRowCellValue(i, "Description").ToString() + "','" + gridView2.GetRowCellValue(i, "Amount").ToString() + "')");
                        }
                    }
                    Database.ExecuteQuery("INSERT INTO ClientLedger VALUES('" + groupControl1.Text + "','" + DateTime.Now.ToShortDateString() + "','888','CLIENT CHARGE ADD ON','CHRG','" + DateTime.Now.ToShortDateString() + "','" + txtfreno.Text + "',0,'" + totalamount + "',0,0,'" + txtfreno.Text + "','" + Login.Fullname + "','*','" + txtremarks.Text + "','" + totalamount + "',0)");
                    Database.ExecuteQuery("Update ClientChargeSalesSummary SET Description='" + txtremarks.Text + "',ReferenceNo='" + txtfreno.Text + "',TotalAmount=TotalAmount+'" + totalamount + "',Balance=Balance+'" + totalamount + "' WHERE ChargeNo='" + txtchargeno.Text + "'", "Successfully Added");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnupdate_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(txtfreno.Text) || String.IsNullOrEmpty(txtremarks.Text))
            {
                XtraMessageBox.Show("All Fields are mandatory!...");
                return;
            }
            else
            {
                add();
                this.Dispose();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ClientChargesDevExUpdate_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("Description");
            table.Columns.Add("Amount");
            gridControl2.DataSource = table;

            Classes.DevXGridViewSettings.getTotalSummation(gridView1, "Amount");
            Classes.DevXGridViewSettings.getTotalSummation(gridView2, "Amount");
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStrip2.Show(gridControl1, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            double amount=0.0;
            amount = Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Amount").ToString());
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this transaction?", "Confirm Delete");
            if (ok == true)
            {
                Database.ExecuteQuery("DELETE FROM ClientChargeSalesDetails WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "' ");
                Database.ExecuteQuery("UPDATE ClientChargeSalesSummary SET TotalAmount=TotalAmount-'" + amount + "',Balance=Balance-'" + amount + "' WHERE ChargeNo='" + txtchargeno.Text + "'", "Successfully Deleted");
                Database.display("Select * FROM view_ClientChargeSalesDetails WHERE ChargeNo='" + txtchargeno.Text + "'", gridControl1, gridView1);
            }
            else
            {
                return;
            }
        }
    }
}