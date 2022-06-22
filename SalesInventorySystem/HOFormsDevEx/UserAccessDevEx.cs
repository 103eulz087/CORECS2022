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
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class UserAccessDevEx : DevExpress.XtraEditors.XtraForm
    {
        string strmenuInventory, strmenuAdmin, strmenuSales, strmenuAccounting, strmenuReporting, strmenuPayroll,strmenuHotel,strmenuForwarding,strmenucif;


        public UserAccessDevEx()
        {
            InitializeComponent();
        }

        private void searchLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            checkAccess();
        }

        private void UserAccessDevEx_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            //Database.display("select * from UserMenuAccess", gridControl1, gridView1);
            Database.displaySearchlookupEdit("SELECT UserID,FullName,Designation,AssignedBranch FROM Users", searchLookUpEdit1, "UserID", "UserID");
            loadeulz();
        }

        void checkAccess()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = "SELECT * FROM UserMenuAccess WHERE UserID='" + searchLookUpEdit1.Text + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    strmenuAdmin = reader["isAdmin"].ToString();
                    strmenuSales = reader["isSales"].ToString();
                    strmenuInventory = reader["isInventory"].ToString();
                    strmenuAccounting = reader["isAccounting"].ToString();
                    strmenuReporting = reader["isReporting"].ToString();
                    strmenuPayroll = reader["isPayroll"].ToString();
                    strmenuHotel = reader["isHotel"].ToString();
                    strmenuForwarding = reader["isForwarding"].ToString();
                    strmenucif = reader["isClientDataSheet"].ToString();
                    reader.Close();
                    readData_Menu_listbox(strmenuAdmin, adminTools_checklist);
                    readData_Menu_listbox(strmenuSales, sales_checklist);
                    readData_Menu_listbox(strmenuInventory, invntory_checklist);
                    readData_Menu_listbox(strmenuAccounting, acct_checklist);
                    readData_Menu_listbox(strmenuReporting, reporting_checklist);
                    // readData_Menu_listbox(strmenuPayroll, p);
                    readData_Menu_listbox(strmenuHotel, hotel_checklist);
                    readData_Menu_listbox(strmenuForwarding, forwarding_checklist);
                    readData_Menu_listbox(strmenucif, cif_checklist);

                }
                reader.Close();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally { con.Close(); }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            //string userid = "";
            //for(int i=0;i<=gridView1.RowCount-1;i++)
            //{
            //    userid = gridView1.GetRowCellValue(i, "UserID").ToString();
            //    Database.ExecuteQuery("UPDATE UserMenuAccess SET isAdmin='"+ gridView1.GetRowCellValue(i, "isAdmin").ToString() + "',isSales='"+ gridView1.GetRowCellValue(i, "isSales").ToString() + "',isInventory='"+ gridView1.GetRowCellValue(i, "isInventory").ToString() + "',isAccounting='"+ gridView1.GetRowCellValue(i, "isAccounting").ToString() + "',isHotel='"+ gridView1.GetRowCellValue(i, "isHotel").ToString() + "',isPayroll='"+ gridView1.GetRowCellValue(i, "isPayroll").ToString() + "',isCustomized='"+ gridView1.GetRowCellValue(i, "isCustomized").ToString() + "' where UserID='"+ userid + "' ");
            //}
            //XtraMessageBox.Show("successfully updated!");
            //this.Dispose();
            //Main m = new Main();
            //SqlConnection con = Database.getConnection();
            //con.Open();
            //string query = "select * FROM UserMenuAccess2 WHERE UserID='eulz'";
            //SqlCommand com = new SqlCommand(query, con);
            //SqlDataReader reader = com.ExecuteReader();
            //while (reader.Read())
            //{
            //    strmenuInventory = reader["isInventory"].ToString();
            //}
            //readMenu(strmenuInventory, m.InventoryPage);

            strmenuInventory=checkMenu(invntory_checklist);
            strmenuAdmin= checkMenu(adminTools_checklist);
            strmenuSales = checkMenu(sales_checklist);
            strmenuAccounting = checkMenu(acct_checklist);
            strmenuReporting = checkMenu(reporting_checklist);
            strmenuHotel = checkMenu(hotel_checklist);
            strmenuForwarding = checkMenu(forwarding_checklist);
            strmenucif = checkMenu(cif_checklist);
            if (String.IsNullOrEmpty(searchLookUpEdit1.Text))
            {
                XtraMessageBox.Show("Please Select User!");
                return;
            }
            else
            {
                Database.ExecuteQuery("DELETE FROM UserMenuAccess WHERE UserID='" + searchLookUpEdit1.Text + "'");
                Database.ExecuteQuery("INSERT INTO UserMenuAccess (UserID,isAdmin,isSales,isInventory,isAccounting,isHotel,isPayroll,isReporting,isForwarding,isClientDataSheet) VALUES ('" + searchLookUpEdit1.Text + "','" + strmenuAdmin + "','" + strmenuSales + "','" + strmenuInventory + "','" + strmenuAccounting + "','"+strmenuHotel+"',0,'" + strmenuReporting + "','"+strmenuForwarding+"','"+strmenucif+"') ", "Successfully Inserted!");
                if(Login.isglobalUserID == searchLookUpEdit1.Text)
                {
                    Application.Restart();
                }
                //else
                //{ Application.Exit(); }
               
            }
           
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void loadeulz()
        {
            Main main = new Main();
            BarItem mcurrentitem;
            adminTools_checklist.Items.Clear();
     
            foreach (RibbonPage currentPage in main.Ribbon.Pages)
            {
                foreach (RibbonPageGroup currentgroup in currentPage.Groups)
                {
                    foreach (BarItemLink currentlink in currentgroup.ItemLinks)
                    {
                        mcurrentitem = currentlink.Item;
                        string STR = currentPage.Text;
                       // if (currentPage.Text == "HOME")
                            //adminTools_checklist.Items.Add(mcurrentitem.Name);
                        if (currentPage.Text == "ADMIN TOOLS")
                            adminTools_checklist.Items.Add(mcurrentitem.Name);
                        else if (currentPage.Text == "SALES")
                            sales_checklist.Items.Add(mcurrentitem.Name);
                        else if (currentPage.Text == "INVENTORY")
                            invntory_checklist.Items.Add(mcurrentitem.Name);
                        else if (currentPage.Text == "ACCOUNTING")
                            acct_checklist.Items.Add(mcurrentitem.Name);
                        else if (currentPage.Text == "REPORTING")
                            reporting_checklist.Items.Add(mcurrentitem.Name);
                        else if (currentPage.Text == "HOTEL MANAGEMENT")
                            hotel_checklist.Items.Add(mcurrentitem.Name);
                        else if (currentPage.Text == "FORWARDING")
                            forwarding_checklist.Items.Add(mcurrentitem.Name);
                        else if (currentPage.Text == "CLIENT DATA SHEET")
                            cif_checklist.Items.Add(mcurrentitem.Name);
                    }
                }
            }
        }

        String checkMenu(CheckedListBoxControl xlist)
        {
            string strmenulist = "";
            if (xlist.SelectedItems.Count > 0)
            {
                for (int x = 0; x <= xlist.Items.Count - 1; x++)
                {
                    if (xlist.Items[x].CheckState == CheckState.Checked)
                    {
                        strmenulist = strmenulist + "|" + xlist.Items[x].ToString();
                    }
                }
            }
            if (String.IsNullOrEmpty(strmenulist))
            {
                strmenulist = "<empty>";
            }
            else
            {
                strmenulist = strmenulist + "|";
            }
            return strmenulist;
        }

        public void readData_Menu_listbox(string strMenu, DevExpress.XtraEditors.CheckedListBoxControl current_list)
        {
            string wholefile;
            string[] lineData;
            string[] fieldData;
            if ((strMenu == ""))
            {
                return;
            }
            wholefile = strMenu;
            lineData = wholefile.Split('\n');
            foreach (string lineOfText in lineData)
            {
                fieldData = lineOfText.Split('|');
                foreach (string wordOfText in fieldData)
                {
                    // Console.WriteLine(wordOfText)
                    // wordOfText
                    if ((wordOfText == ""))
                    {
                        goto outer;
                    }
                    for(int i=0;i<=current_list.Items.Count-1;i++)
                    {
                        if(current_list.Items[i].Value.ToString() == wordOfText)
                        {
                            current_list.Items[i].CheckState = CheckState.Checked;
                        }
                    }
                outer:
                    continue;
            }

                Console.WriteLine("");
            }

        }


    }
}