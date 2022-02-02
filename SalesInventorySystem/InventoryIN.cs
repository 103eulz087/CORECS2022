using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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

namespace SalesInventorySystem
{
    public partial class InventoryIN : Form
    {
        //DataTable table;
        //int ctr = 0;
        public InventoryIN()
        {
            InitializeComponent();
        }

        private void InventoryIN_Load(object sender, EventArgs e)
        {
            txtdestination.Text = "";
            if (Login.assignedBranch == "888")
            {
                lbldestination.Visible = true;
                txtdestination.Visible = true;
            }
            loadInvNum();
            loadProdcat();
            //table = new DataTable();

            //table.Columns.Add("Branch");
            //table.Columns.Add("ShipmentNo");
            //table.Columns.Add("PalletNo"); //UnitPrice
            //table.Columns.Add("BatchCode"); //UnitPrice
            //table.Columns.Add("DateReceived"); //UnitPrice
            //table.Columns.Add("Product"); //UnitPrice
            //table.Columns.Add("Description");
            //table.Columns.Add("Barcode");
            //table.Columns.Add("TipWeight");
            //table.Columns.Add("Quantity");
            //table.Columns.Add("Cost"); //UnitPrice
            //table.Columns.Add("Available");
            //table.Columns.Add("IsStock");
            //table.Columns.Add("IsVat");
            //table.Columns.Add("IsWarehouse");
            //table.Columns.Add("ReferenceCode");


            //gridControl1.DataSource = table;
            //gridView1.BestFitColumns();
        }
        void loadInvNum()
        {
            txtid.Text = IDGenerator.getIDNumberSP("sp_GetInventoryINNumber","InventoryID");
        }

        void loadProdcat()
        {
            Classes.Product.displayProductCategoryComboBoxItems(txtprodcat);
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(scanBarcode.Checked==true)
            {
                XtraMessageBox.Show("Please Uncheck Scan Barcode!");
                return;
            }
            //if (txtbarcode.Text == "")
            //{
            //    XtraMessageBox.Show("Textfield must not empty");
            //    txtbarcode.Text = "";
            //    txtbarcode.Focus();
            //}
            if(String.IsNullOrEmpty(txtdestination.Text) && Login.assignedBranch=="888")
            {
                XtraMessageBox.Show("Please Select Destination!.. Note: Destination Applied in Every Item Entered..");
                return;
            }
            //else if (txtbarcode.Text.Length < 13)
            //{
            //    XtraMessageBox.Show("Barcode Must be 13 Characters");
            //    txtbarcode.Text = "";
            //    txtbarcode.Focus();
            //}
            if (chckboxbarcode.Checked == true)
            {
                simpleButton5.PerformClick();
                add();
            }
            else
            {
                add();
            }
          
        }

        void add()
        {
            string destination = "";
            //int ctr = 1;
            //for (int i = 0; i <= gridView1.RowCount - 1; i++)
            //{
            //    ctr++;
            //}
            string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
            string prodcde = Classes.Product.getProductCode(txtproduct.Text, prodcatcode);
            string isvat = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text + "'", "isVat");
            if (txtdestination.Text == "BigBlue")
                {
                    destination = "0";
                Database.ExecuteQuery("INSERT INTO InventoryIN(ID,Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Description,Barcode,TipWeight,Quantity,Cost,Available,QtyBigBlue,IsStock,IsVat,IsWarehouse,ReferenceCode,LastMovementDate,isProcess,isSource,isConversion,EncodeBy) VALUES('" + txtid.Text + "','" + Login.assignedBranch + "','00000','0','0','" + txtdatereceived.Text + "','" + prodcde + "','" + txtproduct.Text + "','" + txtbarcode.Text + "','" + txtweight.Text + "','" + txtweight.Text + "','" + txtcosting.Text + "',0,'" + txtweight.Text + "','1','" + isvat + "','" + destination + "','','" + DateTime.Now.ToShortDateString() + "','0','1','0','" + Login.isglobalUserID + "')");

            }
            else
                {
                    destination = "1";
                Database.ExecuteQuery("INSERT INTO InventoryIN(ID,Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Description,Barcode,TipWeight,Quantity,Cost,Available,QtyBigBlue,IsStock,IsVat,IsWarehouse,ReferenceCode,LastMovementDate,isProcess,isSource,isConversion,EncodeBy) VALUES('" + txtid.Text + "','" + Login.assignedBranch + "','00000','0','0','" + txtdatereceived.Text + "','" + prodcde + "','" + txtproduct.Text + "','" + txtbarcode.Text + "','" + txtweight.Text + "','" + txtweight.Text + "','" + txtcosting.Text + "','" + txtweight.Text + "',0,'1','" + isvat + "','" + destination + "','','" + DateTime.Now.ToShortDateString() + "','0','1','0','" + Login.isglobalUserID + "')");

            }
            //DataRow newRow = table.NewRow();
            //newRow["Branch"] = Login.assignedBranch;
            //newRow["ShipmentNo"] = "00000";
            //newRow["PalletNo"] = "0";
            //newRow["BatchCode"] = "0";
            //newRow["DateReceived"] = txtdatereceived.Text;
            //newRow["Product"] = prodcde;
            //newRow["Description"] = txtproduct.Text;
            //newRow["Barcode"] = txtbarcode.Text;
            //newRow["TipWeight"] = txtweight.Text;
            //newRow["Quantity"] = txtweight.Text; //Classes.BarcodeSettings.getBarcodeQuantity(txtbarcode.Text);
            //newRow["Cost"] = txtcosting.Text;
            //newRow["Available"] = txtweight.Text; //Classes.BarcodeSettings.getBarcodeQuantity(txtbarcode.Text);
            //newRow["IsStock"] = "1";
            //newRow["IsVat"] = "1";
            //newRow["IsWarehouse"] = "1"; 
            //newRow["ReferenceCode"] = ""; 


            //table.Rows.Add(newRow);
            //gridControl1.DataSource = table;

            display();
           // txtcosting.Text = "";
            txtbarcode.Text = "";
            //txtproduct.Text = "";
            //txtweight.Text = "";
            txtweight.Focus();
        }


        void display()
        {
            Database.display("SELECT SequenceNumber,Branch,DateReceived,Product,Description,Quantity,Cost,IsVat FROM InventoryIN WHERE EncodeBy='" + Login.isglobalUserID + "'", gridControl1, gridView1);
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.Product.displayProductComboBoxItems(txtproduct, txtprodcat.Text, Login.assignedBranch);
        }
        public String sequencePadding(string str)
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
        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool isBarcodeLong = false;
                isBarcodeLong = Database.checkifExist("SELECT TOP(1) isLong FROM dbo.BarcodeSettings WHERE isLong=1");
                decimal quantity;
                string strquantity;
                int ctr2 = 1;
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    ctr2++;
                }
                quantity = Decimal.Parse(txtweight.Text);
                strquantity = String.Format("{0:00.000}", quantity);
               
                string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
                string prodcde = Classes.Product.getProductCode(txtproduct.Text, prodcatcode);
                Random rand = new Random();
                int ctr = rand.Next(0, 9);
                if(isBarcodeLong==true)
                {
                    txtbarcode.Text = "55555" + prodcde + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                }
                else
                {
                    txtbarcode.Text = prodcde + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                }

                //txtbarcode.Text = prodcatcode + prodcde + txtweight.Text.Replace(".", "") + ctr.ToString();
                simpleButton1.Focus();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            int ctr = gridView1.RowCount - 1;
            try
            {
                string query = "sp_UploadInventoryIN";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmid", txtid.Text);
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
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
            //for (int i = 0; i <= gridView1.RowCount - 1; i++)
            //{
            //    Database.ExecuteQuery("INSERT INTO Inventory (Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Description,Barcode,TipWeight,Quantity,Cost,Available,IsStock,IsVat,IsWarehouse,LastMovementDate,isProcess,isSource,isConversion) VALUES ('" + Login.assignedBranch + "','00000',0,0,'" + DateTime.Now.ToShortDateString() + "','" + gridView1.GetRowCellValue(i, "Product").ToString() + "','" + gridView1.GetRowCellValue(i, "Description").ToString() + "','" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "','" + gridView1.GetRowCellValue(i, "Quantity").ToString() + "','" + gridView1.GetRowCellValue(i, "Quantity").ToString() + "','" + gridView1.GetRowCellValue(i, "Cost").ToString() + "','" + gridView1.GetRowCellValue(i, "Quantity").ToString() + "',1,'" + gridView1.GetRowCellValue(i, "IsVat").ToString() + "',1,'" + DateTime.Now.ToShortDateString() + "',0,1,0)");
            //}

            //// Database.ExecuteQuery("SELECT Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Desription,Barcode,TipWeight,Quantity,Cost,Available,IsStock,IsVat,IsWarehouse,ReferenceCode,LastMovementDate,isProcess,isSource,isConversion FROM InventoryIN");
            //Database.ExecuteQuery("DELETE FROM InventoryIN WHERE EncodeBy='" + Login.isglobalUserID + "'");
            XtraMessageBox.Show("Successfully Added!");
            this.Dispose();
        }

        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtcosting.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            
            Database.ExecuteQuery("DELETE FROM InventoryIN WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'","Successfully Deleted");
            XtraMessageBox.Show("Successfully Deleted");
            display();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtweight.Text = "";
            txtcosting.Text = "0";
            txtproduct.Text = "";
            txtbarcode.Text = "";
            txtproduct.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblmanufdate.Text = txtdatereceived.Text;
            bprint.lblxpirydate.Text = txtxpirydate.Text;
            bprint.lblprodtype.Text = txtproduct.Text;
            bprint.lbltotalkilos.Text = txtweight.Text;
            bprint.xrBarCode2.Text = txtbarcode.Text.Trim(); //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
            //report.ShowRibbonPreviewDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to recover unsave transaction?", "Recovered");
            //if (confirm)
            //{
            //    AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
            //    authfrm.ShowDialog(this);
            //    if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
            //    {
            //        Database.display("SELECT Branch,DateReceived,Product,Description,Quantity,Cost,IsVat FROM InventoryIN WHERE EncodeBy='" + Login.isglobalUserID + "'", gridControl1, gridView1);
            //        AuthorizedConfirmationFrm.isconfirmedLogin = false;
            //        authfrm.Dispose();
            //    }
            //}
            //else
            //{
            //    return;
            //}
            ReInventoryINRecovery revin = new ReInventoryINRecovery();
            revin.ShowDialog(this);
            if (ReInventoryINRecovery.isdone == true)
            {
                bool checkfirst = Database.checkifExist("SELECT TOP(1) ID FROM InventoryIN WHERE ID = '" + ReInventoryINRecovery.id + "'");
                if (checkfirst)
                {
                    txtid.Text = ReInventoryINRecovery.id;
                    Database.display("SELECT * FROM InventoryIN WHERE ID='" + txtid.Text + "'", gridControl1, gridView1);
                }
                else
                {
                    XtraMessageBox.Show("Inventory ID Not Exist in Temporary Container, This Number is either not exist OR it is already Uploaded in Inventory Table");
                    return;
                }
                ReInventoryINRecovery.isdone = false;
                revin.Dispose();
            }
        }

        private void txtcosting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtweight.Focus();
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (scanBarcode.Checked == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    bool isexist = Database.checkifExist("SELECT TOP(1) Barcode FROM InventoryALL WHERE Barcode='" + txtbarcode.Text + "'");
                    if (isexist)
                    {
                        string destination = "0";
                        if (txtdestination.Text == "BigBlue")
                        {
                            destination = "0";
                        }
                        else
                        {
                            destination = "1";
                        }
                        Database.ExecuteQuery("INSERT INTO InventoryIN(ID,Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Description,Barcode,TipWeight,Quantity,Cost,Available,IsStock,IsVat,IsWarehouse,ReferenceCode,LastMovementDate,isProcess,isSource,isConversion,EncodeBy) SELECT TOP 1 '" + txtid.Text + "',Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Description,Barcode,TipWeight,Quantity,Cost,Available,IsStock,IsVat,"+ destination + ",ReferenceCode,LastMovementDate,isProcess,isSource,isConversion,'" + Login.isglobalUserID + "' FROM InventoryALL WHERE Barcode='" + txtbarcode.Text + "' ORDER BY SequenceNumber DESC");
                        //Database.ExecuteQuery("SELECT TOP 1 '" + txtid.Text + "',Branch,ShipmentNo,PalletNo,BatchCode,DateReceived,Product,Description,Barcode,TipWeight,Quantity,Cost,Available,IsStock,IsVat,IsWarehouse,ReferenceCode,LastMovementDate,isProcess,isSource,isConversion,'" + Login.Fullname + "' FROM InventoryALL WHERE Barcode='" + txtbarcode.Text + "' ORDER BY DESC");
                        display();
                        txtbarcode.Text = "";
                        txtbarcode.Focus();
                    }
                    else
                    {
                        XtraMessageBox.Show("Not Exist in Record.. Please Re Encode this Item and Print Sticker");
                        return;
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Please Check Scan Barcode Checkbox");
                return;
            }
        }

        private void scanBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if(scanBarcode.Checked==true)
            {
                txtbarcode.Text = "";
                txtbarcode.Focus();
            }
        }
    }
}
