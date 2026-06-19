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

namespace SalesInventorySystem.HOForms
{
    public partial class ChangePassword : DevExpress.XtraEditors.XtraForm
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtnewpass.Text) || String.IsNullOrEmpty(txtconfirmnewpass.Text))
            {
                //XtraMessageBox.Show("Fields must not be empty.");
                BigAlert.Show("EMPTY", "Fields must not be empty.", MessageBoxIcon.Warning);
                return;
            }

            if (txtconfirmnewpass.Text != txtnewpass.Text)
            {
                XtraMessageBox.Show("Password does not match!");
                return;
            }

            string validationMessage;
            if (!PasswordPolicy.Validate(txtnewpass.Text.Trim(), Login.isglobalUserID, out validationMessage))
            {
                BigAlert.Show("WEAK PASSWORD", validationMessage, MessageBoxIcon.Warning);
                //XtraMessageBox.Show(validationMessage, "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            update();
            this.Dispose();


            //if(String.IsNullOrEmpty(txtnewpass.Text) || String.IsNullOrEmpty(txtconfirmnewpass.Text))
            //{
            //    XtraMessageBox.Show("Fields must not Empty");
            //    return;
            //}
            //else
            //{
            //    if (txtconfirmnewpass.Text != txtnewpass.Text)
            //    {
            //        XtraMessageBox.Show("Password Not Match!");
            //        return;
            //    }
            //    update();
            //    this.Dispose();
            //}
        }



        void update()
        {
            try
            {
                var hp = PasswordHasher.HashPassword(txtnewpass.Text);

                using (SqlConnection con = Database.getConnection())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE dbo.Users
                        SET PasswordHash = @Hash,
                            PasswordSalt = @Salt,
                            PasswordIterations = @Iter,
                            PasswordAlgoVersion = @Ver,
                            MustChangePassword = 0
                        WHERE UserID = @UserID;", con))
                    {
                        cmd.Parameters.Add("@Hash", SqlDbType.VarBinary, 32).Value = hp.Hash;
                        cmd.Parameters.Add("@Salt", SqlDbType.VarBinary, 16).Value = hp.Salt;
                        cmd.Parameters.Add("@Iter", SqlDbType.Int).Value = hp.Iterations;
                        cmd.Parameters.Add("@Ver", SqlDbType.TinyInt).Value = hp.AlgoVersion;
                        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = Login.isglobalUserID;

                        int rows = cmd.ExecuteNonQuery();
                        XtraMessageBox.Show(rows > 0 ? "Successfully Updated!" : "User not found / nothing updated.");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Error updating password: " + ex.Message);
            }
        }
    }

    //void update()
    //{
    //    Database.ExecuteQuery("UPDATE Users SET Password='"+txtnewpass.Text+"' WHERE UserID='"+Login.isglobalUserID+"'","Successfully Updated!");
    //}
}