using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem
{
    public partial class HeadOfficeInventory : DevExpress.XtraEditors.XtraForm
    {
        string inventoryowner = "";
        public HeadOfficeInventory()
        {
            InitializeComponent();
        }

        private void HeadOfficeInventory_Load(object sender, EventArgs e)
        {
            display();
        }

        private void display()
        {
            if(radioButton1.Checked==true)
            {
                //display big blue consolidated inventory
                Database.display("Select * FROM view_BigBlueInventory", gridControl1, gridView1);
                inventoryowner = "Big Blue";
            }
            else
            {
                //display big blue consolidated inventory
                Database.display("Select * FROM view_CommissaryInventory", gridControl1, gridView1);
                inventoryowner = "Commissary";
            }
            Database.displayDevComboBoxItems("Select Description FROM ProductCategory", "Description", comboBoxEdit1);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Database.display("Select * FROM view_CommissaryInventory", gridControl1, gridView1);
            inventoryowner = "Commissary";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Database.display("Select * FROM view_BigBlueInventory", gridControl1, gridView1);
            inventoryowner = "Big Blue";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DevExReportTemplate.ConsolidatedInventory xct = new DevExReportTemplate.ConsolidatedInventory();
            xct.Landscape = false;
            xct.PaperKind = System.Drawing.Printing.PaperKind.Legal;
            xct.Margins = new System.Drawing.Printing.Margins(100, 100, 100, 100);
            xct.inventoryowner.Text = inventoryowner;
            xct.Bands[BandKind.Detail].Controls.Add(HelperFunction.CopyGridControl(this.gridControl1));
            xct.Bands[BandKind.Detail].Font = new System.Drawing.Font("Tahoma", 10);
            ReportPrintTool report = new ReportPrintTool(xct);
            report.ShowRibbonPreviewDialog();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string prodcatcode = Classes.Product.getProductCategoryCode(comboBoxEdit1.Text);
            if(radioButton1.Checked==true)
            {
                Database.display("Select * FROM view_BigBlueInventory WHERE SUBSTRING(Product,1,2)='"+prodcatcode+"'", gridControl1, gridView1);
                inventoryowner = "Big Blue";
            }
            else
            {
                Database.display("Select * FROM view_CommissaryInventory WHERE SUBSTRING(Product,1,2)='" + prodcatcode + "'", gridControl1, gridView1);
                inventoryowner = "Commissary";
            }
        }
    }
}