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

namespace SalesInventorySystem.POS
{
    public partial class AddDiscountRestaurant : DevExpress.XtraEditors.XtraForm
    {
        public static string controlno, id, name, discountamount, remarks, pwdidno, pwdname, pwddiscountamount;
        public static bool isdone = false, isOnetimeDiscount = false, isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false;
        string percentamt = "0";
        double totaldiscount = 0.0;
        public static string discounttype = "";
        public AddDiscountRestaurant()
        {
            InitializeComponent();
        }

        private void btnPwdDiscount_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtnoofpax.Text) || String.IsNullOrEmpty(txtnoofsenior.Text))
            {
                XtraMessageBox.Show("Fields must not empty");
                return;
            }
            else if(Convert.ToDouble(txtnoofsenior.Text) > Convert.ToDouble(txtnoofpax.Text))
            {
                XtraMessageBox.Show("Number of Senior/s or PWD must not greater than Total Number of PAX");
                return;
            }
            else
            {
                radchanged();
            }
              
        }

       
        private void AddDiscountRestaurant_Load(object sender, EventArgs e)
        {
            bool isspwdiscreadonly = Database.checkifExist("SELECT isOverride FROM dbo.POSFunctions WHERE isOverride=1 and FunctionName='PWDDISCOUNTFIELD' ");
            if (isspwdiscreadonly) { txtpercentage.Enabled = true; } else { txtpercentage.Enabled = false; }
            btnshowdiscounteditems.Visible = true;
           
        }

        void radchanged()
        {
            //gi filter ang tanan product items nga naay discount
            string getDiscountedItems = Database.getSingleResultSet("SELECT dbo.func_getTotalAmountDiscountedItems('" + Login.assignedBranch + "','" + txtorderno.Text + "')");
            
            //NOT USED
            string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + PointOfSale.refno + "')");

            double netofvat=0.0,lessdisc=0.0,netvatlessdisc=0.0, totalamountdue = 0.0;
            //double totalamountperpax = 0.0, regularsale = 0.0, seniorsale = 0.0;
            double vatablesale = 0.0, vatsale = 0.0,vatexemptsale=0.0,vatdiscount=0.0;
            double shareEachPax = 0.0,shareofNonSC=0.0,netofvatEachSC=0.0,percent=0.0,discountedamount=0.0;

            shareEachPax = Convert.ToDouble(getDiscountedItems) / Convert.ToDouble(txtnoofpax.Text);
            shareofNonSC = shareEachPax * (Convert.ToDouble(txtnoofpax.Text) - Convert.ToDouble(txtnoofsenior.Text));
            netofvatEachSC = Math.Round((shareEachPax / 1.12) * Convert.ToDouble(txtnoofsenior.Text),2); //netofvat
            vatdiscount = Math.Round(netofvatEachSC * 0.12,2); //lessvat

            percent = Convert.ToDouble(txtpercentage.Text) / 100;

            lessdisc = Math.Round((netofvatEachSC * percent),2); //less sc
            netvatlessdisc = (netofvatEachSC - lessdisc); //net of scdisc
            //ADDVAT = netvatlessdisc*.12


            //netofvat = totalamountperpax / 1.12;
            //lessdisc = netofvat * (Convert.ToDouble(txtpwdpercent.Text) / 100);
            //netvatlessdisc = netofvat - lessdisc;

            //regularsale = totalamountperpax * (Convert.ToDouble(txtnoofpax.Text) - Convert.ToDouble(txtnoofsenior.Text));
            //regularsale = totalamountperpax * Convert.ToDouble(txtnoofpax.Text);//( - Convert.ToDouble(txtnoofsenior.Text));
            //seniorsale = netvatlessdisc;

            //totalamountdue = regularsale + seniorsale;
            totalamountdue = shareofNonSC + netvatlessdisc;

            vatablesale = shareofNonSC;//regularsale / 1.12;
            vatsale = vatdiscount;// vatablesale * 0.12;
            vatexemptsale = netofvatEachSC;


            if (String.IsNullOrEmpty(getDiscountedItems))
            {
                getDiscountedItems = "0";
            }

            if(Convert.ToDouble(txtnoofsenior.Text)>Convert.ToDouble(txtnoofpax.Text))
            {
                XtraMessageBox.Show("No of Discount must not greater than No. of Pax");
                return;
            }

            discountedamount = lessdisc + vatdiscount;

            txtamounttodiscount.Text = netofvatEachSC.ToString();
            txtpercentage.Text = "20";  //DEFAULT 5% DISCOUNT
            txtdiscountedamount.Text = lessdisc.ToString();
            txtvatdiscount.Text = vatdiscount.ToString();
                                        //getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + Convert.ToDouble(txtpercentageamount.Text) / 100 + "')");
            txtvatadj.Text = "0";//getVatAdjustment;
            txtvatexadj.Text = "0";// getVATExAdj;

            txtidno.Focus();
            panelpwd.Visible = true;
            //txtamnttobediscount.Text = pwdAndSeniorDiscountAmount().ToString();
            btnshowdiscounteditems.Visible = true;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            percentamt = "0";
            isOnetimeDiscount = true;

            if (radioButton1.Checked == true)  //radiobutton1 SENIOR
            {
                discounttype = "SENIOR";
                isSeniorDiscount = true;
                isPwdDiscount = false;
            }
            else if (radioButton2.Checked == true) //radiobutton2 PWD
            {
                discounttype = "PWD";
                isSeniorDiscount = false;
                isPwdDiscount = true;
            }

            if (String.IsNullOrEmpty(txtname.Text) || String.IsNullOrEmpty(txtnoofsenior.Text) || String.IsNullOrEmpty(txtnoofpax.Text) || String.IsNullOrEmpty(txtdiscountedamount.Text) || String.IsNullOrEmpty(txtpercentage.Text))
            {
                XtraMessageBox.Show("Fields Mandatory");
                return;
            }
            else
            {
                percentamt = txtpercentage.Text;
                id = txtidno.Text;
                totaldiscount = Convert.ToDouble(txtdiscountedamount.Text) + Convert.ToDouble(txtvatdiscount.Text);
                discountamount = totaldiscount.ToString(); //public and static

                name = txtname.Text;
                remarks = "";
                isSeniorDiscount = true;
                isPwdDiscount = true;
            }
            spDiscount();
            
            Database.ExecuteQuery("INSERT INTO dbo.POSTransaction VALUES ('" + Login.assignedBranch + "'" +
                ", '" + txttransactionno.Text + "' " +
                ", '" + Environment.MachineName.ToString() + "' " +
                ", '" + discounttype + "' " +
                ", '" + DateTime.Now.ToShortDateString() + "' " +
                ", '" + Login.isglobalUserID + "' " +
                ", 0" +
                ", 0)");

            isdone = true;
            this.Close();
        }

        void spDiscount()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_POSDiscount";
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.Parameters.AddWithValue("@parmtransno", txttransactionno.Text);
            com.Parameters.AddWithValue("@parmorderno", txtorderno.Text);
            com.Parameters.AddWithValue("@parmcashiertransno", txtcashiertansno.Text);
            com.Parameters.AddWithValue("@parmdisctype", discounttype);
            com.Parameters.AddWithValue("@parmdiscamount", discountamount);
            com.Parameters.AddWithValue("@parmvatadj", txtvatadj.Text);
            com.Parameters.AddWithValue("@parmname", name);
            com.Parameters.AddWithValue("@parmid", id);
            com.Parameters.AddWithValue("@parmremarks", remarks);
            com.Parameters.AddWithValue("@parmuserid", Login.isglobalUserID);
            com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
            com.Parameters.AddWithValue("@parmdateadded", DateTime.Today.ToShortDateString());
            com.Parameters.AddWithValue("@parmerrorcorrect", 0);
            com.Parameters.AddWithValue("@parmvatexadj", txtvatexadj.Text);
            com.Parameters.AddWithValue("@parmdiscpercentage", percentamt); //to be udpate paramaeter
            //com.Parameters.AddWithValue("@parmvatablesalesdiscamt","");
            //com.Parameters.AddWithValue("@parmvatdiscamt","");
            //com.Parameters.AddWithValue("@parmvatexdiscamt","");
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();

        }
    }
}