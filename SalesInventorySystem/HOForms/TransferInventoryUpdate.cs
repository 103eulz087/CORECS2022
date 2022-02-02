using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
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

namespace SalesInventorySystem.HOForms
{
    public partial class TransferInventoryUpdate : Form
    {
        DataTable table;
        //string transferto = "";
        public TransferInventoryUpdate()
        {
            InitializeComponent();
        }

        private void TransferInventoryUpdate_Load(object sender, EventArgs e)
        {
          
            this.ActiveControl = txtskuno;
            txtskuno.Focus();
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber"); //IDGenerator.getTransferedNumber();

            if (perpallet.Checked == true && radTransferToBigBlue.Checked == true) //source is com
            {
                Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM Inventory WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            }
            else if (perpallet.Checked == true && radTransferToCom.Checked == true)
            {
                Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM InventoryBigBlue WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F5) //PAYMENT
            {
                saveBtn.PerformClick();
            }
            else if (keyData == Keys.Delete) //PAYMENT
            {
                cancelLineBtn.PerformClick();
            }
            else if (keyData == Keys.F1) //PAYMENT
            {
                radTransferToBigBlue.Checked = true;
            }
            else if (keyData == Keys.F2) //PAYMENT
            {
                radTransferToCom.Checked = true;
            }
            return functionReturnValue;
        }

        void loadgridview1()
        {
            table = new DataTable();
            table.Columns.Add("Counter");
            table.Columns.Add("Branch");
            table.Columns.Add("Product");
            table.Columns.Add("Description");
            table.Columns.Add("Barcode");
            table.Columns.Add("Quantity");
            table.Columns.Add("DateTransferred");
            table.Columns.Add("IsWarehouse");
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }

        private void txtskuno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addBtn.PerformClick();

        }

        void display()
        {
            
         
            //if (radTransferToBigBlue.Checked == true)
            //{
            //    //type = "0";
            //    transferto = "BigBlue";
            //}
            //else
            //{
            //    //type = "1";
            //    transferto = "Commissary";
            //}

            //Database.display("SELECT * FROM Inventory WHERE IsWarehouse='"+type+"' and Barc");
        }

        void add()
        {
            
            try
            {
               
              
                //if (radTransferToBigBlue.Checked == true)
                //{
                    
                //    transferto = "BigBlue";
                //}
                //else
                //{
                   
                //    transferto = "Commissary";
                //}

                if (txtskuno.Text == "")
                {
                    XtraMessageBox.Show("Textfield must not empty");
                    txtskuno.Text = "";
                    txtskuno.Focus();
                }
                else
                {
                    bool isexists = Database.checkifExist("SELECT Barcode FROM Inventory WHERE Barcode='" + txtskuno.Text + "'");
                    for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    {
                        if (gridView1.GetRowCellValue(i, "Barcode").ToString() == txtskuno.Text)
                        {
                            XtraMessageBox.Show("This Barcode Already Exist!");
                            txtskuno.Text = "";
                            return;
                        }
                    }
                    if (!isexists)
                    {
                        XtraMessageBox.Show("Barcode not exist in the record!");
                        txtskuno.Text = "";
                        return;
                    }
                    else
                    {
                        int ctr = 0;
                        for (int i = 1; i <= gridView1.RowCount; i++)
                        {
                            ctr = i;
                        }
                        DataRow newRow = table.NewRow();
                        newRow["Counter"] = ++ctr;
                        newRow["Branch"] = Login.assignedBranch;
                        newRow["Product"] = Login.assignedBranch;
                        newRow["Description"] = Login.assignedBranch;
                        newRow["Barcode"] = Login.assignedBranch;
                        newRow["Quantity"] = DateTime.Now.ToShortDateString();
                        newRow["DateTransferred"] = DateTime.Now.ToShortDateString();
                        newRow["IsWarehouse"] = "1";
                        table.Rows.Add(newRow);
                        gridControl1.DataSource = table;
                        txtskuno.Text = "";
                        txtskuno.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void insertTransfer()
        {
            string type = "",source="",destination="", getMyProductCode, getMyProductName, getMyQuantity, getMyDateReceived;
            if (radTransferToBigBlue.Checked == true) //transfer to bigbllue
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
           
            if (perbarcode.Checked == true) //if per barcode
            {
                if (radTransferToBigBlue.Checked == true)
                {
                    getMyProductCode = Database.getSingleQuery("Inventory", "Barcode='" + txtskuno.Text + "' and Branch='888'  ", "Product");
                    getMyProductName = Database.getSingleQuery("Inventory", "Barcode='" + txtskuno.Text + "' and Branch='888'  ", "Description");
                    getMyQuantity = Database.getSingleQuery("Inventory", "Barcode='" + txtskuno.Text + "' and Branch='888'   ", "Available");
                    getMyDateReceived = Database.getSingleQuery("Inventory", "Barcode='" + txtskuno.Text + "' and Branch='888'  ", "DateReceived");
                    if(Convert.ToDouble(getMyQuantity) <= 0)
                    {
                        Database.ExecuteQuery("UPDATE Inventory SET Available=Quantity,isStock=1 WHERE Barcode='" + txtskuno.Text + "'", "Qty Updated!!!...");
                        getMyQuantity = Database.getSingleQuery("Inventory", "Barcode='" + txtskuno.Text + "' and Branch='888'   ", "Available");
                    }
                }
                else
                {
                    getMyProductCode = Database.getSingleQuery("InventoryBigBlue", "Barcode='" + txtskuno.Text + "' and Branch='888'  ", "Product");
                    getMyProductName = Database.getSingleQuery("InventoryBigBlue", "Barcode='" + txtskuno.Text + "' and Branch='888' ", "Description");
                    getMyQuantity = Database.getSingleQuery("InventoryBigBlue", "Barcode='" + txtskuno.Text + "' and Branch='888'  ", "Available");
                    getMyDateReceived = Database.getSingleQuery("InventoryBigBlue", "Barcode='" + txtskuno.Text + "' and Branch='888'  ", "DateReceived");
                    if (Convert.ToDouble(getMyQuantity) <= 0)
                    {
                        Database.ExecuteQuery("UPDATE InventoryBigBlue SET Available=Quantity,isStock=1 WHERE Barcode='" + txtskuno.Text + "'", "Qty Updated!!!...");
                        getMyQuantity = Database.getSingleQuery("InventoryBigBlue", "Barcode='" + txtskuno.Text + "' and Branch='888'  ", "Available");
                    }
                }

                Database.ExecuteQuery("INSERT INTO InventoryTransferred VALUES('888','" + getMyProductCode + "','" + getMyProductName + "','" + Convert.ToDateTime(getMyDateReceived).ToShortDateString() + "','" + txtskuno.Text + "','" + getMyQuantity + "','" + DateTime.Now.ToShortDateString() + "','" + type + "','" + textEdit1.Text + "','"+txtdispatchno.Text+"','" + Login.Fullname + "','" + source + "','" + destination + "','','')");
                displayTransferred();
                txtskuno.Text = "";
            }
            else //if per pallet
            {
                //Database.ExecuteQuery("INSERT INTO InventoryTransferred SELECT branch,Product,Description,DateReceived,Barcode,Quantity,GETDATE()," + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "," + textEdit1.Text + "," + txtdispatchno.Text + ",'" + Login.Fullname + "','" + source + "','" + destination + "' FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' and PalletNo='" + txtpalletno.Text + "' and Product='" + getProductCode() + "'");
                //Database.ExecuteQuery("SELECT branch,Product,Description,DateReceived,Barcode,Quantity,GETDATE(),'" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "','" + textEdit1.Text + "','" + txtdispatchno.Text + "','" + Login.Fullname + "','" + source + "','" + destination + "' FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' and PalletNo='" + txtpalletno.Text + "' and Product='" + getProductCode() + "'");
                //DataTable table = new DataTable();
                //table = FillIt(table);

                sp_Transfer(type, source, destination, "ADD");
            }
        }

      

        void sp_Transfer(string type,string source,string destination,string option)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_TransferByPallet";
            //try
            //{
                string mark = getProductCode();
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
                com.Parameters.AddWithValue("@parmoption", option);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
               
                if (option == "ADD")
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable table = new DataTable();
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = null;
                    adapter.Fill(table);
                    gridControl1.DataSource = table;
                    gridView1.BestFitColumns();
                }
                else
                {
                    com.ExecuteNonQuery();
                }
               
               
            //}
            //catch (SqlException ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
            con.Close();
        }

        void displayTransferred()
        {
            Database.display("SELECT PalletNo,Description,Barcode,Quantity FROM InventoryTransferred WHERE DateTransferred='" + DateTime.Now.ToShortDateString() + "' AND BatchNumber='"+textEdit1.Text+"'", gridControl1, gridView1);
        }

        void execute(string source)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_TransferPerBarcode";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmisperbarcode", "1");
            com.Parameters.AddWithValue("@parmsource", source);
            com.Parameters.AddWithValue("@parmbatchno", textEdit1.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        void save()
        {
            try
            {
               
                string source = "", destination="";
                if (radTransferToBigBlue.Checked == true) //transfer to bigblue
                {
                    source = "Commissary";
                    destination = "BigBlue";
                    if (perpallet.Checked==true) //per pallet
                    {
                        sp_Transfer("0", source, destination, "SAVE");
                        for (int i = 0; i <= gridView1.RowCount - 1; i++)
                        {
                            //source is commissary
                            Database.ExecuteQuery("Update Inventory SET Available=0" +
                                ",isStock=0" +
                                ",ReferenceCode='Trans2BigBluePPallet'" +
                                ",LastMovementDate='" + DateTime.Now.ToShortDateString() + "' " +
                                "WHERE Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' " +
                                "and ShipmentNo='" + txtshipmentno.Text + "' " +
                                "and PalletNo='" + gridView1.GetRowCellValue(i, "PalletNo").ToString() + "'" +
                                "and SequenceNumber='"+ gridView1.GetRowCellValue(i, "SequenceReferenceNumber").ToString() + "' ");
                        }

                    }
                    else
                    {
                        //perbarcode transfer
                        execute(source); 
                    }
                }
                else //transfer to commissary
                {
                    source = "BigBlue";
                    destination = "Commissary";
                    if (perpallet.Checked == true) //per pallet
                    {
                        sp_Transfer("1", source, destination, "SAVE");
                        for (int i = 0; i <= gridView1.RowCount - 1; i++)
                        {
                            //source is biglbue
                            Database.ExecuteQuery("Update InventoryBigBlue SET Available=0" +
                                ",isStock=0" +
                                ",ReferenceCode='Trans2ComPPallet'" +
                                ",LastMovementDate='" + DateTime.Now.ToShortDateString() + "' " +
                                "WHERE Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' " +
                                "and SequenceNumber='" + gridView1.GetRowCellValue(i, "SequenceReferenceNumber").ToString() + "' ");
                        }

                    }
                    else
                    {
                        //perbarcode transfer
                        execute(source);
                    }
                }
                XtraMessageBox.Show("Inventory Successfully Transferred");
                txtskuno.Text = "";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
      
        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if(perpallet.Checked==true)
            {
                bool flag = false;
                flag = Database.checkifExist("SELECT top 1 ShipmentNo FROM InventoryTransferred WHERE ShipmentNo='" + txtshipmentno.Text + "' AND PalletNo='" + txtpalletno.Text + "' and BatchNumber='" + textEdit1.Text + "' and Product='" + getProductCode() + "'");
                if(flag)
                {
                    XtraMessageBox.Show("Already Exists");
                    return;
                }
            }
            //else if(perpallet.Checked==true && radTransferToCom.Checked==true)
            //{
            //    bool flag1 = false;
            //    flag1 = Database.checkifExist("SELECT * FROM InventoryTransferred WHERE ShipmentNo='" + txtshipmentno.Text + "' AND PalletNo='" + txtpalletno.Text + "' and Source='BigBlue'");
            //    if (flag1)
            //    {
            //        XtraMessageBox.Show("Already Exists");
            //        return;
            //    }
            //}
            //if per barcode
            bool isExist = false;
            //isExist = Database.checkifExist("SELECT * FROM InventoryBigBlue WHERE Barcode='" + txtskuno.Text + "' and Branch='" + Login.assignedBranch + "' ");

            if (radTransferToBigBlue.Checked == true) //transfer to bigblue
            {
                isExist = Database.checkifExist("SELECT TOP 1 Barcode FROM Inventory WHERE Barcode='" + txtskuno.Text + "' and Branch='" + Login.assignedBranch + "' ");
            }
            else
            {
                isExist = Database.checkifExist("SELECT TOP 1 Barcode FROM InventoryBigBlue WHERE Barcode='" + txtskuno.Text + "' and Branch='" + Login.assignedBranch + "' ");
            }
            bool isdone = false;
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                if (gridView1.GetRowCellValue(i, "Barcode").ToString() == txtskuno.Text)
                {
                    isdone = true;
                }
            }
            if(isdone) //if barcode exists
            {
                XtraMessageBox.Show("Barcode Already Exist");
                txtskuno.Text = "";
                txtskuno.Focus();
                return;
            }
            if(String.IsNullOrEmpty(txtdispatchno.Text))
            {
                XtraMessageBox.Show("Add Dispatch No first.");
                txtdispatchno.Focus();
                return;
            }
            if (radTransferToBigBlue.Checked == false && radTransferToCom.Checked == false)
            {
                XtraMessageBox.Show("Please Select Transfer Type");
                txtskuno.Text = "";
                txtskuno.Focus();
                return;
            }
            if(perbarcode.Checked==true)
            {
                if (!isExist)
                {
                    XtraMessageBox.Show("Barcode Not Exist in your Inventory");
                    txtskuno.Text = "";
                    txtskuno.Focus();
                    return;
                }else
                {
                    insertTransfer();
                }
            }
            //else if (!isExist)
            //{
            //    XtraMessageBox.Show("Barcode Not Exist in your Inventory");
            //    txtskuno.Text = "";
            //    txtskuno.Focus();
            //    return;
            //}
            else //if not perbarcode
            {
                //add();
                insertTransfer();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            save();
            this.Dispose();
        }

        private void cancelLineBtn_Click(object sender, EventArgs e)
        {
            

            if(perpallet.Checked==true)
            {
                Database.ExecuteQuery("DELETE FROM InventoryTransferred WHERE Barcode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "' and BatchCode='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BatchCode").ToString() + "'");
                //if (radTransferToBigBlue.Checked == true) //transfer to bigblue - source is com
                //{
                //    Database.ExecuteQuery("UPDATE Inventory Set isStock=1,Available=Quantity WHERE Barcode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "'");

                //}
                //else //transfer to com  -source is bigblue
                //{
                //    Database.ExecuteQuery("UPDATE InventoryBigBlue Set isStock=1,Available=Quantity WHERE Barcode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString() + "'");
                //}
            }
            //gridView1.DeleteSelectedRows();
            displayTransferred();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.Columns["Branch"].Visible = false;
            gridView1.Columns["DispatchNo"].Visible = false;
            gridView1.Columns["BatchNumber"].Visible = false;
            gridView1.Columns["DateTransferred"].Visible = false;
            gridView1.Columns["isWarehouse"].Visible = false;
            gridView1.Columns["ProcessedBy"].Visible = false;
            gridView1.Columns["Source"].Visible = false;
            gridView1.Columns["Destination"].Visible = false;
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
           // new GridColumnSortInfo(view.Columns["ShipmentNo"],DevExpress.Data.ColumnSortOrder.Ascending),
            new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            }, 1);
            view.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Quantity"];
            gridView1.GroupSummary.Add(ite);

            gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");

            DevExReportTemplate.TransferInventory xct = new DevExReportTemplate.TransferInventory();
            xct.Landscape = false;
            //xct.PaperKind = System.Drawing.Printing.PaperKind.Legal;
            //xct.Margins = new System.Drawing.Printing.Margins(100, 100, 15, 100);
            string transfertype = "";
            if (radTransferToBigBlue.Checked == true)
            {
                transfertype = radTransferToBigBlue.Text;
            }
            else
            {
                transfertype = radTransferToCom.Text;
            }

            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtransfertype.Text = transfertype;
            xct.xrdispatchno.Text = txtdispatchno.Text;

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (perpallet.Checked == true)
            {
                txtpalletno.Enabled = true;
                txtshipmentno.Enabled = true;
                txtskuno.Enabled = false;
            }
        }

        private void perbarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (perbarcode.Checked == true)
            {
                txtpalletno.Enabled = false;
                txtshipmentno.Enabled = false;
                txtskuno.Enabled = true;
            }
            else
            {
                txtpalletno.Enabled = true;
                txtshipmentno.Enabled = true;
                txtskuno.Enabled = false;
            }
        }

        private void txtshipmentno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radTransferToBigBlue.Checked == true) //source is commissary
            {
                Database.displayComboBoxItems("SELECT distinct Description FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' order by Description ASC", "Description", txtproduct);
            }
            else //source is bigblue
            {
                Database.displayComboBoxItems("SELECT distinct Description FROM InventoryBigBlue WHERE ShipmentNo='" + txtshipmentno.Text + "' order by Description ASC", "Description", txtproduct);
            }
        }

        String getProductCode()
        {
            string str = "";
            if (perpallet.Checked == true && radTransferToBigBlue.Checked == true) //source is commissary
            {
                str = Database.getSingleQuery("Inventory", "Description='" + txtproduct.Text + "' and ShipmentNo='" + txtshipmentno.Text + "'", "Product");
            }
            else if (perpallet.Checked == true && radTransferToCom.Checked == true)
            {
                str = Database.getSingleQuery("InventoryBigBlue", "Description='" + txtproduct.Text + "' and ShipmentNo='" + txtshipmentno.Text + "'", "Product");
            }
            return str;
        }

        private void txtproduct_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (perpallet.Checked == true && radTransferToBigBlue.Checked == true) //source is commissary
            {
                Database.displayComboBoxItems("SELECT distinct PalletNo FROM Inventory WHERE ShipmentNo='" + txtshipmentno.Text + "' And Product='" + getProductCode() + "' order by PalletNo ASC", "PalletNo", txtpalletno);
            }
            else if (perpallet.Checked == true && radTransferToCom.Checked == true) //source is bigblue
            {
                Database.displayComboBoxItems("SELECT distinct PalletNo FROM InventoryBigBlue WHERE ShipmentNo='" + txtshipmentno.Text + "' And Product='" + getProductCode() + "' order by PalletNo ASC", "PalletNo", txtpalletno);
            }
        }

        private void txtpalletno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        void checkchanged()
        {
            if (perpallet.Checked == true && radTransferToBigBlue.Checked == true) //source is com
            {
                Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM Inventory WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            }
            else if (perpallet.Checked == true && radTransferToCom.Checked == true)
            {
                Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM InventoryBigBlue WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            }
        }
        private void radTransferToBigBlue_CheckedChanged(object sender, EventArgs e)
        {
            checkchanged();
            //if(perpallet.Checked==true && radTransferToBigBlue.Checked==true) //source is com
            //{
            //    Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM Inventory WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            //}
            //else if (perpallet.Checked == true && radTransferToCom.Checked == true)
            //{
            //    Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM InventoryBigBlue WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            //}
        }

        private void radTransferToCom_CheckedChanged(object sender, EventArgs e)
        {
            checkchanged();
            //if (perpallet.Checked == true && radTransferToBigBlue.Checked == true) //source is com
            //{
            //    Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM Inventory WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            //}
            //else if (perpallet.Checked == true && radTransferToCom.Checked == true)
            //{
            //    Database.displayComboBoxItems("SELECT distinct ShipmentNo FROM InventoryBigBlue WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", "ShipmentNo", txtshipmentno);
            //}
        }
    }
}
