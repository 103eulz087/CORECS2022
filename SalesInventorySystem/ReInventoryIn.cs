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
using DevExpress.XtraGrid;
using SalesInventorySystem.Classes;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem
{
    public partial class ReInventoryIn : DevExpress.XtraEditors.XtraForm
    {
        //DataTable table;
        object brcode = null;
        public static bool ispriceused = false, isusedbarcode = false;
        bool isusedsearchform = false;//, isusedbarcode = false;//, ispriceused=false;
        public static string seqno = "",branchcode="";
        public static bool iswarehouse = false;
        public ReInventoryIn()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            //if (keyData == Keys.Enter)
            //{
            //    simpleButton1.PerformClick();
            //}
            if (keyData == Keys.F1)
            {
                simpleButton6.PerformClick();
            }
            else if (keyData == Keys.F2)
            {
                btnclear.PerformClick();
            }
            else if (keyData == Keys.Delete)
            {
                simpleButton3.PerformClick();
            }
            else if (keyData == Keys.Down)
            {
                gridView1.Focus();
            }
            return functionReturnValue;
        }

      
        void display()
        {
            Database.display("SELECT * " +
                $"FROM dbo.funcview_InventoryIN('{txtid.Text}') ORDER BY SequenceNumber ASC", gridControl1, gridView1);


            //GridView view = gridControl1.FocusedView as GridView;
            //view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            //    new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            //    }, 1);
            //gridView1.ExpandAllGroups();

            GridGroupSummaryItem ite = new GridGroupSummaryItem();
            ite.FieldName = "Quantity";
            ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite.ShowInGroupColumnFooter = gridView1.Columns["Quantity"];
            gridView1.GroupSummary.Add(ite);
            gridView1.Focus();

            Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView1, "SequenceNumber");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "Quantity");
        }

        private void ReInventoryIn_Load(object sender, EventArgs e)
        {
            //Database.displayComboBoxItems("SELECT BranchName FROM Branches WHERE BranchCode='"+Login.assignedBranch+"'", "BranchName", comboBox1);
            string branchname = Database.getSingleQuery("Branches", "BranchCode='" + Login.assignedBranch + "'", "BranchName");
           
            loadInvNum();
            populateBranch();
            txtbarcodescanning.Focus();
        }

        void loadInvNum()
        {
            txtid.Text = IDGenerator.getIDNumberSP("sp_GetInventoryINNumber", "InventoryID"); //IDGenerator.getInventoryINNumber();

        }

        void populateBranch()
        {
           
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbranch,"BranchName","BranchName");
        }

     

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtbranch.Text))
            {
                XtraMessageBox.Show("Branch must not empty");
                return;
            }
            else
            { AddEntryNew(); }
        }

        //void add()
        //{
        //    string pcode = "", desc = "", qty = "", barcode = "", qty1 = "", qty2 = "";
        //    bool isBarcodeLong = false;
        //    isBarcodeLong = Database.checkifExist("SELECT * FROM BarcodeSettings WHERE isLong=1");
        //    bool iswarehouse = false;
        //    string sku = "";
         
        //    //if (sku == "")
        //    //{
        //    //    XtraMessageBox.Show("Barcode must be 13 characters");
        //    //    txtsku.Text = "";
        //    //    return;
        //    //}
        //    if (comboBox1.Text == "")
        //    {
        //        XtraMessageBox.Show("Branch Destination must not empty");
                
        //        return;
        //    }
        //    else
        //    {
        //        if (comboBox1.Text == "HEAD OFFICE" && Commissary.Checked == true)
        //        {
        //            iswarehouse = true;
        //        }
        //        else if (comboBox1.Text == "HEAD OFFICE" && bigblue.Checked == true)
        //        {
        //            iswarehouse = false;
        //        }
        //        else
        //        {
        //            iswarehouse = true;
        //        }
        //    }
        //    string prodname = "";

        //    string productcode = "";

        //    if (isusedsearchform == true) //
        //    {
               
        //        prodname = Database.getSingleQuery("Products", "ProductCode='" + productcode + "' and BranchCode='" + Login.assignedBranch + "'", "Description");
        //        barcode = Database.getSingleQuery("Products", "ProductCode='" + productcode + "' and BranchCode='" + Login.assignedBranch + "'", "Barcode");
        //    }
        //    else
        //    {
        //        if (barcodescanning.Checked == true)
        //        {
        //            if (isBarcodeLong == false) //short barcode
        //            {
        //                if (txtbarcodescanning.Text.Length == 14) //tens 10015 10123 0001
        //                {
        //                    pcode = txtbarcodescanning.Text.Substring(0, 5);
        //                    desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
        //                    qty1 = txtbarcodescanning.Text.Substring(5, 2); //1001512345
        //                    qty2 = txtbarcodescanning.Text.Substring(7, 3);
        //                    qty = qty1 + "." + qty2;
        //                    barcode = txtbarcodescanning.Text;
        //                }
        //                else if (txtbarcodescanning.Text.Length == 15) //hundred
        //                {
        //                    pcode = txtbarcodescanning.Text.Substring(0, 5);
        //                    desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
        //                    qty1 = txtbarcodescanning.Text.Substring(5, 3); //10015100345
        //                    qty2 = txtbarcodescanning.Text.Substring(8, 3);
        //                    qty = qty1 + "." + qty2;
        //                    barcode = txtbarcodescanning.Text;
        //                }
        //                else if (txtbarcodescanning.Text.Length == 16) //thousand
        //                {
        //                    pcode = txtbarcodescanning.Text.Substring(0, 5);
        //                    desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
        //                    qty1 = txtbarcodescanning.Text.Substring(5, 4); //10015100345
        //                    qty2 = txtbarcodescanning.Text.Substring(9, 3);
        //                    qty = qty1 + "." + qty2;
        //                    barcode = txtbarcodescanning.Text;
        //                }
        //                else
        //                {
        //                    XtraMessageBox.Show("Invalid Barcode Type!.. Please use manual input!..");
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                if (txtbarcodescanning.Text.Length == 19) //tens 11111 10015 10123 0001 --10.123 kilos
        //                {
        //                    pcode = txtbarcodescanning.Text.Substring(5, 5);
        //                    desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
        //                    qty1 = txtbarcodescanning.Text.Substring(10, 2); //1001512345
        //                    qty2 = txtbarcodescanning.Text.Substring(12, 3);
        //                    qty = qty1 + "." + qty2;
        //                    barcode = txtbarcodescanning.Text;
        //                }
        //                else if (txtbarcodescanning.Text.Length == 20) //hundred 11111 10015 100123 0001 --100.123 kilos
        //                {
        //                    pcode = txtbarcodescanning.Text.Substring(5, 5);
        //                    desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
        //                    qty1 = txtbarcodescanning.Text.Substring(10, 3); //10015100345
        //                    qty2 = txtbarcodescanning.Text.Substring(13, 3);
        //                    qty = qty1 + "." + qty2;
        //                    barcode = txtbarcodescanning.Text;
        //                }
        //                else if (txtbarcodescanning.Text.Length == 21) //thousand  11111 10015 1000123 0001 --1000.123 kilos
        //                {
        //                    pcode = txtbarcodescanning.Text.Substring(5, 5);
        //                    desc = Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='888'", "Description");
        //                    qty1 = txtbarcodescanning.Text.Substring(10, 4); //10015100345
        //                    qty2 = txtbarcodescanning.Text.Substring(14, 3);
        //                    qty = qty1 + "." + qty2;
        //                    barcode = txtbarcodescanning.Text;
        //                }
        //                else
        //                {
        //                    XtraMessageBox.Show("Invalid Barcode Type!.. Please use manual input!..");
        //                    return;
        //                }
        //            }

        //        }
        //        //bool checkbarcode = Database.checkifExist("SELECT Barcode FROM Products WHERE Barcode='"+txtsku.Text+"' AND BranchCode='"+Login.assignedBranch+"'");
        //        //if (!checkbarcode)
        //        //{
        //        //    XtraMessageBox.Show("Barcode Not Exist..Please used Search Item");
        //        //    txtsku.Text = "";
        //        //    return;
        //        //}
        //        //else
        //        //{
        //        //    productcode = Database.getSingleQuery("Products", "Barcode='" + txtsku.Text + "' and BranchCode='" + Login.assignedBranch + "'", "ProductCode");
        //        //    prodname = Database.getSingleQuery("Products", "Barcode='" + txtsku.Text + "' and BranchCode='" + Login.assignedBranch + "'", "Description");
        //        //    barcode = Database.getSingleQuery("Products", "Barcode='" + txtsku.Text + "' and BranchCode='" + Login.assignedBranch + "'", "Barcode");
        //        //}
        //    }
        //    Database.ExecuteQuery("INSERT INTO TempInventoryIN (ID,Branch,DateReceived,Product,Description,Barcode,Quantity,Cost,isWarehouse,DateEncode,EncodeBy) VALUES('" + txtid.Text + "','" + Login.assignedBranch + "','" + txtdatereceived.Text + "','" + pcode + "','" + desc + "','" + barcode + "','" + qty + "','0','" + iswarehouse + "','" + DateTime.Now.ToShortDateString() + "','" + Login.isglobalUserID + "')");
        //    isusedsearchform = false;
        //    display();
        //    gridView1.MoveLast();
        //    // txtcosting.Text = "";
        //    txtbarcodescanning.Text = "";
        
        //    //txtsku.Focus();
        //    txtbarcodescanning.Focus();
        //}

     

     

        private void btnclear_Click(object sender, EventArgs e)
        {
            seqno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string desc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string qty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Quantity").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            ReInventoryInEditLine editln = new ReInventoryInEditLine();
            editln.txtprodname.Text = desc;
            editln.txtqty1.Text = qty;
            editln.txtcost.Text = cost;
            editln.ShowDialog(this);
            if (ReInventoryInEditLine.isdone == true)
            {
                display();
                ReInventoryInEditLine.isdone = false;
                editln.Dispose();
                gridView1.MoveLast();
            }
            txtbarcodescanning.Focus();
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //Database.ExecuteQuery("DELETE FROM TempInventoryIN WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'");
            Database.ExecuteQuery("DELETE FROM InventoryIN WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'");
            display();
            txtbarcodescanning.Focus();
        }

        void recoverInventoryNew()
        {
            ReInventoryINRecovery revin = new ReInventoryINRecovery();
            revin.ShowDialog(this);
            if (ReInventoryINRecovery.isdone == true)
            {
                bool checkfirst = Database.checkifExist("SELECT ID FROM InventoryIN WHERE ID = '" + ReInventoryINRecovery.id + "'");
                if (checkfirst)
                {
                    Database.display("SELECT * FROM InventoryIN WHERE ID='" + ReInventoryINRecovery.id + "'", gridControl1, gridView1);
                    txtid.Text = ReInventoryINRecovery.id;
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

        //NOT USED
        void recoverInventory()
        {
            ReInventoryINRecovery revin = new ReInventoryINRecovery();
            revin.ShowDialog(this);
            if (ReInventoryINRecovery.isdone == true)
            {
                bool checkfirst = Database.checkifExist("SELECT ID FROM TempInventoryIN WHERE ID = '" + ReInventoryINRecovery.id + "'");
                if (checkfirst)
                {
                    Database.display("SELECT * FROM TempInventoryIN WHERE ID='" + ReInventoryINRecovery.id + "'", gridControl1, gridView1);
                    txtid.Text = ReInventoryINRecovery.id;
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
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            recoverInventoryNew();
        }
        void save()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            int ctr = gridView1.RowCount - 1;
            try
            {
                string query = "sp_UploadTempInventoryIN";
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

            XtraMessageBox.Show("Successfully Added!");
            this.Dispose();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Commit();
        }

        private void txtbarcodescanning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                simpleButton1.PerformClick();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            searchProductItems();
        }

        private void btnanalyze_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("This function is need to Authorized by Inventory Admin.. Are you Sure you want to Proceed?...", "Confirm");
            if (confirm)
            {
                AuthorizedConfirmationFrm authfrm = new AuthorizedConfirmationFrm();
                authfrm.ShowDialog(this);
                if (AuthorizedConfirmationFrm.isconfirmedLogin == true)
                {
                    branchcode = brcode.ToString();
                    ReInventoryAnalyzer asd = new ReInventoryAnalyzer();
                    asd.Show();
                    analyze();
                    //asd.ShowDialog(this);
                    if (POS.POSErrrorCorrect.isdone == true)
                    {
                        POS.POSErrrorCorrect.isdone = false;
                        asd.Dispose();
                    }
                    AuthorizedConfirmationFrm.isconfirmedLogin = false;
                    authfrm.Dispose();
                }
            }
            else
            {
                return;
            }
        }


        void analyze()
        {
            ReInventoryAnalyzer asd = new ReInventoryAnalyzer();
            asd.Show();
            SqlConnection con = Database.getConnection();
            con.Open();
            asd.gridControl1.BeginUpdate();
            try
            {
                string sp = "sp_ReInventoryAnalyzer";
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmbatchid", txtid.Text);
                com.Parameters.AddWithValue("@parmbranchcode",brcode.ToString());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                asd.gridView1.Columns.Clear();
                asd.gridControl1.DataSource = null;
                adapter.Fill(table);
                asd.gridControl1.DataSource = table;
                asd.gridView1.BestFitColumns();

                GridGroupSummaryItem ite = new GridGroupSummaryItem();
                ite.FieldName = "Qty";
                ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                ite.ShowInGroupColumnFooter = asd.gridView1.Columns["Qty"];
                asd.gridView1.GroupSummary.Add(ite);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                asd.gridControl1.EndUpdate();
                con.Close();
            }
        }
        //void searchProductItems()
        //{
        //    try
        //    {

        //        SearchProductItems searchprod = new SearchProductItems();
        //        searchprod.ShowDialog(this);
        //        if (SearchProductItems.isUsedSearchForm == true) //isUsedSearchForm indicator ni cya sa searchproduct form kng nigamit ba cya og searchform
        //        {
        //            txtbarcodescanning.Text = SearchProductItems.prodcode.Substring(0, 2) + SearchProductItems.prodcode;
        //            //isusedbarcode = false;
        //            SearchProductItems.isUsedSearchForm = false;


        //            searchprod.Dispose();
        //            txtbarcodescanning.Focus();
        //            isusedsearchform = true; //is isusedsearchform is a local variable declare in this class
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}
        void searchProductItems()
        {
            //try
            //{
            //    SearchProduct searchprod = new SearchProduct();
            //    searchprod.ShowDialog(this);
            //    if (SearchProduct.isUsedSearchForm == true) //isUsedSearchForm indicator ni cya sa searchproduct form kng nigamit ba cya og searchform
            //    {
            //        isusedsearchform = true; //is isusedsearchform is a local variable declare in this class
            //        if (SearchProduct.havebarcode == true) //kng pag select nya kay naay barcode
            //        {
            //            txtbarcodescanning.Text = SearchProduct.barcode; // 
            //            isusedbarcode = true;
            //            SearchProduct.isUsedSearchForm = false;
            //        }
            //        else //kung pag select nya sa item sa search product kay wlaay barcode
            //        {
            //            //txtbarcodescanning.Text = SearchProduct.prodcode.Substring(0, 2) + SearchProduct.prodcode + SearchProduct.qty.Replace(".", "");
            //            txtbarcodescanning.Text = SearchProduct.prodcode + SearchProduct.qty.Replace(".", "");
            //            isusedbarcode = false;
            //            SearchProduct.isUsedSearchForm = false;
            //        }
            //        //SearchProduct.isUsedSearchForm = false; public static bool ispriceused=false,isusedbarcode=false;
            //        //ispriceused = SearchProduct.priceused;
            //        searchprod.Dispose();
            //        txtbarcodescanning.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
            try
            {
                SearchProductItems searchprod = new SearchProductItems();
                searchprod.ShowDialog(this);
                if (SearchProductItems.isUsedSearchForm == true) //isUsedSearchForm indicator ni cya sa searchproduct form kng nigamit ba cya og searchform
                {
                    isusedsearchform = true; //is isusedsearchform is a local variable declare in this class
                    if (SearchProductItems.havebarcode == true) //kng pag select nya kay naay barcode
                    {
                        txtbarcodescanning.Text = SearchProductItems.barcode; // 
                        isusedbarcode = true;
                        SearchProductItems.isUsedSearchForm = false;
                    }
                    else //kung pag select nya sa item sa search product kay wlaay barcode
                    {
                        //txtbarcodescanning.Text = SearchProduct.prodcode.Substring(0, 2) + SearchProduct.prodcode + SearchProduct.qty.Replace(".", "");
                        txtbarcodescanning.Text = SearchProductItems.prodcode;
                        isusedbarcode = false;
                        SearchProductItems.isUsedSearchForm = false;
                    }
                    //SearchProduct.isUsedSearchForm = false; public static bool ispriceused=false,isusedbarcode=false;
                    //ispriceused = SearchProduct.priceused;
                    searchprod.Dispose();
                    txtbarcodescanning.Focus();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        public static async Task PlayNotificationSoundAsync(string soundFilePath)
        {
            // Use Task.Run to offload the sound loading and playback logic to a background thread.
            // This prevents the main UI thread from becoming unresponsive, even if the sound file
            // is large or there are delays in accessing it.
            await Task.Run(() =>
            {
                try
                {
                    // 1. Check if the specified sound file actually exists.
                    if (!File.Exists(soundFilePath))
                    {
                        Console.WriteLine($"Error: Sound file not found at '{soundFilePath}'. Please verify the path.");
                        // Optionally, you could play a default system sound here if the file is missing.
                        // SystemSounds.Exclamation.Play();
                        return; // Exit the method if the file is not found.
                    }

                    // 2. Create a new SoundPlayer instance with the provided file path.
                    // The 'using' statement ensures that the SoundPlayer object is properly
                    // disposed of after it's no longer needed, releasing system resources.
                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(soundFilePath))
                    {
                        // 3. Load the sound into memory. This operation can be synchronous
                        // but since it's inside Task.Run, it won't block the main thread.
                        player.Load();

                        // 4. Play the sound. The Play() method plays the sound asynchronously
                        // on an internal thread managed by SoundPlayer, and returns immediately.
                        player.Play();

                        //Console.WriteLine($"Notification: Playing sound from '{soundFilePath}'");
                    }
                }
                // 5. Implement robust error handling for common issues.
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Error: The sound file was not found at '{soundFilePath}'. Double-check the file path and ensure it's accessible.");
                }
                catch (InvalidOperationException ex)
                {
                    // This typically occurs if the sound file is not a valid .wav format
                    // or if there's an issue with the audio device.
                    Console.WriteLine($"Error playing sound from '{soundFilePath}': {ex.Message}. Ensure the file is a valid .wav format and your audio device is working.");
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected errors during sound playback.
                    Console.WriteLine($"An unexpected error occurred while attempting to play sound from '{soundFilePath}': {ex.Message}");
                }
            });
        }

        private void Commit()
        {
            if (gridView1.RowCount == 0)
            {
                //XtraMessageBox.Show("Nothing to save.");
                BigAlert.Show(
                 "NOTHING TO SAVE",
                 "No items to be transferred",
                 MessageBoxIcon.Warning);
                return;
            }
           
         
            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("dbo.sp_CommitReInventoryIN", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@parmid", SqlDbType.Int).Value = int.Parse(txtid.Text);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    //XtraMessageBox.Show("Inventory successfully transferred.");
                    BigAlert.Show(
                          "SUCCESS",
                          "Inventory successfully transferred!..",
                          MessageBoxIcon.Information);
                    this.Dispose();
                }
                catch (SqlException ex)
                {
                    //XtraMessageBox.Show(ex.Message, "Commit failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BigAlert.Show(
                         "COMMIT FAILED",
                         ex.Message.ToString(),
                         MessageBoxIcon.Error);
                    display(); // show which lines are error/processed
                }
            }
        }

        async void AddEntryNew() //STAGING
        {
            //bool ifExists = Database.checkifExist($"SELECT 1 FROM dbo.Inventory WHERE Branch='{Login.assignedBranch}' AND Barcode='{txtbarcodescanning.Text.Trim()}' ");
            //var rowz = Database.getMultipleQuery($"SELECT TOP(1) Branch,Product,Description,Barcode,Quantity,Available " +
            //    $"FROM dbo.Inventory with(nolock) WHERE Branch='{Login.assignedBranch}' AND Barcode='{txtbarcodescanning.Text.Trim()}'",
            //    "Branch,Product,Description,Barcode,Quantity,Available");
            //string Branch, Product, Description, Barcode, Quantity, Available;
            //Branch = rowz["Branch"].ToString();
            //Product = rowz["Product"].ToString();
            //Description = rowz["Description"].ToString();
            //Barcode = rowz["Barcode"].ToString();
            //Quantity = rowz["Quantity"].ToString();
            //Available = rowz["Available"].ToString();

            //Database.ExecuteQuery("INSERT INTO dbo.TempInventoryIN (ID,Branch,DateReceived,ExpiryDate,Product,Description,Barcode,Quantity,Cost,isWarehouse,isVat,isDone,DateEncode,EncodeBy) " +
            //    "VALUES('" + txtid.Text + "'" +
            //    $",'888'" +
            //    $",'{txtdatereceived.Text}'" +
            //    $",'{txtxpirydate.Text}'" +
            //    $",'{Product}'" +
            //    $",'{Description}'" +
            //    $",'{Barcode}'" +
            //    $",'{Quantity}'" +
            //    $",'{DateTime.Now.ToString()}'" +
            //    $",'{Login.isglobalUserID}')", "Succesfully Added");
            if (string.IsNullOrWhiteSpace(txtbarcodescanning.Text))
            {
                //XtraMessageBox.Show("Please scan or enter a barcode.");
                BigAlert.Show(
                  "BARCODE EMPTY",
                  "Please scan or enter a barcode.",
                  MessageBoxIcon.Warning);
                txtbarcodescanning.Focus();
                return;
            }

         
            using (var con = Database.getConnection()) // assumes your helper returns SqlConnection
            using (var cmd = new SqlCommand("dbo.sp_StageBarcodeForReInventoryIN", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@parmid", SqlDbType.Int).Value = int.Parse(txtid.Text);
                cmd.Parameters.Add("@parmbranch", SqlDbType.VarChar, 5).Value = brcode.ToString();
                cmd.Parameters.Add("@parmbarcode", SqlDbType.VarChar, 120).Value = txtbarcodescanning.Text.Trim();
                cmd.Parameters.Add("@parmuser", SqlDbType.VarChar, 50).Value = Login.isglobalUserID; 

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    display();
                    gridView1.BestFitColumns();
                    //gridView1.Columns["SequenceNo"].Summary.Clear();
                    //gridView1.Columns["SequenceNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "SequenceNo", "{0}");
                    //gridView1.Columns["Qty"].Summary.Clear();
                    //gridView1.Columns["Qty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Qty", "{0}");

                }
                catch (SqlException ex)
                {

                    await PlayNotificationSoundAsync(Application.StartupPath + "\\error.wav");
                    //XtraMessageBox.Show(ex.Message, "Stage failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BigAlert.Show(
                     "STAGE FAILED",
                     ex.Message.ToString(),
                     MessageBoxIcon.Error);
                }
            }
            gridView1.MoveLast();
            txtbarcodescanning.Text = "";
            txtbarcodescanning.Focus();
        }


        //void AddEntry()
        //{
        //    bool validproductcode = false;
        //    string desc = "", pcode = "", barcode = "", qty = "";
        //    double finalqty = 0.0;
        //    barcode = txtbarcodescanning.Text.Trim();
        //    if (isusedbarcode == true)
        //    {
        //        pcode = Database.getSingleQuery("Products", "Barcode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "ProductCode");
        //        desc = Database.getSingleQuery("Products", "Barcode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "Description");
        //        //Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='" + brcode.ToString() + "'", "Description");
        //    }else
        //    {
        //        pcode = Database.getSingleQuery("Products", "ProductCode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "ProductCode");
        //        desc = Database.getSingleQuery("Products", "ProductCode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "Description");
        //        //Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='" + brcode.ToString() + "'", "Description");
        //    }

        //    qty = "1";
        //    validproductcode = Database.checkifExist("SELECT TOP(1) ProductCode FROM dbo.Products WHERE ProductCode='" + pcode + "' And BranchCode='"+ brcode.ToString() + "'");
        //    if (!validproductcode)
        //    {
        //        XtraMessageBox.Show("Invalid Product Code!!..");
        //        return;
        //    }
           
        //    finalqty = Convert.ToDouble(qty);

        //    string prodcatcode = Database.getSingleQuery("Products", "BranchCode='" + brcode.ToString() + "' AND ProductCode='" + pcode + "'", "ProductCategoryCode");
        //    string isvat = Database.getSingleQuery("ProductCategory", "ProductCategoryID='"+prodcatcode+"'", "isVat");
         
        //    Database.ExecuteQuery("INSERT INTO dbo.TempInventoryIN (ID,Branch,DateReceived,ExpiryDate,Product,Description,Barcode,Quantity,Cost,isWarehouse,isVat,isDone,DateEncode,EncodeBy) " +
        //        "VALUES('" + txtid.Text + "'" +
        //        $",'{brcode.ToString()}'" +
        //        $",'{txtdatereceived.Text}'" +
        //        $",'{txtxpirydate.Text}'" +
        //        $",'{pcode}'" +
        //        $",'{desc}'" +
        //        $",'{barcode}'" +
        //        $",'{finalqty}'" +
        //        $",'{txtcost.Text}'" +
        //        $",'0" +
        //        $",'{isvat}'" +
        //        $",'0'" +
        //        $",'{DateTime.Now.ToShortDateString()}'" +
        //        $",'{Login.isglobalUserID}')", "Succesfully Added");

        //    isusedsearchform = false;
        //    display();
        //    gridView1.MoveLast();
        //    txtbarcodescanning.Text = "";
        //    txtbarcodescanning.Focus();
        //}

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            brcode = SearchLookUpClass.getSingleValue(txtbranch, "BranchCode");
        }

        private void Commissary_CheckedChanged(object sender, EventArgs e)
        {
            if (Commissary.Checked == true)
                iswarehouse = true;
            else
                iswarehouse = false;
        }

        private void bigblue_CheckedChanged(object sender, EventArgs e)
        {
            if (bigblue.Checked == true)
                iswarehouse = false;
            else
                iswarehouse = true;
        }
    }
}