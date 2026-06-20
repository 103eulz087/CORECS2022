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
using DevExpress.XtraReports.UI;
using SalesInventorySystem.POS;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventoryOut : DevExpress.XtraEditors.XtraForm
    {
        // Product data object - more type-safe than loose object variables
        private StockOutItem _currentItem;
        public InventoryOut()
        {
            InitializeComponent();
            _currentItem = new StockOutItem();
        }

        /// <summary>
        /// Inserts the current stock out item into the database
        /// </summary>
        void OutInventory()
        {
            // Ensure we have a valid item to insert
            if (_currentItem == null || _currentItem.TotalCost <= 0)
            {
                XtraMessageBox.Show("Invalid item data. Please ensure all fields are populated.", "Error");
                return;
            }

            // Use the service layer to insert the item
            string errorMessage;
            if (StockOutService.InsertStockOutItem(_currentItem, out errorMessage))
            {
                XtraMessageBox.Show("Successfully Added", "Success");
            }
            else
            {
                XtraMessageBox.Show("Failed to add item: " + errorMessage, "Error");
            }
        }

        void doFIFO()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "sp_FiFoMappingStockOut";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmorderno", txtbatchid.Text);
                com.Parameters.AddWithValue("@parmtransdate", txtdatein.Text);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmprodcode", "");
                com.Parameters.AddWithValue("@parmqty", txtqty.Value);
                com.Parameters.AddWithValue("@parmoption", "2");
                com.Parameters.AddWithValue("@parmsellingprice", "0");
                com.Parameters.AddWithValue("@parmcost", "0");
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
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

        void doStockOutSummary()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string sp = "sp_InsertStockOutSummary";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbatchid", txtbatchid.Text);
                com.Parameters.AddWithValue("@parmbrcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmcategory", txtcategory.Text);
                com.Parameters.AddWithValue("@parmremarks", txtremarks.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
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

        void display()
        {
            Database.display("SELECT * FROM StockOutDetails WHERE ID='" + txtbatchid.Text + "' and isDone=0 " +
                "AND EncodeBy='" + Login.isglobalUserID + "' " +
                "AND BranchCode='" + txtbrcode.Text + "' " +
                "AND DateEncode='" + DateTime.Now.ToShortDateString() + "'", gridControl1, gridView2);
        }

        private void POSInventoryIN_Load(object sender, EventArgs e)
        {
            loadData();
        }

        void loadData()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches ORDER BY BranchCode", txtbrcode, "BranchName", "BranchName");
           
            //Database.displaySearchlookupEdit("Select a.ProductCode,a.Description,a.Barcode,b.Description as Category,c.Available,c.Cost,b.isVat " +
            //    "FROM Products as a " +
            //    "INNER JOIN ProductCategory as b " +
            //    "ON a.ProductCategoryCode=b.ProductCategoryID " +
            //    "INNER JOIN Inventory as c " +
            //    "ON a.ProductCode=c.Product AND a.BranchCode=c.Branch " +
            //    "WHERE a.BranchCode='" + Login.assignedBranch + "' and c.Available > 0 and c.isWarehouse=1", txtproduct, "Description", "Description");
            Database.displayComboBoxItems("SELECT Description FROM dbo.StockOutCategory", "Description", txtcategory);
        }

        //void uploadInventory()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    try
        //    {
        //        string query = "sp_UploadInventoryIN";
        //        SqlCommand com = new SqlCommand(query, con);
        //        com.Parameters.AddWithValue("@parmid", txtbatchid.Text);
        //        com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
        //        com.CommandType = CommandType.StoredProcedure;
        //        com.CommandText = query;
        //        com.ExecuteNonQuery();
        //    }
        //    catch (SqlException ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //if(gridView2.RowCount == 0 || String.IsNullOrEmpty(txtbatchid.Text))
            //{
            //    XtraMessageBox.Show("No Items to Upload!...");
            //    return;
            //}
            //else
            //{
            //    uploadInventory();
            //    XtraMessageBox.Show("Success");
            //    this.Dispose();
            //}
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            //int id = IDGenerator.getIDNumber("StockOutDetails","isDone=1", "ID", 1);
            txtbatchid.Text = IDGenerator.getIDNumberSP("sp_GetBadOrderNumber", "orderno"); //IDGenerator.getPONumber();
            btnnew.Enabled = false;
            enableFields();
        }

       

        private void btncancel_Click(object sender, EventArgs e)
        {
            //Database.ExecuteQuery("DELETE FROM StockOutDetails WHERE ID='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString() + "' " +
            //    "AND BranchCode='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString() + "' " +
            //    "AND ProductCode='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductCode").ToString() + "' " +
            //    "AND Quantity='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Quantity").ToString() + "' " +
            //    "AND DateEncode='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "DateEncode").ToString() + "' " +
            //    "AND EncodeBy='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EncodeBy").ToString() + "'", "Successfully Deleted");
            //display();
            this.Dispose();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                XtraMessageBox.Show("No Items to Print!...");
                return;
            }
            else
            {
                //var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                //string companyname = row["Heading"].ToString();
                //string imagewidth = row["ImageWidth"].ToString();
                //string imageheight = row["ImageHeight"].ToString();
                //string caption1 = row["Caption1"].ToString();
                //string caption2 = row["Caption2"].ToString();

                DeliveryReceiptFrm devrcptfrm = new DeliveryReceiptFrm();
                devrcptfrm.xrdevno.Text = txtbatchid.Text;
                devrcptfrm.xrdate.Text = txtdatein.Text; //Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateProcessed").ToString()).ToShortDateString();dt.ToShortDateString();

                devrcptfrm.Landscape = false;
                //Classes.Utilities.GetImageDevEx(devrcptfrm.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
                //devrcptfrm.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                //devrcptfrm.xrcompanyname.Text = companyname;
                //devrcptfrm.xrcaption1.Text = caption1;
                //devrcptfrm.xrcaption2.Text = caption2;
                devrcptfrm.xrdeliveredby.Text = Login.Fullname;
                devrcptfrm.PaperKind = System.Drawing.Printing.PaperKind.A4;

                gridView2.Columns["ID"].Visible = false;
                gridView2.Columns["BranchCode"].Visible = false;
                gridView2.Columns["Barcode"].Visible = false;

                gridView2.Columns["isVat"].Visible = false;
                gridView2.Columns["isDone"].Visible = false;
                gridView2.Columns["DateEncode"].Visible = false;
                gridView2.Columns["EncodeBy"].Visible = false;

                devrcptfrm.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
                devrcptfrm.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(devrcptfrm);
                report.ShowRibbonPreviewDialog();
            }
        }

        void clear()
        {
            txtproduct.Text = "";
            txtqty.Text = "";
            txtavailable.Text = "";
           
            
           
        }

        void disableFields()
        {
            txtdatein.Enabled = false;
            txtqty.Enabled = false;
            txtproduct.Enabled = false;
            txtbrcode.Enabled = false;
        }
        void enableFields()
        {
            txtdatein.Enabled = true;
            txtqty.Enabled = true;
            txtproduct.Enabled = true;
            txtbrcode.Enabled = true;
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1,e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pcode = _currentItem?.ProductCode;
            if (string.IsNullOrWhiteSpace(pcode))
            {
                XtraMessageBox.Show("No product selected to delete");
                return;
            }

            string errorMessage;
            if (StockOutService.DeleteStockOutItem(txtbatchid.Text, txtbrcode.Text, pcode, out errorMessage))
            {
                XtraMessageBox.Show("Successfully Deleted", "Success");
                display();
            }
            else
            {
                XtraMessageBox.Show("Failed to delete: " + errorMessage, "Error");
            }
        }

        private void txtbrcode_EditValueChanged(object sender, EventArgs e)
        {
            object branchCodeValue = SearchLookUpClass.getSingleValue(txtbrcode, "BranchCode");
            if (branchCodeValue != null)
            {
                _currentItem.BranchCode = branchCodeValue.ToString();
                Database.displaySearchlookupEdit(
                    $"SELECT * FROM dbo.funcview_populateProductsForStockOut('{branchCodeValue.ToString()}') ", 
                    txtproduct, "Description", "Description");
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            //POSInventoryINRecovertBatchID posinvin = new POSInventoryINRecovertBatchID();
            //posinvin.ShowDialog(this);
            //if(POSInventoryINRecovertBatchID.isdone==true)
            //{
            //    txtbatchid.Text=POSInventoryINRecovertBatchID.ID;
            //    posinvin.Dispose();
            //}
            bool check = Database.checkifExist("SELECT TOP(1) ID FROM StockOutDetails WHERE isDone=0");
            if (check)//true
            {
                XtraMessageBox.Show("No Items to Recover");
                return;
            }
            else
            {
                POS.POSUnUploadInv posinv = new POSUnUploadInv();
                Database.display("SELECT distinct ID FROM StockOutDetails WHERE isDone=0", posinv.gridControl1, posinv.gridView2);
                posinv.ShowDialog(this);
                if (POS.POSUnUploadInv.isdone == true)
                {
                    //retrieve
                    txtbatchid.Text = POS.POSUnUploadInv.invID;
                    btnnew.Enabled = false;
                    Database.display("SELECT * FROM StockOutDetails WHERE ID='" + txtbatchid.Text + "' ", gridControl1, gridView2);
                    POS.POSUnUploadInv.isdone = false;
                    posinv.Dispose();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to perform this transaction?", "StockOut Item");
            if (confirm)
            {
                if (gridView2.RowCount == 0)
                {
                    XtraMessageBox.Show("No Items to be Confirmed");
                }
                else
                {
                    //Database.ExecuteQuery($"UPDATE StockOutDetails set isDone=1 WHERE ID='{txtbatchid.Text}'");
                    doFIFO();
                    doStockOutSummary();
                    XtraMessageBox.Show("Success");
                    display();
                    disableFields();
                    btnnew.Enabled = true;
                    btnadd.Enabled = false;
                    btnconfirm.Enabled = false;
                    btncancel.Enabled = false;
                    btnrecover.Enabled = true;
                    clear();
                    txtremarks.Text = "";
                    this.Dispose();
                }
            }
            else
            {
                return;
            }

            
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            btnnew.Enabled = false;

            // Validate input
            if (string.IsNullOrEmpty(txtbatchid.Text) || 
                string.IsNullOrEmpty(txtdatein.Text) || 
                string.IsNullOrEmpty(txtproduct.Text))
            {
                XtraMessageBox.Show("No Empty Fields", "Validation Error");
                return;
            }

            // Validate quantity
            if (!decimal.TryParse(txtqty.Text, out decimal quantity))
            {
                XtraMessageBox.Show("Quantity must be a valid number", "Validation Error");
                return;
            }

            if (!decimal.TryParse(txtavailable.Text, out decimal available))
            {
                XtraMessageBox.Show("Available quantity is invalid", "Validation Error");
                return;
            }

            if (quantity > available)
            {
                XtraMessageBox.Show("Quantity must not be greater than available quantity", "Validation Error");
                return;
            }

            if (quantity <= 0)
            {
                XtraMessageBox.Show("Quantity must be greater than zero", "Validation Error");
                return;
            }

            // Populate the StockOutItem with all required information
            _currentItem.BatchID = txtbatchid.Text;
            _currentItem.DateOut = txtdatein.Text;
            _currentItem.Quantity = quantity;
            _currentItem.EncodedBy = Login.isglobalUserID;
            _currentItem.DateEncoded = DateTime.Now.ToShortDateString();

            // Calculate the total cost before insertion
            _currentItem.CalculateTotalCost();

            // Insert the item
            OutInventory();

            // Refresh the display and clear the form
            display();
            clear();
        }

        private void txtproduct_EditValueChanged(object sender, EventArgs e)
        {
            // Populate product details from the SearchLookUp
            object productCode = SearchLookUpClass.getSingleValue(txtproduct, "ProductCode");
            object description = SearchLookUpClass.getSingleValue(txtproduct, "Description");
            object barcode = SearchLookUpClass.getSingleValue(txtproduct, "Barcode");
            object available = SearchLookUpClass.getSingleValue(txtproduct, "Available");
            object cost = SearchLookUpClass.getSingleValue(txtproduct, "Cost");
            object isVat = SearchLookUpClass.getSingleValue(txtproduct, "isVat");

            // Update the current item with product information
            if (productCode != null) _currentItem.ProductCode = productCode.ToString();
            if (description != null) _currentItem.Description = description.ToString();
            if (barcode != null) _currentItem.Barcode = barcode.ToString();
            if (cost != null && decimal.TryParse(cost.ToString(), out decimal costValue))
            {
                _currentItem.Cost = costValue;
            }
            if (isVat != null && bool.TryParse(isVat.ToString(), out bool isVatValue))
            {
                _currentItem.IsVat = isVatValue;
            }

            // Display available quantity
            if (available != null)
            {
                txtavailable.Text = available.ToString();
                // Pre-fill quantity with available amount
                if (decimal.TryParse(available.ToString(), out decimal availableQty))
                {
                    txtqty.Text = availableQty.ToString();
                    _currentItem.Quantity = availableQty;
                    // Calculate total cost immediately
                    _currentItem.CalculateTotalCost();
                }
            }
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }
    }
}