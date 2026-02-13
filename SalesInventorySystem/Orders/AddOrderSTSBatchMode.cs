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

namespace SalesInventorySystem.Orders
{
    public partial class AddOrderSTSBatchMode : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public AddOrderSTSBatchMode()
        {
            InitializeComponent();
        }

        private void AddOrderSTSBatchMode_Load(object sender, EventArgs e)
        {
            panelControl2.Visible = true;
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            textEdit1.Text = IDGenerator.getIDNumberSP("sp_GetPurchaseOrderNumber", "PONumber"); //IDGenerator.getPONumber();
            txtgroup.Enabled = true;
            Database.displayComboBoxItems("Select CategoryName FROM GroupCategory", "CategoryName", txtgroup);

            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnsave.Enabled = false;
            btncancel.Enabled = false;
            btnclose.Enabled = true;
        }
        
        void saveAll()
        {
            try
            {
                string destinationBranch = "";
                if (radho.Checked == true)
                {
                    destinationBranch = "888";
                }
                else
                {
                    destinationBranch = txtbranch.Text;
                }
                double totalqty = 0.0;
                string approvalstatus = "", dateapproved = "", approvedby = "";
                DateTime dt = DateTime.Now;
                
                approvalstatus = "FOR APPROVAL";
                dateapproved = "";
                approvedby = "";
                totalqty = Database.getTotalSummation2("TransferOrderDetails", "PONumber='" + textEdit1.Text + "' ", "Qty");
                Database.ExecuteQuery("INSERT INTO TransferOrderSummary VALUES('" + textEdit1.Text + "'" +
                    ",'" + Login.assignedBranch + "'" +
                    ",'" + destinationBranch + "'" +
                    ",'" + totalqty + "'" +
                    ",'" + approvalstatus + "'" +
                    ",'" + DateTime.Now.ToString() + "'" +
                    ",'" + DateTime.Now.ToString() + "'" +
                    ",'" + txteffectivedate.Text + "'" +
                    ",'" + Login.Fullname + "'" +
                    ",'" + approvedby + "'" +
                    ",'" + dateapproved + "'" +
                    ",' '" +
                    ",' '" +
                    ",' '" +
                    ",0" +
                    ",'" + ordertype.Text + "'" +
                    ",'" + txtgroup.Text + "')", "Request Successfully Updated!");
                this.Dispose();
            }
            catch (Exception sqx)
            {
                XtraMessageBox.Show(sqx.Message.ToString());
            }
        }
        private void btnsave_Click_1(object sender, EventArgs e)
        {
           
            if (String.IsNullOrEmpty(textEdit1.Text))
            {
                XtraMessageBox.Show("Order Number must not Empty...");
                return;
            }
            if (String.IsNullOrEmpty(txtgroup.Text))
            {
                XtraMessageBox.Show("Group Category Must Not Empty!...");
                return;
            }
            if (ordertype.Text == "" || ordertype == null)
            {
                XtraMessageBox.Show("Please Select Order Type!");
                return;
            }
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("Please Input Product Details!");
                return;
            }
            else
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure?","Confirm Order");
                if (confirm)
                    saveAll();
                else
                    return;
            }
        }
        private void display()
        {
            Database.display("SELECT * FROM TransferOrderDetails WHERE PONumber='" + textEdit1.Text + "'", gridControl1, gridView1);
            gridView1.Columns[0].Visible = false;
        }
        private void btncancel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No Such Line to cancel!");
            }
            else
            {
                Database.ExecuteQuery("DELETE FROM TransferOrderDetails " +
                    "WHERE SeqNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SeqNo").ToString() + "' " +
                    "AND PONumber='" + textEdit1.Text + "' ", "Successfully Deleted!..");
                display();
            }
        }
        void radioChanged()
        {
            if (radho.Checked == true)
            {
                panelControl2.Visible = false;
            }
            else
            {
                panelControl2.Visible = true;
                Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches WHERE (BranchCode <> '888' OR BranchCode <> '" + Login.assignedBranch + "') ORDER BY BranchCode", txtbranch, "BranchCode", "BranchCode");
            }
        }
        private void radho_CheckedChanged(object sender, EventArgs e)
        {
            radioChanged();
        }

        private void radothers_CheckedChanged(object sender, EventArgs e)
        {
            radioChanged();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                this.Dispose();
                this.Close();
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Force Close the form? \n Note: All Transactions will be cancelled", "Force Close");
                if (ok)
                {
                    //  Database.ExecuteQuery("DELETE FROM Inventory WHERE ReferenceCode='" + txtcarcasssku.Text + "'");
                    this.Dispose();
                    this.Close();
                }

            }
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            //if (e.Column.FieldName == "Units")
            //    e.RepositoryItem = repoMetrics;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textEdit1.Text))
            {
                XtraMessageBox.Show("Press NEW First to Start New Transaction..");
            }
            else
            {
                Orders.SearchProductBatchMode orderko = new SearchProductBatchMode();
                orderko.txtpono.Text = textEdit1.Text;
                orderko.ShowDialog(this);
                if (Orders.SearchProductBatchMode.isdone == true)
                {
                    Orders.SearchProductBatchMode.isdone = false;
                    orderko.Dispose();
                    display();

                    btnnew.Enabled = false;
                    btnadd.Enabled = true;
                    btnsave.Enabled = true;
                    btncancel.Enabled = true;
                    btnclose.Enabled = true;
                }
            }
        }
    }
}