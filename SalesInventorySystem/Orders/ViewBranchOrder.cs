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

namespace SalesInventorySystem
{
    public partial class ViewBranchOrder : DevExpress.XtraEditors.XtraForm
    {
        string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");
        public static string branchno,ponumber,effectivedate,refno="",custname="";
        
        public ViewBranchOrder()
        {
            InitializeComponent();
        }

        private void ViewBranchOrder_Load(object sender, EventArgs e)
        {
            //tabfilter();
            display();
        }

        private void tabfilter()
        {

            if (tabMain.SelectedTabPage.Equals(tabPending))//if (tabControl1.SelectedTab.Equals(Pending))
            {
                //Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and DateApproved >= '" + datefrompending.Text + "' and DateApproved <= '" + datetopending.Text + "'", gridControl1, gridView1);
                //gridView1.Focus();
                if (company != "JFC")
                {
                    Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and DateApproved >= '" + datefrompending.Text + "' and DateApproved <= '" + datetopending.Text + "'", gridControl1, gridView1);
                    gridView1.Focus();
                }
                else
                {
                    if (Login.assignedBranch == "888")
                    {
                        Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and DateApproved >= '" + datefrompending.Text + "' and DateApproved <= '" + datetopending.Text + "'  ", gridControl1, gridView1);
                        gridView1.Focus();
                    }
                    else
                    {
                        Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and DateApproved >= '" + datefrompending.Text + "' and DateApproved <= '" + datetopending.Text + "' AND RequestType='BranchOrder' AND BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1);
                        gridView1.Focus();
                    }

                }
            }
            else if (tabMain.SelectedTabPage.Equals(tabForDelivery)) //if (tabControl1.SelectedTab.Equals(Approved))
            {
                //Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text+ "'", gridControl2, gridView2);
                //gridView2.Focus();
                if (company != "JFC")
                {
                    Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and EffectivityDate >= '" + datefromfordev.Text + "' and EffectivityDate <= '" + datetofordev.Text + "'", gridControl2, gridView2);
                    gridView2.Focus();
                }
                else
                {
                    if (Login.assignedBranch == "888")
                    {
                        Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and EffectivityDate >= '" + datefromfordev.Text + "' and EffectivityDate <= '" + datetofordev.Text + "' ", gridControl2, gridView2);
                        gridView2.Focus();
                    }
                    else
                    {
                        Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and EffectivityDate >= '" + datefromfordev.Text + "' and EffectivityDate <= '" + datetofordev.Text + "' AND BranchCode='" + Login.assignedBranch + "'", gridControl2, gridView2);
                        gridView2.Focus();
                    }
                }
            }
            else if (tabMain.SelectedTabPage.Equals(tabRejected))// if (tabControl1.SelectedTab.Equals(Rejected))
            {
                //Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "'", gridControl3, gridView3);
                //gridView3.Focus();
                if (company != "JFC")
                {
                    Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "'", gridControl3, gridView3);
                    gridView3.Focus();
                }
                else
                {
                    if (Login.assignedBranch == "888")
                    {
                        Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "' ", gridControl3, gridView3);
                        gridView3.Focus();
                    }
                    else
                    {
                        Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "' AND BranchCode='" + Login.assignedBranch + "' AND RequestType='BranchOrder' ", gridControl3, gridView3);
                        gridView3.Focus();
                    }
                }
            }
           
        }

        void display()
        {
            Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and DateApproved >= '" + datefrompending.Text + "' and DateApproved <= '" + datetopending.Text + "'", gridControl1, gridView1);
            Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "'", gridControl2, gridView2);
            Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "' ", gridControl3, gridView3);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            openBranchOrder();
        }

        void openBranchOrder()
        {
            string stat = "";
            branchno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();
            ponumber = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PONumber").ToString();
            stat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "WareHouseStatus").ToString();
            effectivedate = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EffectivityDate").ToString();
            custname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CustomerName").ToString();
            bool ok = Database.checkifExist("SELECT isProcess FROM view_BranchOrder WHERE isProcess = '0' " +
                "AND Status='APPROVED' " +
                "AND (WareHouseStatus='PENDING' OR WareHouseStatus is null) " +
                "AND (OrderType='MAIN' OR OrderType='ADD-ONS') " +
                "AND EffectivityDate='" + DateTime.Now.ToShortDateString() + "' " +
                "AND BranchCode='" + branchno + "'");
            
            if (stat == "" || stat == null || stat == "PENDING")
            {
                AddBranchOrder addbrorder = new AddBranchOrder();
                addbrorder.FormClosed += new FormClosedEventHandler(addbrorder_FormClosed);
                addbrorder.Show();

                addbrorder.Text = custname + "-" + Branch.getBranchName(branchno);
                addbrorder.txtbrcode.Text = branchno;
                addbrorder.txtponum.Text = ponumber;

                string id = IDGenerator.getIDNumberSP("sp_GetReferenceNumber", "ReferenceNumber");
                addbrorder.txtrefno.Text = id;//IDGenerator.getReferenceNumber();
                addbrorder.txteffectivedate.Text = effectivedate;
                
                Database.display("SELECT * FROM view_PurchaseOrderDetails WHERE PONumber='" + addbrorder.txtponum.Text + "'", addbrorder.gridControl1, addbrorder.gridView1);
                Database.display("SELECT SeqNo,ProductNo,BarcodeNo,ProductName,QtyDelivered,Status,ProcessedBy FROM DeliveryDetails WHERE DeliveryNo='" + addbrorder.txtdevno.Text + "' AND PONumber='" + addbrorder.txtponum.Text + "' and Status = 'PENDING'", addbrorder.gridControl2, addbrorder.gridView2);
                addbrorder.gridView2.Columns["SeqNo"].Visible = false;
                addbrorder.gridView1.ExpandAllGroups();

                addbrorder.gridView1.Columns["PONumber"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PONumber", "{0:n2}");
                addbrorder.gridView2.Columns["SeqNo"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "SeqNo", "{0:n2}");
            }
            else
            {
                XtraMessageBox.Show("You Already Processed This Request!");
            }
        }


        void addbrorder_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED'", gridControl1, gridView1);
            //gridView1.Focus();
            display();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuPending.Show(gridControl1, e.Location);
            }
        }

        private void processThisOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openBranchOrder();
        }

        private void confirmOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refno = gridView1.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            HOForms.ViewForDeliveryDetails viewdet = new HOForms.ViewForDeliveryDetails();
            viewdet.ShowDialog(this);
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            refno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            branchno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            Orders.viewBranchOrderDetails viewdet = new Orders.viewBranchOrderDetails();
            viewdet.Show();
            Database.display("SELECT * FROM view_BranchOrderDetails WHERE PONumber='" + refno + "' and BranchCode='" + branchno + "'", viewdet.gridControl4, viewdet.gridView4);
        }

        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = view.GetRowCellDisplayText(e.RowHandle, view.Columns["BranchCode"]);
                string ordertype = view.GetRowCellDisplayText(e.RowHandle, view.Columns["OrderType"]);
                if (category == "888" && ordertype == "ADD-ONS")
                {
                    //  e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                    e.Appearance.ForeColor = Color.Blue;
                }
                if(category != "888" && ordertype == "ADD-ONS")
                {
                    e.Appearance.BackColor = Color.LightSkyBlue;
                    e.Appearance.BackColor2 = Color.DeepSkyBlue;
                }
            }
        }

        private void cancelThisOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printDeliveryReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string refno1 = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
            HOForms.ViewForDeliveryDetails viewdet = new HOForms.ViewForDeliveryDetails();
            viewdet.Show();
            Database.display("SELECT ProductName,BarcodeNo,QtyDelivered,FORMAT(SellingPrice, 'N','en-US')  AS Price,FORMAT((QtyDelivered*SellingPrice), 'N','en-US') AS Amount,QtyDelivered AS ActualQtyDelivered,ProductNo FROM view_DeliveryReciept WHERE PONumber = '" + refno1 + "'", viewdet.gridControl4, viewdet.gridView4);
            viewdet.txtpono.Text = refno1;
            viewdet.gridView4.Columns["Amount"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:n2}");
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            refno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "PONumber").ToString();
            branchno = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "BranchCode").ToString();
            Orders.viewBranchOrderDetails viewdet = new Orders.viewBranchOrderDetails();
            viewdet.Show();
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripForDelivery.Show(gridControl2, e.Location);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            branchno = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString();
            ponumber = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "PONumber").ToString();
          
            effectivedate = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EffectivityDate").ToString();
            AddBranchOrder addbrorder = new AddBranchOrder();
            addbrorder.FormClosed += new FormClosedEventHandler(addbrorder_FormClosed);
            addbrorder.Show();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            tabfilter();
        }

      
        private void btnPendingGenerate_Click(object sender, EventArgs e)
        {
            //if (company != "JFC")
            //{
            //    Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and EffectivityDate >= '" + datefrompending.Text + "' and EffectivityDate <= '" + datetopending.Text + "'", gridControl1, gridView1);
            //    gridView1.Focus();
            //}
            //else
            //{
            //    if (Login.assignedBranch == "888")
            //    {
            //        Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and EffectivityDate >= '" + datefrompending.Text + "' and EffectivityDate <= '" + datetopending.Text + "' ", gridControl1, gridView1);
            //        gridView1.Focus();
            //    }
            //    else
            //    {
            //        Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and EffectivityDate >= '" + datefrompending.Text + "' and EffectivityDate <= '" + datetopending.Text + "' AND BranchCode='" + Login.assignedBranch + "'", gridControl1, gridView1);
            //        gridView1.Focus();
            //    }

            //}
            Database.display("SELECT * FROM view_BranchOrder WHERE isProcess = '0' AND Status='APPROVED' and EffectivityDate >= '" + datefrompending.Text + "' and EffectivityDate <= '" + datetopending.Text + "'", gridControl1, gridView1);
            gridView1.Focus();
        }

        private void btnGenerateDelivery_Click(object sender, EventArgs e)
        {
            if (company != "JFC")
            {
                Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "'", gridControl2, gridView2);
                gridView2.Focus();
            }
            else
            {
                if (Login.assignedBranch == "888")
                {
                    Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "' ", gridControl2, gridView2);
                    gridView2.Focus();
                }
                else
                {
                    Database.display("SELECT * FROM view_ForDelivery WHERE Status='FOR DELIVERY' and DateAdded >= '" + datefromfordev.Text + "' and DateAdded <= '" + datetofordev.Text + "'  AND BranchCode='" + Login.assignedBranch + "'", gridControl2, gridView2);
                    gridView2.Focus();
                }
            }
        }

        private void btnGenerateRejected_Click(object sender, EventArgs e)
        {
            if (company != "JFC")
            {
                Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "'", gridControl3, gridView3);
                gridView3.Focus();
            }
            else
            {
                if (Login.assignedBranch == "888")
                {
                    Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "' AND (RequestType='Products' OR RequestType='STS')", gridControl3, gridView3);
                    gridView3.Focus();
                }
                else
                {
                    Database.display("SELECT * FROM view_ForDelivery WHERE Status='REJECTED' and DateAdded >= '" + datefromrej.Text + "' and DateAdded <= '" + datetorej.Text + "' AND BranchCode='" + Login.assignedBranch + "' AND RequestType='BranchOrder' ", gridControl3, gridView3);
                    gridView3.Focus();
                }
            }
        }

        
      
     }
}