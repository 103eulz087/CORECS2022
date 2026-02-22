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
    public partial class TransferPerPalletDevEx : DevExpress.XtraEditors.XtraForm
    {
        object var;
        public TransferPerPalletDevEx()
        {
            InitializeComponent();
        }

        void sp_Transfer(string source, string destination, string option)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_TransferByPallet";
            //try
            //{
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
            com.Parameters.AddWithValue("@parmprodcode", var.ToString());
            com.Parameters.AddWithValue("@parmpalletno", txtpalletno.Text);
            com.Parameters.AddWithValue("@parmbatchnumber", txtbatchno.Text);
            com.Parameters.AddWithValue("@parmdispatchno", txtdispatchno.Text);
            com.Parameters.AddWithValue("@parmsource", source);
            com.Parameters.AddWithValue("@parmdestination", destination);
            com.Parameters.AddWithValue("@parmuser", Login.Fullname);
            com.Parameters.AddWithValue("@parmoption", option);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;

            if (option == "ADD")
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                gridView1.Columns.Clear();
                gridControl1.DataSource = null;
                adapter.Fill(table);
                gridControl1.DataSource = table;
                gridView1.BestFitColumns();
            }
            else
            {
                com.ExecuteNonQuery();
            }
            con.Close();
        }

        private void CallTransferByPallet(string option)
        {
            // Derive source/destination
            string source = radtobigblue.Checked ? "Commissary" : "BigBlue";
            string destination = radtobigblue.Checked ? "BigBlue" : "Commissary";

            using (var con = Database.getConnection())
            using (var cmd = new SqlCommand("dbo.sp_TransferByPallet", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Typed parameters with reasonable sizes
                cmd.Parameters.Add("@parmshipmentno", SqlDbType.VarChar, 50).Value = txtshipmentno.Text?.Trim();
                cmd.Parameters.Add("@parmprodcode", SqlDbType.VarChar, 50).Value = var.ToString() ?? "";
                cmd.Parameters.Add("@parmpalletno", SqlDbType.VarChar, 50).Value = txtpalletno.Text?.Trim();
                cmd.Parameters.Add("@parmbatchnumber", SqlDbType.VarChar, 50).Value = txtbatchno.Text?.Trim();
                cmd.Parameters.Add("@parmdispatchno", SqlDbType.VarChar, 50).Value = txtdispatchno.Text?.Trim();
                cmd.Parameters.Add("@parmsource", SqlDbType.VarChar, 30).Value = source;
                cmd.Parameters.Add("@parmdestination", SqlDbType.VarChar, 30).Value = destination;
                cmd.Parameters.Add("@parmuser", SqlDbType.VarChar, 100).Value = Login.Fullname;
                cmd.Parameters.Add("@parmoption", SqlDbType.VarChar, 20).Value = option;
                cmd.Parameters.Add("@parmbranch", SqlDbType.VarChar, 50).Value = Login.assignedBranch; // NEW

                con.Open();

                if (option == "ADD")
                {
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        gridControl1.DataSource = table;
                        gridView1.BestFitColumns();
                    }
                }
                else // "SAVE"
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
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
                string.IsNullOrWhiteSpace(var.ToString()) ||
                string.IsNullOrWhiteSpace(txtpalletno.Text))
            {
                XtraMessageBox.Show("Please complete Shipment, Product, and Pallet.");
                return;
            }

            try
            {
                CallTransferByPallet("ADD");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message, "Stage failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //string source = "", destination = "";
            //bool isExist = false;
            //if (radtobigblue.Checked == true) //transfer to bigblue
            //{
            //    source = "Commissary";
            //    destination = "BigBlue";
            //    isExist = Database.checkifExist("SELECT TOP(1) Branch FROM Inventory " +
            //        "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
            //        "AND Description='" + txtproduct.Text + "' " +
            //        "AND PalletNo='" + txtpalletno.Text + "'" +
            //         "AND isWarehouse=0 " +
            //        "AND Branch='" + Login.assignedBranch + "' ORDER BY SequenceNumber");
            //}
            //else //transfer to comm
            //{
            //    source = "BigBlue";
            //    destination = "Commissary";
            //    isExist = Database.checkifExist("SELECT TOP(1) Branch FROM Inventory " +
            //        "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
            //        "AND Description='" + txtproduct.Text + "' " +
            //        "AND PalletNo='" + txtpalletno.Text + "'" +
            //         "AND isWarehouse=1 " +
            //        "AND Branch='" + Login.assignedBranch + "' ORDER BY SequenceNumber");
            //}
            //if (String.IsNullOrEmpty(txtdispatchno.Text))
            //{
            //    XtraMessageBox.Show("Add Dispatch No first.");
            //    txtdispatchno.Focus();
            //    return;
            //}
            //if (radtobigblue.Checked == false && radtocomm.Checked == false)
            //{
            //    XtraMessageBox.Show("Please Select Transfer Type");
            //    return;
            //}
            //if (!isExist)
            //{
            //    sp_Transfer(source, destination, "ADD");
            //}
            //else
            //{
            //    XtraMessageBox.Show("Already Exist to Destination Table");
            //    return;
            //}
        }
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

            var = SearchLookUpClass.getSingleValue(txtproduct, "Product");
            if (radtobigblue.Checked == true) //source is commissary
            {
                Database.displayDevComboBoxItems("SELECT distinct PalletNo " +
                    "FROM Inventory " +
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                    "And Product='" + var.ToString() + "' " +
                    "AND Available > 0 " +
                    "AND isWarehouse = 1 " +
                    "order by PalletNo ASC", "PalletNo", txtpalletno);
            }
            else
            {
                Database.displayDevComboBoxItems("SELECT distinct PalletNo " +
                         "FROM Inventory " +
                         "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                         "And Product='" + var.ToString() + "' " +
                         "AND Available > 0 " +
                         "AND isWarehouse=0 " +
                         "order by PalletNo ASC", "PalletNo", txtpalletno);
            }
        }

        private void radtocomm_CheckedChanged(object sender, EventArgs e)
        {
            radchanged();
        }

        void save()
        {
            try
            {

                string source = "", destination = "";
                if (radtobigblue.Checked == true) //transfer to bigblue
                {
                    source = "Commissary";
                    destination = "BigBlue";
                    sp_Transfer(source, destination, "SAVE");
                    //for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    //{
                    //    //source is commissary
                    //    Database.ExecuteQuery("Update Inventory SET isWarehouse=0" +
                    //        ",ReferenceCode='Trans2BigBluePPallet'" +
                    //        ",LastMovementDate='" + DateTime.Now.ToShortDateString() + "' " +
                    //        "WHERE Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' " +
                    //        "and ShipmentNo='" + txtshipmentno.Text + "' " +
                    //        "and PalletNo='" + gridView1.GetRowCellValue(i, "PalletNo").ToString() + "'" +
                    //        "and SequenceNumber='" + gridView1.GetRowCellValue(i, "SequenceReferenceNumber").ToString() + "' ");
                    //}
                }
                else //transfer to commissary
                {
                    source = "BigBlue";
                    destination = "Commissary";
                    sp_Transfer(source, destination, "SAVE");
                    //for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    //{
                    //    //source is biglbue
                    //    Database.ExecuteQuery("Update Inventory SET isWarehouse=1" +
                    //        ",ReferenceCode='Trans2ComPPallet'" +
                    //        ",LastMovementDate='" + DateTime.Now.ToShortDateString() + "' " +
                    //        "WHERE Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' " +
                    //        "and SequenceNumber='" + gridView1.GetRowCellValue(i, "SequenceReferenceNumber").ToString() + "' ");
                    //}
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtdispatchno.Text))
            {
                XtraMessageBox.Show("Add Dispatch No first.");
                txtdispatchno.Focus();
                return;
            }
            if (radtobigblue.Checked == false && radtocomm.Checked == false)
            {
                XtraMessageBox.Show("Please Select Transfer Type");
                return;
            }
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Please Check Transferred Items");
                return;
            }
            else
            {
                save();
                XtraMessageBox.Show("Inventory Successfully Transferred");
                this.Dispose();
            }
        }

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
    }
}