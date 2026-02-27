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
        

        // 1. Change the return type to Task<int> and add the 'async' keyword
        public static async Task<int> getCTRVersionAsAsync(String name)
        {
            // 1. INSTANT CHECK: Is the computer even connected to a network?
            // If there is no internet/network, instantly return -1 and skip the update check!
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return -1;
            }

            int num1 = -1;

            try
            {
                using (SqlConnection connection = Database.getConnection(@"Enzo\ConnSettingsUpdater"))
                {
                    // 2. THE 3-SECOND RULE (Fail Fast)
                    // By default, SQL waits 15 to 30 seconds. We change it to 3 seconds.
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connection.ConnectionString);
                    builder.ConnectTimeout = 3;
                    connection.ConnectionString = builder.ConnectionString;

                    // Try to connect. If the server is unreachable, it will fail in exactly 3 seconds.
                    await connection.OpenAsync();

                    string query = "SELECT TOP 1 CAST(Versions as int) AS CC FROM UploaderLookUp WHERE Company = @CompanyName;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CompanyName", name);

                        using (SqlDataReader sqlDataReader = await command.ExecuteReaderAsync())
                        {
                            if (sqlDataReader != null && await sqlDataReader.ReadAsync())
                            {
                                num1 = Convert.ToInt32(sqlDataReader["CC"]);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 3. SILENT FAIL
                // If the 3 seconds run out, or the server is down, it safely lands here.
                // It returns -1, skipping the update and letting the user log in normally!
            }

            return num1;
        }

        private async void Login_Load(object sender, EventArgs e)
        {
            try
            {
                // 2. Await the new async method! The UI will stay smooth while this runs.
                int server_version = await getCTRVersionAsAsync(file["Company"].ToString());
                int client_version = Convert.ToInt32(file["Version"]);

                // If server_version is -1, it means the internet was down, so we just skip this safely
                if (server_version != -1 && client_version < server_version)
                {
                    MessageBox.Show("A New Update is Available\nGet the latest application update now.");

                    // Generate a bulletproof update script
                    string batScript = $@"
                                    @echo off
                                    taskkill /pid {Process.GetCurrentProcess().Id} /f
                                    timeout /t 2 /nobreak > NUL
                                    cd /d ""{Application.StartupPath}""
                                    start """" ""exeUpdater.exe""
                                    del ""%~f0""
                                    ";
                    System.IO.File.WriteAllText("loaders.bat", batScript);

                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = "loaders.bat",
                        WorkingDirectory = Application.StartupPath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);

                    return; // Stop loading the login screen since we are updating!
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            // 1. Instant Network Check
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No network connection detected. Please check your Wi-Fi or cable.", "No Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            // 3. Lock the UI
            btnLogin.Enabled = false;
            btnLogin.Text = "Connecting...";
            Cursor = Cursors.WaitCursor;

            try
            {
                // 4. Run the single, unified database check!
                string loginStatus = await ProcessLoginAsync(txtuserid.Text.Trim(), txtpassword.Text);

                // 5. Handle the result cleanly
                if (loginStatus == "SUCCESS")
                {
                    this.Hide();
                    Main m = new Main();
                    m.Show();
                }
                else if (loginStatus == "DEFAULT_PASSWORD")
                {
                    XtraMessageBox.Show("The System found out that you have a default Password. Please Change your Password!");
                    HOForms.ChangePassword pcusatfsmr = new HOForms.ChangePassword();
                    pcusatfsmr.ShowDialog(this);
                }
                else
                {
                    // Fails (Wrong Password or Invalid User)
                    XtraMessageBox.Show(loginStatus, "IT Core Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtpassword.Focus();
                    txtpassword.SelectAll();
                }
            }
            catch (SqlException ex)
            {
                // Temporarily show the EXACT error from SQL Server so we can debug it
                MessageBox.Show("SQL Error Details:\n\n" + ex.Message, "Debugging SQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Unlock UI
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
                Cursor = Cursors.Default;
            }

        }
        
        private async Task<string> ProcessLoginAsync(string userId, string inputPassword)
        {
            using (SqlConnection con = Database.getConnection())
            {
                // Enforce the 3-second Fail Fast rule
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(con.ConnectionString) { ConnectTimeout = 3 };
                con.ConnectionString = builder.ConnectionString;
                await con.OpenAsync();

                string encryptedDbPassword = null;

                // --- PHASE 1: Fetch the User Data ---
                using (SqlCommand cmd = new SqlCommand("SELECT TOP(1) * FROM dbo.Users WHERE UserID = @UserID", con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            // Grab the encrypted password
                            encryptedDbPassword = reader["Password"].ToString();

                            // Assign all your global variables here!
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
                            // ... (Add the rest of your variable assignments here: isMaker, isChecker, etc.)
                        }
                        else
                        {
                            return "Invalid User ID or Password given."; // User doesn't exist
                        }
                    }
                }

                // --- PHASE 2: Decrypt the Password ---
                // --- PHASE 2: Decrypt the Password ---
                string decryptedPassword = null;

                // We inject the encryptedDbPassword directly to ensure SQL treats it as VARCHAR,
                // exactly matching your original code's behavior to keep the decryption happy!
                string decryptQuery = $@"
            DECLARE @pwd varchar(50); 
            EXEC master..xp_aes_decrypt '{encryptedDbPassword}', '0123456789ABCDEF0123456789ABCDEF', @pwd OUTPUT; 
            SELECT @pwd AS result;";

                using (SqlCommand cmd = new SqlCommand(decryptQuery, con))
                {
                    var result = await cmd.ExecuteScalarAsync();
                    decryptedPassword = result?.ToString();
                }

                // --- PHASE 3: Validate and Handle Locks ---
                if (inputPassword == decryptedPassword)
                {
                    // SUCCESS: Clear any lockout records
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.UsersLocked WHERE UserID = @UserID", con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    if (inputPassword == "123456") return "DEFAULT_PASSWORD";
                    return "SUCCESS";
                }
                else
                {
                    // FAILED: Update the lockout table (using an efficient IF EXISTS script)
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


        
    }
}