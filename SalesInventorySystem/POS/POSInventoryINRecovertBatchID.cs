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
    public partial class POSInventoryINRecovertBatchID : Form
    {
        public static bool isdone = false;
        public static string ID = "";
        public POSInventoryINRecovertBatchID()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ID = txtbatchid.Text;
            isdone = true;
            this.Close();
        }
    }
}
