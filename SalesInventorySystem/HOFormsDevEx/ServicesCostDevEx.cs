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
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ServicesCostDevEx : DevExpress.XtraEditors.XtraForm
    {
        object serviceid,supplierid;
        public ServicesCostDevEx()
        {
            InitializeComponent();
        }

        private void ServicesCostDevEx_Load(object sender, EventArgs e)
        {
            display();
            populateSupplier();
            populateProductCategory();
            GridView view = gridControl1.FocusedView as GridView;
            view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                new GridColumnSortInfo(view.Columns["SupplierName"],DevExpress.Data.ColumnSortOrder.Ascending)
                }, 2);
        }
        void populateSupplier()
        {
            Database.displaySearchlookupEdit("select SupplierID,SupplierName FROM Supplier", txtsuppliers,"SupplierName","SupplierName");
        }

        void populateProductCategory()
        {
            Database.displaySearchlookupEdit("Select * FROM Services", txtservices,"SRVC_DESC", "SRVC_DESC");
        }

        void display()
        {
            Database.display("SELECT * FROM SERVICESCOST", gridControl1, gridView1);
            Database.displayCheckedListBoxItemsDevEx("Select distinct TransCode FROM TransactionDefinition", "TransCode", checkedListBoxControl1);
        }

        private void txtservices_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtservices.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            serviceid = view.GetRowCellValue(rowHandle, "SRVC_ID");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtsuppliers.Text == "" || txtservices.Text == "" || txtcostkg.Text == "")
            {
                XtraMessageBox.Show("Please input all the forms correctly");
                return;
            }
            else if (Convert.ToDouble(txtcostkg.Text) < 0)
            {
                XtraMessageBox.Show("Cost/Kg must Greater than Negative Value!");
                return;
            }
            else
            {
                bool isexist = Database.checkifExist("SELECT SupplierID FROM SERVICESCOST WHERE SupplierID='" + supplierid + "' AND ServiceCode='" + serviceid + "'");
                if (isexist)
                {
                    XtraMessageBox.Show("Supplier and Service Code are AlreadyExist!");
                    return;
                }
                else
                {
                    Database.ExecuteQuery("INSERT INTO SERVICESCOST VALUES('" + supplierid + "','" + serviceid + "','" + txtcostkg.Text + "','"+txtmnemonics.Text+"')", "Successfully Added!");
                    display();
                    clear();
                }
            }
        }
        void clear()
        {
            txtcostkg.Text = "";
            txtservices.Text = "";
            txtsuppliers.Text = "";
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = true;
            gridView1.OptionsBehavior.ReadOnly = false;
            btnupdate.Enabled = true;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE SERVICESCOST Set CostKg='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CostKg").ToString() + "'  " +
                "WHERE SupplierID='" + supplierid + "'  " +
                "AND ServiceCode='" + serviceid + "' "
                , "Successfully Update");
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsBehavior.ReadOnly = true;
            btnupdate.Enabled = false;
            clear();
            display();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM SERVICESCOST WHERE SupplierID='" + supplierid + "'  AND  ServiceCode='"+serviceid+"' ", "Successfully Deleted");
            clear();
            display();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "CostKg")
                e.Cancel = true;
        }

        private void checkedListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string str = "";
            bool flag=false;
            foreach (int index in checkedListBoxControl1.CheckedIndices)
            {
                if(checkedListBoxControl1.CheckedItemsCount > 1)
                {
                    str = str + checkedListBoxControl1.GetItemValue(index) + ",";
                    flag = true;
                }
                else
                {
                    str += checkedListBoxControl1.GetItemValue(index);
                }
                //str += checkedListBoxControl1.GetItemValue(index);
            }
            if(flag)
            {
                str = str.Remove(str.Length - 1);
            }
            txtmnemonics.Text = str;
        }

        private void txtsuppliers_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtsuppliers.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            supplierid = view.GetRowCellValue(rowHandle, "SupplierID");
        }
    }
}