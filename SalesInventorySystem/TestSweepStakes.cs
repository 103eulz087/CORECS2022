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

namespace SalesInventorySystem
{
    public partial class TestSweepStakes : DevExpress.XtraEditors.XtraForm
    {
        public TestSweepStakes()
        {
            InitializeComponent();
        }

        void txtChanged()
        {
            //string combination = txta.Text + txtb.Text + txtc.Text + txtd.Text + txte.Text + txtf.Text;
            //if (!String.IsNullOrEmpty(txta.Text)) { txtb.Focus(); }
            //else if (String.IsNullOrEmpty(txtb.Text)) { txtb.Focus(); }
            //else if (String.IsNullOrEmpty(txtc.Text)) { txtc.Focus(); }
            //else if (String.IsNullOrEmpty(txtd.Text)) { txtd.Focus(); }
            //else if (String.IsNullOrEmpty(txte.Text)) { txtf.Focus(); }
            //else if (String.IsNullOrEmpty(txtf.Text)) { txtavailable.Text = getAvailableNum(combination); }
        }

        String getAvailableNum(string comb)
        {
            string num = "";
            num = Database.getSingleQuery($"SELECT TOP(1) NumCtr FROM COM000 WHERE Combination2='{comb}'", "NumCtr");
            return num;
        }

        private void TestSweepStakes_Load(object sender, EventArgs e)
        {
            txta.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addTicket();
            Database.display($"SELECT TransNo,SeqNo,Combination,SerialNo,CAST(DatePosted as date) as DatePosted" +
                $" FROM COM001 WHERE MobileNo='09173180339' " +
                $"AND CAST(DatePosted as date)='{DateTime.Now.ToShortDateString()}' ORDER BY SeqNo",gridControl1,gridView1);
        }
        void addTicket()
        {
            string combination = txta.Text + txtb.Text + txtc.Text + txtd.Text + txte.Text + txtf.Text;
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sptest_testsweep";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmcombination", combination);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            zzzDemozzz.TicketSweepstakes printit = new zzzDemozzz.TicketSweepstakes();
            for(int i=0;i<=gridView1.RowCount-1;i++)
            {
                printit.xrLabelDate.Text = gridView1.GetRowCellValue(i, "DatePosted").ToString();
                printit.xrNum.Text = gridView1.GetRowCellValue(i, "Combination").ToString();
                printit.xrserial.Text = gridView1.GetRowCellValue(i, "TransNo").ToString();
            }
            ReportPrintTool report = new ReportPrintTool(printit);
            report.ShowRibbonPreviewDialog();
        }

        private void txta_TextChanged(object sender, EventArgs e)
        {
            //txtChanged();
            if (!String.IsNullOrEmpty(txta.Text)) txtb.Focus();
        }

        private void txtb_TextChanged(object sender, EventArgs e)
        {
            //txtChanged();
            if (!String.IsNullOrEmpty(txtb.Text)) txtc.Focus();
        }

        private void txtc_TextChanged(object sender, EventArgs e)
        {
            //txtChanged();
            if (!String.IsNullOrEmpty(txtc.Text)) txtd.Focus();
        }

        private void txtd_TextChanged(object sender, EventArgs e)
        {
            //txtChanged();
            if (!String.IsNullOrEmpty(txtd.Text)) txte.Focus();
        }

        private void txte_TextChanged(object sender, EventArgs e)
        {
            //txtChanged();
            if (!String.IsNullOrEmpty(txte.Text)) txtf.Focus();
        }

        private void txtf_TextChanged(object sender, EventArgs e)
        {
            //txtChanged();
            if (!String.IsNullOrEmpty(txtf.Text)) txtavailable.Text= getAvailableNum(txta.Text+txtb.Text+txtc.Text+txtd.Text+txte.Text+txtf.Text);
        }
    }
}