using DevExpress.XtraEditors;
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

namespace SalesInventorySystem
{
    public partial class ReInventoryAnalyzer : Form
    {
        public ReInventoryAnalyzer()
        {
            InitializeComponent();
        }

        private void ReInventoryAnalyzer_Load(object sender, EventArgs e)
        {
            txtbranch.Text = ReInventoryIn.branchcode;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure?","Save Inventory");
            if(confirm)
            {
                Database.ExecuteQuery("DELETE FROM TempInventoryAnalyzerStorage WHERE BranchCode='" + ReInventoryIn.branchcode + "'");
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("INSERT INTO TempInventoryAnalyzerStorage VALUES ('" + gridView1.GetRowCellValue(i, "BranchCode").ToString() + "','" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridView1.GetRowCellValue(i, "ProductName").ToString() + "','" + gridView1.GetRowCellValue(i, "Qty").ToString() + "','" + gridView1.GetRowCellValue(i, "AvailableOnHand").ToString() + "','" + gridView1.GetRowCellValue(i, "Variance").ToString() + "','" + gridView1.GetRowCellValue(i, "Status").ToString() + "')");
                }
                XtraMessageBox.Show("Successfully Added!");
                execute();
                XtraMessageBox.Show("Successfully Updated!");
                this.Close();
            }
            else
            {
                return;
            }
        }

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_InventoryAnalyzerAdjustment";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", ReInventoryIn.branchcode);
            com.Parameters.AddWithValue("@parmiswarehouse", ReInventoryIn.iswarehouse);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {
                string filepath = "C:\\MyFiles\\";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = "ReInventoryIN_"+ ReInventoryIn.branchcode +"_"+ DateTime.Now.ToShortDateString().Replace("/", "-") + ".xls";
                string file = filepath + filename;
                gridView1.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles//folder");
            }

            
        }
    }
}
