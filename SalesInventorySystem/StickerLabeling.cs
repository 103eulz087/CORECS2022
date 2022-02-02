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
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem
{
    public partial class StickerLabeling : DevExpress.XtraEditors.XtraForm
    {
        string strdesc = "",barcode="",strpcode="";
        public StickerLabeling()
        {
            InitializeComponent();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string id = IDGenerator.getIDNumberSP("sp_GetPrintLabelNumber", "ID");
            decimal quantity;
            string strquantity;
            barcode = "";
            bool isBarcodeLong = false;
            isBarcodeLong = Database.checkifExist("SELECT isLong FROM BarcodeSettings WHERE isLong=1");
            quantity = Decimal.Parse(txtqty.Text);
            strquantity = String.Format("{0:00.000}", quantity);
            if (isBarcodeLong == true)
            {
                barcode = "66666" + strpcode + strquantity.Replace(".", "") + id.ToString();
            }
            else
            {
                barcode = strpcode + strquantity.Replace(".", "") + id.ToString();
            }
            Database.ExecuteQuery("INSERT INTO PrintLabel VALUES('" + barcode + "','" + dateTimePicker1.Text + "','"+strdesc+"','"+txtqty.Text+"')");
            DateTime dt;
            dt = Convert.ToDateTime(dateTimePicker1.Text).AddYears(1);
            Barcode.BarcodePrinting bprint = new Barcode.BarcodePrinting();
            bprint.lblmanufdate.Text = dateTimePicker1.Text;
            bprint.lblprodtype.Text = strdesc;
            bprint.lbltotalkilos.Text = txtqty.Text;
            bprint.xrBarCode2.Text = barcode;
            bprint.lblxpirydate.Text = dt.ToShortDateString();
            
            ReportPrintTool report = new ReportPrintTool(bprint);
            report.Print();
        }

        private void StickerLabeling_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void txtqty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                simpleButton4.PerformClick();
        }

        void populate()
        {
            Database.displaySearchlookupEdit("Select ProductCode,Description FROM Products WHERE BranchCode='888' ORDER BY ProductCode", searchLookUpEdit1,"Description","Description");
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            GridView view = searchLookUpEdit1.Properties.View;
            int rowHandle = view.FocusedRowHandle;
            object pcode = view.GetRowCellValue(rowHandle, "ProductCode");
            object desc = view.GetRowCellValue(rowHandle, "Description");
            strdesc = desc.ToString();
            strpcode = pcode.ToString();
        }
    }
}