using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POS
{
    public partial class POSXreadReportDetails : Form
    {
        public POSXreadReportDetails()
        {
            InitializeComponent();
        }

        private void POSXreadReportDetails_Load(object sender, EventArgs e)
        {

        }

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
        }
    }
