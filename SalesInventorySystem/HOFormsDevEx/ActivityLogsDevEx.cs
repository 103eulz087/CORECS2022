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
    public partial class ActivityLogsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public ActivityLogsDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM dbo.HistoryLogs WHERE CAST(DateExecute as date) BETWEEN '" + dateFrom.Text + "' AND '" + dateTo.Text + "' ", gridControl1, gridView1);
        }
    }
}