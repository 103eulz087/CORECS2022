using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Branches
{
    public partial class BranchGenInventory : Form
    {
        object brcode = null;
        public BranchGenInventory()
        {
            InitializeComponent();
        }

        private void BranchGenInventory_Load(object sender, EventArgs e)
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches ORDER BY BranchCode", searchLookUpEdit1, "BranchCode", "BranchCode");
        }

        private void display()
        {
            gridControl1.BeginUpdate();
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            try
            {
                if (radioButton1.Checked == true) //DETAILED
                {
                    Database.display("SELECT * FROM view_BranchInventoryDetails WHERE BranchCode='" + brcode + "'", gridControl1, gridView1);

                }
                else if (radioButton2.Checked == true) //SUMMARY
                {
                    Database.display("SELECT * FROM view_BranchInventory WHERE BranchCode='" + brcode + "'", gridControl1, gridView1);
                }
            }
            catch (SqlException sex2)
            {
                XtraMessageBox.Show(sex2.Message.ToString());
            }
            finally
            {
                gridControl1.EndUpdate();
            }
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            brcode = SearchLookUpClass.getSingleValue(searchLookUpEdit1, "BranchCode");
        }

      
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            display();
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "Available")
            {
                e.Appearance.BackColor = Color.LightBlue;
                e.Appearance.BackColor2 = Color.LightCyan;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (radioButton2.Checked == true)
            {
                if (e.RowHandle >= 0)
                {
                    string available = view.GetRowCellDisplayText(e.RowHandle, view.Columns["Available"]);
                    string reorderlevel = view.GetRowCellDisplayText(e.RowHandle, view.Columns["ReOrderLevel"]);
                    if (Convert.ToDouble(available) < Convert.ToDouble(reorderlevel))
                    {
                        e.Appearance.BackColor = Color.Salmon;
                        e.Appearance.BackColor2 = Color.SeaShell;
                    }
                }
            }
        }

        private void btnforapprovalsalesorderexcel_Click(object sender, EventArgs e)
        {
            string filename = "BRANCHINVENTORY_"+searchLookUpEdit1.Text+ DateTime.Now.ToShortDateString().Replace(@"/", "-");
            HelperFunction.exporttoexcel(gridView1, filename);
        }
    }
}
