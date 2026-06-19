using DevExpress.XtraEditors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POSDevEx
{
        public partial class POSDataManagementAutomation : XtraForm
        {

        private readonly ConcurrentQueue<PrintJob> _printQueue = new ConcurrentQueue<PrintJob>();
        private CancellationTokenSource _printCts = new CancellationTokenSource();
        public class PrintJob
        {
            public string BranchCode { get; set; }
            public string DateExecute { get; set; }
            public string MachineUsed { get; set; }
        }
        private bool _isProcessing = false;
        private bool _isRunning = false; // controls Start/Stop
        private int _totalBatches = 0;
        private int _processedCount = 0;
        private int _skippedCount = 0;

        // Change this to your testing target if needed
        private readonly decimal _targetAmount = 30336.08m;

        public POSDataManagementAutomation()
        {
            InitializeComponent();
        }

        public sealed class ProcessingBatch
        {
            public string BranchCode { get; set; }
            public DateTime SalesDate { get; set; }
            public string MachineUsed { get; set; }
            public decimal TotalNetSales { get; set; }
            public decimal VatExemptSale { get; set; }
        }

        //private async void POSDataManagementAutomation_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Run once immediately
        //        await RunProcessingCycleAsync();

        //        // Then poll every 5 seconds
        //        timer1.Interval = 5000;
        //        timer1.Start();
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "Load Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private async void timer1_Tick(object sender, EventArgs e)
        {
            //if (_isProcessing) return;

            //await RunProcessingCycleAsync();

            if (!_isRunning) return;       // ✅ only run if started
            if (_isProcessing) return;     // ✅ prevent overlap

            _isProcessing = true;

            try
            {
                await RunProcessingCycleAsync(); // ✅ correct method
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
            finally
            {
                _isProcessing = false;
            }

        }
        private async Task RunProcessingCycleAsync()
        {
            //if (_isProcessing) return;

            //_isProcessing = true;
            timer1.Stop();

            try
            {
                List<ProcessingBatch> batches = await GetPendingBatchesAsync();

                _totalBatches = batches.Count;
                _processedCount = 0;
                _skippedCount = 0;

                if (_totalBatches == 0)
                {
                    lblStatus.Text = "No pending batches";
                    lblProgress.Text = "0 / 0";
                    return;
                }

                lblStatus.Text = "Running...";
                lblStatus.ForeColor = Color.Green;

                // ✅ convert percentage safely
                decimal percentage = 0m;
                decimal.TryParse(txtpercentage.Text, out percentage);

                //for (int i = 0; i < _totalBatches; i++)
                //{
                //    ProcessingBatch batch = batches[i];

                //    await ReplicateBatchAsync(batch);

                //    // ✅ UPDATE PROGRESS UI
                //    lblCurrentBatch.Text = $"{batch.BranchCode} | {batch.SalesDate:yyyy-MM-dd} | {batch.MachineUsed}";
                //    //lblProgress.Text = $"{i + 1} / {_totalBatches}";

                //    double percent = ((double)(i + 1) / _totalBatches) * 100;
                //    lblProgress.Text = $"{i + 1} / {_totalBatches} ({percent:N1}%)";

                //    Application.DoEvents(); // keep UI responsive (optional)

                //    // ✅ VALIDATION
                //    if (string.IsNullOrWhiteSpace(batch.BranchCode) ||
                //        batch.SalesDate == DateTime.MinValue ||
                //        string.IsNullOrWhiteSpace(batch.MachineUsed))
                //    {
                //        _skippedCount++;
                //        continue;
                //    }

                //    DataTable dt = await LoadData2Async(
                //        batch.BranchCode,
                //        batch.SalesDate,
                //        batch.MachineUsed);

                //    if (dt == null || dt.Rows.Count == 0)
                //    {
                //        _skippedCount++;
                //        continue;
                //    }

                //    // ✅ COMPUTE TARGET
                //    decimal target = 0m;

                //    if (radtypeall.Checked)
                //    {
                //        target = batch.TotalNetSales * percentage;
                //    }
                //    else if (radtypevatex.Checked)
                //    {
                //        target = batch.VatExemptSale * percentage;
                //    }
                //    else
                //    {
                //        target = batch.TotalNetSales * percentage;
                //    }

                //    // ✅ PROCESS
                //    ProcessDataTable(dt, target);

                //    // ✅ EXECUTE
                //    await ExecuteAsync(dt, batch);
                //    //PrintZRead(batch.BranchCode, batch.SalesDate.ToString("yyyy-MM-dd"), batch.MachineUsed);

                //    // ✅ ENQUEUE PRINT (THIS IS THE RIGHT PLACE)
                //    _printQueue.Enqueue(new PrintJob
                //    {
                //        BranchCode = batch.BranchCode,
                //        DateExecute = batch.SalesDate.ToString("yyyy-MM-dd"),
                //        MachineUsed = batch.MachineUsed
                //    });

                //    _processedCount++;
                //}
                for (int i = 0; i < _totalBatches; i++)
                {
                    ProcessingBatch batch = batches[i];

                    await ReplicateBatchAsync(batch);

                    // Update Progress UI
                    lblCurrentBatch.Text = $"{batch.BranchCode} | {batch.SalesDate:yyyy-MM-dd} | {batch.MachineUsed}";
                    double percent = ((double)(i + 1) / _totalBatches) * 100;
                    lblProgress.Text = $"{i + 1} / {_totalBatches} ({percent:N1}%)";

                    // REMOVED: Application.DoEvents(); 

                    if (string.IsNullOrWhiteSpace(batch.BranchCode) ||
                        batch.SalesDate == DateTime.MinValue ||
                        string.IsNullOrWhiteSpace(batch.MachineUsed))
                    {
                        _skippedCount++;
                        continue;
                    }

                    DataTable dt = await LoadData2Async(batch.BranchCode, batch.SalesDate, batch.MachineUsed);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        _skippedCount++;
                        continue;
                    }

                    // Compute Target
                    decimal target = radtypeall.Checked ? batch.TotalNetSales * percentage :
                                     radtypevatex.Checked ? batch.VatExemptSale * percentage :
                                     batch.TotalNetSales * percentage;

                    // Offload the synchronous data-crunching to a background thread to keep UI smooth
                    await Task.Run(() => ProcessDataTable(dt, target));

                    // Execute SQL
                    await ExecuteAsync(dt, batch);

                    _printQueue.Enqueue(new PrintJob
                    {
                        BranchCode = batch.BranchCode,
                        DateExecute = batch.SalesDate.ToString("yyyy-MM-dd"),
                        MachineUsed = batch.MachineUsed
                    });

                    _processedCount++;
                }

                // ✅ FINAL SUMMARY
                lblStatus.Text = $"Done | Processed: {_processedCount}, Skipped: {_skippedCount}";
                lblStatus.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Error";
                lblStatus.ForeColor = Color.Red;
                XtraMessageBox.Show(ex.Message, "Processing Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isProcessing = false;

                if (_isRunning)
                    timer1.Start();
            }
        }
        //private async Task RunProcessingCycleAsync()
        //{
        //    if (_isProcessing) return;

        //    _isProcessing = true;
        //    timer1.Stop();

        //    try
        //    {
        //        List<ProcessingBatch> batches = await GetPendingBatchesAsync();

        //        _totalBatches = batches.Count;
        //        _processedCount = 0;
        //        _skippedCount = 0;

        //        if (_totalBatches == 0)
        //        {
        //            lblStatus.Text = "No pending batches";
        //            lblProgress.Text = "0 / 0";
        //            return;
        //        }
        //        lblStatus.Text = "Running...";
        //        lblStatus.ForeColor = Color.Green;

        //        if (batches.Count == 0)
        //            return;

        //        foreach (ProcessingBatch batch in batches)
        //        {
        //            if (string.IsNullOrWhiteSpace(batch.BranchCode)) continue;
        //            if (batch.SalesDate == DateTime.MinValue) continue;
        //            if (string.IsNullOrWhiteSpace(batch.MachineUsed)) continue; 

        //            DataTable dt = await LoadData2Async(
        //                batch.BranchCode,
        //                batch.SalesDate,
        //                batch.MachineUsed);

        //            if (dt == null || dt.Rows.Count == 0)
        //            {
        //                //await MarkBatchProcessedAsync(
        //                //    batch.BranchCode,
        //                //    batch.SalesDate,
        //                //    batch.MachineUsed);
        //                continue;
        //            }

        //            // ✅ NEW: compute dynamic target
        //            decimal target = 0m;
        //            decimal percentage = Convert.ToDecimal(txtpercentage.Text);
        //            if(radtypeall.Checked == true)
        //            {
        //                target = batch.TotalNetSales * percentage;//0.30m;
        //            }
        //            else if(radtypevatex.Checked==true)
        //            {
        //                target = batch.VatExemptSale * percentage; //0.30m;
        //            }
        //            else
        //            {
        //                target = batch.TotalNetSales * percentage; // 0.30m;
        //            }

        //            ProcessDataTable(dt, target);

        //            // Save selected preview rows
        //            await ExecuteAsync(dt, batch);

        //            // Mark current batch as processed
        //            //await MarkBatchProcessedAsync(
        //            //    batch.BranchCode,
        //            //    batch.SalesDate,
        //            //    batch.MachineUsed);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.Message, "Processing Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    finally
        //    {
        //        _isProcessing = false;
        //        timer1.Start();
        //    }
        //}

        private async Task<List<ProcessingBatch>> GetPendingBatchesAsync()
        {
            List<ProcessingBatch> batches = new List<ProcessingBatch>();

            using (SqlConnection conn = Database.getConnection())
            using (SqlCommand cmd = new SqlCommand("sp_GetSalesDateForDataProcessing", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        batches.Add(new ProcessingBatch
                        {
                            BranchCode = reader["BranchCode"] == DBNull.Value
                                ? string.Empty
                                : Convert.ToString(reader["BranchCode"]),

                            SalesDate = reader["DateExecute"] == DBNull.Value
                                ? DateTime.MinValue
                                : Convert.ToDateTime(reader["DateExecute"]),

                            MachineUsed = reader["MachineUsed"] == DBNull.Value
                                ? string.Empty
                                : Convert.ToString(reader["MachineUsed"]),

                            TotalNetSales = reader["TotalNetSales"] == DBNull.Value 
                                ? 0m : Convert.ToDecimal(reader["TotalNetSales"]),

                            VatExemptSale = reader["VatExemptSale"] == DBNull.Value
                                ? 0m : Convert.ToDecimal(reader["VatExemptSale"])
                        });
                    }
                }
            }

            return batches;
        }

        private async Task ReplicateBatchAsync(ProcessingBatch batch)
        {
            using (SqlConnection con = Database.getConnection())
            using (SqlCommand cmd = new SqlCommand("dbo.sp_ReplicateSales", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@brcode", SqlDbType.Char, 3).Value = batch.BranchCode;
                cmd.Parameters.Add("@petsa", SqlDbType.Date).Value = batch.SalesDate.Date;
                cmd.Parameters.Add("@machinename", SqlDbType.VarChar, 20).Value = batch.MachineUsed;

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    


        private async Task<DataTable> LoadData2Async(string branchCode, DateTime salesDate, string machineName)
        {
            DataTable dt = new DataTable();

            string sql = @"
SELECT 
    x.*,
    SUM(x.TotalAmount) OVER (
        ORDER BY x.TotalAmount DESC, x.ReferenceNo, x.SequenceNumber
        ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW
    ) AS AccumulatedAmount
FROM dbo.func_Allmanip(@brcode, @petsa, @machineused) x
ORDER BY x.TotalAmount DESC, x.ReferenceNo, x.SequenceNumber;";

            using (SqlConnection con = Database.getConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@brcode", SqlDbType.Char, 3).Value = branchCode;
                cmd.Parameters.Add("@petsa", SqlDbType.Date).Value = salesDate.Date;
                cmd.Parameters.Add("@machineused", SqlDbType.VarChar, 20).Value = machineName;

                await con.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }

            // Ensure required preview/testing columns exist
            //EnsureScenarioColumns(dt);

            return dt;
        }

        //private void EnsureScenarioColumns(DataTable dt)
        //{
        //    if (!dt.Columns.Contains("ScenarioSelected"))
        //        dt.Columns.Add("ScenarioSelected", typeof(bool));

        //    if (!dt.Columns.Contains("ScenarioTag"))
        //        dt.Columns.Add("ScenarioTag", typeof(string));

        //    if (!dt.Columns.Contains("NewQty"))
        //        dt.Columns.Add("NewQty", typeof(decimal));

        //    if (!dt.Columns.Contains("NewTotalAmount"))
        //        dt.Columns.Add("NewTotalAmount", typeof(decimal));

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        if (row["ScenarioSelected"] == DBNull.Value)
        //            row["ScenarioSelected"] = false;

        //        if (row["ScenarioTag"] == DBNull.Value)
        //            row["ScenarioTag"] = "";

        //        if (row["NewQty"] == DBNull.Value)
        //            row["NewQty"] = SafeGetDecimal(row, "QtySold");

        //        if (row["NewTotalAmount"] == DBNull.Value)
        //            row["NewTotalAmount"] = SafeGetDecimal(row, "TotalAmount");
        //    }
        //}

        /// <summary>
        /// Finds the row whose AccumulatedAmount is closest to target,
        /// then marks all rows BEFORE it as selected for scenario preview.
        /// IMPORTANT: This only changes the in-memory DataTable for testing/preview.
        /// </summary>
        private void ProcessDataTable(DataTable dt,decimal target)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;

            int bestIndex = -1;
            decimal bestDiff = decimal.MaxValue;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal acc = SafeGetDecimal(dt.Rows[i], "AccumulatedAmount");
                decimal diff = Math.Abs(acc - target);
                //decimal diff = Math.Abs(acc - _targetAmount);

                if (diff < bestDiff)
                {
                    bestDiff = diff;
                    bestIndex = i;
                }
            }

            if (bestIndex < 0)
                return;

            // Clear previous flags
            //foreach (DataRow row in dt.Rows)
            //{
            //    row["ScenarioSelected"] = false;
            //    row["ScenarioTag"] = "";
            //}

            // Mark all rows before the closest row
            for (int i = 0; i < bestIndex; i++)
            {
                dt.Rows[i]["NewTotalAmount"] = 0m;
                //DataRow row = dt.Rows[i];
                //row["ScenarioSelected"] = true;
                //row["ScenarioTag"] = "D";

                //// Keep original values for preview-only storage
                //// (safe testing approach: no original sales overwrite)
                //row["NewQty"] = SafeGetDecimal(row, "QtySold");
                //row["NewTotalAmount"] = SafeGetDecimal(row, "TotalAmount");
            }
        }

        private async Task ExecuteAsync(DataTable source, ProcessingBatch batch)
        {
            try
            {
                DataTable tvp = BuildManipItemsTvpFromDataTable(
                    source,
                    batch.BranchCode,
                    batch.SalesDate);

                if (tvp.Rows.Count == 0)
                    return;

                await ApplyManipulationAndRecalcAsync(
                    tvp,
                    batch.BranchCode,
                    batch.SalesDate,
                    batch.MachineUsed);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Execute Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Builds TVP rows from scenario-selected preview rows only.
        /// </summary>
        private DataTable BuildManipItemsTvpFromDataTable(
            DataTable source,
            string branchCode,
            DateTime petsaDate)
        {
            var dt = new DataTable();
            dt.Columns.Add("BranchCode", typeof(string));
            dt.Columns.Add("MachineUsed", typeof(string));
            dt.Columns.Add("Petsa", typeof(DateTime));
            dt.Columns.Add("ReferenceNo", typeof(string));
            dt.Columns.Add("CashierTransNo", typeof(string));
            dt.Columns.Add("SequenceNumber", typeof(int));
            dt.Columns.Add("NewQty", typeof(decimal));
            dt.Columns.Add("NewTotalAmount", typeof(decimal));

            if (source == null || source.Rows.Count == 0)
                return dt;

            foreach (DataRow row in source.Rows)
            {
                decimal newQty = row["NewQty"] == DBNull.Value ? 0m : Convert.ToDecimal(row["NewQty"]);
                decimal newTotal = row["NewTotalAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(row["NewTotalAmount"]);
                decimal oldQty = row["QtySold"] == DBNull.Value ? 0m : Convert.ToDecimal(row["QtySold"]);
                decimal oldTotal = row["TotalAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(row["TotalAmount"]);

                if (newQty == oldQty && newTotal == oldTotal)
                    continue;
                //bool selected = row["ScenarioSelected"] != DBNull.Value &&
                //                Convert.ToBoolean(row["ScenarioSelected"]);

                //if (!selected)
                //    continue;

                dt.Rows.Add(
                    branchCode,
                    Convert.ToString(row["MachineUsed"]),
                    petsaDate.Date,
                    Convert.ToString(row["ReferenceNo"]),
                    Convert.ToString(row["CashierTransNo"]),
                    row["SequenceNumber"] == DBNull.Value ? 0 : Convert.ToInt32(row["SequenceNumber"]),
                    SafeGetDecimal(row, "NewQty"),
                    SafeGetDecimal(row, "NewTotalAmount")
                );
            }

            return dt;
        }

        /// <summary>
        /// SAFE testing/scenario-preview save.
        /// Replace dbo.spman_save_scenario_preview with your preview/testing SP.
        /// </summary>
        private async Task ApplyManipulationAndRecalcAsync(
            DataTable tvp,
            string brcode,
            DateTime petsa,
            string machinename)
        {
            using (SqlConnection con = Database.getConnection())
            using (SqlCommand cmd = new SqlCommand("dbo.spman_apply_and_calculate", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;

                cmd.Parameters.Add("@brcode", SqlDbType.Char, 3).Value = brcode;
                cmd.Parameters.Add("@petsa", SqlDbType.Date).Value = petsa.Date;
                cmd.Parameters.Add("@machinename", SqlDbType.VarChar, 20).Value = machinename;

                var p = cmd.Parameters.Add("@Items", SqlDbType.Structured);
                p.TypeName = "dbo.ManipItemType";
                p.Value = tvp;

                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        //private async Task MarkBatchProcessedAsync(
        //    string branchCode,
        //    DateTime salesDate,
        //    string machineUsed)
        //{
        //    using (SqlConnection con = Database.getConnection())
        //    using (SqlCommand cmd = new SqlCommand("sp_MarkSalesBatchProcessed", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add("@BranchCode", SqlDbType.Char, 3).Value = branchCode;
        //        cmd.Parameters.Add("@DateExecute", SqlDbType.Date).Value = salesDate.Date;
        //        cmd.Parameters.Add("@MachineUsed", SqlDbType.VarChar, 20).Value = machineUsed;

        //        await con.OpenAsync();
        //        await cmd.ExecuteNonQueryAsync();
        //    }
        //}

        private decimal SafeGetDecimal(DataRow row, string columnName)
        {
            if (row == null || row.Table == null || !row.Table.Columns.Contains(columnName))
                return 0m;

            return row[columnName] == DBNull.Value
                ? 0m
                : Convert.ToDecimal(row[columnName]);
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {

            if (_isRunning)
            {
                XtraMessageBox.Show("Already running.");
                return;
            }

            _isRunning = true;

            btnStart.Enabled = false;
            btnStop.Enabled = true;

            lblStatus.Text = "Starting...";
            lblStatus.ForeColor = Color.Orange;

            timer1.Interval = 5000; // 5 seconds (adjust if needed)
            timer1.Start();

            // ✅ Start print worker
            StartPrintWorker();


        }

        private void btnStop_Click(object sender, EventArgs e)
        {


            _isRunning = false;

            timer1.Stop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;


            lblStatus.Text = "Stopped";
            lblStatus.ForeColor = Color.Red;

            // ✅ Stop print worker
            StopPrintWorker();



        }
        private async Task StartProcessingLoopAsync()
        {
            while (_isRunning)
            {
                try
                {
                    if (!_isProcessing)
                    {
                        _isProcessing = true;

                        //await ForDataProcessingAsync();
                        await RunProcessingCycleAsync();

                        _isProcessing = false;
                    }
                }
                catch (Exception ex)
                {
                    _isProcessing = false;
                    XtraMessageBox.Show(ex.Message);
                }

                // wait before next cycle
                await Task.Delay(5000); // adjust as needed
            }
        }

        private void PrintZRead(string bcode, string dateex, string terminal)
        {
            DateTime dt = Convert.ToDateTime(dateex);

            string filepath = $@"C:\ProgramFlies\EndOfDay\{bcode}\{terminal}\{dt:yyyyMMdd}\";

            // ✅ Get data row once
            var row = Database.getMultipleQuery(
                "POSZReadingTransactions2",
                $"BranchCode='{bcode}' AND CAST(DateExecute AS date)='{dateex}' AND MachineUsed='{terminal}'",
                "*"
            );

            if (row == null)
            {
                XtraMessageBox.Show("No data found for printing.");
                return;
            }

            // ✅ helper for safe reading
            string Val(string col) => row[col]?.ToString() ?? "0";

            double D(string col)
            {
                double.TryParse(Val(col), out double d);
                return d;
            }
            int I(string col)
            {
                int.TryParse(Val(col), out int d);
                return d;
            }

            // ✅ extract needed fields
            string branchcode = Val("BranchCode");
            string counterNo = Val("CounterNo");
            string transactionNo = Val("TransactionNo");
            string machineUsed = Val("MachineUsed");


            string BeginSI = Val("BeginningSINo");
            string EndingSI = Val("EndingSINo");
            string BeginRetNo = Val("BeginningReturnTransNo");
            string EndingRetNo = Val("EndingReturnTransNo");

            int noofsolditems = I("SoldItems");
            int noofcancelleditems = I("CancelledItems");
            int noofvoiditems = I("VoidItems");
            int noofreturneditems = I("ReturnedItems");
            int noofvatitems = I("VatItems");
            int noofdiscountitems = I("DiscountItems");

            int noofscdisc = I("SCDiscItems");
            int noofpwddisc = I("PWDDiscItems");
            int noofregdisc = I("RegDiscItems");
           

            double BeginningBalance = D("BeginningBalance");
            double EndingBalance = D("EndingBalance");

            double totalSales = D("TotalSales");
            double totalReturned = D("TotalReturnedSales");
            double totalDiscount = D("TotalDiscount");


            double TotalCashSales = D("TotalCashSales");
            double TotalCreditSales = D("TotalCreditSales");
            double TotalSales = D("TotalSales");

            double scDisc = D("TotalSCDiscount");
            double pwdDisc = D("TotalPWDDiscount");
            double regDisc = D("TotalRegDiscount");

            double vatExempt = D("VatExemptSale");
            double vatable = D("VatableSale");
            double vatAmount = D("VatInput");
            double vatAdjustment = D("VatAdjustment");

            double netSales = D("TotalNetSales");

            // ✅ compute derived values
            double grossAdjusted = totalSales - totalReturned;

            double totalDiscounts =
                totalDiscount + scDisc + pwdDisc + regDisc;

            // ✅ build receipt
            StringBuilder details = new StringBuilder();

            details.Append((char)27).Append((char)112).Append((char)0).Append((char)25);

            details.AppendLine(Classes.ReceiptSetup.doHeader(branchcode, terminal));

            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";
            details.AppendLine(HelperFunction.PrintLeftText(dt.ToString(format)));

            details.AppendLine(HelperFunction.createAsteriskLine());
            details.AppendLine(HelperFunction.PrintCenterText("Z - READING"));
            details.AppendLine(HelperFunction.createAsteriskLine());

            details.AppendLine(HelperFunction.PrintLeftText($"Z Counter #: {counterNo}"));
            details.AppendLine(HelperFunction.PrintLeftText($"Tran #: {transactionNo}"));
            details.AppendLine(HelperFunction.PrintLeftText($"Terminal #: {machineUsed}"));

            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]"));
            details.AppendLine(HelperFunction.createEqualLine());

            details.AppendLine(HelperFunction.PrintLeftRigthText("GROSS SALES :", HelperFunction.convertToNumericFormat(totalSales)));
            details.AppendLine(HelperFunction.PrintLeftRigthText("LESS: RETURN/REFUND:", HelperFunction.convertToNumericFormat(totalReturned)));
            details.AppendLine(HelperFunction.PrintLeftRigthText("GROSS SALES ADJ:", HelperFunction.convertToNumericFormat(grossAdjusted)));
            details.AppendLine();

            details.AppendLine(HelperFunction.PrintLeftText("LESS DISCOUNTS"));
            details.AppendLine(HelperFunction.createEqualLine());

            details.AppendLine(HelperFunction.PrintLeftRigthText("No. of SC Discount: ", noofscdisc.ToString()));
            details.AppendLine(HelperFunction.PrintLeftRigthText("Total Amount of SC Discount: ", HelperFunction.convertToNumericFormat(scDisc)));

            details.AppendLine(HelperFunction.PrintLeftRigthText("No. of PWD Discount: ", noofpwddisc.ToString()));
            details.AppendLine(HelperFunction.PrintLeftRigthText("Total Amount of PWD Discount: ", HelperFunction.convertToNumericFormat(pwdDisc)));

            details.AppendLine(HelperFunction.PrintLeftRigthText("No. of Regular Discount: ", noofregdisc.ToString()));
            details.AppendLine(HelperFunction.PrintLeftRigthText("Total Amount of Regular Discount: ", HelperFunction.convertToNumericFormat(regDisc)));

            details.AppendLine(HelperFunction.PrintLeftRigthText("No. of Disc P/Item: ", noofdiscountitems.ToString()));
            details.AppendLine(HelperFunction.PrintLeftRigthText("Total Disc P/Item: ", HelperFunction.convertToNumericFormat(totalDiscount)));

            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.PrintLeftRigthText("TOTAL DISCOUNTS:", HelperFunction.convertToNumericFormat(totalDiscounts)));

            details.AppendLine();

            details.AppendLine(HelperFunction.PrintLeftRigthText("LESS VAT ADJUSTMENT:", HelperFunction.convertToNumericFormat(vatAdjustment * -1)));

            details.AppendLine(HelperFunction.createEqualLine());

            details.AppendLine(HelperFunction.PrintLeftRigthText("TOTAL NET SALES:", HelperFunction.convertToNumericFormat(netSales)));

            details.AppendLine(HelperFunction.createAsteriskLine());
            details.AppendLine(HelperFunction.createEqualLine());

            details.AppendLine(HelperFunction.PrintLeftText("MODE OF PAYMENT"));
            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.PrintLeftRigthText("CASH:", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCashSales)))); //total sales
            details.AppendLine(HelperFunction.PrintLeftRigthText("CREDIT:", HelperFunction.convertToNumericFormat(Convert.ToDouble(TotalCreditSales))));
            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.PrintLeftText("DETAILS"));
            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.PrintLeftRigthText("Beginning Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(BeginningBalance)))); //numitemsold
            details.AppendLine(HelperFunction.PrintLeftRigthText("Ending Balance: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance)))); //numitemsold

            details.AppendLine(HelperFunction.PrintLeftRigthText("Beginning SI No.: ", BeginSI)); //beginvoice
            details.AppendLine(HelperFunction.PrintLeftRigthText("Ending SI No.: ", EndingSI)); //lastornum

            details.AppendLine(HelperFunction.PrintLeftRigthText("Beginning Return Transaction No.: ", BeginRetNo));//beginvoice
            details.AppendLine(HelperFunction.PrintLeftRigthText("Ending Return Transaction No.: ", EndingRetNo)); //lastornum

            details.AppendLine(HelperFunction.PrintLeftRigthText("No. of Item Sold: ", noofsolditems.ToString()));                                                                      //details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", HelperFunction.convertToNumericFormat(txtlasttranno.Text)) + Environment.NewLine;

            details.AppendLine(HelperFunction.PrintLeftRigthText("No. of Refunds/Returned: ", noofreturneditems.ToString()));
            details.AppendLine(HelperFunction.PrintLeftRigthText("Total Refunds/Returned: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(totalReturned))));



            details.AppendLine(HelperFunction.PrintLeftText("PAYABLE TO BIR"));
            details.AppendLine(HelperFunction.createEqualLine());

            details.AppendLine(HelperFunction.PrintLeftRigthText("VATable Sales: ", HelperFunction.convertToNumericFormat(vatable)));
            details.AppendLine(HelperFunction.PrintLeftRigthText("VAT Amount:", HelperFunction.convertToNumericFormat(vatAmount)));
            details.AppendLine(HelperFunction.PrintLeftRigthText("VAT Exempt Sale:", HelperFunction.convertToNumericFormat(vatExempt)));
            details.AppendLine(HelperFunction.PrintLeftRigthText("Zero Rated Sales: ", "0.00"));
            details.AppendLine();
            details.AppendLine(HelperFunction.PrintLeftRigthText("ACCUMULATED GRAND TOTAL ", HelperFunction.convertToNumericFormat(Convert.ToDouble(EndingBalance))));//total sales txtTotalSales.Text)
            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.createAsteriskLine());
            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.createEqualLine());
            details.AppendLine(HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname));
            details.AppendLine(HelperFunction.createDottedLine());
            details.AppendLine(HelperFunction.PrintCenterText("SUPERVISOR/Manager"));
            details.AppendLine(HelperFunction.PrintCenterText("Signature Over Printed Name"));

            details.AppendLine(HelperFunction.createAsteriskLine());
            details.AppendLine(HelperFunction.PrintCenterText("Have a nice day!"));
            details.AppendLine(HelperFunction.LastPagePaper());

            // ✅ create directory if needed
            if (!Directory.Exists(filepath))
                Directory.CreateDirectory(filepath);

            string file = Path.Combine(filepath, $"{dt:yyyyMMdd}.txt");

            File.WriteAllText(file, details.ToString());

            // ✅ print
            Printing print = new Printing();
            print.printTextFile(file);
        }

        private void StartPrintWorker()
        {
            Task.Run(async () =>
            {
                while (!_printCts.Token.IsCancellationRequested)
                {
                    try
                    {
                        if (_printQueue.TryDequeue(out PrintJob job))
                        {
                            // ✅ run printing in background
                            PrintZRead(job.BranchCode, job.DateExecute, job.MachineUsed);
                        }
                        else
                        {
                            await Task.Delay(500); // small delay if queue empty
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optional: log error
                        Console.WriteLine("Print error: " + ex.Message);
                    }
                }
            }, _printCts.Token);
        }
        private void StopPrintWorker()
        {
            _printCts.Cancel();
            _printCts = new CancellationTokenSource(); // reset for next run
        }

        private void POSDataManagementAutomation_Load(object sender, EventArgs e)
        {

        }
    }

}


//SP returns:
//   BranchCode | DateExecute | MachineUsed | TotalNetSales

//↓
//GetPendingBatchesAsync()
//   → fills ProcessingBatch(with TotalNetSales)

//↓
//RunProcessingCycleAsync()

//↓
//target = TotalNetSales* 0.30

//↓
//ProcessDataTable(dt, target)

//↓
//closest row found

//↓
//rows before it marked(ScenarioSelected = true)

//↓
//Build TVP

//↓
//ExecuteAsync()

//↓
//Save preview

//↓
//Mark batch processed


//Start Click
//   ↓
//_isRunning = true
//   ↓
//timer1.Start()
//   ↓
//timer ticks every 5s
//   ↓
//RunProcessingCycleAsync()
//   ↓
//Batch processing

//Stop Click
//   ↓
//_isRunning = false
//   ↓
//timer1.Stop()
//   ↓
//Processing stops cleanly