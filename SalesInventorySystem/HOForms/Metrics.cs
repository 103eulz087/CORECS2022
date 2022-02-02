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

namespace SalesInventorySystem.HOForms
{
    public partial class Metrics : Form
    {
        public Metrics()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                XtraMessageBox.Show("Please filled out the form correctly");
            }
            else
            {
                add();
            }
        }

        void add()
        {
            Database.ExecuteQuery("INSERT INTO Metrics VALUES('" + textBox1.Text.Trim() + "')", "Successfully Added");
        }

        void display()
        {
            Database.displayLocalGrid("SELECT * FROM Metrics", dataGridView1);
        }

        private void Metrics_Load(object sender, EventArgs e)
        {
            display();
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
            Database.ExecuteQuery("DELETE FROM Metrics WHERE Metrics='"+dataGridView1.Rows[cord].Cells[0].Value.ToString()+"'","Successfully Deleted");
        }
    }
}
