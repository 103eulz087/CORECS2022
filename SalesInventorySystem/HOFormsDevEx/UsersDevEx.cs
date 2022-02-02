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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class UsersDevEx : DevExpress.XtraEditors.XtraForm
    {
        public UsersDevEx()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            HelperFunction.EnableTextFields(this);
            HelperFunction.EnableCheckbox(this);
            txtbranch.Enabled = true;

            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btncancel.Enabled = true;
        } 
        private void display()
        {
            Database.display("SELECT * FROM view_Users", gridControl1,gridView1);
            //Database.displayComboBoxItems("SELECT * FROM Branches", "BranchName", txtbranch);
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbranch,"BranchCode","BranchCode");
        }
        
        private void UsersDevEx_Load(object sender, EventArgs e)
        {
            display();
        }
        void add()
        {
            string branchcode = "";
            branchcode = Branch.getBranchCode(txtbranch.Text);
            bool sadmin, sglobalofficer, swarehouseofficer, sbranchofficer, sapprover, smaker, schecker, scashier, saccounting;
            if (isadmin.Checked == true)
            {
                sadmin = true;
            }
            else
            {
                sadmin = false;
            }
            if (isglobalofficer.Checked == true)
            {
                sglobalofficer = true;
            }
            else
            {
                sglobalofficer = false;
            }
            if (iswarehouseofficer.Checked == true)
            {
                swarehouseofficer = true;
            }
            else
            {
                swarehouseofficer = false;
            }
            if (isBranchOfficer.Checked == true)
            {
                sbranchofficer = true;
            }
            else
            {
                sbranchofficer = false;
            }
            if (isapprover.Checked == true)
            {
                sapprover = true;
            }
            else
            {
                sapprover = false;
            }
            if (ischecker.Checked == true)
            {
                schecker = true;
            }
            else
            {
                schecker = false;
            }
            if (ismaker.Checked == true)
            {
                smaker = true;
            }
            else
            {
                smaker = false;
            }
            if (isaccounting.Checked == true)
            {
                saccounting = true;
            }
            else
            {
                saccounting = false;
            }
            if (iscashiering.Checked == true)
            {
                scashier = true;
            }
            else
            {
                scashier = false;
            }

            if (HelperFunction.isTextfieldEmpty(txtcashendlimit, txtcashinlimit, txtdesignation, txtemailadd, txtfullname, txtglaccount, txtpass, txtreceivablelimit, txtuserid))
            {
                XtraMessageBox.Show("Please Input All Valid Fields");
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Users VALUES('" + txtuserid.Text + "','" + txtfullname.Text + "','" + txtdesignation.Text + "','" + txtemailadd.Text + "','" + txtpass.Text + "','" + txtbranch.Text + "','" + sadmin + "','" + sglobalofficer + "','" + sbranchofficer + "','" + swarehouseofficer + "','" + scashier + "','" + smaker + "','" + schecker + "','" + sapprover + "','" + saccounting + "','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + txtcashendlimit.Text + "','" + txtcashinlimit.Text + "','" + txtreceivablelimit.Text + "','" + txtglaccount.Text + "')", "Succesfully Added!");
                HelperFunction.ClearAllText(this);
                HelperFunction.DisableTextFields(this);
                HelperFunction.DisableCheckbox(this);
                txtbranch.Text = "";
                txtbranch.Enabled = false;
                display();

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;
            }
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string branchcode = "";
                branchcode = Branch.getBranchCode(txtbranch.Text);
                bool sadmin, sglobalofficer, swarehouseofficer, sbranchofficer, sapprover, smaker, schecker, scashier, saccounting;
                if (isadmin.Checked == true)
                {
                    sadmin = true;
                }
                else
                {
                    sadmin = false;
                }
                if (isglobalofficer.Checked == true)
                {
                    sglobalofficer = true;
                }
                else
                {
                    sglobalofficer = false;
                }
                if (iswarehouseofficer.Checked == true)
                {
                    swarehouseofficer = true;
                }
                else
                {
                    swarehouseofficer = false;
                }
                if (isBranchOfficer.Checked == true)
                {
                    sbranchofficer = true;
                }
                else
                {
                    sbranchofficer = false;
                }
                if (isapprover.Checked == true)
                {
                    sapprover = true;
                }
                else
                {
                    sapprover = false;
                }
                if (isaccounting.Checked == true)
                {
                    saccounting = true;
                }
                else
                {
                    saccounting = false;
                }
                if (ischecker.Checked == true)
                {
                    schecker = true;
                }
                else
                {
                    schecker = false;
                }
                if (ismaker.Checked == true)
                {
                    smaker = true;
                }
                else
                {
                    smaker = false;
                }
                if (iscashiering.Checked == true)
                {
                    scashier = true;
                }
                else
                {
                    scashier = false;
                }

                if (HelperFunction.isTextfieldEmpty(txtcashendlimit, txtcashinlimit, txtdesignation, txtemailadd, txtfullname, txtglaccount, txtpass, txtreceivablelimit, txtuserid))
                {
                    XtraMessageBox.Show("Please Input All Valid Fields");
                }
                else
                {
                    Database.ExecuteQuery("UPDATE Users SET Fullname='" + txtfullname.Text + "',Designation='" + txtdesignation.Text + "',EmailAddress='" + txtemailadd.Text + "',Password='" + txtpass.Text + "',AssignedBranch='" + txtbranch.Text + "',isAdmin='" + sadmin + "',isGlobalOfficer='" + sglobalofficer + "',isBranchOfficer='" + sbranchofficer + "',isWarehouseOfficer='" + swarehouseofficer + "',isCashier='" + scashier + "',isMaker='" + smaker + "',isChecker='" + schecker + "',isApprover='" + sapprover + "',isAccounting='" + saccounting + "',LastUpdated='" + DateTime.Now.ToString() + "',CashEndLimit='" + txtcashendlimit.Text + "',CashInLimit='" + txtcashinlimit.Text + "',ReceivableLimit='" + txtreceivablelimit.Text + "',GLAccount='" + txtglaccount.Text + "' WHERE UserID='" + txtuserid.Text + "' ", "Successfully Updated!");
                    HelperFunction.ClearAllText(this);
                    HelperFunction.DisableTextFields(this);
                    HelperFunction.DisableCheckbox(this);
                    txtbranch.Text = "";
                    txtbranch.Enabled = false;
                    display();

                    btnnew.Enabled = true;
                    btnadd.Enabled = false;
                    btnupdate.Enabled = false;
                    btncancel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            txtbranch.Text = "";
            HelperFunction.DisableCheckbox(this);
            HelperFunction.DisableTextFields(this);
            txtbranch.Enabled = false;
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            HelperFunction.EnableCheckbox(this);
            HelperFunction.EnableTextFields(this);
            txtbranch.Enabled = true;
           
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"isAdmin").ToString()) == true)
            {
                isadmin.Checked = true;
            }
            else
            {
                isadmin.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isGlobalOfficer").ToString()) == true)
            {
                isglobalofficer.Checked = true;
            }
            else
            {
                isglobalofficer.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isBranchOfficer").ToString()) == true)
            {
                isBranchOfficer.Checked = true;
            }
            else
            {
                isBranchOfficer.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isWarehouseOfficer").ToString()) == true)
            {
                iswarehouseofficer.Checked = true;
            }
            else
            {
                iswarehouseofficer.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isCashier").ToString()) == true)
            {
                iscashiering.Checked = true;
            }
            else
            {
                iscashiering.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isMaker").ToString()) == true)
            {
                ismaker.Checked = true;
            }
            else
            {
                ismaker.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isChecker").ToString()) == true)
            {
                ischecker.Checked = true;
            }
            else
            {
                ischecker.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isApprover").ToString()) == true)
            {
                isapprover.Checked = true;
            }
            else
            {
                isapprover.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isAccounting").ToString()) == true)
            {
                isaccounting.Checked = true;
            }
            else
            {
                isaccounting.Checked = false;
            }
            string branchname = Branch.getBranchName(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AssignedBranch").ToString());
            txtbranch.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AssignedBranch").ToString(); //BranchCode
            txtcashendlimit.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"CashEndLimit").ToString();
            txtcashinlimit.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"CashInLimit").ToString();
            txtdesignation.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"Designation").ToString();
            txtemailadd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EmailAddress").ToString();
            txtfullname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"FullName").ToString();
            txtglaccount.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"GLAccount").ToString();
            txtpass.Text = "";
            txtreceivablelimit.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"ReceivableLimit").ToString();
            txtuserid.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"UserID").ToString();

            txtuserid.Enabled = false;
            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Supplier");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM Users WHERE UserID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"UserID").ToString() + "'", "Successfully Deleted");
                display();
            }
        }

        private void resetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery($"UPDATE Users SET Password='123456' WHERE UserID='{txtuserid.Text}'","Successfully Updated!..");
        }
    }
}