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
        private void btnadd_Click(object sender, EventArgs e)
        {
            string source = "", destination = "";
            bool isExist = false;
            if (radtobigblue.Checked == true) //transfer to bigblue
            {
                source = "Commissary";
                destination = "BigBlue";
                isExist = Database.checkifExist("SELECT TOP(1) Branch FROM InventoryBigBlue " +
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                    "AND Description='" + txtproduct.Text + "' " +
                    "AND PalletNo='" + txtpalletno.Text + "'" +
                    "AND Branch='" + Login.assignedBranch + "' ORDER BY SequenceNumber");
            }
            else //transfer to comm
            {
                source = "BigBlue";
                destination = "Commissary";
                isExist = Database.checkifExist("SELECT TOP(1) Branch FROM Inventory " +
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                    "AND Description='" + txtproduct.Text + "' " +
                    "AND PalletNo='" + txtpalletno.Text + "'" +
                    "AND Branch='" + Login.assignedBranch + "' ORDER BY SequenceNumber");
            }
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
            if (!isExist)
            {
                sp_Transfer(source, destination, "ADD");
            }
            else
            {
                XtraMessageBox.Show("Already Exist to Destination Table");
                return;
            }
        }
        void radchanged()
        {
            if (radtobigblue.Checked == true)
            {
                //clear();
                Database.displaySearchlookupEdit("SELECT distinct ShipmentNo FROM Inventory " +
                    "WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", txtshipmentno, "ShipmentNo", "ShipmentNo");
            }
            else
            {
                //clear();
                Database.displaySearchlookupEdit("SELECT distinct ShipmentNo FROM InventoryBigBlue " +
                    "WHERE Available > 0 and isStock=1 order by ShipmentNo ASC", txtshipmentno, "ShipmentNo", "ShipmentNo");
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
                    "AND isStock=1 " +
                    "order by PalletNo ASC", "PalletNo", txtpalletno);
            }
            else
            {
                Database.displayDevComboBoxItems("SELECT distinct PalletNo " +
                         "FROM InventoryBigBlue " +
                         "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                         "And Product='" + var.ToString() + "' " +
                         "AND Available > 0 " +
                         "AND isStock=1 " +
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
                    for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    {
                        //source is commissary
                        Database.ExecuteQuery("Update Inventory SET Available=0" +
                            ",isStock=0" +
                            ",ReferenceCode='Trans2BigBluePPallet'" +
                            ",LastMovementDate='" + DateTime.Now.ToShortDateString() + "' " +
                            "WHERE Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' " +
                            "and ShipmentNo='" + txtshipmentno.Text + "' " +
                            "and PalletNo='" + gridView1.GetRowCellValue(i, "PalletNo").ToString() + "'" +
                            "and SequenceNumber='" + gridView1.GetRowCellValue(i, "SequenceReferenceNumber").ToString() + "' ");
                    }
                }
                else //transfer to commissary
                {
                    source = "BigBlue";
                    destination = "Commissary";
                    sp_Transfer(source, destination, "SAVE");
                    for (int i = 0; i <= gridView1.RowCount - 1; i++)
                    {
                        //source is biglbue
                        Database.ExecuteQuery("Update InventoryBigBlue SET Available=0" +
                            ",isStock=0" +
                            ",ReferenceCode='Trans2ComPPallet'" +
                            ",LastMovementDate='" + DateTime.Now.ToShortDateString() + "' " +
                            "WHERE Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' " +
                            "and SequenceNumber='" + gridView1.GetRowCellValue(i, "SequenceReferenceNumber").ToString() + "' ");
                    }
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
                    "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                    "ORDER BY Description ASC ", txtproduct, "Description", "Description");
            }
            else
            {
                Database.displaySearchlookupEdit("SELECT DISTINCT Product,Description FROM InventoryBigBlue " +
                   "WHERE ShipmentNo='" + txtshipmentno.Text + "' " +
                   "ORDER BY Description ASC ", txtproduct, "Description", "Description");
            }
        }
    }
}