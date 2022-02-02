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

namespace SalesInventorySystem.POS
{
    public partial class POSScreenMirror : Form
    {
        string amount = "";
        private int xpos = 0, ypos = 0;
        public string mode = "Left-to-Right";
        public POSScreenMirror()
        {
            InitializeComponent();
            MydataGridView1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private void POSScreenMirror_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

            if (!backgroundWorker1.IsBusy)
                backgroundWorker1.RunWorkerAsync();
        }
        Double computeTotalAmount()
        {
            double total = 0.0;
            for (int i = 0; i <= MydataGridView1.RowCount - 1; i++)
            {
                amount = MydataGridView1.Rows[i].Cells["Amount"].Value.ToString();
                total += Convert.ToDouble(amount);
            }
            return total;
        }
        void doThreadedStuff()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(
                    () =>
                    {
                        display();
                    }
                ));
            }
            else
            {
                display();
            }
        }

        void display()
        {
            Database.displayLocalGrid("SELECT SequenceNumber AS ID,Description AS Particulars,FORMAT(SellingPrice,'N', 'en-us') AS UnitPrice,QtySold AS Qty,FORMAT(DiscountTotal,'N', 'en-us') AS Discount,FORMAT(TotalAmount,'N', 'en-us') AS Amount,isVat FROM BatchSalesDetails WHERE ReferenceNo='" + PointOfSale.refno + "' AND isVoid='0' AND isCancelled='0' and isHold='0' AND BranchCode='" + Login.assignedBranch + "'", MydataGridView1);
            double amounttendered=0.0;
            double amountchange = 0.0;
            amounttendered = Database.getSingleAmountQuery("BatchSalesSummary", "ReferenceNo='" + PointOfSale.refno + "'", "AmountTendered");
            amountchange = Database.getSingleAmountQuery("BatchSalesSummary", "ReferenceNo='" + PointOfSale.refno + "'", "AmountChange");
            MydataGridView1.Columns["ID"].Visible = false; //localgridview
            MydataGridView1.Columns["Discount"].Visible = false;
            MydataGridView1.Columns["isVat"].Visible = false;
            label2.Text = HelperFunction.numericFormat(computeTotalAmount());
            POSHistoryCaption poscap = new POSHistoryCaption();
            lbltenderedamount.Text = HelperFunction.numericFormat(amounttendered);
            lblamtchange.Text = HelperFunction.numericFormat(amountchange);
            int ctr = Convert.ToInt32(Database.getSingleQuery("GridCtr", "Counter <> ''", "Counter"));
            if (MydataGridView1.RowCount > ctr)
            {

            }
            else
            {
                return;
            }
            update();
        }
        void update()
        {
            Database.ExecuteQuery("UPDATE GridCtr SET Counter = '" + MydataGridView1.RowCount + "'");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
            }
            System.Threading.Thread.Sleep(1);
            doThreadedStuff();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void lbltenderedamount_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (mode == "Left-to-Right")
            {
                if (panel1.Width == xpos)
                {
                    label3.Location = new System.Drawing.Point(0, ypos);
                    xpos = 0;

                }
                else
                {
                    label3.Location = new System.Drawing.Point(xpos, ypos);
                    xpos += 2;
                }
            }
            else if (mode == "Right-to-Left")
            {
                if (xpos == 0)
                {
                    label3.Location = new System.Drawing.Point(panel1.Width, ypos);
                    xpos = this.Width;
                }
                else
                {
                    label3.Location = new System.Drawing.Point(xpos, ypos);
                    xpos -= 2;
                }
            }
        }
    }
}
