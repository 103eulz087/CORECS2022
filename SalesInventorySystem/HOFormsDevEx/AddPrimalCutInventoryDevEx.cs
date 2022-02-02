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
using System.IO.Ports;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using SalesInventorySystem.HOForms;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AddPrimalCutInventoryDevEx : DevExpress.XtraEditors.XtraForm
    {
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;

        private String weight;
        public string wieght2 = "";
        public static string prodprint, weightprint, barcodeprint;
        double existingqty = 0.0;
        bool isexistingqty = false;
        object categorycode, productcode;
        public AddPrimalCutInventoryDevEx()
        {
            InitializeComponent();
            serialPort1.WriteTimeout = 500;
            serialPort1.ReadTimeout = 500;
            this.myDelegate = new AddDataDelegate(AddDataMethod);
        }

        public void AddDataMethod(String myString)
        {
            txtweight.AppendText(myString);
        }

        private void AddPrimalCutInventory_Load(object sender, EventArgs e)
        {
            try
            {
                txtbatchcode.Text = HOForms.SetBatchCodeFrm.batchcode;
                //string _getProdcode = Database.getSingleQuery("Inventory", "BatchCode='" + txtbatchcode.Text + "' and Branch='888'", "Product"); //assumen only one product code at a time
                //string _getproductCategorycode = _getProdcode.Substring(0, 2);
                //string _getProdCatName = Classes.Product.getProductCategoryName(_getproductCategorycode);

                radchanged();

                Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
                getAvailablePort();

                isprimalcuts.Checked = true;
                //Classes.Product.displayProductCategoryComboBoxItems(txtprodcat);
                //txtavailableqty.Text = getAvailableQty().ToString();
                display();
                txtprodcat.Focus();

                Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView1, "Description");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Quantity");
                Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Available");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void radchanged()
        {
            if (isprimalcuts.Checked == true)
            {
                Database.displaySearchlookupEdit("SELECT a.ProductCategoryCode as CategoryCode" +
                  ",b.Description as CategoryName,a.ProductCode,a.Description FROM Products as a INNER JOIN " +
                  "ProductCategory as b ON a.ProductCategoryCode=b.ProductCategoryID WHERE a.BranchCode='" + Login.assignedBranch + "' " +
                  "AND a.isPrimalCut=1", txtsrchprod, "Description", "Description");
            }
            else
            {
                Database.displaySearchlookupEdit("SELECT a.ProductCategoryCode as CategoryCode" +
                  ",b.Description as CategoryName,a.ProductCode,a.Description FROM Products as a INNER JOIN " +
                  "ProductCategory as b ON a.ProductCategoryCode=b.ProductCategoryID WHERE a.BranchCode='" + Login.assignedBranch + "' " +
                  "AND a.isPrimalCut=0", txtsrchprod, "Description", "Description");
            }
        }

        void display()
        {
            Database.display("SELECT * FROM view_ProcessToPrimal " +
                "WHERE BatchCode='" + txtbatchcode.Text + "' " +
                "and Available > 0 " +
                "and isDone = 0", gridControl1, gridView1);
        }

        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtports.Items.AddRange(ports);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F6)
            {
                buttonClear.PerformClick();
            }
            else if (keyData == Keys.F10)
            {
                //button2.PerformClick();
            }
            else if (keyData == Keys.F8)
            {
                //buttonPrintBarcode.PerformClick();
            }

            return functionReturnValue;
        }



        void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                weight = serialPort1.ReadExisting();
                if (weight == String.Empty)
                {
                    XtraMessageBox.Show("NODATARECEVD");
                    return;
                }
                else
                {
                    string tempweight = weight.Substring(0, 6).Trim();
                    if (tempweight.Length == 5)
                    {
                        wieght2 = "0" + tempweight;
                    }
                    else
                    {
                        wieght2 = tempweight;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ActiveControl = txtweight;
            txtweight.Focus();
        }

        String sequencePadding(string str)
        {
            string isnum = "";
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

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonGetWeight.PerformClick();
            }
        }

        private void buttonPrintBarcode_Click(object sender, EventArgs e)
        {

        }



        private void btnaddinventory_Click(object sender, EventArgs e)
        {

        }

        void add()
        {
            try
            {

                bool ifexist = Database.checkifExist("SELECT top 1 ShipmentNo FROM TempCosting WHERE ShipmentNo='" + txtshipmentno.Text + "'");
                string cost;
                if (String.IsNullOrEmpty(txtshipmentno.Text))
                {
                    XtraMessageBox.Show("Please Indicate what Shipment Number you process!");
                    return;
                }
                if (ifexist)
                {
                    cost = Database.getSingleQuery("TempCosting", "ItemCode='" + productcode + "' and ShipmentNo='" + txtshipmentno.Text + "'", "CostPerKg"); //get from excel anna uploading
                }
                else
                {
                    //cost = Database.getSingleQuery("Products", "ProductCode='" + productcode + "' and BranchCode='888'", "LandingCost"); //get to product table landing cost
                    XtraMessageBox.Show("Primal Cut Costing Not Yet Uploaded..");
                    return;
                }

                string finalqty = txtweight.Text;
                string seqno = Convert.ToInt32(txtskuno.Text.Substring(13, 4)).ToString();
                Database.ExecuteQuery("INSERT INTO TempInventoryPrimal " +
                    "VALUES ('" + seqno + "'" +
                    ",'" + Login.assignedBranch + "'" +
                    ",'" + txtshipmentno.Text + "'" +
                    ",'" + txtbatchcode.Text + "'" +
                    ",'" + productcode + "'" +
                    ",'" + txtsrchprod.Text + "'" +
                    ",'" + txtskuno.Text + "'" +
                    ",'" + finalqty + "'" +
                    ",'" + cost + "'" +
                    ",'" + finalqty + "'" +
                    ",0" + //iswarehouse
                    ",0" +
                    ",'" + Login.isglobalUserID + "','" + DateTime.Now.ToString() + "')");
                display();
                txtweight.Text = "";
                txtskuno.Text = "";
                txtweight.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtskuno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (autoprintbarcode.Checked == true)
                {
                    buttonPrintBarcode.PerformClick();
                    buttonAdd.PerformClick();
                }
                else
                {
                    buttonPrintBarcode.PerformClick();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount < 1)
            {
                XtraMessageBox.Show("No Products to Upload!");
                return;
            }
            else if (String.IsNullOrEmpty(txtbatchcode.Text))
            {
                XtraMessageBox.Show("Please Input BatchCode");
                return;
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Upload Batch Items");
                if (ok)
                {
                    //uploadNewBatchProcess(); gitawag na cya sa primaltransferdevex
                    computeShortages(); //gi replasan og SP pro wla pa gi delete ang method na insertShortageOverageData
                    XtraMessageBox.Show("Successfully Uploaded");
                    this.Dispose();
                }
                else
                {
                    return;
                }
            }
        }

        void uploadNewBatchProcess()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_BatchPrimalCutProcess";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbatchcode", txtbatchcode.Text);
                com.Parameters.AddWithValue("@parmprocessby", Login.Fullname);
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

        void computeShortages()
        {

            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_BatchShortageOverage";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbatchcode", txtbatchcode.Text);
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

        void insertShortageOverageData()
        {
            try
            {
                bool isexist = Database.checkifExist("SELECT BatchCode FROM BatchShortageOverageList WHERE BatchCode='" + txtbatchcode.Text + "'");
                if (!isexist)
                {
                    if (isexistingqty)//(isexistingqty)//overage cya
                    {
                        Database.ExecuteQuery("INSERT INTO BatchShortageOverageList VALUES('10000','" + txtbatchcode.Text + "','0','" + existingqty + "')", "Success!");
                    }
                    else
                    {
                        double getTotal = 0.0;
                        getTotal = Database.getTotalSummation2("Inventory", "BatchCode='" + txtbatchcode.Text + "' and Product = '10005'", "Available");
                        Database.ExecuteQuery("INSERT INTO BatchShortageOverageList VALUES('10000','" + txtbatchcode.Text + "','" + getTotal + "','0')", "Success!");
                    }
                }
                else
                {
                    if (isexistingqty)//(isexistingqty)//overage cya
                    {
                        Database.ExecuteQuery("UPDATE BatchShortageOverageList SET Overage='" + existingqty + "' WHERE BatchCode='" + txtbatchcode.Text + "'", "Success!");
                    }
                    else
                    {
                        double getTotal = 0.0;
                        getTotal = Database.getTotalSummation2("Inventory", "BatchCode='" + txtbatchcode.Text + "' and Product = '10005'", "Available");
                        Database.ExecuteQuery("UPDATE BatchShortageOverageList SET Shortage='" + getTotal + "' WHERE BatchCode='" + txtbatchcode.Text + "'", "Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            this.Dispose();
            this.Close();
        }



        private void AddPrimalCutInventory_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }



        private void txtports_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = txtports.Text.Trim();
            serialPort1.Open();
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
            txtprodcat.Focus();
        }

        private void AddPrimalCutInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }


        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    button5.PerformClick();
        }

        private void txtweight_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void txtprodcat_Click(object sender, EventArgs e)
        {

        }
        String getProductCategoryCode()
        {
            string str;
            str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text.Trim() + "'", "ProductCategoryID");
            return str;
        }

        private void txtbatchcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool isexist = Database.checkifExist("Select BatchCode FROM TempInventory WHERE BatchCode='" + txtbatchcode.Text + "'");
                if (isexist)
                {
                    display();
                }
                else
                {
                    XtraMessageBox.Show("This BatchCode is not Exist");
                    return;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        void executeTransfer()
        {
            try
            {
                string id = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber");// IDGenerator.getTransferedNumber();
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString()
                    string batchcode = gridView1.GetRowCellValue(i, "BatchCode").ToString();//dataGridView1.Rows[0].Cells["BatchCode"].Value.ToString();
                    string productcode = gridView1.GetRowCellValue(i, "Product").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string description = gridView1.GetRowCellValue(i, "Description").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString();
                    string barcode = gridView1.GetRowCellValue(i, "Barcode").ToString();//dataGridView1.Rows[0].Cells["Barcode"].Value.ToString();
                    string quantity = gridView1.GetRowCellValue(i, "Quantity").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    string cost = gridView1.GetRowCellValue(i, "Cost").ToString();//dataGridView1.Rows[0].Cells["Cost"].Value.ToString();
                    string available = gridView1.GetRowCellValue(i, "Available").ToString();//dataGridView1.Rows[0].Cells["Available"].Value.ToString();
                    Database.ExecuteQuery("insert into InventoryBigBlue values ('888','',' ','" + batchcode + "','" + DateTime.Now.ToShortDateString() + "','" + productcode + "','" + description + "','" + barcode + "','" + quantity + "','" + quantity + "','" + cost + "','" + available + "',0,1,0,1,' ','" + DateTime.Now.ToShortDateString() + "',0,0,0);");
                    Database.ExecuteQuery("insert into InventoryTransferred values ('888', '" + productcode + "', '" + description + "', '" + DateTime.Now.ToShortDateString() + "', '" + barcode + "', '" + quantity + "', '" + DateTime.Now.ToShortDateString() + "', 1, '" + id + "', 'auto', '" + Login.Fullname + "', 'Commissary', 'BigBlue', ' ', ' ')");
                }
                for (int k = 0; k <= gridView1.RowCount - 1; k++)
                {
                    string prod = gridView1.GetRowCellValue(k, "Product").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string desc = gridView1.GetRowCellValue(k, "Description").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString();
                    string qty = gridView1.GetRowCellValue(k, "Quantity").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    string cst = gridView1.GetRowCellValue(k, "Cost").ToString();//dataGridView1.Rows[0].Cells["Cost"].Value.ToString();

                    string bar = gridView1.GetRowCellValue(k, "Barcode").ToString();
                    Database.ExecuteQuery("UPDATE TempInventory set ReferenceCode='AutoTransferred',Available=0,isStock=0 WHERE BatchCode='" + txtbatchcode.Text + "' and Barcode='" + bar + "' ");
                    //Database.ExecuteQuery("insert into Inventory values ('888','',' ','" + txtbatchcode.Text + "','" + DateTime.Now.ToShortDateString() + "','" + prod + "','" + desc + "','" + bar + "','" + qty + "','" + qty + "','" + cst + "',0,0,0,0,1,' ','" + DateTime.Now.ToShortDateString() + "',0,0,0);");
                }
                spTransfer();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            //Database.ExecuteQuery("INSERT INTO Inventory");
            //Database.ExecuteQuery("select Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Description,Barcode,TipWeight,Quantity,Cost,Available,0,isStock,IsVat,IsWarehouse,ReferenceCode,LastMovementDate,isProcess,0,0 FROM TempInventory WHERE BatchCode='" + txtbatchcode.Text + "' and Barcode not in (Select Barcode FROM Inventory WHERE BatchCode='" + txtbatchcode.Text + "') ");
        }

        void spTransfer()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_TransferDirectly";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbatchcode", txtbatchcode.Text);
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

        void BigBlueTemplate()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption2");

            //DevExReportTemplate.CustomerRequest xct = new DevExReportTemplate.CustomerRequest();
            DevExReportTemplate.TransferInventory xct = new DevExReportTemplate.TransferInventory();
            xct.Landscape = false;
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);


            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtransfertype.Text = "Transfer to BigBlue";

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }
        private void txtshipmentno_Click(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT * FROM ShipmentOrder", "ShipmentNo", txtshipmentno);
        }


        void addItem()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (txtskuno.Text == "")
                {
                    XtraMessageBox.Show("Field must not Empty");
                    txtskuno.Text = "";
                    txtskuno.Focus();
                }

                else
                {
                    add();
                    txtskuno.Text = "";
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            this.Cursor = Cursors.Default;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addItem();
        }

        void cancelLine()
        {
            //int cord = dataGridView1.CurrentCellAddress.Y;
            //getcurrentgridqty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
            Database.ExecuteQuery("DELETE FROM TempInventoryPrimal WHERE BatchCode='" + txtbatchcode.Text + "' AND Barcode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "'");
            display();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            cancelLine();
        }
        void clearFields()
        {
            txtweight.Text = "";
            txtskuno.Text = "";
            txtweight.Focus();
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }
        void saveAndTransfer()
        {
            HOFormsDevEx.PrimalTransferDevEx asd = new HOFormsDevEx.PrimalTransferDevEx();
            asd.txtbatchcode.Text = txtbatchcode.Text;
            asd.ShowDialog(this);
            if (HOFormsDevEx.PrimalTransferDevEx.isdone == true)
            {
                asd.Dispose();
                HOFormsDevEx.PrimalTransferDevEx.isdone = false;
                display();
            }

            //TransferInventory transinv = new TransferInventory();
            //Database.display("Select SequenceNumber,Product,Description,Barcode,Quantity,Available,DateReceived FROM TempInventory WHERE BatchCode='" + txtbatchcode.Text + "' and isStock=1", transinv.gridControl1, transinv.gridView1);
            //transinv.Show();
        }
        private void buttonSaveAndTransfer_Click(object sender, EventArgs e)
        {
            saveAndTransfer();
            button2.PerformClick();
        }

        void getWeight()
        {
            try
            {
                decimal quantity;
                string strquantity;

                if (checkBox1.Checked == true)
                {
                    if (txtports.Text == "" || txtports.Text == null)
                    {
                        XtraMessageBox.Show("Please Select COM-PORT!");
                        txtports.Focus();
                    }
                    else
                    {

                        txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });
                        quantity = Decimal.Parse(txtweight.Text);
                        strquantity = String.Format("{0:00.000}", quantity);

                        string barcode = Database.getSingleResultSet($"SELECT dbo.func_GenerateBarcode" +
                  $"('{Login.assignedBranch}','{txtbatchcode.Text}','{txtshipmentno.Text}','{productcode}','{strquantity}','2') ");

                        txtskuno.Text = barcode;
                        txtskuno.Focus();
                    }
                }
                else
                {

                    quantity = Decimal.Parse(txtweight.Text);
                    strquantity = String.Format("{0:00.000}", quantity);
                    string barcode = Database.getSingleResultSet($"SELECT dbo.func_GenerateBarcode" +
                  $"('{Login.assignedBranch}','{txtbatchcode.Text}','{txtshipmentno.Text}','{productcode}','{strquantity}','2') ");
                    txtskuno.Text = barcode;
                    txtskuno.Focus();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void printBarcode()
        {
            try
            {
                if (txtskuno.Text == "")
                {
                    XtraMessageBox.Show("No SKU No to print");
                }
                else
                {
                    prodprint = txtsrchprod.Text;
                    weightprint = txtweight.Text;
                    barcodeprint = txtskuno.Text.Trim();
                    Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
                    bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
                    bprint.lblprodtype.Text = prodprint;
                    bprint.lbltotalkilos.Text = weightprint;
                    bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
                    bprint.xrBarCode2.Text = barcodeprint;//productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
                    ReportPrintTool report = new ReportPrintTool(bprint);
                    report.Print();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void searchitems()
        {
            string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
            HOForms.SearchProducts saerprod = new HOForms.SearchProducts();
            saerprod.ShowDialog(this);
            Database.displayLocalGrid("SELECT ProductCode as Product,Description FROM Products WHERE ProductCategoryCode='" + prodcatcode + "' and BranchCode='888' ORDER BY Description ASC", saerprod.dataGridView1);
            if (SearchProducts.isdone == true)
            {
                txtsrchprod.Text = SearchProducts.prodname;
                txtweight.Focus();
            }
        }

        private void buttonPrintBarcode_Click_1(object sender, EventArgs e)
        {
            printBarcode();
        }

        private void buttonGetWeight_Click(object sender, EventArgs e)
        {
            getWeight();
        }

        private void txtproduct_SeectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtsrchprod_EditValueChanged(object sender, EventArgs e)
        {
            categorycode = null;
            productcode = null;
            categorycode = SearchLookUpClass.getSingleValue(txtsrchprod, "CategoryCode");
            productcode = SearchLookUpClass.getSingleValue(txtsrchprod, "ProductCode");
            txtprodcat.Text = SearchLookUpClass.getSingleValue(txtsrchprod, "CategoryName").ToString();
            this.ActiveControl = txtweight;
            txtweight.Focus();
        }

        private void buttonSearchItems_Click(object sender, EventArgs e)
        {
            searchitems();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}