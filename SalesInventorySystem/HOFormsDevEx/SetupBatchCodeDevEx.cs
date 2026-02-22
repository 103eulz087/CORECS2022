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
    public partial class SetupBatchCodeDevEx : DevExpress.XtraEditors.XtraForm
    {
        public SetupBatchCodeDevEx()
        {
            InitializeComponent();
        }

        private void SetupBatchCodeDevEx_Load(object sender, EventArgs e)
        {

            string batchnumber = IDGenerator.getIDNumberSP("sp_GetInventoryBatchCode", "BatchNumber");
            //txtbatchcodeno.Text = HelperFunction.sequencePadding1(IDGenerator.getIDNumber("Inventory", "BatchCode", 1).ToString(), 6);
            txtbatchcodeno.Text = batchnumber;
            if(String.IsNullOrEmpty(txtbatchcodeno.Text))
            {
                XtraMessageBox.Show("Error!!!..BatchCode number not Generated!..please check sp_GetInventoryBatchCode");
                this.Dispose();
            }
            //display();
        }

        void display()
        {
            Database.display("SELECT PalletNo,Product,Description,Barcode,Quantity,Available FROM Inventory with(nolock) WHERE BatchCode='" + txtbatchcodeno.Text + "' and Available > 0 and isWarehouse=1", gridControl1, gridView1);
        }

        void radChanged()
        {
            if (radioButton2.Checked == true)
            {
                panelControlmanual.Visible = false;
                panelControlauto.Visible = true;
            }
            else// if (radioButton1.Checked == true)
            {
                panelControlauto.Visible = false;
                panelControlmanual.Visible = true;
            }
        }

        private void txtshipmentno_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayDevComboBoxItems("Select distinct Description FROM Inventory with(nolock) WHERE Available > 0 and ShipmentNo='" + txtshipmentno.Text+ "'  and isWarehouse=1", "Description", txtproduct);
        }

        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.displayDevComboBoxItems("Select distinct PalletNo FROM Inventory with(nolock) WHERE Available > 0 and ShipmentNo='" + txtshipmentno.Text + "' and Description='"+txtproduct.Text+"' and BatchCode=0 and isWarehouse=1", "PalletNo", txtpalletno);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE Inventory SET BatchCode='" + txtbatchcodeno.Text + "' WHERE ShipmentNo='" + txtshipmentno.Text + "' AND Description='" + txtproduct.Text + "' AND PalletNo='" + txtpalletno.Text + "' and BatchCode=0 and Available > 0  ");
            display();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radChanged();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radChanged();
            Database.displayDevComboBoxItems("Select distinct ShipmentNo FROM Inventory with(nolock) WHERE isWarehouse=1 and Available > 0 and (BatchCode < 1 OR BatchCode is null) order by ShipmentNo", "ShipmentNo", txtshipmentno);
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Database.ExecuteQuery("UPDATE Inventory SET BatchCode='" + txtbatchcodeno.Text + "' WHERE Barcode='" + txtbarcode.Text + "' and Available > 0 ");
                txtbarcode.Text = "";
                txtbarcode.Focus();
            }
            display();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you sure?", "Setup BatchCode");
            if (confirm)
            {
                this.Dispose();
            }
            else
            {
                return;
            }
        }

        private void txtshipmentno_Click(object sender, EventArgs e)
        {
        }
    }
}