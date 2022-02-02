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
using System.Data.OleDb;
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class CarcassCostingDevEx : DevExpress.XtraEditors.XtraForm
    {
        public CarcassCostingDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textEdit1.Text = ofd.FileName;
            }
            loadFile();
        }
        private void loadFile()
        {
            String sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textEdit1.Text + ";Extended Properties=" + "\"Excel 8.0;HDR=Yes;\"";
            OleDbConnection con = new OleDbConnection(sexcelconnectionstring);
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * FROM [sheet1$]", con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

        }

        private void Import()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    //string queryInsert = "INSERT INTO BatchUpload VALUES('" + Login.assignedBranch + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "','" + DateTime.Now.ToShortDateString() + "')";
                    // Database.ExecuteQuery("INSERT INTO BatchUpload VALUES('" + Login.assignedBranch + "','" + dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "','" + DateTime.Now.ToShortDateString() + "')");
                    Database.ExecuteQuery("INSERT INTO TempCosting VALUES('" + dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() + "','" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[3].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[4].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[5].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[6].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[7].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[8].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[9].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[10].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[11].Value.ToString() + "','" + dataGridView1.Rows[i].Cells[12].Value.ToString() + "')");

                }

                XtraMessageBox.Show("Successfully Added!");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

            con.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool isexist = Database.checkifExist("SELECT ShipmentNo FROM TempCosting WHERE ShipmentNo='" + dataGridView1.Rows[0].Cells[0].Value.ToString().Trim() + "'");
            if (isexist)
            {
                XtraMessageBox.Show("Shipment Number Already Exist..");
                return;
            }
            else
            {
                Import();
                batchUpload();
                dataGridView1.Columns.Clear();
            }
        }
        void batchUpload()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_BatchUpload";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipno", dataGridView1.Rows[0].Cells[0].Value.ToString().Trim());
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Done");
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

        private void CarcassCostingDevEx_Load(object sender, EventArgs e)
        {
            populateShipment();
        }

        void populateShipment()
        {
            Database.displayDevComboBoxItems("SELECT distinct ShipmentNo FROM TempCosting ORDER BY ShipmentNo", "ShipmentNo", comboBoxEdit1);
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM TempCosting WHERE ShipmentNo='" + comboBoxEdit1.Text + "'", gridControl1, gridView1);
            Classes.DevXGridViewSettings.setGridFormat(gridView1);
            gridView1.BestFitColumns();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(comboBoxEdit1.Text))
            {
                XtraMessageBox.Show("You must select shipment number to delete");
                return;
            }
            else
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this Carcass Costing?", "Delete Costing");
                if (ok)
                {
                    deleteCosting();
                }
                else
                {
                    return;
                }
            }
            this.Dispose();
            
        }

        void deleteCosting()
        {
            Database.ExecuteQuery("DELETE FROM TempCosting WHERE ShipmentNo='" + comboBoxEdit1.Text + "'", "Successfully Deleted");
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "InvoiceCostAdjustment")
            {
                if (Convert.ToDouble(e.CellValue) < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
           
        }
    }
}