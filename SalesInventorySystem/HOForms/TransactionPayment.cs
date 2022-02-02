using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class TransactionPayment : Form
    {
       
        string voucherid;
        public static bool isdone = false;
        public TransactionPayment()
        {
            InitializeComponent();
        }

        private void TransactionPayment_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            dateFrom.Text = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateTo.Text = lastDay.ToShortDateString();
            txtsupplierid.Text = SupplierAccounts.supplierid;
            txtsuppliername.Text = SupplierAccounts.suppliername;
            //display();
            populateRepositorySearchLookUp();
            populateCOA();
        }

        void populateCOA()
        {
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", searchLookUpEdit1, "AccountCode", "AccountCode");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //AddPaymentOLDBtn();
            if(String.IsNullOrEmpty(txtcheckdate.Text) || String.IsNullOrEmpty(searchLookUpEdit1.Text))
            {
                XtraMessageBox.Show("Date / GLCode Must not Empty!....");
                return;
            }
            else
            {
                AddPaymentBtnExecute();
            }
        }
        void AddPaymentBtnExecute()
        {
            int ctr = 0;
            for (int i = 0; i <= gridViewMaster.RowCount - 1; i++)
            {
                if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True")
                //if (Convert.ToBoolean(gridViewMaster.GetRowCellValue(i, "Pay").ToString()) == true)
                {
                    ctr += 1;
                    //break;
                }
                //if(gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True" && Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "DiscountAmount").ToString()) > 0 )
                //{
                //    XtraMessageBox.Show("Please provide GLCode for Discount Amount!");
                //    return;
                //}
                if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True" && Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "OffsetAmount").ToString()) > 0 && String.IsNullOrEmpty(gridViewMaster.GetRowCellValue(i, "OffsetGLCode").ToString()))
                {
                    XtraMessageBox.Show("Please provide GLCode for Offset Amount!");
                    return;
                }

            }

            if (ctr == 0)
            {
                XtraMessageBox.Show("No Payments Executed!");
                return;
            }
            else
            {
                for (int j = 0; j <= gridViewMaster.RowCount - 1; j++)
                {
                    if (gridViewMaster.GetRowCellValue(j, "Pay").ToString() == "True")
                    //if(Convert.ToBoolean(gridViewMaster.GetRowCellValue(j, "Pay").ToString()) == true)
                    {
                        addPaymentNew();
                        //postPaymentNew();
                    }
                }
            }
        }
    
        void addPaymentNew()
        {
            voucherid = IDGenerator.getIDNumberSP("sp_GetVoucherNumber", "TicketNumber"); //IDGenerator.getVoucherNumberSP();
            string description = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + searchLookUpEdit1.Text + "' ", "Description");
           
            try
            {
                
                string status="";

                if (radioButtonPurchase.Checked == true) { status = "PURCHASE"; }
                if (radioButtonExpense.Checked == true) { status = "EXPENSE"; }
                if (radioButtonButchery.Checked == true) { status = "BUTCHERY"; }
                if (radioButtonFreight.Checked == true) { status = "FREIGHT"; }

                string forliquidation = "";
                if (checkforliquidation.Checked == true) { forliquidation = "FOR LIQUIDATION"; }
                double totalamount = 0.0, amountpaid = 0.0;
                amountpaid = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid").ToString());
              
                for (int i = 0; i <= gridViewMaster.RowCount - 1; i++)
                {
                    if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True")
                    //if (Convert.ToBoolean(gridViewMaster.GetRowCellValue(i, "Pay").ToString()) == true)
                    {
                        
                        totalamount += Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString());
                        //string invoiceno = gridViewMaster.GetRowCellValue(i, "InvoiceNo").ToString();
                        //string invoicedate = gridViewMaster.GetRowCellValue(i, "InvoiceDate").ToString();
                        //string amount = gridViewMaster.GetRowCellValue(i, "Amount").ToString();
                        //string amountpaid1 = gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString();
                        //string type = gridViewMaster.GetRowCellValue(i, "Type").ToString();
                        //string discamount = gridViewMaster.GetRowCellValue(i, "DiscountAmount").ToString();
                        //string offsetamt = gridViewMaster.GetRowCellValue(i, "OffsetAmount").ToString();
                        //string variance = gridViewMaster.GetRowCellValue(i, "Variance").ToString();
                        //string brcode = gridViewMaster.GetRowCellValue(i, "BranchCode").ToString();
                        //string seqnum = gridViewMaster.GetRowCellValue(i, "SequenceNumber").ToString();
                        //string disglcode = gridViewMaster.GetRowCellValue(i, "DiscountGLCode").ToString();
                        //string offsetglcode = gridViewMaster.GetRowCellValue(i, "OffsetGLCode").ToString();
                        //string ewtamount = gridViewMaster.GetRowCellValue(i, "EWTAmount").ToString();
                        //string ewtdebit = gridViewMaster.GetRowCellValue(i, "EWTDebitGLCode").ToString();
                        //string ewtcredit = gridViewMaster.GetRowCellValue(i, "EWTCreditGLCode").ToString();
                        Database.ExecuteQuery("INSERT INTO CheckVoucherDetails VALUES('" + voucherid + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceDate").ToString() + "','" + searchLookUpEdit1.Text + "','" + description + "','" + gridViewMaster.GetRowCellValue(i, "ActualCost").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "Type").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "DiscountAmount").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "OffsetAmount").ToString() + "',0,'" + gridViewMaster.GetRowCellValue(i, "BranchCode").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "SequenceNumber").ToString() + "','MAPPING','" + gridViewMaster.GetRowCellValue(i, "OffsetGLCode").ToString() + "','','" + gridViewMaster.GetRowCellValue(i, "EWTAmount").ToString() + "',0)");
                        //Database.ExecuteCommandQuery("INSERT INTO CheckVoucherDetails VALUES('" + voucherid + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceDate").ToString() + "','" + searchLookUpEdit1.Text + "','" + acctcodedesc + "','" + gridViewMaster.GetRowCellValue(i, "Amount").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "Type").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "DiscountAmount").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "OffsetAmount").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "Variance").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "BranchCode").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "SequenceNumber").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "DiscountGLCode").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "OffsetGLCode").ToString() + "','',0)");
                    }
                }
                Database.ExecuteQuery("INSERT INTO CheckVoucher VALUES ('" + voucherid + "','" + txtsupplierid.Text + "','" + txtsuppliername.Text + "','" + txtcheckno.Text + "','" + txtcheckdate.Text + "','" + txtremakrs.Text + "','" + totalamount + "','" + Login.Fullname + "','','','','','" + forliquidation + "','" + status + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','0')");
                //Database.ExecuteCommandQuery("INSERT INTO CheckVoucher VALUES ('" + voucherid + "','" + txtsupplierid.Text + "','" + txtsuppliername.Text + "','" + txtcheckno.Text + "','" + txtcheckdate.Text + "','" + txtremakrs.Text + "','" + totalamount + "','" + Login.Fullname + "','','','','','" + forliquidation + "','" + status + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','0')");
                postPaymentNew();
                XtraMessageBox.Show("Payment Successfully Posted");
                isdone = true;
                this.Close();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            
            }
        }
        
        void postPaymentNew()
        {
            //string refnum = IDGenerator.getReferenceNumber();
            string refnum = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
         
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_AddPaymentSupplier";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmrefno", refnum);
                com.Parameters.AddWithValue("@parmvoucherid",voucherid);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplierid.Text);
                com.Parameters.AddWithValue("@parmsuppliername", txtsuppliername.Text);
                com.Parameters.AddWithValue("@parmcheckamount", txtamounttopay.Text);
                com.Parameters.AddWithValue("@parmcheckno", txtcheckno.Text);
                com.Parameters.AddWithValue("@parmcheckdate", txtcheckdate.Text);
                com.Parameters.AddWithValue("@parmcheckremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmpreparedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmglcode", searchLookUpEdit1.Text);
                com.Parameters.AddWithValue("@parmpaymethod", "");
                com.CommandTimeout = 180;
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

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridViewMaster.DeleteSelectedRows();
        }

      

        private void PreviewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridViewMaster.Columns["BranchCode"].Visible = true;
            gridViewMaster.Columns["Type"].Visible = true;
            gridViewMaster.Columns["Pay"].Visible = true;
            gridViewMaster.Columns["OffsetAmount"].Visible = true;
            gridViewMaster.Columns["OffsetGLCode"].Visible = true;
            gridViewMaster.Columns["DiscountAmount"].Visible = true;
            //gridViewMaster.Columns["DiscountGLCode"].Visible = true;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gridViewMaster.DeleteSelectedRows();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
           
        }

        void populateRepositorySearchLookUp()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                repositoryItemSearchLookUpEdit3.DataSource = table;
                repositoryItemSearchLookUpEdit3.DisplayMember = "AccountCode";
                repositoryItemSearchLookUpEdit3.ValueMember = "AccountCode";
                Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditoffsetglcode, "AccountCode", "AccountCode");
                Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditdiscountglcode, "AccountCode", "AccountCode");


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
        
        private void repositoryItemSearchLookUpEdit1_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= gridViewMaster.RowCount - 1; i++)
            {
                if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "NONE")
                {
                    gridViewMaster.DeleteRow(i);
                }
            }
        }

       

        void populate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControlMaster.BeginUpdate();
            try
            {
                bool ispurchase = false, isexpense = false, isbutchery = false, isfreight = false;

                //if (checkBoxPurchase.Checked == true) { ispurchase = true; }
                //if (checkBoxExpense.Checked == true) { isexpense = true; }
                //if (checkBoxButchery.Checked == true) { isbutchery = true; }
                //if (checkBoxFreight.Checked == true) { isfreight = true; }

                if (radioButtonPurchase.Checked == true) { ispurchase = true; }
                if (radioButtonExpense.Checked == true) { isexpense = true; }
                if (radioButtonButchery.Checked == true) { isbutchery = true; }
                if (radioButtonFreight.Checked == true) { isfreight = true; }

                string sp = "splist_Accounts";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmdatefrom", dateFrom.Text);
                com.Parameters.AddWithValue("@parmdateto", dateTo.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplierid.Text);
                com.Parameters.AddWithValue("@parmispurchase", ispurchase);
                com.Parameters.AddWithValue("@parmisexpense", isexpense);
                com.Parameters.AddWithValue("@parmisbutchery", isbutchery);
                com.Parameters.AddWithValue("@parmisfreight", isfreight);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridViewMaster.Columns.Clear();
                gridControlMaster.DataSource = null;
                adapter.Fill(table);
                gridControlMaster.DataSource = table;
                gridViewMaster.BestFitColumns();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                gridControlMaster.EndUpdate();
                con.Close();
            }
        }

        private void gridViewMaster_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double balance = 0.0, amountpaid = 0.0, diff = 0.0, discountamount = 0.0,ewt=0.0,ewtamount=0.0,netbal=0.0,cleanbal=0.0,actualcost=0.0; //, discountamount = 0.0, offsetamount = 0.0;
            ewt = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "EWT").ToString());
            discountamount = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "DiscountAmount").ToString());
            actualcost = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "ActualCost").ToString());
            balance = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance").ToString());
            amountpaid = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid").ToString());
            double discountAndAmountpaid = 0.0;
            discountAndAmountpaid = discountamount + amountpaid;
            diff = balance - amountpaid;
            ewtamount = ewt * balance;
            netbal = balance - ewtamount;
            cleanbal = actualcost - ewt - discountamount;
            //if (e.Column.FieldName == "Discount")
            //{
            //    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance", Math.Round(actualbalance, 2).ToString());
            //}
            if (e.Column.FieldName == "EWT")
            {
                gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "EWTAmount", Math.Round(ewtamount, 2).ToString());
            }
            if (e.Column.FieldName == "EWTAmount")
            {
              
                gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance", Math.Round(cleanbal, 2).ToString());
            }
            if (e.Column.FieldName == "AmountPaid")
            {
                gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Variance", Math.Round(diff, 2).ToString());
            }
            if (e.Column.FieldName == "DiscountAmount")
            {
                gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance", Math.Round(cleanbal, 2).ToString());
            }
            double totalamount = 0.0;
            for (int i = 0; i <= gridViewMaster.RowCount - 1; i++)
            {
                if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True")
                {
                    totalamount += Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString());
                }
            }
            txtamounttopay.Text = totalamount.ToString();
        }

        private void gridViewMaster_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Pay")
                // e.RepositoryItem = repositoryItemComboBox5;
                e.RepositoryItem = repositoryItemCheckEditStat;
            //if (e.Column.FieldName == "GLCode")
            //    e.RepositoryItem = repositoryItemSearchLookUpEdit3;
            //if (e.Column.FieldName == "VarianceType")
            //    e.RepositoryItem = repositoryItemComboBoxVarianceType;
            if (e.Column.FieldName == "OffsetGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditoffsetglcode;
            if (e.Column.FieldName == "DiscountGLCode")
                e.RepositoryItem = repositoryItemSearchLookUpEditdiscountglcode;
        }

        private void gridViewMaster_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Column.FieldName == "Balance")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            if (e.Column.FieldName == "AmountPaid")
            {
                e.Appearance.BackColor = Color.LightBlue;
            }
            if (e.Column.FieldName == "DiscountAmount")
            {
                e.Appearance.BackColor = Color.LightSalmon;
            }
            if (e.Column.FieldName == "OffsetAmount")
            {
                e.Appearance.BackColor = Color.LightCoral;
            }
            if (e.Column.FieldName == "Pay")
            {
                e.Appearance.BackColor = Color.LightGray;
            }
            //if (e.Column.FieldName == "VarianceType")
            //{
            //    e.Appearance.BackColor = Color.LightCoral;
            //}
            //if (e.Column.FieldName == "GLCode")
            //{
            //    e.Appearance.BackColor = Color.LightCyan;
            //}
            if (e.Column.FieldName == "OffsetGLCode")
            {
                e.Appearance.BackColor = Color.LightCyan;
            }
            if (e.Column.FieldName == "EWTAmount")
            {
                e.Appearance.BackColor = Color.LightCyan;
            }
        }

        private void gridViewMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            //view.FocusedColumn.FieldName != "VarianceType" || || view.FocusedColumn.FieldName != "Discount"
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Pay" ||  view.FocusedColumn.FieldName != "AmountPaid" || view.FocusedColumn.FieldName != "GLCode" || view.FocusedColumn.FieldName != "OffsetAmount" || view.FocusedColumn.FieldName != "OffsetGLCode" || view.FocusedColumn.FieldName != "DiscountAmount" || view.FocusedColumn.FieldName != "DiscountGLCode")
                e.Cancel = false;
        }

        private void repositoryItemCheckEditStat_CheckedChanged(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit edit = (DevExpress.XtraEditors.CheckEdit)sender;
          
        }

        private void repositoryItemCheckEditStat_EditValueChanged(object sender, EventArgs e)
        {
           
            //CheckEdit checkEdit = sender as CheckEdit;

            //if (Convert.ToBoolean(checkEdit.EditValue))
            //    for (int i = 0; i < gridViewMaster.RowCount - 1; i++)
            //    {
            //        //GridColumn col = gridViewMaster.Columns["Pay"];

            //        bool value = Convert.ToBoolean(gridViewMaster.GetRowCellValue(i, "Pay").ToString());
            //        if (value == true)
            //            oldIndex = i;

            //        if (i != gridViewMaster.FocusedRowHandle)
            //        {
            //            gridViewMaster.SetRowCellValue(i, "Pay", false);
            //        }
            //    }
        }

        private void btnextract_Click(object sender, EventArgs e)
        {
            populate();
            gridViewMaster.Columns[0].Visible = false;
            //gridViewMaster.Columns["GLCode"].Visible = false;

            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "AmountPaid");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "ActualCost");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "Balance");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "Discount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "EWTAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "OffsetAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "DiscountAmount");

            //GridGroupSummaryItem ite11 = new GridGroupSummaryItem();
            //ite11.FieldName = "AmountPaid";
            //ite11.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ite11.ShowInGroupColumnFooter = gridViewMaster.Columns["AmountPaid"];
            //gridViewMaster.GroupSummary.Add(ite11);
            //gridViewMaster.Columns["AmountPaid"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "AmountPaid", "{0:n2}");

            //GridGroupSummaryItem ite1111 = new GridGroupSummaryItem();
            //ite1111.FieldName = "ActualCost";
            //ite1111.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ite1111.ShowInGroupColumnFooter = gridViewMaster.Columns["ActualCost"];
            //gridViewMaster.GroupSummary.Add(ite1111);
            //gridViewMaster.Columns["ActualCost"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ActualCost", "{0:n2}");

            //GridGroupSummaryItem ite111 = new GridGroupSummaryItem();
            //ite111.FieldName = "Balance";
            //ite111.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //ite111.ShowInGroupColumnFooter = gridViewMaster.Columns["Balance"];
            //gridViewMaster.GroupSummary.Add(ite111);
            //gridViewMaster.Columns["Balance"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Balance", "{0:n2}");

            //GridGroupSummaryItem i1 = new GridGroupSummaryItem();
            //i1.FieldName = "Discount";
            //i1.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //i1.ShowInGroupColumnFooter = gridViewMaster.Columns["Discount"];
            //gridViewMaster.GroupSummary.Add(i1);
            //gridViewMaster.Columns["Discount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Discount", "{0:n2}");


            //GridGroupSummaryItem i2 = new GridGroupSummaryItem();
            //i2.FieldName = "EWTAmount";
            //i2.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //i2.ShowInGroupColumnFooter = gridViewMaster.Columns["EWTAmount"];
            //gridViewMaster.GroupSummary.Add(i2);
            //gridViewMaster.Columns["EWTAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "EWTAmount", "{0:n2}");

            //GridGroupSummaryItem i3 = new GridGroupSummaryItem();
            //i3.FieldName = "OffsetAmount";
            //i3.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //i3.ShowInGroupColumnFooter = gridViewMaster.Columns["OffsetAmount"];
            //gridViewMaster.GroupSummary.Add(i3);
            //gridViewMaster.Columns["OffsetAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "OffsetAmount", "{0:n2}");

            //GridGroupSummaryItem i4 = new GridGroupSummaryItem();
            //i4.FieldName = "DiscountAmount";
            //i4.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            //i4.ShowInGroupColumnFooter = gridViewMaster.Columns["DiscountAmount"];
            //gridViewMaster.GroupSummary.Add(i4);
            //gridViewMaster.Columns["DiscountAmount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DiscountAmount", "{0:n2}");
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //AddPaymentOLDBtn();
            if (String.IsNullOrEmpty(txtcheckdate.Text) || String.IsNullOrEmpty(searchLookUpEdit1.Text))
            {
                XtraMessageBox.Show("Date / GLCode Must not Empty!....");
                return;
            }
            else
            {
                AddPaymentBtnExecute();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            simpleButton5.PerformClick();
            try
            {
                var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                string companyname = row["Heading"].ToString();
                string imagewidth =  row["ImageWidth"].ToString();
                string imageheight = row["ImageHeight"].ToString();
                string caption1 =    row["Caption1"].ToString();
                string caption2 =    row["Caption2"].ToString();

                DevExReportTemplate.CheckVoucher xct = new DevExReportTemplate.CheckVoucher();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='CheckVoucher'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
                double amounttopay = Convert.ToDouble(txtamounttopay.Text);

                xct.xrcheckno.Text = txtcheckno.Text;
                xct.xrcheckdate.Text = txtcheckdate.Text;
                xct.xrpaidto.Text = txtsuppliername.Text;
                xct.xrparticular.Text = txtremakrs.Text;
                xct.xramount.Text = String.Format("{0:0,0.00}", amounttopay);

                //string str = Classes.Utilities.IntegerToWords(txtamounttopay.Text);
                string str = Classes.CurrencyConversion.ConvertToWords(txtamounttopay.Text);

                gridViewMaster.Columns["BranchCode"].Visible = false;
                gridViewMaster.Columns["Description"].Visible = false;
                gridViewMaster.Columns["Type"].Visible = false;
                gridViewMaster.Columns["Pay"].Visible = false;
                gridViewMaster.Columns["OffsetAmount"].Visible = false;
                gridViewMaster.Columns["OffsetGLCode"].Visible = false;
                gridViewMaster.Columns["DiscountAmount"].Visible = false;
                gridViewMaster.Columns["EWT"].Visible = false;
                //gridViewMaster.Columns["HpyAmount"].Visible = false;
                gridViewMaster.Columns["Discount"].Visible = false;
                //gridViewMaster.Columns["DiscountGLCode"].Visible = false;

                xct.xramountinwords.Text = str.ToUpper();
                xct.xrpreparedby.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControlMaster, gridViewMaster, "Pay"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.PreviewRibbonForm.FormClosed += PreviewForm_FormClosed;
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= gridViewMaster.RowCount - 1; i++)
            {
                if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "NONE")
                {
                    gridViewMaster.DeleteRow(i);
                }
            }
        }

       
    }
}
