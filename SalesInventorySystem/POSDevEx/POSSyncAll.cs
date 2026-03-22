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
using SalesInventorySystem.Classes;

namespace SalesInventorySystem.POSDevEx
{
    public partial class POSSyncAll : DevExpress.XtraEditors.XtraForm
    {
        public POSSyncAll()
        {
            InitializeComponent();
        }

        private async void btnsync_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtcombotables.Text))
            {
                BigAlert.Show("SELECTION REQUIRED", "Please select what you want to sync from the dropdown menu.", MessageBoxIcon.Warning);
                return;
            }
            string selectedSyncType = txtcombotables.Text; // Grab the text from the DevExpress Combo Box
            string currentBranchCode = Login.assignedBranch;
            try
            {
                // 1. LOCK THE UI AND SHOW SPINNER
                // This prevents the cashier from clicking the button twice
                btnsync.Enabled = false;
                txtcombotables.Enabled = false;
                progressSpinner.Visible = true;
                progressBar.EditValue = 0;
                lblStatus.Text = "Connecting to cloud server...";

                // 2. SETUP PROGRESS HANDLERS
                // This bridges the background thread safely to the DevExpress UI thread
                var progressHandler = new Progress<int>(percent =>
                {
                    progressBar.EditValue = percent; // Fills the progress bar
                });

                var statusHandler = new Progress<string>(message =>
                {
                    lblStatus.Text = message; // Updates the label
                    progressSpinner.Description = message; // Updates the text under the spinner
                });

                // Grab the branch code (you likely have this saved in a global static variable)
              

                // 3. EXECUTE THE DOWNLOADER IN THE BACKGROUND
                PosDataDownloader downloader = new PosDataDownloader();
                // 2. PASS THE COMBO BOX SELECTION TO THE NEW METHOD
                await Task.Run(async () =>
                {
                    await downloader.SyncSpecificTableAsync(selectedSyncType, currentBranchCode, progressHandler, statusHandler);
                });
                // We wrap it in Task.Run to guarantee the UI thread stays 100% responsive


                // 4. SUCCESS!
                BigAlert.Show("SYNC COMPLETE", $"Successfully synchronized: {selectedSyncType}", MessageBoxIcon.Information);
                progressBar.EditValue = 100;

                XtraMessageBox.Show("Morning sync complete! All prices, products, and users are up to date.",
                    "Sync Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Optional: Automatically close the sync form and open the Main POS screen here
                // this.DialogResult = DialogResult.OK;
                // this.Close();
            }
            catch (Exception ex)
            {
                // 5. HANDLE FAILURES GRACEFULLY
                lblStatus.Text = "Sync failed.";
                BigAlert.Show("SYNC ERROR", $"Could not synchronize data.\n\nError: {ex.Message}", MessageBoxIcon.Error);
            }
            finally
            {
                // 6. UNLOCK THE UI
                // This runs no matter what (success or crash), so the app never gets stuck
                btnsync.Enabled = true;
                txtcombotables.Enabled = true; // Unlock the dropdown
                progressSpinner.Visible = false;
            }
        }

        private void POSSyncAll_Load(object sender, EventArgs e)
        {
            
        }
    }
}