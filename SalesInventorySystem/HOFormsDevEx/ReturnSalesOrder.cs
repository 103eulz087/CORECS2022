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
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ReturnSalesOrder : DevExpress.XtraEditors.XtraForm
    {
        public ReturnSalesOrder()
        {
            InitializeComponent();
        }
        void executeTransfer()
        {
            try
            {

                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.Clear();

                string id = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber");// IDGenerator.getTransferedNumber();
                int[] selectedRows = gridView1.GetSelectedRows();

                foreach (int rowHandle in selectedRows)
                {

                    if (rowHandle >= 0)
                    {
                        string branchcode = gridView1.GetRowCellValue(rowHandle, "BranchCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                        string prodno = gridView1.GetRowCellValue(rowHandle, "ProductNo").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                        string desc = gridView1.GetRowCellValue(rowHandle, "ProductName").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                        string devno = gridView1.GetRowCellValue(rowHandle, "DeliveryNo").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                        string pono = gridView1.GetRowCellValue(rowHandle, "Description").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString();
                        string barcode = gridView1.GetRowCellValue(rowHandle, "BarcodeNo").ToString();//dataGridView1.Rows[0].Cells["Barcode"].Value.ToString();
                        string seqno = gridView1.GetRowCellValue(rowHandle, "SequenceNo").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                        string qty = gridView1.GetRowCellValue(rowHandle, "QtyDelivered").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                        string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                        string sprice = gridView1.GetRowCellValue(rowHandle, "SellingPrice").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                        string actualqty = gridView1.GetRowCellValue(rowHandle, "ActualQty").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                        string isvat = gridView1.GetRowCellValue(rowHandle, "isVat").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();

                        Database.ExecuteQuery("insert into ReturnedOrderDetails values ('" + devno + "','0','" + prodno + "','" + barcode + "','" + pono + "','" + branchcode + "','" + desc + "','" + barcode + "','" + qty + "','" + cost + "','" + sprice + "','" + actualqty + "',0,'" + seqno + "','RETURNED','" + DateTime.Now.ToShortDateString() + "','" + isvat + "','"+Login.Fullname+"');");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}