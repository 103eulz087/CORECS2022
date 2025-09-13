using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POSStandAloneSetup
{
    public partial class POSStandAloneSettingsBoard : Form
    {
        public POSStandAloneSettingsBoard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.CustomersInfoDevEx cstfmrs = new HOFormsDevEx.CustomersInfoDevEx();
            cstfmrs.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProductCategory prodcat = new ProductCategory();
            prodcat.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //  MessageBox.Show("This feature is not allowed.. Please Contact your Product Manager");
            //HOForms.ProductSettings prodsets = new HOForms.ProductSettings(); ProductsDevEx
            //prodsets.ShowDialog(this);
            HOFormsDevEx.ProductsDevEx prodsets = new HOFormsDevEx.ProductsDevEx();
            prodsets.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //HOForms.ClientAccounts pcusatfsmr = new HOForms.ClientAccounts();
            HOFormsDevEx.ClientAccountsDevEx pcusatfsmr = new HOFormsDevEx.ClientAccountsDevEx();
            pcusatfsmr.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            POSDevEx.POSXReadReportDevEx viewinv = new POSDevEx.POSXReadReportDevEx();
            viewinv.ShowDialog(this);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            POSInventorySettings posnnd = new POSInventorySettings();
            posnnd.ShowDialog(this);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //POSDevEx.POSSettingsDevEx pcusatfsmr = new POSDevEx.POSSettingsDevEx();
            //pcusatfsmr.ShowDialog(this);
            POS.POSDetails posdet = new POS.POSDetails();
            posdet.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            POS.POSCashWalletDashboard pcusatfsmr = new POS.POSCashWalletDashboard();
            pcusatfsmr.ShowDialog(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            HOForms.Users housers = new HOForms.Users();
            housers.ShowDialog(this);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Branches.SyncPrize))
                {
                    form.Activate();
                    return;
                }
            }
            Branches.SyncPrize pcusatfsmr = new Branches.SyncPrize();
            pcusatfsmr.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.BackupDB))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.BackupDB pcusatfsmr = new HOForms.BackupDB();
            pcusatfsmr.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSStandAloneSetup.POSEmailAddresses))
                {
                    form.Activate();
                    return;
                }
            }
            POSStandAloneSetup.POSEmailAddresses pcusatfsmr = new POSStandAloneSetup.POSEmailAddresses();
            pcusatfsmr.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSDevEx.POSCreditCardMonitoring))
                {
                    form.Activate();
                    return;
                }
            }
            POSDevEx.POSCreditCardMonitoring pcusatfsmr = new POSDevEx.POSCreditCardMonitoring();
            pcusatfsmr.Show();
        }

        void EOF()
        {
            POS.POSEndOfDay oiajsd = new POS.POSEndOfDay();
            oiajsd.ShowDialog(this);
        }

        //void closedTransactions()
        //{
        //    try
        //    {
        //        if (Convert.ToDouble(0) > 0)
        //        {
        //            XtraMessageBox.Show("Cannot Continue there is an item that need to be purchased.");
        //        }
        //        else
        //        {
        //            //refno = textEdit3.Text.Trim();
        //            cashierTransactionCode = lblTransactionIDCashier.Text;
        //            refno = txtOrderNo.Text.Trim();
        //            transcode = lblTransactionIDInc.Text;

        //            POS.POSEndOfDay pocls = new POS.POSEndOfDay();
        //            pocls.txttransactionno.Text = transcode;//IDGenerator.getReferenceNumber(); //TRANSACTION REFERENCE #
                    
        //            pocls.txttransactiondate.Text = POSTransaction.getBeginningDate(); //TRANSACTION DATE

        //            pocls.txtBeginningBalance.Text = HelperFunction.numericFormat(POSTransaction.getBeginningCash());//.ToString(); //NEXT TRANSACTION BEGINNING CASH

        //            pocls.txtnoofsolditem.Text = POSTransaction.getTotalSoldItems().ToString(); //NO OF SOLD ITEMS
        //            pocls.txtnoofcancelleditem.Text = POSTransaction.getTotalCancelledItems().ToString(); //NO OF CANCELLED ITEMS
        //            pocls.txtnoofvoiditem.Text = POSTransaction.getTotalVoidItems().ToString(); //NO OF VOID ITEMS
        //            pocls.txtnoofreturneditem.Text = POSTransaction.getTotalReturnedItems().ToString(); //NO OF RETURNED ITEMS

        //            pocls.txtTotalCancelledTransaction.Text = HelperFunction.numericFormat(POSTransaction.getTotalCancelledTransactions());//.ToString(); //TOTAL AMOUNT OF CANCELLED ITEMS
        //            pocls.txtTotalVoidTransaction.Text = HelperFunction.numericFormat(POSTransaction.getTotalVoidTransactions());//.ToString(); //TOTAL AMOUNT OF VOID ITEMS
        //            pocls.txtTotalReturnedTransaction.Text = HelperFunction.numericFormat(POSTransaction.getTotalReturnedTransactions());//.ToString(); //TOTAL AMOUNT OF RETURNED ITEMS
        //            pocls.txtnoofdiscount.Text = POSTransaction.getNoOfDiscountItems().ToString(); //NO OF TOTAL DISCOUNT ITEM
        //            pocls.txtTotalDiscount.Text = HelperFunction.numericFormat(POSTransaction.getTotalDiscount());//,2).ToString(); //TOTAL AMOUNT OF DISCOUNT

        //            pocls.txtnoofcharges.Text = POSTransaction.getNoOfChargeItems().ToString(); ;//NO OF TOTAL CHARGES
        //            pocls.txtTotalCharges.Text = HelperFunction.numericFormat(POSTransaction.getTotalCharge());//.ToString(); ////TOTA AMOUNT OF CHARGES
        //            pocls.txtnoofvat.Text = POSTransaction.getNoOfVATItems().ToString(); ; //NO OF VAT CHARGES
        //            pocls.txtTotalTax.Text = HelperFunction.numericFormat(POSTransaction.getTotalTax());//,2).ToString(); //TOTAL AMOUNT OF TAX
        //            //pocls.txtTotalSales.Text = HelperFunction.numericFormat(POSTransaction.getTotalSales());//,2).ToString(); //TOTAL SALES
        //            //pocls.txtTotalCreditSales.Text = HelperFunction.numericFormat(POSTransaction.getTotalCreditSales());//,2).ToString(); //TOTAL SALES

        //            pocls.txttransactioncount.Text = POSTransaction.getTransactionCount().ToString(); //TRANSACTION COUNT
        //            pocls.txtbeginninginvoice.Text = POSTransaction.getBeginningInvoice().ToString(); //BEGINNING INVOICE
        //            pocls.txtbegorno.Text = POSTransaction.getBeginningORNo().ToString(); //BEGINNING OR NO
        //            pocls.txtlastornumber.Text = POSTransaction.getLastOrNo().ToString(); //LAST OR NO
        //            pocls.txtlasttranno.Text = POSTransaction.getLastTransactionNo().ToString(); //LAST TRANSACTION NUMBER

        //            pocls.txtvatablesales.Text = HelperFunction.numericFormat(POSTransaction.getVatableSales());//.ToString();
        //            pocls.txtvatexemptsale.Text = HelperFunction.numericFormat(POSTransaction.getVatExemptSales());//.ToString();
        //            pocls.txtnetsalesofvat.Text = HelperFunction.numericFormat(POSTransaction.getNetSalesOfVat());//.ToString();
        //            pocls.txtvatamount.Text = HelperFunction.numericFormat(POSTransaction.getVatAmount());//ToString();

                  
        //            pocls.ShowDialog(this);
        //            if (POSClosedTransaction.isdone == true)
        //            {
        //                POSClosedTransaction.isdone = false;
        //                pocls.Dispose();
        //                this.Dispose();
        //                // Classes.Utilities.writeTextfile("C:\\POSTransaction\\TranSeries\\counter.txt", transcode);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}

        private void button14_Click(object sender, EventArgs e)
        {
            EOF();
            //try
            //{
            //    bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Execute End of Day Transaction?", "End of Day Transaction!");
            //    if (ok)
            //    {
            //            AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            //            authfrm.ShowDialog(this);
            //            if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            //            {
            //                EOF();
            //                AuthorizedConfirmationFrm.isconfirmedLogin = false;
            //                authfrm.Dispose();
            //            }
            //        }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void button15_Click(object sender, EventArgs e)
        {
            POSStandAloneSetup.POSTypeSettings posnnd = new POSStandAloneSetup.POSTypeSettings();
            posnnd.ShowDialog(this);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            POS.PointOfSaleManual sadas = new POS.PointOfSaleManual();
            sadas.ShowDialog(this);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            POS.POSReprintReport posreprint = new POS.POSReprintReport();
            posreprint.ShowDialog(this);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POS.POSSalesReportDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            POS.POSSalesReportDevEx posrep = new POS.POSSalesReportDevEx();
            posrep.Show();
            //HOForms.POSTransactions postra = new HOForms.POSTransactions();
            //postra.Show();
        }
    }
}
