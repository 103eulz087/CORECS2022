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

namespace SalesInventorySystem.Accounting
{
    public partial class ViewPurchaseJournal : Form
    {
        public string ticketnum = String.Empty;
        public ViewPurchaseJournal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void display()
        {
            try
            {
                //Database.GridMasterDetail("TicketMaster", "TicketDetails", "CAST(TicketDate as date)='"+txtdate.Text+"' AND JournalType='PURCHASING'", "CAST(TicketDate as date)='" + txtdate.Text + "'", "TicketNumber", "TicketNumber", "TicketDetails", gridControl1);
                Database.display("SELECT * FROM viewTicketMaster WHERE TicketDate ='" + txtdate.Text + "' AND JournalType='PURCHASING' ",gridControl1,gridView1);
            }
          
            catch (SqlException sex2)
            {
                XtraMessageBox.Show(sex2.Message.ToString());
            }
           
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ticketnum = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString();
            Accounting.ViewTicketMasterDetails vies = new ViewTicketMasterDetails();
            vies.Show();
            Database.display("SELECT * FROM TicketDetails WHERE TicketNumber='" + ticketnum + "'", vies.gridControl1, vies.gridView1);
        }
    }
}
