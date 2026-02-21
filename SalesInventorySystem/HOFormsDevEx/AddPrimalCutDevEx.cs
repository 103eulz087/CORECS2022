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
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class AddPrimalCutDevEx : DevExpress.XtraEditors.XtraForm
    {
        public AddPrimalCutDevEx()
        {
            InitializeComponent();
        }

        private void AddPrimalCutDevEx_Load(object sender, EventArgs e)
        {
            btnsave.Enabled = false;
        }

        void populateMasterList()
        {
            bool isExist = false;
            isExist = Database.checkifExist($"SELECT 1 FROM dbo.Inventory WHERE Branch='{Login.assignedBranch}' AND BatchCode='{txtbatchcodeno.Text}'");
            if (isExist)
            {
                Database.display($"SELECT * FROM dbo.funcview_PrimalCutMaster('{Login.assignedBranch}','{txtbatchcodeno.Text}')", gridControlMaster, gridViewMaster);
                populateDetails();
            }
            else
            {
                XtraMessageBox.Show("No Records Found!..");
                return;
            }
        }

        void populateDetails()
        {
            Database.display($"SELECT * FROM dbo.view_PrimalCutPartsForConversion", gridControlDetail, gridViewDetail);
        }

        private void btnextract_Click(object sender, EventArgs e)
        {
            bool bathcodeAlreadyProcess = Database.checkifExist($"SELECT 1 FROM dbo.TempInventoryPrimal WHERE isDone=1 AND BatchCode='{txtbatchcodeno.Text}'");
            if(String.IsNullOrEmpty(txtbatchcodeno.Text))
            {
                XtraMessageBox.Show("BatchCode must not Empty");
                return;
            }
            else if(bathcodeAlreadyProcess)
            {
                XtraMessageBox.Show("This BatchCode is Already Processed!..");
                return;
            }
            else
            {
                populateMasterList();
                btnsave.Enabled = true;
            }
        }

        private void gridViewDetail_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Qty")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
        }

        private void gridViewDetail_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Qty")
                e.Cancel = true;
        }

        private void gridViewDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal quantity;
            string strquantity;
            string productcode;
            string barcode;
            if (e.Column.FieldName == "Qty")
            {
                quantity = Decimal.Parse(gridViewDetail.GetRowCellValue(gridViewDetail.FocusedRowHandle,"Qty").ToString());
                strquantity = String.Format("{0:00.000}", quantity);
                productcode = gridViewDetail.GetRowCellValue(gridViewDetail.FocusedRowHandle, "ProductCode").ToString();
                 barcode = Database.getSingleResultSet($"SELECT dbo.func_GeneratePrimalCutBarcode" +
               $"('{Login.assignedBranch}','{txtbatchcodeno.Text}','{productcode}','{strquantity}') ");

                gridViewDetail.SetRowCellValue(gridViewDetail.FocusedRowHandle, "Barcode", barcode);
                 
            }
        }

        void spConfirm()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                //string query = "spu_postInventory";
                string query = "sp_CommitPrimalCutConversion";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbatchcode", txtbatchcodeno.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(gridViewMaster.RowCount <= 0 || gridViewDetail.RowCount <=0 || String.IsNullOrEmpty(txtbatchcodeno.Text))
            {
                XtraMessageBox.Show("Records must not empty");
                return;
            }
            else
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to save this item/s?", "Save Items");
                if(confirm)
                {
                    for (int i = 0; i <= gridViewDetail.RowCount - 1; i++)
                    {
                        string qty = gridViewDetail.GetRowCellValue(i, "Qty").ToString();
                        string productcode = gridViewDetail.GetRowCellValue(i, "ProductCode").ToString();
                        string desc = gridViewDetail.GetRowCellValue(i, "Description").ToString();
                        string barcode = gridViewDetail.GetRowCellValue(i, "Barcode").ToString();
                        if (Convert.ToUInt32(qty) > 0)
                        {
                            Database.ExecuteQuery("INSERT INTO dbo.TempInventoryPrimal(Branch,BatchCode,Product,Description,Barcode,Quantity,Cost,Available,isDone,ProcessedBy,DateProcessed)" +
                                $" VALUES('{Login.assignedBranch}','{txtbatchcodeno.Text}','{productcode}','{desc}','{barcode}','{qty}','0','{qty}',0,'{Login.userid}','{DateTime.Today.ToShortDateString()}')");
                        }
                    }
                    spConfirm();
                    XtraMessageBox.Show("Primal Cuts Successfully Converted! Please check your Inventory");
                    this.Close();
                }
                else
                {
                    return;
                }
                
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<=gridViewDetail.RowCount-1;i++)
            {
                string qty = gridViewDetail.GetRowCellValue(i, "Qty").ToString();
                string productcode = gridViewDetail.GetRowCellValue(i, "ProductCode").ToString();
                string desc = gridViewDetail.GetRowCellValue(i, "Description").ToString();
                string barcode = gridViewDetail.GetRowCellValue(i, "Barcode").ToString();

                Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
                bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
                bprint.lblprodtype.Text = productcode;
                bprint.lbltotalkilos.Text = qty;
                bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
                bprint.xrBarCode2.Text = barcode;//productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
                ReportPrintTool report = new ReportPrintTool(bprint);
                report.Print();
            }
            
        }
    }
}