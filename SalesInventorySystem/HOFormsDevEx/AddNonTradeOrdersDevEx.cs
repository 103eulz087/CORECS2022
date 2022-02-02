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
    public partial class AddNonTradeOrdersDevEx : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        public AddNonTradeOrdersDevEx()
        {
            InitializeComponent();
        }

        private void AddNonTradeOrdersDevEx_Load(object sender, EventArgs e)
        {
            //txtshipmentno.Text = IDGenerator.getShipmentNumber().ToString();
            //txtshipmentno.Text = IDGenerator.getShipmentNoSP().ToString();
            txtshipmentno.Text = IDGenerator.getIDNumberSP("sp_GetShipmentNo", "ShipmentNo");
            loadMetrics();
            loadCategory();
            loadProdCategory();
            loadBranch();
            displayvendor();
            loadgridview1();
        }

        void loadProdCategory()
        {
            Database.displayComboBoxItems("SELECT Category FROM GenInventoryCategory ", "Category", txtproductcategory);
           // Database.displayComboBoxItems("SELECT Description FROM ProductCategory ", "Description", txtproductcategory);
        }


        private void addbtn_Click(object sender, EventArgs e)
        {
            bool isexist = false;
            if (txtproductcategory.Text == "" || txtcost.Text == "" || txtqty.Text == "" || txtmetrics.Text == ""  || txtbranch.Text == "")
            {
                XtraMessageBox.Show("Fields must not Empty..");
            }
            else if (isexist)
            {
                XtraMessageBox.Show("Product Already Exist");
                return;
            }
            else
            {
                add();
                txtdate.Enabled = false;
                //txtsupplier.Enabled = false;
                searchLookUpEdit1.Enabled = false;
                //txtmetrics.Text = "";
                txtproductcategory.Text = "";
                txtproduct.Text = "";

                txtcost.Text = "";

                txtqty.Text = "0";
               
            }
        }

        void loadgridview1()
        {
            table = new DataTable();
            table.Columns.Add("OrderNo");
            table.Columns.Add("ProductCode");
            table.Columns.Add("Description");
            table.Columns.Add("Category"); 
            table.Columns.Add("Quantity");
            table.Columns.Add("Units");
            table.Columns.Add("Cost");
            dataGridView1.DataSource = table;
        }


        void add()
        {
            // string prodcode = Database.getSingleQuery("GenInventory", "Description='" + txtproduct.Text + "' ", "SerialNo");
            string prodcode = Database.getSingleQuery("GenInventoryItems", "Description='" + txtproduct.Text + "' ", "ItemCode");
            DataRow newRow = table.NewRow();
            newRow["OrderNo"] = txtshipmentno.Text;
            newRow["ProductCode"] = prodcode;
            newRow["Description"] = txtproduct.Text;
            newRow["Category"] = txtproductcategory.Text;
            newRow["Quantity"] = txtqty.Text;
            newRow["Units"] = txtmetrics.Text;
            newRow["Cost"] = txtcost.Text;
            table.Rows.Add(newRow);
            dataGridView1.DataSource = table;
        }

        void displayvendor()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", searchLookUpEdit1, "SupplierID", "SupplierID");
        }

        String getSupplierID()
        {
            string id = Database.getSingleQuery("Supplier", "SupplierName='" + searchLookUpEdit1.Text + "'", "SupplierID");
            return id;
        }
        String getProductCategoryID()
        {
            string id = Database.getSingleQuery("GenInventoryCategory", "Category='" + txtproductcategory.Text + "'", "CategoryID");
            return id;
            //string id = Database.getSingleQuery("ProductCategory", "Description='" + txtproductcategory.Text + "'", "ProductCategoryID");
            //return id;
        }

        void loadBranch()
        {
            Database.displayComboBoxItems("SELECT BranchName FROM Branches", "BranchName", txtbranch);
        }

        void loadCategory()
        {
            Classes.Product.displayProductCategoryComboBoxItems(txtproductcategory);
        }

        void loadMetrics()
        {
            Database.displayComboBoxItems("SELECT Metrics FROM Metrics", "Metrics", txtmetrics);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Cant Save No Entries!");
                return;
            }
            else
            {
                executor();
                XtraMessageBox.Show("Successfully Updated!");
                this.Dispose();
            }
            
        }

        private void executor()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                double qty = 0.0, cost = 0.0, totalcost = 0.0, total = 0.0;
                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    qty = Convert.ToDouble(dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                    cost = Convert.ToDouble(dataGridView1.Rows[i].Cells["Cost"].Value.ToString());
                    total = qty * cost;
                    totalcost += total;
                    string query = "sp_addNonTradeOrder";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                    com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                    com.Parameters.AddWithValue("@parmsupplier", searchLookUpEdit1.Text);
                    com.Parameters.AddWithValue("@parmproduct", dataGridView1.Rows[i].Cells["ProductCode"].Value.ToString());
                    com.Parameters.AddWithValue("@parmtargetdate", txtdate.Text);
                    com.Parameters.AddWithValue("@parmqty", dataGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                    com.Parameters.AddWithValue("@parmunits", dataGridView1.Rows[i].Cells["Units"].Value.ToString());
                    com.Parameters.AddWithValue("@parmcost", dataGridView1.Rows[i].Cells["Cost"].Value.ToString());
                    com.Parameters.AddWithValue("@parmremarks", txtremarks.Text);
                    com.Parameters.AddWithValue("@parmorderedby", Login.isglobalUserID); 
                    com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
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

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
        }

        private void txtproductcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("Select ItemCode,Description FROM GenInventoryItems WHERE ItemCategory='" + txtproductcategory.Text + "'", txtproduct, "Description", "Desscription");
           // Database.displayComboBoxItems("SELECT distinct Description FROM Products WHERE ProductCategoryCode='" + getProductCategoryID() + "' and BranchCode='" + Login.assignedBranch + "'", "Description", txtproduct);
            //Database.displayComboBoxItems("SELECT distinct Description FROM Products WHERE ProductCategoryCode='" + getProductCategoryID() + "' and BranchCode='"+Login.assignedBranch+"'", "Description", txtproduct);
            // Database.displayComboBoxItems("SELECT distinct Description FROM GenInventory WHERE Category='" + txtproductcategory.Text + "' ", "Description", txtproduct);

        }
    }
}