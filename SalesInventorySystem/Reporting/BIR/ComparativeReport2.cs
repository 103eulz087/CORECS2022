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

namespace SalesInventorySystem.Reporting.BIR
{
    public partial class ComparativeReport2 : DevExpress.XtraEditors.XtraForm
    {
        PivotGridField fieldbranchcode;
        PivotGridField fieldbranchname;
        PivotGridField fieldnameofbranchwithcode;
        PivotGridField fielddate;
        PivotGridField fieldnet;
        public ComparativeReport2()
        {
            InitializeComponent();
        }
        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            pivotGridControl1.BeginUpdate();
            try
            {
                SqlCommand com = null;
                string query = "spr_comparativeNetReport2";
                //com = new SqlCommand("exec sp_IS2 '01/01/2018','12/31/2018' ", con);
                com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdatefrom", txtdatefrom.Text);
                com.Parameters.AddWithValue("@parmdateto", txtdateto.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = query;
                //com = new SqlCommand("exec sp_IS2 '" + txtdatefrom.Text + "','" + txtdateto.Text + "' ", con);
                com.ExecuteNonQuery();

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();

                //pivotGridControl1.DataSource = null;
                //table.Rows.Clear();
                //table.Columns.Clear();
                pivotGridControl1.Fields.Clear();
                pivotGridControl1.DataSource = null;
                adapter.Fill(table);
                pivotGridControl1.DataSource = table;
                //pivotGridControl1.RetrieveFields();
                //pivotGridControl1.RefreshData();
                // Create a row Pivot Grid Control field bound to the Country datasource field.
                //fieldbranchcode = new PivotGridField("BranchCode", PivotArea.RowArea);
                //fieldnameofbranchwithcode = new PivotGridField("NameOfBranch", PivotArea.RowArea);
                fieldnameofbranchwithcode = new PivotGridField("NameOfBranch", PivotArea.RowArea);
                fieldnameofbranchwithcode.Caption = "NameOfBranch";

                fielddate = new PivotGridField("DateExecute", PivotArea.ColumnArea);
                fielddate.Caption = "DateExecute";

                fieldnet = new PivotGridField("TotalNet", PivotArea.DataArea);
                fieldnet.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fieldnet.CellFormat.FormatString = "n2";
                // Add the fields to the control's field collection.         
                pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldnameofbranchwithcode, fielddate, fieldnet });

                fieldnameofbranchwithcode.AreaIndex = 0;
                //fieldacctcode.AreaIndex = 1;
                //fielddesc.AreaIndex = 2;

                pivotGridControl1.BestFit(fieldnameofbranchwithcode);

                UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;
                UserLookAndFeel.Default.SkinName = "Visual Studio 2013 Blue";
            }
            catch (SqlException ee)
            {
                XtraMessageBox.Show(ee.Message.ToString());
            }
            finally
            {
                pivotGridControl1.EndUpdate();
                con.Close();
            }
        }
        void exporttoexcel(PivotGridControl view, string title)
        {

            string filepath = "C:\\MyFiles\\";
            Classes.Utilities.createDirectoryFolder(filepath);
            string filename = title + ".xls";
            string file = filepath + filename;
            view.ExportToXls(file);
            XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            execute();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string filename = "COMPARATIVESALESREP_" + txtdatefrom.Text.Replace(@"/", "-") + "_" + txtdateto.Text.Replace(@"/", "-");
            exporttoexcel(pivotGridControl1, filename);
        }
    }
}