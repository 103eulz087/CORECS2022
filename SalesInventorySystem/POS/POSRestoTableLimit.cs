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

namespace SalesInventorySystem.POS
{
    public partial class POSRestoTableLimit : DevExpress.XtraEditors.XtraForm
    {
        public POSRestoTableLimit()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery($"UPDATE dbo.RestaurantTable SET Limit='{txtlimitamount.Text}' WHERE TableNo='{txttableno.Text}'", "Successfully Updated");
            this.Dispose();
        }
    }
}