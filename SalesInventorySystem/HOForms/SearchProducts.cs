using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class SearchProducts : Form
    {

        public static string prodname = "",prodcode="", prodcatcode = "",barcode="";
        public static bool isdone = false;
        string pcat = "";
        public SearchProducts()
        {
            InitializeComponent();
        }
        public SearchProducts(string prodcat)
        {
            InitializeComponent();
            pcat = prodcat;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == (Keys.Alt | Keys.F))
            {
                textBox1.Focus();
            }

            return functionReturnValue;
        }

        private void SearchProducts_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        void display()
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           // display();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                
                if(HOConversionPOS.isConversion==true && txtbox.Text != "ADDORDER")
                {
                    Database.displayLocalGrid("SELECT TOP(10) ProductCategoryCode,ProductCode ,Description FROM dbo.Products with(nolock) WHERE BranchCode='" + Login.assignedBranch + "' and Description like '%" + textBox1.Text + "%' ORDER BY Description ASC ", dataGridView1);
                    dataGridView1.Focus();
                }
                else if (HOConversionPOS.isConversion == false && txtbox.Text != "ADDORDER")
                {
                    Database.displayLocalGrid("SELECT TOP(10) ProductCode,Description,ProductCategoryCode FROM dbo.Products with(nolock) WHERE BranchCode='" + Login.assignedBranch + "' and Description like '%" + textBox1.Text + "%' ORDER BY Description ASC ", dataGridView1);
                    dataGridView1.Focus();
                }
                if (txtbox.Text == "ADDORDER")
                {
                    
                    Database.displayLocalGrid("SELECT top 50 ProductCategoryCode as Code,ProductCode as Product,Description,Barcode " +
                        "FROM Products WHERE BranchCode='"+Login.assignedBranch+"' " +
                        "and (Description like '%" + textBox1.Text + "%' OR Barcode like '%" + textBox1.Text + "%' ) " +
                        "ORDER BY Description ASC ", dataGridView1);
                    dataGridView1.Focus();
                }

            }
            if (e.KeyCode == Keys.Down)
            {
                dataGridView1.Focus();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    prodname = dataGridView1.Rows[cord].Cells["Description"].Value.ToString();
                    prodcode = dataGridView1.Rows[cord].Cells["ProductCode"].Value.ToString();
                    prodcatcode = dataGridView1.Rows[cord].Cells["ProductCategoryCode"].Value.ToString();
                    barcode = "";
                    isdone = true;
                    this.Close();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int cord = dataGridView1.CurrentCellAddress.Y;
            //prodname = dataGridView1.Rows[cord].Cells["Description"].Value.ToString();
            //prodcode = dataGridView1.Rows[cord].Cells["Product"].Value.ToString();
            //isdone = true;
            //this.Close();
        }

      
    }
}
