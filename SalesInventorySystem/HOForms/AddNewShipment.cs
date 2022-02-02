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
    public partial class AddNewShipment : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        //string buttonexec = "";
        
        public AddNewShipment()
        {
            InitializeComponent();
            DataGridViewSettings.gridDefaultSettings(dataGridView1);
        }

        private void AddNewShipment_Load(object sender, EventArgs e)
        {
            //display();
            loadgridview1();
            loadMetrics();
            loadCategory();
            loadBranch();
            displaySupplier();
            displayvendor();
        }
        private void displaySearchlookupEdit()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "select SupplierName FROM Supplier";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            searchLookUpEdit1.Properties.DataSource = table;
            con.Close();
        }
        //void loadSuppliers()
        //{
        //    Database.displayComboBoxItems("SELECT * FROM Supplier", "SupplierName", txtsupplier);
        //}

        void loadBranch()
        {
            Database.displayComboBoxItems("SELECT * FROM Branches", "BranchName", txtbranch);
        }

        void loadCategory()
        {
            Classes.Product.displayProductCategoryComboBoxItems(txtproductcategory);
        }

        void loadMetrics()
        {
            Database.displayComboBoxItems("SELECT * FROM Metrics", "Metrics", txtmetrics);
        }

        void loadgridview1()
        {
            table = new DataTable();
            table.Columns.Add("ShipmentNo");
            table.Columns.Add("ProductCode");
            table.Columns.Add("Description"); //UnitPrice
            table.Columns.Add("Category"); //UnitPrice
            table.Columns.Add("Quantity");
            table.Columns.Add("Units");
            table.Columns.Add("Cost");
            table.Columns.Add("ButcheryCost");
            table.Columns.Add("FreightCost");
            table.Columns.Add("ButcheryVendor");
            table.Columns.Add("FreightVendor");
            dataGridView1.DataSource = table;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            /*txtshipmentno.Text = IDGenerator.getShipmentNoSP();*/ //IDGenerator.getShipmentNumber().ToString();
            txtshipmentno.Text = IDGenerator.getIDNumberSP("sp_GetShipmentNo","ShipmentNo");
            //    txtshipmentno.Text = sample();
            txtshipmentno.Enabled = true;
            txtmetrics.Enabled = true;
            txtproductcategory.Enabled = true;
            txtproduct.Enabled = true;
            txtqty.Enabled = true;
            txtremarks.Enabled = true;
            //txtsupplier.Enabled = true;
            txtdate.Enabled = true;
            txtbutcherycost.Enabled = true;
            txtfreightcost.Enabled = true;
            searchLookUpEdit1.Enabled = true;

            txtqty.Text = "0";
            txtbutcherycost.Text = "0";
            txtfreightcost.Text = "0";

            simpleButton1.Enabled = false;
            addbtn.Enabled = true;
            updatebtn.Enabled = false;
            btncancel.Enabled = true;
            btnsave.Enabled = true;
        }

        private void display()
        {
            //Database.display("SELECT ShipmentNo,LandingCost,SellingPrice,TotalKilos,TotalAmount,DateOrdered,OrderedBy,Status FROM ShipmentOrder", gridControl2, gridView2);
         
            Database.displayLocalGrid("SELECT * FROM ShipmentOrder", dataGridView1);
        }

        String getSupplierID()
        {
            string id = Database.getSingleQuery("Supplier", "SupplierName='" + searchLookUpEdit1.Text + "'", "SupplierID");
            return id;
        }

        void add()
        {
            string prodcode = Database.getSingleQuery("InventoryCost", "ProductName='" + txtproduct.Text + "' ", "ProductCode");
            DataRow newRow = table.NewRow();
            newRow["ShipmentNo"] = txtshipmentno.Text;
            newRow["ProductCode"] = prodcode;
            newRow["Description"] = txtproduct.Text;
            newRow["Category"] = txtproductcategory.Text;
            newRow["Quantity"] = txtqty.Text;
            newRow["Units"] = txtmetrics.Text;
            newRow["Cost"] = txtcost.Text;
            newRow["ButcheryCost"] = txtbutcherycost.Text;
            newRow["FreightCost"] = txtfreightcost.Text;
            newRow["ButcheryVendor"] = butcheryvendor.Text;
            newRow["FreightVendor"] = freightvendor.Text;
            
            
            table.Rows.Add(newRow);
            dataGridView1.DataSource = table;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            bool isexist = false;
            string prodcode = Database.getSingleQuery("InventoryCost", "ProductName='" + txtproduct.Text + "' ", "ProductCode");
            for (int i = 0; i<= dataGridView1.RowCount - 1;i++)
            {
                if(dataGridView1.Rows[i].Cells["ProductCode"].Value.ToString() == prodcode)
                {
                    isexist = true;
                }
            }
            if (txtproduct.Text == "" || txtcost.Text == "" || txtqty.Text == "" || txtmetrics.Text == "" || txtbutcherycost.Text == "" || txtfreightcost.Text == "" || txtbranch.Text == "")
            {
                XtraMessageBox.Show("Fields must not Empty..");
            }
            else if(isexist)
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
                txtbutcherycost.Text = "0";
                txtfreightcost.Text = "0";
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            if (txtproductcategory.Text == "" || txtproduct.Text == "" || txtcost.Text == "" || txtqty.Text == "" || txtmetrics.Text == "" || txtbutcherycost.Text == "" || txtfreightcost.Text == "")
            {
                XtraMessageBox.Show("Fields must not Empty..");
            }
            else
            {
                //Database.ExecuteQuery("UPDATE OrderDetails SET ButcheryCost='" + txtbutcherycost.Text + "',FreightCost='" + txtfreightcost.Text + "' WHERE ShipmentNo='" + txtshipmentno.Text + "' AND ProductCode='" + getProductCode() + "'","Successfully Updated");
                dataGridView1.Rows[cord].Cells[0].Value = txtshipmentno.Text;
                dataGridView1.Rows[cord].Cells[1].Value = getProductCode();
                dataGridView1.Rows[cord].Cells[2].Value = txtproduct.Text;
                dataGridView1.Rows[cord].Cells[3].Value = txtproductcategory.Text;
                dataGridView1.Rows[cord].Cells[4].Value = txtqty.Text;
                dataGridView1.Rows[cord].Cells[6].Value = txtcost.Text;
                dataGridView1.Rows[cord].Cells[7].Value = txtbutcherycost.Text;
                dataGridView1.Rows[cord].Cells[8].Value = txtfreightcost.Text;

                txtproductcategory.Text = "";
                txtproduct.Text = "";
                txtqty.Text = "";
                txtremarks.Text = "";
                txtcost.Text = "";
                txtbutcherycost.Text = "0";
                txtfreightcost.Text = "0";
                butcheryvendor.Text = "";
                freightvendor.Text = "";

                txtproductcategory.Enabled = false;
                txtproduct.Enabled = false;
                txtqty.Enabled = false;
                txtremarks.Enabled = false;
                searchLookUpEdit1.Enabled = false;
                //txtsupplier.Enabled = false;
                txtdate.Enabled = false;
                txtbutcherycost.Enabled = false;
                txtfreightcost.Enabled = false;

                simpleButton1.Enabled = true;
                addbtn.Enabled = false;
                updatebtn.Enabled = false;
                btncancel.Enabled = false;
                btnsave.Enabled = true;
            }
        }

        private void executor()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                double qty=0.0, cost=0.0, totalcost=0.0,total=0.0;
                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    qty = Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value.ToString());
                    cost = Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value.ToString());
                    total = qty * cost;
                    totalcost += total;
                    string query = "sp_addShipmentOrder";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                    com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                    com.Parameters.AddWithValue("@parmsupplier", searchLookUpEdit1.Text);
                    com.Parameters.AddWithValue("@parmproduct", dataGridView1.Rows[i].Cells[1].Value.ToString());
                    com.Parameters.AddWithValue("@parmtargetdate", txtdate.Text);
                    com.Parameters.AddWithValue("@parmqty", dataGridView1.Rows[i].Cells[4].Value.ToString());
                    com.Parameters.AddWithValue("@parmunits", dataGridView1.Rows[i].Cells[5].Value.ToString());
                    com.Parameters.AddWithValue("@parmcost", dataGridView1.Rows[i].Cells[6].Value.ToString());
                    com.Parameters.AddWithValue("@parmbutcherycost", dataGridView1.Rows[i].Cells[7].Value.ToString());
                    com.Parameters.AddWithValue("@parmfreightcost", dataGridView1.Rows[i].Cells[8].Value.ToString());
                    com.Parameters.AddWithValue("@parmremarks", txtremarks.Text);
                    com.Parameters.AddWithValue("@parmorderedby", Login.Fullname);
                    com.Parameters.AddWithValue("@parmbutcheryvendor", dataGridView1.Rows[i].Cells[9].Value.ToString());
                    com.Parameters.AddWithValue("@parmfreightvendor", dataGridView1.Rows[i].Cells[10].Value.ToString());
                    com.Parameters.AddWithValue("@parmbutcheryvendorinvoiceno", txtbutcheryinvoiceno.Text);
                    com.Parameters.AddWithValue("@parmfreightvendorinvoiceno", txtfreightinvoiceno.Text);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                }
                //string supplierid = Database.getSingleQuery("Supplier", "SupplierName='" + searchLookUpEdit1.Text + "'", "SupplierID");
                //string refnum = IDGenerator.getReferenceNumber();
                //Database.ExecuteQuery("INSERT INTO SupplierLedger VALUES('" + supplierid + "','" + DateTime.Now.ToShortDateString() + "','" + txtremarks.Text + "','PRCHSE','" + DateTime.Now.ToShortDateString() + "',0,0,'"+ totalcost + "','0','"+ totalcost + "','" + Login.Fullname + "','','"+ totalcost + "','UNPAID','',0)");
                //Database.ExecuteQuery("exec sp_PayShipmentOrder '"+txtshipmentno.Text+"','"+ refnum + "','888','"+Login.Fullname+"','"+totalcost+"' ");
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

        //void payshipment()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_addShipmentOrder";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
        //    com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
        //    com.Parameters.AddWithValue("@parmsupplier", searchLookUpEdit1.Text);
        //    com.Parameters.AddWithValue("@parmproduct", dataGridView1.Rows[i].Cells[1].Value.ToString());
        //    com.Parameters.AddWithValue("@parmtargetdate", txtdate.Text);
        //    com.Parameters.AddWithValue("@parmqty", dataGridView1.Rows[i].Cells[4].Value.ToString());
        //    com.Parameters.AddWithValue("@parmunits", dataGridView1.Rows[i].Cells[5].Value.ToString());
        //    com.Parameters.AddWithValue("@parmcost", dataGridView1.Rows[i].Cells[6].Value.ToString());
        //    com.Parameters.AddWithValue("@parmbutcherycost", dataGridView1.Rows[i].Cells[7].Value.ToString());
        //    com.Parameters.AddWithValue("@parmfreightcost", dataGridView1.Rows[i].Cells[8].Value.ToString());
        //    com.Parameters.AddWithValue("@parmremarks", txtremarks.Text);
        //    com.Parameters.AddWithValue("@parmorderedby", Login.isglobalUserID);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    com.ExecuteNonQuery();
        //    con.Close();
        //}

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void txtproduct_Click(object sender, EventArgs e)
        {
            
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            txtshipmentno.Enabled = false;
            txtmetrics.Enabled = false;
            txtproductcategory.Enabled = false;
            txtproduct.Enabled = false;
            txtqty.Enabled = false;
            txtremarks.Enabled = false;
            //txtsupplier.Enabled = false;
            searchLookUpEdit1.Enabled = false;
            txtdate.Enabled = false;
            txtbutcherycost.Enabled = false;
            txtfreightcost.Enabled = false;

            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            btncancel.Enabled = false;
            btnsave.Enabled = false;

            txtshipmentno.Text = "";
            txtdate.Text = "";
            txtmetrics.Text = "";
            txtproductcategory.Text = "";
            txtproduct.Text = "";
            txtqty.Text = "";
            txtremarks.Text = "";
            //txtsupplier.Text = "";
            txtcost.Text = "";
            txtfreightcost.Text = "";
            txtbutcherycost.Text = "";
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        String getProduct()
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            string str;
            str = Database.getSingleQuery("OrderDetails", "ShipmentNo='" + dataGridView1.Rows[cord].Cells[1].Value.ToString() + "'", "Description");
            return str;
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
          
                txtmetrics.Text = "";
                txtproductcategory.Text = "";
                txtproduct.Text = "";
                txtqty.Text = "";
                txtcost.Text = "";
                txtbutcherycost.Text = "";
                txtfreightcost.Text = "";

                txtproduct.Text = dataGridView1.Rows[cord].Cells[2].Value.ToString();
                txtproductcategory.Text = dataGridView1.Rows[cord].Cells[3].Value.ToString();
                txtqty.Text = dataGridView1.Rows[cord].Cells[4].Value.ToString();
                txtmetrics.Text = dataGridView1.Rows[cord].Cells[5].Value.ToString();
                txtcost.Text = dataGridView1.Rows[cord].Cells[6].Value.ToString();
                txtbutcherycost.Text = dataGridView1.Rows[cord].Cells[7].Value.ToString();
                txtfreightcost.Text = dataGridView1.Rows[cord].Cells[8].Value.ToString();
                
                simpleButton1.Enabled = false;
                addbtn.Enabled = false;
                updatebtn.Enabled = true;
                btncancel.Enabled = true;
                btnsave.Enabled = false;
            //}
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            //bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete ShipmentOrder");
            //if (ok)
            //{
            //    Database.ExecuteQuery("DELETE FROM ShipmentOrder WHERE ShipmentNo='" + dataGridView1.Rows[cord].Cells[1].Value.ToString() + "'");
            //    Database.ExecuteQuery("DELETE FROM OrderDetails WHERE ShipmentNo='" + dataGridView1.Rows[cord].Cells[1].Value.ToString() + "'", "Successfully Deleted");
            //    display();
            //}
            dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
        }

        private void txtsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT distinct ProductCategoryDescription FROM InventoryCost WHERE SupplierName='" + searchLookUpEdit1.Text+ "' ", "ProductCategoryDescription", txtproductcategory);

        }

        private void txtsupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            //string cost = Database.getSingleData("InventoryCost", "SupplierID", "'" + getSupplierID() + "'", "CostKg");
            //txtcost.Text = cost;
        }

        String getProductCode()
        {
            string str = "";
            str = Database.getSingleQuery("InventoryCost", "ProductName='" + txtproduct.Text + "'", "ProductCode");
            return str;
        }

        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cost = Database.getSingleQuery("InventoryCost", "SupplierID='" + searchLookUpEdit1.Text + "' AND ProductCode='"+getProductCode()+"'", "CostKg");
            txtcost.Text = cost;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            executor();
            XtraMessageBox.Show("Successfully Updated!");
            this.Dispose();
        }

        private void txtbutcherycost_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void txtfreightcost_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void txtproductcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT * FROM InventoryCost WHERE ProductCategoryDescription='" + txtproductcategory.Text + "' AND SupplierID='" + searchLookUpEdit1.Text+"'", "ProductName", txtproduct);
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT distinct ProductCategoryDescription FROM InventoryCost WHERE SupplierID='" + searchLookUpEdit1.Text + "' ", "ProductCategoryDescription", txtproductcategory);

        }

        private void chckbutchery_CheckedChanged(object sender, EventArgs e)
        {
            if (chckbutchery.Checked==true)
            {
                butcheryvendor.Enabled = true;
                txtbutcherycost.Enabled = true;
            }
            else
            {
                butcheryvendor.Enabled = false;
                txtbutcherycost.Enabled = false;
            }
        }

        private void chckfreight_CheckedChanged(object sender, EventArgs e)
        {
            if (chckfreight.Enabled == true)
            {
                freightvendor.Enabled = true;
                freightvendor.Enabled = true;
            }
            else
            {
                freightvendor.Enabled = false;
                freightvendor.Enabled = false;
            }
        }

        void displayvendor()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", butcheryvendor, "SupplierID", "SupplierID");
           
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", freightvendor, "SupplierID", "SupplierID");
        }
        void displaySupplier()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", searchLookUpEdit1, "SupplierID", "SupplierID");
        }
    }
}