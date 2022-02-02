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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class PrimalTransferDevEx : DevExpress.XtraEditors.XtraForm
    {
        // string prodnum, desc, qty, seqnum, barcode, datereceived,cost;
        public static bool isdone = false;
      
        void executeTransfer()
        {
            try
            {
                GridView view = gridControl1.FocusedView as GridView;
                view.SortInfo.Clear();

                int[] selectedRows = gridView1.GetSelectedRows();

                foreach (int rowHandle in selectedRows)
                {
                    string barcode = gridView1.GetRowCellValue(rowHandle, "Barcode").ToString();//dataGridView1.Rows[0].Cells["Barcode"].Value.ToString(); 
                    if (rowHandle >= 0)
                    {
                        Database.ExecuteQuery("Update TempInventoryPrimal SET isWarehouse=0 WHERE Barcode='" + barcode + "'");
                    }
                }
              
                //spTransfer();
                spUpload();

                isdone = true;
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void spUpload()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_BatchPrimalCutProcess";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbatchcode", txtbatchcode.Text);
                com.Parameters.AddWithValue("@parmbatchnumber", textEdit1.Text);
                com.Parameters.AddWithValue("@parmprocessby", Login.isglobalUserID);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        void spTransfer()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_TransferDirectly";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbatchcode", txtbatchcode.Text);
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

       
        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

       
        void BigBlueTemplate()
        {

            Database.display("Select Product,Description,Barcode,Quantity FROM InventoryTransferred where BatchNumber='" + textEdit1.Text+ "' and Source='Commissary' and Destination='BigBlue'", gridControl1, gridView1);
            //gridView1 = gridControl1.FocusedView as GridView;
            gridView1.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(gridView1.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            gridView1.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Quantity"];
            gridView1.GroupSummary.Add(ite);

            gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
            
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption2");

            //DevExReportTemplate.CustomerRequest xct = new DevExReportTemplate.CustomerRequest();
            DevExReportTemplate.TransferInventory xct = new DevExReportTemplate.TransferInventory();
            xct.Landscape = false;
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);


            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtransfertype.Text = "Transfer to BigBlue";

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1,gridView1,"SequenceNumber"));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);

          
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        public PrimalTransferDevEx()
        {
            InitializeComponent();
        }
      

        private void PrimalTransferDevEx_Load(object sender, EventArgs e)
        {
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber"); //IDGenerator.getTransferedNumber();
            display();
        }

        void display()
        {
            Database.display("Select SequenceNumber" +
                ",Product" +
                ",Description" +
                ",Barcode" +
                ",Quantity" +
                ",Available" +
                ",DateReceived " +
                "FROM TempInventory " +
                "WHERE BatchCode='" + txtbatchcode.Text + "' " +
                "and isStock=1 " +
                "and isProcess=0", gridControl1, gridView1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            executeTransfer();
            this.Close();
            XtraMessageBox.Show("Successfully Transfered");
            //BigBlueTemplate();

        }
      

        
    }
}