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
    public partial class AddFreightChargeSTS : DevExpress.XtraEditors.XtraForm
    {
        public AddFreightChargeSTS()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            Database.display("Select * FROM view_STSItems WHERE EffectivityDate between'" + datefrom.Text + "' and '" + dateto.Text + "' ", gridControl1, gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtid.Text) || String.IsNullOrEmpty(txtamount.Text) || String.IsNullOrEmpty(txtinvoicedate.Text) || String.IsNullOrEmpty(txtinvoiceno.Text) || String.IsNullOrEmpty(txtsupp.Text) || gridView1.RowCount==0)
            {
                XtraMessageBox.Show("Please Validate All Fields");
            }
            else
            {
                execute();
                XtraMessageBox.Show("Successfully Added!");
                this.Dispose();
            }
        }

        void execute()
        {
            string id = IDGenerator.getIDNumberSP("sp_GetFreightSTSNumber", "ReferenceNumber"); // IDGenerator.getTransferedNumber();
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {

                if (rowHandle >= 0)
                {
                    //these details are in DeliverySummary
                    string devno = gridView1.GetRowCellValue(rowHandle, "DeliveryNo").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string ponumber = gridView1.GetRowCellValue(rowHandle, "PONumber").ToString();//dataGridView1.Rows[0].Cells["Product"].Value.ToString();
                    string branchcode = gridView1.GetRowCellValue(rowHandle, "BranchCode").ToString();// dataGridView1.Rows[0].Cells["Description"].Value.ToString();
                    string quantity = gridView1.GetRowCellValue(rowHandle, "TotalQtyDelivered").ToString();//dataGridView1.Rows[0].Cells["Quantity"].Value.ToString();
                    
                    Database.ExecuteQuery("INSERT INTO FreightSTSDetails VALUES ('"+ txtid.Text + "','"+ devno + "','"+ ponumber + "','"+ branchcode + "','" + quantity + "',0 )");
                }
            }
            Database.ExecuteQuery("INSERT INTO FreightSTSSummary VALUES ('" + txtid.Text + "',0,'"+txtamount.Text+"',' ','"+Login.Fullname+"') ");
            executeSP();
        }

        void executeSP()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_AddFreightSTS";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmid", txtid.Text);
            com.Parameters.AddWithValue("@parmsupplierid", txtsupp.Text);
            com.Parameters.AddWithValue("@parminvoicedate", txtinvoicedate.Text);
            com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
            com.Parameters.AddWithValue("@parminvoiceamount", txtamount.Text);
            com.Parameters.AddWithValue("@parmuser", Login.Fullname);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void AddFreightChargeSTS_Load(object sender, EventArgs e)
        {
            string id = IDGenerator.getIDNumberSP("sp_GetFreightSTSNumber", "ReferenceNumber");
            txtid.Text = id;
            displaySupplier();
        }
        void displaySupplier()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", txtsupp, "SupplierID", "SupplierID");
        }

        private void txtsupp_EditValueChanged(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("SELECT SupplierName,ServiceCode,ServiceName,Cost from SERVICESCOST WHERE SupplierID='" + txtsupp.Text + "'", srchprod, "ServiceName", "ServiceName");
        }

        private void srchprod_EditValueChanged(object sender, EventArgs e)
        {
            object var = SearchLookUpClass.getSingleValue(srchprod, "Cost");
            txtamount.Text = var.ToString();
        }
    }
}