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
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using SalesInventorySystem.HOFormsDevEx;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class VIEWPO : DevExpress.XtraEditors.XtraForm
    {
        public static string inventorysource = "",action="",suppliername="";
        public VIEWPO()
        {
            InitializeComponent();
        }

        //void getDateApart(DateTime today,DateTime oneMonthAgo)
        //{
        //     today = DateTime.Now;
        //    //DateTime oneMonthAgo;

        //    int previousMonth = today.Month - 1;
        //    int year = today.Year;

        //    if (previousMonth == 0)
        //    {
        //        previousMonth = 12;
        //        year -= 1;
        //    }

        //    // Get the number of days in the previous month
        //    int daysInPrevMonth = DateTime.DaysInMonth(year, previousMonth);

        //    // Ensure the day doesn't exceed the last day of the previous month
        //    int day = Math.Min(today.Day, daysInPrevMonth);

        //    oneMonthAgo = new DateTime(year, previousMonth, day);

        //    datefromForApprovalProd.Text = oneMonthAgo.ToShortDateString();
        //    dateToForApprovalProd.Text = today.ToShortDateString();

        //    dateFromApprovedProd.Text = oneMonthAgo.ToShortDateString();
        //    dateToApprovedProd.Text = today.ToShortDateString();

        //    dateFromForConfirmProd.Text = oneMonthAgo.ToShortDateString();
        //    dateToForConfirmProd.Text = today.ToShortDateString();

        //    dateFromConfirmedProd.Text = oneMonthAgo.ToShortDateString();
        //    dateToConfirmedProd.Text = today.ToShortDateString();
        //}

        private void VIEWPO_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;

            datefromForApprovalProd.Text = HelperFunction.GetPreviousMonthSameDay(today).ToShortDateString();
            dateToForApprovalProd.Text = today.ToShortDateString();

            dateFromApprovedProd.Text = HelperFunction.GetPreviousMonthSameDay(today).ToShortDateString();
            dateToApprovedProd.Text = today.ToShortDateString();

            dateFromForConfirmProd.Text = HelperFunction.GetPreviousMonthSameDay(today).ToShortDateString();
            dateToForConfirmProd.Text = today.ToShortDateString();

            dateFromConfirmedProd.Text = HelperFunction.GetPreviousMonthSameDay(today).ToShortDateString();
            dateToConfirmedProd.Text = today.ToShortDateString();
            filtertab();
        }
        private void filtertab()
        {
            //FOR APPROVAL
            if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPageForApproval))
            {
                if (tabControlForApproval.SelectedTab.Equals(forapproval))
                {
                    Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR APPROVAL' And OrderType='P' AND CAST(DateOrder as date) between '{datefromForApprovalProd.Text}' and '{dateToForApprovalProd.Text}'  ORDER BY ShipmentNo DESC", gridControl2, gridView2);
                    //Database.display("SELECT * FROM POSUMMARY WHERE Status='FOR APPROVAL' ", gridControl2, gridView2);
                }
                else if (tabControlForApproval.SelectedTab.Equals(fordelivery))
                {
                    Database.display("SELECT * FROM view_POSUMMARYREP WHERE Status='FOR APPROVAL' And OrderType='S' ORDER BY ShipmentNo DESC", gridControl1, gridView1);
                }
            }
            //APPROVED
            else if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPageApproved))
            {

                if (tabControlApproved.SelectedTab.Equals(tabPage1))
                {
                    Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR DELIVERY' And OrderType='P' AND CAST(DateOrder as date) between '{dateFromApprovedProd.Text}' and '{dateToApprovedProd.Text}' ORDER BY ShipmentNo DESC", gridControl3, gridView3);
                    //Database.display("SELECT * FROM POSUMMARY WHERE Status='FOR APPROVAL' ", gridControl2, gridView2);
                }
                else if (tabControlApproved.SelectedTab.Equals(tabPage2))
                {
                    Database.display("SELECT * FROM view_POSUMMARYREP WHERE Status='FOR DELIVERY' And OrderType='S' ORDER BY ShipmentNo DESC", gridControl4, gridView4);
                }
            }
            //FOR CONFIRMATION
            else if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPageForConfirmation))
            {

                if (tabControlForConfirmation.SelectedTab.Equals(tabPageForConfirmationProducts))
                {
                    Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR CONFIRMATION' And OrderType='P' AND CAST(DateOrder as date) between '{dateFromForConfirmProd.Text}' and '{dateToForConfirmProd.Text}'  ORDER BY ShipmentNo DESC", gridControlProductForConfirmation, gridViewProductForConfirmation);
                }
                else if (tabControlForConfirmation.SelectedTab.Equals(tabPageForConfirmationServices))
                {
                    Database.display("SELECT * FROM view_POSUMMARYREP WHERE Status='FOR CONFIRMATION' And OrderType='S' ORDER BY ShipmentNo DESC", gridControlServicesForConfirmation, gridViewServicesForConfirmation);
                }
            }
            //CONFIRMED
            else if (xtraTabControl1.SelectedTabPage.Equals(xtraTabPageConfirmed))
            {

                if (tabControlConfirmed.SelectedTab.Equals(tabPageConfirmedProducts))
                {
                    Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='CONFIRMED' And OrderType='P' AND CAST(DateOrder as date) between '{dateFromConfirmedProd.Text}' and '{dateToConfirmedProd.Text}' ORDER BY ShipmentNo DESC", gridControlConfirmedProducts, gridViewConfirmedProducts);
                    //Database.display("SELECT * FROM POSUMMARY WHERE Status='FOR APPROVAL' ", gridControl2, gridView2);
                }
                else if (tabControlConfirmed.SelectedTab.Equals(tabPageConfirmedServices))
                {
                    Database.display("SELECT * FROM view_POSUMMARYREP WHERE Status='CONFIRMED' And OrderType='S' ORDER BY ShipmentNo DESC", gridControlConfirmedServices, gridViewServicesForConfirmation);
                }
            }

        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripForApprovalProducts.Show(gridControl2, e.Location);
                contextMenuStripForApprovalProducts.Items[0].Visible = false;
                contextMenuStripForApprovalProducts.Items[2].Visible = false;
            }
                
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripForApprovalServices.Show(gridControl1, e.Location);
        }

        private void confirmOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
           
        }

        void showServicesForConfirmation()
        {
            
            string shipmentno, supplierid, orderype, amount, branchcode;
            shipmentno = gridViewServicesForConfirmation.GetRowCellValue(gridViewServicesForConfirmation.FocusedRowHandle, "ShipmentNo").ToString();
            supplierid = gridViewServicesForConfirmation.GetRowCellValue(gridViewServicesForConfirmation.FocusedRowHandle, "SupplierID").ToString();
            orderype = gridViewServicesForConfirmation.GetRowCellValue(gridViewServicesForConfirmation.FocusedRowHandle, "OrderType").ToString();
            amount = gridViewServicesForConfirmation.GetRowCellValue(gridViewServicesForConfirmation.FocusedRowHandle, "TotalCost").ToString();
            branchcode = gridViewServicesForConfirmation.GetRowCellValue(gridViewServicesForConfirmation.FocusedRowHandle, "BranchCode").ToString();
            CONFIRMPO confi = new CONFIRMPO();
            confi.txtshipmentno.Text = shipmentno;
            confi.txtsupplier.Text = supplierid;
            confi.txtordertype.Text = orderype;
            confi.txtbranch.Text = branchcode;
            confi.txtamountpayable.Text = amount;
            confi.ShowDialog(this);
            if (CONFIRMPO.isdone == true)
            {
                filtertab();
                CONFIRMPO.isdone = false;
                confi.Dispose();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            viewPODetails(gridView1);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //filtertab();
        }

        void viewPODetails(GridView view)
        {
           
            HOFormsDevEx.VIEWPODETAILS viewpod = new VIEWPODETAILS();
            if (action == "VIEWAPPROVED")
            {
                viewpod.simpleButton2.Visible = false;
                viewpod.simpleButton3.Visible = false;
            }
            viewpod.txtshipmentno.Text = view.GetRowCellValue(view.FocusedRowHandle, "ShipmentNo").ToString();
            viewpod.txtsupplierid.Text = view.GetRowCellValue(view.FocusedRowHandle, "SupplierID").ToString();
            viewpod.txtordertype.Text = view.GetRowCellValue(view.FocusedRowHandle, "OrderType").ToString();
            viewpod.txtbrcode.Text = view.GetRowCellValue(view.FocusedRowHandle, "BranchCode").ToString();
            //Database.display("SELECT ItemCode, FROM view_PODETAILS WHERE ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString() + "' and OrderType='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "OrderType").ToString() + "'", viewpod.gridControl2, viewpod.gridView2);
            Database.display("SELECT * FROM funcview_PODetails('" + view.GetRowCellValue(view.FocusedRowHandle, "ShipmentNo").ToString() + "'" +
                    ",'" + view.GetRowCellValue(view.FocusedRowHandle, "SupplierID").ToString() + "'" +
                    ",'" + view.GetRowCellValue(view.FocusedRowHandle, "OrderType").ToString() + "')"
                    , viewpod.gridControl2, viewpod.gridView2);

            viewpod.ShowDialog(this);
            if (VIEWPODETAILS.isdone == true)
            {
                VIEWPODETAILS.isdone = false;
                viewpod.Dispose();
                filtertab();
            }
        }

        private void printPOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            viewPODetails(gridView2);
        }

        private void manualInventoryEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string shipmentno;
            shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            //shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            AddInventory addinv = new AddInventory();
            addinv.txtshipmentno.Text = shipmentno;
            //addinv.FormClosed += new FormClosedEventHandler(addinv_FormClosed);
            addinv.ShowDialog(this);
            if (AddInventory.isdone == true)
            {
                filtertab();
                AddInventory.isdone = false;
                addinv.Dispose();
            }
        }

        private void fromLocalConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fromLocalConnectionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            inventorysource = "LOCAL";
            HOForms.UPLOADINVENTORY uploadbatch = new HOForms.UPLOADINVENTORY();

            HOForms.UPLOADINVENTORY.branch = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            HOForms.UPLOADINVENTORY.shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.UPLOADINVENTORY.supplierid = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString();

            uploadbatch.ShowDialog(this);
            if (HOForms.UPLOADINVENTORY.isdone == true)
            {
                filtertab();
                HOForms.UPLOADINVENTORY.isdone = false;
                uploadbatch.Dispose();
            }
        }

        private void fromLiveConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            inventorysource = "LIVE";
            HOForms.UPLOADINVENTORY uploadbatch = new HOForms.UPLOADINVENTORY();
         
            HOForms.UPLOADINVENTORY.branch = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            HOForms.UPLOADINVENTORY.shipmentno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            HOForms.UPLOADINVENTORY.supplierid = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString();
           
            uploadbatch.ShowDialog(this);
            if (HOForms.UPLOADINVENTORY.isdone == true)
            {
                filtertab();
                HOForms.UPLOADINVENTORY.isdone = false;
                uploadbatch.Dispose();
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            filtertab();
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            viewPODetails(gridView2);
        }

        private void gridControlProductForConfirmation_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStripForConfirmationProducts.Show(gridControlProductForConfirmation, e.Location);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            displayItems();  
        }

        void displayItems()
        {
            string shipmentno, supplierid, suppliername;
            shipmentno = gridViewProductForConfirmation.GetRowCellValue(gridViewProductForConfirmation.FocusedRowHandle, "ShipmentNo").ToString();
            supplierid = gridViewProductForConfirmation.GetRowCellValue(gridViewProductForConfirmation.FocusedRowHandle, "SupplierID").ToString();
            suppliername = Database.getSingleQuery("Supplier", "SupplierID='" + supplierid + "'", "SupplierName");

            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spout_APAccounts";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", shipmentno);
                com.Parameters.AddWithValue("@parmsupplierid", supplierid);
                com.Parameters.Add("@parminvoiceno", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parminvoicedate", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmvatamount", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmvatinputamount", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmvatexemptamount", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmactualcost", SqlDbType.Money).Direction = ParameterDirection.Output;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

                HOFormsDevEx.AddAPTransaction addap = new HOFormsDevEx.AddAPTransaction();
                double vatamount = Convert.ToDouble(com.Parameters["@parmvatamount"].Value.ToString());
                double vatexamount = Convert.ToDouble(com.Parameters["@parmvatexemptamount"].Value.ToString());
                double vatinputamount = Convert.ToDouble(com.Parameters["@parmvatinputamount"].Value.ToString());
                double totalamount = Convert.ToDouble(com.Parameters["@parmactualcost"].Value.ToString());
                addap.txtshipmentno.Text = shipmentno;
                addap.txtsupplierid.Text = supplierid;
                addap.txtsuppliername.Text = suppliername;
                addap.txtinvoiceno.Text = com.Parameters["@parminvoiceno"].Value.ToString();
                addap.txtinvoicedate.Text = com.Parameters["@parminvoicedate"].Value.ToString();

                addap.txtvatamount.Text = HelperFunction.convertToNumericFormat(vatamount);// vatamount.ToString();
                addap.txtvatexamount.Text = HelperFunction.convertToNumericFormat(vatexamount);
                addap.txtvatinputamount.Text = HelperFunction.convertToNumericFormat(vatinputamount);
                addap.txttotalamount.Text = HelperFunction.convertToNumericFormat(totalamount);

                addap.ShowDialog(this);
                if (HOFormsDevEx.AddAPTransaction.isdone == true)
                {
                    HOFormsDevEx.AddAPTransaction.isdone = false;
                    addap.Dispose();
                    filtertab();
                }
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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            viewPODetails(gridView1);
        }

        private void gridControlServicesForConfirmation_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                contextMenuStripForConfirmationServices.Show(gridControlServicesForConfirmation, e.Location);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            showServicesForConfirmation();
        }

        private void tabControlForConfirmation_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void tabControlConfirmed_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void gridView3_EditFormPrepared(object sender, EditFormPreparedEventArgs e)
        {

        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnForApprovalProd_Click(object sender, EventArgs e)
        {
            //Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR APPROVAL' And OrderType='P' AND CAST(DateOrder as date) between '{datefromForApprovalProd.Text}' and '{dateToForApprovalProd.Text}' ORDER BY ShipmentNo DESC", gridControl2, gridView2);
            string query = $"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR APPROVAL' And OrderType='P' AND CAST(DateOrder as date) between '{datefromForApprovalProd.Text}' and '{dateToForApprovalProd.Text}' ORDER BY ShipmentNo DESC";
            HelperFunction.ShowWaitAndDisplay(query, gridControl2, gridView2, "Please wait", "Populating data into the database...");
            gridView2.Focus();
        }

        private void btnApprovedProd_Click(object sender, EventArgs e)
        {
            //Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR DELIVERY' And OrderType='P' AND CAST(DateOrder as date) between '{dateFromApprovedProd.Text}' and '{dateToApprovedProd.Text}'  ORDER BY ShipmentNo DESC", gridControl3, gridView3);
            string query = $"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR DELIVERY' And OrderType='P' AND CAST(DateOrder as date) between '{dateFromApprovedProd.Text}' and '{dateToApprovedProd.Text}'  ORDER BY ShipmentNo DESC";
            HelperFunction.ShowWaitAndDisplay(query, gridControl3, gridView3, "Please wait", "Populating data into the database...");
            gridView3.Focus();
        }

        private void btnForConfirmProd_Click(object sender, EventArgs e)
        {
            //Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR CONFIRMATION' And OrderType='P' AND CAST(DateOrder as date) between '{dateFromForConfirmProd.Text}' and '{dateToForConfirmProd.Text}'  ORDER BY ShipmentNo DESC", gridControlProductForConfirmation, gridViewProductForConfirmation);
            string query = $"SELECT * FROM view_POSUMMARYREP WHERE Status='FOR CONFIRMATION' And OrderType='P' AND CAST(DateOrder as date) between '{dateFromForConfirmProd.Text}' and '{dateToForConfirmProd.Text}'  ORDER BY ShipmentNo DESC";
            HelperFunction.ShowWaitAndDisplay(query, gridControlProductForConfirmation, gridViewProductForConfirmation, "Please wait", "Populating data into the database...");
            gridViewProductForConfirmation.Focus();
        }

        private void btnConfirmedProd_Click(object sender, EventArgs e)
        {
            //Database.display($"SELECT * FROM view_POSUMMARYREP WHERE Status='CONFIRMED' And OrderType='P'  AND CAST(DateOrder as date) between '{dateFromConfirmedProd.Text}' and '{dateToConfirmedProd.Text}' ORDER BY ShipmentNo DESC", gridControlConfirmedProducts, gridViewConfirmedProducts);
            string query = $"SELECT * FROM view_POSUMMARYREP WHERE Status='CONFIRMED' And OrderType='P'  AND CAST(DateOrder as date) between '{dateFromConfirmedProd.Text}' and '{dateToConfirmedProd.Text}' ORDER BY ShipmentNo DESC";
            HelperFunction.ShowWaitAndDisplay(query, gridControlConfirmedProducts, gridViewConfirmedProducts, "Please wait", "Populating data into the database...");
            gridViewConfirmedProducts.Focus();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            action = "VIEWAPPROVED";
            viewPODetails(gridView3);
            
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripApproved.Show(gridControl3, e.Location);
        }

        private void tabControlApproved_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void editPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            action = "EDITPO";
            ADDPO addpo = new ADDPO();
           
            Database.display("SELECT * FROM view_PODETAILS WHERE ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "' and SupplierID='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString() + "'", addpo.gridControl1, addpo.gridView1);
            addpo.txtshipmentno.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString();
            string branch, targetdate,remakrs,suppid;
            var rowz = Database.getMultipleQuery("POSUMMARY", "ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "'", "SupplierID,BranchCode,TargetDate,Remarks");

            suppid = rowz["SupplierID"].ToString();
            branch = rowz["BranchCode"].ToString();
            targetdate = rowz["TargetDate"].ToString();
            remakrs = rowz["Remarks"].ToString();

            //string suppliername = Database.getSingleQuery("Supplier", "SupplierID='" + suppid + "'", "SupplierName");
            //addpo.searchLookUpEdit1.Text = suppliername;
            Database.displaySearchlookupEdit("SELECT SupplierName,ProductCategoryDescription,ProductCode,ProductName,CostKg from InventoryCost WHERE SupplierID='" + suppid + "'", addpo.srchprod, "ProductName", "ProductName");

            //branch = Database.getSingleQuery("POSUMMARY", "ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "'", "BranchCode");
            //targetdate = Database.getSingleQuery("POSUMMARY", "ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "'", "TargetDate");
            //remakrs = Database.getSingleQuery("POSUMMARY", "ShipmentNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ShipmentNo").ToString() + "'", "TargetDate");
            addpo.txtbranch.Text = branch;
            addpo.txtdate.Text = targetdate;
            addpo.txtremakrs.Text = remakrs;
            suppliername = Database.getSingleQuery("Supplier", "SupplierID='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SupplierID").ToString() + "' ", "SupplierName");
            addpo.ShowDialog(this);
            //addpo.searchLookUpEdit1.Text = suppliername;

        }
    }
}