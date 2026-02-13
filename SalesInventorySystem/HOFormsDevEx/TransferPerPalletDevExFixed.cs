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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class TransferPerPalletDevExFixed : DevExpress.XtraEditors.XtraForm
    {
        object var;
        public TransferPerPalletDevExFixed()
        {
            InitializeComponent();
        }

        private void btnadd_Click(object sender, EventArgs e) => StagePallet();
        
        void radchanged()
        {
            if (radtobigblue.Checked == true)
            {
                //clear();
                Database.displaySearchlookupEdit("SELECT distinct ShipmentNo FROM Inventory " +
                    "WHERE Available > 0 and isWarehouse=1 order by ShipmentNo ASC", txtshipmentno, "ShipmentNo", "ShipmentNo");
            }
            else
            {
                //clear();
                Database.displaySearchlookupEdit("SELECT distinct ShipmentNo FROM Inventory " +
                    "WHERE Available > 0 and isWarehouse=0 order by ShipmentNo ASC", txtshipmentno, "ShipmentNo", "ShipmentNo");
            }
        }
        void clear()
        {
            txtshipmentno.Text = "";
            txtproduct.Text = "";
            txtpalletno.Text = "";
        }
        private void radtobigblue_CheckedChanged(object sender, EventArgs e)
        {
            radchanged();
        }

        private void txtproduct_EditValueChanged(object sender, EventArgs e)
        {
            //gipulihan og SelectedProductCode
            //var = SearchLookUpClass.getSingleValue(txtproduct, "Product");
            if (radtobigblue.Checked == true) //source is commissary
            {
                Database.displayDevComboBoxItems("SELECT distinct PalletNo " +
                    "FROM Inventory " +
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                    "And Product='" + SelectedProductCode + "' " +
                    "AND Available > 0 " +
                    "AND isWarehouse = 1 " +
                    "order by PalletNo ASC", "PalletNo", txtpalletno);
            }
            else
            {
                Database.displayDevComboBoxItems("SELECT distinct PalletNo " +
                         "FROM Inventory " +
                         "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                         "And Product='" + SelectedProductCode + "' " +
                         "AND Available > 0 " +
                         "AND isWarehouse=0 " +
                         "order by PalletNo ASC", "PalletNo", txtpalletno);
            }
        }

        private void radtocomm_CheckedChanged(object sender, EventArgs e)
        {
            radchanged();
        }

        
        private void btnsave_Click(object sender, EventArgs e) => CommitPallet();
        

        private void TransferPerPalletDevEx_Load(object sender, EventArgs e)
        {
            radchanged();
            txtbatchno.Text = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber"); //IDGenerator.getTransferedNumber();
        }

        private void txtshipmentno_EditValueChanged(object sender, EventArgs e)
        {
            if (radtobigblue.Checked == true)
            {
                Database.displaySearchlookupEdit("SELECT DISTINCT Product,Description FROM Inventory " +
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "' and isWarehouse=1 and Available > 0 " +
                    "ORDER BY Description ASC ", txtproduct, "Description", "Description");
            }
            else
            {
                Database.displaySearchlookupEdit("SELECT DISTINCT Product,Description FROM Inventory " +
                   "WHERE ShipmentNo='" + txtshipmentno.Text + "' and isWarehouse=0 and Available > 0 " +
                   "ORDER BY Description ASC ", txtproduct, "Description", "Description");
            }
        }

        void BigBlueTemplate()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='CustomerRequest'", "Caption2");

            //DevExReportTemplate.CustomerRequest xct = new DevExReportTemplate.CustomerRequest();
            DevExReportTemplate.TransferInventory xct = new DevExReportTemplate.TransferInventory();
            xct.Landscape = false;
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);


            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtransfertype.Text = "Transfer to BigBlue";
            xct.xrdispatchno.Text = txtdispatchno.Text;

            //xct.xrdateneeded.Text = DateTime.Now.ToShortDateString();
            //xct.xrrequestedby.Text = Login.Fullname;
            //xct.xrdaterequest.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", DateTime.Now);
            //xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);


            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        void CommissaryTemplate()
        {
            string companyname = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Heading");
            string imagewidth = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageWidth");
            string imageheight = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageHeight");
            string caption1 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption1");
            string caption2 = Database.getSingleQuery("ReportHeaderSettings", "ReportName='StorageReceivingForm'", "Caption2");

            DevExReportTemplate.StorageReceivingForm xct = new DevExReportTemplate.StorageReceivingForm();

            Classes.Utilities.GetImageDevEx(xct.xrPictureBox1, "ReportHeaderSettings", "ReportName='StorageReceivingForm'", "ImageLogo");
            xct.xrPictureBox1.SizeF = new SizeF(float.Parse(imagewidth), float.Parse(imageheight));
            xct.xrcompanyname.Text = companyname;
            xct.xrcaption1.Text = caption1;
            xct.xrcaption2.Text = caption2;

            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.A4;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.xrdate.Text = DateTime.Now.ToShortDateString();
            xct.xrpreparedby.Text = Login.Fullname;
            xct.xrtime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
            //xct.xrdateneeded.Text = String.Format("{0:MM/dd/yyyy HH:mm:ss}", dateTimePicker1.Value);
            //xct.Font = new System.Drawing.Font("Arial Narrow", 8);

            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount <= 0)
            {
                XtraMessageBox.Show("No Data to print");
                return;
            }
            if (radtobigblue.Checked == true)
            {
                BigBlueTemplate();
            }
            else
            {
                CommissaryTemplate();
            }
        }


        private string SelectedProductCode =>
            SearchLookUpClass.getSingleValue(txtproduct, "Product")?.ToString();

        private void StagePallet()
        {
            if (string.IsNullOrWhiteSpace(txtdispatchno.Text))
            {
                XtraMessageBox.Show("Add Dispatch No first.");
                txtdispatchno.Focus();
                return;
            }
            if (!radtobigblue.Checked && !radtocomm.Checked)
            {
                XtraMessageBox.Show("Please select Transfer Type.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtshipmentno.Text) ||
                string.IsNullOrWhiteSpace(SelectedProductCode) ||
                string.IsNullOrWhiteSpace(txtpalletno.Text))
            {
                XtraMessageBox.Show("Please complete Shipment, Product, and Pallet.");
                return;
            }

            string source = radtobigblue.Checked ? "Commissary" : "BigBlue";
            string destination = radtobigblue.Checked ? "BigBlue" : "Commissary";

            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("dbo.sp_StagePalletForTransfer", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BatchNo", SqlDbType.Int).Value = int.Parse(txtbatchno.Text);
                cmd.Parameters.Add("@ShipmentNo", SqlDbType.VarChar, 50).Value = txtshipmentno.Text.Trim();
                cmd.Parameters.Add("@Product", SqlDbType.VarChar, 50).Value = SelectedProductCode;
                cmd.Parameters.Add("@PalletNo", SqlDbType.VarChar, 50).Value = txtpalletno.Text.Trim();
                cmd.Parameters.Add("@Source", SqlDbType.VarChar, 20).Value = source;
                cmd.Parameters.Add("@Destination", SqlDbType.VarChar, 20).Value = destination;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 50).Value = Login.assignedBranch;
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = Login.Fullname;

                var table = new DataTable();
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
            }
        }


        private void CommitPallet()
        {
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Please stage (ADD) items before saving.");
                return;
            }

            string source = radtobigblue.Checked ? "Commissary" : "BigBlue";
            string destination = radtobigblue.Checked ? "BigBlue" : "Commissary";

            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("dbo.sp_CommitTransferPallet", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BatchNo", SqlDbType.Int).Value = int.Parse(txtbatchno.Text);
                cmd.Parameters.Add("@ShipmentNo", SqlDbType.VarChar, 50).Value = txtshipmentno.Text.Trim();
                cmd.Parameters.Add("@Product", SqlDbType.VarChar, 50).Value = SelectedProductCode;
                cmd.Parameters.Add("@PalletNo", SqlDbType.VarChar, 50).Value = txtpalletno.Text.Trim();
                cmd.Parameters.Add("@Source", SqlDbType.VarChar, 20).Value = source;
                cmd.Parameters.Add("@Destination", SqlDbType.VarChar, 20).Value = destination;
                cmd.Parameters.Add("@DispatchNo", SqlDbType.VarChar, 50).Value = txtdispatchno.Text.Trim();
                cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = Login.Fullname;
                cmd.Parameters.Add("@Branch", SqlDbType.VarChar, 50).Value = Login.assignedBranch;

                con.Open();
                cmd.ExecuteNonQuery();
            }

            XtraMessageBox.Show("Inventory Successfully Transferred.");
            this.Dispose();
        }


    }
}