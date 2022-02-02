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
    public partial class EditOrderDetailsDevEx : DevExpress.XtraEditors.XtraForm
    {
        DataTable table = new DataTable();
        public EditOrderDetailsDevEx()
        {
            InitializeComponent();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F1)
            {
                simpleButton5.PerformClick();
            }
            if (keyData == Keys.F5)
            {
                simpleButton2.PerformClick();
            }
            else if (keyData == Keys.Escape)
            {
                simpleButton4.PerformClick();
            }
            return functionReturnValue;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Text == "" || txtprodname.Text.Trim() == "" || comboBox1.Text == "")
            {
                XtraMessageBox.Show("Fields must not Empty");
            }
            else
            {
                int count = 0;
                bool checkifexists = Database.checkifExist("SELECT PONumber FROM PurchaseOrderDetails WHERE PONumber='" + txtponum.Text + "' AND ProductName='" + txtprodname.Text.Trim() + "'");

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "ProductName").ToString() == txtprodname.Text.Trim())
                    {
                         count = 1;
                    }
                    else
                    {
                        count += 0;
                    }
                }

                if (checkifexists)
                {
                    bool ok = HelperFunction.ConfirmDialog("Product is Already Exist!. Are you Sure you want to Continue?", "Product Exists");
                    if (ok)
                    {
                        add2();
                    }
                }
           
                else
                {
                    add2();
                }
            }
            txteffectivedate.Enabled = false;
            gridView1.MoveLast();
        }

        void add2()
        {
            try
            {
                string custid = Classes.ClientAccount.getClientID(txtcust.Text);
                string reamrks = "", specialprice = "", sellingprice = "";
                string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
                string itemcode = Database.getSingleQuery("Products", "Description='" + txtprodname.Text + "' and ProductCategoryCode='" + prodcatcode + "'", "ProductCode");

                if (Login.assignedBranch == "888")
                {
                    if (checkBox1.Checked == true)
                    {
                        reamrks = richTextBox1.Text.Trim();
                    }
                    else
                    {
                        reamrks = Database.getSingleQuery("CustomerProductSetting", "ProductCode = '" + itemcode + "' AND CustID='" + custid + "' ", "Remarks");
                    }
                    //check if item exist in customer table
                    // bool prodexist = Database.checkifExist("SELECT * FROM Customers WHERE ItemCode = '" + itemcode + "' AND CustomerID='" + custid + "' ");// Database.getSingleQuery("Customers","ItemCode = '"+Classes.Product.getProductCode(txtprodname.Text,prodcatcode)+"',"sdd");
                    bool prodexist = Database.checkifExist("SELECT ProductCode FROM view_custprodsettings WHERE ProductCode = '" + itemcode + "' AND CustID='" + custid + "' ");// Database.getSingleQuery("Customers","ItemCode = '"+Classes.Product.getProductCode(txtprodname.Text,prodcatcode)+"',"sdd");

                    //reamrks = Database.getSingleQuery("Customers", "ItemCode = '" + itemcode + "' AND CustomerID='" + custid + "' ", "Remarks");
                    //specialprice = Database.getSingleQuery("Customers", "ItemCode = '" + itemcode + "' AND CustomerID='" + custid + "' ", "SpecialPriceAmount");
                    specialprice = Database.getSingleQuery("CustomerProductSetting", "ProductCode = '" + itemcode + "' AND CustID='" + custid + "' ", "SpecialPriceAmount");

                    sellingprice = specialprice;
                    if (specialprice == null || specialprice == "")
                    {
                        sellingprice = Database.getSingleQuery("Products", "ProductCode = '" + itemcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "SellingPrice");
                    }

                    if (prodexist)
                    {
                        //get the item code and specialpriceamount and remarks
                    }
                }
                else
                {
                    reamrks = richTextBox1.Text.Trim();
                    sellingprice = Database.getSingleQuery("Products", "ProductCode = '" + itemcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "SellingPrice");
                }

                //string itemremarks = richTextBox1.Text.Trim();

                string prodcode = Database.getSingleQuery("Products", "Description='" + txtprodname.Text + "'", "ProductCode");
                DataRow newRow = table.NewRow();
                newRow["PONumber"] = txtponum.Text;
                newRow["BranchCode"] = Login.assignedBranch;
                newRow["ProductCode"] = getProductCode();
                newRow["ProductName"] = txtprodname.Text;
                newRow["Qty"] = spinEdit1.Text;
                newRow["Units"] = comboBox1.Text;
                newRow["SellingPrice"] = sellingprice;
                newRow["Status"] = "FOR APPROVAL";
                newRow["Remarks"] = reamrks;

                table.Rows.Add(newRow);
                gridControl1.DataSource = table;
                spinEdit1.Text = "";
                txtprodname.Text = "";
                richTextBox1.Text = "";
                txtprodname.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        String getProductCategoryCode()
        {
            string str;
            str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text.Trim() + "'", "ProductCategoryID");
            return str;
        }

        String getProductCode()
        {
            string str;
            str = Database.getSingleQuery("Products", "Description='" + txtprodname.Text.Trim() + "' AND ProductCategoryCode='" + getProductCategoryCode() + "'", "ProductCode");
            return str;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
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

        void saveAll()
        {
            try
            {
                double totalqty = 0.0;
                string approvalstatus = "", dateapproved = "", approvedby = "";
                DateTime dt = DateTime.Now;

                if (panel1.Visible == true)
                {
                    if (txtcust.Enabled == true && txtcust.Text == "")
                    {
                        XtraMessageBox.Show("Customer Field must be Selected!");
                        return;
                    }
                }
                approvalstatus = "FOR APPROVAL";
                dateapproved = "";
                approvedby = "";
          
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    //Database.ExecuteQuery("Update PurchaseOrderDetails Set isValid='1' WHERE ProductCode='"+gridView1.GetRowCellValue(i,"ProductCode").ToString()+"' AND PONumber='" + textEdit1.Text + "'");
                    Database.ExecuteQuery("INSERT INTO PurchaseOrderDetails VALUES('" + txtponum.Text + "','" + txtrefno.Text + "','" + Login.assignedBranch + "','" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridView1.GetRowCellValue(i, "ProductName").ToString() + "','" + gridView1.GetRowCellValue(i, "Qty").ToString() + "','" + gridView1.GetRowCellValue(i, "Units").ToString() + "','" + approvalstatus + "','" + String.Format("{0:MM/dd/yyyy}", dt) + "','" + txteffectivedate.Text + "','0','" + gridView1.GetRowCellValue(i, "Remarks").ToString() + "','','')");
                    totalqty += Convert.ToDouble(gridView1.GetRowCellValue(i, "Qty").ToString());
                }
                //Database.ExecuteQuery("UPDATE PurchaseOrderSummary SET ID=ID+1");
                Database.ExecuteQuery("INSERT INTO PurchaseOrderSummary VALUES('" + txtponum.Text + "','" + txtrefno.Text + "','" + txtcust.Text + "','" + Login.assignedBranch + "','" + totalqty + "','Kg','" + approvalstatus + "','" + String.Format("{0:MM/dd/yyyy}", dt) + "','" + txteffectivedate.Text + "','" + Login.isglobalUserID + "','" + approvedby + "','" + dateapproved + "','" + txtnote.Text + "','','0','0')", "Request Successfully Updated!");
                table.Clear();
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                //Database.ExecuteQuery("UPDATE PONumberGenerator SET ID = ID+1");
                //Database.ExecuteQuery("Update PurchaseOrderSummary SET isValid='1',DateAdded='" + DateTime.Now.ToString() + "' WHERE PONumber='" + textEdit1.Text + "'", "Transaction Saved!");
                /*****************************************************/
                
                //connectToServer();
                /*****************************************************/
                this.Dispose();
            }
            catch (SqlException sqx)
            {
                XtraMessageBox.Show(sqx.Message.ToString());
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }
    }
}