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
    public partial class POSUnUploadInv : DevExpress.XtraEditors.XtraForm
    {
        public static string invID = "";
        public static bool isdone = false;
        public POSUnUploadInv()
        {
            InitializeComponent();
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1,e.Location);
        }

        private void continueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            invID = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "ID").ToString();
            isdone = true;
            this.Close();
        }

        private void POSUnUploadInv_Load(object sender, EventArgs e)
        {

        }
    }
}