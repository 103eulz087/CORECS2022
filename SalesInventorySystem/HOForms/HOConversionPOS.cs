using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem
{
    public partial class HOConversionPOS : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        public static string itemno, prodname, qty,unitprice, sellingprice,amount,refno;
        //string totalcost;
        double existingqty = 0.0;
        //bool isoverage = false;
        double totalqtysource = 0.0, totalqtyconverted = 0.0;
        public static bool isConversion = false;


        public HOConversionPOS()
        {
            InitializeComponent();
        }

        private void HOConversionPOS_Load(object sender, EventArgs e)
        {

           

            txtrefcode.Text = IDGenerator.getIDNumberSP("sp_GetConversionNumber", "conversionnumber");
            displayProdCat();
            loadcomb();

           

        }

        void displayGrid()
        {
            gridControl3.BeginUpdate();
            gridView3.Columns.Clear();

            table = new DataTable();
            
      
            table.Columns.Add("SourceProductCode");
            table.Columns.Add("ProductCode");
            table.Columns.Add("Description");

            if(radioButton2.Checked==true)  //Many to One
            {
                table.Columns.Add("SourceQty");
        
            }
           
            table.Columns.Add("Quantity");
            if (radioButton1.Checked == true)  //Many to One
            {
                table.Columns.Add("ActualQty");
            }   
            table.Columns.Add("Barcode");
          
            gridControl3.DataSource = null;
            gridControl3.DataSource = table;
            gridControl3.EndUpdate();
        }

        void displayProdCat()
        {
            Classes.Product.displayProductCategoryComboBoxItems(txtprodcat);
            Classes.Product.displayProductCategoryComboBoxItems(txtprodcatcon);
        }

        void loadcomb()
        {
            Database.displayComboBoxItems("SELECT Description FROM dbo.Products WHERE BranchCode='888' AND ProductCategoryCode='"+Classes.Product.getProductCategoryCode(txtprodcatcon.Text)+"' ORDER BY Description ASC", "Description",comboBox1);
        }

        private void add()
        {
          
            qty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
                DataRow newRow = table.NewRow();
                newRow["#"] = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNumber").ToString();
                newRow["ProductCode"] = "";
                newRow["Description"] = "";
                newRow["Quantity"] = "";
                newRow["Cost"] = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cost").ToString();
                table.Rows.Add(newRow);
                //gridControl2.DataSource = table;
                gridControl3.DataSource = table;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            qty = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                spinEdit1.Focus();
            }
        }

        private void spinEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                add();
                gridView1.Focus();
                spinEdit1.Text = "0";
            }
            else if (e.KeyCode == Keys.Down)
            {
                gridView1.Focus();
            }
        }

        

        private void txtsellingprice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gridView1.Focus();
            }
        }

        String getRatio()
        {
            string ratio = "";
            return ratio = Database.getSingleQuery("Ratio", "Type='Conversion'", "Ratio");
        }

        double getMaxRatio(double value)
        {
            double range = 0.0,maxvalue=0.0;
            range = value * Convert.ToDouble(getRatio());
            maxvalue = value + range;
            return maxvalue;
        }
        double getMinRatio(double value)
        {
            double range = 0.0, minvalue = 0.0;
            range = value * Convert.ToDouble(getRatio());
            minvalue = value - range;
            return minvalue;
        }

        String getProductCategoryCode()
        {
            string str = "";
            str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcatcon.Text + "'", "ProductCategoryID");
            return str;
        }
        String getProductCode()
        {
            string str = "";
            str = Database.getSingleQuery("Products", "Description='" + comboBox1.Text + "' and ProductCategoryCode='"+getProductCategoryCode()+"'", "ProductCode");
            return str;
        }

        void addEntry()
        {
            //GRID VIEW 1
            try
            {
                double sourceTotalAmount = 0.0, sourceTotalAmount2 = 0.0;
                string sourceSeqNum = "0";
                
                string sourceProd = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString();
                string sourceDesc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
               
                string sourceCost = "0";
                string sourceAvailable = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
                
                sourceTotalAmount = Convert.ToDouble(sourceCost) * Convert.ToDouble(txttotalavailableqty.Text);
                double percentagePerPart = 0.0, sourceAmountPerPart = 0.0, percentagePerPart2 = 0.0, sourceAmountPerPart2 = 0.0;
                string conversionType = "";
               
                int ctr = gridView3.RowCount;
                string sourceSeqNum1 = "";
                string srcprodcode = "";
                string sourceProd1 ="";
                string sourceDesc1="";
                string sourceAvailableGrid2="";
                string actualqty = "";
                double totalSourceQuantity = 0.0;
                double totalActualQuantity = 0.0;
                double newcostkg = 0.0;
                for (int i = 0; i <= gridView3.RowCount-1; i++)
                {

                    srcprodcode = gridView3.GetRowCellValue(i, "SourceProductCode").ToString();
                    sourceProd1 = gridView3.GetRowCellValue(i, "ProductCode").ToString();
                    sourceDesc1 = gridView3.GetRowCellValue(i, "Description").ToString();
                    if(radioButton1.Checked==true)
                    {
                        actualqty = gridView3.GetRowCellValue(i, "ActualQty").ToString();
                    }

                    if (radioButton1.Checked == true) //one to many
                    {
                        percentagePerPart = Convert.ToDouble(actualqty) / Convert.ToDouble(txttotalavailableqty.Text);/// Convert.ToDouble(sourceAvailable);
                    }
                    else
                    {
                        percentagePerPart = Convert.ToDouble(txtactualqty.Text) / Convert.ToDouble(txttotalavailableqty.Text);
                    }
                    sourceAmountPerPart = percentagePerPart * sourceTotalAmount;

                    totalqtyconverted += Convert.ToDouble(gridView3.GetRowCellValue(i, "Quantity").ToString()); //Total Quantity sa gi convert
                    if (radioButton1.Checked == true)
                    {
                        totalActualQuantity += Convert.ToDouble(gridView3.GetRowCellValue(i, "ActualQty").ToString());
                    }
                    else
                    {
                        totalActualQuantity = Convert.ToDouble(txtactualqty.Text);
                    }

                    if(radioButton1.Checked==true)
                    {
                        newcostkg = sourceAmountPerPart / Convert.ToDouble(actualqty);
                    }
                   else
                    {
                        newcostkg = sourceAmountPerPart / Convert.ToDouble(txtactualqty.Text);
                    }

                    if (radioButton1.Checked == true)
                    {
                        //ONE TO MANY
                        if(String.IsNullOrEmpty(gridView3.GetRowCellValue(i,"ProductCode").ToString()) || String.IsNullOrEmpty(gridView3.GetRowCellValue(i, "Description").ToString()))
                        {
                            XtraMessageBox.Show("The System found out that one of your Converted Items is No ProductCode or No Description!...");
                            return;
                        }
                        conversionType = "OneToMany";
                        string srcdesc = Database.getSingleQuery("Products", "BranchCode='" + Login.assignedBranch + "' and ProductCode='"+srcprodcode+"'", "Description");
                         //if total quantity converted greater than source quantity
                        if (Convert.ToDouble(txttotalweight.Text) > Convert.ToDouble(txttotalavailableqty.Text))
                        {
                            XtraMessageBox.Show("Quantity must not greater than SourceQty");
                            return;
                        }
                        
                       
                        else if (Convert.ToDouble(txttotalactualweight.Text) > Convert.ToDouble(txttotalweight.Text))
                        {

                             //ang sobra na quantity or overrage
                            existingqty = Convert.ToDouble(txttotalavailableqty.Text) - totalActualQuantity;
                            string mark1 = newcostkg.ToString();
                            string mark2 = percentagePerPart.ToString();
                            string mark3 = sourceAmountPerPart.ToString();
                            Database.ExecuteQuery("INSERT INTO TempConversionDetails VALUES('" + Login.assignedBranch + "','" + txtrefcode.Text + "',1,'" + srcprodcode + "','" + srcdesc + "','" + txtsrcqty.Text + "',0,0,'" + gridView3.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridView3.GetRowCellValue(i, "Description").ToString() + "','" + gridView3.GetRowCellValue(i, "ActualQty").ToString() + "','" + gridView3.GetRowCellValue(i, "ActualQty").ToString() + "','" + 0 + "','" + 0 + "',0,0,'0','" + gridView3.GetRowCellValue(i, "Barcode").ToString() + "')");

                        }
                        else
                        {
                            Database.ExecuteQuery("INSERT INTO TempConversionDetails VALUES('" + Login.assignedBranch + "','" + txtrefcode.Text + "','" + sourceSeqNum + " ','" + srcprodcode + "','" + srcdesc + "','" + txtsrcqty.Text + "','" + sourceCost + "',0,'" + gridView3.GetRowCellValue(i, "ProductCode").ToString() + "','" + gridView3.GetRowCellValue(i, "Description").ToString() + "','" + gridView3.GetRowCellValue(i, "ActualQty").ToString() + "','" + gridView3.GetRowCellValue(i, "ActualQty").ToString() + "','" + newcostkg + "','" + percentagePerPart + "',0,0,'" + sourceAmountPerPart + "','" + gridView3.GetRowCellValue(i, "Barcode").ToString() + "')");
                        }
                    }
                    else //MANY TO ONE
                    {
                        sourceSeqNum1 = "222";
                        sourceAvailableGrid2 = gridView3.GetRowCellValue(i, "SourceQty").ToString(); //ang original quantity sa product nga e convert
                        sourceTotalAmount2 = 0 * Convert.ToDouble(sourceAvailableGrid2);
                        percentagePerPart2 = Convert.ToDouble(gridView3.GetRowCellValue(i, "Quantity").ToString()) / Convert.ToDouble(sourceAvailableGrid2);
                        sourceAmountPerPart = percentagePerPart * sourceTotalAmount;
                        sourceAmountPerPart2 = percentagePerPart2 * sourceTotalAmount2;
                        totalqtysource = Convert.ToDouble(sourceAvailableGrid2);
                        totalSourceQuantity += Convert.ToDouble(sourceAvailableGrid2);

                        conversionType = "ManyToOne";
                        Database.ExecuteQuery("INSERT INTO TempConversionDetails VALUES('" + Login.assignedBranch + "','" + txtrefcode.Text + "','" + sourceSeqNum1 + "','" + sourceProd1 + "','" + sourceDesc1 + "','" + sourceAvailableGrid2 + "','0',0,'" + txtprodcode.Text + "','" + comboBox1.Text + "','" + gridView3.GetRowCellValue(i, "Quantity").ToString() + "','" + txtactualqty.Text + "','0','" + percentagePerPart2 + "',0,0,'" + sourceAmountPerPart2 + "','" + gridView3.GetRowCellValue(i, "Barcode").ToString() + "')");


                    }
                }
                if (radioButton1.Checked == true)
                {
                    //ONE TO MANY (BranchCode,CONID,SourceSequenceNUmber,SourceQty,SourceCost,SourceTotalAmount,TotalItemsConverted,TotalQtyConverted,ActualQty,ConversionType,DateConverted,ConvertedBy)
                    Database.ExecuteQuery("INSERT INTO TempConversionSummary VALUES('" + Login.assignedBranch + "','" + txtrefcode.Text + "','"+sourceSeqNum+"','" + txtsrcqty.Text + "','"+sourceCost+"','" + sourceTotalAmount + "','" + ctr + "','" + totalActualQuantity + "','"+ totalActualQuantity + "','" + conversionType + "','" + txtconversiondate.Text + "','" + Login.Fullname + "','0',0)");
                }
                else
                {
                    //MANY TO ONE 
                    Database.ExecuteQuery("INSERT INTO TempConversionSummary VALUES('" + Login.assignedBranch + "','" + txtrefcode.Text + "','','" + totalSourceQuantity + "','','" + txtsrcqty.Text + "','" + ctr + "','" + txttotalweight.Text + "','" + txtactualqty.Text + "','" + conversionType + "','" + txtconversiondate.Text + "','" + Login.Fullname + "','0',0)");
                }
                // save();
                conversionProcess();
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }       
        }

        bool trappings()
        {
            bool ok = false;
            if (radioButton2.Checked == true)
            {
                for (int i = 0; i <= gridView3.RowCount - 1; i++)
                {
                    string sourceavailableqty,qty;
                    sourceavailableqty = gridView3.GetRowCellValue(i,"SourceQty").ToString();
                    qty = gridView3.GetRowCellValue(i, "Quantity").ToString();
                    if (Convert.ToDouble(sourceavailableqty) < Convert.ToDouble(qty))
                    {
                        XtraMessageBox.Show("Conversion Cannot Proceed.. one of the Source Qty must not less than in Qty Converted!");
                        ok = true;
                        return ok;
                    }
                    else
                    {
                        ok = false;
                    }
                }
            }
            return ok;
        }

        private void save()
        {
            
            string prodcode = "";
            string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcatcon.Text);
            prodcode = Classes.Product.getProductCode(comboBox1.Text, prodcatcode);
            string barcode = prodcatcode + prodcode + txttotalweight.Text + '1';
            
            SqlConnection con = Database.getConnection();
            con.Open();
                try
                {
                    string query = "sp_Conversion";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                    com.Parameters.AddWithValue("@refcode", txtrefcode.Text);
                    com.Parameters.AddWithValue("@parmconvertto", comboBox1.Text);
                    com.Parameters.AddWithValue("@parmconvertprodcode", prodcode);
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

        private void conversionProcess()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_ConversionProcess";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmconid", txtrefcode.Text);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 180;
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

        private String getRefNumber()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("SELECT MAX(ReferenceCode)AS maxref FROM ConversionSummary", con);
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    refno = reader["maxref"].ToString();
                }
            }
            if (refno == "")
            {
                refno = "10000";
            }
            return refno;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                if (Convert.ToDouble(txttotalactualweight.Text) > Database.getTotalSummation2("Inventory", "ProductCode = '" + labeleulz.Text + "' AND BranchCode='" + Login.assignedBranch + "' AND Available > 0 ", "Available")) //Database.getTotalSummation("Inventory", "Product", txtsku.Text.Substring(1, 6), "Quantity"))
                {
                    string mark = Database.getTotalSummation2("Inventory", "ProductCode = '" + labeleulz.Text + "' AND BranchCode='" + Login.assignedBranch + "' AND Available > 0", "Available").ToString();
                    XtraMessageBox.Show("Insuficient Stocks for this Product.. Your Available Quantity is " + mark);
                    return;
                }
                if (String.IsNullOrEmpty(txtsrcqty.Text))
                {
                    XtraMessageBox.Show("Source Quantity must not Equal to Zero or EMpty!");
                    return;
                }
            }
            if (radioButton2.Checked==true)
            {
                if(String.IsNullOrEmpty(txtprodcode.Text) || String.IsNullOrEmpty(txtprodcatcode.Text))
                {
                    XtraMessageBox.Show("Product Category Code or Product Code must not Empty!!!...");
                    return;
                }
                
            }
            addEntry();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }
        
        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Database.display("SELECT ProductCode,Description,SUM(Available) as Available " +
                "FROM Inventory " +
                "WHERE BranchCode='" + Login.assignedBranch+"' " +
                "and Available > 0 " +
                "and ProductCode in (Select ProductCode FROM dbo.Products WHERE BranchCode='"+Login.assignedBranch+"' AND ProductCategoryCode='"+Classes.Product.getProductCategoryCode(txtprodcat.Text)+ "') " +
                "GROUP BY ProductCode,Description", gridControl1, gridView1);
        }


        private void simpleButton3_Click(object sender, EventArgs e)
        {
            addItemEntry();
        }

        void addItemEntry()
        {
           
            try
            {
                bool isNotExist = false;
                if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    XtraMessageBox.Show("Please Select Conversion Type!");
                    return;
                }
                
               
            
                DataRow newRow = table.NewRow();
                newRow["SourceProductCode"] = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString();
                newRow["ProductCode"] = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString();
                newRow["Description"] = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
                if (radioButton2.Checked == true) //many to one
                {
                  
                    newRow["SourceQty"] = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
                    
                }
                if (radioButton1.Checked == true)
                {
                    for (int i = 0; i <= gridView3.RowCount - 1; i++) //one to many
                    {
                        if (gridView3.GetRowCellValue(i, "ProductCode").ToString() != gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Product").ToString())
                        {
                            isNotExist = true;
                        }
                    }
                    labeleulz.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString();
                }
                if (radioButton1.Checked == true && isNotExist) //one to many && isNotExist
                {
                    XtraMessageBox.Show("This ProductCode is Not Exist.. Cannot be used in OneToMany Conversion Type");
                    labeleulz.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ProductCode").ToString();
                    return;
                    
                }

                if(radioButton1.Checked==true)
                {
                    newRow["ActualQty"] = "0";
                }
                newRow["Quantity"] = "0";
                newRow["Barcode"] = "";
                gridView3.Columns.Clear();
                gridControl3.DataSource = null;

                table.Rows.Add(newRow);
                
                gridControl3.DataSource = table;
                gridView3.BestFitColumns();
                if (radioButton2.Checked == true)
                {
                    gridView3.Columns["SourceQty"].Summary.Clear();
                    gridView3.Columns["SourceQty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SourceQty", "{0}");
                    
                }
                if (radioButton1.Checked == true)
                {
                    gridView3.Columns["ActualQty"].Summary.Clear();
                    gridView3.Columns["ActualQty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ActualQty", "{0}");
                    gridView3.Columns["Quantity"].Visible = false;
                }
               
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + " Please Select Conversion Type!");
            }
            finally
            {
         
            }
        }

        void clearfields()
        {
            txttotalavailableqty.Text = "0";
            txttotalcost.Text = "0";
            txttotalcostkg.Text = "0";
            txttotal.Text = "0";
            txttotalweight.Text = "0";
            txttotalactualweight.Text = "0";
            txtmarkup.Text = "0";
            txtprofit.Text = "0";
            txtdiffkg.Text = "0";
            txtdiff.Text = "0";
            gridControl3.DataSource = null;
          
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) //One to Many
            {
                displayGrid();
                clearfields();
                panel2.Visible = false;
                
                gridView3.Columns["ActualQty"].Visible = true;

                labelControl18.Visible = true;
                txtsrcqty.Visible = true;
            }
            else //Many to One
            {
                displayGrid();
                clearfields();
                panel2.Visible = true;
                
                labelControl18.Visible = false;
                txtsrcqty.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                displayGrid();
                clearfields();
                panel2.Visible = false;
                gridView3.Columns["ActualQty"].Visible = true;

                labelControl18.Visible = true;
                txtsrcqty.Visible = true;
            }
            else
            {
                displayGrid();
                clearfields();
                panel2.Visible = true;
                
                labelControl18.Visible = false;
                txtsrcqty.Visible = false;
            }
        }

        private void txtprodcatcon_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadcomb();
            string prodcatcode = "";
            prodcatcode = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcatcon.Text + "'", "ProductCategoryID");
            txtprodcatcode.Text = prodcatcode;
            comboBox1.Text = "";
            txtprodcode.Text = "";
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridView3.DeleteSelectedRows();
        }
        
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            addItemEntry();
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Available")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
        }

        private void gridView3_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "Quantity")
            {
                e.Appearance.BackColor = Color.DeepSkyBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
            }
            if (e.Column.FieldName == "ActualQty")
            {
                e.Appearance.BackColor = Color.DeepPink;
                e.Appearance.BackColor2 = Color.LightPink;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            table.Rows.Clear();
        }

        private void gridView3_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "Description")
                e.RepositoryItem = repositoryItemBtnSearch;
        }

        private void printBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                    Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
                    bprint.lblmanufdate.Text = DateTime.Now.ToShortDateString();
                    bprint.lblxpirydate.Text = DateTime.Now.AddYears(1).ToShortDateString();
                    bprint.lblprodtype.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Description").ToString();
                    bprint.lbltotalkilos.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ActualQty").ToString();
                    bprint.xrBarCode2.Text = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Barcode").ToString(); ;
                    ReportPrintTool report = new ReportPrintTool(bprint);
                    report.Print();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prodcode = "";
            prodcode = Database.getSingleQuery("Products", "ProductCategoryCode='"+txtprodcatcode.Text+"' and BranchCode='"+Login.assignedBranch+"' and Description='"+comboBox1.Text+"'", "ProductCode");
            txtprodcode.Text = prodcode;
        }

        void searchProd_FormClosed(object sender, FormClosedEventArgs e)
        {
            gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "ProductCode", HOForms.SearchProducts.prodcode);
            gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "Description", HOForms.SearchProducts.prodname);
            gridView3.FocusedColumn = gridView3.Columns[gridView3.Columns.Count - 2];
        }


        private void gridView3_RowCountChanged(object sender, EventArgs e)
        {
            double totalcostkg = 0.0, totcost = 0.0, totalqty = 0.0,totalactualqty=0.0;
            int count = 0;
            for (int i = 0; i < gridView3.RowCount; i++)
            {
                count = gridView3.RowCount;
                totalqty += Math.Round(Convert.ToDouble(gridView3.GetRowCellValue(i, "Quantity").ToString()), 3);
                if(radioButton1.Checked==true)
                {
                    totalactualqty += Math.Round(Convert.ToDouble(gridView3.GetRowCellValue(i, "ActualQty").ToString()), 2);
                }
                
            }
           
            txttotalcost.Text = totcost.ToString();
            txttotalcostkg.Text = totalcostkg.ToString();
            txttotal.Text = "0";
            txttotalweight.Text = totalqty.ToString();
            txttotalactualweight.Text = totalactualqty.ToString();
            txtmarkup.Text = "0";
            txtprofit.Text = "0";
            txtdiffkg.Text = "0";
            txtdiff.Text = "0";
        }
        String sequencePadding(string str)
        {
            string isnum = "";
            
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

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bool isBarcodeLong = false;
            string barcode = "";
            //isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
            double totalqty = 0.0, totalactualqty = 0.0;
            int count = 0;
            int ctr2 = 0;
            for (int i = 0; i < gridView3.RowCount; i++)
            {
                ctr2++;
                count = gridView3.RowCount;
                totalqty += Math.Round(Convert.ToDouble(gridView3.GetRowCellValue(i, "Quantity").ToString()), 3);
                if (radioButton1.Checked == true)
                {
                    totalactualqty += Math.Round(Convert.ToDouble(gridView3.GetRowCellValue(i, "ActualQty").ToString()), 3);
                }

            }
            txttotalweight.Text = totalqty.ToString();
            txttotalactualweight.Text = totalactualqty.ToString();
            if(radioButton1.Checked==true)
            {
                if (e.Column.FieldName == "Quantity")
                {
                    double sourceqty = 0.0, destqty = 0.0;
                    sourceqty = Convert.ToDouble(txttotalavailableqty.Text);
                    destqty = Convert.ToDouble(txttotalweight.Text);
                    if (destqty > sourceqty)
                    {
                        XtraMessageBox.Show("Quantity must not greater than Source Quantity");
                        gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "Quantity", "0");
                    }
                }
                decimal quantity;
                string strquantity;
                quantity = Decimal.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ActualQty").ToString());
                strquantity = String.Format("{0:00.000}", quantity);
                if (isBarcodeLong == true) //long barcode type
                {
                    barcode = "44444"+gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ProductCode").ToString() + strquantity.Replace(".", "") + sequencePadding(gridView3.GetRowHandle(gridView3.FocusedRowHandle).ToString());
                }
                else
                {
                    barcode = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ProductCode").ToString() + strquantity.Replace(".", "") + sequencePadding(gridView3.GetRowHandle(gridView3.FocusedRowHandle).ToString());
                }

                if (e.Column.FieldName == "ActualQty")
                {
                    gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "Barcode", barcode);
                }
            }
            if(radioButton2.Checked == true)
            {
                if (e.Column.FieldName == "Quantity")
                {
                    double sourceqty = 0.0, destqty = 0.0;
                    sourceqty = Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle,"SourceQty"));
                    destqty = Convert.ToDouble(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Quantity"));
                    if (destqty > sourceqty)
                    {
                        XtraMessageBox.Show("Quantity must not greater than Source Quantity");
                        gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "Quantity", "0");
                    }
                }
                decimal quantity;
                string strquantity;
                quantity = Decimal.Parse(gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "Quantity").ToString());
                strquantity = String.Format("{0:00.000}", quantity);
                if (isBarcodeLong == true) //long barcode type
                {
                    barcode = "44444" + gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ProductCode").ToString() + strquantity.Replace(".", "") + sequencePadding(gridView3.GetRowHandle(gridView3.FocusedRowHandle).ToString());
                }
                else
                {
                    barcode = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "ProductCode").ToString() + strquantity.Replace(".", "") + sequencePadding(gridView3.GetRowHandle(gridView3.FocusedRowHandle).ToString());
                }
                if (e.Column.FieldName == "Quantity")// if (e.Column.FieldName == "ActualQty")
                {
                    gridView3.SetRowCellValue(gridView3.FocusedRowHandle, "Barcode", barcode);
                }
            }
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl3, e.Location);
            }
        }

        private void repositoryItemBtnSearch_Click(object sender, EventArgs e)
        {
            isConversion = true;
            string prodcatcode = Classes.Product.getProductCategoryCode(txtprodcat.Text);
            HOForms.SearchProducts searchProd = new HOForms.SearchProducts(prodcatcode);
            searchProd.FormClosed += new FormClosedEventHandler(searchProd_FormClosed);
            searchProd.Show();
        }

        private void repositoryItemComboBox1_Click(object sender, EventArgs e)
        {
            repositoryItemComboBox1.Items.Add("Eulz");
        }
    }
}