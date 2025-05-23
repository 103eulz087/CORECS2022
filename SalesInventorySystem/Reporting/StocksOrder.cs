using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.Reporting
{
    public partial class StocksOrder : Form
    {
        int totalreceive = 0;
        public static bool isdone = false;
        public StocksOrder()
        {
            InitializeComponent();
        }

        private void StocksOrder_Load(object sender, EventArgs e)
        {
            //display();
        }

        void display()
        {
            Database.display("SELECT Description,ProductName as Item,Qty as Requested,Remarks FROM view_StockOrderReport WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' AND PONumber='" + comboBox2.Text + "'", gridControl1, gridView1);
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending) 
                }, 1);
            gridView1.ExpandAllGroups();
            // gridView1.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:n2}");
            loadData();
        }

        void loadData()
        {
            Branch.displayBranchNameComboBoxItems(comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT * FROM PurchaseOrderSummary WHERE BranchCode='"+Branch.getBranchCode(comboBox1.Text)+"'","PONumber",comboBox2);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadGrid();
        }

        void loadGrid()
        {
            //Database.display("SELECT Description,ProductName as Item,Qty as Requested,Dispatched,Received,Remarks FROM view_StockOrderReport WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' AND PONumber='" + comboBox2.Text + "'", gridControl1, gridView1);
            display();
        }

        String getRequestedBy()
        {
            string str = "";
            str = Database.getSingleQuery("PurchaseOrderSummary", "PONumber='" + comboBox2.Text + "'", "RequestedBy");
            return str;
        }

        String getDateRequest()
        {
            string str = "";
            str = Database.getSingleQuery("PurchaseOrderSummary", "PONumber='" + comboBox2.Text + "'", "EffectivityDate");
            return str;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
        void printSTS()
        {
            var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

            string companyname = row["Heading"].ToString();
            string imagewidth = row["ImageWidth"].ToString();
            string imageheight = row["ImageHeight"].ToString();
            string caption1 = row["Caption1"].ToString();
            string caption2 = row["Caption2"].ToString();

            DevExReportTemplate.StockOrderRep xct = new DevExReportTemplate.StockOrderRep();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);

            string branchname = Database.getSingleQuery("Branches", "BranchCode='" + txtbranch.Text + "'", "BranchName");
            string branchaddress = Database.getSingleQuery("Branches", "BranchCode='" + txtbranch.Text + "'", "Address");
            string dateprocessed = Database.getSingleQuery("DeliverySummary", "PONumber='" + txtpono.Text + "'", "DateAdded");

            xct.xrbranchname.Text = branchname;// comboBox1.Text;
            xct.xrdate.Text = txteffectivitydate.Text;//getDateRequest().Substring(0, 10);
            xct.xrdateprocessed.Text = Convert.ToDateTime(dateprocessed).ToShortDateString();
            xct.xrrequestedby.Text = txtrequestedby.Text;// getRequestedBy();
            xct.xrpono.Text = txtpono.Text;//comboBox2.Text;
            xct.xrbranchaddress.Text = branchaddress;
            xct.xrpreparedby.Text = txtpreparedby.Text;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            if(gridView1.RowCount == 0)
            {
                MessageBox.Show("Nothing To Print!...");
            }
            else
            {
                printSTS();
            }
           
        }

        void returnOrder(string devno,string refno,string pono,string prodno,string qty,string brcode,string devseqno)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_CancelDelivery";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", devno);
                com.Parameters.AddWithValue("@parmrefno", refno);
                com.Parameters.AddWithValue("@parmpono", pono);
                com.Parameters.AddWithValue("@parmprodno", prodno);
                com.Parameters.AddWithValue("@parmqty", qty);
                com.Parameters.AddWithValue("@parmbranchcode", brcode);
                com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmdevseqno", devseqno);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        void executeErrorCorrect()
        {
            try
            {
                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.Clear();

                int[] selectedRows = gridView1.GetSelectedRows();

                foreach (int rowHandle in selectedRows)
                {
                    string seqno = gridView1.GetRowCellValue(rowHandle, "SeqNo").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    string productcode = gridView1.GetRowCellValue(rowHandle, "ProductCode").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string description = gridView1.GetRowCellValue(rowHandle, "ProductName").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString(); 
                    string cost = gridView1.GetRowCellValue(rowHandle, "Cost").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    string quantity = gridView1.GetRowCellValue(rowHandle, "Qty").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    string barcode = gridView1.GetRowCellValue(rowHandle, "BarcodeNo").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    totalreceive = rowHandle;
                    if (rowHandle >= 0)
                    {
                        returnOrder(txtdevno.Text, "", txtpono.Text, productcode, quantity,txtbranchdestination.Text, seqno);
                    }
                }
                totalreceive = gridView1.SelectedRowsCount;
                isdone = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        { 
            bool confirmRcv = HelperFunction.ConfirmDialog("Are you sure you want to Error Correct this Transaction?", "Confirm Error Correct");
            if (confirmRcv)
            {
                executeErrorCorrect();
                MessageBox.Show("Successfully Deleted");
                isdone = true;
                this.Close();
            }
            else
            {
                return;
            }
            
        }
    }
}
