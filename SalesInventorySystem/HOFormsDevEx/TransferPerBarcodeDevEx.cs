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
    public partial class TransferPerBarcodeDevEx : DevExpress.XtraEditors.XtraForm
    {
        public TransferPerBarcodeDevEx()
        {
            InitializeComponent();
        }

        private void TransferPerBarcodeDevEx_Load(object sender, EventArgs e)
        {
            txtbatchno.Text = IDGenerator.getIDNumberSP("sp_GetTransNumber", "BatchNumber"); //IDGenerator.getTransferedNumber();
            txtbarcodeno.Focus();
        }

        void add()
        {
            bool checkifExists = false;
            string source = "", destination = "";
            string SequenceNumber = "", Product = "", Description = "", Quantity = "", Available = "", DateReceived = "", ExpiryDate = "";
            bool checkifAlreadyInserted = false;



            if (radtobigblue.Checked == true) //Transfer to BigBlue
            {
                source = "Commissary";
                destination = "BigBlue";
                checkifExists = Database.checkifExist("SELECT TOP 1 Barcode " +
                    "FROM Inventory " +
                    "WHERE Barcode='" + txtbarcodeno.Text + "' " +
                    "AND Available > 0 " +
                    "AND isStock=1 ");


            }
            else //Transfer to Commissary
            {
                source = "BigBlue";
                destination = "Commissary";
                checkifExists = Database.checkifExist("SELECT TOP 1 Barcode " +
                      "FROM InventoryBigblue " +
                      "WHERE Barcode='" + txtbarcodeno.Text + "' " +
                      "AND Available > 0 " +
                      "AND isStock=1 ");

            }
            if (!checkifExists)
            {
                XtraMessageBox.Show("Barcode Not Exist in your Inventory");
                txtbarcodeno.Text = "";
                txtbarcodeno.Focus();
                return;
            }
            else
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    if (gridView1.GetRowCellValue(i, "BarCode").ToString() == txtbarcodeno.Text)
                    {
                        checkifAlreadyInserted = true;
                    }
                }
                if (checkifAlreadyInserted) //if barcode exists
                {
                    XtraMessageBox.Show("Barcode Already Exist");
                    txtbarcodeno.Text = "";
                    txtbarcodeno.Focus();
                    return;
                }

                if (radtobigblue.Checked == true)
                {
                    var rows = Database.getMultipleQuery("Inventory", "Barcode='" + txtbarcodeno.Text + "' AND Available > 0 AND isStock=1"
                        , "SequenceNumber,Product,DateReceived,ExpiryDate,Description,Quantity,Available");
                    SequenceNumber = rows["SequenceNumber"].ToString();
                    Product = rows["Product"].ToString();
                    DateReceived = rows["DateReceived"].ToString();
                    ExpiryDate = rows["ExpiryDate"].ToString();
                    Description = rows["Description"].ToString();
                    Quantity = rows["Quantity"].ToString();
                    Available = rows["Available"].ToString();
                }
                else if (radtocomm.Checked == true)
                {
                    var rows = Database.getMultipleQuery("InventoryBigblue", "Barcode='" + txtbarcodeno.Text + "' AND Available > 0 AND isStock=1"
                        , "SequenceNumber,Product,DateReceived,ExpiryDate,Description,Quantity,Available");
                    SequenceNumber = rows["SequenceNumber"].ToString();
                    Product = rows["Product"].ToString();
                    DateReceived = rows["DateReceived"].ToString();
                    ExpiryDate = rows["ExpiryDate"].ToString();
                    Description = rows["Description"].ToString();
                    Quantity = rows["Quantity"].ToString();
                    Available = rows["Available"].ToString();
                }

                Database.ExecuteQuery("INSERT INTO TempInventoryTransferPerBarcode VALUES ('" + txtbatchno.Text + "'" +
                    ",'" + SequenceNumber + "'" +
                    ",'" + Product + "'" +
                    ",'" + DateReceived + "'" +
                    ",'" + ExpiryDate + "'" +
                    ",'" + Description + "'" +
                    ",'" + txtbarcodeno.Text + "'" +
                    ",'" + Quantity + "'" +
                    ",'" + Available + "'" +
                    ",'" + source + "'" +
                    ",'" + destination + "'" +
                    ",'" + Login.Fullname + "')");
                display();
            }
            txtbarcodeno.Text = "";
            txtbarcodeno.Focus();
        }

        void display()
        {
            //DISPLAY AFTER INSERT
            Database.display("SELECT SequenceReferenceNumber,ProductCode,Description,BarCode,Quantity,Available " +
                "FROM TempInventoryTransferPerBarcode " +
                "WHERE BatchNumber='" + txtbatchno.Text + "'", gridControl1, gridView1);
        }
        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_TransferPerBarcode";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbatchno", txtbatchno.Text);
            com.Parameters.AddWithValue("@parmdispatchno", txtdispatchno.Text);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            bool checkitems = Database.checkifExist("SELECT TOP 1 ProductCode FROM TempInventoryTransferPerBarcode WHERE BatchNumber='" + txtbatchno.Text + "'");
            if (gridView1.RowCount == 0)
            {
                XtraMessageBox.Show("Nothing to Save");
                return;
            }
            else if (!checkitems)
            {
                XtraMessageBox.Show("No Inventory Items Entered!..");
                return;
            }
            else if (String.IsNullOrEmpty(txtdispatchno.Text))
            {
                XtraMessageBox.Show("Please Provide Dispatch Number!...");
                txtdispatchno.Focus();
                return;
            }
            else
            {
                execute();
                XtraMessageBox.Show("Inventory Successfully Transfered!...");
                this.Dispose();
            }
        }

        private void txtbarcodeno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnadd.PerformClick();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM TempInventoryTransferPerBarcode " +
                "WHERE SequenceReferenceNumber='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceReferenceNumber").ToString() + "'");
            display();
        }
    }
}