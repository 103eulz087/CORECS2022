using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    class Utilities:GlobalVariables
    {

        public static String readTextfile(string path)
        {
            //int counter = 0;
           // string path = "C:\\POSTransaction\\ORSeries\\"; //default value
            string line, value = "";
            string details = "counter.txt";
            string filetoprint = path + details; //C:\POSTransaction\ORSeries\counter.txt
            if (!Directory.Exists(path)) //if not exist create new file then read
            {
                StreamWriter writer;//,writer22;
                Directory.CreateDirectory(path); 

                writer = new StreamWriter(filetoprint);
                writer.Write("10000"); 
                writer.Close();

                value=readFile(filetoprint);
            }
            else
            {
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(filetoprint);
                while ((line = file.ReadLine()) != null)
                {
                    value = line;
                }
                file.Close();
            }
            return value;
        }
        public static String readTextfile(string path,string filename)
        {
            //int counter = 0;
            // string path = "C:\\POSTransaction\\ORSeries\\"; //default value
            string line, value = "";
            string details = filename;
            string filetoprint = path + details; //C:\POSTransaction\ORSeries\counter.txt
            if (!Directory.Exists(path)) //if not exist create new file then read
            {
                StreamWriter writer;//,writer22;
                Directory.CreateDirectory(path);

                writer = new StreamWriter(filetoprint);
                writer.Write("10000");
                writer.Close();

                value = readFile(filetoprint);
            }
            else
            {
                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(filetoprint);
                while ((line = file.ReadLine()) != null)
                {
                    value = line;
                }
                file.Close();
            }
            return value;
        }

       
        public static string readFile(string path)
        {
            string line,value = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                value = line;
            }
            file.Close();
            return value;
        }
        public static string readFile2(string path)
        {
            string line, value = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                value = line;
                break;
            }
            file.Close();
            return value;
        }

        //     if (!Directory.Exists(filepath))
        //        {
        //            Directory.CreateDirectory(filepath);
        //        }
        //txtorder = "\\" + PointOfSale.refno + ".txt";
        //        string filetoprint = filepath + txtorder;
        //StreamWriter writer = new StreamWriter(filepath + txtorder);
        //writer.Write(details);
        //        writer.Close();

        public static String writeTextfile(string path,string value)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.Write(value);
            writer.Close();
            return value;
        }

        public static bool checkEmptyTextBox(TextBox txt, string strMessage)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                displayMessage("Fields with (*) are compulsory. \n" + strMessage, MessageBoxIcon.Exclamation);
                txt.Focus();
                return true;
            }
            else { return false; }

        }

        //public static bool checkComboBox(ComboBox cbo, string strMessage)
        //{
        //    if (cbo.SelectedIndex == -1)
        //    {
        //        displayMessage("Fields with (*) are compulsory. \n" + strMessage, MessageBoxIcon.Exclamation);
        //        cbo.Focus();
        //        return true;
        //    }
        //    else { return false; }

        //}

        public static bool checkImage(PictureBox pic, string strMessage)
        {
            if (pic.Image == null)
            {
                displayMessage(strMessage, MessageBoxIcon.Exclamation);
                return true;
            }
            else { return false; }

        }

        public static bool validateEmail(TextBox txt, string strMessage)
        {
            if (!Regex.IsMatch(txt.Text, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$"))
            {
                displayMessage(strMessage, MessageBoxIcon.Exclamation);
                txt.Focus();
                return true;
            }
            else { return false; }
        }

        public static bool displayMessage(string strMessage)
        {
            DialogResult dialogResult = XtraMessageBox.Show(strMessage, strApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes) { return true; }
            else { return false; }
        }

        public static void displayMessage(string strMessage, MessageBoxIcon msgBoxIcon)
        {
            XtraMessageBox.Show(strMessage, strApplicationName, MessageBoxButtons.OK, msgBoxIcon);
        }

        public static bool checkPrevInstance()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1) { return true; }
            else { return false; }
        }

        public static void ClearFields(Control ctrl)
        {
            foreach (Control ctl in ctrl.Controls)
            {
                if (ctl is TextBox) { ctl.Text = string.Empty; }
                //else if (ctl is ComboBox) { ((ComboBox)ctl).SelectedIndex = -1; }
                else if (ctl is DateTimePicker) { ((DateTimePicker)ctl).Value = DateTime.Today; }
                //else if (ctl is DateTimePicker){((DateTimePicker)ctl).Checked  = false;}
                else if (ctl is PictureBox) { ((PictureBox)ctl).Image = null; }
                else if (ctl is CheckBox) { ((CheckBox)ctl).Checked = false; }
                else if (ctl is ListView) { ((ListView)ctl).Items.Clear(); }
            }
        }

      

        public static void disableFields(Control ctrl)
        {
            foreach (Control ctl in ctrl.Controls)
            {
                if (ctl is TextBox) { ((TextBox)ctl).Enabled = false; }
                //else if (ctl is ComboBox) { ((ComboBox)ctl).Enabled = false; }
                else if (ctl is DataGridView) { ((DataGridView)ctl).Enabled = false; }
                else if (ctl is DateTimePicker) { ((DateTimePicker)ctl).Enabled = false; }
                else if (ctl is Button) { ((Button)ctl).Enabled = false; }
                else if (ctl is CheckBox) { ((CheckBox)ctl).Enabled = false; }
                else if (ctl is ListView) { ((ListView)ctl).Enabled = false; }
            }
        }

        public static void enableFields(Control ctrl)
        {
            foreach (Control ctl in ctrl.Controls)
            {
                if (ctl is TextBox) { ((TextBox)ctl).Enabled = true; }
                //else if (ctl is ComboBox) { ((ComboBox)ctl).Enabled = true; }
                else if (ctl is DataGridView) { ((DataGridView)ctl).Enabled = true; }
                else if (ctl is DateTimePicker) { ((DateTimePicker)ctl).Enabled = true; }
                else if (ctl is Button) { ((Button)ctl).Enabled = true; }
                else if (ctl is CheckBox) { ((CheckBox)ctl).Enabled = true; }
                else if (ctl is ListView) { ((ListView)ctl).Enabled = true; }
            }
        }

        public static bool CopyImage(string strPictureSource, string strDestination, string strFileName)
        {
            try
            {
                if (Directory.Exists(strDestination))
                {
                    File.Copy(strPictureSource, strDestination + @"\" + strFileName, false);
                }
                else
                {
                    Directory.CreateDirectory(strDestination);
                    File.Copy(strPictureSource, strDestination + @"\" + strFileName, false);
                }
                return true;
            }
            catch (Exception exc)
            {
                XtraMessageBox.Show(exc.Message.ToString());
                return false;
            }
        }

        public static void DeleteImage(string strDestination, string strFileName)
        {
            if (Directory.Exists(strDestination))
            {
                File.Delete(strDestination + @"\" + strFileName);
            }
        }


        public static byte[] GetImage(PictureBox pictureBox)
        {
            MemoryStream memoryStream = new MemoryStream();
            pictureBox.Image.Save(memoryStream, pictureBox.Image.RawFormat);
            byte[] byteImage = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(byteImage, 0, Convert.ToInt32(memoryStream.Length));
            memoryStream.Close();
            return byteImage;
        }

        public static byte[] GetImage(PictureBox pictureBox1, string tableName,string condition,string ImageColName)
        {
            byte[] img = null;
            pictureBox1.Image = null;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "select * FROM "+tableName+" WHERE "+condition+" ";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (reader[ImageColName] == System.DBNull.Value)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            img = (byte[])reader[ImageColName];
                            MemoryStream ms = new MemoryStream(img);
                            ms.Seek(0, SeekOrigin.Begin);
                            pictureBox1.Image = Image.FromStream(ms);
                        }

                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return img;
        }
        public static byte[] GetImage(PictureBox pictureBox1, string tableName, string condition, string ImageColName,SqlConnection con)
        {
            byte[] img = null;
            pictureBox1.Image = null;
            con.Open();
            try
            {
                string query = "select * FROM " + tableName + " WHERE " + condition + " ";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (reader[ImageColName] == System.DBNull.Value)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            img = (byte[])reader[ImageColName];
                            MemoryStream ms = new MemoryStream(img);
                            ms.Seek(0, SeekOrigin.Begin);
                            pictureBox1.Image = Image.FromStream(ms);
                        }

                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return img;
        }
        public static byte[] GetImageDevEx(XRPictureBox pictureBox1, string tableName, string condition, string ImageColName)
        {
            byte[] img = null;
            pictureBox1.Image = null;
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "select * FROM " + tableName + " WHERE " + condition + " ";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (reader[ImageColName] == System.DBNull.Value)
                        {
                            pictureBox1.Image = null;
                        }
                        else
                        {
                            img = (byte[])reader[ImageColName];
                            MemoryStream ms = new MemoryStream(img);
                            ms.Seek(0, SeekOrigin.Begin);
                            pictureBox1.Image = Image.FromStream(ms);
                        }

                    }
                }
                else
                {
                    pictureBox1.Image = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
            return img;
        }


        public static void loadImage(PictureBox pictureBox, byte[] byteImage)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream(byteImage);
                pictureBox.Image = Image.FromStream(memoryStream);
                //pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
                //displayMessage(ex.Message, MessageBoxIcon.Error); 
            }
        }

        //public void GetItem(ComboBox cbo, int intID)
        //{
        //    foreach (cls_LIST_ITEM cls_LIST_ITEM_obj in cbo.Items)
        //    {
        //        if (cls_LIST_ITEM_obj.ListID == intID)
        //        {
        //            cbo.SelectedItem = cls_LIST_ITEM_obj;
        //            break;
        //        }
        //    }
        //}


        public static void uncheckLvwItem(ListView lvw)
        {
            foreach (ListViewItem lvwItem in lvw.Items)
            {
                lvwItem.Checked = false;
            }
        }
        public static String sequencePadding(string str)
        {
            string isnum = "";
            //  string str = IDGenerator.getSequenceNumber().ToString();
            if (str.Length == 1)
            {
                isnum = "000" + str;
            }
            else if (str.Length == 2)
            {
                isnum = "00" + str;
            }
            else if (str.Length == 3)
            {
                isnum = "0" + str;
            }
            return isnum;
        }

        public static string IntegerToWords(long inputNum)
        {
            int dig1, dig2, dig3, level = 0, lasttwo, threeDigits;

            string retval = "";
            string x = "";
            string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tens = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] thou = { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion" };

            bool isNegative = false;
            if (inputNum < 0)
            {
                isNegative = true;
                inputNum *= -1;
            }

            if (inputNum == 0)
                return ("zero");

            string s = inputNum.ToString();

            while (s.Length > 0)
            {
                // Get the three rightmost characters
                x = (s.Length < 3) ? s : s.Substring(s.Length - 3, 3);

                // Separate the three digits
                threeDigits = int.Parse(x);
                lasttwo = threeDigits % 100;
                dig1 = threeDigits / 100;
                dig2 = lasttwo / 10;
                dig3 = (threeDigits % 10);

                // append a "thousand" where appropriate
                if (level > 0 && dig1 + dig2 + dig3 > 0)
                {
                    retval = thou[level] + " " + retval;
                    retval = retval.Trim();
                }

                // check that the last two digits is not a zero
                if (lasttwo > 0)
                {
                    if (lasttwo < 20) // if less than 20, use "ones" only
                        retval = ones[lasttwo] + " " + retval;
                    else // otherwise, use both "tens" and "ones" array
                        retval = tens[dig2] + " " + ones[dig3] + " " + retval;
                }

                // if a hundreds part is there, translate it
                if (dig1 > 0)
                    retval = ones[dig1] + " hundred " + retval;

                s = (s.Length - 3) > 0 ? s.Substring(0, s.Length - 3) : "";
                level++;
            }

            while (retval.IndexOf("  ") > 0)
                retval = retval.Replace("  ", " ");

            retval = retval.Trim();

            if (isNegative)
                retval = "negative " + retval;

            return (retval);
        }

        public static string getComputerName()
        {
            return Environment.MachineName;
        }

        public static int CentimeterToPixel(Control ctrl ,double Centimeter)
        {
            double pixel = -1;
            using (Graphics g = ctrl.CreateGraphics())
            {
                pixel = Centimeter * g.DpiY / 2.54d;
            }
            return (int)pixel;
        }

        public static void setDate(string dateFrom,string dateTo)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            dateFrom = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateTo = lastDay.ToShortDateString();
        }

        public static void setMinimumDateDevEx(string date, DevExpress.XtraEditors.DateEdit edit)
        {
            DateTime oDate = Convert.ToDateTime(date);
            edit.Properties.MinValue = oDate;
        }

        public static void createDirectoryFolder(string filepath)
        {
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
        }
    }
}
