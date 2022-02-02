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
using Microsoft.Win32;

namespace SalesInventorySystem
{
    public partial class LoginDevEx : DevExpress.XtraEditors.XtraForm
    {
        public static string assignedBranch, Fullname, isMaker, isChecker, isglobalPOS, isglobalAccounting, iscashBegin, isglobalUserID, isglobalAdmin, isglobalOfficer, isglobalBranchOfficer, isglobalWarehouseOfficer, isCashier, isglobalApprover, glacctcode, cashinlimit, cashendlimit;
        //RegistryKey regkey;
        //string password;
        //string encryptedpassword;
        //string decryptedpassword;
        //string strmenuInventory;
        //String constr;
        public static string userid;
        public static string serverpassword;
        public static string servername;
        public static string dbname;
        public static string connsettings;

        string company = Database.getSingleQuery("CompanyProfile", "CompanyName <> ''", "CompanyName");
        public LoginDevEx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}