using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class AddCarcassInventory : Form
    {
        DataTable table;
        public AddCarcassInventory()
        {
            InitializeComponent();
        }

        private void AddCarcassInventory_Load(object sender, EventArgs e)
        {
            this.ActiveControl = comboBox1;
            //string str = HelperFunction.sequencePadding1(IDGenerator.getIDNumber("Inventory", "BatchCode", 1).ToString(), 6);
            txtrefno.Text = HelperFunction.sequencePadding1(IDGenerator.getIDNumber("Inventory", "BatchCode",1).ToString(),6);
            loadgridview1();
            Classes.Product.displayProductCategoryComboBoxItems(comboBox1);
            comboBox1.Focus();
        }

        void display()
        {
            Database.display("SELECT * FROM view_WarehouseInventory Where Batchcode='" + txtrefno.Text + "'", gridControl1, gridView1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                bool barcodenotexist = false;
                barcodenotexist =  Database.checkifExist("SELECT Barcode FROM Inventory WHERE Barcode = '" + txtskuno.Text + "'");

                if (txtskuno.Text=="")
                {
                    XtraMessageBox.Show("SKU must not Empty");
                    return;
                }
                else if (!barcodenotexist)
                {
                    XtraMessageBox.Show("This Barcode is not Exist in your Inventory!");
                    return;
                }
                else
                {
                    addCarcassItem();
                    display();
                    txtskuno.Text = "";
                }
                
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
                txtskuno.Text = "";
            }
            txtskuno.Focus();
        }

        void addCarcassItem()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                 string query = "sp_AddCarcassInventory";
                 SqlCommand com = new SqlCommand(query, con);
                 com.Parameters.AddWithValue("@parmbatcode", txtrefno.Text);
                 com.Parameters.AddWithValue("@parmbarcode", txtskuno.Text);
                 com.CommandType = CommandType.StoredProcedure;
                 com.CommandText = query;
                 com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        void loadgridview1()
        {
            table = new DataTable();
            table.Columns.Add("Counter");
            table.Columns.Add("Branch");
            table.Columns.Add("ShipmentNo");
            table.Columns.Add("DateReceived");
            table.Columns.Add("Product"); //UnitPrice
            table.Columns.Add("Barcode");
            gridControl1.DataSource = table;
            gridView1.BestFitColumns();
        }

        void add()
        {

            try
            {
                if (txtskuno.Text == "")
                {
                    XtraMessageBox.Show("Textfield must not empty");
                    txtskuno.Text = "";
                    txtskuno.Focus();
                }
           
                else
                {
                    add2();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void add2()
        {
            try
            {
                int ctr = 0;
                for (int i = 1; i <= gridView1.RowCount; i++)
                {
                    ctr = i;
                }
                DataRow newRow = table.NewRow();
                newRow["Counter"] = ++ctr;
                newRow["Branch"] = Login.assignedBranch;
                newRow["ShipmentNo"] = Classes.BarcodeSettings.getBarcodeShipmentNo(txtskuno.Text);
                newRow["DateReceived"] = DateTime.Now.ToShortDateString();
                newRow["Product"] = Classes.BarcodeSettings.getParentBarcodeProductCode(txtskuno.Text);
                newRow["Barcode"] = txtskuno.Text;
                table.Rows.Add(newRow);
                gridControl1.DataSource = table;
                txtskuno.Text = "";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //save();
            
            XtraMessageBox.Show("Successfully Added!");
            this.Dispose();
            this.Close();
        }

        private void txtskuno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                simpleButton1.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                simpleButton2.PerformClick();
            }
            

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE Inventory SET IsWarehouse='1',BatchCode=' ' WHERE SequenceNumber = '" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString() + "' ");//temporary query execution because existing barcode is only 13 digit character
            display();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                simpleButton3.PerformClick();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                comboBox1.Enabled = false;
                txtskuno.Enabled = true;
                txtskuno.Focus();
            }
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {

        }

        private void txtskuno_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
