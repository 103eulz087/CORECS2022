using DevExpress.XtraEditors;
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

namespace SalesInventorySystem.POS
{
    public partial class POSEditLine : Form
    {
        public static bool isdone = false;
        public static string specialpriceamount = "0";
        public POSEditLine()
        {
            InitializeComponent();
        }

        private void POSEditLine_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtqty1;
            txtqty1.Focus();
            txtqty1.Select(0, txtqty1.Text.Length);
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            //if (keyData == Keys.Enter) //PAYMENT
            //{
            //    btnsubmit.PerformClick();
            //}
            if (keyData == Keys.Escape) //PAYMENT
            {
                this.Close();
            }
            else if (keyData == Keys.F1) //PAYMENT
            {
                if (checkBox1.Checked == false)
                    checkBox1.Checked = true;
                else
                    checkBox1.Checked = false;
            }
            return functionReturnValue;
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if(Convert.ToDouble(PointOfSale.uprice) != Convert.ToDouble(txtuprice.Text))
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    add();
                    isdone = true;
                    this.Close();
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
            else
            {
                add();
                isdone = true;
                this.Close();
            }
            
        }

        void add()
        {
            bool ischeck = false;
            if (checkBox1.Checked == true)
                ischeck = true;
            else
                ischeck = false;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_EditLine";
                SqlCommand com = new SqlCommand(query, con);
                //com.Parameters.AddWithValue("@parmorderno", textEdit3.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmorderno", PointOfSale.refno);
                com.Parameters.AddWithValue("@parmtransid", PointOfSale.transcode);
                com.Parameters.AddWithValue("@parmsequencenumber", PointOfSale.sequenceNum);
                com.Parameters.AddWithValue("@parmprodname", txtprodname.Text);
                com.Parameters.AddWithValue("@parmunitprice", txtuprice.Text);
                com.Parameters.AddWithValue("@parmqty", txtqty1.Text);
                com.Parameters.AddWithValue("@parmtotalamount", txttotal.Text);
                com.Parameters.AddWithValue("@parmisspecialprice", ischeck);
                com.Parameters.AddWithValue("@parmdiscountrate", Convert.ToDouble(percentagedisc.Text)/100);
                com.Parameters.AddWithValue("@parmdiscountamount", txtspecialprice.Text);
                com.Parameters.AddWithValue("@parmnewtotal", txtnewtotal.Text);
                com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
               
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                bool isoverride = false;
                isoverride = Database.checkifExist("SELECT isnull(isOverride,0) FROM POSFunctions WHERE FunctionName='EDITPRICE'");
                if (!isoverride)
                {
                    txtspecialprice.Visible = true;
                    percentagedisc.Focus();
                    percentagedisc.Select(0, percentagedisc.Text.Length);
                }
                else
                {
                    AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                    authfrm.ShowDialog(this);
                    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                    {
                        txtspecialprice.Visible = true;
                        percentagedisc.Focus();
                        percentagedisc.Select(0, percentagedisc.Text.Length);
                        AuthorizedConfirmationFrm.isconfirmedLogin = false;
                        authfrm.Dispose();
                    }
                }
               
            }
            else
                txtspecialprice.Visible = false;
        }

        private void txtspecialprice_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtqty1_TextChanged(object sender, EventArgs e)
        {
            double total = 0.0;
            total = Convert.ToDouble(txtqty1.Text) * Convert.ToDouble(txtuprice.Text);
            txttotal.Text = total.ToString();
        }

        private void txtqty1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtuprice.Focus();
        }

        private void txtuprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnsubmit.PerformClick();
            }
                
        }

      

        private void txtspecialprice_EditValueChanged(object sender, EventArgs e)
        {
            //double total = 0.0;
            //total = Convert.ToDouble(txtqty1.Text) * Convert.ToDouble(txtspecialprice.Text);
            //txttotal.Text = total.ToString();
        }

        private void percentagedisc_EditValueChanged(object sender, EventArgs e)
        {
            double percent = 0.0, discountamount = 0.0,newtotalamount=0.0;
            
            percent = Convert.ToDouble(percentagedisc.Value) / 100;
            discountamount = percent * Convert.ToDouble(txttotal.Text);
            txtspecialprice.Text = discountamount.ToString() ;
            newtotalamount = Convert.ToDouble(txttotal.Text) - discountamount;
            txtnewtotal.Text = newtotalamount.ToString();
        }

        private void percentagedisc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure that applied discount is Correct? YES if Ok else Cancel", "Confirm Discount");
                if (confirm)
                {
                    btnsubmit.PerformClick();
                    isdone = true;
                    this.Close();
                }
                else
                { return; }

                //AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                //authfrm.ShowDialog(this);
                //if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                //{
                //    btnsubmit.PerformClick();
                //    isdone = true;
                //    this.Close();
                //    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                //    authfrm.Dispose();
                //}
                
            }
        }
    }
}
