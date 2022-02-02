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

namespace SalesInventorySystem
{
    public partial class AuthorizedConfirmationFrm : DevExpress.XtraEditors.XtraForm
    {
        public static bool isconfirmedLogin = false;
        public static string assignedBranch, isMaker, isChecker, isglobalPOS, iscashBegin, isglobalUserID, isglobalAdmin, isglobalOfficer, isglobalBranchOfficer, isglobalWarehouseOfficer, isCashier, isglobalApprover, glacctcode, cashinlimit, cashendlimit;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string password;
        string encryptedpassword;
        string decryptedpassword;
        public AuthorizedConfirmationFrm()
        {
            InitializeComponent();
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

            //if (HelperFunction.GetLocalIPAddress() Database.ex)
            //{
            //    XtraMessageBox.Show("Mac Address not Assigned To Branch", "SPIRE");
            //    return;
            //}
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
                            XtraMessageBox.Show("Invalid user id or password given.", "SPIRE IT BUSINESS SOLUTIONS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            txtuserid.Focus();
                            return;
                        }
                        if (Convert.ToBoolean(isglobalAdmin)==true  || (Convert.ToBoolean(isglobalBranchOfficer) == true && assignedBranch == Login.assignedBranch))
                        {
                            isconfirmedLogin = true;
                            this.Hide();
                        }
                        else
                        {
                            XtraMessageBox.Show("You are not Authorized!...", "SPIRE IT BUSINESS SOLUTIONS", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                            txtuserid.Focus();
                            return;
                        }
                        //if(Convert.ToBoolean(isglobalOfficer))
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

        private void AuthorizedConfirmationFrm_Load(object sender, EventArgs e)
        {
            txtuserid.Focus();
        }

        private void txtpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.PerformClick();
            }
        }

        private void txtuserid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //simpleButton1.PerformClick();
                if(String.IsNullOrEmpty(txtuserid.Text))
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
    }
}