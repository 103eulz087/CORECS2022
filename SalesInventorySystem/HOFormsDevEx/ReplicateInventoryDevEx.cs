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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ReplicateInventoryDevEx : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        public static bool isdone = false;
        public ReplicateInventoryDevEx()
        {
            InitializeComponent();
        }

        private void ReplicateInventoryDevEx_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("TagNumber");
            gridControl1.DataSource = table;
        }

        void reset()
        {
            gridControl1.BeginUpdate();
            for (int i = 0; i < gridView1.RowCount;)
            { gridView1.DeleteRow(i); }
            gridControl1.DataSource = null;
            gridControl1.EndUpdate();
        }
        private void btnsubmit_Click(object sender, EventArgs e)
        {
            reset();
            string tagno = txttagno.Text.Substring(0,8);
            int inc = 1000;
            int ctr = 0, ctr1 = 0;
           
            //lastincno = tagno.Substring(8, 4);
            if (txtstart.Value != 1)
            {
                ctr1 = inc + Convert.ToInt32(txtstart.Value);
                ctr = ctr1;
            }else
            {
                ctr = inc;
            }
           
            for (int i=1;i<=numericUpDown1.Value;i++)
            {
                //ctr = inc++;
                ctr++;
                DataRow newRow = table.NewRow();
                newRow["TagNumber"] = tagno+ctr.ToString();
                table.Rows.Add(newRow);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("No AssetTag # to Replicate!...");
                return;
            }
            string body = "";
            int ctr = 0;
            for (int i = 0; i <= gridView1.RowCount - 1; i++)
            {
                bool isexist = Database.checkifExist("SELECT TagNumber FROM GenInventory WHERE TagNumber='" + gridView1.GetRowCellValue(i, "TagNumber").ToString() + "'");
                if (isexist)
                {
                    body += gridView1.GetRowCellValue(i, "TagNumber").ToString() + Environment.NewLine;
                    ctr = 1;
                }
            }
            //int invcomponents = Database.getCountData("GenInventory", "TagNumber='" + txttagno.Text + "'", "TagNumber
            if (ctr==1)
            {
                XtraMessageBox.Show("Duplicate Tag #, change your replication start number"+Environment.NewLine+Environment.NewLine + body +Environment.NewLine + Environment.NewLine + "Click retry to reconfig your replication start number or click cancel to cancel this process","Duplicate Values",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                return;
            }
            
            for (int i=0;i<=gridView1.RowCount-1;i++)
            {  
                Database.ExecuteQuery("INSERT INTO GenInventory (BranchCode,TagNumber,ItemCode,Description,Model,SerialNo,Barcode,BrandName,ItemClass,Category,Condition,Notes,JournalDescription,Vendor,PurchaseType,AccountNumber,AcquisitionDate,AcquisitionCost,Term,TypeofTerm,Location,Department,Custodian,ServiceProvider,Warranty,ServiceContract,DepreciationMethod,PhotoImage) SELECT BranchCode,'" + gridView1.GetRowCellValue(i, "TagNumber").ToString() + "',ItemCode,Description,Model,SerialNo,Barcode,BrandName,ItemClass,Category,Condition,Notes,JournalDescription,Vendor,PurchaseType,AccountNumber,AcquisitionDate,AcquisitionCost,Term,TypeofTerm,Location,Department,Custodian,ServiceProvider,Warranty,ServiceContract,DepreciationMethod,PhotoImage FROM GenInventory WHERE TagNumber='" + txttagno.Text + "'");
                // Database.ExecuteQuery("INSERT INTO InventoryComponents (ReferenceTagNo,ExistingTagNo,AssetTagNo,Description,Serial,Condition,ImagePhoto) SELECT top "+invcomponents+" ReferenceTagNo,'"+gridView1.GetRowCellValue(i,"TagNumber").ToString()+"',ExistingTagNo,Description,Serial,Condition,ImagePhoto FROM InventoryComponents WHERE ReferenceTagNo='" + txttagno.Text + "'");
                Database.ExecuteQuery("INSERT INTO InventoryComponents (ReferenceTagNo,AssetTagNo,ExistingTagNo,Description,Serial,Condition,ImagePhoto) SELECT AssetTagNo,'" + gridView1.GetRowCellValue(i, "TagNumber").ToString()+"', ExistingTagNo,Description,Serial,Condition,ImagePhoto FROM InventoryComponents WHERE AssetTagNo='" + txttagno.Text + "'");   
                //Database.ExecuteQuery("UPDATE InventoryComponents SET AssetTagNo='"+gridView1.GetRowCellValue(i,"TagNumber").ToString()+"' WHERE ")
                //Database.ExecuteQuery("SELECT BranchCode,'"+gridView1.GetRowCellValue(i,"TagNumber").ToString()+"',ItemCode,Description,Model,SerialNo,Barcode,BrandName,ItemClass,Category,Condition,Notes,JournalDescription,Vendor,PurchaseType,AccountNumber,AcquisitionDate,AcquisitionCost,Term,TypeofTerm,Location,Department,Custodian,ServiceProvider,Warranty,ServiceContract,DepreciationMethod,PhotoImage FROM GenInventory WHERE TagNumber='"+txttagno.Text+"'");
            }
            isdone = true;
            XtraMessageBox.Show("Successfully Uploaded");
            this.Close() ;
        }
    }
}