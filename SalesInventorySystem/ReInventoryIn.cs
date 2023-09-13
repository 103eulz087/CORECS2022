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

namespace SalesInventorySystem
{
    public partial class ReInventoryIn : DevExpress.XtraEditors.XtraForm
    {
        //DataTable table;
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
            Database.display("SELECT SequenceNumber,Product,Description,Quantity,Cost FROM TempInventoryIN WHERE EncodeBy='" + Login.isglobalUserID + "'", gridControl1, gridView1);
        }

        private void ReInventoryIn_Load(object sender, EventArgs e)
        {
            txtbarcodescanning.Focus();
            Database.displayComboBoxItems("SELECT BranchName FROM Branches WHERE BranchCode='"+Login.assignedBranch+"'", "BranchName", comboBox1);
            string branchname = Database.getSingleQuery("Branches", "BranchCode='" + Login.assignedBranch + "'", "BranchName");
            comboBox1.Text = branchname;
            loadInvNum();
            populateBranch();
        }

        void loadInvNum()
        {
            txtid.Text = IDGenerator.getIDNumberSP("sp_GetInventoryINNumber", "InventoryID"); //IDGenerator.getInventoryINNumber();

        }

        void populateBranch()
        {
            Database.displayComboBoxItems("Select BranchCode,BranchName FROM Branches", "BranchName", comboBox1);
        }

     

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(comboBox1.Text))
            {
                XtraMessageBox.Show("Branch must not empty");
                return;
            }
            else
            { AddEntry(); }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "HEAD OFFICE")
            {
                panel1.Visible = true;
                iswarehouse = true;
            }
            else
            {
                panel1.Visible = false;
                iswarehouse = true;
            }
        }

        private String getBranchCode()
        {
            return Database.getSingleData("Branches", "BranchName", comboBox1.Text, "BranchCode");
        }

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
            Database.ExecuteQuery("DELETE FROM TempInventoryIN WHERE SequenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "'");
            display();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ReInventoryINRecovery revin = new ReInventoryINRecovery();
            revin.ShowDialog(this);
            if(ReInventoryINRecovery.isdone == true)
            {
                bool checkfirst = Database.checkifExist("SELECT ID FROM TempInventoryIN WHERE ID = '" + ReInventoryINRecovery.id + "'");
                if(checkfirst)
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

        private void simpleButton2_Click(object sender, EventArgs e)
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
                    branchcode = Branch.getBranchCode(comboBox1.Text);
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
                com.Parameters.AddWithValue("@parmbranchcode", Branch.getBranchCode(comboBox1.Text));
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


        void AddEntry()
        {
            bool validproductcode = false;
            string desc = "", pcode = "", barcode = "", qty = "";
            double finalqty = 0.0;
            barcode = txtbarcodescanning.Text.Trim();
            if (isusedbarcode == true)
            {
                pcode = Database.getSingleQuery("Products", "Barcode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "ProductCode");
                desc = Database.getSingleQuery("Products", "Barcode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "Description");
                //Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "'", "Description");
            }else
            {
                pcode = Database.getSingleQuery("Products", "ProductCode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "ProductCode");
                desc = Database.getSingleQuery("Products", "ProductCode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "Description");
                //Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "'", "Description");
            }

            qty = "1";
            validproductcode = Database.checkifExist("SELECT ProductCode FROM Products WHERE ProductCode='" + pcode + "'");
            if (!validproductcode)
            {
                XtraMessageBox.Show("Invalid Product Code!!..");
                return;
            }
            //if (isusedsearchform == true)
            //{
            //    //pcode = txtbarcodescanning.Text.Substring(2, 5).Trim();
            //    pcode = txtbarcodescanning.Text.Trim();
            //    qty = "1";// SearchProduct.qty; //"1";
            //}
            //else if (isusedsearchform == false) //Barcode field ang gigamit
            //{
            //    if(txtbarcodescanning.Text.Length < 19)
            //    { 
            //        barcode = txtbarcodescanning.Text.Trim();
            //        pcode = Database.getSingleQuery("Products", "Barcode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "ProductCode");
            //        desc = Database.getSingleQuery("Products", "Barcode='" + txtbarcodescanning.Text + "' and BranchCode='" + Login.assignedBranch + "'", "Description");
            //        //Database.getSingleQuery("Products", "ProductCode='" + pcode + "' and BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "'", "Description");

            //        qty = "1";
            //    }
            //    //CUSTOMIZED BARCODE -- NO STANDARD BARCODE IS EQUAL TO 19 FIGURES
            //    //else if (txtbarcodescanning.Text.Length == 19) //tens 11111 10015 10123 0001 --10.123 kilos
            //    //{
            //    //    pcode = txtbarcodescanning.Text.Substring(5, 5);
            //    //    qty1 = txtbarcodescanning.Text.Substring(10, 2); //1001512345
            //    //    qty2 = txtbarcodescanning.Text.Substring(12, 3);
            //    //    qty = qty1 + "." + qty2;
            //    //    barcode = txtbarcodescanning.Text.Trim();
            //    //}
            //    //else if (txtbarcodescanning.Text.Length == 20) //hundred 11111 10015 100123 0001 --100.123 kilos
            //    //{
            //    //    pcode = txtbarcodescanning.Text.Substring(5, 5);
            //    //    qty1 = txtbarcodescanning.Text.Substring(10, 3); //10015100345
            //    //    qty2 = txtbarcodescanning.Text.Substring(13, 3);
            //    //    qty = qty1 + "." + qty2;
            //    //    barcode = txtbarcodescanning.Text.Trim();
            //    //}
            //    //else if (txtbarcodescanning.Text.Length == 21) //thousand  11111 10015 1000123 0001 --1000.123 kilos
            //    //{
            //    //    pcode = txtbarcodescanning.Text.Substring(5, 5);
            //    //    qty1 = txtbarcodescanning.Text.Substring(10, 4); //10015100345
            //    //    qty2 = txtbarcodescanning.Text.Substring(14, 3);
            //    //    qty = qty1 + "." + qty2;
            //    //    barcode = txtbarcodescanning.Text.Trim();
            //    //}
            //    validproductcode = Database.checkifExist("SELECT ProductCode FROM Products WHERE ProductCode='" + pcode + "'");
            //    if(!validproductcode)
            //    {
            //        XtraMessageBox.Show("Invalid Product Code!!..");
            //        return;
            //    }
            //}
            finalqty = Convert.ToDouble(qty);

            string prodcatcode = Database.getSingleQuery("Products", "BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' AND ProductCode='" + pcode + "'", "ProductCategoryCode");
            string isvat = Database.getSingleQuery("ProductCategory", "ProductCategoryID='"+prodcatcode+"'", "isVat");
            Database.ExecuteQuery("INSERT INTO TempInventoryIN (ID" +
                        ",Branch" +
                        ",DateReceived" +
                        ",Product" +
                        ",Description" +
                        ",Barcode" +
                        ",Quantity" +
                        ",Cost" +
                        ",isWarehouse" +
                        ",isVat" +
                        ",DateEncode" +
                        ",EncodeBy) " +
                "VALUES('" + txtid.Text + "'" +
                        ",'" + Branch.getBranchCode(comboBox1.Text) + "'" +
                        ",'" + txtdatereceived.Text + "'" +
                        ",'" + pcode + "'" +
                        ",'" + desc + "'" +
                        ",'" + barcode + "'" +
                        ",'" + finalqty + "'" +
                        ",'0'" +
                        ",'"+iswarehouse+"'" +
                        ",'"+ isvat + "'" +
                        ",'" + DateTime.Now.ToShortDateString() + "'" +
                        ",'" + Login.isglobalUserID + "')");
            isusedsearchform = false;
            display();
            gridView1.MoveLast();
            txtbarcodescanning.Text = "";
            txtbarcodescanning.Focus();
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