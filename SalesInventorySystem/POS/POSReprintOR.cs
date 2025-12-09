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
    public partial class POSReprintOR : DevExpress.XtraEditors.XtraForm
    {
        public static bool isdone = false;
        public POSReprintOR()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool functionReturnValue = false;
            if (keyData == Keys.Escape) //(keyData == (Keys.O | Keys.Control)) //POS SETTINGS
            {
                this.Dispose();
            }
            return functionReturnValue;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Printing printit = new Printing();
          
            //if(radioButton1.Checked==true)
            //{
            //    printit.printTextFile("C:\\POSTransaction\\CopyForReprint\\" + txtorno.Text + ".txt"); //print and select specific OR Number in CopyForReprint folder
            //    //printit.printTextFile("C:\\POSTransaction\\LastTransaction\\LastTran.txt");
            //    string details = String.Empty;
            //    string filepath = "C:\\POSTransaction\\CopyForReprint\\" + txtorno.Text + ".txt"; //open this or file
            //    var fileContent = string.Empty;
            //    using (var streamReader = new StreamReader(filepath, Encoding.UTF8))
            //    {
            //        fileContent = File.ReadAllText(filepath).Replace("*", txtcounter.Text); //copy this or file
            //    }


            //    string filepathorig = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            //    string txtorder = "\\" + txtcashiertranid.Text + "_E-JOURNAL.txt";
            //    string filetoprint = filepathorig + txtorder;
            //    StreamWriter writer;//,writer22;

            //    if (!Directory.Exists(filepathorig))
            //    {
            //        Directory.CreateDirectory(filepathorig);
            //        writer = new StreamWriter(filetoprint);
            //    }
            //    else
            //    {
            //        writer = new StreamWriter(filetoprint, true);
            //    }
            //    writer.Write(fileContent);
            //    writer.Close();
            //}
            //else
            //{
                
                
            //}
            bool isxists = Database.checkifExist("SELECT TOP(1) ReferenceNo FROM dbo.BatchSalesDetails WHERE BranchCode='" + Login.assignedBranch + "' and MachineUsed='" + Environment.MachineName + "' and ReferenceNo='" + txtorno.Text + "' ");
            if (!isxists)
            {
                XtraMessageBox.Show("OR Number not Exists");
                return;
            }
            else
            {
                string filepath1 = "C:\\POSTransaction\\CopyForReprint\\" + txtorno.Text + ".txt";
                var fileContent = string.Empty;
                fileContent = File.ReadAllText(filepath1);

                string petsa = DateTime.Now.ToShortDateString();
                string oras = DateTime.Now.ToShortTimeString();
                string fulldate1 = "Date Reprint: "+petsa + ' ' + oras;

                if (fileContent.Contains("*"))
                {
                    fileContent = fileContent.Replace("*", txtcounter.Text);
                }
                if (fileContent.Contains("$$$$$$"))
                {
                    fileContent = fileContent.Replace("$$$$$$", fulldate1);
                }
                if (fileContent.Contains("$$$$$"))
                {
                    fileContent = fileContent.Replace("$$$$$", fulldate1);
                }
                File.WriteAllText(filepath1, fileContent);
                //StreamWriter writer;//,writer22;
                //writer = new StreamWriter(filepath1, true);
                //writer.Write(fileContent);
                //writer.Close();
                printit.printTextFile(filepath1);
                //printit.printTextFile("C:\\POSTransaction\\CopyForReprint\\" + txtorno.Text + ".txt"); //print and select specific OR Number in CopyForReprint folder
                string filepath = "C:\\POSTransaction\\CopyForReprint\\" + txtorno.Text + ".txt"; //open this or file
                string filepathorig = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";

                HelperFunction.embedToJournal1(filepath,filepathorig,txtcashiertranid.Text,txtcounter.Text,txttransno.Text);
                //string details = String.Empty;
                //string filepath = "C:\\POSTransaction\\CopyForReprint\\" + txtorno.Text + ".txt"; //open this or file
                //string filepathpath = "C:\\POSTransaction\\CopyForReprint\\CopyForReprint\\" + txtorno.Text + ".txt"; //open this or file
                //var fileContent = string.Empty;
                //using (var streamReader = new StreamReader(filepath, Encoding.UTF8))
                //{
                //    fileContent = File.ReadAllText(filepath).Replace("*", txtcounter.Text); //copy this or file
                //}


                //string filepathorig = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
                //string txtorder = "\\" + txtcashiertranid.Text + "_E-JOURNAL.txt";
                //string filetoprint = filepathorig + txtorder;
                //StreamWriter writer;//,writer22;

                //if (!Directory.Exists(filepathorig))
                //{
                //    Directory.CreateDirectory(filepathorig);
                //    writer = new StreamWriter(filetoprint);
                //}
                //else
                //{
                //    writer = new StreamWriter(filetoprint, true);
                //}
                //writer.Write(fileContent);
                //writer.Close();
            }
            Database.ExecuteQuery("INSERT INTO POSTransaction VALUES('" + Login.assignedBranch + "','" + txttransno.Text + "','" + Environment.MachineName + "','Receipt Reprint','" + DateTime.Now.ToString() + "','" + Login.Fullname + "','"+txtcounter.Text+"','0','Receipt Reprint: OR#: "+ txtorno.Text + " was reprinted by Cashier.','','','','')");
            XtraMessageBox.Show("Successfully Print");
            isdone = true;
            this.Close();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                txtorno.Enabled = false;
                txtorno.Text = getLastTransaction();
                button1.Focus();
            }
            else
            {
                txtorno.Enabled = true;
                txtorno.Focus();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                txtorno.Enabled = true;
                txtorno.Focus();
            }
            else
            {
                txtorno.Enabled = false;
                txtorno.Text = getLastTransaction();
                button1.Focus();
            }
        }

        private void txtorno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        String getLastTransaction()
        {
            string currentor = PointOfSale._refno;
            double prevor = 0.0;
            prevor = Convert.ToDouble(currentor) - 1;
            string or = HelperFunction.sequencePadding1(prevor.ToString(),18);
            return or;
        }

        private void POSReprintOR_Load(object sender, EventArgs e)
        {
            this.ActiveControl = button1;
            radioButton1.Checked = true;
            txtorno.Text = getLastTransaction();
            button1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}