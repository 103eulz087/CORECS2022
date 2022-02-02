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
        string password;
        string encryptedpassword;
        string decryptedpassword;
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
            bool checkifexist = Database.checkifExist("SELECT TOP 1 UserID FROM SalesTransactionSummary WHERE BranchCode='" + Login.assignedBranch + "' " +
                "AND DateOpen='" + DateTime.Today.ToShortDateString() + "' AND isOpen=1 AND UserID='"+ txtuserid.Text.Trim() + "'");
            if (checkifexist)
            {
                var rows = Database.getMultipleQuery("SalesTransactionSummary", "BranchCode='" + Login.assignedBranch + "' " +
                 "AND UserID='" + txtuserid.Text.Trim() + "' " +
                 "AND DateOpen='" + DateTime.Today.ToShortDateString() + "' AND isOpen=1"
                 , "UserID,CashierTransNo,MachineUsed");
                CashierTransNo = "";
                MachineUsed = "";
                UserID = "";
                CashierTransNo = rows["CashierTransNo"].ToString();
                MachineUsed = rows["MachineUsed"].ToString();
                UserID = rows["UserID"].ToString();
                get_password();
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
                XtraMessageBox.Show("Invalid user id or password given.", "SPIRE Solutions Inc.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                txtpassword.Focus();
                txtpassword.SelectionStart = 0;
                txtpassword.SelectionLength = txtpassword.Text.Length;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
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
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void validate_user()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("Select * from Users where UserID= '" + txtuserid.Text + "' and Password = '" + password + "'", con);
            SqlDataReader reader = com.ExecuteReader();
            try
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
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
                        if (txtpassword.Text != decryptedpassword)
                        {
                            XtraMessageBox.Show("Invalid user id or password given.", "SPIRE IT SOLUTIONS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            txtuserid.Focus();
                            return;
                        }
                        else
                        {
                            isconfirmedLogin = true;
                            this.Hide();
                        }
                        //if ((Convert.ToBoolean(isglobalBranchOfficer) == true || Convert.ToBoolean(isglobalAdmin)) && assignedBranch == Login.assignedBranch)
                        //{
                        //    isconfirmedLogin = true;
                        //    this.Hide();
                        //}
                        //if (Convert.ToBoolean(isglobalOfficer))
                        //{
                        //    isconfirmedLogin = true;
                        //    this.Hide();
                        //}
                        // this.Close();


                    }
                }
                // XtraMessageBox.Show("Invalid user id or password given.", "Spire Solution", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                txtpassword.Focus();
                txtpassword.SelectionStart = 0;
                txtpassword.SelectionLength = txtpassword.Text.Length;
                return;
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