using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Web;
//using Excel = Microsoft.Office.Interop.Excel;

namespace SalesInventorySystem
{
    public partial class AddOrder : DevExpress.XtraEditors.XtraForm
    { 
        
        object var = null;
        private const int portNum = 8888;
        delegate void SetTextCallback(string text);
        object custkey = null;
        object srvcid = null,srvccustid=null; 
        private const string hostName = "192.168.99.143";
        DataTable table;
        //string itemno,desc,unitprice,sellingprice,prodname;
        //string number;
        public static string text_to_send="";
        public static string Isconnected = "";
        string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");

        public AddOrder()
        {
            InitializeComponent();
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            //if (keyData == Keys.F1)
            //{
            //    simpleButton5.PerformClick();
            //}
            if (keyData == Keys.F5)
            {
                btnsave.PerformClick();
            }
            else if (keyData == Keys.Escape)
            {

                simpleButton9.PerformClick();
            }
            return functionReturnValue;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            if (panel1.Visible == true && txtcustomer.Text=="")
            {
                XtraMessageBox.Show("Customer Name Must Not Empty");
                return;
            }

            if (txtqty.Text == "" || txtpname.Text.Trim()=="" || comboBox1.Text=="")
            {
                XtraMessageBox.Show("Fields must not Empty");
            }
            //else if (Convert.ToDouble(spinEdit1.Text) < 1)
            //{
            //    XtraMessageBox.Show("Quantity must Non Negative Value!");
            //}
            else
            {
                int count = 0;
                bool checkifexists = Database.checkifExist("SELECT TOP(1) PONumber FROM PurchaseOrderDetails WHERE PONumber='" + textEdit1.Text + "' AND ProductName='" + txtpname.Text.Trim() + "'");
                
                for(int i=0;i<=gridView1.RowCount-1;i++)
                {
                    if (gridView1.GetRowCellValue(i, "ProductName").ToString() == txtpname.Text.Trim())
                    {
                        //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Qty", Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Qty").ToString()) + Convert.ToDouble(spinEdit1.Text));
                        count = 1;
                    }
                    else
                    {
                        count += 0;
                    }
                }
                if(count > 0)
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
            //MydataGridView1.CurrentCell = MydataGridView1.Rows[MydataGridView1.Rows.Count - 1].Cells[0];
        }

        void add2()
        {
            try
            {
                var row = Database.getMultipleQuery("Customers", "CustomerName='" + txtcustomer.Text + "'", "CustomerID,isActive");
                string custid = row["CustomerID"].ToString();// Database.getSingleQuery("Customers", "CustomerName='" + txtcustomer.Text + "'", "CustomerID");//Classes.ClientAccount.getClientID(txtcust.Text);
                string isactive = row["isActive"].ToString();//Database.getSingleQuery("Customers", "CustomerName='" + txtcustomer.Text + "'", "isActive");//Classes.ClientAccount.getClientID(txtcust.Text);
                string lapseTerm = Database.getSingleQuery("SalesSettings", "EnableInvoiceLapsingTerm is not null", "EnableInvoiceLapsingTerm");
                string reamrks = "",specialprice="",sellingprice="";
                string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
                //string itemcode = Database.getSingleQuery("Products", "Description='" + txtpname.Text + "' and ProductCategoryCode='" + prodcatcode + "' and BranchCode='888'", "ProductCode");
                string itemcode = Classes.Product.getProductCode(txtpname.Text, prodcatcode);
                string rs =Database.getSingleResultSet("SELECT dbo.func_checkLapseInvoice('" + custid + "')");


                if (Login.assignedBranch == "888")
                {
                    if(checkBox1.Checked==true)
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

                    if (Convert.ToBoolean(lapseTerm) == true)
                    {
                        if (Convert.ToInt32(rs) > 0)
                        {
                            XtraMessageBox.Show("The System found out that One of these Invoices are already in lapse in term.. Please Visit this Account and Settle first all pastdue invoice/s...");
                            return;
                        }
                    }

                    if (chckspecialprice.Checked == true)
                    {
                        specialprice = Database.getSingleQuery("CustomerProductSetting", "ProductCode = '" + itemcode + "' AND CustID='" + custid + "' ", "SpecialPriceAmount");
                        if (String.IsNullOrEmpty(specialprice))
                        {
                            XtraMessageBox.Show("Please check your customer product settings.. No Special Price defined");
                            return;
                        }
                        else
                        {
                            sellingprice = specialprice;
                        }
                           
                    }
                    else
                    {
                        sellingprice = Database.getSingleQuery("Products", "ProductCode = '" + itemcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "SellingPrice");
                    }

                    //specialprice = Database.getSingleQuery("CustomerProductSetting", "ProductCode = '" + itemcode + "' AND CustID='" + custid + "' ", "SpecialPriceAmount");
                    //sellingprice = specialprice;
                    //if (String.IsNullOrEmpty(specialprice) && chckspecialprice.Checked==true)
                    //{
                    //    XtraMessageBox.Show("Please check your customer product settings.. No Special Price defined");
                    //    return;
                    //}
                    //else if (String.IsNullOrEmpty(specialprice) && chckspecialprice.Checked == false)
                    //{
                    //    sellingprice = Database.getSingleQuery("Products", "ProductCode = '" + itemcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "SellingPrice");
                    //}
                }
                else
                {
                    reamrks = richTextBox1.Text.Trim();
                    sellingprice = Database.getSingleQuery("Products", "ProductCode = '" + itemcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "SellingPrice");
                }
                //if realtime inventory.. global settings
                if(chckfinal.Checked.Equals(true))
                {
                    if (Convert.ToInt32(txtqty.Text) > Database.getTotalSummation2("Inventory", "Branch='" + Login.assignedBranch + "' and Available > 0 and Product='" + getProductCode() + "' ", "Available"))
                    {
                        XtraMessageBox.Show("The System found out that the Quantity you Entered is Greater than your Inventory Stocks..");
                        return;
                    }
                    else
                    {
                        Database.ExecuteQuery($"EXEC dbo.sp_FiFoMapping '{DateTime.Now.ToShortDateString()}','{Login.assignedBranch}','{getProductCode()}','{txtqty.Text}','1' ");
                    }
                }
                //string itemremarks = richTextBox1.Text.Trim();
              
                string prodcode = Database.getSingleQuery("Products", "Description='" + txtpname.Text + "'", "ProductCode");
                DataRow newRow = table.NewRow();
                //newRow["PONumber"] = textEdit1.Text;
                //newRow["BranchCode"] = Login.assignedBranch;
                newRow["ProductCode"] = getProductCode();
                newRow["ProductName"] = txtpname.Text;
                newRow["Qty"] = txtqty.Text;
                newRow["Units"] = comboBox1.Text;
                newRow["SellingPrice"] = sellingprice;
                //newRow["Status"] = "FOR APPROVAL";
                newRow["Remarks"] = reamrks;
                
                table.Rows.Add(newRow);
                gridControl1.DataSource = table;
                txtqty.Text = "";
                txtpname.Text = "";
                richTextBox1.Text = "";
                txtpname.Focus();
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void addServices()
        {
            try
            {

                string custid = Database.getSingleQuery("Customers", "CustomerName='" + txtcustomersservices.Text + "'", "CustomerID");//Classes.ClientAccount.getClientID(txtcust.Text);
                string isactive = Database.getSingleQuery("Customers", "CustomerName='" + txtcustomersservices.Text + "'", "isActive");//Classes.ClientAccount.getClientID(txtcust.Text);
                string lapseTerm = Database.getSingleQuery("SalesSettings", "EnableInvoiceLapsingTerm is not null", "EnableInvoiceLapsingTerm");

                //string itemcode = Database.getSingleQuery("Products", "Description='" + txtpname.Text + "' and ProductCategoryCode='" + prodcatcode + "' and BranchCode='888'", "ProductCode");
                string sellingprice = "";
                string rs = Database.getSingleResultSet("SELECT dbo.func_checkLapseInvoice('" + custid + "')");


                if (Login.assignedBranch == "888")
                {
                    
                    
                    if (Convert.ToBoolean(lapseTerm) == true)
                    {
                        if (Convert.ToInt32(rs) > 0)
                        {
                            XtraMessageBox.Show("The System found out that One of these Invoices are already in lapse in term.. Please Visit this Account and Settle first all pastdue invoice/s...");
                            return;
                        }
                    }

                    sellingprice = Database.getSingleQuery("CustomerProductSetting", "ProductCode = '" + srvcid + "' AND CustID='"+srvccustid+"'  ", "SpecialPriceAmount");
                }
              
                 
                DataRow newRow = table.NewRow();
                //newRow["PONumber"] = textEdit1.Text;
                //newRow["BranchCode"] = Login.assignedBranch;
                newRow["ServiceCode"] = srvcid;
                newRow["ServiceName"] = txtservices.Text;
                newRow["Qty"] = txtqtyservices.Text;
                newRow["SellingPrice"] = sellingprice;
               

                table.Rows.Add(newRow);
                gridControlitem.DataSource = table;
                txtqtyservices.Text = "";
                txtservices.Text = "";
                txtservices.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void display()
        {
            Database.display("SELECT * FROM PurchaseOrderDetails WHERE PONumber='" + textEdit1.Text + "'", gridControl1, gridView1);
            gridView1.Columns[8].Visible = false;
        }

       
        private void AddOrder_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtprodcat;
            comboBox1.Text = "Kg";
            if (Login.assignedBranch == "888")
            {
                panel1.Visible = true;
                checkBox1.Visible = true;
                chckspecialprice.Visible = true;
            }


        }


        void loadMetrics()
        {
            Database.displayComboBoxItems("SELECT * FROM Metrics", "Metrics", comboBox1);
        }
      

        void populateCustomer(SearchLookUpEdit edit)
        {
            //Database.displayComboBoxItems("SELECT CustomerName FROM Customers", "CustomerName", txtcustomer);
            Database.displaySearchlookupEdit("Select CustomerKey,CustomerName FROM Customers", edit, "CustomerName", "CustomerName");
        }
        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string creditlimit = Database.getSingleQuery("Customers", "CustomerName='" + txtcustomer.Text + "'", "CustomerCreditLimit");
            string accountbalance = Database.getSingleQuery("ClientAccounts", "AccountID='" + Customers.getCustAccountID(txtcustomer.Text) + "'", "AccountBalance");
            if(Convert.ToDouble(accountbalance) > Convert.ToDouble(creditlimit))
            {
                XtraMessageBox.Show("Credit Limit Exceeded!...");
                return;
            }
            if(ordertype.Text=="" || ordertype == null)
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

        void saveAll()
        {
            try
            {
                int ctr = 1;
                double totalqty = 0.0;
                string dateapproved = "", approvedby = "";
                DateTime dt = DateTime.Now;

                if (panel1.Visible == true)
                {
                    if (txtcustomer.Enabled == true && txtcustomer.Text == "")
                    {
                        XtraMessageBox.Show("Customer Field must be Selected!");
                        return;
                    }
                }
            
                dateapproved = "";
                approvedby = "";
               

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("INSERT INTO PurchaseOrderDetails VALUES( '" + ctr + "','" + textEdit1.Text + "','" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridView1.GetRowCellValue(i, "ProductName").ToString() + "','" + gridView1.GetRowCellValue(i, "Qty").ToString() + "','" + gridView1.GetRowCellValue(i, "Units").ToString() + "','0','" + gridView1.GetRowCellValue(i, "Remarks").ToString() + "')");
                    totalqty += Convert.ToDouble(gridView1.GetRowCellValue(i, "Qty").ToString());
                    ctr += 1;
                }
                Database.ExecuteQuery("UPDATE PurchaseOrderDetails SET Remarks=Replace(replace(Remarks, char(10), ''), char(13), '') WHERE PONumber='" + textEdit1.Text + "' ");
                //Database.ExecuteQuery("INSERT INTO PurchaseOrderSummary VALUES('" + textEdit1.Text + "', '" + txtcustomer.Text + "','" + Login.assignedBranch + "','" + totalqty + "','Kg','" + approvalstatus + "','" + String.Format("{0:MM/dd/yyyy}", dt) + "','" + txteffectivedate.Text + "','" + Login.Fullname + "','" + approvedby + "','" + dateapproved + "','" + txtnote.Text + "','','0','0','"+ordertype.Text+"','"+reqtype+"','"+txtpaytype.Text+"')", "Request Successfully Updated!");
                Database.ExecuteQuery("INSERT INTO PurchaseOrderSummary VALUES('" + textEdit1.Text + "', '" + custkey+ "','" + Login.assignedBranch + "','" + totalqty + "','FOR APPROVAL','" + DateTime.Now.ToString() + "','" + txteffectivedate.Text + "','" + Login.Fullname + "','" + approvedby + "','" + dateapproved + "','" + txtnote.Text + "',' ','0','" + ordertype.Text + "','" + txtpaytype.Text + "')", "Request Successfully Updated!");
                table.Clear();
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
             
                Isconnected = "OK";
                if(chckfinal.Checked.Equals(true))
                {
                    saveFinalSalesOrder();
                }
                this.Dispose();
            }
            catch (SqlException sqx)
            {
                XtraMessageBox.Show(sqx.Message.ToString());
            }
        }

        void saveFinalSalesOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spitcr_AddSalesOrderFinal";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmpono",textEdit1.Text);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
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


        void saveAllServices()
        {
            try
            {
                int ctr = 1;
                double totalqty = 0.0;
                string dateapproved = "", approvedby = "";
                DateTime dt = DateTime.Now;

                if (panel1.Visible == true)
                {
                    if (txtcustomer.Enabled == true && txtcustomer.Text == "")
                    {
                        XtraMessageBox.Show("Customer Field must be Selected!");
                        return;
                    }
                }

                dateapproved = "";
                approvedby =   "";

                for (int i = 0; i <= gridViewitem.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("INSERT INTO PurchaseOrderDetails VALUES( '" + ctr + "','" + txtposervices.Text + "','" + gridViewitem.GetRowCellValue(i, "ServiceCode").ToString() + "','" + gridViewitem.GetRowCellValue(i, "ServiceName").ToString() + "','" + gridViewitem.GetRowCellValue(i, "Qty").ToString() + "',' ','0',' ')");
                    totalqty += Convert.ToDouble(gridViewitem.GetRowCellValue(i, "Qty").ToString());
                    ctr += 1;
                }
                Database.ExecuteQuery("UPDATE PurchaseOrderDetails SET Remarks=Replace(replace(Remarks, char(10), ''), char(13), '') WHERE PONumber='" + txtposervices.Text + "' ");
                Database.ExecuteQuery("INSERT INTO PurchaseOrderSummary VALUES('" + txtposervices.Text + "', '" + srvccustid.ToString() + "','" + Login.assignedBranch + "','" + totalqty + "','FOR APPROVAL','" + DateTime.Now.ToString() + "',' ','" + Login.Fullname + "','" + approvedby + "','" + dateapproved + "',' ',' ','0','MAIN','" + txtpaytypeservices.Text + "')", "Request Successfully Updated!");
                table.Clear();
                gridControlitem.DataSource = null;
                gridViewitem.Columns.Clear();

                Isconnected = "OK";
                this.Dispose();
            }
            catch (SqlException sqx)
            {
                XtraMessageBox.Show(sqx.Message.ToString());
            }
        }


        private void clear()
        {
            txtqty.Text = "";
            txtpname.Text = "";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount ==0)
            {
                XtraMessageBox.Show("No Such Line to cancel!");
            }
            else
            {
               
                gridView1.DeleteSelectedRows();
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void AddOrder_FormClosing(object sender, FormClosingEventArgs e)
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
                else
                {
                    e.Cancel = true;
                }
            }
            ///this.Close();
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
                   this.Dispose();
                    this.Close();
                }
                
            }
        }

        private void txtprodcat_Click(object sender, EventArgs e)
        {
            //Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
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
            str = Database.getSingleQuery("Products", "Description='" + txtpname.Text.Trim() + "' AND ProductCategoryCode='"+getProductCategoryCode()+"'", "ProductCode");
            return str;
        }

        private void txtprodname_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtqty.Focus();
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtpname.Text="";
            Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' and ProductCategoryCode='" + getProductCategoryCode() + "'", txtpname, "Description", "Description");
            
            txtpname.Focus();
        }

      
       
        private void simpleButton6_Click(object sender, EventArgs e)
        {
       
            FolderBrowserDialog folder = new FolderBrowserDialog();
            try
            {
                if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                
                    Main main = new Main();
                    main.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    main.notifyIcon1.BalloonTipTitle = "Successfully Exported";
                    main.notifyIcon1.BalloonTipText = "Your file successfully exported at " + folder.SelectedPath + "\\" +this.Text+ ".xls";
                    main.notifyIcon1.ShowBalloonTip(1000);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

      
   
        private void btnitemcancel_Click(object sender, EventArgs e)
        {
            if (gridViewitem.RowCount == 0)
            {
                XtraMessageBox.Show("No Such Line to cancel!");
            }
            else
            {
                gridViewitem.DeleteSelectedRows();
            }
        }

        

        private void btnitemclose_Click(object sender, EventArgs e)
        {
            if (gridViewitem.RowCount == 0)
            {
                this.Dispose();
                this.Close();
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Force Close the form? \n Note: All Transactions will be cancelled", "Force Close");
                if (ok)
                {
                    this.Dispose();
                    this.Close();
                }

            }
        }

        private void btnitemexport_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            try
            {
                if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Main main = new Main();
                    main.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    main.notifyIcon1.BalloonTipTitle = "Successfully Exported";
                    main.notifyIcon1.BalloonTipText = "Your file successfully exported at " + folder.SelectedPath + "\\" + this.Text + ".xls";
                    main.notifyIcon1.ShowBalloonTip(1000);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

       
        private void simpleButton8_Click(object sender, EventArgs e)
        {
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetPurchaseOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            txtprodcat.Enabled = true;
            txtpname.Enabled = true;
            txtprodcat.Focus();

            populateCustomer(txtcustomer);
            loadMetrics();

        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnadd.PerformClick();
        }

        private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void txtpname_EditValueChanged(object sender, EventArgs e)
        {
            var = SearchLookUpClass.getSingleValue(txtpname, "ProductCode");
            txtqty.Focus();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
            table = new DataTable();
            table.Columns.Add("ProductCode");
            table.Columns.Add("ProductName"); //UnitPrice
            table.Columns.Add("Qty"); //Total UnitPrice * weight
            table.Columns.Add("Units");
            table.Columns.Add("SellingPrice");
            table.Columns.Add("Remarks");
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();

            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetPurchaseOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            txtprodcat.Enabled = true;
            txtpname.Enabled = true;
            txtprodcat.Focus();

            populateCustomer(txtcustomer);
            loadMetrics();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {


            if (panel1.Visible == true && txtcustomer.Text == "")
            {
                XtraMessageBox.Show("Customer Name Must Not Empty");
                return;
            }

            if (txtqty.Text == "" || txtpname.Text.Trim() == "" || comboBox1.Text == "")
            {
                XtraMessageBox.Show("Fields must not Empty");
            }
             
            else
            {
                int count = 0;
                bool checkifexists = Database.checkifExist("SELECT top 1 PONumber FROM PurchaseOrderDetails WHERE PONumber='" + textEdit1.Text + "' AND ProductName='" + txtpname.Text.Trim() + "'");

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "ProductName").ToString() == txtpname.Text.Trim())
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            string creditlimit = Database.getSingleQuery("Customers", "CustomerName='" + txtcustomer.Text + "'", "CustomerCreditLimit");
            string accountbalance = Database.getSingleQuery("ClientAccounts", "AccountID='" + Customers.getCustAccountID(txtcustomer.Text) + "'", "AccountBalance");
            string enableCreditLimit = Database.getSingleQuery("SalesSettings", "EnableCreditLimit is not null", "EnableCreditLimit");

            if (Convert.ToBoolean(enableCreditLimit) == true)
            {
                if (Convert.ToDouble(accountbalance) > Convert.ToDouble(creditlimit))
                {
                    XtraMessageBox.Show("Credit Limit Exceeded!...");
                    return;
                }
            }
            if (String.IsNullOrEmpty(ordertype.Text))
            {
                XtraMessageBox.Show("Please Select Order Type!");
                return;
            }
            if (String.IsNullOrEmpty(txtpaytype.Text))
            {
                XtraMessageBox.Show("Please Select Payment Type!");
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
                    
                    this.Dispose();
                    this.Close();
                }

            }
        }

        private void btnexport_Click(object sender, EventArgs e)
        {
           
            FolderBrowserDialog folder = new FolderBrowserDialog();
            try
            {
                if (folder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    
                    Main main = new Main();
                    main.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    main.notifyIcon1.BalloonTipTitle = "Successfully Exported";
                    main.notifyIcon1.BalloonTipText = "Your file successfully exported at " + folder.SelectedPath + "\\" + this.Text + ".xls";
                    main.notifyIcon1.ShowBalloonTip(1000);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtcustomer_EditValueChanged(object sender, EventArgs e)
        {
            custkey = SearchLookUpClass.getSingleValue(txtcustomer, "CustomerKey");

        }

        void populateServices()
        {
            Database.displaySearchlookupEdit("Select * FROM SERVICES",txtservices,"SRVC_DESC","SRVC_DESC");
        }
        private void btnnewservices_Click(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("ServiceCode");
            table.Columns.Add("ServiceName"); //UnitPrice
            table.Columns.Add("Qty"); //Total UnitPrice * weight
            table.Columns.Add("SellingPrice"); 
            gridControlitem.DataSource = table;
            gridViewitem.BestFitColumns();

            txtposervices.Text = IDGenerator.getIDNumberSP("sp_GetPurchaseOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            txtservices.Enabled = true;
            populateCustomer(txtcustomersservices);
            populateServices();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

 
            if (String.IsNullOrEmpty(txtqtyservices.Text)  || String.IsNullOrEmpty(txtcustomersservices.Text) || String.IsNullOrEmpty(txtpaytypeservices.Text))
            {
                XtraMessageBox.Show("Fields must not Empty");
            }
            
            else
            {
                int count = 0;
                bool checkifexists = Database.checkifExist("SELECT top 1 PONumber FROM PurchaseOrderDetails WHERE PONumber='" + txtposervices.Text + "' AND ProductName='" + txtservices.Text.Trim() + "'");

                for (int i = 0; i <= gridViewitem.RowCount - 1; i++)
                {
                    if (gridViewitem.GetRowCellValue(i, "ServiceName").ToString() == txtservices.Text.Trim())
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
                    XtraMessageBox.Show("Services Already Exist");
                    return;
                }
                if (checkifexists)
                {
                    bool ok = HelperFunction.ConfirmDialog("Services is Already Exist!. Are you Sure you want to Continue?", "Services Exists");
                    if (ok)
                    {
                        addServices();
                        // display();
                    }
                }

                else
                {
                    addServices();
                    // display();
                }
            }
            gridViewitem.MoveLast();
        }

        private void txtservices_EditValueChanged(object sender, EventArgs e)
        {
            srvcid = SearchLookUpClass.getSingleValue(txtservices, "SRVC_ID");
        }

        double getCustBalance()
        {
            double balance = 0.0;
            balance = Database.getTotalSummation2("TransactionChargeSales", "CustomerKey='" + Classes.ClientAccount.getClientKey(srvccustid.ToString()) + "' AND PayStatus <> 'FULLYPAID' ", "Balance");
            return Math.Round(balance, 2);
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            string creditlimit = Database.getSingleQuery("Customers", "CustomerName='" + txtcustomersservices.Text + "'", "CustomerCreditLimit");
            string accountbalance = getCustBalance().ToString();
            string enableCreditLimit = Database.getSingleQuery("SalesSettings", "EnableCreditLimit is not null", "EnableCreditLimit");

            if (Convert.ToBoolean(enableCreditLimit) == true)
            {
                if (Convert.ToDouble(accountbalance) > Convert.ToDouble(creditlimit))
                {
                    XtraMessageBox.Show("Credit Limit Exceeded!...");
                    return;
                }
            }
           
            if (String.IsNullOrEmpty(txtpaytypeservices.Text))
            {
                XtraMessageBox.Show("Please Select Payment Type!");
                return;
            }
            if (gridViewitem.RowCount <= 0)
            {
                XtraMessageBox.Show("Please Input Product Details!");
                return;
            }
            else
            {
                saveAllServices();
            }
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            if (gridViewitem.RowCount == 0)
            {
                XtraMessageBox.Show("No Such Line to cancel!");
            }
            else
            {
                gridViewitem.DeleteSelectedRows();
            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (gridViewitem.RowCount == 0)
            {
                this.Dispose();
                this.Close();
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Force Close the form? \n Note: All Transactions will be cancelled", "Force Close");
                if (ok)
                {

                    this.Dispose();
                    this.Close();
                }

            }
        }

        private void txtcustomersservices_EditValueChanged(object sender, EventArgs e)
        {
            srvccustid = SearchLookUpClass.getSingleValue(txtcustomersservices, "CustomerKey");
        }
    }
}