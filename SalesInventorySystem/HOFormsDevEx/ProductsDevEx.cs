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

namespace SalesInventorySystem.HOFormsDevEx
{
    public partial class ProductsDevEx : DevExpress.XtraEditors.XtraForm
    {
        //bool isprimalcut = false;
        //string parmoption = "";
        string action = "";
        object txtprodcatobject = null;
        object txtprodtypeobject = null;
        public ProductsDevEx()
        {
            InitializeComponent();
        }

        private void ProductSettings_Load(object sender, EventArgs e)
        {
            //display();
            panelControl1.Visible = false;
            if (Login.assignedBranch != "888")
            {
                Database.displayComboBoxItems("SELECT TOP(1) BranchCode,BranchName FROM dbo.Branches WHERE BranchCode='" + Login.assignedBranch + "'", "BranchName", comboBox1);
            }
            else
            {
                chckapplytoall.Visible = true;
                Database.displayComboBoxItems("SELECT BranchCode,BranchName FROM dbo.Branches", "BranchName", comboBox1);
            }
            //HelperFunction.DisableTextFields(this);
            disableFields();
            txtbranch.Enabled = false;
            comboBox1.Text = Branch.getBranchName(Login.assignedBranch);
        }

        void disableFields()
        {
            txtsellingprice.Enabled = false;
            txtdesc.Enabled = false;
            //txtprodcat.Enabled = false;
            txtprodcatlookup.Enabled = false;
            txtlandingcost.Enabled = false;
            txtsellingprice.Enabled = false;
            txtprice1.Enabled = false;
            txtprice2.Enabled = false;
            txtprice3.Enabled = false;
            txtprice4.Enabled = false;
            //txtprice5.Enabled = false;
            txtbarcode.Enabled = false;
            txtreorderlevel.Enabled = false;
        }

        void display()
        {
            Database.display("SELECT * FROM dbo.view_Products WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "'", gridControl1, gridView1);
        }
        void display(string condition)
        {
            Database.display("SELECT * FROM dbo.view_Products WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' AND ProductCategory='" + condition + "'", gridControl1, gridView1);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            HelperFunction.EnableTextFields(this);
            
            txtlandingcost.Text = "0";
            txtsellingprice.Text = "0";
            txtprice1.Text = "0";
            txtprice2.Text = "0";
            txtprice3.Text = "0";
            txtprice4.Text = "0";
            //txtprice5.Text = "0";
            txtreorderlevel.Text = "0";
            txtprodcode.ReadOnly = false;
            txtbranch.Enabled = true;
            txtprodcatlookup.Enabled = true;
            txtprodtype.Enabled = true;
            txtprodcatlookup.Focus();

            simpleButton1.Enabled = false;

            addbtn.Enabled = true;
            updatebtn.Enabled = false;
            btncancel.Enabled = true;

            loadProdCategoryLookUp();
        }

        void loadProdCategoryLookUp()
        {
            Database.displaySearchlookupEdit("SELECT ProductCategoryID as ID,Description,isVat FROM dbo.ProductCategory ORDER BY ProductCategoryID", txtprodcatlookup, "Description", "Description");
            Database.displaySearchlookupEdit("SELECT * FROM dbo.ProductType ORDER BY TypeCode", txtprodtype, "TypeDescription", "TypeDescription");
        }

        //String getProductCategoryCode()
        //{
        //    string str = "";
        //    str = Database.getSingleData("ProductCategory", "Description", txtprodcat.Text, "ProductCategoryID");
        //    return str;
        //}

        private void addbtn_Click(object sender, EventArgs e)
        {
            string barcode = "";
            string smainprice = "0", sprice1 = "0", sprice2 = "0", sprice3 = "0", sprice4 = "0", sdiscountitem = "0", ishavbarcode = "0", isvat = "0";
            try
            {
                if (ismainprice.Checked == true)
                {
                    smainprice = "1";
                }
                else if (isprice1.Checked == true)
                {
                    sprice1 = "1";
                }
                else if (isprice2.Checked == true)
                {
                    sprice2 = "1";
                }
                else if (isprice3.Checked == true)
                {
                    sprice3 = "1";
                }
                else if (isprice4.Checked == true)
                {
                    sprice4 = "1";
                }
                if (isdiscountitem.Checked == true)
                {
                    sdiscountitem = "1";
                }

                if (havbarcode.Checked == true)
                {
                    ishavbarcode = "1";
                    barcode = txtbarcode.Text;
                }
                else
                {
                    ishavbarcode = "0";
                    barcode = "";
                }

                if (chckisvat.Checked == true)
                {
                    isvat = "1";
                   
                }
                else
                {
                    isvat = "0";
                   
                }



                if (HelperFunction.isTextBoxEmpty(txtdesc, txtprodcode, txtsellingprice, txtlandingcost, txtreorderlevel))
                {
                    XtraMessageBox.Show("Please Input All Fields");
                }
                else
                {
                    //brcode = Branch.getBranchCode(txtbranch.Text);
                    bool checkifexist = Database.checkifExist("SELECT TOP(1) ProductCode FROM dbo.Products WHERE ProductCode='" + txtprodcode.Text + "' AND BranchCode='888'");
                    if (checkifexist)
                    {
                        XtraMessageBox.Show("Product Code Already Exist");
                    }
                    else
                    {
                        string mark = txtprodcatobject.ToString();
                        //string mark = getProductCategoryCode();
                        string prodcode = txtprodcode.Text;
                        Database.ExecuteQuery("INSERT INTO dbo.Products VALUES('888'" +
                            ",'" + txtprodcode.Text + "'" +
                            ",'" + txtdesc.Text + "'" +
                            ",'" + txtlongdesc.Text + "'" +
                            ",'" + txtlandingcost.Text + "'" +
                            ",'" + txtsellingprice.Text + "'" +
                            //",'" + getProductCategoryCode() + "'" +
                            ",'" + mark + "'" +
                            //",'0'" +
                            //",'0'" +
                            ",'" + txtprice1.Text + "'" +
                            ",'" + txtprice2.Text + "'" +
                            ",'" + txtprice3.Text + "'" +
                            ",'" + txtprice4.Text + "'" +
                            //",'" + txtprice5.Text + "'" +
                            ",'" + smainprice + "'" +
                            ",'" + sprice1 + "'" +
                            ",'" + sprice2 + "'" +
                            ",'" + sprice3 + "'" +
                            ",'" + sprice4 + "'" +
                            ",'" + sdiscountitem + "'" +
                            ",'" + ishavbarcode + "'" +
                            ",'" + barcode + "'" +
                            ",'" + txtreorderlevel.Text + "'" +
                            ",'" + isvat + "'" +
                            ",'" + txtprodtypeobject.ToString() + "')");

                        string caption = "Add New Product with Description name of "+txtdesc.Text+" and Product Code="+txtprodcode.Text+" ";
                        Database.ExecuteQuery("INSERT INTO dbo.HistoryLogs VALUES('"+Login.isglobalUserID+"'" +
                            ",'"+DateTime.Now.ToString()+"'" +
                            ",'"+ caption + "' " +
                            ",'888')");

                        XtraMessageBox.Show("Successfully Added!");
                        display(comboBox2.Text);
                        HelperFunction.ClearAllText(this);
                        //HelperFunction.DisableTextFields(this);
                        disableFields();

                        //txtprodcat.Text = "";
                        //txtprodcat.Enabled = false;
                        txtprodcatlookup.Text = "";
                        txtprodtype.Text = "";
                        txtprodcatlookup.Enabled = false;
                        txtprodtype.Enabled = false;

                        simpleButton1.Enabled = true;
                        addbtn.Enabled = false;
                        updatebtn.Enabled = false;
                        btncancel.Enabled = false;

                    }
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }


        private void editDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                // int cord = dataGridView1.CurrentCellAddress.Y;
                loadProdCategoryLookUp();
                action = "UPDATE";
                HelperFunction.ClearAllText(this);
                HelperFunction.EnableTextFields(this);
                txtprodcode.Enabled = false;
                //txtprodcat.Enabled = true;
                txtprodcatlookup.Enabled = true;

                txtprodtype.Enabled = true;
                txtbranch.Enabled = false;
                // bool mark = Convert.ToBoolean(dataGridView1.Rows[cord].Cells[6].Value.ToString());
                //localGrid
                //if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isRegular"].Value.ToString()) == true)
                //{
                //    ismainprice.Checked = true;
                //}
                //else if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isPrice1"].Value.ToString()) == true)
                //{
                //    isprice1.Checked = true;
                //}
                //else if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isPrice2"].Value.ToString()) == true)
                //{
                //    isprice2.Checked = true;
                //}
                //else if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isPrice3"].Value.ToString()) == true)
                //{
                //    isprice3.Checked = true;
                //}
                //else if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isPrice4"].Value.ToString()) == true)
                //{
                //    isprice4.Checked = true;
                //}
                //else if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["isPrice5"].Value.ToString()) == true)
                //{
                //    isprice5.Checked = true;
                //}

                //if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells["haveBarcode"].Value.ToString()) == true)
                //{
                //    havbarcode.Checked = true;
                //}
                //DevExGrid
                if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isRegular").ToString()) == true)
                {
                    ismainprice.Checked = true;
                }
                else if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isPrice1").ToString()) == true)
                {
                    isprice1.Checked = true;
                }
                else if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isPrice2").ToString()) == true)
                {
                    isprice2.Checked = true;
                }
                else if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isPrice3").ToString()) == true)
                {
                    isprice3.Checked = true;
                }
                else if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isPrice4").ToString()) == true)
                {
                    isprice4.Checked = true;
                }
                if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "isDiscount").ToString()) == true)
                {
                    isdiscountitem.Checked = true;
                }

                if (Convert.ToBoolean(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "haveBarcode").ToString()) == true)
                {
                    havbarcode.Checked = true;
                }

                //if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[6].Value.ToString()) == true)
                //{
                //    checkBox1.Checked = true;
                //}
                //else
                //{
                //    checkBox1.Checked = false;
                //}
                //if (Convert.ToBoolean(dataGridView1.Rows[cord].Cells[7].Value.ToString()) == true)
                //{
                //    incontainer.Checked = true;
                //}
                //else
                //{
                //    incontainer.Checked = false;
                //}

                //txtbranch.Text = dataGridView1.Rows[cord].Cells[0].Value.ToString();
                //txtprodcode.Text = dataGridView1.Rows[cord].Cells[1].Value.ToString();
                //txtdesc.Text = dataGridView1.Rows[cord].Cells[2].Value.ToString();
                //txtlandingcost.Text = dataGridView1.Rows[cord].Cells[3].Value.ToString();
                //txtsellingprice.Text = dataGridView1.Rows[cord].Cells[4].Value.ToString();
                //txtprodcat.Text = dataGridView1.Rows[cord].Cells[5].Value.ToString();
                //txtprice1.Text = dataGridView1.Rows[cord].Cells["Price1"].Value.ToString();
                //txtprice2.Text = dataGridView1.Rows[cord].Cells["Price2"].Value.ToString();
                //txtprice3.Text = dataGridView1.Rows[cord].Cells["Price3"].Value.ToString();
                //txtprice4.Text = dataGridView1.Rows[cord].Cells["Price4"].Value.ToString();
                //txtprice5.Text = dataGridView1.Rows[cord].Cells["Price5"].Value.ToString();
                //txtbarcode.Text = dataGridView1.Rows[cord].Cells["Barcode"].Value.ToString();
                var eulz = Database.getMultipleQuery($"SELECT TOP(1) ProductCategoryCode FROM dbo.Products WHERE BranchCode='{gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString()}' and ProductCode='{gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString()}'", "ProductCategoryCode");
                string ProductCategoryCode = eulz["ProductCategoryCode"].ToString();
                txtproductcategorycode.Text = ProductCategoryCode;

                var mafi = Database.getMultipleQuery($"SELECT TOP(1) TypeCode FROM dbo.ProductType WHERE  TypeDescription='{gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TypeDescription").ToString()}'", "TypeDescription");
                string ProductTypeCode = mafi["TypeCode"].ToString();
                txtproducttypecode.Text = ProductTypeCode;

                var rwz = Database.getMultipleQuery($"SELECT TOP(1) Description FROM dbo.ProductCategory WHERE ProductCategoryID='{txtproductcategorycode.Text}'","Description");
                string ProductCategoryName = rwz["Description"].ToString();


                var rowz = Database.getMultipleQuery($"SELECT TOP(1) TypeDescription FROM dbo.ProductType WHERE TypeCode='{txtproducttypecode.Text}'", "TypeDescription");
                string ProductTypeName = rowz["TypeDescription"].ToString();

                txtprodcatlookup.Text = ProductCategoryName;//  dataGridView1.Rows[cord].Cells[5].Value.ToString();
                //txtprodcatlookup.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCategory").ToString();//  dataGridView1.Rows[cord].Cells[5].Value.ToString();
                //txtprodcat.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCategory").ToString();//  dataGridView1.Rows[cord].Cells[5].Value.ToString();
                txtbranch.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BranchCode").ToString();// dataGridView1.Rows[cord].Cells[0].Value.ToString();
                txtprodcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString();// dataGridView1.Rows[cord].Cells[1].Value.ToString();
                txtdesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Description").ToString(); //dataGridView1.Rows[cord].Cells[2].Value.ToString();
                txtlongdesc.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LongDescription").ToString(); //dataGridView1.Rows[cord].Cells[2].Value.ToString();
                txtlandingcost.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "LandingCost").ToString(); //dataGridView1.Rows[cord].Cells[3].Value.ToString();
                txtsellingprice.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SellingPrice").ToString(); // dataGridView1.Rows[cord].Cells[4].Value.ToString();
                txtprice1.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Price1").ToString();//  dataGridView1.Rows[cord].Cells["Price1"].Value.ToString();
                txtprice2.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Price2").ToString();// dataGridView1.Rows[cord].Cells["Price2"].Value.ToString();
                txtprice3.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Price3").ToString();//  dataGridView1.Rows[cord].Cells["Price3"].Value.ToString();
                txtprice4.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Price4").ToString(); // dataGridView1.Rows[cord].Cells["Price4"].Value.ToString();
                //txtprice5.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Price5").ToString();// dataGridView1.Rows[cord].Cells["Price5"].Value.ToString();
                txtbarcode.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Barcode").ToString(); //dataGridView1.Rows[cord].Cells["Barcode"].Value.ToString();
                txtreorderlevel.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ReOrderLevel").ToString(); //dataGridView1.Rows[cord].Cells["Barcode"].Value.ToString();

               // txtproducttypecode.Text = txtprodcode.Text;

                simpleButton1.Enabled = false;
                addbtn.Enabled = false;
                updatebtn.Enabled = true;
                btncancel.Enabled = true;
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            //parmoption = "UPDATE";
            string barcode = "";
            string smainprice = "0", sprice1 = "0", sprice2 = "0", sprice3 = "0", sprice4 = "0", sdiscountitem = "0", shavbarcode = "0",isvat="0"; //ncontainer = "",
            string applytoall = "";
            try
            {
                if (chckapplytoall.Checked == true)
                {
                    applytoall = "APPLYTOALL";
                }
                else
                {
                    applytoall = "";
                }

                if (ismainprice.Checked == true)
                {
                    smainprice = "1";
                }
                else if (isprice1.Checked == true)
                {
                    sprice1 = "1";
                }
                else if (isprice2.Checked == true)
                {
                    sprice2 = "1";
                }
                else if (isprice3.Checked == true)
                {
                    sprice3 = "1";
                }
                else if (isprice4.Checked == true)
                {
                    sprice4 = "1";
                }
                if (isdiscountitem.Checked == true)
                {
                    sdiscountitem = "1";
                }

                if (havbarcode.Checked == true)
                {
                    shavbarcode = "1";
                    barcode = txtbarcode.Text;
                }
                else
                {
                    shavbarcode = "0";
                    barcode = "";
                }

                if (chckisvat.Checked == true)
                {
                    isvat = "1";
                    
                }
                else
                {
                    isvat = "0";
                 
                }


                if (HelperFunction.isTextBoxEmpty(txtreorderlevel, txtdesc, txtprodcode, txtsellingprice, txtlandingcost, txtprice1, txtprice2, txtprice3, txtprice4) || txtbranch.Text == "" || txtprodcatlookup.Text == "")
                {
                    XtraMessageBox.Show("Please Supply All Fields");
                }
                else
                {
                    string brcode = "", prodcatcode = "", prodtypecode="";
                    brcode = Branch.getBranchCode(txtbranch.Text.Trim());
                    //prodcatcode = getProductCategoryCode();
                    prodcatcode = txtproductcategorycode.Text;//txtprodcatobject.ToString();
                    prodtypecode = txtproducttypecode.Text; 
                    SqlConnection con = Database.getConnection();
                    con.Open();
                    string query = "sp_UpdateProducts";
                    SqlCommand com = new SqlCommand(query, con);
                    com.Parameters.AddWithValue("@parmbranchcode", txtbranch.Text);
                    com.Parameters.AddWithValue("@parmprodcatcode", prodcatcode);
                    com.Parameters.AddWithValue("@parmprodcode", txtprodcode.Text);
                    com.Parameters.AddWithValue("@parmdesc", txtdesc.Text);
                    com.Parameters.AddWithValue("@parmlongdesc", txtlongdesc.Text); //new field
                    com.Parameters.AddWithValue("@parmlandcost", txtlandingcost.Text);
                    com.Parameters.AddWithValue("@parmsellingprice", txtsellingprice.Text);
                    //com.Parameters.AddWithValue("@parmisprimal","");
                    //com.Parameters.AddWithValue("@parmincontainer",ncontainer);
                    com.Parameters.AddWithValue("@parmprice1", txtprice1.Text);
                    com.Parameters.AddWithValue("@parmprice2", txtprice2.Text);
                    com.Parameters.AddWithValue("@parmprice3", txtprice3.Text);
                    com.Parameters.AddWithValue("@parmprice4", txtprice4.Text);
                    //com.Parameters.AddWithValue("@parmprice5",txtprice5.Text);
                    com.Parameters.AddWithValue("@parmisregular", smainprice);
                    com.Parameters.AddWithValue("@parmisprice1", sprice1);
                    com.Parameters.AddWithValue("@parmisprice2", sprice2);
                    com.Parameters.AddWithValue("@parmisprice3", sprice3);
                    com.Parameters.AddWithValue("@parmisprice4", sprice4);
                    com.Parameters.AddWithValue("@parmisdiscountitem", sdiscountitem);
                    com.Parameters.AddWithValue("@parmhavebarcode", shavbarcode);
                    com.Parameters.AddWithValue("@parmbarcode", txtbarcode.Text);
                    com.Parameters.AddWithValue("@parmoption", applytoall);
                    com.Parameters.AddWithValue("@parmexectype", "UPDATE");
                    com.Parameters.AddWithValue("@parmreorderlevel", txtreorderlevel.Text);
                    com.Parameters.AddWithValue("@parmuser", Login.isglobalUserID);
                    com.Parameters.AddWithValue("@parmisvat", isvat);
                    com.Parameters.AddWithValue("@parmprodtype", prodtypecode);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = query;
                    com.ExecuteNonQuery();
                    XtraMessageBox.Show("Successfully Updated!");
                    con.Close();

                    Database.display("SELECT TOP(50) * FROM dbo.view_Products WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' " +
                     "AND ProductCode = '" + txtprodcode.Text + "' ", gridControl1, gridView1);
                    HelperFunction.ClearAllText(this);
                    //HelperFunction.DisableTextFields(this);
                    disableFields();

                    //txtprodcat.Text = "";
                    txtprodcatlookup.Text = "";
                    txtprodcatlookup.Enabled = false;
                    //txtprodcat.Enabled = false;

                    simpleButton1.Enabled = true;
                    addbtn.Enabled = false;
                    updatebtn.Enabled = false;
                    btncancel.Enabled = false;
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }


        private void btncancel_Click(object sender, EventArgs e)
        {
            HelperFunction.ClearAllText(this);
            HelperFunction.DisableTextFields(this);
            txtbranch.Enabled = false;

            //txtprodcat.Enabled = false;
            txtprodcatlookup.Enabled = false;
            simpleButton1.Enabled = true;
            addbtn.Enabled = false;
            updatebtn.Enabled = false;
            btncancel.Enabled = false;
        }

        private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = HelperFunction.ConfirmDialog("Are you sure?", "Delete Products");
                if (ok)
                {
                    //Database.ExecuteQuery("DELETE FROM Products WHERE ProductCode='" + dataGridView1.Rows[cord].Cells[1].Value.ToString() + "'", "Successfully Deleted");
                    Database.ExecuteQuery("DELETE FROM dbo.Products WHERE ProductCode='" + gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "ProductCode").ToString() + "'", "Successfully Deleted");
                    display();
                }
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtbranch_Click(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT BranchName FROM dbo.Branches", "BranchName", txtbranch);
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            //Database.displayComboBoxItems("SELECT * FROM Branches", "BranchName", comboBox1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string brcode = "";
            brcode = Branch.getBranchCode(comboBox1.Text);
            Database.display("SELECT * FROM view_Products WHERE BranchCode='" + brcode + "'", gridControl1, gridView1);
        }

        //private void txtprodcat_Click(object sender, EventArgs e)
        //{
        //    Database.displayComboBoxItems("SELECT Description FROM dbo.ProductCategory", "Description", txtprodcat);
        //}

        //String getProductCategoryCode()
        //{
        //    string str = "";
        //    str = Database.getSingleQuery("ProductCategory", "Description='" + txtprodcat.Text + "'", "ProductCategoryID");
        //    return str;
        //}

        private void txtprodcat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int newid = 0;
            //string createID = getProductCategoryCode() + "000";
            //if (getLastProductCode() != 0)
            //{
            //    newid = getLastProductCode() + 5;
            //}
            //else
            //{
            //    newid = Convert.ToInt32(createID);
            //}
            //txtprodcode.Text = newid.ToString();
            //txtprodcode.ReadOnly = true;
            //txtdesc.Focus();
            if (action != "UPDATE")
            {
                int newid = 0;
                //string createID = getProductCategoryCode() + "000";
                newid = IDGenerator.getIDNumber("Products", "ProductCode", 1); //getLastProductCode()
                //if (getLastProductCode() != 0)
                //{
                //    newid = IDGenerator.getIDNumber("Products", "ProductCode", 1); //getLastProductCode()
                //}
                //else
                //{
                //    newid = Convert.ToInt32(createID);
                //}
                txtprodcode.Text = HelperFunction.sequencePadding1(newid.ToString(), 5);
                txtprodcode.ReadOnly = true;
                txtdesc.Focus();
            }
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            Database.displayComboBoxItems("SELECT * FROM dbo.ProductCategory", "Description", comboBox2);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Focus();
                return;
            }

            // Database.displayLocalGrid("SELECT * FROM view_Products WHERE ProductCategory ='" + comboBox2.Text + "' AND BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "'", dataGridView1);
            Database.display("SELECT * FROM dbo.view_Products WHERE ProductCategory ='" + comboBox2.Text + "' AND BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "'", gridControl1, gridView1);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                btnbatchupdate.Enabled = true;
                //dataGridView1.ReadOnly = false;
                gridView1.OptionsBehavior.Editable = false;
            }
            else
            {
                btnbatchupdate.Enabled = false;
                gridView1.OptionsBehavior.Editable = true;
                //dataGridView1.ReadOnly = true;
            }
        }

        private void btnbatchupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string mark3;
                for (int i = 0; i <= gridView1.RowCount - 1; i++)
                {
                    mark3 = Branch.getBranchCode(comboBox1.Text);
                    if (chckapplytoall.Checked == true)
                    {
                        Database.ExecuteQuery("Update dbo.Products SET LandingCost='" + gridView1.GetRowCellValue(i, "LandingCost").ToString() + "',SellingPrice='" + gridView1.GetRowCellValue(i, "SellingPrice").ToString() + "',Price1='" + gridView1.GetRowCellValue(i, "Price1").ToString() + "',Price2='" + gridView1.GetRowCellValue(i, "Price2").ToString() + "',Price3='" + gridView1.GetRowCellValue(i, "Price3").ToString() + "',Price4='" + gridView1.GetRowCellValue(i, "Price4").ToString() + "',Price5='" + gridView1.GetRowCellValue(i, "Price5").ToString() + "',Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' WHERE ProductCode='" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "'");
                    }
                    else
                    {
                        Database.ExecuteQuery("Update dbo.Products SET LandingCost='" + gridView1.GetRowCellValue(i, "LandingCost").ToString() + "',SellingPrice='" + gridView1.GetRowCellValue(i, "SellingPrice").ToString() + "',Price1='" + gridView1.GetRowCellValue(i, "Price1").ToString() + "',Price2='" + gridView1.GetRowCellValue(i, "Price2").ToString() + "',Price3='" + gridView1.GetRowCellValue(i, "Price3").ToString() + "',Price4='" + gridView1.GetRowCellValue(i, "Price4").ToString() + "',Price5='" + gridView1.GetRowCellValue(i, "Price5").ToString() + "',Barcode='" + gridView1.GetRowCellValue(i, "Barcode").ToString() + "' WHERE BranchCode='" + Branch.getBranchCode(comboBox1.Text) + "' and ProductCode='" + gridView1.GetRowCellValue(i, "ProductCode").ToString() + "'");
                    }
                }
                XtraMessageBox.Show("Products Successfully Updated");
            }
            catch (SqlException ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            display(comboBox2.Text);
        }

        private void havbarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (havbarcode.Checked == true)
                txtbarcode.Enabled = true;
            else
                txtbarcode.Enabled = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string filepath = "C:\\MyFiles\\";
            SalesInventorySystem.Classes.Utilities.createDirectoryFolder(filepath);
            gridView1.ExportToCsv(filepath);
        }
        private void ExtractDataToCSV(DataGridView dgv)
        {

            // Don't save if no data is returned
            if (dgv.Rows.Count == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            // Column headers
            string columnsHeader = "";
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                columnsHeader += dgv.Columns[i].Name + ",";
            }
            sb.Append(columnsHeader + Environment.NewLine);
            // Go through each cell in the datagridview
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                // Make sure it's not an empty row.
                if (!dgvRow.IsNewRow)
                {
                    for (int c = 0; c < dgvRow.Cells.Count; c++)
                    {
                        // Append the cells data followed by a comma to delimit.

                        sb.Append(dgvRow.Cells[c].Value + ",");
                    }
                    // Add a new line in the text file.
                    sb.Append(Environment.NewLine);
                }
            }
            // Load up the save file dialog with the default option as saving as a .csv file.
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // If they've selected a save location...
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    // Write the stringbuilder text to the the file.
                    sw.WriteLine(sb.ToString());
                }
            }
            // Confirm to the user it has been completed.
            XtraMessageBox.Show("CSV file saved.");
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            string brcode = "";
            brcode = Branch.getBranchCode(comboBox1.Text);

            //Database.displayLocalGrid("SELECT ProductCode,Description,SellingPrice FROM view_Products WHERE BranchCode='" + brcode + "'", dataGridView1);
            Database.display("SELECT ProductCode,Description,SellingPrice FROM dbo.view_Products WHERE BranchCode='" + brcode + "'", gridControl1, gridView1);
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(gridControl1, e.Location);
            }
        }


        private void txtsrchprodname_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string brcode = Branch.getBranchCode(comboBox1.Text);
                Database.display("SELECT TOP(50) * FROM dbo.view_Products WHERE BranchCode='" + brcode + "' " +
                    "AND (Description like '%" + txtsrchprodname.Text + "%' OR Barcode like '%" + txtsrchprodname.Text + "%') ", gridControl1, gridView1);
            }
        }

        private void txtprodcatlookup_EditValueChanged(object sender, EventArgs e)
        {
            txtprodcatobject = SearchLookUpClass.getSingleValue(txtprodcatlookup, "ID");
            if (action != "UPDATE")
            {
                int newid = 0;
                
                newid = IDGenerator.getIDNumber("Products", "ProductCode", 1); //getLastProductCode()
                
                txtprodcode.Text = HelperFunction.sequencePadding1(newid.ToString(), 5);
                txtprodcode.ReadOnly = true;
                txtdesc.Focus();
            }
        }

        private void txtprodtype_EditValueChanged(object sender, EventArgs e)
        {
            txtprodtypeobject = SearchLookUpClass.getSingleValue(txtprodtype, "TypeCode");
            //if (action != "UPDATE")
            //{
            //    int newid = 0;

            //    newid = IDGenerator.getIDNumber("Products", "ProductCode", 1); //getLastProductCode()

            //    txtprodcode.Text = HelperFunction.sequencePadding1(newid.ToString(), 5);
            //    txtprodcode.ReadOnly = true;
            //    txtdesc.Focus();
            //}
        }
    }
}