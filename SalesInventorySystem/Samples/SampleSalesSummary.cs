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

namespace SalesInventorySystem.Samples
{
    public partial class SampleSalesSummary : DevExpress.XtraEditors.XtraForm
    { // Create a row Pivot Grid Control field bound to the Country datasource field.
        PivotGridField fielddate;
        PivotGridField fieldgamecat;
        PivotGridField fieldgamename;

        PivotGridField fieldgross;
        PivotGridField fieldwinnings;
        PivotGridField fieldggr;

        bool isExpanded = false; // Define this at the top of your class (outside the method)
        public SampleSalesSummary()
        {
            InitializeComponent();
        }

        private void SampleSalesSummary_Load(object sender, EventArgs e)
        {
            execute();
        }

        void execute()
        {
            // Use 'using' statements to ensure connections and commands are disposed properly
            using (SqlConnection con = Database.getConnection())
            {
                try
                {
                    con.Open();
                    using (SqlCommand com = new SqlCommand("spr_SalesSummary", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandTimeout = 3600;

                        // Parameters
                        com.Parameters.AddWithValue("@parmdatefrom", "01/01/2026");
                        com.Parameters.AddWithValue("@parmdateto", "01/26/2026");
                        com.Parameters.AddWithValue("@parmbranchcode", "");
                        com.Parameters.AddWithValue("@parmgamecategoryid", "");
                        com.Parameters.AddWithValue("@parmgameid", "");

                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Lock UI updates for performance while configuring fields
                        pivotGridControl1.BeginUpdate();
                        try
                        {
                            pivotGridControl1.DataSource = null;
                            pivotGridControl1.Fields.Clear();

                            pivotGridControl1.DataSource = table;

                            // Initialize Fields
                            var fielddate = new PivotGridField("DateTransaction", PivotArea.RowArea) { AreaIndex = 0 };
                            fielddate.TotalsVisibility = PivotTotalsVisibility.AutomaticTotals ; // <--- Add this
                            fielddate.SortOrder = PivotSortOrder.Descending;
                            var fieldgamecat = new PivotGridField("GameCategory", PivotArea.RowArea) { AreaIndex = 1 };
                            var fieldgamename = new PivotGridField("GameName", PivotArea.RowArea) { AreaIndex = 2 };

                            var fieldgross = CreateDataField("TotalGross", "Total Gross");
                            var fieldwinnings = CreateDataField("TotalWinnings", "Total Winnings");
                            var fieldggr = CreateDataField("GGR","Gross Gaming Revenue");

                            // Add all at once
                            pivotGridControl1.Fields.AddRange(new PivotGridField[] {
                        fielddate, fieldgamecat, fieldgamename,
                        fieldgross, fieldwinnings, fieldggr
                    });

                            fieldgross.MinWidth = 100;
                            fieldwinnings.MinWidth = 100;
                            fieldggr.MinWidth = 100;

                            pivotGridControl1.EndUpdate();
                            pivotGridControl1.OptionsView.ShowRowTotals = true;
                            // 1. Forces the control to finish calculating its internal boundaries
                            pivotGridControl1.ForceInitialize();

                            // Use the Control's method instead of the Field's property
                            pivotGridControl1.CollapseAllRows(); // Optional: Start fresh

                            // This collapses only the DateTransaction level
                            pivotGridControl1.CollapseValue(false, new object[] { fielddate });

                            // NOW call BestFit, once the grid is unlocked and rendered
                            pivotGridControl1.BestFit();
                        }
                        finally
                        {
                            // Unlock UI - This triggers the visual creation
                  

                            //// 3. Apply the fit
                            //pivotGridControl1.BestFit();
                            // 4. (Optional) Set a minimum width for specific data fields if they still look tight
                            con.Close();
                        }

                     
                    }
                }
                catch (SqlException ee)
                {
                    XtraMessageBox.Show(ee.Message);
                }
            }
        }

        // Helper method to reduce repetitive code
        private PivotGridField CreateDataField(string fieldName, string caption)
        {
            var field = new PivotGridField(fieldName, PivotArea.DataArea);
            field.Caption = caption;
            field.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            field.CellFormat.FormatString = "n2";
            return field;
        }

        private void pivotGridControl1_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            if (e.RowValueType == PivotGridValueType.Total || e.RowValueType == PivotGridValueType.GrandTotal)
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            if (e.DataField != null && e.DataField.FieldName == "GGR")
            {
                // 2. Check if the value is negative
                if (Convert.ToDecimal(e.Value) < 0)
                {
                    // 3. Set the color to Red
                    e.Appearance.ForeColor = Color.Red;

                    // Optional: Make it bold to stand out more
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pivotGridControl1.BeginUpdate();
            try
            {
                if (isExpanded)
                {
                    pivotGridControl1.CollapseAllRows();
                    btnToggle.Text = "Expand All";
                }
                else
                {
                    pivotGridControl1.ExpandAllRows();
                    btnToggle.Text = "Collapse All";
                }

                // Flip the state
                isExpanded = !isExpanded;
            }
            finally
            {
                // Unlocks the grid so the UI can refresh
                pivotGridControl1.EndUpdate();
            }

            // IMPORTANT: Actions that measure pixels must happen after EndUpdate
            pivotGridControl1.BestFit();
        }

        private void pivotGridControl1_FieldValueExpanded(object sender, PivotFieldValueEventArgs e)
        {
            isExpanded = true;
            btnToggle.Text = "Collapse All";
        }
    }
}