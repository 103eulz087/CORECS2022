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
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class LiquidationDevExFrm : DevExpress.XtraEditors.XtraForm
    {
        string voucherid = "";
        DataTable table,table1;
        public LiquidationDevExFrm()
        {
            InitializeComponent();
        }

        private void LiquidationDevExFrm_Load(object sender, EventArgs e)
        {
            loadRepositoryItem();
         
            table = new DataTable();
            table.Columns.Add("BranchCode");
            table.Columns.Add("LiquidationName");
            table.Columns.Add("Particulars");
            table.Columns.Add("Amount");
            gridControl1.DataSource = table;

            table1 = new DataTable();
            table1.Columns.Add("CheckNo");
            table1.Columns.Add("CheckDate");
            table1.Columns.Add("CheckAmount");
            table1.Columns.Add("Supplier");
            gridControl2.DataSource = table1;
        }
       
        void loadRepositoryItem()
        {
            Database.displayRepositorySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", repbrcode, "BranchCode", "BranchCode");
            Database.displayRepositorySearchlookupEdit("SELECT LiquidationID,LiquidationName FROM LiquidationList", reptypeofexpense, "LiquidationName", "LiquidationName");
            Database.displayRepositorySearchlookupEdit("SELECT CheckNo,CheckDate,Amount,PaidTo FROM CheckVoucher WHERE isLiquidation=1 AND OfficialReceiptNo<>'LIQUIDATED' ", repositoryItemSearchLookUpEditCheckNo, "CheckNo", "CheckNo");

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            newRow["Amount"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "BranchCode")
                e.RepositoryItem = repbrcode;
            if (e.Column.FieldName == "LiquidationName")
                e.RepositoryItem = reptypeofexpense;
            if (e.Column.FieldName == "Particulars")
                e.RepositoryItem = repparticulars;
            if (e.Column.FieldName == "Amount")
                e.RepositoryItem = repamount;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                voucherid = IDGenerator.getIDNumberSP("sp_GetLiquidationNumber", "TicketNumber");//IDGenerator.getLiquidationNumberSP();
                string branchcode, expname, particulars, amount;
                string checkno, checkdate, checkamount, supp;
                bool isEmpty = false;
                double totalamount = 0.0,totalcheckamount=0.0;
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (String.IsNullOrEmpty(gridView1.GetRowCellValue(i, "BranchCode").ToString()) || String.IsNullOrEmpty(gridView1.GetRowCellValue(i, "LiquidationName").ToString()))
                    {
                        isEmpty = true;
                        break;
                    }
                }
                
                if (isEmpty)
                {
                    XtraMessageBox.Show("Some Fields are Empty..");
                    return;
                }
                if (txtticketdate.Text == "")
                {
                    XtraMessageBox.Show("Please Input All Valid Fields");
                }
                else
                {
                    for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    {
                        branchcode = gridView1.GetRowCellValue(i, "BranchCode").ToString();
                        expname = gridView1.GetRowCellValue(i, "LiquidationName").ToString();
                        particulars = gridView1.GetRowCellValue(i, "Particulars").ToString();
                        amount = gridView1.GetRowCellValue(i, "Amount").ToString();
                        totalamount += Convert.ToDouble(gridView1.GetRowCellValue(i, "Amount").ToString());
                        Database.ExecuteQuery("INSERT INTO LiquidationDetails VALUES ('" + voucherid + "','"+branchcode+"','" + expname + "','" + particulars + "','" + amount + "','0',' ')");
                    }
                    
                    for (int j = 0; j <= gridView2.RowCount - 1; j++)
                    {
                        checkno = gridView2.GetRowCellValue(j, "CheckNo").ToString();
                        checkdate = gridView2.GetRowCellValue(j, "CheckDate").ToString();
                        checkamount = gridView2.GetRowCellValue(j, "CheckAmount").ToString();
                        supp = gridView2.GetRowCellValue(j, "Supplier").ToString();
                        totalcheckamount += Convert.ToDouble(gridView2.GetRowCellValue(j, "CheckAmount").ToString());
                        Database.ExecuteQuery("INSERT INTO LiquidationMaster VALUES ('" + voucherid + "','" + txtvoucherid.Text + "','" + checkno + "','" + checkdate + "','" + checkamount + "','" + supp + "','" + totalcheckamount + "','" + DateTime.Now.ToShortDateString() + "',0)");
                    }
                    Database.ExecuteQuery("INSERT INTO LiquidationGroupMaster VALUES('"+voucherid+"','"+ totalcheckamount + "','"+ totalamount + "','0')");
                    postTicket();
                    XtraMessageBox.Show("Successfully Added!");
                    this.Close();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void postTicket()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {

              
                string query = "sp_LiquidateVoucher";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmliquidationid", voucherid);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.Parameters.AddWithValue("@parmticketdate", txtticketdate.Text);
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

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void gridView2_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "CheckNo")
                e.RepositoryItem = repositoryItemSearchLookUpEditCheckNo;
        }

        private void repositoryItemSearchLookUpEditCheckNo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            string checkdate, checkamount, supplier;
            checkdate = Database.getSingleQuery("CheckVoucher", "CheckNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "CheckNo").ToString() + "'", "CheckDate");
            checkamount = Database.getSingleQuery("CheckVoucher", "CheckNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "CheckNo").ToString() + "'", "Amount");
            supplier = Database.getSingleQuery("CheckVoucher", "CheckNo='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "CheckNo").ToString() + "'", "SupplierID");
            if (e.Column.FieldName == "CheckNo")
            {
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "CheckDate", checkdate);
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "CheckAmount", checkamount);
                gridView2.SetRowCellValue(gridView2.FocusedRowHandle, "Supplier", supplier);
            }
          
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow newRow = table1.NewRow();
            //newRow["Amount"] = 0;
            table1.Rows.Add(newRow);
            gridControl2.DataSource = table1;
            gridView2.BestFitColumns();
        }
    }
}