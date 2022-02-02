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
    public partial class SearchAccountCode : Form
    {
        public static string acctcode, acctdesc;
        public SearchAccountCode()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }
        private void display()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "SELECT * FROM view_GLAccounts WHERE Description like '%" + textBox1.Text + "%' ";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            con.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            acctcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountCode").ToString();
            acctdesc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                display();
            }
        }
        
    }
}
