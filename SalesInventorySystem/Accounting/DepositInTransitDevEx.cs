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

namespace SalesInventorySystem.Accounting
{
    public partial class DepositInTransitDevEx : DevExpress.XtraEditors.XtraForm
    {
        public DepositInTransitDevEx()
        {
            InitializeComponent();
        }

        private void DepositInTransitDevEx_Load(object sender, EventArgs e)
        {
            populaterows();
        }

        void populaterows()
        {
            Database.displaySearchlookupEdit("SELECT * FROM view_GLAccounts", txtdebitglcode, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("SELECT * FROM view_GLAccounts", txtcreditglcode, "AccountCode", "AccountCode");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtamount.Text) || String.IsNullOrEmpty(txtcreditglcode.Text) || String.IsNullOrEmpty(txtdebitglcode.Text) || String.IsNullOrEmpty(txtremarks.Text))
            {
                XtraMessageBox.Show("You must filled out the form correctly");
            }
            else
            {
                add();
            }
        }

        void add()
        {
            string ticketnumber = IDGenerator.getIDNumberSP("sp_GetTicketNumber", "TicketNumber"); //IDGenerator.getTicketNumberSP();
            Database.ExecuteQuery("INSERT INTO TicketMaster VALUES('" + DateTime.Now.ToString() + "','0','" + Login.assignedBranch + "','" + Login.assignedBranch + "','" + ticketnumber + "','','DEPOSIT IN TRANSIT','Uncleared','" + txtremarks.Text + "','" + Login.Fullname + "','*','*','Updated','DepTrns','')");
            Database.ExecuteQuery("INSERT INTO TicketDetails VALUES('" + DateTime.Now.ToShortDateString() + "','0','" + Login.assignedBranch + "','" + Login.assignedBranch + "','" + ticketnumber + "','','" + txtdebitglcode.Text + "','"+txtamount.Text+"','','')");
            Database.ExecuteQuery("INSERT INTO TicketDetails VALUES('" + DateTime.Now.ToShortDateString() + "','0','" + Login.assignedBranch + "','" + Login.assignedBranch + "','" + ticketnumber + "','','" + txtcreditglcode.Text+ "','','"+txtamount.Text+"','')");
            Database.ExecuteQuery("INSERT INTO BankReconLedger VALUES ('" + DateTime.Now.ToShortDateString() + "','Cleared',0,'" + txtamount.Text + "','0','" + txtremarks.Text + "',0,0,'','" + ticketnumber + "')");
            XtraMessageBox.Show("Successfully Added");
            this.Dispose();
        }
    }
}