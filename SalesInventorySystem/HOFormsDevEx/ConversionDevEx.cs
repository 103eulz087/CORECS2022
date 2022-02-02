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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ConversionDevEx : DevExpress.XtraEditors.XtraForm
    {
        double totalqty = 0.0, totalactualqty=0.0;//, totalqtyconverted=0.0
        string conversiontype = "";
        DataTable tablePrepare,tableOutput;
        public ConversionDevEx()
        {
            InitializeComponent();
        }

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Database.display("SELECT SequenceNumber as #,Product,Description,Available,Cost,Barcode FROM Inventory WHERE Branch='" + Login.assignedBranch + "' and isWarehouse=1 and Available > 0 and IsStock=1 and SUBSTRING(Product,1,2)='" + Classes.Product.getProductCategoryCode(txtprodcat.Text) + "' ", gridControlDisplay, gridViewDisplay);
        }

        private void ConversionDevEx_Load(object sender, EventArgs e)
        {
            txtrefcode.Text = IDGenerator.getIDNumberSP("sp_GetConversionNumber", "conversionnumber"); //IDGenerator.getConversionNumber();
            loadcomb();
            displayProdCat();
        }
        void displayProdCat()
        {
            Classes.Product.displayProductCategoryComboBoxItems(txtprodcat);
            Classes.Product.displayProductCategoryComboBoxItems(txtprodcatcon);
        }
        void loadcomb()
        {
            Database.displayComboBoxItems("SELECT Description FROM Products WHERE BranchCode='888' AND ProductCategoryCode='" + Classes.Product.getProductCategoryCode(txtprodcatcon.Text) + "' ORDER BY Description ASC", "Description", comboBox1);
        }
        void addItemEntry()
        {
            try
            {
                bool isexist = false;
                if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    XtraMessageBox.Show("Please Select Conversion Type!");
                    return;
                }
                for (int i = 0; i <= gridViewPrepare.RowCount - 1; i++) //one to many
                {
                    if (gridViewPrepare.GetRowCellValue(i, "#").ToString() == gridViewDisplay.GetRowCellValue(gridViewDisplay.FocusedRowHandle, "#").ToString())
                    {
                        isexist = true;
                    }
                }
                if(radioButton1.Checked==true && isexist)
                {
                    XtraMessageBox.Show("This Item is Already in the list!");
                    return;
                }
                //string qty1 = HOForms.ConversionQty.conversionqty;
                DataRow newRow = tablePrepare.NewRow();
                newRow["#"] = gridViewDisplay.GetRowCellValue(gridViewDisplay.FocusedRowHandle, "#").ToString();
                newRow["ProductCode"] = gridViewDisplay.GetRowCellValue(gridViewDisplay.FocusedRowHandle, "Product").ToString();
                newRow["Description"] = gridViewDisplay.GetRowCellValue(gridViewDisplay.FocusedRowHandle, "Description").ToString();
                newRow["Quantity"] = gridViewDisplay.GetRowCellValue(gridViewDisplay.FocusedRowHandle, "Available").ToString();
                newRow["Cost"] = gridViewDisplay.GetRowCellValue(gridViewDisplay.FocusedRowHandle, "Cost").ToString();
                newRow["ActualQty"] = "0";
                newRow["Variance"] = "0";
                newRow["Barcode"] = gridViewDisplay.GetRowCellValue(gridViewDisplay.FocusedRowHandle, "Barcode").ToString();

                gridViewPrepare.Columns.Clear();
                gridControlPrepare.DataSource = null;
                tablePrepare.Rows.Add(newRow);
                gridControlPrepare.DataSource = tablePrepare;
                gridViewPrepare.BestFitColumns();
                gridViewPrepare.Columns["ActualQty"].Summary.Clear();
                gridViewPrepare.Columns["ActualQty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ActualQty", "{0}");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + " Please Select Conversion Type!");
            }
            finally
            {
                
            }
        }

        private void gridView2_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            
        }
        String sequencePadding(string str)
        {
            string isnum = "";
            //  string str = IDGenerator.getSequenceNumber().ToString();
            if (str.Length == 1)
            {
                isnum = "000" + str;
            }
            else if (str.Length == 2)
            {
                isnum = "00" + str;
            }
            else if (str.Length == 3)
            {
                isnum = "0" + str;
            }
            return isnum;
        }
        private void conversionProcess()
        {
            SqlConnection con = Database.getConnection();
            con.Open();

            try
            {
                string query = "sp_ConversionProcessNewEulzHO";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmconid", txtrefcode.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmconversiontype", conversiontype);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                XtraMessageBox.Show("Successfully Converted");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            this.Close();
        }

        void addEntry()
        {
            //GRID VIEW 1
            string sourceprodcode = "", sourcedesc = "";
            double sourceqty =0.0, sourceqtyMto1=0.0,sourceCostMto1=0.0;
            int totalitemsconverted = 0;
            for(int i=0;i<=gridViewPrepare.RowCount-1;i++)
            {
                sourceprodcode = gridViewPrepare.GetRowCellValue(i, "ProductCode").ToString();
                sourcedesc = gridViewPrepare.GetRowCellValue(i, "Description").ToString();
                sourceqty += Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "ActualQty").ToString());
            }
            try
            {
                if(radioButton1.Checked==true)
                {
                    for (int i = 0; i <= gridViewOutput.RowCount - 1; i++)
                    {
                        totalitemsconverted += 1;
                        //totalqtyconverted += Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "Quantity").ToString());
                        Database.ExecuteQuery("INSERT INTO ConversionDetails VALUES('" + Login.assignedBranch + "','" + txtrefcode.Text + "',1,'" + sourceprodcode + "','"+sourcedesc+"','" + sourceqty + "','0',0,'" + gridViewOutput.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridViewOutput.GetRowCellValue(i, "Description").ToString() + "','" + gridViewOutput.GetRowCellValue(i, "Quantity").ToString() + "','" + gridViewOutput.GetRowCellValue(i, "Quantity").ToString() + "',0,0,0,0,0,'" + gridViewOutput.GetRowCellValue(i, "Barcode").ToString() + "')");
                    }
                    for (int i = 0; i <= gridViewPrepare.RowCount - 1; i++)
                    {
                        double available = 0.0,actualqty=0.0;
                        available = Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "Quantity").ToString());
                        actualqty = Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "ActualQty").ToString());
                        if (Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "Variance").ToString()) <= 0)
                        {
                            Database.ExecuteQuery("UPDATE Inventory SET isStock=0,Available=0 WHERE SequenceNumber='" + gridViewPrepare.GetRowCellValue(i, "#").ToString() + "'");
                        }
                        else
                        {
                            Database.ExecuteQuery("UPDATE Inventory SET Available=Available-"+ actualqty + " WHERE SequenceNumber='" + gridViewPrepare.GetRowCellValue(i, "#").ToString() + "'");
                        }
                        Database.ExecuteQuery("INSERT INTO ConversionFIFO VALUES('" + Login.assignedBranch + "','" + gridViewPrepare.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "Description").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "Quantity").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "Cost").ToString() + "',0,'" + gridViewPrepare.GetRowCellValue(i, "ActualQty").ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + gridViewPrepare.GetRowCellValue(i, "#").ToString() + "','" + txtrefcode.Text + "',0,'')");
                    }
                    conversiontype = "OneToMany";
                }
                else if(radioButton2.Checked == true)
                {
                    for (int i = 0; i <= gridViewPrepare.RowCount - 1; i++)
                    {
                        double available = 0.0, actualqty = 0.0;
                        available = Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "Quantity").ToString());
                        actualqty = Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "ActualQty").ToString());
                        totalitemsconverted += 1;
                        sourceqtyMto1 = Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "Quantity").ToString());
                        sourceCostMto1 = Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "Cost").ToString());
                        Database.ExecuteQuery("INSERT INTO ConversionDetails VALUES('" + Login.assignedBranch + "','" + txtrefcode.Text + "',2,'" + gridViewPrepare.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "Description").ToString() + "','" + sourceqtyMto1 + "','"+sourceCostMto1+"',0,'" + gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "ProductCode").ToString() + "','" + gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "Description").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "ActualQty").ToString() + "','" + gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "Quantity").ToString() + "',0,0,0,0,0,'" + gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "Barcode").ToString() + "')");


                        if (Convert.ToDouble(gridViewPrepare.GetRowCellValue(i, "Variance").ToString()) <= 0)
                        {
                            Database.ExecuteQuery("UPDATE Inventory SET isStock=0,Available=0 WHERE SequenceNumber='" + gridViewPrepare.GetRowCellValue(i, "#").ToString() + "'");
                        }
                        else
                        {
                            Database.ExecuteQuery("UPDATE Inventory SET Available=Available-" + actualqty + " WHERE SequenceNumber='" + gridViewPrepare.GetRowCellValue(i, "#").ToString() + "'");
                        }
                        Database.ExecuteQuery("INSERT INTO ConversionFIFO VALUES('" + Login.assignedBranch + "','" + gridViewPrepare.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "Description").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "Quantity").ToString() + "','" + gridViewPrepare.GetRowCellValue(i, "Cost").ToString() + "',0,'" + gridViewPrepare.GetRowCellValue(i, "ActualQty").ToString() + "','" + DateTime.Now.ToShortDateString() + "','" + gridViewPrepare.GetRowCellValue(i, "#").ToString() + "','" + txtrefcode.Text + "',0,'')");

                    }
                    conversiontype = "ManyToOne";
                }
                conversionProcess();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bool isempty = false;
            for(int i=0;i<=gridViewOutput.RowCount-1;i++)
            {
                if(String.IsNullOrEmpty(gridViewOutput.GetRowCellValue(i,"Quantity").ToString()) || String.IsNullOrEmpty(gridViewOutput.GetRowCellValue(i, "Description").ToString()))
                {
                    isempty = true;
                }
            }
            if(gridViewOutput.RowCount==0)
            {
                XtraMessageBox.Show("No Items to be Converted!");
                return;
            }
            if(isempty)
            {
                XtraMessageBox.Show("Please Delete Empty Rows!");
                return;
            }
            else
            { addEntry(); }
          
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) //One to Many
            {
                displayGrid();
               
                panel2.Visible = false;
                
                
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            addItemEntry();
            gridViewDisplay.DeleteSelectedRows();
        }

        private void gridViewPrepare_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                //get quantity
                double qty = 0.0,actualqty=0.0,variance=0.0;
                
                if (e.Column.FieldName == "ActualQty")
                {
                    qty = Convert.ToDouble(gridViewPrepare.GetRowCellValue(gridViewPrepare.FocusedRowHandle, "Quantity").ToString());
                    actualqty = Convert.ToDouble(gridViewPrepare.GetRowCellValue(gridViewPrepare.FocusedRowHandle, "ActualQty").ToString());
                    if (actualqty > qty)
                    {
                        XtraMessageBox.Show("ActualQty must not greater than OrigQty");
                        return;
                    }
                    variance = qty - actualqty;
                    gridViewPrepare.SetRowCellValue(gridViewPrepare.FocusedRowHandle, "Variance", variance);
                }
            }
        }

        private void gridViewOutput_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Description")
                e.RepositoryItem = repositoryItemButtonSearch;
        }

        private void repositoryItemButtonSearch_Click(object sender, EventArgs e)
        {
            string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts(prodcatcode);
            searchProd.FormClosed += SearchProd_FormClosed;
            searchProd.Show();
        }

        private void SearchProd_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridViewOutput.SetRowCellValue(gridViewOutput.FocusedRowHandle, "ProductCode", HOForms.SearchProducts.prodcode);
            gridViewOutput.SetRowCellValue(gridViewOutput.FocusedRowHandle, "Description", HOForms.SearchProducts.prodname);
            gridViewOutput.FocusedColumn = gridViewOutput.Columns[gridViewOutput.Columns.Count - 2];
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton2.Checked == true) //One to Many
            {
                displayGrid();
            }
        }

        private void gridControlOutput_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlOutput,e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridViewOutput.DeleteSelectedRows();
        }

        private void printBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
                bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
                bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
                bprint.lblprodtype.Text = gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "Description").ToString();
                bprint.lbltotalkilos.Text = gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "Quantity").ToString();
                bprint.xrBarCode2.Text = gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "Barcode").ToString(); ;//productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
                ReportPrintTool report = new ReportPrintTool(bprint);
                report.Print();
                //Barcode.BarcodePrintingOptions baropt = new Barcode.BarcodePrintingOptions();
                //baropt.ShowDialog(this);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void gridViewPrepare_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "ActualQty")
            {
                e.Appearance.BackColor = Color.DeepPink;
                e.Appearance.BackColor2 = Color.LightPink;
            }
        }

        private void gridViewPrepare_ShownEditor(object sender, EventArgs e)
        {
          
        }

        private void gridViewPrepare_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName != "ActualQty")
                e.Cancel = true;
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            int count = 0;
            int ctr2 = 0;
            for (int i = 0; i < gridViewOutput.RowCount; i++)
            {
                ctr2++;
                count = gridViewOutput.RowCount;
                if (radioButton1.Checked == true)
                {
                    totalqty += Math.Round(Convert.ToDouble(gridViewOutput.GetRowCellValue(i, "Quantity").ToString()), 3);
                }

            }
            txttotalweight.Text = totalqty.ToString();
            txttotalactualweight.Text = totalactualqty.ToString();
            
                decimal quantity;
                string strquantity;
                quantity = Decimal.Parse(gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "Quantity").ToString());
                strquantity = String.Format("{0:00.000}", quantity);
                string barcode = "99999" + gridViewOutput.GetRowCellValue(gridViewOutput.FocusedRowHandle, "ProductCode").ToString() + strquantity.Replace(".", "") + sequencePadding(gridViewOutput.GetRowHandle(gridViewOutput.FocusedRowHandle).ToString());

                if (e.Column.FieldName == "Quantity")
                {
                    gridViewOutput.SetRowCellValue(gridViewOutput.FocusedRowHandle, "Barcode", barcode);
                }
           
        }
        void displayGrid()
        {
            gridControlPrepare.BeginUpdate();
            gridViewPrepare.Columns.Clear();
            tablePrepare = new DataTable();
            tablePrepare.Columns.Add("#");
            tablePrepare.Columns.Add("ProductCode");
            tablePrepare.Columns.Add("Description");
            tablePrepare.Columns.Add("Quantity");
            tablePrepare.Columns.Add("Cost");
            tablePrepare.Columns.Add("ActualQty");
            tablePrepare.Columns.Add("Variance");
            tablePrepare.Columns.Add("Barcode");
            gridControlPrepare.DataSource = null;
            gridControlPrepare.DataSource = tablePrepare;
            gridControlPrepare.EndUpdate();

            //if (radioButton2.Checked == true)
            //{
                gridControlOutput.BeginUpdate();
                gridViewOutput.Columns.Clear();
                tableOutput = new DataTable();
                tableOutput.Columns.Add("ProductCode");
                tableOutput.Columns.Add("Description");
                tableOutput.Columns.Add("Quantity");
                tableOutput.Columns.Add("Barcode");
                gridControlOutput.DataSource = null;
                gridControlOutput.DataSource = tableOutput;
                gridControlOutput.EndUpdate();
            //}
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if(radioButton2.Checked==true)
            {
                if(gridViewOutput.RowCount==1)
                {
                    XtraMessageBox.Show("You Can only add one item in this conversion!..");
                    return;
                }
            }
            try
            {
                DataRow newRows = tableOutput.NewRow();
                newRows["ProductCode"] = "";
                newRows["Description"] = "";
                newRows["Quantity"] = "0";
                newRows["Barcode"] = "";
                gridViewOutput.Columns.Clear();
                gridControlOutput.DataSource = null;
                tableOutput.Rows.Add(newRows);
                gridControlOutput.DataSource = tableOutput;
                gridViewOutput.BestFitColumns();
                gridViewOutput.Columns["Quantity"].Summary.Clear();
                gridViewOutput.Columns["Quantity"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity", "{0}");
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
    }
}