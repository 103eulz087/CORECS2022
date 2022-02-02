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

namespace SalesInventorySystem.HOForms
{
    public partial class Users : DevExpress.XtraEditors.XtraForm
    {
        public Users()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HelperFunction.EnableTextFields(this);
            HelperFunction.EnableCheckbox(this);
            txtbranch.Enabled = true;

            simpleButton1.Enabled = false;
            addbtn.Enabled = true;
            btncancel.Enabled = true;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            string branchcode="";
            branchcode = Branch.getBranchCode(txtbranch.Text);
            bool sadmin,sglobalofficer,swarehouseofficer,sbranchofficer,sapprover,smaker,schecker,scashier,saccounting;
            if(isadmin.Checked == true)
            {
                sadmin=true;
            }
            else
            {
                sadmin=false;
            }
            if(isglobalofficer.Checked == true)
            {
                sglobalofficer = true;
            }
            else
            {
                sglobalofficer = false;
            }
            if (iswarehouseofficer.Checked== true)
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
            if (isaccounting.Checked== true)
            {
                saccounting = true;
            }
            else
            {
                saccounting = false;
            }
            if (iscashiering.Checked== true)
            {
                scashier = true;
            }
            else
            {
                scashier = false;
            }

            if (HelperFunction.isTextBoxEmpty( txtcashendlimit, txtcashinlimit, txtdesignation, txtemailadd, txtfullname, txtglaccount, txtpassword, txtreceivablelimit, txtuserid))
            {
                XtraMessageBox.Show("Please Input All Valid Fields");
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO Users VALUES('" + txtuserid.Text + "','" + txtfullname.Text + "','" + txtdesignation.Text + "','" + txtemailadd.Text + "','" + txtpassword.Text + "','" + branchcode + "','" + sadmin + "','" + sglobalofficer + "','" + sbranchofficer + "','" + swarehouseofficer + "','" + scashier + "','" + smaker + "','" + schecker + "','" + sapprover + "','"+saccounting+"','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + txtcashendlimit.Text + "','" + txtcashinlimit.Text + "','" + txtreceivablelimit.Text + "','" + txtglaccount.Text + "')", "Succesfully Added!");
                HelperFunction.ClearAllText(this);
                HelperFunction.DisableTextFields(this);
                HelperFunction.DisableCheckbox(this);
                txtbranch.Text = "";
                txtbranch.Enabled = false;
                display();

                simpleButton1.Enabled = true;
                addbtn.Enabled = false;
                updatebtn.Enabled = false;
                btncancel.Enabled = false;
            }
           
        }

        private void display()
        {
            Database.displayLocalGrid("SELECT * FROM view_Users", dataGridView1);
        }

        private void Users_Load(object sender, EventArgs e)
        {
            display();
        }

        private void txtbranch_Click(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT * FROM Branches", "BranchName", txtbranch);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            txtbranch.Text = "";
            HelperFunction.DisableCheckbox(this);
            HelperFunction.DisableTextFields(this);
            txtbranch.Enabled = false;
            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            HelperFunction.EnableCheckbox(this);
            HelperFunction.EnableTextFields(this);
            txtbranch.Enabled = true;
            int cord = dataGridView1.CurrentCellAddress.Y;
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[6].Value.ToString()) == true)
            {
                isadmin.Checked = true;
            }
            else
            {
                isadmin.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[7].Value.ToString()) == true)
            {
                isglobalofficer.Checked = true;
            }
            else
            {
                isglobalofficer.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[8].Value.ToString()) == true)
            {
                isBranchOfficer.Checked = true;
            }
            else
            {
                isBranchOfficer.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[9].Value.ToString()) == true)
            {
                iswarehouseofficer.Checked = true;
            }
            else
            {
                iswarehouseofficer.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[10].Value.ToString()) == true)
            {
                iscashiering.Checked = true;
            }
            else
            {
                iscashiering.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[11].Value.ToString()) == true)
            {
                ismaker.Checked = true;
            }
            else
            {
                ismaker.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[12].Value.ToString()) == true)
            {
                ischecker.Checked = true;
            }
            else
            {
                ischecker.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[13].Value.ToString()) == true)
            {
                isapprover.Checked = true;
            }
            else
            {
                isapprover.Checked = false;
            }
            if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[14].Value.ToString()) == true)
            {
                isaccounting.Checked = true;
            }
            else
            {
                isaccounting.Checked = false;
            }
            string branchname = Branch.getBranchName(dataGridView1.Rows[cord].Cells[5].Value.ToString());
            txtbranch.Text = branchname;
            txtcashendlimit.Text = dataGridView1.Rows[cord].Cells["CashEndLimit"].Value.ToString();
            txtcashinlimit.Text = dataGridView1.Rows[cord].Cells["CashInLimit"].Value.ToString();
            txtdesignation.Text = dataGridView1.Rows[cord].Cells[2].Value.ToString();
            txtemailadd.Text = dataGridView1.Rows[cord].Cells[3].Value.ToString();
            txtfullname.Text = dataGridView1.Rows[cord].Cells[1].Value.ToString();
            txtglaccount.Text = dataGridView1.Rows[cord].Cells["GLAccount"].Value.ToString();
            txtpassword.Text = "";
            txtreceivablelimit.Text = dataGridView1.Rows[cord].Cells["ReceivableLimit"].Value.ToString();
            txtuserid.Text = dataGridView1.Rows[cord].Cells[0].Value.ToString();

            txtuserid.Enabled = false;
            simpleButton1.Enabled = false;
            addbtn.Enabled = false;
            updatebtn.Enabled = true;
            btncancel.Enabled = true;

        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(dataGridView1, e.Location);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
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

                if (HelperFunction.isTextBoxEmpty(txtcashendlimit, txtcashinlimit, txtdesignation, txtemailadd, txtfullname, txtglaccount, txtpassword, txtreceivablelimit, txtuserid))
                {
                    XtraMessageBox.Show("Please Input All Valid Fields");
                }
                else
                {
                    Database.ExecuteQuery("UPDATE Users SET Fullname='" + txtfullname.Text + "',Designation='" + txtdesignation.Text + "',EmailAddress='" + txtemailadd.Text + "',Password='" + txtpassword.Text + "',AssignedBranch='" + branchcode + "',isAdmin='" + sadmin + "',isGlobalOfficer='" + sglobalofficer + "',isBranchOfficer='" + sbranchofficer + "',isWarehouseOfficer='" + swarehouseofficer + "',isCashier='" + scashier + "',isMaker='" + smaker + "',isChecker='" + schecker + "',isApprover='" + sapprover + "',isAccounting='" + saccounting + "',LastUpdated='" + DateTime.Now.ToString() + "',CashEndLimit='" + txtcashendlimit.Text + "',CashInLimit='" + txtcashinlimit.Text + "',ReceivableLimit='" + txtreceivablelimit.Text + "',GLAccount='" + txtglaccount.Text + "' WHERE UserID='" + txtuserid.Text + "' ", "Successfully Updated!");
                    HelperFunction.ClearAllText(this);
                    HelperFunction.DisableTextFields(this);
                    HelperFunction.DisableCheckbox(this);
                    txtbranch.Text = "";
                    txtbranch.Enabled = false;
                    display();

                    simpleButton1.Enabled = true;
                    addbtn.Enabled = false;
                    updatebtn.Enabled = false;
                    btncancel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cord = dataGridView1.CurrentCellAddress.Y;
             bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Supplier");
             if (ok)
             {
                 Database.ExecuteQuery("DELETE FROM Users WHERE UserID='" + dataGridView1.Rows[cord].Cells[0].Value.ToString() + "'", "Successfully Deleted");
                 display();
             }
        }

        //private void isadmin_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (isadmin.Checked == true)
        //    {
        //        //isapprover.Enabled = false;
        //        //isBranchOfficer.Enabled = false;
        //        //iswarehouseofficer.Enabled = false;
        //        //iscashiering.Enabled = false;
        //        //ischecker.Enabled = false;
        //        //isglobalofficer.Enabled = false;
        //        //ismaker.Enabled = false;
        //        HelperFunction.DisableCheckbox(this);
        //        isadmin.Enabled = true;
        //    }
        //    else
        //    {
        //        //isapprover.Enabled = true;
        //        //isBranchOfficer.Enabled = true;
        //        //iswarehouseofficer.Enabled = true;
        //        //iscashiering.Enabled = true;
        //        //ischecker.Enabled = true;
        //        //isglobalofficer.Enabled = true;
        //        //ismaker.Enabled = true;
        //        HelperFunction.EnableCheckbox(this);
        //    }
        //}

        //private void isglobalofficer_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (isglobalofficer.Checked == true)
        //    {
               
        //        //isapprover.Enabled = false;
        //        //isBranchOfficer.Enabled = false;
        //        //iswarehouseofficer.Enabled = false;
        //        //iscashiering.Enabled = false;
        //        //ischecker.Enabled = false;
        //        //isglobalofficer.Enabled = false;
        //        HelperFunction.DisableCheckbox(this);
        //        isglobalofficer.Enabled = true;
        //        ismaker.Enabled = true;
        //    }
        //    else
        //    {
        //        HelperFunction.EnableCheckbox(this);
        //        ismaker.Checked = false;
        //        ischecker.Checked = false;
        //        isapprover.Checked = false;
        //    }
        //}

        //private void isBranchOfficer_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (isBranchOfficer.Checked == true)
        //    {
        //        HelperFunction.DisableCheckbox(this);
        //        iscashiering.Enabled = true;
        //        isBranchOfficer.Enabled = true;
        //    }
        //    else
        //    {
        //        HelperFunction.EnableCheckbox(this);
        //        iscashiering.Checked = false;
        //    }
        //}

        //private void iswarehouseofficer_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (iswarehouseofficer.Checked == true)
        //    {
        //        HelperFunction.DisableCheckbox(this);
        //        iswarehouseofficer.Enabled = true;
        //    }
        //    else
        //    {
        //        HelperFunction.EnableCheckbox(this);
        //    }
        //}

        //private void ismaker_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ismaker.Checked == true)
        //    {
        //        HelperFunction.DisableCheckbox(this);
        //        ismaker.Enabled = true;
        //        isapprover.Enabled = true;
        //        ischecker.Enabled = true;
        //        isglobalofficer.Enabled = true;
        //    }
        //    else
        //    {
        //        HelperFunction.EnableCheckbox(this);
        //        isglobalofficer.Checked = false;
        //        ischecker.Checked = false;
        //        isapprover.Checked = false;
        //    }
        //}

        //private void iscashiering_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (iscashiering.Checked == true)
        //    {
        //        HelperFunction.DisableCheckbox(this);
        //        iscashiering.Enabled = true;
        //        isBranchOfficer.Enabled = true;
        //    }
        //    else
        //    {
        //        HelperFunction.EnableCheckbox(this);
        //        isBranchOfficer.Checked = false;
        //    }
        //}

    }
}