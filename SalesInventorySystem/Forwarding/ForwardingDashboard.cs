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

namespace SalesInventorySystem.Forwarding
{
    public partial class ForwardingDashboard : DevExpress.XtraEditors.XtraForm
    {
        string connection = @"ITCSI\ConnSettingsForwarding";
        public ForwardingDashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayPrimeOverPending();
        }

        void displayPrimeOverPending()
        {
            Database.display("SELECT * FROM view_Monitoring WHERE Status='PENDING' and DateAdded Between '" + datefrompending.Text+"' and '"+datetopending.Text+"'", gridControl1, gridView1, Database.getCustomizeConnection(connection));
        }

        void filtertab()
        {
            if (tabControl1.SelectedTab.Equals(Pending))
            {
                Database.display("SELECT * FROM view_Monitoring WHERE Status='PENDING' and DateAdded Between '" + datefrompending.Text + "' and '" + datetopending.Text + "' ", gridControl1, gridView1, Database.getCustomizeConnection(connection));
            }
            else if (tabControl1.SelectedTab.Equals(delivered))
            {
                Database.display("SELECT * FROM view_MonitoringDelivered WHERE Status='DELIVERED' and DateAdded Between '" + datefromdelivered.Text + "' and '" + datetodelivered.Text + "' ", gridControlDelivered, gridViewDelivered, Database.getCustomizeConnection(connection));
            }

           
        }
        void filtertab2()
        {
            if (tabControl2.SelectedTab.Equals(pendingtrip))
            {
                Database.display("SELECT * FROM TripTicketMaster WHERE Status='PENDING'  and DateAdded Between '" + datefrompendingtrip.Text + "' and '" + datetopending.Text + "' ", gridControlpendingtrip, gridViewpendingtrip, Database.getCustomizeConnection(connection));
            }
            else if (tabControl2.SelectedTab.Equals(deliveredtrip))
            {
                Database.display("SELECT * FROM TripTicketMaster WHERE Status='DELIVERED' and DateAdded Between '" + datefromdelivtrip.Text + "' and '" + datetodelivered.Text + "' ", gridControldelivtrip, gridViewdelivtrip, Database.getCustomizeConnection(connection));
            }
        }
        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ForwardingUpdateShipment asdi = new ForwardingUpdateShipment();
            asdi.txttripid.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TripID").ToString();
            asdi.txtdatepullout.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DatePullOut").ToString();
            asdi.ShowDialog(this);
            
        }

        private void ForwardingDashboard_Load(object sender, EventArgs e)
        {
            filtertab();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_MonitoringDelivered WHERE Status='DELIVERED' and DateAdded Between '" + datefromdelivered.Text + "' and '" + datetodelivered.Text + "' ", gridControlDelivered, gridViewDelivered, Database.getCustomizeConnection(connection));
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM TripTicketMaster WHERE Status='DELIVERED' and DateAdded Between '" + datefromdelivtrip.Text + "' and '" + datetodelivered.Text + "' ", gridControldelivtrip, gridViewdelivtrip, Database.getCustomizeConnection(connection));
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM TripTicketMaster WHERE Status='PENDING'  and DateAdded Between '" + datefrompendingtrip.Text + "' and '" + datetopending.Text + "' ", gridControlpendingtrip, gridViewpendingtrip, Database.getCustomizeConnection(connection));
        }
    }
}