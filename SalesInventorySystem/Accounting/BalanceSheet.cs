using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Accounting
{
    public partial class BalanceSheet : Form
    {
        public BalanceSheet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                populateRows();
                lblnetincome.Text = String.Format("{0:0,0.##}", OverallTotal());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
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

        void populateRows()
        {
            try
            {
                Database.display("SELECT * FROM view_BalanceSheet WHERE BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + dateTimePicker2.Text + "' AND EndingBalance <> '0' AND AccountCode < '4' ORDER BY AccountCode ASC", gridControl1, gridView1);
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[1].Visible = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void BalanceSheet_Load(object sender, EventArgs e)
        {
            populateComboBox();
        }

        Double computeIncome()
        {
            double totaldebits = 0.0;
            totaldebits = Database.getTotalSummation2("view_BalanceSheet", "BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + dateTimePicker2.Text + "' AND AccountCode='4' ", "EndingBalance");
            return totaldebits;
        }

        Double computeExpense()
        {
            double totalcredits = 0.0;
            totalcredits = Database.getTotalSummation2("view_BalanceSheet", "BranchCode='" + txtbrcode.Text + "' AND PostingDate='" + dateTimePicker2.Text + "' AND AccountCode='5' ", "EndingBalance");
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

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["AccountCode"]);
                if (category.Length == 1)
                {
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }
    }
}
