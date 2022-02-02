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

namespace SalesInventorySystem.HOForms
{
    public partial class ADDPOSUMMARY : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        public ADDPOSUMMARY()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(isProd.Checked==true)
            {
                txtproductcategory.Enabled = true;
                txtproduct.Enabled = true;
                txtservices.Enabled = false;
                txtqty.Text = "";
            }
            else
            {
                txtproductcategory.Enabled = true;
                txtproduct.Enabled = false;
                txtservices.Enabled = true;
                txtqty.Text = "1";
            }
        }

        private void isServ_CheckedChanged(object sender, EventArgs e)
        {
            if (isProd.Checked != true)
            {
                txtproductcategory.Enabled = true;
                txtproduct.Enabled = false;
                txtservices.Enabled = true;
                txtqty.Text = "1";
            }
            else
            {
                txtproductcategory.Enabled = true;
                txtproduct.Enabled = true;
                txtservices.Enabled = false;
                txtqty.Text = "";
            }
        }

        private void ADDPOSUMMARY_Load(object sender, EventArgs e)
        {

        }
        void add()
        {
            string orderType = "",ordercode="";
            if(isProd.Checked==true)
            {
                orderType = "01";
                ordercode = Classes.Product.getProductCode(txtproduct.Text, txtproductcategory.Text);
            }
            else
            {
                orderType = "02";
                ordercode = Database.getSingleQuery("SERVICES", "SRVC_DESC='" + txtservices.Text + "' ", "SRVC_ID");
            }
            DataRow newRow = table.NewRow();
            newRow["PONumber"] = txtshipmentno.Text;
            newRow["SupplierID"] = searchLookUpEdit1.Text;
            newRow["OrderType"] = orderType;
            newRow["OrderCode"] = ordercode;
            newRow["Quantity"] = txtqty.Text;
            newRow["Cost"] = txtcost.Text;
            table.Rows.Add(newRow);
            dataGridView1.DataSource = table;
        }
        String getProductCode()
        {
            string str = "";
            str = Database.getSingleQuery("InventoryCost", "ProductName='" + txtproduct.Text + "'", "ProductCode");
            return str;
        }
        String geServiceCode()
        {
            string str = "";
            str = Database.getSingleQuery("SERVICESCOST", "ProductName='" + txtservices.Text + "'", "ProductCode");
            return str;
        }
        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cost = Database.getSingleQuery("InventoryCost", "SupplierID='" + searchLookUpEdit1.Text + "' AND ProductCode='" + getProductCode() + "'", "CostKg");
            txtcost.Text = cost;
        }

        private void txtservices_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cost = Database.getSingleQuery("SERVICESCOST", "SupplierID='" + searchLookUpEdit1.Text + "' AND ProductCode='" + geServiceCode() + "'", "CostKg");
            txtcost.Text = cost;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            add();
        }
        void insert()
        {

            try
            {
                string ponum, suppid, ordtype, ordcde, qty, cost;
                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    ponum = dataGridView1.Rows[i].Cells["PONumber"].Value.ToString();
                    suppid = dataGridView1.Rows[i].Cells["SupplierID"].Value.ToString();
                    ordtype = dataGridView1.Rows[i].Cells["OrderType"].Value.ToString();
                    ordcde = dataGridView1.Rows[i].Cells["OrderCode"].Value.ToString();
                    qty = dataGridView1.Rows[i].Cells["Quantity"].Value.ToString();
                    cost = dataGridView1.Rows[i].Cells["Cost"].Value.ToString();
                    Database.ExecuteQuery("INSERT INTO PODETAILS VALUES('" + ponum + "','" + suppid + "','" + ordtype + "','" + ordcde + "','" + qty + "','kg','" + cost + "','0','0','0','')");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        void ExecuteSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SP_ADDPOSUM";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                com.Parameters.AddWithValue("@parmtargetdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmorderedby", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }

        private void btnsave_Click(object sender, EventArgs e)
        {

        }
    }
}