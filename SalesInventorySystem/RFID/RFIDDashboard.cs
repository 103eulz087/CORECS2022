using DevExpress.XtraEditors;
using SalesInventorySystem.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.RFID
{
    public partial class RFIDDashboard : Form
    {// 1. Thread-safe queue for raw, rapid-fire reads from the SDK
     //private ConcurrentQueue<string> _rawReadsQueue = new ConcurrentQueue<string>();

        //// 2. HashSet for lightning-fast deduplication (Maintained on UI thread)
        //private HashSet<string> _uniqueTags = new HashSet<string>();

        //// 3. Binding list directly attached to your DataGridView
        //private BindingList<ScannedAsset> _gridData = new BindingList<ScannedAsset>();

        //// 4. Timer to periodically process the queue without locking the UI
        //private Timer _uiBatchTimer;
        // 1. We still need these to track duplicates and bind to the grid
        private HashSet<string> _uniqueTags = new HashSet<string>();
        private BindingList<ScannedAsset> _gridData = new BindingList<ScannedAsset>();
        private BindingList<FoundInventoryItem> _foundItems = new BindingList<FoundInventoryItem>();
        private BindingList<NotFoundInventoryItem> _notFoundItems = new BindingList<NotFoundInventoryItem>();
        //private string _connectionString = "Your_Database_Connection_String_Here";
        public RFIDDashboard()
        {
            InitializeComponent();
            // Setup the UI update timer (Processes every 500ms)
            // 2. Bind the BindingList to your DevExpress GridControl
            gridControlGather.DataSource = _gridData;
            gridViewGather.OptionsView.ColumnAutoWidth = true;

            // 3. Ensure the scanner textbox is ready to receive input when the screen loads
            this.Load += (s, e) => { txtScannerInput.Focus(); };

        }

        // This simulates the event fired by your RFID Reader SDK
        // This usually happens on a background worker thread created by the SDK
        //private void OnRfidTagRead(string epcCode)
        //{
        //    // Simply push to the queue and get out. Extremely fast, no blocking.
        //    _rawReadsQueue.Enqueue(epcCode);
        //}

        //// This runs on the UI thread every 500ms
        //// DevExpress grids need to be refreshed safely when updating from background timers
        //private void ProcessQueuedReads(object sender, EventArgs e)
        //{
        //    bool newTagsAdded = false;

        //    // Use BeginUpdate to pause grid repaints while we add a batch of tags
        //    gridView1.BeginUpdate();

        //    while (_rawReadsQueue.TryDequeue(out string epc))
        //    {
        //        if (_uniqueTags.Add(epc))
        //        {
        //            _gridData.Add(new ScannedAsset { EPC = epc });
        //            newTagsAdded = true;
        //        }
        //    }

        //    // EndUpdate forces a single, clean redraw
        //    gridView1.EndUpdate();

        //    if (newTagsAdded)
        //    {
        //        lblCount.Text = $"Unique Assets Scanned: {_uniqueTags.Count}";
        //    }
        //}

        private void txtScannerInput_KeyDown(object sender, KeyEventArgs e)
        {
            // Most wedge scanners automatically send an "Enter" keystroke after the barcode/RFID
            if (e.KeyCode == Keys.Enter)
            {
                // Stop the annoying Windows "ding" sound when pressing Enter in a single-line textbox
                e.SuppressKeyPress = true;

                // 1. Grab the scanned text and clean up any accidental spaces
                string scannedEpc = txtScannerInput.Text.Trim();

                // 2. Clear the textbox IMMEDIATELY so it's ready for the next lightning-fast scan
                txtScannerInput.Text = string.Empty;

                // 3. Ignore empty scans
                if (string.IsNullOrEmpty(scannedEpc)) return;

                // 4. Check for duplicates using the HashSet
                if (_uniqueTags.Add(scannedEpc))
                {
                    // 5. It's a new tag! Add it to the BindingList. 
                    // This instantly displays it on your DevExpress GridView.
                    _gridData.Add(new ScannedAsset
                    {
                        EPC = scannedEpc,
                        Status = "Preview (Unverified)"
                    });

                    // Update your counter label
                    //lblCount.Text = $"Preview Count: {_uniqueTags.Count} unique tags";
                    lbltotcount.Text = $" {_uniqueTags.Count} ";
                }
            }
        }

        private void RFIDDashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (_uniqueTags.Count == 0)
            {
                XtraMessageBox.Show("There are no scanned barcodes to retrieve.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Clear previous results
            _foundItems.Clear();
            _notFoundItems.Clear();

            // 2. Prepare the TVP DataTable from our HashSet
            DataTable tvpTable = new DataTable();
            tvpTable.Columns.Add("EPCCode", typeof(string));
            foreach (string barcode in _uniqueTags)
            {
                tvpTable.Rows.Add(barcode);
            }

            try
            {
                // 3. Execute the Database Query
                using (SqlConnection conn = Database.getConnection())
                using (SqlCommand cmd = new SqlCommand("dbo.ProcessScannedInventory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter tvpParam = cmd.Parameters.AddWithValue("@ScannedTags", tvpTable);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.ScannedRFIDList"; // Must match your SQL Type

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string barcode = reader["ScannedBarcode"].ToString();
                            bool isFound = Convert.ToBoolean(reader["IsFound"]);

                            if (isFound)
                            {
                                // 4a. Add to Found List
                                _foundItems.Add(new FoundInventoryItem
                                {
                                    Barcode = barcode,
                                    ItemCode = reader["ItemCode"]?.ToString(),
                                    Description = reader["Description"]?.ToString(),
                                    BrandName = reader["BrandName"]?.ToString(),
                                    Category = reader["Category"]?.ToString(),
                                    Condition = reader["Condition"]?.ToString(),
                                    Location = reader["Location"]?.ToString(),
                                    Department = reader["Department"]?.ToString(),
                                    Custodian = reader["Custodian"]?.ToString()
                                });
                            }
                            else
                            {
                                // 4b. Add to Not Found List
                                _notFoundItems.Add(new NotFoundInventoryItem
                                {
                                    ScannedBarcode = barcode
                                });
                            }
                        }
                    }
                }

                // 5. Bind data to DevExpress GridControls
                gridControlFound.DataSource = _foundItems;
                gridControlnotfound.DataSource = _notFoundItems;

                // Optional: Tell DevExpress to auto-size the columns to fit the data
                gridViewFound.OptionsView.ColumnAutoWidth = true;
                gridViewnotfound.OptionsView.ColumnAutoWidth = true;

                // 6. Update the Labels
                lblfound.Text = _foundItems.Count.ToString();
                lblnotfound.Text = _notFoundItems.Count.ToString();

                // Optional: Clear the initial preview list now that processing is done
                _uniqueTags.Clear();
                _gridData.Clear(); // Assuming _gridData is the BindingList for your preview grid
                //lblScanCount.Text = "Preview Count: 0 unique tags";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"An error occurred during retrieval:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    public class FoundInventoryItem
    {
        public string Barcode { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Custodian { get; set; }
    }

    public class NotFoundInventoryItem
    {
        public string ScannedBarcode { get; set; }
        public string Status { get; set; } = "Not found in database";
    }
}
