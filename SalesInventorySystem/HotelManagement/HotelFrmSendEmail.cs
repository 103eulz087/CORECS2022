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
using System.Data.SqlClient;
using System.Net.Mail;
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmSendEmail : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmSendEmail()
        {
            InitializeComponent();
        }

        private void HotelFrmSendEmail_Load(object sender, EventArgs e)
        {
            
        }

        void populate()
        {
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            string query = "SELECT * FROM GuestInfo";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                txtto.Text += reader["EmailAddress"].ToString() + ',';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            //mail.From = new MailAddress("avancena.eulz@gmail.com");
            //mail.To.Add(txtto.Text);
            //mail.Subject = txtsubject.Text;
            //mail.Body = txtmessage.Text;

            //SmtpServer.Port = 587;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("avancena.eulz@gmail.com", "103eulz087M@fi");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);
            //XtraMessageBox.Show("mail Send");
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("avancena.eulz@gmail.com");
            mail.To.Add(txtto.Text);
            mail.Subject = txtsubject.Text;
            mail.Body = txtmessage.Text;

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment("your attachment file");
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("avancena.eulz@gmail.com", "103eulz087M@fi");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            XtraMessageBox.Show("mail Send");
        }



        void sendEmail()
        {
            try
            {
                string subject = "", body = "";

                body += txtmessage.Text;

                subject = txtsubject.Text;
                EmailSetup mailsetup = new EmailSetup();
                mailsetup.setupEmailParam(subject, body);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                populate();
            }else
            {
                txtto.Text = "";
            }
        }
    }
}