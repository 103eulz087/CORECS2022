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
using SalesInventorySystem.Classes;
using DevExpress.XtraEditors.Repository;

namespace SalesInventorySystem.Orders
{
    public partial class AddItemsBranchOrderSTS : DevExpress.XtraEditors.XtraForm
    {
        RepositoryItemSpinEdit repoQuantity;
        public int Quantity { get; set; }
        int totalrowhandle = 0;
        HashSet<string> selectedProductCodes = new HashSet<string>();
        //List<string> selectedProductCodes = new List<string>();
        public static bool isdone = false;
        object prodcatid = null;
        public AddItemsBranchOrderSTS()
        {
            InitializeComponent();
        }

        private void txtsrchprodcat_EditValueChanged(object sender, EventArgs e)
        {
            prodcatid = SearchLookUpClass.getSingleValue(txtsrchprodcat, "ProductCategoryID");
            populateProducts(prodcatid.ToString());
        }

        private void AddItemsBranchOrderSTS_Load(object sender, EventArgs e)
        {
      
            loadCategory();
            // Create and register editors once
            repoQuantity = new RepositoryItemSpinEdit();
            repoQuantity.MinValue = 0;
            repoQuantity.MaxValue = 99999;
            repoQuantity.IsFloatValue = false;
            gridControl1.RepositoryItems.Add(repoQuantity);
        }
        void save()
        {
            try
            {
                int ctr = Database.getLastID("TransferOrderDetails",
                                             "PONumber='" + txtpono.Text + "' ",
                                             "SeqNo");

                //foreach (string productCode in selectedProductCodes)
                for(int i=0;i<=gridView1.RowCount-1;i++)
                {
                    //int rowHandle = gridView1.LocateByValue("ProductCode", productCode);
                    if (gridView1.RowCount < 0) continue;

                    ctr += 1;

                    string description = gridView1.GetRowCellValue(i, "Description").ToString();
                    string quantity = gridView1.GetRowCellValue(i, "Quantity").ToString();
                    //string units = gridView1.GetRowCellValue(rowHandle, "Units").ToString();
                    string remarks = gridView1.GetRowCellValue(i, "Remarks").ToString();

                    bool checkifexists = Database.checkifExist(
                        $"SELECT TOP(1) ProductCode FROM TransferOrderDetails " +
                        $"WHERE ProductCode='{gridView1.GetRowCellValue(i, "ProductCode").ToString()}' AND PONumber='{txtpono.Text}'");

                    if (checkifexists)
                    {
                        BigAlert.Show("ALREADY EXISTS",
                            "One of the products you requested is already added. Please check!", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        Database.ExecuteQuery("INSERT INTO TransferOrderDetails (PONumber" +
                            ",SeqNo" +
                            ",ProductCode" +
                            ",ProductName" +
                            ",Qty" +
                            ",Units" +
                            ",isValid" +  
                            ",Remarks) " +
                            "VALUES ('" + txtpono.Text + "'" +
                            ",'" + ctr + "'" +
                            ",'" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "'" +
                            ",'" + description + "'" +
                            ",'" + quantity + "'" +
                            ",'kgs'" +
                            ",'1'" +
                            ",'" + remarks + "') ");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to save this Request?", "Confirm Request Order");
            if (confirm)
            {

                //UnfilterAndSave();
                save();
                isdone = true;
                this.Close();
            }
            else
            {
                return;
            }
        }
        void loadCategory()
        {
            Database.displaySearchlookupEdit("SELECT ProductCategoryID,Description FROM ProductCategory with(nolock)", txtsrchprodcat, "Description", "Description");
        }

        void populateProducts(string catid)
        {
            string query = $"SELECT * FROM dbo.funcview_STSProductAndInventory('{Login.assignedBranch}','{catid}') ";
            HelperFunction.ShowWaitAndDisplayNonAsync(query, gridControl1, gridView1, "Please wait", "Populating data into the database...");
            gridView1.Focus();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Quantity" )
                e.Cancel = true;
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
          

            if (!gridView1.IsDataRow(e.RowHandle))
                return;

            object qtyObj = gridView1.GetRowCellValue(e.RowHandle, "Quantity");
            if (qtyObj == null || qtyObj == DBNull.Value)
                return;

            if (decimal.TryParse(qtyObj.ToString(), out decimal qty) && qty > 0)
            {
                e.Appearance.BackColor = Color.LightGoldenrodYellow;
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (simpleButton1.Text == "Preview")
            {
                label3.Text = "Add More Items";
                simpleButton1.Text = "AddMore";
                gridView1.CloseEditor();
                gridView1.UpdateCurrentRow();

                // Hide rows where Quantity <= 0
                gridView1.ActiveFilter.Clear();
                gridView1.ActiveFilterString = "[Quantity] > 0";

            }
            else
            {
                gridView1.ActiveFilter.Clear();
                simpleButton1.Text = "Preview";
                label3.Text = "PREVIEW MODE – Only Quantity > 0 shown";
            }
        }
    }
}