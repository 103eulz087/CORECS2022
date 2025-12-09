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
    public partial class POSAddDiscount : DevExpress.XtraEditors.XtraForm
    {
        public static string controlno, id, name, discountamount;
        //public static bool isdone = false, isOnetimeDiscount = false, isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false, isNacDiscount = false, isMovDiscount = false;
       
        public static bool isdone = false, isOnetimeDiscount = false;
        object discid = null;
        public static string discounttype = "";
        private void btnSave_Click(object sender, EventArgs e)
        {
            percentamt = "0";
            isOnetimeDiscount = true;

            if (String.IsNullOrEmpty(txtcontrolno.Text) || String.IsNullOrEmpty(txtdiscountamount.Text) || String.IsNullOrEmpty(txtpercentageamount.Text))
            {
                XtraMessageBox.Show("Fields Mandatory");
                return;
            }
            else
            {
                percentamt = txtpercentageamount.Text;
                id = txtcontrolno.Text;
                discountamount = txtdiscountamount.Text;
                name = txtname.Text;
                //remarks = "";
            }
            spDiscount();


            //Database.ExecuteQuery(
            //    "INSERT INTO dbo.POSTransaction VALUES (" +
            //    "'" + Login.assignedBranch + "', " +
            //    "'" + txttransactionno.Text + "', " +
            //    "'" + Environment.MachineName.ToString() + "', " +
            //    "'" + "Sales Discount" + "', " +
            //    "'" + DateTime.Now.ToShortDateString() + "', " +
            //    "'" + Login.isglobalUserID + "', " +
            //    "0, " +
            //    "0, " +
            //    "'" + "Discount Applied. OR# " + txtorderno.Text + " " + txtdiscountypes.Text +
            //    " Discount of " + discountamount + " applied by Cashier." + "'" +
            //    "'','','',''"+
            //    ")"
            //);

            discounttype = txtdiscountypes.Text;
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
            com.Parameters.AddWithValue("@parmdisctype", txtdiscountypes.Text);
            com.Parameters.AddWithValue("@parmdiscamount", discountamount);
            com.Parameters.AddWithValue("@parmvatadj", txtvatadj.Text);
            com.Parameters.AddWithValue("@parmname", name);
            com.Parameters.AddWithValue("@parmid", id);
            com.Parameters.AddWithValue("@parmremarks", "");
            com.Parameters.AddWithValue("@parmuserid", Login.isglobalUserID);
            com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
            com.Parameters.AddWithValue("@parmdateadded", DateTime.Today.ToShortDateString());
            com.Parameters.AddWithValue("@parmerrorcorrect", 0);
            com.Parameters.AddWithValue("@parmvatexadj", txtvatexadj.Text);
            com.Parameters.AddWithValue("@parmdiscpercentage", percentamt); //to be udpate paramaeter
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
        }

        private void txtdiscountypes_EditValueChanged(object sender, EventArgs e)
        {
            discid = SearchLookUpClass.getSingleValue(txtdiscountypes, "DiscountID");
        }

        private void btnshowdiscounteditems_Click(object sender, EventArgs e)
        {

            POS.POSShowDiscountedItems posdite = new POS.POSShowDiscountedItems();
            Database.display("SELECT a.ProductCode " +
                ",a.Description " +
                ",a.QtySold as Qty " +
                ",a.SellingPrice as UnitPrice " +
                ",a.TotalAmount " +
                ",a.isVat  " +
                ",case when a.isVat=1 then CAST(ROUND(a.TotalAmount/1.12,2) as decimal(12,2)) else 0 end as NetOfVat   " +
                ",case when a.isVat=1 then CAST(ROUND(a.TotalAmount/1.12*0.12,2) as decimal(12,2)) else 0 end as LessVat " +
                ",case when a.isVat=1 then CAST(ROUND(a.TotalAmount/1.12*0.05,2) as decimal(12,2)) else CAST(a.TotalAmount*0.05 as decimal(12,2)) end as LessDiscount " +
                ",case when a.isVat=1 then CAST(ROUND((a.TotalAmount/1.12*0.05)*0.12,2) as decimal(12,2)) else 0 end as VatAdj " +
                ",case when a.isVat=1 then CAST(ROUND((a.TotalAmount/1.12)-(a.TotalAmount/1.12*0.05),2) as decimal(12,2)) else CAST(a.TotalAmount-(a.TotalAmount*0.05) as decimal(12,2)) end as NetDiscount " +
                ",case when a.isVat=1 then CAST(ROUND(((a.TotalAmount/1.12*0.12)-(a.TotalAmount/1.12*0.05))*0.12,2) as decimal(12,2)) else 0 end as AddVat " +
                ",case when a.isVat=0 then a.TotalAmount else 0 end as VatExemptSales  " +
                //",case when b.isPrice5=1 AND a.isVat=0 then CAST(a.TotalAmount-(a.TotalAmount*0.05) as decimal(12,2)) when b.isPrice5=0 AND a.isVat=0 then CAST(a.TotalAmount as decimal(12,2)) else 0 end as VATExemptADJ "+
                ",case when b.isDiscount=1 AND a.isVat=0 then CAST(a.TotalAmount-(a.TotalAmount*0.05) as decimal(12,2)) when b.isDiscount=0 AND a.isVat=0 then CAST(a.TotalAmount as decimal(12,2)) else 0 end as VATExemptADJ " +
                ",b.isDiscount as isDiscounted  " +
                "FROM BatchSalesDetails as a  " +
                "INNER JOIN Products as b  " +
                "ON a.ProductCode=b.ProductCode  " +
                "AND a.BranchCode=b.BranchCode  " +
                "WHERE a.isCancelled=0  " +
                "and a.isVoid=0    " +
                "and a.ReferenceNo='" + PointOfSale.refno + "' " +
                "and b.BranchCode='" + Login.assignedBranch + "' ", posdite.gridControl1, posdite.gridView1);
            posdite.ShowDialog(this);
        }

        string percentamt = "0";
        public POSAddDiscount()
        {
            InitializeComponent();
        }

        private void POSAddDiscount_Load(object sender, EventArgs e)
        {
            populate();
            Database.displaySearchlookupEdit("SELECT DiscountID,DiscountName FROM dbo.DiscountType", txtdiscountypes, "DiscountName","DiscountName");
        }

        void populate()
        {
            string getDiscountedItems = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItems('" + Login.assignedBranch + "','" + PointOfSale.refno + "')");
            //DEFAULT 5% DISCOUNT
            string getVatAdjustment = "0";
            string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + PointOfSale.refno + "')");
            if (String.IsNullOrEmpty(getDiscountedItems))
            {
                getDiscountedItems = "0";
            }
            else
            {

                txtamnttobediscount.Text = getDiscountedItems;
                txtpercentageamount.Text = "5";  //DEFAULT 5% DISCOUNT
                getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + Convert.ToDouble(txtpercentageamount.Text) / 100 + "')");
                txtvatadj.Text = getVatAdjustment;
                txtvatexadj.Text = getVATExAdj;

                btnshowdiscounteditems.Visible = true;
            }
        }

        private void txtpercentageamount_TextChanged(object sender, EventArgs e)
        {
            double discountedamount = 0.0;
            double percentageAmount = Convert.ToDouble(txtpercentageamount.Text) / 100;
            string getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + percentageAmount + "')");
            discountedamount = Math.Round(Convert.ToDouble(txtamnttobediscount.Text) * percentageAmount, 2);
            txtdiscountamount.Text = discountedamount.ToString();
            txtvatadj.Text = getVatAdjustment;

            string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + Environment.MachineName + "','" + percentageAmount + "')");
            txtvatexadj.Text = getVATExAdj;
        }

        private void txtamnttobediscount_TextChanged(object sender, EventArgs e)
        {
            double discountamount = 0.0;
            discountamount = Convert.ToDouble(txtamnttobediscount.Text) * 0.05;
            txtdiscountamount.Text = discountamount.ToString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure All Data are inputted correcty?", "Add Discount");
            if (ok)
            {
                if (String.IsNullOrEmpty(txtcontrolno.Text) || String.IsNullOrEmpty(txtdiscountamount.Text))
                {
                    XtraMessageBox.Show("Fields Mandatory");
                    return;
                }
                //values that need to be carry after submitting
                controlno = txtcontrolno.Text;
                name = txtname.Text;
                discountamount = txtdiscountamount.Text;

                isOnetimeDiscount = true;
                isdone = true;
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}