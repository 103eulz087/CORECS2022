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
    public partial class ForwardingUpdateShipment : DevExpress.XtraEditors.XtraForm
    {
        public ForwardingUpdateShipment()
        {
            InitializeComponent();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            bool confirm = HelperFunction.ConfirmDialog("Are you Sure you want to Update this Shipment?","Update Shipment");
            if (confirm)
            {
                Database.ExecuteQuery("UPDATE Monitoring SET DatePullOut='" + txtdatepullout.Text + "',DeliveryNo='" + txtdrno.Text + "',ReturnCy='" + txtdatereturncy.Text + "',ShippingLines='" + txtshippinglines.Text + "',CY='" + txtcy.Text + "',EIRNo='" + txteirno.Text + "',Remarks='" + txtremakrs.Text + "',Status='DELIVERED' WHERE TripID='" + txttripid.Text + "' ", "Successfully Updated!", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            }
            else
            {
                return;
            }
        }
    }
}