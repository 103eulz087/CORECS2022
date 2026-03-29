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
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.Reporting
{
    public partial class BranchInventoryProductSummary : DevExpress.XtraEditors.XtraForm
    {
        public BranchInventoryProductSummary()
        {
            InitializeComponent();
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
           await ExecuteAsync();
        }

        private async Task ExecuteAsync()
        {
            DataTable table = new DataTable();

            try
            {
                // 1. OFFLOAD TO BACKGROUND THREAD (Prevents UI Freeze)
                await Task.Run(() =>
                {
                    // Using blocks ensure connections are closed even if an error occurs
                    using (SqlConnection con = Database.getConnection())
                    {
                        string query = "SELECT Category, ProductCode, Description, BranchName, Qty FROM view_InventoryProductsSummary";
                        using (SqlCommand com = new SqlCommand(query, con))
                        {
                            com.CommandType = CommandType.Text;
                            using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                            {
                                adapter.Fill(table);
                            }
                        }
                    }
                });

                // 2. LOCK THE UI FOR FAST RENDERING
                pivotGridControl1.BeginUpdate();

                pivotGridControl1.Fields.Clear();
                pivotGridControl1.DataSource = table;

                // 3. BUILD FIELDS
                PivotGridField fieldCategory = new PivotGridField("Category", PivotArea.RowArea) { AreaIndex = 0 };
                PivotGridField fieldProduct = new PivotGridField("Description", PivotArea.RowArea) { Caption = "PRODUCT NAME", AreaIndex = 1 };
                PivotGridField fieldBranch = new PivotGridField("BranchName", PivotArea.ColumnArea) { Caption = "BRANCH NAME" };

                PivotGridField fieldQty = new PivotGridField("Qty", PivotArea.DataArea);
                fieldQty.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fieldQty.CellFormat.FormatString = "n3"; // Adjusted to n3 since we converted inventory to decimal(18,3) earlier!

                pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldCategory, fieldProduct, fieldBranch, fieldQty });

                //pivotGridControl1.BestFit(fieldProduct);

                // 4. APPLY STYLING
                pivotGridControl1.Appearance.Cell.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
                pivotGridControl1.Appearance.HeaderArea.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);
                pivotGridControl1.Appearance.FieldValue.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);
                pivotGridControl1.Appearance.GrandTotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                pivotGridControl1.Appearance.TotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                pivotGridControl1.Appearance.HeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                UserLookAndFeel.Default.SkinName = "Office 2019 Colorful";
            }
            catch (Exception ex)
            {
                // Safely catch without crashing the app or showing raw stack traces
                BigAlert.Show("Database Error", "Could not load inventory summary.\n" + ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                // 1. UNLOCK THE UI FIRST
                // This forces the grid to finally process the data and build its layout
                pivotGridControl1.EndUpdate();

                // 2. THE BULLETPROOF BEST FIT
                // BeginInvoke waits for the UI thread to take a breath and finish painting the screen.
                //// Once the text actually exists on the screen, BestFit measures it perfectly.
                //pivotGridControl1.BeginInvoke(new Action(() =>
                //{
                //    pivotGridControl1.BestFit(fieldProduct);

                //    // Optional: If your Category names are also getting cut off, best fit them too!
                //    pivotGridControl1.BestFit(fieldProduct);
                //}));
            }
        }
        //void execute()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    try
        //    {
        //        SqlCommand com = null;

        //        // Instead of stored procedure, query the view directly
        //        string query = "SELECT * FROM view_InventoryProductsSummary";
        //        com = new SqlCommand(query, con);
        //        //com.Parameters.AddWithValue("@parmdate", dateEdit1.Text);
        //        com.CommandType = CommandType.Text;  // important: it's now a text query

        //        SqlDataAdapter adapter = new SqlDataAdapter(com);
        //        DataTable table = new DataTable();

        //        pivotGridControl1.Fields.Clear();
        //        adapter.Fill(table);
        //        pivotGridControl1.DataSource = table;

        //        PivotGridField fieldCategory = new PivotGridField("Category", PivotArea.RowArea);
        //        PivotGridField fieldProduct = new PivotGridField("Description", PivotArea.RowArea);
        //        fieldProduct.Caption = "PRODUCT NAME";

        //        PivotGridField fieldBranch = new PivotGridField("BranchName", PivotArea.ColumnArea);
        //        fieldBranch.Caption = "BRANCH NAME";

        //        PivotGridField fieldQty = new PivotGridField("Qty", PivotArea.DataArea);
        //        fieldQty.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //        fieldQty.CellFormat.FormatString = "n2";

        //        pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldCategory, fieldProduct, fieldBranch, fieldQty });

        //        fieldCategory.AreaIndex = 0;
        //        fieldProduct.AreaIndex = 1;
        //        pivotGridControl1.BestFit(fieldProduct);
        //        // 1. Data Cells: Clean, readable, standard size
        //        pivotGridControl1.Appearance.Cell.Font = new Font("Segoe UI", 9f, FontStyle.Regular);

        //        // 2. Column and Row Headers (The gray areas): Slightly larger and Bold
        //        pivotGridControl1.Appearance.HeaderArea.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);
        //        pivotGridControl1.Appearance.FieldValue.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);

        //        // 3. Grand Totals: Keep the same size as data, but make them Bold
        //        pivotGridControl1.Appearance.GrandTotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        //        pivotGridControl1.Appearance.TotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

        //        // Optional but highly recommended: Center the column headers for a cleaner look
        //        pivotGridControl1.Appearance.HeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //        // 5. SKINNING
        //        UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
        //        UserLookAndFeel.Default.SkinName = "Office 2019 Colorful";
        //    }
        //    catch (SqlException ee)
        //    {
        //        XtraMessageBox.Show(ee.ToString());
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pivotGridControl1.ShowRibbonPrintPreview();
        }
        private async Task ShowDrillDownPopupAsync(string branchName, string productCode, string productName)
        {
            DataTable detailsTable = new DataTable();

            try
            {
                // 1. Fetch the data asynchronously so the UI doesn't freeze
                await Task.Run(() =>
                {
                    using (SqlConnection con = Database.getConnection())
                    using (SqlCommand cmd = new SqlCommand("sp_GetInventoryDrillDown", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BranchName", branchName);
                        cmd.Parameters.AddWithValue("@ProductCode", productCode);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(detailsTable);
                        }
                    }
                });

                if (detailsTable.Rows.Count == 0)
                {
                    BigAlert.Show("No Details", "There are no active inventory batches for this item. It may have just been depleted.", MessageBoxIcon.Information);
                    return;
                }

                // 2. Dynamically create a DevExpress GridControl
                DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
                DevExpress.XtraGrid.Views.Grid.GridView view = new DevExpress.XtraGrid.Views.Grid.GridView();

                grid.MainView = view;
                grid.ViewCollection.Add(view);
                grid.DataSource = detailsTable;
                grid.Dock = DockStyle.Fill;

                // Make the grid read-only and look clean
                view.OptionsBehavior.Editable = false;
                view.OptionsView.ShowGroupPanel = false;
                view.Appearance.HeaderPanel.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);
                view.Appearance.Row.Font = new Font("Segoe UI", 9f, FontStyle.Regular);

                // 3. Dynamically create the Popup Form
                Form popup = new Form();
                popup.Text = $"Active FIFO Batches: {productName} ({branchName})";
                popup.Size = new Size(800, 400);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowIcon = false;
                popup.Controls.Add(grid);

                // Auto-size the columns perfectly right after the form opens
                popup.Shown += (s, ev) => view.BestFitColumns();

                // 4. Show it to the user!
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                BigAlert.Show("Drill Down Error", "Could not load batch details.\n" + ex.Message, MessageBoxIcon.Error);
            }
        }
        private async void pivotGridControl1_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            // 1. Ignore clicks on Grand Totals or Headers
            if (e.RowValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value ||
                e.ColumnValueType != DevExpress.XtraPivotGrid.PivotGridValueType.Value)
            {
                return;
            }

            // 2. Safely extract the hidden ProductCode and BranchName from the clicked cell
            var drillDownData = e.CreateDrillDownDataSource();
            if (drillDownData.RowCount == 0) return;

            string productCode = drillDownData[0]["ProductCode"].ToString();
            string branchName = drillDownData[0]["BranchName"].ToString();
            string productName = drillDownData[0]["Description"].ToString();

            // 3. Fire the popup!
            await ShowDrillDownPopupAsync(branchName, productCode, productName);
        }
    }
}