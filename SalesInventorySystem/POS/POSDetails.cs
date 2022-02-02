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
using System.IO;

namespace SalesInventorySystem.POS
{
    public partial class POSDetails : DevExpress.XtraEditors.XtraForm
    {
        public POSDetails()
        {
            InitializeComponent();
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }

        void saveReturnFooter()
        {
            String details = "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Operated By: Eulen Topacio Avancena") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("NON VAT Reg Tin: 221-413-885-00000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("3720 Woodland Heights R. Duterte St.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Guadalupe Cebu City 6000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Accd#: " + txtaccreditationno.Text.Trim()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Date Issued: " + Convert.ToDateTime(txtdateaccredited.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Valid Until: " + Convert.ToDateTime(txtvaliduntil.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU No.: " + txtpermitno.Text) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Date Issued: " + Convert.ToDateTime(txtdateofpermit.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Valid Until: " + Convert.ToDateTime(txtvaliduntil.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("This serves as your " + txttype.Text) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS DOCUMENT SHALL BE VALID FOR") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FIVE (5) YEARS FROM DATE OF THE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PERMIT TO USE.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS DOCUMENT IS NOT VALID FOR") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("CLAIM OF INPUT TAX") + Environment.NewLine;
            string path = Application.StartupPath + "\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string file = "FOOTERRETURN.txt";
            StreamWriter writer = new StreamWriter(path + file);
            writer.Write(details);
            writer.Close();
        }
    

        void save()
        {
           
            //FOOTER 
            string type = "";
            if (txttype.Text == "SALES INVOICE")
            {
                type = "INVOICE";
            }
            else if (txttype.Text == "OFFICIAL RECEIPT")
            {
                type = "RECEIPT";
            }

            String details = "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Operated By: Eulen Topacio Avancena") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("NON VAT Reg Tin: 221-413-885-00000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("3720 Woodland Heights R. Duterte St.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Guadalupe Cebu City 6000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Accd#: "+txtaccreditationno.Text.Trim()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Date Issued: "+Convert.ToDateTime(txtdateaccredited.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Valid Until: "+ Convert.ToDateTime(txtvaliduntil.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU No.: "+txtpermitno.Text) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Date Issued: "+ Convert.ToDateTime(txtdateofpermit.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Valid Until: "+ Convert.ToDateTime(txtvaliduntil.Text).ToShortDateString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("This serves as your "+txttype.Text) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS "+ type + " SHALL BE VALID FOR") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FIVE (5) YEARS FROM DATE OF THE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PERMIT TO USE.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THANK YOU! Pls. Come Again.") + Environment.NewLine;
            string path = Application.StartupPath+"\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string file = "FOOTER.txt";
            StreamWriter writer = new StreamWriter(path + file);
            writer.Write(details);
            writer.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            save();
            saveReturnFooter();
            Database.ExecuteQuery("INSERT INTO POSInfoDetails VALUES('" + txtbrcode.Text + "'" +
                ",'" + txtbusinessname.Text + "'" +
                ",'" + txtbusinessaddress.Text + "'" +
                ",'" + txttinno.Text + "'" +
                ",'" + txtmachinename.Text + "'" +
                ",'" + txtaccreditationno.Text + "'" +
                ",'" + txtdateaccredited.Text + "'" +
                ",'" + txtserialno.Text + "'" +
                ",'" + txtregtransno.Text + "'" +
                ",'" + txtdateofapplication.Text + "'" +
                ",'" + txtpermitno.Text + "'" +
                ",'" + txtminno.Text + "'" +
                ",'" + txtdateofpermit.Text + "'" +
                ",'" + txtvaliduntil.Text + "')", "Successfully Added");
            this.Dispose();
        }

        private void POSDetails_Load(object sender, EventArgs e)
        {
            txtbrcode.Text = Login.assignedBranch;
            txtmachinename.Text = Environment.MachineName;
        }
    }
}