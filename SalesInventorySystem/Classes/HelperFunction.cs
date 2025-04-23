using AForge.Video.DirectShow;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace SalesInventorySystem
{
    class HelperFunction
    {
        static int papersize = 38;
        //static int papersize = 27;
        static int cornerlength = 0;

       

        public static string convertToNumericFormat(double value)
        {
            string str="";
            str = String.Format("{0:0,0.00}", value); 
            return str;
        }

        public static string numericFormat(double value)
        {
            string str = "";
            str = String.Format("{0:n2}", value);
            return str;
        }

        public static bool ConfirmDialog(string msg, string title)
        {
            DialogResult dr = XtraMessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        
        public static void HideGrid(DataGridView grid, String[] col)
        {
            foreach (string var in col)
            {
                for (int i = 0; i <= grid.ColumnCount - 1; i++)
                {
                    if (var == grid.Columns[i].Name)
                    {
                        grid.Columns[i].Visible = false;
                    }
                }
            }
            
        }

        public static String getMacAddress()
        {
            string macaddress = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet)
                    continue;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macaddress += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macaddress;
        }

        

        public static WinControlContainer CopyGridControl(GridControl grid)
        {

            // Create a WinControlContainer object.
            WinControlContainer winContainer = new WinControlContainer();
            // Set its location and size.
            winContainer.Location = new Point(0, 0);
            winContainer.Size = new Size(200, 100);
            // Set the grid as a wrapped object.
            winContainer.WinControl = grid;
            return winContainer;
        }
        public static WinControlContainer CopyGridControl(GridControl grid,GridView view,String col)
        {

            // Create a WinControlContainer object.
            WinControlContainer winContainer = new WinControlContainer();
            // Set its location and size.
            winContainer.Location = new Point(0, 0);
            winContainer.Size = new Size(200, 100);
            for(int i=0;i<=view.RowCount-1;i++)
            {
                if (view.GetRowCellValue(i, col).ToString() == "True")
                {
                    winContainer.WinControl = grid;
                }
            }
           
            // Set the grid as a wrapped object.
            
            return winContainer;
        }
        public static WinControlContainer CopyGridControlCredMemo(GridControl grid, GridView view, String col)
        {

            // Create a WinControlContainer object.
            WinControlContainer winContainer = new WinControlContainer();
            // Set its location and size.
            winContainer.Location = new Point(0, 0);
            winContainer.Size = new Size(200, 100);
            for (int i = 0; i <= view.RowCount - 1; i++)
            {
                if (Convert.ToDouble(view.GetRowCellValue(i, col).ToString()) > 0)
                {
                    winContainer.WinControl = grid;
                }
            }

            // Set the grid as a wrapped object.

            return winContainer;
        }

        public static WinControlContainer CopyGridControl(GridControl grid,Point point)
        {

            // Create a WinControlContainer object.
            WinControlContainer winContainer = new WinControlContainer();
            // Set its location and size.
            winContainer.Location = point;
            winContainer.Size = new Size(200, 100);
            // Set the grid as a wrapped object.
            winContainer.WinControl = grid;
            return winContainer;
        }

        public static WinControlContainer CopyDataGridControl(DataGridView grid)
        {

            // Create a WinControlContainer object.
            WinControlContainer winContainer = new WinControlContainer();
            // Set its location and size.
            winContainer.Location = new Point(0, 0);
            winContainer.Size = new Size(200, 100);
            // Set the grid as a wrapped object.
            winContainer.WinControl = grid;
            return winContainer;
        }

        public static DataGridView colviewAlignmentGrid(DataGridView view, string[] col, DataGridViewContentAlignment colalignment)
        {
            foreach(string val in col)
            {
                for(int i=0;i<=view.ColumnCount-1;i++)
                {
                    if(val == view.Columns[i].Name)
                    {
                        view.Columns[i].DefaultCellStyle.Alignment = colalignment;
                        view.Columns[i].HeaderCell.Style.Alignment = colalignment;
                    }
                }
            }
            return view;
        }

        public static String PrintSpaceLine()
        {
            string spaceline = "";
            for(int i=0;i<=papersize-(cornerlength*2);i++)
            {
                spaceline+="-";
            }
            return PrintGetSpace(cornerlength)+spaceline;
        }

        public static String PrintCenterText(String align)
        {
            String str = "";
            for (int i = 0; i <= (papersize / 2) - (align.Length / 2); i++)
            {
                str = str + " ";
            }
            return str + align;
        }

        public static String PrintLeftText(String value)
        {
            return PrintGetSpace(cornerlength) + value;
        }
      
        public static String PrintLeftRigthText(String value_left,String value_right)
        {
            string str="";
            if(value_left.Length > (papersize-value_right.Length))
            {
                value_left=Split1(value_left)+"..";
            }
            int a = (papersize-((cornerlength*2)+value_left.Length))-value_right.Length;
            str = PrintGetSpace(cornerlength)+value_left+PrintGetSpace(a)+value_right;
            return str;
        }
        public static String AlignToCenter(String align)
        {
            string space = "";
            for (int i = 0; i <= (40 / 2) - (align.Length / 2); i++)
            {
                space = space + " ";
            }
            return space + align;
        }

        public static String AlignToRight(String align)
        {
            string space = "";
            for (int i = 0; i <= (40) - (align.Length); i++)
            {
                space = space + " ";
            }
            return space + align;
        }

        public static String createDottedLine()
        {
            string dottedline = "";
            for(int i=0;i<=(papersize)-(cornerlength*2);i++)
            {
                dottedline = dottedline+"-";
            }
            return PrintGetSpace(cornerlength)+dottedline;
        }

        public static String createAsteriskLine()
        {
            string dottedline = "";
            for (int i = 0; i <= (papersize) - (cornerlength * 2); i++)
            {
                dottedline = dottedline + "*";
            }
            return PrintGetSpace(cornerlength) + dottedline;
        }

        public static String createEqualLine()
        {
            string dottedline = "";
            for (int i = 0; i <= (papersize) - (cornerlength * 2); i++)
            {
                dottedline = dottedline + "=";
            }
            return PrintGetSpace(cornerlength) + dottedline;
        }
         

        public static String PrintGetSpace(int val)
        {
            string space="";
            for(int i=0;i<=val;i++)
            {
                space = space+" ";
            }
            return space;
        }

        public static void OpenDrawer()
        {
            //string DrawerCode = Strings.Chr(27) + Strings.Chr(112) + Strings.Chr(48) + Strings.Chr(64) + Strings.Chr(64);
            String command = "/C print /d:LPT1: " + (Char)27 + (Char)112 + (Char)48 + (Char)64 + (Char)64;
            //String command= "/C print /d:LPT1: " + (Char)27 + (Char)112 + (Char)0 + (Char)25 + (Char)250;
            ProcessStartInfo application =  new ProcessStartInfo("cmd.exe",command);
            application.WindowStyle = ProcessWindowStyle.Hidden;
            Process process = Process.Start(application);
            process.WaitForExit();
            process.Close();
        }

        public static string PrinttoRight(string righttext)
        {
            string str = "";
            for (int i = 0; i <= (39 - righttext.Length); i++)
            {
                str += " ";
            }
            return str + righttext;
        }

        public static String PrintRightToLeft(string leftstr,string rightstr)
        {
            string str="";
            if(leftstr.Length > (40-rightstr.Length))
            {
                leftstr = Split(leftstr) + "..";
            }
            int dob = (40 - ((0 * 2) + leftstr.Length)) - rightstr.Length;
        //   int ctr = (40 - (leftstr.Length))-rightstr.Length;
            str = PrintGetSpace(0) + leftstr + PrintGetSpace(dob) + rightstr;
         //   return str;
            int a = 32 - leftstr.Length;
            return leftstr + PrintGetSpace(a) + rightstr;
        }

        public static String PrintRightToMiddle(string leftstr, string rightstr)
        {
            string str = "";
            if (leftstr.Length > (40 - rightstr.Length))
            {
                leftstr = Split(leftstr) + "..";
            }
            int dob = (40 - ((0 * 2) + leftstr.Length)) - rightstr.Length;
            //   int ctr = (40 - (leftstr.Length))-rightstr.Length;
            str = PrintGetSpace(0) + leftstr + PrintGetSpace(dob) + rightstr;
            //   return str;
            int a = 24 - leftstr.Length;
            return leftstr + PrintGetSpace(a) + rightstr;
        }
        
        public  static String  Split1(String expression)
        {
          
            string str = expression;
            str = str.Remove(papersize-(18),str.Length-(papersize-(18)));
            return str;
        }

        public static String Split(string str)
        {
            string str1 = str;
            int a = 40 - Convert.ToInt32((40 / 4.25));
            int b = 40 - a;
            int c = str1.Length - b;
            str1 = str1.Remove(40 - (40 / 4), str1.Length - (40 - (40 / 4)));
            //str1 = str1.Remove((int)(40- (40 / 4.25)), (int)(str1.Length-(40-(40/4.25))));
            //str1 = str1.Remove(a,c);
            return str1;
        }

        public static String LastPagePaper()
        {
            string lastpage = "";
            lastpage = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + (Char)27 + "i";
            return lastpage;
        }

        public static void DisableTextFields(Control con)
        {
            foreach(Control c in con.Controls)
            {
                if(c is TextBox)
                {
                    ((TextBox)c).Enabled = false;
                }
                DisableTextFields(c);
            }
        }

        public static void DisableCheckbox(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Enabled = false;
                }
                DisableCheckbox(c);
            }
        }

        public static void EnableCheckbox(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Enabled = true;
                }
                EnableCheckbox(c);
            }
        }
        public static void EnableDevCheckbox(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is CheckBox)
                {
                    ((CheckBox)c).Enabled = true;
                }
                EnableCheckbox(c);
            }
        }

        public static void EnableTextFields(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Enabled = true;
                }
                EnableTextFields(c);
            }
        }
        public static void EnableDevExTextFields(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                {
                    ((TextEdit)c).Enabled = true;
                }
                EnableDevExTextFields(c);
            }
        }

        public static void isEnableAlpha(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void isEnableAlphaWithDecimal(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public static void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                else if (c is RichTextBox)
                {
                    ((RichTextBox)c).Clear();
                }
                //else if (c is ComboBox)
                //{
                //    ((ComboBox)c).Text = "";
                //}
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                else
                {
                    ClearAllText(c);
                }
            }
        }

        public static Boolean isTextfieldEmpty(params TextEdit[] edit)
        {
            foreach (TextEdit txtedit in edit)
            {
                if (txtedit.Text.Trim().Equals(""))
                    return true;
            }
            return false;
        }

        public static Boolean isTextBoxEmpty(params TextBox[] edit)
        {
            foreach (TextBox txtedit in edit)
            {
                if (txtedit.Text.Trim().Equals(""))
                    return true;
            }
            return false;
        }

        public static Boolean isDatefieldEmpty(params DateEdit[] edit)
        {
            foreach (DateEdit txtedit in edit)
            {
                if (txtedit.Text.Trim().Equals(""))
                    return true;
            }
            return false;
        }

        public static Boolean isComboBoxEmpty(params ComboBoxEdit[] edit)
        {
            foreach (ComboBoxEdit txtedit in edit)
            {
                if (txtedit.Text.Trim().Equals(""))
                    return true;
            }
            return false;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        public static GridView getTotalSum(string var)
        {
            GridView views =new GridView();
            views.Columns[var].Summary.Add(DevExpress.Data.SummaryItemType.Sum, var, "{0:n2}");
            return views;
        }

        public static GridView displayFooter(string var,SummaryItemType type)
        {
            GridView views = new GridView();
            views.Columns[var].Summary.Add(type, var, "{0:n2}");
            return views;
        }


        public static String NumWords(double n) //converts double to words
        {
            string[] numbersArr = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensArr = new string[] { "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] suffixesArr = new string[] { "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
            string words = "";

            bool tens = false;

            if (n < 0)
            {
                words += "negative ";
                n *= -1;
            }

            int power = (suffixesArr.Length + 1) * 3;

            while (power > 3)
            {
                double pow = Math.Pow(10, power);
                if (n >= pow)
                {
                    if (n % pow > 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + ", ";
                    }
                    else if (n % pow == 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                    }
                    n %= pow;
                }
                power -= 3;
            }
            if (n >= 1000)
            {
                if (n % 1000 > 0) words += NumWords(Math.Floor(n / 1000)) + " thousand, ";
                else words += NumWords(Math.Floor(n / 1000)) + " thousand";
                n %= 1000;
            }
            if (0 <= n && n <= 999)
            {
                if ((int)n / 100 > 0)
                {
                    words += NumWords(Math.Floor(n / 100)) + " hundred";
                    n %= 100;
                }
                if ((int)n / 10 > 1)
                {
                    if (words != "")
                        words += " ";
                    words += tensArr[(int)n / 10 - 2];
                    tens = true;
                    n %= 10;
                }

                if (n < 20 && n > 0)
                {
                    if (words != "" && tens == false)
                        words += " ";
                    words += (tens ? "-" + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                    n -= Math.Floor(n);
                }
            }

            return words;

        }
        public static String sequencePadding(string reference)
        {
            string isnum = "";
            string str = reference;
            if (str.Length == 1)
            {
                isnum = "00000" + str;
            }
            else if (str.Length == 2)
            {
                isnum = "0000" + str;
            }
            else if (str.Length == 3)
            {
                isnum = "000" + str;
            }
            else if (str.Length == 4)
            {
                isnum = "00" + str;
            }
            else if (str.Length == 5)
            {
                isnum = "0" + str;
            }
            return isnum;
        }
        public static String sequencePadding1(string reference)
        {
            string isnum = "";
            string str = reference;
            int max = 18;
            for(int i=1;i<=max-str.Length-1;i++)
            {
                isnum = isnum + "0";
            }
         
            return isnum+str;
        }
        public static String sequencePadding1(string reference,int max)
        {
            string isnum = "";
            string str = reference;
            for (int i = 0; i <= max - str.Length - 1; i++)
            {
                isnum = isnum + "0";
            }

            return isnum + str;
        }


        public static String getDevices()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            string val = "";
            foreach (FilterInfo device in videoDevices)
            {
                val = device.Name;
            }
            VideoCaptureDevice videoSource = new VideoCaptureDevice();
            return val;
        }

        public static void embedToJournal(string filepath,string filepathJournal,string cashiertranscode)
        {
            string details = String.Empty;
            var fileContent = string.Empty;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            using (var streamReader = new StreamReader(filepath, Encoding.UTF8))
            {
                fileContent = File.ReadAllText(filepath); //copy this or file
            }
            string txtorder = "\\" + cashiertranscode + "_E-JOURNAL.txt";
            string filetoprint = filepathJournal + txtorder;
            StreamWriter writer;//,writer22;

            if (!Directory.Exists(filepathJournal))
            {
                Directory.CreateDirectory(filepathJournal);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(fileContent);
            writer.Close();
        }
        public static void embedToJournal(string filepath, string filepathJournal, string cashiertranscode, string replaceChar)
        {
            string details = String.Empty;
            var fileContent = string.Empty;
            using (var streamReader = new StreamReader(filepath, Encoding.UTF8))
            {
                fileContent = File.ReadAllText(filepath).Replace("*", replaceChar); //copy this or file
            }

            string txtorder = "\\" + cashiertranscode + "_E-JOURNAL.txt";
            string filetoprint = filepathJournal + txtorder;
            StreamWriter writer;//,writer22;

            if (!Directory.Exists(filepathJournal))
            {
                Directory.CreateDirectory(filepathJournal);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(fileContent);
            writer.Close();
        }

        public static void embedToJournal1(string filepath, string filepathJournal, string cashiertranscode, string replaceChar,string transcode)
        {
            string details = String.Empty;
            var fileContent = string.Empty;
            using (var streamReader = new StreamReader(filepath, Encoding.UTF8))
            {
                //fileContent = File.ReadAllText(filepath).Replace("*", replaceChar); //copy this or file
                fileContent = File.ReadAllText(filepath);
                if(fileContent.Contains("*"))
                {
                    fileContent = fileContent.Replace("*",replaceChar);
                }
                if(fileContent.Contains("$$$$$$"))
                {
                    fileContent = fileContent.Replace("$$$$$$", transcode);
                }
                //foreach (string line in File.ReadLines(filepath, Encoding.UTF8))
                //{fileContent
                //    // process the line
                //    if(line.Contains("$"))
                //    {
                //        fileContent += line.Replace("$", transcode);
                //    }
                //    if(line.Contains("*"))
                //    {
                //        fileContent += line.Replace("*", replaceChar);
                //    }
                //}
            }

            string txtorder = "\\" + cashiertranscode + "_E-JOURNAL.txt";
            string filetoprint = filepathJournal + txtorder;
            StreamWriter writer;//,writer22;

            if (!Directory.Exists(filepathJournal))
            {
                Directory.CreateDirectory(filepathJournal);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(fileContent);
            writer.Close();
        }

        public static String decimalParser(string value)
        {
            string strquantity = "";
            Decimal qty = Decimal.Parse(value);
            strquantity = String.Format("{0:00.000}", qty);
            return strquantity;
        }
        public static String decimalParser(string value,string pattern)
        {
            string strquantity = "";
            Decimal qty = Decimal.Parse(value);
            strquantity = String.Format(pattern, qty);
            return strquantity;
        }

        public static void exporttoexcel(GridView view, string title)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {

                string filepath = "C:\\MyFiles\\";
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles folder");
            }
        }
        public static void exporttoexcel(GridView view, string title,string filepath)
        {
            if (view.RowCount == 0)
            {
                XtraMessageBox.Show("No Data to Import!..");
                return;
            }
            else
            {
                Classes.Utilities.createDirectoryFolder(filepath);
                string filename = title + ".xls";
                string file = filepath + filename;
                view.ExportToXls(file);
                XtraMessageBox.Show("Successfully Exported.. Please Check your Drive C://MyFiles folder");
            }
        }

        public static void setDate(string datefrom,string dateto)
        {
            DateTime now = DateTime.Now;
            DateTime date = new DateTime(now.Year, now.Month, 1);
            datefrom = date.ToShortDateString();
            var now2 = DateTime.Now;
            var startOfMonth = new DateTime(now2.Year, now2.Month, 1);
            var DaysInMonth = DateTime.DaysInMonth(now2.Year, now2.Month);
            var lastDay = new DateTime(now2.Year, now2.Month, DaysInMonth);
            dateto = lastDay.ToShortDateString();
        }
        //Decimal qty = Decimal.Parse(txtqty.Text);
        //string strquantity = String.Format("{0:00.000}", qty);

        //public void ExtractZipFile(string filename)
        //{
        //    string downloadLocation = Application.StartupPath.ToString() + "UpdateVersion" + filename;
        //    string extractlocation = System.IO.Path.GetTempPath() + "SalesInventoryUpdate";
        //    try
        //    {
        //        if (System.IO.Directory.Exists(extractlocation))
        //            System.IO.File.Delete(extractlocation);
        //        if (!System.IO.Directory.Exists(extractlocation))
        //        {
        //            System.IO.Directory.CreateDirectory(extractlocation);
        //        }
        //        ICSharpCode.Sha
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}

    }
}
