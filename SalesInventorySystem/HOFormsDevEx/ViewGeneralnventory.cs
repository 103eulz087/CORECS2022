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
using System.IO;
using System.Data.SqlClient;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewGeneralnventory : DevExpress.XtraEditors.XtraForm
    {
        public static string proctype = "",tagno="";
        public ViewGeneralnventory()
        {
            InitializeComponent();
      
        }

        private void ViewGeneralnventory_Load(object sender, EventArgs e)
        {
            display();
            populateItems();
        }
        
        void populateItems()
        {
            
        }

        void display()
        {
           
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            proctype = "ADD";
            HOFormsDevEx.GenInventoryDevEx gendev = new GenInventoryDevEx();
            gendev.Show();
            gendev.btnsubmit.Text = "ADD";



            Database.displaySearchlookupEdit("SELECT * FROM GenInventoryCategory", gendev.txtclasscat, "Category", "Category");
            Database.displaySearchlookupEdit("SELECT * FROM GenInventoryItems", gendev.txtdesc, "Description", "Description");
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", gendev.txtbrcode, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", gendev.txtvendor, "SupplierName", "SupplierName");
            Database.displaySearchlookupEdit("SELECT LocationID,LocationName FROM Location", gendev.txtloc, "LocationName", "LocationName");
            Database.displaySearchlookupEdit("SELECT DeptID,DeptName FROM Departments", gendev.txtdept, "DeptName", "DeptName");

            if (HOFormsDevEx.GenInventoryDevEx.isdone == true)
            {
                display();
            }
            HOFormsDevEx.GenInventoryDevEx.isdone = false;
        }

      
        private void gridControl1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            proctype = "DELETE";
            Database.ExecuteQuery("DELETE FROM GenInventory WHERE TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Successfully Deleted!");
            display();
        }

        private void txtcategory_EditValueChanged(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_GenInventory WHERE Category='" + txtcategory.Text + "'", gridControl1, gridView1);
        }

        private void showDepriciationScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSchedule();
        }

        void showSchedule()
        {
            DateTime today = DateTime.Today;
            DateTime endOfMonth = new DateTime(today.Year,
                                               today.Month,
                                               DateTime.DaysInMonth(today.Year,
                                                                    today.Month));
            HOFormsDevEx.ViewDepreciationSchedule viewsked = new ViewDepreciationSchedule();
            viewsked.Show();
            Database.display("SELECT * FROM DepreciationSchedule WHERE TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", viewsked.gridControl1, viewsked.gridView1);
            
            StyleFormatCondition stle = new StyleFormatCondition(FormatConditionEnum.Equal, viewsked.gridView1.Columns["DateDepreciate"], null, endOfMonth);
            stle.Appearance.BackColor = Color.LightSeaGreen;
            stle.ApplyToRow = true;
            viewsked.gridView1.FormatConditions.Add(stle);
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
           
        }

        private void txtcustodian_EditValueChanged(object sender, EventArgs e)
        {
            Database.display("SELECT TagNumber,Description,Category,Model,ItemClass,Location,Department,AcquisitionCost,AcquisitionDate FROM GenInventory WHERE Custodian='" + txtcustodian.Text + "'", gridControl1, gridView1);
        }

        private void txtvendor_EditValueChanged(object sender, EventArgs e)
        {
            Database.display("SELECT TagNumber,Description,Category,Model,ItemClass,Location,Department,AcquisitionCost,AcquisitionDate FROM GenInventory WHERE Vendor='" + txtvendor.Text + "'", gridControl1, gridView1);
        }

        private void txtlocation_EditValueChanged(object sender, EventArgs e)
        {
            Database.display("SELECT TagNumber,Description,Category,Model,ItemClass,Location,Department,AcquisitionCost,AcquisitionDate FROM GenInventory WHERE Custodian='" + txtcustodian.Text + "'", gridControl1, gridView1);
        }

        private void txtdept_EditValueChanged(object sender, EventArgs e)
        {
            Database.display("SELECT TagNumber,Description,Category,Model,ItemClass,Location,Department,AcquisitionCost,AcquisitionDate FROM GenInventory WHERE Department='" + txtdept.Text + "'", gridControl1, gridView1);
        }

        private void addInventoryComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.GenInventoryComponentsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.GenInventoryComponentsDevEx pcusatfsmr = new HOFormsDevEx.GenInventoryComponentsDevEx();
            pcusatfsmr.Show();
            string mark = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString();
            Database.display("SELECT * FROM InventoryComponents WHERE AssetTagNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", pcusatfsmr.gridControl1, pcusatfsmr.gridView1);
            pcusatfsmr.txttagno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString();
            pcusatfsmr.txtdesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            pcusatfsmr.txtmodel.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Model").ToString();
            pcusatfsmr.txtclasscat.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemClass").ToString();
            pcusatfsmr.txtloc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Location").ToString();
            pcusatfsmr.txtdept.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Department").ToString();
        }

        private void replicateInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.ReplicateInventoryDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.ReplicateInventoryDevEx pcusatfsmr = new HOFormsDevEx.ReplicateInventoryDevEx();
            pcusatfsmr.Show();
            pcusatfsmr.txttagno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString();
            if(HOFormsDevEx.ReplicateInventoryDevEx.isdone == true)
            {
                refreshDisplay();
                ReplicateInventoryDevEx.isdone = false;
            }
        }
        
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int dx = 5;
            //Rectangle r = e.Bounds;
            //r.X += e.Bounds.Height + dx * 2;
            //r.Width -= (e.Bounds.Height + dx * 3);
            //e.Graphics.DrawImage(DevExpress.XtraEditors.Controls.ByteImageConverter.FromByteArray(
            //    (byte[])gridView1.GetDataRow(e.RowHandle)["PhotoImage"]), e.Bounds.X + dx,
            //    e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
            ////e.Appearance.DrawString(e.Cache, gridView1.GetRow(e.RowHandle), r);
            //e.Handled = true;

            GridView currentView = sender as GridView;
            if (e.RowHandle == currentView.FocusedRowHandle) return;
            Rectangle r = e.Bounds;
            if (e.Column.FieldName == "PhotoImage")
            {

                r.X += e.Bounds.Height + dx * 2;
                r.Width -= (e.Bounds.Height + dx * 3);
                e.Graphics.DrawImage(DevExpress.XtraEditors.Controls.ByteImageConverter.FromByteArray(
                    (byte[])gridView1.GetDataRow(e.RowHandle)["PhotoImage"]), e.Bounds.X + dx,
                    e.Bounds.Y, e.Bounds.Height, e.Bounds.Height);
                //e.Appearance.DrawString(e.Cache, gridView1.GetRow(e.RowHandle), r);
                e.Handled = true;
            }
        }
        void refreshDisplay()
        {
            Database.display("Select TOP 100 * FROM view_GenInventory ", gridControl1, gridView1);
        }
        private void refreshDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshDisplay();
        }

        private void printAssetTagBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Barcode.AssetTagBarcode bprint = new Barcode.AssetTagBarcode();
            bprint.lbldate.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "AcquisitionDate"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountNumber").ToString();
            bprint.lbldescription.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Description"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountNumber").ToString();
            bprint.xrBarCode2.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "TagNumber"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountNumber").ToString();                                                                                                                                                                   //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            //report.Print();
            report.ShowRibbonPreviewDialog();
        }

        private void gridView6_RowClick(object sender, RowClickEventArgs e)
        {
            //pictureBox1.Image = null;
            //SqlConnection con = Database.getConnection();
            //con.Open();
            //try
            //{
            //    string query = "select * FROM InventoryComponents WHERE AssetTagNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AssetTagNo").ToString() + "' AND Description='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString() + "' AND Serial='"+ gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Serial").ToString() + "'";
            //    SqlCommand com = new SqlCommand(query, con);
            //    SqlDataReader reader = com.ExecuteReader();
            //    if (reader != null)
            //    {
            //        if (reader.Read())
            //        {
            //            if (reader["PhotoImage"] == System.DBNull.Value)
            //            {
            //                pictureBox1.Image = null;
            //            }
            //            else
            //            {
            //                byte[] img = null;
            //                img = (byte[])reader["PhotoImage"];
            //                MemoryStream ms = new MemoryStream(img);
            //                ms.Seek(0, SeekOrigin.Begin);
            //                pictureBox1.Image = Image.FromStream(ms);
            //            }

            //        }
            //    }
            //    else
            //    {
            //        pictureBox1.Image = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(HOFormsDevEx.GenInventoryDetailsDevEx))
                {
                    form.Activate();
                    return;
                }
            }
            HOFormsDevEx.GenInventoryDetailsDevEx pcusatfsmr = new HOFormsDevEx.GenInventoryDetailsDevEx();
            pcusatfsmr.Show();

            //byte[] mypicbyte = Classes.Utilities.GetImage(pcusatfsmr.pictureBox1, "ReportHeaderSettings", "ReportName='checkvoucher'", "ImageLogo");
            //pcusatfsmr.pictureBox1 = mypicbyte;
            pcusatfsmr.pictureBox1.Image = null;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "select * FROM GenInventory WHERE TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (reader["PhotoImage"] == System.DBNull.Value)
                        {
                            pcusatfsmr.pictureBox1.Image = null;
                        }
                        else
                        {
                            byte[] img = null;
                            img = (byte[])reader["PhotoImage"];
                            MemoryStream ms = new MemoryStream(img);
                            ms.Seek(0, SeekOrigin.Begin);
                            pcusatfsmr.pictureBox1.Image = Image.FromStream(ms);
                        }

                    }
                }
                else
                {
                    pcusatfsmr.pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            pcusatfsmr.txttagno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString();
            pcusatfsmr.txtacctno.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "AccountNumber"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountNumber").ToString();
            pcusatfsmr.txtacqcost.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "AcquisitionCost"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AcquisitionCost").ToString();
            pcusatfsmr.txtacqdate.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "AcquisitionDate"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AcquisitionDate").ToString();
            pcusatfsmr.txtbarcode.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Barcode"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString();
            pcusatfsmr.txtbrandname.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "BrandName");// gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BrandName").ToString();
            pcusatfsmr.txtnewcat.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Category"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Category").ToString();
            pcusatfsmr.txtcondition.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Condition"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Condition").ToString();
            pcusatfsmr.txtnewcustodian.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Custodian"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Custodian").ToString();
            pcusatfsmr.txtdepmethod.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "DepreciationMethod");// gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DepreciationMethod").ToString();
            pcusatfsmr.txtnewdept.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Department"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Department").ToString();
            pcusatfsmr.txtdesc.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Description"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            pcusatfsmr.txtitemclass.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "ItemClass"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ItemClass").ToString();
            pcusatfsmr.txtnewloc.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Location"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Location").ToString();
            pcusatfsmr.txtmodel.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Model"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Model").ToString();
            pcusatfsmr.txtpurchtype.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "PurchaseType"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PurchaseType").ToString();
            pcusatfsmr.txtserialno.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "SerialNo"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SerialNo").ToString();
            pcusatfsmr.txtservicecontract.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "ServiceContract"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ServiceContract").ToString();
            pcusatfsmr.txtserviceprov.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "ServiceProvider"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ServiceProvider").ToString();
            pcusatfsmr.txttagno.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "TagNumber"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString();
            pcusatfsmr.txtterm.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Term"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Term").ToString();
            pcusatfsmr.txtnewvendor.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Vendor"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Vendor").ToString();
            pcusatfsmr.txtwarranty.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Warranty"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Warranty").ToString();
            pcusatfsmr.txtnewbranch.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "BranchCode"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Warranty").ToString();
            pcusatfsmr.txtnotes.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "Notes"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Warranty").ToString();
            pcusatfsmr.txtjournaldesc.Text = Database.getSingleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", "JournalDescription"); //gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Warranty").ToString();
            //displayComponents();
            Database.display("SELECT * FROM view_InventoryComponents WHERE AssetTagNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'", pcusatfsmr.gridControl2, pcusatfsmr.gridView6);
            pcusatfsmr.gridView6.BestFitColumns();

        }

        private void navi(object sender, EventArgs e)
        {

        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            proctype = "UPDATE";
            HOFormsDevEx.GenInventoryDevEx gendev = new GenInventoryDevEx();
            gendev.Show();
            gendev.btnsubmit.Text = "UPDATE";
            tagno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString();

            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "select * FROM GenInventory WHERE TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "'";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                if (reader.Read())
                {
                    if (reader["PhotoImage"] == System.DBNull.Value)
                    {
                        gendev.pictureBox1.Image = null;
                    }
                    else
                    {
                        byte[] img = null;
                        img = (byte[])reader["PhotoImage"];
                        MemoryStream ms = new MemoryStream(img);
                        ms.Seek(0, SeekOrigin.Begin);
                        gendev.pictureBox1.Image = Image.FromStream(ms);
                    }

                }
            }
            else
            {
                gendev.pictureBox1.Image = null;
            }
           

            Database.displaySearchlookupEdit("SELECT * FROM GenInventoryCategory", gendev.txtclasscat, "Category", "Category");
            Database.displaySearchlookupEdit("SELECT * FROM GenInventoryItems", gendev.txtdesc, "Description", "Description");
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", gendev.txtbrcode, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", gendev.txtvendor, "SupplierName", "SupplierName");
            Database.displaySearchlookupEdit("SELECT LocationID,LocationName FROM Location", gendev.txtloc, "LocationName", "LocationName");
            Database.displaySearchlookupEdit("SELECT DeptID,DeptName FROM Departments", gendev.txtdept, "DeptName", "DeptName");
            
            var rows = Database.getMultipleQuery("GenInventory", "TagNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TagNumber").ToString() + "' ", "AccountNumber," +
                "AcquisitionCost" +
                ",AcquisitionDate" +
                ",Barcode" +
                ",BrandName" +
                ",BranchCode" +
                ",Category" +
                ",Condition" +
                ",Custodian" +
                ",DepreciationMethod" +
                ",Department" +
                ",Description" +
                ",ItemClass" +
                ",JournalDescription" +
                ",Location" +
                ",Model" +
                ",Notes" +
                ",PurchaseType" +
                ",SerialNo" +
                ",ServiceContract" +
                ",ServiceProvider" +
                ",TagNumber" +
                ",Term" +
                ",TypeOfTerm" +
                ",Vendor" +
                ",Warranty");
            string AccountNumber = rows["AccountNumber"].ToString();
            string AcquisitionCost = rows["AcquisitionCost"].ToString();
            string AcquisitionDate = rows["AcquisitionDate"].ToString();
            string Barcode = rows["Barcode"].ToString();
            string BrandName = rows["BrandName"].ToString();
            string BranchCode = rows["BranchCode"].ToString();
            string Category = rows["Category"].ToString();
            string Condition = rows["Condition"].ToString();
            string Custodian = rows["Custodian"].ToString();
            string DepreciationMethod = rows["DepreciationMethod"].ToString();
            string Department = rows["Department"].ToString();
            string Description = rows["Description"].ToString();
            string ItemClass = rows["ItemClass"].ToString();
            string JournalDescription = rows["JournalDescription"].ToString();
            string Location = rows["Location"].ToString();
            string Model = rows["Model"].ToString();
            string Notes = rows["Notes"].ToString();
            string PurchaseType = rows["PurchaseType"].ToString();
            string SerialNo = rows["SerialNo"].ToString();
            string ServiceContract = rows["ServiceContract"].ToString();
            string ServiceProvider = rows["ServiceProvider"].ToString();
            string TagNumber = rows["TagNumber"].ToString();
            string Term = rows["Term"].ToString();
            string TypeOfTerm = rows["TypeOfTerm"].ToString();
            string Vendor = rows["Vendor"].ToString();
            string Warranty = rows["Warranty"].ToString();



            gendev.txtacctno.Text = AccountNumber;
            gendev.txtacqcost.Text = AcquisitionCost;
            gendev.txtacqdate.Text = AcquisitionDate;
            gendev.txtbarcode.Text = Barcode;
            gendev.txtbrandname.Text = BrandName;
            gendev.txtbrcode.Text = BranchCode;

            gendev.txtclasscat.Text = Category;

            gendev.txtcondition.Text = Condition;
            gendev.txtcustodian.Text = Custodian;

            gendev.txtdepmethod.Text = DepreciationMethod;
            gendev.txtdept.Text = Department;

            gendev.txtdesc.Text = Description;
            gendev.txtitemclass.Text = ItemClass;
            gendev.txtjournaldesc.Text = JournalDescription;
            gendev.txtloc.Text = Location;
            gendev.txtmodel.Text = Model;
            gendev.txtnotes.Text = Notes;
            gendev.txtpurchtype.Text = PurchaseType;
            gendev.txtserialno.Text = SerialNo;
            gendev.txtservicecontract.Text = ServiceContract;
            gendev.txtserviceprov.Text = ServiceProvider;
            gendev.txttagno.Text = TagNumber;
            gendev.txtterm.Text = Term;
            gendev.txttypeofterm.Text = TypeOfTerm;
            gendev.txtvendor.Text = Vendor;
            gendev.txtwarranty.Text = Warranty;
        }
    }
}