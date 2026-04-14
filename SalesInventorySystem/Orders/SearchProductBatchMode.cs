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
using DevExpress.XtraEditors.Repository;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.Orders
{
    public partial class SearchProductBatchMode : DevExpress.XtraEditors.XtraForm
    {
        RepositoryItemSpinEdit repoQuantity;
        public int Quantity { get; set; }
        int totalrowhandle = 0;
        HashSet<string> selectedProductCodes = new HashSet<string>();
        //List<string> selectedProductCodes = new List<string>();
        public static bool isdone = false;
        object prodcatid = null;
        

        public SearchProductBatchMode()
        {
            InitializeComponent();
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == (Keys.F | Keys.Control))
            {
                txtsearchprod.Focus();
            }

            return functionReturnValue;
        }
        private void SearchProductBatchMode_Load(object sender, EventArgs e)
        {
            txtsearchprod.Focus();
            loadCategory();
            // Create and register editors once
            repoQuantity = new RepositoryItemSpinEdit();
            repoQuantity.MinValue = 0;
            repoQuantity.MaxValue = 99999;
            repoQuantity.IsFloatValue = false;
            gridControlMain.RepositoryItems.Add(repoQuantity);
        }
        
        
        void save()
        {
            try
            {
                int ctr = Database.getLastID("TransferOrderDetails",
                                             "PONumber='" + txtpono.Text + "' ",
                                             "SeqNo");

                foreach (string productCode in selectedProductCodes)
                {
                    int rowHandle = gridViewMain.LocateByValue("ProductCode", productCode);
                    if (rowHandle < 0) continue;

                    ctr += 1;

                    string description = gridViewMain.GetRowCellValue(rowHandle, "Description").ToString();
                    string quantity = gridViewMain.GetRowCellValue(rowHandle, "Quantity").ToString();
                    //string units = gridView1.GetRowCellValue(rowHandle, "Units").ToString();
                    string remarks = gridViewMain.GetRowCellValue(rowHandle, "Remarks").ToString();

                    bool checkifexists = Database.checkifExist(
                        $"SELECT TOP(1) ProductCode FROM TransferOrderDetails " +
                        $"WHERE ProductCode='{productCode}' AND PONumber='{txtpono.Text}'");

                    if (checkifexists)
                    {
                        BigAlert.Show("ALREADY EXISTS", 
                            "One of the products you requested is already added. Please check!",MessageBoxIcon.Warning);
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
                            ",'" + productCode + "'" +
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

     
        private void txtsearchprod_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    //btnsubmit.PerformClick();
        }

      
        private void RestoreSelections()
        {
            foreach (string productCode in selectedProductCodes)
            {
                int rowHandle = gridViewMain.LocateByValue("ProductCode", productCode);
                if (rowHandle >= 0)
                    gridViewMain.SelectRow(rowHandle);
            }
        }

        private void txtsearchprod_EditValueChanged(object sender, EventArgs e)
        {
            //CaptureSelection();
            //gridView1.ActiveFilterString = $"[Description] LIKE '%{txtsearchprod.Text}%'";

        }

        

        private void txtsrchprodcat_EditValueChanged(object sender, EventArgs e)
        {
            prodcatid = SearchLookUpClass.getSingleValue(txtsrchprodcat, "ProductCategoryID");
            populateProducts(prodcatid.ToString());
        }

        void loadCategory()
        {
            Database.displaySearchlookupEdit("SELECT ProductCategoryID,Description FROM ProductCategory with(nolock)", txtsrchprodcat, "Description", "Description");
        }

        void populateProducts(string catid)
        {
            string query = "SELECT ProductCode,Description,'0' AS Quantity,' ' as Remarks " +
               "FROM dbo.Products with(nolock) " +
               "WHERE BranchCode='" + Login.assignedBranch + "' AND ProductCategoryCode='"+ catid + "' ";

            HelperFunction.ShowWaitAndDisplayNonAsync(query, gridControlMain, gridViewMain, "Please wait", "Populating data into the database...");
            gridViewMain.Focus();
            

        }

        private void gridViewMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.BackColor = Color.Salmon;
                e.Appearance.BackColor2 = Color.LightSalmon;
            }
            if (e.Column.FieldName == "Remarks")
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.BackColor2 = Color.Yellow;
            }
        }

        private void gridViewMain_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Quantity" && view.FocusedColumn.FieldName != "Remarks")
                e.Cancel = true;
        }

        private void gridViewMain_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            //GridView view = sender as GridView;
            //int rowHandle = e.ControllerRow;
            //totalrowhandle = rowHandle;
            //if (rowHandle >= 0)
            //{
            //    string productCode = view.GetRowCellValue(rowHandle, "ProductCode").ToString();

            //    if (view.IsRowSelected(rowHandle))
            //        selectedProductCodes.Add(productCode);
            //    else
            //        selectedProductCodes.Remove(productCode);
            //}
            GridView view = sender as GridView;
            int rowHandle = e.ControllerRow;
            if (rowHandle < 0) return;

            // Get quantity safely (handle nulls)
            object qObj = view.GetRowCellValue(rowHandle, "Quantity");
            int qty = 0;
            int.TryParse(qObj?.ToString() ?? "0", out qty);

            // If quantity is zero, prevent selection
            if (qty == 0)
            {
                // Deselect the row that was just selected
                if (view.IsRowSelected(rowHandle))
                    view.UnselectRow(rowHandle);

                // Optional: focus the row so user sees it, and show message
                view.FocusedRowHandle = rowHandle;
                XtraMessageBox.Show("Quantity cannot be zero. Please enter a valid quantity before selecting this item.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // If valid, keep your existing selection-tracking logic
            string productCode = view.GetRowCellValue(rowHandle, "ProductCode")?.ToString();
            if (string.IsNullOrEmpty(productCode)) return;

            if (view.IsRowSelected(rowHandle))
                selectedProductCodes.Add(productCode);
            else
                selectedProductCodes.Remove(productCode);


        }

        private void gridViewMain_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Quantity")
                e.RepositoryItem = repoQuantity;
        }

        private void gridViewMain_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null || view.FocusedColumn == null) return;

            if (view.FocusedColumn.FieldName == "Quantity")
            {
                // Try parse to int
                int val;
                if (!int.TryParse(e.Value?.ToString() ?? "0", out val) || val <= 0)
                {
                    e.Valid = false;
                    e.ErrorText = "Quantity must be a positive integer greater than zero.";
                }
            }

        }
    }
}