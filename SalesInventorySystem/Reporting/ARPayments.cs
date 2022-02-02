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

namespace SalesInventorySystem.Reporting
{
    public partial class ARPayments : DevExpress.XtraEditors.XtraForm
    {
        public ARPayments()
        {
            InitializeComponent();
        }

        private void ARPayments_Load(object sender, EventArgs e)
        {
            populateColumns();
        }

        void populateColumns()
        {
            Database.displayDevComboBoxItems("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'TransactionPayment'", "COLUMN_NAME", txtcols);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            search();
        }

        void search()
        {
            if (String.IsNullOrEmpty(txtcols.Text))
            {
                XtraMessageBox.Show("Please Select Search Category Criteria");
                return;
            }
            else
            {
                Database.display("SELECT * FROM TransactionPayment WHERE " + txtcols.Text + " like '%" + txtsearchfield.Text + "%'   ", gridControl1, gridView1);
            }
        }

    }
}