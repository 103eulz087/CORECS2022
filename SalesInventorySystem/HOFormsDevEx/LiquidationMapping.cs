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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class LiquidationMapping : DevExpress.XtraEditors.XtraForm
    {
        string refid, expname, debacctcode, debitdesc, creditacctcode, creddesc, code, factor;

        private void txtexpdebacctcode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtexpdebacctcode.Properties.View;
            if (view.RowCount > 0)
            {
                int rowHandle = view.FocusedRowHandle;
                object value = view.GetRowCellValue(rowHandle, "Description");
                txtdebitdesc.Text = value.ToString();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE LiquidationMapping SET LiquidationName='" + txtexpname.Text + "',DebitAccountCode='" + txtexpdebacctcode.Text + "',DebitDescription='" + txtdebitdesc.Text + "',CreditAccountCode='" + txtcreditcode.Text + "',CreditDescription='" + txtcreddesc.Text + "',Code='" + txtcode.Text + "',Factor='" + txtfactor.Text + "' WHERE ReferenceID='" + refid + "' AND LiquidationName='" + expname + "' AND Code='" + code + "' ", "Successfully Updated!");
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = (GridView)sender;
            if (e.Column.FieldName == "Code")
            {
                e.Appearance.BackColor = Color.LightSalmon;
            }
        }

        private void txtcreditcode_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtcreditcode.Properties.View;
            if (view.RowCount > 0)
            {
                int rowHandle = view.FocusedRowHandle;
                object value = view.GetRowCellValue(rowHandle, "Description");
                txtcreddesc.Text = value.ToString();
            }
        }

        private void txtfactor_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            enablefields();

            display();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            bool ok = Database.checkifExist("SELECT LiquidationName FROM LiquidationMapping WHERE LiquidationName='" + txtexpname.Text.Trim() + "' AND Code = '" + txtcode.Text.Trim() + "'");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in LiquidationList Table.. Please use Edit Function");
                return;
            }
            else
            {
                Database.ExecuteQuery("INSERT INTO LiquidationMapping VALUES('" + txtexpid.Text + "','" + txtexpname.Text + "','" + txtexpdebacctcode.Text + "','" + txtdebitdesc.Text + "','" + txtcreditcode.Text + "','" + txtcreddesc.Text + "','" + txtcode.Text + "','" + txtfactor.Text + "')", "Successfully Added");
                clear();

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;

                disablefields();
                display();
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            disablefields();

            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete LiquidationList");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM LiquidationMapping WHERE ReferenceID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceID").ToString() + "' AND LiquidationName='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LiquidationName").ToString() + "' and Code='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Code").ToString() + "'", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReferenceID").ToString();
            expname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LiquidationName").ToString();
            debacctcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitAccountCode").ToString();
            debitdesc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DebitDescription").ToString();
            creditacctcode = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditAccountCode").ToString();
            creddesc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CreditDescription").ToString();
            code = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Code").ToString();
            factor = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Factor").ToString();
            txtcode.Text = code;
            txtcreddesc.Text = creddesc;
            txtcreditcode.Text = creditacctcode;
            txtdebitdesc.Text = debitdesc;
            txtexpdebacctcode.Text = debacctcode;
            txtexpid.Text = refid;
            txtexpname.Text = expname;
            txtfactor.Text = factor;
            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void txtexpname_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = txtexpname.Properties.View;
            if (view.RowCount > 0)
            {
                int rowHandle = view.FocusedRowHandle;
                object value = view.GetRowCellValue(rowHandle, "LiquidationID");
                txtexpid.Text = value.ToString();
            }
        }

        public LiquidationMapping()
        {
            InitializeComponent();
        }

        private void LiquidationMapping_Load(object sender, EventArgs e)
        {
            display();
            populate();
        }
        void display()
        {
            Database.display("SELECT * FROM LiquidationMapping ORDER BY ReferenceID", gridControl1, gridView1);
        }
        void clear()
        {
            txtcode.Text = "";
            txtcreddesc.Text = "";
            txtcreditcode.Text = "";
            txtdebitdesc.Text = "";
            txtexpdebacctcode.Text = "";
            txtexpid.Text = "";
            txtexpname.Text = "";
            txtfactor.Text = "";
        }

        void disablefields()
        {
            txtcode.Enabled = false;
            txtcreddesc.Enabled = false;
            txtcreditcode.Enabled = false;
            txtdebitdesc.Enabled = false;
            txtexpdebacctcode.Enabled = false;
            txtexpid.Enabled = false;
            txtexpname.Enabled = false;
            txtfactor.Enabled = false;
        }
        void enablefields()
        {
            txtcode.Enabled = true;

            txtcreditcode.Enabled = true;
            
            txtexpdebacctcode.Enabled = true;

            txtexpname.Enabled = true;
            txtfactor.Enabled = true;
        }
        void populate()
        {
            Database.displaySearchlookupEdit("Select * FROM LiquidationList", txtexpname, "LiquidationName", "LiquidationName");
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts", txtexpdebacctcode, "AccountCode", "AccountCode");
            Database.displaySearchlookupEdit("Select AccountCode,Description FROM ChartOfAccounts", txtcreditcode, "AccountCode", "AccountCode");
        }

    }
}