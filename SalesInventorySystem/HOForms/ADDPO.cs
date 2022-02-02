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

namespace SalesInventorySystem.HOForms
{
    public partial class ADDPO : DevExpress.XtraEditors.XtraForm
    {
        string updatetype = "",updsuppid,updshipno,updordercode,updordertype;
        //DataTable table;
        string code = "",suppid="",action="";
        public ADDPO()
        {
            InitializeComponent();
        }

        private void ADDPO_Load(object sender, EventArgs e)
        {
            //txtshipmentno.Text = IDGenerator.getShipmentNoSP();
            displaySupplier();
            displayBranch();
            if (VIEWPO.action == "")
            {

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;
                btndone.Enabled = false;
            }
            else
            {
                txtbranch.Enabled = true; 
                txtdate.Enabled = true;
                txtmetrics.Enabled = true;
                searchLookUpEdit1.Enabled = true;
                //string suppliername = Database.getSingleQuery("Supplier", "SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' ", "SupplierID");
                //searchLookUpEdit1.Text = suppliername;
                searchLookUpEdit1.Text = VIEWPO.suppliername;
                displayBranch();
                srchprod.Enabled = true;
                btnnew.Visible = false;
                btnadd.Enabled = true;
                btnupdate.Enabled = false;
                btncancel.Enabled = true;
                btndone.Enabled = true;
            }
            //table = new DataTable();
            //table.Columns.Add("ShipmentNo");
            //table.Columns.Add("PONumber");
            //table.Columns.Add("SupplierID");
            //table.Columns.Add("OrderType");
            //table.Columns.Add("OrderCode");
            //table.Columns.Add("Quantity");
            //table.Columns.Add("Cost");
            //gridControl1.DataSource = null;
            //gridControl1.DataSource = table;
        }
        void displaySupplier()
        {
            Database.displaySearchlookupEdit("select SupplierKey,SupplierID,SupplierName FROM Supplier", searchLookUpEdit1, "SupplierName", "SupplierName");
        }
        void displayBranch()
        {
            Database.displaySearchlookupEdit("select BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");

            Database.displaySearchlookupEdit("select * FROM Metrics", txtmetrics, "Metrics", "Metrics");
        }
        void add()
        {
            string orderType = "", ordercode = "";
            if (isProd.Checked == true)
            {
                orderType = "P"; //PRODUCTS
                ordercode = code;
            }
            else
            {
                orderType = "S"; //SERVICES
                ordercode = code;
            }
            bool isexist = Database.checkifExist("SELECT ShipmentNo FROM POSUMMARY WHERE ShipmentNo='" + txtshipmentno.Text + "' and SupplierID='" + suppid + "' and OrderType='" + ordercode + "'");
            if(isexist)
            {
                XtraMessageBox.Show("This Supplier and OrderType is already exist.. you can edit your Purchase Order details if you have additional..");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO PODETAILS VALUES('" + txtshipmentno.Text + "','" + suppid + "','" + orderType + "','" + ordercode + "','" + txtqty.Text + "','" + txtcost.Text + "','"+txtinvoiceamount.Text+"','" + txtmetrics.Text + "',0,0,0,0)");
                gridControl1.DataSource = null;
                display();
            }
            srchprod.Text="";
            txtqty.Text="";
            txtcost.Text="";
            txtinvoiceamount.Text = "";
            txtmetrics.Text = "";

            //gridView1.Columns.Clear();
            //gridControl1.DataSource = null;
            //DataRow newRow = table.NewRow();
            //newRow["ShipmentNo"] = txtshipmentno.Text;
            //newRow["PONumber"] = txtpono.Text;
            //newRow["SupplierID"] = searchLookUpEdit1.Text;
            //newRow["OrderType"] = orderType;
            //newRow["OrderCode"] = ordercode;
            //newRow["Quantity"] = txtqty.Text;
            //newRow["Cost"] = txtcost.Text;
            //table.Rows.Add(newRow);
            //gridControl1.DataSource = table;


        }

        void display()
        {
            //Database.display("SELECT ShipmentNo,SupplierID,OrderType,OrderCode,Quantity,Cost FROM PODETAILS WHERE ShipmentNo='"+txtshipmentno.Text+"' and SupplierID='"+searchLookUpEdit1.Text+"'",gridControl1,gridView1);
            Database.display("SELECT * FROM view_PODETAILS WHERE ShipmentNo='" + txtshipmentno.Text + "' and SupplierID='" + suppid + "'", gridControl1, gridView1);
        }
        void insert(string action)
        {

            try
            {
                ExecuteSP(action);
                gridView1.Columns.Clear();
                
                searchLookUpEdit1.Text = "";
                if(action!="EDITPO") { srchprod.Text = ""; }
                
                txtqty.Text = "";
                txtcost.Text = "";
                txtinvoiceamount.Text = "";
                txtmetrics.Text = "";

                txtbranch.Text = "";
               // txtcoshpy.Text = "";
                txtdate.Text = "";
               // txtinvoiceno.Text = "";
                txtremakrs.Text = "";
                XtraMessageBox.Show("Successfully Submitted");

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        void ExecuteSP(string anaction)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SP_ADDPOSUM";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmbranch", txtbranch.Text);
                com.Parameters.AddWithValue("@parmtargetdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmremarks", txtremakrs.Text);
                com.Parameters.AddWithValue("@parmorderedby", Login.Fullname);
                com.Parameters.AddWithValue("@parmaction", anaction);
                com.Parameters.AddWithValue("@parmsuppid", suppid);
                //  com.Parameters.AddWithValue("@parminvoiceno", txtinvoiceno.Text);
                //  com.Parameters.AddWithValue("@parmcoshpy", txtcoshpy.Text);
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

        private void addbtn_Click(object sender, EventArgs e)
        {
            add();
            srchprod.Focus();
        }

        private void isProd_CheckedChanged(object sender, EventArgs e)
        {
            if (isProd.Checked == true)
            {
                Database.displaySearchlookupEdit("SELECT SupplierName,ProductCategoryDescription,ProductCode,ProductName,CostKg from InventoryCost WHERE SupplierID='" + suppid + "'", srchprod, "ProductName", "ProductName");
            }
            else
            {
                Database.displaySearchlookupEdit("SELECT SupplierName,ServiceCode,ServiceName,Cost from SERVICESCOST WHERE SupplierID='" + suppid + "'", srchprod, "ServiceName", "ServiceName");
            }

        }

        private void isServices_CheckedChanged(object sender, EventArgs e)
        {
            if (isProd.Checked == true)
            {
                Database.displaySearchlookupEdit("SELECT SupplierName,ProductCategoryDescription,ProductCode,ProductName,CostKg from InventoryCost WHERE SupplierID='" + suppid + "'", srchprod, "ProductName", "ProductName");
            }
            else
            {
                Database.displaySearchlookupEdit("SELECT SupplierName,ServiceCode,ServiceName,Cost from SERVICESCOST WHERE SupplierID='" + suppid + "'", srchprod, "ServiceName", "ServiceName");
            }
        }

        private void txtproductcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void srchprod_EditValueChanged(object sender, EventArgs e)
        {
            object var,varcost;
            //string cost = "";
            if (isProd.Checked == true)
            {
                var = SearchLookUpClass.getSingleValue(srchprod, "ProductCode");
                varcost = SearchLookUpClass.getSingleValue(srchprod, "CostKg");
                //var = SearchLookUpClass.getSingleValue(srchprod, "ProductCode");
                //varcost = 0;
            }
            else
            {
                //var = SearchLookUpClass.getSingleValue(srchprod, "ServiceCode");
                //varcost = SearchLookUpClass.getSingleValue(srchprod, "Cost");
                var = SearchLookUpClass.getSingleValue(srchprod, "SRVC_DESC");
                varcost = 0;
                txtqty.Text = "1";
            }
            code = var.ToString();
            txtcost.Text = varcost.ToString();
            txtqty.Focus();
        }

        void clear()
        {
            //for (int i = 0; i <= gridView1.RowCount - 1; i++)
            //{
            //    gridView1.DeleteRow(i);
            //}
            //gridControl1.DataSource = null;

            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtbranch.Text))
            {
                XtraMessageBox.Show("Invoice Number must not Empty!...");
            }
            else
            {
                insert(action);
                XtraMessageBox.Show("PO Successfully Created!!!...");
                txtshipmentno.Enabled = false;
                txtmetrics.Enabled = false;
                srchprod.Enabled = false;
                txtqty.Enabled = false;
                txtbranch.Enabled = false;
                //txtsupplier.Enabled = true;
                txtdate.Enabled = false;

                searchLookUpEdit1.Enabled = false;

                txtqty.Text = "0";

                searchLookUpEdit1.Text = "";
                srchprod.Text = "";
                txtdate.Text = "";
                txtqty.Text = "";
                txtcost.Text = "";
                txtbranch.Text = "";


                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;
                btnsave.Enabled = false;
                clear();
                this.Dispose();
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (updatetype == "DETAILS")
            {
                
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("UPDATE PODETAILS SET Quantity='" + txtqty.Text + "',Cost='" + txtcost.Text + "',Unit='"+txtmetrics.Text+"' WHERE ShipmentNo='" + txtshipmentno.Text + "' AND SupplierID='" + gridView1.GetRowCellValue(i, "SupplierID").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(i, "OrderCode").ToString() + "'");
                }
                btnupdate.Enabled = false;
                btnadd.Enabled = true;
            }
           
        }

        private void txtcost_EditValueChanged(object sender, EventArgs e)
        {
            double costkg = 0.0, qty = 0.0,total=0.0;
            costkg = Convert.ToDouble(txtcost.Text);
            qty = Convert.ToDouble(txtqty.Text);
            total = Math.Round(costkg * qty,2);
            txtinvoiceamount.Text = total.ToString();
        }

        private void txtqty_EditValueChanged(object sender, EventArgs e)
        {
            double costkg = 0.0, qty = 0.0, total = 0.0;
            costkg = Convert.ToDouble(txtcost.Text);
            qty = Convert.ToDouble(txtqty.Text);
            total = Math.Round(costkg * qty, 2);
            txtinvoiceamount.Text = total.ToString();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM PODETAILS WHERE ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' and OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "'", "Successfully Deleted!");
            display();
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatetype = "DETAILS";

            updsuppid= gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString();
            updshipno=gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            updordercode=gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString();
            updordertype = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString();

            string ordercode = Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "OrderCode");
            string ordertype = Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "OrderType");
            searchLookUpEdit1.Text = Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "SupplierID");
            
            string prodname = "";
            searchLookUpEdit1.Text = Database.getSingleQuery("Supplier","SupplierID='"+gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"SupplierID").ToString()+"'","SupplierName");
            if (ordertype == "P")
            {
                isProd.Checked = true;
                prodname = Database.getSingleQuery("Products", "ProductCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "'  and BranchCode='" + Login.assignedBranch + "' ", "Description");

            }
            else
            {
                isServices.Checked = true;
                prodname = Database.getSingleQuery("SERVICES", "SRVC_ID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "'  ", "SRVC_DESC");

            }

            srchprod.Text = prodname;//Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridViewSummary.GetRowCellValue(gridViewSummary.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "OrderCode");
            txtqty.Text = Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "Quantity");
            txtcost.Text = Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "Cost");
            txtinvoiceamount.Text = Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "TotalCost");
            txtmetrics.Text = Database.getSingleQuery("PODETAILS", "ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' and OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' ", "Unit");
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //searchLookUpEdit1.Enabled = false;
            if(VIEWPO.action=="")
            {
                object var = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "SupplierID");
                suppid = var.ToString();
            }
            else
            {
                //object var = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "SupplierID");
                suppid = Database.getSingleQuery("Supplier", "SupplierName='" + searchLookUpEdit1.Text + "'", "SupplierID");
            }
            
            if (isProd.Checked == true)
            {
                Database.displaySearchlookupEdit("SELECT SupplierName,ProductCategoryDescription,ProductCode,ProductName,CostKg from InventoryCost WHERE SupplierID='" + suppid + "'", srchprod, "ProductName", "ProductName");
                //Database.displaySearchlookupEdit("SELECT ProductCode,Description from Products WHERE BranchCode='888' ", srchprod, "Description", "Description");
            }
            else
            {
                //Database.displaySearchlookupEdit("SELECT * from Services", srchprod, "SRVC_DESC", "SRVC_DESC");
                Database.displaySearchlookupEdit("SELECT SupplierName,ServiceCode,ServiceName,Cost from SERVICESCOST WHERE SupplierID='" + suppid + "'", srchprod, "ServiceName", "ServiceName");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            txtshipmentno.Text = IDGenerator.getIDNumberSP("sp_GetShipmentNo", "ShipmentNo"); 
            //int shipno = IDGenerator.getIDNumber("POSUMMARY", "ShipmentNo", 10000);
            //txtshipmentno.Text = shipno.ToString();
            displaySupplier();
            displayBranch();
            populateRepositoryMetrics();
            txtshipmentno.Enabled = true;
            txtmetrics.Enabled = true;
            srchprod.Enabled = true;
            txtqty.Enabled = true;
            txtbranch.Enabled = true;
            //txtsupplier.Enabled = true;
            txtdate.Enabled = true;

            searchLookUpEdit1.Enabled = true;

            txtqty.Text = "0";


            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
            btndone.Enabled = true;
        }

        private void ADDPO_FormClosed(object sender, FormClosedEventArgs e)
        {
            VIEWPO.action = "";
        }
        void populateRepositoryMetrics()
        {
            Database.displayRepositoryComboBoxItems("SELECT * FROM Metrics", "Metrics", repometrics);
        }
        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Units")
                e.RepositoryItem = repometrics;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //bool flag = false; 
            //if (gridView1.RowCount != 0)
            //{
            //    for (int i = 0; i <= gridView1.RowCount - 1; i++)
            //    {
            //        if (gridView1.GetRowCellValue(i, "SupplierID").ToString() != searchLookUpEdit1.Text && gridView1.RowCount != 0)
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }
            //}
            if (String.IsNullOrEmpty(searchLookUpEdit1.Text) || String.IsNullOrEmpty(srchprod.Text))
            {
                XtraMessageBox.Show("Please filled out the form correctly!...");
                return;
            }
            //if (flag == true)
            //{
            //    XtraMessageBox.Show("You Cant Mix Different Supplier!...");
            //}
            else
            {
                add();
                srchprod.Focus();
            }
            
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
           
            if (updatetype == "DETAILS")
            {

                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    Database.ExecuteQuery("UPDATE PODETAILS SET Quantity='" + txtqty.Text + "',Cost='" + txtcost.Text + "',TotalCost='"+Convert.ToDouble(txtqty.Text)*Convert.ToDouble(txtcost.Text)+"',Unit='" + txtmetrics.Text + "' WHERE ShipmentNo='" + updshipno + "' AND SupplierID='" + updsuppid + "' and OrderCode='" + updordercode + "' AND OrderType='"+updordertype+"'");
                }
                btnupdate.Enabled = false;
                btnadd.Enabled = true;
                display();
                srchprod.Text = "";
                txtqty.Text = "";
                txtcost.Text = "";
                txtinvoiceamount.Text = "";
                txtmetrics.Text = "";

            
            }
           
        }


        private void btncancel_Click_1(object sender, EventArgs e)
        {
            btnadd.Enabled = true;
            btnupdate.Enabled = false;


            txtshipmentno.Text = "";
            IDGenerator.getIDNumberSP("sp_GetShipmentNoMinusOne", "ShipmentNo");
            txtshipmentno.Enabled = false;
            txtmetrics.Enabled = false;
            srchprod.Enabled = false;
            txtqty.Enabled = false;
            txtbranch.Enabled = false;
            //txtsupplier.Enabled = true;
            txtdate.Enabled = false;

            searchLookUpEdit1.Enabled = false;

            txtqty.Text = "0";


            btnnew.Enabled = true;
            //btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            btndone.Enabled = false;
            //Database.ExecuteQuery("DELETE FROM PODETAILS WHERE ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' AND SupplierID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SupplierID").ToString() + "' AND OrderType='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderType").ToString() + "' AND OrderCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrderCode").ToString() + "' AND Cost='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString() + "' ");
            //display();
        }

        private void btndone_Click(object sender, EventArgs e)
        {
            groupControl2.Enabled = true;
            groupControl1.Enabled = false;
            if(VIEWPO.action != "EDITPO")
            { srchprod.Text = ""; }
           
            txtqty.Text = "";
            txtcost.Text = "";
            txtinvoiceamount.Text = "";
            txtmetrics.Text = "";
        }

        private void btnsave_Click_1(object sender, EventArgs e)
        {
            action = "";
            if(VIEWPO.action== "EDITPO")
            {
                action = "EDITPO";
            }

            bool confirm = HelperFunction.ConfirmDialog("Are you sure?", "Confirm Purchase Order");
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Cant Save.. Please add Order Details");
                return;
            }
            if (String.IsNullOrEmpty(txtbranch.Text) || String.IsNullOrEmpty(txtdate.Text)) //|| String.IsNullOrEmpty(txtcoshpy.Text)  || String.IsNullOrEmpty(txtinvoiceno.Text)
            {
                bool ok = HelperFunction.ConfirmDialog("The system found out that there are missing field.. Are you sure you want to continue? else review some fields to correct the data information", "Confirm Purchase Order");
               
                if (ok)
                {
                    if (confirm)
                    {
                        insert(action);
                        searchLookUpEdit1.Enabled = true;
                        groupControl1.Enabled = true;
                        groupControl2.Enabled = false;
                        btnnew.Enabled = true;
                        btnadd.Enabled = false;
                        btnupdate.Enabled = false;
                        btncancel.Enabled = false;
                        btndone.Enabled = false;
                        this.Dispose();
                    }
                    else
                    {
                        return;
                    }
                   

                }
                else
                {
                    return;
                }
            }
            else
            {
                if (confirm)
                {
                    insert(action);
                    searchLookUpEdit1.Enabled = true;
                   
                    groupControl1.Enabled = true;
                    groupControl2.Enabled = false;
                    btnnew.Enabled = true;
                    btnadd.Enabled = false;
                    btnupdate.Enabled = false;
                    btncancel.Enabled = false;
                    btndone.Enabled = false;
                    this.Dispose();
                }
                else
                {
                    return;
                }
               
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            groupControl2.Enabled = false;
            groupControl1.Enabled = true;
        }
    }
}