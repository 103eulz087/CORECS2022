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

namespace SalesInventorySystem.Forwarding
{
    public partial class ForwardingTruckLogs : DevExpress.XtraEditors.XtraForm
    {
        public ForwardingTruckLogs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filepath = @"C:\ITCSI\IRLTRUCKING\";
            string file = txtplateno.Text+".txt";
            string body = "";
            body += "Date:" + txtdate.Text + Environment.NewLine;
            body += "Subject:" + txtsubject.Text + Environment.NewLine;
            body += "Remarks:" + txtremarks.Text + Environment.NewLine;
            body += "Added By:" + Login.Fullname + Environment.NewLine;
            body += "Date Added:" + DateTime.Now.ToString() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            SalesInventorySystem.Classes.Utilities.createDirectoryFolder(@"C:\ITCSI\IRLTRUCKING\");
            if (File.Exists(filepath+file) == true)
            {
                File.AppendAllText(filepath+file, body + "\n");
             
            }
            else
            {
                StreamWriter writer = new StreamWriter(filepath + file);
                writer.Write(body);
                writer.Close();
            }
            XtraMessageBox.Show("Successfully Added!");
        }

        private void ForwardingTruckLogs_Load(object sender, EventArgs e)
        {
            string filepath = @"C:\ITCSI\IRLTRUCKING\"+txtplateno.Text+".txt";
            if (File.Exists(filepath) == true)
            {
                memoEdit1.Text = File.ReadAllText(filepath);
                //File.ReadAllText(filepath);
            }
            else
            {
                return;
            }
            txtdate.Focus();
        }
    }
}