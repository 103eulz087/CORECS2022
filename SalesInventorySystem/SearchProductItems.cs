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

namespace SalesInventorySystem
{
    public partial class SearchProductItems : DevExpress.XtraEditors.XtraForm
    {
        public static string prodcode, prodname, unitprice, barcode = "", priceused = "";
        public static string pname, uprice, qtysold, totamount;
        public static string qty = "0";
        public static bool havebarcode = true;
        public static bool isUsedSearchForm = false;

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
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
               
                dataGridView1.CurrentCell = dataGridView1[col, row];
                e.Handled = true;
                isUsedSearchForm = true;
                this.Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Database.displayLocalGrid("SELECT ProductCode,Description,Barcode FROM Products WHERE Description like '%" + textBox1.Text + "%' and BranchCode='" + Login.assignedBranch + "' ORDER BY Description", dataGridView1);
                dataGridView1.Focus();
            }
        }
        
        public SearchProductItems()
        {
            InitializeComponent();
            dataGridView1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void SearchProductItems_Load(object sender, EventArgs e)
        {
            display();
        }
        private void display()
        {
            Database.displayLocalGrid("SELECT TOP(100) ProductCode,Description,Barcode FROM Products WHERE BranchCode='" + Login.assignedBranch + "' ORDER BY Description", dataGridView1);
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
                this.Dispose();
            }
            return functionReturnValue;
        }
       
    }
}