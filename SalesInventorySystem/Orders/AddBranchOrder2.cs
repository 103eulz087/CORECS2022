using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Orders
{
    public partial class AddBranchOrder2 : Form
    {
        string productcategorycode = "";
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
      //  private Boolean eventFlag = false;
        DataTable table;
        private String weight;
        public string wieght2 = "";
        public AddBranchOrder2()
        {
            InitializeComponent();
            serialPort1.WriteTimeout = 500;
            serialPort1.ReadTimeout = 500;
            this.myDelegate = new AddDataDelegate(AddDataMethod);
            //if (eventFlag)
            //    displayweight();
        }

        public void AddDataMethod(String myString)
        {
            txtweight.AppendText(myString);
        }

        private void AddBranchOrder2_Load(object sender, EventArgs e)
        {
            try
            {
                //HOForms.SetBatchCodeFrm setbatchcode = new SetBatchCodeFrm();
                //setbatchcode.FormClosed += new FormClosedEventHandler(setbatchcode_FormClosed);
                //setbatchcode.ShowDialog(this);
                //serialPort1.Open();
                //serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
                //serialPort1.RtsEnable = true;
                //serialPort1.DtrEnable = true;
                getAvailablePort();

                isprimalcuts.Checked = true;
                displayComboBoxItems();
                //displayExistingItem();
                txtavailableqty.Text = getAvailableQty().ToString();
                loadgridview1();
                txtproduct.Focus();
                //txtdest.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        

        Double getAvailableQty()
        {
            double qty=0.0;
            qty = Database.getTotalSummation2("Inventory", "IsStock='1' AND Product='" + getProductCode() + "' ", "Available");
            return qty;
        }

        private void getAvailablePort()
        {
            string[] ports = SerialPort.GetPortNames();
            txtports.Items.AddRange(ports);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.F6)
            {
                button4.PerformClick();
            }
            else if (keyData == Keys.F10)
            {
                button2.PerformClick();
            }
            else if (keyData == Keys.F8)
            {
                button1.PerformClick();
            }
           
            return functionReturnValue;
        }
         
        void loadgridview1()
        {
            table = new DataTable();
            table.Columns.Add("Branch");
            table.Columns.Add("DateReceived");
            table.Columns.Add("Product"); //UnitPrice
            table.Columns.Add("Description"); 
            table.Columns.Add("Barcode");
            table.Columns.Add("Quantity");
            table.Columns.Add("Cost");
            table.Columns.Add("Available");
            dataGridView1.DataSource = table;
        }

        void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                weight = serialPort1.ReadExisting();
                if (weight == String.Empty)
                {
                    MessageBox.Show("NODATARECEVD");
                    return;
                }
                else
                {
                    string tempweight = weight.Substring(0, 6).Trim();
                    //tempweight = weight.Replace("?", "0");
                    if (tempweight.Length == 5)
                    {
                        wieght2 = "0" + tempweight;
                    }
                    else
                    {
                        wieght2 = tempweight;
                    }
                    //eventFlag = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        
        void displayComboBoxItems()
        {
            string sPrimalCut="";
            if (isprimalcuts.Checked == true)
            {
                sPrimalCut = "1";
            }
            else
            {
                sPrimalCut = "0";
            }
            Database.displayComboBoxItems("SELECT * FROM PrimalCuts WHERE BranchCode='888' AND isPrimalCut='"+sPrimalCut+"' ", "Description", txtproduct);
        }

        //private void displaySearchlookupEdit()
        //{
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "SELECT Description FROM PrimalCuts WHERE BranchCode='888' ";
        //    SqlCommand com = new SqlCommand(query, con);
        //    SqlDataAdapter adapter = new SqlDataAdapter(com);
        //    DataTable table = new DataTable();
        //    adapter.Fill(table);
        //    searchLookUpEdit1.Properties.DataSource = table;
        //}

        private void txtproduct_Click(object sender, EventArgs e)
        {
            displayComboBoxItems();
        }

        private void txtproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            productcategorycode = Database.getSingleQuery("PrimalCuts", "Description='" + txtproduct.Text.Trim() + "'", "ProductCode");
            //txtprodcode.Text = productcategorycode;
            this.ActiveControl = txtweight;
            txtweight.Focus();
        }

        String getProductCategoryCode()
        {
            string str;
            str = Database.getSingleQuery("PrimalCuts", "Description='" + txtproduct.Text.Trim() + "'", "ProductCategoryCode");
            return str;
        }

        String getProductCode()
        {
            string str;
            str = Database.getSingleQuery("PrimalCuts", "Description='" + txtproduct.Text.Trim() + "'", "ProductCode");
            return str;
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            try
            {
                Random rand = new Random();
                int ctr = rand.Next(0, 9);
                txtweight.Invoke(this.myDelegate, new Object[] { wieght2 });
                txtskuno.Text = getProductCategoryCode() + getProductCode() + txtweight.Text.Remove(2, 1) + ctr.ToString();
               // button1.Focus();
                btnaddinventory.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtweight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnD.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.xrBarCode2.Text = txtskuno.Text.Trim();//productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void btnaddinventory_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (txtskuno.Text == "")
            {
                MessageBox.Show("Barcode Must be 13 Digit Characters");
                txtskuno.Text = "";
                txtskuno.Focus();
            }
            else if (txtskuno.Text.Length < 13)
            {
                MessageBox.Show("Barcode Must be 13 Digit Characters");
                txtskuno.Text = "";
                txtskuno.Focus();
            }
            else
            {
                //string existingpord = "";
                //int count = 0;
                string finalqty = "";
                finalqty = Classes.BarcodeSettings.getBarcodeQuantity(txtskuno.Text);//txtskuno.Text.Substring(0, 12).Trim();//Classes.BarcodeSettings.getBarcodeQuantity(txtskuno.Text);
                string primalproductcode = Classes.BarcodeSettings.getBarcodePrimalProductCode(txtskuno.Text);
                double availableqty = 0.0;
                double getcurrentqty = 0.0;
                string getcurrentgridqty = "0";
                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    getcurrentgridqty = dataGridView1.Rows[i].Cells["Available"].Value.ToString();
                    getcurrentqty = getcurrentqty + Convert.ToDouble(getcurrentgridqty);
                }
                string productcategorycode2 = Database.getSingleQuery("PrimalCuts", "ProductCode='" + Classes.BarcodeSettings.getBarcodePrimalProductCode(txtskuno.Text) + "'  ", "ProductCategoryCode");

                double getexistingqty = 0.0;
                //get the  total summation of inventory per batch code
                string mark = getProductCode();
                getexistingqty = Database.getTotalSummation2("Inventory", "Product = '" + primalproductcode + "' AND Branch='" + Login.assignedBranch + "' AND IsStock='1'", "Available"); //no batch code FIFO lookup to carcass Product Code
                //getexistingqty = Database.getTotalSummation2("Inventory", "IsStock='1' AND Product='"+getProdCatCode()+"' AND BatchCode='"+txtbatchcode.Text+"'", "Available");
                availableqty = Convert.ToDouble(getexistingqty) - getcurrentqty; //23.221-40
                double totalqty = getcurrentqty + Convert.ToDouble(finalqty); //sulod sa gridview plus currentqty to be input in textfield
                if (txtskuno.Text.Substring(0, 2).Trim() != productcategorycode2)//!isnvalidproduct)
                {
                    MessageBox.Show("Invalid ProductCategoryCode for this Product");
                    txtskuno.Text = "";
                }
                else
                {
                    //add();
                    if (Convert.ToDouble(finalqty.Trim()) > Convert.ToDouble(availableqty))
                    {
                        MessageBox.Show("Total Qty = " + getexistingqty + " \n Available Qty is " + availableqty);
                    }
                    else
                    {
                        add();
                        //display();
                        txtskuno.Text = "";
                    }
                }
            }
            this.Cursor = Cursors.Default;
        }

        void add()
        {
            try
            {
                SqlConnection con = Database.getConnection();
                con.Open();
                string query = "sp_AddBranchOrder";
                try
                {
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmdevno", AddBranchOrder.txtdevno.Text);
                    com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                    com.Parameters.AddWithValue("@parmpono", txtponum.Text);
                    com.Parameters.AddWithValue("@parmbarcode", txtsku.Text);
                    com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                    com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                    com.Parameters.AddWithValue("@preparedby", Login.isglobalUserID);
                    //com.Parameters.Add("@ParmError", SqlDbType.VarChar, 200);
                    //com.Parameters["@ParmError"].Direction = ParameterDirection.Output;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                    //MessageBox.Show(com.Parameters["@ParmError"].Value.ToString());
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        void ConfirmBranchOrder()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_ConfirmBranchOrder";
            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmdevno", txtdevno.Text);
                com.Parameters.AddWithValue("@parmrefno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmpono", txtponum.Text);
                com.Parameters.AddWithValue("@parmbarcode", txtsku.Text);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmorigin", Login.assignedBranch);
                com.Parameters.AddWithValue("@preparedby", Login.isglobalUserID);
                //com.Parameters.Add("@ParmError", SqlDbType.VarChar, 200);
                //com.Parameters["@ParmError"].Direction = ParameterDirection.Output;
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                //MessageBox.Show(com.Parameters["@ParmError"].Value.ToString());
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void txtskuno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            btnaddinventory.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ConfirmBranchOrder();
                MessageBox.Show("SUCCESS");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace.ToString());
            }
            this.Dispose();
            this.Close();
            this.Cursor = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void AddPrimalCutInventory_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtweight.Text = "";
            txtskuno.Text = "";
            txtweight.Focus();
        }

        private void txtproduct_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void txtports_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = txtports.Text.Trim();
            serialPort1.Open();
            serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
            serialPort1.RtsEnable = true;
            serialPort1.DtrEnable = true;
        }

        private void AddPrimalCutInventory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) 
                serialPort1.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HOForms.SearchProducts saerprod = new HOForms.SearchProducts();
            saerprod.FormClosed += new FormClosedEventHandler(saerprod_FormClosed);
            saerprod.ShowDialog(this);
        }

        void saerprod_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.ActiveControl = txtweight;
            txtproduct.Text = HOForms.SearchProducts.prodname;
            if(txtprodcode.Text!="")
            txtweight.Focus();
        }

        private void txtproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button5.PerformClick();
        }

        private void txtproduct_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtproduct_TextUpdate(object sender, EventArgs e)
        {
            txtweight.Focus();
        }

        private void txtweight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //HelperFunction.isEnableAlphaWithDecimal(e);
        }

        
    }
}
