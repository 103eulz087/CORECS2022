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

namespace SalesInventorySystem
{
    public partial class ProductCategory : Form
    {
        public ProductCategory()
        {
            InitializeComponent();
        }

        private void ProductCategory_Load(object sender, EventArgs e)
        {
           
            display();
        }
        private void display()
        {
          
            Database.displayLocalGrid("SELECT * FROM ProductCategory", dataGridView1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtcattype.Focus();
            HelperFunction.EnableTextFields(this);
            txtcatid.Text = IDGenerator.getIDNumber("ProductCategory", "ProductCategoryID",10).ToString();
            checkBox1.Enabled = true;
            simpleButton1.Enabled = false;
            addbtn.Enabled = true;
            updatebtn.Enabled = false;
            btncancel.Enabled = true;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            HelperFunction.DisableTextFields(this);
            txtcatid.Text = "";
            txtcattype.Text = "";
            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            string isvatable = "";
            if (checkBox1.Checked == true)
            {
                isvatable = "1";
            }
            else
            {
                isvatable = "0";
            }
            Database.ExecuteQuery("INSERT INTO ProductCategory VALUES('" + txtcatid.Text + "','" + txtcattype.Text + "','" + isvatable + "')", "Successfully Added!");
            display();
            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;

            txtcatid.Text = "";
            txtcattype.Text = "";
            HelperFunction.DisableTextFields(this);
            checkBox1.Enabled = false;
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            HelperFunction.EnableTextFields(this);
            int cord = dataGridView1.CurrentCellAddress.Y;
            checkBox1.Enabled = true;
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[2].Value.ToString()) == true)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
           
            txtcatid.Enabled = false;
            txtcatid.Text = dataGridView1.Rows[cord].Cells[0].Value.ToString();
            txtcattype.Text = dataGridView1.Rows[cord].Cells[1].Value.ToString();
            simpleButton1.Enabled = false;
            addbtn.Enabled = false;
            updatebtn.Enabled = true;
            btncancel.Enabled = true;
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            
            if (HelperFunction.isTextBoxEmpty(txtcatid, txtcattype))
            {
                XtraMessageBox.Show("Please Input Valid Fields");
            }
            else
            {
                string isvatable = "";
                if (checkBox1.Checked == true)
                {
                    isvatable = "1";
                }
                else
                {
                    isvatable = "0";
                }
                Database.ExecuteQuery("UPDATE ProductCategory SET Description='"+txtcattype.Text+"',isVat='"+isvatable+"' WHERE ProductCategoryID='"+txtcatid.Text+"'","Successfully Updated!");
                display();
                txtcattype.Enabled = false;
                simpleButton1.Enabled = true;
                addbtn.Enabled = false;
                updatebtn.Enabled = false;
                btncancel.Enabled = false;
            }
        }

        private void deleteCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
             bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Supplier");
             if (ok)
             {
                 Database.ExecuteQuery("DELETE FROM ProductCategory WHERE ProductCategoryID='" + dataGridView1.Rows[cord].Cells[0].Value.ToString() + "'", "Successfully Deleted");
                 display();
             }
        }
    }
}
