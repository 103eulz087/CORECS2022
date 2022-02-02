using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class ViewJournalTickets : Form
    {
        string referencePOS = "";//refPaidTickets = "",refRcvInv="";
        public static string referencecode;
        public ViewJournalTickets()
        {
            InitializeComponent();
        }

        private void ViewJournalTickets_Load(object sender, EventArgs e)
        {
            referencePOS = POSTransactions.refno;
            //refPaidTickets = ViewOrder.refno;
            //refRcvInv = ViewRequest.refno;
            if (referencePOS != "")
            {
                Database.display("SELECT * FROM TicketMaster WHERE ReferenceNumber='" + referencePOS + "'", gridControl1, gridView1);
            }
            //if(refPaidTickets != "")
            //{
            //    Database.display("SELECT * FROM TicketMaster WHERE ReferenceNumber='" + refPaidTickets + "' and Mnemonic='InvPay'", gridControl1, gridView1);
            //}
            //if (refRcvInv != "")
            //{
            //    Database.display("SELECT * FROM TicketMaster WHERE ReferenceNumber='" + refRcvInv + "' and Mnemonic='InvRcpt'", gridControl1, gridView1);
            //}
            //Database.GridMasterDetail("TicketMaster", "TicketDetails", "ReferenceNumber='" + POSTransactions.refno + "'", "ReferenceNumber='" + POSTransactions.refno + "'", "ReferenceNumber", "ReferenceNumber", "JournalDetails", gridControl1);
           // Database.display("SELECT * FROM TicketMaster WHERE ReferenceNumber='" + referencePOS + "'", gridControl1, gridView1);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            referencecode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString();
            HOForms.ViewJournalTicketDetails viewtickdet = new ViewJournalTicketDetails();
            viewtickdet.Show();
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);

                // contextMenuStrip1.Items[2].Visible = false;
            }
        }

        private void viewTicketDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            referencecode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TicketNumber").ToString();
            HOForms.ViewJournalTicketDetails viewtickdet = new ViewJournalTicketDetails();
            viewtickdet.Show();
        }
    }
}
