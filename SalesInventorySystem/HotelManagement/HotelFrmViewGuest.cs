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
using System.IO;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmViewGuest : DevExpress.XtraEditors.XtraForm
    {
        public HotelFrmViewGuest()
        {
            InitializeComponent();
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            HotelFrmGuestDetails guesdet = new HotelFrmGuestDetails();
            guesdet.Show();
            guesdet.txtidno.Text = Classes.HotelIDGenerator.getGuestIDNumber().ToString();
        }

        private void HotelFrmViewGuest_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            Database.display("SELECT * FROM view_guestinfo", gridControl1, gridView1, Database.getCustomizeConnection());
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotelFrmGuestDetails guesdet = new HotelFrmGuestDetails();
            guesdet.Show();
            guesdet.txtidno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"GuestID").ToString();
            guesdet.txtfname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "FirstName").ToString();
            guesdet.txtmname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MiddleName").ToString();
            guesdet.txtlname.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LastName").ToString();
            guesdet.txtaddress.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Address").ToString();
            guesdet.txtcontactno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ContactNo").ToString();
            guesdet.txtemailadd.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "EmailAddress").ToString();
            guesdet.txtbplace.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PlaceofBirth").ToString();
            guesdet.txtcitizenship.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Citizenship").ToString();
            guesdet.cmbcivilstat.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "CivilStatus").ToString();
            guesdet.txtnationality.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Nationality").ToString();
            guesdet.txtreligion.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Religion").ToString();
            guesdet.txtid1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PrimaryID").ToString();
            guesdet.txtid2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SecondaryID").ToString();
            guesdet.txtidno1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "PrimaryIDNo").ToString();
            guesdet.txtidno2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SecondaryIDNo").ToString();
            SalesInventorySystem.Classes.Utilities.GetImage(guesdet.pictureBox1, "GuestInfo", "GuestID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "GuestID").ToString() + "'", "ImageID",Database.getCustomizeConnection());
        }
   
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }

        private void accountHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string guestid = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "GuestID").ToString();
            string acctname = Database.getSingleQuery("ClientAccounts", "AccountID='" + guestid + "'", "AccountName", Database.getCustomizeConnection());
            string acctbal = Database.getSingleQuery("ClientAccounts", "AccountID='" + guestid + "'", "AccountBalance", Database.getCustomizeConnection());
            HotelFrmGuestLedger sad = new HotelFrmGuestLedger();
            Database.display("SELECT CAST(PostingDate as date) as PostingDate,CAST(TransactionDate as date) as TransactionDate,TransCode,Description,Debit,Credit,ReferenceNumber FROM ClientLedger WHERE AccountID='" + guestid + "'  ORDER BY SequenceNumber ASC", sad.gridControlLEdger, sad.gridViewLedger, Database.getCustomizeConnection());
            sad.txtacctname.Text = acctname;
            sad.txtacctbalance.Text = acctbal;
            sad.ShowDialog(this);
           
        }

        private void viewPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotelFrmGuestPhoto asd = new HotelFrmGuestPhoto();
            
            asd.pictureBox1.Image = null;
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string query = "select * FROM GuestInfo WHERE GuestID='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "GuestID").ToString() + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        if (reader["ImageID"] == System.DBNull.Value)
                        {
                            asd.pictureBox1.Image = null;
                        }
                        else
                        {
                            byte[] img = null;
                            img = (byte[])reader["ImageID"];
                            MemoryStream ms = new MemoryStream(img);
                            ms.Seek(0, SeekOrigin.Begin);
                            asd.pictureBox1.Image = Image.FromStream(ms);
                        }

                    }
                }
                else
                {
                    asd.pictureBox1.Image = null;
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
            asd.ShowDialog(this);
        }
    }
}