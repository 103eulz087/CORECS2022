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
    public partial class JFCInventory : DevExpress.XtraEditors.XtraForm
    {
        object var = null;
        public JFCInventory()
        {
            InitializeComponent();
        }

        private void JFCInventory_Load(object sender, EventArgs e)
        {
            populate();
            display();
        }
        void populate()
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches order by BranchCode", txtbrcode, "BranchName", "BranchName");
        }
        void display()
        {
            if(radsum.Checked==true)
            {
                Database.display("SELECT * FROM view_JFCInventorySummary  order by ShipmentNo", gridControl1, gridView1);
            }
            else
            {
                Database.display("SELECT * FROM view_JFCInventoryDetailed  order by ShipmentNo,PalletNo", gridControl1, gridView1);
            }
            
        }

        private void txtbrcode_EditValueChanged(object sender, EventArgs e)
        {
             var = SearchLookUpClass.getSingleValue(txtbrcode, "BranchCode");
            if (radsum.Checked == true)
            {
                if (checkBox1.Checked == true)
                {
                    Database.display("SELECT * FROM view_JFCInventorySummary WHERE   Branch='" + var.ToString() + "' order by ShipmentNo", gridControl1, gridView1);
                }
                else
                {
                    Database.display("SELECT * FROM view_JFCInventorySummary    order by ShipmentNo,PalletNo", gridControl1, gridView1);
                }
            }
            else
            {
                if (checkBox1.Checked == true)
                {
                    Database.display("SELECT * FROM view_JFCInventoryDetailed WHERE   Branch='" + var.ToString() + "' order by ShipmentNo", gridControl1, gridView1);
                }
                else
                {
                    Database.display("SELECT * FROM view_JFCInventoryDetailed   order by ShipmentNo,PalletNo", gridControl1, gridView1);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                txtbrcode.Enabled = true;
            }
            else
            {
                txtbrcode.Enabled = false;
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            
                if (e.Button == MouseButtons.Right)
                {
                    
                        contextMenuStrip1.Show(gridControl1, e.Location);
                }
            
        }

        private void refreshDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display();
            txtbrcode.Enabled = false;
            checkBox1.Checked = false;
        }

        void checkChanged()
        {
            if (radsum.Checked == true)
            {
                display();
                txtbrcode.Enabled = false;
                checkBox1.Checked = false;
                contextMenuStrip1.Items[1].Visible = false;
            }
            else
            {
                display();
                txtbrcode.Enabled = false;
                checkBox1.Checked = false;
                contextMenuStrip1.Items[1].Visible = true;
            }
        }

        private void radsum_CheckedChanged(object sender, EventArgs e)
        {
            checkChanged();
            
        }

        private void raddetailed_CheckedChanged(object sender, EventArgs e)
        {
            checkChanged(); 
            
        }

        private void showPerBoxItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HOFormsDevEx.JFCInventoryPerBoxDevEx jfcinv = new JFCInventoryPerBoxDevEx();
            jfcinv.Show();
            Database.display("SELECT * FROM view_JFCInventoryPerBox WHERE ShipmentNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString() + "' and PalletNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PalletNo").ToString() + "' and Description='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString() + "'", jfcinv.gridControl1, jfcinv.gridView1);
        }
    }
}