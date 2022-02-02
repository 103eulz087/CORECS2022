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

namespace SalesInventorySystem
{
    public partial class TestTestTest : DevExpress.XtraEditors.XtraForm
    {
        public TestTestTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtrefno.Text) || String.IsNullOrEmpty(txtmname.Text) || String.IsNullOrEmpty(txtloanamount.Text) || String.IsNullOrEmpty(txtlname.Text) || String.IsNullOrEmpty(txtfname.Text) || String.IsNullOrEmpty(txtcontactno.Text) || String.IsNullOrEmpty(txtaddress.Text))
            {
                XtraMessageBox.Show("All Fields are mandatory!");
                return;
            }
            else
            {
                add();
            }
               
        }

        void add()
        {
            Database.ExecuteQuery("INSERT INTO LoanApplication VALUES ('"+txtrefno.Text+"','" + txtfname.Text + "','" + txtmname.Text + "','" + txtlname.Text + "','" + txtaddress.Text + "','" + txtloanamount.Text + "','" + txtcontactno.Text + "','FORAPPROVAL','" + DateTime.Now.ToShortDateString() + "','','','','0')", "Successfully Added!");
            clear();
            updateLoanReferenceNo();
        }
        void clear()
        {
            txtrefno.Text = "";
            txtaddress.Text = "";
            txtcontactno.Text = "";
            txtfname.Text = "";
            txtlname.Text = "";
            txtloanamount.Text = "";
            txtmname.Text = "";
        }

        void display()
        {
            Database.display("Select * FROM LoanApplication WHERE Status='FORAPPROVAL'", gridControl1, gridView1);
            Database.display("Select * FROM LoanApplication WHERE Status='APPROVED'", gridControl2, gridView2);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display();
        }

        private void TestTestTest_Load(object sender, EventArgs e)
        {
            updateLoanReferenceNo();
            display();
        }

        void updateLoanReferenceNo()
        {
            int id = 0;
            int refnumber = Database.getLastID("LoanApplication", "LoanID <> ''", "LoanReferenceNo");
            if (refnumber != 0)
            {
                id = refnumber + 1;
            }
            else
            {
                id = 1;
            }
            txtrefno.Text = HelperFunction.sequencePadding(id.ToString());
        }
    }
}