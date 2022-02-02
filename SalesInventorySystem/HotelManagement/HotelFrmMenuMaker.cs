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

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmMenuMaker : DevExpress.XtraEditors.XtraForm
    {
        object foodcode = null;
        DataTable table;
        public HotelFrmMenuMaker()
        {
            InitializeComponent();
        }

        private void HotelFrmMenuMaker_Load(object sender, EventArgs e)
        {
            populate();
            disablefields();
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
            loadgridview1();
        }
        void clear()
        {
            txtfood.Text = "";
            txtingredients.Text = "";
            txtqty.Text = "";
        }

        void disablefields()
        {
            txtfood.Enabled = false;
            txtingredients.Enabled = false;
            txtqty.Enabled = false;

        }
        void enablefields()
        {
            txtfood.Enabled = true;
            txtingredients.Enabled = true;
            txtqty.Enabled = true;
        }
        void populate()
        {
            Database.displaySearchlookupEdit("SELECT FoodCode,FoodMenu,MenuCategory FROM FoodMenu", txtfood, "FoodMenu", "FoodMenu", Database.getCustomizeConnection());
            Database.displaySearchlookupEdit("SELECT ProductCode,Description FROM Products WHERE BranchCode='"+Login.assignedBranch+"'", txtingredients, "Description", "Description");

        }
        void loadgridview1()
        {
            table = new DataTable();
            table.Columns.Add("FoodCode");
            table.Columns.Add("FoodName");
            table.Columns.Add("Ingredients"); //UnitPrice
            table.Columns.Add("Qty"); //UnitPrice
            dataGridView1.DataSource = table;
        }
        private void addbtn_Click(object sender, EventArgs e)
        {
           
            if (txtfood.Text == "" || txtingredients.Text == "" || txtqty.Text == "" )
            {
                XtraMessageBox.Show("Fields must not Empty..");
            }
           
            else
            {
                add();
                txtingredients.Text = "";
                txtfood.Enabled = false;
                txtingredients.Focus();
            }
        }
        void add()
        {
            DataRow newRow = table.NewRow();
            newRow["FoodCode"] = foodcode;
            newRow["FoodName"] = txtfood.Text;
            newRow["Ingredients"] = txtingredients.Text;
            newRow["Qty"] = txtqty.Text;
            table.Rows.Add(newRow);
            dataGridView1.DataSource = table;
        }

        private void txtfood_EditValueChanged(object sender, EventArgs e)
        {
            foodcode = "";
            GridView view = txtfood.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            foodcode = view.GetRowCellValue(rowHandle, "FoodCode");
            txtingredients.Focus();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            for (int i=0;i<=dataGridView1.RowCount-1;i++)
            {
                Database.ExecuteQuery2("INSERT INTO FoodMenuMaker VALUES ('" + dataGridView1.Rows[i].Cells["FoodCode"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["FoodName"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Ingredients"].Value.ToString() + "','" + dataGridView1.Rows[i].Cells["Qty"].Value.ToString() + "')", Database.getCustomizeConnection());
            }
            XtraMessageBox.Show("Successfully Added!");
            this.Dispose();
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                addbtn.PerformClick();
        }

        private void txtingredients_EditValueChanged(object sender, EventArgs e)
        {
            txtqty.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            enablefields();
            simpleButton1.Enabled = false;
            addbtn.Enabled = true;
            updatebtn.Enabled = true;
            btncancel.Enabled = true;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            disablefields();

            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
        }
    }
}