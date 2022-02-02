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

namespace SalesInventorySystem.POS
{
    public partial class POSRefund : Form
    {
        public POSRefund()
        {
            InitializeComponent();
        }

        private void POSRefund_Load(object sender, EventArgs e)
        {
            populateBranches();
        }

        void populateBranches()
        {
            Database.displayComboBoxItems("SELECT BranchCode FROM Branches", "BranchCode", comboBox1);
            Database.displayComboBoxItems("SELECT distinct Description FROM view_XReadGroupReport", "Description", comboBox2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)),BranchCode,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + comboBox1.Text + "' AND Description='" + comboBox2.Text + "' and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' UNION ALL (select CAST(SequenceNumber as varchar(10)),BranchCode,Description,SubTotal,DateOrder FROM (select '' AS SequenceNumber,'' AS BranchCode,'' AS Description,SUM(SubTotal) AS SubTotal,'' AS DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + comboBox1.Text + "' AND Description='" + comboBox2.Text + "' and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD') as dt) ", dataGridView1);
            dataGridView1.Columns[0].Visible = false;
            DataGridViewSettings.gridFooter(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Refund!");
            }
            else
            {
                refund();
            }
        }

        void refund()
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                Database.ExecuteQuery("UPDATE BatchSalesDetails SET isErrorCorrect='1' WHERE CAST(SequenceNumber as varchar(10))='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'");
            }
            XtraMessageBox.Show("Success!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
