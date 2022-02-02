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
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ReportHeaderSettings : DevExpress.XtraEditors.XtraForm
    {
        
        byte[] myPicbyte;
        string reportname;
        public ReportHeaderSettings()
        {
            InitializeComponent();
        }
        void clear()
        {
            txtrepname.Text = "";
            txtheading.Text = "";
            txtwidth.Text = "";
            txtheight.Text = "";
            txtcaption1.Text = "";
            txtcaption2.Text = "";
        }

        void disablefields()
        {
            txtheading.Enabled = false;
            txtrepname.Enabled = false;
            txtwidth.Enabled = false;
            txtheight.Enabled = false;
            txtcaption1.Enabled = false;
            txtcaption2.Enabled = false;
        }
        void enablefields()
        {
            txtheading.Enabled = true;
            txtrepname.Enabled = true;
            txtwidth.Enabled = true;
            txtheight.Enabled = true;
            txtcaption1.Enabled = true;
            txtcaption2.Enabled = true;
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialog.FileName);

            }
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
                
        }

        private void ReportHeaderSettings_Load(object sender, EventArgs e)
        {
            display();
        }
        void display()
        {
            Database.display("SELECT ReportName,ImageWidth,ImageHeight,Heading,Caption1,Caption2 FROM ReportHeaderSettings", gridControl1, gridView1);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void editItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReportName").ToString();
            txtrepname.Text = reportname;
            txtwidth.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ImageWidth").ToString();
            txtheight.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ImageHeight").ToString();
            txtheading.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Heading").ToString();
            txtcaption1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Caption1").ToString();
            txtcaption2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Caption2").ToString();

            Classes.Utilities.GetImage(pictureBox1, "ReportHeaderSettings", "ReportName='" + reportname + "'", "ImageLogo");

            enablefields();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            enablefields();
            display();
            btnnew.Enabled = false;
            btnadd.Enabled = true;
            btnupdate.Enabled = false;
            btncancel.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

            bool ok = Database.checkifExist("SELECT ReportName FROM ReportHeaderSettings WHERE ReportName='" + txtrepname.Text.Trim() + "' ");
            if (ok)
            {
                XtraMessageBox.Show("Already Exist in Report Header Table.. Please use Edit Function");
                return;
            }
            else
            {
                if (pictureBox1.Image != null)
                {
                    MemoryStream ms11 = new MemoryStream();
                    pictureBox1.Image.Save(ms11, ImageFormat.Jpeg);
                    myPicbyte = new byte[ms11.Length];
                    ms11.Position = 0;
                    ms11.Read(myPicbyte, 0, myPicbyte.Length);

                    //com.Parameters.AddWithValue("@photo", myPicbyte);
                }
                string query = "INSERT INTO ReportHeaderSettings (ReportName,ImageLogo,ImageWidth,ImageHeight,Heading,Caption1,Caption2) VALUES (@repname,@repimage,@repwidth,@repheight,@repheading,@caption1,@caption2)";
                SqlConnection con = Database.getConnection();
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@repname", txtrepname.Text);
                com.Parameters.AddWithValue("@repwidth", txtwidth.Text);
                com.Parameters.AddWithValue("@repheight", txtheight.Text);
                com.Parameters.AddWithValue("@repheading", txtheading.Text);
                com.Parameters.AddWithValue("@caption1", txtcaption1.Text);
                com.Parameters.AddWithValue("@caption2", txtcaption2.Text);

                if (pictureBox1.Image == null)
                {
                    // com.Parameters.AddWithValue("@imagess", DBNull.Value);
                    var binary1 = com.Parameters.Add("@repimage", SqlDbType.VarBinary, -1);
                    binary1.Value = DBNull.Value;
                }
                else
                {
                    com.Parameters.AddWithValue("@repimage", myPicbyte);
                }

                //com.Parameters.AddWithValue("@imagess", myPicbyte);
                if (com.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Data Inserted!");
                }
                con.Close();
                clear();

                btnnew.Enabled = true;
                btnadd.Enabled = false;
                btnupdate.Enabled = false;
                btncancel.Enabled = false;

                disablefields();
                display();
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                MemoryStream ms11 = new MemoryStream();
                pictureBox1.Image.Save(ms11, ImageFormat.Jpeg);
                myPicbyte = new byte[ms11.Length];
                ms11.Position = 0;
                ms11.Read(myPicbyte, 0, myPicbyte.Length);

                //com.Parameters.AddWithValue("@photo", myPicbyte);
            }
            //Database.ExecuteQuery("UPDATE ReportHeaderSettings SET ImageLogo='"+myPicbyte+"',ReportName='" + txtrepname.Text + "',ImageWidth='" + txtwidth.Text + "',ImageHeight='"+txtheight.Text+"',Caption1='"+txtcaption1.Text+"',Caption2='"+txtcaption2.Text+"',Heading='"+txtheading.Text+"' WHERE ReportName='" + reportname + "' ", "Successfully Updated!");

            string query = "UPDATE ReportHeaderSettings SET ImageLogo=@imagess,ReportName = @repname, ImageWidth = @repwidth, ImageHeight = @repheight, Caption1 = @caption1, Caption2 = @caption2, Heading = @repheading WHERE ReportName = @repname";
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("@repname", txtrepname.Text);
            com.Parameters.AddWithValue("@repwidth", txtwidth.Text);
            com.Parameters.AddWithValue("@repheight", txtheight.Text);
            com.Parameters.AddWithValue("@repheading", txtheading.Text);
            com.Parameters.AddWithValue("@caption1", txtcaption1.Text);
            com.Parameters.AddWithValue("@caption2", txtcaption2.Text);
            if (pictureBox1.Image == null)
            {
                // com.Parameters.AddWithValue("@imagess", DBNull.Value);
                var binary1 = com.Parameters.Add("@imagess", SqlDbType.VarBinary, -1);
                binary1.Value = DBNull.Value;
            }
            else
            {
                com.Parameters.AddWithValue("@imagess", myPicbyte);
            }

            //com.Parameters.AddWithValue("@imagess", myPicbyte);
            if (com.ExecuteNonQuery() == 1)
            {
                XtraMessageBox.Show("Data Inserted!");
            }

            //Database.ExecuteQuery("UPDATE ReportHeaderSettings SET ImageLogo='"+myPicbyte+"',ReportName='" + txtrepname.Text + "',ImageWidth='" + txtwidth.Text + "',ImageHeight='"+txtheight.Text+"',Caption1='"+txtcaption1.Text+"',Caption2='"+txtcaption2.Text+"',Heading='"+txtheading.Text+"' WHERE ReportName='" + reportname + "' ", "Successfully Updated!");
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            disablefields();

            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reportname = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReportName").ToString();
            bool ok = HelperFunction.ConfirmDialog("Are you sure you want to delete this item?", "Delete ReportHeaderSettings");
            if (ok)
            {
                Database.ExecuteQuery("DELETE FROM ReportHeaderSettings WHERE ReportName='" + reportname + "' ", "Successfully Deleted");
                display();
            }
            else
            {
                return;
            }
        }
    }
}