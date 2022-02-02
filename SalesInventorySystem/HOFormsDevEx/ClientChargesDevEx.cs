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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ClientChargesDevEx : DevExpress.XtraEditors.XtraForm
    {
        double totalamount = 0.0;
        DataTable table;
        public ClientChargesDevEx()
        {
            InitializeComponent();
        }

        private void ClientChargesDevEx_Load(object sender, EventArgs e)
        {
            
            table = new DataTable();
            table.Columns.Add("Description");
            table.Columns.Add("Amount");
            gridControl1.DataSource = table;

            Classes.DevXGridViewSettings.getTotalSummation(gridView1, "Amount");
            //GridGroupSummaryItem ite11 = new GridGroupSummaryItem();
            //ite11.FieldName = "Amount";
            //ite11.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ite11.ShowInGroupColumnFooter = gridView1.Columns["Amount"];
            //gridView1.GroupSummary.Add(ite11);
            //gridView1.Columns["Amount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:n2}");

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtfreno.Text) || String.IsNullOrEmpty(txtremarks.Text))
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

        void add()
        {
            try
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (!String.IsNullOrEmpty(gridView1.GetRowCellValue(i, "Amount").ToString()))
                    {
                        totalamount += Convert.ToDouble(gridView1.GetRowCellValue(i, "Amount").ToString());
                        Database.ExecuteQuery("INSERT INTO ClientChargeSalesDetails VALUES('" + txtchargeno.Text + "','" + txtpono.Text + "','" + gridView1.GetRowCellValue(i, "Description").ToString() + "','" + gridView1.GetRowCellValue(i, "Amount").ToString() + "')");
                    }
                }
                Database.ExecuteQuery("INSERT INTO ClientLedger VALUES('" + groupControl1.Text + "','" + DateTime.Now.ToShortDateString() + "','888','CLIENT CHARGE','CHRG','" + DateTime.Now.ToShortDateString() + "','" + txtfreno.Text + "',0,'" + totalamount + "',0,0,'" + txtfreno.Text + "','" + Login.Fullname + "','*','" + txtremarks.Text + "','" + totalamount + "',0)");
                Database.ExecuteQuery("INSERT INTO ClientChargeSalesSummary VALUES('" + txtchargeno.Text + "','" + txtpono.Text + "','"+txtinvoiceno.Text+"','" + txtfreno.Text + "','" + groupControl1.Text + "','" + txtremarks.Text + "','" + totalamount + "','"+totalamount+"',0,'" + DateTime.Now.ToShortDateString() + "','" + Login.Fullname + "')", "Successfully Added");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["Amount"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();

           
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (!String.IsNullOrEmpty(gridView1.GetRowCellValue(i, "Amount").ToString()))
                    {
                        totalamount += Convert.ToDouble(gridView1.GetRowCellValue(i, "Amount").ToString());
                        Database.ExecuteQuery("INSERT INTO ClientChargeSalesDetails VALUES('" + txtchargeno.Text + "','" + txtpono.Text + "','" + gridView1.GetRowCellValue(i, "Description").ToString() + "','" + gridView1.GetRowCellValue(i, "Amount").ToString() + "')");
                    }
                }
                Database.ExecuteQuery("INSERT INTO ClientLedger VALUES('" + groupControl1.Text + "','" + DateTime.Now.ToShortDateString() + "','888','CLIENT CHARGE','CHRG','" + DateTime.Now.ToShortDateString() + "','" + txtfreno.Text + "',0,'" + totalamount + "',0,0,'" + txtfreno.Text + "','" + Login.Fullname + "','*','" + txtremarks.Text + "','" + totalamount + "',0)");
                Database.ExecuteQuery("INSERT INTO ClientChargeSalesSummary VALUES('" + txtchargeno.Text + "','" + txtpono.Text + "','" + txtinvoiceno.Text + "','" + txtfreno.Text + "','" + groupControl1.Text + "','" + txtremarks.Text + "','" + totalamount + "','" + totalamount + "',0,'" + DateTime.Now.ToShortDateString() + "','" + Login.Fullname + "')", "Successfully Added");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}