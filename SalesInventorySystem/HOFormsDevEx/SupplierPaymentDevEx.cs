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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class SupplierPaymentDevEx : DevExpress.XtraEditors.XtraForm
    {
        string referenceno = "";
        string voucherid;
        string vouchertype = "";
        string status = "";
        public static bool isdone = false,forliquidation=false;
        public SupplierPaymentDevEx()
        {
            InitializeComponent();
        }

        void radChanged()
        {
            if(radCashVoucher.Checked==true)
            {
                panelCheckVoucher.Visible = false;
            }
            else if (radCheckVoucher.Checked == true)
            {
                panelCheckVoucher.Visible = true;
            }
        }

        private void SupplierPaymentDevEx_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            dateFrom.Text = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateTo.Text = lastDay.ToShortDateString();
            //display();
            populateCOA();
            radChanged();
        }
        void populateCOA()
        {
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", searchLookUpEdit1, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditoffsetdebitglcode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditoffsetCreditGLCode, "AccountCode", "AccountCode");
            Database.displayRepositorySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts WHERE AccountType='D'", repositoryItemSearchLookUpEditdiscountglcode, "AccountCode", "AccountCode");
        }

        void confirmPayment()
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you Sure?", "Confirm Payment");
            if (confirm == true)
            {
                if (radCheckVoucher.Checked == true) //CHECK
                {
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
                else //CASH
                {
                    if (String.IsNullOrEmpty(searchLookUpEdit1.Text))
                    {
                        XtraMessageBox.Show("Credit GLCode Must not Empty!....");
                        return;
                    }
                    else
                    {
                        AddPaymentBtnExecute();
                    }
                }
            }
           
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            confirmPayment();
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
                //if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True" && Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "ReturnAllowances").ToString()) > 0 && String.IsNullOrEmpty(gridViewMaster.GetRowCellValue(i, "OffsetDebitGLCode").ToString()) && String.IsNullOrEmpty(gridViewMaster.GetRowCellValue(i, "OffsetCreditGLCode").ToString()))
                ////if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True" && Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "OffsetAmount").ToString()) > 0 && String.IsNullOrEmpty(gridViewMaster.GetRowCellValue(i, "OffsetDebitGLCode").ToString()) && String.IsNullOrEmpty(gridViewMaster.GetRowCellValue(i, "OffsetCreditGLCode").ToString()))
                //{
                //    XtraMessageBox.Show("Please provide GLCode for Offset Amount!");
                //    return;
                //}

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
            //lastvoucher id is incremental sequence number per supplier
            referenceno = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber"); //IDGenerator.getVoucherNumberSP(); //not used
            int lastvoucherid = 0;
           
            if (radCheckVoucher.Checked==true)
            {
                lastvoucherid = Database.getLastID("CheckVoucher", "SupplierID='" + txtsupplierid.Text + "'", "VoucherID") + 1;
                voucherid = lastvoucherid.ToString();// IDGenerator.getIDNumberSP("sp_GetVoucherNumber", "TicketNumber"); //IDGenerator.getVoucherNumberSP(); //not used
                vouchertype = "CHECK";
            }
            else
            {
                lastvoucherid = Database.getLastID("CashVoucher", "SupplierID='" + txtsupplierid.Text + "'", "VoucherID") + 1;
                voucherid = lastvoucherid.ToString();// IDGenerator.getIDNumberSP("sp_GetVoucherNumber", "TicketNumber"); //IDGenerator.getVoucherNumberSP(); //not used
                vouchertype = "CASH";
            }

            string description = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + searchLookUpEdit1.Text + "' ", "Description");

            try
            {
                if (radioButtonPurchase.Checked == true) { status = "PURCHASE"; }
                if (radioButtonExpense.Checked == true) { status = "EXPENSE"; }
                if (checkforliquidation.Checked == true) { forliquidation = true; }
                double totalamount = 0.0, amountpaid = 0.0;
                amountpaid = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid").ToString());

                for (int i = 0; i <= gridViewMaster.RowCount - 1; i++)
                {
                    if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "True")
                    //if (Convert.ToBoolean(gridViewMaster.GetRowCellValue(i, "Pay").ToString()) == true)
                    {

                        totalamount += Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString());
                        //reference of APPaymentDetails is InvoiceNumber and SequenceNumber
                        //PAYMENT METHOD = INVOICE PAYMENT
                        if (Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO APPaymentDetails VALUES('" + lastvoucherid + "','" + txtsupplierid.Text + "','" + referenceno + "','" + gridViewMaster.GetRowCellValue(i, "BranchCode").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceDate").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "AmountPaid").ToString() + "','INVOICE PAYMENT','" + status + "','"+ vouchertype + "',' ',' ',' ','" + gridViewMaster.GetRowCellValue(i, "SequenceNumber").ToString() + "')");
                        }
                        if (Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "EWTAmount").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO APPaymentDetails VALUES('" + lastvoucherid + "','" + txtsupplierid.Text + "','" + referenceno + "','" + gridViewMaster.GetRowCellValue(i, "BranchCode").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceDate").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "EWTAmount").ToString() + "','EWT','" + status + "','" + vouchertype + "',' ',' ',' ','" + gridViewMaster.GetRowCellValue(i, "SequenceNumber").ToString() + "')");
                        }
                        //PAYMENT METHOD = DISCOUNT
                        if (Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "DiscountAmount").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO APPaymentDetails VALUES('" + lastvoucherid + "','" + txtsupplierid.Text + "','" + referenceno + "','" + gridViewMaster.GetRowCellValue(i, "BranchCode").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceDate").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "DiscountAmount").ToString() + "','DISCOUNT','" + status + "','" + vouchertype + "',' ',' ',' ','" + gridViewMaster.GetRowCellValue(i, "SequenceNumber").ToString() + "')");
                        }
                        //PAYMENT METHOD = OFFSET
                        if (Convert.ToDouble(gridViewMaster.GetRowCellValue(i, "ReturnAllowances").ToString()) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO APPaymentDetails VALUES('" + lastvoucherid + "','" + txtsupplierid.Text + "','" + referenceno + "','" + gridViewMaster.GetRowCellValue(i, "BranchCode").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceNo").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "InvoiceDate").ToString() + "','" + gridViewMaster.GetRowCellValue(i, "ReturnAllowances").ToString() + "','RETURNALLOWANCES','" + status + "','" + vouchertype + "',' ',' ',' ','" + gridViewMaster.GetRowCellValue(i, "SequenceNumber").ToString() + "')");
                        }
                    }
                }
                //Database.ExecuteQuery("INSERT INTO CheckVoucher VALUES ('" + lastvoucherid + "','" + txtsupplierid.Text + "','" + txtsuppliername.Text + "','" + txtcheckno.Text + "','" + txtcheckdate.Text + "','" + txtremakrs.Text + "','" + totalamount + "','" + Login.Fullname + "','','','','','" + forliquidation + "','" + status + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortDateString() + "','0')");
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
            //string refnum = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");

            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_AddPaymentSupplier";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmrefno", referenceno);
                com.Parameters.AddWithValue("@parmvoucherid", voucherid);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplierid.Text);
                com.Parameters.AddWithValue("@parmsuppliername", txtsuppliername.Text);
                com.Parameters.AddWithValue("@parmcheckamount", txtamounttopay.Text);
                com.Parameters.AddWithValue("@parmcheckno", txtcheckno.Text);
                com.Parameters.AddWithValue("@parmcheckdate", txtcheckdate.Text);
                com.Parameters.AddWithValue("@parmcheckremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmpreparedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmglcode", searchLookUpEdit1.Text);
                com.Parameters.AddWithValue("@parmpaymethod", status);
                com.Parameters.AddWithValue("@parmforliquidation", forliquidation);
                com.Parameters.AddWithValue("@parmvouchertype", vouchertype);
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
        void populate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControlMaster.BeginUpdate();
            try
            {
                bool ispurchase = false, isexpense = false;

                if (radioButtonPurchase.Checked == true) { ispurchase = true; }
                if (radioButtonExpense.Checked == true) { isexpense = true; }
             

                string sp = "splist_Accounts";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmdatefrom", dateFrom.Text);
                com.Parameters.AddWithValue("@parmdateto", dateTo.Text);
                com.Parameters.AddWithValue("@parmsupplierid", txtsupplierid.Text);
                com.Parameters.AddWithValue("@parmispurchase", ispurchase);
                com.Parameters.AddWithValue("@parmisexpense", isexpense);
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
            double balance = 0.0, amountpaid = 0.0, diff = 0.0, discountamount = 0.0, ewt = 0.0,  netbal = 0.0, cleanbal = 0.0, actualcost = 0.0; //, discountamount = 0.0, offsetamount = 0.0;
            //ewt = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "EWT").ToString());
            discountamount = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "DiscountAmount").ToString());
            actualcost = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "ActualCost").ToString());
            balance = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance").ToString());
            amountpaid = Convert.ToDouble(gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid").ToString());
            double discountAndAmountpaid = 0.0;
            discountAndAmountpaid = discountamount + amountpaid;
            diff = balance - amountpaid;
            //ewtamount = ewt * balance;
            netbal = amountpaid + ewt + discountamount;
            cleanbal = actualcost - ewt - discountamount;

            //if (e.Column.FieldName == "Discount")
            //{
            //    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance", Math.Round(actualbalance, 2).ToString());
            //}

            //if (e.Column.FieldName == "EWTAmount")
            //{

            //    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance", Math.Round(cleanbal, 2).ToString());
            //}
            //if (e.Column.FieldName == "AmountPaid")
            //{
            //    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Variance", Math.Round(diff, 2).ToString());
            //}
            //if (e.Column.FieldName == "DiscountAmount")
            //{
            //    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance", Math.Round(cleanbal, 2).ToString());
            //}

            //if (e.Column.FieldName == "Pay")
            //{

            //if ((string)e.Value == "True")
            if (e.Value == "True")
            {
                //gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid", gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance").ToString());
                HOFormsDevEx.SupplierAddPaymentDevEx asdds = new SupplierAddPaymentDevEx();
                asdds.txtshipno.Text = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "ShipmentNo").ToString();
                asdds.txtinvoiceno.Text = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "InvoiceNo").ToString();
                asdds.txtinvoicedate.Text = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "InvoiceDate").ToString();
                asdds.txtactualcost.Text = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "ActualCost").ToString();
                asdds.txtbalance.Text = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "Balance").ToString();
                asdds.groupControl1.Text = txtsupplierid.Text + "-" + txtsuppliername.Text;
                asdds.ShowDialog(this);
                if (HOFormsDevEx.SupplierAddPaymentDevEx.isdone == true)
                {
                    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid", HOFormsDevEx.SupplierAddPaymentDevEx.amountpaid);
                    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "DiscountAmount", HOFormsDevEx.SupplierAddPaymentDevEx.discount);
                    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "EWTAmount", HOFormsDevEx.SupplierAddPaymentDevEx.ewt);
                    gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "ReturnAllowances", HOFormsDevEx.SupplierAddPaymentDevEx.offset);
                    //gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "OffsetAmount", HOFormsDevEx.SupplierAddPaymentDevEx.offset);
                    HOFormsDevEx.SupplierAddPaymentDevEx.isdone = false;
                    asdds.Dispose();
                }
            }
            //else if ((string)e.Value == "False")
            else if (e.Value == "False")
            {
                gridViewMaster.SetRowCellValue(gridViewMaster.FocusedRowHandle, "AmountPaid", "0");
            }
            //}

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

        private void gridViewMaster_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Pay")
                // e.RepositoryItem = repositoryItemComboBox5;
                e.RepositoryItem = repositoryItemCheckEditStat;
            //if (e.Column.FieldName == "GLCode")
            //    e.RepositoryItem = repositoryItemSearchLookUpEdit3;
            //if (e.Column.FieldName == "VarianceType")
            //    e.RepositoryItem = repositoryItemComboBoxVarianceType;
            //if (e.Column.FieldName == "OffsetDebitGLCode")
            //    e.RepositoryItem = repositoryItemSearchLookUpEditoffsetdebitglcode; 
            //if (e.Column.FieldName == "OffsetCreditGLCode")
            //    e.RepositoryItem = repositoryItemSearchLookUpEditoffsetdebitglcode;
            //if (e.Column.FieldName == "DiscountGLCode")
            //    e.RepositoryItem = repositoryItemSearchLookUpEditdiscountglcode;
        }

        private void gridViewMaster_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Column.FieldName == "Balance")
            {
                e.Appearance.ForeColor = Color.Red;
            }
            if (e.Column.FieldName == "AmountPaid")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            if (e.Column.FieldName == "DiscountAmount")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            //if (e.Column.FieldName == "OffsetAmount")
            if (e.Column.FieldName == "ReturnAllowances")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            if (e.Column.FieldName == "Pay")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
            //if (e.Column.FieldName == "VarianceType")
            //{
            //    e.Appearance.BackColor = Color.LightCoral;
            //}
            //if (e.Column.FieldName == "GLCode")
            //{
            //    e.Appearance.BackColor = Color.LightCyan;
            //}
            //if (e.Column.FieldName == "OffsetDebitGLCode")
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //} 
            //if (e.Column.FieldName == "OffsetCreditGLCode")
            //{
            //    e.Appearance.BackColor = Color.LightYellow;
            //}
            if (e.Column.FieldName == "EWTAmount")
            {
                e.Appearance.BackColor = Color.LightYellow;
            }
        }
        //USED TO EDIT CELL 
        private void gridViewMaster_ShowingEditor(object sender, CancelEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (view.FocusedColumn.FieldName != "Pay" || view.FocusedColumn.FieldName != "AmountPaid" || view.FocusedColumn.FieldName != "OffsetAmount" || view.FocusedColumn.FieldName != "OffsetDebitGLCode" || view.FocusedColumn.FieldName != "OffsetCreditGLCode" || view.FocusedColumn.FieldName != "DiscountAmount")
            //    e.Cancel = false;
        }

        private void btnextract_Click(object sender, EventArgs e)
        {
            populate();
            gridViewMaster.Columns[0].Visible = false;
            //if (radioButtonExpense.Checked == true)
            //{
            //    //gridViewMaster.Columns["EWTAmount"].Visible = false;
            //    gridViewMaster.Columns["DiscountAmount"].Visible = false;
            //}
          

            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "AmountPaid");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "ActualCost");
            //Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "Balance");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "DiscountAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "EWTAmount");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "ReturnAllowances");
            //Classes.DevXGridViewSettings.ShowFooterTotal(gridViewMaster, "OffsetAmount");
        }

        void printVoucher()
        {
            btnfilter.PerformClick();
            try
            {
                var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                string companyname = row["Heading"].ToString();
                string imagewidth = row["ImageWidth"].ToString();
                string imageheight = row["ImageHeight"].ToString(); 
                string caption1 = row["Caption1"].ToString(); 
                string caption2 = row["Caption2"].ToString();

                DevExReportTemplate.CheckVoucher xct = new DevExReportTemplate.CheckVoucher();
                xct.Landscape = false;

                Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='CheckVoucher'", "ImageLogo");
                xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                xct.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                xct.xrcompanyname.Text = companyname;
                xct.xrcaption1.Text = caption1;
                xct.xrcaption2.Text = caption2;
                xct.xrcheckno.Text = txtcheckno.Text;

                xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
                double amounttopay = Convert.ToDouble(txtamounttopay.Text);

               
                xct.xrcheckdate.Text = txtcheckdate.Text;
                xct.xrpaidto.Text = txtsuppliername.Text;
                xct.xrparticular.Text = txtremakrs.Text;
                xct.xramount.Text = String.Format("{0:0,0.00}", amounttopay);
                //public static string ToWords(this decimal value)
                //long str = long.Parse(txtamounttopay.Text);
                //string str = Classes.Utilities.IntegerToWords(Convert.ToDouble(txtamounttopay.Text));
                //string str = Classes.CurrencyConversion.ConvertToWords(txtamounttopay.Text);
                string str = Classes.DecimalToWordExtension.ToWords(Convert.ToDecimal(txtamounttopay.Text));

                gridViewMaster.Columns["Balance"].Visible = false;
                gridViewMaster.Columns["BranchCode"].Visible = false;
                //gridViewMaster.Columns["Description"].Visible = false;
                //gridViewMaster.Columns["Type"].Visible = false;
                gridViewMaster.Columns["Pay"].Visible = false;
                //gridViewMaster.Columns["OffsetAmount"].Visible = false;
                //gridViewMaster.Columns["OffsetDebitGLCode"].Visible = false;
                //gridViewMaster.Columns["OffsetCreditGLCode"].Visible = false;
                gridViewMaster.Columns["Variance"].Visible = false;

                //gridViewMaster.Columns["HpyAmount"].Visible = false;
                //gridViewMaster.Columns["DiscountGLCode"].Visible = false;

                xct.xramountinwords.Text = str.ToString().ToUpper();
                //xct.xramountinwords.Text = str.ToUpper();
                xct.xrpreparedby.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControlMaster, gridViewMaster, "Pay"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void printCashVoucher()
        {
            btnfilter.PerformClick();
            try
            {
                var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='CheckVoucher'", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                string companyname = row["Heading"].ToString();
                string imagewidth = row["ImageWidth"].ToString();
                string imageheight = row["ImageHeight"].ToString();
                string caption1 = row["Caption1"].ToString();
                string caption2 = row["Caption2"].ToString();

                DevExReportTemplate.Cash5Voucher xct = new DevExReportTemplate.Cash5Voucher();
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

                
                xct.xrcheckdate.Text = txtcheckdate.Text;
                xct.xrpaidto.Text = txtsuppliername.Text;
                xct.xrparticular.Text = txtremakrs.Text;
                xct.xramount.Text = String.Format("{0:0,0.00}", amounttopay);
                //public static string ToWords(this decimal value)
                //long str = long.Parse(txtamounttopay.Text);
                //string str = Classes.Utilities.IntegerToWords(Convert.ToDouble(txtamounttopay.Text));
                //string str = Classes.CurrencyConversion.ConvertToWords(txtamounttopay.Text);
                string str = Classes.DecimalToWordExtension.ToWords(Convert.ToDecimal(txtamounttopay.Text));

                gridViewMaster.Columns["Balance"].Visible = false;
                gridViewMaster.Columns["BranchCode"].Visible = false;
                //gridViewMaster.Columns["Description"].Visible = false;
                //gridViewMaster.Columns["Type"].Visible = false;
                gridViewMaster.Columns["Pay"].Visible = false;
                //gridViewMaster.Columns["OffsetAmount"].Visible = false;
                //gridViewMaster.Columns["OffsetDebitGLCode"].Visible = false;
                //gridViewMaster.Columns["OffsetCreditGLCode"].Visible = false;
                gridViewMaster.Columns["Variance"].Visible = false;

                //gridViewMaster.Columns["HpyAmount"].Visible = false;
                //gridViewMaster.Columns["DiscountGLCode"].Visible = false;

                xct.xramountinwords.Text = str.ToString().ToUpper();
                //xct.xramountinwords.Text = str.ToUpper();
                xct.xrpreparedby.Text = Login.Fullname;
                xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(gridControlMaster, gridViewMaster, "Pay"));
                xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(xct);
                report.ShowRibbonPreviewDialog();

            }
            catch (FormatException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if(radCashVoucher.Checked==true)
            {
                printCashVoucher();
            }
            else if(radCheckVoucher.Checked==true)
            {
                if (String.IsNullOrEmpty(txtcheckno.Text) || String.IsNullOrEmpty(txtcheckdate.Text))
                {
                    XtraMessageBox.Show("Please Filled-out the CheckNo/CheckDate Field");
                }
                else
                {
                    printVoucher();
                }
            }
          
        }

        private void gridViewMaster_Click(object sender, EventArgs e)
        {
            
        }

        private void radCashVoucher_CheckedChanged(object sender, EventArgs e)
        {
            radChanged();
        }

        private void radCheckVoucher_CheckedChanged(object sender, EventArgs e)
        {
            radChanged();
        }

        private void contextMenuStrip4_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string refno = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "ReferenceNo").ToString();
            string invoiceno = gridViewMaster.GetRowCellValue(gridViewMaster.FocusedRowHandle, "InvoiceNo").ToString();
            if(radioButtonExpense.Checked == true && gridViewMaster.RowCount > 0)
            {
                bool checkifNotYetProcessed = Database.checkifExist("SELECT TOP 1 InvoiceNo " +
                "FROM ExpenseMaster " +
                "WHERE InvoiceNo='" + invoiceno + "' " +
                "AND SupplierID='" + txtsupplierid.Text + "' " +
                "AND ReferenceNumber='" + refno + "' " +
                "AND AmountPaid=0 " +
                "AND Status='UNPAID' " +
                "AND isErrorCorrect=0 ");
                if (checkifNotYetProcessed)
                {
                    bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Execute as ErrorCorrect this Transaction? ", "Confirm Error Correct");
                    if (confirm)
                    {
                        Database.ExecuteQuery("UPDATE ExpenseMaster SET isErrorCorrect=1 " +
                        "WHERE ReferenceNumber='" + refno + "' " +
                        "AND InvoiceNo='" + invoiceno + "' " +
                        "AND SupplierID='" + txtsupplierid.Text + "' ");
                        Database.ExecuteQuery("UPDATE TicketMaster SET BranchCode=BranchCode+'X' " +
                            "WHERE ReferenceNumber='" + refno + "' " +
                            "AND ReferenceKey='" + invoiceno + "' " +
                            "AND OWNER='" + txtsuppliername.Text + "' ");
                        Database.ExecuteQuery("UPDATE TicketDetails SET BranchCode=BranchCode+'X' " +
                            "WHERE ReferenceNumber='" + refno + "' " +
                            "AND ReferenceKey='" + invoiceno + "' ", "Error Correct Successfully Executed");
                    }
                    else
                    {
                        return;
                    }
                    btnextract.PerformClick();
                }
                else
                {
                    XtraMessageBox.Show("You cannot Cancel this Invoice, because it is already processed...");
                    return;
                }
            }
        }

        private void gridControlMaster_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlMaster, e.Location);
        }

        private void btnfilter_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= gridViewMaster.RowCount - 1; i++)
            {
                if (gridViewMaster.GetRowCellValue(i, "Pay").ToString() == "NONE")
                {
                    gridViewMaster.DeleteRow(i);

                    for (int j = 0; j <= gridViewMaster.RowCount - 1; j++)
                    {
                        if (gridViewMaster.GetRowCellValue(j, "Pay").ToString() == "NONE")
                        {
                            gridViewMaster.DeleteRow(j);
                        }
                    }

                }
            }
            
        }
    }
}