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
using DevExpress.LookAndFeel;
using DevExpress.XtraPivotGrid;
using System.Globalization;
using DevExpress.XtraEditors.Controls;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.Reporting
{
    public partial class InventoryDailyActivityPivot : DevExpress.XtraEditors.XtraForm
    {
        object objbrcode = null;
        string globalbrcode;
        public InventoryDailyActivityPivot()
        {
            InitializeComponent();
            loadComboBoxValues();
        }


        void loadComboBoxValues()
        {
            // 1. Populate Months (January - December)
            cmbFromMonth.Properties.Items.Clear();
            cmbToMonth.Properties.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                // This gets the localized month name (e.g., "March")
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                cmbFromMonth.Properties.Items.Add(new ImageComboBoxItem(monthName, i));
                cmbToMonth.Properties.Items.Add(new ImageComboBoxItem(monthName, i));
            }

            // 2. Populate Years (Dynamic range: 5 years back, 1 year forward)
            int currentYear = DateTime.Now.Year; // It's 2026!
            cmbFromYear.Properties.Items.Clear();
            cmbToYear.Properties.Items.Clear();
            for (int y = currentYear - 5; y <= currentYear + 1; y++)
            {
                cmbFromYear.Properties.Items.Add(y);
                cmbToYear.Properties.Items.Add(y);
            }

            // 3. Set Defaults to the CURRENT Month and Year
            // (Because users usually want to see the most recent data first)
            cmbFromMonth.SelectedIndex = DateTime.Now.Month - 1;
            cmbToMonth.SelectedIndex = DateTime.Now.Month - 1;
            cmbFromYear.EditValue = currentYear;
            cmbToYear.EditValue = currentYear;
        }
        // 1. Made the method ASYNC so the UI never freezes
        private async Task ExecuteAsync()
        {
            // Prevent the user from clicking the button twice while it's loading
            // Assuming you call this from a button named btnExecute
            // btnExecute.Enabled = false; 

            DataTable table = new DataTable();

            try
            {
                // 2. OFFLOAD DATABASE WORK TO A BACKGROUND THREAD
                // This keeps the UI perfectly smooth while SQL Server thinks
                await Task.Run(() =>
                {
                    using (SqlConnection con = Database.getConnection())
                    using (SqlCommand com = new SqlCommand("sp_GenerateInventoryDailyReport", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@parmbrcode", globalbrcode);
                        com.Parameters.AddWithValue("@datefrom", datefrom.DateTime.Date); // Ensure you pass valid Dates, not strings
                        com.Parameters.AddWithValue("@dateto", dateto.DateTime.Date);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                        {
                            adapter.Fill(table);
                        }
                    }
                });

                // 3. UI PERFORMANCE LOCKS
                // Lock the PivotGrid so it doesn't recalculate math while we build it
                pivotGridControl1.BeginUpdate();

                pivotGridControl1.Fields.Clear();
                pivotGridControl1.DataSource = table;

                // 4. BUILD THE FIELDS
                PivotGridField fielddescription = new PivotGridField("Category", PivotArea.RowArea) { AreaIndex = 0 };
                PivotGridField fieldprodname = new PivotGridField("Description", PivotArea.RowArea) { Caption = "PRODUCT NAME", AreaIndex = 1 };

                PivotGridField fieldtransdate = new PivotGridField("TransactionDate", PivotArea.ColumnArea) { Caption = "TRANSACTION DATE" };

                // Define Data Fields with formatting
                PivotGridField fldbeginning = CreateDataField("Beginning");
                PivotGridField fldsts = CreateDataField("STSQtyRcvd");
                PivotGridField fldreturnin = CreateDataField("ReturnIN");
                PivotGridField fldconvin = CreateDataField("ConversionIN");
                PivotGridField fldtotalin = CreateDataField("TOTALIN");
                PivotGridField fldreturnout = CreateDataField("ReturnOUT");
                PivotGridField fldconvout = CreateDataField("ConversionOut");
                PivotGridField fldslsout = CreateDataField("SalesOut");
                PivotGridField fldtotalout = CreateDataField("TOTALOUT");
                PivotGridField fldending = CreateDataField("EndingQty");

                // Add all fields at once
                pivotGridControl1.Fields.AddRange(new PivotGridField[] {
            fielddescription, fieldprodname, fieldtransdate,
            fldbeginning, fldsts,fldreturnin, fldconvin, fldtotalin,fldreturnout,
            fldconvout, fldslsout, fldtotalout, fldending
        });
                pivotGridControl1.OptionsView.ShowColumnGrandTotals = false;
                pivotGridControl1.OptionsView.ShowRowGrandTotals = false;

                //pivotGridControl1.CustomAppearance += (s, e) =>
                //{
                //    if (e.DataField != null)
                //    {
                //        // Highlight TOTALIN, TOTALOUT, EndingQty columns
                //        if (e.DataField.FieldName == "TOTALIN" ||
                //            e.DataField.FieldName == "TOTALOUT" ||
                //            e.DataField.FieldName == "EndingQty")
                //        {
                //            e.Appearance.BackColor = Color.LightYellow;
                //            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //        }

                //        // Highlight ConversionOut and SalesOut values in red if not zero
                //        if ((e.DataField.FieldName == "ConversionOut" ||
                //             e.DataField.FieldName == "SalesOut") &&
                //            e.Value != null && e.Value is decimal val && val != 0)
                //        {
                //            e.Appearance.ForeColor = Color.Red;
                //            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //        }
                //    }
                //};

                pivotGridControl1.CustomAppearance += (s, e) =>
                {
                    if (e.DataField != null)
                    {
                        // 1. Highlight TOTALIN, TOTALOUT, EndingQty columns (Your existing logic)
                        if (e.DataField.FieldName == "TOTALIN" ||
                            e.DataField.FieldName == "TOTALOUT" )
                        {
                            e.Appearance.BackColor = Color.LightYellow;
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }

                        if (e.DataField.FieldName == "EndingQty")
                        {
                            e.Appearance.BackColor = Color.Yellow;
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }

                        // 2. Highlight ConversionOut and SalesOut values in red if not zero (Your existing logic)
                        if ((e.DataField.FieldName == "ConversionOut" || e.DataField.FieldName == "ReturnOUT" ||
                             e.DataField.FieldName == "SalesOut") &&
                            e.Value != null)
                        {
                            // Safely convert to decimal regardless of underlying type
                            decimal val = Convert.ToDecimal(e.Value);
                            if (val != 0)
                            {
                                e.Appearance.ForeColor = Color.Red;
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                            }
                        }

                        // =================================================================================
                        // 3. NEW: THE ANOMALY DETECTOR (Ending Qty vs Next Day's Beginning Qty)
                        // =================================================================================

                        // We only want to run this check when we are painting the "Beginning" cell
                        if (e.DataField.FieldName == "Beginning")
                        {
                            int currentColumnIndex = e.ColumnIndex;

                            // We can't check yesterday if today is the very first day in the grid
                            if (currentColumnIndex > 0)
                            {
                                // LOOK BACK IN TIME: Just subtract 1 from the column index!
                                // Because "EndingQty" is your last field and "Beginning" is your first field,
                                // the column immediately to the left (-1) is ALWAYS yesterday's EndingQty.
                                object yesterdayEndingObj = pivotGridControl1.GetCellValue(currentColumnIndex - 1, e.RowIndex);

                                object todayBeginningObj = e.Value;

                                // Safely convert both to decimals to avoid type mismatch crashes
                                decimal yesterdayEnding = yesterdayEndingObj == null ? 0 : Convert.ToDecimal(yesterdayEndingObj);
                                decimal todayBeginning = todayBeginningObj == null ? 0 : Convert.ToDecimal(todayBeginningObj);

                                // TRIGGER THE ANOMALY HIGHLIGHT!
                                if (yesterdayEnding != todayBeginning)
                                {
                                    // Paint the Beginning Qty cell RED with WHITE text to make it scream "ERROR"
                                    e.Appearance.BackColor = Color.Firebrick;
                                    e.Appearance.ForeColor = Color.White;
                                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                }
                            }
                        }
                    }
                };

                // --- FONT OPTIMIZATION FOR REPORTING ---

                // 1. Data Cells: Clean, readable, standard size
                pivotGridControl1.Appearance.Cell.Font = new Font("Segoe UI", 9f, FontStyle.Regular);

                // 2. Column and Row Headers (The gray areas): Slightly larger and Bold
                pivotGridControl1.Appearance.HeaderArea.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);
                pivotGridControl1.Appearance.FieldValue.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);

                // 3. Grand Totals: Keep the same size as data, but make them Bold
                pivotGridControl1.Appearance.GrandTotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                pivotGridControl1.Appearance.TotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

                // Optional but highly recommended: Center the column headers for a cleaner look
                pivotGridControl1.Appearance.HeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                // 5. SKINNING
                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                UserLookAndFeel.Default.SkinName = "Office 2019 Colorful";
            }
            catch (SqlException ee)
            {
                XtraMessageBox.Show("Database Error: " + ee.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Application Error: " + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                // 6. UNLOCK THE PIVOT GRID
                // This forces exactly ONE single redraw, making it lightning fast.
                pivotGridControl1.EndUpdate();
                pivotGridControl1.BestFit(pivotGridControl1.Fields["Description"]);

                // btnExecute.Enabled = true;
            }
        }

        private async Task ExecuteAsyncMonthly()
        {
            DataTable table = new DataTable();
            try
            {
                // 2. Extract Values Safely (Reads the visible text instead of casting objects)

                // Converts "March" -> 3
                int fromM = DateTime.ParseExact(cmbFromMonth.Text, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
                int fromY = Convert.ToInt32(cmbFromYear.Text);

                // Converts "June" -> 6
                int toM = DateTime.ParseExact(cmbToMonth.Text, "MMMM", System.Globalization.CultureInfo.CurrentCulture).Month;
                int toY = Convert.ToInt32(cmbToYear.Text);

                // 3. Construct the exact dates
                DateTime dateFrom = new DateTime(fromY, fromM, 1);
                DateTime dateTo = new DateTime(toY, toM, 1).AddMonths(1).AddDays(-1);

                // 4. Validate the range
                if (dateFrom > dateTo)
                {
                    BigAlert.Show("Invalid Range", "'From' date cannot be after 'To' date.", MessageBoxIcon.Error);
                    return;
                }
                // 2. OFFLOAD DATABASE WORK TO A BACKGROUND THREAD
                // This keeps the UI perfectly smooth while SQL Server thinks
                await Task.Run(() =>
                {
                    using (SqlConnection con = Database.getConnection())
                    using (SqlCommand com = new SqlCommand("sp_GenerateInventoryMonthlyRangeReport", con))
                    {
                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@parmbrcode", globalbrcode);
                        com.Parameters.AddWithValue("@datefrom", dateFrom); // Ensure you pass valid Dates, not strings
                        com.Parameters.AddWithValue("@dateto", dateTo);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(com))
                        {
                            adapter.Fill(table);
                        }
                    }
                });

                // 3. UI PERFORMANCE LOCKS
                // Lock the PivotGrid so it doesn't recalculate math while we build it
                pivotGridControl2.BeginUpdate();

                pivotGridControl2.Fields.Clear();
                pivotGridControl2.DataSource = table;

                // 4. BUILD THE FIELDS
                PivotGridField fielddescription = new PivotGridField("Category", PivotArea.RowArea) { AreaIndex = 0 };
                PivotGridField fieldprodname = new PivotGridField("Description", PivotArea.RowArea) { Caption = "PRODUCT NAME", AreaIndex = 1 };

                //PivotGridField fieldtransdate = new PivotGridField("TransactionDate", PivotArea.ColumnArea) { Caption = "TRANSACTION DATE" };
                PivotGridField fieldtransdate = new PivotGridField("ReportPeriod", PivotArea.ColumnArea) { Caption = "REPORT PERIOD" };

                // Tell DevExpress to format the Date as a Month/Year string 
                fieldtransdate.ValueFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                fieldtransdate.ValueFormat.FormatString = "MMMM yyyy"; // Will display as "March 2026", "April 2026", etc.
                // Define Data Fields with formatting
                PivotGridField fldbeginning = CreateDataField("Beginning");
                PivotGridField fldsts = CreateDataField("STSQtyRcvd");
                PivotGridField fldreturnin = CreateDataField("ReturnIN");
                PivotGridField fldconvin = CreateDataField("ConversionIN");
                PivotGridField fldtotalin = CreateDataField("TOTALIN");
                PivotGridField fldreturnout = CreateDataField("ReturnOUT");
                PivotGridField fldconvout = CreateDataField("ConversionOut");
                PivotGridField fldslsout = CreateDataField("SalesOut");
                PivotGridField fldtotalout = CreateDataField("TOTALOUT");
                PivotGridField fldending = CreateDataField("EndingQty");

                // Add all fields at once
                pivotGridControl2.Fields.AddRange(new PivotGridField[] {
            fielddescription, fieldprodname, fieldtransdate,
            fldbeginning, fldsts,fldreturnin, fldconvin, fldtotalin,fldreturnout,
            fldconvout, fldslsout, fldtotalout, fldending
        });
                pivotGridControl2.OptionsView.ShowColumnGrandTotals = false;
                pivotGridControl2.OptionsView.ShowRowGrandTotals = false;

                pivotGridControl2.CustomAppearance += (s, e) =>
                {
                    if (e.DataField != null)
                    {
                        // 1. Highlight TOTALIN, TOTALOUT, EndingQty columns (Your existing logic)
                        if (e.DataField.FieldName == "TOTALIN" ||
                            e.DataField.FieldName == "TOTALOUT")
                        {
                            e.Appearance.BackColor = Color.LightYellow;
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }

                        if (e.DataField.FieldName == "EndingQty")
                        {
                            e.Appearance.BackColor = Color.Yellow;
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }

                        // 2. Highlight ConversionOut and SalesOut values in red if not zero (Your existing logic)
                        if ((e.DataField.FieldName == "ConversionOut" || e.DataField.FieldName == "ReturnOUT" ||
                             e.DataField.FieldName == "SalesOut") &&
                            e.Value != null)
                        {
                            // Safely convert to decimal regardless of underlying type
                            decimal val = Convert.ToDecimal(e.Value);
                            if (val != 0)
                            {
                                e.Appearance.ForeColor = Color.Red;
                                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                            }
                        }

                        // =================================================================================
                        // 3. NEW: THE ANOMALY DETECTOR (Ending Qty vs Next Day's Beginning Qty)
                        // =================================================================================

                        // We only want to run this check when we are painting the "Beginning" cell
                        if (e.DataField.FieldName == "Beginning")
                        {
                            int currentColumnIndex = e.ColumnIndex;

                            // We can't check yesterday if today is the very first day in the grid
                            if (currentColumnIndex > 0)
                            {
                                // LOOK BACK IN TIME: Just subtract 1 from the column index!
                                // Because "EndingQty" is your last field and "Beginning" is your first field,
                                // the column immediately to the left (-1) is ALWAYS yesterday's EndingQty.
                                object yesterdayEndingObj = pivotGridControl2.GetCellValue(currentColumnIndex - 1, e.RowIndex);

                                object todayBeginningObj = e.Value;

                                // Safely convert both to decimals to avoid type mismatch crashes
                                decimal yesterdayEnding = yesterdayEndingObj == null ? 0 : Convert.ToDecimal(yesterdayEndingObj);
                                decimal todayBeginning = todayBeginningObj == null ? 0 : Convert.ToDecimal(todayBeginningObj);

                                // TRIGGER THE ANOMALY HIGHLIGHT!
                                if (yesterdayEnding != todayBeginning)
                                {
                                    // Paint the Beginning Qty cell RED with WHITE text to make it scream "ERROR"
                                    e.Appearance.BackColor = Color.Firebrick;
                                    e.Appearance.ForeColor = Color.White;
                                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                                }
                            }
                        }
                    }
                };

                // --- FONT OPTIMIZATION FOR REPORTING ---

                // 1. Data Cells: Clean, readable, standard size
                pivotGridControl2.Appearance.Cell.Font = new Font("Segoe UI", 9f, FontStyle.Regular);

                // 2. Column and Row Headers (The gray areas): Slightly larger and Bold
                pivotGridControl2.Appearance.HeaderArea.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);
                pivotGridControl2.Appearance.FieldValue.Font = new Font("Segoe UI", 8.75f, FontStyle.Bold);

                // 3. Grand Totals: Keep the same size as data, but make them Bold
                pivotGridControl2.Appearance.GrandTotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                pivotGridControl2.Appearance.TotalCell.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

                // Optional but highly recommended: Center the column headers for a cleaner look
                pivotGridControl2.Appearance.HeaderArea.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                // 5. SKINNING
                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                UserLookAndFeel.Default.SkinName = "Office 2019 Colorful";
            }
            catch (SqlException ee)
            {
                XtraMessageBox.Show("Database Error: " + ee.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Application Error: " + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                // 6. UNLOCK THE PIVOT GRID
                // This forces exactly ONE single redraw, making it lightning fast.
                pivotGridControl2.EndUpdate();
                pivotGridControl2.BestFit(pivotGridControl2.Fields["Description"]);

                // btnExecute.Enabled = true;
            }
        }

        // Helper method to keep your code clean
        private PivotGridField CreateDataField(string fieldName)
        {
            PivotGridField field = new PivotGridField(fieldName, PivotArea.DataArea);
            field.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            field.CellFormat.FormatString = "n2";
            return field;
        }

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            //pivotGridControl1.BeginUpdate();
            try
            {
                SqlCommand com = null;

                //com = new SqlCommand("select a.BranchName,b.ProductName,d.EffectivityDate,b.Qty,c.Description FROM dbo.TransferOrderDetails  as b INNER JOIN dbo.TransferOrderSummary as d ON d.PONumber=b.PONumber INNER JOIN dbo.Branches as a ON d.BranchCode=a.BranchCode INNER JOIN dbo.ProductCategory as c ON c.ProductCategoryID=SUBSTRING(b.ProductCode,1,2)  WHERE CAST(d.EffectivityDate as date)='" + dateEdit1.Text + "' order by c.Description", con);
                string query = "sp_GenerateInventoryDailyReport";
                com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbrcode", objbrcode.ToString());
                com.Parameters.AddWithValue("@datefrom", datefrom.Text);
                com.Parameters.AddWithValue("@dateto", dateto.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                 
             

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();

               
                pivotGridControl1.Fields.Clear();
                adapter.Fill(table);
                pivotGridControl1.DataSource = table;
                
                PivotGridField fielddescription = new PivotGridField("Category", PivotArea.RowArea);
                PivotGridField fieldprodname = new PivotGridField("Description", PivotArea.RowArea);
                fieldprodname.Caption = "PRODUCT NAME";

                PivotGridField fieldtransdate = new PivotGridField("TransactionDate", PivotArea.ColumnArea);
                fieldtransdate.Caption = "TRANSACTION DATE";

                PivotGridField fldbeginning= new PivotGridField("Beginning", PivotArea.DataArea);
                fldbeginning.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldbeginning.CellFormat.FormatString = "n2";

                PivotGridField fldsts = new PivotGridField("STSQtyRcvd", PivotArea.DataArea);
                fldsts.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldsts.CellFormat.FormatString = "n2";

                PivotGridField fldconvin = new PivotGridField("ConversionIN", PivotArea.DataArea);
                fldconvin.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldconvin.CellFormat.FormatString = "n2";

                PivotGridField fldtotalin = new PivotGridField("TOTALIN", PivotArea.DataArea);
                fldtotalin.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldtotalin.CellFormat.FormatString = "n2";

                PivotGridField fldconvout = new PivotGridField("ConversionOut", PivotArea.DataArea);
                fldconvout.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldconvout.CellFormat.FormatString = "n2";

                PivotGridField fldslsout = new PivotGridField("SalesOut", PivotArea.DataArea);
                fldslsout.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldslsout.CellFormat.FormatString = "n2";

                PivotGridField fldtotalout = new PivotGridField("TOTALOUT", PivotArea.DataArea);
                fldtotalout.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldtotalout.CellFormat.FormatString = "n2";

                PivotGridField fldending = new PivotGridField("EndingQty", PivotArea.DataArea);
                fldending.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fldending.CellFormat.FormatString = "n2";
                // Add the fields to the control's field collection.         
                pivotGridControl1.Fields.AddRange(new PivotGridField[] { fielddescription, fieldprodname, fieldtransdate, fldbeginning,fldsts,fldconvin,fldtotalin,fldconvout,fldslsout,fldtotalout,fldending });

                pivotGridControl1.OptionsView.ShowColumnGrandTotals = false;
                pivotGridControl1.OptionsView.ShowRowGrandTotals = false;

                fielddescription.AreaIndex = 0;
                fieldprodname.AreaIndex = 1;
                pivotGridControl1.BestFit(fieldprodname);

            
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
        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            if ((Login.assignedBranch == "888" && String.IsNullOrEmpty(txtbranch.Text) ) || String.IsNullOrEmpty(datefrom.Text) || String.IsNullOrEmpty(dateto.Text))
            {
                XtraMessageBox.Show("Field must not Empty");
                return;
            }
            else
            {
                simpleButton1.Enabled = false;
                Cursor = Cursors.WaitCursor;
                await ExecuteAsync();
                Cursor = Cursors.Default;
                simpleButton1.Enabled = true;
            }
        }

        private void InventoryDailyActivityPivot_Load(object sender, EventArgs e)
        {
            //xtraTabPage2.PageVisible = false;
            if (Login.assignedBranch == "888")
            {
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM dbo.Branches order by BranchCode", txtbranch, "BranchName", "BranchName");
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM dbo.Branches order by BranchCode", txtbranchmonthly, "BranchName", "BranchName");
            }
            else
            {
                labelbranch.Visible = false;
                txtbranch.Visible = false;
                //monthyl
                labelControl4.Visible = false;
                txtbranchmonthly.Visible = false;
                globalbrcode = Login.assignedBranch;
            }
         }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            objbrcode = SearchLookUpClass.getSingleValue(txtbranch, "BranchCode");
            globalbrcode = objbrcode.ToString();
        }

        private async void simpleButton4_Click(object sender, EventArgs e)
        {// 1. Validate selections
           
            if (string.IsNullOrEmpty(cmbFromMonth.Text) || string.IsNullOrEmpty(cmbFromYear.Text) ||
                string.IsNullOrEmpty(cmbToMonth.Text) || string.IsNullOrEmpty(cmbToYear.Text))
            {
                BigAlert.Show("Missing Filter", "Please select a full date range.", MessageBoxIcon.Warning);
                return;
            }
            if ((Login.assignedBranch == "888" && String.IsNullOrEmpty(txtbranchmonthly.Text)) )
            {
                XtraMessageBox.Show("Field must not Empty");
                return;
            }
            else
            {
                simpleButton4.Enabled = false;
                Cursor = Cursors.WaitCursor;
                await ExecuteAsyncMonthly();
                Cursor = Cursors.Default;
                simpleButton4.Enabled = true;
            }
        }

        private void txtbranchmonthly_EditValueChanged(object sender, EventArgs e)
        {
            objbrcode = SearchLookUpClass.getSingleValue(txtbranchmonthly, "BranchCode");
            globalbrcode = objbrcode.ToString();
        }
    }
}