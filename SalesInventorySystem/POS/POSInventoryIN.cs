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
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.POS
{
    public partial class POSInventoryIN : DevExpress.XtraEditors.XtraForm
    {
        object pcode, desc, barcode, isvat;
        public POSInventoryIN()
        {
            InitializeComponent();
        }

        void addInventory()
        {
            Database.ExecuteQuery("INSERT INTO TempInventoryIN (ID,BranchCode,DateReceived,ExpiryDate,ProductCode,Description,Barcode,Cost,Quantity,isVat,isDone,DateEncode,EncodeBy) " +
                "VALUES('" + txtbatchid.Text + "'" +
                $",'{txtbrcode.Text}'" +
                $",'{txtdatein.Text}'" +
                $",'{txtexpirydate.Text}'" +
                $",'{pcode}'" +
                $",'{desc}'" +
                $",'{barcode}'" +
                $",'{txtcost.Text}'" +
                $",'{txtqty.Text}'" +
                $",'{isvat}'" +
                $",'0'" +
                $",'{DateTime.Now.ToShortDateString()}'" +
                $",'{Login.isglobalUserID}')", "Succesfully Added");
        }

        void display()
        {
            Database.display("SELECT * FROM TempInventoryIN WHERE ID='" + txtbatchid.Text + "' " +
                "AND EncodeBy='"+Login.isglobalUserID+"' " +
                "AND DateEncode='"+DateTime.Now.ToShortDateString()+"'", gridControl1, gridView2);
        }

        private void POSInventoryIN_Load(object sender, EventArgs e)
        {
           
            loadData();
        }

        void loadData()
        {
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches ORDER BY BranchCode", txtbrcode,"BranchCode","BranchCode");
            Database.displaySearchlookupEdit("Select a.ProductCode,a.Description,a.Barcode,b.Description as Category,b.isVat " +
                "FROM Products as a " +
                "INNER JOIN ProductCategory as b " +
                "ON a.ProductCategoryCode=b.ProductCategoryID " +
                "WHERE a.BranchCode='"+Login.assignedBranch+"'", txtproduct,"Description","Description");
        }

        void uploadInventory()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_UploadInventoryIN";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmid", txtbatchid.Text);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(gridView2.RowCount == 0 || String.IsNullOrEmpty(txtbatchid.Text))
            {
                XtraMessageBox.Show("No Items to Upload!...");
                return;
            }
            else
            {
                uploadInventory();
                XtraMessageBox.Show("Success");
                this.Dispose();
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            int id = IDGenerator.getIDNumber("TempInventoryIN", "ID", 1);
            txtbatchid.Text = id.ToString();
            btnnew.Enabled = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //POSInventoryINRecovertBatchID posinvin = new POSInventoryINRecovertBatchID();
            //posinvin.ShowDialog(this);
            //if(POSInventoryINRecovertBatchID.isdone==true)
            //{
            //    txtbatchid.Text=POSInventoryINRecovertBatchID.ID;
            //    posinvin.Dispose();
            //}
            bool check = Database.checkifExist("SELECT TOP(1) ID FROM TempInventoryIN WHERE isDone=0");
            if (!check)//true
            {
                XtraMessageBox.Show("No Items to Recover");
                return;
            }
            else
            {
                POS.POSUnUploadInv posinv = new POSUnUploadInv();
                Database.display("SELECT distinct ID FROM TempInventoryIN WHERE isDone=0", posinv.gridControl1, posinv.gridView2);
                posinv.ShowDialog(this);
                if (POS.POSUnUploadInv.isdone == true)
                {
                    //retrieve
                    txtbatchid.Text = POS.POSUnUploadInv.invID;
                    btnnew.Enabled = false;
                    Database.display("SELECT * FROM TempInventoryIN WHERE ID='" + txtbatchid.Text + "' ", gridControl1, gridView2);
                    POS.POSUnUploadInv.isdone = false;
                    posinv.Dispose();
                }
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM TempInventoryIN WHERE ID='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString() + "' " +
                "AND BranchCode='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "BranchCode").ToString() + "' " +
                "AND ProductCode='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ProductCode").ToString() + "' " +
                "AND Quantity='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "Quantity").ToString() + "' " +
                "AND DateEncode='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "DateEncode").ToString() + "' " +
                "AND EncodeBy='" + gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "EncodeBy").ToString() + "'", "Successfully Deleted");
            display();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount == 0)
            {
                XtraMessageBox.Show("No Items to Print!...");
                return;
            }
            else
            {
               

                //var row = Database.getMultipleQuery("ReportHeaderSettings", "ReportName='StockOrderRep' ", "Heading,ImageWidth,ImageHeight,Caption1,Caption2");

                //string companyname = row["Heading"].ToString();
                //string imagewidth = row["ImageWidth"].ToString();
                //string imageheight = row["ImageHeight"].ToString();
                //string caption1 = row["Caption1"].ToString();
                //string caption2 = row["Caption2"].ToString();

                DeliveryReceiptFrm devrcptfrm = new DeliveryReceiptFrm();
                devrcptfrm.xrdevno.Text = txtbatchid.Text;
                devrcptfrm.xrdate.Text = txtdatein.Text; //Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateProcessed").ToString()).ToShortDateString();dt.ToShortDateString();

                devrcptfrm.Landscape = false;
                //Classes.Utilities.GetImageDevEx(devrcptfrm.xrPictureBox1, "ReportHeaderSettings", "ReportName='StockOrderRep'", "ImageLogo");
                //devrcptfrm.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
                //devrcptfrm.xrcompanyname.Text = companyname;
                //devrcptfrm.xrcaption1.Text = caption1;
                //devrcptfrm.xrcaption2.Text = caption2;
                devrcptfrm.xrdeliveredby.Text = Login.Fullname;
                devrcptfrm.PaperKind = System.Drawing.Printing.PaperKind.A4;

                gridView2.Columns["ID"].Visible = false;
                gridView2.Columns["BranchCode"].Visible = false;
                gridView2.Columns["Barcode"].Visible = false;
                gridView2.Columns["Cost"].Visible = false;
                gridView2.Columns["isVat"].Visible = false;
                gridView2.Columns["isDone"].Visible = false;
                gridView2.Columns["DateEncode"].Visible = false;
                gridView2.Columns["EncodeBy"].Visible = false;

                devrcptfrm.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
                devrcptfrm.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
                ReportPrintTool report = new ReportPrintTool(devrcptfrm);
                report.ShowRibbonPreviewDialog();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtbatchid.Text) || String.IsNullOrEmpty(txtdatein.Text) || String.IsNullOrEmpty(txtproduct.Text))
            {
                XtraMessageBox.Show("No Empty Fields");
                return;
            }
            else
            {
                addInventory();
                display();
            }
         
        }

        private void txtproduct_EditValueChanged(object sender, EventArgs e)
        {
           
            pcode=SearchLookUpClass.getSingleValue(txtproduct, "ProductCode");
            desc = SearchLookUpClass.getSingleValue(txtproduct, "Description");
            barcode = SearchLookUpClass.getSingleValue(txtproduct, "Barcode"); 
            isvat = SearchLookUpClass.getSingleValue(txtproduct, "isVat");
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }
    }
}