using DevExpress.XtraEditors;
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

namespace SalesInventorySystem.POS
{
    public partial class PointOfSaleManual : Form
    {
        string prodcode = "";
        string prodcatcode = "";
        string quantity = "0";
        string price = "0";
        //string sprice = "";
        public PointOfSaleManual()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F1) //PAYMENT
            {
                btnsearch.PerformClick();
            }else if (keyData == Keys.Enter) //PAYMENT
            {
                simpleButton1.PerformClick();
            }
            return functionReturnValue;
        }

        private void PointOfSaleManual_Load(object sender, EventArgs e)
        {
            loadData();
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches WHERE BranchCode='" + Login.assignedBranch + "' ORDER BY BranchCode", txtbranch, "BranchCode", "BranchCode");

        }
        void loadData()
        {
            if(Login.assignedBranch=="888")
            {
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches ORDER BY BranchCode", txtbranch, "BranchCode", "BranchCode");
            }
            else
            {
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches WHERE BranchCode='" + Login.assignedBranch + "' ORDER BY BranchCode", txtbranch, "BranchCode", "BranchCode");
                txtbranch.Text = Login.assignedBranch;
                txtbranch.ReadOnly = true;
            }
        }

        private void txtsino_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        void display()
        {
            //Database.display("SELECT * FROM SalesTemp WHERE InvoiceDate='" + txteffectivedate.Text + "' and BranchCode='" + Login.assignedBranch + "' and isDone='0'",gridControl1,gridView1);
            Database.display("SELECT * FROM SalesIN " +
                "WHERE SalesDate='" + txteffectivedate.Text + "' " +
                "and BranchCode='" + txtbranch.Text + "' " +
                "and isDone='0' ORDER BY BranchCode,ReferenceNo,SeqNo",gridControl1,gridView1);

            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalAmount");
        }
        void add(int id)
        {
            string isvat = Database.getSingleQuery("ProductCategory", "ProductCategoryID='" + prodcatcode + "'", "isVat");
            double totalamount = 0.00;
            string mark = prodcode;
            string isvatable = "";
            if (isvat=="True")
            {
                isvatable = "1";
            }
            else
            {
                isvatable = "0";
            }
            //Decimal qty = Decimal.Parse(txtqty.Text);
            Decimal qty = Decimal.Parse(quantity);
            string strquantity = String.Format("{0:00.000}", qty);
            totalamount = Math.Round(Convert.ToDouble(quantity) * Convert.ToDouble(price),2); //not used, used manual encoding
            //Database.ExecuteQuery($"INSERT INTO SalesTemp VALUES('{ Login.assignedBranch }'" +
            //    $",'{ txteffectivedate.Text}'" +
            //    $",'{ txtsino.Text }'" +
            //    $",'{ prodcode }'" +
            //    $",'{ txtprodname.Text }'" +
            //    $",'{ strquantity }'" +
            //    $",'{ txtsprice.Text }'" +
            //    $",'{ txtamount.Text }'" +
            //    $",'{ DateTime.Now.ToString() }'" +
            //    $",'{ Login.Fullname } '" +
            //    ",'0' " +
            //    $",'{ isvatable }')");
            Database.ExecuteQuery($"INSERT INTO SalesIN VALUES('{ id }'" +
               $",'{ txtbranch.Text}'" +
               $",'{ txtsino.Text}'" +
               $",'{ prodcode }'" +
               $",'{ txtprodname.Text }'" +
               $",'{ strquantity }'" +
               $",'{ price }'" +
               $",'{ totalamount }'" +
               //$",'{ txtamount.Text }'" +
               $",'{ isvatable }'" +
               $",'{ DateTime.Now.ToString() }'" +
               $",'{ DateTime.Now.ToString() }'" +
               $",'{ Login.isglobalUserID } '" +
               $",'{ Environment.MachineName } '" +
               ",'0' )");
            //txtsino.Text = "";
            //txtqty.Text = "0";
             
            //txtqty.Focus();
            display();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtsino.Text) ||  String.IsNullOrEmpty(txteffectivedate.Text) || String.IsNullOrEmpty(txtprodname.Text) )
            {
                XtraMessageBox.Show("Please Input All Fields");
                return;
            }
            else
            {
                int seqno = 0;
                seqno = IDGenerator.getIDNumber("SalesIN", $"BranchCode='{txtbranch.Text}' AND ReferenceNo='{txtsino.Text}' ", "SeqNo", 1);

                add(seqno);
                txtsino.Focus();
                txtprodname.Text = "";
            }
        }

        private void txteffectivedate_ValueChanged(object sender, EventArgs e)
        {
            
        }
        void cancelLine()
        {
            //Database.ExecuteQuery("DELETE FROM SalesTemp WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "'", "Successfully Deleted!");
            Database.ExecuteQuery("DELETE FROM SalesIN WHERE SeqNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SeqNo").ToString() + "' AND ReferenceNo='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceNo").ToString() + "' AND BranchCode='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString() + "'", "Successfully Deleted!");
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            cancelLine();
            display();
        }
        void executeSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_SalesTemp";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parminvoicedate", txteffectivedate.Text);
                com.Parameters.AddWithValue("@parmaddedby", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            
        }

        void save()
        {
            int count = 0;
            count = gridView1.RowCount;
            if(count==0)
            {
                XtraMessageBox.Show("No Items to be Uploaded");
                return;
            }
            else
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure?", "Upload Data");
                if(confirm)
                {
                    Database.ExecuteQuery("UPDATE SalesIN SET isDone=1 WHERE BranchCode='" + txtbranch.Text + "' AND isDone=0 and SalesDate='"+txteffectivedate.Text+"' ");
                    //for(int i=0;i<=gridView1.RowCount-1;i++)
                    //{
                    //    Database.ExecuteQuery("UPDATE SalesIN SET isDone=1 WHERE SeqNo='" + gridView1.GetRowCellValue(i, "SequenceNo").ToString() + "' ");
                    //}
                    //executeSP();
                    XtraMessageBox.Show("Successfully Saved!");
                    this.Dispose();
                }
                else
                {
                    return;
                }
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            save();
          
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.PerformClick();
            }
        }

        void searchProductItems()
        {
            try
            {
                SearchProduct searchprod = new SearchProduct();
                searchprod.ShowDialog(this);
                if (SearchProduct.isUsedSearchForm == true) //isUsedSearchForm indicator ni cya sa searchproduct form kng nigamit ba cya og searchform
                {
                    quantity = "0";
                    price ="0";
                    prodcode = SearchProduct.prodcode;
                    quantity = SearchProduct.qty;
                    //sprice = SearchProduct.priceused; //mainprice is the default
                    price = SearchProduct.unitprice;

                    var rows = Database.getMultipleQuery("Products", "BranchCode='" + Login.assignedBranch + "' AND ProductCode='" + prodcode + "' ", "ProductCategoryCode,Description");
                    string ProductCategoryCode = rows["ProductCategoryCode"].ToString();
                    string Description = rows["Description"].ToString();

                    prodcatcode = ProductCategoryCode; // Database.getSingleQuery("Products", "BranchCode='" + Login.assignedBranch + "' AND ProductCode='" + prodcode + "' ", "ProductCategoryCode");
                    txtprodname.Text = Description; // Database.getSingleQuery("Products", "BranchCode='" + Login.assignedBranch + "' AND ProductCode='" + prodcode + "' ", "Description");

                    SearchProduct.isUsedSearchForm = false;
                    searchprod.Dispose();
                }
               
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            searchProductItems();
        }
    }
}
