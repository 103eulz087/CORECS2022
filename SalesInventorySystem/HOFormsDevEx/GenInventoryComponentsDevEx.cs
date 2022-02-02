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
using System.Drawing.Imaging;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class GenInventoryComponentsDevEx : DevExpress.XtraEditors.XtraForm
    {
        byte[] myPicbyte;
        public GenInventoryComponentsDevEx()
        {
            InitializeComponent();
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

        void display()
        {
            Database.display("SELECT * FROM InventoryComponents WHERE AssetTagNo='" + txttagno.Text + "'", gridControl1, gridView1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Add();
            display();
        }

        void Add()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
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
                //string qeury = "sp_AddInventoryComponents";
                string qeury = "INSERT INTO InventoryComponents (ReferenceTagNo,AssetTagNo,ExistingTagNo,Description,Serial,Condition,ImagePhoto) VALUES (@parmreftagno,@parmassettagno,@parmexistingtagno,@parmdesc,@parmserial,@parmcondition,@parmphoto)";

                SqlCommand com = new SqlCommand(qeury, con);
                com.Parameters.AddWithValue("@parmreftagno", "");
                com.Parameters.AddWithValue("@parmassettagno", txttagno.Text);
                com.Parameters.AddWithValue("@parmexistingtagno", txtexistingtagno.Text);
                com.Parameters.AddWithValue("@parmdesc", txtdesccomp.Text);
                com.Parameters.AddWithValue("@parmserial", txtserialcomp.Text);
                com.Parameters.AddWithValue("@parmcondition", txtconditioncomp.Text);
                if (pictureBox1.Image == null)
                {
                    var binary1 = com.Parameters.Add("@parmphoto", SqlDbType.VarBinary, -1);
                    binary1.Value = DBNull.Value;
                }
                else
                {
                    com.Parameters.AddWithValue("@parmphoto", myPicbyte);
                }
                if (com.ExecuteNonQuery() == 1)
                {
                    XtraMessageBox.Show("Data Inserted!");
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

        }

        private void GenInventoryComponentsDevEx_Load(object sender, EventArgs e)
        {
            disablefields();
           // display();
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }

        void clear()
        {
            txtexistingtagno.Text = "";
            txtdesccomp.Text = "";
            txtserialcomp.Text = "";
            txtconditioncomp.Text = "";
        }

        void disablefields()
        {
            txtexistingtagno.Enabled = false;
            txtdesccomp.Enabled = false;
            txtserialcomp.Enabled = false;
            txtconditioncomp.Enabled = false;
        }
        void enablefields()
        {
         
            txtexistingtagno.Enabled = true;
            txtdesccomp.Enabled = true;
            txtserialcomp.Enabled = true;
            txtconditioncomp.Enabled = true;
        }


        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enablefields();
            txtexistingtagno.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ExistingTagNo").ToString();
            txtdesccomp.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString();
            txtserialcomp.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Serial").ToString();
            txtconditioncomp.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Condition").ToString();

            btnnew.Enabled = false;
            btnadd.Enabled = false;
            btnupdate.Enabled = true;
            btncancel.Enabled = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE InventoryComponents SET ExistingTagno='" + txtexistingtagno.Text + "',Description='" + txtdesccomp.Text + "',Serial='" + txtserialcomp.Text + "',Condition='" + txtconditioncomp.Text + "' WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "'","Successfully Updated!");
            display();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("DELETE FROM InventoryComponents WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "'", "Successfully Deleted!");
            display();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            Add();
            clear();

            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;

            disablefields();
            display();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            Database.ExecuteQuery("UPDATE InventoryComponents SET ExistingTagno='" + txtexistingtagno.Text + "',Description='" + txtdesccomp.Text + "',Serial='" + txtserialcomp.Text + "',Condition='" + txtconditioncomp.Text + "' WHERE SequenceNo='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SequenceNo").ToString() + "'", "Successfully Updated!");
            clear();
            disablefields();
            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
            display();
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

        private void btncancel_Click(object sender, EventArgs e)
        {
            clear();
            disablefields();

            btnnew.Enabled = true;
            btnadd.Enabled = false;
            btnupdate.Enabled = false;
            btncancel.Enabled = false;
        }
    }
}