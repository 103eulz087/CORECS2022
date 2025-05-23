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
using System.Data.SqlClient;
using SalesInventorySystem.Reporting;

namespace SalesInventorySystem.Orders
{
    public partial class POForApprovalSTS : DevExpress.XtraEditors.XtraForm
    {
        public static string refno, stat, refno1 = "", stat1 = "", brcode, devno = "", menu = "", invoiceno = "";
        public static string requestedBy = "";
        string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");
        public static bool isupdated = false;
        public POForApprovalSTS()
        {
            InitializeComponent();
        }

        private void POForApprovalSTS_Load(object sender, EventArgs e)
        {
            filtertab();
        }
        private void filtertab()
        {
            if (tabMain.SelectedTabPage.Equals(tabForApproval))
            {
                Database.display("SELECT * FROM view_TransferSummary WHERE Status='FOR APPROVAL' and EffectivityDate >= '" + datefromsts.Text + "' and EffectivityDate <= '" + datetosts.Text + "'and BranchCode='" + Login.assignedBranch + "' ", gridControlSTS, gridViewSTS);
            }
            else if (tabMain.SelectedTabPage.Equals(tabApproved))
            {
                Database.display("SELECT * FROM view_TransferSummary WHERE Status='APPROVED' and DateApproved >= '" + datefromsts.Text + "' and DateApproved <= '" + datetosts.Text + "' AND BranchCode='" + Login.assignedBranch + "' ORDER BY DateAdded DESC", gridControlapprvdsts, gridViewapprvdsts);
            }
            else if (tabMain.SelectedTabPage.Equals(tabRejected))
            {
                Database.display("SELECT * FROM view_TransferSummary WHERE Status='REJECTED' and DateApproved >= '" + datefrmrjctdsts.Text + "' and DateApproved <= '" + datetorjctdsts.Text + "' AND BranchCode='" + Login.assignedBranch + "' ", gridControlrjctdsts, gridViewrjctdsts);
            }
            else if (tabMain.SelectedTabPage.Equals(tabForDelivery))
            {
                Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='FOR DELIVERY'  and DateAdded >= '" + datefromfordelivsts.Text + "' and DateAdded <= '" + datetofordelivsts.Text + "' and BranchCode='" + Login.assignedBranch + "' ", gridControlForDelivSts, gridViewForDelivSts);
                Classes.DevXGridViewSettings.ShowFooterCountTotal(gridViewForDelivSts, "DeliveryNo");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridViewForDelivSts, "TotalItem");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridViewForDelivSts, "TotalQtyDelivered");
            }
            else if (tabMain.SelectedTabPage.Equals(tabDelivered))
            {
                Database.display("SELECT * FROM view_TransferSummary WHERE Status='DELIVERED' and EffectivityDate >= '" + datefromdelivsts.Text + "' and EffectivityDate <= '" + datetodelivsts.Text + "' and BranchCode='" + Login.assignedBranch + "'", gridControlDelivSts, gridViewDelivSts);
            }
        }

        private void btnForApprovalSTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_TransferSummary WHERE Status='FOR APPROVAL' and EffectivityDate >= '" + datefromsts.Text + "' and EffectivityDate <= '" + datetosts.Text + "'and BranchCode='" + Login.assignedBranch + "' ", gridControlSTS, gridViewSTS);
        }

        private void btnApprovedSTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_TransferSummary WHERE Status='APPROVED' and EffectivityDate >= '" + datefromsts.Text + "' and EffectivityDate <= '" + datetosts.Text + "' AND BranchCode='" + Login.assignedBranch + "' ORDER BY DateAdded DESC", gridControlapprvdsts, gridViewapprvdsts);
        }

        private void btnRejectedSTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_TransferSummary WHERE Status='REJECTED' and EffectivityDate >= '" + datefrmrjctdsts.Text + "' and EffectivityDate <= '" + datetorjctdsts.Text + "' AND BranchCode='" + Login.assignedBranch + "' ", gridControlrjctdsts, gridViewrjctdsts);
        }

        private void btnForDeliverySTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_ForDeliverySTS WHERE Status='FOR DELIVERY'  and EffectivityDate >= '" + datefromfordelivsts.Text + "' and EffectivityDate <= '" + datetofordelivsts.Text + "' and BranchCode='" + Login.assignedBranch + "' ", gridControlForDelivSts, gridViewForDelivSts);
            Classes.DevXGridViewSettings.ShowFooterCountTotal(gridViewForDelivSts, "DeliveryNo");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewForDelivSts, "TotalItem");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewForDelivSts, "TotalQtyDelivered");
        }

        private void btnDeliveredSTS_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_TransferSummary WHERE Status='DELIVERED' and EffectivityDate >= '" + datefromdelivsts.Text + "' and EffectivityDate <= '" + datetodelivsts.Text + "' and  BranchCode='" + Login.assignedBranch + "'", gridControlDelivSts, gridViewDelivSts);
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            filtertab();
        }

        private void btndeliveredstsexcel_Click(object sender, EventArgs e)
        {
            string filename = "DELIVERED_STS" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridViewDelivSts, filename);
        }

        private void gridViewapprvdsts_DoubleClick(object sender, EventArgs e)
        {
            //this will show purchase order details
            menu = "approvedrequest";
            refno = gridViewapprvdsts.GetRowCellValue(gridViewapprvdsts.FocusedRowHandle, "PONumber").ToString();
            stat = gridViewapprvdsts.GetRowCellValue(gridViewapprvdsts.FocusedRowHandle, "Status").ToString();
            GlobalVariables.ponumber = gridViewapprvdsts.GetRowCellValue(gridViewapprvdsts.FocusedRowHandle, "PONumber").ToString();
            Orders.STSForApprovalDetails podetails = new Orders.STSForApprovalDetails();
            podetails.Show();
            podetails.txtpono.Text = refno;
            if (Convert.ToBoolean(Login.isglobalWarehouseOfficer).Equals(true))
            {
                //Database.display("SELECT * FROM view_TransferOrderDetails WHERE PONumber = '" + refno + "'", podetails.gridControl1, podetails.gridView1);
                Database.display($"SELECT * FROM funcview_TransferOrderDetailsSTS('{Login.assignedBranch}') WHERE PONumber = '{refno}' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                podetails.txtpono.Text = refno;
                GridView view = podetails.gridControl1.FocusedView as GridView;
                view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Category"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
                podetails.gridView1.Columns["SeqNo"].Visible = false;
                podetails.gridView1.ExpandAllGroups();
            }
            if (Convert.ToBoolean(Login.isglobalApprover).Equals(true))
            {
                //Database.display("SELECT * FROM view_TransferOrderDetails WHERE PONumber = '" + refno + "'", podetails.gridControl1, podetails.gridView1);
                Database.display($"SELECT * FROM funcview_TransferOrderDetailsSTS('{Login.assignedBranch}') WHERE PONumber = '{refno}' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                podetails.txtpono.Text = refno;
                GridView view = podetails.gridControl1.FocusedView as GridView;
                view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Category"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);
                podetails.gridView1.Columns["SeqNo"].Visible = false;
                podetails.gridView1.ExpandAllGroups();
            }
            else
            {
                //Database.display("SELECT SeqNo,PONumber,ProductName,Qty,Units FROM view_TransferOrderDetails WHERE PONumber = '" + refno + "' ", podetails.gridControl1, podetails.gridView1);
                Database.display($"SELECT * FROM funcview_TransferOrderDetailsSTS('{Login.assignedBranch}') WHERE PONumber = '{refno}' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                podetails.gridView1.Columns["SeqNo"].Visible = false;
                podetails.txtpono.Text = refno;
            }
            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Qty";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = podetails.gridView1.Columns["Qty"];
            podetails.gridView1.GroupSummary.Add(ite);
            podetails.gridView1.Focus();
        }

        private void gridViewSTS_RowCellStyle(object sender, RowCellStyleEventArgs e)
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

        private void gridControlForDelivSts_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripForDelivery.Show(gridControlForDelivSts, e.Location);
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
        void showSTSDetails(GridView views)
        {

            string branchcode = views.GetRowCellValue(views.FocusedRowHandle, "InitiatingBranch").ToString();
            string effectivitydate = views.GetRowCellValue(views.FocusedRowHandle, "EffectivityDate").ToString();
            string requestedby = views.GetRowCellValue(views.FocusedRowHandle, "RequestedBy").ToString();
            string preparedby = views.GetRowCellValue(views.FocusedRowHandle, "PreparedBy").ToString();

            string ponum = views.GetRowCellValue(views.FocusedRowHandle, "PONumber").ToString();
            StocksOrder devrepfrm = new StocksOrder();
            devrepfrm.txtpono.Text = ponum;
            devrepfrm.txtbranch.Text = branchcode;
            devrepfrm.txteffectivitydate.Text = Convert.ToDateTime(effectivitydate).ToShortDateString();
            devrepfrm.txtrequestedby.Text = requestedby;
            devrepfrm.txtpreparedby.Text = preparedby;
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
            showSTSDetails(gridViewForDelivSts);
        }

        private void btnfordelivstsexcel_Click(object sender, EventArgs e)
        {
            string filename = "FORDELIVERY_STS" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridViewForDelivSts, filename);
        }

        private void btnrejectedstsexcel_Click(object sender, EventArgs e)
        {
            string filename = "REJECTED_STS" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridViewrjctdsts, filename);
        }

        private void btnapprovedreqstsexcel_Click(object sender, EventArgs e)
        {
            string filename = "APPROVED_STS" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridViewapprvdsts, filename);
        }

        private void btnforapprovalstsexcel_Click(object sender, EventArgs e)
        {
            string filename = "FORAPPROVAL_STS" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridViewSTS, filename);
        }
        void exporttoexcel(GridView view, string title)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {

                string filepath = "C:\\MyFiles\\";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }
        void showSTSForApproval()
        {
            menu = "";
            requestedBy = gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "RequestedBy").ToString();
            refno = gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "PONumber").ToString();
            stat = gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "Status").ToString();
            GlobalVariables.ponumber = gridViewSTS.GetRowCellValue(gridViewSTS.FocusedRowHandle, "PONumber").ToString();
            Orders.STSForApprovalDetails podetails = new Orders.STSForApprovalDetails();
            if (stat == "FOR APPROVAL")
            {


                //Database.display("SELECT * FROM view_TransferOrderDetails WHERE PONumber = '" + refno + "' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                Database.display($"SELECT * FROM funcview_TransferOrderDetailsSTS('{Login.assignedBranch}') WHERE PONumber = '{refno}' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                podetails.txtpono.Text = refno;
                GridView view = podetails.gridControl1.FocusedView as GridView;
                view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(view.Columns["Category"],DevExpress.Data.ColumnSortOrder.Ascending)
                        }, 1);
                //podetails.gridView1.Columns["SequenceNumber"].Visible = false;
                podetails.gridView1.ExpandAllGroups();
                podetails.ShowDialog(this);
                if (Orders.STSForApprovalDetails.isdone == true)
                {
                    Orders.STSForApprovalDetails.isdone = false;
                    podetails.Dispose();
                    btnForApprovalSTS.PerformClick();
                }
                //if (Convert.ToBoolean(Login.isglobalWarehouseOfficer) == true)
                //{

                    
                //}
                //if (Convert.ToBoolean(Login.isglobalApprover) == true)
                //{

                //    Database.display("SELECT * FROM view_TransferOrderDetails WHERE PONumber = '" + refno + "' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                //    podetails.txtpono.Text = refno;
                //    GridView view = podetails.gridControl1.FocusedView as GridView;
                //    view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                //        new GridColumnSortInfo(view.Columns["Category"],DevExpress.Data.ColumnSortOrder.Ascending)
                //        }, 1);
                //    //podetails.gridView1.Columns["SequenceNumber"].Visible = false;
                //    podetails.gridView1.ExpandAllGroups();
                //}
                //else
                //{
                //    Database.display("SELECT SeqNo,PONumber,ProductName,Qty,Units FROM view_TransferOrderDetails WHERE PONumber = '" + refno + "' ORDER BY SeqNo ", podetails.gridControl1, podetails.gridView1);
                //    podetails.txtpono.Text = refno;
                //    //podetails.gridView1.Columns["SequenceNumber"].Visible = false;
                //}

                
                GridGroupSummaryItem ite = new GridGroupSummaryItem();
                ite.FieldName = "Qty";
                ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                ite.ShowInGroupColumnFooter = podetails.gridView1.Columns["Qty"];
                podetails.gridView1.GroupSummary.Add(ite);
            }
            else
            {
                XtraMessageBox.Show("You Already Approved this Order!");
            }
            
        }
        private void gridViewSTS_DoubleClick(object sender, EventArgs e)
        {
            showSTSForApproval();
        }
    }
}