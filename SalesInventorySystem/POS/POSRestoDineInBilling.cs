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
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.POS
{
    public partial class POSRestoDineInBilling : DevExpress.XtraEditors.XtraForm
    {
        string invno = "";
        public static bool isClosed = false, ismerge = false,isdone=false;
        string amount = "", num = "", paymenttype = "";
        public static string disctype = "", discname = "", discidno = "", discamount = "", discremarks = "";
        public static bool transactiondone = false, isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false, isOnetimeDiscount = false;
        public static string refno = "", totamount = "";
        public static bool iszeroratedsale = false;
        public static DataGridView mygridview;

        public POSRestoDineInBilling()
        {
            InitializeComponent();
        }

        private void POSRestoDineInBilling_Load(object sender, EventArgs e)
        {
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "' AND MachineUsed='"+Environment.MachineName+"' ", "TransactionNo", 1);
            lblTransactionID.Text = HelperFunction.sequencePadding1(refnumber.ToString(), 10);//refnumber.ToString();
            //lblTransactionID.Text = Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' AND isOpen='1' ", "AccountCode");
            //int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
            //txtOrderNo.Text = refnumber.ToString();
            populateTables();
        }

        void populateTables()
        {
            //Database.displayCheckedListBoxItemsDevEx("SELECT TableNo FROM OrderSummary where isFloat=1 and Status='Pending' and OrderType='DINE-IN'", "TableNo", checkedListBoxControl1,Database.getCustomizeConnection());
            Database.displayCheckedListBoxItemsDevEx("SELECT TableNo FROM dbo.BatchSalesSummary where isFloat=1 and Status='Pending' and OrderType='DINE-IN' And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "' AND CAST(TransDate as date)='"+DateTime.Now.ToShortDateString()+"' ", "TableNo", checkedListBoxControl1);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        void display()
        {
            lblTotalItems.Text = dataGridView1.RowCount.ToString();
            lblTotalAmount.Text = HelperFunction.numericFormat(computeTotalAmount());//.ToString();
            lblvat.Text = HelperFunction.numericFormat(computeVAT());//.ToString();
            lblvatexemptsale.Text = HelperFunction.numericFormat(computeVATExemptSale());//.ToString();
            lblvatsale.Text = HelperFunction.numericFormat(computeVATableSale());//.ToString();

            //txtamountpayable.Text = lblTotalAmount.Text;
        }
        Double computeTotalAmount()
        {
            double total = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                amount = dataGridView1.Rows[i].Cells["Amount"].Value.ToString();
                total += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
            }
            return total;
        }
        Double computeVATableSale()
        {
            double vatexemptsale = 0.0, finalvalue = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "True")
                {
                    vatexemptsale += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            finalvalue = vatexemptsale / 1.12;
            return finalvalue;
        }

        Double computeVATExemptSale()
        {
            double vatexemptsale = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "False")
                {
                    vatexemptsale += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            return vatexemptsale;
        }

        Double computeVAT()
        {
            double vatexemptsale = 0.0;
            double vat = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells["isVat"].Value.ToString() == "True")
                // if (dataGridView1.GetRowCellValue(i,"isVat").ToString() == "True")

                {
                    vatexemptsale += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value.ToString());
                }
            }
            vat = Math.Round((vatexemptsale / 1.12) * 0.12, 2);
            return vat;
        }

        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            SqlDataAdapter adapter;
            DataTable table = new DataTable();

            int ctr = 0;

            if (checkedListBoxControl1.CheckedItemsCount == 0)
            {
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
            }
            //Database.ExecuteQuery("TRUNCATE TABLE aaplock1 ");

            foreach (Object item in checkedListBoxControl1.CheckedItems)
            {
                //Database.ExecuteQuery("INSERT INTO aaplock1 VALUES('" + item + "')");
                //Database.ExecuteLocalQuery("INSERT INTO aaplock1 VALUES('" + item + "')",Database.getCustomizeConnection());
                //string refno = Database.getSingleQuery("OrderSummary", "TableNo='" + item + "' and OrderType='DINE-IN' and isFloat=1 and Status='Pending'", "ReferenceNo",Database.getCustomizeConnection());
                //string refno = Database.getSingleQuery("BatchSalesSummary", "TableNo='" + item + "' and OrderType='DINE-IN' and isFloat=1 and Status='Pending'", "ReferenceNo");
                var rowz = Database.getMultipleQuery("Select a.ReferenceNo,a.CashierTransNo " +
                    "FROM dbo.BatchSalesSummary a " +
                    "WHERE a.TableNo='" + item + "' " +
                    "and a.OrderType='DINE-IN' " +
                    "and a.isFloat=1 " +
                    "and a.Status='Pending' " +
                    "And a.BranchCode='" + Login.assignedBranch + "' " +
                    "And CAST(a.TransDate as date)='" + DateTime.Now.ToShortDateString() + "' " +
                    "and a.MachineUsed='" + Environment.MachineName.ToString() + "'", "ReferenceNo,CashierTransNo");
                string refno = "", cashiertransno = "";

                refno = rowz["ReferenceNo"].ToString();
                cashiertransno = rowz["CashierTransNo"].ToString();


                ctr = ctr + 1;
                SqlConnection con = Database.getConnection(); //Database.getCustomizeConnection();
                if (ctr == 1) //if one table only
                {
                    ismerge = false;
                    txtOrderNo.Text = refno;
                    //lblTransactionID.Text = transno;
                    lblTransactionIDCashier.Text = cashiertransno;
                    con.Open();
                    string query = "SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,DiscountTotal as Discount,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat " +
                        "FROM BatchSalesDetails " +
                        "WHERE ReferenceNo='" + refno + "' " +
                        "And BranchCode='" + Login.assignedBranch + "' " +
                        "and MachineUsed='" + Environment.MachineName.ToString() + "' " +
                        "and isCancelled<>1 " +
                        "and isVoid<>1 " +
                        "and isErrorCorrect<>1  " +
                        "and Status='Pending' ";
                    SqlCommand com = new SqlCommand(query, con);
                    adapter = new SqlDataAdapter(com);
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    //string query1 = "SELECT Description AS Particulars ,SUM(QtySold) AS Qty,SUM(DiscountTotal) as Discount,FORMAT(SUM(TotalAmount),'N', 'en-us') AS Amount " +
                    //    "FROM BatchSalesDetails " +
                    //    "WHERE ReferenceNo='" + refno + "' " +
                    //    "And BranchCode='" + Login.assignedBranch + "' " +
                    //    "and MachineUsed='" + Environment.MachineName.ToString() + "' " +
                    //    "and isCancelled<>1 " +
                    //    "and isVoid<>1 " +
                    //    "and isErrorCorrect<>1 " +
                    //    "and Status='Pending' " +
                    //    "GROUP BY Description";
                    //SqlCommand com11 = new SqlCommand(query1, con);
                    //SqlDataAdapter adapter11 = new SqlDataAdapter(com11);
                    //DataTable table11 = new DataTable();
                    //adapter11.Fill(table11);
                    //dataGridView2.DataSource = table11;


                }
                //else //merge table only
                //{
                //    ismerge = true;
                //    int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
                //    txtOrderNo.Text = refnumber.ToString();
                //    con.Open();
                //    string query = "SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,DiscountTotal as Discount,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat FROM dbo.BatchSalesDetails WHERE ReferenceNo='" + refno + "' And BranchCode='"+Login.assignedBranch+"' and MachineUsed='"+Environment.MachineName.ToString()+ "' and isCancelled<>1 and isVoid<>1 and isErrorCorrect<>1  and Status='Pending' ";
                //    SqlCommand com = new SqlCommand(query, con);
                //    adapter = new SqlDataAdapter(com);
                //    adapter.Fill(table);
                //    dataGridView1.DataSource = table;

                //    string query1 = "SELECT Description AS Particulars ,SUM(QtySold) AS Qty,SUM(DiscountTotal) as Discount,FORMAT(SUM(TotalAmount),'N', 'en-us') AS Amount FROM dbo.BatchSalesDetails WHERE ReferenceNo='" + refno + "' And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "' and isCancelled<>1 and isVoid<>1 and isErrorCorrect<>1  and Status='Pending' GROUP BY Description";
                //    SqlCommand com11 = new SqlCommand(query1, con);
                //    SqlDataAdapter adapter11 = new SqlDataAdapter(com11);
                //    DataTable table11 = new DataTable();
                //    adapter11.Fill(table11);
                //    dataGridView2.DataSource = table11;
                //}


            }
            display();
        }

        private void POSRestoDineInBilling_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClosed = true;
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM dbo.POSFunctions WHERE FunctionName='DISCOUNT' AND isOverride=1");
            if (!isoverride)
            {
                bool checkifexists = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + refno + "' AND isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'");
                if (checkifexists)
                {
                    bool isExists = HelperFunction.ConfirmDialog("The System found out that you already Discount this Transaction.. Do you want to Override and make a new Discount of this Transaction?", "Confirm Transaction");
                    if (isExists) //IF YES
                    {
                        Database.ExecuteQuery("Update dbo.SalesDiscount SET isErrorCorrect=1 WHERE OrderNo='" + txtOrderNo.Text + "' AND isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'");
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
                    bool checkifexists = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + txtOrderNo.Text + "' AND isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'");
                    if (checkifexists)
                    {
                        bool isExists = HelperFunction.ConfirmDialog("The System found out that you already Discount this Transaction.. Do you want to Override and make a new Discount of this Transaction?", "Confirm Transaction");
                        if (isExists) //IF YES
                        {
                            Database.ExecuteQuery("Update dbo.SalesDiscount SET isErrorCorrect=1 WHERE OrderNo='" + txtOrderNo.Text + "' AND isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'");
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

        void AddDiscounts()
        {
            string postype = Database.getSingleQuery("SELECT TOP(1) PosType FROM dbo.POSType", "PosType");
            totamount = "0";
            refno = txtOrderNo.Text;
            totamount = lblTotalAmount.Text;
            double newtotalamount = 0.0;

            double totalpayment = 0.0;

            totalpayment = Convert.ToDouble(lblvatsale.Text) + Convert.ToDouble(lblvatexemptsale.Text); //NOT USED
                                                                                                        // discountAmount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblvat)* (0.0

            int countitems = Database.getCountData("BatchSalesDetails", "ReferenceNo='" + txtOrderNo.Text + "' " +
                "AND CashierTransNo='" + lblTransactionIDCashier.Text + "'" +
                "AND isCancelled=0 AND isVoid=0 and isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'", "SequenceNumber");

            AddDiscount adis = new AddDiscount();
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
                    adis.txttransactionno.Text = lblTransactionID.Text;
                    adis.ShowDialog(this);
                }
                else if (postype == "2")
                {

                    adisres.txtorderno.Text = txtOrderNo.Text;
                    adisres.txtcashiertansno.Text = lblTransactionIDCashier.Text;
                    adisres.txttransactionno.Text = lblTransactionID.Text;
                    adisres.ShowDialog(this);
                }

            }
            if (AddDiscount.isdone == true)
            {
                double totdiscount = 0.0;
                //totdiscount = Math.Round(Convert.ToDouble(lblTotalDiscount.Text) + getSalesDiscount(), 2);
                totdiscount = Math.Round(getPerItemDiscount() + getSalesDiscount(), 2);
                lblTotalDiscount.Text = totdiscount.ToString();//Convert.ToDouble(lblTotalDiscount.Text)+AddDiscount.discountamount;
                newtotalamount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblTotalDiscount.Text);
                //txtamountpayable.Text = newtotalamount.ToString();
                if (AddDiscount.discounttype == "SENIOR")
                {
                    lblseniordiscount.Text = getSalesDiscount().ToString();
                }
                else if (AddDiscount.discounttype == "PWD")
                {
                    lblpwddiscount.Text = getSalesDiscount().ToString();
                }

                updateTransactionNo();
                adis.Dispose();
            }
            if (AddDiscountRestaurant.isdone == true)
            {
                double totdiscount = 0.0;
                //totdiscount = Math.Round(Convert.ToDouble(lblTotalDiscount.Text) + getSalesDiscount(), 2);
                totdiscount = Math.Round(getPerItemDiscount() + getSalesDiscount(), 2);
                lblTotalDiscount.Text = totdiscount.ToString();//Convert.ToDouble(lblTotalDiscount.Text)+AddDiscount.discountamount;
                newtotalamount = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(lblTotalDiscount.Text);
                //txtamountpayable.Text = newtotalamount.ToString();
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

        private void button16_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private DataGridView CopyDataGridView(DataGridView dgv_org)
        {
            DataGridView dgv_copy = new DataGridView();
            try
            {
                if (dgv_copy.Columns.Count == 0)
                {
                    foreach (DataGridViewColumn dgvc in dgv_org.Columns)
                    {
                        dgv_copy.Columns.Add(dgvc.Clone() as DataGridViewColumn);
                    }
                }

                DataGridViewRow row = new DataGridViewRow();

                for (int i = 0; i < dgv_org.Rows.Count; i++)
                {
                    row = (DataGridViewRow)dgv_org.Rows[i].Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in dgv_org.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }
                    dgv_copy.Rows.Add(row);
                }
                dgv_copy.AllowUserToAddRows = false;
                dgv_copy.Refresh();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                //cf.ShowExceptionErrorMsg("Copy DataGridViw", ex);
            }
            return dgv_copy;
        }

       

        private void button18_Click(object sender, EventArgs e)
        {
            string tableno = "";
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Generate the Bill?", "Generate Bill");
            if (confirm)
            {
                if (checkedListBoxControl1.CheckedItemsCount == 0)
                {
                    XtraMessageBox.Show("Please Select atleast 1 Table Number");
                    return;
                }

                //List<string> tableno;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem items in checkedListBoxControl1.CheckedItems)
                {
                    tableno += "[" + items.Value.ToString() + "] ";
                }
                //Database.ExecuteLocalQuery("Truncate Table TempTable", Database.getCustomizeConnection());
                Database.ExecuteQuery("Truncate Table TempTable");

                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("INSERT INTO TempTable VALUES('" + dataGridView1.Rows[i].Cells["Particulars"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Qty"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Amount"].Value.ToString() + "')");
                }
                SqlConnection con = Database.getConnection();//Database.getCustomizeConnection();
                con.Open();
                string query = "SELECT Description as Particulars,SUM(Qty) As Qty,SUM(Amount) As Amount FROM TempTable GROUP BY Description";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView2.DataSource = table;

                Printing printit = new Printing();

                bool haveDiscount = false;
                haveDiscount = haveOneTimeDiscount();

                string vatablesales = String.Format("{0:n2}", Convert.ToDouble(lblvatsale.Text));
                string vatexemptsales = String.Format("{0:n2}", Convert.ToDouble(lblvatexemptsale.Text));
                string vat = String.Format("{0:n2}", Convert.ToDouble(lblvat.Text));

                if (haveDiscount)
                {
                    var rows = Database.getMultipleQuery("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0",
                           "DiscountType,DiscountAmount,DiscName,DiscIDNo,DiscRemarks");

                    string DiscountType, DiscountAmount, DiscName, DiscIDNo, DiscRemarks;
                    double netdue = 0.0;
                   
                    //
                    DiscountType = rows["DiscountType"].ToString();
                    DiscountAmount = rows["DiscountAmount"].ToString();
                    DiscName = rows["DiscName"].ToString();
                    DiscIDNo = rows["DiscIDNo"].ToString();
                    DiscRemarks = rows["DiscRemarks"].ToString();

                    netdue = Convert.ToDouble(lblTotalAmount.Text) - Convert.ToDouble(DiscountAmount);

                    printit.printReceiptBillingWithDiscount(lblTransactionID.Text, txtOrderNo.Text, lblTotalAmount.Text, vatablesales, vatexemptsales, vat, "0", "0", dataGridView2, tableno,true, DiscountType,netdue.ToString());
                    //printit.printReceiptConsolidated(lbltranscode.Text, lblorderno.Text, txtamountpayable.Text, vatablesales, vatexemptsales, vat, amounttender, change, PointOfSale.mygridview);

                }
                else
                {
                    printit.printReceiptBilling(lblTransactionID.Text, txtOrderNo.Text, lblTotalAmount.Text, vatablesales, vatexemptsales, vat, "0", "0", dataGridView2, tableno);

                }
                XtraMessageBox.Show("Successfully Generated!..");
            }
            else
            { return; }

        }

         
        private void chckmerge_CheckedChanged(object sender, EventArgs e)
        {
            if (chckmerge.Checked == true)
            {
                int refnumber = IDGenerator.getIDNumber("BatchSalesSummary", "BranchCode='" + Login.assignedBranch + "' AND MachineUsed='"+Environment.MachineName+"'", "ReferenceNo", 1); //IDGenerator.getOrderNumber();
                txtOrderNo.Text = refnumber.ToString();
                checkedListBoxControl1.CheckMode = CheckMode.Multiple;
            }
            else
            {
                checkedListBoxControl1.CheckMode = CheckMode.Single;
                checkedListBoxControl1.UnCheckAll();
                txtOrderNo.Text = "";
            }

        }
        
        void updateTransactionNo()
        {
            //int refnumber = IDGenerator.getPOSTransactionID();
            int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'", "TransactionNo", 1);
            lblTransactionID.Text = HelperFunction.sequencePadding1(refnumber.ToString(), 10);//refnumber.ToString();
        }
        double getPerItemDiscount()
        {
            double discount = 0.0;
            discount = Database.getTotalSummation2("BatchSalesDetails", "ReferenceNo='" + txtOrderNo.Text + "'" +
                " and isErrorCorrect=0" +
                "and isCancelled=0" +
                "and isVoid=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'", "DiscountTotal");
            return Math.Round(discount, 2);
        }
        double getSalesDiscount()
        {
            double discount = 0.0;
            discount = Database.getTotalSummation2("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'", "DiscountAmount");
            return Math.Round(discount, 2);
        }
        private void button19_Click(object sender, EventArgs e)
        {
            paymentTransaction();
        }
        bool haveOneTimeDiscount()
        {
            bool ok = false;
            ok = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'");
            return ok;
        }
        void paymentTransaction()
        {
            try
            {
                //_transcode = lblTransactionIDCashier.Text;
                mygridview = dataGridView1;//MydataGridView1; //mygridview is a static variable declare inside the class, while MyDataGridView1 is a dataGridViewForm and set modifier to public
                //custname = txtcustname.Text; //Customer Name
                //custcode = txtcustid.Text; //Customer Code
                refno = txtOrderNo.Text.Trim(); //Order No.
                //vat = lblvat.Text; //Vat Sale
                //vatablesale = lblvatsale.Text; //Vatable Sales
                //vatexemptsale = lblvatexemptsale.Text; //Vat Exempt Sales
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
                    POS.POSConfirmPaymentResto posconfirm = new POS.POSConfirmPaymentResto();

                    bool haveDiscount = false;
                    haveDiscount = haveOneTimeDiscount();
                    if (haveDiscount) //IF TRUE
                    {
                        var rows = Database.getMultipleQuery("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'",
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
                        //else if (DiscountType == "REGULAR")
                        //{
                        //    otherDiscountAmount = DiscountAmount;
                        //    othersRemarks = DiscRemarks;
                        //    lblotherdiscount.Text = DiscountAmount;
                        //    isOthersDiscount = true;

                        //    posconfirm.txtothersdiscountamount.Text = DiscountAmount;//seniorcontrolno;
                        //    posconfirm.txtothersremarks.Text = DiscRemarks;//seniorname;
                        //    totalonetimediscount = Convert.ToDouble(DiscountAmount);

                        //    totaldiscount = Convert.ToDouble(DiscountAmount) + Convert.ToDouble(posconfirm.txtordinarydiscountamount.Text);
                        //}
                    }

                    posconfirm.txtcustnamercpt.Text = txtcustnamercpt.Text;
                    posconfirm.txtcustaddressrcpt.Text = txtcustaddressrcpt.Text;
                    posconfirm.txtcusttinrcpt.Text = txtcusttinrcpt.Text;
                    posconfirm.txtcustbussstyle.Text = txtcustbussstyle.Text;

                    posconfirm.lblorderno.Text = txtOrderNo.Text; //ORDER NO
                    posconfirm.lbltransno.Text = lblTransactionID.Text;
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
                    posconfirm.btncaller.Text = "RETAILWITHDASHBOARD";
                    posconfirm.lblcashiertransno.Text = lblTransactionIDCashier.Text;
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
                        //updateOR();
                        refreshView();
                        updateTransactionNo();
                        

                        POS.POSConfirmPaymentResto.transactiondone = false;

                        isOnetimeDiscount = false;
                        isSeniorDiscount = false;
                        isPwdDiscount = false;
                        isOthersDiscount = false;

                        posconfirm.Dispose();
                        //setStandByText();
                        //txtdiscount.Text = "0";
                       
                    }
                    isdone = true;
                    isClosed = true;
                    this.Close();
                }
               
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void refreshView()
        {

            //txtsku.Text = "";
            //txtsku.Focus();
            chckZeroRated.Checked = false;

            lblTotalDiscount.Text = "0";
            lblseniordiscount.Text = "0";
            lblpwddiscount.Text = "0";
            //lblotherdiscount.Text = "0";
            //lblonetimediscountamount.Text = "0";
            //lblperitemdiscountamount.Text = "0";

            lblTotalAmount.Text = "0";
            lblTotalItems.Text = "0";
            lblvatexemptsale.Text = "0";
            lblvatsale.Text = "0";
            lblvat.Text = "0";

            //txtcustbussstyle.Text = "";
            //txtcustaddressrcpt.Text = "";
            //txtcustnamercpt.Text = "";
            //txtcusttinrcpt.Text = "";

            //table = new DataTable();
            //table.Columns.Add("ID");
            //table.Columns.Add("Particulars");
            //table.Columns.Add("UnitPrice");
            //table.Columns.Add("Qty");
            //table.Columns.Add("Discount");
            //table.Columns.Add("Amount");
            //table.Columns.Add("isVat");
            //MydataGridView1.DataSource = table; //local gridview
            //MydataGridView1.Columns["ID"].Visible = false; //localgridview
            //MydataGridView1.Columns["isVat"].Visible = false;

            populateTables();

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
        }

        double getVatAdjustment()
        {
            double discount = 0.0;
            discount = Database.getTotalSummation2("SalesDiscount", "OrderNo='" + txtOrderNo.Text + "' and isErrorCorrect=0 And BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName.ToString() + "'", "VatAdjustment");
            return Math.Round(discount, 2);
        }
        Double computeTotalAmountWithDiscount()
        {
            double total = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                total += Convert.ToDouble(dataGridView1.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(dataGridView1.Rows[i].Cells["Discount"].Value);
            }
            return Math.Round(total, 2);
        }

    }
}