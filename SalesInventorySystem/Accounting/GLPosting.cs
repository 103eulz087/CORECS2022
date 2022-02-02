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

namespace SalesInventorySystem.Accounting
{
    public partial class GLPosting : Form
    {
        //bool result;
        public GLPosting()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbranch.Text = Branch.getBranchCode(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "GLPosting";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@Branch", txtbranch.Text);
                com.Parameters.AddWithValue("@PPostDate", dateTimePicker1.Text);
                com.Parameters.AddWithValue("@PSupplementary", textBox4.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Succesfully Posted!");
                this.Close();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void displayBranchItems()
        {
            Database.displayComboBoxItems("Select * FROM Branches", "BranchName", comboBox1);
        }
        private void GLPosting_Load(object sender, EventArgs e)
        {
            displayBranchItems();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
