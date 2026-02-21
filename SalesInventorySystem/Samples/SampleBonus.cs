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

namespace SalesInventorySystem.Samples
{
    public partial class SampleBonus : DevExpress.XtraEditors.XtraForm
    {
        public SampleBonus()
        {
            InitializeComponent();
        }

        private void SampleBonus_Load(object sender, EventArgs e)
        {
            populate();
        }

        void populate()
        {
            Database.display("SELECT * FROM vw_SampleBonus ORDER BY Balance DESC", gridControlMaster, gridViewMaster);
        }

        private void gridViewMaster_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {

            if (e.Column.FieldName == "Send")
                // e.RepositoryItem = repositoryItemComboBox5;
                e.RepositoryItem = repositoryItemCheckEditStat;
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}