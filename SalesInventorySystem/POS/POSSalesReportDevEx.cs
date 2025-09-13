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

namespace SalesInventorySystem.POS
{
    public partial class POSSalesReportDevEx : DevExpress.XtraEditors.XtraForm
    {
        object custkey = null;
        object brcodesummary = null, brcodedetails = null;
        public POSSalesReportDevEx()
        {
            InitializeComponent();
        }

        private void btnsalestransummary_Click(object sender, EventArgs e)
        {
            if (Login.assignedBranch != "888")
            {
                Database.display("SELECT * FROM view_batchTransactionSummary " +
                      "WHERE BranchCode='" + Login.assignedBranch + "' " +
                      "AND CAST(TransDate as Date) >= '" + datefromsalessum.Text + "' AND CAST(TransDate as Date) <= '" + datetosalessum.Text + "' ORDER BY ReferenceNo", gridControl2, gridView2);
            }
            else
            {
                Database.display("SELECT * FROM view_batchTransactionSummary " +
                       $"WHERE BranchCode='{brcodesummary.ToString()}' " +
                       "AND CAST(TransDate as Date) >= '" + datefromsalessum.Text + "' AND CAST(TransDate as Date) <= '" + datetosalessum.Text + "' ORDER BY ReferenceNo", gridControl2, gridView2);

            }
        }

        private void btnTransactionDet_Click(object sender, EventArgs e)
        {
            gridControl1.BeginUpdate();
            gridView1.GroupSummary.Clear();
            gridView1.Columns.Clear();
            if(Login.assignedBranch != "888")
            {
                Database.display("SELECT * FROM view_detailTransactionHistory " +
                 "WHERE BranchCode='" + Login.assignedBranch + "' " +
                 "AND CAST(DateOrder as date) >= '" + txtdateFromTransDet.Text + "' AND CAST(DateOrder as date) <= '" + txtdateToTransDet.Text + "' ORDER BY ReferenceNo", gridControl1, gridView1);
            }
            else
            {
                Database.display("SELECT * FROM view_detailTransactionHistory " +
             $"WHERE BranchCode='{brcodedetails.ToString()}' " +
             "AND CAST(DateOrder as date) >= '" + txtdateFromTransDet.Text + "' AND CAST(DateOrder as date) <= '" + txtdateToTransDet.Text + "' ORDER BY ReferenceNo", gridControl1, gridView1);

            }
            Classes.DevXGridViewSettings.ShowFooterCountTotal(gridView1, "BranchCode");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "QtySold");
            Classes.DevXGridViewSettings.ShowFooterTotal(gridView1, "TotalAmount");
            gridControl1.EndUpdate();
        }

        private void POSSalesReportDevEx_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);

            var now2 = DateTime.Now;
            //var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);


            datefromsalessum.Text = date.ToShortDateString();
            datetosalessum.Text = lastDay.ToShortDateString();

            txtdateFromTransDet.Text = date.ToShortDateString();
            txtdateToTransDet.Text = lastDay.ToShortDateString();

            
            populate();
        }

        void populate()
        {
            if(Login.assignedBranch != "888")
            {
                txtbranchsummary.Visible = false;
                txtbranchdetails.Visible = false;
            }
            else
            {
                Database.displaySearchlookupEdit("Select distinct BranchCode,BranchName FROM Branches Order By BranchCode", txtbranchsummary, "BranchName", "BranchName");
                Database.displaySearchlookupEdit("Select distinct BranchCode,BranchName FROM Branches Order By BranchCode", txtbranchdetails, "BranchName", "BranchName");
            }
            Database.displaySearchlookupEdit("SELECT CustomerID,CustomerName From dbo.Customers", searchLookUpEdit1,"CustomerName", "CustomerName");
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            custkey = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "CustomerID");
        }

        private void btnTransactionPayment_Click(object sender, EventArgs e)
        {
            var rowz = Database.getMultipleQuery("SELECT * FROM dbo.Customers WHERE CustomerKey='" + custkey.ToString() + "'", "CustomerKey,CustomerID ,CustomerName,CustomerEmail,CustomerContactNo,CustomerAddress,CustomerBirthDate,CustomerCreditLimit,BranchCode,Term,isActive,DateAdded,AddedBy,UpdatedBy,AccountOfficer,TinNo");
            string CustomerKey = rowz["CustomerKey"].ToString();
            string CustomerID = rowz["CustomerID"].ToString();
            string CustomerName = rowz["CustomerName"].ToString();
            string CustomerEmail = rowz["CustomerEmail"].ToString();
            string CustomerContactNo = rowz["CustomerContactNo"].ToString();
            string CustomerAddress = rowz["CustomerAddress"].ToString();
            string CustomerBirthDate = rowz["CustomerBirthDate"].ToString();
            string CustomerCreditLimit = rowz["CustomerCreditLimit"].ToString();
            string BranchCode = rowz["BranchCode"].ToString();
            string Term = rowz["Term"].ToString();
            string isActive = rowz["isActive"].ToString();
            string DateAdded = rowz["DateAdded"].ToString();
            string AddedBy = rowz["AddedBy"].ToString();
            string UpdatedBy = rowz["UpdatedBy"].ToString();
            string AccountOfficer = rowz["AccountOfficer"].ToString();
            string TinNo = rowz["TinNo"].ToString();
            txtid.Text = CustomerKey;
            txtname.Text = CustomerName;
            txtcontactno.Text = CustomerContactNo;
            txtaddress.Text = CustomerAddress;
            getData();
        }

        void getData()
        {
            var rowz = Database.getMultipleQuery($"SELECT * FROM func_CustomerSalesBoard('{custkey.ToString()}','{Environment.MachineName}') ", "TotalInvoice,SubTotal,TotalAmount,Average");
            string TotalInvoice = rowz["TotalInvoice"].ToString();
            string SubTotal = rowz["SubTotal"].ToString();
            string TotalAmount = rowz["TotalAmount"].ToString();
            string Average = rowz["Average"].ToString();
            txtavg.Text = Average;
            txttotinvoice.Text = TotalInvoice;
            txttotasalesb4tax.Text = SubTotal;
            txttotsalesnet.Text = TotalAmount;
            Database.display("SELECT a.DateOrder,a.ReferenceNo,a.Category,a.Description,a.QtySold,a.TotalAmount " +
                "FROM dbo.view_detailTransactionHistory a with(nolock) LEFT OUTER JOIN BatchSalesSummary b with(nolock) " +
                "ON a.ReferenceNo=b.ReferenceNo WHERE b.CustomerNo='" + custkey.ToString() + "' ORDER BY ReferenceNo DESC", gridControl3, gridView3);
        }

        private void txtbranchsummary_EditValueChanged(object sender, EventArgs e)
        {
            brcodesummary = SearchLookUpClass.getSingleValue(txtbranchsummary, "BranchCode");
        }

        private void txtbranchdetails_EditValueChanged(object sender, EventArgs e)
        {
            brcodedetails = SearchLookUpClass.getSingleValue(txtbranchdetails, "BranchCode");
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}