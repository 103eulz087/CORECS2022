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
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AddInventoryDevEx : DevExpress.XtraEditors.XtraForm
    {
        bool iserror = false;
        //DataTable table = null;
        //string productcategorycode;
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
        // private String weight;
        public string wieght2 = "";
        public static bool isdone = false;
        object productcode,categorycode,productname;
        public AddInventoryDevEx()
        {
            InitializeComponent();
        }

        private void AddInventoryDevEx_Load(object sender, EventArgs e)
        {
            getAvailablePort();
            //txtshipmentno.Text = ViewShipmentDashboard.shipmentno;
            //txtrefno.Text = IDGenerator.getReferenceNumber();

            txtrefno.Text = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
            loadgridview1();
            txtdestination.Text = "Commissary";
            txtinvoicedate.Text = DateTime.Today.ToShortDateString();
            txtduedate.Text = DateTime.Today.AddYears(1).ToShortDateString();
            populateProductCategory();
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
                //txtsearch.PerformClick();
            }
            return functionReturnValue;
        }
        public void AddDataMethod(String myString)
        {
            //txtweight.AppendText(myString);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool isproductexist = true;
            if (checkBox3.Checked == true && String.IsNullOrEmpty(txtcost.Text))
            {
                XtraMessageBox.Show("Please Input Cost Field!");
                return;
            }
            if (String.IsNullOrEmpty(txtpalletno.Text))
            {
                XtraMessageBox.Show("Please Input Pallet Number!");
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
                Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView1, "PalletNo");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalCost");
            }

        }

        void InsertData()
        {
            bool isoverride = false,isusedbarcode=false;
            if (checkBox3.Checked == true)
            {
                isoverride = true;
            }
            else
            {
                isoverride = false;
            }
            if (chckboxUseBarcode.Checked == true)
            {
                isusedbarcode = true;
            }
            else
            {
                isusedbarcode = false;
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
                com.Parameters.AddWithValue("@parmprodcode", productcode.ToString());
                com.Parameters.AddWithValue("@parmprodname", txtsrchprod.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtbarcode.Text);
                com.Parameters.AddWithValue("@parmtipweight", txtweight.Text);
                com.Parameters.AddWithValue("@parmqty", txtweight.Text);
                com.Parameters.AddWithValue("@parmcost", txtcost.Text);
                com.Parameters.AddWithValue("@parmisoverride", isoverride);
                com.Parameters.AddWithValue("@parmisusebarcode", isusedbarcode);

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
            catch (SqlException ex)
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
            //checkBatchUpload();
            //if (iserror == true)
            //{
            //    display();
            //    return;
            //}
            //else
            //{
            //    finalupdate();
            //    isdone = true;
            //}
            finalupdate();
            isdone = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int totalorders = Database.getCountData("SELECT COUNT(distinct(OrderCode)) as Counter FROM PODETAILS  WHERE ShipmentNo=" + txtshipmentno.Text + "", "Counter");
            int totalreceive = Database.getCountData("SELECT COUNT(distinct(Product)) as Counter FROM TempInventory  WHERE ShipmentNo=" + txtshipmentno.Text + "", "Counter");
            if (txtdestination.Text == "" || txtinvoicedate.Text == "" || txtinvoiceno.Text == "" || txtduedate.Text == "")
            {
                XtraMessageBox.Show("Please Input All Fields!");
                return;
            }
            bool confirmRcv = HelperFunction.ConfirmDialog("Are you sure you want to save this Inventory?","Confirm Inventory Entry");
            if (confirmRcv)
            {
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
            else
            {
                return;
            }
            
        }
        void loadgridview1()
        {
            display();
        }
        void populateProductCategory()
        {
            //Database.displayComboBoxItems("select Description FROM ProductCategory WHERE ProductCategoryID in (Select SUBSTRING(CAST(ProductCode as varchar(10)),1,2) as ProductCode  from OrderDetails WHERE ShipmentNo='" + txtshipmentno.Text + "')", "Description", txtprodcat);
            //Database.displayDevComboBoxItems("select Description FROM ProductCategory WHERE ProductCategoryID in (Select SUBSTRING(CAST(OrderCode as varchar(10)),1,2) as ProductCode  from PODETAILS WHERE ShipmentNo='" + txtshipmentno.Text + "' and OrderType='P')", "Description", txtprodcat);
            //Database.displayDevComboBoxItems("select ProductCategory FROM view_PODETAILS WHERE ShipmentNo='" + txtshipmentno.Text + "' and OrderType='P' ", "ProductCategory", txtprodcat);
            Database.displaySearchlookupEdit($"SELECT * FROM dbo.funcview_populateProducts('{Login.assignedBranch}') " +
                $"WHERE ProductCode in (Select distinct OrderCode FROM PODETAILS WHERE ShipmentNo='{txtshipmentno.Text}')", txtsrchprod,"Description","Description");
        }

        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtcomport.Items.AddRange(ports);
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
            Database.display("SELECT PalletNo,Product,Description,Barcode,Quantity,Cost,IsVat,(Quantity*Cost) as TotalCost " +
                "FROM dbo.TempInventory WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                "ORDER BY SequenceNumber DESC", gridControl1, gridView1);
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //Database.ExecuteQuery("DELETE FROM TempInventory WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'");
            Database.ExecuteQuery("DELETE FROM TempInventory WHERE Barcode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "'");
            gridView1.DeleteSelectedRows();
        }
        
        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtweight.Focus();
        }

        

        private void txtsearch_Click(object sender, EventArgs e)
        {
          
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts();
            searchProd.ShowDialog(this);
            if (HOForms.SearchProducts.isdone == true)
            {
                string productcategorydesc = Database.getSingleQuery("ProductCategory", "ProductCategoryID='" + HOForms.SearchProducts.prodcatcode + "'", "Description");
                //txtprodcat.Text = productcategorydesc;
                //txtproduct.Text = HOForms.SearchProducts.prodname;
                txtweight.Focus();
                HOForms.SearchProducts.isdone = false;
            }
        }
        
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                decimal quantity;
                string strquantity;

                if (checkBox2.Checked == true)
                {
                    if (txtcomport.Text == "" || txtcomport.Text == null)
                    {
                        XtraMessageBox.Show("Please Select COM-PORT!");
                        txtcomport.Focus();
                    }
                    else
                    {
                    
                        txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });
                        quantity = Decimal.Parse(txtweight.Text);
                        strquantity = String.Format("{0:00.000}", quantity);

                        string barcode = Database.getSingleResultSet($"SELECT dbo.func_GenerateBarcode" +
                $"('{Login.assignedBranch}',0,'{txtshipmentno.Text}','{productcode.ToString()}','{strquantity}','1') ");

                        txtbarcode.Text = barcode;

                       
                        simpleButton1.Focus();
                    }
                }
                else
                {
                   
                    quantity = Decimal.Parse(txtweight.Text);
                    strquantity = String.Format("{0:00.000}", quantity); 
                

                    string barcode = Database.getSingleResultSet($"SELECT dbo.func_GenerateBarcode" +
            $"('{Login.assignedBranch}',0,'{txtshipmentno.Text}','{productcode.ToString()}','{strquantity}','1') ");
                    txtbarcode.Text = barcode;

                    simpleButton1.Focus();
                }
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
                //displayweight();
                simpleButton4.PerformClick();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtbarcode.Text = "";
            txtweight.Text = "";
            txtweight.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.xrshipno.Text = txtshipmentno.Text;
            bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
            bprint.lblprodtype.Text = productname.ToString();
            bprint.lbltotalkilos.Text = txtweight.Text;
            bprint.xrpalletno.Text = txtpalletno.Text;
            bprint.lblxpirydate.Text = Convert.ToDateTime(txtduedate.Text).ToShortDateString();//DateTime.Now.AddYears(1).ToShortDateString();
            bprint.xrBarCode2.Text = txtbarcode.Text.Trim(); //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
           
            ReportPrintTool report = new ReportPrintTool(bprint);
            //report.ShowRibbonPreviewDialog();
            //report.PrintDialog();
            report.Print();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
                txtcost.Enabled = true;
            else
                txtcost.Enabled = false;
        }

        private void btnchecker_Click(object sender, EventArgs e)
        {
            //Orders.OrderCheckerDevEx oread = new Orders.OrderCheckerDevEx();

            ////Database.display("SELECT Description,Quantity FROM view_PODETAILS WHERE ShipmentNo='" + txtshipmentno.Text + "'", oread.gridControlDelivByComm, oread.gridViewDelivByComm);
            ////Database.display("SELECT Description,SUM(Quantity) as TotalKilos,COUNT(distinct Product) as TotalBox FROM TempInventory WHERE ShipmentNo='" + txtshipmentno.Text + "' GROUP BY Description", oread.gridControlActualRcvd, oread.gridViewActualRcvd);
            ////Database.display("SELECT Description,Quantity FROM view_PODETAILS WHERE OrderCode not in (Select Product FROM TempInventory WHERE ShipmentNo='" + txtshipmentno.Text + "') AND ShipmentNo='" + txtshipmentno.Text + "' ", oread.gridControlMyStsReq, oread.gridViewMyStsReq);
            //////MY STS REQUEST ITEMS
            ////Database.display("SELECT ProductCode,ProductName,Qty FROM TransferOrderDetails WHERE PONumber= ORDER BY ProductCode ASC", oread.gridControlMyStsReq, oread.gridViewMyStsReq);


            //////DELIVERED BY COMMISSARY
            ////Database.display("SELECT ProductNo,ProductName,QtyDelivered FROM DeliveryDetails WHERE PONumber='" + oread.gridViewMyReq.GetRowCellValue(oread.gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' ORDER BY ProductNo ASC", oread.gridControlDelivByComm, oread.gridViewDelivByComm);

            //////ACTUAL RECEIVED
            ////Database.display("SELECT ProductCode,ProductName,SUM(Qty) as TotalKilos FROM ReceivedOrderDetails WHERE PONumber='" + oread.gridViewMyReq.GetRowCellValue(oread.gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' GROUP BY ProductCode,ProductName  ORDER BY ProductCode ASC", oread.gridControlActualRcvd, oread.gridViewActualRcvd);

            //oread.ShowDialog(this);
        }

        private void txtsrchprod_EditValueChanged(object sender, EventArgs e)
        {
            productcode = null;
            categorycode = null;
            productname = null;
            productcode = SearchLookUpClass.getSingleValue(txtsrchprod, "ProductCode");
            categorycode = SearchLookUpClass.getSingleValue(txtsrchprod, "CategoryCode");
            productname = SearchLookUpClass.getSingleValue(txtsrchprod, "Description");
            txtpalletno.Text = "";
            txtweight.Focus();
        }
    }
}