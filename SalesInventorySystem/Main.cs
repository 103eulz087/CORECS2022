using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using DevExpress.XtraBars.Localization;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using SalesInventorySystem.POS;
using SalesInventorySystem.HotelManagement;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem
{
    public partial class Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static string getUserTransCode = "";
        string strmenuInventory,strmenuAdmin,strmenuSales,strmenuAccounting,strmenuReporting,strmenuHotel, strmenuForwarding, strmenucif;
        double cashbegin = Convert.ToDouble(Login.iscashBegin);
        public static bool isbatchupload = false;
        public static string program = "";
        bool isadmin = false, issales = false, isinv = false, isaccounting = false, ishotel = false, ispayroll = false, isreporting = false, isforwarding = false;
        public Main()
        {
            InitializeComponent();
            //DevExpress.UserSkins.BonusSkins.Register();
            //DevExpress.Skins.SkinManager.EnableFormSkins();
            //DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(skinRibbonGalleryBarItem1, true, true);
        }
        
        //public void DoWork()
        //{
        //    byte[] bytes = new byte[1024];
        //    while (true)
        //    {
        //        Thread.Sleep(10);
        //        int bytesRead = ns.Read(bytes, 0, bytes.Length);
        //       // this.SetText(Encoding.ASCII.GetString(bytes, 0, bytesRead));
        //        //XtraMessageBox.Show(Encoding.ASCII.GetString(bytes, 0, bytesRead));
        //    }
        //}

        //private void SetText(string text)
        //{
        //    // InvokeRequired required compares the thread ID of the
        //    // calling thread to the thread ID of the creating thread.
        //    // If these threads are different, it returns true.
        //    if (this.textBox1.InvokeRequired)
        //    {
        //        SetTextCallback d = new SetTextCallback(SetText);
        //        this.Invoke(d, new object[] { text });
        //    }
        //    else
        //    {
        //        this.textBox1.Text = this.textBox1.Text + text;
        //    }
        //}

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            showInventory1();
        }
        private bool IsVisible(BarItemLink itemLink)
        {
            return (itemLink.Item.Visibility == BarItemVisibility.Always) && itemLink.Visible;
            //return (itemLink.Item.Visibility == BarItemVisibility.Always || itemLink.Item.Visibility == BarItemVisibility.OnlyInRuntime) && itemLink.Visible;
        }
        public void readMenu(string strMenu, RibbonPage currentPage)
        {
            if (strMenu == "<empty>")
            {
                currentPage.Visible = false;
                return;
            }
            if (String.IsNullOrEmpty(strMenu))
            {
                currentPage.Visible = false;
                return;
            }
            BarItem mCurrentItem = default(BarItem);
            string wholefile = null;
            string[] linedata = null;
            string[] fielddata = null;
            wholefile = strMenu;
            //linedata = Regex.Split(wholefile, Environment.NewLine);
            linedata = wholefile.Split('\n');
            foreach (string lineoftext in linedata)
            {
                fielddata = lineoftext.Split('|');
                foreach (string wordoftexgt in fielddata)
                {
                    foreach (RibbonPageGroup currentGroup in currentPage.Groups)
                    {
                        bool isHidden = true;
                        foreach (BarItemLink currentLink in currentGroup.ItemLinks)
                        {
                            if (currentLink.Item.Visibility == BarItemVisibility.Always)
                            {
                                isHidden = false;
                                //break;
                            }
                            mCurrentItem = currentLink.Item;
                            if (currentLink.Item.Name == wordoftexgt)
                            {
                                currentLink.Item.Visibility = BarItemVisibility.Always;
                            }
                        }
                        currentGroup.Visible = !isHidden;
                    }
                }
            }
        }
        private void validate_userAccess()
        {
            Main m = new Main();
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("Select * from UserMenuAccess where UserID= '" +Login.isglobalUserID + "'  ", con);
            SqlDataReader reader = com.ExecuteReader();

            try
            {
                
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        strmenuAdmin = reader["isAdmin"].ToString();
                        strmenuSales = reader["isSales"].ToString();
                        strmenuInventory = reader["isInventory"].ToString();
                        strmenuAccounting = reader["isAccounting"].ToString();
                        strmenuHotel = reader["isHotel"].ToString();
                        strmenuReporting = reader["isReporting"].ToString();
                        strmenuForwarding = reader["isForwarding"].ToString();
                        strmenucif = reader["isClientDataSheet"].ToString();
                    }
                }
                readMenu(strmenuAdmin, AdminPage);
                readMenu(strmenuSales, SalesPage);
                readMenu(strmenuInventory, InventoryPage);
                readMenu(strmenuAccounting, AccountingPage);
                readMenu(strmenuHotel, HOTELMANAGEMENT);
                readMenu(strmenuReporting, REPORTING);
                readMenu(strmenuForwarding, FORWARDING);
                readMenu(strmenuForwarding, CIF);

                if (Login.isglobalUserID == "eulz")
                {
                    AdminPage.Visible = true;
                    btnUserAccess.Visibility = BarItemVisibility.Always;
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

        private void Main_Load(object sender, EventArgs e)
        {
            //Login login = new Login();
            //login.ShowDialog(this);

           // enzowarehouse.Visible = false;
            validate_userAccess();
            //checkAccess();
            //  ribbonPage1.Visible = false;
            //if (Convert.ToBoolean(Login.isglobalAdmin) != true) //ADMIN TOOLS TAB
            //{
            //    AdminPage.Visible = false;
            //}
            //if(Convert.ToBoolean(Login.isglobalWarehouseOfficer) ==true)
            //{
            //    AdminPage.Visible = false;
            //    SALES.Visible = false;
            //    INVENTORY.Visible = false;
            //    HOTELMANAGEMENT.Visible = false;
            //    ACCOUNTING.Visible = false;
            //}
            ////if (Convert.ToBoolean(Login.isglobalOfficer) != true) //HEAD OFFICE TAB
            ////{
            ////    HOPage.Visible = false;
            ////}
            ////if (Convert.ToBoolean(Login.isglobalWarehouseOfficer) != true) //WAREHOUSE TAB
            ////{
            ////    WarehousePage.Visible = false;
            ////}
            ////if (Convert.ToBoolean(Login.isglobalBranchOfficer) != true) //BRANCH CASHIER
            ////{
            ////    BranchPage.Visible = false;
            ////}
            //if (Convert.ToBoolean(Login.isCashier) != true)
            //{
            //    SALES.Visible = false;
            //}
            //if (Convert.ToBoolean(Login.isglobalAccounting) != true) //ACCOUNTING TAB
            //{
            //    ACCOUNTING.Visible = false;
            //}

            barStaticItem3.Caption = HelperFunction.GetLocalIPAddress();
            barStaticItem2.Caption = Login.assignedBranch + " - " + getbranchname();
            barHeaderItem1.Caption = Login.isglobalUserID;
            barHeaderItem3.Caption = Login.servername;
            barHeaderItem4.Caption = DateTime.Now.ToShortDateString();
             
            if (Convert.ToBoolean(Login.isCashier) == true)
            {
                string transdate = Database.getSingleResultSet("SELECT  dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToString() + "')");
                string getCashierTransNo = Database.getSingleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' AND UserID='" + barHeaderItem1.Caption + "' and TransactionDate='" + transdate.Trim() + "'", "CashierTransNo");

                bool isUserExistToday = Database.checkifExist("SELECT BranchCode FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' and TransactionDate='" + transdate.Trim() + "' AND isOpen='1' and CashierTransNo='" + getCashierTransNo + "'"); //UserID='" + Login.isglobalUserID + "'
                if (isUserExistToday == true)
                {
                    barStaticCashierTransNo.Caption = getCashierTransNo;
                }
                else
                {
                    barStaticCashierTransNo.Caption = "NON";
                }
                
            }
            else
            {
                barStaticItem8.Visibility = BarItemVisibility.Never;
                barStaticCashierTransNo.Visibility = BarItemVisibility.Never;
            }
            
            
            //RemoteWindow remwin = new RemoteWindow();
            //remwin.Hide();
            //connectServer();
           
        }

        void checkAccess()
        {
            SqlConnection con = Database.getConnection();
            try
            {
                con.Open();
                string query = "sp_CheckAccess";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmuserid", Login.isglobalUserID);
                com.Parameters.Add("@isadmin", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("@issales", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("@isinv", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("@isacct", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("@ishotel", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("@ispayroll", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("@isreporting", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.Parameters.Add("@isforwarding", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 3600;
                com.ExecuteNonQuery();
                isadmin = Convert.ToBoolean(com.Parameters["@isadmin"].Value.ToString());
                issales = Convert.ToBoolean(com.Parameters["@issales"].Value.ToString());
                isinv = Convert.ToBoolean(com.Parameters["@isinv"].Value.ToString());
                isaccounting = Convert.ToBoolean(com.Parameters["@isacct"].Value.ToString());
                ishotel = Convert.ToBoolean(com.Parameters["@ishotel"].Value.ToString());
                ispayroll = Convert.ToBoolean(com.Parameters["@ispayroll"].Value.ToString());
                isreporting = Convert.ToBoolean(com.Parameters["@isreporting"].Value.ToString());
                isforwarding = Convert.ToBoolean(com.Parameters["@isforwarding"].Value.ToString());

                AdminPage.Visible = isadmin;
                SalesPage.Visible = issales;
                InventoryPage.Visible = isinv;
                AccountingPage.Visible = isaccounting;
                HOTELMANAGEMENT.Visible = ishotel;
                REPORTING.Visible = isreporting;
                FORWARDING.Visible = isforwarding;

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

        // Custom localizer that changes skin captions 

        string getbranchname()
        {
            string str = Database.getSingleData("Branches", "BranchCode", Login.assignedBranch,"BranchName");
            return str;
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            //HOForms.AddCarcassInventory addinv = new HOForms.AddCarcassInventory();
            //addinv.Show();

            HOFormsDevEx.SetupBatchCodeDevEx setup = new HOFormsDevEx.SetupBatchCodeDevEx();
            setup.Show();
            //ViewShipmentDashboard viewshipdash = new ViewShipmentDashboard();
            //viewshipdash.Show();
        }

     
        private void showInventory1()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ViewInventory))
                {
                    form.Activate();
                    return;
                }
            }
            ViewInventory viewinv = new ViewInventory();
            viewinv.MdiParent = this;
            viewinv.Show();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            showInventory2();
        }

        private void showInventory2()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ViewPrimalCutsInventory))
                {
                    form.Activate();
                    return;
                }
            }
            ViewPrimalCutsInventory viewprinv = new ViewPrimalCutsInventory();
            viewprinv.MdiParent = this;
            viewprinv.Show();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            //CarcassSettings carset = new CarcassSettings();
            //carset.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.InventoryCost))
                {
                    form.Activate();
                    return;
                }
            }
            //HOForms.InventoryCostDevEx invcost = new HOForms.InventoryCostDevEx();
            //invcost.MdiParent = this;
            //invcost.Show();
            HOFormsDevEx.InventoryCost invcost = new HOFormsDevEx.InventoryCost();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ProductsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ProductsDevEx prodsets = new HOFormsDevEx.ProductsDevEx();
            prodsets.MdiParent = this;
            prodsets.Show();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddOrder adrode = new AddOrder();
            adrode.Show();

            //string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToString() + "')");

            //bool isUserExistToday = Database.checkifExist("SELECT BranchCode FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' " +
            //    "AND DateOpen='" + DateTime.Now.ToShortDateString() + "' " +
            //    "AND isOpen='1' " +
            //    "AND UserID='" + Login.isglobalUserID + "'");
            //bool isEOFProcess = Database.checkifExist("SELECT BranchCode FROM POSEODMonitoring WHERE BranchCode='" + Login.assignedBranch + "' " +
            //    "AND TransactionDate='" + transdate + "' " +
            //    "AND isEndOfDay=1");

            //if (isEOFProcess == true)
            //{
            //    XtraMessageBox.Show("The System found out that you already Execute END OF DAY Process...");
            //    return;
            //}
            //else if (isUserExistToday)
            //{
            //    AddOrder adrode = new AddOrder();
            //    adrode.Show();
            //}
            //else
            //{
            //    CashBeginningFrm cashbeg = new CashBeginningFrm();
            //    cashbeg.ShowDialog(this);
            //    if (CashBeginningFrm.isdone == true)
            //    {
            //        barStaticCashierTransNo.Caption = CashBeginningFrm.cashiertransno;
            //        CashBeginningFrm.isdone = false;
            //    }

            //}
        }



        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POForApproval))
                {
                    form.Activate();
                    return;
                }
            }
            POForApproval pfoap = new POForApproval();
            pfoap.MdiParent = this;
            pfoap.Show();

        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            POForDelivery podeliv = new POForDelivery();
            podeliv.Show();
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ViewRequest))
                {
                    form.Activate();
                    return;
                }
            }
            ViewRequest viewreq = new ViewRequest();
            //  chrginv.MdiParent = this;
            viewreq.Show();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HeadOfficeInventory))
                {
                    form.Activate();
                    return;
                }
            }
            HeadOfficeInventory headinv = new HeadOfficeInventory();
            headinv.MdiParent = this;
            headinv.Show();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(BranchInventory))
                {
                    form.Activate();
                    return;
                }
            }
            BranchInventory brancinv = new BranchInventory();
            brancinv.MdiParent = this;
            brancinv.Show();
        }

        void openPOS()
        {
            bool isRetail = Database.checkifExist("SELECT POSType FROM dbo.POSType where POSType=1");

            if (isRetail == true)
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(PointOfSale))
                    {
                        form.Activate();
                        return;
                    }
                }
                PointOfSale psale = new PointOfSale();
                psale.Show();
            }
            else
            {
                //POS.POSMainWithDashboard pcusatfsmr = new POS.POSMainWithDashboard();
                //HotelFrmRestaurant pcusatfsmr = new HotelFrmRestaurant();
                POSMainRestoDashboard pcusatfsmr = new POSMainRestoDashboard();
                pcusatfsmr.Show();
            }
        }

        string getTransactionNumber()
        {
            string num = Classes.Utilities.readTextfile("C:\\POSTransaction\\TranSeries\\");
            int ornumnew = Convert.ToInt32(num) + 1;
            return ornumnew.ToString();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {

            //if (cashbegin > 0 || CashBeginningFrm.cashbegin > 0)
            //{
                string getlasttransactiondate = Database.getLastDate("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "'", "DateOpen");
                string getcashbeg = "0";
                getcashbeg = Database.getSingleQueryWithNull("SalesTransactionSummary", "NextBeginningCash", "DateOpen='" + getlasttransactiondate + "'", "NextBeginningCash");
                string begincash;
                if (getcashbeg == "")
                {
                    begincash = "0";
                }
                else
                {
                    begincash = getcashbeg;
                }
                //PointOfSale psale = new PointOfSale();
                //psale.Show();
                bool ok = Database.checkifExist("SELECT BranchCode FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' and DateOpen='" + DateTime.Now.ToShortDateString() + "' AND isOpen='0'");
                if (Database.checkifExist("SELECT UserID FROM SalesTransactionSummary WHERE UserID='" + Login.isglobalUserID + "' AND isOpen='1' And BranchCode='" + Login.assignedBranch + "'")) //if true OPEN POS 
                {
                    openPOS();
                    //PointOfSale psale = new PointOfSale();
                    //psale.Show();
                    //psale.TopMost = true;
                    //this.Hide();
                }
                else if (ok) //if 
                {
                    // XtraMessageBox.Show("The System found out that you already Closed Transaction of the day.");
                    bool confirm = HelperFunction.ConfirmDialog("The System found out that you already Closed Transaction of the day..Are you sure you want to Create New Transaction?", "New Transaction!");
                    if (confirm)
                    {
                        AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                        authfrm.Show();
                        if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                        {
                        //int id = IDGenerator.getSalesTransactionID();
                            int id = Convert.ToInt32(getTransactionNumber());
                            string getlasttransactiondate1 = Database.getLastDate("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "'", "DateOpen");

                            string getcashbeg1 = "0";
                            getcashbeg = Database.getSingleQueryWithNull("SalesTransactionSummary", "NextBeginningCash", "DateOpen='" + getlasttransactiondate1 + "'", "NextBeginningCash");
                            //Database.ExecuteQuery("UPDATE Users set CashBeginning='" + textEdit1.Text + "' WHERE UserID='" + Login.isglobalUserID + "'");
                            Database.ExecuteQuery("INSERT INTO SalesTransactionSummary VALUES('" + id + "','" + Login.assignedBranch + "','" + Login.isglobalUserID + "','" + getcashbeg1 + "','','" + DateTime.Now.ToString() + "',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'','',0,0,0,'" + Login.isglobalUserID + "','','','1','" + DateTime.Now.ToString() + "','0')");
                            // Database.ExecuteQuery("INSERT INTO Transactions VALUES('" + Login.assignedBranch + "','" + referencenumber + "','CshBeg','CshBeg','" + textEdit1.Text + "','" + DateTime.Now.ToString() + "','" + Login.isglobalUserID + "' )");
                            Database.ExecuteQuery("INSERT INTO CashiersBlotter VALUES('" + Login.assignedBranch + "','" + DateTime.Now.ToString() + "','" + id + "','CshBeg','" + getcashbeg1 + "','','" + Login.isglobalUserID + "','0')");
                            Database.ExecuteQuery("INSERT INTO TransactionCash VALUES('" + DateTime.Now.ToString() + "','" + id + "','" + Login.isglobalUserID + "','','" + Login.assignedBranch + "','CshBeg','" + getcashbeg1 + "','0','','0')", "Transaction Successfully Open");
                            //this.Close();

                            PointOfSale psale = new PointOfSale();
                            psale.Show();
                            AuthorizedConfirmationFrm.isconfirmedLogin = false;
                            authfrm.Dispose();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (Convert.ToDouble(begincash) != 0)
                {
                    //int id = IDGenerator.getSalesTransactionID();
                    int id = Convert.ToInt32(getTransactionNumber());
                    int referencenumber = Convert.ToInt32(IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber"));
                    string debitgl, creditgl;
                    debitgl = Database.getSingleQuery("TransactionDefinition", "TransCode='CshBeg' AND BranchCode='" + Login.assignedBranch + "' ", "DebitGLAccount");
                    creditgl = Database.getSingleQuery("TransactionDefinition", "TransCode='CshBeg' AND BranchCode='" + Login.assignedBranch + "' ", "CreditGLAccount");
                    Database.ExecuteQuery("INSERT INTO SalesTransactionSummary VALUES('" + id + "','" + Login.assignedBranch + "','" + Login.isglobalUserID + "','" + getcashbeg + "','','" + DateTime.Now.ToString() + "',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'','',0,0,0,'" + Login.isglobalUserID + "','','','1','" + DateTime.Now.ToString() + "','0')");
                     // Database.ExecuteQuery("INSERT INTO Transactions VALUES('" + Login.assignedBranch + "','" + referencenumber + "','CshBeg','CshBeg','" + textEdit1.Text + "','" + DateTime.Now.ToString() + "','" + Login.isglobalUserID + "' )");
                    Database.ExecuteQuery("INSERT INTO CashiersBlotter VALUES('" + Login.assignedBranch + "','" + DateTime.Now.ToString() + "','" + referencenumber + "','CshBeg','" + getcashbeg + "','','" + Login.isglobalUserID + "','0')");
                    Database.ExecuteQuery("INSERT INTO TransactionCash VALUES('" + DateTime.Now.ToString() + "','" + referencenumber + "','" + Login.isglobalUserID + "','','" + Login.assignedBranch + "','CshBeg','" + getcashbeg + "','0','','0')", "Transaction Successfully Open");
                    
                    openPOS();
                  
                }
                else
                {
                    CashBeginningFrm cashgbeg = new CashBeginningFrm();
                    cashgbeg.Show();
                    //bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Create New Transaction?", "New Transaction!");
                    //if (ok)
                    //{

                    //    Database.ExecuteQuery("INSERT INTO SalesTransactionSummary VALUES('CSHBEG','100001','" + Login.assignedBranch + "','" + Login.isglobalUserID + "','" + Login.iscashBegin + "','','" + DateTime.Now.ToString() + "',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'','',0,0,0,'" + Login.isglobalUserID + "','','','1','" + DateTime.Now.ToString() + "')", "Transaction Successfully Open");
                    //    PointOfSale psale = new PointOfSale();
                    //    psale.Show();
                    //    //Database.ExecuteQuery("UPDATE BatchSalesDetails SET isCancelled=1,CancelledBy='EULZ' WHERE SequenceNumber='" + dataGridView1[0,dataGridView1.CurrentRow.Index].Value.ToString() + "'", "Successfully Cancelled");
                    //}

                }
            //}
            //else
            //{
                  //bool oks = HelperFunction.ConfirmDialog("The System found out that you have no CashBeginning Amount! Are you sure you want to Proceed for New Transaction?", "New Transaction!");
                  //if (oks)
                  //{
                  //  CashBeginningFrm cashgbeg = new CashBeginningFrm();
                  //  cashgbeg.Show();
                  //}
            //}
           
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(CashSalesReport))
                {
                    form.Activate();
                    return;
                }
            }
            CashSalesReport cashreps = new CashSalesReport();
            cashreps.MdiParent = this;
            cashreps.Show();

        }

        private void barStaticItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {

            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOForms.ProcessPrimalCutForm))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            HOForms.SetBatchCodeFrm setbarcho = new HOForms.SetBatchCodeFrm();

            setbarcho.Show();
            //setbarcho.TopMost = true;
            //AddPrimalCuts pcutfmr = new AddPrimalCuts();
            //pcutfmr.Show();

            //HOForms.ProcessPrimalCutForm pcutform = new HOForms.ProcessPrimalCutForm();
            //pcutform.Show();

            //HOForms.AddPrimalCutInventory pcutform = new HOForms.AddPrimalCutInventory();
            //pcutform.Show();

            //HOFormsDevEx.AddPrimalCutDevEx pcutform = new HOFormsDevEx.AddPrimalCutDevEx();
            //pcutform.Show();

        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ViewBranchOrder))
                {
                    form.Activate();
                    return;
                }
            }
            ViewBranchOrder viewbrordcr = new ViewBranchOrder();
            viewbrordcr.MdiParent = this;
            viewbrordcr.Show();
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.AddPurchaseOrder))
                {
                    form.Activate();
                    return;
                }
            }

            //HOForms.ADDPO asda = new HOForms.ADDPO();
            //asda.Show();
            HOFormsDevEx.AddPurchaseOrder asda = new HOFormsDevEx.AddPurchaseOrder();
            asda.Show();
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ViewGeneralInventory))
                {
                    form.Activate();
                    return;
                }
            }
            ViewGeneralInventory viewgeninv = new ViewGeneralInventory();
            viewgeninv.MdiParent = this;
            viewgeninv.Show();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POS.POSSalesReportDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            POS.POSSalesReportDevEx postra = new POS.POSSalesReportDevEx();
            postra.Show();
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.AddSupplierDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.AddSupplierDevEx addsupp = new HOFormsDevEx.AddSupplierDevEx();
            addsupp.MdiParent = this;
            addsupp.Show();
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.VIEWPO))
                {
                    form.Activate();
                    return;
                }
            }
            //ViewOrder viewrod = new ViewOrder();
            //viewrod.MdiParent = this;
            //viewrod.Show();
            HOFormsDevEx.VIEWPO sad = new HOFormsDevEx.VIEWPO();
            sad.MdiParent = this;
            sad.Show();
        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(ProductCategory))
                {
                    form.Activate();
                    return;
                }
            }
            ProductCategory prodcat = new ProductCategory();
            prodcat.MdiParent = this;
            prodcat.Show();
        }

        private void barButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(Products))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //Products prod = new Products();
            //prod.Show();
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(InventoryCost))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //InventoryCost invcost = new InventoryCost();
            //invcost.Show();
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOForms.Users))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOForms.Users housers = new HOForms.Users();
            //housers.MdiParent = this;
            //housers.Show();

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.UsersDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.UsersDevEx housers = new HOFormsDevEx.UsersDevEx();
            housers.MdiParent = this;
            housers.Show();
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(Branches.Branches))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //Branches.Branches brnchs = new Branches.Branches();
            //brnchs.MdiParent = this;
            ////brnchs.Show();
            //brnchs.Show();

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.BranchDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.BranchDevEx brnchs = new HOFormsDevEx.BranchDevEx();
            brnchs.MdiParent = this;
            //brnchs.Show();
            brnchs.Show();
        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                //   if (form.GetType() == typeof(HOForms.CustomersFrm))
                if (form.GetType() == typeof(HOFormsDevEx.CustomersInfoDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.CustomersInfoDevEx cstfmrs = new HOFormsDevEx.CustomersInfoDevEx();
            cstfmrs.MdiParent = this;
            cstfmrs.Show();
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.TransactionDefinitionForm))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.TransactionDefinitionForm transdefnit = new HOForms.TransactionDefinitionForm();
            transdefnit.MdiParent = this;
            transdefnit.Show();
        }

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.COA))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.COA coacct = new Accounting.COA();
            coacct.Show();
        }

        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.AddNewTicket))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.AddNewTicket addnewtcet = new Accounting.AddNewTicket();
            addnewtcet.Show();
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Accounting.GLPosting glpost = new Accounting.GLPosting();
            //glpost.Show();
            Accounting.GLPostingDevEx glpost = new Accounting.GLPostingDevEx();
            glpost.ShowDialog(this);
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Accounting.ViewTicket actvoewti = new Accounting.ViewTicket();
            //actvoewti.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.ViewTicketDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.ViewTicketDevEx hocn = new Accounting.ViewTicketDevEx();
            hocn.Show();
        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            Accounting.GLSummary acctglsum = new Accounting.GLSummary();
            acctglsum.Show();
        }

        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if(Login.assignedBranch == "888")
            //{
            //    foreach (Form form in Application.OpenForms)
            //    {
            //        if (form.GetType() == typeof(HOFormsDevEx.ConversionDevEx))
            //        {
            //            form.Activate();
            //            return;
            //        }
            //    }
            //    HOFormsDevEx.ConversionDevEx hocn = new HOFormsDevEx.ConversionDevEx();
            //    hocn.Show();
            //}
            //else
            //{
            //    foreach (Form form in Application.OpenForms)
            //    {
            //        if (form.GetType() == typeof(HOConversion))
            //        {
            //            form.Activate();
            //            return;
            //        }
            //    }
            //    HOConversion hocn = new HOConversion();
            //    hocn.Show();
            //}
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOConversionPOS))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOConversionPOS hocn = new HOConversionPOS();
            //hocn.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOConversion))
                {
                    form.Activate();
                    return;
                }
            }
            HOConversion hocn = new HOConversion();
            hocn.Show();
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ViewShipmentDashboard viewshipdash = new ViewShipmentDashboard();
            //viewshipdash.Show();
            HOFormsDevEx.ReceivedInventoryDevEx viewshipdash = new HOFormsDevEx.ReceivedInventoryDevEx();
            viewshipdash.Show();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BatchProcessMasterDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BatchProcessMasterDevEx pcutfmr = new Reporting.BatchProcessMasterDevEx();
            pcutfmr.MdiParent = this;
            pcutfmr.Show();
        }

        

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem20_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BatchProcessReports))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BatchProcessReports pcutfsmr = new Reporting.BatchProcessReports();
            pcutfsmr.Show();
        }

        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.AcctTicketReports))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.AcctTicketReports pcusatfsmr = new Reporting.AcctTicketReports();
            pcusatfsmr.Show();
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(Reporting.ViewTicketDevExRep))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //Reporting.ViewTicketDevExRep pcusatfsmr = new Reporting.ViewTicketDevExRep();
            //pcusatfsmr.Show();
        }

        private void barButtonItem52_ItemClick(object sender, ItemClickEventArgs e)
        {
            //POS.POSXreadReport xread = new POS.POSXreadReport();
            //xread.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSDevEx.POSXReadReportDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            POSDevEx.POSXReadReportDevEx viewinv = new POSDevEx.POSXReadReportDevEx();
            viewinv.MdiParent = this;
            viewinv.Show();
        }

        private void barButtonItem53_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.ReturnInventory))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.ReturnInventory pcusatfsmr = new HOForms.ReturnInventory();
            pcusatfsmr.Show();
        }

        private void barButtonItem54_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOForms.TransferInventory))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOForms.TransferInventory pcusatfsmr = new HOForms.TransferInventory();
            //pcusatfsmr.MdiParent = this;
            //pcusatfsmr.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.TransferUpdateDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.TransferUpdateDevEx pcusatfsmr = new HOFormsDevEx.TransferUpdateDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.DeliveryReportsFrm))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.DeliveryReportsFrm pcusatfsmr = new Reporting.DeliveryReportsFrm();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            string getlasttransactiondate = Database.getLastDate("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "'", "DateOpen");
            string getcashbeg = "0";
            getcashbeg = Database.getSingleQueryWithNull("SalesTransactionSummary", "NextBeginningCash", "DateOpen='" + getlasttransactiondate + "'", "NextBeginningCash");
            string begincash;
            if (getcashbeg == "")
            {
                begincash = "0";
            }
            else
            {
                begincash = getcashbeg;
            }
            bool ok = Database.checkifExist("SELECT BranchCode FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' and DateOpen='" + DateTime.Now.ToShortDateString() + "' AND isOpen='0'");
            if (Database.checkifExist("SELECT UserID FROM SalesTransactionSummary WHERE UserID='" + Login.isglobalUserID + "' AND isOpen='1' And BranchCode='" + Login.assignedBranch + "'"))
            {
                BatchUploading pcusatfsmr = new BatchUploading();
                pcusatfsmr.Show();
            }
            else if (Convert.ToDouble(begincash) != 0)
            {
                int id = IDGenerator.getIDNumber("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "'", "AccountCode",1);
                int referencenumber = Convert.ToInt32(IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber"));
                // int ticketnum = Convert.ToInt32(IDGenerator.getTicketNumberSP());
                string debitgl, creditgl;
                debitgl = Database.getSingleQuery("TransactionDefinition", "TransCode='CshBeg' AND BranchCode='" + Login.assignedBranch + "' ", "DebitGLAccount");
                creditgl = Database.getSingleQuery("TransactionDefinition", "TransCode='CshBeg' AND BranchCode='" + Login.assignedBranch + "' ", "CreditGLAccount");
                Database.ExecuteQuery("INSERT INTO SalesTransactionSummary VALUES('" + id + "','" + Login.assignedBranch + "','" + Login.isglobalUserID + "','" + getcashbeg + "','','" + DateTime.Now.ToString() + "',0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,'','',0,0,0,'" + Login.isglobalUserID + "','','','1','" + DateTime.Now.ToString() + "')");
                Database.ExecuteQuery("INSERT INTO CashiersBlotter VALUES('" + Login.assignedBranch + "','" + DateTime.Now.ToString() + "','" + referencenumber + "','CshBeg','" + getcashbeg + "','','" + Login.isglobalUserID + "')");
                Database.ExecuteQuery("INSERT INTO TransactionCash VALUES('" + DateTime.Now.ToString() + "','" + referencenumber + "','" + Login.isglobalUserID + "','','" + Login.assignedBranch + "','CshBeg','" + getcashbeg + "','0','')", "Transaction Successfully Open");
                BatchUploading pcusatfsmr = new BatchUploading();
                pcusatfsmr.Show();
                
            }
            else
            {
                isbatchupload = true;
                CashBeginningFrm cashgbeg = new CashBeginningFrm();
                cashgbeg.Show();
            }
        }

        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(InventoryIN))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //InventoryIN pcusatfsmr = new InventoryIN();
            //pcusatfsmr.Show();
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(ReInventoryIn))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //ReInventoryIn pcusatfsmr = new ReInventoryIn();
            //pcusatfsmr.Show();
            //FOR FOR BUILTIN INVENTORY IN
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POS.POSInventoryIN))
                {
                    form.Activate();
                    return;
                }
            }
           
            POS.POSInventoryIN pcusatfsmr = new POS.POSInventoryIN();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.Metrics))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.Metrics pcusatfsmr = new HOForms.Metrics();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();

        }

        private void barButtonItem58_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.ConversionReports))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.ConversionReports pcusatfsmr = new Reporting.ConversionReports();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem59_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOFormsDevEx.CarcassCostingDevEx))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOFormsDevEx.CarcassCostingDevEx pcusatfsmr = new HOFormsDevEx.CarcassCostingDevEx();
            //pcusatfsmr.MdiParent = this;
            //pcusatfsmr.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.PrimalCutCosting))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.PrimalCutCosting pcusatfsmr = new HOFormsDevEx.PrimalCutCosting();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem60_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ClientAccountsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            //HOForms.ClientAccounts pcusatfsmr = new HOForms.ClientAccounts();
            HOFormsDevEx.ClientAccountsDevEx pcusatfsmr = new HOFormsDevEx.ClientAccountsDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem61_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.StocksOrder))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.StocksOrder pcusatfsmr = new Reporting.StocksOrder();
            pcusatfsmr.Show();
        }

        private void barButtonItem62_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(Sticker.PrinterSettings))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //Sticker.PrinterSettings pcusatfsmr = new Sticker.PrinterSettings();
            //pcusatfsmr.Show();

            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(ReInventoryIn))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //ReInventoryIn pcusatfsmr = new ReInventoryIn();
            //pcusatfsmr.Show();
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOFormsDevEx.ConversionDevEx))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOFormsDevEx.ConversionDevEx pcusatfsmr = new HOFormsDevEx.ConversionDevEx();
            //pcusatfsmr.Show();

            StickerLabeling pcusatfsmr = new StickerLabeling();
            pcusatfsmr.ShowDialog(this);
        }

        private void barButtonItem64_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOForms.TransferInventoryUpdate))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOForms.TransferInventoryUpdate pcusatfsmr = new HOForms.TransferInventoryUpdate();
            //pcusatfsmr.Show();
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOForms.TransferInventoryUpdate))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOForms.TransferInventoryUpdate pcusatfsmr = new HOForms.TransferInventoryUpdate();
            //pcusatfsmr.Show();
        }

        private void barButtonItem63_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.DataAnalysisAndTicketing))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.DataAnalysisAndTicketing pcusatfsmr = new HOForms.DataAnalysisAndTicketing();
            pcusatfsmr.Show();
        }

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.GLSummary))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.GLSummary pcusatfsmr = new Accounting.GLSummary();
            pcusatfsmr.Show();
        }

        private void barButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(AccountingDevEx.BalanceSheetDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            //Accounting.BalanceSheet pcusatfsmr = new Accounting.BalanceSheet();
            //pcusatfsmr.Show();
            AccountingDevEx.BalanceSheetDevEx pcusatfsmr = new AccountingDevEx.BalanceSheetDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem51_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(Accounting.IncomeStatement))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //Accounting.IncomeStatement pcusatfsmr = new Accounting.IncomeStatement();
            //pcusatfsmr.MdiParent = this;
            //pcusatfsmr.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(PivotPractice))
                {
                    form.Activate();
                    return;
                }
            }
            PivotPractice pcusatfsmr = new PivotPractice();
            pcusatfsmr.Show();
        }

        private void barButtonItem65_ItemClick(object sender, ItemClickEventArgs e)
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

        private void barButtonItem66_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.AddCheckVoucher))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.AddCheckVoucher pcusatfsmr = new Accounting.AddCheckVoucher();
            pcusatfsmr.Show();
        }

        private void barButtonItem67_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(AccountingDevEx.ViewCheckVoucherDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            //Accounting.ViewCheckVoucher pcusatfsmr = new Accounting.ViewCheckVoucher();
            AccountingDevEx.ViewCheckVoucherDevEx pcusatfsmr = new AccountingDevEx.ViewCheckVoucherDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem68_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.ViewPurchaseJournal))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.ViewPurchaseJournal pcusatfsmr = new Accounting.ViewPurchaseJournal();
            pcusatfsmr.Show();
        }

        private void barButtonItem70_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.InventoryTransferDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.InventoryTransferDevEx pcusatfsmr = new HOFormsDevEx.InventoryTransferDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem71_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.SupplierAccountsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            //HOForms.SupplierAccounts pcusatfsmr = new HOForms.SupplierAccounts();
            HOFormsDevEx.SupplierAccountsDevEx pcusatfsmr = new HOFormsDevEx.SupplierAccountsDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem72_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOFormsDevEx.AccountReconDevEx))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOFormsDevEx.AccountReconDevEx pcusatfsmr = new HOFormsDevEx.AccountReconDevEx();
            //pcusatfsmr.MdiParent = this;
            //pcusatfsmr.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.AccountReconDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.AccountReconDevEx pcusatfsmr = new Reporting.AccountReconDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem73_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSDevEx.AccountPayablesDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            POSDevEx.AccountPayablesDevEx pcusatfsmr = new POSDevEx.AccountPayablesDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem69_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem75_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.AccountMasterListDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.AccountMasterListDevEx pcusatfsmr = new HOFormsDevEx.AccountMasterListDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem76_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.CustomerProductSettingsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.CustomerProductSettingsDevEx invcost = new HOFormsDevEx.CustomerProductSettingsDevEx();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void barButtonItem77_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.InventorySettlementDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.InventorySettlementDevEx pcusatfsmr = new HOFormsDevEx.InventorySettlementDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem78_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOFormsDevEx.InventorySettlementDevEx))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOFormsDevEx.SalesCheckerDevExFrm pcusatfsmr = new HOFormsDevEx.SalesCheckerDevExFrm();
            //pcusatfsmr.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POS.POSUploadChecker))
                {
                    form.Activate();
                    return;
                }
            }
            POS.POSUploadChecker pcusatfsmr = new POS.POSUploadChecker();
            pcusatfsmr.Show();
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOFormsDevEx.AnalysisAndTicketing))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOFormsDevEx.AnalysisAndTicketing pcusatfsmr = new HOFormsDevEx.AnalysisAndTicketing();
            //pcusatfsmr.Show();
        }

        private void barButtonItem79_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.DepositInTransitDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.DepositInTransitDevEx pcusatfsmr = new Accounting.DepositInTransitDevEx();
            pcusatfsmr.Show();
        }

        private void barButtonItem80_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.AddBankTicketDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.AddBankTicketDevEx pcusatfsmr = new Accounting.AddBankTicketDevEx();
            pcusatfsmr.Show();
        }

        private void barButtonItem83_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ViewNonTradeOrdersDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ViewNonTradeOrdersDevEx pcusatfsmr = new HOFormsDevEx.ViewNonTradeOrdersDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem82_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.AddNonTradeOrdersDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.AddNonTradeOrdersDevEx pcusatfsmr = new HOFormsDevEx.AddNonTradeOrdersDevEx();
            pcusatfsmr.Show();
        }

        private void barButtonItem84_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOFormsDevEx.CostAdjustmentDevEx))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOFormsDevEx.CostAdjustmentDevEx pcusatfsmr = new HOFormsDevEx.CostAdjustmentDevEx();
            //pcusatfsmr.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.InventoryCostAdjustment))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.InventoryCostAdjustment pcusatfsmr = new HOFormsDevEx.InventoryCostAdjustment();
            pcusatfsmr.Show();
        }

        private void barButtonItem85_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(HOFormsDevEx.AddExpenseDevExFrm))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //HOFormsDevEx.AddExpenseDevExFrm pcusatfsmr = new HOFormsDevEx.AddExpenseDevExFrm();
            //pcusatfsmr.Show();
          
        }

        private void barButtonItem81_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ViewGeneralnventory))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ViewGeneralnventory pcusatfsmr = new HOFormsDevEx.ViewGeneralnventory();
            
            pcusatfsmr.Show();
        }

        private void barButtonItem86_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.DepartmentsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.DepartmentsDevEx pcusatfsmr = new HOFormsDevEx.DepartmentsDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem87_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.LocationDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.LocationDevEx pcusatfsmr = new HOFormsDevEx.LocationDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem88_ItemClick(object sender, ItemClickEventArgs e)
        {
            program = "SALESANDINVENTORY";
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.GenInventoryCategoryDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.GenInventoryCategoryDevEx pcusatfsmr = new HOFormsDevEx.GenInventoryCategoryDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
            Database.display("SELECT * FROM GenInventoryCategory", pcusatfsmr.gridControl1, pcusatfsmr.gridView1);
        }

        private void barButtonItem89_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ViewGeneralnventory))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ViewGeneralnventory pcusatfsmr = new HOFormsDevEx.ViewGeneralnventory();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
            Database.displaySearchlookupEdit("SELECT * FROM GenInventoryCategory", pcusatfsmr.txtcategory, "Category", "Category");
            Database.displaySearchlookupEdit("SELECT * FROM Custodian", pcusatfsmr.txtcustodian, "Custodian", "Custodian");
            Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", pcusatfsmr.txtvendor, "SupplierID", "SupplierID");
            Database.displaySearchlookupEdit("SELECT LocationID,LocationName FROM Location", pcusatfsmr.txtlocation, "LocationID", "LocationID");
            Database.displaySearchlookupEdit("SELECT DeptID,DeptName FROM Departments", pcusatfsmr.txtdept, "DeptID", "DeptID");
            Database.display("Select TOP 100 * FROM view_GenInventory ", pcusatfsmr.gridControl1, pcusatfsmr.gridView1);
            //pcusatfsmr.gridView1.Columns["PhotoImage"].Width = 150;
            //pcusatfsmr.gridView1.RowHeight = 50;
            //pcusatfsmr.gridView1.BestFitColumns();
        }

        private void barButtonItem90_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.GenInventoryItems))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.GenInventoryItems pcusatfsmr = new HOFormsDevEx.GenInventoryItems();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem91_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.DepreciationReports))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.DepreciationReports pcusatfsmr = new Reporting.DepreciationReports();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.PurchaseOrderRepDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.PurchaseOrderRepDevEx pcusatfsmr = new Reporting.PurchaseOrderRepDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem92_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmMainDashBoard))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmMainDashBoard pcusatfsmr = new HotelManagement.HotelFrmMainDashBoard();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem93_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmRoomCategory))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmRoomCategory pcusatfsmr = new HotelManagement.HotelFrmRoomCategory();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnCustodian_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.Custodian))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.Custodian pcusatfsmr = new HOFormsDevEx.Custodian();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnReportHeaderSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ReportHeaderSettings))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ReportHeaderSettings pcusatfsmr = new HOFormsDevEx.ReportHeaderSettings();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnCollectionList_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.CollectionList))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.CollectionList pcusatfsmr = new Reporting.CollectionList();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnHotelFoodMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmFoodMenu))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmFoodMenu pcusatfsmr = new HotelManagement.HotelFrmFoodMenu();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem2_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOForms.ChangePassword))
                {
                    form.Activate();
                    return;
                }
            }
            HOForms.ChangePassword pcusatfsmr = new HOForms.ChangePassword();
            pcusatfsmr.ShowDialog(this);
        }

        private void btnExpenseList_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ExpenseList))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ExpenseList invcost = new HOFormsDevEx.ExpenseList();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnExpenseMapping_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ExpenseMapping))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ExpenseMapping invcost = new HOFormsDevEx.ExpenseMapping();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void barButtonItem5_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.PostExpenseDevExFrm))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.PostExpenseDevExFrm pcusatfsmr = new HOFormsDevEx.PostExpenseDevExFrm();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem7_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.AddExpenseDevExFrm))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.AddExpenseDevExFrm pcusatfsmr = new HOFormsDevEx.AddExpenseDevExFrm();
            pcusatfsmr.Show();
        }

        private void btnLiquidationList_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.LiquidationList))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.LiquidationList invcost = new HOFormsDevEx.LiquidationList();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnLiquidationMapping_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.LiquidationMapping))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.LiquidationMapping invcost = new HOFormsDevEx.LiquidationMapping();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnInventoryQtyAdjustment_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.InventoryQtyAdjustmentDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.InventoryQtyAdjustmentDevEx pcusatfsmr = new HOFormsDevEx.InventoryQtyAdjustmentDevEx();
            pcusatfsmr.Show();
        }

        private void barButtonItem96_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmTypeOfRates))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmTypeOfRates pcusatfsmr = new HotelManagement.HotelFrmTypeOfRates();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnHotelFoodCategory_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmFoodCategory))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmFoodCategory pcusatfsmr = new HotelManagement.HotelFrmFoodCategory();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnHotelFoodMenuMaker_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmMenuMaker))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmMenuMaker pcusatfsmr = new HotelManagement.HotelFrmMenuMaker();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnHotelCharges_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmCharges))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmCharges pcusatfsmr = new HotelManagement.HotelFrmCharges();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnHotelReports_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmReports))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmReports pcusatfsmr = new HotelManagement.HotelFrmReports();
            pcusatfsmr.ShowDialog(this);
        }

        private void ribbonControl_SelectedPageChanged(object sender, EventArgs e)
        {
            RibbonControl ribbon = sender as RibbonControl;
            if (ribbon.SelectedPage == HOTELMANAGEMENT)
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(HotelManagement.HotelFrmMainDashBoard))
                    {
                        form.Activate();
                        return;
                    }
                }
                HotelManagement.HotelFrmMainDashBoard pcusatfsmr = new HotelManagement.HotelFrmMainDashBoard();
                pcusatfsmr.MdiParent = this;
                pcusatfsmr.Show();
            }
        }

        private void btnForwardingDashboard_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Forwarding.ForwardingDashboard))
                {
                    form.Activate();
                    return;
                }
            }
            Forwarding.ForwardingDashboard pcusatfsmr = new Forwarding.ForwardingDashboard();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnfwrdingbilling_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Forwarding.Reporting.ForwardingDeliveryFrm))
                {
                    form.Activate();
                    return;
                }
            }
            Forwarding.Reporting.ForwardingDeliveryFrm pcusatfsmr = new Forwarding.Reporting.ForwardingDeliveryFrm();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnCompany_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.AddCompanyDevFrm))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.AddCompanyDevFrm pcusatfsmr = new HOFormsDevEx.AddCompanyDevFrm();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem7_ItemClick_2(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Forwarding.ForwardingAddPrimeOver))
                {
                    form.Activate();
                    return;
                }
            }
            Forwarding.ForwardingAddPrimeOver pcusatfsmr = new Forwarding.ForwardingAddPrimeOver();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnforwardngvhcleregistration_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Forwarding.ForwardingTruckDetails))
                {
                    form.Activate();
                    return;
                }
            }
            Forwarding.ForwardingTruckDetails pcusatfsmr = new Forwarding.ForwardingTruckDetails();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnAccountingAging_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Accounting.AgingReports))
                {
                    form.Activate();
                    return;
                }
            }
            Accounting.AgingReports pcusatfsmr = new Accounting.AgingReports();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnPassbookBalances_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.PassbookBalancesDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.PassbookBalancesDevEx pcusatfsmr = new HOFormsDevEx.PassbookBalancesDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnBanks_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.BanksDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.BanksDevEx pcusatfsmr = new HOFormsDevEx.BanksDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btCounterReceipt_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.CounterReceiptDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.CounterReceiptDevEx pcusatfsmr = new Reporting.CounterReceiptDevEx();
            pcusatfsmr.Show();
        }

        private void btnCreditMemo_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.CreditMemoRepDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.CreditMemoRepDevEx pcusatfsmr = new Reporting.CreditMemoRepDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnSTSSummaryRep_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.STSSummaryReport))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.STSSummaryReport pcusatfsmr = new Reporting.STSSummaryReport();
            pcusatfsmr.Show();
        }

        private void btnConnectionSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Connection))
                {
                    form.Activate();
                    return;
                }
            }
           Connection pcusatfsmr = new Connection();
            pcusatfsmr.ShowDialog(this);
        }

        private void btnServicesCost_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ServicesCostDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ServicesCostDevEx invcost = new HOFormsDevEx.ServicesCostDevEx();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnServices_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ServicesDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ServicesDevEx invcost = new HOFormsDevEx.ServicesDevEx();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnInventoryUnitActivity_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.InventoryUnitActivity))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.InventoryUnitActivity invcost = new Reporting.InventoryUnitActivity();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnRequestStockTransfer_ItemClick(object sender, ItemClickEventArgs e)
        {
            //Orders.AddOrderSTS addsts = new Orders.AddOrderSTS();
            //addsts.ShowDialog(this);
            Orders.AddOrderSTSBatchMode addsts = new Orders.AddOrderSTSBatchMode();
            addsts.ShowDialog(this);
        }

        private void btnProcessSTS_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Orders.ViewBranchOrderSTS))
                {
                    form.Activate();
                    return;
                }
            }
            Orders.ViewBranchOrderSTS invcost = new Orders.ViewBranchOrderSTS();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnAddSTSCharges_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.AddFreightChargeSTS))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.AddFreightChargeSTS invcost = new HOFormsDevEx.AddFreightChargeSTS();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void btnViewJFCInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.JFCInventory))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.JFCInventory invcost = new HOFormsDevEx.JFCInventory();
            invcost.MdiParent = this;
            invcost.Show();
        }

        private void barButtonItem8_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Forwarding.ForwardingAddSmallTrucks))
                {
                    form.Activate();
                    return;
                }
            }
            Forwarding.ForwardingAddSmallTrucks pcusatfsmr = new Forwarding.ForwardingAddSmallTrucks();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnHotelHousekeeper_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmAddHouseKeeper))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmAddHouseKeeper pcusatfsmr = new HotelManagement.HotelFrmAddHouseKeeper();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnfwrdAddShipment_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem95_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmRoomRates))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmRoomRates pcusatfsmr = new HotelManagement.HotelFrmRoomRates();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem97_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmRestaurantTables))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmRestaurantTables pcusatfsmr = new HotelManagement.HotelFrmRestaurantTables();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem94_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmRooms))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmRooms pcusatfsmr = new HotelManagement.HotelFrmRooms();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem109_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.UserAccessDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.UserAccessDevEx pcusatfsmr = new HOFormsDevEx.UserAccessDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem104_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenPOSTransaction();
        }

        void OpenPOSTransaction()
        {
            bool isUserExistToday = Database.checkifExist("SELECT BranchCode FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' and DateOpen='" + DateTime.Now.ToShortDateString() + "' AND isOpen='1' and UserID='" + Login.isglobalUserID + "'");
            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='CASHBEGIN' AND isOverride=1");
            if (!isoverride)
            {
                if (!isUserExistToday)
                {
                    CashBeginningFrm cashgbeg = new CashBeginningFrm();
                    cashgbeg.Show();
                }
                else
                {
                     openPOS();
                }
            }
            else
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    if (!isUserExistToday)
                    {
                        CashBeginningFrm cashgbeg = new CashBeginningFrm();
                        cashgbeg.Show();
                    }
                    else
                    {
                        openPOS();
                    }
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
            
        }
        private void barButtonItem105_ItemClick(object sender, ItemClickEventArgs e)
        {
            HotelManagement.HotelFrmGuestDetails pcusatfsmr = new HotelManagement.HotelFrmGuestDetails();
            //pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem106_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmViewGuest))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmViewGuest pcusatfsmr = new HotelManagement.HotelFrmViewGuest();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem107_ItemClick(object sender, ItemClickEventArgs e)
        {
            HotelManagement.HotelFrmSendEmail senemail = new HotelManagement.HotelFrmSendEmail();
            senemail.Show();
        }

        private void barButtonItem108_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HotelManagement.HotelFrmKitchenSection))
                {
                    form.Activate();
                    return;
                }
            }
            HotelManagement.HotelFrmKitchenSection pcusatfsmr = new HotelManagement.HotelFrmKitchenSection();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void POSMachine_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSDevEx.POSSettingsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            POSDevEx.POSSettingsDevEx pcusatfsmr = new POSDevEx.POSSettingsDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();


            Sabong.SBDashboard sbdash = new Sabong.SBDashboard();
            sbdash.Show();

            //ShowSeniorCitizenReport();
        }


        void display(POS.POSTransactionChecker a)
        {
            
            string charDate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToShortDateString() + "')");
            Database.display("SELECT BranchCode,CashierTransNo,TransactionDate,UserID,isOpen,MachineUsed FROM SalesTransactionSummary " +
                    "WHERE BranchCode='" + Login.assignedBranch + "' " +
                    "and isOpen=1 " +
                    //"and UserID='" + Login.isglobalUserID + "' " +
                    "and TransactionDate <> '" + charDate.Trim() + "' " +
                    "AND MachineUsed='" + Environment.MachineName.ToString() + "' ", a.gridControl1, a.gridView1);
           
            Database.display("SELECT * FROM POSEODMonitoring " +
                    "WHERE BranchCode='" + Login.assignedBranch + "' " +
                    "and TransactionDate <> '" + charDate.Trim() + "' " +
                    "AND isEndOfDay=0 ", a.gridControl2, a.gridView2);
           
        }
        private void btnPointOFSale_ItemClick(object sender, ItemClickEventArgs e)
        {
            string charDate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToShortDateString() + "')");

            bool isPOSDetailsExists = Database.checkifExist("SELECT TOP(1) BranchCode FROM dbo.POSInfoDetails WHERE MachineUsed='" + Environment.MachineName + "'");

            //check if END OF DAY Transaction is already Executed, IF True, cashiers cannot login anymore.
            bool isExists = Database.checkifExist("SELECT TOP(1) MachineUsed " +
                                                    "FROM POSZReadingTransactions " +
                                                    "WHERE MachineUsed='" + Environment.MachineName + "' " +
                                                    "AND BranchCode='"+Login.assignedBranch+"' " +
                                                    "and DateExecute='" + DateTime.Now.ToShortDateString() + "' ");
            //bool notyetEOF = Database.checkifExist("Select isEndOfDay FROM POSEODMonitoring WHERE isEndOfDay=0 and TransactionDate='" + DateTime.Now.ToShortDateString() + "'");
            
            //check if END OF DAY Monitoring is not yet EXECUTED on this date.
            //used this condition always, because what if the CASHIER are forgot to EXECUTE END OF DAY
            bool notyetEOF = Database.checkifExist("Select TOP(1) isEndOfDay " +
                                                    "FROM POSEODMonitoring " +
                                                    "WHERE isEndOfDay=0 AND BranchCode='"+Login.assignedBranch+"' " +
                                                    "AND TransactionDate<>'"+ charDate + "'"); 

            //check if there are still OPEN CASHIER TRANSACTION on previous date.
            bool stillOpenTransaction = Database.checkifExist("Select isOpen " +
                                                          "FROM SalesTransactionSummary " +
                                                          "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                          "and isOpen=1 " +
                                                          //"and UserID='" + Login.isglobalUserID + "' " +
                                                          "and CAST(TransactionDate as date) < '" + DateTime.Now.ToShortDateString() + "'" +
                                                          "AND MachineUsed='" + Environment.MachineName.ToString() + "' ");

           bool cashierAlreadyLogin = Database.checkifExist("Select isOpen " +
                                                          "FROM SalesTransactionSummary " +
                                                          "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                          "and UserID='" + Login.isglobalUserID + "' " +
                                                          "and isOpen=0 " +
                                                          "and CAST(TransactionDate as date) = '" + DateTime.Now.ToShortDateString() + "'" +
                                                          "AND MachineUsed='" + Environment.MachineName.ToString() + "' ");

            if (!isPOSDetailsExists)
            {
                POS.POSDetails posdet = new POS.POSDetails();
                posdet.ShowDialog(this);
            }
            bool checkIfSameBranch = Database.checkifExist("SELECT TOP 1 BranchCode FROM POSInfoDetails WHERE MachineUsed='" + Environment.MachineName + "' AND BranchCode='" + Login.assignedBranch + "'");
            if (!checkIfSameBranch)
            {
                XtraMessageBox.Show("The System found out that your Profile Branch Code is not registered in this machine....");
                return;
            }
            if(cashierAlreadyLogin)
            {
                XtraMessageBox.Show("The System found out that this Cashier is Already Transacted in this Machine....");
                return;
            }
            //either one of these condition are true.. 
            if (notyetEOF == true || stillOpenTransaction==true) //naay transaction nga wla ka end of day..bsan ang mga cashier kay nag close transaction pro ang admin or supervisor wla ka execute og end of day.
            {
                XtraMessageBox.Show("The System found out that some transactions are still not yet END OF DAY Process..Please contact Admin..");
                POS.POSTransactionChecker asjkdh = new POS.POSTransactionChecker();
                display(asjkdh);
                asjkdh.ShowDialog(this);
                if(POS.POSTransactionChecker.isdone==true)
                {
                    display(asjkdh);
                    if(asjkdh.gridView1.RowCount==0 && asjkdh.gridView2.RowCount == 0)
                    {
                        asjkdh.Dispose();
                    }
                }
            }
            else if (isExists == true)//dli na mka login kay na execute na ang end of day transaction sa karon nga adlaw...so by next day na pwedi ka cash begin or login.
            {
                XtraMessageBox.Show("The System found out that END OF DAY Transaction is Already Process for today's date...You can login on the next business day/s..");
                return;
            }
            else
            {
                OpenPOSTransaction();
            }
        }

        private void btnPOSSettings_ItemClick(object sender, ItemClickEventArgs e)
        {
            POSStandAloneSetup.POSStandAloneSettingsBoard pcusatfsmr = new POSStandAloneSetup.POSStandAloneSettingsBoard();
            pcusatfsmr.ShowDialog(this);
        }

        private void barbtnDeductInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.DeductInventoryDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.DeductInventoryDevEx pcusatfsmr = new HOFormsDevEx.DeductInventoryDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnBranchInventory_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(BranchInventory))
                {
                    form.Activate();
                    return;
                }
            }
            BranchInventory brancinv = new BranchInventory();
            brancinv.MdiParent = this;
            brancinv.Show();
        }


        private void btnTransferPerPallet_ItemClick(object sender, ItemClickEventArgs e)
        {
            HOFormsDevEx.TransferPerPalletDevExFixed ads = new HOFormsDevEx.TransferPerPalletDevExFixed();
            ads.Show();
            //HOFormsDevEx.TransferPerPalletDevEx ads = new HOFormsDevEx.TransferPerPalletDevEx();
            //ads.Show();
        }

        private void btnTransferPerBarcode_ItemClick(object sender, ItemClickEventArgs e)
        {
            HOFormsDevEx.TransferByBarcode ads = new HOFormsDevEx.TransferByBarcode();
            ads.Show();
            //HOFormsDevEx.TransferPerBarcodeDevEx ads = new HOFormsDevEx.TransferPerBarcodeDevEx();
            //ads.Show();
        }

        private void barButtonItem24_ItemClick_1(object sender, ItemClickEventArgs e)
        {

        }



        private void btnPOSChecker_ItemClick(object sender, ItemClickEventArgs e)
        {
            POSUploadChecker posUpload = new POSUploadChecker();
            posUpload.ShowDialog(this);
        }

        private void btnLockedUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            HOFormsDevEx.LockedUsers lockeusr = new HOFormsDevEx.LockedUsers();
            lockeusr.ShowDialog(this);
        }


        public void ShowSeniorCitizenReport()
        {
            // 1. Load the .repx file
            XtraReport report = XtraReport.FromFile("SeniorCitizenSalesReport.repx", true);

            // 2. Fetch data from your database
           ;
            string query = @"SELECT *
                             FROM BatchSalesSummary";

            DataTable dt = new DataTable();
            using (SqlConnection conn = Database.getConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }

            // 3. Bind the DataTable to the report
            report.DataSource = dt;
            report.DataMember = ""; // For DataTable, leave empty

            // 4. Optional: Set parameter values (if used in header)
            report.Parameters["ReportDate"].Value = DateTime.Now;

            // 5. Show the report in a preview dialog
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
         
    }

    private void btnSalesManualEntry_ItemClick(object sender, ItemClickEventArgs e)
        {
            POS.PointOfSaleManual posman = new PointOfSaleManual();
            posman.ShowDialog(this);
          
        }

        private void btnInventoryMapping_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POS.POSInventoryMapping))
                {
                    form.Activate();
                    return;
                }
            }
            POS.POSInventoryMapping pcusatfsmr = new POS.POSInventoryMapping();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnInventoryCostRep_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.InventoryCostingRep))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.InventoryCostingRep pcusatfsmr = new Reporting.InventoryCostingRep();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnMerchants_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.MerchantsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.MerchantsDevEx pcusatfsmr = new HOFormsDevEx.MerchantsDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnActivityLogs_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ActivityLogsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ActivityLogsDevEx pcusatfsmr = new HOFormsDevEx.ActivityLogsDevEx();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void barButtonItem25_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.ComparativeReport))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.ComparativeReport pfoap = new Reporting.BIR.ComparativeReport();
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void btnPOSManagement2_ItemClick(object sender, ItemClickEventArgs e)
        {
            //POS.POSXreadReport xread = new POS.POSXreadReport();
            //xread.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSDevEx.POSManagementReport))
                {
                    form.Activate();
                    return;
                }
            }
            POSDevEx.POSManagementReport viewinv = new POSDevEx.POSManagementReport();
            viewinv.MdiParent = this;
            viewinv.Show();
        }

        private void barButtonItem26_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.BIR2550M2))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.BIR2550M2 pfoap = new Reporting.BIR.BIR2550M2();
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void barButtonItem27_ItemClick_1(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem30_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.ComparativeReport2))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.ComparativeReport2 pfoap = new Reporting.BIR.ComparativeReport2();
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void barBtnStockOutItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            //foreach (Form form in Application.OpenForms)
            //{
            //    if (form.GetType() == typeof(POS.POSInventoryOut))
            //    {
            //        form.Activate();
            //        return;
            //    }
            //}
            //POS.POSInventoryOut pcusatfsmr = new POS.POSInventoryOut();
            //pcusatfsmr.Show();
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.InventoryOut))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.InventoryOut pcusatfsmr = new HOFormsDevEx.InventoryOut();
            pcusatfsmr.Show();


        }

        private void btnInventoryPerBranch_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Branches.BranchGenInventory))
                {
                    form.Activate();
                    return;
                }
            }
            Branches.BranchGenInventory brancinv = new Branches.BranchGenInventory();
            brancinv.MdiParent = this;
            brancinv.Show();
        }

        private void btnCashierSalesCollectionSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(POSDevEx.POSSalesCollectionSummary))
                {
                    form.Activate();
                    return;
                }
            }
            POSDevEx.POSSalesCollectionSummary viewinv = new POSDevEx.POSSalesCollectionSummary();
            viewinv.MdiParent = this;
            viewinv.Show();
        }

        private void barbtnInventoryMonitoring_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ViewZeroInventory))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ViewZeroInventory viewgeninv = new HOFormsDevEx.ViewZeroInventory();
            viewgeninv.MdiParent = this;
            viewgeninv.Show();
        }

        private void btnReturnOrders_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BadOrderReport))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BadOrderReport viewgeninv = new Reporting.BadOrderReport();
            viewgeninv.MdiParent = this;
            viewgeninv.Show();
        }

        private void btnViewExpense_ItemClick(object sender, ItemClickEventArgs e)
        {

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ViewExpenseDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ViewExpenseDevEx viewexpense = new HOFormsDevEx.ViewExpenseDevEx();
            viewexpense.MdiParent = this;
            viewexpense.Show();
        }

        private void barButtonItem32_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.POSSalesReportSummary))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.POSSalesReportSummary pfoap = new Reporting.BIR.POSSalesReportSummary("B");
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void barButtonItem33_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.POSSalesReportSummary))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.SalesDetailsComparative pfoap = new Reporting.BIR.SalesDetailsComparative();
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //bool confirm = HelperFunction.ConfirmDialog("Are you want to close the Main Window?", "Close Main Window");
            //if (confirm == true)
            //{
            //    Application.Exit();
            //}
        }

        private void btnARCharges_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.ARFreight))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.ARFreight pcusatfsmr = new Reporting.ARFreight();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnARPayments_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.ARPayments))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.ARPayments pcusatfsmr = new Reporting.ARPayments();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        void displayItems()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string cashiertransno = Database.getSingleQuery("SalesTransactionSummary", "UserID='" + Login.isglobalUserID + "' AND BranchCode='" + Login.assignedBranch + "' AND isOpen='1' and DateOpen='" + DateTime.Now.ToShortDateString() + "' ", "CashierTransNo");
                string query = "spr_POSCloseTransaction";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranhcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmcashiertransno", cashiertransno);
                com.Parameters.AddWithValue("@parmmachineused", Environment.MachineName);
                com.Parameters.AddWithValue("@parmuserid", Login.isglobalUserID);

                
                com.Parameters.Add("@parmtransdate", SqlDbType.Char,8).Direction = ParameterDirection.Output;
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

                com.Parameters.Add("@parmtotalofcancelleditem", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalofvoiditem", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalofdiscountitem", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalofvatitems", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalofreturneditems", SqlDbType.Money).Direction = ParameterDirection.Output;


                com.Parameters.Add("@parmtotalofscdisc", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalofpwddisc", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalofregdisc", SqlDbType.Money).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmvatablesale", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmvatexemptsale", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmvatamount", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmzeroratedsale", SqlDbType.Money).Direction = ParameterDirection.Output;

                com.Parameters.Add("@parmtotalcashsales", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalcreditsales", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalnetsales", SqlDbType.Money).Direction = ParameterDirection.Output;
                com.Parameters.Add("@parmtotalgrosssales", SqlDbType.Money).Direction = ParameterDirection.Output;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

                POSClosedTransaction pocls = new POSClosedTransaction();
                pocls.txttransactiondate.Text = com.Parameters["@parmtransdate"].Value.ToString();
                pocls.txtcashiertransno.Text = cashiertransno;// lblTransactionIDCashier.Text;
                pocls.txttransactionno.Text = com.Parameters["@parmtransno"].Value.ToString();//lblTransactionIDInc.Text; //get tthe LAST TransactionNo in POSTRansaction Table

                pocls.txtBeginningCash.Text = com.Parameters["@parmbeginningcash"].Value.ToString();

                pocls.txtbeginninginvoice.Text = com.Parameters["@parmbeginsino"].Value.ToString();
                pocls.txtendingsi.Text = com.Parameters["@parmendsino"].Value.ToString();

                pocls.txtbegtransno.Text = com.Parameters["@parmbegintransno"].Value.ToString();
                pocls.txtendtransno.Text = com.Parameters["@parmtransno"].Value.ToString();//lblTransactionIDInc.Text;

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

                pocls.txtvatablesales.Text = com.Parameters["@parmvatablesale"].Value.ToString();
                pocls.txtvatexemptsale.Text = com.Parameters["@parmvatexemptsale"].Value.ToString();
                pocls.txtvatamount.Text = com.Parameters["@parmvatamount"].Value.ToString();
                pocls.txtzeroratedsale.Text = com.Parameters["@parmzeroratedsale"].Value.ToString();
                //pocls.txtnetsalesofvat.Text = com.Parameters["@parmnetsalesofvat"].Value.ToString();

                pocls.txtTotalCashSales.Text = com.Parameters["@parmtotalcashsales"].Value.ToString();
                pocls.txtTotalCreditSales.Text = com.Parameters["@parmtotalcreditsales"].Value.ToString();
                pocls.txtTotalNetSales.Text = com.Parameters["@parmtotalnetsales"].Value.ToString();
                pocls.txttotalgross.Text = com.Parameters["@parmtotalgrosssales"].Value.ToString();

                //Database.displayLocalGrid("select CONVERT(VARCHAR(10), DateOrder, 120) AS Date , DatePart(hh, DateOrder) as Hour , SUM(QtySold) as QtySold , SUM(TotalAmount) AS TotalAmount , COUNT(*) as TotalItems from BatchSalesDetails WHERE CAST(DateOrder as date)='" + DateTime.Now.ToShortDateString() + "' AND TransactionCode='" + lblTransactionIDCashier.Text + "' GROUP BY CONVERT(VARCHAR(10), DateOrder, 120), DatePart(hh, DateOrder) ", pocls.MydataGridView1);

                pocls.ShowDialog(this);
                if (POSClosedTransaction.isdone == true)
                {
                    POSClosedTransaction.isdone = false;
                    pocls.Dispose();
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
                displayItems();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnCloseSalesTransaction_ItemClick(object sender, ItemClickEventArgs e)
        {
            Main m = new Main();
            string transdate = Database.getSingleResultSet("SELECT  dbo.func_ConvertDateTimeToChar('DATE','" + DateTime.Now.ToString() + "')");
            bool isopen = Database.checkifExist("Select isOpen from SalesTransactionSummary WHERE BranchCode='"+Login.assignedBranch+"' and CashierTransNo='"+barStaticCashierTransNo.Caption+"' and TransactionDate='"+transdate+"' and isOpen=1");
           
            if (isopen == true)
            {
                closedTransactions();
            }
            
            else
            {
                XtraMessageBox.Show("You dont have any Open Transaction for this Day!..");
                return;
            }
            
        }

        private void btnENDofDayTrans_ItemClick(object sender, ItemClickEventArgs e)
        {
            string transdate = Database.getSingleResultSet("SELECT dbo.func_ConvertDateTimeToChar('DATE','"+DateTime.Now.ToString()+"') ");
            bool isexist = Database.checkifExist("SELECT TOP(1) BranchCode " +
                "FROM dbo.POSEODMonitoring " +
                "WHERE BranchCode='" + Login.assignedBranch + "' " +
                "AND TransactionDate='" + transdate.Trim() + "' " +
                "and isCashBegin=1 " +
                "and isEndOfDay=0 ");

            bool isoverride = false;
            isoverride = Database.checkifExist("SELECT TOP(1) isnull(isOverride,0) FROM dbo.POSFunctions WHERE FunctionName='ENDOFDAY' AND isOverride=1");
            if (!isoverride)
            {
                if (isexist)
                {
                    POS.POSEndOfDay oiajsd = new POS.POSEndOfDay();
                    oiajsd.ShowDialog(this);
                }
                else
                {
                    XtraMessageBox.Show("You dont have any Transactions for this Day!..");
                    return;
                }
            }
            else
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    if (isexist)
                    {
                        POS.POSEndOfDay oiajsd = new POS.POSEndOfDay();
                        oiajsd.ShowDialog(this);
                    }
                    else
                    {
                        XtraMessageBox.Show("You dont have any Transactions for this Day!..");
                        return;
                    }
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
            
           
        }

        private void btnReceivedSTS_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Orders.ReceivedSTS))
                {
                    form.Activate();
                    return;
                }
            }
            Orders.ReceivedSTS pcusatfsmr = new Orders.ReceivedSTS();
            pcusatfsmr.MdiParent = this;
            pcusatfsmr.Show();
        }

        private void btnViewSTSOrder_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Orders.POForApprovalSTS))
                {
                    form.Activate();
                    return;
                }
            }
            Orders.POForApprovalSTS pfoap = new Orders.POForApprovalSTS();
            pfoap.MdiParent = this;
            pfoap.Show();

        }

        private void btnSalesSummary_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.BIR2550M))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.BIR2550M pfoap = new Reporting.BIR.BIR2550M();
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void barButtonItem19_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.BIR2550M))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.BIR2550M pfoap = new Reporting.BIR.BIR2550M();
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void barButtonItem20_ItemClick_2(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.POSSalesReportSummary))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.POSSalesReportSummary pfoap = new Reporting.BIR.POSSalesReportSummary("A");
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void barButtonItem18_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.EWTReports))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.EWTReports pfoap = new Reporting.BIR.EWTReports();
            pfoap.MdiParent = this;
            pfoap.Show();
        }

        private void barButtonItem23_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BIR.VATInput))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BIR.VATInput pfoap = new Reporting.BIR.VATInput();
            pfoap.MdiParent = this;
            pfoap.Show();
        }
    }
}