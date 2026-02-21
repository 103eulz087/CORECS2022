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
using Microsoft.Win32;
using System.Threading;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using SalesInventorySystem.Classes;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace SalesInventorySystem
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public static string assignedBranch, Fullname,isMaker,isChecker,isglobalPOS,isglobalAccounting, iscashBegin, isglobalUserID,isglobalAdmin,isglobalOfficer,isglobalBranchOfficer,isglobalWarehouseOfficer,isCashier,isglobalApprover,glacctcode,cashinlimit,cashendlimit;
        RegistryKey regkey;
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
        private int passwordctr=0;
        string user = "";

        private static string localhost = "http://itcore-apps.com:1101/";
        private static FileObject file = new FileObject(Application.StartupPath + "\\checkVersion.txt");
        //bool isRequiredToUpdate = false;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == (Keys.O | Keys.Control)) //PAYMENT
            {
                Connection C = new Connection();
                C.ShowDialog(this);
                this.Opacity = 0;
            }
            return functionReturnValue;
        }


        static async Task<int> GetCTRVersion()
        {
            try
            {
                string uriString = $"{ localhost }?version={ file["Company"] }";
                using (var client = new HttpClient())
                {
                    var content = await client.GetStringAsync(uriString).ConfigureAwait(false);
                    Dictionary<string, object> response = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                    if (response.ContainsKey("Version"))
                    {
                        return Convert.ToInt32(response["Version"]);
                    }
                }
            }
            catch { }
            return -1;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //Thread.Sleep(3000);
            //********RYAN VIAJEDOR*****************************************************************************
            try
            {
                int server_version  = GetCTRVersion().Result;
                int client_version = Convert.ToInt32(file["Version"]);
                if (server_version != -1 && client_version < server_version)
                {
                    MessageBox.Show("A New Updates Available\nGet the latest application update now.");
                    //System.IO.File.WriteAllText("loaders.bat", @"taskkill /pid " + Process.GetCurrentProcess().Id + @" /f start " + Application.StartupPath + @"\exeUpdater.exe");
                    System.IO.File.WriteAllText("loaders.bat", @"taskkill /pid " + Process.GetCurrentProcess().Id + @" /f
                    CD """ + Application.StartupPath + @"""
                    START exeUpdater.exe ");
                    Process.Start("loaders.bat");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsMain");
                if (regkey.GetValue("dbconn") == null)
                {
                    Connection C = new Connection();
                    C.lblservername.Text = "Main Server";
                    C.txtconnsettingsname.Text = @"AAITCRE\ConnSettingsMain";
                    C.ShowDialog();
                    this.Opacity = 0;
                    return;
                }
                else
                {
                    // constr = regkey.GetValue("dbconn").ToString();
                    userid = regkey.GetValue("serverid").ToString();
                    serverpassword = regkey.GetValue("serverpassword").ToString();
                    dbname = regkey.GetValue("dbname").ToString();
                    servername = regkey.GetValue("servername").ToString();
                }
                //regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsCloud");
                //if (regkey.GetValue("dbconn") == null)
                //{
                //    Connection C = new Connection();
                //    C.lblservername.Text = "Cloud Server";
                //    C.txtconnsettingsname.Text = @"AAITCRE\ConnSettingsCloud";
                //    C.ShowDialog();
                //    this.Opacity = 0;
                //    return;
                //}
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            //********/RYAN VIAJEDOR****************************************************************************
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
            //validate_user();
        }

        private void get_password()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("Select TOP(1) Password from Users where UserID = '" + txtuserid.Text + "'", con);
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
                XtraMessageBox.Show("Invalid User ID.", "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                        foreach (BarItemLink currentLink in currentGroup.ItemLinks)
                        {
                            mCurrentItem = currentLink.Item;
                            if (currentLink.Item.Name == wordoftexgt)
                            {
                                currentLink.Item.Visibility = BarItemVisibility.Always;
                            }
                        }
                    }
                }
            }
        }
        private void validate_user()
        {   
            SqlConnection con = Database.getConnection();
            con.Open();
            //SqlCommand com = new SqlCommand("Select TOP(1) UserID,Password from dbo.UserMenuAccess where UserID= '" + txtuserid.Text + "' and Password = '" + password + "'", con);
            SqlCommand com = new SqlCommand($"Select TOP(1) * from dbo.Users where UserID='{txtuserid.Text}' and Password='{password}'", con);
            //SqlCommand com = new SqlCommand("Select * from UserMenuAccess2 where UserID= '"+txtuserid.Text+"'  ", con);
            SqlDataReader reader = com.ExecuteReader();


            try
            {

                //string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");
                bool isUserExists = Database.checkifExist($"SELECT TOP(1) UserID FROM dbo.Users WHERE UserID='{txtuserid.Text}' ");
                user = txtuserid.Text;
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

                        if ((txtpassword.Text == decryptedpassword) && txtpassword.Text == "123456")
                        {
                            XtraMessageBox.Show("The System found out that you have a default Password.. Please Change your Password!...");
                            HOForms.ChangePassword pcusatfsmr = new HOForms.ChangePassword();
                            pcusatfsmr.ShowDialog(this);
                            return;
                        }
                        if ((isUserExists) && txtpassword.Text != decryptedpassword) //user exists and wrong password
                        {
                            bool isExists = Database.checkifExist($"SELECT TOP(1) UserID FROM dbo.UsersLocked WHERE UserID='{txtuserid.Text}'");
                            if (!isExists)
                            {
                                passwordctr = 1;
                                Database.ExecuteQuery($"INSERT INTO dbo.UsersLocked VALUES('{txtuserid.Text}','{passwordctr}','{DateTime.Now.ToShortDateString()}')");
                            }
                            else
                            {
                                string getLastAttempt = Database.getSingleQuery("UsersLocked", $"UserID='{txtuserid.Text}'", "LoginAttempts");
                                passwordctr = Convert.ToInt32(getLastAttempt) + 1;
                                Database.ExecuteQuery($"UPDATE dbo.UsersLocked SET LoginAttempts='{passwordctr}',dateLogin='{DateTime.Now.ToShortDateString()}' WHERE UserID='{txtuserid.Text}'");
                            }
                            XtraMessageBox.Show("Wrong Password.", "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            txtuserid.Text = "";
                            txtpassword.Text = "";
                            txtuserid.Focus();
                            return;
                        }
                        else
                        {
                            Database.ExecuteQuery($"DELETE FROM dbo.UsersLocked WHERE UserID='{txtuserid.Text}'");
                            this.Hide();
                            Main m = new Main();
                            m.Show();
                            return;
                        }
                    }
                }
                else
                {
                    if (isUserExists)
                    {
                        bool isExists = Database.checkifExist($"SELECT TOP(1) UserID FROM dbo.UsersLocked WHERE UserID='{txtuserid.Text}'");
                        if (!isExists)
                        {
                            passwordctr = 1;
                            Database.ExecuteQuery($"INSERT INTO dbo.UsersLocked VALUES('{txtuserid.Text}','{passwordctr}','{DateTime.Now.ToShortDateString()}')");
                        }
                        else
                        {
                            string getLastAttempt = Database.getSingleQuery("UsersLocked", $"UserID='{txtuserid.Text}'", "LoginAttempts");
                            passwordctr = Convert.ToInt32(getLastAttempt) + 1;
                            Database.ExecuteQuery($"UPDATE dbo.UsersLocked SET LoginAttempts='{passwordctr}',dateLogin='{DateTime.Now.ToShortDateString()}' WHERE UserID='{txtuserid.Text}'");
                        }
                        XtraMessageBox.Show("Wrong Password.", "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        txtpassword.Focus();
                        txtpassword.SelectionStart = 0;
                        txtpassword.SelectionLength = txtpassword.Text.Length;
                        return;
                    }
                    else
                    {
                        XtraMessageBox.Show("Invalid User ID or Password given.", "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        txtpassword.Focus();
                        txtpassword.SelectionStart = 0;
                        txtpassword.SelectionLength = txtpassword.Text.Length;
                        return;
                    }
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
                    string mark = password;
                    bool isLocked = Database.checkifExist($"SELECT TOP(1) UserID FROM dbo.UsersLocked WHERE UserID='{txtuserid.Text}' AND LoginAttempts >= 3");
                    //bool isipexist = Database.checkifExist("SELECT * FROM BranchIPAddresses WHERE IPAddress='" + HelperFunction.GetLocalIPAddress() + "'");
                    if (txtuserid.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("User ID is required.", "ITCore Solutions Inc.");
                        return;
                    }
                    if (txtpassword.Text.Trim() == "")
                    {
                        XtraMessageBox.Show("Password is required.", "ITCore Solutions Inc.");
                        return;
                    }
                    if(isLocked)
                    {
                        XtraMessageBox.Show("Your Account is Temporarily Locked!..You've reached a maximum of 3 wrong password attempt.. Please Contact IT to Reset your Password..", "ITCore Solutions Inc.");
                        return;
                    }
                   
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
            ip = Database.getSingleData("BranchIPAddresses", "BranchCode", User.getUserBranch(txtuserid.Text),"IPAddress");
            return ip; //output is 192.168.99.143
        }

        String validateMacAddress()
        {
            string macadd = "";
            macadd = Database.getSingleQuery("BranchMacAddresses", "BranchCode='"+User.getUserBranch(txtuserid.Text)+"'", "MacAddress");
            return macadd;
        }

        string validateaddress()
        {
            string str="";
            str = Database.getSingleQuery("BranchMacAddresses", "MacAddress='" + validateMacAddress() + "'", "BranchCode");
            return str;
        }

        private void txtuserid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
                //if (txtuserid.Text.Trim() == "")
                //{
                //    XtraMessageBox.Show("User id is required.", "IT Core Solutions Inc.");
                //    return;
                //}
                //if (txtpassword.Text.Trim() == "")
                //{
                //    XtraMessageBox.Show("Password is required.", "IT Core Solutions Inc.");
                //    return;
                //}
                //get_password();
                txtpassword.Focus();
            }
        }

        private void txtpassword_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Login_Activated(object sender, EventArgs e)
        {
            //for(int i=0;i<=Application.OpenForms.Count-1;i+=1)
            //{
            //    if(!object.ReferenceEquals(Application.OpenForms[i],this))
            //    {
            //        Application.OpenForms[i].Close();
            //    }
            //}
        }
           //foreach (Form form in Application.OpenForms)
//            {
//                if (form.GetType() == typeof(Login))
//                {
//                    form.Activate();
//                    return;
//                }
//}
//Application.Run(new Login());

        private void txtuserid_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click_1(object sender, EventArgs e)
        {

        }


        
    }
}