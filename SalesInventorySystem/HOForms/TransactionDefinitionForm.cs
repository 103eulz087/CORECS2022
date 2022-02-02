using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HOForms
{
    public partial class TransactionDefinitionForm : Form
    {
        string globalcode = "";
        public TransactionDefinitionForm()
        {
            InitializeComponent();
        }

        private void TransactionDefinitionForm_Load(object sender, EventArgs e)
        {
            //display();
            populateTransactionMapping();
            HelperFunction.DisableTextFields(this);
            HelperFunction.DisableCheckbox(this);
        }

        void populateTransactionMapping()
        {
            // Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches ", txtbranch, "BranchName", "BranchCode");
            //Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches ", txtbranchfilter, "BranchName", "BranchCode");
            Database.display("Select * FROM TransactionDefinition ORDER BY TransCode", gridControl1, gridView1);

            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts ", txtdebitglacct, "AccountCode", "Description");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts ", txtcreditglacct, "AccountCode", "Description");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts ", txtdebitprodacct, "AccountCode", "Description");
            Database.displaySearchlookupEdit("SELECT AccountCode,Description FROM ChartOfAccounts ", txtcreditprodacct, "AccountCode", "Description");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HelperFunction.EnableTextFields(this);
            HelperFunction.EnableCheckbox(this);
            simpleButton1.Enabled = false;
            addbtn.Enabled = true;
            updatebtn.Enabled = false;
            btncancel.Enabled = true;
        }

        void display()
        {
            //Database.display("SELECT * FROM view_TransactionDefinitions", gridControl1,gridView1);
            Database.displaySearchlookupEdit("Select BranchCode,BranchName from Branches", txtbranchfilter, "BranchName", "BranchCode");
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            if (HelperFunction.isTextBoxEmpty(txtcode) )
            {
                XtraMessageBox.Show("Please Input All Fields");
            }
            else
            {
                add();
                XtraMessageBox.Show("Successfully Added!");
                HelperFunction.ClearAllText(this);
                HelperFunction.DisableTextFields(this);
                HelperFunction.DisableCheckbox(this);
                simpleButton1.Enabled = true;
                addbtn.Enabled = false;
                updatebtn.Enabled = false;
                btncancel.Enabled = false;
                display();
            }
        }

        void add()
        {
            string debitCash, creditCash, debitRcvbl, creditRcvbl, nonCash, isAdmin, isCashier, isAccounting;
            if (isdebcash.Checked == true)
            {
                debitCash = "1";
            }
            else
            {
                debitCash = "0";
            }
            if (iscredcash.Checked == true)
            {
                creditCash = "1";
            }
            else
            {
                creditCash = "0";
            }
            if (isdebrcvbl.Checked == true)
            {
                debitRcvbl = "1";
            }
            else
            {
                debitRcvbl = "0";
            }
            if (iscredrcvbl.Checked == true)
            {
                creditRcvbl = "1";
            }
            else
            {
                creditRcvbl = "0";
            }
            if (isnoncash.Checked == true)
            {
                nonCash = "1";
            }
            else
            {
                nonCash = "0";
            }
            if (isadmin.Checked == true)
            {
                isAdmin = "1";
            }
            else
            {
                isAdmin = "0";
            }
            if (iscashier.Checked == true)
            {
                isCashier = "1";
            }
            else
            {
                isCashier = "0";
            }
            if (isaccounting.Checked == true)
            {
                isAccounting = "1";
            }
            else
            {
                isAccounting = "0";
            }
            Database.ExecuteQuery("INSERT INTO TransactionDefinition VALUES('" + txtcode.Text + "','','" + debitCash + "','" + creditCash + "','" + debitRcvbl + "','" + creditRcvbl + "','" + nonCash + "','" + isAdmin + "','" + isCashier + "','" + isAccounting + "','" + txtdesc.Text + "','" + txtdebitglacct.Text + "','" + txtdebitgldesc.Text + "','" + txtdebitprodacct.Text + "','" + txtdebitproddesc.Text + "','" + txtcreditglacct.Text + "','"+ txtcreditgldesc.Text+"','" + txtcreditprodacct.Text + "','"+ txtcreditproddesc.Text + "')");
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            HelperFunction.DisableTextFields(this);
            HelperFunction.DisableCheckbox(this);
            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            string debitCash, creditCash, debitRcvbl, creditRcvbl, nonCash, isAdmin, isCashier, isAccounting;
            if (isdebcash.Checked == true)
            {
                debitCash = "1";
            }
            else
            {
                debitCash = "0";
            }
            if (iscredcash.Checked == true)
            {
                creditCash = "1";
            }
            else
            {
                creditCash = "0";
            }
            if (isdebrcvbl.Checked == true)
            {
                debitRcvbl = "1";
            }
            else
            {
                debitRcvbl = "0";
            }
            if (iscredrcvbl.Checked == true)
            {
                creditRcvbl = "1";
            }
            else
            {
                creditRcvbl = "0";
            }
            if (isnoncash.Checked == true)
            {
                nonCash = "1";
            }
            else
            {
                nonCash = "0";
            }
            if (isadmin.Checked == true)
            {
                isAdmin = "1";
            }
            else
            {
                isAdmin = "0";
            }
            if (iscashier.Checked == true)
            {
                isCashier = "1";
            }
            else
            {
                isCashier = "0";
            }
            if (isaccounting.Checked == true)
            {
                isAccounting = "1";
            }
            else
            {
                isAccounting = "0";
            }
            if (HelperFunction.isTextBoxEmpty(txtcode))
            {
                XtraMessageBox.Show("Please Supply All Fields");
            }
            else
            {
                Database.ExecuteQuery("UPDATE TransactionDefinition SET TransCode='" + txtcode.Text + "',DebitCash='" + debitCash + "',CreditCash='" + creditCash + "',DebitRcvbl='" + debitRcvbl + "',CreditRcvbl='" + creditRcvbl + "',NonCash='" + nonCash + "',isAdmin='" + isAdmin + "',isCashier='" + isCashier + "',isAccounting='" + isAccounting + "',Description='" + txtdesc.Text + "',DebitGLAccount='" + txtdebitglacct.Text + "',DebitGLAccountDesc='"+txtdebitgldesc.Text+"',DebitGLProduct='" + txtdebitprodacct.Text + "',CreditGLAccount='" + txtcreditglacct.Text + "',CreditGLAccountDesc='"+txtcreditgldesc.Text+"',CreditGLProduct='" + txtcreditprodacct.Text+ "' WHERE TransCode='" + globalcode + "' ", "Successfully Updated!");
                HelperFunction.ClearAllText(this);
                HelperFunction.DisableTextFields(this);
                HelperFunction.DisableCheckbox(this);
                display();
                simpleButton1.Enabled = true;
                addbtn.Enabled = false;
                updatebtn.Enabled = false;
                btncancel.Enabled = false;
            }
        }

       
        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            HelperFunction.EnableTextFields(this);
            HelperFunction.EnableCheckbox(this);
            //int cord = dataGridView1.CurrentCellAddress.Y;
            //GridViewInfo info = gridView1.GetViewInfo() as GridViewInfo;
            //GridCellInfo cellInfo = info.GetGridCellInfo(gridView1.FocusedRowHandle, gridView1.FocusedColumn);

            globalcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransCode").ToString();// dataGridView1.Rows[cord].Cells[0].Value.ToString();
            txtdesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            //txtbranch.Text = Branch.getBranchName(txtbranch.Text);
            txtcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransCode").ToString(); //globalcode;
            txtdebitglacct.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitGLAccount").ToString();
            txtcreditglacct.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditGLAccount").ToString();
            txtdebitgldesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitGLAccountDesc").ToString();
            txtcreditgldesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditGLAccountDesc").ToString();
            txtdebitprodacct.Text = "";// gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitGLProduct").ToString();
            txtcreditprodacct.Text = "";// gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditGLProduct").ToString();
            txtdebitproddesc.Text = "";// gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitGLProductDesc").ToString();
            txtcreditproddesc.Text = "";// gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditGLProductDesc").ToString();
            populateTransactionMapping();
            //string debitCash, creditCash, debitRcvbl, creditRcvbl, nonCash, isAdmin, isCashier, isAccounting;

            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitCash").ToString()) == true)
            {
               //debitCash = "1";
                isdebcash.Checked = true;
            }
            else
            {
                //debitCash = "0";
                isdebcash.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditCash").ToString()) == true)
            {
               // creditCash = "1";
                iscredcash.Checked = true;
            }
            else
            {
                //creditCash = "0";
                iscredcash.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitRcvbl").ToString()) == true)
            {
                //debitRcvbl = "1";
                isdebrcvbl.Checked = true;
            }
            else
            {
                //debitRcvbl = "0";
                isdebrcvbl.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditRcvbl").ToString()) == true)
            {
                //creditRcvbl = "1";
                iscredrcvbl.Checked = true;
            }
            else
            {
               // creditRcvbl = "0";
                iscredrcvbl.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "NonCash").ToString()) == true)
            {
               // nonCash = "1";
                isnoncash.Checked = true;
            }
            else
            {
                //nonCash = "0";
                isnoncash.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isAdmin").ToString()) == true)
            {
                //isAdmin = "1";
                isadmin.Checked = true;
            }
            else
            {
               // isAdmin = "0";
                isadmin.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isCashier").ToString()) == true)
            {
               // isCashier = "1";
                iscashier.Checked = true;
            }
            else
            {
               // isCashier = "0";
                iscashier.Checked = false;
            }
            if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isAccounting").ToString()) == true)
            {
               // isAccounting = "1";
                isaccounting.Checked = true;
            }
            else
            {
               // isAccounting = "0";
                isaccounting.Checked = false;
            }


            //HelperFunction.EnableTextFields(this);
            
            //HelperFunction.ClearAllText(this);
            //HelperFunction.EnableCheckbox(this);
            simpleButton1.Enabled = false;
            addbtn.Enabled = false;
            updatebtn.Enabled = true;
            btncancel.Enabled = true;


        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  int cord = dataGridView1.CurrentCellAddress.Y;
            GridViewInfo info = gridView1.GetViewInfo() as GridViewInfo;
            GridCellInfo cellInfo = info.GetGridCellInfo(gridView1.FocusedRowHandle, gridView1.FocusedColumn);

            bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Transaction Code");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM TransactionDefinition WHERE TransCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TransCode").ToString() + "' ", "Successfully Deleted");
                display();
            }
        }

        private void txtdebitglacct_EditValueChanged(object sender, EventArgs e)
        {
            txtdebitgldesc.Text = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + txtdebitglacct.Text + "'", "Description");
        }

        private void txtcreditglacct_EditValueChanged(object sender, EventArgs e)
        {
            txtcreditgldesc.Text = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + txtcreditglacct.Text + "'", "Description");
        }

        private void txtdebitprodacct_EditValueChanged(object sender, EventArgs e)
        {
            txtdebitproddesc.Text = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + txtdebitprodacct.Text + "'", "Description");
        }

        private void txtcreditprodacct_EditValueChanged(object sender, EventArgs e)
        {
            txtcreditproddesc.Text = Database.getSingleQuery("ChartOfAccounts", "AccountCode='" + txtcreditprodacct.Text + "'", "Description");
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }

        private void txtbranchfilter_EditValueChanged(object sender, EventArgs e)
        {
            Database.display("SELECT * FROM view_TransactionDefinitions WHERE BranchCode='"+Branch.getBranchCode(txtbranchfilter.Text)+"'", gridControl1, gridView1);
        }
    }
}
