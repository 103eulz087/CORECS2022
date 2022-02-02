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

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmFoodMenu : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmFoodMenu()
        {
            InitializeComponent();
        }

        private void HotelFrmFoodMenu_Load(object sender, EventArgs e)
        {
            HelperFunction.DisableTextFields(this);
            populate();
            display();
        }
        void display()
        {
            Database.display("SELECT * FROM FoodMenu", gridControl1, gridView1, Database.getCustomizeConnection());
        }
        void populate()
        {
            Database.displayComboBoxItems("SELECT CategoryName FROM FoodCategory", "CategoryName", txtprodcat,Database.getCustomizeConnection());
        }
        String getProductCategoryCode()
        {
            string str = "";
            str = Database.getSingleQuery("FoodCategory", "CategoryName='" + txtprodcat.Text + "'", "SequenceNo", Database.getCustomizeConnection());
            return str; //return the sequenceno
        }

        int getLastProductCode()
        {
            int str;
            str = Database.getLastID("FoodMenu", "MenuCategory='" + txtprodcat.Text + "'", "FoodCode",Database.getCustomizeConnection());
            return str;
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newid = 0;
            string createID = getProductCategoryCode() + "000"; //2 + '0000' 20000
            if (getLastProductCode() != 0)
            {
                newid = getLastProductCode() + 1; //2+1 = 3
            }
            else
            {
                newid = Convert.ToInt32(createID);
            }
            txtprodcode.Text = newid.ToString();
            txtprodcode.ReadOnly = true;
            txtdesc.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HelperFunction.EnableTextFields(this);
            txtprodcode.ReadOnly = false;
            txtprodcat.Enabled = true;
            txtprodcat.Focus();

            simpleButton1.Enabled = false;
            addbtn.Enabled = true;
            updatebtn.Enabled = false;
            btncancel.Enabled = true;
        }
     
        private void addbtn_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (HelperFunction.isTextBoxEmpty(txtdesc, txtprodcode, txtsellingprice, txtlandingcost))
                {
                    XtraMessageBox.Show("Please Input All Fields");
                }
                else
                {
                    //brcode = Branch.getBranchCode(txtbranch.Text);
                    bool checkifexist = Database.checkifExist("SELECT * FROM FoodMenu WHERE FoodCode='" + txtprodcode.Text + "' ",Database.getCustomizeConnection());
                    if (checkifexist)
                    {
                        XtraMessageBox.Show("Product Code Already Exist");
                    }
                    else
                    {
                       
                        string prodcode = txtprodcode.Text;
                        Database.ExecuteQuery("INSERT INTO FoodMenu VALUES('" + txtprodcode.Text + "','" + txtdesc.Text + "','"+txtprodcat.Text+"','" + txtlandingcost.Text + "','" + txtsellingprice.Text + "','1')","Successfully Added",Database.getCustomizeConnection());
                        XtraMessageBox.Show("Successfully Added!");
                        HelperFunction.ClearAllText(this);
                        HelperFunction.DisableTextFields(this);

                        txtprodcat.Text = "";
                        //txtbranch.Text = "";
                        txtprodcat.Enabled = false;
                        //txtbranch.Enabled = false;
                       
                        simpleButton1.Enabled = true;
                        addbtn.Enabled = false;
                        updatebtn.Enabled = false;
                        btncancel.Enabled = false;
                        display();
                    }
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                HelperFunction.ClearAllText(this);
                HelperFunction.EnableTextFields(this);
                txtprodcode.Enabled = false;
                txtprodcat.Enabled = true;
                
                txtprodcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FoodCode").ToString();// dataGridView1.Rows[cord].Cells[1].Value.ToString();
                txtdesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FoodMenu").ToString(); //dataGridView1.Rows[cord].Cells[2].Value.ToString();
                txtlandingcost.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString(); //dataGridView1.Rows[cord].Cells[3].Value.ToString();
                txtsellingprice.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Price").ToString(); // dataGridView1.Rows[cord].Cells[4].Value.ToString();
                txtprodcat.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MenuCategory").ToString();//  dataGridView1.Rows[cord].Cells[5].Value.ToString();
               
                simpleButton1.Enabled = false;
                addbtn.Enabled = false;
                updatebtn.Enabled = true;
                btncancel.Enabled = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            try
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Products");
                if (ok)
                {
                    //Database.ExecuteQuery("DELETE FROM Products WHERE ProductCode='" + dataGridView1.Rows[cord].Cells[1].Value.ToString() + "'", "Successfully Deleted");
                    Database.ExecuteQuery("DELETE FROM FoodMenu WHERE FoodCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FoodCode").ToString() + "'", "Successfully Deleted",Database.getCustomizeConnection());
                    display();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            //parmoption = "UPDATE";
           
            try
            {
                if (HelperFunction.isTextBoxEmpty( txtdesc, txtprodcode, txtsellingprice, txtlandingcost))
                {
                    XtraMessageBox.Show("Please Supply All Fields");
                }
                else
                {
                    Database.ExecuteQuery("UPDATE FoodMenu SET FoodMenu='"+txtdesc.Text+"',Cost='"+txtlandingcost.Text+"',Price='"+txtsellingprice.Text+"' WHERE FoodCode='"+txtprodcode.Text+"'","Successfully Updated!",Database.getCustomizeConnection());

                    HelperFunction.ClearAllText(this);
                    HelperFunction.DisableTextFields(this);
                    txtprodcat.Text = "";
                    txtprodcat.Enabled = false;
                  
                    display();
                    simpleButton1.Enabled = true;
                    addbtn.Enabled = false;
                    updatebtn.Enabled = false;
                    btncancel.Enabled = false;
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            HelperFunction.DisableTextFields(this);
            txtprodcat.Enabled = false;
            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
        }
    }
}