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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.IO.Ports;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.Orders
{
    public partial class AddBranchOrderSTS : DevExpress.XtraEditors.XtraForm
    {
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
        object pcode,catcode;
        private String weight;
        public string wieght2 = "";
        public static bool isdone = false, ispending = false;
        string productcategorycode = "";
        string primalproductcode = "";
        string globaltxtbarcodescanning = "", globalproductcode = "";
        bool isFifo = Database.checkifExist("SELECT isFifo FROM InventorySettings WHERE isFifo=1");
        bool isusedSearch = false;

       
       
        public AddBranchOrderSTS()
        {         
            this.myDelegate = new AddDataDelegate(AddDataMethod);
            InitializeComponent();
        }
        public void AddDataMethod(String myString)
        {
            txtweight.AppendText(myString);
        }
        private void AddBranchOrderSTS_Load(object sender, EventArgs e)
        {
            getAvailablePort();

            if (isFifo == true)
            {
                barcodescanning.Checked = false;
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = true;
            }
            else
            {
                barcodescanning.Checked = true;
                panel1.Visible = true;

            }

            Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);

            bool fExst = Database.checkifExist("SELECT PONumber FROM DeliveryDetails WHERE PONumber='" + ViewBranchOrderSTS.ponumber + "'");
            string getID = Database.getSingleData("DeliveryDetails", "PONumber", ViewBranchOrderSTS.ponumber, "DeliveryNo");
            if (fExst)
            {
                txtdevno.Text = getID;
            }
            else
            {
                txtdevno.Text = IDGenerator.getIDNumberSP("sp_GetDeliveryNumber", "DeliveryNumber");
            }

            txtcomport.Focus();

            //txtbrcode.Text = ViewBranchOrder.branchno;
            //txtponum.Text = ViewBranchOrder.ponumber;
            //txtrefno.Text = IDGenerator.getReferenceNumber();
            //txteffectivedate.Text = ViewBranchOrder.effectivedate;

            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["Category"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);

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
            else if (keyData == Keys.F8)
            {
                simpleButton10.PerformClick();
            }
            if (keyData == Keys.F1)
            {
                simpleButton3.PerformClick();
            }
            if (keyData == Keys.F5)
            {
                btnchecker.PerformClick();
            }
            return functionReturnValue;
        }

        private void txtsku_KeyDown(object sender, KeyEventArgs e)
        {
            bool isExist = Database.checkifExist("SELECT BarcodeNo FROM DeliveryDetails WHERE BarcodeNo='" + txtsku.Text + "'");
            if (e.KeyCode == Keys.Enter)
            {
                btnadd.PerformClick();
            }
            else if (e.KeyCode == Keys.F10)
            {
                simpleButton2.PerformClick();
            }
        }

        /*****************************************NEW*****************************/
        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtcomport.Items.AddRange(ports);
        }

        void displayComboBoxItems()
        {
            Database.displayComboBoxItems("SELECT * FROM Products WHERE BranchCode='888' ORDER BY Description", "Description", txtproduct);
            Database.displayComboBoxItems("SELECT Description FROM ProductCategory", "Description", txtprodcat);
        }

        /*****************************************NEW*****************************/
        private void displayForDelivery()
        {
            gridControl2.BeginUpdate();
            //     Database.display("SELECT QtyDelivered,BarcodeNo,ProductNo,ProductName,FORMAT(Cost,'N','en-us')AS Cost,SequenceNo FROM DeliveryDetails WHERE DeliveryNo='" + txtdevno.Text + "'", gridControl2, gridView2);
            Database.display("SELECT SeqNo,ProductNo,ProductName,BarcodeNo,QtyDelivered FROM DeliveryDetails WHERE DeliveryNo='" + txtdevno.Text + "' AND Status='PENDING'", gridControl2, gridView2);
            gridView2.Columns["SeqNo"].Visible = false;
            gridView2.Columns["ProductNo"].Summary.Clear();
            gridView2.Columns["ProductNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ProductNo", "{0:n2}");
            gridControl2.EndUpdate();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                XtraMessageBox.Show("Cant Save no Order to be Processed!");
                return;
            }
          

            bool confirm = HelperFunction.ConfirmDialog("Are you sure all order has been Processed?", "Save Transaction");
            if (!confirm)
            {
                return;
            }
            else
            {
                ConfirmBranchOrder();
                isdone = true;
                XtraMessageBox.Show("Transaction Successfully Saved!");
                this.Close();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl2.BeginUpdate();
            try
            {
                if (txtsku.Text == "")
                {
                    XtraMessageBox.Show("Please Input Valid Fields");
                    return;
                }
                else
                {
                    string qty = txtweight.Text;
                    if (barcodescanning.Checked == false)
                    {
                        productcategorycode = Classes.Product.getProductCategoryCode(txtprodcat.Text);//Database.getSingleQuery("Products", "ProductCode='" + primalproductcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "ProductCategoryCode");
                        primalproductcode = Classes.Product.getProductCode(txtproduct.Text, productcategorycode);//Classes.BarcodeSettings.getBarcodePrimalProductCode(txtsku.Text);
                    }
                    else
                    {
                        //old barcode scanning picking per barcode per plastic
                        //primalproductcode = Database.getSingleQuery("Inventory", "Barcode='" + globaltxtbarcodescanning + "' and isWarehouse=1 and Available > 0 and Branch='888' and IsStock=1", "Product");
                        //productcategorycode = primalproductcode.Substring(0, 2);
                        primalproductcode = globalproductcode;
                        productcategorycode = primalproductcode.Substring(0, 2);
                    }
                    bool requestedProductExist = false;
                    for (int j = 0; j <= gridView1.RowCount - 1; j++)
                    {
                        if (gridView1.GetRowCellValue(j, "ProductCode").ToString() == primalproductcode)
                            requestedProductExist = true;
                        if (requestedProductExist)
                            break;
                    }
                    bool inventoryExist = Database.checkifExist("SELECT Product FROM Inventory WHERE Product='" + primalproductcode + "' AND Branch='" + Login.assignedBranch + "' AND IsStock='1'");
                 
                    if (!requestedProductExist)
                    {
                        XtraMessageBox.Show("You cant add this Product because it is not available in Purchase Order List!");
                        txtsku.Text = "";
                    }
                    else if (!inventoryExist)
                    {
                        XtraMessageBox.Show("No Product Inventory");
                        txtsku.Text = "";
                    }
                    //kung imong gi encode na quantity is greater than sa total quantity sa imong Inventory sa commisary
                    else if (Convert.ToDouble(txtweight.Text) > Database.getTotalSummation2("Inventory", "Product = '" + primalproductcode + "' AND Branch='" + Login.assignedBranch + "' AND IsStock='1' and Available > 0 ", "Available")) //Database.getTotalSummation("Inventory", "Product", txtsku.Text.Substring(1, 6), "Quantity"))
                    {
                        string mark = Database.getTotalSummation2("Inventory", "Product = '" + primalproductcode + "' AND Branch='" + Login.assignedBranch + "' AND IsStock='1'  and Available > 0 ", "Available").ToString();
                        XtraMessageBox.Show("Insuficient Stocks for this Product.. Your Available Quantity is " + mark);
                    }
                    else
                    {
                        add();
                        displayForDelivery();
                        //if(isFifo==false)
                        //{
                        //    searchLookUpEdit1.Text = "";
                        //}
                        if (barcodescanning.Checked == true)
                        {
                            txtbarcodescanning.Focus();
                        }
                        else
                        {
                            txtproduct.Focus();
                        }
                        txtweight.Text = "";
                        txtproduct.Text = "";
                        txtsku.Text = "";
                    }
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + "123");
            }
            gridControl2.EndUpdate();
        }

        private void txtsku_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void add()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_AddBranchOrder";
            try
            {
                bool isbarcodscanning = false;
                string sourceseqnum = "";
                if (barcodescanning.Checked == true)
                {
                    isbarcodscanning = true;
                    ///////////////////////////////////////////////////////////////////////////////////OLD per plastic pler barcode
                    //primalproductcode = Database.getSingleQuery("Inventory","Barcode='"+txtbarcodescanning.Text+"' and isWarehouse=1 and Available > 0 and Branch='888' and IsStock=1","Product");
                    //productcategorycode = primalproductcode.Substring(0,2);
                    //sourceseqnum = Database.getSingleQuery("Inventory", "Barcode='" + txtbarcodescanning.Text + "' and isWarehouse=1 and Available > 0 and Branch='888' and IsStock=1", "SequenceNumber");
                    //////////////////////////////////////////////////////////////////////////////////OLD per plastic pler barcode
                    primalproductcode = globalproductcode;
                    productcategorycode = primalproductcode.Substring(0, 2);
                }
                else
                {
                    sourceseqnum = txtseqno.Text;
                }
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmpono", txtponum.Text);
                com.Parameters.AddWithValue("@parmprodcatcode", productcategorycode);
                com.Parameters.AddWithValue("@parmprodcode", primalproductcode);
                com.Parameters.AddWithValue("@parmqty", txtweight.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtsku.Text);

                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text); //initiating branhc
                com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                //com.Parameters.AddWithValue("@parmeffectivitydate", txteffectivedate.Text);
                com.Parameters.AddWithValue("@parmsourceseqno", sourceseqnum);
                com.Parameters.AddWithValue("@parmbarcodescanning", isbarcodscanning);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + "XYZ");
            }
            con.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            returnOrder();
            displayForDelivery();
        }

        void ConfirmBranchOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_ConfirmBranchOrderSTS";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmeffectivitydate", txteffectivedate.Text);
                com.Parameters.AddWithValue("@parmpono", txtponum.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtsku.Text);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            con.Close();
        }
        void returnOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_CancelDelivery";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmpono", txtponum.Text);
                com.Parameters.AddWithValue("@parmprodno", gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductNo").ToString());
                com.Parameters.AddWithValue("@parmqty", gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "QtyDelivered").ToString());
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmdevseqno", Convert.ToInt32(gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "SeqNo").ToString()));
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Successfully Deleted");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            con.Close();
        }
        String getProductCategoryCode()
        {
            string str;
            if (isusedSearch == true)
            {
                str = productcategorycode;
            }
            else
            {
                str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text.Trim() + "'", "ProductCategoryID");
            }
            return str;
        }

        private void txtcomport_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = txtcomport.Text.Trim();
            serialPort1.Open();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
            txtprodcat.Focus();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
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
                    string tempweight = weight.Substring(0, 6).Trim(); //01.234
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
                XtraMessageBox.Show(ex.Message.ToString() + "CHECK YOUR COMPORT OR RESTART THE WEIGHING SCALE");
            }
        }

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                displayweight();
             
            }
        }

        private void txtproduct_Click(object sender, EventArgs e)
        {
            isusedSearch = false;
            if (txtprodcat.Text == "")
            {
                txtprodcat.Focus();
            }
            else
            {
                Database.displayComboBoxItems("SELECT Description FROM Products WHERE BranchCode='888' AND ProductCategoryCode='" + getProductCategoryCode() + "' ORDER BY Description", "Description", txtproduct);
            }
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtproduct.Text = "";
            txtproduct.Focus();
            Database.displayComboBoxItems("SELECT * FROM Products WHERE BranchCode='888'  AND ProductCategoryCode='" + getProductCategoryCode() + "' ORDER BY Description", "Description", txtproduct);

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            displayweight();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtsku.Text = "";
            txtweight.Text = "";
            txtweight.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
            bprint.lblprodtype.Text = txtproduct.Text;
            bprint.lbltotalkilos.Text = txtweight.Text;
            bprint.xrBarCode2.Text = txtsku.Text.Trim(); //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                simpleButton3.PerformClick();
        }

        private void txtsearch_Click(object sender, EventArgs e)
        {
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts();
            searchProd.ShowDialog(this);
            Database.displayLocalGrid("SELECT * FROM view_CommissaryInventory ORDER BY Description ASC", searchProd.dataGridView1);
            if (HOForms.SearchProducts.isdone == true)
            {
                productcategorycode = HOForms.SearchProducts.prodcode.Substring(0, 2);
                string productcategorydesc = Database.getSingleQuery("ProductCategory", "ProductCategoryID='" + productcategorycode + "'", "Description");
                txtprodcat.Text = productcategorydesc;
                txtproduct.Text = HOForms.SearchProducts.prodname;
                txtweight.Focus();
                HOForms.SearchProducts.isdone = false;
                isusedSearch = true;
                searchProd.Dispose();
            }

        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            bool isFifo = Database.checkifExist("SELECT isFifo FROM InventorySettings WHERE isFifo=1");
            if (isFifo == false)
            {
                try
                {
                    GridView view = searchLookUpEdit1.Properties.View;
                    int rowHandle = view.FocusedRowHandle;
                    object value = view.GetRowCellValue(rowHandle, "SeqNo");
                    object valueAvailable = view.GetRowCellValue(rowHandle, "Available");
                    txtseqno.Text = value.ToString();
                    txtweight.Text = valueAvailable.ToString();
                    txtweight.Focus();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message.ToString() + "---");
                }
            }
        }

        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isFifo = Database.checkifExist("SELECT isFifo FROM InventorySettings WHERE isFifo=1");
            if (isFifo == false)
            {
                if (barcodescanning.Checked == false)
                {
                    Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", searchLookUpEditBranch, "BranchCode", "BranchCode");
                    searchLookUpEditBranch.Text = Login.assignedBranch;
                    Database.displaySearchlookupEdit("SELECT SequenceNumber as SeqNo,DateReceived,Barcode,Quantity,Available FROM Inventory WHERE Branch='" + searchLookUpEditBranch.Text + "' and isStock=1 and Available > 0  and Product='" + getProductCode() + "'", searchLookUpEdit1, "Barcode", "Barcode");
                    searchLookUpEdit1.Focus();
                }
                else
                {
                    txtbarcodescanning.Focus();
                }
            }
            else
            {
                if (barcodescanning.Checked == false)
                {
                    //OLD
                    //Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", searchLookUpEditBranch, "BranchCode", "BranchCode");
                    //searchLookUpEditBranch.Text = Login.assignedBranch;
                    //Database.displaySearchlookupEdit("SELECT SequenceNumber as SeqNo,DateReceived,Barcode,Quantity,Available FROM Inventory WHERE Branch='" + searchLookUpEditBranch.Text + "' and isStock=1 and Available > 0 and isWarehouse = 1 and Product='" + getProductCode() + "'", searchLookUpEdit1, "Barcode", "Barcode");
                    //searchLookUpEdit1.Focus();
                    txtweight.Focus();
                }
                else
                {
                    txtbarcodescanning.Focus();
                }
            }
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl2, e.Location);
        }

        private void printBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string qtydel = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "QtyDelivered").ToString();
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
            bprint.lblprodtype.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductName").ToString();
            bprint.lbltotalkilos.Text = qtydel;
            bprint.xrBarCode2.Text = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BarcodeNo").ToString();
            bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void barcodescanning_CheckedChanged(object sender, EventArgs e)
        {
            if (barcodescanning.Checked == true)
            {
                isFifo = false;
                panel2.Visible = true;
                panel4.Visible = false;
                panel3.Visible = false;
                panel1.Visible = false;
                txtbarcodescanning.Focus();
            }
            else
            {
                panel2.Visible = false;
                panel3.Visible = true;
                panel1.Visible = true;
            }
        }

        private void txtbarcodescanning_KeyDown(object sender, KeyEventArgs e)
        {
            ///OLD
            if (e.KeyCode == Keys.Enter)
            {
                bool checkifexists = Database.checkifExist($"SELECT TOP(1) Barcode " +
                    $"FROM Inventory WHERE Barcode='{txtbarcodescanning.Text}' " +
                    $"and Available > 0 and Branch='{Login.assignedBranch}'");
                if(!checkifexists)
                {
                    XtraMessageBox.Show("Barcode Not Exist in Inventory!...");
                    return;
                }
                //bool isBarcodeLong = false;
                //isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
                string pcode = "",  qty = "", barcode = "";// qty1 = "", qty2 = "";desc = "",
                //bool isExist = Database.checkifExist("SELECT * FROM Inventory WHERE Barcode='" + txtbarcodescanning.Text + "' AND isWarehouse=1 AND isStock=1 AND Available > 0 AND Branch='888' ");
                //globaltxtbarcodescanning = "";
                //globaltxtbarcodescanning = txtbarcodescanning.Text;
                //if (isExist)
                //{
                //    string valueAvailable = Database.getSingleQuery("Inventory", "Barcode='" + txtbarcodescanning.Text + "' and isWarehouse=1 and Available > 0 and Branch='888' and IsStock=1", "Available");
                //    txtweight.Text = valueAvailable;
                //    txtweight.Focus();
                //    displayweight();
                //    simpleButton1.PerformClick();
                //    txtbarcodescanning.Text = "";
                //    txtbarcodescanning.Focus();
                //}
                //else
                //{
                //    XtraMessageBox.Show("Barcode Not Exist in Inventory!");
                //    return;
                //}
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                globaltxtbarcodescanning = "";
                globaltxtbarcodescanning = txtbarcodescanning.Text;

                var rows = Database.getMultipleQuery($"SELECT * FROM dbo.funcTable_DecryptBarcode('{txtbarcodescanning.Text}')", "ProductCode,Quantity");
                string ProductCode, Quantity;
                ProductCode = rows["ProductCode"].ToString();
                Quantity = rows["Quantity"].ToString();
                pcode = ProductCode;
                qty = Quantity;
                barcode = txtbarcodescanning.Text;
                //if (isBarcodeLong == false)
                //{
                //    if (txtbarcodescanning.Text.Length == 14) //tens 10015 10123 0001
                //    {
                //        pcode = txtbarcodescanning.Text.Substring(0, 5);
                //        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                //        qty1 = txtbarcodescanning.Text.Substring(5, 2); //1001512345
                //        qty2 = txtbarcodescanning.Text.Substring(7, 3);
                //        qty = qty1 + "." + qty2;
                //        barcode = txtbarcodescanning.Text;
                //    }
                //    else if (txtbarcodescanning.Text.Length == 15) //hundred
                //    {
                //        pcode = txtbarcodescanning.Text.Substring(0, 5);
                //        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                //        qty1 = txtbarcodescanning.Text.Substring(5, 3); //10015100345
                //        qty2 = txtbarcodescanning.Text.Substring(8, 3);
                //        qty = qty1 + "." + qty2;
                //        barcode = txtbarcodescanning.Text;
                //    }
                //    else if (txtbarcodescanning.Text.Length == 16) //thousand
                //    {
                //        pcode = txtbarcodescanning.Text.Substring(0, 5);
                //        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                //        qty1 = txtbarcodescanning.Text.Substring(5, 4); //10015100345
                //        qty2 = txtbarcodescanning.Text.Substring(9, 3);
                //        qty = qty1 + "." + qty2;
                //        barcode = txtbarcodescanning.Text;
                //    }
                //    else
                //    {
                //        XtraMessageBox.Show("Invalid Barcode Type!.. Please use manual input!..");
                //        return;
                //    }
                //}
                //else //if barcode is longtype
                //{
                //    if (txtbarcodescanning.Text.Length == 19) //tens 11111 10015 10123 0001 --10.123 kilos
                //    {
                //        pcode = txtbarcodescanning.Text.Substring(5, 5);
                //        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                //        qty1 = txtbarcodescanning.Text.Substring(10, 2); //1001512345
                //        qty2 = txtbarcodescanning.Text.Substring(12, 3);
                //        qty = qty1 + "." + qty2;
                //        barcode = txtbarcodescanning.Text;
                //    }
                //    else if (txtbarcodescanning.Text.Length == 20) //hundred 11111 10015 100123 0001 --100.123 kilos
                //    {
                //        pcode = txtbarcodescanning.Text.Substring(5, 5);
                //        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                //        qty1 = txtbarcodescanning.Text.Substring(10, 3); //10015100345
                //        qty2 = txtbarcodescanning.Text.Substring(13, 3);
                //        qty = qty1 + "." + qty2;
                //        barcode = txtbarcodescanning.Text;
                //    }
                //    else if (txtbarcodescanning.Text.Length == 21) //thousand  11111 10015 1000123 0001 --1000.123 kilos
                //    {
                //        pcode = txtbarcodescanning.Text.Substring(5, 5);
                //        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                //        qty1 = txtbarcodescanning.Text.Substring(10, 4); //10015100345
                //        qty2 = txtbarcodescanning.Text.Substring(14, 3);
                //        qty = qty1 + "." + qty2;
                //        barcode = txtbarcodescanning.Text;
                //    }
                //    else
                //    {
                //        XtraMessageBox.Show("Invalid Barcode Type!.. Please use manual input!..");
                //        return;
                //    }
                //}
                globalproductcode = pcode;
                txtweight.Text = qty;
                txtweight.Focus();
                displayweight();
                btnadd.PerformClick();
                txtbarcodescanning.Text = "";
                txtbarcodescanning.Focus();
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to print all barcodes?", "Print All Barcode");
            if (confirm)
            {
                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    //string qtydel = gridView2.GetRowCellValue(i, "QtyDelivered").ToString();
                    Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
                    bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
                    bprint.lblprodtype.Text = gridView2.GetRowCellValue(i, "ProductName").ToString();
                    bprint.lbltotalkilos.Text = gridView2.GetRowCellValue(i, "QtyDelivered").ToString();
                    bprint.xrBarCode2.Text = gridView2.GetRowCellValue(i, "BarcodeNo").ToString();
                    bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
                    ReportPrintTool report = new ReportPrintTool(bprint);
                    //report.ShowRibbonPreviewDialog();
                    report.Print();
                }
            }
            else
            {
                return;
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            Orders.OrderCheckerDevEx oread = new Orders.OrderCheckerDevEx();

            Database.display("SELECT ProductName,Qty FROM TransferOrderDetails WHERE PONumber='" + txtponum.Text + "'", oread.gridControl1, oread.gridView1);
            Database.display("SELECT ProductName,QtyDelivered FROM DeliveryDetails WHERE PONumber='" + txtponum.Text + "'", oread.gridControl2, oread.gridView2);
            Database.display("SELECT ProductName,Qty FROM TransferOrderDetails WHERE ProductCode not in (Select ProductNo FROM DeliveryDetails WHERE PONumber='" + txtponum.Text + "') AND PONumber='" + txtponum.Text + "' ", oread.gridControl3, oread.gridView3);

            oread.ShowDialog(this);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount <= 0)
            {
                XtraMessageBox.Show("The System found out that there are no items to be pending!..");
                this.Close();
            }
            else
            {
                ispending = true;
                XtraMessageBox.Show("Transaction Save as Pending!");
                this.Dispose();
            }
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
                decimal quantity;
                string strquantity;
                string productcode = "";
                if (txtcomport.Text == "")
                {
                    XtraMessageBox.Show("Please Select COM-PORT");
                }
                else
                {
                    for (int i = 0; i <= gridView2.RowCount - 1; i++)
                    {
                        ctr2++;
                    }
                    Random rand = new Random();
                    int ctr = rand.Next(0, 9);
                    txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });

                    quantity = Decimal.Parse(txtweight.Text);
                    strquantity = String.Format("{0:00.000}", quantity);

                    if (barcodescanning.Checked == true)
                    {
                        productcode = globalproductcode;//Database.getSingleQuery("Inventory", "Barcode='" + globaltxtbarcodescanning + "' and isWarehouse=1 and Available > 0 and Branch='888' and IsStock=1", "Product");
                        txtsku.Text = txtbarcodescanning.Text;
                    }
                    else
                    {
                        productcode = pcode.ToString();//getProductCode();
                        txtsku.Text = Database.getSingleQuery("Products", "BranchCode='" + Login.assignedBranch + "' AND ProductCode='" + pcode + "' ", "Barcode");
                        //if (isBarcodeLong == true)
                        //{
                        //    txtsku.Text = "33333" + productcode + strquantity.Replace(".", "") + Classes.Utilities.sequencePadding(ctr2.ToString());
                        //}
                        //else
                        //{
                        //    txtsku.Text = productcode + strquantity.Replace(".", "") + Classes.Utilities.sequencePadding(ctr2.ToString());
                        //}
                        //if (txtsku.Text.Length < 19)
                        //{
                        //    XtraMessageBox.Show("Please Enter Again.. Barcode Length must Greater than or Equal to 19 digits!...");
                        //    return;
                        //}
                        //else
                        //{
                        //    simpleButton1.Focus();
                        //}
                        txtsku.Focus();
                    }



                }

            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + "ABC");
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            gridControl2.BeginUpdate();
            try
            {
                if (txtsku.Text == "")
                {
                    XtraMessageBox.Show("Please Input Valid Fields");
                    return;
                }
                else
                {
                    string qty = txtweight.Text;
                    if (barcodescanning.Checked == false)
                    {
                        //productcategorycode = Classes.Product.getProductCategoryCode(txtprodcat.Text);//Database.getSingleQuery("Products", "ProductCode='" + primalproductcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "ProductCategoryCode");
                        //primalproductcode = Classes.Product.getProductCode(txtproduct.Text, productcategorycode);//Classes.BarcodeSettings.getBarcodePrimalProductCode(txtsku.Text);
                        productcategorycode = catcode.ToString();//Database.getSingleQuery("Products","ProductCode='"+pcode.ToString()+"' AND BranchCode='"+Login.assignedBranch+"' ","ProductCategoryCode");//Database.getSingleQuery("Products", "ProductCode='" + primalproductcode + "' AND BranchCode='" + Login.assignedBranch + "' ", "ProductCategoryCode");
                        primalproductcode = pcode.ToString();//Classes.BarcodeSettings.getBarcodePrimalProductCode(txtsku.Text);

                    }
                    else //nagamit og barcode
                    {
                        //old barcode scanning picking per barcode per plastic
                        //primalproductcode = Database.getSingleQuery("Inventory", "Barcode='" + globaltxtbarcodescanning + "' and isWarehouse=1 and Available > 0 and Branch='888' and IsStock=1", "Product");
                        //productcategorycode = primalproductcode.Substring(0, 2);
                        primalproductcode = globalproductcode;
                        productcategorycode = Database.getSingleQuery("Products",$"ProductCode='{primalproductcode}' AND BranchCode='{Login.assignedBranch}'","ProductCategoryCode");//primalproductcode.Substring(0, 2);

                    }
                    bool requestedProductExist = false;
                    for (int j = 0; j <= gridView1.RowCount - 1; j++)
                    {
                        if (gridView1.GetRowCellValue(j, "ProductCode").ToString() == primalproductcode)
                            requestedProductExist = true;
                        if (requestedProductExist)
                            break;
                    }
                    bool inventoryExist = Database.checkifExist("SELECT TOP(1) Product FROM Inventory WHERE Product='" + primalproductcode + "' AND Branch='" + Login.assignedBranch + "' AND IsStock='1'");

                    if (!requestedProductExist && txtbrcode.Text!="999")
                    {
                        XtraMessageBox.Show("You cant add this Product because it is not available in Purchase Order List!");
                        txtsku.Text = "";
                    }
                    else if (!inventoryExist)
                    {
                        XtraMessageBox.Show("No Product Inventory");
                        txtsku.Text = "";
                    }
                    //kung imong gi encode na quantity is greater than sa total quantity sa imong Inventory sa commisary
                    else if (Convert.ToDouble(txtweight.Text) > Database.getTotalSummation2("Inventory", "Product = '" + primalproductcode + "' AND Branch='" + Login.assignedBranch + "' AND IsStock='1' and Available > 0 ", "Available")) //Database.getTotalSummation("Inventory", "Product", txtsku.Text.Substring(1, 6), "Quantity"))
                    {
                        string mark = Database.getTotalSummation2("Inventory", "Product = '" + primalproductcode + "' AND Branch='" + Login.assignedBranch + "' AND IsStock='1'  and Available > 0 ", "Available").ToString();
                        XtraMessageBox.Show("Insuficient Stocks for this Product.. Your Available Quantity is " + mark);
                    }
                    else
                    {
                        add();
                        displayForDelivery();
                        //if(isFifo==false)
                        //{
                        //    searchLookUpEdit1.Text = "";
                        //}
                        if (barcodescanning.Checked == true)
                        {
                            txtbarcodescanning.Focus();
                        }
                        else
                        {
                            //txtproduct.Focus();
                            txtsearchlookupproduct.Focus();
                        }
                        txtweight.Text = "";
                        txtproduct.Text = "";
                        txtsearchlookupproduct.Text = "";
                        txtsku.Text = "";
                    }
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + "123");
            }
            gridControl2.EndUpdate();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            txtsku.Text = "";
            txtweight.Text = "";
            txtweight.Focus();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
            bprint.lblprodtype.Text = txtproduct.Text;
            bprint.lbltotalkilos.Text = txtweight.Text;
            bprint.xrBarCode2.Text = txtsku.Text.Trim(); //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            returnOrder();
            displayForDelivery();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                XtraMessageBox.Show("Cant Save no Order to be Processed!");
                return;
            }


            bool confirm = HelperFunction.ConfirmDialog("Are you sure all order has been Processed?", "Save Transaction");
            if (!confirm)
            {
                return;
            }
            else
            {
                ConfirmBranchOrder();
                isdone = true;
                XtraMessageBox.Show("Transaction Successfully Saved!");
                this.Close();
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            displayweight();
        }

        private void btnsaveasdraft_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount <= 0)
            {
                XtraMessageBox.Show("The System found out that there are no items to be pending!..");
                this.Close();
            }
            else
            {
                ispending = true;
                XtraMessageBox.Show("Transaction Save as Pending!");
                this.Dispose();
            }
        }

        private void txtsearchlookupproduct_EditValueChanged(object sender, EventArgs e)
        {
            catcode = null;
            pcode = null;
            catcode = SearchLookUpClass.getSingleValue(txtsearchlookupproduct, "CategoryCode");
            pcode =SearchLookUpClass.getSingleValue(txtsearchlookupproduct, "ProductCode");
            txtweight.Focus();
        }

        private void btnchecker_Click(object sender, EventArgs e)
        {
            Orders.OrderCheckerDevEx oread = new Orders.OrderCheckerDevEx();

            Database.display("SELECT ProductName,Qty FROM TransferOrderDetails WHERE PONumber='" + txtponum.Text + "'", oread.gridControl1, oread.gridView1); 

            Database.display("SELECT ProductName,SUM(QtyDelivered) as TotalKilos,COUNT(distinct BarcodeNo) as TotalBox FROM DeliveryDetails WHERE PONumber='" + txtponum.Text + "' GROUP BY ProductName", oread.gridControl2, oread.gridView2);
           
            Database.display("SELECT ProductName,Qty FROM TransferOrderDetails WHERE ProductCode not in (Select ProductNo FROM DeliveryDetails WHERE PONumber='" + txtponum.Text + "') AND PONumber='" + txtponum.Text + "' ", oread.gridControl3, oread.gridView3);

            oread.ShowDialog(this);
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to print all barcodes?", "Print All Barcode");
            if (confirm)
            {
                for (int i = 0; i <= gridView2.RowCount - 1; i++)
                {
                    //string qtydel = gridView2.GetRowCellValue(i, "QtyDelivered").ToString();
                    Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
                    bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
                    bprint.lblprodtype.Text = gridView2.GetRowCellValue(i, "ProductName").ToString();
                    bprint.lbltotalkilos.Text = gridView2.GetRowCellValue(i, "QtyDelivered").ToString();
                    bprint.xrBarCode2.Text = gridView2.GetRowCellValue(i, "BarcodeNo").ToString();
                    bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
                    ReportPrintTool report = new ReportPrintTool(bprint);
                    //report.ShowRibbonPreviewDialog();
                    report.Print();
                }
            }
            else
            {
                return;
            }
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts();
            searchProd.ShowDialog(this);
            Database.displayLocalGrid("SELECT * FROM view_CommissaryInventory ORDER BY Description ASC", searchProd.dataGridView1);
            if (HOForms.SearchProducts.isdone == true)
            {
                productcategorycode = HOForms.SearchProducts.prodcatcode;
                string productcategorydesc = Database.getSingleQuery("ProductCategory", "ProductCategoryID='" + productcategorycode + "'", "Description");
                txtprodcat.Text = productcategorydesc;
                txtproduct.Text = HOForms.SearchProducts.prodname;
                txtweight.Focus();
                HOForms.SearchProducts.isdone = false;
                isusedSearch = true;
                searchProd.Dispose();
            }
        }

    }
}