using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.Reporting
{
    public partial class DeliveryReportsFrm : Form
    {
        public static string ponum,devno;
        public static string branchcode, effectivitydate, requestedby;
        public DeliveryReportsFrm()
        {
            InitializeComponent();
        }

      

        void display()
        {
            
                string status = radfordeliv.Checked ? "FOR DELIVERY" : "DELIVERED";

                            string masterQuery = @"
                    SELECT *
                    FROM view_DeliveryReportSummary
                    WHERE DateAdded >= @DateFrom
                      AND DateAdded <  DATEADD(day,1,@DateTo)
                      AND BranchCode = @Branch
                      AND Status = @Status";

                            string detailQuery = @"
                    SELECT d.*
                    FROM view_DeliveryReportDetails d
                    WHERE EXISTS
                    (
                        SELECT 1
                        FROM view_DeliveryReportSummary s
                        WHERE s.PONumber = d.PONumber
                          AND s.DateAdded >= @DateFrom
                          AND s.DateAdded <  DATEADD(day,1,@DateTo)
                          AND s.BranchCode = @Branch
                          AND s.Status = @Status
                    )";

                            var masterParams = new[]
                            {
                    new SqlParameter("@DateFrom", dateFrom.DateTime),
                    new SqlParameter("@DateTo", dateTo.DateTime),
                    new SqlParameter("@Branch", searchLookUpEdit1.Text),
                    new SqlParameter("@Status", status)
                };

                            var detailParams = new[]
                            {
                    new SqlParameter("@DateFrom", dateFrom.DateTime),
                    new SqlParameter("@DateTo", dateTo.DateTime),
                    new SqlParameter("@Branch", searchLookUpEdit1.Text),
                    new SqlParameter("@Status", status)
                };

                            Database.GridMasterDetail(
                                masterQuery,
                                detailQuery,
                                "Master",
                                "Detail",
                                "PONumber",
                                "PONumber",
                                "DeliveryDetails",
                                gridControl1,
                                masterParams,
                                detailParams
                            );

        }

        private void DeliveryReportsFrm_Load(object sender, EventArgs e)
        {
            displayBranches();
        }
        void analyze(string spname, string pono, GridControl cont, GridView view)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            cont.BeginUpdate();
            
            try
            {
                //spname = "spview_SalesInvoice";
                string sp = spname;
                SqlCommand com = new SqlCommand(sp, con);
                com.Parameters.AddWithValue("@parmpono", pono);

                com.CommandType = CommandType.StoredProcedure;
                com.CommandTimeout = 3600;
                com.CommandText = sp;
                //com.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                view.Columns.Clear();
                cont.DataSource = null;
                adapter.Fill(table);
                cont.DataSource = table;
                view.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cont.EndUpdate();
                con.Close();
            }
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ponum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            devno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DeliveryNo").ToString();
            Reporting.DeliveryDetailsReportsFrm devrepfrm = new DeliveryDetailsReportsFrm();
            analyze("spview_SalesInvoice", ponum, devrepfrm.gridControl1, devrepfrm.gridView1);
          
            devrepfrm.Show();
            devrepfrm.txtpo.Text = ponum;
            devrepfrm.txtbrcode.Text = searchLookUpEdit1.Text;
            devrepfrm.txteffectivitydate.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString();

            //analyze("spview_SalesInvoice", ponum, devrepfrm.gridControl1, devrepfrm.gridView1);
            //Database.display("SELECT COUNT(ProductName) as TotalBox,SUM(QtyDelivered) as QtyDelivered,ProductName,'' as UnitPrice,'' as Amount FROM view_DeliveryDetailsReportSummary WHERE PONumber='" + ponum + "' GROUP BY ProductName", devrepfrm.gridControl1, devrepfrm.gridView1);
           
            GridGroupSummaryItem ite11 = new GridGroupSummaryItem();
            ite11.FieldName = "Quantity";
            ite11.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            ite11.ShowInGroupColumnFooter = devrepfrm.gridView1.Columns["Quantity"];
            devrepfrm.gridView1.GroupSummary.Add(ite11);
            devrepfrm.gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        void displayBranches()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", searchLookUpEdit1, "BranchCode", "BranchCode");
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
        void exporttoexcel(GridView view, string title)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {

                string filepath = "C:\\MyFiles\\";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles/folder");
            }
        }
        private void btnforapprovalsalesorderexcel_Click(object sender, EventArgs e)
        {
            string filename = "DeliveryReportSummary" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            HelperFunction.exporttoexcel(gridView1, filename);
        }

        void showSTSDetails()
        {
            ponum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            DeliverySummaryReports delivsum = new DeliverySummaryReports();
            Database.display($"SELECT * FROM dbo.funcview_DeliveryDetailsSummary('{ponum}')", delivsum.gridControl1, delivsum.gridView1);
            delivsum.ShowDialog(this);
            //branchcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            //effectivitydate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString();
            //requestedby = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PreparedBy").ToString();

            //ponum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            //StocksOrder devrepfrm = new StocksOrder();
            //devrepfrm.txtpono.Text = ponum;
            //devrepfrm.txtbranch.Text = branchcode;
            //devrepfrm.txteffectivitydate.Text = Convert.ToDateTime(effectivitydate).ToShortDateString();
            //devrepfrm.txtpreparedby.Text = requestedby;
            //analyze("spr_STSSummary", ponum, devrepfrm.gridControl1, devrepfrm.gridView1);
            //devrepfrm.Show();

            //GridView view = devrepfrm.gridControl1.FocusedView as GridView;
            //view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
            //    new GridColumnSortInfo(view.Columns["Description"],DevExpress.Data.ColumnSortOrder.Ascending)
            //    }, 1);
            //devrepfrm.gridView1.ExpandAllGroups();
        }

        private void showSTSDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSTSDetails();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}
