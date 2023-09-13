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
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;

namespace SalesInventorySystem
{
    public partial class AddDiscount : DevExpress.XtraEditors.XtraForm
    {
        public static string controlno,id,name,discountamount,remarks,pwdidno,pwdname,pwddiscountamount;
        public static bool isdone = false, isOnetimeDiscount = false, isSeniorDiscount = false, isPwdDiscount = false, isOthersDiscount = false;
        public static string discounttype = "";
        string percentamt = "0";
        public AddDiscount()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;

            if (keyData == Keys.F1)
            {
                radioButton1.Checked = true;
            }
            else if (keyData == Keys.F2)
            {
                radioButton2.Checked = true;
            }
            else if (keyData == Keys.F3)
            {
                radioButton3.Checked = true;
            }
            else if (keyData == Keys.Escape)
            {
                this.Dispose();
            }
            return functionReturnValue;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double discountedamount = 0.0;
            double percentageAmount = Convert.ToDouble(txtpercentageamount.Text) / 100;
            string getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + percentageAmount + "')");
            discountedamount = Math.Round(Convert.ToDouble(txtamnttobediscount.Text) * percentageAmount,2);
            txtdiscountamount.Text = discountedamount.ToString();
            txtvatadj.Text = getVatAdjustment;

            string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + PointOfSale.refno + "','"+Environment.MachineName+"','"+percentageAmount+"')");
            txtvatexadj.Text = getVATExAdj;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure All Data are inputted correcty?", "Add Discount");
            if(ok)
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
                isSeniorDiscount = true;
                isPwdDiscount = false;
                isOthersDiscount = false;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void txtamnttobediscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                simpleButton1.Focus();
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

        private void txtcontrolno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtname.Focus();
        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //simpleButton1.Focus();
                btnSave.Focus();
        }

        private void txtpwdidno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtpwdname.Focus();
        }

        private void txtpwdname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                //btnPwdDiscount.Focus();
                btnSave.Focus();
        }

        void vatAdjustment()
        {

        }
        static void StartOSK()
        {
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            string osk = null;

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "sysnative"), "osk.exe");
                if (!File.Exists(osk))
                {
                    osk = null;
                }
            }

            if (osk == null)
            {
                osk = Path.Combine(Path.Combine(windir, "system32"), "osk.exe");
                if (!File.Exists(osk))
                {
                    osk = null;
                }
            }

            if (osk == null)
            {
                osk = "osk.exe";
            }

            Process.Start(osk);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //StartOSK();
        }

        private void txtpwdamount_TextChanged(object sender, EventArgs e)
        {
            double discountedamount = 0.0;
            double percentageAmount = Convert.ToDouble(txtpwdpercent.Text) / 100;
            discountedamount = Math.Round(Convert.ToDouble(txtpwdamount.Text) * percentageAmount, 2);
            txtpwddiscountamount.Text = discountedamount.ToString();
        }

        void spDiscount()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "sp_POSDiscount";
            SqlCommand com = new SqlCommand(query,con);
            com.Parameters.AddWithValue("@parmbranchcode", Login.assignedBranch);
            com.Parameters.AddWithValue("@parmtransno", txttransactionno.Text);
            com.Parameters.AddWithValue("@parmorderno", txtorderno.Text);
            com.Parameters.AddWithValue("@parmcashiertransno", txtcashiertansno.Text);
            com.Parameters.AddWithValue("@parmdisctype", discounttype);
            com.Parameters.AddWithValue("@parmdiscamount", discountamount);
            com.Parameters.AddWithValue("@parmvatadj", txtvatadj.Text);
            com.Parameters.AddWithValue("@parmname", name);
            com.Parameters.AddWithValue("@parmid",id);
            com.Parameters.AddWithValue("@parmremarks", remarks);
            com.Parameters.AddWithValue("@parmuserid", Login.isglobalUserID);
            com.Parameters.AddWithValue("@parmmachinename", Environment.MachineName.ToString());
            com.Parameters.AddWithValue("@parmdateadded", DateTime.Today.ToShortDateString());
            com.Parameters.AddWithValue("@parmerrorcorrect",0);
            com.Parameters.AddWithValue("@parmvatexadj", txtvatexadj.Text);
            com.Parameters.AddWithValue("@parmdiscpercentage", percentamt); //to be udpate paramaeter
            //com.Parameters.AddWithValue("@parmvatablesalesdiscamt","");
            //com.Parameters.AddWithValue("@parmvatdiscamt","");
            //com.Parameters.AddWithValue("@parmvatexdiscamt","");
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            com.ExecuteNonQuery();
            con.Close();
            
            //Database.ExecuteQuery("INSERT INTO SalesDiscount VALUES ('" + Login.assignedBranch + "'" +
            //    ", '" + txttransactionno.Text + "'" +
            //    ", '" + txtorderno.Text + "'" +
            //    ", '" + txtcashiertansno.Text + "'" +
            //    ", '" + discounttype + "'" +
            //    ", '" + discountamount + "'" +
            //    ", '" + txtvatadj.Text + "'" +
            //    ", '" + name + "'" +
            //    ", '" + id + "'" +
            //    ", '" + remarks + "'" +
            //    ", '" + Login.isglobalUserID + "'" +
            //    ", '" + Environment.MachineName.ToString() + "'" +
            //    ", '" + DateTime.Today.ToShortDateString() + "'" +
            //    ",'0' " +
            //    ",'" + txtvatexadj.Text + "' )");
        }

        private void txtotherspercent_TextChanged(object sender, EventArgs e)
        {
             

            double discountedamount = 0.0;
            double percentageAmount = Convert.ToDouble(txtotherspercent.Text) / 100;
            //NOT USED BECAUSE THIS IS ONLY APPLICABLE IN SC/PWD, THESE ARE THE SELECTED ITEMS which the SC and PWD can AVAIL A DISCOUNT
            //string getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + percentageAmount + "')");
            
            //GET VATADJUSTMENT
            double getVatAdj = Database.getTotalSummation2("BatchSalesDetails", $"ReferenceNo='{PointOfSale.refno}' " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}' " +
                $"AND isCancelled=0 " +
                $"AND isVoid=0 " +
                $"AND isErrorCorrect=0 " +
                $"AND isVat=1 " +
                $"and DiscountTotal <=0 ", "TotalAmount");
            double netofvat = Math.Round(getVatAdj / 1.12,2);
            double discountamount = Math.Round(netofvat * percentageAmount,2);
            double vatadj = Math.Round(discountamount * 0.12,2);

            discountedamount = Math.Round(Convert.ToDouble(txtothersamount.Text) * percentageAmount, 2);
            txtotherdiscountamount.Text = discountedamount.ToString();
            txtvatadj.Text = vatadj.ToString();// getVatAdjustment;
            
            
            //GET VAT-EXEMPT ADJUSTMENT
            double getVatExAdj = Database.getTotalSummation2("BatchSalesDetails", $"ReferenceNo='{PointOfSale.refno}' " +
               $"AND BranchCode='{Login.assignedBranch}' " +
               $"AND MachineUsed='{Environment.MachineName}' " +
               $"AND isCancelled=0 " +
               $"AND isVoid=0 " +
               $"AND isErrorCorrect=0 " +
               $"AND isVat=0 " +
               $"and DiscountTotal <=0 ", "TotalAmount");
            double vatexDiscountamount = getVatExAdj * percentageAmount;
            double vatExNetAmt = getVatExAdj - vatexDiscountamount;
            txtvatexadj.Text = vatExNetAmt.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            percentamt = "0";
             isOnetimeDiscount = true;
            //bool checkifexists = Database.checkifExist("SELECT TOP 1 OrderNo  FROM SalesDiscount WHERE OrderNo='"+asdsa+"' AND isErrorCorrect=0 ");
            //if(checkifexists)
            //{
            //    bool isExists = HelperFunction.ConfirmDialog("The System found out that you already Discount this Transaction.. Do you want to Override and make a new Discount of this Transaction?", "Confirm Transaction");
            //    if(isExists)
            //    {
            //        Database.ExecuteQuery("Update From SalesDiscount SET isErrorCorrect=1 WHERE OrderNo='" + asdsa + "' AND isErrorCorrect=0");

            //    }
            //}

            if(radioButton1.Checked==true)  //radiobutton1 SENIOR
            {
                discounttype = "SENIOR";
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
                    remarks = "";
                    isSeniorDiscount = true;
                    isPwdDiscount = false;
                    isOthersDiscount = false;
                }
                
            }
            else if (radioButton2.Checked == true) //radiobutton2 PWD
            {
                discounttype = "PWD";
                if (String.IsNullOrEmpty(txtpwdidno.Text) || String.IsNullOrEmpty(txtpwddiscountamount.Text) || String.IsNullOrEmpty(txtpwdpercent.Text))
                {
                    XtraMessageBox.Show("Fields Mandatory");
                    return;
                }
                else
                {
                    percentamt = txtpwdpercent.Text;
                    id = txtpwdidno.Text;
                    discountamount = txtpwddiscountamount.Text;
                    name = txtpwdname.Text;
                    remarks = "";
                    isSeniorDiscount = false;
                    isPwdDiscount = true;
                    isOthersDiscount = false;
                }
            }
            else if (radioButton3.Checked == true) //radiobutton3 OTHERS
            {
                discounttype = "REGULAR";
                if (String.IsNullOrEmpty(txtremarks.Text) || String.IsNullOrEmpty(txtothersamount.Text) || String.IsNullOrEmpty(txtotherspercent.Text))
                {
                    XtraMessageBox.Show("Fields Mandatory");
                    return;
                }
                else
                {
                    percentamt = txtotherspercent.Text;
                    id = "";
                    discountamount = txtotherdiscountamount.Text;
                    name = "";
                    remarks = txtremarks.Text;
                    isSeniorDiscount = false;
                    isPwdDiscount = false;
                    isOthersDiscount = true;
                }
            }
            spDiscount();
            //Database.ExecuteQuery("INSERT INTO SalesDiscount VALUES ('"+Login.assignedBranch+"'" +
            //    ", '"+txttransactionno.Text+"'" +
            //    ", '"+txtorderno.Text+"'" +
            //    ", '"+txtcashiertansno.Text+"'" +
            //    ", '"+discounttype+"'" +
            //    ", '"+discountamount+"'" +
            //    ", '"+txtvatadj.Text+"'" +
            //    ", '"+name+"'" +
            //    ", '"+id+"'" +
            //    ", '"+remarks+"'" +
            //    ", '"+Login.isglobalUserID+"'" +
            //    ", '"+Environment.MachineName.ToString()+"'" +
            //    ", '"+DateTime.Today.ToShortDateString()+"'" +
            //    ",'0' "+
            //    ",'"+txtvatexadj.Text+"' )");


            Database.ExecuteQuery("INSERT INTO dbo.POSTransaction VALUES ('"+Login.assignedBranch+"'" +
                ", '"+txttransactionno.Text+"' " +
                ", '"+Environment.MachineName.ToString()+"' " +
                ", '"+ discounttype + "' " +
                ", '"+DateTime.Now.ToShortDateString() +"' " +
                ", '"+Login.isglobalUserID+"' " +
                ", 0" +
                ", 0)");

            isdone = true;
            this.Close();
        }

        double pwdAndSeniorDiscountAmount()
        {
           double discountamount = 
                Database.getTotalSummation2("BatchSalesDetails", "ReferenceNo='" + PointOfSale.refno + "' " +
                "AND BranchCode='" + Login.assignedBranch + "' " +
                "AND isCancelled=0" +
                "AND isVoid=0 " +
                "AND isErrorCorrect=0 " +
                "AND ProductCode in (Select ProductCode FROM Products WHERE Price5 > 0 AND BranchCode='"+Login.assignedBranch+"')", "TotalAmount");
            return discountamount;
        }

        void radchanged()
        {
            string getDiscountedItems = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItems('" + Login.assignedBranch + "','" + PointOfSale.refno + "')");
            //DEFAULT 5% DISCOUNT
            string getVatAdjustment = "0";
            string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + PointOfSale.refno + "')");
            if (String.IsNullOrEmpty(getDiscountedItems))
            {
                getDiscountedItems = "0";
            }

            if (radioButton1.Checked == true)
            {
                //SENIOR
                txtamnttobediscount.Text = getDiscountedItems;
                txtpercentageamount.Text = "5";  //DEFAULT 5% DISCOUNT
                getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','"+Convert.ToDouble(txtpercentageamount.Text)/100 +"')");
                txtvatadj.Text = getVatAdjustment;
                txtvatexadj.Text = getVATExAdj;

                txtcontrolno.Focus();
                panelsenior.Visible = true;
                panelothers.Visible = false;
                panelpwd.Visible = false;
                //txtamnttobediscount.Text = pwdAndSeniorDiscountAmount().ToString();
                btnshowdiscounteditems.Visible = true;
            }
            else if (radioButton2.Checked == true)
            {
                //PWD
                txtpwdamount.Text = getDiscountedItems;
                txtpwdpercent.Text = "5";  //DEFAULT 5% DISCOUNT
                getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + Convert.ToDouble(txtpwdpercent.Text) / 100 + "')");
                txtvatadj.Text = getVatAdjustment;
                txtvatexadj.Text = getVATExAdj;

                txtpwdidno.Focus();
                panelsenior.Visible = false;
                panelothers.Visible = false;
                panelpwd.Visible = true;
                //txtpwdamount.Text = pwdAndSeniorDiscountAmount().ToString();
                btnshowdiscounteditems.Visible = true;
            }
            else if (radioButton3.Checked == true)
            {
                txtothersamount.Text = PointOfSale.totamount;
                txtotherspercent.Text = "5";
                txtotherspercent.Focus();
                panelsenior.Visible = false;
                panelothers.Visible = true;
                panelpwd.Visible = false;
                btnshowdiscounteditems.Visible = false;
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//SENIOR
        {
            radchanged();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)//OTHERS
        {
            radchanged();
        }

        private void txtpwdpercent_TextChanged(object sender, EventArgs e)
        {
            double discountedamount = 0.0;
            double percentageAmount = Convert.ToDouble(txtpwdpercent.Text) / 100;
            string getVatAdjustment = Database.getSingleResultSet("SELECT dbo.func_getVatAdjustment('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + percentageAmount + "')");

            discountedamount = Math.Round(Convert.ToDouble(txtpwdamount.Text) * percentageAmount,2);
            txtpwddiscountamount.Text = discountedamount.ToString();
            txtvatadj.Text = getVatAdjustment;

            string getVATExAdj = Database.getSingleResultSet("SELECT dbo.func_getDiscountedItemsVATEXADJ('" + Login.assignedBranch + "','" + PointOfSale.refno + "','" + Environment.MachineName + "','" + percentageAmount + "')");
            txtvatexadj.Text = getVATExAdj;
          
        }

        private void btnPwdDiscount_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure All Data are inputted correcty?", "Add Discount");
            if (ok)
            {
                if (String.IsNullOrEmpty(txtpwdpercent.Text))
                {
                    XtraMessageBox.Show("Fields Mandatory");
                    return;
                }
                pwdidno = txtpwdidno.Text;
                pwdname = txtpwdname.Text;
                discountamount = txtpwddiscountamount.Text;
                isdone = true;
                isOnetimeDiscount = true;
                isPwdDiscount = true;
                isSeniorDiscount = false;
                isOthersDiscount = false;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) //PWD
        {
            radchanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure All Data are inputted correcty?", "Add Discount");
            if (ok)
            {
                if (String.IsNullOrEmpty(txtothersamount.Text))
                {
                    XtraMessageBox.Show("Fields Mandatory");
                    return;
                }
                controlno = "Others";
                name = "Others";
                discountamount = "0";
                discountamount = txtothersamount.Text;
                remarks = txtremarks.Text;
                isdone = true;
                isOnetimeDiscount = true;
                isOthersDiscount = true;
                isSeniorDiscount = false;
                isPwdDiscount = false;
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void txtamnttobediscount_TextChanged(object sender, EventArgs e)
        {
            double discountamount = 0.0;
            discountamount = Convert.ToDouble(txtamnttobediscount.Text) * 0.05;
            txtdiscountamount.Text = discountamount.ToString();
        }

        private void AddDiscount_Load(object sender, EventArgs e)
        {
            bool isscdiscreadonly = Database.checkifExist("SELECT isOverride FROM POSFunctions WHERE isOverride=1 and FunctionName='SCDISCOUNTFIELD' ");
            bool isspwdiscreadonly = Database.checkifExist("SELECT isOverride FROM POSFunctions WHERE isOverride=1 and FunctionName='PWDDISCOUNTFIELD' ");
            if (isscdiscreadonly) { txtpercentageamount.Enabled = true; }else { txtpercentageamount.Enabled = false; }
            if (isspwdiscreadonly) { txtpwdpercent.Enabled = true; } else { txtpwdpercent.Enabled = false; }
            btnshowdiscounteditems.Visible = true;
            radchanged();
            this.ActiveControl = txtcontrolno;
            txtcontrolno.Focus();
        }
    }
}