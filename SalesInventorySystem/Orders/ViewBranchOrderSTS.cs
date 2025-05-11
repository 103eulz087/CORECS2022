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
using SalesInventorySystem.Reporting;
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.Orders
{
    public partial class ViewBranchOrderSTS : DevExpress.XtraEditors.XtraForm
    {
        string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");
        public static string branchno, ponumber, effectivedate, refno = "", custname = "",devno="";
        public ViewBranchOrderSTS()
        {
            InitializeComponent();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            openBranchOrder("STS");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabfilter();
        }

        private void gridControl1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

       

        private void processThisOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            refno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            //branchno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            Orders.viewBranchOrderDetails viewdet = new Orders.viewBranchOrderDetails();
            viewdet.Show();
            Database.display("SELECT * FROM view_BranchOrderDetails WHERE PONumber='" + refno + "' ", viewdet.gridControl4, viewdet.gridView4);
        }
        
      

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            refno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "PONumber").ToString();
            branchno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "BranchCode").ToString();
            Orders.viewBranchOrderDetails viewdet = new Orders.viewBranchOrderDetails();
            viewdet.Show();
        }

     

        private void confirmOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refno = gridView1.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            HOForms.ViewForDeliveryDetails viewdet = new HOForms.ViewForDeliveryDetails();
            viewdet.ShowDialog(this);
        }
        void analyze(string spname, string pono, GridControl cont, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            cont.BeginUpdate();

            try
            {
                //spname = "spview_SalesInvoice";
                string sp = spname;
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmpono", pono);

                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                //com.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                view.Columns.Clear();
                cont.DataSource = null;
                adapter.Fill(table);
                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cont.EndUpdate();
                con.Close();
            }
        }
        void showSTSDetails()
        {

            string branchcode = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            string effectivitydate = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EffectivityDate").ToString();
            string requestedby = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PreparedBy").ToString();
          
            string ponum = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            StocksOrder devrepfrm = new StocksOrder();
            devrepfrm.txtpono.Text = ponum;
            devrepfrm.txtbranch.Text = branchcode;
            devrepfrm.txteffectivitydate.Text = Convert.ToDateTime(effectivitydate).ToShortDateString();
            devrepfrm.txtpreparedby.Text = requestedby;
            analyze("spr_STSSummary", ponum, devrepfrm.gridControl1, devrepfrm.gridView1);
            devrepfrm.Show();

            GridView view = devrepfrm.gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
            devrepfrm.gridView1.ExpandAllGroups();
        }
        private void printDeliveryReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSTSDetails();
            //string refno1 = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            //HOForms.ViewForDeliveryDetails viewdet = new HOForms.ViewForDeliveryDetails();
            //viewdet.Show();
            //Database.display("SELECT ProductName,BarcodeNo,QtyDelivered,FORMAT(SellingPrice, 'N','en-US')  AS Price,FORMAT((QtyDelivered*SellingPrice), 'N','en-US') AS Amount,QtyDelivered AS ActualQtyDelivered,ProductNo FROM view_DeliveryReciept WHERE PONumber = '" + refno1 + "'", viewdet.gridControl4, viewdet.gridView4);
            //viewdet.txtpono.Text = refno1;
            //viewdet.gridView4.Columns["Amount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:n2}");
        }

         
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_BranchOrderSTS WHERE isProcess = '0' AND Status='APPROVED' and EffectivityDate >= '" + datefrompending.Text + "' and EffectivityDate <= '" + datetopending.Text + "' and BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1);
            gridView1.Focus();
        }

        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "'", gridControl3, gridView3);
            gridView3.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "'", gridControl2, gridView2);
            gridView2.Focus();
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {

                string groupcategory = view.GetRowCellDisplayText(e.RowHandle, view.Columns["GroupCategory"]);
                if (groupcategory == "REPACKING")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (groupcategory == "GULAYAN")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightBlue;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (groupcategory == "LUTOAN")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightCyan;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (groupcategory == "CHORIZO")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (groupcategory == "DRYGOODS")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LimeGreen;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (groupcategory == "CHICKEN")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightSlateGray;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (groupcategory == "MEAT")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.LightYellow;
                    e.Appearance.ForeColor = Color.Black;
                }
                if (groupcategory == "SEAFOODS")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void gridControl1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuPending.Show(gridControl1, e.Location);
            }
        }

        private void gridControl2_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripForDelivery.Show(gridControl2, e.Location);
            }
        }

        private void btnPendingHRI_Click(object sender, EventArgs e)
        {
            Database.display("SELECT BranchCode,EffectivityDate,SUM(Qty) AS TotalQty,Status " +
                "FROM PurchaseOrderSummary " +
                "WHERE EffectivityDate BETWEEN '" + datefrompendingHRI.Text + "' AND '" + datetoPendingHRI.Text + "' " +
                "AND Status='FOR APPROVAL' " +
                "GROUP BY BranchCode,EffectivityDate,Status ", gridControlPendingHRI, gridViewPendingHRI);
        }

        private void ViewBranchOrderSTS_Load(object sender, EventArgs e)
        {

        }

        private void gridViewPendingHRI_DoubleClick(object sender, EventArgs e)
        {
            openHRIOrder();
        }

        private void cancelThisOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void singleModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openBranchOrder("STS");
        }

        private void tabfilter()
        {
            if (tabMain.SelectedTabPage.Equals(tabForApproval))
            {
                Database.display("SELECT * FROM view_BranchOrderSTS WHERE isProcess = '0' AND Status='APPROVED' and DateApproved >= '" + datefrompending.Text + "' and DateApproved <= '" + datetopending.Text + "'", gridControl1, gridView1);
                gridView1.Focus();
            }
           
            else if (tabMain.SelectedTabPage.Equals(tabRejected))
            {
                Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "'", gridControl3, gridView3);
                gridView3.Focus();
            }
            else if (tabMain.SelectedTabPage.Equals(tabForDelivery))
            {
                Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "'", gridControl2, gridView2);
                gridView2.Focus();
            }
        }

        private void batchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openBranchOrderBatchMode("STS");
        }

        void display()
        {
            Database.display("SELECT * FROM view_BranchOrderSTS WHERE isProcess = '0' AND Status='APPROVED' and DateApproved >= '" + datefrompending.Text + "' and DateApproved <= '" + datetopending.Text + "'", gridControl1, gridView1);
            Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "'", gridControl2, gridView2);
            Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='REJECTED' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "' ", gridControl3, gridView3);
        }

        void openHRIOrder()
        {
            Orders.AddBranchOrderSTS addbrorder = new Orders.AddBranchOrderSTS();
            addbrorder.Show();

            addbrorder.txtbrcode.Text = "999";  

            string id = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            addbrorder.txtrefno.Text = id;//IDGenerator.getReferenceNumber();
            addbrorder.txteffectivedate.Text = gridViewPendingHRI.GetRowCellValue(gridViewPendingHRI.FocusedRowHandle,"EffectivityDate").ToString();

            Database.display("SELECT a.ProductCode,a.ProductName,SUM(a.Qty) AS TotalQty FROM PurchaseOrderDetails as a WHERE EXISTS (SELECT 1 FROM PurchaseOrderSummary as b " +
                "WHERE b.EffectivityDate='"+addbrorder.txteffectivedate.Text+ "' AND b.PONumber=a.PONumber) GROUP BY a.ProductCode,a.ProductName", addbrorder.gridControl1, addbrorder.gridView1);//

            Database.display("SELECT SeqNo,ProductNo,ProductName,BarcodeNo,QtyDelivered,Status,ProcessedBy " +
                "FROM DeliveryDetails " +
                "WHERE DeliveryNo='" + addbrorder.txtdevno.Text + "' and Status = 'PENDING'", addbrorder.gridControl2, addbrorder.gridView2);
            
            addbrorder.gridView1.ExpandAllGroups();
             
            addbrorder.gridView2.Columns["ProductNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ProductNo", "{0:n2}");
 
            Database.displaySearchlookupEdit($"SELECT * FROM dbo.funcview_populateProducts('{Login.assignedBranch}') " +
              $"WHERE ProductCode in (Select distinct ProductCode FROM PurchaseOrderDetails WHERE PONumber in (SELECT PONumber FROM PurchaseOrderSummary WHERE EffectivityDate='{addbrorder.txteffectivedate.Text}') )", addbrorder.txtsearchlookupproduct, "Description", "Description");

            if (Orders.AddBranchOrderSTS.isdone == true)
            {
                Orders.AddBranchOrderSTS.isdone = false;
                addbrorder.Dispose();
                display();
            }
        }

        void openBranchOrder(string type)
        {
            string stat = "";
            branchno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InitiatingBranch").ToString();
            ponumber = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            stat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "WareHouseStatus").ToString();
            effectivedate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString();
          
            if (String.IsNullOrEmpty(stat) || stat == "PENDING")
            {
                Orders.AddBranchOrderSTS addbrorder = new Orders.AddBranchOrderSTS();
                addbrorder.Show();

                addbrorder.txtbrcode.Text = branchno;
                addbrorder.txtponum.Text = ponumber;
                addbrorder.Text = custname + "-" + Branch.getBranchName(branchno);

                string id = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
                addbrorder.txtrefno.Text = id;//IDGenerator.getReferenceNumber();
                addbrorder.txteffectivedate.Text = effectivedate;

                Database.display("SELECT * FROM view_TransferOrderDetails WHERE PONumber='" + addbrorder.txtponum.Text + "'", addbrorder.gridControl1, addbrorder.gridView1);//
                Database.display("SELECT SeqNo,ProductNo,ProductName,BarcodeNo,QtyDelivered,Status,ProcessedBy FROM DeliveryDetails WHERE DeliveryNo='" + addbrorder.txtdevno.Text + "' AND PONumber='" + addbrorder.txtponum.Text + "' and Status = 'PENDING'", addbrorder.gridControl2, addbrorder.gridView2);
                //addbrorder.gridView2.Columns["SequenceNo"].Visible = false;
                addbrorder.gridView1.Columns["PONumber"].Visible = false;
                addbrorder.gridView1.ExpandAllGroups();

                addbrorder.gridView1.Columns["SeqNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "SeqNo", "{0:n2}");
                addbrorder.gridView2.Columns["ProductNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ProductNo", "{0:n2}");

                //Database.displaySearchlookupEdit("SELECT ProductCode,Barcode,Description FROM Products WHERE BranchCode='888' " +
                // "AND ProductCode in (SELECT ProductCode FROM TransferOrderDetails WHERE PONumber='" + ponumber + "' )", addbrorder.txtsearchlookupproduct, "Description", "Description");

                Database.displaySearchlookupEdit($"SELECT * FROM dbo.funcview_populateProducts('{Login.assignedBranch}') " +
                  $"WHERE ProductCode in (Select distinct ProductCode FROM TransferOrderDetails WHERE PONumber='{ponumber}')", addbrorder.txtsearchlookupproduct, "Description", "Description");

                if (Orders.AddBranchOrderSTS.isdone == true)
                {
                    Orders.AddBranchOrderSTS.isdone = false;
                    addbrorder.Dispose();
                    display();
                }
            }
            else
            {
                XtraMessageBox.Show("You Already Processed This Request!");
            }
        }

        void openBranchOrderBatchMode(string type)
        {
            AddBranchOrderSTSBatchMode addbrorder = new AddBranchOrderSTSBatchMode();
            addbrorder.Show();
            
            branchno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "InitiatingBranch").ToString();
            ponumber = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            string id = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            addbrorder.txtbrcode.Text = branchno;
            addbrorder.txtponum.Text = ponumber;
            addbrorder.txtdevno.Text = IDGenerator.getIDNumberSP("sp_GetDeliveryNumber", "DeliveryNumber");
            addbrorder.txtrefno.Text = id;//IDGenerator.getReferenceNumber();
            Database.display("SELECT * FROM view_TransferOrderDetailsSTS WHERE PONumber='" + addbrorder.txtponum.Text + "'", addbrorder.gridControl1, addbrorder.gridView1);//
            addbrorder.gridView1.Columns["PONumber"].Visible = false;
            addbrorder.gridView1.ExpandAllGroups();
            addbrorder.gridView1.Columns["SeqNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "SeqNo", "{0:n2}");
            if (Orders.AddBranchOrderSTSBatchMode.isdone == true)
            {
                Orders.AddBranchOrderSTSBatchMode.isdone = false;
                addbrorder.Dispose();
                display();
            }
        }
    }
}