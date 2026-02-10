using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using System.IO.Ports;
using System.Threading;
using System.IO;
using System.Diagnostics;
using SalesInventorySystem.POS;
using SalesInventorySystem.SalesModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace SalesInventorySystem
{
    
    public partial class PointOfSale : DevExpress.XtraEditors.XtraForm
    {
        public static string reprintlabel = "REPRINT";
        //SerialPort serialPort1;
        private delegate void SetTextDeleg(string text);
        DataTable table;
        public static string _transcode,_refno,_pname, _uprice, _qtysold, _totamount,_discount,ispriceused="mainprice";
        public static int _rowctr;
        string amount,totaldiscount;
        public static string totalamountstr="0",strtext;
        public static string transcode,refno="", prodcode, pname, uprice, qtysold, totamount,machinename,custname,custcode;
        public static double totalamount = 0.0, totalkilo = 0.0;
        public static DataGridView mygridview;
        public static string sequenceNum = "";
        
        public static string  vatablesale, vatexemptsale, vat;
        bool isusedbarcode = false, isusedsearchform = false,  isDataInserted = false;
        string constring = "";
        public static string seniorcontrolno = "", seniorname = "";

        //FOR DISCOUNT
        public static string txtseniorcontrolno="", txtseniorname = "", seniordiscountAmount = "", pwdIDNo = "", pwdName = "", pwdDiscountAmount = "", otherDiscountAmount = "", othersRemarks = "";
        public static bool isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false, isOnetimeDiscount = false;
        public static string cashierTransactionCode = "";
        public static bool iszeroratedsale = false;
        public static string userid = "";
        private System.Windows.Forms.Timer statusTimer;
        SqlCommand com;
        public PointOfSale()
        {
            InitializeComponent();
            InitializeStatusChecker();
            //serialPort1.WriteTimeout = 500;
            //serialPort1.ReadTimeout = 500;
            //_timer = new Timer();
            //_timer.Interval = 1000;
            //_timer.Tick += new EventHandler(Timer_Tick);
            MydataGridView1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }



        private void InitializeStatusChecker()
        {
            statusTimer = new System.Windows.Forms.Timer();
            statusTimer.Interval = 5000; // Check every 5 seconds
            statusTimer.Tick += (s, e) =>
            {
                using (var conn = Database.getConnection())
                {
                    try
                    {
                        conn.Open();
                        labelControl22.Text = "Connected";
                        labelControl22.ForeColor = System.Drawing.Color.Green;
                    }
                    catch
                    {
                        labelControl22.Text = "Offline";
                        labelControl22.ForeColor = System.Drawing.Color.Red;
                    }
                }
            };
            statusTimer.Start();
        }


        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    //const int timeout = 1500;
        //    //if ((DateTime.Now - _lastBarCodeCharReadTime).Milliseconds < timeout)
        //    //    return;

        //    //_timer.Stop();
        //}

        private void PointOfSale_Load(object sender, EventArgs e)
        {
            updateOR(); //generate OR Number
            updateTransactionNo(); //generate Transaction Number

            refreshView();
           
            txtsku.Focus();
            //Database.displayComboBoxItems("SELECT distinct CustomerName FROM Customers", "CustomerName", txtcustname);

            //if(POS.POSConnectionSettings.spValue == "sp_AddSalesInvoiceOnline")
            //{
            //    constring = "Online";
            //}else
            //{
            //    constring = "Offline";
            //}
            //lblstatus.Text = constring;
            //loadDefaultClient();
            //lblTransactionIDInc.Text = Database.getSingleQuery("POSTransaction", "TransactionNo <> ''", "TransactionNo");
            lblTransactionIDCashier.Text = Database.getSingleQuery("SalesTransactionSummary", "UserID='" + Login.isglobalUserID + "' AND BranchCode='" + Login.assignedBranch + "' AND isOpen='1' and DateOpen='"+DateTime.Now.ToShortDateString()+"' ", "CashierTransNo");
            lblMachineName.Text = Classes.Utilities.getComputerName();
            labelControl7.Text = Login.Fullname;

            string displaypoolport = Database.getSingleQuery("POSType", "DisplayPoolPort <> 'eulz'", "DisplayPoolPort");
            string isuseddisplaypool = Database.getSingleQuery("POSType", "DisplayPoolPort <> 'eulz'", "isUsedDisplayPool");
            if (!String.IsNullOrEmpty(displaypoolport))
            {
                txtcomport.Text = "";
                txtcomport.Text = displaypoolport;
            }
            else
            {
                btndisplaypoolset.Enabled = true;
            }
            if(Convert.ToBoolean(isuseddisplaypool)==true)
            {
                chckdisplaypool.Checked = true;
            }
            else
            {
                chckdisplaypool.Checked = false;
            }

            checkCOMPort();
            timer1.Start();
        }
        void checkCOMPort()
        {
            try
            {
                if (serialPort1.IsOpen == false)
                {
                    serialPort1.PortName = txtcomport.Text.Trim();
                    serialPort1.Open();
                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                    serialPort1.ReadTimeout = 500;
                    serialPort1.WriteTimeout = 500;
                }

            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtcomport.Items.AddRange(ports);
        }
        private void txtcomport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = txtcomport.Text.Trim();
                serialPort1.Open();
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;
            }
           catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            //serialPort1.RtsEnable = true;
            //serialPort1.DtrEnable = true;
        }


        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = serialPort1.ReadLine();
            //data = "";
            //data = serialPort1.ReadLine();
            //data = serialPort1.ReadExisting();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }
        private void si_DataReceived(string data)
        {
            txtcustid.Text = data.Trim();
        }
        void loadDefaultClient()
        {
            txtcustname.Text = "WALKIN";
            txtcustid.Text = Database.getSingleQuery("Customers", "CustomerName='" + txtcustname.Text.Trim() + "'", "CustomerID");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F5) //PAYMENT
            {
                btnPayment.PerformClick();
            }
            else if (keyData == Keys.Enter) //ADD ORDER
            {
                addOrder();
            }
            else if (keyData == Keys.Down) //FOCUS TO GRID VIEW
            {
                MydataGridView1.Focus();
            }
            else if (keyData == Keys.F && keyData == Keys.Alt) //FOCUS TO SKU TEXTFIELD
            {
                //textEdit3.Focus();
                txtsku.Focus();
            }
            else if (keyData == (Keys.D | Keys.Control)) //FOCUS TO SKU TEXTFIELD (keyData == (Keys.O | Keys.Control))
            {
                //textEdit3.Focus();
                POS.POSScreenMirror posmir = new POS.POSScreenMirror();
                posmir.Show();
            }
            else if (keyData == (Keys.S | Keys.Control | Keys.Alt)) //FOCUS TO SKU TEXTFIELD (keyData == (Keys.O | Keys.Control))
            {
                //textEdit3.Focus();
                POS.POSSyncProducts asdj = new POS.POSSyncProducts();
                asdj.ShowDialog(this);
            }
            else if (keyData == (Keys.H | Keys.Control)) //FOCUS TO SKU TEXTFIELD
            {
                //textEdit3.Focus();
                POS.POSScreenMirror posmir = new POS.POSScreenMirror();
                posmir.Hide();
            }
            else if (keyData == Keys.F1)//SEARCH BUTTON
            {
                btnsearch.PerformClick();
            }
            else if (keyData == Keys.F2) //EDIT LINE (TEMPORARYLY DISABLED)
            {
                btnEditLine.PerformClick();
            }
            else if (keyData == Keys.F6) //HOLD BUTTON (NOT USED) ADD DISCOUNT
            {
                simpleButton3.PerformClick(); //simpleButton7.PerformClick();
            }
            else if (keyData == Keys.F7) //TRANSACTION VOID BUTTON
            {
                btnVoid.PerformClick();
            }
            else if (keyData == Keys.F8) //VOID BUTTON
            {
                btnErrorCorrect.PerformClick();
            }
            else if (keyData == Keys.Delete) //CANCEL LINE
            {
                btnCancelLine.PerformClick();
            }
            else if (keyData == Keys.F10)//SEARCH BUTTON
            {
                btnreprint.PerformClick();
            }
            //else if (e.KeyCode == Keys.F1)
            //{
            //    //toolStripButton1.PerformClick();
            //    Onhold.PerformClick();
            //}
            else if (keyData == Keys.F3) //RECOVERED BUTTON (NOT USED)
            {
                //toolStripButton3.PerformClick();
                //recovered.PerformClick();
                //btnChargeAccount.PerformClick();
                btnhold.PerformClick();
            }
            else if (keyData == Keys.F4) //HISTORY BUTTON
            {
                //toolStripButton4.PerformClick();
                //btnHistory.PerformClick();
                Onhold.PerformClick();
            }
            else if (keyData == Keys.F12) //CLOSED TRANSACTION BUTTON
            {
                btnCloseAccount.PerformClick();
            }
            //else if (keyData == Keys.X) //XREAD REPORT
            //{
            //    POS.POSXreadReport xread = new POS.POSXreadReport();
            //    xread.Show();
            //}
            //else if (keyData == (Keys.Z | Keys.Control)) //REFUND REPORT
            //{
            //    POS.POSRefund xreadss = new POS.POSRefund();
            //    xreadss.Show();
            //}
            else if (keyData == (Keys.O | Keys.Control)) //(keyData == (Keys.O | Keys.Control)) //REFUND REPORT
            {
                Printing printit = new Printing();
                bool isoverride = false;
                isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='OPENDRAWER' AND isOverride=1");
                if (!isoverride)
                {
                    printit.printReceiptAtik();
                }
                else
                {
                    AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                    authfrm.ShowDialog(this);
                    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                    {
                        printit.printReceiptAtik();
                        AuthorizedConfirmationFrm.isconfirmedLogin = false;
                        authfrm.Dispose();
                    }
                }

            }
            //else if (keyData == (Keys.Delete | Keys.Control)) //(keyData == (Keys.O | Keys.Control)) //REFUND REPORT
            //{
            //    //deleteTransactions();
            //}
            else if (keyData == Keys.Escape) //(keyData == (Keys.O | Keys.Control)) //REFUND REPORT
            {
                btnExit.PerformClick();
            }
            //else if (keyData == (Keys.X | Keys.Control)) //(keyData == (Keys.O | Keys.Control)) //REFUND REPORT
            //{
            //    Reading();
            //}
            else if (keyData == (Keys.S | Keys.Control)) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                POSStandAloneSetup.POSTypeSettings posnnd = new POSStandAloneSetup.POSTypeSettings();
                posnnd.ShowDialog(this);
            }
            return functionReturnValue;
        }
        //public void OpenDrawer()
        //{
        //    //string DrawerCode = Strings.Chr(27) + Strings.Chr(112) + Strings.Chr(48) + Strings.Chr(64) + Strings.Chr(64);
        //    String command = "/C print /d:LPT1: " + (Char)27 + (Char)112 + (Char)48 + (Char)64 + (Char)64;
        //    //String command= "/C print /d:LPT1: " + (Char)27 + (Char)112 + (Char)0 + (Char)25 + (Char)250;
        //    ProcessStartInfo application = new ProcessStartInfo("cmd.exe", command);
        //    application.WindowStyle = ProcessWindowStyle.Hidden;
        //    Process process = Process.Start(application);
        //    process.WaitForExit();
        //    process.Close();
        //}

        void atghdfrm_FormClosed(object sender, FormClosedEventArgs e) //CONFIRMATION FORM WTH CLOSING EVENT
        {
            voidTransaction();
        }



        public class TerminalVerifier
        {


            public async Task<bool> VerifyTerminalAsync(string terminalId)
            {

                try
                {
                    using (var client = new HttpClient())
                    {
                        if (string.IsNullOrWhiteSpace(terminalId))
                        {
                            MessageBox.Show("Terminal ID cannot be empty.");
                            return false;
                        }

                        var url = $"http://itcore-apps.com:8181/api/terminals/{Uri.EscapeDataString(terminalId)}";

                        var response = await client.GetAsync(url);

                        if (!response.IsSuccessStatusCode)
                        {
                            MessageBox.Show($"Failed to verify terminal. Status: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                            return false;
                        }

                        var responseBody = await response.Content.ReadAsStringAsync();
                        var apiResult = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

                        if (apiResult == null || apiResult.Data == null)
                        {
                            MessageBox.Show("Invalid response from server.");
                            return false;
                        }

                        // ✅ Terminal exists if Success = true and Data.TerminalId matches
                        return apiResult.Success && apiResult.Data.TerminalId.Equals(terminalId, StringComparison.OrdinalIgnoreCase);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    return false;
                }

            }


        }


        public class ApiResponse
        {
            public bool Success { get; set; }
            public TerminalData Data { get; set; }
        }

        public class TerminalData
        {
            public int Id { get; set; }
            public int MerchantId { get; set; }
            public string TerminalId { get; set; }
            public string TerminalName { get; set; }
            public string DateAdded { get; set; }
            public string TimeAdded { get; set; }
            public string DateTimeAdded { get; set; }
            public string DateUpdated { get; set; }
            public string TimeUpdated { get; set; }
            public string DateTimeUpdated { get; set; }
            public int Status { get; set; }
            public string UatKeys { get; set; }
            public string ProdKeys { get; set; }
        }



        async Task<bool> verifyTerminalAsync()
        {

            string terminalId = HelperFunction.GetMacAddress2();
            var verifier = new TerminalVerifier();

            bool exists = await verifier.VerifyTerminalAsync(terminalId);
            return exists; // ✅ Return the bool value
        }

        async void addOrder()
        {
            //this.Cursor = Cursors.WaitCursor;
            try
            {
                //bool exists = await verifyTerminalAsync();
                //ispriceused = "mainprice";

                if (String.IsNullOrEmpty(txtsku.Text))
                {
                    XtraMessageBox.Show("SKU must not Empty");
                    txtsku.Focus();
                    return;
                }
                //else if(!exists)
                //{
                //    XtraMessageBox.Show("Mac Address Not Exists!!..");
                //    return;
                //}
                else
                {
                    insertData();
                    isusedsearchform = false;
                    ispriceused = "mainprice";
                    display();
                    txtsku.Text = "";
                    if (isDataInserted)
                        MydataGridView1.Rows[0].Selected = true;
                }
                //if(isSpecialPrice.Checked==true)
                //{
                //    POS.POSSpecialPrice possprice = new POS.POSSpecialPrice();
                //    //possprice.FormClosed += new FormClosedEventHandler(possprice_FormClosed);
                //    possprice.ShowDialog(this);
                //    if (POS.POSSpecialPrice.isconfirmed == true)
                //    {
                //        insertData();
                        
                //        isusedsearchform = false;
                //        display();
                //        txtsku.Text = "";
                //        POS.POSSpecialPrice.isconfirmed = false;
                //        possprice.Dispose();
                //        if (isDataInserted)
                //            MydataGridView1.Rows[0].Selected = true;
                //        //MydataGridView1.CurrentCell = MydataGridView1.Rows[MydataGridView1.Rows.Count - 1].Cells[1];
                //    }
                //}
                //else
                //{
                   
                //    insertData();
                //    isusedsearchform = false;
                //    ispriceused = "mainprice";
                //    display();
                //    txtsku.Text = "";
                //    if (isDataInserted)
                //        MydataGridView1.Rows[0].Selected = true;
                //}
                
                bool isLinkedServer = Database.checkifExist("Select isnull(isLinkedServer,0) FROM POSType WHERE isLinkedServer=1");

                if (isLinkedServer) //if they used linkedserver
                {
                    string linkedServerName = Database.getSingleQuery("POSType", "isLinkedServer is not null", "linkedServerName"); //linkedservername
                    string conLink = Database.getSingleResultSet("exec checkLinkedServer  " + linkedServerName + "  "); //check connection
                    if (conLink == "1")
                    {
                        labelControl8.ForeColor = Color.Green;
                    }
                    else
                    {
                        labelControl8.ForeColor = Color.Red;
                    }
                }
                else
                {
                    labelControl8.ForeColor = Color.Red;
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            //this.Cursor = Cursors.Default;
        }

        //void possprice_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    //throw new NotImplementedException();
        //    if (POS.POSSpecialPrice.stat == "YES")
        //    {
        //        insertData();
        //        display();
        //        txtsku.Text = "";
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        void displayHoldTransactions(string refno,string machinename)
        {
            //Database.display("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(UnitPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(SubTotal,'N', 'en-us') AS Amount FROM BatchSalesDetails WHERE ReferenceNo='" + textEdit3.Text + "' AND isVoid='0' AND isCancelled='0' and isHold='0'", gridControl2, gridView2);
            //Database.displayLocalGrid("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(Sellin gPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(SubTotal,'N', 'en-us') AS Amount FROM BatchSalesDetails WHERE ReferenceNo='" + textEdit3.Text + "' AND isVoid='0' AND isCancelled='0' and isHold='0' AND BranchCode='"+Login.assignedBranch+"'", MydataGridView1);
            Database.displayLocalGrid("SELECT SequenceNumber AS ID" +
                ",Description AS Particulars" +
                ",FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice" +
                ",QtySold AS Qty" +
                ",FORMAT(DiscountTotal,'N', 'en-us') AS Discount" +
                ",FORMAT(TotalAmount,'N', 'en-us') AS Amount" +
                ",isVat FROM BatchSalesDetails " +
                "WHERE ReferenceNo='" + refno + "' " +
                "AND isVoid='0' " +
                "AND isCancelled='0' " +
                "and MachineUsed='"+machinename+"' " +
                "AND BranchCode='" + Login.assignedBranch + "' " +
                "ORDER BY SequenceNumber DESC", MydataGridView1);

            lblTotalItems.Text = MydataGridView1.RowCount.ToString();
            lblTotalAmount.Text = HelperFunction.numericFormat(computeTotalAmount());//.ToString();
            lblvat.Text = HelperFunction.numericFormat(computeVAT());//.ToString();
            lblvatexemptsale.Text = HelperFunction.numericFormat(computeVATExemptSale());//.ToString();
            lblvatsale.Text = HelperFunction.numericFormat(computeVATableSale());//.ToString();
            double totaldiscount = 0.0;
            totaldiscount = GetTotalOneTimeDiscount();//computeDiscount()+GetTotalOneTimeDiscount();
            lblTotalDiscount.Text = HelperFunction.numericFormat(totaldiscount);//.ToString();

            lblperitemdiscountamount.Text = HelperFunction.numericFormat(computeDiscount());//.ToString();

            totalamountstr = lblTotalAmount.Text;
        }

        void display()
        {
            //Database.display("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(UnitPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(SubTotal,'N', 'en-us') AS Amount FROM BatchSalesDetails WHERE ReferenceNo='" + textEdit3.Text + "' AND isVoid='0' AND isCancelled='0' and isHold='0'", gridControl2, gridView2);
            //Database.displayLocalGrid("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(SubTotal,'N', 'en-us') AS Amount FROM BatchSalesDetails WHERE ReferenceNo='" + textEdit3.Text + "' AND isVoid='0' AND isCancelled='0' and isHold='0' AND BranchCode='"+Login.assignedBranch+"'", MydataGridView1);
            Database.displayLocalGrid("SELECT SequenceNumber AS ID" +
                ",Description AS Particulars" +
                ",FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice" +
                ",QtySold AS Qty" +
                ",FORMAT(DiscountTotal,'N', 'en-us') AS Discount" +
                ",FORMAT(TotalAmount,'N', 'en-us') AS Amount" +
                ",isVat FROM BatchSalesDetails " +
                "WHERE ReferenceNo='" + txtOrderNo.Text + "' " +
                "AND isVoid='0' " +
                "AND isCancelled='0' " +
                "and isHold='0' " +
                "AND BranchCode='" + Login.assignedBranch + "' " +
                "ORDER BY SequenceNumber DESC", MydataGridView1);

            lblTotalItems.Text = MydataGridView1.RowCount.ToString();
            lblTotalAmount.Text = HelperFunction.numericFormat(computeTotalAmount());//.ToString();
            lblvat.Text = HelperFunction.numericFormat(computeVAT());//.ToString();
            lblvatexemptsale.Text = HelperFunction.numericFormat(computeVATExemptSale());//.ToString();
            lblvatsale.Text = HelperFunction.numericFormat(computeVATableSale());//.ToString();
            double totaldiscount = 0.0;
            totaldiscount = GetTotalOneTimeDiscount();//computeDiscount()+GetTotalOneTimeDiscount();
            lblTotalDiscount.Text = HelperFunction.numericFormat(totaldiscount);//.ToString();

            lblperitemdiscountamount.Text = HelperFunction.numericFormat(computeDiscount());//.ToString();

            totalamountstr = lblTotalAmount.Text;
        }

        Double computeTotalAmountWithDiscount()
        {
            double total = 0.0;
            for(int i=0;i<=MydataGridView1.RowCount-1;i++)
            {
                total += Convert.ToDouble(MydataGridView1.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(MydataGridView1.Rows[i].Cells["Discount"].Value);
            }
            return Math.Round(total,2);
        }

        Double GetTotalOneTimeDiscount()
        {
            double discount = 0.0;
            discount=computeDiscount() + Convert.ToDouble(lblseniordiscount.Text) + Convert.ToDouble(lblpwddiscount.Text) + Convert.ToDouble(lblotherdiscount.Text);
            return Math.Round(discount,2);
        }

        Double computeVATableSale()
        {
            double vatexemptsale = 0.0,finalvalue=0.0;
            for (int i = 0; i <= MydataGridView1.RowCount-1; i++)
            {
                if (MydataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "True")
                {
                    vatexemptsale += Convert.ToDouble(MydataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            finalvalue = vatexemptsale / 1.12;
            return Math.Round(finalvalue,2);
        }

        Double computeVATExemptSale()
        {
            double vatexemptsale = 0.0;
            for (int i = 0; i <= MydataGridView1.RowCount - 1; i++)
            {
                if (MydataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "False")
                {
                    vatexemptsale += Convert.ToDouble(MydataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            return Math.Round(vatexemptsale,2);
        }

        Double computeVAT()
        {
            double vatexemptsale = 0.0;
            double vat = 0.0;
            for (int i = 0; i <= MydataGridView1.RowCount - 1; i++)
            {
                if (MydataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "True")
                {
                    vatexemptsale += Convert.ToDouble(MydataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            vat = Math.Round((vatexemptsale / 1.12) * 0.12,2);
            return vat;
        }

        Double computeDiscount()
        {
            double discount = 0.0;
            for (int i = 0; i <= MydataGridView1.RowCount - 1; i++)
            {
                totaldiscount = MydataGridView1.Rows[i].Cells["Discount"].Value.ToString();
                discount += Convert.ToDouble(totaldiscount);
            }
            return Math.Round(discount,2);
        }

        Double computeTotalAmount()
        {
            double total = 0.0;
            for (int i = 0; i <= MydataGridView1.RowCount-1; i++)
            {
                amount = MydataGridView1.Rows[i].Cells["Amount"].Value.ToString();
                total += Convert.ToDouble(amount);
            }
            return total;
        }

        private void insertData()   //ADD ITEM
        {
            string qty = "0";
            qty = SearchProduct.qty;
            string productcode = "";
            //if(Convert.ToDouble(qty) > 999)
            //{
            //    XtraMessageBox.Show("Quantity must not Greater than 999");
            //    return;
            //}
            
            if (isusedsearchform == true)//kung naay barcode pag select sa form
            {
                productcode = prodcode;
            }
            //else
            //{
            //    productcode = prodcode;
            //}

            bool islinkedServer = Database.checkifExist("SELECT isnull(isLinkedServer,0) FROM POSType WHERE isLinkedServer=1");
            string linkedServerName = Database.getSingleQuery("POSType", "isLinkedServer is not null", "linkedServerName"); //linkedservername
           
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                //string query = POS.POSConnectionSettings.spValue;
                // string query = "sp_AddSalesInvoiceOffline";
                string query = "";
                if(islinkedServer)
                {
                    string conLink = Database.getSingleResultSet("exec checkLinkedServer " + linkedServerName + " "); //check connection
                    if(conLink == "1")
                    {
                        query = "sp_AddSalesInvoiceLinkToServer";
                    }
                    else
                    {
                        query = "sp_AddSalesInvoiceLinkToServer";
                    }
                }
                else
                {
                    query = "sp_AddSalesInvoice";
                }
               
                SqlCommand com = new SqlCommand(query, con);
                //refno = textEdit3.Text.Trim();
                refno = txtOrderNo.Text.Trim();
                com.Parameters.AddWithValue("@parmorderno", refno); //ORDER NO
                com.Parameters.AddWithValue("@parmcustid", txtcustid.Text); 
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmtranscode", lblTransactionIDCashier.Text);//CASHIER TRANS NO
                com.Parameters.AddWithValue("@parmbarcode", txtsku.Text);
                com.Parameters.AddWithValue("@parmprodcode", productcode);
                com.Parameters.AddWithValue("@parmqty", qty);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmdiscountamount", POS.POSSpecialPrice.spprice);
                com.Parameters.AddWithValue("@parmissellingprice", "0");
                com.Parameters.AddWithValue("@parmisusedform", isusedsearchform);
                com.Parameters.AddWithValue("@parmvatsale", lblvatsale.Text);
                com.Parameters.AddWithValue("@parmvatexemptsale", lblvatexemptsale.Text);
                com.Parameters.AddWithValue("@parmvat", lblvat.Text);
                com.Parameters.AddWithValue("@parmispriceused", ispriceused);

                com.Parameters.AddWithValue("@parmtableno", "");
                com.Parameters.AddWithValue("@parmoption", "");
                com.Parameters.AddWithValue("@parmroomnum", "");
                com.Parameters.AddWithValue("@parmbookingno", "");

                com.Parameters.Add("@parmdesc1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmqty2", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmsel1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotal1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
                com.ExecuteNonQuery();
                isDataInserted = true;

                string desc1 = com.Parameters["@parmdesc1"].Value.ToString();
                string qty1 = com.Parameters["@parmqty2"].Value.ToString();
                string sel1 = com.Parameters["@parmsel1"].Value.ToString();
                string total1 = com.Parameters["@parmtotal1"].Value.ToString();
                string currentPrice = String.Format("\u20B1{0}",total1);
                string full = qty1 + " @ " + sel1 + " = " + total1;
                int desclength = desc1.Length;
                string description = "";
                if (desclength > 20)
                {
                    description = desc1.Substring(0, 19);
                }
                else
                {
                    description = desc1;
                }
                if (txtcomport.Text == "")
                {
                    XtraMessageBox.Show("Please Select COM-PORT");
                }
                else if(serialPort1.IsOpen && chckdisplaypool.Checked==true)
                {
                    serialPort1.Write(Convert.ToString((char)12));
                    serialPort1.WriteLine(description);
                    serialPort1.WriteLine((char)13 + "Amount: " + total1);
                }
             
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            //isusedbarcode = false;
           con.Close();
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateKron = DateTime.Now;
            DateTime mytime = DateTime.Now;
            labelControl8.Text = dateKron.ToString();
        }

        //String sequencePadding(string str)
        //{
        //    string isnum = "";//A000001
        //    if (str.Length == 1) //
        //    {
        //        isnum = "A00000" + str;
        //    }
        //    else if (str.Length == 2)
        //    {
        //        isnum = "A0000" + str;
        //    }
        //    else if (str.Length == 3)
        //    {
        //        isnum = "A000" + str;
        //    }
        //    else if (str.Length == 4)
        //    {
        //        isnum = "A00" + str;
        //    }
        //    else if (str.Length == 5) //A012345
        //    {
        //        isnum = "A0" + str;
        //    }
        //    return isnum;
        //}

        //string getORNumberNew()
        //{
        //    string num = Classes.Utilities.readTextfile("C:\\POSTransaction\\ORSeries\\"); //000001
        //    string splitnum = num.Substring(1, 6);//A [000001]

        //    int ornumnew = Convert.ToInt32(splitnum) + 1; //sample if start to 000001 if convert to int, it become 1 only single digit so 1 + 1 = 2
        //    string newornum = sequencePadding(num.ToString()); //000001
        //    //string ORNum = PrefixLetter + newornum
        //    return ornumnew.ToString();
        //}
        string getORNumber()
        {
            string num= Classes.Utilities.readTextfile("C:\\POSTransaction\\ORSeries\\");
            int ornumnew = Convert.ToInt32(num) + 1;
            return ornumnew.ToString();
        }
        
       void updateTransNo()
        {
            //string transno = IDGenerator.getPOSTransactionNoSP();
            string transno = IDGenerator.getIDNumberSP("sp_GetPOSTransactionNo", "TransactionNo");
            lblTransactionIDInc.Text = transno;
        }

        void updateOR()
        {
            int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
            txtOrderNo.Text = HelperFunction.sequencePadding1(refnumber.ToString(),18);//refnumber.ToString();
        }
        void updateTransactionNo()
        {
            //int refnumber = IDGenerator.getPOSTransactionID();
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "'", "TransactionNo", 1);
            lblTransactionIDInc.Text = HelperFunction.sequencePadding1(refnumber.ToString(),10);//refnumber.ToString();
        }
       

        void refreshView()
        {
            
            txtsku.Text = "";
            txtsku.Focus();
            chckZeroRated.Checked = false;

            lblTotalDiscount.Text = "0";
            lblseniordiscount.Text = "0";
            lblpwddiscount.Text = "0";
            lblotherdiscount.Text = "0";
            lblonetimediscountamount.Text = "0";
            lblperitemdiscountamount.Text = "0";

            lblTotalAmount.Text = "0";
            lblTotalItems.Text = "0";
            lblvatexemptsale.Text = "0";
            lblvatsale.Text = "0";
            lblvat.Text = "0";

            txtcustbussstyle.Text = "";
            txtcustaddressrcpt.Text = "";
            txtcustnamercpt.Text = "";
            txtcusttinrcpt.Text = "";

            table = new DataTable();
            table.Columns.Add("ID");
            table.Columns.Add("Particulars");
            table.Columns.Add("UnitPrice");
            table.Columns.Add("Qty");
            table.Columns.Add("Discount");
            table.Columns.Add("Amount");
            table.Columns.Add("isVat");
            MydataGridView1.DataSource = table; //local gridview
            MydataGridView1.Columns["ID"].Visible = false; //localgridview
            MydataGridView1.Columns["isVat"].Visible = false;
        }

        double getSalesDiscount()
        {
            double discount = 0.0;
            discount = Database.getTotalSummation2("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0", "DiscountAmount");
            return Math.Round(discount,2);
        }
        double getVatAdjustment()
        {
            double discount = 0.0;
            discount = Database.getTotalSummation2("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0", "VatAdjustment");
            return Math.Round(discount, 2);
        }
        double getPerItemDiscount()
        {
            double discount = 0.0;
            discount = Database.getTotalSummation2("BatchSalesDetails", "ReferenceNo='" + txtOrderNo.Text + "'" +
                " and isErrorCorrect=0" +
                "and isCancelled=0" +
                "and isVoid=0", "DiscountTotal");
            return Math.Round(discount,2);
        }

        bool haveOneTimeDiscount()
        {
            bool ok = false;
            ok = Database.checkifExist("SELECT TOP 1 OrderNo FROM SalesDiscount WHERE OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0");
            return ok;
        }

        void AddDiscounts()
        {
            string postype = Database.getSingleQuery("SELECT TOP(1) PosType FROM dbo.POSType", "PosType");
            totamount = "0";
            refno = txtOrderNo.Text;
            totamount = lblTotalAmount.Text;
            double discountAmount = 0.0, cleanamount = 0.0, newtotalamount = 0.0;
            double totalpayment = 0.0;

            totalpayment = Convert.ToDouble(lblvatsale.Text) + Convert.ToDouble(lblvatexemptsale.Text); //NOT USED
            // discountAmount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblvat)* (0.05);
           
            //NOT USED
            discountAmount = (Convert.ToDouble(lblvatsale.Text) + Convert.ToDouble(lblvatexemptsale.Text)) * (0.05);// Convert.ToDouble(lblvat) * (0.05);
            cleanamount = Convert.ToDouble(lblTotalAmount.Text) - discountAmount;
            //////////////////////////////////////////////////////////////////////////////////
            
            int countitems = Database.getCountData("BatchSalesDetails", "ReferenceNo='" + txtOrderNo.Text + "' " +
                "AND CashierTransNo='" + lblTransactionIDCashier.Text + "'" +
                "AND isCancelled=0 AND isVoid=0 and isErrorCorrect=0 ", "SequenceNumber");

            //AddDiscount adis = new AddDiscount();
            POSAddDiscount adis = new POSAddDiscount();
            AddDiscountRestaurant adisres = new AddDiscountRestaurant();
            


            if (countitems < 1)
            {
                XtraMessageBox.Show("You Cant Add Discount no Items to be discounted");
                return;
            }
            else
            {
                if (postype == "1")
                {

                    adis.txtorderno.Text = txtOrderNo.Text;
                    adis.txtcashiertansno.Text = lblTransactionIDCashier.Text;
                    adis.txttransactionno.Text = lblTransactionIDInc.Text;
                    adis.ShowDialog(this);
                }
                else if (postype == "2")
                {

                    adisres.txtorderno.Text = txtOrderNo.Text;
                    adisres.txtcashiertansno.Text = lblTransactionIDCashier.Text;
                    adisres.txttransactionno.Text = lblTransactionIDInc.Text;
                    adisres.ShowDialog(this);
                }
                //adis.ShowDialog(this);

                //string getDiscountedItems = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItems('" + Login.assignedBranch + "','" + txtOrderNo.Text + "')");
                ////DEFAULT 5% DISCOUNT
                //string getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + txtOrderNo.Text + "',0.05)");
                //string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + txtOrderNo.Text + "')");

                ////DEFAULT CHECKED ITEMS IS SENIOR, BUT ALL ITEMS IS AUTO COMPUTED
                //if(adis.radioButton1.Checked == true || adis.radioButton1.Checked == true) //SENIOR OR PWD
                //{
                //    if (String.IsNullOrEmpty(getDiscountedItems))
                //    {
                //        getDiscountedItems = "0";
                //    }
                //    ////SENIOR
                //    //adis.txtamnttobediscount.Text = getDiscountedItems;  
                //    //adis.txtpercentageamount.Text = "5";  //DEFAULT 5% DISCOUNT
                //    //adis.txtvatadj.Text = getVatAdjustment;
                //    //adis.txtvatexadj.Text = getVATExAdj;
                //    ////PWD
                //    //adis.txtpwdamount.Text = getDiscountedItems; 
                //    //adis.txtpwdpercent.Text = "5";  //DEFAULT 5% DISCOUNT
                //    //adis.txtvatadj.Text = getVatAdjustment;
                //    //adis.txtvatexadj.Text = getVATExAdj;

                //    //adis.txtothersamount.Text = lblTotalAmount.Text;
                //    //adis.txtotherspercent.Text = "5";

                //    //adis.ShowDialog(this);
                //}
            }
            if (POSAddDiscount.isdone == true)
            {
                double totdiscount = 0.0;
                //totdiscount = Math.Round(Convert.ToDouble(lblTotalDiscount.Text) + getSalesDiscount(), 2);
                totdiscount = Math.Round(getPerItemDiscount() + getSalesDiscount(), 2);
                lblTotalDiscount.Text = totdiscount.ToString();//Convert.ToDouble(lblTotalDiscount.Text)+AddDiscount.discountamount;
                newtotalamount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblTotalDiscount.Text);

                //if (POSAddDiscount.discounttype == "SENIOR")
                //{
                //    lblseniordiscount.Text = getSalesDiscount().ToString();
                //}
                //else if (POSAddDiscount.discounttype == "PWD")
                //{
                //    lblpwddiscount.Text = getSalesDiscount().ToString();
                //}
                //else if (POSAddDiscount.discounttype == "PWD")
                //{
                //    lblpwddiscount.Text = getSalesDiscount().ToString();
                //}
                //else if (POSAddDiscount.discounttype == "PWD")
                //{
                //    lblpwddiscount.Text = getSalesDiscount().ToString();
                //}


                updateTransactionNo();
                adis.Dispose();
            }
            //if (AddDiscount.isdone == true)
            //{
            //    double totdiscount = 0.0;
            //    //totdiscount = Math.Round(Convert.ToDouble(lblTotalDiscount.Text) + getSalesDiscount(), 2);
            //    totdiscount = Math.Round(getPerItemDiscount() + getSalesDiscount(), 2);
            //    lblTotalDiscount.Text = totdiscount.ToString();//Convert.ToDouble(lblTotalDiscount.Text)+AddDiscount.discountamount;
            //    newtotalamount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblTotalDiscount.Text);
            //    if(AddDiscount.discounttype=="SENIOR")
            //    {
            //        lblseniordiscount.Text = getSalesDiscount().ToString();
            //    }
            //    else if (AddDiscount.discounttype == "PWD")
            //    {
            //        lblpwddiscount.Text = getSalesDiscount().ToString();
            //    }

            //    updateTransactionNo();
            //    adis.Dispose();
            //}
            if (AddDiscountRestaurant.isdone == true)
            {
                double totdiscount = 0.0;
                //totdiscount = Math.Round(Convert.ToDouble(lblTotalDiscount.Text) + getSalesDiscount(), 2);
                totdiscount = Math.Round(getPerItemDiscount() + getSalesDiscount(), 2);
                lblTotalDiscount.Text = totdiscount.ToString();//Convert.ToDouble(lblTotalDiscount.Text)+AddDiscount.discountamount;
                newtotalamount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblTotalDiscount.Text);
                if (AddDiscountRestaurant.discounttype == "SENIOR")
                {
                    lblseniordiscount.Text = getSalesDiscount().ToString();
                }
                else if (AddDiscountRestaurant.discounttype == "PWD")
                {
                    lblpwddiscount.Text = getSalesDiscount().ToString();
                }
                updateTransactionNo();
                adisres.Dispose();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e) //F6
        {
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM dbo.POSFunctions WHERE FunctionName='DISCOUNT' AND isOverride=1");
            if (!isoverride)
            {
                bool checkifexists = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + refno + "' AND isErrorCorrect=0 ");
                if (checkifexists)
                {
                    bool isExists = HelperFunction.ConfirmDialog("The System found out that you already Discount this Transaction.. Do you want to Override and make a new Discount of this Transaction?", "Confirm Transaction");
                    if (isExists) //IF YES
                    {
                        Database.ExecuteQuery("Update dbo.SalesDiscount SET isErrorCorrect=1 WHERE OrderNo='" + refno + "' AND isErrorCorrect=0");
                        AddDiscounts();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    AddDiscounts();
                }
            }
            else
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    bool checkifexists = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + refno + "' AND isErrorCorrect=0 ");
                    if (checkifexists)
                    {
                        bool isExists = HelperFunction.ConfirmDialog("The System found out that you already Discount this Transaction.. Do you want to Override and make a new Discount of this Transaction?", "Confirm Transaction");
                        if (isExists) //IF YES
                        {
                            Database.ExecuteQuery("Update dbo.SalesDiscount SET isErrorCorrect=1 WHERE OrderNo='" + refno + "' AND isErrorCorrect=0");
                            AddDiscounts();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        AddDiscounts();
                    }
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
                
        }

        void adfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //txtdiscount.Text = AddDiscount.discount;
        }

        private void txtdiscount_EditValueChanged(object sender, EventArgs e)
        {
            double subtotal = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(txtdiscount.Text);
            lblTotalAmount.Text = subtotal.ToString();
        }

    
        private void cancelTransaction()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_CancelPOSTransaction";
                SqlCommand com = new SqlCommand(query, con);
                //com.Parameters.AddWithValue("@parmorderno", textEdit3.Text);
                com.Parameters.AddWithValue("@parmorderno", txtOrderNo.Text);
                com.Parameters.AddWithValue("@parmtransno", lblTransactionIDInc.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmseqnum", MydataGridView1[0, MydataGridView1.CurrentRow.Index].Value.ToString());
                com.Parameters.AddWithValue("@parmqty", MydataGridView1[3, MydataGridView1.CurrentRow.Index].Value.ToString());
                com.Parameters.AddWithValue("@parmamount", MydataGridView1[4, MydataGridView1.CurrentRow.Index].Value.ToString());
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void deleteTransaction()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_DeletePOSTransaction";
                SqlCommand com = new SqlCommand(query, con);
                //com.Parameters.AddWithValue("@parmorderno", textEdit3.Text);
                com.Parameters.AddWithValue("@parmorderno", txtOrderNo.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmseqnum", MydataGridView1[0, MydataGridView1.CurrentRow.Index].Value.ToString());
                com.Parameters.AddWithValue("@parmqty", MydataGridView1[3, MydataGridView1.CurrentRow.Index].Value.ToString());
                com.Parameters.AddWithValue("@parmamount", MydataGridView1[4, MydataGridView1.CurrentRow.Index].Value.ToString());
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

     

        //void possstran_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    updateOR();
        //    refreshView();
        //    updateTransNo();
        //}



        void voidTransaction()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_VoidPOSTransaction";
                SqlCommand com = new SqlCommand(query, con);
                //com.Parameters.AddWithValue("@parmorderno", textEdit3.Text);
                com.Parameters.AddWithValue("@parmorderno", txtOrderNo.Text);
                com.Parameters.AddWithValue("@parmtransno", lblTransactionIDInc.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmuser", AuthorizedConfirmationFrm.isglobalUserID);
                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            //Database.ExecuteQuery("UPDATE BatchSubSalesDetails SET isVoid='1',VoidBy='" + Login.isglobalUserID + "',Status='Void' WHERE ReferenceNo='" + textEdit3.Text + "'");
            //Database.ExecuteQuery("UPDATE BatchSalesDetails SET isVoid='1',VoidBy='"+Login.isglobalUserID+"',Status='Void' WHERE ReferenceNo='" + textEdit3.Text + "'");
            //Database.ExecuteQuery("UPDATE BatchSalesSummary SET isVoid='1',VoidBy='" + Login.isglobalUserID + "',Status='Void' WHERE ReferenceNo='" + textEdit3.Text + "'", "Successfully Void");
           
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
                btnCancelLine.PerformClick();
        }

      
        void OnHoldTransaction()
        {
            try
            {
                //check if there is hold transactions
                bool isPendingTranSummary = Database.checkifExist("SELECT TOP(1) Status FROM BatchSalesSummary WHERE (Status='Pending' OR isHold='1') " +
               "AND BranchCode='" + Login.assignedBranch + "' " +
               "AND MachineUsed='" + Environment.MachineName + "' " +
               //"AND isHold='1' " +
               "AND CashierTransNo='" + lblTransactionIDCashier.Text + "' ");
              
                if (isPendingTranSummary)
                {
                    ViewOnHoldTransaction vonhldtrn = new ViewOnHoldTransaction();
                    Database.display($"SELECT BranchCode,MachineUsed,Transdate,ReferenceNo,TotalAmount,OnHoldName,PreparedBy " +
                       $"FROM dbo.BatchSalesSummary WHERE (Status='Pending' OR isHold='1') and BranchCode='{Login.assignedBranch}' " +
                       $"AND MachineUsed='{Environment.MachineName}' AND CashierTransNo='{lblTransactionIDCashier.Text}' ", vonhldtrn.gridControl1, vonhldtrn.gridView1);
                    vonhldtrn.ShowDialog(this);
                    if (ViewOnHoldTransaction.isdone == true)
                    {
                        //update transaction number
                        //update OR

                        //display();
                        //updateOR();
                        //refreshView();
                        //updateTransactionNo();
                        displayHoldTransactions(ViewOnHoldTransaction.refno, ViewOnHoldTransaction.machinename);
                        txtOrderNo.Text = ViewOnHoldTransaction.refno;
                        updateTransactionNo();
                        Database.ExecuteQuery($"INSERT INTO dbo.POSTransaction VALUES ('{Login.assignedBranch}','{lblTransactionIDInc.Text}','{Environment.MachineName}','RECOVER TRAN','{DateTime.Now.ToString()}','{Login.isglobalUserID}','0','0')");
                        updateTransactionNo();
                        ViewOnHoldTransaction.isdone = false;
                        vonhldtrn.Dispose();
                    }
                }
                else
                {
                    XtraMessageBox.Show("No Pending Transactions!...");
                    return;
                }
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void recovered_Click(object sender, EventArgs e)
        {
            recoveredTransaction();
        }

        void recoveredTransaction()
        {
            try
            {
                ViewRecoveredTransactionsFrm vrsdftrn = new ViewRecoveredTransactionsFrm();
                Database.display("SELECT ReferenceNo" +
                               ",OnHoldName AS CustomerName" +
                               ",TotalItem" +
                               ",SubTotal" +
                               ",TotalAmount" +
                               ",AdvancePayment" +
                               ",Transdate AS DateHold" +
                               ",PreparedBy as TransactedBy " +
                               "FROM BatchSalesSummary " +
                               "WHERE isFloat='0' " +
                               "and isHold='1' " +
                               "and isVoid='0'", vrsdftrn.gridControl1, vrsdftrn.gridView1);
                vrsdftrn.ShowDialog(this);
               

                if (ViewRecoveredTransactionsFrm.isdone == true)
                {
                    ViewRecoveredTransactionsFrm.isdone = false;
                    vrsdftrn.Dispose();
                  
                    Database.displayLocalGrid("SELECT SequenceNumber AS ID" +
                                           ",Description AS Particulars" +
                                           ",FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice" +
                                           ",QtySold AS Qty" +
                                           ",FORMAT(DiscountTotal,'N', 'en-us') AS Discount" +
                                           ",FORMAT(TotalAmount,'N', 'en-us') AS Amount" +
                                           ",isVat " +
                                           "FROM BatchSalesDetails " +
                                           "WHERE ReferenceNo='" + ViewRecoveredTransactionsFrm.refno + "' " +
                                           "and isHold='1' " +
                                           "and isCancelled=0 " +
                                           "and isVoid=0 " +
                                           "AND BranchCode='" + Login.assignedBranch + "' " +
                                           "ORDER BY SequenceNumber DESC", MydataGridView1);
                    MydataGridView1.Columns["ID"].Visible = true; //localgridview

                    var rows = Database.getMultipleQuery("BatchSalesSummary", "ReferenceNo='" + ViewRecoveredTransactionsFrm.refno + "' and isHold=1 ",
                        "TotalItemSold" +
                        ",TotalVatSale" +
                        ",TotalVatExemptSale" +
                        ",TotalVatableSale" +
                        ",TotalDiscountAmount" +
                        ",TotalItemDiscount" +
                        ",SeniorDiscount" +
                        ",PwdDiscountAmount" +
                        ",SubTotal" +
                        ",TotalAmount");

                    string TotalItemSold = rows["TotalItemSold"].ToString();
                    string TotalVatSale = rows["TotalVatSale"].ToString();
                    string TotalVatExemptSale = rows["TotalVatExemptSale"].ToString();
                    string TotalVatableSale = rows["TotalVatableSale"].ToString();
                    string TotalDiscountAmount = rows["TotalDiscountAmount"].ToString();
                    string TotalItemDiscount = rows["TotalItemDiscount"].ToString();
                    string SeniorDiscount = rows["SeniorDiscount"].ToString();
                    string PwdDiscountAmount = rows["PwdDiscountAmount"].ToString();
                    string SubTotal = rows["SubTotal"].ToString();
                    string TotalAmount = rows["TotalAmount"].ToString();


                    lblTotalItems.Text = TotalItemSold;
                    txtOrderNo.Text = ViewRecoveredTransactionsFrm.refno;
                    lblvatsale.Text = TotalVatableSale;
                    lblvatexemptsale.Text = TotalVatExemptSale;
                    lblvat.Text = TotalVatSale;
                    lblTotalDiscount.Text = TotalDiscountAmount;
                    lblseniordiscount.Text = SeniorDiscount;
                    lblpwddiscount.Text = PwdDiscountAmount;
                    lblperitemdiscountamount.Text = TotalItemDiscount;
                    lblTotalAmount.Text = TotalAmount;

                    //Database.displayLocalGrid("SELECT Description AS Particulars" +
                    //                            ",UnitPrice" +
                    //                            ",QtySold AS Qty" +
                    //                            ",TotalAmount AS Amount " +
                    //                            "FROM BatchSalesDetails " +
                    //                            "WHERE ReferenceNo='" + ViewRecoveredTransactionsFrm.refno + "' " +
                    //                            "and isHold='1' ", MydataGridView1);

                    //bool isexist = Database.checkifExist("SELECT Description AS Particulars,UnitPrice,QtySold AS Qty,TotalAmount AS Amount FROM BatchSalesDetails WHERE ReferenceNo='" + ViewRecoveredTransactionsFrm.refno + "' and  isVoid='0' ");
                    //if (isexist)
                    //{
                    //    MydataGridView1.Columns["ID"].Visible = true; //localgridview
                    //    Database.displayLocalGrid("SELECT Description AS Particulars,UnitPrice,QtySold AS Qty,TotalAmount AS Amount FROM BatchSalesDetails WHERE ReferenceNo='" + ViewRecoveredTransactionsFrm.refno + "' and  isVoid='0' ", MydataGridView1);
                    //    // textEdit3.Text = ViewRecoveredTransactionsFrm.refno;
                    //    txtOrderNo.Text = ViewRecoveredTransactionsFrm.refno;
                    //    txtsku.Focus();
                    //}
                 
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
      
        private void txtcustname_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtcustid.Text = Customers.getCustID(txtcustname.Text);
        }

        private void txtsku_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            POS.POSEncodeCustomerInfo asda = new POS.POSEncodeCustomerInfo();
            asda.ShowDialog(this);
            if(POS.POSEncodeCustomerInfo.isdone == true)
            {
                txtcustnamercpt.Text = POS.POSEncodeCustomerInfo.custname;
                txtcustaddressrcpt.Text = POS.POSEncodeCustomerInfo.custaddress;
                txtcusttinrcpt.Text = POS.POSEncodeCustomerInfo.custtin;
                txtcustbussstyle.Text = POS.POSEncodeCustomerInfo.custbusiness;
                POS.POSEncodeCustomerInfo.isdone = false;
                asda.Dispose();
            }
        }

        void holdTransaction()
        {
            bool isPendingTran = Database.checkifExist("SELECT TOP(1) Status FROM dbo.BatchSalesSummary WHERE Status='Pending' " +
            "AND BranchCode='" + Login.assignedBranch + "' " +
            "AND MachineUsed='" + Environment.MachineName + "' " +
            "AND ReferenceNo='" + txtOrderNo.Text + "' " +
            //"AND isHold='0' " +
            "AND CashierTransNo='" + lblTransactionIDCashier.Text + "' ");

            if (MydataGridView1.RowCount > 0 && isPendingTran == true)
            {
                bool isOK = HelperFunction.ConfirmDialog("Are you sure you want to hold this transaction?", "HOLD TRANSACTION");
                if (isOK == true)
                {
                    POSOnHoldTransaction possstran = new POSOnHoldTransaction();
                    //possstran.lbltranscode.Text = lblTransactionIDInc.Text;
                    possstran.lblrefno.Text = txtOrderNo.Text;
                    possstran.ShowDialog(this);
                    if (POSOnHoldTransaction.isdone == true)
                    {
                        POSOnHoldTransaction.isdone = false;
                        possstran.Dispose();
                        display();
                        updateOR();
                        refreshView();
                        updateTransactionNo();

                    }
                }
            }
            else
            {
                XtraMessageBox.Show("No Selected Item to be hold!");
            }
        }

        private void btnhold_Click(object sender, EventArgs e)
        {
            
            //bool isoverride = false;
            //isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM dbo.POSFunctions WHERE FunctionName='HOLDTRAN' AND isOverride=1");
            //if (!isoverride)
            //{
            //    holdTransaction();
            //}
            //else
            //{
            //    AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            //    authfrm.ShowDialog(this);
            //    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            //    {
            //        holdTransaction();
            //        AuthorizedConfirmationFrm.isconfirmedLogin = false;
            //        authfrm.Dispose();
            //    }
            //}
        }

        private void Onhold_Click(object sender, EventArgs e)
        {
            
            //bool isoverride = false;
            //isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM dbo.POSFunctions WHERE FunctionName='RECOVERTRAN' AND isOverride=1");
            //if (!isoverride)
            //{
            //    OnHoldTransaction();
            //}
            //else
            //{
            //    AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            //    authfrm.ShowDialog(this);
            //    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            //    {
            //        OnHoldTransaction();
            //        AuthorizedConfirmationFrm.isconfirmedLogin = false;
            //        authfrm.Dispose();
            //    }
            //}
        }

        void poschar_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (POS.POSChargeToClient.paymentstat == "OK")
            {
                updateOR();
                refreshView();
                updateTransNo();
            }
            else if (POS.POSChargeToClient.paymentstat == "CANCEL")
            {
                return;
            }
        }

        private void txtsku_TextChanged(object sender, EventArgs e)
        {
            //_lastBarCodeCharReadTime = DateTime.Now;
            //if (!_timer.Enabled)
            //    _timer.Start();
        }

    

        private void btnreprint_Click(object sender, EventArgs e)
        {
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM dbo.POSFunctions WHERE FunctionName='REPRINT' AND isOverride=1");
            if (!isoverride)
            {
                _refno = txtOrderNo.Text;
                POS.POSReprintOR posrep = new POS.POSReprintOR();
                int id = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "' and ReprintCtr is not null AND MachineUsed='" + Environment.MachineName + "' and Type='REPRINT OR'", "ReprintCtr", 1);

                posrep.txtcounter.Text = id.ToString();
                posrep.txttransno.Text = lblTransactionIDInc.Text;
                posrep.txtcashiertranid.Text = lblTransactionIDCashier.Text;
                posrep.ShowDialog(this);
                if (POS.POSReprintOR.isdone == true)
                {
                    posrep.Dispose();
                    POS.POSReprintOR.isdone = false;
                    updateTransactionNo();
                }
            }
            else
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    _refno = txtOrderNo.Text;
                    POS.POSReprintOR posrep = new POS.POSReprintOR();
                    int id = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "' and ReprintCtr is not null AND MachineUsed='" + Environment.MachineName + "' and Type='REPRINT OR'", "ReprintCtr", 1);

                    posrep.txtcounter.Text = id.ToString();
                    posrep.txttransno.Text = lblTransactionIDInc.Text;
                    posrep.txtcashiertranid.Text = lblTransactionIDCashier.Text;
                    posrep.ShowDialog(this);
                    if (POS.POSReprintOR.isdone == true)
                    {
                        posrep.Dispose();
                        POS.POSReprintOR.isdone = false;
                        updateTransactionNo();

                    }
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }

            

            
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            searchProductItems();
        }

        private void chckdisplaypool_CheckedChanged(object sender, EventArgs e)
        {
            if(chckdisplaypool.Checked==true)
            {
                txtcomport.Enabled = true;
                getAvailablePort();
            }
            else
            {
                txtcomport.Enabled = false;
            }
        }

        private void btndisplaypoolset_Click(object sender, EventArgs e)
        {
            //bool isAlreadySet = Database.checkifExist("SELECT * FROM POSType WHERE DisplayPoolPort='" + txtcomport.Text + "'");
            //if(isAlreadySet)
            //{

            //}
            //else
            //{
            //    btndisplaypoolset.Enabled = true;
            //    Database.ExecuteQuery("UPDATE POSType SET DisplayPoolPort='" + txtcomport.Text + "'");
            //    XtraMessageBox.Show("Display Pool Port Successfully Updated");
            //}

            Database.ExecuteQuery("UPDATE POSType SET DisplayPoolPort='" + txtcomport.Text + "'");
            XtraMessageBox.Show("Display Pool Port Successfully Updated");
        }

        void searchProductItems()
        {
            try
            {
                SearchProduct searchprod = new SearchProduct();
                searchprod.ShowDialog(this);
                if (SearchProduct.isUsedSearchForm == true) //isUsedSearchForm indicator ni cya sa searchproduct form kng nigamit ba cya og searchform
                {
                    isusedsearchform = true; //is isusedsearchform is a local variable declare in this class
                    txtsku.Text = SearchProduct.prodcode;
                    prodcode = "";
                    prodcode = SearchProduct.prodcode;
                    //SearchProduct.isUsedSearchForm = false;
                    ispriceused = SearchProduct.priceused;
                    searchprod.Dispose();
                    txtsku.Focus();
                    addOrder();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        //OLD
        //void searchProductItems()
        //{
        //    try
        //    {
        //        SearchProduct searchprod = new SearchProduct();
        //        searchprod.ShowDialog(this);
        //        if (SearchProduct.isUsedSearchForm == true) //isUsedSearchForm indicator ni cya sa searchproduct form kng nigamit ba cya og searchform
        //        {
        //            isusedsearchform = true; //is isusedsearchform is a local variable declare in this class
        //            if (SearchProduct.havebarcode == true) //kng pag select nya kay naay barcode
        //            {
        //                txtsku.Text = SearchProduct.barcode; // 
        //                isusedbarcode = true;
        //                SearchProduct.isUsedSearchForm = false;
        //            }
        //            else //kung pag select nya sa item sa search product kay wlaay barcode
        //            {
        //                txtsku.Text = SearchProduct.prodcode;// SearchProduct.prodcode.Substring(0, 2) + SearchProduct.prodcode + SearchProduct.qty.Replace(".", "");

        //                isusedbarcode = false;
        //                SearchProduct.isUsedSearchForm = false;
        //            }
        //            prodcode = "";
        //            prodcode = SearchProduct.prodcode;
        //            //SearchProduct.isUsedSearchForm = false;
        //            ispriceused = SearchProduct.priceused;
        //            searchprod.Dispose();
        //            txtsku.Focus();
        //            addOrder();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}

        private void txtOrderNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            totalamountstr = lblTotalAmount.Text;
            string totalamount = lblTotalAmount.Text;
            if (txtcomport.Text == "")
            {
                XtraMessageBox.Show("Please Select COM-PORT");
            }
            else if (serialPort1.IsOpen && chckdisplaypool.Checked == true)
            {
                serialPort1.Write(Convert.ToString((char)12));
                serialPort1.WriteLine("Total: " + totalamount);
                //serialPort1.WriteLine((char)13 + "₱ " + total1);
            }
            paymentTransaction();
        }

        void paymentTransaction()
        {
            try
            {
                _transcode = lblTransactionIDCashier.Text;
                mygridview = MydataGridView1; //mygridview is a static variable declare inside the class, while MyDataGridView1 is a dataGridViewForm and set modifier to public
                custname = txtcustname.Text; //Customer Name
                custcode = txtcustid.Text; //Customer Code
                refno = txtOrderNo.Text.Trim(); //Order No.
                vat = lblvat.Text; //Vat Sale
                vatablesale = lblvatsale.Text; //Vatable Sales
                vatexemptsale = lblvatexemptsale.Text; //Vat Exempt Sales
                if (Convert.ToDouble(lblTotalItems.Text) < 1)
                {
                    XtraMessageBox.Show("No Transaction Entry");
                }
                else
                {
                    if(chckZeroRated.Checked==true)
                    {
                        iszeroratedsale = true;
                    }
                    else
                    {
                        iszeroratedsale = false;
                    }
                    double totaldiscount = 0.0, totalonetimediscount = 0.0;
                    POS.POSConfirmPayment posconfirm = new POS.POSConfirmPayment();

                    bool haveDiscount = false;
                    haveDiscount = haveOneTimeDiscount();
                    if (haveDiscount) //IF TRUE
                    {
                        var rows = Database.getMultipleQuery("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0",
                        "DiscountType,DiscountAmount,DiscName,DiscIDNo,DiscRemarks");
                        string DiscountType, DiscountAmount, DiscName, DiscIDNo, DiscRemarks;

                        DiscountType = rows["DiscountType"].ToString();
                        DiscountAmount = rows["DiscountAmount"].ToString();
                        DiscName = rows["DiscName"].ToString();
                        DiscIDNo = rows["DiscIDNo"].ToString();
                        DiscRemarks = rows["DiscRemarks"].ToString();

                        if (DiscountType == "SENIOR")
                        {
                            posconfirm.txtseniorcontrolno.Text = DiscIDNo;//seniorcontrolno;
                            posconfirm.txtseniorname.Text = DiscName;//seniorname;
                            posconfirm.txtseniordiscountamount.Text = DiscountAmount;
                            totalonetimediscount = Convert.ToDouble(DiscountAmount);

                            totaldiscount = Convert.ToDouble(DiscountAmount) + Convert.ToDouble(posconfirm.txtordinarydiscountamount.Text);
                        }
                        else if (DiscountType == "PWD")
                        {
                            posconfirm.txtpwdid.Text = DiscIDNo;//seniorcontrolno;
                            posconfirm.txtpwdname.Text = DiscName;//seniorname;
                            posconfirm.txtpwddiscountamount.Text = DiscountAmount;
                            totalonetimediscount = Convert.ToDouble(DiscountAmount);

                            totaldiscount = Convert.ToDouble(DiscountAmount) + Convert.ToDouble(posconfirm.txtordinarydiscountamount.Text);
                        }
                        else if (DiscountType == "REGULAR")
                        {
                            otherDiscountAmount = DiscountAmount;
                            othersRemarks = DiscRemarks;
                            lblotherdiscount.Text = DiscountAmount;
                            isOthersDiscount = true;

                            posconfirm.txtothersdiscountamount.Text = DiscountAmount;//seniorcontrolno;
                            posconfirm.txtothersremarks.Text = DiscRemarks;//seniorname;
                            totalonetimediscount = Convert.ToDouble(DiscountAmount);

                            totaldiscount = Convert.ToDouble(DiscountAmount) + Convert.ToDouble(posconfirm.txtordinarydiscountamount.Text);
                        }
                    }
                  
                    posconfirm.txtcustnamercpt.Text = txtcustnamercpt.Text;
                    posconfirm.txtcustaddressrcpt.Text = txtcustaddressrcpt.Text;
                    posconfirm.txtcusttinrcpt.Text = txtcusttinrcpt.Text;
                    posconfirm.txtcustbussstyle.Text = txtcustbussstyle.Text;

                    posconfirm.lblorderno.Text = txtOrderNo.Text; //ORDER NO
                    posconfirm.lbltransno.Text = lblTransactionIDInc.Text;
                    posconfirm.lbltranscode.Text = lblTransactionIDCashier.Text; //TRANSACTION CODE

                    posconfirm.txtordinarydiscountamount.Text = lblperitemdiscountamount.Text; //ORDINARY DISCOUNT FIELD OR PER ITEM
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    double totaldiscounts = 0.0,totaldue=0.0,netdue=0.0,vatadj=0.0,subtotal=0.0;
                    //totaldiscounts = totaldiscount + Convert.ToDouble(lblperitemdiscountamount.Text); //ONE TIME DISCOUNT AMOUNT + TOTAL OF PER ITEM DISCOUNT
                    //totaldiscounts = getSalesDiscount() + Convert.ToDouble(lblperitemdiscountamount.Text); //ONE TIME DISCOUNT AMOUNT + TOTAL OF PER ITEM DISCOUNT
                    totaldiscounts = getSalesDiscount(); //ONE TIME DISCOUNT AMOUNT  SC,PWD,OTHERS
                    posconfirm.txtdiscount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS
                    posconfirm.txttotaldiscountamount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS

                    vatadj = getVatAdjustment();

                    if (iszeroratedsale == true)
                    {
                        string zeroratedsales = Database.getSingleResultSet("SELECT dbo.func_getZeroRatedSales('" + Login.assignedBranch + "','" + txtOrderNo.Text + "')");
                        subtotal = Convert.ToDouble(zeroratedsales);
                        netdue = subtotal; //TOTAL DUE - ONE TIME DISCOUNT
                    }
                    else
                    {
                        subtotal = computeTotalAmount();
                        totaldue = Convert.ToDouble(lblTotalAmount.Text) - totaldiscounts; //lbltotal is already deducted by per item discount
                        netdue = totaldue - vatadj; //TOTAL DUE - ONE TIME DISCOUNT
                                                    //netdue = totaldue - totaldiscount; //TOTAL DUE - ONE TIME DISCOUNT
                    }
                    subtotal = computeTotalAmount();
                    totaldue = Convert.ToDouble(lblTotalAmount.Text) - totaldiscounts; //lbltotal is already deducted by per item discount
                    netdue = totaldue - vatadj; //TOTAL DUE - ONE TIME DISCOUNT
                                                //netdue = totaldue - totaldiscount; //TOTAL DUE - ONE TIME DISCOUNT
                    posconfirm.txtamountpayable.Text = netdue.ToString();
                    posconfirm.txtamountpayableb4onetimediscount.Text = subtotal.ToString();// totaldue.ToString(); //TOTAL DUE
                    posconfirm.txtorigamountpayable.Text = computeTotalAmountWithDiscount().ToString();

                    posconfirm.lblvatsale.Text = lblvatsale.Text;
                    posconfirm.lblvatexempt.Text = lblvatexemptsale.Text;
                    posconfirm.lblvatinput.Text = lblvat.Text;
                    posconfirm.btncaller.Text = "POS";

                    posconfirm.lblcashiertransno.Text = lblTransactionIDCashier.Text;
                    posconfirm.ShowDialog(this);
                    if (POS.POSConfirmPayment.transactiondone == true)
                    {
                        //Classes.Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                        //refreshView();
                        //loadDefaultClient();
                        if (txtcomport.Text == "")
                        {
                            XtraMessageBox.Show("Please Select COM-PORT");
                        }
                        else if (serialPort1.IsOpen && chckdisplaypool.Checked==true)
                        {
                            serialPort1.Write(Convert.ToString((char)12));
                            serialPort1.WriteLine("Tender: " + String.Format("{0:0.00}", Convert.ToDouble(posconfirm.txtamounttender.Text)));
                            serialPort1.WriteLine((char)13 + "Change: " + String.Format("{0:0.00}", Convert.ToDouble(posconfirm.txtamountchange.Text)));
                        }

                        POS.POSHistoryCaption poshiscap = new POS.POSHistoryCaption();
                        poshiscap.txtamounttenderedcap.Text = posconfirm.txtamounttender.Text;
                        poshiscap.txtamountchangecap.Text = posconfirm.txtamountchange.Text;
                       
                        poshiscap.ShowDialog(this);
                       
                        POS.POSHistoryCaption.transactiondone = false;
                        updateOR(); 
                        refreshView();

                        //updateTransNo();
                        updateTransactionNo();

                        POS.POSConfirmPayment.transactiondone = false;

                        isOnetimeDiscount = false;
                        isSeniorDiscount = false;
                        isPwdDiscount = false;
                        isOthersDiscount = false;

                        posconfirm.Dispose();
                        setStandByText();
                        txtdiscount.Text = "0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void setStandByText()
        {
            try
            {
                string filename = "";
                string body = "WELCOME TO ENZO"+Environment.NewLine;
                body += "NEXT CUSTOMER PLEASE";
                string filepath = "C:\\POSTransaction\\DisplayPool\\";
                if (serialPort1.IsOpen && chckdisplaypool.Checked == true)
                {
                    serialPort1.Write(Convert.ToString((char)12));
                    if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);
                        filename = "\\standby.txt";
                        string filetoprint = filepath + filename;
                        StreamWriter writer = new StreamWriter(filetoprint, true);
                        writer.Write(body);
                        writer.Close();
                    }
                    string str = Classes.Utilities.readFile2("C:\\POSTransaction\\DisplayPool\\standby.txt");
                    string str1 = Classes.Utilities.readFile("C:\\POSTransaction\\DisplayPool\\standby.txt");
                    serialPort1.WriteLine(str);
                    serialPort1.WriteLine((char)13 + str1);

                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn()
        {
            string welcome = "WELCOME TO ENZO";
            byte[] txbuff = new byte[10]; // Byte array for the transmit data
            byte uid = 255;
            txbuff[0] = uid; // Load the transmit buffer
            txbuff[1] = 98;
            string a = welcome.PadLeft(6); // Insert spaces to made 6 bytes
            byte[] c = StrToByteArray(a); // Convert string to byte array
            int z = 0;
            for (z = 0; z < 6; z++)
            {
                txbuff[z + 2] = c[z];
            }
            serialPort1.Write(Convert.ToString((char)12));
            serialPort1.Write(txbuff, 0, 8); // Send transmit buffer to SC6Dlite
        }
        private static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }




        private void btnChargeAccount_Click(object sender, EventArgs e)
        {
            chargeToAccountTransaction();
        }

        void chargeToAccountTransaction()
        {
            try
            {
                _transcode = lblTransactionIDCashier.Text.Trim();
                mygridview = MydataGridView1; //mygridview is a static variable declare inside the class, while MyDataGridView1 is a dataGridViewForm and set modifier to public
                //refno = textEdit3.Text.Trim();
                refno = txtOrderNo.Text.Trim();
                custname = txtcustname.Text;
                custcode = txtcustid.Text;
                if (Convert.ToDouble(lblTotalItems.Text) < 1)
                {
                    XtraMessageBox.Show("No Transaction Entry");
                }
                else
                {
                    POS.POSChargeToClient poschar = new POS.POSChargeToClient();
                    poschar.ShowDialog(this);
                    if (POS.POSChargeToClient.transactiondone == true)
                    {
                        updateOR();
                        refreshView();
                        updateTransNo();
                        //loadDefaultClient();
                        POS.POSChargeToClient.transactiondone = false;
                        poschar.Dispose();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            showTransactionHistory();
        }

        void showTransactionHistory()
        {
            ViewTransactionHistoryFrm vsdtfrm = new ViewTransactionHistoryFrm();
            vsdtfrm.Show();
        }

        private void btnCancelLine_Click(object sender, EventArgs e)
        {
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='CANCELLINE' AND isOverride=1");
            if (!isoverride)
            {
                cancelLineTransaction();
                updateTransactionNo();
            }
            else
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    cancelLineTransaction();
                    updateTransactionNo();
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
        }

        void deleteTransactions()
        {
            try
            {
                bool ok = HelperFunction.ConfirmDialog("Sigurado?", "Delete Transaction!");
                if (ok)
                {
                    if (Convert.ToDouble(lblTotalItems.Text) < 1)
                    {
                        XtraMessageBox.Show("Please Select an item to Cancel");
                    }
                    else
                    {
                        //AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                        //authfrm.ShowDialog(this);
                        //if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                        //{
                        deleteTransaction();
                        updateOR();
                        refreshView();
                        updateTransNo();
                        //    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                        //    authfrm.Dispose();
                        //}
                    }
                    //Database.ExecuteQuery("UPDATE BatchSalesDetails SET isCancelled='1',CancelledBy='EULZ' WHERE SequenceNumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString() + "'", "Successfully Cancelled");
                }
                display();
                txtsku.Focus();
                MydataGridView1.CurrentCell = MydataGridView1.Rows[MydataGridView1.Rows.Count - 1].Cells[1];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void cancelLineTransaction()
        {
            try
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Cancel this Transaction?", "Cancel Transaction!");
                if (ok)
                {
                    if (Convert.ToDouble(lblTotalItems.Text) < 1)
                    {
                        XtraMessageBox.Show("Please Select an item to Cancel");
                    }
                    else
                    {
                        bool checkIfDiscounted = Database.checkifExist("SELECT TOP 1 OrderNo FROM SalesDiscount " +
                            "WHERE OrderNo='" + txtOrderNo.Text + "' " +
                            "AND isErrorCorrect=0 " +
                            "AND BranchCode='" + Login.assignedBranch + "' " +
                            "AND MachineUsed='" + Environment.MachineName + "' ");
                        if(checkIfDiscounted)
                        {
                            bool flag = HelperFunction.ConfirmDialog("You Already Apply a One Time Discount on this Transaction.. Are you sure you want to Cancel? Discount Amount will be Discarded. Please Re-Apply Discount Again.", "Cancel Transaction!");
                            if(flag)
                            {
                                Database.ExecuteQuery($"UPDATE SalesDiscount set isErrorCorrect=1 " +
                                    $"WHERE OrderNo='{txtOrderNo.Text}' " +
                                    $"AND isErrorCorrect=0 " +
                                    $"AND BranchCode='{Login.assignedBranch}' " +
                                    $"AND MachineUsed='{Environment.MachineName}' ");
                                cancelTransaction();
                               
                                lblseniordiscount.Text = "0";
                                lblpwddiscount.Text = "0";
                                lblotherdiscount.Text = "0";
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            cancelTransaction();
                        }
                        bool ifnoitemcancelled = Database.checkifExist($"SELECT TOP(1) BranchCode FROM BatchSalesDetails " +
                                   $"WHERE ReferenceNo='{txtOrderNo.Text}' " +
                                   $"AND BranchCode='{Login.assignedBranch}' " +
                                   $"AND MachineUsed='{Environment.MachineName}' " +
                                   $"AND Status='Pending' ");
                        if (ifnoitemcancelled == false)
                        {
                            Database.ExecuteQuery($"UPDATE BatchSalesSummary SET Status='CANCELLED' " +
                                $"WHERE ReferenceNo='{txtOrderNo.Text}' " +
                                $"AND BranchCode='{Login.assignedBranch}'" +
                                $"AND MachineUsed='{Environment.MachineName}' ");
                        }
                    }
                    //Database.ExecuteQuery("UPDATE BatchSalesDetails SET isCancelled='1',CancelledBy='EULZ' WHERE SequenceNumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString() + "'", "Successfully Cancelled");
                }
                display();
                txtsku.Focus();
                //MydataGridView1.CurrentCell = MydataGridView1.Rows[MydataGridView1.Rows.Count - 1].Cells[1];
                if(MydataGridView1.RowCount>0)
                {
                    MydataGridView1.Rows[0].Selected = true;
                }
               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
           
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='TRANSVOID' AND isOverride=1");
            if (!isoverride)
            {
                voidTransactions();
            }
            else
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    voidTransactions();
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
        }

        void voidTransactions()
        {
            try
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Void this Transaction?", "Void Transaction!");
                if (ok)
                {
                    if (Convert.ToDouble(lblTotalItems.Text) < 1)
                    {
                        XtraMessageBox.Show("Please Select an item to Void");
                    }
                    else
                    {
                        voidTransaction();
                        //updateOR();
                        refreshView();
                        updateTransactionNo();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnErrorCorrect_Click(object sender, EventArgs e)
        {
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='RETURN' AND isOverride=1");
            if (!isoverride)
            {
                cashierTransactionCode = lblTransactionIDCashier.Text;
                //transcode = lblTransactionIDInc.Text;
                POS.POSErrrorCorrect poserror = new POS.POSErrrorCorrect();
                poserror.txttransno.Text = lblTransactionIDInc.Text;
                poserror.ShowDialog(this);
                if (POS.POSErrrorCorrect.isdone == true)
                {
                    updateTransactionNo();
                    POS.POSErrrorCorrect.isdone = false;
                    poserror.Dispose();
                }
            }
            else
            {
                userid = AuthorizedConfirmationFrm.isglobalUserID;
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    
                    cashierTransactionCode = lblTransactionIDCashier.Text;
                    //transcode = lblTransactionIDInc.Text;
                    POS.POSErrrorCorrect poserror = new POS.POSErrrorCorrect();
                    poserror.txttransno.Text = lblTransactionIDInc.Text;
                    poserror.ShowDialog(this);
                    if (POS.POSErrrorCorrect.isdone == true)
                    {
                        updateTransactionNo();
                        POS.POSErrrorCorrect.isdone = false;
                        poserror.Dispose();
                    }
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
        }

        private void btnCloseAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(lblTotalItems.Text) > 0)
                {
                    XtraMessageBox.Show("Cannot Continue there is an item that need to be purchased.");
                }
                else
                {

                    POS.POSCloseTransactionAuthentication authfrm = new POSCloseTransactionAuthentication();
                    authfrm.ShowDialog(this);
                    if (POSCloseTransactionAuthentication.isconfirmedLogin == true)
                    {
                        cashierTransactionCode = POSCloseTransactionAuthentication.CashierTransNo;
                        displayItems(POSCloseTransactionAuthentication.UserID, cashierTransactionCode, POSCloseTransactionAuthentication.MachineUsed);
                        POSCloseTransactionAuthentication.isconfirmedLogin = false;
                        authfrm.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }


        void displayItems(string userid,string cashiertransno,string machinename)
        {
            
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spr_POSCloseTransaction";
                com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", Login.assignedBranch);
                //com.Parameters.AddWithValue("@parmcashiertransno", lblTransactionIDCashier.Text);
                com.Parameters.AddWithValue("@parmcashiertransno", cashiertransno);
                //com.Parameters.AddWithValue("@parmmachineused", Environment.MachineName);
                com.Parameters.AddWithValue("@parmmachineused", machinename);
                //com.Parameters.AddWithValue("@parmuserid", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmuserid", userid);

                com.Parameters.Add("@parmtransdate", SqlDbType.Char, 8).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtransno", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmbeginsino", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmendsino", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmbegintransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmendtransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmbeginrettransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmendrettransno", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmbeginningcash", SqlDbType.Money).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmnoofsolditem", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofcancelleditem", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofvoiditem", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofdiscount", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofvatitems", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofreturneditem", SqlDbType.Int).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmnoofscdisc", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofpwddisc", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofnaacdisc", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofmovdisc", SqlDbType.Int).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmnoofspdisc", SqlDbType.Int).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmnoofregdisc", SqlDbType.Int).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmtotalofcancelleditem", SqlDbType.Decimal,12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofcancelleditem"].Precision = 12;
                com.Parameters["@parmtotalofcancelleditem"].Scale = 2;

                com.Parameters.Add("@parmtotalofvoiditem", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofvoiditem"].Precision = 12;
                com.Parameters["@parmtotalofvoiditem"].Scale = 2;

                com.Parameters.Add("@parmtotalofdiscountitem", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofdiscountitem"].Precision = 12;
                com.Parameters["@parmtotalofdiscountitem"].Scale = 2;

                com.Parameters.Add("@parmtotalofvatitems", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofvatitems"].Precision = 12;
                com.Parameters["@parmtotalofvatitems"].Scale = 2;

                com.Parameters.Add("@parmtotalofreturneditems", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofreturneditems"].Precision = 12;
                com.Parameters["@parmtotalofreturneditems"].Scale = 2;

                com.Parameters.Add("@parmtotalofscdisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofscdisc"].Precision = 12;
                com.Parameters["@parmtotalofscdisc"].Scale = 2;


                com.Parameters.Add("@parmtotalofpwddisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofpwddisc"].Precision = 12;
                com.Parameters["@parmtotalofpwddisc"].Scale = 2;


                ////////////////////
                com.Parameters.Add("@parmtotalofnaacdisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofnaacdisc"].Precision = 12;
                com.Parameters["@parmtotalofnaacdisc"].Scale = 2;


                com.Parameters.Add("@parmtotalofmovdisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofmovdisc"].Precision = 12;
                com.Parameters["@parmtotalofmovdisc"].Scale = 2;

                com.Parameters.Add("@parmtotalofspdisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofspdisc"].Precision = 12;
                com.Parameters["@parmtotalofspdisc"].Scale = 2;
                ///////////////////////////









                com.Parameters.Add("@parmtotalofregdisc", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalofregdisc"].Precision = 12;
                com.Parameters["@parmtotalofregdisc"].Scale = 2;

                com.Parameters.Add("@parmvatadjustment", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmvatadjustment"].Precision = 12;
                com.Parameters["@parmvatadjustment"].Scale = 2;

                com.Parameters.Add("@parmvatablesale", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmvatablesale"].Precision = 12;
                com.Parameters["@parmvatablesale"].Scale = 2;


                com.Parameters.Add("@parmvatexemptsale", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmvatexemptsale"].Precision = 12;
                com.Parameters["@parmvatexemptsale"].Scale = 2;

                com.Parameters.Add("@parmvatamount", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmvatamount"].Precision = 12;
                com.Parameters["@parmvatamount"].Scale = 2;

                com.Parameters.Add("@parmzeroratedsale", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmzeroratedsale"].Precision = 12;
                com.Parameters["@parmzeroratedsale"].Scale = 2;


                com.Parameters.Add("@parmtotalcashsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalcashsales"].Precision = 12;
                com.Parameters["@parmtotalcashsales"].Scale = 2;

                com.Parameters.Add("@parmtotalcreditsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalcreditsales"].Precision = 12;
                com.Parameters["@parmtotalcreditsales"].Scale = 2;

                com.Parameters.Add("@parmtotalnetsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalnetsales"].Precision = 12;
                com.Parameters["@parmtotalnetsales"].Scale = 2;

                com.Parameters.Add("@parmtotalgrosssales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalgrosssales"].Precision = 12;
                com.Parameters["@parmtotalgrosssales"].Scale = 2;

                com.Parameters.Add("@parmtotalchargetoaccountsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalchargetoaccountsales"].Precision = 12;
                com.Parameters["@parmtotalchargetoaccountsales"].Scale = 2;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

                POSClosedTransaction pocls = new POSClosedTransaction();

                pocls.txtcashiertransno.Text = cashierTransactionCode;// lblTransactionIDCashier.Text;
                pocls.txttransactionno.Text = com.Parameters["@parmtransno"].Value.ToString(); //lblTransactionIDInc.Text;
                pocls.txtcashiername.Text = Login.Fullname;

                pocls.txttransactiondate.Text = com.Parameters["@parmtransdate"].Value.ToString();
                pocls.txtBeginningCash.Text = com.Parameters["@parmbeginningcash"].Value.ToString();

                pocls.txtbeginninginvoice.Text = com.Parameters["@parmbeginsino"].Value.ToString();
                pocls.txtendingsi.Text = com.Parameters["@parmendsino"].Value.ToString();

                pocls.txtbegtransno.Text = com.Parameters["@parmbegintransno"].Value.ToString();
                pocls.txtendtransno.Text = lblTransactionIDInc.Text;

                pocls.txtbegretno.Text = com.Parameters["@parmbeginrettransno"].Value.ToString();
                pocls.txtendretno.Text = com.Parameters["@parmendrettransno"].Value.ToString();

                pocls.txtnoofsolditem.Text = com.Parameters["@parmnoofsolditem"].Value.ToString();
                pocls.txtnoofcancelleditem.Text = com.Parameters["@parmnoofcancelleditem"].Value.ToString();
                pocls.txtnoofvoiditem.Text = com.Parameters["@parmnoofvoiditem"].Value.ToString();
                pocls.txtnoofdiscount.Text = com.Parameters["@parmnoofdiscount"].Value.ToString();
                pocls.txtnoofvat.Text = com.Parameters["@parmnoofvatitems"].Value.ToString();
                pocls.txtnoofreturneditem.Text = com.Parameters["@parmnoofreturneditem"].Value.ToString();

                pocls.txtnoofscdisc.Text = com.Parameters["@parmnoofscdisc"].Value.ToString();
                pocls.txtnoofpwddisc.Text = com.Parameters["@parmnoofpwddisc"].Value.ToString();
                pocls.txtnoofnaacdisc.Text = com.Parameters["@parmnoofnaacdisc"].Value.ToString();
                pocls.txtnoofmovdisc.Text = com.Parameters["@parmnoofmovdisc"].Value.ToString(); 
                pocls.txtnoofspdisc.Text = com.Parameters["@parmnoofspdisc"].Value.ToString();

                pocls.txtnoofregdisc.Text = com.Parameters["@parmnoofregdisc"].Value.ToString();

                pocls.txtTotalCancelledTransaction.Text = com.Parameters["@parmtotalofcancelleditem"].Value.ToString();
                pocls.txtTotalVoidTransaction.Text = com.Parameters["@parmtotalofvoiditem"].Value.ToString();
                pocls.txtTotalDiscount.Text = com.Parameters["@parmtotalofdiscountitem"].Value.ToString();
                pocls.txtTotalTax.Text = com.Parameters["@parmtotalofvatitems"].Value.ToString();
                pocls.txtTotalReturnedTransaction.Text = com.Parameters["@parmtotalofreturneditems"].Value.ToString();


                pocls.txttotalofscdisc.Text = com.Parameters["@parmtotalofscdisc"].Value.ToString();
                pocls.txttotalofpwddisc.Text = com.Parameters["@parmtotalofpwddisc"].Value.ToString();

                pocls.txttotalofnaacdisc.Text = com.Parameters["@parmtotalofnaacdisc"].Value.ToString();
                pocls.txttotalofmovdisc.Text = com.Parameters["@parmtotalofmovdisc"].Value.ToString();
                pocls.txttotalofspdisc.Text = com.Parameters["@parmtotalofspdisc"].Value.ToString();
                pocls.txttotalofregdisc.Text = com.Parameters["@parmtotalofregdisc"].Value.ToString();

                pocls.txtvatadjustment.Text = com.Parameters["@parmvatadjustment"].Value.ToString();

                pocls.txtvatablesales.Text = com.Parameters["@parmvatablesale"].Value.ToString();
                pocls.txtvatexemptsale.Text = com.Parameters["@parmvatexemptsale"].Value.ToString();
                pocls.txtvatamount.Text = com.Parameters["@parmvatamount"].Value.ToString();
                pocls.txtzeroratedsale.Text = com.Parameters["@parmzeroratedsale"].Value.ToString();
                //pocls.txtnetsalesofvat.Text = com.Parameters["@parmnetsalesofvat"].Value.ToString();

                pocls.txtTotalCashSales.Text = com.Parameters["@parmtotalcashsales"].Value.ToString();
                pocls.txtTotalCreditSales.Text = com.Parameters["@parmtotalcreditsales"].Value.ToString();
                pocls.txtTotalNetSales.Text = com.Parameters["@parmtotalnetsales"].Value.ToString();
                pocls.txttotalgross.Text = com.Parameters["@parmtotalgrosssales"].Value.ToString();
                pocls.txtchargesales.Text = com.Parameters["@parmtotalchargetoaccountsales"].Value.ToString();

                

                Database.displayLocalGrid("select CONVERT(VARCHAR(10), DateOrder, 120) AS Date " +
                    ", DatePart(hh, DateOrder) as Hour " +
                    ", SUM(QtySold) as QtySold " +
                    ", SUM(TotalAmount) AS TotalAmount " +
                    ", COUNT(*) as TotalItems " +
                    "from dbo.BatchSalesDetails " +
                    "WHERE CAST(DateOrder as date)='" + DateTime.Now.ToShortDateString() + "' " +
                    // "AND CashierTransNo='" + lblTransactionIDCashier.Text + "' " +
                    "AND CashierTransNo='" + cashierTransactionCode + "' " +
                    "GROUP BY CONVERT(VARCHAR(10), DateOrder, 120), DatePart(hh, DateOrder) ", pocls.MydataGridView1);

                pocls.ShowDialog(this);
                if (POSClosedTransaction.isdone == true)
                {
                    POSClosedTransaction.isdone = false;
                    pocls.Dispose();
                    if(lblTransactionIDCashier.Text==cashierTransactionCode)
                    {
                        this.Dispose();
                    }
                    // Classes.Utilities.writeTextfile("C:\\POSTransaction\\TranSeries\\counter.txt", transcode);
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

        void closedTransactions()
        {
            try
            {
                if (Convert.ToDouble(lblTotalItems.Text) > 0)
                {
                    XtraMessageBox.Show("Cannot Continue there is an item that need to be purchased.");
                }
                else
                {
                    POS.POSCloseTransactionAuthentication authfrm = new POSCloseTransactionAuthentication();
                    authfrm.ShowDialog(this);
                    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                    {
                        cashierTransactionCode = POSCloseTransactionAuthentication.CashierTransNo;//lblTransactionIDCashier.Text;
                        //transcode = lblTransactionIDInc.Text;

                        //CashierTransNo, MachineUsed;
                        displayItems(POSCloseTransactionAuthentication.UserID, cashierTransactionCode, POSCloseTransactionAuthentication.MachineUsed);
                        POSCloseTransactionAuthentication.isconfirmedLogin = false;
                        authfrm.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            exitPOS();
        }

        void exitPOS()
        {
            try
            {
                if (Convert.ToDouble(lblTotalItems.Text) != 0)
                {
                    bool ok = HelperFunction.ConfirmDialog("The System found that current transactions is not yet validated! Are you sure you want to void this transaction before Exit? \n\n Note: Voiding Transaction needs an Administrative Login Approval!", "Void Transaction");
                    if (ok)
                    {
                        //AuthorizedConfirmationFrm atghdfrm = new AuthorizedConfirmationFrm();
                        //atghdfrm.FormClosed += new FormClosedEventHandler(atghdfrm_FormClosed);
                        //atghdfrm.Show();
                        //cancelTransaction();
                        voidTransactions();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtcustname_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtcustid.Text = Customers.getCustID(txtcustname.Text);
            txtsku.Focus();
        }

        private void btnEditLine_Click(object sender, EventArgs e)
        {
            //btn();//editLineTransaction();

            //AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            //authfrm.ShowDialog(this);
            //if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            //{

            //    AuthorizedConfirmationFrm.isconfirmedLogin = false;
            //    authfrm.Dispose();
            //}
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='EDITQTY' AND isOverride=1");
            if (!isoverride)
            {
                editLineTransaction();
            }
            else
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    editLineTransaction();
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
           
        }

        void Reading()
        {
            AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            authfrm.ShowDialog(this);
            if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            {
                Xread();
                AuthorizedConfirmationFrm.isconfirmedLogin = false;
                authfrm.Dispose();
            }
        }

        public void Xread()
        {
            try
            {

                String details = "";
                string filepath = "C:\\POSTransaction\\FinancialReport\\XRead\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\";
                details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

                details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
              
                string petsa = DateTime.Now.ToShortDateString();
                string oras = DateTime.Now.ToShortTimeString();
                string fulldate1 = petsa + ' ' + oras;
                DateTime dt = DateTime.Now;
                string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

                //double grosssales = 0.0;
                string transcode = lblTransactionIDCashier.Text;
                details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER : " + Login.Fullname) + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintCenterText("FINANCIAL REPORT (X)") + Environment.NewLine;
                details += HelperFunction.PrintLeftText(petsa) + Environment.NewLine;
                details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;

                details += HelperFunction.PrintLeftText("CASHIER : " + Login.Fullname) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Terminal #: " + Environment.MachineName) + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                double gross = POSTransaction.getTotalSales(transcode) + POSTransaction.getTotalCreditSales(transcode);
                details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", HelperFunction.convertToNumericFormat(gross)) + Environment.NewLine; //total sales
                details += HelperFunction.createAsteriskLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CASH :", HelperFunction.convertToNumericFormat(POSTransaction.getTotalSales(transcode))) + Environment.NewLine; //total sales
                details += HelperFunction.PrintLeftRigthText("CREDIT :", HelperFunction.convertToNumericFormat(POSTransaction.getTotalCreditSales(transcode))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("RETURN :", HelperFunction.convertToNumericFormat(txtTotalVoidTransaction.Text)) + Environment.NewLine; //total void
               // grosssales = POSTransaction.getTotalSales(transcode) + POSTransaction.getTotalCreditSales(transcode);
                details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("GROSS SALE ", HelperFunction.convertToNumericFormat(gross)) + Environment.NewLine; //total sales
                details += HelperFunction.createEqualLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
                details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

                details += HelperFunction.PrintLeftRigthText("BeginningCash: ", HelperFunction.convertToNumericFormat(POSTransaction.getBeginningCash(transcode))) + Environment.NewLine; //numitemsold
                
                details += HelperFunction.PrintLeftRigthText("Transaction #: ", lblTransactionIDCashier.Text) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", POSTransaction.getTotalSoldItems(transcode).ToString()) + Environment.NewLine; //numitemsold
                details += HelperFunction.PrintLeftRigthText("Transaction Count: ", POSTransaction.getTransactionCount(transcode).ToString()) + Environment.NewLine; //numtranscunt

                details += HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", POSTransaction.getTotalReturnedItems(transcode).ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(POSTransaction.getTotalReturnedTransactions(transcode))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", POSTransaction.getTotalCancelledItems(transcode).ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", HelperFunction.convertToNumericFormat(POSTransaction.getTotalCancelledTransactions(transcode))) + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("Tot ServiceFee: ", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of Deleted/Voids: ", POSTransaction.getTotalVoidItems(transcode).ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Deleted/Voids: ", HelperFunction.convertToNumericFormat(POSTransaction.getTotalVoidTransactions(transcode))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", POSTransaction.getNoOfDiscountItems(transcode).ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total Discounts: ", HelperFunction.convertToNumericFormat(POSTransaction.getTotalDiscount(transcode))) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("No. of 12% VAT: ", POSTransaction.getNoOfVATItems(transcode).ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total 12% VAT: ", HelperFunction.convertToNumericFormat(POSTransaction.getTotalTax(transcode))) + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                //details += HelperFunction.PrintLeftRigthText("Total Add On Amt: ", HelperFunction.convertToNumericFormat(txtTotalCharges.Text)) + Environment.NewLine + Environment.NewLine;


                details += HelperFunction.LastPagePaper();
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string transno = transcode + ".txt";
                string filetoprint = filepath + transno;
                string mark = filepath + transno;
                StreamWriter writer = new StreamWriter(filepath + transno);
                writer.Write(details);
                writer.Close();
                Printing printfile = new Printing();
                printfile.printTextFile(filetoprint);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
       
        void editLineTransaction()
        {
            
            try
            {
                //bool ok = HelperFunction.ConfirmDialog("Sigurado?", "Edit Transaction!");
                //if (ok)
                //{
                    if (Convert.ToDouble(lblTotalItems.Text) < 1)
                    {
                        XtraMessageBox.Show("Please Select an item to Edit");
                    }
                    else
                    {
                        sequenceNum = MydataGridView1[0, MydataGridView1.CurrentRow.Index].Value.ToString();
                        refno = txtOrderNo.Text.Trim();
                    transcode = lblTransactionIDInc.Text;//lblTransactionIDCashier.Text;
                        POS.POSEditLine edtfmr = new POS.POSEditLine();
                        edtfmr.txtprodname.Text = MydataGridView1[1, MydataGridView1.CurrentRow.Index].Value.ToString();
                        edtfmr.txtuprice.Text = MydataGridView1[2, MydataGridView1.CurrentRow.Index].Value.ToString();
                        edtfmr.txtqty1.Text = MydataGridView1[3, MydataGridView1.CurrentRow.Index].Value.ToString();
                        edtfmr.txttotal.Text = MydataGridView1[5, MydataGridView1.CurrentRow.Index].Value.ToString();

                    uprice = MydataGridView1[2, MydataGridView1.CurrentRow.Index].Value.ToString();
                    edtfmr.ShowDialog(this);

                        if (POS.POSEditLine.isdone == true)
                        {
                            display();
                            POS.POSEditLine.isdone = false;
                            edtfmr.Dispose();
                        //MydataGridView1.CurrentCell = MydataGridView1.Rows[MydataGridView1.Rows.Count - 1].Cells[1];
                          MydataGridView1.Rows[0].Selected = true;
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            POS.POSScreenMirror poscen = new POS.POSScreenMirror();
            poscen.Show();
        }
    }
}