using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.IO.Ports;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem
{
    public partial class AddInventory : DevExpress.XtraEditors.XtraForm
    {
        // string exec = "";
        bool iserror = false;
        //DataTable table=null;
        //string productcategorycode;
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
        // private String weight;
        public string wieght2 = "";
        public static bool isdone = false;
        public AddInventory()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F10)
            {
                simpleButton2.PerformClick();
            }
            else if (keyData == Keys.F6)
            {
                btnclear.PerformClick();
            }
            else if (keyData == Keys.F1)
            {
                txtsearch.PerformClick();
            }
            return functionReturnValue;
        }
        public void AddDataMethod(String myString)
        {
            txtweight.AppendText(myString);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //bool isproductexist = Database.checkifExist("SELECT * FROM OrderDetails WHERE ShipmentNo='" + txtshipmentno.Text + "' AND Description='" + txtproduct.Text + "'");
            bool isproductexist = true;
            if (checkBox3.Checked == true && String.IsNullOrEmpty(txtcost.Text))
            {
                XtraMessageBox.Show("Please Input Cost Field!");
                return;
            }
            if (txtbarcode.Text == "")
            {
                XtraMessageBox.Show("Textfield must not empty");
                txtbarcode.Text = "";
                txtbarcode.Focus();
            }
            else if (!isproductexist)
            {
                XtraMessageBox.Show("Product Not Exist in OrderDetails");
                txtbarcode.Text = "";
                txtbarcode.Focus();
            }
            else
            {
                if (chckboxbarcode.Checked == true)
                {
                    simpleButton5.PerformClick();
                    InsertData();
                    display();
                    txtbarcode.Text = "";
                    txtweight.Text = "";
                    txtweight.Focus();
                }
                else
                {
                    InsertData();
                    display();
                    txtbarcode.Text = "";
                    txtweight.Text = "";
                    txtweight.Focus();
                }
            }



        }

        void InsertData()
        {
            bool isoverride = false;
            if(checkBox3.Checked==true)
            {
                isoverride = true;
            }else
            {
                isoverride = false;
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_AddTempInventory";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmpalletno", txtpalletno.Text);
                com.Parameters.AddWithValue("@parmcategory", txtprodcat.Text);
                com.Parameters.AddWithValue("@parmprodtype", txtproduct.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtbarcode.Text);
                com.Parameters.AddWithValue("@parmtipweight", txtweight.Text);
                com.Parameters.AddWithValue("@parmqty", txtweight.Text);
                com.Parameters.AddWithValue("@parmcost", txtcost.Text);
                com.Parameters.AddWithValue("@parmisoverride", isoverride);
                com.Parameters.AddWithValue("@parmdestination", txtdestination.Text);
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
        private void clear()
        {
           
        }

       
        void addInventory()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_ImportCarcassInventory";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmreceivedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmdestination", txtdestination.Text.Trim());
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
        
        void checkBatchUpload()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_CheckBatchUploadedItems";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.Add("@parmiserror", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;

                iserror = Convert.ToBoolean(com.Parameters["@parmiserror"].Value);
                if (iserror == true)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable table = new DataTable();
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = null;
                    adapter.Fill(table);
                    gridControl1.DataSource = table;
                    gridView1.BestFitColumns();
                }
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
        void uploadDB()
        {
            //addInventory();
            checkBatchUpload();
            if (iserror == true)
            {
                display();
                return;
            }
            else
            {
                finalupdate();
                isdone = true;
            }
           
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int totalorders = Database.getCountData("SELECT COUNT(distinct(ProductCode)) as Counter FROM OrderDetails  WHERE ShipmentNo=" + txtshipmentno.Text + "", "Counter");
            int totalreceive = Database.getCountData("SELECT COUNT(distinct(Product)) as Counter FROM TempInventory  WHERE ShipmentNo=" + txtshipmentno.Text + "", "Counter");
            if (txtdestination.Text == "" || txtinvoicedate.Text == "" || txtinvoiceno.Text == "" || txtduedate.Text == "")
            {
                XtraMessageBox.Show("Please Input All Fields!");
                return;
            }
            
            if (totalorders != totalreceive)
            {
                bool confirm = HelperFunction.ConfirmDialog("The System found out that there are remaining items in OrderDetails that you do not receive.. Are you sure you want to Continue", "Dscrepancy");
                if (confirm)
                {
                    uploadDB();
                    XtraMessageBox.Show("Successfully Added!");
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                uploadDB();
                XtraMessageBox.Show("Successfully Added!");
                this.Close();
            }
        }

        void loadgridview1()
        {
            display();
        }

        private void AddInventory_Load(object sender, EventArgs e)
        {
            getAvailablePort();
            //txtshipmentno.Text = ViewShipmentDashboard.shipmentno;
            //txtrefno.Text = IDGenerator.getReferenceNumber();

            txtrefno.Text = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            loadgridview1();
            txtdestination.Text = "Commissary";
            populateProductCategory();
        }

        void populateProductCategory()
        {
            //Database.displayComboBoxItems("select Description FROM ProductCategory WHERE ProductCategoryID in (Select SUBSTRING(CAST(ProductCode as varchar(10)),1,2) as ProductCode  from OrderDetails WHERE ShipmentNo='" + txtshipmentno.Text + "')", "Description", txtprodcat);
            Database.displayComboBoxItems("select Description FROM ProductCategory WHERE ProductCategoryID in (Select SUBSTRING(CAST(OrderCode as varchar(10)),1,2) as ProductCode  from PODETAILS WHERE ShipmentNo='" + txtshipmentno.Text + "' and OrderType='P')", "Description", txtprodcat);
        }

        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtcomport.Items.AddRange(ports);
        }

        private void txttotalkg_KeyDown(object sender, KeyEventArgs e)
        {

            //else if (e.KeyCode == Keys.F5)
            //{
            //    update();
            //}
        }

      

        private void add()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_AddHOCarcassInventory";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.Parameters.AddWithValue("@barcode", txtbarcode.Text);
            com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
            com.Parameters.AddWithValue("@parmreceivedby", Login.isglobalUserID);
            com.Parameters.AddWithValue("@exec", "ADD");

            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            try
            {
                com.ExecuteNonQuery();
                display();
                txtbarcode.Text = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void display()
        {
            //Database.display("SELECT * FROM view_CarcassInventory WHERE ShipmentNo='" + txtshipmentno.Text + "'", gridControl1, gridView1);

            Database.display("SELECT * FROM TempInventory WHERE ShipmentNo='" + txtshipmentno.Text + "' ORDER BY SequenceNumber DESC", gridControl1, gridView1);
        }

        private void display2()
        {
            //Database.display("SELECT * FROM view_CarcassInventory WHERE ShipmentNo='" + txtshipmentno.Text + "'", gridControl1, gridView1);

            Database.display("SELECT * FROM TempInventory WHERE ShipmentNo='" + txtshipmentno.Text + "' ORDER BY SequenceNumber DESC", gridControl1, gridView1);
        }

        

        

        void finalupdate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                //string query = "spu_postInventory";
                string query = "SP_POSTINVENTORY";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmbranch", Login.assignedBranch);
                com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                com.Parameters.AddWithValue("@parminvoicedate", txtinvoicedate.Text);
                com.Parameters.AddWithValue("@parmduedate", txtduedate.Text);
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

        int getLastTicketNumber()
        {
            int id = 0;
            id = Database.getLastID("TicketMaster", "TicketNumber");
            return id;
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                simpleButton2.PerformClick();
            }
        }

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void AddInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            //bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Force Close the form? \n Note: All Transactions will be cancelled", "Force Close");
            //if (ok)
            //{
            //    //Database.ExecuteQuery("DELETE FROM Inventory WHERE ShipmentNo='"+txtshipmentno.Text+"'");
            //    this.Dispose();
            //    this.Close();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM TempInventory WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'");
            gridView1.DeleteSelectedRows();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                txtfreightcost.Enabled = true;
            else
                txtfreightcost.Enabled = false;
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void txtprodcat_Click(object sender, EventArgs e)
        {
            //Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
        }

        private void txtproduct_Click(object sender, EventArgs e)
        {
            
        }

        String getProductCategoryCode()
        {
            string str;
            str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text.Trim() + "'", "ProductCategoryID");
            return str;
        }

       
        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtweight.Focus();
        }

        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtsearch.PerformClick();
        }

        private void txtsearch_Click(object sender, EventArgs e)
        {
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts(getProductCategoryCode());
            searchProd.ShowDialog(this);
            if (HOForms.SearchProducts.isdone == true)
            {
                txtproduct.Text = HOForms.SearchProducts.prodname;
                txtweight.Focus();
                HOForms.SearchProducts.isdone = false;
            }
        }

        String sequencePadding(string str)
        {
            string isnum = "";
            //  string str = IDGenerator.getSequenceNumber().ToString();
            if (str.Length == 1)
            {
                isnum = "000" + str;
            }
            else if (str.Length == 2)
            {
                isnum = "00" + str;
            }
            else if (str.Length == 3)
            {
                isnum = "0" + str;
            }
            return isnum;
        }

        String getProductCode(string prodcatcode)
        {
            string str = "";
            str = Database.getSingleQuery("Products", "BranchCode='" + Login.assignedBranch + "' and ProductCategoryCode='" + prodcatcode + "' and Description='" + txtproduct.Text + "'", "ProductCode");
            return str;
        }
    

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //displayweight();
            try
            {
                bool isBarcodeLong = false;
                isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
                decimal quantity;
                string strquantity;
                string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
                string prodcde = Classes.Product.getProductCode(txtproduct.Text, prodcatcode);//getProductCode(prodcatcode);
                //Classes.Product.getProductCode(txtproduct.Text, txtprodcat.Text);
                int ctr2=1;
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    ctr2++;
                }
              
                    if (checkBox2.Checked == true)
                    {
                        if (txtcomport.Text == "" || txtcomport.Text == null)
                        {
                            XtraMessageBox.Show("Please Select COM-PORT!");
                            txtcomport.Focus();
                        }
                        else
                        {
                            Random rand = new Random();
                            int ctr = rand.Next(0, 9);
                            txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });
                            quantity = Decimal.Parse(txtweight.Text);
                            strquantity = String.Format("{0:00.000}", quantity);
                        // txtbarcode.Text = txtshipmentno.Text + prodcde + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                            if(isBarcodeLong==true)
                            {
                                txtbarcode.Text = txtshipmentno.Text + prodcde + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                            }
                            else
                            {
                                txtbarcode.Text = prodcde + strquantity.Replace(".", "") + Classes.Utilities.sequencePadding(ctr2.ToString());
                            }
                            simpleButton1.Focus();
                        }
                    }
                    else
                    {
                        Random rand = new Random();
                        int ctr = rand.Next(0, 9);
                        quantity = Decimal.Parse(txtweight.Text);
                        strquantity = String.Format("{0:00.000}", quantity);
                    //txtbarcode.Text = txtshipmentno.Text + prodcde + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                        if (isBarcodeLong == true)
                        {
                            txtbarcode.Text = txtshipmentno.Text + prodcde + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                        }
                        else
                        {
                            txtbarcode.Text = prodcde + strquantity.Replace(".", "") + Classes.Utilities.sequencePadding(ctr2.ToString());
                    }
                        simpleButton1.Focus();
                    }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void displayweight()
        {
            //try
            //{
            //    Random rand = new Random();
            //    int ctr = rand.Next(0, 9);
            //    txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });
            //    txtbarcode.Text = getProductCategoryCode() + getProductCode() + txtweight.Text.Replace(".", "") + ctr.ToString();
            //    // button1.Focus();
            //    simpleButton1.Focus();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
            
        }

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //displayweight();
                simpleButton4.PerformClick();
            }
        }

        //private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        //{

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (txtbarcode.Text.Equals(""))
        //        {
        //            XtraMessageBox.Show("Please Input Valid Fields!");
        //        }
        //        else
        //        {
        //                simpleButton1.PerformClick();
        //        }

        //    }
        //    else if (e.KeyCode == Keys.F10)
        //    {
        //        simpleButton2.PerformClick();
        //    }
        //}

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtbarcode.Text = "";
            txtweight.Text = "";
            txtweight.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
            bprint.lblprodtype.Text = txtproduct.Text;
            bprint.lbltotalkilos.Text = txtweight.Text;
            bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            bprint.xrBarCode2.Text = txtbarcode.Text.Trim() ; //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void txtrefno_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                txtcost.Enabled = true;
            else
                txtcost.Enabled = false;
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtprodcat.Text == "")
            {
                txtprodcat.Focus();
            }
            else
            {
                //Database.displayComboBoxItems("SELECT Description FROM OrderDetails WHERE SUBSTRING(CAST(ProductCode as varchar(10)),1,2)='" + getProductCategoryCode() + "' AND ShipmentNo='" + txtshipmentno.Text + "' ORDER BY Description", "Description", txtproduct);
                Database.displayComboBoxItems("SELECT distinct Description FROM Products WHERE ProductCode in (Select OrderCode FROM PODETAILS WHERE ShipmentNo='"+txtshipmentno.Text+"') AND ProductCategoryCode='" + getProductCategoryCode() + "' ORDER BY Description", "Description", txtproduct);
            }
        }
    }
}