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

namespace SalesInventorySystem.Forwarding
{
    public partial class ForwardingAddSmallTrucks : DevExpress.XtraEditors.XtraForm
    {
        DataTable table;
        public ForwardingAddSmallTrucks()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRow newRow = table.NewRow();
            table.Rows.Add(newRow);
            gridControlMain.DataSource = table;
        }

        private void ForwardingAddSmallTrucks_Load(object sender, EventArgs e)
        {
            populate();
            txttripid.Text = IDGenerator.getIDbySP("sp_GetTripID", "tripnumber", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding")); //IDGenerator.getShipmentNumber().ToString();
            table = new DataTable();
            table.Columns.Add("InvoiceNo");
            table.Columns.Add("Customer");
            table.Columns.Add("Boxes");
            table.Columns.Add("TotalKilo");
            table.Columns.Add("From");
            table.Columns.Add("To");
            table.Columns.Add("DateDeparture");
            table.Columns.Add("DateArrival");
            table.Columns.Add("Remarks");
            gridControlMain.DataSource = table;
            gridViewMain.BestFitColumns();
        }
        void populate()
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches", txtbranch, "BranchCode", "BranchCode");
            Database.displaySearchlookupEdit("Select CustomerID,CustomerName FROM Customers", txtconsignee, "CustomerID", "CustomerID");
            Database.displaySearchlookupEdit("Select SequenceNumber,FirstName,MiddleName,LastName FROM EmployeeInfo Where Designation='Driver'", txtdriver, "FirstName", "SequenceNumber", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            Database.displaySearchlookupEdit("Select PlateNo,Make,Series,BodyType FROM TruckDetails", txtplateno, "PlateNo", "PlateNo", Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(gridControlMain, e.Location);
        }

        private void cancelLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridViewMain.DeleteSelectedRows();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string invoiceno, customer, boxes, kilos, from, to, datedeparture, datearrival, remarks;
           
            for(int i=0;i<=gridViewMain.RowCount-1;i++)
            {
                invoiceno = gridViewMain.GetRowCellValue(i, "InvoiceNo").ToString();
                customer = gridViewMain.GetRowCellValue(i, "Customer").ToString();
                boxes = gridViewMain.GetRowCellValue(i, "Boxes").ToString();
                kilos = gridViewMain.GetRowCellValue(i, "TotalKilo").ToString();
                from = gridViewMain.GetRowCellValue(i, "From").ToString();
                to = gridViewMain.GetRowCellValue(i, "To").ToString();
                datedeparture = gridViewMain.GetRowCellValue(i, "DateDeparture").ToString();
                datearrival = gridViewMain.GetRowCellValue(i, "DateArrival").ToString();
                remarks = gridViewMain.GetRowCellValue(i, "Remarks").ToString();
                Database.ExecuteLocalQuery("INSERT INTO TripTicketDetails VALUES ('" + txttripid.Text + "','"+txtbranch.Text+"','"+ invoiceno + "','','"+customer+"','"+boxes+"','"+kilos+"','"+from+"','"+to+"','"+datedeparture+"','"+datearrival+"','','"+remarks+"')",Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            }
            Database.ExecuteLocalQuery("INSERT INTO TripTicketMaster VALUES('"+txttripid.Text+"','"+txtbranch.Text+"','"+txtconsignee.Text+"','"+txtdriver.Text+"','"+txtplateno.Text+"','"+txthelper.Text+"','','"+txtrate.Text+"',0,'"+txtrate.Text+"','UNPAID',0,0,0,0,'PENDING','"+Login.Fullname+"','"+ DateTime.Now.ToShortDateString() + "','"+txtdatestart.Text+"','"+txtdateend.Text+"')",Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding"));
            updateTickets();
            XtraMessageBox.Show("Successfully Added");
        }

        void updateTickets()
        {
            SqlConnection con = null;
            try
            {
                con = Database.getCustomizeConnection(@"ITCSI\ConnSettingsForwarding");
                con.Open();
                string query = "sp_UpdateTripTickets";
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@parmtripno", txttripid.Text);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                com.ExecuteNonQuery();
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

        private void gridViewMain_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "DateDeparture")
                e.RepositoryItem = repdatefrom;
            if (e.Column.FieldName == "DateArrival")
                e.RepositoryItem = repdateto;
        }
    }
}