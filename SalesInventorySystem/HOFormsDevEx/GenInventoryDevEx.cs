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
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using AForge.Video;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class GenInventoryDevEx : DevExpress.XtraEditors.XtraForm
    {
        string lastincno = "";
        public string tagnnumber = "";
        public static bool isdone = false,istagnoExist=false;
        VideoCaptureDevice videoSource;
        FilterInfoCollection videoDevices;
        ResizeNearestNeighbor size = new ResizeNearestNeighbor(100, 100);
        string photofilename;
        Bitmap imagepic;
        byte[] myPicbyte;
        object branchvalue;
        public GenInventoryDevEx()
        {
            InitializeComponent();
        }

        private void GenInventoryDevEx_Load(object sender, EventArgs e)
        {
            //populateItems();
            loadCameraDevices();
        }
        void populateItems()
        {
            Database.displaySearchlookupEdit("SELECT * FROM Custodian", txtcustodian, "Custodian", "Custodian");
            Database.displaySearchlookupEdit("SELECT * FROM GenInventoryItems", txtdesc, "Description", "Description");
            Database.displaySearchlookupEdit("SELECT BranchCode,BranchName FROM Branches", txtbrcode, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("SELECT SupplierID,SupplierName FROM Supplier", txtvendor, "SupplierID", "SupplierID");
            Database.displaySearchlookupEdit("SELECT LocationID,LocationName FROM Location", txtloc, "LocationID", "LocationID");
            Database.displaySearchlookupEdit("SELECT DeptID,DeptName FROM Departments", txtdept, "DeptID", "DeptID");
        }

        String getItemCode()
        {
            string code = "";
            code = Database.getSingleQuery("GenInventoryItems", "Description='" + txtdesc.Text + "'", "ItemCode");
            return code;
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

        void clear()
        {
            
            txtacctno.Text = "";
            txtacqcost.Text = "";
            txtacqdate.Text = "";
            txtbarcode.Text = "";
            txtbrandname.Text = "";
            txtbrcode.Text = "";
            txtclasscat.Text = "";
            txtcondition.Text = "";
            txtcustodian.Text = "";
            txtdepmethod.Text = "";
            txtdept.Text = "";
            txtdesc.Text = "";
            txtitemclass.Text = "";
            txtjournaldesc.Text = "";
            txtloc.Text = "";
            txtmodel.Text = "";
            txtnotes.Text = "";
            txtpurchtype.Text = "";
            txtserialno.Text = "";
            txtservicecontract.Text = "";
            txtserviceprov.Text = "";
            txttagno.Text = "";
            txtterm.Text = "";
            txttypeofterm.Text = "";
            txtvendor.Text = "";
            txtwarranty.Text = "";
        }

        void enableFields()
        {
            txtacctno.Enabled = true;
            txtacqcost.Enabled = true;
            txtacqdate.Enabled = true;
            txtbarcode.Enabled = true;
            txtbrandname.Enabled = true;
            txtbrcode.Enabled = true;
            txtclasscat.Enabled = true;
            txtcondition.Enabled = true;
            txtcustodian.Enabled = true;
            txtdepmethod.Enabled = true;
            txtdept.Enabled = true;
            txtdesc.Enabled = true;
            txtitemclass.Enabled = true;
            txtjournaldesc.Enabled = true;
            txtloc.Enabled = true;
            txtmodel.Enabled = true;
            txtnotes.Enabled = true;
            txtpurchtype.Enabled = true;
            txtserialno.Enabled = true;
            txtservicecontract.Enabled = true;
            txtserviceprov.Enabled = true;
            txttagno.Enabled = true;
            txtterm.Enabled = true;
            txttypeofterm.Enabled = true;
            txtvendor.Enabled = true;
            txtwarranty.Enabled = true;
        }

        void disablefields()
        {
            txtacctno.Enabled = false;
            txtacqcost.Enabled = false;
            txtacqdate.Enabled = false;
            txtbarcode.Enabled = false;
            txtbrandname.Enabled = false;
            txtbrcode.Enabled = false;
            txtclasscat.Enabled = false;
            txtcondition.Enabled = false;
            txtcustodian.Enabled = false;
            txtdepmethod.Enabled = false;
            txtdept.Enabled = false;
            txtdesc.Enabled = false;
            txtitemclass.Enabled = false;
            txtjournaldesc.Enabled = false;
            txtloc.Enabled = false;
            txtmodel.Enabled = false;
            txtnotes.Enabled = false;
            txtpurchtype.Enabled = false;
            txtserialno.Enabled = false;
            txtservicecontract.Enabled = false;
            txtserviceprov.Enabled = false;
            txttagno.Enabled = false;
            txtterm.Enabled = false;
            txttypeofterm.Enabled = false;
            txtvendor.Enabled = false;
            txtwarranty.Enabled = false;
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if(txtbrcode.Text=="" || txtdesc.Text=="" || txttagno.Text=="" || txtvendor.Text=="" || txtloc.Text=="" || txtdept.Text=="" || txtclasscat.Text=="")
            {
                XtraMessageBox.Show("Mandatory Fields must not Empty!");
                return;
            }
            if (chckprintbarcode.Checked == true)
            {
                Barcode.AssetTagBarcode bprint = new Barcode.AssetTagBarcode();
                bprint.lbldate.Text = txtacqdate.Text;
                bprint.lbldescription.Text = txtdesc.Text;
                bprint.xrBarCode2.Text = txttagno.Text.Trim(); //productcategorycode + primalcode + txtweight.Text.Remove(2, 1);
                ReportPrintTool report = new ReportPrintTool(bprint);
                if (checkBox1.Checked == true)
                    report.ShowRibbonPreviewDialog();
                else
                    report.Print();
            }
            if (HOFormsDevEx.ViewGeneralnventory.proctype == "ADD")
            {
                //bool isexist = Database.checkifExist("Select * FROM GenInventory WHERE TagNumber='" + txttagno.Text + "'");
                //if (isexist)
                //{
                //    XtraMessageBox.Show("The System found out that there is an already existing set of item, To Add the same Addtional Item please select choose replicate option..!");
                //    return;
                //}
               
                if (pictureBox1.Image != null)
                {
                    MemoryStream ms11 = new MemoryStream();
                    pictureBox1.Image.Save(ms11, ImageFormat.Jpeg);
                    myPicbyte = new byte[ms11.Length]; 
                    ms11.Position = 0;
                    ms11.Read(myPicbyte, 0, myPicbyte.Length);

                    //com.Parameters.AddWithValue("@photo", myPicbyte);
                }
                
                btnsubmit.Text = "ADD";
               
                //Database.ExecuteQuery("INSERT INTO GenInventory (BranchCode,TagNumber,Description,Model,SerialNo,Barcode,BrandName,ItemClass,Category,Condition,Notes,JournalDescription,Vendor,PurchaseType,AccountNumber,AcquisitionDate,AcquisitionCost,Term,TypeofTerm,Location,Department,Custodian,ServiceProvider,Warranty,ServiceContract,DepreciationMethod,PhotoImage) VALUES ('" + txtbrcode.Text + "','" + txttagno.Text + "','" + txtdesc.Text + "','" + txtmodel.Text + "','" + txtserialno.Text + "','" + txtbarcode.Text + "','" + txtbrandname.Text + "','" + txtitemclass.Text + "','"+txtclasscat.Text+"','" + txtcondition.Text + "','" + txtnotes.Text + "','" + txtjournaldesc.Text + "','" + txtvendor.Text + "','" + txtpurchtype.Text + "','" + txtacctno.Text + "','" + txtacqdate.Text + "','" + txtacqcost.Text + "','" + txtterm.Text + "','" + txttypeofterm.Text + "','" + txtloc.Text + "','" + txtdept.Text + "','" + txtcustodian.Text + "','" + txtserviceprov.Text + "','" + txtwarranty.Text + "','" + txtservicecontract.Text + "','" + txtdepmethod.Text + "','"+ myPicbyte + "')", "Succesfully Added");
                string query = "INSERT INTO GenInventory (BranchCode,TagNumber,ItemCode,Description,Model,SerialNo,Barcode,BrandName,ItemClass,Category,Condition,Notes,JournalDescription,Vendor,PurchaseType,AccountNumber,AcquisitionDate,AcquisitionCost,Term,TypeofTerm,Location,Department,Custodian,ServiceProvider,Warranty,ServiceContract,DepreciationMethod,PhotoImage) VALUES (@brcode,@tagno,@itemcode,@desc,@model,@serialno,@barcode,@brandname,@itemclass,@category,@condition,@notes,@journaldesc,@vendor,@purchtype,@acctno,@acqdate,@acqcost,@term,@typeofterm,@loc,@dept,@custodian,@serviceproc,@warranty,@servicecontract,@depmethod,@imagess)";
                SqlConnection con = Database.getConnection();
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@brcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@tagno", txttagno.Text);
                com.Parameters.AddWithValue("@itemcode", getItemCode());
                com.Parameters.AddWithValue("@desc", txtdesc.Text);
                com.Parameters.AddWithValue("@model", txtmodel.Text);
                com.Parameters.AddWithValue("@serialno", txtserialno.Text);
                com.Parameters.AddWithValue("@barcode", txtbarcode.Text);
                com.Parameters.AddWithValue("@brandname", txtbrandname.Text);
                com.Parameters.AddWithValue("@itemclass", txtitemclass.Text);
                com.Parameters.AddWithValue("@category", txtclasscat.Text);
                com.Parameters.AddWithValue("@condition", txtcondition.Text);
                com.Parameters.AddWithValue("@notes", txtnotes.Text);
                com.Parameters.AddWithValue("@journaldesc", txtjournaldesc.Text);
                com.Parameters.AddWithValue("@vendor", txtvendor.Text);
                com.Parameters.AddWithValue("@purchtype", txtpurchtype.Text);
                com.Parameters.AddWithValue("@acctno", txtacctno.Text);
                com.Parameters.AddWithValue("@acqdate", txtacqdate.Text);
                com.Parameters.AddWithValue("@acqcost", txtacqcost.Text);
                com.Parameters.AddWithValue("@term", txtterm.Text);
                com.Parameters.AddWithValue("@typeofterm", txttypeofterm.Text);
                com.Parameters.AddWithValue("@loc", txtloc.Text);
                com.Parameters.AddWithValue("@dept", txtdept.Text);
                com.Parameters.AddWithValue("@custodian", txtcustodian.Text);
                com.Parameters.AddWithValue("@serviceproc", txtserviceprov.Text);
                com.Parameters.AddWithValue("@warranty", txtwarranty.Text);
                com.Parameters.AddWithValue("@servicecontract", txtservicecontract.Text);
                com.Parameters.AddWithValue("@depmethod", txtdepmethod.Text);
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
                con.Close();
                postDepreciationSchedule();
                clear();
                isdone = true;
                
                this.Close();
            }
            if (HOFormsDevEx.ViewGeneralnventory.proctype == "UPDATE")
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
               
                btnsubmit.Text = "UPDATE";
                string query = "UPDATE GenInventory SET PhotoImage=@imagess,BranchCode = @brcode, Description = @desc, Model = @model, SerialNo = @serialno, Barcode = @barcode, BrandName = @brandname, ItemClass = @itemclass, Category = @category, Condition = @condition, Notes = @notes, JournalDescription = @journaldesc, Vendor = @vendor, PurchaseType = @purchtype, AccountNumber = @acctno, AcquisitionDate = @acqdate,AcquisitionCost = @acqcost, Term = @term, TypeofTerm = @typeofterm, Location = @loc, Department = @dept, Custodian = @custodian,ServiceProvider = @serviceproc, Warranty = @warranty, ServiceContract = @servicecontract, DepreciationMethod = @depmethod WHERE TagNumber = @tagno";
                SqlConnection con = Database.getConnection();
                con.Open();
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@brcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@tagno", txttagno.Text);
                com.Parameters.AddWithValue("@itemcode", getItemCode());
                com.Parameters.AddWithValue("@desc", txtdesc.Text);
                com.Parameters.AddWithValue("@model", txtmodel.Text);
                com.Parameters.AddWithValue("@serialno", txtserialno.Text);
                com.Parameters.AddWithValue("@barcode", txtbarcode.Text);
                com.Parameters.AddWithValue("@brandname", txtbrandname.Text);
                com.Parameters.AddWithValue("@itemclass", txtitemclass.Text);
                com.Parameters.AddWithValue("@category", txtclasscat.Text);
                com.Parameters.AddWithValue("@condition", txtcondition.Text);
                com.Parameters.AddWithValue("@notes", txtnotes.Text);
                com.Parameters.AddWithValue("@journaldesc", txtjournaldesc.Text);
                com.Parameters.AddWithValue("@vendor", txtvendor.Text);
                com.Parameters.AddWithValue("@purchtype", txtpurchtype.Text);
                com.Parameters.AddWithValue("@acctno", txtacctno.Text);
                com.Parameters.AddWithValue("@acqdate", txtacqdate.Text);
                com.Parameters.AddWithValue("@acqcost", txtacqcost.Text);
                com.Parameters.AddWithValue("@term", txtterm.Text);
                com.Parameters.AddWithValue("@typeofterm", txttypeofterm.Text);
                com.Parameters.AddWithValue("@loc", txtloc.Text);
                com.Parameters.AddWithValue("@dept", txtdept.Text);
                com.Parameters.AddWithValue("@custodian", txtcustodian.Text);
                com.Parameters.AddWithValue("@serviceproc", txtserviceprov.Text);
                com.Parameters.AddWithValue("@warranty", txtwarranty.Text);
                com.Parameters.AddWithValue("@servicecontract", txtservicecontract.Text);
                com.Parameters.AddWithValue("@depmethod", txtdepmethod.Text);
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
                clear();
                isdone = true;
                this.Close();
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
                videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame1);
                videoSource.Start();
            }
        }

        private void VideoSource_NewFrame1(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void btncapture_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = (Bitmap)pictureBox1.Image.Clone();
            imagepic = (Bitmap)pictureBox1.Image.Clone();
            
            imagepic = size.Apply(imagepic);
            resizeImage(imagepic, new Size(10, 10));
            photofilename = txtmodel.Text + "_photo" + ".jpg";
            imagepic.Save(photofilename);
            videoSource.Stop();
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }


        void postDepreciationSchedule()
        {
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string qeury = "sp_DepreciationSchedule";
                SqlCommand com = new SqlCommand(qeury, con);
                com.Parameters.AddWithValue("@parmbranchcode", txtbrcode.Text);
                com.Parameters.AddWithValue("@parmtagno", txttagno.Text);
                com.Parameters.AddWithValue("@parmcost", txtacqcost.Text);
                com.Parameters.AddWithValue("@parmterm", txtterm.Text);
                com.Parameters.AddWithValue("@parmdatepurchased", txtacqdate.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = qeury;
                com.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }

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

        private void txtacqcost_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void txtterm_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlphaWithDecimal(e);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        string getLastInc()
        {
            int last = 0;
            string itemCode = Database.getSingleQuery("GenInventoryItems", "Description='" + txtdesc.Text + "'", "ItemCode");
            bool isExist = Database.checkifExist("SELECT BranchCode FROM GenInventory WHERE BranchCode='" + txtbrcode.Text + "' AND ItemCode='" + itemCode + "'");
            if(isExist)
            {
                //get last id
                last = Database.getLastID("GenInventory", "BranchCode='" + txtbrcode.Text + "' AND ItemCode='" + itemCode + "'", "SequenceNumber");
            }
            else
            {
                last = 0;
            }
            
            
            if (last!=0)
            {
                string tagno = Database.getSingleQuery("GenInventory", "SequenceNumber='" + last + "'", "TagNumber");
                lastincno = tagno.Substring(8, 4);
                istagnoExist = true;
            }
            else
            {
                lastincno = "1000";
                istagnoExist = false;
            }
            return lastincno;
        }
       
        private void txtdesc_EditValueChanged(object sender, EventArgs e)
        {
            if (ViewGeneralnventory.proctype != "UPDATE")
            {
                tagnnumber = branchvalue.ToString();
                GridView view = txtdesc.Properties.View;
                int rowHandle = view.FocusedRowHandle;
                //string fieldName = "Name"; // or other field name
                object value = view.GetRowCellValue(rowHandle, "ItemCode");
                object valueCat = view.GetRowCellValue(rowHandle, "ItemCategory");
                tagnnumber += value.ToString();
                txtclasscat.Text = valueCat.ToString();
                if(getLastInc() == "1000" && !istagnoExist)
                {
                    txttagno.Text = tagnnumber + "1000";
                }
                else
                {
                    int inc = Convert.ToInt32(getLastInc()) + 1;
                    txttagno.Text = tagnnumber + inc.ToString();
                }
            }
        }

        private void chckprintbarcode_CheckedChanged(object sender, EventArgs e)
        {
            if(chckprintbarcode.Checked==true)
            {
                checkBox1.Enabled = true;
            }
            else
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }
        }

        private void txtbrcode_EditValueChanged(object sender, EventArgs e)
        {
            if (ViewGeneralnventory.proctype != "UPDATE")
            {
                tagnnumber = "";
                GridView view = txtbrcode.Properties.View;
                int rowHandle = view.FocusedRowHandle;
                //string fieldName = "Name"; // or other field name
                branchvalue = view.GetRowCellValue(rowHandle, "BranchCode");
                tagnnumber += branchvalue.ToString();
            }
        }
    }
}