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
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class TransferUpdateDevEx : DevExpress.XtraEditors.XtraForm
    {
        string source = "";
        int ctr2 = 0;
        bool flag=false;
        DataTable table;
        string productcode;
        public TransferUpdateDevEx()
        {
            InitializeComponent();
        }

        private void TransferUpdateDevEx_Load(object sender, EventArgs e)
        {
            searchLookUpEdit1.Enabled = true;
            txtbarcodescanning.Enabled = false;
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber"); //IDGenerator.getTransferedNumber();
            display();
            populate();
        }

        void display()
        {
            table = new DataTable();
            table.Columns.Add("ProductCode");
            table.Columns.Add("Description"); 
            table.Columns.Add("Qty"); 
            table.Columns.Add("Barcode");
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }
        void populate()
        {
            Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM Inventory WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='888' ORDER BY ProductCode", searchLookUpEdit1, "Description", "Description");
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = searchLookUpEdit1.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            object value = view.GetRowCellValue(rowHandle, "ProductCode");
            productcode = value.ToString();
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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string barcode = "";
            bool isBarcodeLong = false;
            isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
           
            decimal quantity;
            string strquantity;
            quantity = Decimal.Parse(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Qty").ToString());
            strquantity = String.Format("{0:00.000}", quantity);
            
            
            if (e.Column.FieldName == "Qty")
            {
                //for (int i = 0; i <= gridView1.RowCount - 1; i++)
                //{
                //    ctr2++;
                //    break;
                //}
                if (isBarcodeLong == true)
                {
                    barcode = "11111" + productcode + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                }
                else
                {
                    barcode = productcode + strquantity.Replace(".", "") + sequencePadding(ctr2.ToString());
                }
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Barcode", barcode);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            GridView view = gridControl1.FocusedView as GridView;
            //view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            //new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            //}, 1);
            view.SortInfo.Clear();
          

            string src;
            if(bigblue.Checked==false && Commissary.Checked==false)
            {
                XtraMessageBox.Show("Please Select Destination");
                return;
            }
           
         
            if(bigblue.Checked==true)
            {
                
                src = "BigBlue";
            
                source = "BIGBLUE";
            }
            else
            {
                src = "Commissary";
             
                source = "COMMISSARY";
            }
            Database.ExecuteQuery("DELETE FROM TempInvTransfer WHERE BatchNumber='"+textEdit1.Text+"'");
            for (int i = 0; i <= gridView1.RowCount - 1; i++) //for checking only
            {
               Database.ExecuteQuery("INSERT INTO TempInvTransfer VALUES('"+textEdit1.Text+"','" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridView1.GetRowCellValue(i, "Description").ToString() + "','" + gridView1.GetRowCellValue(i, "Qty").ToString() + "','" + src + "')");
            }
            flag = false;
            //checkFIFO();
            if (flag == true)
            {
                XtraMessageBox.Show("One of your Inventory is Greater than source amount");
                return;
            }
            else
            {
                for (int j = 0; j <= gridView1.RowCount - 1; j++)
                {
                    double sourceqty = 0.0;
                    string proddesc = "";
                    if (bigblue.Checked == true)
                    {
                        sourceqty = Database.getTotalSummation2("InventoryBigBlue", "isStock=1 and Available > 0 and Branch='888' and Product='" + gridView1.GetRowCellValue(j, "ProductCode").ToString() + "'", "Available");
                    }
                    else
                    {
                        sourceqty = Database.getTotalSummation2("Inventory", "isStock=1 and Available > 0 and Branch='888' and Product='" + gridView1.GetRowCellValue(j, "ProductCode").ToString() + "'", "Available");
                    }
                    if (Convert.ToDouble(gridView1.GetRowCellValue(j, "Qty").ToString()) > sourceqty)
                    {
                        proddesc = gridView1.GetRowCellValue(j, "Description").ToString();
                        XtraMessageBox.Show("One of your Inventory is Greater than source amount " + proddesc);
                        return;
                    }
                }
                doFIFO(source);
                XtraMessageBox.Show("Successfully Transferred!");
            }
            this.Dispose();
        }

        void doFIFO(string sourcedest)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_InventoryTransferFIFO";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmdate", DateTime.Now.ToShortDateString());
                com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                com.Parameters.AddWithValue("@parmbatchnumber", textEdit1.Text);
                com.Parameters.AddWithValue("@parmsource", sourcedest);
                com.Parameters.AddWithValue("@parmdispatchno", txtdispatchno.Text);
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


        void checkFIFO()
        {
            
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_CheckInventoryTransfer";
            try
            {
                flag = false;
                string source = "";
                if(bigblue.Checked==true)
                {
                    source = "BIGBLUE";
                }
                else
                {
                    source = "COMMISSARY";
                }
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmsource", source);
                com.Parameters.Add("@parmiserror", SqlDbType.Bit).Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                flag = Convert.ToBoolean(com.Parameters["@parmiserror"].Value.ToString());

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gridControl2.DataSource = table;
                gridView2.BestFitColumns();
               
                
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message);
                return;
            }
            con.Close();
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Qty")
                e.Cancel = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("No Data to print");
                return;
            }
            if (bigblue.Checked == true)
            {
                BigBlueTemplate();
            }
            else
            {
                CommissaryTemplate();
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
            xct.xrdispatchno.Text = txtdispatchno.Text;

            //xct.xrdateneeded.Text = DateTime.Now.ToShortDateString();
            //xct.xrrequestedby.Text = Login.Fullname;
            //xct.xrdaterequest.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now);
            //xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);
          

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void CommissaryTemplate()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption2");

            DevExReportTemplate.StorageReceivingForm xct = new DevExReportTemplate.StorageReceivingForm();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
            //xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void printBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string qtydel = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Qty").ToString();
            DateTime dt;
            dt = Convert.ToDateTime(dateTimePicker1.Text).AddYears(1);
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblmanufdate.Text = dateTimePicker1.Text;
            bprint.lblprodtype.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            bprint.lbltotalkilos.Text = qtydel;
            bprint.xrBarCode2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
            bprint.lblxpirydate.Text = dt.ToShortDateString();
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool isexist = Database.checkifExist("SELECT DispatchNo FROM InventoryTransferred WHERE DispatchNo='" + txtdispatchno.Text + "'");
            if(String.IsNullOrEmpty(txtdispatchno.Text) && bigblue.Checked==true)
            {
                XtraMessageBox.Show("Dispatch Number must not Empty");
                return;
            }
            else if (isexist && bigblue.Checked==true)
            {
                XtraMessageBox.Show("Dispatch Number Already Exist!..");
                return;
            }
            else if(String.IsNullOrEmpty(searchLookUpEdit1.Text))
            {
                XtraMessageBox.Show("Product Field must not Empty!!!...");
                return;
            }
            else
            {
                add();
            }
            if (barcodescanning.Checked == true)
            {
                txtbarcodescanning.Text = "";
                txtbarcodescanning.Focus();
            }
            else
            {
                searchLookUpEdit1.Focus();
            }
        }

        private void barcodescanning_CheckedChanged(object sender, EventArgs e)
        {
            if(barcodescanning.Checked==true)
            {
                searchLookUpEdit1.Enabled = false;
                txtbarcodescanning.Enabled = true;
                txtbarcodescanning.Focus();
            }
            else
            {
                searchLookUpEdit1.Enabled = true;
                txtbarcodescanning.Enabled = false;
                searchLookUpEdit1.Focus();
            }
        }

        private void txtbarcodescanning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { simpleButton1.PerformClick();
               
            }
                
        }

        private void txtshipmentno_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT distinct Description FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' order by Description ASC", "Description", txtproduct);
        }
        String getProductCode()
        {
            string str = Database.getSingleQuery("Inventory", "Description='" + txtproduct.Text + "' and ShipmentNo='" + txtshipmentno.Text + "'", "Product");
            return str;
        }
        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT distinct PalletNo FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' And Product='" + getProductCode() + "' order by PalletNo ASC", "PalletNo", txtpalletno);
        }
        void displayTransferred()
        {
            Database.display("SELECT * FROM InventoryTransferred WHERE DateTransferred='" + DateTime.Now.ToShortDateString() + "' AND BatchNumber='" + textEdit1.Text + "'", gridControl1, gridView1);
        }
        void transferPerPallet()
        {
            string type = "", source = "", destination = "";
            if (Commissary.Checked == true) //transfer to bigbllue
            {
                type = "0";
                //transferto = "BigBlue";
                source = "Commissary";
                destination = "BigBlue";
            }
            else //transfer to commissary
            {
                type = "1";
                //transferto = "Commissary";
                source = "BigBlue";
                destination = "Commissary";
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_TransferByPallet";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmprodcode", getProductCode());
                com.Parameters.AddWithValue("@parmpalletno", txtpalletno.Text);
                com.Parameters.AddWithValue("@parmtype", type);
                com.Parameters.AddWithValue("@parmbatchnumber", textEdit1.Text);
                com.Parameters.AddWithValue("@parmdispatchno", txtdispatchno.Text);
                com.Parameters.AddWithValue("@parmsource", source);
                com.Parameters.AddWithValue("@parmdestination", destination);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            con.Close();
            displayTransferred();
        }

        private void txtdispatchno_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        void add()
        {
            string pcode = "", desc = "", qty = "", barcode = "",qty1="",qty2="";
            bool isBarcodeLong = false;
            isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
           
            if (barcodescanning.Checked==true)
            {
                if(isBarcodeLong==false) //short barcode
                {
                    if (txtbarcodescanning.Text.Length == 14) //tens 10015 10123 0001
                    {
                        pcode = txtbarcodescanning.Text.Substring(0, 5);
                        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                        qty1 = txtbarcodescanning.Text.Substring(5, 2); //1001512345
                        qty2 = txtbarcodescanning.Text.Substring(7, 3);
                        qty = qty1 + "." + qty2;
                        barcode = txtbarcodescanning.Text;
                    }
                    else if (txtbarcodescanning.Text.Length == 15) //hundred
                    {
                        pcode = txtbarcodescanning.Text.Substring(0, 5);
                        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                        qty1 = txtbarcodescanning.Text.Substring(5, 3); //10015100345
                        qty2 = txtbarcodescanning.Text.Substring(8, 3);
                        qty = qty1 + "." + qty2;
                        barcode = txtbarcodescanning.Text;
                    }
                    else if (txtbarcodescanning.Text.Length == 16) //thousand
                    {
                        pcode = txtbarcodescanning.Text.Substring(0, 5);
                        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                        qty1 = txtbarcodescanning.Text.Substring(5, 4); //10015100345
                        qty2 = txtbarcodescanning.Text.Substring(9, 3);
                        qty = qty1 + "." + qty2;
                        barcode = txtbarcodescanning.Text;
                    }
                    else
                    {
                        XtraMessageBox.Show("Invalid Barcode Type!.. Please use manual input!..");
                        return;
                    }
                }
                else
                {
                    if (txtbarcodescanning.Text.Length == 19) //tens 11111 10015 10123 0001 --10.123 kilos
                    {
                        pcode = txtbarcodescanning.Text.Substring(5, 5);
                        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                        qty1 = txtbarcodescanning.Text.Substring(10, 2); //1001512345
                        qty2 = txtbarcodescanning.Text.Substring(12, 3);
                        qty = qty1 + "." + qty2;
                        barcode = txtbarcodescanning.Text;
                    }
                    else if (txtbarcodescanning.Text.Length == 20) //hundred 11111 10015 100123 0001 --100.123 kilos
                    {
                        pcode = txtbarcodescanning.Text.Substring(5, 5);
                        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                        qty1 = txtbarcodescanning.Text.Substring(10, 3); //10015100345
                        qty2 = txtbarcodescanning.Text.Substring(13, 3);
                        qty = qty1 + "." + qty2;
                        barcode = txtbarcodescanning.Text;
                    }
                    else if (txtbarcodescanning.Text.Length == 21) //thousand  11111 10015 1000123 0001 --1000.123 kilos
                    {
                        pcode = txtbarcodescanning.Text.Substring(5, 5);
                        desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
                        qty1 = txtbarcodescanning.Text.Substring(10, 4); //10015100345
                        qty2 = txtbarcodescanning.Text.Substring(14, 3);
                        qty = qty1 + "." + qty2;
                        barcode = txtbarcodescanning.Text;
                    }
                    else
                    {
                        XtraMessageBox.Show("Invalid Barcode Type!.. Please use manual input!..");
                        return;
                    }
                }
               
            }
            else
            {
                pcode = productcode;
                desc = searchLookUpEdit1.Text;
                qty = "0";
                barcode = "";
            }

          
            gridControl1.BeginUpdate();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            DataRow newRow = table.NewRow();
            newRow["ProductCode"] = pcode;//productcode;
            newRow["Description"] = desc;//searchLookUpEdit1.Text;
            newRow["Qty"] = qty;
            newRow["Barcode"] = barcode;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            gridView1.Columns["Qty"].Summary.Clear();
            // gridView2.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}"); //duplicate footer
            gridControl1.EndUpdate();

          
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Qty";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Qty"];
            gridView1.GroupSummary.Add(ite);

            gridView1.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0:n2}");

        }
    }
}