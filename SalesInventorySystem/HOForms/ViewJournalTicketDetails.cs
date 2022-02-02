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
    public partial class ViewJournalTicketDetails : Form
    {
        public ViewJournalTicketDetails()
        {
            InitializeComponent();
        }

        private void ViewJournalTicketDetails_Load(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM TicketDetails WHERE TicketNumber='" + HOForms.ViewJournalTickets.referencecode + "'", gridControl1, gridView1);
        }
    }
}
