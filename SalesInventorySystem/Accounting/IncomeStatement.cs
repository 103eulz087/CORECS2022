using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class IncomeStatement : Form
    {
        public IncomeStatement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(chckasofdate.Checked==false)
                {
                    if(Convert.ToDateTime(dateTimePicker2.Text) < Convert.ToDateTime(datecutoff.Text))
                    {
                        XtraMessageBox.Show("Date must not less than Cutoff Date!...");
                        return;
                    }
                }
                execute();
                lblnetincome.Text = String.Format("{0:0,0.##}", OverallTotal());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void IncomeStatement_Load(object sender, EventArgs e)
        {
            populateComboBox();
        }

        void populateComboBox()
        {
            try
            {
                Database.displayComboBoxItems("SELECT * FROM Branches", "BranchCode", txtbrcode);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void execute()
        {
            bool isasofdate = true;
            if(chckasofdate.Checked==true)
            {
                isasofdate = true;
            }
            else
            {
                isasofdate = false;
            }
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_IncomeStatement";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode",txtbrcode.Text);
            com.Parameters.AddWithValue("@parmdate", dateTimePicker2.Text);
            com.Parameters.AddWithValue("@parmcutoffdate", datecutoff.Text);
            com.Parameters.AddWithValue("@parmisasofdate", isasofdate);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            adapter.Fill(table);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
            con.Close();
        }
        //void populateRows()
        //{
        //    try
        //    {
        //        Database.display("SELECT * FROM view_IncomeStatement WHERE BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + dateTimePicker2.Text + "' AND EndingBalance <> '0' AND AccountCode >= '4' AND AccountCode < '7' ORDER BY AccountCode ASC", gridControl1, gridView1);
        //        gridView1.Columns[0].Visible = false;
        //        gridView1.Columns[1].Visible = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message.ToString());
        //    }
        //}

        Double computeIncome()
        {
            double totaldebits = 0.0;
            totaldebits = Database.getTotalSummation2("view_IncomeStatement", "BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + dateTimePicker2.Text + "' AND AccountCode='4' ", "EndingBalance");
            return totaldebits;
        }

        Double computeExpense()
        {
            double totalcredits = 0.0;
            totalcredits = Database.getTotalSummation2("view_IncomeStatement", "BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + dateTimePicker2.Text + "' AND AccountCode='5' ", "EndingBalance");
            return totalcredits;
        }

        Double OverallTotal()
        {
            double total = 0.0;
            total = Math.Abs(computeIncome()) - computeExpense();
            return total;
        }

        private void txtbrcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblbranchname.Text = Branch.getBranchName(txtbrcode.Text);
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category =  view.GetRowCellDisplayText(e.RowHandle, view.Columns["AccountCode"]);
                if (category.Length == 1)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }

        }

        private void chckasofdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chckasofdate.Checked == true)
                datecutoff.Enabled = false;
            else
                datecutoff.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\";
            string filename = "INCOMESTATEMENT" + "_" + txtbrcode.Text + '-' + dateTimePicker2.Text.Replace('/', '-') + ".xls";
            string file = filepath + filename;
            gridView1.ExportToXls(file);
            XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
        }
    }
}
