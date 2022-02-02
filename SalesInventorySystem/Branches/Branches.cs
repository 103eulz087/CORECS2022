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

namespace SalesInventorySystem.Branches
{
    public partial class Branches : Form
    {
        public Branches()
        {
            InitializeComponent();
        }

        private void Branches_Load(object sender, EventArgs e)
        {
            display();
            HelperFunction.DisableTextFields(this);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HelperFunction.EnableTextFields(this);
            display();
            simpleButton1.Enabled = false;
            addbtn.Enabled = true;
            updatebtn.Enabled = false;
            btncancel.Enabled = true;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            if (HelperFunction.isTextBoxEmpty(txtbrcode, txtbrname, txtaddress, txtcashier, txtmanager))
            {
                XtraMessageBox.Show("Please Input All Fields");
            }
            else
            {
                add();
                HelperFunction.ClearAllText(this);
                HelperFunction.DisableTextFields(this);
                simpleButton1.Enabled = true;
                addbtn.Enabled = false;
                updatebtn.Enabled = false;
                btncancel.Enabled = false;
                display();
            }
        }

        void add()
        {
            try
            {
                Database.ExecuteQuery("insert into Branches Values('" + txtbrcode.Text + "','" + txtbrname.Text + "','" + txtaddress.Text + "','" + txtmanager.Text + "','" + txtcashier.Text + "')", "Successfully Added!");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void display()
        {
            Database.displayLocalGrid("SELECT * FROm view_Branches",dataGridView1);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            HelperFunction.DisableTextFields(this);
            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
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
            txtbrcode.Text = dataGridView1.Rows[cord].Cells[0].Value.ToString();
            txtbrname.Text = dataGridView1.Rows[cord].Cells[1].Value.ToString();
            txtaddress.Text = dataGridView1.Rows[cord].Cells[2].Value.ToString();
            txtmanager.Text = dataGridView1.Rows[cord].Cells[3].Value.ToString();
            txtcashier.Text = dataGridView1.Rows[cord].Cells[4].Value.ToString();
            txtbrcode.Enabled = false;
            simpleButton1.Enabled = false;
            addbtn.Enabled = false;
            updatebtn.Enabled = true;
            btncancel.Enabled = true;
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (HelperFunction.isTextBoxEmpty(txtbrcode, txtbrname, txtaddress, txtcashier, txtmanager))
            {
                XtraMessageBox.Show("Please Supply All Fields");
            }
            else
            {
                Database.ExecuteQuery("UPDATE Branches SET BranchName='" + txtbrname.Text + "',Address='" + txtaddress.Text + "',SignatoryManager='" + txtmanager.Text + "',SignatoryCashier='" + txtcashier.Text + "' WHERE BranchCode='" + txtbrcode.Text + "' ", "Successfully Updated!");
                HelperFunction.ClearAllText(this);
                HelperFunction.DisableTextFields(this);
                display();

                simpleButton1.Enabled = true;
                addbtn.Enabled = false;
                updatebtn.Enabled = false;
                btncancel.Enabled = false;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Branch");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Branches WHERE BranchCode='" + dataGridView1.Rows[cord].Cells[0].Value.ToString() + "'", "Successfully Deleted");
                display();
            }
        }
    }
}
