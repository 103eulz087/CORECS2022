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

namespace SalesInventorySystem.Samples
{
    public partial class SampleCashierTransactions : DevExpress.XtraEditors.XtraForm
    {
        public SampleCashierTransactions()
        {
            InitializeComponent();
        }

        private void SampleCashierTransactions_Load(object sender, EventArgs e)
        {
            populate();
            gridView1.Columns[0].Visible = false;
            // Define the columns you want to format
            string[] numericColumns = { "Beginning", "Debit", "Credit", "Ending" };

            foreach (string colName in numericColumns)
            {
                if (gridView1.Columns[colName] != null)
                {
                    // Set the format type to Numeric
                    gridView1.Columns[colName].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

                    // "N2" provides commas and 2 decimal places (e.g., 1,500.00)
                    // Use "N4" if you want to keep the 4 decimal places shown in your image
                    gridView1.Columns[colName].DisplayFormat.FormatString = "N2";
                }
                
            }
            if (gridView1.Columns["DateTransaction"] != null)
            {
                gridView1.Columns["DateTransaction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["DateTransaction"].DisplayFormat.FormatString = "MM/dd/yyyy HH:mm:ss";
            }

        }

        void populate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            gridControl1.BeginUpdate();
            try
            {
                string sp = "spr_cashierledger";
                SqlCommand com = new SqlCommand(sp, con); 
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                gridControl1.EndUpdate();
                con.Close();
            }
        }
    }
}