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

namespace SalesInventorySystem.Orders
{
    public partial class OrderCheckerDevEx : DevExpress.XtraEditors.XtraForm
    {
        public OrderCheckerDevEx()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
        
            return functionReturnValue;
        }
        private void OrderCheckerDevEx_Load(object sender, EventArgs e)
        {

        }
    }
}