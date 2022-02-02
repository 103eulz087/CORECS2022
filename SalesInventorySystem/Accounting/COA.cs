using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Accounting
{
    public partial class COA : Form
    {
        public static string code, desc;
        string coaAcctCode, coaDesc, coaAcctType, coaLevelNum, coaMotherAcct, coaglsl, coabranch;
        public COA()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            simpleButton2.Enabled = true;
            simpleButton3.Enabled = false;
            simpleButton4.Enabled = true;

            clearfields();

            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            txtacctcode.ReadOnly = false;
            txtaccttitle.ReadOnly = false;
            txtaccttype.ReadOnly = false;
            txtlevelnum.ReadOnly = false;
            txtmotheracct.ReadOnly = false;
            txtbranch.ReadOnly = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string type = String.Empty, gl = String.Empty;
            if (txtaccttype.Text == "S")
            {
                type = "S";
            }
            else if (txtaccttype.Text == "D")
            {
                type = "D";
            }
            if (radioButton1.Checked == true)
            {
                gl = "G";
            }
            else if (radioButton2.Checked == true)
            {
                gl = "S";
            }
            if (simpleButton2.Text == "Add Account")
            {

                Database.ExecuteQuery("INSERT INTO ChartOfAccounts VALUES('" + txtacctcode.Text + "','" + txtaccttitle.Text + "','" + type + "','" + txtlevelnum.Text + "','" + txtmotheracct.Text + "','" + gl + "','" + txtbranch.Text + "','','','')", "Successfully Added!");
                clearfields();

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                txtacctcode.ReadOnly = true;
                txtaccttitle.ReadOnly = true;
                txtaccttype.ReadOnly = true;
                txtlevelnum.ReadOnly = true;
                txtmotheracct.ReadOnly = true;
                txtbranch.ReadOnly = true;
            }
            else if (simpleButton2.Text == "Update Account")
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                txtacctcode.ReadOnly = false;
                txtaccttitle.ReadOnly = false;
                txtaccttype.ReadOnly = false;
                txtlevelnum.ReadOnly = false;
                txtmotheracct.ReadOnly = false;
                txtbranch.ReadOnly = false;

                Database.ExecuteQuery("UPDATE ChartOfAccounts SET AccountCode='" + txtacctcode.Text + "',Description='" + txtaccttitle.Text + "',AccountType='" + type + "',LevelNumber='" + txtlevelnum.Text + "',SummaryAccount='" + txtmotheracct.Text + "',GLSL='" + gl + "',BranchCode='" + txtbranch.Text + "' WHERE AccountCode='" + txtacctcode.Text + "' ", "Successfully Updated!");
                simpleButton3.Text = "Add Account";
                clearfields();

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                txtacctcode.ReadOnly = true;
                txtaccttitle.ReadOnly = true;
                txtaccttype.ReadOnly = true;
                txtlevelnum.ReadOnly = true;
                txtmotheracct.ReadOnly = true;
                txtbranch.ReadOnly = true;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            txtacctcode.ReadOnly = false;
            txtaccttitle.ReadOnly = false;
            txtaccttype.ReadOnly = false;
            txtlevelnum.ReadOnly = false;
            txtmotheracct.ReadOnly = false;
            txtbranch.ReadOnly = false;

            simpleButton1.Enabled = false;
            simpleButton2.Enabled = true;
            simpleButton2.Text = "Update Account";
            simpleButton3.Enabled = false;
            simpleButton4.Enabled = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = false;
            simpleButton2.Text = "Add Account";
            simpleButton3.Enabled = true;
            simpleButton4.Enabled = true;

            clearfields();

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            txtacctcode.ReadOnly = true;
            txtaccttitle.ReadOnly = true;
            txtaccttype.ReadOnly = true;
            txtlevelnum.ReadOnly = true;
            txtmotheracct.ReadOnly = true;
            txtbranch.ReadOnly = true;
        }

        void clearfields()
        {
            txtacctcode.Text = String.Empty;
            txtaccttitle.Text = String.Empty;
            txtaccttype.Text = String.Empty;
            txtlevelnum.Text = String.Empty;
            txtmotheracct.Text = String.Empty;
            txtbranch.Text = String.Empty;
        }

        private void COA_Load(object sender, EventArgs e)
        {
            simpleButton3.Enabled = true;

            string path = Application.StartupPath + "\\foldericon.ico";
            display();
            ImageList list = new ImageList();
            Image img = Image.FromFile(path);
            list.Images.Add(img);
            img = Image.FromFile(path);
            list.Images.Add(img);
            this.treeView1.ImageList = list;
        }

        private void display()
        {
            treeView1.Nodes.Clear();
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "AccountL0";
            SqlCommand com = new SqlCommand("AccountL0", con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            SqlDataReader reader = com.ExecuteReader();
            TreeNode parentNode = new TreeNode();
            TreeNode level0 = new TreeNode();
            TreeNode level1 = new TreeNode();
            TreeNode level2 = new TreeNode();
            TreeNode level3 = new TreeNode();
            TreeNode level4 = new TreeNode();
            TreeNode level5 = new TreeNode();
            if (reader != null)
            {
                while (reader.Read())
                {
                    code = reader["AccountCode"].ToString();
                    desc = reader["Description"].ToString();
                    string fulltext = code + " = " + desc;
                    if (reader["levelnumber"].ToString() == "0")
                    {
                        parentNode = treeView1.Nodes.Add(fulltext);
                    }
                    if (reader["levelnumber"].ToString() == "1")
                    {
                        if (reader["SummaryAccount"].ToString() == reader["levelnumber"].ToString())
                        {
                            level0 = treeView1.Nodes[0].Nodes.Add(fulltext);
                        }
                        else
                        {
                            level0 = parentNode.Nodes.Add(fulltext);
                        }
                    }
                    if (reader["levelnumber"].ToString() == "2")
                    {
                        level1 = level0.Nodes.Add(fulltext);
                    }
                    if (reader["levelnumber"].ToString() == "3")
                    {
                        level2 = level1.Nodes.Add(fulltext);
                    }
                    if (reader["levelnumber"].ToString() == "4")
                    {
                        level3 = level2.Nodes.Add(fulltext);
                    }
                    if (reader["levelnumber"].ToString() == "5")
                    {
                        level4 = level3.Nodes.Add(fulltext);
                    }
                    if (reader["levelnumber"].ToString() == "6")
                    {
                        level5 = level4.Nodes.Add(fulltext);
                    }
                }
                reader.Close();
                parentNode.EnsureVisible();
                con.Close();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string source = treeView1.SelectedNode.Text.ToString();
            String[] separator = new String[] { " = ", " ", "=" };
            string[] result = source.Split(separator, StringSplitOptions.None);
            string searchString = result[0];
            SqlConnection con = Database.getConnection();
            con.Open();
            string query = "SELECT * FROM ChartOfAccounts WHERE AccountCode = '" + searchString + "'";
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                coaAcctCode = reader["AccountCode"].ToString();
                coaDesc = reader["Description"].ToString();
                coaAcctType = reader["AccountType"].ToString();
                coaLevelNum = reader["LevelNumber"].ToString();
                coaMotherAcct = reader["SummaryAccount"].ToString();
                coaglsl = reader["GLSL"].ToString();
                coabranch = reader["BranchCode"].ToString();
            }
            if (coaglsl == "S")
            {
                radioButton2.Checked = true;
            }
            else if (coaglsl == "G")
            {
                radioButton1.Checked = true;
            }
            txtacctcode.Text = coaAcctCode;
            txtaccttitle.Text = coaDesc;
            txtaccttype.Text = coaAcctType;
            txtlevelnum.Text = coaLevelNum;
            txtmotheracct.Text = coaMotherAcct;
            txtbranch.Text = coabranch;
        }

        private void txtbranch_Click(object sender, EventArgs e)
        {
            Database.getSingleQuery("Branches", "BranchCode <> '' ", "BranchCode");
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(treeView1, e.Location);
            }
        }

        private void refreshDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}
