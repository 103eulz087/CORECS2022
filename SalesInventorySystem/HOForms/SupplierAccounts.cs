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

namespace SalesInventorySystem.HOForms
{
    public partial class SupplierAccounts : Form
    {
        public static string supplierid, suppliername;
        public SupplierAccounts()
        {
            InitializeComponent();
        }

        private void SupplierAccounts_Load(object sender, EventArgs e)
        {
            displaySearchlookupEdit();
        }
       
    private void displaySearchlookupEdit()
    {
        SqlConnection con = Database.getConnection();
        con.Open();
        string query = "select SupplierID,SupplierName FROM Supplier";
        SqlCommand com = new SqlCommand(query, con);
        SqlDataAdapter adapter = new SqlDataAdapter(com);
        DataTable table = new DataTable();
        adapter.Fill(table);
        searchLookUpEdit1.Properties.DataSource = table;
    }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            txtacctid.Text = Classes.Suppliers.getSupplierID(searchLookUpEdit1.Text);
            txtacctstatus.Text = Classes.Suppliers.getSupplierStatus(searchLookUpEdit1.Text);
            txtacctbalance.Text = Classes.Suppliers.getSupplierBalance(searchLookUpEdit1.Text);
            txtmvmtdate.Text = Classes.Suppliers.getSupplierLastMovementDate(searchLookUpEdit1.Text);
            //loadLedger();
            
        }

    
        private void btnget_Click(object sender, EventArgs e)
        {
            loadLedger();
        }
        void loadLedger()
        {
            Database.display("SELECT CAST(PostingDate as date) as PostingDate,CAST(TransactionDate as date) as TransactionDate,TransCode,Description,Debit,Credit,ReferenceNumber FROM SupplierLedger WHERE SupplierID='" + txtacctid.Text + "' AND CAST(TransactionDate as Date) >= '" + dateTimePicker1.Text + "' and CAST(TransactionDate as date)<= '" + dateTimePicker2.Text + "' ORDER BY SequenceNumber ASC", gridControl2, gridView2);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked==true)
            {
                datetopurch.Enabled = false;
                datefrompurch.Enabled = false;
            }else
            {
                datetopurch.Enabled = true;
                datefrompurch.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadPurchases();
        }

        void loadPurchases()
        {
            if(checkBox2.Checked==true)
                  Database.display("SELECT ShipmentNo,InvoiceNo,InvoiceDate,OrderDate,ActualCost,Status,DatePaid FROM ShipmentOrder WHERE SupplierID='" + txtacctid.Text + "' ORDER BY SequenceNumber DESC", gridControl1, gridView1);
            else
                Database.display("SELECT ShipmentNo,InvoiceNo,InvoiceDate,OrderDate,ActualCost,Status,DatePaid FROM ShipmentOrder WHERE SupplierID='" + txtacctid.Text + "' AND CAST(OrderDate as Date) >= '" + datefrompurch.Text + "' and CAST(OrderDate as date)<= '" + datetopurch.Text + "' ORDER BY SequenceNumber ASC", gridControl1, gridView1);

        }

        void loadPayments()
        {
            if (checkBox3.Checked == true)
                Database.display("SELECT VoucherID,PaidTo,CheckNo,CheckDate,Particulars,Amount,PreparedBy FROM CheckVoucher WHERE PaidTo='" + searchLookUpEdit1.Text + "' ORDER BY SequenceNumber DESC", gridControl3, gridView3);
            else
                Database.display("SELECT VoucherID,PaidTo,CheckNo,CheckDate,Particulars,Amount,PreparedBy  FROM CheckVoucher WHERE PaidTo='" + searchLookUpEdit1.Text + "' AND CAST(DateAdded as Date) >= '" + datefrompay.Text + "' and CAST(DateAdded as date)<= '" + datetopay.Text + "' ORDER BY SequenceNumber ASC", gridControl3, gridView3);

        }
        void loadExpenses()
        {
            if (checkBox4.Checked == true)
                Database.display("SELECT * FROM ExpenseMaster WHERE Owner='" + txtacctid.Text + "' ORDER BY SequenceNumber DESC", gridControl4, gridView4);
            else
                Database.display("SELECT * FROM ExpenseMaster WHERE Owner='" + txtacctid.Text + "' AND CAST(ExpenseDate as Date) >= '" + expdatefrom.Text + "' and CAST(ExpenseDate as date)<= '" + expdateto.Text + "' ORDER BY SequenceNumber ASC", gridControl4, gridView4);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            loadPayments();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                datetopay.Enabled = false;
                datefrompay.Enabled = false;
            }
            else
            {
                datetopay.Enabled = true;
                datefrompay.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadExpenses();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                expdatefrom.Enabled = false;
                expdateto.Enabled = false;
            }
            else
            {
                expdatefrom.Enabled = true;
                expdateto.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            supplierid = txtacctid.Text;
            suppliername = searchLookUpEdit1.Text;
            HOForms.TransactionPayment adhipma = new TransactionPayment();
            adhipma.Show();
            if(TransactionPayment.isdone == true)
            {
                //searchLookUpEdit1.Text = "";
                txtacctid.Text = "";
                txtacctbalance.Text = "";
                txtacctstatus.Text = "";
                txtmvmtdate.Text = "";
                txtacctid.Text = Classes.Suppliers.getSupplierID(adhipma.txtsuppliername.Text);
                txtacctstatus.Text = Classes.Suppliers.getSupplierStatus(adhipma.txtsuppliername.Text);
                txtacctbalance.Text = Classes.Suppliers.getSupplierBalance(adhipma.txtsuppliername.Text);
                txtmvmtdate.Text = Classes.Suppliers.getSupplierLastMovementDate(adhipma.txtsuppliername.Text);

                TransactionPayment.isdone = false;
                adhipma.Dispose();
            }
        }
    }
}
