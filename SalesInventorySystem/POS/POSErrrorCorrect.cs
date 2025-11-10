using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSErrrorCorrect : Form
    {
        string orderno = "", sequenceNo = "";
       
        string str, option;
        public static bool isdone = false;
        public POSErrrorCorrect()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                this.Dispose();
            }
           
            return functionReturnValue;
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                searchItem();
        }

        void searchItem()
        {
            bindgrid();
        }

        private void POSErrrorCorrect_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtorderno;
            txtorderno.Focus();
            this.Text = "REFUND TRANSACTION";
            txtbranch.Text = Login.assignedBranch;
            //int id = IDGenerator.getPOSReturnTransactionNumber();"POSReturnTransaction", "BranchCode='" + Login.assignedBranch + "'", "ReturnTransactionNo"
            int id = IDGenerator.getIDNumber("POSReturnTransaction", "BranchCode='" + Login.assignedBranch + "'", "ReturnTransactionNo", 1);
            txtreturntransno.Text = HelperFunction.sequencePadding(id.ToString());
        }

        private void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_ErrorCorrect"; //VOID ALL TRANSACTION VOID TANAN ITEMS SA BATCHSALESDETAILS ASTA PUD SA BATCHSALESSUMMARY
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.Parameters.AddWithValue("@parmrefcode", HelperFunction.sequencePadding1(txtorderno.Text, 18));
            com.Parameters.AddWithValue("@parmtransno", txttransno.Text);
            com.Parameters.AddWithValue("@parmcashiertranscode", PointOfSale.cashierTransactionCode);
            com.Parameters.AddWithValue("@parmremarks", txtremarks.Text);
            com.Parameters.AddWithValue("@parmuser", PointOfSale.userid);
            com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        void returnALL()
        {
            try
            {
                Printing printit = new Printing();
                bool ok = HelperFunction.ConfirmDialog("Are you sure you want to Error Correct this item?", "Error Correct");
                if (ok)
                {
                    if (Convert.ToInt32(dataGridView1.RowCount) < 1)
                    {
                        XtraMessageBox.Show("No Transaction to be Correct");
                    }
                    else
                    {
                        //printit.printVoidSales(dataGridView1.Rows[0].Cells["TransactionCode"].Value.ToString(), txtorderno.Text, String.Format("{0:n2}", Convert.ToDouble(lblvatsale.Text)), String.Format("{0:n2}", Convert.ToDouble(lblvatexemptsale.Text)), String.Format("{0:n2}", Convert.ToDouble(lblvat.Text)), this.dataGridView1, qtysold, sellingprice, totalamount, desc);
                        //printVoidAllSales
                        //printit.printVoidAllSales(txtreturntransno.Text,txttransno.Text, txtorderno.Text, String.Format("{0:n2}", Convert.ToDouble(lblvatsale.Text)), String.Format("{0:n2}", Convert.ToDouble(lblvatexemptsale.Text)), String.Format("{0:n2}", Convert.ToDouble(lblvat.Text)), this.dataGridView1);


                        printit.printReturnSelectedItem(txtreturntransno.Text, txttransno.Text, HelperFunction.sequencePadding1(txtorderno.Text, 18), this.dataGridView1);
                        Database.ExecuteQuery("INSERT INTO POSReturnTransaction VALUES('" + Login.assignedBranch + "'" +
                            ",'" + txtreturntransno.Text + "'" +
                            ",'" + HelperFunction.sequencePadding1(txtorderno.Text,18) + "'" +
                            ",'" + txttransno.Text + "'" +
                            ",'" + PointOfSale.cashierTransactionCode + "'" +
                            ",'" + DateTime.Now.ToString() + "'" +
                            ",'" + Login.Fullname + "'" +
                            ",'" + Environment.MachineName.ToString() + "')");
                        execute();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
      
        
        void returnSelectedItem()
        {
            option = "";
            option = "Checkbox";
            orderno = HelperFunction.sequencePadding1(txtorderno.Text, 18);
            bool ok = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (chk.Value != chk.FalseValue || chk.Value != null)
                {
                    ok = true;
                }
            }
            if (!ok)
            {
                XtraMessageBox.Show("No items to Returned!.. Please select item in the checkbox you want to void.");
                return;
            }
            Printing printit = new Printing();
            delete(); //gi update lng sa ang isErrorCorrect = 1
            voidSelectedItem(); //dri na gi update ang status sa SP Status='RETURNED'
            printit.printReturnSelectedItem(txtreturntransno.Text, txttransno.Text, orderno, this.dataGridView1);
            ////////////////////////////////////////////////
            //string filepathConso = "C:\\POSTransaction\\ReturnedSales\\";
            string filepath = "C:\\POSTransaction\\ReturnedSalesConso\\" + orderno + ".txt";
            var fileContent = string.Empty;
            using (var streamReader = new StreamReader(filepath, Encoding.UTF8))
            {
                fileContent = File.ReadAllText(filepath);
            }
            string filepathorig = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            string txtorder = "\\" + PointOfSale.cashierTransactionCode + "_E-JOURNAL.txt";
            string filetoprint = filepathorig + txtorder;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepathorig))
            {
                Directory.CreateDirectory(filepathorig);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(fileContent);
            writer.Close();

            ////////////////////////////////////////////////
            //Database.ExecuteQuery("INSERT INTO POSReturnTransaction VALUES('"+ Login.assignedBranch + "','"+txtreturntransno.Text+"','"+txtorderno.Text+"','"+txttransno.Text+"','"+DateTime.Now.ToString()+ "','" + Login.Fullname + "')");
            Database.ExecuteQuery("INSERT INTO POSTransaction VALUES('" + Login.assignedBranch + "','" + txttransno.Text + "','" + Environment.MachineName + "','Transaction Return','" + DateTime.Now.ToString() + "','" + Login.Fullname + "','','','Tranaction Return: OR#: '"+txtorderno.Text+"' processed.')  ");
            XtraMessageBox.Show("Successfully Updated");
            isdone = true;
            this.Close();
        }
    

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

      

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            searchItem();
        }

        private void btnReturnSelected_Click(object sender, EventArgs e)
        {
            returnSelectedItem();
        }

        private void btnReturnALL_Click(object sender, EventArgs e)
        {
            returnALL();
            XtraMessageBox.Show("Successfully Returned!...");
            this.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //if(e.Button == MouseButtons.Right)
            //{
            //    contextMenuStrip1.Show(dataGridView1, e.Location);
            //}
        }

        private void voidSelectedTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void voidSelectedItem()
        {
            sequenceNo = dataGridView1.Rows[0].Cells["SequenceNumber"].Value.ToString();
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_VoidSelectedItem";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.Parameters.AddWithValue("@parmorderno", orderno);
            com.Parameters.AddWithValue("@parmtransno", txttransno.Text);
            com.Parameters.AddWithValue("@parmcashiertranscode", PointOfSale.cashierTransactionCode);
            com.Parameters.AddWithValue("@parmreturntransno", txtreturntransno.Text);
            com.Parameters.AddWithValue("@parmsequenceno", sequenceNo);
            com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
            com.Parameters.AddWithValue("@parmoption", option);
            com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void bindgrid()
        {
            //if(txtorderno.Text.Length <= 9)
            //{
            //    int orderno = 0;
            //    orderno = Convert.ToInt32(txtorderno.Text);
            //}
            //else if (txtorderno.Text.Length > 9)
            //{
            //    Int64 orderno = Convert.ToInt64(txtorderno.Text);
            //}
            Int64 orderno = Convert.ToInt64(txtorderno.Text);
            string neworderno = HelperFunction.sequencePadding1(orderno.ToString(),18);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Clear();
            DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
            col1.Name = "chkcol";
            col1.HeaderText = "CheckBox";
            dataGridView1.Columns.Add(col1);

            SqlConnection con = Database.getConnection();
            con.Open();
            //Database.displayLocalGrid("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(DiscountTotal,'N', 'en-us') AS Discount,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat FROM BatchSalesDetails WHERE ReferenceNo='" + txtOrderNo.Text + "' AND isVoid='0' AND isCancelled='0' and isHold='0' AND BranchCode='" + Login.assignedBranch + "'", MydataGridView1);
            str = "SELECT SequenceNumber" +
              //  ",TransactionCode" +
                ",ReferenceNo" +
                ",BranchCode" +
                ",ProductCode" +
                ",Description" +
                ",FORMAT(SellingPrice,'N', 'en-us') AS SellingPrice" +
                ",QtySold" +
                ",FORMAT(DiscountTotal,'N', 'en-us') AS DiscountTotal" +
                ",FORMAT(TaxTotal,'N', 'en-us') AS TaxTotal" +
                ",FORMAT(SubTotal,'N', 'en-us') AS SubTotal" +
                ",FORMAT(TotalAmount,'N', 'en-us') AS TotalAmount " +
                ",isVat " +
                //",isConfirmed" +
                //",isErrorCorrect" +
                //",isVoid " +
                "FROM BatchSalesDetails  " +
                //"WHERE ReferenceNo='" + txtorderno.Text.Trim() + "' " +
                "WHERE ReferenceNo='" + neworderno.Trim() + "' " +
                "AND BranchCode='" + txtbranch.Text.Trim() + "' " +
                "AND isConfirmed='1' " +
                "AND isErrorCorrect='0' " +
                "AND isCancelled='0' " +
                "AND MachineUsed='"+Environment.MachineName+"' " +
                "AND isVoid='0' ";
            //str = "SELECT * FROM BatchSalesDetails WHERE ReferenceNo='" + txtorderno.Text.Trim() + "' AND BranchCode='" + txtbranch.Text.Trim() + "' AND isConfirmed='1' AND isErrorCorrect='0' AND isVoid='0' ";
            SqlCommand com = new SqlCommand(str, con);
            SqlDataAdapter oledbda = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            oledbda.Fill(ds, "BatchSalesDetails");
            dataGridView1.DataMember = "BatchSalesDetails";
            dataGridView1.DataSource = ds;
            con.Close();
        }

        private void bindgridReverse()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Clear();
            DataGridViewCheckBoxColumn col1 = new DataGridViewCheckBoxColumn();
            col1.Name = "chkcol";
            col1.HeaderText = "CheckBox";
            dataGridView1.Columns.Add(col1);

            SqlConnection con = Database.getConnection();
            con.Open();
            //str = "SELECT * FROM BatchSalesDetails WHERE ReferenceNo='" + txtorderno.Text.Trim() + "' AND BranchCode='" + txtbranch.Text.Trim() + "' AND isConfirmed='1' AND isErrorCorrect='1' ";
            str = "SELECT SequenceNumber" +
                ",ReferenceNo" +
                ",BranchCode" +
                ",Description" +
                ",FORMAT(SellingPrice,'N', 'en-us') AS SellingPrice" +
                ",QtySold" +
                ",FORMAT(DiscountTotal,'N', 'en-us') AS DiscountTotal" +
                ",FORMAT(TaxTotal,'N', 'en-us') AS TaxTotal" +
                ",FORMAT(SubTotal,'N', 'en-us') AS SubTotal" +
                ",FORMAT(TotalAmount,'N', 'en-us') AS TotalAmount " +
                ",isVat " +
                //",isConfirmed" +
                //",isErrorCorrect" +
                //",isVoid " +
                "FROM BatchSalesDetails " +
                "WHERE ReferenceNo='" + HelperFunction.sequencePadding1(txtorderno.Text, 18) + "' " +
                "AND BranchCode='" + txtbranch.Text.Trim() + "' " +
                "AND isConfirmed=1 " +
                "AND isErrorCorrect=1 " +
                "AND MachineUsed='" + Environment.MachineName + "' " +
                "AND Status <> 'RETURNED' ";

            SqlCommand com = new SqlCommand(str, con);
            SqlDataAdapter oledbda = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            oledbda.Fill(ds, "BatchSalesDetails");
            dataGridView1.DataMember = "BatchSalesDetails";
            dataGridView1.DataSource = ds;
            con.Close();
        }

        private void delete()
        {
            string user = "";
            if (String.IsNullOrEmpty(PointOfSale.userid)) { user = Login.isglobalUserID; } else { user = PointOfSale.userid; }

            int i = 0;
            List<int> ChkedRow = new List<int>();
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["chkcol"].Value) == true)
                {
                    ChkedRow.Add(i);
                }
            }
            if (ChkedRow.Count == 0)
            {
                XtraMessageBox.Show("Select one checkbox");
                return;
            }
            bool isHRI = Database.checkifExist($"SELECT TOP(1) isEnableInvoicePrinting FROM dbo.POSType WHERE isEnableInvoicePrinting=1");
            foreach (int j in ChkedRow)
            {
                //str = "Update BatchSalesDetails SET isVoid='1',VoidBy='"+Login.isglobalUserID+ "',isErrorCorrect=1,Status='RETURNED' where SequenceNumber='" + dataGridView1.Rows[j].Cells["SequenceNumber"].Value + "'";
                str = "Update BatchSalesDetails SET isPosted=1,isErrorCorrect=1,VoidBy='" + user + "' " +
                    "WHERE SequenceNumber='" + dataGridView1.Rows[j].Cells["SequenceNumber"].Value + "' " +
                    "AND ReferenceNo='" + HelperFunction.sequencePadding1(txtorderno.Text, 18) +"' " +
                    "AND BranchCode='"+txtbranch.Text+"' " +
                    "AND MachineUsed='"+Environment.MachineName+"' ";
                if (isHRI)
                {
                    Database.ExecuteQuery($"INSERT INTO dbo.CreditMemoHRI VALUES('{Login.assignedBranch}'" +
                         $",'{HelperFunction.sequencePadding1(txtorderno.Text, 18) }'" +
                         $",'{dataGridView1.Rows[j].Cells["SequenceNumber"].Value }'" +
                         $",'{dataGridView1.Rows[j].Cells["ProductCode"].Value.ToString()}'" +
                         $",'{dataGridView1.Rows[j].Cells["Description"].Value.ToString()}'" +
                         $",'{dataGridView1.Rows[j].Cells["SellingPrice"].Value.ToString()}'" +
                         $",'{dataGridView1.Rows[j].Cells["QtySold"].Value}'" +
                         $",'{dataGridView1.Rows[j].Cells["SubTotal"].Value}'" +
                         $",'{dataGridView1.Rows[j].Cells["TotalAmount"].Value}'" +
                         $",'{dataGridView1.Rows[j].Cells["isVat"].Value}'" +
                         $",'0'" +
                         $",'0'" +
                         $",'0'" +
                         $",'{Environment.MachineName.ToString()}'" +
                         $",'{DateTime.Now.ToString()}','{Login.Fullname}')");
                }

                try
                {
                    using (SqlConnection con = Database.getConnection())
                    {
                        using (SqlCommand com = new SqlCommand(str, con))
                        {
                            con.Open();
                            com.ExecuteNonQuery();
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

            //XtraMessageBox.Show("Are you sure you want to delete this Record?", "Confirm deletion", MessageBoxButtons.OK);
            bindgridReverse();
           
        }
    }
}
