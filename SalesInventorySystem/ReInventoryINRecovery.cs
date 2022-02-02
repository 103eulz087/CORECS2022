using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    public partial class ReInventoryINRecovery : Form
    {
        public static string id = "";
        public static bool isdone = false;
        public ReInventoryINRecovery()
        {
            InitializeComponent();
        }

        private void ReInventoryINRecovery_Load(object sender, EventArgs e)
        {
          
        }

        private void txtid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                id = txtid.Text;
                isdone = true;
                this.Close();
            }
                
        }
    }
}
