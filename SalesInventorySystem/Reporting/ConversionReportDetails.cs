using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Reporting
{
    public partial class ConversionReportDetails : Form
    {
        public ConversionReportDetails()
        {
            InitializeComponent();
        }

        Double getTotal(string value)
        {
           
            double qty = 0.0;
            for(int i=0;i<=gridView1.RowCount-1;i++)
            {
                qty += Convert.ToDouble(gridView1.GetRowCellValue(i, value));
            }
            return qty;
        }

        private void ConversionReportDetails_Load(object sender, EventArgs e)
        {
            //display();
            txtconversiontype.Text = ConversionReports.contype;
            txtqtyconverted.Text = getTotal("Quantity").ToString();
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if(ConversionReports.contype == "OneToMany")
                {
                    txtsrcqty.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SourceQty").ToString();
                    gridView1.OptionsView.AllowCellMerge = true;
                    if (gridView1.Columns[i].FieldName == "BranchCode" || gridView1.Columns[i].FieldName == "ConID" || gridView1.Columns[i].FieldName == "SourceDescription" || gridView1.Columns[i].FieldName == "SourceQty" || gridView1.Columns[i].FieldName == "SourceCost")
                        gridView1.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    else
                        gridView1.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    txtsrcqty.Text = getTotal("SourceQty").ToString();
                    gridView1.OptionsView.AllowCellMerge = true;
                    if (gridView1.Columns[i].FieldName == "BranchCode" || gridView1.Columns[i].FieldName == "ConID" || gridView1.Columns[i].FieldName == "Description" || gridView1.Columns[i].FieldName == "Variance" || gridView1.Columns[i].FieldName == "Cost" || gridView1.Columns[i].FieldName == "ActualQty")
                        gridView1.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True;
                    else
                        gridView1.Columns[i].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                    
                }
            }
            gridView1.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0:n2}");
            if (ConversionReports.contype == "OneToMany")
                txtvariance.Text = getDifference().ToString();
            else
                txtvariance.Text = getDifference().ToString();
        }

        void display()
        {
            Database.display("SELECT * FROM view_ConversionDetails WHERE ConID='" + Reporting.ConversionReports.conid + "'", gridControl1,gridView1);
            
        }

        double getDifference()
        {
            double diff = 0.0,totqty=0.0,totactqty=0.0;
            if(txtconversiontype.Text=="OneToMany")
            {
                diff = Convert.ToDouble(txtsrcqty.Text) - Convert.ToDouble(txtqtyconverted.Text);
            }
            else
            {
                for(int i=0;i<=gridView1.RowCount-1;i++)
                {
                    totqty+= Convert.ToDouble(gridView1.GetRowCellValue(i, "Quantity").ToString());
                    //totactqty = Convert.ToDouble(gridView1.GetRowCellValue(i, "ActualQty").ToString());
                    //variance += Convert.ToDouble(gridView1.GetRowCellValue(i, "Variance").ToString());
                }
                diff = totqty-totactqty;
            }
            return diff;
        }

        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            //if (ConversionReports.contype == "ManyToOne")
            //{
            //    if (e.Column == gridView1.Columns["Quantity"])
            //    {
            //        double value1 = Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle1, e.Column));
            //        double value2 = Convert.ToDouble(gridView1.GetRowCellValue(e.RowHandle2, e.Column));
            //        if (value1 == value2)
            //        {
            //            e.Merge = true;
            //            e.Handled = true;
            //        }
            //    }
            //}
            //GridView view = sender as GridView;
            //try
            //{
            //    if ((e.Column.FieldName == "SourceQty"))
            //    {
            //        int value1 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle1, e.Column));
            //        int value2 = Convert.ToInt32(view.GetRowCellValue(e.RowHandle2, e.Column));

            //        e.Merge = (value1 == value2);
            //        e.Handled = true;
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
           
            //double total = 0.0;
            //if (ConversionReports.contype == "ManyToOne")
            //{
            //    if (e.Column == gridView1.Columns["Quantity"])
            //    {
            //        total += Convert.ToDouble(e.CellValue);
            //        e.DisplayText = total.ToString();
            //    }

            //}
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Column.FieldName == "SourceQty")
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }
            else if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Blue;
            }
            else if (e.Column.FieldName == "Variance")
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Red;
            }
            else if (e.Column.FieldName == "CostAdjustment")
            {
                e.Appearance.Font = new System.Drawing.Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.Green;
            }
        }

        private void exporttoxls_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\";
            string filename = "ConversionReport_" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"BranchCode").ToString() + "_" + Reporting.ConversionReports.conid + ".xls";
            string file = filepath + filename;
            gridControl1.ExportToXls(file);
            XtraMessageBox.Show("Export Success");
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "CostAdjustment")
                e.Cancel = true;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double costadj = 0.0, totalamount = 0.0,totalcost=0.0,cost=0.0;
            
            if (e.Column.FieldName == "CostAdjustment")
            {
                costadj = Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CostAdjustment").ToString());
                totalamount = Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalAmount").ToString());
                totalcost = costadj + totalamount;
                cost = totalcost / Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Quantity").ToString());
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalCost", totalcost.ToString());
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Cost", cost.ToString());
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<=gridView1.RowCount-1;i++)
            {
                Database.ExecuteQuery("Update ConversionDetails set FinalRatio='" + gridView1.GetRowCellValue(i, "CostAdjustment").ToString() + "',TotalCost='" + gridView1.GetRowCellValue(i, "TotalCost").ToString() + "',Cost='" + gridView1.GetRowCellValue(i, "Cost").ToString() + "' WHERE ConID='" + Reporting.ConversionReports.conid + "'","Successfully Updated!..");
                this.Dispose();
            }
        }

        //private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.ColumnIndex == 3)
        //    {
        //        e.CellStyle.BackColor = Color.LightGreen;
        //    }
        //    if (e.ColumnIndex == 6)
        //    {
        //        e.CellStyle.BackColor = Color.LightBlue;
        //    }
        //    if (e.ColumnIndex == 7)
        //    {
        //        e.CellStyle.BackColor = Color.Red;
        //    }
        //}
    }
}
