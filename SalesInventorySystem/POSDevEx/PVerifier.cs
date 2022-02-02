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

namespace SalesInventorySystem.POSDevEx
{
    public partial class PVerifier : DevExpress.XtraEditors.XtraForm
    {
        public PVerifier()
        {
            InitializeComponent();
            txtprodname.SelectionAlignment = HorizontalAlignment.Center;
        }
        void getDetails()
        {
            try
            {
                string prd = "", prc = "";
                // var rows = Database.getMultipleQuery($"SELECT Description,SellingPrice FROM Products WHERE BranchCode='002' and Barcode='{txtbarcode.Text}' ", "Description,SellingPrice");
                var rows = Database.getMultipleQuery($"SELECT TOP(1) Description,SellingPrice " +
                    $"FROM Products " +
                    $"WHERE BranchCode='002' " +
                    $"and Barcode='{txtbarcode.Text}' "
                    , "Description,SellingPrice");
                //var rows = Database.getMultipleQueryLocal($"SELECT Description,SellingPrice " +
                //    $"FROM Products " +
                //    $"WHERE BranchCode='002' " +
                //    $"and Barcode='{txtbarcode.Text}' "
                //    , "Description,SellingPrice"
                //    ,Database.getCustomConnection(constringLocal));
                prd = rows["Description"].ToString();
                prc = rows["SellingPrice"].ToString();
                txtprodname.Text = prd;
                txtprice.Text = String.Format("{0:0,0.00}", Convert.ToDouble(prc));


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString() + " ITEM NOT FOUND");
            }
            txtbarcode.Text = "";
            txtbarcode.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Enter) //FOCUS TO SKU TEXTFIELD (keyData == (Keys.O | Keys.Control))
            {
                getDetails();
            }
            else if (keyData == (Keys.S | Keys.Control | Keys.Alt)) //FOCUS TO SKU TEXTFIELD (keyData == (Keys.O | Keys.Control))
            {
                //textEdit3.Focus();
                POS.POSSyncProducts asdj = new POS.POSSyncProducts();
                asdj.ShowDialog(this);
            }

            else if (keyData == Keys.Escape) //(keyData == (Keys.O | Keys.Control)) //REFUND REPORT
            {
                bool confirm = HelperFunction.ConfirmDialog("Are you sure you want to close this Window?", "Confirm Close");
                if (confirm)
                {
                    this.Dispose();
                }

            }
            //else if (keyData == (Keys.X | Keys.Control)) //(keyData == (Keys.O | Keys.Control)) //REFUND REPORT
            //{
            //    Reading();
            //}
            else if (keyData == (Keys.S | Keys.Control)) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                POSStandAloneSetup.POSTypeSettings posnnd = new POSStandAloneSetup.POSTypeSettings();
                posnnd.ShowDialog(this);
            }
            return functionReturnValue;
        }

        private void txtbarcode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    getDetails();
            //}
        }

        private void PVerifier_Load(object sender, EventArgs e)
        {
            txtbarcode.Focus();
        }

        private void txtprice_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}