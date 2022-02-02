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
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ViewInventoryItemsDevEx : DevExpress.XtraEditors.XtraForm
    {
        public ViewInventoryItemsDevEx()
        {
            InitializeComponent();
        }

        private void ViewInventoryItemsDevEx_Load(object sender, EventArgs e)
        {
           
        }

        private void txtproducts_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtproducts.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            //string fieldName = "Name"; // or other field name
            object value = view.GetRowCellValue(rowHandle, "Description");
            object cost = view.GetRowCellValue(rowHandle, "Cost");
            txtdesc.Text = value.ToString();
            txtcost.Text = cost.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            update();
            XtraMessageBox.Show("Succesfully Updated");
            this.Dispose();
        }
        void update()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "spu_UpdateCost";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmshipmentno", txtshipmentno.Text);
                com.Parameters.AddWithValue("@parmproductcode", txtproducts.Text);
                com.Parameters.AddWithValue("@parmcost", txtcost.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
           
        }
    }
}