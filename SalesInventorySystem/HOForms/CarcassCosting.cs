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
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using DevExpress.XtraEditors;

namespace SalesInventorySystem.HOForms
{
    public partial class CarcassCosting : Form
    {
        DataTable table;
        public CarcassCosting()
        {
            InitializeComponent();
        }

        private void CarcassCosting_Load(object sender, EventArgs e)
        {
            loadShipment();
            
           // Database.display("Select * FROM CarcassCosting", gridControl1, gridView1);
            table = new DataTable();
            table.Columns.Add("Code");
            table.Columns.Add("Item");
            table.Columns.Add("Cutting");
            table.Columns.Add("TestCut");
            table.Columns.Add("QtyPerContainer");
            table.Columns.Add("CostAllocation");
            table.Columns.Add("CostAdjustment");
            table.Columns.Add("InvoiceAmount");
            table.Columns.Add("Butchery");
            table.Columns.Add("Freight");
            table.Columns.Add("TotalCost");
            table.Columns.Add("CostPerKilo");
            table.Columns.Add("SRP");
            //table.Columns.Add("Markup");
           
            gridControl1.DataSource = table;
        }

        void loadShipment()
        {
            Database.displayComboBoxItems("SELECT * FROM ShipmentOrder", "ShipmentNo", txtshipment);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Database.display("SELECT  ProductCode AS Code,Description AS Item,0 AS TestCut,0 as QtyPerContainer,0 as CostAllocation,0 as CostAdjustment,0 as InvoiceAmount,0 as Butchery,0 as Freight,0 as TotalCost,0 as CostPerKilo,0 as SRP FROM Products WHERE BranchCode=888 AND isPrimalCut=1", gridControl1, gridView1);
            //gridView1.Columns["TestCut"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            
            // Database.ExecuteQuery("SELECT  ProductCode,Description, 0,0,0,0,0,0,0,0,0,0 FROM Products WHERE Branch=888 AND IsPrimalCut=1")
            add();
            gridView1.SetRowCellValue(0, "Code", "10015");
            gridView1.SetRowCellValue(0, "Item", "Belly BI");

            gridView1.SetRowCellValue(1, "Code", "10020");
            gridView1.SetRowCellValue(1, "Item", "Belly BL");

            gridView1.SetRowCellValue(2, "Code", "10030");
            gridView1.SetRowCellValue(2, "Item", "Pata Front");

            gridView1.SetRowCellValue(3, "Code", "10035");
            gridView1.SetRowCellValue(3, "Item", "Ham Strips");

            gridView1.SetRowCellValue(4, "Code", "10040");
            gridView1.SetRowCellValue(4, "Item", "Humba Cut");

            gridView1.SetRowCellValue(5, "Code", "10265");
            gridView1.SetRowCellValue(5, "Item", "Trimmings");

            gridView1.SetRowCellValue(6, "Code", "10060");
            gridView1.SetRowCellValue(6, "Item", "Menudo Cut");

            gridView1.SetRowCellValue(7, "Code", "10070");
            gridView1.SetRowCellValue(7, "Item", "Pata Slice");

            gridView1.SetRowCellValue(8, "Code", "10075");
            gridView1.SetRowCellValue(8, "Item", "Pigue");

            gridView1.SetRowCellValue(9, "Code", "10085");
            gridView1.SetRowCellValue(9, "Item", "Pork Chop BI");

            gridView1.SetRowCellValue(10, "Code", "10145");
            gridView1.SetRowCellValue(10, "Item", "Pork Strips");

            gridView1.SetRowCellValue(11, "Code", "10095");
            gridView1.SetRowCellValue(11, "Item", "Pork Cutlets");

            gridView1.SetRowCellValue(12, "Code", "10115");
            gridView1.SetRowCellValue(12, "Item", "Pork Feet");

            gridView1.SetRowCellValue(13, "Code", "10140");
            gridView1.SetRowCellValue(13, "Item", "Pork Steak Cut");

            gridView1.SetRowCellValue(14, "Code", "10150");
            gridView1.SetRowCellValue(14, "Item", "Sinigang Cut");

            gridView1.SetRowCellValue(15, "Code", "10155");
            gridView1.SetRowCellValue(15, "Item", "Soup Bones");

            gridView1.SetRowCellValue(16, "Code", "10160");
            gridView1.SetRowCellValue(16, "Item", "Spare Ribs");

            gridView1.SetRowCellValue(17, "Code", "10165");
            gridView1.SetRowCellValue(17, "Item", "Tenderloin");

            gridView1.SetRowCellValue(18, "Code", "10175");
            gridView1.SetRowCellValue(18, "Item", "Sawdust");
          
        }

        void setItemTemplate()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
        }

        void add()
        {
            for (int i = 0; i <= 18; i++)
            {
                DataRow newRow = table.NewRow();
                newRow["Code"] = 0;
                newRow["Item"] = 0;
                newRow["Cutting"] = 0;
                newRow["TestCut"] = 0;
                newRow["QtyPerContainer"] = 0;
                newRow["CostAllocation"] = 0;
                newRow["CostAdjustment"] = 0;
                newRow["InvoiceAmount"] = 0;
                newRow["Butchery"] = 0;
                newRow["Freight"] = 0;
                newRow["TotalCost"] = 0;
                newRow["CostPerKilo"] = 0;
                newRow["SRP"] = 0;
                //newRow["Markup"] = 0;
                table.Rows.Add(newRow);
                gridControl1.DataSource = table;
            }
        }

      
        String testcutPecentage(string value)
        {
            string str = "";
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            double total = 0.0;
            total = double.Parse(value.TrimEnd(new[] { '%' })) / 100;
            str = total.ToString();
            return str;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           //gridView1.SetRowCellValue(e.RowHandle, gridView1.Columns["QtyPerContainer"], qty.ToString());
            try
            {
                

                double qty = 0.0,costallocation=0.0,invoiceamount=0.0,butchery=0.0,freight=0.0,totalcost=0.0,costperkilo=0.0;
                //qty = Math.Round(Convert.ToDouble(txtqtypercontainer.Text) * Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TestCut").ToString()), 2);
                qty = Math.Round(Convert.ToDouble(txtqtypercontainer.Text) * Convert.ToDouble(testcutPecentage(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TestCut").ToString())), 9);
                costallocation = Math.Round(Convert.ToDouble(txtcostallocation.Text) * Convert.ToDouble(testcutPecentage(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TestCut").ToString())),9);

                invoiceamount = Math.Round(costallocation + Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CostAdjustment").ToString()),9);

                butchery = Math.Round(Convert.ToDouble(txtbutchery.Text) * Convert.ToDouble(testcutPecentage(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TestCut").ToString())),9);
                freight = Math.Round(Convert.ToDouble(txtfreight.Text) * Convert.ToDouble(testcutPecentage(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TestCut").ToString())), 9);

                totalcost = Math.Round(invoiceamount + butchery + freight,2);
                costperkilo = Math.Round(totalcost / Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "QtyPerContainer").ToString()),2);
                if (e.Column.FieldName == "Cutting")
                {
                    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                    double total = 0.0;
                    total = Math.Round(Convert.ToDouble(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Cutting").ToString()) / Convert.ToDouble(txttotaltestcut.Text), 9);
                    //textBox3.Text = total.ToString();
                    //textBox4.Text = total.ToString("P", nfi); //DisplayPercentage(total);
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TestCut", total.ToString("P", nfi));
                }
                else if (e.Column.FieldName == "TestCut")
                {
                   // gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Cutting", testcutPecentage(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TestCut").ToString()));
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "QtyPerContainer", qty.ToString());
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "CostAllocation", costallocation.ToString());
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Butchery", butchery.ToString());
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Freight", freight.ToString());
                }
                else if (e.Column.FieldName == "CostAdjustment")
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "InvoiceAmount", invoiceamount.ToString());
                }
                else if (e.Column.FieldName == "Butchery")
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalCost", totalcost.ToString());
                }
                else if (e.Column.FieldName == "Freight")
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalCost", totalcost.ToString());
                }
                else if (e.Column.FieldName == "TotalCost")
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "CostPerKilo", costperkilo.ToString());
                }
                else if (e.Column.FieldName == "InvoiceAmount")
                {
                    gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalCost", costperkilo.ToString());
                }
            }
            catch (StackOverflowException ex) 
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Cutting")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
            if (e.Column.FieldName == "TestCut")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
            if (e.Column.FieldName == "CostAdjustment")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
          
        }

        String getQtyPerContainer()
        {
            string str = "";
            str = Database.getSingleQuery("ShipmentOrder", "ShipmentNo='" + txtshipment.Text + "'", "ActualKilos");
            return str;
        }
        String getInvoiceCostAllocation()
        {
            string str = "";
            str = Database.getSingleQuery("OrderDetails", "ShipmentNo='" + txtshipment.Text + "' and ProductCode='10005'", "TotalProductCost");
            return str;
        }
        String getButcheryCost()
        {
            string str = "";
            str = Database.getSingleQuery("ShipmentOrder", "ShipmentNo='" + txtshipment.Text + "'", "TotalButcheryCost");
            return str;
        }
        String getFreightCost()
        {
            string str = "";
            str = Database.getSingleQuery("ShipmentOrder", "ShipmentNo='" + txtshipment.Text + "'", "TotalFreightCost");
            return str;
        }

        private void txtshipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtqtypercontainer.Text = getQtyPerContainer();
            txtcostallocation.Text = getInvoiceCostAllocation();
            txtbutchery.Text = getButcheryCost();
            txtfreight.Text = getFreightCost();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
            //gridView1.AddNewRow();
            DataRow newRow = table.NewRow();
            newRow["Code"] = 0;
            newRow["Item"] = 0;
            newRow["Cutting"] = 0;
            newRow["TestCut"] = 0;
            newRow["QtyPerContainer"] = 0;
            newRow["CostAllocation"] = 0;
            newRow["CostAdjustment"] = 0;
            newRow["InvoiceAmount"] = 0;
            newRow["Butchery"] = 0;
            newRow["Freight"] = 0;
            newRow["TotalCost"] = 0;
            newRow["CostPerKilo"] = 0;
            newRow["SRP"] = 0;
            table.Rows.Add(newRow);
            gridControl1.DataSource = table;
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Code", "0");
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Item", "0");
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TestCut", 0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "QtyPerContainer", 0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "CostAllocation", 0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "CostAdjustment", 0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "InvoiceAmount",0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Butchery", 0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Freight", 0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalCost", 0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "CostPerKilo",0);
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "SRP", "0");
          
        }

        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            string prodcatcode = Classes.Product.getProductCategoryCode("LocalPork");
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts(prodcatcode);
            searchProd.FormClosed += new FormClosedEventHandler(searchProd_FormClosed);
            searchProd.Show();

        }

        void searchProd_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Item", HOForms.SearchProducts.prodname);
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Code", HOForms.SearchProducts.prodcode);
            gridView1.FocusedColumn = gridView1.Columns[gridView1.Columns.Count - 2];
        }

        private void gridView1_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Item")
                e.RepositoryItem = repositoryItemButtonEdit1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            insertData();
            SaveCosting();
        }

        void insertData()
        {
            string code,item,testcut,qtypercontainer,costallocation,costadjustment,invoiceamount,butchery,freight,totalcost,costperkilo,srp;
            try
            {
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    code = gridView1.GetRowCellValue(i, "Code").ToString();
                    item = gridView1.GetRowCellValue(i, "Item").ToString();
                    testcut = gridView1.GetRowCellValue(i, "Cutting").ToString();
                    testcut = gridView1.GetRowCellValue(i, "TestCut").ToString();
                    qtypercontainer = gridView1.GetRowCellValue(i, "QtyPerContainer").ToString();
                    costallocation = gridView1.GetRowCellValue(i, "CostAllocation").ToString();
                    costadjustment = gridView1.GetRowCellValue(i, "CostAdjustment").ToString();
                    invoiceamount = gridView1.GetRowCellValue(i, "InvoiceAmount").ToString();
                    butchery = gridView1.GetRowCellValue(i, "Butchery").ToString();
                    freight = gridView1.GetRowCellValue(i, "Freight").ToString();
                    totalcost = gridView1.GetRowCellValue(i, "TotalCost").ToString();
                    costperkilo = gridView1.GetRowCellValue(i, "CostPerKilo").ToString();
                    srp = gridView1.GetRowCellValue(i, "SRP").ToString();
                    Database.ExecuteQuery("INSERT INTO CarcassCosting VALUES('" + txtshipment.Text + "','" + code + "','" + item + "','" + testcut + "','" + qtypercontainer + "','" + costallocation + "','" + costadjustment + "','" + invoiceamount + "','" + butchery + "','" + freight + "','" + totalcost + "','" + costperkilo + "','" + srp + "',' ',' ')");
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        void SaveCosting()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_CreateCosting";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno",txtshipment.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Costing Successfully Updated");
                this.Dispose();
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

        private void button4_Click(object sender, EventArgs e)
        {
            for(int i=0;i<=gridView1.RowCount-1;i++)
            {
                gridView1.DeleteRow(i);
            }
            gridControl1.DataSource = null;
        }

        private void deleteSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView1.DeleteSelectedRows();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }
    }
}
