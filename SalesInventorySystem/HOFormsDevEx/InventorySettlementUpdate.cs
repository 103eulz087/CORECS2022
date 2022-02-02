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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventorySettlementUpdate : DevExpress.XtraEditors.XtraForm
    {
        public InventorySettlementUpdate()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE DeliverySummary SET isSettled='1' WHERE DeliveryNo='"+txtdevno.Text+"' and PONumber='"+txtpono.Text+"'");
            Database.ExecuteQuery("UPDATE DeliveryDetails SET isSettled='1' WHERE DeliveryNo='" + txtdevno.Text + "' and PONumber='" + txtpono.Text + "'");
            Database.ExecuteQuery("INSERT into InventoryVarianceSettlement VALUES('"+Login.assignedBranch+"','"+InventorySettlementDevEx.brcode+"','"+txtpono.Text+"','"+txtdevno.Text+"',0,0,0,0,'"+DateTime.Now.ToShortDateString()+"','"+Login.Fullname+"','"+txtremarks.Text+"')","Successfully Updated!");
            this.Dispose();
        }
    }
}