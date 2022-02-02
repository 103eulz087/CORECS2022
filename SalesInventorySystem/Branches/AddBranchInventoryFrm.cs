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
using System.IO.Ports;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace SalesInventorySystem
{
    public partial class AddBranchInventoryFrm : DevExpress.XtraEditors.XtraForm
    {
        string decide = String.Empty;
        string specialprice = String.Empty, sellingprice = String.Empty;
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
        private String weight;
        public string wieght2 = String.Empty;
        public static bool isDone=false,ispending=false;
        
        public AddBranchInventoryFrm()
        {
            InitializeComponent();
            serialPort1.WriteTimeout = 500;
            serialPort1.ReadTimeout = 500;
            this.myDelegate = new AddDataDelegate(AddDataMethod);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F10)
            {
                simpleButton11.PerformClick();
              
            }
            else if (keyData == Keys.F6)
            {
                simpleButton9.PerformClick();
            }
            else if (keyData == Keys.F1)
            {
                simpleButton7.PerformClick();
            }
            return functionReturnValue;
        }

        public void AddDataMethod(String myString)
        {
            txtweight.AppendText(myString);
        }

        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtcomport.Items.AddRange(ports);
        }

        void displayComboBoxItems()
        {
            Database.displayComboBoxItems("SELECT * FROM Products WHERE BranchCode='888'", "Description", txtproduct);
            Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
        }

        void addItems()
        {
            this.Cursor = Cursors.WaitCursor;
            string productcategorycode;
            string primalproductcode;
           
            bool isBarcodeLong = false;
            isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
            if (scanbarcode.Checked == true)
            {
                productcategorycode = txtbarcode.Text.Substring(5, 2); //33333 10015 12345
                primalproductcode = txtbarcode.Text.Substring(5, 5);
            }
            else
            {
                productcategorycode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
                primalproductcode = Classes.Product.getProductCode(txtproduct.Text, productcategorycode);

            }

            if (String.IsNullOrEmpty(txtbarcode.Text))
            {
                XtraMessageBox.Show("Please Input Valid Fields!");
            }
            else
            {
                bool isbarcodeexist = Database.checkifExist("SELECT TOP 1 DeliveryNo FROM DeliveryDetails WHERE DeliveryNo='" + txtdevno.Text + "'");

                bool isExists = false;
                string qtydelivered = "";
                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    if (gridView2.GetRowCellValue(i, "ProductNo").ToString() == primalproductcode)
                    {
                        isExists = true;
                        qtydelivered = gridView2.GetRowCellValue(i, "QtyDelivered").ToString();
                    }
                }
                if (isExists)
                {

                    if (scanbarcode.Checked == true)
                    {
                        if (!isbarcodeexist)
                        {
                            XtraMessageBox.Show("Barcode Not Exist in your Order Details..");
                            return;
                        }
                        else
                        {
                            add();
                            display();
                            txtweight.Text = "";
                            txtproduct.Text = "";
                            txtbarcode.Text = "";
                            txtproduct.Focus();
                        }
                    }
                    else
                    {
                        add();
                        display();
                        txtweight.Text = "";
                        txtproduct.Text = "";
                        txtbarcode.Text = "";
                        txtproduct.Focus();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Product Code does not Exist in your purchase order!");
                }
            }
            this.Cursor = Cursors.Default;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void add()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                bool isBarcodeLong = false;
                isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
                bool isscan = false;
                string pcode = "";
                if(scanbarcode.Checked==true)
                {
                    if(txtbarcode.Text.Length >= 10 && txtbarcode.Text.Length <= 16) //10 start para naay allowance 99999100051234560001
                    {
                        pcode = txtbarcode.Text.Substring(0, 5); //10015 10123 0001
                    }
                    else if (txtbarcode.Text.Length >= 19 && txtbarcode.Text.Length <= 21)
                    {
                        pcode = txtbarcode.Text.Substring(5, 5); //11111 10015 10123 0001
                    }
                  
                    isscan = true;
                   
                }
                else
                {
                    isscan = false;
                    pcode = getProductCode();
                }
             
                string query = "sp_AddBranchInventory";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmponumber", txtpono.Text);
                com.Parameters.AddWithValue("@parmproductcode", pcode);
                com.Parameters.AddWithValue("@parmqty", txtweight.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtbarcode.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmreceivedby", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmsellingprice", sellingprice);
                com.Parameters.AddWithValue("@parmisscan", isscan);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
              
              
            }catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
           
        }

        private void AddBranchInventoryFrm_Load(object sender, EventArgs e)
        {
            getAvailablePort();

            //display();
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                if (txtbarcode.Text.Equals(""))
                {
                    XtraMessageBox.Show("Please Input Valid Fields!");
                }
                else
                {
                    bool ok = Database.checkifExist("SELECT Barcode FROM ReceivedOrderDetails WHERE Barcode='" + txtbarcode.Text + "' AND PONumber='" + txtpono.Text + "'");
                    if (ok)
                    {
                        XtraMessageBox.Show("Product Already Exist!");
                    }
                    else
                    {
                        btnadd.PerformClick();
                        
                    }
                }
                if(scanbarcode.Checked==true)
                {
                    txtbarcode.Focus();
                }
            }
            else if (e.KeyCode == Keys.F10)
            {
                simpleButton11.PerformClick();
            }
        }

        private void display()
        {
            //Database.display("SELECT * FROM view_BranchOrderForReceiving WHERE DeliveryNo='" + txtdevno.Text + "' and Status='FOR DELIVERY'", gridControl2, gridView2);
            Database.display("SELECT  ProductNo,ProductName,QtyDelivered,Cost,SellingPrice FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "'", gridControl2, gridView2);
            Database.display("SELECT SeqNo,ProductCode,ProductName,Barcode,Qty,SellingPrice FROM ReceivedOrderDetails WHERE PONumber='" + txtpono.Text + "'", gridControl1, gridView1);
        }

        void ConfirmBranchReceivedOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                
                string query = "sp_ConfirmBranchRecievedOrder";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);

                com.Parameters.AddWithValue("@parmpono", txtpono.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtbarcode.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch); 
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void txtbarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void returnOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_CancelReceivedDelivery";
            try
            {
                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@parmpono", txtpono.Text);
                com.Parameters.AddWithValue("@parmseqno", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SeqNo"));
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Successfully Deleted");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void AddBranchInventoryFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //bool fexist = Database.checkifExist("SELECT * FROM DeliverySummary WHERE Status='DELIVERED' AND DeliveryNo='" + txtdevno.Text + "' and PONumber='" + txtpono.Text + "' ");
            //if (fexist)
            //{
            //    this.Close();
            //}
            //else
            //{
            //    bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Force Close the form? \n Note: All Transactions will be cancelled", "Force Close");
            //    if (ok)
            //    {
            //        Database.ExecuteQuery("DELETE FROM ReceiveOrderSummary WHERE DeliveryNo='" + txtdevno.Text + "' AND PONumber='" + txtpono.Text + "'");
            //        Database.ExecuteQuery("DELETE FROM ReceivedOrderDetails WHERE DeliveryNo='" + txtdevno.Text + "'  AND PONumber='" + txtpono.Text + "'");
            //        Database.ExecuteQuery("DELETE FROM InventoryReceivedFIFO WHERE DeliveryNo='" + txtdevno.Text + "' AND PONumber='" + txtpono.Text + "'");
            //        //Database.ExecuteQuery("DELETE FROM Inventory WHERE Branch='" + Login.assignedBranch + "' AND ShipmentNo='" + txtpono.Text + "' ", "Successfully Closed");
            //        this.Dispose();
            //        this.Close();
            //    }
            //    else
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }

        private void txtcomport_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = txtcomport.Text.Trim();
            serialPort1.Open();
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
        }

        void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                weight = serialPort1.ReadExisting();
                if (weight == String.Empty || txtcomport.Text == "")
                {
                    XtraMessageBox.Show("NODATARECEVD");
                    return;
                }
                else
                {
                    string tempweight = weight.Substring(0, 6).Trim();
                    //tempweight = weight.Replace("?", "0");
                    if (tempweight.Length == 5)
                    {
                        wieght2 = "0" + tempweight;
                    }
                    else
                    {
                        wieght2 = tempweight;
                    }
                    //eventFlag = true;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() +" CHECK YOUR COMPORT OR RESTART THE WEIGHING SCALE");
            }
        }

        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtweight.Focus();
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
            str = Database.getSingleQuery("Products", "Description='" + txtproduct.Text.Trim() + "' AND ProductCategoryCode='" + getProductCategoryCode() + "'", "ProductCode");
            return str;
        }

        void displayweight()
        {
            try
            {
                int ctr2 = 1;
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    ctr2++;
                }
                decimal quantity;
                string strquantity;
                Random rand = new Random();
                int ctr = rand.Next(0, 9);
                txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });

                quantity = Decimal.Parse(txtweight.Text);
                strquantity = String.Format("{0:00.000}", quantity);
                ////////////////////////////
                //Random rand = new Random();
                //int ctr = rand.Next(0, 9);
                //txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });
                txtbarcode.Text = "77777"+getProductCode() + strquantity.Replace(".", "") + Classes.Utilities.sequencePadding(ctr2.ToString());
                // button1.Focus();
       
                btnadd.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                displayweight();
            }
        }

        private void txtprodcat_Click(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
        }

        private void txtproduct_Click(object sender, EventArgs e)
        {   
           
        }

      
        private void btnclear_Click(object sender, EventArgs e)
        {
            
        }

        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                simpleButton7.PerformClick();
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT * FROM Products WHERE BranchCode='888' AND ProductCategoryCode='" + getProductCategoryCode() + "' ORDER BY Description", "Description", txtproduct);
        }

        private void txtsearch_Click(object sender, EventArgs e)
        {
            
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            bool check = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsStock"));
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            displayweight();
        }

        private void btnsaveasdraft_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("The System found out that there are no items to be pending!..");
                this.Close();
            }
            else
            {
                ispending = true;
                XtraMessageBox.Show("Transaction Save as Pending!");
                this.Close();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            addItems();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            txtbarcode.Text = "";
            txtweight.Text = "";
            txtweight.Focus();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            //Database.ExecuteQuery("DELETE FROM ");
            // action = "cancelline";
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Delete this Item?", "Delete Item");
            if (confirm)
            {
                returnOrder();
                display();
            }
            else
            {
                return;
            }
          
        }


        void checker()
        {
            Orders.OrderCheckerDevEx oread = new Orders.OrderCheckerDevEx();

            Database.display("SELECT ProductName,ActualQty FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "'", oread.gridControl1, oread.gridView1); 
            //Database.display("SELECT ProductName,QtyDelivered FROM DeliveryDetails WHERE PONumber='" + txtponum.Text + "'", oread.gridControl2, oread.gridView2);
            Database.display("SELECT ProductName,SUM(Qty) as TotalKilos,COUNT(distinct Barcode) as TotalBox FROM ReceivedOrderDetails WHERE PONumber='" + txtpono.Text + "' GROUP BY ProductName", oread.gridControl2, oread.gridView2);
            //Database.display("SELECT ProductName,SUM(QtyDelivered) as TotalKilos,COUNT(distinct BarcodeNo) as TotalBox FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "' GROUP BY ProductName", oread.gridControl2, oread.gridView2);
            Database.display("SELECT ProductName,ActualQty FROM DeliveryDetails WHERE ProductNo not in (Select ProductCode FROM ReceivedOrderDetails WHERE PONumber='" + txtpono.Text + "') AND PONumber='" + txtpono.Text + "' ", oread.gridControl3, oread.gridView3);
            //Database.display("SELECT ProductName,Qty FROM TransferOrderDetails WHERE ProductCode not in (Select ProductNo FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "') AND PONumber='" + txtpono.Text + "' ", oread.gridControl3, oread.gridView3);

            oread.ShowDialog(this);
        }

        private void btnchecker_Click(object sender, EventArgs e)
        {
            checker();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {

            int ctr = gridView2.RowCount;
            int ctr2 = gridView1.RowCount;

            if (ctr != ctr2)
            {
                XtraMessageBox.Show("The System found out that you there are some items left to be entered in the system!. \n Please check your purchase order");
                return;
            }
            ConfirmBranchReceivedOrder();
            XtraMessageBox.Show("Transaction Saved!");
            isDone = true;
            this.Close();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
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
    }
}