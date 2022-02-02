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
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AddAPTransaction : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public AddAPTransaction()
        {
            InitializeComponent();
        }

        private void AddAPTransaction_Load(object sender, EventArgs e)
        {

        }

        double amountChanged()
        {
            double amount = 0.0;
            //amount = (Convert.ToDouble(txtvatamount.Text) + Convert.ToDouble(txtvatinputamount.Text) + Convert.ToDouble(txtvatexamount.Text))-Convert.ToDouble(txtewtamount.Text);
            amount = Convert.ToDouble(txttotalamount.Text) - Convert.ToDouble(txtewtamount.Text);
            return amount; //txttotalamount.Text = amountChanged().ToString();
        }
   
        void executeSP()
        {
            //insert into ledger and update supplier accounts, also update final figure in APACCOUNTS and generate TICKETS
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "spu_APAccounts";
            SqlCommand com = new SqlCommand(query,con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
            com.Parameters.AddWithValue("@parmsupplierid", txtsupplierid.Text);
            com.Parameters.AddWithValue("@parmsuppliername", txtsuppliername.Text);
            com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
            com.Parameters.AddWithValue("@parminvoicedate", txtinvoicedate.Text);
            com.Parameters.AddWithValue("@parmvatamount", txtvatamount.Text);
            com.Parameters.AddWithValue("@parmvatinputamount", txtvatinputamount.Text);
            com.Parameters.AddWithValue("@parmvatexemptamount", txtvatexamount.Text);
            com.Parameters.AddWithValue("@parmewtamount", txtewtamount.Text);
            com.Parameters.AddWithValue("@parmparticulars", txtremakrs.Text);
            com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to Confirm this Transaction", "Confirm Transaction");
            if (confirm)
            {
                executeSP();
                isdone = true;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void txtewtamount_EditValueChanged(object sender, EventArgs e)
        {
            txttotalamount.Text = amountChanged().ToString();
        }

        private void txtvatexamount_EditValueChanged(object sender, EventArgs e)
        {
            txttotalamount.Text = amountChanged().ToString();
        }

        private void txtvatamount_EditValueChanged(object sender, EventArgs e)
        {
            txttotalamount.Text = amountChanged().ToString();
        }

        private void txtvatinputamount_EditValueChanged(object sender, EventArgs e)
        {
            txttotalamount.Text = amountChanged().ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(Reporting.BatchProcessMasterDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            Reporting.BatchProcessMasterDevEx pcutfmr = new Reporting.BatchProcessMasterDevEx();
            pcutfmr.Show();
            Database.display("SELECT ShipmentNo,SupplierID,InvoiceDate,InvoiceNo FROM APACCOUNTS WHERE SupplierID='" + txtsupplierid.Text + "' and ShipmentNo='"+txtshipmentno.Text+"' ", pcutfmr.gridControl1, pcutfmr.gridView1);
        
        }

     
    }
}