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
using DevExpress.XtraPivotGrid;
using DevExpress.LookAndFeel;

namespace SalesInventorySystem.Reporting
{
    public partial class BranchInventoryProductSummary : DevExpress.XtraEditors.XtraForm
    {
        public BranchInventoryProductSummary()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            execute();
        }
        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                SqlCommand com = null;

                // Instead of stored procedure, query the view directly
                string query = "SELECT * FROM view_InventoryProductsSummary";
                com = new SqlCommand(query, con);
                //com.Parameters.AddWithValue("@parmdate", dateEdit1.Text);
                com.CommandType = CommandType.Text;  // important: it's now a text query

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();

                pivotGridControl1.Fields.Clear();
                adapter.Fill(table);
                pivotGridControl1.DataSource = table;

                PivotGridField fieldCategory = new PivotGridField("Category", PivotArea.RowArea);
                PivotGridField fieldProduct = new PivotGridField("Description", PivotArea.RowArea);
                fieldProduct.Caption = "PRODUCT NAME";

                PivotGridField fieldBranch = new PivotGridField("BranchName", PivotArea.ColumnArea);
                fieldBranch.Caption = "BRANCH NAME";

                PivotGridField fieldQty = new PivotGridField("Qty", PivotArea.DataArea);
                fieldQty.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fieldQty.CellFormat.FormatString = "n2";

                pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldCategory, fieldProduct, fieldBranch, fieldQty });

                fieldCategory.AreaIndex = 0;
                fieldProduct.AreaIndex = 1;
                pivotGridControl1.BestFit(fieldProduct);

                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";
            }
            catch (SqlException ee)
            {
                XtraMessageBox.Show(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pivotGridControl1.ShowRibbonPrintPreview();
        }
    }
}