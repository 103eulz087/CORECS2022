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
    public partial class LockedUsers : DevExpress.XtraEditors.XtraForm
    {
        public LockedUsers()
        {
            InitializeComponent();
        }

        private void LockedUsers_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            Database.display("SELECT * FROM UsersLocked", gridControl1, gridView1);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1,e.Location);
        }

        private void reuploadThisTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery($"DELETE FROM UsersLocked WHERE UserID='{gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"UserID").ToString()}'","Successfully Deleted!...");
            display();
        }
    }
}