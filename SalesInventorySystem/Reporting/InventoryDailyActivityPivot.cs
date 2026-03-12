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

namespace SalesInventorySystem.Reporting
{
    public partial class InventoryDailyActivityPivot : DevExpress.XtraEditors.XtraForm
    {
        object objbrcode = null;
        string globalbrcode = "";
        public InventoryDailyActivityPivot()
        {
            InitializeComponent();
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
                        com.Parameters.AddWithValue("@parmbrcode", objbrcode.ToString());
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
                PivotGridField fldconvin = CreateDataField("ConversionIN");
                PivotGridField fldtotalin = CreateDataField("TOTALIN");
                PivotGridField fldconvout = CreateDataField("ConversionOut");
                PivotGridField fldslsout = CreateDataField("SalesOut");
                PivotGridField fldtotalout = CreateDataField("TOTALOUT");
                PivotGridField fldending = CreateDataField("EndingQty");

                // Add all fields at once
                pivotGridControl1.Fields.AddRange(new PivotGridField[] {
            fielddescription, fieldprodname, fieldtransdate,
            fldbeginning, fldsts, fldconvin, fldtotalin,
            fldconvout, fldslsout, fldtotalout, fldending
        });

                // 5. SKINNING
                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";
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
            if (String.IsNullOrEmpty(txtbranch.Text) || String.IsNullOrEmpty(datefrom.Text) || String.IsNullOrEmpty(dateto.Text))
            {
                XtraMessageBox.Show("Field must not Empty");
                return;
            }
            else
            {
                await ExecuteAsync(); 
            }
        }

        private void InventoryDailyActivityPivot_Load(object sender, EventArgs e)
        {
            if (Login.assignedBranch == "888")
            {
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM dbo.Branches order by BranchCode", txtbranch, "BranchName", "BranchName");
            }
            else
            {
                labelbranch.Visible = false;
                txtbranch.Visible = false;
                globalbrcode = Login.assignedBranch;
            }
         }

        private void txtbranch_EditValueChanged(object sender, EventArgs e)
        {
            objbrcode = SearchLookUpClass.getSingleValue(txtbranch, "BranchCode");
            globalbrcode = objbrcode.ToString();
        }
    }
}