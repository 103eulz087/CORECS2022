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
using DevExpress.XtraGrid.Views.Grid;

namespace SalesInventorySystem.Reporting
{
    public partial class ViewTicketDevExRep : DevExpress.XtraEditors.XtraForm
    {
        public ViewTicketDevExRep()
        {
            InitializeComponent();
        }

        private void ViewTicketDevExRep_Load(object sender, EventArgs e)
        {
            loadBranch();
        }

        void loadBranch()
        {
            Database.displaySearchlookupEdit("Select BranchCode,BranchName FROM Branches Order By BranchCode", txtbrcode, "BranchCode", "BranchCode");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Database.GridMasterDetail("SELECT TicketDate,TicketNumber,ReferenceNumber,ReferenceKey,Owner,Particulars,EnteredBy FROM TicketMaster WHERE TicketDate between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and BranchCode='" + txtbrcode.Text + "'", "SELECT TicketDate,TicketNumber,ReferenceNumber,ReferenceKey,AccountCode,Description,Debit,Credit FROM view_TicketDetails WHERE TicketDate between '" + txtdatefrom.Text + "' and '" + txtdateto.Text + "' and BranchCode='" + txtbrcode.Text + "'", "TicketMaster", "TicketDetails", "TicketNumber", "TicketDate", "TicketNumber", "TicketDate", "TicketDetails", gridControlTicketSummary, gridView3,"");
        }

        private void gridViewTicketSummary_MasterRowGetLevelDefaultView(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventArgs e)
        {
            //e.DefaultView = new GridView(gridControlTicketSummary);
        }

        private void gridControlTicketSummary_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            //GridView view = e.View as GridView;
            //GridView view = new GridView(gridControlTicketSummary);
            GridView view = (GridView)e.View;
            if (view.Name == "gridViewTicketSummary")
            {
                view.OptionsView.RowAutoHeight = true;
                view.OptionsBehavior.ReadOnly = true;
                view.OptionsBehavior.Editable = false;
                view.BestFitColumns();
            }

        }

        private void gridViewTicketSummary_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            gridView3.BestFitColumns();
            gridViewTicketSummary.BestFitColumns();
        }
    }
}