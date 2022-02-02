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

namespace SalesInventorySystem.Branches
{
    public partial class InventoryBranchQtyAdj : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public InventoryBranchQtyAdj()
        {
            InitializeComponent();
        }

        void addInv()
        {
            string option = "";
            if(radadd.Checked == true)
            {
                option = "ADD";
            }
            else
            {
                option = "DEDUCT";
            }
            string query = "sp_InvBranchQtyAdj";
            SqlConnection con = Database.getConnection();
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmbranchcode",Login.assignedBranch);
                com.Parameters.AddWithValue("@parmprodcode",txtprodcode.Text);
                com.Parameters.AddWithValue("@parmdesc",txtdesc.Text);
                com.Parameters.AddWithValue("@parmqty",txtqtyadj.Text);
                com.Parameters.AddWithValue("@parmoption",option);
                com.Parameters.AddWithValue("@parmaddedby",Login.Fullname);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
                isdone = true;
                this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (radadd.Checked == false && raddeduct.Checked == false)
            {
                XtraMessageBox.Show("Must Select Inventory Adjustment Method");
                return;
            }
            else
            {
                addInv();
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}