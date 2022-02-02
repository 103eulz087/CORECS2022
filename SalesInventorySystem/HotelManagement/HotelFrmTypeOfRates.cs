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

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmTypeOfRates : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmTypeOfRates()
        {
            InitializeComponent();
        }

        private void HotelFrmTypeOfRates_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            Database.display("SELECT * FROM Rates", gridControl1, gridView1, Database.getCustomizeConnection());
        }

        void add()
        {
            bool ishalfday = false, iswholeday = false;
            if(rad12hrs.Checked==true)
            {
                ishalfday = true;
            }else if (rad24hrs.Checked == true)
            {
                iswholeday = true;
            }
            Database.ExecuteQuery("INSERT INTO Rates VALUES('" + textBox1.Text + "',0,'"+ ishalfday + "','"+ iswholeday + "')", "Successfully Added!",Database.getCustomizeConnection());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox1.Text))
            {
                XtraMessageBox.Show("Please input fields");
                return;
            }
            else
            {
                add();
                textBox1.Text = "";
                display();
            }
        }
    }
}