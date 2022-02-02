using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSMainWithDashboard : Form
    {
        public static string reprintlabel = "REPRINT";
        //SerialPort serialPort1;
        private delegate void SetTextDeleg(string text);
        DataTable table;
        public static string _transcode, _refno, _pname, _uprice, _qtysold, _totamount, _discount, ispriceused = "mainprice";
        public static int _rowctr;
        string amount, totaldiscount;
        public static string totalamountstr = "0", strtext;
        public static string transcode, refno = "", prodcode, pname, uprice, qtysold, totamount, machinename, custname, custcode;
        public static double totalamount = 0.0, totalkilo = 0.0;
        public static DataGridView mygridview;
        public static string sequenceNum = "";

        public static string vatablesale, vatexemptsale, vat;
        bool   isusedsearchform = false, isDataInserted = false;
        string constring = "";
        public static string seniorcontrolno = "", seniorname = "";

        //FOR DISCOUNT
        public static string txtseniorcontrolno = "", txtseniorname = "", seniordiscountAmount = "", pwdIDNo = "", pwdName = "", pwdDiscountAmount = "", otherDiscountAmount = "", othersRemarks = "";
        public static bool isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false, isOnetimeDiscount = false;
        public static string cashierTransactionCode = "";
        public static bool iszeroratedsale = false;
        SqlCommand com;

        string selectedCategory = "", selectedProductName = "",productcategorycode="", selectedProductCode = "";
        List<Button> CategoryBtns = new List<Button>();
        List<Button> ItemBtns = new List<Button>();


        public POSMainWithDashboard()
        {
            InitializeComponent();
        }

        private void POSMainWithDashboard_Load(object sender, EventArgs e)
        {
            updateOR(); //generate OR Number
            updateTransactionNo(); //generate Transaction Number

            refreshView();
              

            if (POS.POSConnectionSettings.spValue == "sp_AddSalesInvoiceOnline")
            {
                constring = "Online";
            }
            else
            {
                constring = "Offline";
            }
          
            //lblTransactionIDInc.Text = Database.getSingleQuery("POSTransaction", "TransactionNo <> ''", "TransactionNo");
            lblTransactionIDCashier.Text = Database.getSingleQuery("SalesTransactionSummary", "UserID='" + Login.isglobalUserID + "' AND BranchCode='" + Login.assignedBranch + "' AND isOpen='1' and DateOpen='" + DateTime.Now.ToShortDateString() + "' ", "CashierTransNo");
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
            if (Convert.ToBoolean(isuseddisplaypool) == true)
            {
                chckdisplaypool.Checked = true;
            }
            else
            {
                chckdisplaypool.Checked = false;
            }

            checkCOMPort();
            timer1.Start();
            displayProductCategory();
        }


        void displayProductCategory()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "SELECT * FROM ProductCategory";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            flowLayoutPanel1.Controls.Clear();
            while (reader.Read())
            {
                Button btn = new Button();
                btn.Text = reader.GetValue(1).ToString();
              
                //productcategorycode = reader.GetValue(0).ToString();
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.BackColor = Color.MediumSeaGreen;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Width = 102;
                btn.Height = 40;
                CategoryBtns.Add(btn);
                flowLayoutPanel1.Controls.Add(btn);
                btn.Click += this.ButtonFoodCategory_Click;
                // buttonname = btn.Text;
            }
        }
        void displayProducts()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "SELECT ProductCategoryCode,ProductCode,Description FROM Products WHERE ProductCategoryCode='" + productcategorycode + "'";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            flowLayoutPanel2.Controls.Clear();
            while (reader.Read())
            {
                Button btn = new Button();
                btn.Text = reader.GetValue(2).ToString();
                
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.BackColor = Color.MediumSeaGreen;
                btn.FlatStyle = FlatStyle.Popup;
                btn.Width = 100;
                btn.Height = 40;
                ItemBtns.Add(btn);
                flowLayoutPanel2.Controls.Add(btn);
                btn.Click += this.ButtonFoodMenu_Click;
                // buttonname = btn.Text;

            }
        }
        private void ButtonFoodCategory_Click(System.Object sender, System.EventArgs e)
        {
            selectedCategory = (sender as Button).Text;
            productcategorycode = Database.getSingleQuery("ProductCategory", "Description='" + selectedCategory + "'", "ProductCategoryID");
            displayProducts();
        }
        private void ButtonFoodMenu_Click(System.Object sender, System.EventArgs e)
        {
            selectedProductName = (sender as Button).Text;
            insertData();
         
            display();
            
            if (isDataInserted)
                MydataGridView1.Rows[0].Selected = true;

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            cancelLineTransaction();
            //updateTransNo();
            updateTransactionNo();
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
                        AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                        authfrm.ShowDialog(this);
                        if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                        {

                            cancelTransaction();
                            //refreshView();
                            AuthorizedConfirmationFrm.isconfirmedLogin = false;
                            authfrm.Dispose();
                        }
                    }
                    //Database.ExecuteQuery("UPDATE BatchSalesDetails SET isCancelled='1',CancelledBy='EULZ' WHERE SequenceNumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString() + "'", "Successfully Cancelled");
                }
                display(); 
                //MydataGridView1.CurrentCell = MydataGridView1.Rows[MydataGridView1.Rows.Count - 1].Cells[1];
                if (MydataGridView1.RowCount > 0)
                {
                    MydataGridView1.Rows[0].Selected = true;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void voidTransactions()
        {
            try
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Void this Transaction?", "Void Transaction!");
                if (ok)
                {
                    //if (mygridview.SelectedRows == null)
                    //{
                    //    XtraMessageBox.Show("Please Select an item to Void");
                    //}
                    //else 
                    if (Convert.ToDouble(lblTotalItems.Text) < 1)
                    {
                        XtraMessageBox.Show("Please Select an item to Void");
                    }
                    else
                    {
                        AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                        authfrm.ShowDialog(this);
                        if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                        {
                            voidTransaction();
                            updateOR();
                            //Classes.Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                            refreshView();
                            //updateTransNo();
                            updateTransactionNo();
                            AuthorizedConfirmationFrm.isconfirmedLogin = false;
                            authfrm.Dispose();
                            //Classes.Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            voidTransactions();
        }

        private void btndineinbilling_Click(object sender, EventArgs e)
        {
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

        private void btnclosedtran_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(lblTotalItems.Text) > 0)
                {
                    XtraMessageBox.Show("Cannot Continue there is an item that need to be purchased.");
                }
                else
                {
                    displayItems();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void displayItems()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spr_POSCloseTransaction";
                com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmcashiertransno", lblTransactionIDCashier.Text);
                com.Parameters.AddWithValue("@parmmachineused", Environment.MachineName);
                com.Parameters.AddWithValue("@parmuserid", Login.isglobalUserID);

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
                com.Parameters.Add("@parmnoofregdisc", SqlDbType.Int).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmtotalofcancelleditem", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
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

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

                POSClosedTransaction pocls = new POSClosedTransaction();

                pocls.txtcashiertransno.Text = lblTransactionIDCashier.Text;
                pocls.txttransactionno.Text = lblTransactionIDInc.Text;

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
                pocls.txtnoofregdisc.Text = com.Parameters["@parmnoofregdisc"].Value.ToString();

                pocls.txtTotalCancelledTransaction.Text = com.Parameters["@parmtotalofcancelleditem"].Value.ToString();
                pocls.txtTotalVoidTransaction.Text = com.Parameters["@parmtotalofvoiditem"].Value.ToString();
                pocls.txtTotalDiscount.Text = com.Parameters["@parmtotalofdiscountitem"].Value.ToString();
                pocls.txtTotalTax.Text = com.Parameters["@parmtotalofvatitems"].Value.ToString();
                pocls.txtTotalReturnedTransaction.Text = com.Parameters["@parmtotalofreturneditems"].Value.ToString();


                pocls.txttotalofscdisc.Text = com.Parameters["@parmtotalofscdisc"].Value.ToString();
                pocls.txttotalofpwddisc.Text = com.Parameters["@parmtotalofpwddisc"].Value.ToString();
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

                Database.displayLocalGrid("select CONVERT(VARCHAR(10), DateOrder, 120) AS Date " +
                    ", DatePart(hh, DateOrder) as Hour " +
                    ", SUM(QtySold) as QtySold " +
                    ", SUM(TotalAmount) AS TotalAmount " +
                    ", COUNT(*) as TotalItems " +
                    "from BatchSalesDetails " +
                    "WHERE CAST(DateOrder as date)='" + DateTime.Now.ToShortDateString() + "' " +
                    "AND CashierTransNo='" + lblTransactionIDCashier.Text + "' " +
                    "GROUP BY CONVERT(VARCHAR(10), DateOrder, 120), DatePart(hh, DateOrder) ", pocls.MydataGridView1);

                pocls.ShowDialog(this);
                if (POSClosedTransaction.isdone == true)
                {
                    POSClosedTransaction.isdone = false;
                    pocls.Dispose();
                    this.Dispose();
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
                    displayItems();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
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

        private void btnaddqty_Click(object sender, EventArgs e)
        {
            int cord = MydataGridView1.CurrentCellAddress.Y;
            string ID = MydataGridView1.Rows[cord].Cells["ID"].Value.ToString();
            Database.ExecuteQuery("UPDATE BatchSalesDetails Set QtySold=QtySold+1 WHERE SequenceNumber='" + ID + "' AND ReferenceNo='"+txtOrderNo.Text+"'");
            Database.ExecuteQuery("UPDATE BatchSalesDetails Set SubTotal=SellingPrice*QtySold,TotalAmount=SellingPrice*QtySold WHERE SequenceNumber='" + ID + "'  AND ReferenceNo='" + txtOrderNo.Text + "'");
            display();
            MydataGridView1.CurrentCell = MydataGridView1.Rows[cord].Cells[1];
        }

        private void POSMainWithDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(lblTotalItems.Text) > 0)
                {
                    XtraMessageBox.Show("Cannot Continue there is an item that need to be purchased.");
                }
                else
                {
                    displayItems();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btndeductqty_Click(object sender, EventArgs e)
        {
            int cord = MydataGridView1.CurrentCellAddress.Y;
            string ID = MydataGridView1.Rows[cord].Cells["ID"].Value.ToString();
            if(Convert.ToDouble(MydataGridView1.Rows[cord].Cells["Qty"].Value.ToString()) <= 1)
            {
                XtraMessageBox.Show("You cant deduct to negative value!...");
            }
            else
            {
                Database.ExecuteQuery("UPDATE BatchSalesDetails Set QtySold=QtySold-1 WHERE SequenceNumber='" + ID + "'  AND ReferenceNo='" + txtOrderNo.Text + "'");
                Database.ExecuteQuery("UPDATE BatchSalesDetails Set SubTotal=SellingPrice*QtySold,TotalAmount=SellingPrice*QtySold WHERE SequenceNumber='" + ID + "'  AND ReferenceNo='" + txtOrderNo.Text + "'");
                display();
                MydataGridView1.CurrentCell = MydataGridView1.Rows[cord].Cells[1];
            }
           
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            string data = serialPort1.ReadLine();
            //data = "";
            //data = serialPort1.ReadLine();
            //data = serialPort1.ReadExisting();
            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }
        private void si_DataReceived(string data)
        {
             
        }
        void display()
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
            for (int i = 0; i <= MydataGridView1.RowCount - 1; i++)
            {
                total += Convert.ToDouble(MydataGridView1.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(MydataGridView1.Rows[i].Cells["Discount"].Value);
            }
            return Math.Round(total, 2);
        }

        Double GetTotalOneTimeDiscount()
        {
            double discount = 0.0;
            discount = computeDiscount() + Convert.ToDouble(lblseniordiscount.Text) + Convert.ToDouble(lblpwddiscount.Text) + Convert.ToDouble(lblotherdiscount.Text);
            return Math.Round(discount, 2);
        }

        Double computeVATableSale()
        {
            double vatexemptsale = 0.0, finalvalue = 0.0;
            for (int i = 0; i <= MydataGridView1.RowCount - 1; i++)
            {
                if (MydataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "True")
                {
                    vatexemptsale += Convert.ToDouble(MydataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            finalvalue = vatexemptsale / 1.12;
            return Math.Round(finalvalue, 2);
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
            return Math.Round(vatexemptsale, 2);
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
            vat = Math.Round((vatexemptsale / 1.12) * 0.12, 2);
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
            return Math.Round(discount, 2);
        }

        Double computeTotalAmount()
        {
            double total = 0.0;
            for (int i = 0; i <= MydataGridView1.RowCount - 1; i++)
            {
                amount = MydataGridView1.Rows[i].Cells["Amount"].Value.ToString();
                total += Convert.ToDouble(amount);
            }
            return total;
        }

        private void insertData()   //ADD ITEM
        {
            string qty = "0"; 
            bool islinkedServer = Database.checkifExist("SELECT isnull(isLinkedServer,0) FROM POSType WHERE isLinkedServer=1");
            string linkedServerName = Database.getSingleQuery("POSType", "isLinkedServer is not null", "linkedServerName"); //linkedservername
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            { 
                string query = "";
                if (islinkedServer)
                {
                    string conLink = Database.getSingleResultSet("exec checkLinkedServer " + linkedServerName + " "); //check connection
                    if (conLink == "1")
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

                string productcode = Database.getSingleQuery("Products", " Description='"+ selectedProductName +"' " +
                    "AND BranchCode='"+Login.assignedBranch+"'", "ProductCode");
                SqlCommand com = new SqlCommand(query, con); 
                refno = txtOrderNo.Text.Trim();
                com.Parameters.AddWithValue("@parmorderno", refno); //ORDER NO
                com.Parameters.AddWithValue("@parmcustid", "");
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmtranscode", lblTransactionIDCashier.Text);//CASHIER TRANS NO
                com.Parameters.AddWithValue("@parmbarcode", "");
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
                string currentPrice = String.Format("\u20B1{0}", total1);
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
                else if (serialPort1.IsOpen && chckdisplaypool.Checked == true)
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

        string getORNumber()
        {
            string num = Classes.Utilities.readTextfile("C:\\POSTransaction\\ORSeries\\");
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
            txtOrderNo.Text = HelperFunction.sequencePadding1(refnumber.ToString());//refnumber.ToString();
        }
        void updateTransactionNo()
        {
            //int refnumber = IDGenerator.getPOSTransactionID();
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "'", "TransactionNo", 1);
            lblTransactionIDInc.Text = HelperFunction.sequencePadding(refnumber.ToString());//refnumber.ToString();
        }


        void refreshView()
        {
             
            //chckZeroRated.Checked = false;

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
            return Math.Round(discount, 2);
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
            return Math.Round(discount, 2);
        }

        bool haveOneTimeDiscount()
        {
            bool ok = false;
            ok = Database.checkifExist("SELECT TOP 1 OrderNo FROM SalesDiscount WHERE OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0");
            return ok;
        }

        void AddDiscounts()
        {
            refno = txtOrderNo.Text;
            double discountAmount = 0.0, cleanamount = 0.0, newtotalamount = 0.0;
            double totalpayment = 0.0;

            totalpayment = Convert.ToDouble(lblvatsale.Text) + Convert.ToDouble(lblvatexemptsale.Text);
            // discountAmount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblvat)* (0.05);
            discountAmount = (Convert.ToDouble(lblvatsale.Text) + Convert.ToDouble(lblvatexemptsale.Text)) * (0.05);// Convert.ToDouble(lblvat) * (0.05);
            cleanamount = Convert.ToDouble(lblTotalAmount.Text) - discountAmount;

            int countitems = Database.getCountData("BatchSalesDetails", "ReferenceNo='" + txtOrderNo.Text + "' " +
                "AND CashierTransNo='" + lblTransactionIDCashier.Text + "'" +
                "AND isCancelled=0 ", "SequenceNumber");

            AddDiscount adis = new AddDiscount();
            adis.txtorderno.Text = txtOrderNo.Text;
            adis.txtcashiertansno.Text = lblTransactionIDCashier.Text;
            adis.txttransactionno.Text = lblTransactionIDInc.Text;
            if (countitems < 1)
            {
                XtraMessageBox.Show("You Cant Add Discount no Items to be discounted");
                return;
            }
            else
            {
                string getDiscountedItems = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItems('" + Login.assignedBranch + "','" + txtOrderNo.Text + "')");
                string getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + txtOrderNo.Text + "',0.05)");
                string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + txtOrderNo.Text + "')");

                //SENIOR
                if (String.IsNullOrEmpty(getDiscountedItems))
                {
                    getDiscountedItems = "0";
                }
                adis.txtamnttobediscount.Text = getDiscountedItems; //totalpayment.ToString();//lblTotalAmount.Text;
                adis.txtpercentageamount.Text = "5";
                adis.txtvatadj.Text = getVatAdjustment;
                adis.txtvatexadj.Text = getVATExAdj;
                //adis.txtdiscountamount.Text = "0"; //discountAmount.ToString();
                //PWD
                if (String.IsNullOrEmpty(getDiscountedItems))
                {
                    getDiscountedItems = "0";
                }
                adis.txtpwdamount.Text = getDiscountedItems; // totalpayment.ToString();// lblTotalAmount.Text;
                adis.txtpwdpercent.Text = "5";
                adis.txtvatadj.Text = getVatAdjustment;
                adis.txtvatexadj.Text = getVATExAdj;
                //adis.txtpwddiscountamount.Text = "0";//discountAmount.ToString();

                adis.ShowDialog(this);
            }
            if (AddDiscount.isdone == true)
            {
                double totdiscount = 0.0;
                //totdiscount = Math.Round(Convert.ToDouble(lblTotalDiscount.Text) + getSalesDiscount(), 2);
                totdiscount = Math.Round(getPerItemDiscount() + getSalesDiscount(), 2);
                lblTotalDiscount.Text = totdiscount.ToString();//Convert.ToDouble(lblTotalDiscount.Text)+AddDiscount.discountamount;
                newtotalamount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblTotalDiscount.Text);
                if (AddDiscount.discounttype == "SENIOR")
                {
                    lblseniordiscount.Text = getSalesDiscount().ToString();
                }
                else if (AddDiscount.discounttype == "PWD")
                {
                    lblpwddiscount.Text = getSalesDiscount().ToString();
                }
                else if (AddDiscount.discounttype == "OTHERS")
                {
                    lblotherdiscount.Text = getSalesDiscount().ToString();
                }
                updateTransactionNo();
                adis.Dispose();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool checkifexists = Database.checkifExist("SELECT TOP 1 OrderNo FROM SalesDiscount WHERE OrderNo='" + refno + "' AND isErrorCorrect=0 ");
            if (checkifexists)
            {
                bool isExists = HelperFunction.ConfirmDialog("The System found out that you already Discount this Transaction.. Do you want to Override and make a new Discount of this Transaction?", "Confirm Transaction");
                if (isExists) //IF YES
                {
                    Database.ExecuteQuery("Update SalesDiscount SET isErrorCorrect=1 WHERE OrderNo='" + refno + "' AND isErrorCorrect=0");
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
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
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
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            POS.POSEncodeCustomerInfo asda = new POS.POSEncodeCustomerInfo();
            asda.ShowDialog(this);
            if (POS.POSEncodeCustomerInfo.isdone == true)
            {
                txtcustnamercpt.Text = POS.POSEncodeCustomerInfo.custname;
                txtcustaddressrcpt.Text = POS.POSEncodeCustomerInfo.custaddress;
                txtcusttinrcpt.Text = POS.POSEncodeCustomerInfo.custtin;
                txtcustbussstyle.Text = POS.POSEncodeCustomerInfo.custbusiness;
                POS.POSEncodeCustomerInfo.isdone = false;
                asda.Dispose();
            }
        }
        private void btnreprint_Click(object sender, EventArgs e)
        {
            _refno = txtOrderNo.Text;
            POS.POSReprintOR posrep = new POS.POSReprintOR();
            //posrep.txtcounter.Text = IDGenerator.getPOSReprintCounter().ToString();
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
        private void chckdisplaypool_CheckedChanged(object sender, EventArgs e)
        {
            if (chckdisplaypool.Checked == true)
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
            Database.ExecuteQuery("UPDATE POSType SET DisplayPoolPort='" + txtcomport.Text + "'");
            XtraMessageBox.Show("Display Pool Port Successfully Updated");
        }
        private void btnpayment_Click(object sender, EventArgs e)
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
            //if (!serialPort1.IsOpen)
            //{
            //    serialPort1.Open();
            //    serialPort1.Write(Convert.ToString((char)12));
            //    serialPort1.WriteLine("Total: " + totalamount);
            //}

            paymentTransaction();
        }
        void paymentTransaction()
        {
            try
            {
                _transcode = lblTransactionIDCashier.Text;
                mygridview = MydataGridView1; //mygridview is a static variable declare inside the class, while MyDataGridView1 is a dataGridViewForm and set modifier to public
                //custname = txtcustname.Text; //Customer Name
                //custcode = txtcustid.Text; //Customer Code
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
                    if (chckZeroRated.Checked == true)
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
                        else if (DiscountType == "OTHERS")
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
                    double totaldiscounts = 0.0, totaldue = 0.0, netdue = 0.0, vatadj = 0.0, subtotal = 0.0;
                    //totaldiscounts = totaldiscount + Convert.ToDouble(lblperitemdiscountamount.Text); //ONE TIME DISCOUNT AMOUNT + TOTAL OF PER ITEM DISCOUNT
                    //totaldiscounts = getSalesDiscount() + Convert.ToDouble(lblperitemdiscountamount.Text); //ONE TIME DISCOUNT AMOUNT + TOTAL OF PER ITEM DISCOUNT
                    totaldiscounts = getSalesDiscount(); //ONE TIME DISCOUNT AMOUNT  SC,PWD,OTHERS
                    posconfirm.txtdiscount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS
                    posconfirm.txttotaldiscountamount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS

                    vatadj = getVatAdjustment();

                    subtotal = computeTotalAmount();
                    totaldue = Convert.ToDouble(lblTotalAmount.Text) - totaldiscounts; //lbltotal is already deducted by per item discount
                    netdue = totaldue - vatadj; //TOTAL DUE - ONE TIME DISCOUNT
                    //netdue = totaldue - totaldiscount; //TOTAL DUE - ONE TIME DISCOUNT

                    posconfirm.txtamountpayable.Text = netdue.ToString();
                    posconfirm.txtamountpayableb4onetimediscount.Text = subtotal.ToString();// totaldue.ToString(); //TOTAL DUE
                    posconfirm.txtorigamountpayable.Text = computeTotalAmountWithDiscount().ToString();
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //double overalltotaldiscount = 0.0;
                    //overalltotaldiscount = totaldiscount;// + Convert.ToDouble(lblperitemdiscountamount.Text);
                    //posconfirm.txtdiscount.Text = overalltotaldiscount.ToString();//DISCOUNT FIELD
                    //posconfirm.txttotaldiscountamount.Text = overalltotaldiscount.ToString();
                    //amountpayable = Convert.ToDouble(lblTotalAmount.Text) - overalltotaldiscount;
                    //posconfirm.txtamountpayableb4onetimediscount.Text = lblTotalAmount.Text; //TOTAL DUE
                    //posconfirm.txtamountpayable.Text = amountpayable.ToString();//lblTotalAmount.Text; //NET DUE NAH
                    //posconfirm.txtorigamountpayable.Text = computeTotalAmountWithDiscount().ToString();//lblTotalAmount.Text;

                    posconfirm.lblvatsale.Text = lblvatsale.Text;
                    posconfirm.lblvatexempt.Text = lblvatexemptsale.Text;
                    posconfirm.lblvatinput.Text = lblvat.Text;
                    posconfirm.btncaller.Text = "RETAILWITHDASHBOARD";
                    posconfirm.ShowDialog(this);
                    if (POS.POSConfirmPayment.transactiondone == true)
                    {
                        //Classes.Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                        //refreshView();
                      
                        if (txtcomport.Text == "")
                        {
                            XtraMessageBox.Show("Please Select COM-PORT");
                        }
                        else if (serialPort1.IsOpen && chckdisplaypool.Checked == true)
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
                string body = "WELCOME TO ENZO" + Environment.NewLine;
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
