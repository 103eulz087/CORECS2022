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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class CustomersInfoDevEx : DevExpress.XtraEditors.XtraForm
    {
        string id, name;
        public CustomersInfoDevEx()
        {
            InitializeComponent();
        }

        private void CustomersInfoDevEx_Load(object sender, EventArgs e)
        {
            display();
            displayAO();
        }

        void newButton()
        {
            int id = IDGenerator.getIDNumber("Customers", "CustomerKey", 1);
            txtcustkey.Text = HelperFunction.sequencePadding1(id.ToString(), 6);
            txtcustid.Text = txtcustkey.Text;
            //txtsupplierid.Text = "000"+IDGenerator.getSupplierNumber().ToString();
            simpleButton2.Enabled = false;
            addbtn.Enabled = true;
            updatebtn.Enabled = false;
            btncancel.Enabled = true;
        }

        void sp_addCust(string cmd)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spitcr_addCust";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@CustomerKey", txtcustkey.Text);
                com.Parameters.AddWithValue("@CustomerID", txtcustid.Text);
                com.Parameters.AddWithValue("@CustomerName", txtcustname.Text);
                com.Parameters.AddWithValue("@CustomerEmail", txtemail.Text);
                com.Parameters.AddWithValue("@CustomerContactNo", txtcontactno.Text);
                com.Parameters.AddWithValue("@CustomerAddress", txtaddress.Text);
                com.Parameters.AddWithValue("@CustomerBirthDate", txtbdate.Text);
                com.Parameters.AddWithValue("@CustomerCreditLimit", txtcreditlimit.Text);
                com.Parameters.AddWithValue("@BranchCode", txtbrcode.Text);
                com.Parameters.AddWithValue("@Term", txtterm.Text);
                com.Parameters.AddWithValue("@isActive", "1");
                com.Parameters.AddWithValue("@DateAdded", DateTime.Now.ToString());
                com.Parameters.AddWithValue("@AddedBy", Login.isglobalUserID);
                com.Parameters.AddWithValue("@UpdatedBy", " ");
                com.Parameters.AddWithValue("@AccountOfficer", txtao.Text);
                com.Parameters.AddWithValue("@TinNo", txtrfid.Text);
                com.Parameters.AddWithValue("@parmcmd", cmd);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
           
        }

        void addButton()
        {
            try
            {
                if (String.IsNullOrEmpty(txtbrcode.Text) || txtcustid.Text == "" || txtcustname.Text == "" || txtaddress.Text == "" || txtemail.Text == "" || txtcontactno.Text == "" || txtcreditlimit.Text == "" || txtbdate.Text == "" || txtao.Text == "")
                {
                    XtraMessageBox.Show("Please Input All Fields");
                }
                else
                {
                    //add();
                    sp_addCust("1"); //ADD
                    XtraMessageBox.Show("Successfully Added!");
                    txtbdate.Text = "";
                    display();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void updateButton()
        {
            try
            {
                //TEMPORARILY NOT USED--DEFAULT IS ACTIVE 1
                //string isactive = "";
                //if (checkBox1.Checked == true)
                //{
                //    isactive = "1";
                //}
                //else
                //{
                //    isactive = "0";
                //}

                if (txtcustid.Text == "" || txtcustname.Text == "" || txtaddress.Text == "" || txtemail.Text == "" || txtcontactno.Text == "" || txtcreditlimit.Text == "")
                {
                    XtraMessageBox.Show("Please Supply All Fields");
                }
                else
                {
                    sp_addCust("2"); //UPDATE
                    //if (id != txtcustid.Text || name != txtcustname.Text)
                    //{
                    //    Database.ExecuteQuery("UPDATE ClientAccounts SET AccountID='" + txtcustid.Text + "',AccountName='" + txtcustname.Text + "' WHERE AccountKey='" + id + "' ");
                    //    Database.ExecuteQuery("UPDATE CustomerProductSetting SET CustID='" + txtcustid.Text + "',CustName='" + txtcustname.Text + "' WHERE CustomerKey='" + id + "' ");
                    //}
                    //Database.ExecuteQuery("UPDATE Customers SET Term='" + txtterm.Text + "'" +
                    //    ",CustomerName='" + txtcustname.Text + "'" +
                    //    ",CustomerEmail='" + txtemail.Text + "'" +
                    //    ",CustomerContactNo='" + txtcontactno.Text + "'" +
                    //    ",CustomerAddress='" + txtaddress.Text + "'" +
                    //    ",CustomerBirthDate='" + txtbdate.Text + "'" +
                    //    ",CustomerCreditLimit='" + txtcreditlimit.Text + "'" +
                    //    ",BranchCode='" + txtbrcode.Text + "'" +
                    //    ",UpdatedBy='" + Login.isglobalUserID + "'" +
                    //    ",isActive='" + isactive + "'" +
                    //    ",AccountOfficer='" + txtao.Text + "'" +
                    //    ",TinNo='" + txtrfid.Text + "' " +
                    //    "WHERE CustomerKey='" + txtcustkey.Text + "' ", "Successfully Updated!");
                    txtbdate.Text = "";
                    display();

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void cancelButton()
        {
            //HelperFunction.ClearAllText(this);
            //HelperFunction.DisableTextFields(this);
            simpleButton2.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
        }

        void displayAO()
        {
            Database.displaySearchlookupEdit("SELECT UserID,FullName FROM Users", txtao, "UserID", "UserID");
        }
        void display()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
            //Database.display("SELECT * FROM func_viewCustomer('"++"')", gridControl1,gridView1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        void add()
        {
            string isactive = "";
            if (checkBox1.Checked == true)
            {
                isactive = "1";
            }
            else
            {
                isactive = "0";
            }
            
            Database.ExecuteQuery("INSERT INTO Customers (CustomerKey,CustomerID" +
                ",CustomerName" +
                ",CustomerEmail" +
                ",CustomerContactNo" +
                ",CustomerAddress" +
                ",CustomerBirthDate" +
                ",CustomerCreditLimit" +
                ",BranchCode" +
                ",Term" +
                ",isActive" +
                ",AccountOfficer" +
                ",DateAdded" +
                ",AddedBy" +
                ",UpdatedBy" +
                ",TinNo) " +
                "VALUES('"+txtcustkey.Text+"','" + txtcustid.Text+ "'" +
                ",'" + txtcustname.Text + "'" +
                ",'" + txtemail.Text + "'" +
                ",'" + txtcontactno.Text + "'" +
                ",'" + txtaddress.Text + "'" +
                ",'" + txtbdate.Text + "'" +
                ",'" + txtcreditlimit.Text + "'" +
                ",'"+txtbrcode.Text+"'" +
                ",'"+txtterm.Text+"'" +
                ",'" + isactive + "'" +
                ",'"+txtao.Text+"'" +
                ",'" + DateTime.Now.ToShortDateString() + "'" +
                ",'" + Login.isglobalUserID + "'" +
                ",''" +
                ",'"+txtrfid.Text+"')");
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
                contextMenuStrip1.Items[1].Visible = false;
            }
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                id = "";
                name = "";
                string isactive = "";
               
                txtbdate.Text = "";
                if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"isActive").ToString()) == true)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }

                id = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerKey").ToString();
                name= gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerName").ToString();

                txtcustkey.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerKey").ToString();
                txtcustid.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerID").ToString();
                txtcustname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerName").ToString();
                txtemail.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerEmail").ToString();
                txtcontactno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerContactNo").ToString();
                txtaddress.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerAddress").ToString();
                txtbdate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerBirthDate").ToString();
                txtcreditlimit.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerCreditLimit").ToString();
                txtbrcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
                txtao.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountOfficer").ToString();
                txtterm.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Term").ToString();
                isactive = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isActive").ToString();
                txtrfid.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TinNo").ToString();
                updatebtn.Enabled = true;
              
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM func_viewCustomer('" + txtbranch.Text + "')", gridControl1, gridView1);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
              
                bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Customer");
                if (ok)
                {
                    Database.ExecuteQuery("DELETE FROM Customers WHERE CustomerKey ='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerKey").ToString() + "'", "Successfully Deleted");
                    display();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            newButton();
            checkBox1.Checked = true;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            addButton(); clear(); cancelButton();


        }

        void clear()
        {
            txtbrcode.Text = "";
            txtaddress.Text = "";
            txtao.Text = "";
            txtbdate.Text = "";
            txtbranch.Text = "";
            txtcontactno.Text = "";
            txtcreditlimit.Text = "";
            txtcustid.Text = "";
            txtcustkey.Text = "";
            txtcustname.Text = "";
            txtemail.Text = "";
            txtrfid.Text = "";
            txtterm.Text = "";
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            updateButton();
            clear();
            cancelButton();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            cancelButton();
        }

        
    }
}