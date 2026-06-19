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
using System.Net.NetworkInformation;
using SalesInventorySystem.V5;

namespace SalesInventorySystem
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public static string compname, assignedBranch, Fullname, isMaker, isChecker, isglobalPOS, isglobalAccounting, iscashBegin, isglobalUserID, isglobalAdmin, isglobalOfficer, isglobalBranchOfficer, isglobalWarehouseOfficer, isCashier, isglobalApprover, glacctcode, cashinlimit, cashendlimit;
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
        private int passwordctr = 0;
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
            if (keyData == Keys.Escape) //PAYMENT
            {
                btnclose.PerformClick();
            }
            return functionReturnValue;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void Login_Load(object sender, EventArgs e)
        {

            //FOR STAND ALONE POS ONLY ENZOSTORE,VROSSSTORE, KRAFT
            if (GlobalConfig.Token== "MTQ2NzgwNjAz" || GlobalConfig.Token == "ODM1NTI0ODYz" 
                || GlobalConfig.Token == "1234567890XX"
                || GlobalConfig.Token == "ONEzNTE4NjEx"
                || GlobalConfig.Token == "NjQwOTg4MzU1") 
            {
                Database.RunLocalDatabaseMigrations();
          
            }
            if(GlobalConfig.Token== "ODM1NTI0ODYz" || GlobalConfig.Token == "1234567890XX")//VROSS STORE
            {
                Database.ExecuteQuery("UPDATE POSType set isAutoSystemDeduct=1");
            }
            tryCheckUpdate(); //#tryCheckUpdateV1();
            labelversion.Text= HelperFunction.readFileVersion();

           
            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(@"AAITCRE\ConnSettingsMain");
                //////////////////////////////////////////////////////
                ///////TEMPORARY ONLY FOR MIGRATION PURPOSES
                /////////////////////////////////////////////////////////
                string migratedStamp = regkey.GetValue("db_migrated_to")?.ToString();
                string targetStamp = "2026-NEWDB";

                //FOR STAND ALONE POS ONLY ENZOSTORE,VROSSSTORE, KRAFT
                if (GlobalConfig.Token == "MTQ2NzgwNjAz" 
                    || GlobalConfig.Token == "ODM1NTI0ODYz"
                    || GlobalConfig.Token == "ONEzNTE4NjEx"
                    || GlobalConfig.Token == "NjQwOTg4MzU1")
                {
                    migratedStamp = "";
                    targetStamp = "";

                    //WRITE REGISTRY INTO HKEYUSER/AAITCRE/CONSETTINSSERVER
                    if(GlobalConfig.Token == "MTQ2NzgwNjAz") //ENZO STORE
                    {
                        ConnRegistry.SetTargetConnSettingsServer(
                               serverNameWithPort: "erp.itcoreapps.com,4281",
                               dbName: "CORECSERP_001",
                               userId: "erp001_user",
                               password: "$tr0ngP@ssw0rd2026!"
                        );
                    }
                    else if(GlobalConfig.Token == "ODM1NTI0ODYz") //VROSS STORE
                    {
                        ConnRegistry.SetTargetConnSettingsServer(
                               serverNameWithPort: "erp.itcoreapps.com,4281",
                               dbName: "CORECSERP_003",
                               userId: "erp003_user",
                               password: "$tr0ngP@ssw0rd2026003!"
                        );
                    }
                    else if(GlobalConfig.Token == "NjQwOTg4MzU1") //KRAFT STORE
                    {
                        ConnRegistry.SetTargetConnSettingsServer(
                               serverNameWithPort: "erp.itcoreapps.com,4281",
                               dbName: "CORECSERP_004",
                               userId: "erp004_user",
                               password: "$tr0ngP@ssw0rd2026004!"
                        );
                    }
                    else if(GlobalConfig.Token == "1234567890XX") //ITCORE STORE
                    {
                        ConnRegistry.SetTargetConnSettingsServer(
                               serverNameWithPort: "erp.itcoreapps.com,4281",
                               dbName: "CORECSERP_001",
                               userId: "erp001_user",
                               password: "$tr0ngP@ssw0rd2026!"
                        );
                    }
                    else if(GlobalConfig.Token == "ONEzNTE4NjEx") //ONELOVE STORE
                    {
                        ConnRegistry.SetTargetConnSettingsServer(
                               serverNameWithPort: "erp.itcoreapps.com,4281",
                               dbName: "CORECSERP_001",
                               userId: "erp001_user",
                               password: "$tr0ngP@ssw0rd2026!"
                        );
                    }
                   

                    ConnRegistry.Set("db_migrated_to", targetStamp);
                }

                //VROSSACCTG, VROSSCORP, VROSSINV, WRITE REGISTRY TO CONSETTINGSMAINLOCAL
                if (GlobalConfig.Token == "ATk1NjU1NTU1" 
                    || GlobalConfig.Token == "MzEyNzU2Njk1" 
                                   || GlobalConfig.Token == "iTAyNjU5Mjk5")
                {
                    migratedStamp = "";
                    targetStamp = "";
                    ConnRegistry.SetTargetConnSettingsServer(
                        serverNameWithPort: "erp.itcoreapps.com,4281",
                        dbName: "CORECSERP_003",
                        userId: "erp003_user",
                        password: "$tr0ngP@ssw0rd2026003!"
                    );

                    ConnRegistry.Set("db_migrated_to", targetStamp);
                }
                if (regkey.GetValue("dbconn") == null)
                {
                    Connection C = new Connection();
                    C.lblservername.Text = "Main Server";
                    C.txtconnsettingsname.Text = @"AAITCRE\ConnSettingsMain";
                    C.ShowDialog();
                    this.Opacity = 0;
                    return;
                }

                //NOT APPLICABLE FOR POS, SINCE POS SETUP IS ASSIGNED AS EMPTY VARIABLE OF 
                //migratedStamp = "";
                //targetStamp = "";
                if (!string.Equals(migratedStamp, targetStamp, StringComparison.OrdinalIgnoreCase))
                {
                    // set your NEW DB details here (or fetch from a secure source)

                    //TARGET TO CONSETTINGSMAIN
                    if((GlobalConfig.Token == "ODU2NDE4OTA3" //ENZO
                        || GlobalConfig.Token == "MzMyODgyODc0" //ENZOCOMM
                        || GlobalConfig.Token == "HTQwNzExMTYx" //ENZOHRI
                        || GlobalConfig.Token == "MjYxMjQ3MTkz"  //ENZOKIM
                        || GlobalConfig.Token == "OTQ1NDczOTYy"  //ENZOKIM
                        || GlobalConfig.Token == "B2Y2NjYxMzY3"  //ENZOb2
                        || GlobalConfig.Token == "Nzc0Njk4NjY0") && String.IsNullOrEmpty(migratedStamp)) //ENZOSTAGING
                    {
                        //TARGET TO CONSETTINGSMAIN
                        ConnRegistry.SetTarget(
                               serverNameWithPort: "erp.itcoreapps.com,4281",
                               dbName: "CORECSERP_001",
                               userId: "erp001_user",
                               password: "$tr0ngP@ssw0rd2026!"
                           );
                    }
                    else if ((GlobalConfig.Token == "ATk1NjU1NTU1" //VROSSACCTG
                        || GlobalConfig.Token == "MzEyNzU2Njk1" //VROSSCORP
                        || GlobalConfig.Token == "iTAyNjU5Mjk5") && String.IsNullOrEmpty(migratedStamp)) //VROSSINV
                    {
                        //TARGET TO CONSETTINGSMAIN
                        ConnRegistry.SetTarget(
                               serverNameWithPort: "erp.itcoreapps.com,4281",
                               dbName: "CORECSERP_003",
                               userId: "erp003_user",
                               password: "$tr0ngP@ssw0rd2026003!"
                           );
                    }
                    

                    ConnRegistry.Set("db_migrated_to", targetStamp);

                    // Optional: reload variables you cache in Login.cs
                    userid = ConnRegistry.Get("serverid");
                    serverpassword = ConnRegistry.Get("serverpassword");
                    dbname = ConnRegistry.Get("dbname");
                    servername = ConnRegistry.Get("servername");

                    // Optional: Restart if other parts cache the old connection early
                    // Application.Restart();
                    // return;
                }

                userid = regkey.GetValue("serverid").ToString();
                serverpassword = regkey.GetValue("serverpassword").ToString();
                dbname = regkey.GetValue("dbname").ToString();
                servername = regkey.GetValue("servername").ToString();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }

        }

        static async Task<int> GetCTRVersion()
        {
            try
            {
                string urlChecker = file["VersionCheckerUrl"];
                using (var client = new HttpClient())
                {
                    var content = await client.GetStringAsync(urlChecker).ConfigureAwait(false);
                    return Convert.ToInt32(content);
                }
            }
            catch { }
            return -1;
        }
        private void tryCheckUpdate()
        {
            try
            {
                if (String.IsNullOrEmpty(file["Version"])) return;
                int client_version = Convert.ToInt32(file["Version"]);
                int server_version = GetCTRVersion().Result;
                if (server_version != -1 && client_version < server_version)
                {
                    if (!String.IsNullOrEmpty(file["DownloadUrlFormat"]))
                    {
                        file["DownloadUrl"] = file["DownloadUrlFormat"].Replace("file.zip", "file-v" + server_version + ".zip");
                        file.Update();
                    }
                    MessageBox.Show("A New Updates Available\nGet the latest application update now.");
                    string batCmdLaunch = $"bat_{ DateTime.Now.ToString("yyyyMMdd.HHmmss") }.bat";
                    System.IO.File.WriteAllText(batCmdLaunch, @"
                        @echo off
                        taskkill /pid " + Process.GetCurrentProcess().Id + @" /f
                        START exeUpdater.exe 
                        del ""%~f0""
                        exit /b
                    ");
                    ProcessStart(batCmdLaunch).WaitForExit();
                    Application.Exit();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //private async void tryCheckUpdateV1()
        //{
        //    try
        //    {
        //        // 2. Await the new async method! The UI will stay smooth while this runs.
        //        int server_version = await getCTRVersionAsAsync(file["Company"].ToString());
        //        int client_version = Convert.ToInt32(file["Version"]);

        //        // If server_version is -1, it means the internet was down, so we just skip this safely
        //        if (server_version != -1 && client_version < server_version)
        //        {
        //            MessageBox.Show("A New Update is Available\nGet the latest application update now.");

        //            // Generate a bulletproof update script
        //            string batScript = $@"
        //                            @echo off
        //                            taskkill /pid {Process.GetCurrentProcess().Id} /f
        //                            timeout /t 2 /nobreak > NUL
        //                            cd /d ""{Application.StartupPath}""
        //                            start """" ""exeUpdater.exe""
        //                            del ""%~f0""
        //                            ";
        //            System.IO.File.WriteAllText("loaders.bat", batScript);

        //            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo()
        //            {
        //                FileName = "loaders.bat",
        //                WorkingDirectory = Application.StartupPath,
        //                UseShellExecute = true
        //            };
        //            Process.Start(psi);

        //            return; // Stop loading the login screen since we are updating!
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}

        public void SucessPayment(V5Pay form)
        {
            MessageBox.Show("Success Payment from receipt: " + JsonConvert.SerializeObject(form.receipt));
        }
        public void FailPayment(V5Pay form, string message)
        {
            MessageBox.Show("Fail Payment: " + message);
            //#MessageBox.Show("Data from receipt " + JsonConvert.SerializeObject(form.receipt));
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            //Database.ExecuteQuery("UPDATE dbo.POSType SET linkedServerName='1', DataUploading=1");
            ////start loading
            //HelperFunction.ShowWaitAndDisplay("PLEASE WAIT","LOADING DATA",3000);
            //var referenceId = "001-POS1-000000000000000007"; 
            //var amount = 2.22;
            //var form = new V5Pay(this, referenceId, amount); //r form = new V5Pay(this); // POS
            //await form.InitializeWebView();
            //await form.CreatePayment();
            ////hide loading
            //if (!form.failed) form.Show(this); //form.ShowDialog();
            //return;
            //// 1. Instant Network Check
            //if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            //{
            //    MessageBox.Show("No network connection detected. Please check your Wi-Fi or cable.", "No Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            // 2. Basic UI Validation

            if (string.IsNullOrWhiteSpace(txtuserid.Text))
            {
                XtraMessageBox.Show("User ID is required.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                XtraMessageBox.Show("Password is required.");
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "Connecting...";
            Cursor = Cursors.WaitCursor;

            try
            {
                string loginStatus = await ProcessLoginAsync(txtuserid.Text.Trim(), txtpassword.Text);

                if (loginStatus == "SUCCESS")
                {
                    this.Hide();
                    Main m = new Main();
                    m.Show();
                }
                else if (loginStatus == "DEFAULT_PASSWORD")
                {
                    XtraMessageBox.Show("The System found out that you have a default Password. Please Change your Password!");
                    HOForms.ChangePassword frm = new HOForms.ChangePassword();
                    frm.ShowDialog(this);
                }
                else if (loginStatus == "PASSWORD_RESET_REQUIRED")
                {
                    //XtraMessageBox.Show("Your account needs a password reset. Please set a new password to continue.");
                    BigAlert.Show("RESET PASSWORD", "Your account needs a password reset. Please set a new password to continue.", MessageBoxIcon.Warning);
                    HOForms.ChangePassword frm = new HOForms.ChangePassword();
                    frm.ShowDialog(this);
                }
                else if (loginStatus == "DATABASE_ERROR")
                {
                    XtraMessageBox.Show("Cannot connect to the database right now. Please try again or contact IT.");
                }
                else if (loginStatus == "UNEXPECTED_ERROR")
                {
                    XtraMessageBox.Show("Unexpected error occurred. Please contact IT.");
                }
                else
                {
                    XtraMessageBox.Show(loginStatus, "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpassword.Focus();
                    txtpassword.SelectAll();
                }
            }
            catch (Exception ex)
            {
                // If ProcessLoginAsync throws (instead of returning codes), you'll land here
                XtraMessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
                Cursor = Cursors.Default;
            }


        }
        private async Task<string> ProcessLoginAsync(string userId, string inputPassword)
        {

            try
             {
                using (SqlConnection con = Database.getConnection())
                {
                    // Fail fast
                    var builder = new SqlConnectionStringBuilder(con.ConnectionString) { ConnectTimeout = 3 };
                    con.ConnectionString = builder.ConnectionString;

                    await con.OpenAsync();

                    byte[] passwordHash = null;
                    byte[] passwordSalt = null;
                    int iterations = 0;
                    bool mustChangePassword = false;

                    // --- PHASE 1: Fetch user row (avoid SELECT *)
                    const string sql = @"
                    SELECT TOP(1)
                        UserID, FullName, isAdmin, isGlobalOfficer, isBranchOfficer, isWarehouseOfficer,
                        isMaker, isChecker, isCashier, isApprover, AssignedBranch, CashInLimit, CashEndLimit,
                        GLAccount, isAccounting,
                        PasswordHash, PasswordSalt, PasswordIterations, MustChangePassword
                    FROM dbo.Users
                    WHERE UserID = @UserID;";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (!await reader.ReadAsync())
                            {
                                return "Invalid User ID or Password given.";
                            }

                            // Your globals
                            user = userId;
                            Fullname = reader["FullName"].ToString();
                            isglobalAdmin = reader["isAdmin"].ToString();
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

                            mustChangePassword = reader["MustChangePassword"] != DBNull.Value && (bool)reader["MustChangePassword"];

                            // New hash fields
                            passwordHash = reader["PasswordHash"] == DBNull.Value ? null : (byte[])reader["PasswordHash"];
                            passwordSalt = reader["PasswordSalt"] == DBNull.Value ? null : (byte[])reader["PasswordSalt"];
                            iterations = reader["PasswordIterations"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PasswordIterations"]);
                        }
                    }

                    // Optional: keep your existing company lookup if you need it
                    // compname = Database.getSingleQuery("CompanyProfile", "CompanyName='JFC'", "CompanyName");

                    // --- PHASE 2: Verify using PBKDF2 hash ---
                    if (passwordHash == null || passwordSalt == null || iterations <= 0)
                    {
                        // Not migrated yet => force reset
                        return "PASSWORD_RESET_REQUIRED";
                    }

                    bool ok = PasswordHasher.VerifyPassword(inputPassword, passwordSalt, iterations, passwordHash);

                    // --- PHASE 3: Lockout handling + return ---
                    if (ok)
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.UsersLocked WHERE UserID = @UserID", con))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        if (mustChangePassword)   return "DEFAULT_PASSWORD";
                        return "SUCCESS";
                    }
                    else
                    {
                        string lockQuery = @"
IF EXISTS (SELECT 1 FROM dbo.UsersLocked WHERE UserID = @UserID)
    UPDATE dbo.UsersLocked SET LoginAttempts = LoginAttempts + 1, dateLogin = @Date WHERE UserID = @UserID;
ELSE
    INSERT INTO dbo.UsersLocked (UserID, LoginAttempts, dateLogin) VALUES (@UserID, 1, @Date);";

                        using (SqlCommand cmd = new SqlCommand(lockQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userId);
                            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToShortDateString());
                            await cmd.ExecuteNonQueryAsync();
                        }

                        return "Wrong Password.";
                    }
                }
            }
            catch (SqlException)
            {
                // You can log ex here if you want
                return "DATABASE_ERROR";
            }
            catch (Exception)
            {
                return "UNEXPECTED_ERROR";
            }

            // Fallback (should never be hit, but guarantees CS0161 never happens)
            // return "UNEXPECTED_ERROR";
        }
        //private async Task<string> ProcessLoginAsync(string userId, string inputPassword)
        //{
        //    using (SqlConnection con = Database.getConnection())
        //    {
        //        // Enforce the 3-second Fail Fast rule
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(con.ConnectionString) { ConnectTimeout = 3 };
        //        con.ConnectionString = builder.ConnectionString;
        //        await con.OpenAsync();

        //        string encryptedDbPassword = null;

        //        // --- PHASE 1: Fetch the User Data ---
        //        using (SqlCommand cmd = new SqlCommand("SELECT TOP(1) * FROM dbo.Users WHERE UserID = @UserID", con))
        //        {
        //            cmd.Parameters.AddWithValue("@UserID", userId);
        //            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    // Grab the encrypted password
        //                    encryptedDbPassword = reader["Password"].ToString();

        //                    // Assign all your global variables here!
        //                    user = userId;
        //                    Fullname = reader["FullName"].ToString();
        //                    isglobalAdmin = reader["isAdmin"].ToString();
        //                    isglobalOfficer = reader["isGlobalOfficer"].ToString();
        //                    isglobalBranchOfficer = reader["isBranchOfficer"].ToString();
        //                    isglobalWarehouseOfficer = reader["isWarehouseOfficer"].ToString();
        //                    isMaker = reader["isMaker"].ToString();
        //                    isChecker = reader["isChecker"].ToString();
        //                    isCashier = reader["isCashier"].ToString();
        //                    isglobalApprover = reader["isApprover"].ToString();
        //                    isglobalUserID = reader["UserID"].ToString();
        //                    assignedBranch = reader["AssignedBranch"].ToString();
        //                    cashinlimit = reader["CashInLimit"].ToString();
        //                    cashendlimit = reader["CashEndLimit"].ToString();
        //                    glacctcode = reader["GLAccount"].ToString();
        //                    isglobalAccounting = reader["isAccounting"].ToString();
        //                    // ... (Add the rest of your variable assignments here: isMaker, isChecker, etc.)
        //                }
        //                else
        //                {
        //                    return "Invalid User ID or Password given."; // User doesn't exist
        //                }
        //            }
        //        }
        //        compname = Database.getSingleQuery("CompanyProfile", "CompanyName='JFC'", "CompanyName");
        //        // --- PHASE 2: Decrypt the Password ---
        //        // --- PHASE 2: Decrypt the Password ---
        //        string decryptedPassword = null;

        //        // We inject the encryptedDbPassword directly to ensure SQL treats it as VARCHAR,
        //        // exactly matching your original code's behavior to keep the decryption happy!
        //        string decryptQuery = $@"
        //    DECLARE @pwd varchar(50); 
        //    EXEC master..xp_aes_decrypt '{encryptedDbPassword}', '0123456789ABCDEF0123456789ABCDEF', @pwd OUTPUT; 
        //    SELECT @pwd AS result;";

        //        using (SqlCommand cmd = new SqlCommand(decryptQuery, con))
        //        {
        //            var result = await cmd.ExecuteScalarAsync();
        //            decryptedPassword = result?.ToString();
        //        }

        //        // --- PHASE 3: Validate and Handle Locks ---
        //        if (inputPassword == decryptedPassword)
        //        {
        //            // SUCCESS: Clear any lockout records
        //            using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.UsersLocked WHERE UserID = @UserID", con))
        //            {
        //                cmd.Parameters.AddWithValue("@UserID", userId);
        //                await cmd.ExecuteNonQueryAsync();
        //            }

        //            if (inputPassword == "123456") return "DEFAULT_PASSWORD";
        //            return "SUCCESS";
        //        }
        //        else
        //        {
        //            // FAILED: Update the lockout table (using an efficient IF EXISTS script)
        //            string lockQuery = @"
        //        IF EXISTS (SELECT 1 FROM dbo.UsersLocked WHERE UserID = @UserID)
        //            UPDATE dbo.UsersLocked SET LoginAttempts = LoginAttempts + 1, dateLogin = @Date WHERE UserID = @UserID;
        //        ELSE
        //            INSERT INTO dbo.UsersLocked (UserID, LoginAttempts, dateLogin) VALUES (@UserID, 1, @Date);";

        //            using (SqlCommand cmd = new SqlCommand(lockQuery, con))
        //            {
        //                cmd.Parameters.AddWithValue("@UserID", userId);
        //                cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToShortDateString());
        //                await cmd.ExecuteNonQueryAsync();
        //            }

        //            return "Wrong Password.";
        //        }
        //    }
        //}

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
        
        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    buttonLogin.PerformClick();
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
                txtpassword.Focus();
            }
        }

        private void txtpassword_EditValueChanged(object sender, EventArgs e)
        {

        }
        

        private void txtuserid_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private static Process ProcessStart(string batLocation)
        {
            //var process = Process.Start(batCmd);
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = batLocation;
            p.Start();
            return p;
        }
    }
}