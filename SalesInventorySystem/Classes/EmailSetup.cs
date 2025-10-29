using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    
    public class EmailSetup
    {
        NetworkCredential login;
        SmtpClient client;
        MailMessage msg;

       
        public void setupEmailParam(string subject, string body, string filepath)
        {
            try
            {
                //string filepath = @"C:\\POSTransaction\\DailySales\\" + Login.Fullname + "\\TransactionSummary\\" + PointOfSale.transcode + "_E-JOURNAL.txt";
                //filepath = @"C:\\POSTransaction\\DailySales\\" + "000000000000000001.txt";
                string username = Database.getSingleQuery("EmailServer", "Description <> ''", "Description");
                string password = Database.getSingleQuery("EmailServer", "Description <> ''", "Password");
                string smtp = Database.getSingleQuery("EmailServer", "Description <> ''", "SmtpClient");
                string port = Database.getSingleQuery("EmailServer", "Description <> ''", "Port");
                ArrayList list_emails = new ArrayList();
                int i = 0;
                string email = "";
                // string subjectEmail = "ENZO REPORT GENERATOR";;
                SqlConnection sqlConnection1 = Database.getConnection();//Database.getCustomizeConnection();
                sqlConnection1.Open(); //connection to the database.
                SqlCommand cmd_Email = new SqlCommand("Select EmailAddress from EmailAddresses", sqlConnection1);
                SqlDataReader read_Email = cmd_Email.ExecuteReader();
                while (read_Email.Read())
                {
                    // email = read_Email.GetValue(i).ToString();
                    email = read_Email.GetValue(i).ToString();
                    list_emails.Add(email); //Add email to a arraylist
                    i = i + 1 - 1; //increment or ++i
                }
                read_Email.Close();
                sqlConnection1.Close(); //Close connection

                login = new NetworkCredential(username, password);
                client = new SmtpClient(smtp);
                client.Port = Convert.ToInt32(port);
                client.EnableSsl = true;
                client.Credentials = login;

                msg = new MailMessage { From = new MailAddress("eulzreportservices@gmail.com", "EULZ REPORT GENERATOR", Encoding.UTF8) };
               
                foreach (string email_to in list_emails)
                {
                    msg.To.Add(new MailAddress(email_to));
                }
                msg.Attachments.Add(new Attachment(filepath));
                msg.Subject = subject;
                msg.Body = body;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);
                client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);
                string userstate = "Sending...";
                client.SendAsync(msg, userstate);
            }
            catch (SmtpException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }
        
        public void setupEmailParam(string subject,string body,bool isattachment=false)
        {
            try
            {
                string filepath = String.Empty;
                if (isattachment == true)
                {
                    //filepath = @"C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\" + POSClosedTransaction.cashiertranscode + "_E-JOURNAL.txt";
                    filepath = @"C:\\ENDOFDAY_INVENTORY_REPORTS\\" + "028_1_16_2020" + ".xls";
                    Utilities.createDirectoryFolder(filepath);
                }
                var rowx = Database.getMultipleQuery("EmailServer", "Description <> ''", "Description,Password,SmtpClient,Port,SubjectTitle");
                string username = rowx["Description"].ToString();
                string password =   rowx["Password"].ToString();
                string smtp =       rowx["SmtpClient"].ToString();
                string port =       rowx["Port"].ToString();
                string subj =       rowx["SubjectTitle"].ToString();
                ArrayList list_emails = new ArrayList();
                int i = 0;
                string email = "";
                // string subjectEmail = "ENZO REPORT GENERATOR";
                SqlConnection sqlConnection1 = Database.getConnection();
                sqlConnection1.Open(); //connection to the database.
                SqlCommand cmd_Email = new SqlCommand("Select EmailAddress from dbo.EmailAddresses", sqlConnection1);
                SqlDataReader read_Email = cmd_Email.ExecuteReader();
                while (read_Email.Read())
                {
                    // email = read_Email.GetValue(i).ToString();
                    email = read_Email.GetValue(i).ToString();
                    list_emails.Add(email); //Add email to a arraylist
                    i = i + 1 - 1; //increment or ++i
                }
                read_Email.Close();
                sqlConnection1.Close(); //Close connection

                login = new NetworkCredential(username, password);
                client = new SmtpClient(smtp);
                client.Port = Convert.ToInt32(port);
                client.EnableSsl = true;
                client.Credentials = login;

                //-------
                //client = new SmtpClient(smtp);
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = false;
                //client.Credentials = new NetworkCredential(username, password);
                //client.Host = "smtp.gmail.com";
                //client.Port = 587;
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //-------


                //SmtpClient client = new SmtpClient();
                //client.Host = "smtp.gmail.com";
                //client.Port = Convert.ToInt32(port); 
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = false;
                //var credentials = new System.Net.NetworkCredential(username, password);
                //client.Credentials = credentials;

                // client.UseDefaultCredentials = true;
                //client.Send(message);


                msg = new MailMessage { From = new MailAddress("eulzreportservices@gmail.com", subj, Encoding.UTF8) };

                foreach (string email_to in list_emails)
                {
                    msg.To.Add(new MailAddress(email_to));
                }
                if (isattachment == true)
                {
                    msg.Attachments.Add(new Attachment(filepath));
                }
               
                msg.Subject = subject;
                msg.Body = body;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);
                client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);
                string userstate = "Sending...";
                client.SendAsync(msg, userstate);
            }
            catch(SmtpException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        public void setupEmailParamWithAttachment(string subject, string body, string fpath,bool isattachment,string emailaddressclient)
        {
            try
            {
                string filepath = fpath;//String.Empty;
                var rowx = Database.getMultipleQuery("EmailServer", "Description <> ''", "Description,Password,SmtpClient,Port,SubjectTitle");
                string username = rowx["Description"].ToString();
                string password = rowx["Password"].ToString();
                string smtp = rowx["SmtpClient"].ToString();
                string port = rowx["Port"].ToString();
                string subj = rowx["SubjectTitle"].ToString();
                ArrayList list_emails = new ArrayList();
                int i = 0;
                string email = "";
                // string subjectEmail = "ENZO REPORT GENERATOR";
                SqlConnection sqlConnection1 = Database.getConnection();
                sqlConnection1.Open(); //connection to the database.
                SqlCommand cmd_Email = new SqlCommand("Select EmailAddress from dbo.EmailAddresses", sqlConnection1);
                SqlDataReader read_Email = cmd_Email.ExecuteReader();
                while (read_Email.Read())
                {
                    // email = read_Email.GetValue(i).ToString();
                    email = read_Email.GetValue(i).ToString();
                    list_emails.Add(email); //Add email to a arraylist
                    i = i + 1 - 1; //increment or ++i
                }
                read_Email.Close();
                sqlConnection1.Close(); //Close connection

                login = new NetworkCredential(username, password);
                client = new SmtpClient(smtp);
                client.Port = Convert.ToInt32(port);
                client.EnableSsl = true;
                client.Credentials = login;


                string htmlBody = "<html><body style='font-family: monospace; white-space: pre;'>"
                                + WebUtility.HtmlEncode(body)
                                + "</body></html>";


                msg = new MailMessage { From = new MailAddress("eulzreportservices@gmail.com", subj, Encoding.UTF8) };

                string[] emailArray = emailaddressclient.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                string emailRegexPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                foreach (string clientEmail in emailArray)
                {
                    string trimmedEmail = clientEmail.Trim();
                    if (!string.IsNullOrWhiteSpace(trimmedEmail))
                    {
                        // 3. Optional: Validate the format before adding
                        if (Regex.IsMatch(trimmedEmail, emailRegexPattern))
                        {
                            try
                            {
                                // Add the valid email address to the 'To' collection
                                msg.To.Add(trimmedEmail);
                            }
                            catch (FormatException)
                            {
                                // Handle cases where the format is technically invalid 
                                // despite the basic regex check (e.g., domain too long)
                                // You might log this or show an error to the user
                                System.Diagnostics.Debug.WriteLine($"Error: Invalid email format for: {trimmedEmail}");
                            }
                        }
                        else
                        {
                            // Handle addresses that don't match the regex (e.g., log them)
                            System.Diagnostics.Debug.WriteLine($"Warning: Email address '{trimmedEmail}' failed regex validation.");
                        }
                    }
                }
                //msg.To.Add(emailaddressclient);
                foreach (string email_to in list_emails)
                {
                    msg.To.Add(new MailAddress(email_to));
                }
                if (isattachment == true)
                {
                    msg.Attachments.Add(new Attachment(filepath));
                }

                msg.Subject = subject;
                msg.Body = htmlBody;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);
                client.SendCompleted += new SendCompletedEventHandler(Client_SendCompleted);
                string userstate = "Sending...";
                client.SendAsync(msg, userstate);
            }
            catch (SmtpException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                XtraMessageBox.Show(String.Format("{0} send canceled.", e.UserState), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (e.Error != null)
                XtraMessageBox.Show(String.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                XtraMessageBox.Show("Your message successfully sent", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
