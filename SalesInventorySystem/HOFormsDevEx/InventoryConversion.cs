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
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class InventoryConversion : DevExpress.XtraEditors.XtraForm
    {
        public InventoryConversion()
        {
            InitializeComponent();
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            //double result = 0.0;
            //if (e.Column.FieldName == "QtyToConvert")
            //{
            //    string a = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();
            //    string b = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "QtyToConvert").ToString();
               
            //    result = Convert.ToDouble(a) * Convert.ToDouble(b);

            //}
            //gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "TotalConversion",result.ToString());
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            double result = 0.0;
            if (e.Column.FieldName == "QtyToConvert")
            {
                string a = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Quantity").ToString();
                string b = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "QtyToConvert").ToString();
                string c = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Available").ToString();

                if(Convert.ToDouble(b) > Convert.ToDouble(c))
                {
                    XtraMessageBox.Show("Conversion Qty must not Greater than Available Quantity");
                    return;
                }

                result = Convert.ToDouble(a) * Convert.ToDouble(b);
                gridView1.SetRowCellValue(0, "TotalConversion", result.ToString());

            }
            
        }

        void spConvert()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sp_FiFoConvert";
                SqlCommand com = new SqlCommand(query, con);
               
                com.Parameters.AddWithValue("@parmtransdate", txtdate.Text);
                com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
                com.Parameters.AddWithValue("@parmprodcode", gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ParentProductCode").ToString());
                com.Parameters.AddWithValue("@parmchildprodcode", gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ChildProductCode").ToString());
                com.Parameters.AddWithValue("@parmqty", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "QtyToConvert").ToString());
                com.Parameters.AddWithValue("@parmnewqty", gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TotalConversion").ToString());
                com.Parameters.Add("@parmcost", SqlDbType.Decimal, 12).Direction = ParameterDirection.Output;
                com.Parameters["@parmcost"].Precision = 12;
                com.Parameters["@parmcost"].Scale = 2;
                //com.Parameters.AddWithValue("@parmcost", "0"); 
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.CommandTimeout = 180;
                com.ExecuteNonQuery();
                txtcost.Text= com.Parameters["@parmcost"].Value.ToString();
                XtraMessageBox.Show("Successfully Converted");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            spConvert();
        }
    }
}