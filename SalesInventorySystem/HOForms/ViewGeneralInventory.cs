using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public partial class ViewGeneralInventory : Form
    {
        public static string status = "",isvat="";
        public ViewGeneralInventory()
        {
            InitializeComponent();
        }

         private void ViewGeneralInventory_Load(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT BranchName FROM Branches", "BranchName", comboBox1);
        }

        //private void comboBox1_Click(object sender, EventArgs e)
        //{
           
        //    Database.displayComboBoxItems("SELECT BranchName FROM Branches", "BranchName", comboBox1);
            
        //}

        private String getBranchCode()
        {
            return Database.getSingleData("Branches", "BranchName", comboBox1.Text, "BranchCode");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "")
                {
                    XtraMessageBox.Show("Please Select Branch");
                }
                else
                {
                   
                    if (comboBox1.Text == "HEAD OFFICE" && checkBox1.Checked == true) //head office and isConversion
                    {
                        if(bigblue.Checked==true)//bigblue inventory
                        {
                            if (checkBox2.Checked == true) //with date filtering
                            {
                                Database.display("select Branch,Product,Barcode,Description,Cost,Available,DateReceived,ExpiryDate,isVat,SequenceNumber FROM InventoryBigBlue WHERE Branch='888' AND Available > 0 and isStock=1 and isConversion=1 AND DateReceived >= '" + dateEdit1.Text + "' AND DateReceived <= '" + dateEdit2.Text + "'", gridControl1, gridView1);
                            }
                            else
                            {
                                Database.display("select Branch,Product,Barcode,Description,Cost,Available,DateReceived,ExpiryDate,isVat,SequenceNumber FROM InventoryBigBlue WHERE Branch='888' AND Available > 0 and isStock=1 and isConversion=1", gridControl1, gridView1);
                            }
                        }
                        else
                        {
                            if (checkBox2.Checked == true) //with date filtering commissary
                            {
                                Database.display("select Branch,Product,Barcode,Description,Cost,Available,DateReceived,ExpiryDate,isVat,SequenceNumber FROM Inventory WHERE Branch='888' AND Available > 0 and isStock=1 and isConversion=1 AND DateReceived >= '" + dateEdit1.Text + "' AND DateReceived <= '" + dateEdit2.Text + "'", gridControl1, gridView1);
                            }
                            else
                            {
                                Database.display("select  Branch,Product,Barcode,Description,Cost,Available,DateReceived,ExpiryDate,isVat,SequenceNumber FROM Inventory WHERE Branch='888' AND Available > 0 and isStock=1 and isConversion=1", gridControl1, gridView1);
                            }
                        }
                        
                    }
                    else if (comboBox1.Text == "HEAD OFFICE" && checkBox1.Checked == false) //head office and ALL conversion and nonconversion
                    {
                        if (bigblue.Checked == true)
                        {
                            if (checkBox2.Checked == true) //with date filtering
                            {
                                Database.display("select Branch,Product,Barcode,Description,Cost,Available,DateReceived,ExpiryDate,isVat,SequenceNumber FROM InventoryBigBlue WHERE Branch='888' AND Available > 0 and isStock=1 AND DateReceived >= '" + dateEdit1.Text + "' AND DateReceived <= '" + dateEdit2.Text + "'", gridControl1, gridView1);
                            }
                            else
                            {
                                Database.display("select Branch,Product,Barcode,Description,Cost,Available,DateReceived,ExpiryDate,isVat,SequenceNumber FROM InventoryBigBlue WHERE Branch='888' AND Available > 0 and isStock=1  ", gridControl1, gridView1);
                            }
                        }
                        else
                        {
                            if (checkBox2.Checked == true) //with date filtering
                            {
                                Database.display("select * FROM vw_generalInventory WHERE Branch='888' AND Available > 0 AND DateReceived >= '" + dateEdit1.Text + "' AND DateReceived <= '" + dateEdit2.Text + "'", gridControl1, gridView1);
                            }
                            else
                            {
                                Database.display("select * FROM vw_generalInventory WHERE Branch='888' AND Available > 0  ", gridControl1, gridView1);
                            }
                        }
                         

                       
                    }
                    else if(comboBox1.Text != "HEAD OFFICE" && checkBox1.Checked == true)
                    {
                        if (checkBox2.Checked == true)
                        {
                            Database.display("select * FROM vw_generalInventory WHERE Branch='" + getBranchCode() + "' AND  isConversion='1' AND DateReceived >= '" + dateEdit1.Text + "' AND DateReceived <= '" + dateEdit2.Text + "'", gridControl1, gridView1);
                        }
                        else
                        {
                            Database.display("select * FROM vw_generalInventory WHERE Branch='" + getBranchCode() + "' AND isConversion=1", gridControl1, gridView1);
                        }
                       
                    }
                    else if (comboBox1.Text != "HEAD OFFICE" && checkBox1.Checked == false)
                    {
                        if (checkBox2.Checked == true)
                        {
                            Database.display("select * FROM vw_generalInventory WHERE Branch='" + getBranchCode() + "' AND DateReceived >= '" + dateEdit1.Text + "' AND DateReceived <= '" + dateEdit2.Text + "'", gridControl1, gridView1);
                        }
                        else
                        {
                            Database.display("select * FROM vw_generalInventory WHERE Branch='" + getBranchCode() + "' ", gridControl1, gridView1);
                        }
                        
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text=="HEAD OFFICE")
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void adjustCostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "Cost")
                e.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i=0;i<=gridView1.RowCount-1;i++)
            {
                if(bigblue.Checked==true)
                {
                    Database.ExecuteQuery("UPDATE InventoryBigBlue SET Cost='" + gridView1.GetRowCellValue(i, "Cost").ToString() + "' WHERE SequenceNumber='" + gridView1.GetRowCellValue(i, "SequenceNumber").ToString() + "'");

                }
                else 
                {
                    Database.ExecuteQuery("UPDATE Inventory SET Cost='" + gridView1.GetRowCellValue(i, "Cost").ToString() + "' WHERE SequenceNumber='" + gridView1.GetRowCellValue(i, "SequenceNumber").ToString() + "'");
                }
            }
            comboBox1.Text = "";
            gridView1.Columns.Clear();
            XtraMessageBox.Show("Successfully Updated!");
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                dateEdit1.Enabled = true;
                dateEdit2.Enabled = true;
            }
            else
            {
                dateEdit1.Enabled = false;
                dateEdit2.Enabled = false;
            }
        }

        private void otherExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = "DeductOtherExpense";
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            invtrans.Text = "Deduct Inventory Adjustment";
            invtrans.lblstatus.Text = "DeductOtherExpense";
        }



        private void otherIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = "AddOtherIncome";
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            invtrans.Text = "Add Inventory Adjustment";
            invtrans.lblstatus.Text = "AddOtherIncome";
        }

        private void inTransitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = "AddInTransit";
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            invtrans.Text = "Add Inventory Adjustment";
            invtrans.lblstatus.Text = "AddInTransit";
        }

        private void inTransitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            status = "DeductInTransit";
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            invtrans.Text = "Deduct Inventory Adjustment";
            invtrans.lblstatus.Text = "DeductInTransit";
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            status = "AddIntransit";
            //string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            //string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            //string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            //string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            //string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            //string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            //isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            //HOFormsDevEx.InventoryCostAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryCostAdjustmentDevEx();
            //invtrans.Show();
            //invtrans.txtrefnum.Text = seqnum;
            //invtrans.txtbranch.Text = branch;
            //invtrans.txtprodcode.Text = prodcode;
            //invtrans.txtitemname.Text = prodname;
            //invtrans.txtcostkg.Text = cost;
            //invtrans.txtqty.Text = quantity;
            //invtrans.Text = "Add Inventory Adjustment";

            //status = "AdjustCost";
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string shipmentno = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ShipmentNo").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryCostAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryCostAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtshipmentno.Text = shipmentno;
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            double oldcost = 0.0;
            oldcost = Convert.ToDouble(cost) * Convert.ToDouble(quantity);
            invtrans.txtoldcostvalue.Text = oldcost.ToString();
            invtrans.Text = "Add Inventory Adjustment";
        }

        private void deductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            status = "DeductIntransit";
            //string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            //string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            //string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            //string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            //string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            //string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            //isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            //HOFormsDevEx.InventoryCostAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryCostAdjustmentDevEx();
            //invtrans.Show();
            //invtrans.txtrefnum.Text = seqnum;
            //invtrans.txtbranch.Text = branch;
            //invtrans.txtprodcode.Text = prodcode;
            //invtrans.txtitemname.Text = prodname;
            //invtrans.txtcostkg.Text = cost;
            //invtrans.txtqty.Text = quantity;
            //invtrans.Text = "Add Inventory Adjustment";

            //status = "AdjustCost";
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryCostAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryCostAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            double oldcost = 0.0;
            oldcost = Convert.ToDouble(cost) * Convert.ToDouble(quantity);
            invtrans.txtoldcostvalue.Text = oldcost.ToString();
            invtrans.Text = "Add Inventory Adjustment";
        }

        private void addToSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = "AddAddtoSupplier"; 
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            invtrans.Text = "Add Inventory Adjustment";
            invtrans.lblstatus.Text = "AddAddtoSupplier";
        }

        private void deductToSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status = "DeductDeducttoSupplier";
            string seqnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
            string branch = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Branch").ToString();
            string prodcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
            string prodname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            string cost = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
            string quantity = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            isvat = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isVat").ToString();
            HOFormsDevEx.InventoryAdjustmentDevEx invtrans = new HOFormsDevEx.InventoryAdjustmentDevEx();
            invtrans.Show();
            invtrans.txtrefnum.Text = seqnum;
            invtrans.txtbranch.Text = branch;
            invtrans.txtprodcode.Text = prodcode;
            invtrans.txtitemname.Text = prodname;
            invtrans.txtcostkg.Text = cost;
            invtrans.txtqty.Text = quantity;
            invtrans.Text = "Add Inventory Adjustment";
            invtrans.lblstatus.Text = "DeductDeducttoSupplier";
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Available")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void deductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
