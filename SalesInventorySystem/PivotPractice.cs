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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem
{
    public partial class PivotPractice : DevExpress.XtraEditors.XtraForm
    {
          // Create a row Pivot Grid Control field bound to the Country datasource field.
                PivotGridField fieldbranchcode;
                PivotGridField fieldacctcode;
                PivotGridField fielddesc;
                PivotGridField fielddate;
                PivotGridField fieldEndingBalance;

        //DataGridView grid;
        GridControl gridcontrol;
        GridView view;
        public PivotPractice()
        {
            InitializeComponent();
        }

        private void PivotPractice_Load(object sender, EventArgs e)
        {
          
        }
        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            pivotGridControl1.BeginUpdate();
            try
            {
                SqlCommand com = null;
                string query = "sp_IS2";
                //com = new SqlCommand("exec sp_IS2 '01/01/2018','12/31/2018' ", con);
                com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdatefrom", txtdatefrom.Text);
                com.Parameters.AddWithValue("@parmdateto", txtdateto.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout=3600;
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
                 fieldbranchcode = new PivotGridField("BranchCode", PivotArea.RowArea);
                 fieldacctcode = new PivotGridField("AccountCode", PivotArea.RowArea);
                 fielddesc = new PivotGridField("Description", PivotArea.RowArea);
                fielddesc.Caption = "Description";

                 fielddate = new PivotGridField("PostingDate", PivotArea.ColumnArea);
                fielddate.Caption = "PostingDate";

                 fieldEndingBalance = new PivotGridField("EndingBalance", PivotArea.DataArea);
                fieldEndingBalance.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                fieldEndingBalance.CellFormat.FormatString = "n2";
                // Add the fields to the control's field collection.         
                pivotGridControl1.Fields.AddRange(new PivotGridField[] { fieldbranchcode, fielddesc, fieldacctcode, fielddate, fieldEndingBalance });

                fieldbranchcode.AreaIndex = 0;
                fieldacctcode.AreaIndex = 1;
                fielddesc.AreaIndex = 2;

                pivotGridControl1.BestFit(fielddesc);

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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            execute();
        }
        private void GetFieldValues(int columnIndex, int rowIndex, string columnFieldName, string rowFieldName)
        {

            PivotGridField columnField = pivotGridControl1.Fields[columnFieldName];
            if (columnField != null)
            {
                for (int i = 0; i <= columnField.AreaIndex; i++)
                {
                    PivotGridField cfield = pivotGridControl1.GetFieldByArea(PivotArea.ColumnArea, i);
                    if (cfield == null) continue;
                    //Response.Write(cfield.ToString() + " : " + pivotGridControl1.GetFieldValue(cfield, columnIndex) + "<br//>");
                    XtraMessageBox.Show(cfield.ToString() + " : " + pivotGridControl1.GetFieldValue(cfield, i) + "<br//>");
                }
            }
            //PivotGridField rowField = pivotGridControl1.Fields[rowFieldName];
            //if (rowField != null)
            //{
            //    for (int j = 0; j <= rowField.AreaIndex; j++)
            //    {
            //        PivotGridField rfield = pivotGridControl1.GetFieldByArea(PivotArea.RowArea, j);
            //        if (rfield == null) continue;
            //        Response.Write(rfield.ToString() + " : " + pivotGridControl1.GetFieldValue(rfield, rowIndex) + "<br//>");
            //    }
            //}
        }

        private void pivotGridControl1_DoubleClick(object sender, EventArgs e)
        {
            
            //if (e.ColumnField != null)
            //{
            //    var value = _pivotGrid.GetFieldValue(e.ColumnField, e.ColumnIndex);
            //    var dataColumn = _dataSource.GetColumn(e.ColumnField.Name);
            //    data.Add(dataColumn.Name, value);
            //}
            //if (e.RowField != null)
            //{
            //    var value = _pivotGrid.GetFieldValue(e.RowField, e.RowFieldIndex);
            //    var dataColumn = _dataSource.GetColumn(e.RowField.Name);
            //    data.Add(dataColumn.Name, value);

            //}
            //XtraMessageBox.Show(departmentValue.ToString());
            
            //PivotGridField rowfield = pivotGridControl1.Fields[1];
            //if (rowfield != null)
            //{
            //    PivotGridField rfield = pivotGridControl1.GetFieldByArea(PivotArea.RowArea,1);
            //    XtraMessageBox.Show(rfield.ToString());
            //}
            
        }

        private void pivotGridControl1_CellDoubleClick(object sender, PivotCellEventArgs e)
        {
            //// Create a new form.
            Form form = new Form();
            form.Text = "Records";
            // Place a DataGrid control on the form.
            gridcontrol = new GridControl();
            view = new GridView(gridcontrol);
            gridcontrol.Parent = form;
            gridcontrol.Dock = DockStyle.Fill;
            gridcontrol.MainView = view;
            view.PopulateColumns();
            view.OptionsBehavior.Editable = false;
            view.OptionsBehavior.ReadOnly = true;
          
            //grid = new DataGridView();
            //grid.Parent = form;
            //grid.Dock = DockStyle.Fill;

            // Get the recrd set associated with the current cell and bind it to the grid.
            gridcontrol.DataSource = e.CreateDrillDownDataSource();
            gridcontrol.MouseUp += gridcontrol_MouseUp;

          
            //grid.DataSource = e.CreateDrillDownDataSource();
            //string rowhandle = grid.Rows[1].Cells[0].Value.ToString();
            //grid.MouseUp += grid_MouseUp;
            //XtraMessageBox.Show(rowhandle);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Bounds = new Rectangle(100, 100, 600, 150);
            
            // Display the form.
            form.ShowDialog();
            form.Dispose();
            
            //row values
            //PivotDrillDownDataSource ds = e.CreateDrillDownDataSource();
            //string empDisplayName = Convert.ToString(ds[0]["AccountCode"]);
            ////XtraMessageBox.Show(empDisplayName);

            //XtraXtraMessageBox.Show(e.GetFieldValue(e.ColumnField).ToString());
            ////XtraXtraMessageBox.Show(e.GetFieldValue(e.RowField).ToString());
            //XtraXtraMessageBox.Show(empDisplayName);

        }

        void gridcontrol_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridcontrol, e.Location);
                if (view.GetRowCellValue(view.FocusedRowHandle, "AccountCode").ToString().Contains("4010") || view.GetRowCellValue(view.FocusedRowHandle, "AccountCode").ToString().Equals("501") || view.GetRowCellValue(view.FocusedRowHandle, "AccountCode").ToString().Equals("502"))
                {
                    contextMenuStrip1.Items[0].Visible = true;
                }
                else
                {
                    contextMenuStrip1.Items[0].Visible = false;
                }
            }
        }

        double getVatExemptSales(GridView viewer)
        { 
            double amount = 0.0;
            for (int i = 0; i <= viewer.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(viewer.GetRowCellValue(i, "isVat").ToString()) == false)
                {
                    amount += Convert.ToDouble(viewer.GetRowCellValue(i, "TotalAmount").ToString());
                }
            }
            return amount;
        }
        double getVatableSales(GridView viewer)
        {
            double amount = 0.0;
            for (int i = 0; i <= viewer.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(viewer.GetRowCellValue(i, "isVat").ToString()) == true)
                {
                    amount += Convert.ToDouble(viewer.GetRowCellValue(i, "TotalAmount").ToString());
                }
            }
            return amount;
        }
        //Show Sales Details

        void showSalesDetails()
        {
            string acctcode = view.GetRowCellValue(view.FocusedRowHandle, "AccountCode").ToString();
            string branchcode = view.GetRowCellValue(view.FocusedRowHandle, "BranchCode").ToString();
            string postingdate = view.GetRowCellValue(view.FocusedRowHandle, "PostingDate").ToString();

            var myDate = Convert.ToDateTime(postingdate);
            var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
            string startdate = startOfMonth.ToShortDateString();


            Reporting.AccountingReportingDetails actrec = new Reporting.AccountingReportingDetails();
            actrec.Show();
            actrec.lblsalesdate.Text = startdate + " to " + Convert.ToDateTime(postingdate).ToShortDateString();
            actrec.lblBranch.Text = branchcode;

            if (acctcode == "40101") //SALES VAT EXEMPT
            {
                Database.display("select ProductCode,Description,SUM(TotalAmount) as TotalAmount,isVat FROM BatchSalesDetails WHERE BranchCode='" + branchcode.Substring(0, 3) + "' AND CAST(DateOrder as date) between '" + startdate + "' and '" + postingdate + "' and isConfirmed=1  and isVoid=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat=0 GROUP BY ProductCode,Description,isVat", actrec.gridControl1, actrec.gridView1);
                actrec.panelsalesvatex.Visible = true;
                actrec.panelControlsalesvat.Visible = false;
                Classes.DevXGridViewSettings.ShowFooterTotal(actrec.gridView1, "TotalAmount");
                actrec.txtvatexemptsales.Text = HelperFunction.convertToNumericFormat(getVatExemptSales(actrec.gridView1));
            }
            else if (acctcode == "40102")//SALES VAT
            {
                Database.display("select ProductCode,Description,SUM(TotalAmount) as TotalAmount,isVat FROM BatchSalesDetails WHERE BranchCode='" + branchcode.Substring(0, 3) + "' AND CAST(DateOrder as date) between '" + startdate + "' and '" + postingdate + "' and isConfirmed=1  and isVoid=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat=1 GROUP BY ProductCode,Description,isVat", actrec.gridControl1, actrec.gridView1);
                actrec.panelsalesvatex.Visible = false;
                actrec.panelControlsalesvat.Visible = true;
                Classes.DevXGridViewSettings.ShowFooterTotal(actrec.gridView1, "TotalAmount");
                actrec.txtvatablesales.Text = HelperFunction.convertToNumericFormat(getVatableSales(actrec.gridView1));
                actrec.txtnetofvatsales.Text = HelperFunction.convertToNumericFormat(getVatableSales(actrec.gridView1) / 1.12);
            }
            else if (acctcode == "501") //COST VAT EXEMPT
            {
                //Database.display("select ProductCode,Description,QtySold,Cost,(QtySold*Cost) as TotalCost,isVat FROM BatchSalesDetails WHERE BranchCode='" + branchcode.Substring(0, 3) + "' AND CAST(DateOrder as date) between '" + startdate + "' and '" + postingdate + "' and isConfirmed=1  and isVoid=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat=0 GROUP BY ProductCode,Description,isVat", actrec.gridControl1, actrec.gridView1);
                Database.display("select ProductCode,Description,SUM(QtySold) as QtySold,Cost,FORMAT(SUM(QtySold)*Cost,'N', 'en-us') as TotalCost,isVat  FROM BatchSalesDetails WHERE BranchCode='" + branchcode.Substring(0, 3) + "' AND CAST(DateOrder as date) between '" + startdate + "' and '" + postingdate + "' and isConfirmed=1  and isVoid=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat=0 group by ProductCode,Description,Cost,isVat", actrec.gridControl1, actrec.gridView1);
                actrec.panelsalesvatex.Visible = false;
                actrec.panelControlsalesvat.Visible = false;
                Classes.DevXGridViewSettings.ShowFooterTotal(actrec.gridView1, "TotalCost");
            }
            else if (acctcode == "502")//COST VAT
            {
                //Database.display("select ProductCode,Description,QtySold,Cost,(QtySold*Cost) as TotalCost,isVat FROM BatchSalesDetails WHERE BranchCode='" + branchcode.Substring(0, 3) + "' AND CAST(DateOrder as date) between '" + startdate + "' and '" + postingdate + "' and isConfirmed=1  and isVoid=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat=1 GROUP BY ProductCode,Description,isVat", actrec.gridControl1, actrec.gridView1);
                Database.display("select ProductCode,Description,SUM(QtySold) as QtySold,Cost,FORMAT(SUM(QtySold)*Cost,'N', 'en-us') as TotalCost,isVat  FROM BatchSalesDetails WHERE BranchCode='" + branchcode.Substring(0, 3) + "' AND CAST(DateOrder as date) between '" + startdate + "' and '" + postingdate + "' and isConfirmed=1  and isVoid=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat=1 group by ProductCode,Description,Cost,isVat", actrec.gridControl1, actrec.gridView1);
                actrec.panelsalesvatex.Visible = false;
                actrec.panelControlsalesvat.Visible = false;
                Classes.DevXGridViewSettings.ShowFooterTotal(actrec.gridView1, "TotalCost");
            }

            //Database.display("select ProductCode,Description,SUM(TotalAmount) as TotalAmount,isVat FROM BatchSalesDetails WHERE BranchCode='" + branchcode.Substring(0, 3) + "' AND CAST(DateOrder as date) between '" + startdate + "' and '" + postingdate + "' and isConfirmed=1  and isVoid=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' GROUP BY ProductCode,Description,isVat", actrec.gridControl1, actrec.gridView1);
            Classes.DevXGridViewSettings.ShowFooterCountTotal(actrec.gridView1, "ProductCode");
            

           

        }
        private void sampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSalesDetails();
        }

        void showReconDetails()
        {
            string acctcode = view.GetRowCellValue(view.FocusedRowHandle, "AccountCode").ToString();
            string branchcode = view.GetRowCellValue(view.FocusedRowHandle, "BranchCode").ToString();
            string postingdate = view.GetRowCellValue(view.FocusedRowHandle, "PostingDate").ToString();

            var myDate = Convert.ToDateTime(postingdate);
            var startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
            string startdate = startOfMonth.ToShortDateString();

            Reporting.AccountingReportingDetails actrec = new Reporting.AccountingReportingDetails();
            actrec.Show();
            actrec.lblsalesdate.Text = startdate + " to " + Convert.ToDateTime(postingdate).ToShortDateString();
            actrec.lblBranch.Text = branchcode;
            actrec.panelsalesvatex.Visible = false;
            Database.display("select TicketDate,TicketNumber,ReferenceNumber,Owner,Particulars,Debit,Credit FROM view_AccountReconSample WHERE AccountCode='" + acctcode + "' AND CAST(TicketDate as date) between '" + startdate + "' and '" + postingdate + "' and BranchCode='" + branchcode.Substring(0, 3) + "' ", actrec.gridControl1, actrec.gridView1);
            Classes.DevXGridViewSettings.ShowFooterCountTotal(actrec.gridView1, "TicketDate");
            Classes.DevXGridViewSettings.ShowFooterTotal(actrec.gridView1, "Debit");
            Classes.DevXGridViewSettings.ShowFooterTotal(actrec.gridView1, "Credit");
        }

        private void showReconDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showReconDetails();
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
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string filename = "INCOME_STATEMENT_" + txtdatefrom.Text.Replace(@"/", "-") + "_" + txtdateto.Text.Replace(@"/", "-");
            exporttoexcel(pivotGridControl1, filename);
        }

        private void pivotGridControl1_CustomAppearance(object sender, PivotCustomAppearanceEventArgs e)
        {
            //if (Convert.ToInt32(e.GetFieldValue(fieldYear)) == 2011)
            //    e.Appearance.BackColor2 = Color.GreenYellow;
            if (Convert.ToString(e.GetFieldValue(fieldacctcode)) == "4" || Convert.ToString(e.GetFieldValue(fieldacctcode)) == "5" || Convert.ToString(e.GetFieldValue(fieldacctcode)) == "6")
            {
                e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            } 
            if (Convert.ToString(e.GetCellValue(fielddesc)) == "GROSS PROFIT")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            } 
            if (Convert.ToString(e.GetFieldValue(fielddesc)) == "GROSS PROFIT")
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            } 
            if (Convert.ToString(e.GetFieldValue(fielddesc)) == "NET INCOME")
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
                
                //if (e.Value != null && (string)e.Value == "GROSS PROFIT")
                //{
                //    e.Appearance.BackColor = Color.Yellow;
                //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                //}
            
        }

        private void pivotGridControl1_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e)
        {
            //if (e.ValueType == PivotGridValueType.)
            //{
            //    if (e.IsColumn)
            //        e.DisplayText = "*Custom Column Grand Total*";
            //    else
            //        e.DisplayText = "*Custom Row Grand Total*";
            //}
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}