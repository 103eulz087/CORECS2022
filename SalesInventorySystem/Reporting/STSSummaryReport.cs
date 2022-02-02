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
    public partial class STSSummaryReport : DevExpress.XtraEditors.XtraForm
    {
        public STSSummaryReport()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(dateEdit1.Text))
            {
                XtraMessageBox.Show("Date Field must not Empty");
                return;
            }
            else
            {
                execute();
            }
        }
        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            //pivotGridControl1.BeginUpdate();
            try
            {
                SqlCommand com=null;

                //com = new SqlCommand("select a.BranchName,b.ProductName,d.EffectivityDate,b.Qty,c.Description FROM dbo.TransferOrderDetails  as b INNER JOIN dbo.TransferOrderSummary as d ON d.PONumber=b.PONumber INNER JOIN dbo.Branches as a ON d.BranchCode=a.BranchCode INNER JOIN dbo.ProductCategory as c ON c.ProductCategoryID=SUBSTRING(b.ProductCode,1,2)  WHERE CAST(d.EffectivityDate as date)='" + dateEdit1.Text + "' order by c.Description", con);
                string query = "spr_STS";
                com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdate",dateEdit1.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                //if (company == "JFC")
                //{
                //    com = new SqlCommand("select a.BranchName,b.ProductName,b.EffectivityDate,b.Qty,c.Description FROM dbo.PurchaseOrderDetails  as b INNER JOIN dbo.Branches as a ON b.BranchCode=a.BranchCode INNER JOIN dbo.ProductCategory as c ON c.ProductCategoryID=SUBSTRING(b.ProductCode,1,2) WHERE CAST(b.EffectivityDate as date)='" + dateEdit1.Text + "' order by c.Description", con);

                //}
                //else if(company == "ENZO")
                //{     
                //    com = new SqlCommand("select a.BranchName,b.ProductName,b.EffectivityDate,b.Qty,c.Description FROM dbo.TransferOrderDetails  as b INNER JOIN dbo.Branches as a ON b.BranchCode=a.BranchCode INNER JOIN dbo.ProductCategory as c ON c.ProductCategoryID=SUBSTRING(b.ProductCode,1,2) WHERE CAST(b.EffectivityDate as date)='" + dateEdit1.Text + "' order by c.Description", con);
                //}

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();

                //pivotGridControl1.DataSource = null;
                //table.Rows.Clear();
                //table.Columns.Clear();
                pivotGridControl1.Fields.Clear();
                adapter.Fill(table);
                pivotGridControl1.DataSource = table;
                //pivotGridControl1.RetrieveFields();
                //pivotGridControl1.RefreshData();
                // Create a row Pivot Grid Control field bound to the Country datasource field.
                PivotGridField fieldCountry = new PivotGridField("Description", PivotArea.RowArea);
                PivotGridField fieldCustomer = new PivotGridField("ProductName", PivotArea.RowArea);
                fieldCustomer.Caption = "PRODUCT NAME";

                PivotGridField fieldYear = new PivotGridField("BranchName", PivotArea.ColumnArea);
                fieldYear.Caption = "BRANCH NAME";

                PivotGridField fieldExtendedPrice = new PivotGridField("Qty", PivotArea.DataArea);
                fieldExtendedPrice.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fieldExtendedPrice.CellFormat.FormatString = "n2";
                // Add the fields to the control's field collection.         
                pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldCountry, fieldCustomer, fieldYear, fieldExtendedPrice });

                fieldCountry.AreaIndex = 0;
                fieldCustomer.AreaIndex = 1;
                pivotGridControl1.BestFit(fieldCustomer);

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