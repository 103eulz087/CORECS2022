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
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Video;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmGuestDetails : DevExpress.XtraEditors.XtraForm
    {
        VideoCaptureDevice videoSource;
        FilterInfoCollection videoDevices;
        ResizeNearestNeighbor size = new ResizeNearestNeighbor(100, 100);
        string photofilename;
        Bitmap imagepic;
        byte[] myPicbyte;
        public HotelFrmGuestDetails()
        {
            InitializeComponent();
        }

        private void HotelFrmGuestDetails_Load(object sender, EventArgs e)
        {
            loadCameraDevices();
            txtidno.Text = IDGenerator.getGuestID().ToString();
        }

        void loadCameraDevices()
        {
            txtlistofcams.Items.Add(HelperFunction.getDevices());
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                txtlistofcams.Items.Add(device.Name);
            }
            txtlistofcams.SelectedIndex = 0;
            videoSource = new VideoCaptureDevice();
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

        private void btnstartcam_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
            else
            {
                videoSource = new VideoCaptureDevice(videoDevices[txtlistofcams.SelectedIndex].MonikerString);
                //videoSource.NewFrame += VideoSource_NewFrame1;
                //videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame1);
                videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                videoSource.Start();
            }
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void btncapture_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (Bitmap)pictureBox1.Image.Clone();
            imagepic = (Bitmap)pictureBox1.Image.Clone();

            imagepic = size.Apply(imagepic);
            resizeImage(imagepic, new Size(10, 10));
            photofilename = txtfname.Text + "_photo" + ".jpg";
            imagepic.Save(photofilename);
            videoSource.Stop();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            execute();
            XtraMessageBox.Show("Successfully Added");
            this.Dispose();
        }

        void execute()
        {
            string gender = "",citizenship="";
            if(radiosex.Checked==true)
            {
                gender = "Male";
            }else
            {
                gender = "Female";
            }
            if(radiocitizenship.Checked==true)
            {
                citizenship = "Filipino";
            }
            else
            {
                citizenship = txtcitizenship.Text;
            }
            if (pictureBox1.Image != null)
            {
                MemoryStream ms11 = new MemoryStream();
                pictureBox1.Image.Save(ms11, ImageFormat.Jpeg);
                myPicbyte = new byte[ms11.Length];
                ms11.Position = 0;
                ms11.Read(myPicbyte, 0, myPicbyte.Length);

                //com.Parameters.AddWithValue("@photo", myPicbyte);
            }
            SqlConnection con = Database.getCustomizeConnection();
            con.Open();
            try
            {
                string qeury = "sp_AddGuestInfo";
                SqlCommand com = new SqlCommand(qeury, con);
                com.Parameters.AddWithValue("@parmguestid", txtidno.Text);
                com.Parameters.AddWithValue("@parmfname", txtfname.Text);
                com.Parameters.AddWithValue("@parmmname", txtmname.Text);
                com.Parameters.AddWithValue("@parmlname", txtlname.Text);
                com.Parameters.AddWithValue("@parmaddress", txtaddress.Text);
                com.Parameters.AddWithValue("@parmcontactno", txtcontactno.Text);
                com.Parameters.AddWithValue("@parmdateofbirth", txtbdate.Text);
                com.Parameters.AddWithValue("@parmemailadd", txtemailadd.Text);
                com.Parameters.AddWithValue("@parmbplace", txtbplace.Text);
                com.Parameters.AddWithValue("@parmgender", gender);
                com.Parameters.AddWithValue("@parmcitizenship", citizenship);
                com.Parameters.AddWithValue("@parmcivilstat", cmbcivilstat.Text);
                com.Parameters.AddWithValue("@parmnationality", txtnationality.Text);
                com.Parameters.AddWithValue("@parmreligion", txtreligion.Text);
                com.Parameters.AddWithValue("@parmprimaryid", txtid1.Text);
                com.Parameters.AddWithValue("@parmsecondaryid", txtid2.Text);
                com.Parameters.AddWithValue("@parmprimaryidno", txtidno1.Text);
                com.Parameters.AddWithValue("@parmsecondaryidno", txtidno2.Text);
                com.Parameters.AddWithValue("@parmcompany", txtcompany.Text);

                // com.ExecuteNonQuery();
                if (pictureBox1.Image == null)
                {
                    // com.Parameters.AddWithValue("@imagess", DBNull.Value);
                    var binary1 = com.Parameters.Add("@parmimage", SqlDbType.VarBinary, -1);
                    binary1.Value = DBNull.Value;
                }
                else
                {
                    com.Parameters.AddWithValue("@parmimage", myPicbyte);
                }
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = qeury;
                //com.Parameters.AddWithValue("@imagess", myPicbyte);
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

        private void radiocitizenship_CheckedChanged(object sender, EventArgs e)
        {
            if (radiocitizenship.Checked == true)
            {
                txtcitizenship.Text = "";
                txtcitizenship.Enabled = false;
            }
            else
            {
                txtcitizenship.Focus();
                txtcitizenship.Text = "";
                txtcitizenship.Enabled = true;
            }
        }

        private void radiocitizenship2_CheckedChanged(object sender, EventArgs e)
        {
            if (radiocitizenship2.Checked == true)
            {
                txtcitizenship.Focus();
                txtcitizenship.Text = "";
                txtcitizenship.Enabled = true;
            }
            else
            {
                txtcitizenship.Text = "";
                txtcitizenship.Enabled = false;
            }
        }
    }
}