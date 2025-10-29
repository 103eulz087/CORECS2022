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
using DevExpress.XtraReports.UI;
using System.Net.WebSockets;
using System.Threading;

namespace SalesInventorySystem.Sabong
{
    public partial class SBDashboard : DevExpress.XtraEditors.XtraForm
    {

        private Classes.WebSocketDashboardClient dashboardClient;
        ClientWebSocket ws = new ClientWebSocket();
        //string updateSignal = "";
        string referenceNo = ""; string refno = "";
        public SBDashboard()
        {
            InitializeComponent();
        }


        private async void ConnectWebSocket()
        {
            await ws.ConnectAsync(new Uri("ws://127.0.0.1:7990/ws/"), CancellationToken.None);
            ListenForUpdates();
        }

        private async void SendUpdate(string message)
        {
            //string updateSignal = "update";
            var buffer = Encoding.UTF8.GetBytes(message);
            await ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

            //var buffer = Encoding.UTF8.GetBytes(message);
            //await ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);


            //string updateSignal = "update"; // simple trigger message
            //await ws.SendAsync(Encoding.UTF8.GetBytes(updateSignal), WebSocketMessageType.Text, true, CancellationToken.None);

        }


        private void SBDashboard_Load(object sender, EventArgs e)
        {

            ConnectWebSocket();

            //get fightid and fightno in GameEvents WHERE Status = Open
            var rowz = Database.getMultipleQuery("SELECT FightID,FightNo FROM dbo.GameEventDetails WHERE Status=1", "FightID,FightNo");
            string FightID, FightNo, MachineID;
            MachineID = Database.getSingleQuery($"SELECT MachineID,MachineName FROM dbo.GameMachines WHERE MachineName='{Environment.MachineName}'", "MachineID");
            FightID = rowz["FightID"].ToString();
            FightNo = rowz["FightNo"].ToString();
            lblfightid.Text = FightID;
            lblfightno.Text = FightNo;
            lblmachineid.Text = MachineID;
            string cashierid = IDGenerator.getIDNumberSP("sb_GetCashierID", "CashierID");
            lblTransactionIDCashier.Text = cashierid;

        }

        private void btnmeron_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtbet.Text))
            {
                XtraMessageBox.Show("Please Input Bet Amount");
                return;
            }
            else
            {
                lblside.Text = "MERON";
                lblamount.Text = txtbet.Text;
            }
            
        }

        private void btndraw_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtbet.Text))
            {
                XtraMessageBox.Show("Please Input Bet Amount");
                return;
            }
            else
            {
                lblside.Text = "DRAW";
                lblamount.Text = txtbet.Text;
            }
        }

        private void btnwala_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtbet.Text))
            {
                XtraMessageBox.Show("Please Input Bet Amount");
                return;
            }
            else
            {
                lblside.Text = "WALA";
                lblamount.Text = txtbet.Text;
            }
        }

        private void btnpostbet_Click(object sender, EventArgs e)
        {

            

            referenceNo = IDGenerator.getIDNumberSP("sb_GetReferenceNumber", "ReferenceNumber");
            refno = HelperFunction.sequencePadding1(referenceNo, 10);
            int side = 0;
            if(lblside.Text.Trim() == "MERON")
            {
                side = 1;
            }else if(lblside.Text.Trim() == "WALA")
            {
                side = 2;
            }
            else if(lblside.Text.Trim() == "DRAW")
            {
                side = 3;
            }
            else { side = 1; }
            addBet(refno,side); 
            printBarcode(refno);
            display();
            clear();

            
        }

        void printBarcode(string refno)
        {
            DevExReportTemplate.SabongBetReceipt bprint = new DevExReportTemplate.SabongBetReceipt();
            //Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.xrlbldatetime.Text = DateTime.Now.ToString();
            bprint.xrlblfightno.Text = lblfightno.Text;
            bprint.xrlblside.Text = lblside.Text;
            bprint.xrlblamount.Text = lblamount.Text;
            bprint.xrBarCode1.Text = refno.Trim(); //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.ShowRibbonPreviewDialog();
            //report.Print();
        }

        void clear()
        {
            lblside.Text = "";
            lblamount.Text = "";
            txtbet.Text = "";
            txtbet.Focus();
        }

        void display()
        {
            double totalmeronbet = Database.getTotalSummation2("GameSummary", "BranchCode = '" + Login.assignedBranch + "' and FightID = '" + lblfightid.Text + "' and FightNo='" + lblfightno.Text + "'", "TotalBetAmountMeron");
            double totaldrawbet = Database.getTotalSummation2("GameSummary", "BranchCode = '" + Login.assignedBranch + "' and FightID = '" + lblfightid.Text + "' and FightNo='" + lblfightno.Text + "'", "TotalBetAmountDraw");
            double totalwalabet = Database.getTotalSummation2("GameSummary", "BranchCode = '" + Login.assignedBranch + "' and FightID = '" + lblfightid.Text + "' and FightNo='" + lblfightno.Text + "'", "TotalBetAmountWala");
            txtmeronbet.Text = totalmeronbet.ToString();
            txtdrawbet.Text = totaldrawbet.ToString();
            txtwalabet.Text = totalwalabet.ToString();

            Database.display("SELECT ReferenceNo,Side,Amount " +
                "FROM dbo.sbview_GameDetails WHERE BranchCode = '" + Login.assignedBranch+ "' and FightID ='" + lblfightid.Text + "' and FightNo='" + lblfightno.Text+ "' and CashierTransNo='"+lblTransactionIDCashier.Text+"' AND MachineID='"+lblmachineid.Text+"' ", gridControl1, gridView2);
        }
        async void addBet(string refno,int side)
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "sb_AddBet";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@brcode", Login.assignedBranch);
                com.Parameters.AddWithValue("@fightid", lblfightid.Text.Trim());
                com.Parameters.AddWithValue("@fightno", lblfightno.Text);
                com.Parameters.AddWithValue("@cashiertransno", lblTransactionIDCashier.Text);
                com.Parameters.AddWithValue("@machineid", lblmachineid.Text);
                com.Parameters.AddWithValue("@referenceno", refno);
                com.Parameters.AddWithValue("@side", side);
                com.Parameters.AddWithValue("@amount", lblamount.Text);
                com.Parameters.AddWithValue("@addedby", Login.userid);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

            // Send WebSocket update signal
            string updateSignal = "update";
            var buffer = Encoding.UTF8.GetBytes(updateSignal);
            await ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);


        }


        private void UpdateDashboardFromDatabase()
        {
            SqlConnection conn = Database.getConnection();
            
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT SUM(Amount) FROM dbo.GameDetails with(nolock) WHERE FightID='"+lblfightid.Text+"'", conn);
                decimal totalAmount = (decimal)cmd.ExecuteScalar();
                
                lbltotal.Text = totalAmount.ToString("C");
            
        }

        private async void ListenForUpdates()
        {
            var buffer = new byte[1024];

            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                if (message == "update")
                {
                    UpdateDashboardFromDatabase();
                }
            }
        }


        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ConnectWebSocket();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Sabong.Trends sbtrends = new Trends();
            sbtrends.Show();
        }
    }
}