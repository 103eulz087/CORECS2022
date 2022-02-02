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

namespace SalesInventorySystem.POS
{
    public partial class POSXreadReport : Form
    {
        public static string totalkilos, totalamount,totalvat,totalnonvat;
        string option = "",groupname;
        int cord;
        public static string reportTitle = "";
        //double total = 0.0;
        public POSXreadReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                option = "GROUP";
                panelgroup.Visible = true;
                panelfull.Visible = false;
            }
            else if (radioButton3.Checked == true)
            {
                option = "GROUPITEMSALES";
                panelgroup.Visible = false;
                panelfull.Visible = false;
            }
            else if (radioButton2.Checked == true)
            {
                option = "FULL";
                panelgroup.Visible = false;
                panelfull.Visible = true;
            }
            else if (radioButton4.Checked == true)
            {
                option = "BYCASHIER";
                panelgroup.Visible = false;
                panelfull.Visible = false;
            }
            execute();
            txttotalkilos.Text = totalkilos;
            txttotalamount.Text = totalamount;
            txtvat.Text = totalvat;
            txtnonvat.Text = totalnonvat;
        }

        void execute()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
               
                string query="";
                if (radx.Checked == true)
                {
                    query = "spr_XreadRep";
                }
                else if (rady.Checked == true)
                {
                    query = "spr_XreadRep2";
                }
                SqlCommand com = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                DataTable table = new DataTable();
                com.Parameters.AddWithValue("@parmbrcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@datefrom", dateTimePicker1.Text);
                com.Parameters.AddWithValue("@parmoption", option);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                dataGridView1.DataSource=null;
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                //Database.displayLocalGrid("SELECT * FROM view_XReadGroupReport WHERE DateOrder >= '" + dateTimePicker1.Text + "' AND DateOrder <= '" + dateTimePicker2.Text + "'", dataGridView1);
                double totkg = 0.0, totamount = 0.0, vat = 0.0, vatableExemptAmount = 0.0;
                //dataGridView1.Columns["TotalAmount"].DefaultCellStyle.Format = "N2";
                for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    if (radioButton1.Checked == true) //if group category sales is selected
                    {
                        groupname = dataGridView1.Rows[i].Cells["Description"].Value.ToString();
                        totkg += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalKilos"].Value),2);

                    }
                    
                    totamount += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value),2);

                    if (option == "FULL") //if full transaction summary is selected of radiobutton2
                    {
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["isVat"].Value.ToString()) == false)
                        {
                            vat = 0.0;
                        }
                        else
                        {
                            vat = totamount * 0.12;
                            vatableExemptAmount += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value),2);
                        }
                        //total += Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value);
                    }

                    //if (radioButton3.Checked == true)
                    //{

                    //}
                }
                if (radioButton4.Checked != true)
                {
                    totalkilos = totkg.ToString();
                    totalamount = totamount.ToString();
                    totalvat = computeTotalVAT().ToString(); //12 % vat
                    totalnonvat = computeTotalVATableAndNonVatableSale().ToString(); //total
                    lblvat.Text = computeVAT().ToString();
                    lblvatablesale.Text = computeVATableSale().ToString();
                    lblvatexemptsale.Text = computeVATExemptSale().ToString();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            con.Close();
        }

        Double computeTotalVATableAndNonVatableSale()
        {
            double vatexemptsale = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                 vatexemptsale += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
            }
            return vatexemptsale;
        }

        Double computeVATableSale()
        {
            double vatexemptsale = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    vatexemptsale += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                }
            }
            return vatexemptsale;
        }

        Double computeVATExemptSale()
        {
            double vatexemptsale = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["isVat"].Value.ToString()) == false)
                {
                    vatexemptsale += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                }
            }
            return vatexemptsale;
        }

        Double computeVAT()
        {
            double vatexemptsale = 0.0;
            double vat = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    vatexemptsale += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                }
            }
            vat = (vatexemptsale / 1.12) * .12;
            return vat;
        }

        Double computeTotalVAT()
        {
            double vatexemptsale = 0.0;
            double vat = 0.0;
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    vatexemptsale += Math.Round(Convert.ToDouble(dataGridView1.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                }
            }
            vat = vatexemptsale * .12;
            return vat;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Printing printit = new Printing();
               //public void printXReadReportGroupSales(string totalkilos, string totalamount, DataGridView gridview,string date1,string date2)
            //printit.printReceipt(lbltranscode.Text, lblrefno.Text, txtamountpayable.Text, String.Format("{0:n2}", Convert.ToDouble(txtamounttender.Text)), txtamountchange.Text, PointOfSale.mygridview);
            if (dataGridView1.RowCount < 1)
            {
                XtraMessageBox.Show("No Data to Print!");
                return;
            }
            if (radx.Checked == true)
            {
                reportTitle = "X";
            }
            else if (rady.Checked == true)
            {
                reportTitle = "Z";
            }

            if (radioButton1.Checked == true)
            {
                printit.printXReadReportGroupSales(txttotalkilos.Text, txttotalamount.Text, dataGridView1, dateTimePicker1.Text, dateTimePicker1.Text);
            }
            else if (radioButton2.Checked == true)
            {
                printit.printXReadReportFullTransactionSales(txtvat.Text, txttotalamount.Text, dataGridView1, dateTimePicker1.Text, dateTimePicker1.Text);
            }
            else if (radioButton3.Checked == true)
            {
                printit.printXReadReportFullTransactionSales(txtvat.Text, txttotalamount.Text, dataGridView1, dateTimePicker1.Text, dateTimePicker1.Text);
            }
            this.Dispose();
        }

        private void POSXreadReport_Load(object sender, EventArgs e)
        {
            loadBranch();
            radioButton1.Checked = true;
        }

        void loadBranch()
        {
            Database.displayComboBoxItems("SELECT BranchCode FROM Branches", "BranchCode", txtbrcode);
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void showTransactionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cord = dataGridView1.CurrentCellAddress.Y;
            //display();
            //txttotal.Text = ComputeTotal().ToString();
        }

        //double ComputeTotal()
        //{
        //    double tot = 0.0;
        //    for (int i = 0; i <= dataGridView2.RowCount - 1; i++)
        //    {
        //        tot += Convert.ToDouble(dataGridView2.Rows[i].Cells["SubTotal"].Value);
        //    }
        //    return tot;
        //}

        //void display()
        //{
        //    int cord = dataGridView1.CurrentCellAddress.Y;
        //    if (radioButton2.Checked == true)
        //    {
        //        if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isVat"].Value.ToString()) == true)
        //        {
        //            //Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)),BranchCode,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' and isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and isVat='1' and Status='SOLD' UNION ALL (select CAST(SequenceNumber as varchar(10)),BranchCode,Description,SubTotal,DateOrder FROM (select '' AS SequenceNumber,'' AS BranchCode,'' AS Description,SUM(SubTotal) AS SubTotal,'' AS DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' AND isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat='1') as dt) ", dataGridView2);
        //            //DataGridViewSettings.gridFooter(dataGridView2);
        //            Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)) AS SequenceNumber,BranchCode,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE CAST(DateOrder as date)='"+dateTimePicker1.Text+"' AND BranchCode='" + txtbrcode.Text + "' and isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and isVat='1' and Status='SOLD'", dataGridView2);
        //          //  Database.display("select CAST(SequenceNumber as varchar(10)) AS SequenceNumber,BranchCode,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE CAST(DateOrder as date)='" + dateTimePicker1.Text + "' AND BranchCode='" + txtbrcode.Text + "' and isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and isVat='1' and Status='SOLD'");
        //        }
        //        if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isVat"].Value.ToString()) == false)
        //        {
        //            //Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)),BranchCode,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' and isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and isVat='0' and Status='SOLD' UNION ALL (select CAST(SequenceNumber as varchar(10)),BranchCode,Description,SubTotal,DateOrder FROM (select '' AS SequenceNumber,'' AS BranchCode,'' AS Description,SUM(SubTotal) AS SubTotal,'' AS DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' AND isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and isVat='0') as dt) ", dataGridView2);
        //            //DataGridViewSettings.gridFooter(dataGridView2);
        //            Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)) AS SequenceNumber,BranchCode,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE CAST(DateOrder as date)='" + dateTimePicker1.Text + "' AND BranchCode='" + txtbrcode.Text + "' and isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and isVat='0' and Status='SOLD' ", dataGridView2);
        //        }
        //    }
        //    else if (radioButton1.Checked == true)
        //    {
        //        //Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)),BranchCode,ReferenceNo,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' AND Description='" + dataGridView1.Rows[cord].Cells["Description"].Value.ToString() + "' and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' UNION ALL (select CAST(SequenceNumber as varchar(10)),BranchCode,ReferenceNo,Description,SubTotal,DateOrder FROM (select '' AS SequenceNumber,'' AS BranchCode,'' AS ReferenceNo,'' AS Description,SUM(SubTotal) AS SubTotal,'' AS DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' AND Description='" + dataGridView1.Rows[cord].Cells["Description"].Value.ToString() + "' and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD') as dt) ", dataGridView2);
        //        //DataGridViewSettings.gridFooter(dataGridView2);
        //        Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)) AS SequenceNumber,BranchCode,ReferenceNo,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE CAST(DateOrder as date)='" + dateTimePicker1.Text + "' AND BranchCode='" + txtbrcode.Text + "' AND Description='" + dataGridView1.Rows[cord].Cells["Description"].Value.ToString() + "' and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD'", dataGridView2);
        //        dataGridView2.Columns[0].Visible = false;
        //    }
        //    else if (radioButton3.Checked == true)
        //    {
        //        //Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)),BranchCode,ReferenceNo,Description,SubTotal,DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' AND Description='" + dataGridView1.Rows[cord].Cells["Description"].Value.ToString() + "' and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' UNION ALL (select CAST(SequenceNumber as varchar(10)),BranchCode,ReferenceNo,Description,SubTotal,DateOrder FROM (select '' AS SequenceNumber,'' AS BranchCode,'' AS ReferenceNo,'' AS Description,SUM(SubTotal) AS SubTotal,'' AS DateOrder FROM view_XReadGroupReport WHERE BranchCode='" + txtbrcode.Text + "' AND Description='" + dataGridView1.Rows[cord].Cells["Description"].Value.ToString() + "' and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD') as dt) ", dataGridView2);
        //        //DataGridViewSettings.gridFooter(dataGridView2);
        //        Database.displayLocalGrid("select CAST(SequenceNumber as varchar(10)) AS SequenceNumber,ProductCode,Description,QtySold,SubTotal,TotalAmount,TaxTotal,DateOrder FROM view_XReadGroupReport WHERE CAST(DateOrder as date)='" + dateTimePicker1.Text + "' AND BranchCode='" + txtbrcode.Text + "'  and  isConfirmed=1 and isVoid=0 and isHold=0 and isCancelled=0 and isErrorCorrect=0 and Status='SOLD' and ProductCode='"+dataGridView1.Rows[cord].Cells["ProductCode"].Value.ToString()+"'", dataGridView2);
        //        dataGridView2.Columns[0].Visible = false;
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            //if (dataGridView2.RowCount == 0)
            //{
            //    XtraMessageBox.Show("No Data to Refund!");
            //}
            //else
            //{
                Printing printit = new Printing();
                //printit.printRefundSales(dataGridView2.Rows[0].Cells["ReferenceNo"].Value.ToString(), String.Format("{0:n2}", Convert.ToDouble(lblvatablesale.Text)), String.Format("{0:n2}", Convert.ToDouble(lblvatexemptsale.Text)), String.Format("{0:n2}", Convert.ToDouble(lblvat.Text)), this.dataGridView1);
                //refund();
                refundTickets();
                XtraMessageBox.Show("Success!!!");
            //}
            this.Dispose();
        }

        //void refund()
        //{
        //    for (int i = 0; i <= dataGridView2.RowCount - 1; i++)
        //    {
        //        Database.ExecuteQuery("UPDATE BatchSalesDetails2 SET isErrorCorrect='1',TotalAmount=0 WHERE CAST(SequenceNumber as varchar(10))='" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "'");
        //        Database.ExecuteQuery("UPDATE TransactionSalesDetails2 SET ErrorTag='1'  WHERE CAST(BatchSeqNum as varchar(10))='" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "'");
        //    }
        //    XtraMessageBox.Show("Success!");
        //}

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        void refundTickets()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_RefundKunuhay";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode",txtbrcode.Text);
                com.Parameters.AddWithValue("@parmdate", dateTimePicker1.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
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

        private void dataGridView2_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        //private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        this.dataGridView2.Rows[e.RowIndex].Selected = true;
        //        this.rowindex = e.RowIndex;
        //        this.dataGridView2.CurrentCell = this.dataGridView2.Rows[e.RowIndex].Cells[1];
        //        this.contextMenuStrip2.Show(this.dataGridView2, e.Location);
        //        contextMenuStrip2.Show(Cursor.Position);
        //    }
        //}

        //private void deleteThisItemToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (!this.dataGridView2.Rows[this.rowindex].IsNewRow)
        //    {
        //        this.dataGridView2.Rows.RemoveAt(this.rowindex);
        //    }
        //    txttotal.Text = ComputeTotal().ToString();
        //}

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //foreach (DataGridViewRow Myrow in dataGridView1.Rows)
            //{            //Here 2 cell is target value and 1 cell is Volume
            //    if (radioButton3.Checked == false && radioButton4.Checked == false)
            //    {
            //        if (Convert.ToBoolean(Myrow.Cells["isVat"].Value.ToString()) == true)// Or your condition 
            //        {
            //            Myrow.DefaultCellStyle.BackColor = Color.Red;
            //        }
            //        else
            //        {
            //            Myrow.DefaultCellStyle.BackColor = Color.Green;
            //        }
            //    }
            //}
        }
    }
}
