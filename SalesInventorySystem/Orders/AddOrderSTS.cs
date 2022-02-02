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

namespace SalesInventorySystem.Orders
{
    public partial class AddOrderSTS : Form
    {
        DataTable table; 
        string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");
        object var = null;
        string productcategorycode = "";
        public AddOrderSTS()
        {
            InitializeComponent();
        }

        private void AddOrderSTS_Load(object sender, EventArgs e)
        {
            //this.ActiveControl = txtprodcat;
            panelControl2.Visible = false;
            comboBox1.Text = "Kg";
        
            table = new DataTable();
         
            table.Columns.Add("ProductCode");
            table.Columns.Add("ProductName"); //UnitPrice
            table.Columns.Add("Qty"); //Total UnitPrice * weight
            table.Columns.Add("Units");
            table.Columns.Add("SellingPrice"); 
            table.Columns.Add("Remarks");
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
          
            if (keyData == Keys.F5)
            {
                btnsave.PerformClick();
            }
            else if (keyData == Keys.F1)
            {
                simpleButton3.PerformClick();
            }
            else if (keyData == Keys.Escape)
            {
                simpleButton9.PerformClick();
            }
            return functionReturnValue;
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            //textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetTransferOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetPurchaseOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            //txtprodcat.Enabled = true;
            //txtpname.Enabled = true;
            //txtprodcat.Focus(); 
            loadMetrics();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //if (txtqty.Text == "" || txtpname.Text.Trim() == "" || comboBox1.Text == "")
            if (txtqty.Text == "" || comboBox1.Text == "")
            {
                XtraMessageBox.Show("Fields must not Empty");
            }
            else
            {
                int count = 0;
                //bool checkifexists = Database.checkifExist("SELECT PONumber FROM TransferOrderDetails WHERE PONumber='" + textEdit1.Text + "' AND ProductName='" + txtpname.Text.Trim() + "'");
                bool checkifexists = Database.checkifExist("SELECT PONumber FROM TransferOrderDetails WHERE PONumber='" + textEdit1.Text + "' AND ProductName='" + txtpcode.Text.Trim() + "'");

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    //if (gridView1.GetRowCellValue(i, "ProductName").ToString() == txtpname.Text.Trim())
                    if (gridView1.GetRowCellValue(i, "ProductName").ToString() == txtpcode.Text.Trim())
                    {
                        //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Qty", Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Qty").ToString()) + Convert.ToDouble(spinEdit1.Text));
                        count = 1;
                    }
                    else
                    {
                        count += 0;
                    }
                }
                if (count > 0)
                {
                    XtraMessageBox.Show("Product Already Exist");
                    return;
                }
                if (checkifexists)
                {
                    bool ok = HelperFunction.ConfirmDialog("Product is Already Exist!. Are you Sure you want to Continue?", "Product Exists");
                    if (ok)
                    {
                        add2();
                        // display();
                    }
                }

                else
                {
                    add2();
                    // display();
                }
            }
            txteffectivedate.Enabled = false;
            gridView1.MoveLast();
        }

        void add2()
        {
            try
            {
                string destinationBranch = "";
                if (radho.Checked == true)
                {
                    destinationBranch = "888";
                }
                else
                {
                    destinationBranch = txtbranch.Text;
                }
                string reamrks = "",  sellingprice = "";
                //string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
                string prodcatcode = Classes.Product.getProductCategoryCode(txtpcat.Text);
                //string itemcode = Database.getSingleQuery("Products", "Description='" + txtpname.Text + "' and ProductCategoryCode='" + prodcatcode + "'", "ProductCode");
                string itemcode = Database.getSingleQuery("Products", "Description='" + txtpcode.Text + "' and ProductCategoryCode='" + prodcatcode + "'", "ProductCode");

                //string custterm = Database.getSingleQuery("Customers", "CustomerName='" + txtcustomer.Text + "'", "Term");//Classes.ClientAccount.getClientID(txtcust.Text);
                //int term = Convert.ToInt16(custterm);
                reamrks = richTextBox1.Text.Trim();
                sellingprice = Database.getSingleQuery("Products", "ProductCode = '" + itemcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "SellingPrice");
              
                //string itemremarks = richTextBox1.Text.Trim();

                //string prodcode = Database.getSingleQuery("Products", "Description='" + txtpname.Text + "'", "ProductCode");
                string prodcode = Database.getSingleQuery("Products", "Description='" + txtpcode.Text + "'", "ProductCode");
                DataRow newRow = table.NewRow();
                newRow["ProductCode"] = getProductCode();
                //newRow["ProductName"] = txtpname.Text;
                newRow["ProductName"] = txtpcode.Text;
                newRow["Qty"] = txtqty.Text;
                newRow["Units"] = comboBox1.Text;
                newRow["SellingPrice"] = sellingprice;
                newRow["Remarks"] = reamrks;

                table.Rows.Add(newRow);
                gridControl1.DataSource = table;
                txtqty.Text = "";
                //txtpname.Text = "";
                txtpcode.Text = "";
                richTextBox1.Text = "";
                /*txtpname*/
                txtpcode.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void loadMetrics()
        {
            Database.displayComboBoxItems("SELECT * FROM Metrics", "Metrics", comboBox1);
        }
      

        void saveAll()
        {
            try
            {
                int ctr = 1;
                string destinationBranch = "";
                if (radho.Checked == true)
                {
                    destinationBranch = "888";
                }
                else
                {
                    destinationBranch = txtbranch.Text;
                }
                double totalqty = 0.0;
                string approvalstatus = "", dateapproved = "", approvedby = "";
                DateTime dt = DateTime.Now;

           
                approvalstatus = "FOR APPROVAL";
                dateapproved = "";
                approvedby = "";

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("INSERT INTO TransferOrderDetails VALUES('" + textEdit1.Text + "','"+ctr+"','" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridView1.GetRowCellValue(i, "ProductName").ToString() + "','" + gridView1.GetRowCellValue(i, "Qty").ToString() + "','" + gridView1.GetRowCellValue(i, "Units").ToString() + "', '0','" + gridView1.GetRowCellValue(i, "Remarks").ToString() + "' )");
                    totalqty += Convert.ToDouble(gridView1.GetRowCellValue(i, "Qty").ToString());
                    ctr += 1;
                }
                Database.ExecuteQuery("UPDATE TransferOrderDetails SET Remarks=Replace(replace(Remarks, char(10), ''), char(13), '') WHERE PONumber='" + textEdit1.Text + "' ");

                Database.ExecuteQuery("INSERT INTO TransferOrderSummary VALUES('" + textEdit1.Text + "','" + Login.assignedBranch + "','" + destinationBranch + "','" + totalqty + "','" + approvalstatus + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + txteffectivedate.Text + "','" + Login.Fullname + "','" + approvedby + "','" + dateapproved + "',' ','" + richTextBox1.Text + "',' ',0,'" + ordertype.Text + "','"+txtgroup.Text+"')", "Request Successfully Updated!");
                table.Clear();
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

           
                this.Dispose();
            }
            catch (SqlException sqx)
            {
                XtraMessageBox.Show(sqx.Message.ToString());
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
            if (ordertype.Text == "" || ordertype == null)
            {
                XtraMessageBox.Show("Please Select Order Type!");
                return;
            }
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("Please Input Product Details!");
                return;
            }
            else
            {
                saveAll();
            }
        }
        private void clear()
        {
            txtqty.Text = "";
            txtpcode.Text = "";
            //txtpname.Text = "";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Such Line to cancel!");
            }
            else
            {
                
                gridView1.DeleteSelectedRows();
            }
        }
        private void display()
        {
            Database.display("SELECT * FROM TransferOrderDetails WHERE PONumber='" + textEdit1.Text + "'", gridControl1, gridView1);
            //gridView1.Columns[8].Visible = false;
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F1)
            //{
            //    simpleButton3.PerformClick();
            //    display();
            //}
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                this.Dispose();
                this.Close();
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Force Close the form? \n Note: All Transactions will be cancelled", "Force Close");
                if (ok)
                {
                    //  Database.ExecuteQuery("DELETE FROM Inventory WHERE ReferenceCode='" + txtcarcasssku.Text + "'");
                    this.Dispose();
                    this.Close();
                }

            }
        }

        private void txtprodcat_Click(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
        }
        String getProductCategoryCode()
        {
            string str;
            str = Database.getSingleQuery("ProductCategory", "Description='" + txtpcat.Text.Trim() + "'", "ProductCategoryID");
            //str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text.Trim() + "'", "ProductCategoryID");
            return str;
        }

        String getProductCode()
        {
            string str;
            str = Database.getSingleQuery("Products", "Description='" + txtpcode.Text.Trim() + "' AND ProductCategoryCode='" + getProductCategoryCode() + "'", "ProductCode");
            //str = Database.getSingleQuery("Products", "Description='" + txtpname.Text.Trim() + "' AND ProductCategoryCode='" + getProductCategoryCode() + "'", "ProductCode");
            return str;
        }

        private void txtprodname_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtqty.Focus();
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtprodname.Text = "";
            //Database.displayComboBoxItems("SELECT distinct Description FROM Products WHERE ProductCategoryCode='" + getProductCategoryCode() + "' ORDER BY Description", "Description", txtprodname);
            //txtprodname.Focus();
            txtpname.Text = "";

            Database.displaySearchlookupEdit("SELECT b.Description as Category,a.ProductCode,a.Description,a.Barcode FROM " +
              "Products as a " +
              "INNER JOIN ProductCategory as b " +
              "ON a.ProductCategoryCode=b.ProductCategoryID " +
              "AND a.BranchCode='" + Login.assignedBranch + "' " +
              "WHERE a.BranchCode='" + Login.assignedBranch + "' " +
              "AND a.ProductCategoryCode='"+ getProductCategoryCode() + "'", txtpname, "Description", "Description");

            //Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' and ProductCategoryCode='" + getProductCategoryCode() + "'", txtpname, "Description", "Description");

            txtpname.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            
        }

        private void txtprodname_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnadd.PerformClick();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            //textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetTransferOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetPurchaseOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            //txtprodcat.Enabled = true;
            txtgroup.Enabled = true;
            //txtpname.Enabled = true;
            //txtprodcat.Focus();
           
            loadMetrics();
            Database.displayComboBoxItems("Select CategoryName FROM GroupCategory", "CategoryName", txtgroup);
          
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //if (txtqty.Text == "" || txtpname.Text.Trim() == "" || comboBox1.Text == "")
            if (txtqty.Text == "" ||  comboBox1.Text == "")
            {
                XtraMessageBox.Show("Fields must not Empty");
            }
            else
            {
                int count = 0;
                //bool checkifexists = Database.checkifExist("SELECT PONumber FROM TransferOrderDetails WHERE PONumber='" + textEdit1.Text + "' AND ProductName='" + txtpname.Text.Trim() + "'");
                bool checkifexists = Database.checkifExist("SELECT PONumber FROM TransferOrderDetails WHERE PONumber='" + textEdit1.Text + "' AND ProductName='" + txtpcode.Text.Trim() + "'");

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "ProductName").ToString() == txtpcode.Text.Trim())
                    //if (gridView1.GetRowCellValue(i, "ProductName").ToString() == txtpname.Text.Trim())
                    {
                        //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Qty", Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Qty").ToString()) + Convert.ToDouble(spinEdit1.Text));
                        count = 1;
                    }
                    else
                    {
                        count += 0;
                    }
                }
                if (count > 0)
                {
                    XtraMessageBox.Show("Product Already Exist");
                    return;
                }
                if (checkifexists)
                {
                    bool ok = HelperFunction.ConfirmDialog("Product is Already Exist!. Are you Sure you want to Continue?", "Product Exists");
                    if (ok)
                    {
                        add2();
                        // display();
                    }
                }

                else
                {
                    add2();
                    // display();
                }
            }
            txteffectivedate.Enabled = false;
            gridView1.MoveLast();
            simpleButton3.Focus();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (radothers.Checked == true && String.IsNullOrEmpty(txtbranch.Text))
            {
                XtraMessageBox.Show("Please Select Branch!..");
                return;
            }
            if(String.IsNullOrEmpty(textEdit1.Text))
            {
                XtraMessageBox.Show("Order Number must not Empty...");
                return;
            }
            if (String.IsNullOrEmpty(txtgroup.Text))
            {
                XtraMessageBox.Show("Group Category Must Not Empty!...");
                return;
            }
            if (ordertype.Text == "" || ordertype == null)
            {
                XtraMessageBox.Show("Please Select Order Type!");
                return;
            }
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("Please Input Product Details!");
                return;
            }
            else
            {
                saveAll();
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Such Line to cancel!");
            }
            else
            {

                gridView1.DeleteSelectedRows();
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                this.Dispose();
                this.Close();
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Force Close the form? \n Note: All Transactions will be cancelled", "Force Close");
                if (ok)
                {
                    //  Database.ExecuteQuery("DELETE FROM Inventory WHERE ReferenceCode='" + txtcarcasssku.Text + "'");
                    this.Dispose();
                    this.Close();
                }

            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        private void txtpname_EditValueChanged(object sender, EventArgs e)
        {
            var = SearchLookUpClass.getSingleValue(txtpname, "ProductCode");
            txtqty.Focus();
        }

        void radioChanged()
        {
            if (radho.Checked == true)
            {
                panelControl2.Visible = false;
            }
            else
            {
                panelControl2.Visible = true;
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches WHERE (BranchCode <> '888' OR BranchCode <> '"+Login.assignedBranch+"') ORDER BY BranchCode", txtbranch, "BranchCode", "BranchCode");
            }
        }

        private void radho_CheckedChanged(object sender, EventArgs e)
        {
            radioChanged();
        }

        private void radothers_CheckedChanged(object sender, EventArgs e)
        {
            radioChanged();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts();
            searchProd.txtbox.Text = "ADDORDER";
            searchProd.ShowDialog(this);
            if (HOForms.SearchProducts.isdone == true)
            {
                productcategorycode = HOForms.SearchProducts.prodcatcode;
                string productcategorydesc = Database.getSingleQuery("ProductCategory", "ProductCategoryID='" + productcategorycode + "'", "Description");
                txtpcat.Text = productcategorydesc;
                txtpcode.Text = HOForms.SearchProducts.prodname;
                HOForms.SearchProducts.isdone = false;
                searchProd.Dispose();
                txtqty.Focus();
            }


        }
    }
}
