using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace SalesInventorySystem
{
    public partial class BatchUploading : DevExpress.XtraEditors.XtraForm
    {

        double totalamount = 0.0;
        public BatchUploading()
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
            loadFile1();
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

        void loadFile1()
        {
            String sexcelconnectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textEdit1.Text + ";Extended Properties=" + "\"Excel 8.0;HDR=Yes;\"";
            OleDbConnection con = new OleDbConnection(sexcelconnectionstring);
            string strsql = "select * FROM [sheet1$]";
            OleDbCommand cmd = new OleDbCommand(strsql, con);
            DataTable dt = new DataTable();
            con.Open();
            try
            {
                OleDbDataReader dr1 = cmd.ExecuteReader();
                StreamWriter sw = new StreamWriter("C:\\MyFiles\\export.txt");
                if(dr1.Read())
                {
                    dt.Load(dr1);
                }
                int icolcount = dt.Columns.Count;
                //for(int i=0;i< icolcount;i++)
                //{
                //    sw.Write(dt.Columns[i]);
                //    if(i < icolcount - 1)
                //    {
                //        sw.Write("\t");
                //    }
                //}
                //sw.Write(sw.NewLine);
                foreach(DataRow dr in dt.Rows)
                {
                    for(int i=0; i < icolcount;i++)
                    {
                        if(!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }
                        if(i < icolcount - 1)
                        {
                            sw.Write("");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
            con.Close();
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

        private void BatchUploading_Load(object sender, EventArgs e)
        {
            //int id = IDGenerator.getSalesTransactionID();
            
            //int refnumber = IDGenerator.getOrderNumber();
            //string referencenumber2 = IDGenerator.getReferenceNumber();
            //txtrefno.Text = referencenumber2;
            //txtorderno.Text = refnumber.ToString();
            //txttransid.Text = Database.getSingleQuery("SalesTransactionSummary", "UserID='" + Login.isglobalUserID + "' AND BranchCode='" + Login.assignedBranch + "' AND isOpen='1' ", "AccountCode");
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            analyze();
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
            txttotalitem.Text = dataGridView1.RowCount.ToString();
            for (int i =0;i<=dataGridView1.RowCount-1;i++)
            {
                totalamount += Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            txttotalamount.Text = totalamount.ToString();
        }

        void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            //{            //Here 2 cell is target value and 1 cell is Volume
            //    if (Convert.ToInt32(Myrow.Cells[2].Value) > Convert.ToInt32(Myrow.Cells[4].Value))// Or your condition 
            //    {
            //        Myrow.DefaultCellStyle.BackColor = Color.Red;
            //    }
            //    else
            //    {
            //        Myrow.DefaultCellStyle.BackColor = Color.Green;
            //    }
            //}
        }

        void analyze()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_Analyze";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                adapter.Fill(table);
                dataGridView1.DataSource = table;
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

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //process();
        }

       

        void process()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                //string referencenumber2 = IDGenerator.getReferenceNumber();
                string query = "spu_ComputeBatchUpload";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@parmrefno", txtorderno.Text);
                com.Parameters.AddWithValue("@parmtransno", txttransid.Text);
                com.Parameters.AddWithValue("@parmreferenceno", txtrefno.Text);
                com.Parameters.AddWithValue("@parmuser", Login.Fullname);
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

        //private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    if ((double)cell.Value < 0) { e.CellStyle.ForeColor = Color.Red; }
        //}
       
    }
}