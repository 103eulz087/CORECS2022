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

namespace SalesInventorySystem.POS
{
    public partial class POSCloseTransactionAuthentication : DevExpress.XtraEditors.XtraForm
    {
        public static bool isconfirmedLogin = false;
        public static string assignedBranch, isMaker, isChecker, isglobalPOS, iscashBegin, isglobalUserID, isglobalAdmin, isglobalOfficer, isglobalBranchOfficer, isglobalWarehouseOfficer, isCashier, isglobalApprover, glacctcode, cashinlimit, cashendlimit;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public static string UserID,CashierTransNo, MachineUsed;
        //string password;
        //string encryptedpassword;
        //string decryptedpassword;
        public POSCloseTransactionAuthentication()
        {
            InitializeComponent();
        }

        private void POSCloseTransactionAuthentication_Load(object sender, EventArgs e)
        {
            txtuserid.Focus();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //PAYMENT
            {
                this.Dispose();
            }
            return functionReturnValue;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtuserid.Text.Trim() == "")
            {
                XtraMessageBox.Show("User id is required.", "ITCORE Solutions Inc.");
                return;
            }
            if (txtpassword.Text.Trim() == "")
            {
                XtraMessageBox.Show("Password is required.", "ITCORE Solutions Inc.");
                return;
            }
            bool checkifexist = Database.checkifExist("SELECT TOP 1 UserID FROM SalesTransactionSummary " +
                "WHERE BranchCode='" + Login.assignedBranch + "' " +
                "AND DateOpen='" + DateTime.Today.ToShortDateString() + "' AND MachineUsed='"+Environment.MachineName.ToString()+"' AND isOpen=1 AND UserID='"+ txtuserid.Text.Trim() + "'");
            if (checkifexist)
            {
                var rows = Database.getMultipleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' " +
                 "AND UserID='" + txtuserid.Text.Trim() + "' AND MachineUsed='" + Environment.MachineName.ToString() + "' " +
                 "AND DateOpen='" + DateTime.Today.ToShortDateString() + "' AND isOpen=1"
                 , "UserID,CashierTransNo,MachineUsed");
                CashierTransNo = "";
                MachineUsed = "";
                UserID = "";
                CashierTransNo = rows["CashierTransNo"].ToString();
                MachineUsed = rows["MachineUsed"].ToString();
                UserID = rows["UserID"].ToString();
                AuthenticateCashierTransactionUser();
                //get_password();
            }
            else
            {
                XtraMessageBox.Show("The Credentials you Enter is no Transaction for this Day!..", "ITCORE Solutions Inc.");
                return;
            }

            //if (HelperFunction.GetLocalIPAddress() Database.ex)
            //{
            //    XtraMessageBox.Show("Mac Address not Assigned To Branch", "SPIRE");
            //    return;
            //}
           
        }
        private void AuthenticateCashierTransactionUser()
        {
            try
            {
                using (SqlConnection con = Database.getConnection())
                {
                    if (con == null)
                    {
                        XtraMessageBox.Show("Database connection is not available.", "ITCORE Solutions Inc.");
                        return;
                    }

                    con.Open();

                    // ==========================================================
                    // STEP 1: Check if this cashier has an open transaction today
                    // on this branch + current machine
                    // ==========================================================
                    string transactionSql = @"
                            SELECT TOP 1 UserID, CashierTransNo, MachineUsed
                            FROM dbo.SalesTransactionSummary
                            WHERE BranchCode = @BranchCode
                              AND DateOpen = @DateOpen
                              AND MachineUsed = @MachineUsed
                              AND isOpen = 1
                              AND UserID = @UserID;";

                    using (SqlCommand transCmd = new SqlCommand(transactionSql, con))
                    {
                        transCmd.Parameters.AddWithValue("@BranchCode", Login.assignedBranch);
                        transCmd.Parameters.AddWithValue("@DateOpen", DateTime.Today.ToShortDateString());
                        transCmd.Parameters.AddWithValue("@MachineUsed", Environment.MachineName);
                        transCmd.Parameters.AddWithValue("@UserID", txtuserid.Text.Trim());

                        using (SqlDataReader transReader = transCmd.ExecuteReader())
                        {
                            if (!transReader.Read())
                            {
                                XtraMessageBox.Show("The credentials you entered have no transaction for this day!.", "ITCORE Solutions Inc.");
                                txtuserid.Focus();
                                return;
                            }

                            CashierTransNo = transReader["CashierTransNo"].ToString();
                            MachineUsed = transReader["MachineUsed"].ToString();
                            UserID = transReader["UserID"].ToString();
                        }
                    }

                    // ==========================================================
                    // STEP 2: Load user account + password hash
                    // ==========================================================
                    string userSql = @"
                            SELECT TOP 1
                                UserID,
                                isAdmin,
                                isGlobalOfficer,
                                isBranchOfficer,
                                isWarehouseOfficer,
                                isMaker,
                                isChecker,
                                isCashier,
                                isApprover,
                                AssignedBranch,
                                CashInLimit,
                                CashEndLimit,
                                GLAccount,
                                PasswordHash,
                                PasswordSalt,
                                PasswordIterations,
                                MustChangePassword
                            FROM dbo.Users
                            WHERE UserID = @UserID;";

                    using (SqlCommand userCmd = new SqlCommand(userSql, con))
                    {
                        userCmd.Parameters.AddWithValue("@UserID", txtuserid.Text.Trim());

                        using (SqlDataReader reader = userCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                XtraMessageBox.Show("Invalid user id or password given.", "ITCORE Solutions Inc.",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                txtpassword.Focus();
                                txtpassword.SelectAll();
                                return;
                            }

                            byte[] passwordHash = reader["PasswordHash"] == DBNull.Value ? null : (byte[])reader["PasswordHash"];
                            byte[] passwordSalt = reader["PasswordSalt"] == DBNull.Value ? null : (byte[])reader["PasswordSalt"];
                            int passwordIterations = reader["PasswordIterations"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PasswordIterations"]);
                            bool mustChangePassword = reader["MustChangePassword"] != DBNull.Value && Convert.ToBoolean(reader["MustChangePassword"]);

                            if (passwordHash == null || passwordSalt == null || passwordIterations <= 0)
                            {
                                XtraMessageBox.Show("This account needs a password reset before it can be used.",
                                    "ITCORE Solutions Inc.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1);
                                txtuserid.Focus();
                                return;
                            }

                            if (mustChangePassword)
                            {
                                XtraMessageBox.Show("This account is required to change password before it can be used.",
                                    "ITCORE Solutions Inc.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button1);
                                txtuserid.Focus();
                                return;
                            }

                            bool passwordOk = PasswordHasher.VerifyPassword(
                                txtpassword.Text,
                                passwordSalt,
                                passwordIterations,
                                passwordHash);

                            if (!passwordOk)
                            {
                                XtraMessageBox.Show("Invalid user id or password given.",
                                    "ITCORE Solutions Inc.",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1);
                                txtpassword.Focus();
                                txtpassword.SelectAll();
                                return;
                            }

                            // ==================================================
                            // STEP 3: Populate existing global variables
                            // ==================================================
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

                            // ==================================================
                            // STEP 4: Success
                            // ==================================================
                            isconfirmedLogin = true;
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Authentication error: " + ex.Message,
                    "ITCORE Solutions Inc.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }
        //private void get_password()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    SqlCommand com = new SqlCommand("Select Password from Users where UserID = '" + txtuserid.Text + "'", con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    try
        //    {
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                password = reader["Password"].ToString();
        //                decrypt_password();
        //                return;
        //            }
        //        }
        //        XtraMessageBox.Show("Invalid user id or password given.", "SPIRE Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //        txtpassword.Focus();
        //        txtpassword.SelectionStart = 0;
        //        txtpassword.SelectionLength = txtpassword.Text.Length;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        //private void decrypt_password()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    SqlCommand com = new SqlCommand("declare @pwd varchar(50) exec master..xp_aes_decrypt '" + password + "','0123456789ABCDEF0123456789ABCDEF',@pwd output select @pwd result", con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    try
        //    {
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                decryptedpassword = reader["result"].ToString();
        //                validate_user();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        //private void encrypt()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    SqlCommand com = new SqlCommand("exec master..xp_aes_encrypt '" + txtpassword.Text + "','0123456789ABCDEF0123456789ABCDEF'", con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    try
        //    {
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                encryptedpassword = reader["result"].ToString();
        //                validate_user();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
        //private void validate_user()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    SqlCommand com = new SqlCommand("Select * from Users where UserID= '" + txtuserid.Text + "' and Password = '" + password + "'", con);
        //    SqlDataReader reader = com.ExecuteReader();
        //    try
        //    {
        //        if (reader != null)
        //        {
        //            while (reader.Read())
        //            {
        //                isglobalAdmin = reader["isAdmin"].ToString();
        //                isglobalOfficer = reader["isGlobalOfficer"].ToString();
        //                isglobalBranchOfficer = reader["isBranchOfficer"].ToString();
        //                isglobalWarehouseOfficer = reader["isWarehouseOfficer"].ToString();
        //                isMaker = reader["isMaker"].ToString();
        //                isChecker = reader["isChecker"].ToString();
        //                isCashier = reader["isCashier"].ToString();
        //                isglobalApprover = reader["isApprover"].ToString();
        //                isglobalUserID = reader["UserID"].ToString();
        //                assignedBranch = reader["AssignedBranch"].ToString();
        //                cashinlimit = reader["CashInLimit"].ToString();
        //                cashendlimit = reader["CashEndLimit"].ToString();
        //                glacctcode = reader["GLAccount"].ToString();
        //                if (txtpassword.Text != decryptedpassword)
        //                {
        //                    XtraMessageBox.Show("Invalid user id or password given.", "SPIRE IT SOLUTIONS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //                    txtuserid.Focus();
        //                    return;
        //                }
        //                else
        //                {
        //                    isconfirmedLogin = true;
        //                    this.Hide();
        //                }
        //                //if ((Convert.ToBoolean(isglobalBranchOfficer) == true || Convert.ToBoolean(isglobalAdmin)) && assignedBranch == Login.assignedBranch)
        //                //{
        //                //    isconfirmedLogin = true;
        //                //    this.Hide();
        //                //}
        //                //if (Convert.ToBoolean(isglobalOfficer))
        //                //{
        //                //    isconfirmedLogin = true;
        //                //    this.Hide();
        //                //}
        //                // this.Close();


        //            }
        //        }
        //        // XtraMessageBox.Show("Invalid user id or password given.", "Spire Solution", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //        txtpassword.Focus();
        //        txtpassword.SelectionStart = 0;
        //        txtpassword.SelectionLength = txtpassword.Text.Length;
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        private void txtuserid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //simpleButton1.PerformClick();
                if (String.IsNullOrEmpty(txtuserid.Text))
                {
                    XtraMessageBox.Show("Fields must not Empty!...");
                    txtuserid.Focus();
                }
                else
                {
                    txtpassword.Focus();
                }

            }
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.PerformClick();
            }
        }


    }
}