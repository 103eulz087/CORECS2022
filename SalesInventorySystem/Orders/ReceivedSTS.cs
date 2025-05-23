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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace SalesInventorySystem.Orders
{
    public partial class ReceivedSTS : DevExpress.XtraEditors.XtraForm
    {
        public ReceivedSTS()
        {
            InitializeComponent();
        }

        private void ReceivedSTS_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            if (tabMain.SelectedTabPage.Equals(tabForReceiving))
            {
                Database.display("SELECT * FROM view_ForReceivingSTS WHERE InitiatingBranch='" + Login.assignedBranch + "' and Status='FOR DELIVERY' ", gridControlForReceiving, gridViewForReceiving);
            }
            else if (tabMain.SelectedTabPage.Equals(tabMyRequest))
            {
                Database.display("SELECT * FROM view_MyRequestSTS WHERE InitiatingBranch='" + Login.assignedBranch + "' and EffectivityDate between '"+datefrom.Text+"' and '"+dateto.Text+"' ", gridControlMyReq, gridViewMyReq);
            }
        }

        private void btnMyReq_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_MyRequestSTS WHERE InitiatingBranch='" + Login.assignedBranch + "' and EffectivityDate between '" + datefrom.Text + "' and '" + dateto.Text + "' ", gridControlMyReq, gridViewMyReq);
        }

        private void btnMyReqExcel_Click(object sender, EventArgs e)
        {
            string filename = "MYREQUEST_STS" + DateTime.Now.ToShortDateString().Replace(@"\", "-");
            exporttoexcel(gridViewMyReq, filename);
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

        void checker()
        {
            Orders.OrderCheckerDevEx oread = new Orders.OrderCheckerDevEx();
            Database.display("SELECT SeqNo,ProductName,Qty FROM TransferOrderDetails WHERE PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' ", oread.gridControl1, oread.gridView1);
            Database.display("SELECT SeqNo,ProductName,ActualQty FROM DeliveryDetails WHERE   PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' ", oread.gridControl2, oread.gridView2);

            //Database.display("SELECT ProductName,ActualQty FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "'", oread.gridControl1, oread.gridView1);
            //Database.display("SELECT ProductName,QtyDelivered FROM DeliveryDetails WHERE PONumber='" + txtponum.Text + "'", oread.gridControl2, oread.gridView2);
            Database.display("SELECT SeqNo,ProductName,Qty FROM ReceivedOrderDetails  WHERE PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' ", oread.gridControl3, oread.gridView3);
            //Database.display("SELECT ProductName,SUM(QtyDelivered) as TotalKilos,COUNT(distinct BarcodeNo) as TotalBox FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "' GROUP BY ProductName", oread.gridControl2, oread.gridView2);
           
            oread.ShowDialog(this);
        }
        void checker2()
        {
            bool exist = false;
            exist = Database.checkifExist("SELECT TOP(1) PONumber FROM DeliveryDetails WHERE PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' ");
            if (gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "Status").ToString() == "FOR APPROVAL" || (gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "Status").ToString() == "APPROVED" && exist==false))
            {

                Orders.STSForApprovalDetails podetails = new Orders.STSForApprovalDetails();
                //Database.display("SELECT * FROM view_TransferOrderDetails WHERE PONumber = '" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle,"PONumber").ToString() + "' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                Database.display($"SELECT * FROM funcview_TransferOrderDetailsSTS('{Login.assignedBranch}') WHERE PONumber = '{ gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle,"PONumber").ToString() }' ORDER BY SeqNo", podetails.gridControl1, podetails.gridView1);
                podetails.txtpono.Text = gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString();
                GridView view = podetails.gridControl1.FocusedView as GridView;
                view.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(view.Columns["Category"],DevExpress.Data.ColumnSortOrder.Ascending)
                        }, 1);
                //podetails.gridView1.Columns["SequenceNumber"].Visible = false;
                podetails.gridView1.ExpandAllGroups();
                podetails.groupBox1.Visible = false;
                podetails.ShowDialog(this);
                GridGroupSummaryItem ite = new GridGroupSummaryItem();
                ite.FieldName = "Qty";
                ite.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                ite.ShowInGroupColumnFooter = podetails.gridView1.Columns["Qty"];
                podetails.gridView1.GroupSummary.Add(ite);
            }
            else
            {
                Orders.OrderCheckerDevEx oread = new Orders.OrderCheckerDevEx();

                Database.display("SELECT ProductName,ActualQty FROM DeliveryDetails WHERE PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "'", oread.gridControl1, oread.gridView1);
                //Database.display("SELECT ProductName,QtyDelivered FROM DeliveryDetails WHERE PONumber='" + txtponum.Text + "'", oread.gridControl2, oread.gridView2);
                Database.display("SELECT ProductName,SUM(Qty) as TotalKilos,COUNT(distinct Barcode) as TotalBox FROM ReceivedOrderDetails WHERE PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' GROUP BY ProductName", oread.gridControl2, oread.gridView2);
                //Database.display("SELECT ProductName,SUM(QtyDelivered) as TotalKilos,COUNT(distinct BarcodeNo) as TotalBox FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "' GROUP BY ProductName", oread.gridControl2, oread.gridView2);
                Database.display("SELECT ProductName,ActualQty FROM DeliveryDetails WHERE ProductNo not in (Select ProductCode FROM ReceivedOrderDetails WHERE PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "') AND PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "' ", oread.gridControl3, oread.gridView3);
                //Database.display("SELECT ProductName,Qty FROM TransferOrderDetails WHERE ProductCode not in (Select ProductNo FROM DeliveryDetails WHERE PONumber='" + txtpono.Text + "') AND PONumber='" + txtpono.Text + "' ", oread.gridControl3, oread.gridView3);

                oread.ShowDialog(this);
            }
         
        }
        private void gridViewMyReq_DoubleClick(object sender, EventArgs e)
        {
            //Orders.ReceivedSTSDetails recvdsts = new ReceivedSTSDetails();
            //Database.display("SELECT * FROM TransferOrderDetails WHERE PONumber='" + gridViewMyReq.GetRowCellValue(gridViewMyReq.FocusedRowHandle, "PONumber").ToString() + "'", recvdsts.gridControlMyReq, recvdsts.gridViewMyReq);
            //recvdsts.ShowDialog(this);
            checker2();
        }

        private void tabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            display();
        }

        private void gridViewForReceiving_DoubleClick(object sender, EventArgs e)
        {
            //AddBranchInventoryFrm addbrnchinv = new AddBranchInventoryFrm();
            //addbrnchinv.Show();

            //addbrnchinv.txtdevno.Text = gridViewForReceiving.GetRowCellValue(gridViewForReceiving.FocusedRowHandle, "DeliveryNo").ToString();
            //addbrnchinv.txtpono.Text = gridViewForReceiving.GetRowCellValue(gridViewForReceiving.FocusedRowHandle, "PONumber").ToString();
            //Database.display("SELECT ProductNo,ProductName,QtyDelivered,Cost,SellingPrice FROM DeliveryDetails WHERE PONumber='" + addbrnchinv.txtpono.Text + "' ", addbrnchinv.gridControl2, addbrnchinv.gridView2);
          
            string pono;
            pono = gridViewForReceiving.GetRowCellValue(gridViewForReceiving.FocusedRowHandle, "PONumber").ToString();
            HOFormsDevEx.ReceivedSTSBatchMode askdh = new HOFormsDevEx.ReceivedSTSBatchMode();
        
            askdh.txtshipmentno.Text = pono;
            Database.display("SELECT ProductNo,ProductName,BarcodeNo,Cost,QtyDelivered,QtyDelivered as ActualQty FROM DeliveryDetails WHERE PONumber='" + pono + "' ", askdh.gridControl1, askdh.gridView1);
            //Database.display("SELECT ProductCategory,OrderCode,Description,Quantity,Cost FROM view_PODETAILS WHERE ShipmentNo='" + shipmentno + "'", addinv.gridControl1, addinv.gridView1);
            askdh.ShowDialog(this);
            if (HOFormsDevEx.ReceivedSTSBatchMode.isdone == true)
            {
                display();
                HOFormsDevEx.ReceivedSTSBatchMode.isdone = false;
                askdh.Dispose();
            }
        }
    }
}