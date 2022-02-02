using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using DevExpress.XtraBars;
//using DevExpress.XtraBars.Ribbon;
using Microsoft.Win32;
using DevExpress.XtraEditors;

namespace SalesInventorySystem
{
    public partial class LoginOrig : Form
    {
        public static string assignedBranch, Fullname, isMaker, isChecker, isglobalPOS, isglobalAccounting, iscashBegin, isglobalUserID, isglobalAdmin, isglobalOfficer, isglobalBranchOfficer, isglobalWarehouseOfficer, isCashier, isglobalApprover, glacctcode, cashinlimit, cashendlimit;
        //RegistryKey regkey;
        string password;
        string encryptedpassword;
        string decryptedpassword;
        //string strmenuInventory;
        //String constr;
        public static string userid;
        public static string serverpassword;
        public static string servername;
        public static string dbname;
        public static string connsettings;

        string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");
        public LoginOrig()
        {
            InitializeComponent();
        }
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    bool functionReturnValue = false;
        //    if (keyData == (Keys.O | Keys.Control)) //PAYMENT
        //    {
        //        Connection C = new Connection();
        //        C.ShowDialog(this);
        //        this.Opacity = 0;
        //    }
        //    return functionReturnValue;
        //}

        private void LoginOrig_Load(object sender, EventArgs e)
        {
            //try
            //{

            //    regkey = Registry.CurrentUser.CreateSubKey(@"Enzo\ConnSettings");
            //    if (regkey.GetValue("dbconn") == null)
            //    {
            //        Connection C = new Connection();
            //        C.ShowDialog();
            //        this.Opacity = 0;
            //        return;
            //    }
            //    else
            //    {
            //        constr = regkey.GetValue("dbconn").ToString();
            //        userid = regkey.GetValue("serverid").ToString();
            //        serverpassword = regkey.GetValue("serverpassword").ToString();
            //        dbname = regkey.GetValue("dbname").ToString();
            //        servername = regkey.GetValue("servername").ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtuserid.Text.Trim() == "")
            {
                XtraMessageBox.Show("User id is required.");
                return;
            }
            if (txtpassword.Text.Trim() == "")
            {
                XtraMessageBox.Show("Password is required.");
                return;
            }
            get_password();
        }
        private void get_password()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("Select Password from Users where UserID = '" + txtuserid.Text + "'", con);
            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        password = reader["Password"].ToString();
                        decrypt_password();
                        return;
                    }
                }
                XtraMessageBox.Show("Invalid user id or password given.", "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                txtpassword.Focus();
                txtpassword.SelectionStart = 0;
                txtpassword.SelectionLength = txtpassword.Text.Length;
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

        private void decrypt_password()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("declare @pwd varchar(50) exec master..xp_aes_decrypt '" + password + "','0123456789ABCDEF0123456789ABCDEF',@pwd output select @pwd result", con);
            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        decryptedpassword = reader["result"].ToString();
                        validate_user();
                    }
                }
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
        private void encrypt()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("exec master..xp_aes_encrypt '" + txtpassword.Text + "','0123456789ABCDEF0123456789ABCDEF'", con);
            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        encryptedpassword = reader["result"].ToString();
                        validate_user();
                    }
                }
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
        //public void readMenu(string strMenu, RibbonPage currentPage)
        //{
        //    if (strMenu == "<empty>")
        //    {
        //        currentPage.Visible = false;
        //        return;
        //    }
        //    if (String.IsNullOrEmpty(strMenu))
        //    {
        //        currentPage.Visible = false;
        //        return;
        //    }
        //    BarItem mCurrentItem = default(BarItem);
        //    string wholefile = null;
        //    string[] linedata = null;
        //    string[] fielddata = null;
        //    wholefile = strMenu;
        //    //linedata = Regex.Split(wholefile, Environment.NewLine);
        //    linedata = wholefile.Split('\n');
        //    foreach (string lineoftext in linedata)
        //    {
        //        fielddata = lineoftext.Split('|');
        //        foreach (string wordoftexgt in fielddata)
        //        {
        //            foreach (RibbonPageGroup currentGroup in currentPage.Groups)
        //            {
        //                foreach (BarItemLink currentLink in currentGroup.ItemLinks)
        //                {
        //                    mCurrentItem = currentLink.Item;
        //                    if (currentLink.Item.Name == wordoftexgt)
        //                    {
        //                        currentLink.Item.Visibility = BarItemVisibility.Always;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        private void validate_user()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("Select * from Users where UserID= '" + txtuserid.Text + "' and Password = '" + password + "'", con);
            //SqlCommand com = new SqlCommand("Select * from UserMenuAccess2 where UserID= '"+txtuserid.Text+"'  ", con);
            SqlDataReader reader = com.ExecuteReader();

            try
            {

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        isglobalAdmin = reader["isAdmin"].ToString();
                        Fullname = reader["FullName"].ToString();
                        isglobalOfficer = reader["isGlobalOfficer"].ToString();
                        isglobalBranchOfficer = reader["isBranchOfficer"].ToString();
                        isglobalWarehouseOfficer = reader["isWarehouseOfficer"].ToString();
                        isMaker = reader["isMaker"].ToString();
                        isChecker = reader["isChecker"].ToString();
                        isCashier = reader["isCashier"].ToString();
                        isglobalApprover = reader["isApprover"].ToString();
                        isglobalUserID = reader["UserID"].ToString();
                        assignedBranch = reader["AssignedBranch"].ToString();
                        cashinlimit = reader["CashInLimit"].ToString();
                        cashendlimit = reader["CashEndLimit"].ToString();
                        glacctcode = reader["GLAccount"].ToString();
                        isglobalAccounting = reader["isAccounting"].ToString();

                        if (txtpassword.Text != decryptedpassword)
                        {
                            XtraMessageBox.Show("Invalid user id or password given.", "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            txtuserid.Focus();
                            return;
                        }
                        if (company != "JFC")
                        {
                            if (assignedBranch != "888")
                            {

                                this.Hide();
                                MainBranch mb = new MainBranch();
                                mb.Show();
                                return;
                            }
                            else
                            {
                                this.Hide();
                                Main m = new Main();
                                m.Show();
                                return;
                            }
                        }
                        else
                        {
                            this.Hide();
                            Main m = new Main();
                            m.Show();
                            return;
                        }

                    }
                }
                else
                {
                    XtraMessageBox.Show("Invalid user id or password given.", "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    txtpassword.Focus();
                    txtpassword.SelectionStart = 0;
                    txtpassword.SelectionLength = txtpassword.Text.Length;
                    return;
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

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    //bool isipexist = Database.checkifExist("SELECT * FROM BranchIPAddresses WHERE IPAddress='" + HelperFunction.GetLocalIPAddress() + "'");
                    if (txtuserid.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("User id is required.", "ITCore Solutions Inc.");
                        return;
                    }
                    if (txtpassword.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Password is required.", "ITCore Solutions Inc.");
                        return;
                    }
                    //if (!isipexist)
                    //{
                    //    XtraMessageBox.Show("Branch IPAddress not Assigned To Branch", "SPIRE");
                    //    return;
                    //}
                    //if (HelperFunction.GetLocalIPAddress() Database.ex)
                    //{
                    //    XtraMessageBox.Show("Mac Address not Assigned To Branch", "SPIRE");
                    //    return;
                    //}
                    get_password();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        String getBranchAssignedIP()
        {
            string ip = "";
            ip = Database.getSingleData("BranchIPAddresses", "BranchCode", User.getUserBranch(txtuserid.Text), "IPAddress");
            return ip; //output is 192.168.99.143
        }

        String validateMacAddress()
        {
            string macadd = "";
            macadd = Database.getSingleQuery("BranchMacAddresses", "BranchCode='" + User.getUserBranch(txtuserid.Text) + "'", "MacAddress");
            return macadd;
        }

        string validateaddress()
        {
            string str = "";
            str = Database.getSingleQuery("BranchMacAddresses", "MacAddress='" + validateMacAddress() + "'", "BranchCode");
            return str;
        }

        private void txtuserid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtuserid.Text.Trim() == "")
                {
                    XtraMessageBox.Show("User id is required.", "IT Core Solutions Inc.");
                    return;
                }
                if (txtpassword.Text.Trim() == "")
                {
                    XtraMessageBox.Show("Password is required.", "IT Core Solutions Inc.");
                    return;
                }
                get_password();
            }
        }
    }
}
