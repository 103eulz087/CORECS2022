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

namespace SalesInventorySystem
{
    public partial class SearchProduct : DevExpress.XtraEditors.XtraForm
    {
        public static string prodcode,prodname, unitprice,barcode="",priceused="";
        public static string pname, uprice, qtysold, totamount;
        public static string qty = "0";
        public static bool havebarcode=true;
        public static bool isUsedSearchForm = false,isdone=false;
        
        public SearchProduct()
        {
            InitializeComponent();
            dataGridView1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int col = dataGridView1.CurrentCell.ColumnIndex;
            int row = dataGridView1.CurrentCell.RowIndex;

            int cord = dataGridView1.CurrentCellAddress.Y;
            prodcode = dataGridView1.Rows[cord].Cells["ProductCode"].Value.ToString();
            barcode = dataGridView1.Rows[cord].Cells["Barcode"].Value.ToString();
            prodname = dataGridView1.Rows[cord].Cells["Description"].Value.ToString();
            textBox1.Text = prodname;

            if (dataGridView1.Rows[cord].Cells["Barcode"].Value == System.DBNull.Value || String.IsNullOrEmpty(barcode) || barcode == "")
            {
                havebarcode = false; //no barcode
            }
            //if()
            //{
            //    havebarcode = true; //there is an existing barcode during selecting data
            //}
            dataGridView1.CurrentCell = dataGridView1[col, row];
           
        }

        private void SearchProduct_Load(object sender, EventArgs e)
        {
            //display();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Down)
            {
                dataGridView1.Focus();
            }
            else if (keyData == (Keys.F | Keys.Control))
            {
                textBox1.Focus();
            }
            else if (keyData == Keys.Escape)
            {
                isUsedSearchForm = false;
                this.Dispose();
            }
            return functionReturnValue;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int col = dataGridView1.CurrentCell.ColumnIndex;
                int row = dataGridView1.CurrentCell.RowIndex;
                
                int cord = dataGridView1.CurrentCellAddress.Y;
                prodcode = dataGridView1.Rows[cord].Cells["ProductCode"].Value.ToString();
                //barcode = dataGridView1.Rows[cord].Cells["Barcode"].Value.ToString();
                prodname = dataGridView1.Rows[cord].Cells["Description"].Value.ToString();
                textBox1.Text = prodname;

                //if (dataGridView1.Rows[cord].Cells["Barcode"].Value == System.DBNull.Value || String.IsNullOrEmpty(barcode) || barcode == "")
                //{
                //    havebarcode = false; //no barcode
                //}
                
                dataGridView1.CurrentCell = dataGridView1[col, row];
                e.Handled = true;
             
                POS.POSAddQty posadd = new POS.POSAddQty();
                posadd.ShowDialog(this);
                if(POS.POSAddQty.isdone==true)
                {
                    priceused = usedPrice();
                    qty = POS.POSAddQty.strqty;
                    isUsedSearchForm = true;
                    isdone = true;
                    POS.POSAddQty.isdone = false;
                    this.Close();
                }
                else
                {
                    return;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Database.displayLocalGrid($"SELECT * FROM dbo.funcview_POSSearchProduct('{Login.assignedBranch}','{textBox1.Text}')", dataGridView1);
                dataGridView1.Focus();
            }
        }

        String usedPrice()
        {
            string str = "";
            if (radioButton1.Checked == true)
            {
                str = "mainprice";
                unitprice = Database.getSingleQuery("Products", $"BranchCode='{Login.assignedBranch}' AND ProductCode='{prodcode}'", "SellingPrice");
            }
            else if (radioButton2.Checked == true)
            {   str = "price1";
                unitprice = Database.getSingleQuery("Products", $"BranchCode='{Login.assignedBranch}' AND ProductCode='{prodcode}'", "Price1");
            }
            else if (radioButton3.Checked == true)
            {   str = "price2";
                unitprice = Database.getSingleQuery("Products", $"BranchCode='{Login.assignedBranch}' AND ProductCode='{prodcode}'", "Price2");
            }
            else if (radioButton4.Checked == true)
            {  str = "price3";
                unitprice = Database.getSingleQuery("Products", $"BranchCode='{Login.assignedBranch}' AND ProductCode='{prodcode}'", "Price3");
            }
            else if (radioButton5.Checked == true)
            {   str = "price4";
                unitprice = Database.getSingleQuery("Products", $"BranchCode='{Login.assignedBranch}' AND ProductCode='{prodcode}'", "Price4");
            }
            else
                str = "mainprice";
            return str;
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                priceused = usedPrice();
                qty = txtqty.Text.Trim();
                isUsedSearchForm = true;
                isdone = true;
                this.Close();
            }
        }

       

        
    }
}