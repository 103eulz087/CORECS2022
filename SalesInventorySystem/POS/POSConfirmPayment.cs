using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Newtonsoft.Json;
using SalesInventorySystem.SalesModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSConfirmPayment : Form
    {
        object custcode = null;
        string customercode = "";
        string invno = "";

        SerialPort _serialPort = null;
        private delegate void SetTextDeleg(string text);
        public static bool transactiondone = false, isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false, isOnetimeDiscount = false;
        string paymenttype = "";
        public static string senioridno = "", seniorname = "", seniordiscountamount = "", netamountpayable = "", seniordiscountAmount = "", totalamountpayable = "", pwdIDNo = "", pwdName = "", pwdDiscountAmount = "", otherDiscountAmount = "", othersRemarks = "";
        public static string disctype = "", discname = "", discidno = "", discamount = "", discremarks = "";
        static DataGridView gview;
        public static string orderno = "", transno = "", merchantpaytype = "";
        public static string totalcashsales = "", totalcreditsales = "";

        private void radmerchant_CheckedChanged(object sender, EventArgs e)
        {
            if (radmerchant.Checked.Equals(true))  //MERCHANT
            {
                Database.ExecuteQuery("UPDATE dbo.POSType SET isEnableInvoicePrinting=0");
                txtamounttender.Text = txtamountpayable.Text;
                transno = lbltranscode.Text;
                orderno = lblorderno.Text;
                POS.POSPaytoMerchant ps = new POSPaytoMerchant();
                ps.txtamount.Text = txtamountpayable.Text;
                ps.ShowDialog(this);
                if (POS.POSPaytoMerchant.isdone == true)
                {

                    merchantpaytype = POS.POSPaytoMerchant.paytype;
                    txtamounttender.Text = POS.POSPaytoMerchant.amount;
                    POS.POSPaytoMerchant.isdone = false;
                    ps.Dispose();
                    button1.PerformClick();
                }
                else
                {
                    radcash.Checked = true;
                    txtamounttender.Text = "";
                    txtamountchange.Text = "";
                    txtamounttender.Focus();
                }
            }
        }

        private void radcash_CheckedChanged(object sender, EventArgs e)
        {
            if (radcash.Checked.Equals(true))  //CHARGE TO ACCOUNT
            {
                Database.ExecuteQuery("UPDATE dbo.POSType SET isEnableInvoicePrinting=0");
            }
        }

        private void radwallet_CheckedChanged(object sender, EventArgs e)
        {
            //if (radwallet.Checked.Equals(true))  //WALLET
            //{
            //    Database.ExecuteQuery("UPDATE dbo.POSType SET isEnableInvoicePrinting=0");
            //    netamountpayable = txtamountpayable.Text;
            //    POS.POSCashWalletTapper pasd = new POSCashWalletTapper();
            //    pasd.ShowDialog(this);
            //    if (POSCashWalletTapper.isdone == true)
            //    {
            //        //customercode = POSCashWalletTapper.clientid;
            //        txtamounttender.Text = txtamountpayable.Text;
            //        POSCashWalletTapper.isdone = false;
            //        pasd.Dispose();
            //        button1.PerformClick();
            //    }
            //    else
            //    {
            //        radcash.Checked = true;
            //        txtamounttender.Text = "";
            //        txtamountchange.Text = "";
            //        txtamounttender.Focus();
            //    }
            //}
            if (radwallet.Checked.Equals(true))  //WALLET
            {
                Database.ExecuteQuery("UPDATE dbo.POSType SET isEnableInvoicePrinting=0");
                //netamountpayable = txtamountpayable.Text;
                //POS.POSCashWalletTapper pasd = new POSCashWalletTapper();
                //pasd.ShowDialog(this);
                //if (POSCashWalletTapper.isdone == true)
                //{
                //    //customercode = POSCashWalletTapper.clientid;
                //    txtamounttender.Text = txtamountpayable.Text;
                //    POSCashWalletTapper.isdone = false;
                //    pasd.Dispose();
                //    button1.PerformClick();
                //}
                //else
                //{
                //    radcash.Checked = true;
                //    txtamounttender.Text = "";
                //    txtamountchange.Text = "";
                //    txtamounttender.Focus();
                //}

                POSplitBillFinal plslit = new POSplitBillFinal();
                plslit.txtamountpayable.Text = txtamountpayable.Text;
                plslit.txtinvoiceno.Text = lblorderno.Text;
                plslit.txtdiscount.Text = txtdiscount.Text;

                plslit.lblvatexemptsale.Text = lblvatexempt.Text;
                plslit.lblvatsale.Text = lblvatsale.Text;
                plslit.lblvat.Text = lblvatinput.Text;

                POSplitBillFinal.orderno = lblorderno.Text;
                POSplitBillFinal.cashiertransno = lbltranscode.Text;
                POSplitBillFinal.transno = lbltransno.Text;

                plslit.ShowDialog(this);
                if (POSplitBillFinal.isdone == true)
                {
                    txtamounttender.Text = txtamountpayable.Text;
                    totalcashsales = POSplitBillFinal.totalCashSales;
                    totalcreditsales = POSplitBillFinal.totalCreditSales;
                    POSplitBillFinal.isdone = false;
                    plslit.Dispose();
                    button1.PerformClick();
                }
                else
                {
                    radcash.Checked = true;
                    txtamounttender.Text = "";
                    txtamountchange.Text = "";
                    txtamounttender.Focus();
                }
                transactiondone = true;
                this.Close();
            }
        }

        private void txtcustnamelookup_EditValueChanged(object sender, EventArgs e)
        {
            custcode = SearchLookUpClass.getSingleValue(txtcustnamelookup, "CustomerID");
        }

        private void lblvatexempt_Click(object sender, EventArgs e)
        {

        }

        void checkZeroRated()
        {
            if (PointOfSale.iszeroratedsale == true)
            {
                string zeroratedsales = Database.getSingleResultSet("SELECT dbo.func_getZeroRatedSales('" + Login.assignedBranch + "','" + lblorderno.Text + "')");
            }

        }

        private void radcharge_CheckedChanged(object sender, EventArgs e)
        {
            if (radcharge.Checked.Equals(true))  //CHARGE TO ACCOUNT
            {
                //Database.ExecuteQuery("UPDATE dbo.POSType SET isEnableInvoicePrinting=1");
                POSChargeToClient poschrge = new POSChargeToClient();
                poschrge.txtorderno.Text = lblorderno.Text;
                poschrge.txtamount.Text = txtamountpayable.Text;
                poschrge.txtdiscountamnt.Text = txtamountpayable.Text;
                //poschrge.txtinvoiceno.Text = lblorderno.Text;
                poschrge.ShowDialog(this);
                if (POSChargeToClient.transactiondone == true)
                {
                    txtamounttender.Text = txtamountpayable.Text;
                    txtinvoiceno.Text = poschrge.txtinvoiceno.Text;
                    button1.PerformClick();
                    POSChargeToClient.transactiondone = false;
                    poschrge.Dispose();
                }
                else
                {
                    radcash.Checked = true;
                    txtamounttender.Text = "";
                    txtamountchange.Text = "";
                    txtamounttender.Focus();
                }
            }
        }

        private void radcc_CheckedChanged(object sender, EventArgs e)
        {
            
            if (radcc.Checked.Equals(true)) //CREDIT CARD
            {
                Database.ExecuteQuery("UPDATE dbo.POSType SET isEnableInvoicePrinting=0");
                txtamounttender.Text = txtamountpayable.Text;
                transno = lbltranscode.Text;
                orderno = lblorderno.Text;
                POS.POSPaymentDetails ps = new POSPaymentDetails();
                ps.groupCreditCardDetails.Visible = true;
                ps.ShowDialog(this);
                if (POSPaymentDetails.isdone == true)
                {

                    POSPaymentDetails.isdone = false;
                    ps.Dispose();
                    button1.PerformClick();
                }
                else
                {
                    radcash.Checked = true;
                    txtamounttender.Text = "";
                    txtamountchange.Text = "";
                    txtamounttender.Focus();
                }

            }
        }

        public POSConfirmPayment()
        {
            InitializeComponent();
        }

        private void POSConfirmPayment_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtamounttender;
            string invoicenum = IDGenerator.getIDNumber("BatchSalesSummary", "ReferenceNo", 10000).ToString();
            string referencenumber = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");// IDGenerator.getReferenceNumber();
            txtinvoiceno.Text = invoicenum.Trim();
            lblreferenceno.Text = referencenumber.Trim();

            if (String.IsNullOrEmpty(txtcustnamelookup.Text)) {
                string custkey = "000001";
                custcode = custkey.ToString();
            }

            txtamounttender.Focus();
            //Database.displayComboBoxItems("SELECT distinct CustomerName FROM dbo.Customers", "CustomerName", txtcustname);
            Database.displaySearchlookupEdit("SELECT CustomerID,CustomerName FROM dbo.Customers", txtcustnamelookup,"CustomerName", "CustomerName");

        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = _serialPort.ReadLine();

            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }
        private void si_DataReceived(string data)
        {
            txtamountchange.Text = data.Trim();
        }
        private void txtamounttender_TextChanged(object sender, EventArgs e)
        {
            double payable = 0.0;
            if (txtamounttender.Text == "")
            {
                return;
            }
            else
            {
                payable = Convert.ToDouble(txtamounttender.Text) - Convert.ToDouble(txtamountpayable.Text);
                txtamountchange.Text = Math.Round(payable, 2).ToString();
            }
        }

        private void txtamounttender_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }



        public async Task PushSaleAsync(SalesDataDto sale)
        {
            using (var client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(sale);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://itcore-apps.com:8181/api/sales", content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Sale pushed successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to push sale: " + response.ReasonPhrase);
                }
            }
        }



        public SalesDataDto GetInsertedSalesData(string orderNo, string posId)
        {
            SalesDataDto data = null;

            using (SqlConnection conn = Database.getConnection())
            {
                string query = "SELECT * FROM BatchSalesSummary WHERE ReferenceNo = @OrderNo AND MachineUsed = @POSID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderNo", orderNo);
                    cmd.Parameters.AddWithValue("@POSID", posId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string paymenttype = "", stat = "", disctype = "";
                            if (reader["PaymentType"].ToString() == "Cash") { paymenttype = "1"; } else if (reader["PaymentType"].ToString() == "Credit") { paymenttype = "1"; } else { paymenttype = " "; }
                            if (reader["Status"].ToString() == "SOLD") { stat = "1"; } else if (reader["Status"].ToString() == "Pending") { stat = "2"; } else if (reader["Status"].ToString() == "CANCELLED") { stat = "3"; } else if (reader["Status"].ToString() == "VOID") { stat = "4"; } else { stat = " "; }
                            if (reader["DiscountType"].ToString() == "SENIOR") { disctype = "1"; } else if (reader["DiscountType"].ToString() == "PWD") { disctype = "2"; } else if (reader["DiscountType"].ToString() == "REGULAR") { disctype = "3"; } else { disctype = " "; }
                            data = new SalesDataDto
                            {

                                TenantID = 1,//Convert.ToInt64(reader["TenantID"]),
                                POSID = reader["MachineUsed"].ToString(),//reader["POSID"].ToString(),
                                OrderNo = reader["ReferenceNo"].ToString(),//reader["OrderNo"].ToString(),
                                UserID = reader["CashierTransNo"].ToString(),
                                CustomerName = reader["CustomerNo"].ToString(),//reader["CustomerName"].ToString(),
                                TotalItem = Convert.ToInt32(reader["TotalItem"]),
                                TotalItemSold = Convert.ToInt32(reader["TotalItemSold"]),
                                TotalItemCancelled = reader["TotalItemCancelled"] != DBNull.Value ? Convert.ToInt32(reader["TotalItemCancelled"]) : 0,
                                TotalItemVoid = reader["TotalItemVoid"] != DBNull.Value ? Convert.ToInt32(reader["TotalItemVoid"]) : 0,
                                TotalItemReturned = reader["TotalItemReturned"] != DBNull.Value ? Convert.ToInt32(reader["TotalItemReturned"]) : 0,
                                TotalItemDiscount = reader["TotalItemDiscount"] != DBNull.Value ? Convert.ToInt32(reader["TotalItemDiscount"]) : 0,
                                TotalVatableItems = reader["TotalVatableItems"] != DBNull.Value ? Convert.ToInt32(reader["TotalVatableItems"]) : 0,
                                TotalNonVatableItems = 0,//Convert.ToInt32(reader["TotalNonVatableItems"]),
                                TotalSoldAmount = Convert.ToDecimal(reader["TotalSoldAmount"]),
                                TotalCancelledAmount = Convert.ToDecimal(reader["TotalCancelledAmount"]),
                                TotalVoidAmount = Convert.ToDecimal(reader["TotalVoidAmount"]),
                                TotalReturnedAmount = Convert.ToDecimal(reader["TotalReturnedAmount"]),
                                TotalDiscountAmount = Convert.ToDecimal(reader["TotalDiscountAmount"]),
                                TotalCharge = Convert.ToDecimal(reader["TotalCharge"]),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"]),//Convert.ToDecimal(reader["SubTotal"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                TotalVATSale = Convert.ToDecimal(reader["TotalVATSale"]),
                                TotalVATExemptSale = Convert.ToDecimal(reader["TotalVATExemptSale"]),
                                TotalVatableSale = Convert.ToDecimal(reader["TotalVatableSale"]),
                                TotalZeroRatedSale = Convert.ToDecimal(reader["ZeroRatedSale"]),
                                PaymentType = Convert.ToChar(paymenttype),
                                AmountTendered = Convert.ToDecimal(reader["AmountTendered"]),
                                AmountChange = Convert.ToDecimal(reader["AmountChange"]),
                                isFloat = Convert.ToBoolean(reader["isFloat"]),
                                isHold = Convert.ToBoolean(reader["isHold"]),
                                isVoid = Convert.ToBoolean(reader["isVoid"]),
                                Status = Convert.ToChar(stat),
                                DiscountType = Convert.ToChar(disctype),
                                SeniorControlNo = reader["SeniorControlNo"].ToString(),
                                SeniorName = reader["SeniorName"].ToString(),
                                SeniorDiscount = Convert.ToDecimal(reader["SeniorDiscount"]),
                                PwdIDNo = reader["PwdIDNo"].ToString(),
                                PwdName = reader["PwdName"].ToString(),
                                PwdDiscountAmount = Convert.ToDecimal(reader["PwdDiscountAmount"]),
                                DateAdded = Convert.ToDateTime(reader["TransDate"]),
                                //TimeAdded = (TimeSpan)reader["TimeAdded"],
                                DateTimeAdded = Convert.ToDateTime(reader["TransDate"])
                                //DateUpdated = Convert.ToDateTime(reader["DateUpdated"]),
                                //TimeUpdated = (TimeSpan)reader["TimeUpdated"],
                                //DateTimeUpdated = Convert.ToDateTime(reader["DateTimeUpdated"])
                            };
                        }
                    }
                }
            }

            return data;
        }

        async void pushit()
        {
            try
            {
                var sale = GetInsertedSalesData(lblorderno.Text.Trim(), Environment.MachineName.ToString());

                if (sale != null)
                {
                    await PushSaleAsync(sale);
                }
                else
                {
                    MessageBox.Show("No sale data found to push.");
                }
            }

            catch (Exception ex)
            {
                string errorMessage = $"Exception: {ex.Message}";

                if (ex.InnerException != null)
                {
                    errorMessage += $"\nInner Exception: {ex.InnerException.Message}";
                }

                MessageBox.Show(errorMessage);
            }



        }



        void spSaveTransaction(string discounttype, string invoiceno)
        {
            bool isRetail = Database.checkifExist("Select PosType FROM dbo.POSType WHERE PosType=1");
            bool OneTimeDisc = false,ZeroRated=false;
            if (isRetail)
            {
                OneTimeDisc = PointOfSale.isOnetimeDiscount;
                ZeroRated = PointOfSale.iszeroratedsale;
            }
            else
            {
                OneTimeDisc = POSRestoDineInBilling.isOnetimeDiscount;
                ZeroRated = POSRestoDineInBilling.iszeroratedsale;
            }
           
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_saveTransaction";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmrefno", lblorderno.Text.Trim());
                com.Parameters.AddWithValue("@parmcashiertransno", lbltranscode.Text.Trim());
                com.Parameters.AddWithValue("@parmreferenceno", lblreferenceno.Text.Trim());
                com.Parameters.AddWithValue("@parmtransno", lbltransno.Text.Trim());
                com.Parameters.AddWithValue("@parmamountpayable", txtamountpayable.Text.Trim());
                com.Parameters.AddWithValue("@parmamounttender", txtamounttender.Text.Trim());
                com.Parameters.AddWithValue("@parmamountchange", txtamountchange.Text.Trim());
                com.Parameters.AddWithValue("@parminvoice", invoiceno);
                com.Parameters.AddWithValue("@parmpaymenttype", paymenttype);
                com.Parameters.AddWithValue("@parmcustid", customercode); //PointOfSale.custcode
                com.Parameters.AddWithValue("@transby", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmvatsale", lblvatsale.Text);
                com.Parameters.AddWithValue("@parmvatexemptsale", lblvatexempt.Text);
                com.Parameters.AddWithValue("@parmvat", lblvatinput.Text);
                com.Parameters.AddWithValue("@parmdiscount", txtdiscount.Text);
                //com.Parameters.AddWithValue("@parmseniorcontrolno", senioridno);//txtseniorcontrolno.Text);
                //com.Parameters.AddWithValue("@parmseniorname", seniorname);//txtseniorname.Text);
                com.Parameters.AddWithValue("@parmonetimediscount", OneTimeDisc);//isOnetimeDiscount);
                //com.Parameters.AddWithValue("@parmseniordiscountamount", seniordiscountamount);//seniordiscountAmount);
                com.Parameters.AddWithValue("@parmdiscounttype", discounttype);
                //com.Parameters.AddWithValue("@parmpwdidno", pwdIDNo);
                //com.Parameters.AddWithValue("@parmpwdname", pwdName);
                //com.Parameters.AddWithValue("@parmpwddiscountamount", pwdDiscountAmount);
                com.Parameters.AddWithValue("@parmdiscidno", discidno);
                com.Parameters.AddWithValue("@parmdiscname", discname);
                com.Parameters.AddWithValue("@parmdiscamount", discamount);

                com.Parameters.AddWithValue("@parmiszeroratedsale", ZeroRated);
                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());

                com.Parameters.AddWithValue("@parmtotalcashsales", totalcashsales);
                com.Parameters.AddWithValue("@parmtotalcreditsales", totalcreditsales);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                transactiondone = false;
                this.Dispose();
            }
            finally
            {
                con.Close();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            //if (keyData == Keys.Enter)
            //{
            //    button1.PerformClick();
            //}
            if (keyData == Keys.Escape)
            {
                button2.PerformClick();
            }
            else if (keyData == Keys.F1)
            {
                radcash.Checked = true;
            }
            //else if (keyData == Keys.F2)
            //{
            //    radcc.Checked = true;
            //}
            //else if (keyData == Keys.F3)
            //{
            //    radmerchant.Checked = true;
            //}
            //else if (keyData == Keys.F4)
            //{
            //    radwallet.Checked = true;
            //}
            //else if (keyData == Keys.F5)
            //{
            //    radcharge.Checked = true;
            //}
            return functionReturnValue;
        }

        private void execute()
        {
            string discounttype = "";
            if (isSeniorDiscount == true)
            {
                discounttype = "SENIOR";
            }
            else if (isPwdDiscount == true)
            {
                discounttype = "PWD";
            }
            else if (isOthersDiscount == true)
            {
                discounttype = "REGULAR";
            }
            spSaveTransaction(discounttype, invno);
            //pushit();
        }
        bool haveOneTimeDiscount()
        {
            bool ok = false;
            ok = Database.checkifExist("SELECT TOP 1 OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + lblorderno.Text + "' and isErrorCorrect=0");
            return ok;
        }

        void ExecutePayment()
        {
            try
            {

                //update as of 04142020
                if (radcash.Checked.Equals(true))
                {
                    paymenttype = "Cash";
                    invno = txtinvoiceno.Text;
                    customercode = custcode.ToString();
                }
                else if (radcc.Checked.Equals(true))
                {
                    paymenttype = "Credit";
                    invno = txtinvoiceno.Text;
                    customercode = custcode.ToString();
                    //update as of 04142020
                    Database.ExecuteQuery("INSERT INTO dbo.POSCreditCardTransactions VALUES('" + lbltranscode.Text + "','" + lblorderno.Text + "','" + Login.assignedBranch + "','" + DateTime.Now.ToString() + "','" + POSPaymentDetails.creditcardname + "','" + POSPaymentDetails.creditcardnum + "','" + POSPaymentDetails.creditcardtype + "','" + POSPaymentDetails.creditcardexpirydate + "','" + POSPaymentDetails.creditcardbankname + "','" + POSPaymentDetails.creditcardmerchant + "','" + POSPaymentDetails.creditcardrefno + "','" + txtamountpayable.Text + "','0','" + DateTime.Now.ToString() + "',' ','" + Login.Fullname + "','" + GlobalVariables.computerName + "','"+lblcashiertransno.Text+"')");
                    //update as of 04142020
                }
                else if (radmerchant.Checked.Equals(true))
                {
                    paymenttype = "Merchant";// merchantpaytype;// 
                    customercode= custcode.ToString(); 
                    //customercode = POSPaytoMerchant.merchantname;

                    //update as of 04142020
                    bool isClear = false;
                    if (merchantpaytype == "Credit") { isClear = false; } else { isClear = true; }
                    Database.ExecuteQuery("INSERT INTO dbo.POSMerchantTransactions VALUES('" + Login.assignedBranch + "'" +
                        ",'" + DateTime.Now.ToString() + "','" + Environment.MachineName + "','" + lblorderno.Text + "','" + POSPaytoMerchant.refno + "'" +
                        ",'" + POSPaytoMerchant.merchantname + "','" + POSPaytoMerchant.vouchercode + "','" + txtamountpayable.Text + "'" +
                        ",'" + DateTime.Now.ToString() + "','" + Login.isglobalUserID + "','" + isClear + "',' ')");
                    ////update as of 04142020
                    invno = txtinvoiceno.Text;

                }
                else if (radwallet.Checked.Equals(true))
                {
                    paymenttype = "CashWallet";
                    customercode = custcode.ToString(); 
                    //customercode = POSCashWalletTapper.clientkey;
                    invno = txtinvoiceno.Text;
                }
                else if (radcharge.Checked.Equals(true))
                {
                    paymenttype = "ChargeToAccount";
                    customercode= custcode.ToString();
                    //customercode = POSChargeToClient.customercode;
                    invno = txtinvoiceno.Text; //POSChargeToClient.invoiceNum;


                }

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////

                if (String.IsNullOrEmpty(txtamounttender.Text))
                {
                    XtraMessageBox.Show("Please Input Tender Amount!.");
                    txtamounttender.Focus();
                }
                else if (Convert.ToDouble(txtamounttender.Text) < Convert.ToDouble(txtamountpayable.Text))
                {
                    XtraMessageBox.Show("Tender Amount must not less than Amount Payable");
                    txtamounttender.Focus();
                }
                else if (String.IsNullOrEmpty(paymenttype) || paymenttype == " ")
                {
                    XtraMessageBox.Show("Please Select Payment Type");
                    txtamounttender.Focus();
                }
                else
                {





                    if (btncaller.Text == "POS")
                    {
                        gview = PointOfSale.mygridview;
                    }
                    else if (btncaller.Text == "RETAILWITHDASHBOARD")
                    {
                        //gview = POS.POSMainWithDashboard.mygridview;
                        gview = POS.POSRestoDineInBilling.mygridview;
                    }

                    //if (radmerchant.Checked.Equals(true))//Merchant 
                    //{
                    //    //paymenttype = "Merchant";
                    //    bool isClear = false;
                    //    if (merchantpaytype == "Credit") { isClear = false; } else { isClear = true; }
                    //    Database.ExecuteQuery("INSERT INTO POSMerchantTransactions VALUES('" + Login.assignedBranch + "'" +
                    //        ",'" + DateTime.Now.ToString() + "','" + Environment.MachineName + "','"+lblorderno.Text+"','" + POSPaytoMerchant.refno + "'" +
                    //        ",'" + POSPaytoMerchant.merchantname + "','" + POSPaytoMerchant.vouchercode + "','" + txtamountpayable.Text + "'" +
                    //        ",'" + DateTime.Now.ToString() + "','" + Login.isglobalUserID + "','"+ isClear + "',' ')");
                    //}

                    //else if (radcc.Checked.Equals(true))
                    //{
                    //    Database.ExecuteQuery("INSERT INTO POSCreditCardTransactions VALUES('" + lbltranscode.Text + "','" + lblorderno.Text + "','" + Login.assignedBranch + "','" + DateTime.Now.ToString() + "','" + POSPaymentDetails.creditcardname + "','" + POSPaymentDetails.creditcardnum + "','" + POSPaymentDetails.creditcardtype + "','" + POSPaymentDetails.creditcardexpirydate + "','" + POSPaymentDetails.creditcardbankname + "','" + POSPaymentDetails.creditcardmerchant + "','" + POSPaymentDetails.creditcardrefno + "','" + txtamountpayable.Text + "','0','" + DateTime.Now.ToString() + "',' ','" + Login.Fullname + "','"+GlobalVariables.computerName+"')");
                    //}


                    netamountpayable = "";
                    netamountpayable = txtamountpayable.Text;
                    double netamount = 0.0, totaldue = 0.0;
                    totaldue = Convert.ToDouble(txtamountpayableb4onetimediscount.Text);
                    netamount = Convert.ToDouble(netamountpayable);

                    string vatablesales = String.Format("{0:n2}", Convert.ToDouble(lblvatsale.Text));
                    string vatexemptsales = String.Format("{0:n2}", Convert.ToDouble(lblvatexempt.Text));
                    string vat = String.Format("{0:n2}", Convert.ToDouble(lblvatinput.Text));

                    string amounttender = String.Format("{0:n2}", Convert.ToDouble(txtamounttender.Text));
                    string change = String.Format("{0:n2}", Convert.ToDouble(txtamountchange.Text));
                    string amountpayable = String.Format("{0:n2}", Convert.ToDouble(txtamountpayable.Text));

                    string netofvatafteronetimedisc = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInDiscountedItems('" + Login.assignedBranch + "','" + lblorderno.Text + "')");
                    string netOfVatAfterNonOneTimeDisc = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInNonDiscountedItems('" + Login.assignedBranch + "','" + lblorderno.Text + "')");

                    execute();
                    //after execute of SP check if the Status of BatchSalesSummary is Equal to SOLD
                    bool checkIfSold = Database.checkifExist($"SELECT TOP(1) ReferenceNo FROM dbo.BatchSalesSummary " +
                        $"WHERE BranchCode='{Login.assignedBranch}' " +
                        $"AND MachineUsed='{Environment.MachineName}' " +
                        $"AND ReferenceNo='{lblorderno.Text}' " +
                        $"AND Status='SOLD' ");
                    if (!checkIfSold) //if status is not sold
                    {
                        XtraMessageBox.Show("Process Interrupted...Please Confirm Payment Again!..");
                        return;
                    }
                    else
                    {
                        Printing printit = new Printing();
                        bool haveDiscount = false;
                        haveDiscount = haveOneTimeDiscount();
                        bool clientEmail = false;
                        if (chckboxeinvoicemail.Checked == true) { clientEmail = true; } else { clientEmail = false; }
                        //if (String.IsNullOrEmpty(txteinvoicemail.Text)) { clientEmail = false; } else { clientEmail = true; }
                        if (haveDiscount)
                        {
                            var rows = Database.getMultipleQuery("SalesDiscount", "OrderNo='" + lblorderno.Text + "' and isErrorCorrect=0",
                           "DiscountType,DiscountAmount,DiscName,DiscIDNo,DiscRemarks");

                            string DiscountType, DiscountAmount, DiscName, DiscIDNo, DiscRemarks;

                            //
                            DiscountType = rows["DiscountType"].ToString();
                            DiscountAmount = rows["DiscountAmount"].ToString();
                            DiscName = rows["DiscName"].ToString();
                            DiscIDNo = rows["DiscIDNo"].ToString();
                            DiscRemarks = rows["DiscRemarks"].ToString();

                            //GLOBAL VARIABLES
                            disctype = DiscountType;
                            discname = DiscName;
                            discidno = DiscIDNo;
                            discamount = DiscountAmount;


                            printit.printReceipt(lbltransno.Text    //string transcode
                                                , lblorderno.Text   //string ordercode
                                                , HelperFunction.convertToNumericFormat(totaldue)   //string total
                                                , txtordinarydiscountamount.Text    //string peritemdiscount
                                                , netofvatafteronetimedisc  //string NET OF VAT AFTER ONE TIME DISCOUNT
                                                , netOfVatAfterNonOneTimeDisc   //string vatable of non-senior items
                                                , vatablesales  //string vatablesale
                                                , vatexemptsales    //string vatexemptsale
                                                , vat   //string vat
                                                , amounttender  //string cash
                                                , change    //string change
                                                , gview //DataGridView gridview
                                                , haveDiscount  //bool isDiscount
                                                , disctype  //string disctype
                                                , "CLIENT-COPY" //string footerlabel
                                                , txtcustnamercpt.Text  //string name
                                                , txtcustaddressrcpt.Text   //string address
                                                , txtcusttinrcpt.Text   //string tin
                                                , txtcustbussstyle.Text //string bussstyle	
                                                , paymenttype); //paymenttype


                            printit.printReceiptConsolidated(lbltranscode.Text, lbltransno.Text, lblorderno.Text, HelperFunction.convertToNumericFormat(totaldue), txtordinarydiscountamount.Text, netofvatafteronetimedisc, netOfVatAfterNonOneTimeDisc, vatablesales, vatexemptsales, vat, amounttender, change, gview, haveDiscount, disctype, "CLIENT-COPY", txtcustnamercpt.Text, txtcustaddressrcpt.Text, txtcusttinrcpt.Text, txtcustbussstyle.Text, paymenttype);
                            printit.printReceipt(lbltransno.Text, lblorderno.Text, HelperFunction.convertToNumericFormat(totaldue), txtordinarydiscountamount.Text, netofvatafteronetimedisc, netOfVatAfterNonOneTimeDisc, vatablesales, vatexemptsales, vat, amounttender, change, gview, haveDiscount, disctype, "ACCOUNTING-COPY", txtcustnamercpt.Text, txtcustaddressrcpt.Text, txtcusttinrcpt.Text, txtcustbussstyle.Text, paymenttype);
                            printit.printReceiptConsolidated(lbltranscode.Text, lbltransno.Text, lblorderno.Text, HelperFunction.convertToNumericFormat(totaldue), txtordinarydiscountamount.Text, netofvatafteronetimedisc, netOfVatAfterNonOneTimeDisc, vatablesales, vatexemptsales, vat, amounttender, change, gview, haveDiscount, disctype, "ACCOUNTING-COPY", txtcustnamercpt.Text, txtcustaddressrcpt.Text, txtcusttinrcpt.Text, txtcustbussstyle.Text, paymenttype);

                        }
                        else //if no discount
                        {
                            printit.printReceipt(lbltransno.Text, lblorderno.Text, HelperFunction.convertToNumericFormat(totaldue), txtordinarydiscountamount.Text, vatablesales, vatexemptsales, vat, amounttender, change, gview, haveDiscount, txtcustnamercpt.Text, txtcustaddressrcpt.Text, txtcusttinrcpt.Text, txtcustbussstyle.Text, paymenttype, txteinvoicemail.Text,PointOfSale.iszeroratedsale, clientEmail);
                            printit.printReceiptConsolidated(lbltranscode.Text, lbltransno.Text, lblorderno.Text, HelperFunction.convertToNumericFormat(totaldue), txtordinarydiscountamount.Text, vatablesales, vatexemptsales, vat, amounttender, change, gview, haveDiscount, txtcustnamercpt.Text, txtcustaddressrcpt.Text, txtcusttinrcpt.Text, txtcustbussstyle.Text, paymenttype, PointOfSale.iszeroratedsale);
                        }
                        printit.ReprintReceipt(lbltransno.Text, lblorderno.Text, HelperFunction.convertToNumericFormat(totaldue), txtordinarydiscountamount.Text, netofvatafteronetimedisc, netOfVatAfterNonOneTimeDisc, vatablesales, vatexemptsales, vat, amounttender, change, gview, haveDiscount, disctype, "ACCOUNTING-COPY", txtcustnamercpt.Text, txtcustaddressrcpt.Text, txtcusttinrcpt.Text, txtcustbussstyle.Text, paymenttype);
                        

                        string isprinting = Database.getSingleQuery("POSType", "PosType is not null", "isEnableInvoicePrinting");
                        if (Convert.ToBoolean(isprinting) == true)
                        {
                            printSalesInvoice();
                        }
                        //printSalesInvoice();
                        transactiondone = true;
                        this.Close();
                    }

                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void analyze(string spname, string pono, GridControl cont, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            //gridControl1.BeginUpdate();
            try
            {
                string sp = spname;
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmpono", pono);

                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
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
                //gridControl1.EndUpdate();
                con.Close();
            }
        }


        void printSalesInvoice()
        {
            string custkey = "", custname = "", custaddress = "", custterm = "", TinNo="";

            //custkey = Database.getSingleQuery("TransactionChargeSales", $"ReferenceNo='{lblorderno.Text}' ", "CustomerKey");
            custkey = Database.getSingleQuery("BatchSalesSummary", $"ReferenceNo='{lblorderno.Text}' AND BranchCode='{Login.assignedBranch}'", "CustomerNo");
            var row = Database.getMultipleQuery("Customers", "CustomerKey='" + custkey + "'", "CustomerName,CustomerAddress,Term,TinNo");

            custname = row["CustomerName"].ToString();
            custaddress = row["CustomerAddress"].ToString();
            custterm = row["Term"].ToString();
            TinNo = row["TinNo"].ToString();


            Reporting.SalesInvoiceDexEx viewdet = new Reporting.SalesInvoiceDexEx();


            analyze("spview_SalesInvoice", lblorderno.Text, viewdet.gridControl4, viewdet.gridView4);

            viewdet.txtpono.Text = lblorderno.Text;
            viewdet.txtcustkey.Text = custkey;
            viewdet.txtcustname.Text = custname;
            viewdet.txtcustaddress.Text = custaddress;
            viewdet.txtterm.Text = custterm;
            viewdet.txtcusttin.Text = TinNo;

            double vatablesales = 0.0, vatexemptsale = 0.0, zeroratedsale = 0.0,vatamount = 0.0, totalsales = 0.0, lessvat = 0.0, netofvat = 0.0, amountdue = 0.0, addvat = 0.0, vatsales = 0.0, totalamountdue = 0.0;
            for (int i = 0; i <= viewdet.gridView4.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(viewdet.gridView4.GetRowCellValue(i, "isVat").ToString()) == true)
                {
                    vatablesales += Convert.ToDouble(viewdet.gridView4.GetRowCellValue(i, "Amount").ToString());
                }
                if (Convert.ToBoolean(viewdet.gridView4.GetRowCellValue(i, "isVat").ToString()) == false)
                {
                    vatexemptsale += Convert.ToDouble(viewdet.gridView4.GetRowCellValue(i, "Amount").ToString());
                }
            }
            bool isZeroRated = Database.checkifExist($"SELECT TOP(1) * FROM dbo.BatchSalesSummary WHERE ReferenceNo='{lblorderno.Text}' AND BranchCode='{Login.assignedBranch}' AND ZeroRatedSale<>0");
            bool isOnetimeDiscount = Database.checkifExist($"SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='{lblorderno.Text}' and isErrorCorrect=0");
            if(!isOnetimeDiscount) 
            {
                if (!isZeroRated) //exists
                {
                    vatsales = Math.Round(vatablesales / 1.12, 2);
                    vatamount = Math.Round(vatsales * 0.12, 2);
                    totalsales = Math.Round(vatablesales + vatexemptsale, 2);

                    lessvat = vatamount;
                    netofvat = totalsales - vatamount;
                    amountdue = netofvat;
                    addvat = vatamount;
                    totalamountdue = totalsales;
                }
                else //ZERO RATED SALES
                {
                    vatsales = 0;
                    vatamount = 0;
                   
                    totalsales = Math.Round(vatablesales + vatexemptsale, 2);
                    zeroratedsale = totalsales;

                    lessvat = 0;
                    netofvat = 0;
                    amountdue = totalsales;
                    addvat = 0;
                    totalamountdue = Math.Round(totalsales/1.12,2);
                }

                viewdet.txtvatablesale.Text = vatsales.ToString();
                viewdet.txtvatexemptsale.Text = vatexemptsale.ToString();
                //zero rated
                viewdet.txtzeroratedsale.Text = zeroratedsale.ToString();
                viewdet.txtvatamount.Text = vatamount.ToString();
                viewdet.txttotalsales.Text = totalsales.ToString();
                viewdet.txtlessvat.Text = lessvat.ToString();
                viewdet.txtamountnetofvat.Text = netofvat.ToString();
                viewdet.txtamountdue.Text = amountdue.ToString();
                viewdet.txtaddvat.Text = addvat.ToString();
                viewdet.txttotalamountdue.Text = totalamountdue.ToString();
                viewdet.ShowDialog(this);
            }
            else //SNIOR PWD REGULAR
            {
                string netOfVatAfterNonOneTimeDisc = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInNonDiscountedItems('" + Login.assignedBranch + "','" + lblorderno.Text + "')");
                string netofvatafteronetimedisc = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInDiscountedItems('" + Login.assignedBranch + "','" + lblorderno.Text + "')");
                double lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0;//,  totaltotal = 0.0;

                if (!isZeroRated) //exists
                {
                    netofnonscdisc = Convert.ToDouble(netOfVatAfterNonOneTimeDisc); //netOfVatAfterNonOneTimeDisc
                    lessvat = Math.Round(Convert.ToDouble(netofvatafteronetimedisc) * 0.12, 2); //netofvatafteronetimedisc
                    netofvat = Math.Round(Convert.ToDouble(netofvatafteronetimedisc), 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(0.12), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    amountdue = Math.Round(netofscdisc + addvat, 2); ; //totaltotal = Math.Round(netofscdisc + addvat, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totalamountdue = Convert.ToDouble(netamountpayable);
                }
                else
                {
                    vatsales = 0;
                    vatamount = 0;

                    totalsales = Math.Round(vatablesales + vatexemptsale, 2);
                    zeroratedsale = totalsales;

                    lessvat = 0;
                    netofvat = 0;
                    amountdue = totalsales;
                    addvat = 0;
                    totalamountdue = totalsales;
                }
              


                double totalvatableSales = netofscdisc + netofnonscdisc; //**
                //double totalVatInputSale = 0.0; //**
                double totalVatInputSale = totalvatableSales * 0.12; //**

                //details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;// vat) + Environment.NewLine;
                //// details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;


                viewdet.txtvatablesale.Text = totalvatableSales.ToString();
                viewdet.txtvatexemptsale.Text = vatexemptsale.ToString();
                //zero rated sale
                viewdet.txtzeroratedsale.Text = zeroratedsale.ToString();
                viewdet.txtvatamount.Text = totalVatInputSale.ToString();
                viewdet.txttotalsales.Text = Math.Round(vatablesales,2).ToString();//totalsales.ToString();
                viewdet.txtlessvat.Text = lessvat.ToString();
                viewdet.txtamountnetofvat.Text = netofvat.ToString();
                viewdet.txtamountdue.Text = amountdue.ToString();
                viewdet.txtaddvat.Text = addvat.ToString();
                viewdet.txttotalamountdue.Text = totalamountdue.ToString();
                viewdet.ShowDialog(this);
            }


           
            //bool isOnetimeDiscount = Database.checkifExist("SELECT TOP 1 OrderNo FROM SalesDiscount WHERE OrderNo='" + lblorderno.Text + "' and isErrorCorrect=0");

            //string netofvatafteronetimedisc = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInDiscountedItems('" + Login.assignedBranch + "','" + lblorderno.Text + "')");
            //string netOfVatAfterNonOneTimeDisc = Database.getSingleResultSet("SELECT dbo.func_getNetOfVatInNonDiscountedItems('" + Login.assignedBranch + "','" + lblorderno.Text + "')");

            //double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            //netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            //lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
            //netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
            //lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
            ////lessscdisc = Math.Round(netofvat * 0.05, 2);
            //netofscdisc = Math.Round(netofvat - lessscdisc, 2);
            //addvat = Math.Round(netofscdisc * .12, 2);
            //totaltotal = Math.Round(netofscdisc + addvat, 2);
            //details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //details += HelperFunction.createDottedLine() + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;

            //double totalvatableSales = netofscdisc + netofnonscdisc; //**
            //double totalVatInputSale = 0.0; //**
            //totalVatInputSale = totalvatableSales * 0.12; //**

            //details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;// vat) + Environment.NewLine;
            //// details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //get default walkin in customers table
          
            if (txtamounttender.Text.Length > 10)
            {
                //show authentication
                //AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                //authfrm.ShowDialog(this);
                //if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                //{
                //    ExecutePayment();
                //    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                //    authfrm.Dispose();
                //}
                txtamounttender.Text = "";
                txtamountchange.Text = "";
                txtamounttender.Focus();
            }
            else if ( String.IsNullOrEmpty(txtinvoiceno.Text) && radcharge.Checked == true) //(String.IsNullOrEmpty(txtcustname.Text) ||
            {
                XtraMessageBox.Show("Please Input Customer Name");
            }
            else
            {
                ExecutePayment();
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            transactiondone = false;
            this.Close();
        }
        
        private void txtamounttender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                isSeniorDiscount = false;
                isPwdDiscount = false;
                isOthersDiscount = false;

                txtseniorcontrolno.Focus();
                totalamountpayable = txtamountpayable.Text;
                double discountAmount = 0.0, cleanamount = 0.0, newtotalamount = 0.0;
                //totalamount = Convert.ToDouble(lblTotalAmount);
                discountAmount = Convert.ToDouble(txtamountpayable.Text) * 0.05;
                cleanamount = Convert.ToDouble(txtamountpayable.Text) - discountAmount;
                AddDiscount adis = new AddDiscount();
                adis.txtamnttobediscount.Text = txtamountpayable.Text;
                adis.txtdiscountamount.Text = discountAmount.ToString();
                
                adis.ShowDialog(this);
                if (AddDiscount.isdone == true)
                {
                    seniordiscountAmount = "";
                    pwdDiscountAmount = "";
                    otherDiscountAmount = "";
                    txtdiscount.Text = AddDiscount.discountamount;
                    newtotalamount = Convert.ToDouble(txtamountpayable.Text) - Convert.ToDouble(txtdiscount.Text);
                    txtamountpayable.Text = newtotalamount.ToString();
                    if (AddDiscount.isSeniorDiscount==true)
                    {
                        txtseniorcontrolno.Text = AddDiscount.controlno;
                        txtseniorname.Text = AddDiscount.name;
                        //display();
                        
                        seniordiscountAmount = AddDiscount.discountamount;
                        isSeniorDiscount = true;
                    }
                    else if(AddDiscount.isPwdDiscount == true)
                    {
                        pwdIDNo = AddDiscount.pwdidno;
                        pwdName = AddDiscount.pwdname;
                        pwdDiscountAmount = AddDiscount.discountamount;
                        isPwdDiscount = true;
                    }
                    else if (AddDiscount.isOthersDiscount == true)
                    {
                        otherDiscountAmount = AddDiscount.discountamount;
                        othersRemarks = AddDiscount.remarks;
                        isOthersDiscount = true;
                    }
                    isOnetimeDiscount = AddDiscount.isOnetimeDiscount;
                    AddDiscount.isdone = false;
                    AddDiscount.isSeniorDiscount = false;
                    AddDiscount.isPwdDiscount = false;
                    AddDiscount.isOthersDiscount = false;
                    adis.Dispose();
                    txtamounttender.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
