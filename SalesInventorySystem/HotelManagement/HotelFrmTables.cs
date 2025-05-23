﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmTables : Form
    {
        List<Button> UserBtns = new List<Button>();
        public static string buttonname = "";
        public static string buttonname1 = "";
        public static string existingor="",status="";
        public static bool isdone = false,iseditOrder=false;
        public static string waiterid = "";
        public HotelFrmTables()
        {
            InitializeComponent();
        }

        private void HotelFrmTables_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            string query = "SELECT * FROM RestaurantTable";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            flowLayoutPanel1.Controls.Clear();
            while (reader.Read())
            {
                Button btn = new Button();
                string stat = reader.GetValue(3).ToString();
                if(stat=="Occupied")
                {
                    btn.BackgroundImage = global::SalesInventorySystem.Properties.Resources.resnotavailable;
                }
                else
                {
                    btn.BackgroundImage = global::SalesInventorySystem.Properties.Resources.resavailable;
                }
                
                btn.Text = reader.GetValue(1).ToString();
                btn.TextAlign = ContentAlignment.TopCenter;
               
                btn.FlatStyle = FlatStyle.Flat;
                btn.Margin = new Padding(10, 10, 10, 10);

                btn.Font = new System.Drawing.Font("Arial Black", 10.25F);
                btn.ForeColor = System.Drawing.Color.DodgerBlue;
                btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                btn.FlatAppearance.BorderSize = 0;
                btn.Width = 150;
                btn.Height = 90;
                UserBtns.Add(btn);
                flowLayoutPanel1.Controls.Add(btn);
                btn.Click += this.Button2_Click;
            }
        }

        private void Button2_Click(System.Object sender,System.EventArgs e)
        {
            buttonname = (sender as Button).Text; //Name sa button which is ang TableNumber
            
            string status = Database.getSingleQuery("RestaurantTable", "TableNo='"+buttonname+"'", "TableStatus",Database.getCustomizeConnection());
             if(status=="Occupied")
            {
                bool ok = HelperFunction.ConfirmDialog("This Table is Already Occupied.. But if you want to Add Order in this Table, Please Select YES otherwise Cancel", "Warning");

                if (ok)
                {
                    existingor = Database.getSingleQuery("OrderSummary", "TableNo='" + buttonname + "'", "ReferenceNo");
                    iseditOrder = true;
                    isdone = true;
                    status = HotelFrmTableOption.status;
                    this.Close();
                }
                else
                {
                    return;
                }
                HotelFrmTableOption asdsa = new HotelFrmTableOption();
                asdsa.ShowDialog(this);
                
            }
            else
            {
                HotelFrmAuthentication authfrm = new HotelFrmAuthentication();
                authfrm.ShowDialog(this);
                if (HotelFrmAuthentication.isconfirmedLogin == true)
                {
                    existingor = "";
                    waiterid = HotelFrmAuthentication.waiterid;
                    isdone = true;
                    HotelFrmAuthentication.isconfirmedLogin = false;
                    authfrm.Dispose();
                    this.Close();
                    //Classes.Utilities.writeTextfile("C:\\POSTransaction\\ORSeries\\counter.txt", txtOrderNo.Text);
                }else
                {
                    return;
                }
              
            }



            //if (status == "Occupied")
            //{
            //    if (ok)
            //    {

            //    }
            //    XtraMessageBox.Show("You Can't Select this Table!");
            //    return;
            //}
            //else

            //{
            //    isdone = true;
            //    this.Close();
            //}
        }
    }
}
