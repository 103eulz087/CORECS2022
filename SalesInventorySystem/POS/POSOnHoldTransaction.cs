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

namespace SalesInventorySystem
{
    public partial class POSOnHoldTransaction : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public POSOnHoldTransaction()
        {
            InitializeComponent();
        }


        void HoldTransaction()
        {

        }

        private void btnonhold_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtcustname.Text))
            {
                XtraMessageBox.Show("Please Input CustomerName Field");
                return;
            }
            else
            {
                //generate transactionnumber
                int refnumber = IDGenerator.getIDNumber("POSTransaction", "BranchCode='" + Login.assignedBranch + "' AND MachineUsed='"+Environment.MachineName+"' ", "TransactionNo", 1);
                string transactionno = HelperFunction.sequencePadding1(refnumber.ToString(), 10);
                //insert into POSTransaction
                Database.ExecuteQuery($"INSERT INTO dbo.POSTransaction VALUES ('{Login.assignedBranch}'" +
                    $",'{transactionno}'" +
                    $",'{Environment.MachineName}'" +
                    $",'HOLD TRAN','{DateTime.Now.ToString()}','{Login.isglobalUserID}','0','0')");
                Database.ExecuteQuery($"UPDATE dbo.BatchSalesSummary set isHold=1,OnHoldName='{txtcustname.Text}'" +
                    $" WHERE ReferenceNo='{lblrefno.Text}' " +
                    $"AND BranchCode='{Login.assignedBranch}' " +
                    $"AND MachineUsed='{Environment.MachineName}'", "Successfully HOLD Transaction!..");
                isdone = true;
                this.Close();
            }
            
            //SqlConnection con = Database.getConnection();
            //con.Open();
            //try
            //{
            //    string query = "sp_OnHoldSalesInvoice";
            //    SqlCommand com = new SqlCommand(query, con);
            //    com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            //    com.Parameters.AddWithValue("@parmtransno", lbltranscode.Text);
            //    com.Parameters.AddWithValue("@parmorderno", lblrefno.Text);
            //    com.Parameters.AddWithValue("@parmcustname", txtcustname.Text);
            //    com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
            //    com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
            //    com.CommandType = CommandType.StoredProcedure;
            //    com.CommandText = query;
            //    com.ExecuteNonQuery();
                
            //}
            //catch(SqlException ex)
            //{
            //    XtraMessageBox.Show(ex.Message.ToString());
            //}
            //finally
            //{
            //    con.Close();
            //}
            //XtraMessageBox.Show("Transaction Successfully Put On-Hold!");
            //isdone = true;
            //this.Close();
           
        }

        private void POSOnHoldTransaction_Load(object sender, EventArgs e)
        {
            //Database.getSingleData("BatchSalesSummary", "ReferenceNo", PointOfSale._refno,"ReferenceNo");
         
        }
    }
}