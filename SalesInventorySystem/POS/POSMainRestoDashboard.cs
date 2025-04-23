using DevExpress.XtraEditors;
using SalesInventorySystem.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSMainRestoDashboard : Form
    {
        public static string seniorcontrolno = "", seniorname = "";
        string totalamountstr = "";
        private delegate void SetTextDeleg(string text);
        string selectedCategory = "", selectedProductName = "", productcategorycode = "";
        List<Button> CategoryBtns = new List<Button>();
        List<Button> ItemBtns = new List<Button>();
        DataTable table = new DataTable();

        public static string sequenceNum="";
        public static string refno="";
        public static string transcode="";

        //bool isDataInserted = false;
        string amount = "";
        public static DataGridView mygridview;
        SqlCommand com;
        public static string cashierTransactionCode = "";
        public static string vatablesale, vatexemptsale, vat;
        bool iszeroratedsale = false;
        public static string uprice;
        public static string userid = "";
        public POSMainRestoDashboard()
        {
            InitializeComponent();
        }

        void updateOR()
        {
            int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
            txtOrderNo.Text = HelperFunction.sequencePadding1(refnumber.ToString(), 18);//refnumber.ToString();

        }
        void updateTransactionNo()
        {
            //int refnumber = IDGenerator.getPOSTransactionID();
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "'", "TransactionNo", 1);
            lblTransactionIDInc.Text = HelperFunction.sequencePadding1(refnumber.ToString(), 10);//refnumber.ToString();
        }

        private void POSMainRestoDashboard_Load(object sender, EventArgs e)
        {
            //lblTransactionID.Text = Database.getSingleQuery("SalesTransactionSummary", "UserID='" + Login.isglobalUserID + "' AND BranchCode='" + Login.assignedBranch + "' AND isOpen='1' and DateOpen='" + DateTime.Now.ToShortDateString() + "' ", "AccountCode");
            lblTransactionIDCashier.Text = Database.getSingleQuery("SalesTransactionSummary", "UserID='" + Login.isglobalUserID + "' AND BranchCode='" + Login.assignedBranch + "' AND isOpen='1' and DateOpen='" + DateTime.Now.ToShortDateString() + "' ", "CashierTransNo");

            updateOR(); //generate OR Number
            updateTransactionNo(); //generate Transaction Number

            panelroomnum.Visible = false;
            displayCategory();
            refreshView();
            lbltableno.Text = "";

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F5) //PAYMENT
            {
                btnpayment.PerformClick();
            }
            else if (keyData == Keys.F1) //PAYMENT
            {
                if (MydataGridView1.RowCount > 0)
                {
                    XtraMessageBox.Show("The system detects that there are items need to be cancel first.");
                }
                else
                {
                    raddinein.Checked = true;
                }
            }
            else if (keyData == Keys.F2) //PAYMENT
            {
                //if (MydataGridView1.RowCount > 0)
                //{
                //    XtraMessageBox.Show("The system detects that there are items need to be cancel first.");
                //}
                //else
                //{ radtkeout.Checked = true; }
                btnedit.PerformClick();

            }
            else if (keyData == Keys.F3) //PAYMENT
            {
                if (MydataGridView1.RowCount > 0)
                {
                    XtraMessageBox.Show("The system detects that there are items need to be cancel first.");
                }
                else
                {
                    radcharge.Checked = true;

                }
            }
            else if (keyData == Keys.Down) //FOCUS TO GRID VIEW
            {
                MydataGridView1.Focus();
            }
            return functionReturnValue;
        }
        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtcomport.Items.AddRange(ports);
        }

        void refreshView()
        {
            // int refnumber = IDGenerator.getOrderNumberRestaurant();// IDGenerator.getOrderNumber();
            //int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1);//getOrderNumber();
            //txtOrderNo.Text = refnumber.ToString();
            lblTotalAmount.Text = "0";
            lblTotalItems.Text = "0";
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

        void display(string tableName, string id)
        {
            Database.displayLocalGrid("SELECT SequenceNumber AS ID" +
                ",Description AS Particulars" +
                ",FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice" +
                ",QtySold AS Qty" +
                ",FORMAT(DiscountTotal,'N', 'en-us') AS Discount" +
                ",FORMAT(TotalAmount,'N', 'en-us') AS Amount" +
                ",isVat FROM " + tableName + " " +
                "WHERE ReferenceNo='" + id + "' " +
                "AND BranchCode='"+Login.assignedBranch+"' " +
                "AND MachineUsed='"+Environment.MachineName.ToString()+"' " +
                "AND isVoid='0' " +
                "AND isCancelled='0' " +
                "and isHold='0' ORDER BY SequenceNumber DESC", MydataGridView1, Database.getConnection());
            //if (raddinein.Checked == true)
            //{
            //    Database.displayLocalGrid("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat FROM " + tableName + " WHERE ReferenceNo='" + id + "' AND isVoid='0' AND isCancelled='0' and isHold='0' ", MydataGridView1,Database.getCustomizeConnection());
            //}
            //else
            //{
            //    Database.displayLocalGrid("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat FROM " + tableName + " WHERE ReferenceNo='" + id + "' AND isVoid='0' AND isCancelled='0' and isHold='0' ", MydataGridView1);
            //}
            lblTotalItems.Text = MydataGridView1.RowCount.ToString();
            lblTotalAmount.Text = HelperFunction.numericFormat(computeTotalAmount());//.ToString();
            lblvat.Text = HelperFunction.numericFormat(computeVAT());//.ToString();
            lblvatexemptsale.Text = HelperFunction.numericFormat(computeVATExemptSale());//.ToString();
            lblvatsale.Text = HelperFunction.numericFormat(computeVATableSale());//.ToString();
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

        void displayCategory()
        {
            //SqlConnection con = Database.getCustomizeConnection();
            SqlConnection con = Database.getConnection();
            con.Open();
            //string query = "SELECT * FROM FoodCategory";
            string query = "SELECT * FROM ProductCategory WHERE ProductCategoryID IN (SELECT ProductCategoryCode FROM Products WHERE ProdType IN ('1','3'))";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            flowLayoutPanel1.Controls.Clear();
            while (reader.Read())
            {
                Button btn = new Button();
                btn.Text = reader.GetValue(1).ToString();
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
        void displayItems()
        {
            //SqlConnection con = Database.getCustomizeConnection();
            SqlConnection con = Database.getConnection();
            con.Open();
            //string query = "SELECT * FROM FoodMenu WHERE MenuCategory='"+ selectedCategory + "'";
            string query = "SELECT * FROM Products WHERE BranchCode='"+Login.assignedBranch+"' AND ProductCategoryCode='" + selectedCategory + "'";
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
            //selectedCategory = (sender as Button).Text;
            //displayItems();
            selectedCategory = (sender as Button).Text;
            productcategorycode = Database.getSingleQuery("ProductCategory", "Description='" + selectedCategory + "'", "ProductCategoryID");
            displayProducts();
        }

        void displayProducts()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "SELECT ProductCategoryCode,ProductCode,Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' AND ProductCategoryCode='" + productcategorycode + "'";
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

        private void ButtonFoodMenu_Click(System.Object sender, System.EventArgs e)
        {
            selectedProductName = (sender as Button).Text;
            if (raddinein.Checked == true)
            {
                if (String.IsNullOrEmpty(lbltableno.Text))
                {
                    XtraMessageBox.Show("Please Select Table # First!");
                    return;
                }
                else
                {
                    addOrder();
                }
            }
            else if(radtkeout.Checked==true)
            { addOrder(); }
            else
            {
                XtraMessageBox.Show("Please Select DINE-IN OR TAKEOUT");
                return;
            }
        }

        private void calcEdit1_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            e.DisplayText = e.DisplayText.Replace(",", "");
        }

        private void button7_Click(object sender, EventArgs e)
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
            }
            paymentTransactionRestoEulz();
        }
        double getSalesDiscount()
        {
            double discount = 0.0;
            discount = Database.getTotalSummation2("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0", "DiscountAmount");
            return Math.Round(discount, 2);
        }
        void paymentTransactionRestoEulz()
        {
            try
            {
                mygridview = MydataGridView1; //mygridview is a static variable declare inside the class, while MyDataGridView1 is a dataGridViewForm and set modifier to public

                if (Convert.ToDouble(lblTotalItems.Text) < 1)
                {
                    XtraMessageBox.Show("No Transaction Entry");
                }
                else
                {
                    POS.POSConfirmPaymentResto posconfirm = new POS.POSConfirmPaymentResto();
                    double totaldiscounts = 0.0, totaldue = 0.0, netdue = 0.0;
                    totaldiscounts = getSalesDiscount(); //ONE TIME DISCOUNT AMOUNT + TOTAL OF PER ITEM DISCOUNT
                    posconfirm.txtdiscount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS
                    posconfirm.txttotaldiscountamount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS

                    totaldue = Convert.ToDouble(lblTotalAmount.Text); //lbltotal is already deducted by per item discount
                    netdue = totaldue - totaldiscounts; //TOTAL DUE - ONE TIME DISCOUNT
                    posconfirm.txtamountpayable.Text = netdue.ToString();
                    posconfirm.txtamountpayableb4onetimediscount.Text = totaldue.ToString(); //TOTAL DUE


                    posconfirm.lblcashiertransno.Text = lblTransactionIDCashier.Text;
                    posconfirm.lblorderno.Text = txtOrderNo.Text;
                    posconfirm.lbltranscode.Text = lblTransactionIDCashier.Text;
                    posconfirm.txtdiscount.Text = txtdiscount.Text;
                    posconfirm.txtamountpayable.Text = lblTotalAmount.Text;
                    posconfirm.txtseniorcontrolno.Text = seniorcontrolno;
                    posconfirm.txtseniorname.Text = seniorname;
                    posconfirm.lblvatsale.Text = lblvatsale.Text;
                    posconfirm.lblvatexempt.Text = lblvatexemptsale.Text;
                    posconfirm.lblvatinput.Text = lblvat.Text;
                    posconfirm.btncaller.Text = "RESTAURANT";

                    posconfirm.lbltransno.Text = lblTransactionIDInc.Text;

                    posconfirm.ShowDialog(this);
                    if (POS.POSConfirmPaymentResto.transactiondone == true)
                    {
                        //if (txtcomport.Text == "")
                        //{
                        //    XtraMessageBox.Show("Please Select COM-PORT");
                        //}
                        //else if (serialPort1.IsOpen && chckdisplaypool.Checked == true)
                        //{
                        //    serialPort1.Write(Convert.ToString((char)12));
                        //    serialPort1.WriteLine("Tender: " + String.Format("{0:0.00}", Convert.ToDouble(posconfirm.txtamounttender.Text)));
                        //    serialPort1.WriteLine((char)13 + "Change: " + String.Format("{0:0.00}", Convert.ToDouble(posconfirm.txtamountchange.Text)));
                        //}

                        POS.POSHistoryCaption poshiscap = new POS.POSHistoryCaption();
                        poshiscap.txtamounttenderedcap.Text = posconfirm.txtamounttender.Text;
                        poshiscap.txtamountchangecap.Text = posconfirm.txtamountchange.Text;
                        poshiscap.ShowDialog(this);

                        POS.POSHistoryCaption.transactiondone = false;
                        refreshView();
                        updateTransactionNo();
                        updateOR();

                        POS.POSConfirmPayment.transactiondone = false;
                        posconfirm.Dispose();
                        txtdiscount.Text = "0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void paymentTransaction()
        {
            try
            {
                mygridview = MydataGridView1; //mygridview is a static variable declare inside the class, while MyDataGridView1 is a dataGridViewForm and set modifier to public

                if (Convert.ToDouble(lblTotalItems.Text) < 1)
                {
                    XtraMessageBox.Show("No Transaction Entry");
                }
                else
                {
                    POS.POSConfirmPayment posconfirm = new POS.POSConfirmPayment();
                    double totaldiscounts = 0.0, totaldue = 0.0, netdue = 0.0;
                    totaldiscounts = getSalesDiscount(); //ONE TIME DISCOUNT AMOUNT + TOTAL OF PER ITEM DISCOUNT
                    posconfirm.txtdiscount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS
                    posconfirm.txttotaldiscountamount.Text = totaldiscounts.ToString();//DISCOUNT FIELD - TOTAL OF ALL DISCOUNTS

                    totaldue = Convert.ToDouble(lblTotalAmount.Text); //lbltotal is already deducted by per item discount
                    netdue = totaldue - totaldiscounts; //TOTAL DUE - ONE TIME DISCOUNT
                    posconfirm.txtamountpayable.Text = netdue.ToString();
                    posconfirm.txtamountpayableb4onetimediscount.Text = totaldue.ToString(); //TOTAL DUE


                    posconfirm.lblorderno.Text = txtOrderNo.Text;
                    posconfirm.lbltranscode.Text = lblTransactionIDCashier.Text;
                    posconfirm.txtdiscount.Text = txtdiscount.Text;
                    posconfirm.txtamountpayable.Text = lblTotalAmount.Text;
                    posconfirm.txtseniorcontrolno.Text = seniorcontrolno;
                    posconfirm.txtseniorname.Text = seniorname;
                    posconfirm.lblvatsale.Text = lblvatsale.Text;
                    posconfirm.lblvatexempt.Text = lblvatexemptsale.Text;
                    posconfirm.lblvatinput.Text = lblvat.Text;
                    posconfirm.btncaller.Text = "RESTAURANT";
                    posconfirm.ShowDialog(this);
                    if (POS.POSConfirmPayment.transactiondone == true)
                    {
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
                        refreshView();

                        POS.POSConfirmPayment.transactiondone = false;
                        posconfirm.Dispose();
                        txtdiscount.Text = "0";
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (raddinein.Checked == true)
            {
                POSRestoTables tbles = new POSRestoTables();
                tbles.ShowDialog(this);
                if (POSRestoTables.isdone == true)
                {
                    lbltableno.Text = POSRestoTables.buttonname;
                    if (POSRestoTables.existingor != "" || POSRestoTables.status == "AddOrder")
                    {
                        txtOrderNo.Text = POSRestoTables.existingor;
                        display("BatchSalesDetails", POSRestoTables.existingor);
                    }
                    lblwaitername.Text = POSRestoTables.waiterid;
                }
            }


        }

        void addOrder()
        {
            try
            {
                if (raddinein.Checked == true)
                {
                    addDineInOrder();
                    display("BatchSalesDetails", txtOrderNo.Text);
                }
                else
                {
                    insertData();
                    display("BatchSalesDetails", txtOrderNo.Text);
                }
                //insertData();
                //display("BatchSalesDetails", txtOrderNo.Text);
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void addDineInOrder()   //ADD ITEM
        {
            //string productcode = Database.getSingleQuery("FoodMenu", " FoodMenu='" + selectedProductName + "'", "FoodCode", Database.getCustomizeConnection());
            string productcode = Database.getSingleQuery("Products", " Description='" + selectedProductName + "'", "ProductCode");
            //SqlConnection con = Database.getConnection();
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_AddSalesInvoiceResto";
                SqlCommand com = new SqlCommand(query, con);
                //refno = textEdit3.Text.Trim();

                refno = txtOrderNo.Text.Trim();
                com.Parameters.AddWithValue("@parmorderno", refno);
                com.Parameters.AddWithValue("@parmcustid", "");
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmtranscode", lblTransactionIDCashier.Text);
                com.Parameters.AddWithValue("@parmbarcode", "");
                com.Parameters.AddWithValue("@parmprodcode", productcode);
                com.Parameters.AddWithValue("@parmqty", 1);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmdiscountamount", "0");
                com.Parameters.AddWithValue("@parmissellingprice", "0");
                com.Parameters.AddWithValue("@parmisusedform", "1");
                com.Parameters.AddWithValue("@parmvatsale", lblvatsale.Text);
                com.Parameters.AddWithValue("@parmvatexemptsale", lblvatexemptsale.Text);
                com.Parameters.AddWithValue("@parmvat", lblvat.Text);
                com.Parameters.AddWithValue("@parmispriceused", "mainprice");
                com.Parameters.AddWithValue("@parmtableno", lbltableno.Text);
                com.Parameters.AddWithValue("@parmoption", "DINE-IN");
                com.Parameters.AddWithValue("@parmroomnum", "");
                com.Parameters.AddWithValue("@parmbookingno", "");


                com.Parameters.Add("@parmdesc1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmqty2", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmsel1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotal1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                //isDataInserted = true;

            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            //isusedbarcode = false;
            con.Close();

        }

        private void insertData()   //ADD ITEM
        {
            string option = "", bookino = "", roomno = "";
            if (raddinein.Checked == true)
            {
                option = "DINE-IN";
                bookino = "000";
                roomno = "000";
            }
            else if (radcharge.Checked == true)
            {
                if (String.IsNullOrEmpty(searchLookUpEdit1.Text))
                {
                    XtraMessageBox.Show("Please Select Room Number..");
                    return;
                }
                option = "CHARGE";
                bookino = Database.getSingleQuery("CheckInGuest", "RoomNumber='" + searchLookUpEdit1.Text + "' and isDone=0", "BookingNo", Database.getCustomizeConnection());
                roomno = searchLookUpEdit1.Text;
            }
            if (radtkeout.Checked == true)
            {
                option = "TAKEOUT";
                bookino = "000";
                roomno = "000";
                //bookino = "0000";
            }
            string productcode = Database.getSingleQuery("Products", " Description='" + selectedProductName + "'", "ProductCode");
            //string productcode = Database.getSingleQuery("FoodMenu", " FoodMenu='" + selectedProductName + "'", "FoodCode", Database.getConnection());
            SqlConnection con = Database.getConnection();//Database.getCustomizeConnection();
            con.Open();
            try
            {
                //string query = "sp_AddRestaurantOrder";
                string query = "sp_AddSalesInvoiceResto";
                //string query = "sp_AddSalesInvoice";
                SqlCommand com = new SqlCommand(query, con);
                //refno = textEdit3.Text.Trim();
                refno = txtOrderNo.Text.Trim();
                com.Parameters.AddWithValue("@parmorderno", refno);
                com.Parameters.AddWithValue("@parmcustid", "");
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmtranscode", lblTransactionIDCashier.Text);
                com.Parameters.AddWithValue("@parmbarcode", "");
                com.Parameters.AddWithValue("@parmprodcode", productcode);
                com.Parameters.AddWithValue("@parmqty", 1);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmdiscountamount", "0");
                com.Parameters.AddWithValue("@parmissellingprice", "0");
                com.Parameters.AddWithValue("@parmisusedform", "1");
                com.Parameters.AddWithValue("@parmvatsale", lblvatsale.Text);
                com.Parameters.AddWithValue("@parmvatexemptsale", lblvatexemptsale.Text);
                com.Parameters.AddWithValue("@parmvat", lblvat.Text);
                com.Parameters.AddWithValue("@parmispriceused", "mainprice");
                com.Parameters.AddWithValue("@parmtableno", lbltableno.Text);
                com.Parameters.AddWithValue("@parmoption", option);
                com.Parameters.AddWithValue("@parmroomnum", roomno);
                com.Parameters.AddWithValue("@parmbookingno", bookino);
                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());

                com.Parameters.Add("@parmdesc1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmqty2", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmsel1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotal1", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                //  isDataInserted = true;
                //string desc1 = com.Parameters["@parmdesc1"].Value.ToString();
                //string qty1 = com.Parameters["@parmqty2"].Value.ToString();
                //string sel1 = com.Parameters["@parmsel1"].Value.ToString();
                //string total1 = com.Parameters["@parmtotal1"].Value.ToString();
                //string currentPrice = String.Format("\u20B1{0}", total1);
                //string full = qty1 + " @ " + sel1 + " = " + total1;
                //int desclength = desc1.Length;
                //string description = "";
                //if (desclength > 20)
                //{
                //    description = desc1.Substring(0, 19);
                //}
                //else
                //{
                //    description = desc1;
                //}
                //if (txtcomport.Text == "")
                //{
                //    XtraMessageBox.Show("Please Select COM-PORT");
                //}
                //else if (serialPort1.IsOpen && chckdisplaypool.Checked == true)
                //{
                //    serialPort1.Write(Convert.ToString((char)12));
                //    serialPort1.WriteLine(description);
                //    serialPort1.WriteLine((char)13 + "Amount: " + total1);
                //}
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            //isusedbarcode = false;
            con.Close();

        }

        private void panelconfirmpayment_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int cord = MydataGridView1.CurrentCellAddress.Y;
            string ID = MydataGridView1.Rows[cord].Cells["ID"].Value.ToString();
            Database.ExecuteQuery("UPDATE BatchSalesDetails Set QtySold=QtySold+1 WHERE SequenceNumber='" + ID + "' and ReferenceNo='"+txtOrderNo.Text+"' and BranchCode='"+Login.assignedBranch+"' and MachineUsed='"+Environment.MachineName.ToString()+"' ");
            Database.ExecuteQuery("UPDATE BatchSalesDetails Set SubTotal=SellingPrice*QtySold,TotalAmount=SellingPrice*QtySold WHERE SequenceNumber='" + ID + "' and ReferenceNo='" + txtOrderNo.Text + "' and BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "' ");
            display("BatchSalesDetails", txtOrderNo.Text);
            MydataGridView1.CurrentCell = MydataGridView1.Rows[cord].Cells[1];
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int cord = MydataGridView1.CurrentCellAddress.Y;
            string ID = MydataGridView1.Rows[cord].Cells["ID"].Value.ToString();
            Database.ExecuteQuery("UPDATE BatchSalesDetails Set QtySold=QtySold-1 WHERE SequenceNumber='" + ID + "' and ReferenceNo='" + txtOrderNo.Text + "' and BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "' ");
            Database.ExecuteQuery("UPDATE BatchSalesDetails Set SubTotal=SellingPrice*QtySold,TotalAmount=SellingPrice*QtySold WHERE SequenceNumber='" + ID + "' and ReferenceNo='" + txtOrderNo.Text + "' and BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "' ");
            display("BatchSalesDetails", txtOrderNo.Text);
            MydataGridView1.CurrentCell = MydataGridView1.Rows[cord].Cells[1];
        }
        void Save()
        {
            try
            {
                mygridview = MydataGridView1;
                Printing printit = new Printing();
                //printit.printOrders(txtOrderNo.Text, lblwaitername.Text, lbltableno.Text, "MEZANINE", mygridview);
                printit.PrintOrderToFile(txtOrderNo.Text, lblwaitername.Text, lbltableno.Text, "MEZANINE", mygridview);
                //refreshView();
                display("BatchSalesDetails",txtOrderNo.Text);
                updateOR();
                refreshView();
                updateTransactionNo();

                //int refno = IDGenerator.getIDNumber("BatchSalesSummary", "OrderType<>''", "ReferenceNo", 10000);//getReferenceNumberRestaurant();
                //txtrefno.Text = refno.ToString();

            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (raddinein.Checked == true)
            {
                if (String.IsNullOrEmpty(lbltableno.Text))
                {
                    XtraMessageBox.Show("Please Select Table");
                    return;

                }
                else
                {
                    Save();
                    raddinein.Checked = false;
                    lbltableno.Text = "";
                    Printing printit = new Printing();
                    //printit.ReprintReceiptRestoOneLove(lblTransactionIDInc.Text, txtOrderNo.Text, "", "", "", "", "", "", "", "", "", MydataGridView1, false, "", "", "", "", "", "", "");
                    //printit.printReceiptRestoOneLove(lblTransactionIDInc.Text, txtOrderNo.Text, "", "", "", "", "", "", "", "", "", MydataGridView1, false, "", "", "", "", "", "", "");
                    printit.printReceiptRestoOneLove(lblTransactionIDInc.Text, txtOrderNo.Text, "", "0", "", "", "", "", "", MydataGridView1, false, "", "", "", "", "",false);

                    //txtOrderNo.Text = getORNumber();
                }
            }
            else if (radcharge.Checked == true)
            {
                Save();
                radcharge.Checked = false;
            }
            else
            {
                XtraMessageBox.Show("Save Button is use for Dine-In Transaction Only!");
                return;
            }


        }


        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

            if (radtkeout.Checked == true)
            {

                lbltableno.Text = "";
                btnsave.Visible = false;
                btnpayment.Visible = true;
                btnTAbles.Visible = false;
                paneltable.Visible = false;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (raddinein.Checked == true)
            {
                
                //txtOrderNo.Text = refno.ToString();
                // txtrefno.Text = IDGenerator.getDineInReferenceNumberRestaurant().ToString();
              
                lbltableno.Text = "";
                btnsave.Visible = true;
                btnpayment.Visible = false;
                btnTAbles.Visible = true;
                paneltable.Visible = true;
            }
            else
            {
                btnTAbles.Visible = false;
                paneltable.Visible = false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }
       

        private void button8_Click(object sender, EventArgs e)
        {
            cancelLineTransaction();
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
                        cancelTransaction();
                        //AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                        //authfrm.ShowDialog(this);
                        //if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                        //{
                        //    cancelTransaction();
                        //    //refreshView();
                        //    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                        //    authfrm.Dispose();
                        //}
                    }
                    //Database.ExecuteQuery("UPDATE BatchSalesDetails SET isCancelled='1',CancelledBy='EULZ' WHERE SequenceNumber='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString() + "'", "Successfully Cancelled");
                }
                display("BatchSalesDetails", txtOrderNo.Text);
                MydataGridView1.CurrentCell = MydataGridView1.Rows[MydataGridView1.Rows.Count - 1].Cells[1];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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
                com.Parameters.AddWithValue("@parmuser", AuthorizedConfirmationFrm.isglobalUserID);
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
        private void radcharge_CheckedChanged(object sender, EventArgs e)
        {
            if (radcharge.Checked == true)
            {
                panelroomnum.Visible = true;
                //int refnumber = IDGenerator.getOrderNumber();
                //txtOrderNo.Text = refnumber.ToString();
                lbltableno.Text = "";
                Database.displaySearchlookupEdit("Select RoomNumber,GuestName FROM CheckinGuest WHERE isDone='0'", searchLookUpEdit1, "RoomNumber", "RoomNumber", Database.getCustomizeConnection());
                searchLookUpEdit1.Focus();
                btnsave.Visible = true;
                btnpayment.Visible = false;
                btnTAbles.Visible = false;
                paneltable.Visible = false;
            }
            else
            {
                panelroomnum.Visible = false;
            }
        }

        private void txtcomport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = txtcomport.Text.Trim();
                serialPort1.Open();
                serialPort1.DataReceived += SerialPort1_DataReceived; //new SerialDataReceivedEventHandler()
                serialPort1.ReadTimeout = 500;
                serialPort1.WriteTimeout = 500;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = serialPort1.ReadLine();

            this.BeginInvoke(new SetTextDeleg(si_DataReceived), new object[] { data });
        }

        private void btndineinbilling_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSRestoDineInBilling))
                {
                    form.Activate();
                    return;
                }
            }
            POSRestoDineInBilling pcusatfsmr = new POSRestoDineInBilling();
            pcusatfsmr.Show();

            //POSRestoFinalBilling billing = new POSRestoFinalBilling();
            //billing.Show();
            //if (POSRestoFinalBilling.isClosed == true)
            //{
            //    raddinein.Checked = true;
            //    POSRestoFinalBilling.isClosed = false;
            //    billing.Dispose();
            //}
            //POSRestoDineInBilling billing = new POSRestoDineInBilling();
            //billing.Show();
            //if (POSRestoDineInBilling.isClosed == true)
            //{
            //    raddinein.Checked = true;
            //    POSRestoDineInBilling.isClosed = false;
            //    billing.Dispose();
            //}
        }

        private void btnvoid_Click(object sender, EventArgs e)
        {
            //AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            //authfrm.ShowDialog(this);
            //if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            //{
            //    POS.POSErrrorCorrect poserror = new POS.POSErrrorCorrect();
            //    poserror.ShowDialog(this);
            //    AuthorizedConfirmationFrm.isconfirmedLogin = false;
            //    authfrm.Dispose();
            //}
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



        void displayItems(string userid, string cashiertransno, string machinename)
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

                com.Parameters.Add("@parmtotalchargetoaccountsales", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmtotalchargetoaccountsales"].Precision = 12;
                com.Parameters["@parmtotalchargetoaccountsales"].Scale = 2;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

                POSClosedTransaction pocls = new POSClosedTransaction();

                pocls.txtcashiertransno.Text = cashierTransactionCode;// lblTransactionIDCashier.Text;
                pocls.txttransactionno.Text = com.Parameters["@parmtransno"].Value.ToString(); //lblTransactionIDInc.Text;

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
                pocls.txtchargesales.Text = com.Parameters["@parmtotalchargetoaccountsales"].Value.ToString();



                Database.displayLocalGrid("select CONVERT(VARCHAR(10), DateOrder, 120) AS Date " +
                    ", DatePart(hh, DateOrder) as Hour " +
                    ", SUM(QtySold) as QtySold " +
                    ", SUM(TotalAmount) AS TotalAmount " +
                    ", COUNT(*) as TotalItems " +
                    "from BatchSalesDetails " +
                    "WHERE CAST(DateOrder as date)='" + DateTime.Now.ToShortDateString() + "' " +
                    // "AND CashierTransNo='" + lblTransactionIDCashier.Text + "' " +
                    "AND CashierTransNo='" + lblTransactionIDCashier.Text + "' " +
                    "GROUP BY CONVERT(VARCHAR(10), DateOrder, 120), DatePart(hh, DateOrder) ", pocls.MydataGridView1);

                pocls.ShowDialog(this);
                if (POSClosedTransaction.isdone == true)
                {
                    POSClosedTransaction.isdone = false;
                    pocls.Dispose();
                    if (lblTransactionIDCashier.Text == cashierTransactionCode)
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
                    //refno = textEdit3.Text.Trim();
                    refno = txtOrderNo.Text.Trim();
                    POSClosedTransaction pocls = new POSClosedTransaction();

                    //pocls.txttransno.Text = IDGenerator.getReferenceNumber(); //TRANSACTION REFERENCE #

                    pocls.txttransactiondate.Text = POSTransaction.getBeginningDate(lblTransactionIDCashier.Text); //TRANSACTION DATE

                    pocls.txtBeginningCash.Text = HelperFunction.numericFormat(POSTransaction.getBeginningCash(lblTransactionIDCashier.Text));//.ToString(); //NEXT TRANSACTION BEGINNING CASH

                    pocls.txtnoofsolditem.Text = POSTransaction.getTotalSoldItems(lblTransactionIDCashier.Text).ToString(); //NO OF SOLD ITEMS
                    pocls.txtnoofcancelleditem.Text = POSTransaction.getTotalCancelledItems(lblTransactionIDCashier.Text).ToString(); //NO OF CANCELLED ITEMS
                    pocls.txtnoofvoiditem.Text = POSTransaction.getTotalVoidItems(lblTransactionIDCashier.Text).ToString(); //NO OF VOID ITEMS
                    pocls.txtTotalCancelledTransaction.Text = HelperFunction.numericFormat(POSTransaction.getTotalCancelledTransactions(lblTransactionIDCashier.Text));//.ToString(); //TOTAL AMOUNT OF CANCELLED ITEMS
                    pocls.txtTotalVoidTransaction.Text = HelperFunction.numericFormat(POSTransaction.getTotalVoidTransactions(lblTransactionIDCashier.Text));//.ToString(); //TOTAL AMOUNT OF VOID ITEMS
                    pocls.txtnoofdiscount.Text = POSTransaction.getNoOfDiscountItems(lblTransactionIDCashier.Text).ToString(); //NO OF TOTAL DISCOUNT ITEM
                    pocls.txtTotalDiscount.Text = HelperFunction.numericFormat(POSTransaction.getTotalDiscount(lblTransactionIDCashier.Text));//,2).ToString(); //TOTAL AMOUNT OF DISCOUNT

                    pocls.txtnoofcharges.Text = POSTransaction.getNoOfChargeItems(lblTransactionIDCashier.Text).ToString(); ;//NO OF TOTAL CHARGES
                    pocls.txtTotalCharges.Text = HelperFunction.numericFormat(POSTransaction.getTotalCharge(lblTransactionIDCashier.Text));//.ToString(); ////TOTA AMOUNT OF CHARGES
                    pocls.txtnoofvat.Text = POSTransaction.getNoOfVATItems(lblTransactionIDCashier.Text).ToString(); ; //NO OF VAT CHARGES
                    pocls.txtTotalTax.Text = HelperFunction.numericFormat(POSTransaction.getTotalTax(lblTransactionIDCashier.Text));//,2).ToString(); //TOTAL AMOUNT OF TAX
                    pocls.txtTotalCashSales.Text = HelperFunction.numericFormat(POSTransaction.getTotalSales(lblTransactionIDCashier.Text));//,2).ToString(); //TOTAL SALES

                    //pocls.txtbeginninginvoice.Text = POSTransaction.getBeginningInvoice(lblTransactionID.Text).ToString(); //BEGINNING INVOICE
                    //pocls.txtbegorno.Text = POSTransaction.getBeginningORNo(lblTransactionID.Text).ToString(); //BEGINNING OR NO
                    pocls.txtendingsi.Text = POSTransaction.getLastOrNo(lblTransactionIDCashier.Text).ToString(); //LAST OR NO

                    pocls.txtvatablesales.Text = HelperFunction.numericFormat(POSTransaction.getVatableSales(lblTransactionIDCashier.Text));//.ToString();
                    pocls.txtvatexemptsale.Text = HelperFunction.numericFormat(POSTransaction.getVatExemptSales(lblTransactionIDCashier.Text));//.ToString();

                    pocls.txtvatamount.Text = HelperFunction.numericFormat(POSTransaction.getVatAmount(lblTransactionIDCashier.Text));//ToString();

                   

                    Database.displayLocalGrid("select CONVERT(VARCHAR(10), DateOrder, 120) AS Date , DatePart(hh, DateOrder) as Hour , SUM(QtySold) as QtySold , SUM(TotalAmount) AS TotalAmount , COUNT(*) as TotalItems from dbo.BatchSalesDetails WHERE CAST(DateOrder as date)='" + DateTime.Now.ToShortDateString() + "' AND TransactionCode='" + lblTransactionIDInc.Text + "' GROUP BY CONVERT(VARCHAR(10), DateOrder, 120), DatePart(hh, DateOrder) ", pocls.MydataGridView1);

                    pocls.ShowDialog(this);
                    if (POSClosedTransaction.isdone == true)
                    {
                        POSClosedTransaction.isdone = false;
                        pocls.Dispose();
                        this.Dispose();
                        // Classes.Utilities.writeTextfile("C:\\POSTransaction\\TranSeries\\counter.txt", transcode);
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
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
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
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
                        AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                        authfrm.ShowDialog(this);
                        if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                        {
                            voidTransaction();
                            refreshView();
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            //voidTransactions();
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

        void editLineTransaction()
        {

            try
            {
                if (Convert.ToDouble(lblTotalItems.Text) < 1)
                {
                    XtraMessageBox.Show("Please Select an item to Edit");
                }
                else
                {
                    sequenceNum = MydataGridView1[0, MydataGridView1.CurrentRow.Index].Value.ToString();
                    refno = txtOrderNo.Text.Trim();
                    transcode = lblTransactionIDCashier.Text;
                    POS.POSEditLine edtfmr = new POS.POSEditLine();
                    edtfmr.txtprodname.Text = MydataGridView1[1, MydataGridView1.CurrentRow.Index].Value.ToString();
                    edtfmr.txtuprice.Text = MydataGridView1[2, MydataGridView1.CurrentRow.Index].Value.ToString();
                    edtfmr.txtqty1.Text = MydataGridView1[3, MydataGridView1.CurrentRow.Index].Value.ToString();
                    edtfmr.txttotal.Text = MydataGridView1[5, MydataGridView1.CurrentRow.Index].Value.ToString();

                    uprice = MydataGridView1[2, MydataGridView1.CurrentRow.Index].Value.ToString();
                    edtfmr.ShowDialog(this);

                    if (POS.POSEditLine.isdone == true)
                    {
                        //display();
                        display("BatchSalesDetails", txtOrderNo.Text);
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

        private void si_DataReceived(string data)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            return finalvalue;
        }

        private void btnreprint_Click(object sender, EventArgs e)
        {
            Printing printit = new Printing();
            printit.printTextFile("C:\\POSTransaction\\LastTransaction\\LastTran.txt");
            XtraMessageBox.Show("Successfully Print");
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

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
            return vatexemptsale;
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
    }
}
