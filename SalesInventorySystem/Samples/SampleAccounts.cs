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
using Npgsql;
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.Samples
{
    public partial class SampleAccounts : DevExpress.XtraEditors.XtraForm
    {
        public SampleAccounts()
        {
            InitializeComponent();
        }

        private void SampleAccounts_Load(object sender, EventArgs e)
        {
            populate();
        }

        void populate()
        {
            //string sql = @"SELECT ""AccountId"", ""AccountObjectId"", ""UserId"", ""FirstName"", ""LastName"", 
            //       ""MiddleName"", ""Email"", ""Age"", ""Gender"", ""BirthDate"", ""MobileNumber"", 
            //       ""PointPercentage"", ""AccountTypeId"", ""ReferralKey"", ""IsActive"", 
            //       ""AccountStatusId"", ""SalaryRange"", ""Remarks"", ""ManagerAccountId"", 
            //       ""LastSetPassword"", ""CreatedOn"", ""CreatedBy"", ""LastModified"", 
            //       ""ModifiedBy"", ""OfficerGroupId"", ""AccountBalance"", ""ProfileImage"", 
            //       ""OrganizationId"", ""Label"", ""AccountBonusObjectId"", ""AccountCreditObjectId"" 
            //       FROM public.""Account"" WHERE ""AccountTypeId"" = 6";
            //string sql = @"SELECT ""ManagerAccountId"",""AccountId"", ""FirstName"", ""LastName""   
            //       FROM public.""Account"" WHERE ""AccountTypeId"" = 7";
            string sql = $@"
                            SELECT 
                                a.""ManagerAccountId"", 
                                a.""AccountId"", 
                                a.""FirstName"", 
                                a.""LastName"",
                                COUNT(p.""PlayerId"") AS ""NoOfPlayers""
                            FROM public.""Account"" a
                            LEFT JOIN public.""Player"" p ON a.""AccountId"" = p.""ReferedByAccountId""
                            WHERE a.""AccountTypeId"" = 7 
                            GROUP BY a.""AccountId"", a.""ManagerAccountId"", a.""FirstName"", a.""LastName""";

            Database.displayPg(sql, gridControl1, gridView1);
            gridView1.Columns[0].Visible = false;
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            // 1. Get the raw value from the grid
            string rawValue = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AccountId").ToString();

            // 2. Check for DBNull and handle it
            //long managerId = (rawValue == null || rawValue == DBNull.Value) ? 0 : Convert.ToInt64(rawValue);

            // 3. Now build your query
            if (Convert.ToInt64(rawValue) > 0)
            {
                //string sql = $@"SELECT ""ManagerAccountId"",""AccountId"", ""FirstName"", ""LastName""
                //    FROM public.""Account"" 
                //    WHERE ""AccountTypeId"" = 8 
                //    AND ""ManagerAccountId"" = {rawValue}";
                string sql = $@"
                            SELECT 
                                a.""ManagerAccountId"", 
                                a.""AccountId"", 
                                a.""FirstName"", 
                                a.""LastName"",
                                COUNT(p.""PlayerId"") AS ""NoOfPlayers""
                             
                            FROM public.""Account"" a
                            LEFT JOIN public.""Player"" p ON a.""AccountId"" = p.""ReferedByAccountId""
                       
                            WHERE a.""AccountTypeId"" = 8 
                            AND a.""ManagerAccountId"" = {rawValue}
                            GROUP BY a.""AccountId"", a.""ManagerAccountId"", a.""FirstName"", a.""LastName""";

                //string sql = $@"
                //            SELECT 
                //                a.""ManagerAccountId"", 
                //                a.""AccountId"", 
                //                a.""FirstName"", 
                //                a.""LastName"",
                //                -- Subquery to count Downline Agents
                //                (SELECT COUNT(*) 
                //                 FROM public.""Account"" subA 
                //                 WHERE subA.""ManagerAccountId"" = a.""AccountId"" 
                //                 AND subA.""AccountTypeId"" = 8) AS ""NoOfAgents"",
                //                -- Subquery to count Players
                //                (SELECT COUNT(*) 
                //                 FROM public.""Player"" p 
                //                 WHERE p.""ReferedByAccountId"" = a.""AccountId"") AS ""NoOfPlayers""
                //            FROM public.""Account"" a
                //            WHERE a.""AccountTypeId"" = 8 
                //            AND a.""ManagerAccountId"" = {rawValue}";

                Database.displayPg(sql, gridControl2, gridView2);
                gridView2.Columns[0].Visible = false;
            }
            else
            {
                XtraMessageBox.Show("Please select a valid Manager.");
            }
        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            // 1. Get the raw value from the grid
            string rawValue = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "AccountId").ToString();

            // 2. Check for DBNull and handle it
            //long managerId = (rawValue == null || rawValue == DBNull.Value) ? 0 : Convert.ToInt64(rawValue);

            // 3. Now build your query
            if (Convert.ToInt64(rawValue) > 0)
            {
                //string sql = $@"SELECT ""ManagerAccountId"",""AccountId"", ""FirstName"", ""LastName""
                //    FROM public.""Account"" 
                //    WHERE ""AccountTypeId"" = 9 
                //    AND ""ManagerAccountId"" = {rawValue}";
                string sql = $@"
                            SELECT 
                                a.""ManagerAccountId"", 
                                a.""AccountId"", 
                                a.""FirstName"", 
                                a.""LastName"",
                                COUNT(p.""PlayerId"") AS ""NoOfPlayers""
                            FROM public.""Account"" a
                            LEFT JOIN public.""Player"" p ON a.""AccountId"" = p.""ReferedByAccountId""
                            WHERE a.""AccountTypeId"" = 9 
                            AND a.""ManagerAccountId"" = {rawValue}
                            GROUP BY a.""AccountId"", a.""ManagerAccountId"", a.""FirstName"", a.""LastName""";

                Database.displayPg(sql, gridControl3, gridView3);
                gridView3.Columns[0].Visible = false;
            }
            else
            {
                XtraMessageBox.Show("Please select a valid Manager.");
            }
        }

        private void gridView3_Click(object sender, EventArgs e)
        {
            // 1. Get the raw value from the grid
            string rawValue = gridView3.GetRowCellValue(gridView3.FocusedRowHandle, "AccountId").ToString();

            // 2. Check for DBNull and handle it
            //long managerId = (rawValue == null || rawValue == DBNull.Value) ? 0 : Convert.ToInt64(rawValue);

            // 3. Now build your query
            if (Convert.ToInt64(rawValue) > 0)
            {
                //string sql = $@"SELECT ""ManagerAccountId"",""AccountId"", ""FirstName"", ""LastName"" 
                //    FROM public.""Account"" 
                //    WHERE ""AccountTypeId"" = 10 
                //    AND ""ManagerAccountId"" = {rawValue}";
                string sql = $@"
                            SELECT 
                                a.""ManagerAccountId"", 
                                a.""AccountId"", 
                                a.""FirstName"", 
                                a.""LastName"",
                                COUNT(p.""PlayerId"") AS ""NoOfPlayers""
                            FROM public.""Account"" a
                            LEFT JOIN public.""Player"" p ON a.""AccountId"" = p.""ReferedByAccountId""
                            WHERE a.""AccountTypeId"" = 10 
                            AND a.""ManagerAccountId"" = {rawValue}
                            GROUP BY a.""AccountId"", a.""ManagerAccountId"", a.""FirstName"", a.""LastName""";

                Database.displayPg(sql, gridControl4, gridView4);
                gridView4.Columns[0].Visible = false;
            }
            else
            {
                XtraMessageBox.Show("Please select a valid Manager.");
            }
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControl1, e.Location);
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(gridControl2, e.Location);
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip3.Show(gridControl3, e.Location);
        }

        private void gridControl4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip4.Show(gridControl4, e.Location);
        }

        private void OpenPlayerSample(GridView sourceView)
        {
            // 1. Safety Check: Ensure a row is selected and not null
            object accId = sourceView.GetRowCellValue(sourceView.FocusedRowHandle, "AccountId");
            if (accId == null || accId == DBNull.Value)
            {
                XtraMessageBox.Show("Please select a valid account.");
                return;
            }

            // 2. Initialize the Sample Form
            Samples.SamplePlayers sample = new Samples.SamplePlayers();

            // 3. Build Query (using string interpolation)
            string sql = $@"SELECT ""PlayerId"", ""PlayerAccountId"", ""FirstName"", ""LastName"", ""MobileNumber"", ""AccountStatus"", ""DateRegistered"" 
                    FROM public.""Player"" 
                    WHERE ""ReferedByAccountId"" = {accId}";

            // 4. Populate the target grid
            Database.displayPg(sql, sample.gridControl1, sample.gridView1);

            // 5. Hide ID columns in the POPUP's grid view
            // Note: Use sample.gridView1 here if that is where the columns should be hidden
            if (sample.gridView1.Columns.Count > 1)
            {
                sample.gridView1.Columns[0].Visible = false;
                sample.gridView1.Columns[1].Visible = false;
            }

            sample.ShowDialog(this);
        }

        private void showPlayersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPlayerSample(gridView1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenPlayerSample(gridView2);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenPlayerSample(gridView3);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenPlayerSample(gridView4);
        }
    }
}