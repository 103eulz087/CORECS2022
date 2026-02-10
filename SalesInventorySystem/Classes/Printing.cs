using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem
{
    class Printing
    {
        public string txtorder, txtorder2;
        private Dictionary<string, Tuple<string, int>> printerMappings = new Dictionary<string, Tuple<string, int>>();

        //string tradename = Database.getSingleQuery("CompanyProfile", "BranchCode='"+Login.assignedBranch+"'", "CompanyName");
        //string compaddress1 = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "'", "Address1");
        //string compaddress2 = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "Address2");
        //string comptinno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "TinNo");
        //string compminno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "MinNo");
        //string compbirpermitno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "'", "BirPermitNo");
        //string compserialno = Database.getSingleQuery("CompanyProfile", "BranchCode='" + Login.assignedBranch + "' ", "SerialNo");
        public static void SendRawData(string printerIpAddress, int printerPort, string dataToSend)
        {
            TcpClient client = null;
            try
            {
                client = new TcpClient(printerIpAddress, printerPort);

                // Convert the string data to bytes using a suitable encoding (e.g., ASCII, UTF-8)
                byte[] dataBytes = Encoding.ASCII.GetBytes(dataToSend);

                // Get the network stream
                NetworkStream stream = client.GetStream();

                // Send the data
                stream.Write(dataBytes, 0, dataBytes.Length);
                //Console.WriteLine($"Data sent successfully to {printerIpAddress}:{printerPort}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Close the connection
                client?.Close();
            }
        }

        public static async void SendRawDataAsync(string printerIpAddress, int printerPort, string dataToSend)
        {
            await Task.Run(() =>
            {
                TcpClient client = null;
                try
                {
                    client = new TcpClient(printerIpAddress, printerPort);

                    byte[] dataBytes = Encoding.ASCII.GetBytes(dataToSend);

                    NetworkStream stream = client.GetStream();

                    stream.Write(dataBytes, 0, dataBytes.Length);
                   
                    //Console.WriteLine($"Data sent successfully to {printerIpAddress}:{printerPort}");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Socket error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                finally
                {
                    client?.Close();
                }
            });
        }

        public static async Task PlayNotificationSoundAsync(string soundFilePath)
        {
            // Use Task.Run to offload the sound loading and playback logic to a background thread.
            // This prevents the main UI thread from becoming unresponsive, even if the sound file
            // is large or there are delays in accessing it.
            await Task.Run(() =>
            {
                try
                {
                    // 1. Check if the specified sound file actually exists.
                    if (!File.Exists(soundFilePath))
                    {
                        Console.WriteLine($"Error: Sound file not found at '{soundFilePath}'. Please verify the path.");
                        // Optionally, you could play a default system sound here if the file is missing.
                        // SystemSounds.Exclamation.Play();
                        return; // Exit the method if the file is not found.
                    }

                    // 2. Create a new SoundPlayer instance with the provided file path.
                    // The 'using' statement ensures that the SoundPlayer object is properly
                    // disposed of after it's no longer needed, releasing system resources.
                    using (SoundPlayer player = new SoundPlayer(soundFilePath))
                    {
                        // 3. Load the sound into memory. This operation can be synchronous
                        // but since it's inside Task.Run, it won't block the main thread.
                        player.Load();

                        // 4. Play the sound. The Play() method plays the sound asynchronously
                        // on an internal thread managed by SoundPlayer, and returns immediately.
                        player.Play();

                        //Console.WriteLine($"Notification: Playing sound from '{soundFilePath}'");
                    }
                }
                // 5. Implement robust error handling for common issues.
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"Error: The sound file was not found at '{soundFilePath}'. Double-check the file path and ensure it's accessible.");
                }
                catch (InvalidOperationException ex)
                {
                    // This typically occurs if the sound file is not a valid .wav format
                    // or if there's an issue with the audio device.
                    Console.WriteLine($"Error playing sound from '{soundFilePath}': {ex.Message}. Ensure the file is a valid .wav format and your audio device is working.");
                }
                catch (Exception ex)
                {
                    // Catch any other unexpected errors during sound playback.
                    Console.WriteLine($"An unexpected error occurred while attempting to play sound from '{soundFilePath}': {ex.Message}");
                }
            });
        }
        public void SendRawDataFromFile(string printerIpAddress, int printerPort, string filePath)
        {
            TcpClient client = null;
            try
            {
                // Read all text from the file
                string dataToSend = File.ReadAllText(filePath);

                client = new TcpClient(printerIpAddress, printerPort);

                // Convert the string data to bytes using a suitable encoding
                byte[] dataBytes = Encoding.ASCII.GetBytes(dataToSend); // Adjust encoding if needed

                // Get the network stream
                NetworkStream stream = client.GetStream();

                // Send the data
                stream.Write(dataBytes, 0, dataBytes.Length);
                Console.WriteLine($"Data from '{filePath}' sent successfully to {printerIpAddress}:{printerPort}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: File not found at '{filePath}'");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Close the connection
                client?.Close();
            }
        }

        public void printReceiptAtik()
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Temp\\";

            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += "-";
         
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\temp.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
           
        }

        public void printOrders(string refno,string waiterid,string tableno,string location, DataGridView gridview)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\Restaurant\\Orders\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            details += HelperFunction.PrintCenterText(location) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Table #:" + tableno,"Waiter:"+waiterid) + Environment.NewLine;
            details += Classes.ReceiptSetup.doTitle("ORDERS");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Qty"].Value.ToString()) + Environment.NewLine;
               
            }
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + refno + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile2(filetoprint);
        }


        //private string GeneratePrintData(string itemName)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine(itemName);
        //    sb.AppendLine("-----------------------");
        //    // Add more formatting and ESC/POS commands as needed
        //    return sb.ToString();
        //}

        //public void printOrdersTest(string refno, string waiterid, string tableno, string location, DataGridView gridview)
        //{
        //    StringBuilder kitchenReceipt = new StringBuilder();
        //    StringBuilder grillReceipt = new StringBuilder();
        //    StringBuilder AllOrdersReceipt = new StringBuilder();


        //    // Add order-level information to the receipts
        //    AllOrdersReceipt.AppendLine($"Order ID: {refno}");
        //    AllOrdersReceipt.AppendLine("----- ALL ORDERS -----");
        //    AllOrdersReceipt.AppendLine("" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "");
        //    AllOrdersReceipt.AppendLine(HelperFunction.PrintCenterText(location) + Environment.NewLine);
        //    AllOrdersReceipt.AppendLine(HelperFunction.PrintLeftRigthText("Table #:" + tableno, "Waiter:" + waiterid) + Environment.NewLine);

        //    kitchenReceipt.AppendLine($"Order ID: {refno}");
        //    kitchenReceipt.AppendLine("----- Kitchen -----");
        //    kitchenReceipt.AppendLine("" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "");
        //    kitchenReceipt.AppendLine(HelperFunction.PrintCenterText(location) + Environment.NewLine);
        //    kitchenReceipt.AppendLine(HelperFunction.PrintLeftRigthText("Table #:" + tableno, "Waiter:" + waiterid) + Environment.NewLine);

        //    grillReceipt.AppendLine($"Order ID: {refno}");
        //    grillReceipt.AppendLine("------- Grill -------");
        //    grillReceipt.AppendLine("" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "");
        //    grillReceipt.AppendLine(HelperFunction.PrintCenterText(location) + Environment.NewLine);
        //    grillReceipt.AppendLine(HelperFunction.PrintLeftRigthText("Table #:" + tableno, "Waiter:" + waiterid) + Environment.NewLine);


        //    //String details = "";
        //    string filepath = "";
        //    string filetoprintmain = "";
        //    //string filetoprintgrill = "";
        //    string filetoprintkitchen = "";
        //    //details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
        //    //details += HelperFunction.PrintCenterText(location) + Environment.NewLine;
        //    //details += HelperFunction.PrintLeftRigthText("Table #:" + tableno,"Waiter:"+waiterid) + Environment.NewLine;
        //    //details += Classes.ReceiptSetup.doTitle("ORDERS");
        //    //bool flag = false;
        //    for (int i = 0; i <= gridview.RowCount - 1; i++)
        //    {


        //        //get productcategorycode
        //        string prodcatcode = Database.getSingleQuery("SELECT ProductCategoryCode FROM dbo.Products WHERE BranchCode='" + Login.assignedBranch + "' and Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "'", "ProductCategoryCode");
        //        string prodcatname = Classes.Product.getProductCategoryName(prodcatcode);

        //        var rowz = Database.getMultipleQuery($"SELECT Description,PrinterID FROM dbo.ProductCategory WHERE ProductCategoryID='{prodcatcode}'", "Description,PrinterID");
        //        string Description = rowz["Description"].ToString();
        //        string PrinterID = rowz["PrinterID"].ToString();

        //        string itemcombi = HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Qty"].Value.ToString()) + Environment.NewLine;

        //        if (prodcatcode == "10")
        //        {

        //            filepath = "C:\\POSTransaction\\Restaurant\\Orders\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + prodcatname + "\\";

        //            kitchenReceipt.AppendLine(itemcombi);
        //            AllOrdersReceipt.AppendLine(itemcombi);

        //            if (!Directory.Exists(filepath))
        //            {
        //                Directory.CreateDirectory(filepath);
        //            }
        //            txtorder = "\\" + refno + ".txt";
        //            filetoprintmain = filepath + txtorder;
        //            //StreamWriter writer = new StreamWriter(filetoprintmain);
        //            //writer.Write(beverageReceipt.ToString());
        //            //writer.Close();

        //        }
        //        else if (prodcatcode == "11")
        //        {
        //            filepath = "C:\\POSTransaction\\Restaurant\\Orders\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + prodcatname + "\\";

        //            grillReceipt.AppendLine(itemcombi);
        //            AllOrdersReceipt.AppendLine(itemcombi);

        //            if (!Directory.Exists(filepath))
        //            {
        //                Directory.CreateDirectory(filepath);
        //            }
        //            txtorder = "\\" + refno + ".txt";
        //            filetoprintkitchen = filepath + txtorder;
        //        }
        //        else
        //        {
        //            AllOrdersReceipt.AppendLine(itemcombi);
        //        }
        //        //else if (prodcatcode == "12")
        //        //{
        //        //    filepath = "C:\\POSTransaction\\Restaurant\\Orders\\" + prodcatname + "\\";
        //        //    details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Qty"].Value.ToString()) + Environment.NewLine;
        //        //    if (!Directory.Exists(filepath))
        //        //    {
        //        //        Directory.CreateDirectory(filepath);
        //        //    }
        //        //    txtorder = "\\" + refno + ".txt";
        //        //    string filetoprint = filepath + txtorder;
        //        //    StreamWriter writer = new StreamWriter(filetoprint);
        //        //    writer.Write(details);
        //        //    writer.Close();
        //        //}
        //    }
        //    //gi append pa ang last page before e ewrite sa notepad sa ubos
        //    AllOrdersReceipt.AppendLine(HelperFunction.LastPagePaper());
        //    kitchenReceipt.AppendLine(HelperFunction.LastPagePaper());
        //    grillReceipt.AppendLine(HelperFunction.LastPagePaper());

        //    //gi write na sa notepad
        //    StreamWriter writer = new StreamWriter(filetoprintmain);
        //    writer.Write(kitchenReceipt.ToString());
        //    writer.Close();

        //    StreamWriter writer1 = new StreamWriter(filetoprintkitchen);
        //    writer1.Write(kitchenReceipt.ToString());
        //    writer1.Close();

        //    //StreamWriter writer2 = new StreamWriter(filetoprintgrill);
        //    //writer2.Write(grillReceipt.ToString());
        //    //writer2.Close();

        //    printTextFile(filetoprintmain); //print to main printer

        //    string printerIp = "192.168.0.103"; // Replace with your printer's IP address
        //    int printerPort = 9100;              // Typically 9100 for raw printing
        //    //string receiptData = "This is a test receipt.\n\x1D\x56\x41\x03"; // Example with ESC/POS cut command
        //    SendRawDataFromFile(printerIp, printerPort, filetoprintkitchen);
        //}

        private void LoadPrinterMappings()
        {
            SqlConnection connection = Database.getConnection();
            
                try
                {
                    connection.Open();
                    string query = "SELECT Description, PrinterID, PrinterPort FROM dbo.ProductCategory"; // Adjust query and table/column names

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader.GetString(0);
                                string printerIP = reader.GetString(1);
                                int printerPort = reader.GetInt32(2);
                                printerMappings[categoryName.ToLower()] = Tuple.Create(printerIP, printerPort);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading printer mappings: {ex.Message}");
                    // Handle the error appropriately (e.g., use default mappings or log)
                }
            

            //// Example fallback mappings if database loading fails or for categories not in the DB
            //if (!printerMappings.ContainsKey("beverages"))
            //{
            //    printerMappings["beverages"] = Tuple.Create("192.168.0.101", 9100);
            //}
            //if (!printerMappings.ContainsKey("grill"))
            //{
            //    printerMappings["grill"] = Tuple.Create("192.168.0.102", 9100);
            //}
            // Add more fallback mappings as needed
        }

        public async void PrintOrderToFileTestAsync(string refno, string waiterid, string tableno, string location, DataGridView gridview)
        {
            LoadPrinterMappings();
            string baseFolderPath = @"C:\RestaurantOrders\";
            Dictionary<string, StringBuilder> categoryOrderData = new Dictionary<string, StringBuilder>();
            StringBuilder consolidatedOrder = new StringBuilder();
            consolidatedOrder.AppendLine($"Order ID: {refno}");
            consolidatedOrder.AppendLine("" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "");
            consolidatedOrder.AppendLine(HelperFunction.PrintCenterText(location) + Environment.NewLine);
            consolidatedOrder.AppendLine(HelperFunction.PrintLeftRigthText("Table #:" + tableno, "Waiter:" + waiterid) + Environment.NewLine);

            string query = $"SELECT Description,QtySold,CategoryCode FROM dbo.BatchSalesDetails WHERE ReferenceNo='{refno}' AND Barcode='{location}' AND BranchCode='{Login.assignedBranch}' AND MachineUsed='{Environment.MachineName.ToString()}' AND isCancelled=0 and isVoid=0 AND isErrorCorrect=0";
            string CategoryName = "";
            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand(query, con);
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                string Description = reader.GetString(0);
                double QtySold = Convert.ToDouble(reader.GetDecimal(1));
                string CategoryCode = reader.GetString(2);
                CategoryName = Classes.Product.getProductCategoryName(CategoryCode).ToLower();
                if (printerMappings.ContainsKey(CategoryName.ToLower()))
                {
                    if (!categoryOrderData.ContainsKey(CategoryName))
                    {
                        categoryOrderData[CategoryName] = new StringBuilder();
                        categoryOrderData[CategoryName].AppendLine($"Order ID: {refno}");
                        categoryOrderData[CategoryName].AppendLine($"Transaction No.: {location}");
                        categoryOrderData[CategoryName].AppendLine("" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "");
                        categoryOrderData[CategoryName].AppendLine(HelperFunction.PrintLeftRigthText("Table #:" + tableno, "Waiter:" + waiterid) + Environment.NewLine);
                        categoryOrderData[CategoryName].AppendLine(HelperFunction.PrintCenterText(CategoryName.ToUpper()) + Environment.NewLine);
                    }
                    categoryOrderData[CategoryName].AppendLine(HelperFunction.PrintLeftRigthText(Description, QtySold.ToString()) + Environment.NewLine);
                    consolidatedOrder.AppendLine(HelperFunction.PrintLeftRigthText(Description, QtySold.ToString()) + Environment.NewLine);
                }
            }
            reader.Close(); 
            consolidatedOrder.AppendLine(HelperFunction.LastPagePaper());

            foreach (var category in categoryOrderData.Keys)
            {
                categoryOrderData[category].AppendLine(HelperFunction.LastPagePaper());
                ////////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                string folderPath = Path.Combine(baseFolderPath,category,refno,location);
                // Create the folder if it doesn't exist
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, $"Order_{refno}.txt");
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(categoryOrderData[category].ToString());
                    }
                    //using (StreamWriter writer = new StreamWriter(filePath, true))
                    //{
                    //    writer.WriteLine(HelperFunction.LastPagePaper());
                    //}
                }
                catch (Exception ex)
                {
                    ex.StackTrace.ToString();
                    //Console.WriteLine($"Error writing order for category '{category.ToUpper()}': {ex.Message}");
                }
                ////////////////////////////////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////////////////////////////////
                if (categoryOrderData[category].Length > ($"Order ID: {refno}\n----- {category.ToUpper()} -----\n").Length && printerMappings.ContainsKey(category))
                {
                    var printerInfo = printerMappings[category]; 
                    //SendRawData(printerInfo.Item1, printerInfo.Item2, categoryOrderData[category].ToString());
                    SendRawDataAsync(printerInfo.Item1, printerInfo.Item2, categoryOrderData[category].ToString());
                   // await PlayNotificationSoundAsync("C:\\BackupSalesInventory\\notify1.wav");
                    await PlayNotificationSoundAsync(Application.StartupPath+"\\notify1.wav");
                   
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////
            ////CONSOLIDATEDORDERS FILE//////////////////////////////////////////////////////////
            
            string consolidatedFolderPath = Path.Combine(baseFolderPath, "ConsolidatedOrders",refno);
            if (!Directory.Exists(consolidatedFolderPath))
            {
                Directory.CreateDirectory(consolidatedFolderPath);
            }
            string consolidatedFilePath = Path.Combine(consolidatedFolderPath, $"Order_{refno}_{location}.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(consolidatedFilePath))
                {
                    writer.Write(consolidatedOrder.ToString());
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }
            printTextFile(consolidatedFilePath); //print to main printer
           
            //////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////
        }

        public void PrintOrderToFile(string refno, string waiterid, string tableno, string location, DataGridView gridview)
        {
            LoadPrinterMappings();
            string baseFolderPath = @"C:\RestaurantOrders\";
            Dictionary<string, StringBuilder> categoryOrderData = new Dictionary<string, StringBuilder>();
            StringBuilder consolidatedOrder = new StringBuilder();
            consolidatedOrder.AppendLine($"Order ID: {refno}");
            consolidatedOrder.AppendLine("" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "");
            consolidatedOrder.AppendLine(HelperFunction.PrintCenterText(location) + Environment.NewLine);
            consolidatedOrder.AppendLine(HelperFunction.PrintLeftRigthText("Table #:" + tableno, "Waiter:" + waiterid) + Environment.NewLine);

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //get productcategorycode
                string prodcatcode = Database.getSingleQuery("SELECT ProductCategoryCode FROM dbo.Products WHERE BranchCode='" + Login.assignedBranch + "' and Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "'", "ProductCategoryCode");
                string prodcatname = Classes.Product.getProductCategoryName(prodcatcode);

                var rowz = Database.getMultipleQuery($"SELECT Description,PrinterID FROM dbo.ProductCategory WHERE ProductCategoryID='{prodcatcode}'", "Description,PrinterID");
                string Description = rowz["Description"].ToString();
                string PrinterID = rowz["PrinterID"].ToString();

                string itemName = gridview.Rows[i].Cells["Particulars"].Value.ToString();
                string qty = gridview.Rows[i].Cells["Qty"].Value.ToString();
                string itemType = prodcatname.ToLower();
                string itemOrderLine = GenerateOrderLine(itemName); // Format each item line
                if (printerMappings.ContainsKey(itemType))
                {
                    if (!categoryOrderData.ContainsKey(itemType))
                    {
                        categoryOrderData[itemType] = new StringBuilder();
                        categoryOrderData[itemType].AppendLine($"Order ID: {refno}");
                        categoryOrderData[itemType].AppendLine("" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "");
                        categoryOrderData[itemType].AppendLine(HelperFunction.PrintLeftRigthText("Table #:" + tableno, "Waiter:" + waiterid) + Environment.NewLine);
                        categoryOrderData[itemType].AppendLine(HelperFunction.PrintCenterText(itemType.ToUpper()) + Environment.NewLine);
                        //categoryOrderData[itemType].AppendLine($"----- {itemType.ToUpper()} -----");
                    }
                    //categoryOrderData[itemType].AppendLine(itemOrderLine); // Add to consolidated order data
                    //consolidatedOrder.AppendLine($"{itemType.ToUpper()}: {itemOrderLine}");

                    //new
                    categoryOrderData[itemType].AppendLine(HelperFunction.PrintLeftRigthText(itemOrderLine, qty) + Environment.NewLine);
                   consolidatedOrder.AppendLine(HelperFunction.PrintLeftRigthText(itemOrderLine, qty) + Environment.NewLine);
                }
            }
            consolidatedOrder.AppendLine(HelperFunction.LastPagePaper());

            
            // Write the accumulated data to separate files
            foreach (var category in categoryOrderData.Keys)
            {
                
                string folderPath = Path.Combine(baseFolderPath, category);
                // Create the folder if it doesn't exist
                Directory.CreateDirectory(folderPath);
                string filePath = Path.Combine(folderPath, $"Order_{refno}.txt");            
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.Write(categoryOrderData[category].ToString());
                    }
                    using (StreamWriter writer = new StreamWriter(filePath,true))
                    {
                        writer.WriteLine(HelperFunction.LastPagePaper());
                    }
                    //MessageBox.Show(categoryOrderData[category].ToString());
                    //Console.WriteLine($"Order ID {refno} for category '{category.ToUpper()}' written to: {filePath}");


                }
                catch (Exception ex)
                {
                    ex.StackTrace.ToString();
                    //Console.WriteLine($"Error writing order for category '{category.ToUpper()}': {ex.Message}");
                }
                if (categoryOrderData[category].Length > ($"Order ID: {refno}\n----- {category.ToUpper()} -----\n").Length && printerMappings.ContainsKey(category))
                {
                    var printerInfo = printerMappings[category];
                    //SendRawDataFromFile(printerInfo.Item1, printerInfo.Item2, categoryOrderData[category].ToString());
                    //SendRawData(printerInfo.Item1, printerInfo.Item2, categoryOrderData[category].ToString());
                    SendRawDataFromFile(printerInfo.Item1, printerInfo.Item2, filePath);
                }
            }

            // Write the consolidated order file
            string consolidatedFolderPath = Path.Combine(baseFolderPath, "ConsolidatedOrders");
            Directory.CreateDirectory(consolidatedFolderPath);
            string consolidatedFilePath = Path.Combine(consolidatedFolderPath, $"Order_{refno}_Consolidated.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(consolidatedFilePath))
                {
                    writer.Write(consolidatedOrder.ToString());
                    //printTextFile(@"C:\RestaurantOrders\beverages\Order_000000000000000003.txt"); //print to main printer
                    //SendRawDataFromFile(printerInfo.Item1, printerInfo.Item2, consolidatedFilePath);
                    //Console.WriteLine($"Consolidated Order ID {refno} written to: {consolidatedFilePath}");
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                //Console.WriteLine($"Error writing consolidated order for ID {refno}: {ex.Message}");
            }
            printTextFile(consolidatedFilePath); //print to main printer
        }

        private string GenerateOrderLine(string itemName)
        {
            return itemName; // Replace with your desired format for each item line
        }

        public void PrintOrderDynamic(int orderId)
        {
            Dictionary<string, StringBuilder> categoryReceipts = new Dictionary<string, StringBuilder>();

            SqlConnection connection = Database.getConnection();
            
                connection.Open();
                string query = "SELECT ItemName, ItemType FROM OrderItems WHERE OrderId = @OrderId"; // Adjust your query

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string itemName = reader.GetString(0);
                            string itemType = reader.GetString(1).ToLower(); // Use lowercase for consistent matching
                            string itemPrintData = GeneratePrintData(itemName);

                            if (printerMappings.ContainsKey(itemType))
                            {
                                if (!categoryReceipts.ContainsKey(itemType))
                                {
                                    categoryReceipts[itemType] = new StringBuilder();
                                    categoryReceipts[itemType].AppendLine($"Order ID: {orderId}");
                                    categoryReceipts[itemType].AppendLine($"----- {itemType.ToUpper()} -----");
                                }
                                categoryReceipts[itemType].AppendLine(itemPrintData);
                            }
                            else
                            {
                                Console.WriteLine($"No printer mapping found for category '{itemType}' for item '{itemName}'. Not printed.");
                                // Handle unmapped categories (e.g., log, print to a default printer)
                            }
                        }
                    }
                }

                // Send the accumulated data for each category to its respective printer
                foreach (var category in categoryReceipts.Keys)
                {
                    if (categoryReceipts[category].Length > ($"Order ID: {orderId}\n----- {category.ToUpper()} -----\n").Length && printerMappings.ContainsKey(category))
                    {
                        var printerInfo = printerMappings[category];
                         SendRawDataFromFile(printerInfo.Item1, printerInfo.Item2, categoryReceipts[category].ToString());
                        Console.WriteLine($"{category.ToUpper()} receipt sent for Order ID: {orderId} to {printerInfo.Item1}:{printerInfo.Item2}");
                    }
                }
            
        }

        private string GeneratePrintData(string itemName)
        {
            return itemName; // Replace with your actual formatting
        }




        public void printReceiptBillingWithDiscount(string transcode, string ordercode, string total, string vatablesale, string vatexemptsale, string vat, string cash, string change, DataGridView gridview, string tableno, bool isDiscount, string disctype,string netdue)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Restaurant\\Billing\\";
            string filepath1 = "C:\\POSTransaction\\Restaurant\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            //string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += HelperFunction.PrintLeftText("Table #:" + tableno) + Environment.NewLine;
            details += Classes.ReceiptSetup.doTitle("TRANSACTION BILL");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                //if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                //{
                //    addV = "V";
                //}
                //else
                //{
                //    addV = "";
                //}
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Amount"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value;// + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                //details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE", total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            if (isDiscount == true)
            {
                var rowz = Database.getMultipleQuery($"SELECT DiscIDNo,DiscName,DiscountAmount FROM dbo.SalesDiscount WHERE OrderNo='{ordercode}' and isErrorCorrect=0", "DiscIDNo,DiscName,DiscountAmount");
                string DiscIDNo, DiscName, DiscountAmount;
                DiscIDNo = rowz["DiscIDNo"].ToString();
                DiscName = rowz["DiscName"].ToString();
                DiscountAmount = rowz["DiscountAmount"].ToString();
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + DiscIDNo) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + DiscName) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(DiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(netdue))) + Environment.NewLine + Environment.NewLine;

                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + DiscIDNo) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + DiscName) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(DiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(netdue))) + Environment.NewLine + Environment.NewLine;
                }
            }

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine;

            //details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);

            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = "\\LastTran.txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
            //printTextFile(filetoprint);
        }

        //IS USED
        public void printReceiptBilling(string transcode, string ordercode, string total, string vatablesale, string vatexemptsale, string vat, string cash, string change, DataGridView gridview,string tableno)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Restaurant\\Billing\\";
            string filepath1 = "C:\\POSTransaction\\Restaurant\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            //string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += HelperFunction.PrintLeftText("Table #:"+tableno) + Environment.NewLine;
            details += Classes.ReceiptSetup.doTitle("TRANSACTION BILL");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //string addV = "";
                //if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                //{
                //    addV = "V";
                //}
                //else
                //{
                //    addV = "";
                //}
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;

               

                //details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Particulars"].Value.ToString(), gridview.Rows[i].Cells["Amount"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value;// + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["Amount"].Value;// + addV;
                double totamnt = 0.0;
                totamnt = Convert.ToDouble(b);
                details += HelperFunction.PrintLeftRigthText(a, HelperFunction.convertToNumericFormat(totamnt)) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE", total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            //details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine;

            //details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);

            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = "\\LastTran.txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
            //printTextFile(filetoprint);
        }

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION NO ONE TIME DISCOUNT 
        public void printReceipt(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype
                                , string emailadd
                                , bool iszerorated = false,bool issendemail=false)


        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\"+DateTime.Now.ToString("yyyyMMdd")+"\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, GlobalVariables.computerName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
           
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string mark = gridview.Rows[i].Cells["isVat"].Value.ToString();
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if(Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance=Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "("+gridview.Rows[i].Cells["Discount"].Value.ToString()+")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if(Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
            
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                var rox = Database.getMultipleQuery("POSCreditCardTransactions", $"ReferenceNo='{ordercode}' ", "CCName,CCNumber,CCType,CCBank,CCPaymentReferenceNo");
                cardno = rox["CCNumber"].ToString();
                cardtype = rox["CCType"].ToString();
                cardrefno = rox["CCPaymentReferenceNo"].ToString();
                string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            if (paytype == "Merchant")
            {
                string ReferenceNo = "", MerchantName = "", VoucherCode = "";
                var rox = Database.getMultipleQuery("POSMerchantTransactions", $"OrderNo='{ordercode}' ", "ReferenceNo,MerchantName,VoucherCode,Amount");
                ReferenceNo = rox["ReferenceNo"].ToString();
                MerchantName = rox["MerchantName"].ToString();
                VoucherCode = rox["VoucherCode"].ToString();
                //string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Merchant "+ MerchantName) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("VoucherCode: " + VoucherCode) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + ReferenceNo) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath+"\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            details += HelperFunction.LastPagePaper();
            
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filetoprint);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();

            printTextFile(filetoprint);
            if (issendemail == true)
            {
                Classes.EmailSetup mailsetup = new Classes.EmailSetup();
                mailsetup.setupEmailParamWithAttachment("INVOICE DETAILS", details, filetoprint, true,emailadd);
            }
        }

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION NO ONE TIME DISCOUNT 
        public void printReceiptResto(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype
                                , bool iszerorated = false)


        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, GlobalVariables.computerName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string mark = gridview.Rows[i].Cells["isVat"].Value.ToString();
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                var rox = Database.getMultipleQuery("POSCreditCardTransactions", $"ReferenceNo='{ordercode}' ", "CCName,CCNumber,CCType,CCBank,CCPaymentReferenceNo");
                cardno = rox["CCNumber"].ToString();
                cardtype = rox["CCType"].ToString();
                cardrefno = rox["CCPaymentReferenceNo"].ToString();
                string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            if (paytype == "Merchant")
            {
                string ReferenceNo = "", MerchantName = "", VoucherCode = "";
                var rox = Database.getMultipleQuery("POSMerchantTransactions", $"OrderNo='{ordercode}' ", "ReferenceNo,MerchantName,VoucherCode,Amount");
                ReferenceNo = rox["ReferenceNo"].ToString();
                MerchantName = rox["MerchantName"].ToString();
                VoucherCode = rox["VoucherCode"].ToString();
                //string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Merchant " + MerchantName) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("VoucherCode: " + VoucherCode) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + ReferenceNo) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }

            txtorder =  ordercode + ".txt";
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + ordercode);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + ordercode;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();


            printTextFile(filetoprint);
        }

        public void printReceiptRestoOneLove(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype
                                , bool iszerorated = false)


        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string mark = gridview.Rows[i].Cells["isVat"].Value.ToString();
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                var rox = Database.getMultipleQuery("POSCreditCardTransactions", $"ReferenceNo='{ordercode}' ", "CCName,CCNumber,CCType,CCBank,CCPaymentReferenceNo");
                cardno = rox["CCNumber"].ToString();
                cardtype = rox["CCType"].ToString();
                cardrefno = rox["CCPaymentReferenceNo"].ToString();
                string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            if (paytype == "Merchant")
            {
                string ReferenceNo = "", MerchantName = "", VoucherCode = "";
                var rox = Database.getMultipleQuery("POSMerchantTransactions", $"OrderNo='{ordercode}' ", "ReferenceNo,MerchantName,VoucherCode,Amount");
                ReferenceNo = rox["ReferenceNo"].ToString();
                MerchantName = rox["MerchantName"].ToString();
                VoucherCode = rox["VoucherCode"].ToString();
                //string last4 = Database.getSingleResultSet($"SELECT RIGHT(CCNumber,4) FROM POSCreditCardTransactions WHERE ReferenceNo='{ordercode}'");
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Merchant " + MerchantName) + Environment.NewLine;
                //details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + last4) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("VoucherCode: " + VoucherCode) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + ReferenceNo) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }

            txtorder = "\\" + ordercode + ".txt";
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filetoprint);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + ordercode;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();
            printTextFile(filetoprint);
            //for (int xxx=0;xxx<=3;xxx++)
            //{
            //    printTextFile(filetoprint);
            //}
        }
        //THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)--eulzcccc
        public void printReceipt(string transcode
                                , string ordercode /**/
                                , string total/**/
                                , string peritemdiscount/**/
                                , string netofvatindiscitems/**/
                                , string netofvatindinonscitems
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string disctype
                                , string footerlabel
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch,Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,name,address,tin,bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            var rows = Database.getMultipleQuery("SalesDiscount", $"OrderNo='{ordercode}' " +
                $"AND isErrorCorrect=0 " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}'", "DiscRemarks,DiscountPercentage,DiscountAmount");

            string discremarks = rows["DiscRemarks"].ToString();
            string discountpercentage = rows["DiscountPercentage"].ToString();
            string discountamount = rows["DiscountAmount"].ToString();
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            double newdiscitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
               
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if(disctype=="REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if(disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }
                   
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc=0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                //if (POS.POSConfirmPayment.isSeniorDiscount == true)
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat =  Math.Round(netofscdisc * .12, 2);//0;//
                    totaltotal = Math.Round(netofscdisc + addvat, 2);
                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                   
                } 
                //else if (POS.POSConfirmPayment.isPwdDiscount == true)
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);//0;//
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "NAAC")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("NAAC DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("NAAC ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);//0;//
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less NAAC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net NAAC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "SOLOPARENT")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SOLO PARENT DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);//0;//
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SP Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SP Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "MOV")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("MOV DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);//0;//
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less MOV Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net MOV Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                //else if (POS.POSConfirmPayment.isOthersDiscount == true)
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12,2);
                    netofvat = Math.Round(totalvatitems / 1.12,2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc,2);
                    addvat = Math.Round(netofscdisc * .12,2);
                    totaltotal = Math.Round(netofscdisc + addvat,2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    //details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(total)-Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine + Environment.NewLine;
                    newdiscitems = Convert.ToDouble(total) - Convert.ToDouble(POS.POSConfirmPayment.discamount);
                }

            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", HelperFunction.convertToNumericFormat(Convert.ToDouble(cash)- newdiscitems)) + Environment.NewLine + Environment.NewLine;

            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;// vat) + Environment.NewLine;
            // details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            if(paytype=="Credit")
            {
                string cardno="", cardtype="", cardrefno="";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-"+ cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: "+ cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: "+ cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }


            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if(isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }
            if (isDiscount)
            {
                txtorder = PointOfSale.refno + "-" + footerlabel + ".txt";
            }
            else
            {
                txtorder = PointOfSale.refno+ ".txt";
            }
            string lastranfile = "\\LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();

            printTextFile(filetoprint);
        }

        ///--------------------------------------------------------------------------------
        /////THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptResto(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string netofvatindiscitems
                                , string netofvatindinonscitems
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string disctype
                                , string footerlabel
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            var rows = Database.getMultipleQuery("SalesDiscount", $"OrderNo='{ordercode}' " +
                $"AND isErrorCorrect=0 " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}'", "DiscRemarks,DiscountPercentage,DiscountAmount");

            string discremarks = rows["DiscRemarks"].ToString();
            string discountpercentage = rows["DiscountPercentage"].ToString();
            string discountamount = rows["DiscountAmount"].ToString();
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);


                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                //if (POS.POSConfirmPayment.isSeniorDiscount == true)
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);
                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
                //else if (POS.POSConfirmPayment.isPwdDiscount == true)
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                //else if (POS.POSConfirmPayment.isOthersDiscount == true)
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;// vat) + Environment.NewLine;
            // details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }


            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }
            if (isDiscount)
            {
                txtorder = ordercode + "-" + footerlabel + ".txt";
            }
            else
            {
                txtorder = ordercode + ".txt";
            }
            string lastranfile = "LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();

            printTextFile(filetoprint);
        }
        ///

        ///--------------------------------------------------------------------------------
        /////THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptRestoOneLove(string transcode
                                , string ordercode
                                , string total
                                , string peritemdiscount
                                , string netofvatindiscitems
                                , string netofvatindinonscitems
                                , string vatablesale
                                , string vatexemptsale
                                , string vat
                                , string cash
                                , string change
                                , DataGridView gridview
                                , bool isDiscount
                                , string disctype
                                , string footerlabel
                                , string name
                                , string address
                                , string tin
                                , string bussstyle
                                , string paytype)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            string LastTransactionfilepath = "C:\\POSTransaction\\LastTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;

            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            var rows = Database.getMultipleQuery("SalesDiscount", $"OrderNo='{ordercode}' " +
                $"AND isErrorCorrect=0 " +
                $"AND BranchCode='{Login.assignedBranch}' " +
                $"AND MachineUsed='{Environment.MachineName}'", "DiscRemarks,DiscountPercentage,DiscountAmount");

            string discremarks = rows["DiscRemarks"].ToString();
            string discountpercentage = rows["DiscountPercentage"].ToString();
            string discountamount = rows["DiscountAmount"].ToString();
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);


                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                //if (POS.POSConfirmPayment.isSeniorDiscount == true)
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);
                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
                //else if (POS.POSConfirmPayment.isPwdDiscount == true)
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                //else if (POS.POSConfirmPayment.isOthersDiscount == true)
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }


            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                //details += HelperFunction.PrintCenterText(footerlabel);
            }
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            if (!Directory.Exists(LastTransactionfilepath)) //last ttransaction filepath
            {
                Directory.CreateDirectory(LastTransactionfilepath);
            }
            if (isDiscount)
            {
                txtorder = ordercode + "-" + footerlabel + ".txt";
            }
            else
            {
                txtorder = ordercode + ".txt";
            }
            string lastranfile = "LastTran.txt";

            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            string lastfiletoprint = LastTransactionfilepath + txtorder;
            StreamWriter writerLast = new StreamWriter(LastTransactionfilepath + lastranfile);
            writerLast.Write(details);
            writerLast.Close();

            printTextFile(filetoprint);
        }
        ///


        //THIS TEMPLATE HAS BEEN ALWAYS USED FOR REPRINT
        public void ReprintReceipt(string transcode
                                    , string ordercode
                                    , string total
                                    , string peritemdiscount
                                    , string netofvatindiscitems
                                    , string netofvatindinonscitems
                                    , string vatablesale
                                    , string vatexemptsale
                                    , string vat
                                    , string cash
                                    , string change
                                    , DataGridView gridview
                                    , bool isDiscount
                                    , string disctype
                                    , string footerlabel
                                    , string name
                                    , string address
                                    , string tin
                                    , string bussstyle
                                    , string paytype 
                                    , bool iszerorated=false
                                   )
        {

            String details = "";
            string filepath1 = "C:\\POSTransaction\\CopyForReprint\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doTitle("REPRINT");
            details += HelperFunction.PrintLeftText("$$$$$") + Environment.NewLine;
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,name,address,tin,bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = "0";
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
             
                if (isDiscount)
                {
                    discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
                    double discPercent = Convert.ToDouble(discountpercentage) * 100;
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPayment.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
           
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if(isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();


            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = "\\" + PointOfSale.refno + ".txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
        }

        /// ////////////////////////////////////////////////////////////////
        //THIS TEMPLATE HAS BEEN ALWAYS USED FOR REPRINT
        public void ReprintReceiptResto(string transcode
                                    , string ordercode
                                    , string total
                                    , string peritemdiscount
                                    , string netofvatindiscitems
                                    , string netofvatindinonscitems
                                    , string vatablesale
                                    , string vatexemptsale
                                    , string vat
                                    , string cash
                                    , string change
                                    , DataGridView gridview
                                    , bool isDiscount
                                    , string disctype
                                    , string footerlabel
                                    , string name
                                    , string address
                                    , string tin
                                    , string bussstyle
                                    , string paytype
                                    , bool iszerorated = false
                                   )
        {

            String details = "";
            string filepath1 = "C:\\POSTransaction\\CopyForReprint\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doTitle("REPRINT");
            //details += HelperFunction.PrintLeftText("REPRINT#:*") + Environment.NewLine;
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = "0";
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }

                if (isDiscount)
                {
                    discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
                    double discPercent = Convert.ToDouble(discountpercentage) * 100;
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPaymentResto.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();


            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = ordercode + ".txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
        }

        public void ReprintReceiptRestoOneLove(string transcode
                                    , string ordercode
                                    , string total
                                    , string peritemdiscount
                                    , string netofvatindiscitems
                                    , string netofvatindinonscitems
                                    , string vatablesale
                                    , string vatexemptsale
                                    , string vat
                                    , string cash
                                    , string change
                                    , DataGridView gridview
                                    , bool isDiscount
                                    , string disctype
                                    , string footerlabel
                                    , string name
                                    , string address
                                    , string tin
                                    , string bussstyle
                                    , string paytype
                                    , bool iszerorated = false
                                   )
        {

            String details = "";
            string filepath1 = "C:\\POSTransaction\\CopyForReprint\\"; 
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            string discountpercentage = "0";
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }

                if (isDiscount)
                {
                    discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
                    double discPercent = Convert.ToDouble(discountpercentage) * 100;
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discPercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    //details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.othersRemarks) + Environment.NewLine + Environment.NewLine;
                    //details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.otherDiscountAmount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    lessvat = Math.Round(totalvatitems / 1.12 * 0.12, 2);
                    netofvat = Math.Round(totalvatitems / 1.12, 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less Reg Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net Reg Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;

                }
            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            //if(Convert.ToDouble(peritemdiscount) > 0 && isDiscount == false)
            //{
            //    details += HelperFunction.PrintLeftRigthText("NET Due:", POS.POSConfirmPaymentResto.netamountpayable) + Environment.NewLine + Environment.NewLine;
            //}
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();


            if (!Directory.Exists(filepath1))
            {
                Directory.CreateDirectory(filepath1);
            }
            string lasttrans = ordercode + ".txt";
            StreamWriter writer1 = new StreamWriter(filepath1 + lasttrans);
            writer1.Write(details);
            writer1.Close();
        }
        //////////////////////////////////////////////////////////////////////

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION
        public void printReceiptConsolidated(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype
                                            , bool iszerorated=false)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\"+ DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,name,address,tin,bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {

                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;
            
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
          
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = "\\" + cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();

        }

        //----------------------------------------------------------------------------------------------
        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION
        public void printReceiptConsolidatedResto(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype
                                            , bool iszerorated = false)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {

                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;


            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", total) + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", vatablesale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", vat) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();

        }

        public void printReceiptConsolidatedRestoOneLove(string cashiertranscode
                                           , string transcode
                                           , string ordercode
                                           , string total
                                           , string peritemdiscount
                                           , string vatablesale
                                           , string vatexemptsale
                                           , string vat
                                           , string cash
                                           , string change
                                           , DataGridView gridview
                                           , bool isDiscount
                                           , string name
                                           , string address
                                           , string tin
                                           , string bussstyle
                                           , string paytype
                                           , bool iszerorated = false)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {

                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;


            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            // details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();

        }
        //---------------------------------------------------------------------------------------------------------

        //THIS TEMPLATE IS USED FOR PLAIN TRANSACTION
        public void printReceiptConsolidatedXX()
        {
            double totalPerItemDiscount = 0.0, total = 0.0;
            String details = "";
            string filepath = "C:\\POSTransactionX\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string paytype = "",amounttender="",amountchange="",TotalVatableSalesSum="",TotalVatSaleSum="",TotalVatExemptSum="", CashierTransNo="";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            //details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            SqlConnection con = Database.getConnection();
            con.Open();
            SqlCommand com = new SqlCommand("SELECT TOP 3 * FROM BatchSalesDetails", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            foreach (DataRow row in table.Rows)
            {
                CashierTransNo = row["CashierTransNo"].ToString();
                TotalVatableSalesSum = row["TotalVatableSale"].ToString();
                TotalVatSaleSum = row["TotalVATSale"].ToString();
                TotalVatExemptSum = row["TotalVATExemptSale"].ToString();

                amounttender = row["AmountTendered"].ToString();
                amountchange = row["AmountChange"].ToString();
                paytype = row["PaymentType"].ToString();
                totalPerItemDiscount += Convert.ToDouble(row["DiscountTotal"].ToString());
                total += Convert.ToDouble(row["TotalAmount"].ToString());
                string addV = "";
                if (Convert.ToBoolean(row["isVat"].ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(Convert.ToDouble(row["DiscountTotal"].ToString())) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                details += HelperFunction.PrintLeftText(row["Description"].ToString()) + Environment.NewLine;
                string a = "  - " + row["QtySold"].ToString() + " @ " + row["SellingPrice"].ToString();
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(row["TotalAmount"].ToString()) + Convert.ToDouble(row["DiscountTotal"].ToString());

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(row["DiscountTotal"].ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + row["DiscountTotal"].ToString() + ")") + Environment.NewLine;
                }
            }
            
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(totalPerItemDiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", totalPerItemDiscount.ToString()) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total.ToString()) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;


            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", amounttender) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", amounttender) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", amountchange) + Environment.NewLine + Environment.NewLine;
            bool iszerorated = false;
            if (iszerorated == true)
            {
                details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "0.00") + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            else
            {
               
                details += HelperFunction.PrintLeftRigthText("VATable Sales", TotalVatableSalesSum) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT Amount", TotalVatSaleSum) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", TotalVatExemptSum) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            //if (paytype == "Credit")
            //{
            //    string cardno = "", cardtype = "", cardrefno = "";
            //    details += HelperFunction.createDottedLine() + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
            //    details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
            //    details += HelperFunction.createDottedLine() + Environment.NewLine;
            //}

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = "\\" + CashierTransNo + "_E-JOURNAL.txt";
            txtorder2 = "\\" + CashierTransNo + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();

        }
        //Additional Overriding method
        //THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptConsolidated(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string netofvatindiscitems
                                            , string netofvatindinonscitems
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string disctype
                                            , string footerlabel
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            ,string paytype)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }
                
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///netofvatafteronetimedisc

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPayment.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPayment.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPayment.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2); //must change the percentage
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPayment.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales:", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount:", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES:", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES:", "0.00") + Environment.NewLine + Environment.NewLine;
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            
            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }
           
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
           
            txtorder = "\\" + cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = "\\" + cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();

        }
        ///
        //Additional Overriding method
        //THIS TEMPLATE HAS BEEN USED WITH ONE TIME DISCOUNT (e.g SENIOR,PWD,OTHERS)
        public void printReceiptConsolidatedResto(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string netofvatindiscitems
                                            , string netofvatindinonscitems
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string disctype
                                            , string footerlabel
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch, Environment.MachineName);
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, name, address, tin, bussstyle);
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///netofvatafteronetimedisc

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2); //must change the percentage
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;

            details += HelperFunction.PrintLeftRigthText("VATable Sales:", HelperFunction.convertToNumericFormat(totalvatableSales)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount:", HelperFunction.convertToNumericFormat(totalVatInputSale)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES:", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES:", "0.00") + Environment.NewLine + Environment.NewLine;
            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);

            if (isDiscount)
            {
                details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder =  cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();

        }


        public void printReceiptConsolidatedRestoOneLove(string cashiertranscode
                                            , string transcode
                                            , string ordercode
                                            , string total
                                            , string peritemdiscount
                                            , string netofvatindiscitems
                                            , string netofvatindinonscitems
                                            , string vatablesale
                                            , string vatexemptsale
                                            , string vat
                                            , string cash
                                            , string change
                                            , DataGridView gridview
                                            , bool isDiscount
                                            , string disctype
                                            , string footerlabel
                                            , string name
                                            , string address
                                            , string tin
                                            , string bussstyle
                                            , string paytype)
        {
            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + Login.Fullname + "\\TransactionJournalSummary\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Environment.NewLine;
            details += Convert.ToInt32(ordercode).ToString();
            details += Environment.NewLine;
            string discountpercentage = Database.getSingleQuery("SalesDiscount", $"OrderNo='{ordercode}' and isErrorCorrect=0 AND BranchCode='{Login.assignedBranch}' ", "DiscountPercentage");
            double discpercent = Convert.ToDouble(discountpercentage) * 100;
            double totalvatitems = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                    totalvatitems += Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value.ToString());
                }
                else
                {
                    addV = "";
                }
                string addD = "";
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    addD = "  - (Less: Discount)";
                }
                else
                {
                    addD = "";
                }

                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " @ " + gridview.Rows[i].Cells["UnitPrice"].Value;
                double cleanbalance = 0.0;
                cleanbalance = Convert.ToDouble(gridview.Rows[i].Cells["Amount"].Value) + Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value);

                // string b = " " + gridview.Rows[i].Cells["Amount"].Value + addV;
                string b = " " + HelperFunction.convertToNumericFormat(cleanbalance) + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
                if (Convert.ToDouble(gridview.Rows[i].Cells["Discount"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["Discount"].Value.ToString() + ")") + Environment.NewLine;
                }
                if (isDiscount)
                {
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                    }
                    else if (disctype != "REGULAR")
                    {
                        bool isSCorPWDDiscounted = false;
                        isSCorPWDDiscounted = Database.checkifExist("SELECT TOP 1 Description FROM Products WHERE BranchCode='" + Login.assignedBranch + "' " +
                            "AND Description='" + gridview.Rows[i].Cells["Particulars"].Value.ToString() + "' " +
                            "AND isDiscount=1");
                        if (isSCorPWDDiscounted)
                        {
                            details += HelperFunction.PrintLeftText("  - (Less: Discount " + discpercent.ToString() + "%)") + Environment.NewLine;
                        }
                    }

                }
            }
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            if (Convert.ToDouble(peritemdiscount) > 0)
            {
                details += HelperFunction.PrintLeftRigthText("TOTAL DISCOUNT:", peritemdiscount) + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;

            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, netofnonscdisc = 0.0, addvat = 0.0, totaltotal = 0.0;
            netofnonscdisc = Convert.ToDouble(netofvatindinonscitems);
            if (isDiscount == true)
            {
                if (disctype == "SENIOR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("SENIOR DISCOUNT") + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("OSCA SC/ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    ///netofvatafteronetimedisc

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less SC Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net SC Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////

                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("PWD ID: " + POS.POSConfirmPaymentResto.discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + POS.POSConfirmPaymentResto.discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;

                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2);
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + POS.POSConfirmPaymentResto.discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.discamount))) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                    lessvat = Math.Round(Convert.ToDouble(netofvatindiscitems) * 0.12, 2);
                    netofvat = Math.Round(Convert.ToDouble(netofvatindiscitems), 2);
                    //lessscdisc = Math.Round(netofvat * 0.05, 2); //must change the percentage
                    lessscdisc = Math.Round(netofvat * Convert.ToDouble(discountpercentage), 2);
                    netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                    addvat = Math.Round(netofscdisc * .12, 2);
                    totaltotal = Math.Round(netofscdisc + addvat, 2);

                    details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Less PWD Discount:", HelperFunction.convertToNumericFormat(lessscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Net PWD Discount:", HelperFunction.convertToNumericFormat(netofscdisc)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal)) + Environment.NewLine;

                    details += HelperFunction.PrintLeftRigthText("AMOUNT DUE:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                }

            }
            if (paytype == "Credit")
            {
                details += HelperFunction.PrintLeftRigthText("TENDERED:", HelperFunction.convertToNumericFormat(Convert.ToDouble(POS.POSConfirmPaymentResto.netamountpayable))) + Environment.NewLine + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("CHANGE  :", "0.00") + Environment.NewLine + Environment.NewLine;
            }
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;


            double totalvatableSales = netofscdisc + netofnonscdisc;
            double totalVatInputSale = 0.0;
            totalVatInputSale = totalvatableSales * 0.12;


            if (paytype == "Credit")
            {
                string cardno = "", cardtype = "", cardrefno = "";
                details += HelperFunction.createDottedLine() + Environment.NewLine;
                details += HelperFunction.PrintLeftText("PAYMENT TYPE: Credit Card") + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Number: XXXX-XXXX-XXXX-" + cardno) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Card Type: " + cardtype) + Environment.NewLine;
                details += HelperFunction.PrintLeftText("Reference No.: " + cardrefno) + Environment.NewLine;
                details += HelperFunction.createDottedLine() + Environment.NewLine;
            }
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            //details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);

            if (isDiscount)
            {
                //details += HelperFunction.PrintCenterText(footerlabel);
            }

            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            txtorder = cashiertranscode + "_E-JOURNAL.txt";
            txtorder2 = cashiertranscode + "CSVJournal" + ".csv";
            string filetoprint = filepath + txtorder;
            string filetoprintcsv = filepath + txtorder2;
            StreamWriter writer;//,writer22;
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
                writer = new StreamWriter(filetoprint);
            }
            else
            {
                writer = new StreamWriter(filetoprint, true);
            }
            writer.Write(details);
            writer.Close();

        }


        ///
        // IS USED VOID SELECTED ITEM BUTTON katong mo select ka sa checkbox //CHANGE TO RETURNED
        public void printReturnSelectedItem(string returntranscode,string transcode, string ordercode, DataGridView gridview)
        {
            bool isdiscounted = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + ordercode + "' " +
                "AND isErrorCorrect=0 " +
                "AND BranchCode='"+Login.assignedBranch+"' " +
                "AND MachineUsed='"+Environment.MachineName+"'");
            string discidno = "", discname = "", discremarks = "", disctype = "", discamount = "", discvatadj = "", discPercentage = "";
            double discpercentageamount = 0.0;
            
            if (isdiscounted==true)
            {
                var row = Database.getMultipleQuery("SalesDiscount", "OrderNo='" + ordercode + "' " +
                    "AND isErrorCorrect=0 and BranchCode='" + Login.assignedBranch + "'", "DiscountType,DiscIDNo,DiscName,DiscRemarks,DiscountAmount,VatAdjustment,DiscountPercentage");
                discidno = row["DiscIDNo"].ToString();
                discname = row["DiscName"].ToString();
                discremarks = row["DiscRemarks"].ToString();
                disctype = row["DiscountType"].ToString();
                discamount = row["DiscountAmount"].ToString();
                discvatadj = row["VatAdjustment"].ToString();
                discPercentage = row["DiscountPercentage"].ToString();
                if (discPercentage == "") { discPercentage = "0"; }

                discpercentageamount = Convert.ToDouble(discPercentage) * 100;
            }
            //string disctype = Database.getSingleQuery("SalesDiscount", "OrderNo='" + ordercode + "'", "DiscountType");

            String details = "";
            string filepathConso = "C:\\POSTransaction\\ReturnedSalesConso\\";
            string filepath = "C:\\POSTransaction\\ReturnedSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doTitle("RETURNED TRANSACTION");
            details += HelperFunction.PrintLeftText("Return Transaction No.: " + returntranscode)+Environment.NewLine;
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno,"Ref");
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;
            string isvat = "";

            double vatablesale = 0.0;
            double vatableWithSCDiscount = 0.0, vatableWithNoSCDiscount=0.0, vatExWithSCDiscount=0.0, vatExWithNoSCDiscount=0.0;
            double discountAmount=0.0,totalDiscountAmountSC = 0.0;
            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, addvat = 0.0, totaltotal = 0.0, totalAmount = 0.0,_totalvatablesales= 0.0, _totalvatexsales=0.0;
            string addD = "";
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string a = "  - " + gridview.Rows[i].Cells["QtySold"].Value + " @ " + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = "-" + gridview.Rows[i].Cells["TotalAmount"].Value.ToString();
                string c = gridview.Rows[i].Cells["isVat"].Value.ToString();

                totalAmount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());

                if (Convert.ToBoolean(c) == true)
                {
                    isvat = "V";
                    _totalvatablesales += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                }
                else
                {
                    isvat = "";
                    _totalvatexsales += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                }

                //--------------------------------------
                //DISPLAY A,B AND C
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(a, b + isvat) + Environment.NewLine;
                //--------------------------------------
                if (isdiscounted) //there is a onetime discount either SC,PWD AND REGULAR
                {
                    bool isSCorPWDDiscountedVat = false, isSCorPWDDiscountedNonVat = false;
                    //---------------VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                    isSCorPWDDiscountedVat = Database.checkifExist("SELECT TOP(1) ProductCode " +
                                                               "FROM dbo.BatchSalesDetails " +
                                                               "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                               "AND Description='" + gridview.Rows[i].Cells["Description"].Value.ToString() + "' " +
                                                               "AND ReferenceNo='" + ordercode + "' " +
                                                               "AND DiscountTotal <= 0 " +
                                                               "AND isVat = 1 " +
                                                               "AND ProductCode in (SELECT ProductCode " +
                                                                                       "FROM dbo.Products " +
                                                                                       "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                                       "AND isDiscount=1)");
                    //---------------NON VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                    isSCorPWDDiscountedNonVat = Database.checkifExist("SELECT TOP(1) ProductCode " +
                                                              "FROM dbo.BatchSalesDetails " +
                                                              "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                              "AND Description='" + gridview.Rows[i].Cells["Description"].Value.ToString() + "' " +
                                                              "AND ReferenceNo='" + ordercode + "' " +
                                                              "AND DiscountTotal <= 0 " +
                                                              "AND isVat = 0 " +
                                                              "AND ProductCode in (SELECT ProductCode " +
                                                                                      "FROM dbo.Products " +
                                                                                      "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                                      "AND isDiscount=1)");
                    //##############################################################
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                        discountAmount += (Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString()) / 1.12) * Convert.ToDouble(discPercentage);
                    }
                    else
                    {
                        //---------------VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                        /*##*/
                        if (isSCorPWDDiscountedVat == true && Convert.ToBoolean(c) == true)
                        {
                            vatableWithSCDiscount = Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                            details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                            discountAmount += (vatableWithSCDiscount / 1.12) * Convert.ToDouble(discPercentage);
                        }
                        else if (isSCorPWDDiscountedVat == false && Convert.ToBoolean(c) == true)
                        {
                            vatableWithNoSCDiscount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                        }
                        //---------------VAT EXEMPT PRODUCT WITH SENIOR DISCOUNT ITEM
                        /*##*/
                        if (isSCorPWDDiscountedNonVat == true && Convert.ToBoolean(c) == false)
                        {
                            vatExWithSCDiscount = Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                            details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                            discountAmount += (vatExWithSCDiscount) * Convert.ToDouble(discPercentage);

                        }
                        else if (isSCorPWDDiscountedNonVat == false && Convert.ToBoolean(c) == false)
                        {
                            vatExWithNoSCDiscount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                        }
                    }
                    totalDiscountAmountSC = Math.Round(discountAmount,2);
                }
                else
                {
                    vatableWithNoSCDiscount += Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString());
                }

                //--------------------------------------IF TRUE MEANING THERE IS PER ITEM DISCOUNT
                if (Convert.ToDouble(gridview.Rows[i].Cells["DiscountTotal"].Value.ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.Rows[i].Cells["DiscountTotal"].Value.ToString() + ")") + Environment.NewLine;
                }
                //--------------------------------------

            }//end of loop
            //--------------------------------------
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", HelperFunction.convertToNumericFormat(totalAmount*-1)) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
            //--------------------------------------
            double totalvatableSales = 0.0;
            double totalVatInputSale = 0.0;
            double totalVatExemptSale = 0.0;

            if (isdiscounted == true)
            {
                string id = "";
                if (disctype == "SENIOR") { id = "OSCA SC/ID: "; }
                else if (disctype == "PWD") { id = "PWD ID: "; }
                else if (disctype == "REGULAR") { id = " "; }

                if (disctype == "SENIOR" || disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText(disctype + " DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText(id + discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(totalDiscountAmountSC * -1)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(totalDiscountAmountSC * -1)) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                }
                
                lessvat = Math.Round((vatableWithSCDiscount / 1.12) * 0.12, 2);
                netofvat = Math.Round((vatableWithSCDiscount / 1.12), 2);
                lessscdisc = Math.Round(netofvat * Convert.ToDouble(discpercentageamount/100), 2); //must change the percentage
                netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                addvat = Math.Round(netofscdisc * .12, 2);
                totaltotal = Math.Round(netofscdisc + addvat, 2);

                details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Less Discount:", HelperFunction.convertToNumericFormat(lessscdisc * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Net Discount:", HelperFunction.convertToNumericFormat(netofscdisc * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal * -1)) + Environment.NewLine;

                details += HelperFunction.createDottedLine() + Environment.NewLine;

                vatablesale = vatableWithNoSCDiscount / 1.12;

                totalvatableSales = Math.Round(netofscdisc + vatablesale, 2);
                totalVatInputSale = Math.Round(totalvatableSales * 0.12, 2);
                totalVatExemptSale = Math.Round((vatExWithSCDiscount + vatExWithNoSCDiscount), 2);
            }
            else
            {
                vatablesale = _totalvatablesales / 1.12;
                totalvatableSales = Math.Round(vatablesale, 2);
                totalVatInputSale = Math.Round(totalvatableSales * 0.12, 2);
                totalVatExemptSale = Math.Round(_totalvatexsales, 2);
            }
         
            details += Environment.NewLine;
           

            details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales * -1)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale *-1)) + Environment.NewLine;// vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", HelperFunction.convertToNumericFormat(totalVatExemptSale *-1)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;
 
            //----------------------------------------------------------------------------------------------------
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch,"");

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTERRETURN.txt");
            //string str1 = Classes.Utilities.readFile(path); 
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);


            details += HelperFunction.LastPagePaper() + Environment.NewLine;

            if (!Directory.Exists(filepathConso))
            {
                Directory.CreateDirectory(filepathConso);
            }
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + ordercode + ".txt";
            //conso
            string filetoprintConso = filepathConso + txtorder;
            StreamWriter writerConso = new StreamWriter(filepathConso + txtorder);
            writerConso.Write(details);
            writerConso.Close();
            //per folder per date
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        ///
        // IS USED VOID SELECTED ITEM BUTTON katong mo select ka sa checkbox //CHANGE TO RETURNED
        public void printReturnSelectedItemDevEx(string returntranscode, string transcode, string ordercode, GridView gridview)
        {
            bool isdiscounted = Database.checkifExist("SELECT TOP(1) OrderNo FROM dbo.SalesDiscount WHERE OrderNo='" + ordercode + "' " +
                "AND isErrorCorrect=0 " +
                "AND BranchCode='" + Login.assignedBranch + "' " +
                "AND MachineUsed='" + Environment.MachineName + "'");
            string discidno = "", discname = "", discremarks = "", disctype = "", discamount = "", discvatadj = "", discPercentage = "";
            double discpercentageamount = 0.0;

            if (isdiscounted == true)
            {
                var row = Database.getMultipleQuery("SalesDiscount", "OrderNo='" + ordercode + "' " +
                    "AND isErrorCorrect=0 and BranchCode='" + Login.assignedBranch + "'", "DiscountType,DiscIDNo,DiscName,DiscRemarks,DiscountAmount,VatAdjustment,DiscountPercentage");
                discidno = row["DiscIDNo"].ToString();
                discname = row["DiscName"].ToString();
                discremarks = row["DiscRemarks"].ToString();
                disctype = row["DiscountType"].ToString();
                discamount = row["DiscountAmount"].ToString();
                discvatadj = row["VatAdjustment"].ToString();
                discPercentage = row["DiscountPercentage"].ToString();
                if (discPercentage == "") { discPercentage = "0"; }

                discpercentageamount = Convert.ToDouble(discPercentage) * 100;
            }
            //string disctype = Database.getSingleQuery("SalesDiscount", "OrderNo='" + ordercode + "'", "DiscountType");

            String details = "";
            string filepathConso = "C:\\POSTransaction\\ReturnedSalesConso\\";
            string filepath = "C:\\POSTransaction\\ReturnedSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doTitle("RETURNED TRANSACTION");
            details += HelperFunction.PrintLeftText("Return Transaction No.: " + returntranscode) + Environment.NewLine;
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, "Ref");
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;
            string isvat = "";

            double vatablesale = 0.0;
            double vatableWithSCDiscount = 0.0, vatableWithNoSCDiscount = 0.0, vatExWithSCDiscount = 0.0, vatExWithNoSCDiscount = 0.0;
            double discountAmount = 0.0, totalDiscountAmountSC = 0.0;
            double lessvat = 0.0, netofvat = 0.0, lessscdisc = 0.0, netofscdisc = 0.0, addvat = 0.0, totaltotal = 0.0, totalAmount = 0.0, _totalvatablesales = 0.0, _totalvatexsales = 0.0;
            string addD = "";
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {

                string a = "  - " + gridview.GetRowCellValue(i, "QtySold").ToString() + " @ " + gridview.GetRowCellValue(i, "SellingPrice").ToString();
                string b = "-" + gridview.GetRowCellValue(i, "TotalAmount").ToString();
                string c = gridview.GetRowCellValue(i, "isVat").ToString();

                totalAmount += Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());

                if (Convert.ToBoolean(c) == true)
                {
                    isvat = "V";
                    _totalvatablesales += Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                }
                else
                {
                    isvat = "";
                    _totalvatexsales += Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                }

                //--------------------------------------
                //DISPLAY A,B AND C
                details += HelperFunction.PrintLeftText(gridview.GetRowCellValue(i, "Description").ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(a, b + isvat) + Environment.NewLine;
                //--------------------------------------
                if (isdiscounted) //there is a onetime discount either SC,PWD AND REGULAR
                {
                    bool isSCorPWDDiscountedVat = false, isSCorPWDDiscountedNonVat = false;
                    //---------------VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                    isSCorPWDDiscountedVat = Database.checkifExist("SELECT TOP(1) ProductCode " +
                                                               "FROM dbo.BatchSalesDetails " +
                                                               "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                               "AND Description='" + gridview.GetRowCellValue(i, "Description").ToString() + "' " +
                                                               "AND ReferenceNo='" + ordercode + "' " +
                                                               "AND DiscountTotal <= 0 " +
                                                               "AND isVat = 1 " +
                                                               "AND ProductCode in (SELECT ProductCode " +
                                                                                       "FROM dbo.Products " +
                                                                                       "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                                       "AND isDiscount=1)");
                    //---------------NON VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                    isSCorPWDDiscountedNonVat = Database.checkifExist("SELECT TOP(1) ProductCode " +
                                                              "FROM dbo.BatchSalesDetails " +
                                                              "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                              "AND Description='" + gridview.GetRowCellValue(i, "Description").ToString() + "' " +
                                                              "AND ReferenceNo='" + ordercode + "' " +
                                                              "AND DiscountTotal <= 0 " +
                                                              "AND isVat = 0 " +
                                                              "AND ProductCode in (SELECT ProductCode " +
                                                                                      "FROM dbo.Products " +
                                                                                      "WHERE BranchCode='" + Login.assignedBranch + "' " +
                                                                                      "AND isDiscount=1)");
                    //##############################################################
                    if (disctype == "REGULAR")
                    {
                        details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                        discountAmount += (Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()) / 1.12) * Convert.ToDouble(discPercentage);
                    }
                    else
                    {
                        //---------------VATABLE PRODUCT WITH SENIOR DISCOUNT ITEM
                        /*##*/
                        if (isSCorPWDDiscountedVat == true && Convert.ToBoolean(c) == true)
                        {
                            vatableWithSCDiscount = Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                            details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                            discountAmount += (vatableWithSCDiscount / 1.12) * Convert.ToDouble(discPercentage);
                        }
                        else if (isSCorPWDDiscountedVat == false && Convert.ToBoolean(c) == true)
                        {
                            vatableWithNoSCDiscount += Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                        }
                        //---------------VAT EXEMPT PRODUCT WITH SENIOR DISCOUNT ITEM
                        /*##*/
                        if (isSCorPWDDiscountedNonVat == true && Convert.ToBoolean(c) == false)
                        {
                            vatExWithSCDiscount = Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                            details += HelperFunction.PrintLeftText("  - (Less: Discount: " + discpercentageamount.ToString() + "%)") + Environment.NewLine;
                            discountAmount += (vatExWithSCDiscount) * Convert.ToDouble(discPercentage);

                        }
                        else if (isSCorPWDDiscountedNonVat == false && Convert.ToBoolean(c) == false)
                        {
                            vatExWithNoSCDiscount += Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                        }
                    }
                    totalDiscountAmountSC = Math.Round(discountAmount, 2);
                }
                else
                {
                    vatableWithNoSCDiscount += Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                }

                //--------------------------------------IF TRUE MEANING THERE IS PER ITEM DISCOUNT
                if (Convert.ToDouble(gridview.GetRowCellValue(i, "DiscountTotal").ToString()) > 0)
                {
                    details += HelperFunction.PrintLeftRigthText(addD, "(" + gridview.GetRowCellValue(i, "DiscountTotal").ToString() + ")") + Environment.NewLine;
                }
                //--------------------------------------

            }//end of loop
            //--------------------------------------
            details += HelperFunction.PrinttoRight("----------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL DUE:", HelperFunction.convertToNumericFormat(totalAmount * -1)) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("==========") + Environment.NewLine;
            //--------------------------------------
            double totalvatableSales = 0.0;
            double totalVatInputSale = 0.0;
            double totalVatExemptSale = 0.0;

            if (isdiscounted == true)
            {
                string id = "";
                if (disctype == "SENIOR") { id = "OSCA SC/ID: "; }
                else if (disctype == "PWD") { id = "PWD ID: "; }
                else if (disctype == "REGULAR") { id = " "; }

                if (disctype == "SENIOR" || disctype == "PWD")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText(disctype + " DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText(id + discidno) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Name: " + discname) + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount:", HelperFunction.convertToNumericFormat(totalDiscountAmountSC * -1)) + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Signature: _______________") + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;

                }
                else if (disctype == "REGULAR")
                {
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("REGULAR DISCOUNT") + Environment.NewLine;
                    details += HelperFunction.PrintLeftText("Remarks: " + discremarks) + Environment.NewLine + Environment.NewLine;
                    details += HelperFunction.PrintLeftRigthText("Discount Amount: ", HelperFunction.convertToNumericFormat(totalDiscountAmountSC * -1)) + Environment.NewLine;
                    details += HelperFunction.createDottedLine() + Environment.NewLine;
                }

                lessvat = Math.Round((vatableWithSCDiscount / 1.12) * 0.12, 2);
                netofvat = Math.Round((vatableWithSCDiscount / 1.12), 2);
                lessscdisc = Math.Round(netofvat * Convert.ToDouble(discpercentageamount / 100), 2); //must change the percentage
                netofscdisc = Math.Round(netofvat - lessscdisc, 2);
                addvat = Math.Round(netofscdisc * .12, 2);
                totaltotal = Math.Round(netofscdisc + addvat, 2);

                details += HelperFunction.PrintLeftRigthText("Less VAT:", HelperFunction.convertToNumericFormat(lessvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Net of VAT:", HelperFunction.convertToNumericFormat(netofvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Less Discount:", HelperFunction.convertToNumericFormat(lessscdisc * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Net Discount:", HelperFunction.convertToNumericFormat(netofscdisc * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Add VAT:", HelperFunction.convertToNumericFormat(addvat * -1)) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText("Total:", HelperFunction.convertToNumericFormat(totaltotal * -1)) + Environment.NewLine;

                details += HelperFunction.createDottedLine() + Environment.NewLine;

                vatablesale = vatableWithNoSCDiscount / 1.12;

                totalvatableSales = Math.Round(netofscdisc + vatablesale, 2);
                totalVatInputSale = Math.Round(totalvatableSales * 0.12, 2);
                totalVatExemptSale = Math.Round((vatExWithSCDiscount + vatExWithNoSCDiscount), 2);
            }
            else
            {
                vatablesale = _totalvatablesales / 1.12;
                totalvatableSales = Math.Round(vatablesale, 2);
                totalVatInputSale = Math.Round(totalvatableSales * 0.12, 2);
                totalVatExemptSale = Math.Round(_totalvatexsales, 2);
            }

            details += Environment.NewLine;


            details += HelperFunction.PrintLeftRigthText("VATable Sales", HelperFunction.convertToNumericFormat(totalvatableSales * -1)) + Environment.NewLine;// vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", HelperFunction.convertToNumericFormat(totalVatInputSale * -1)) + Environment.NewLine;// vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", HelperFunction.convertToNumericFormat(totalVatExemptSale * -1)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine;

            //----------------------------------------------------------------------------------------------------
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch,"");

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTERRETURN.txt");
            //string str1 = Classes.Utilities.readFile(path); 
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);


            details += HelperFunction.LastPagePaper() + Environment.NewLine;

            if (!Directory.Exists(filepathConso))
            {
                Directory.CreateDirectory(filepathConso);
            }
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + ordercode + ".txt";
            //conso
            string filetoprintConso = filepathConso + txtorder;
            StreamWriter writerConso = new StreamWriter(filepathConso + txtorder);
            writerConso.Write(details);
            writerConso.Close();
            //per folder per date
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        //IS USED void selected item katong mo right click ka
        public void printVoidSales(string transcode, string ordercode, string vatablesale, string vatexemptsale, string vat, DataGridView gridview, string qtysold, string sellingprice, string totalamount, string desc)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\VoidSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += Classes.ReceiptSetup.doTitle("VOID TRANSACTION");

            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;

            string a = sellingprice +  " @ " + totalamount;
            string b = totalamount;

            details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText(desc, b) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VOID") + Environment.NewLine;

            details += Environment.NewLine;

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CASH");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a2 = totalChange.Text;

            //details += HelperFunction.PrintRightToMiddle(totalLabel.Text, total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("O.R No.", orderr) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Login.Fullname) + Environment.NewLine;

            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch);
            details += HelperFunction.LastPagePaper() + Environment.NewLine;

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //StreamWriter writer = new StreamWriter(filepath + @"\rea.txt");
            txtorder = "\\" + ordercode + ".txt";
            //txtorder = "\\10009.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }


        //IS USED VOID ALL TRANSACTION NA BUTTON
        public void printVoidAllSales(string returntranscode, string transcode, string ordercode, string vatablesale, string vatexemptsale, string vat, DataGridView gridview)
        {

            String details = "";
            //string filepath = "C:\\POSTransaction\\ReturnedSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";

            string filepathConso = "C:\\POSTransaction\\ReturnedSalesConso\\";
            string filepath = "C:\\POSTransaction\\ReturnedSales\\" + DateTime.Now.ToString("yyyyMMdd") + "\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doTitle("RETURNED ALL TRANSACTION");
            details += HelperFunction.PrintLeftText("Return Transaction No.: " + returntranscode);
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno, "Ref");
            details += HelperFunction.createDottedLine() + Environment.NewLine;

        
            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                
                string a = gridview.Rows[i].Cells["QtySold"].Value + " @ " + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = gridview.Rows[i].Cells["TotalAmount"].Value.ToString();

                details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Description"].Value.ToString(), b) + Environment.NewLine;
               
            }
            
            details += Environment.NewLine;

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CASH");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a2 = totalChange.Text;
            
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------


            details += HelperFunction.PrintLeftRigthText("VATable Sales", "-" + vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Amount", "-"+ vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", "-"+ vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED SALES", "0.00") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            
            string pathfile = System.IO.File.ReadAllText(Application.StartupPath + "\\FOOTER.txt");
            details += HelperFunction.PrintCenterText(pathfile) + Environment.NewLine;
            //details += Classes.ReceiptSetup.doFooter(Login.assignedBranch, "");
            details += HelperFunction.LastPagePaper() + Environment.NewLine;

            if(!Directory.Exists(filepathConso))
            {
                Directory.CreateDirectory(filepathConso);
            }
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + ordercode + ".txt";
            //conso
            string filetoprintConso = filepathConso + txtorder;
            StreamWriter writerConso = new StreamWriter(filepathConso + txtorder);
            writerConso.Write(details);
            writerConso.Close();
            //per folder per date
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }
        //IS USED PRINT AUTO FULL TRANSACTION
        public void printXReadReportFullTransactionSalesDevEx(string totalvat, string totalamount, GridView gridview, string date1, string date2, string brcode)
        {

            int beginOR = 0;
            beginOR = Database.getBeginningID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            int lastOR = 0;
            lastOR = Database.getLastID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string cashiername = "-----------------";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            string petsa = DateTime.Now.ToShortDateString();
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT SUMMARY (Z)") + Environment.NewLine;
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("V896 CASHIER : " + cashiername) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            string oras = DateTime.Now.ToShortTimeString();

            string strLabel = "";
            double amount = 0.0, newtotalamount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string vattype = gridview.GetRowCellValue(i, "isVat").ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()) / 1.12, 2);
                    //vatableamount = amount / 1.12;
                    strLabel = "Vatable Sale";
                    newtotalamount += amount;
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()), 2);
                    strLabel = "Vat Exempt Sale";
                    newtotalamount += amount;
                }

                details += HelperFunction.PrintLeftRigthText(strLabel, HelperFunction.convertToNumericFormat(amount)) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", HelperFunction.convertToNumericFormat(newtotalamount)) + Environment.NewLine;
            double totalvatnew = Convert.ToDouble(totalvat) / 1.12;
            string totalvatnew1 = HelperFunction.convertToNumericFormat(totalvatnew);

            double salestotals = 0.0;
            salestotals = newtotalamount + Convert.ToDouble(totalvatnew1);

            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            string totalcashamount1 = HelperFunction.convertToNumericFormat(salestotals);
            details += HelperFunction.PrintLeftRigthText("GRAND TOTAL", totalcashamount1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
           // details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("B OR.#:" + beginOR.ToString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("L OR.#:" + lastOR.ToString()) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();
            
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            string filename = "FullTransactionSales_" + date1.Replace("/", "-") + ".txt";
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + filename;
            StreamWriter writer = new StreamWriter(filepath + filename);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }
        //IS USED PRINT AUTO FULL TRANSACTION
        public void printXReadReportFullTransactionSalesDevEx(string totalvat, string totalamount, GridView gridview, string date1, string date2, string brcode,string posname)
        {

            int beginOR = 0;
            beginOR = Database.getBeginningID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "' and MachineUsed='" + posname +"'", "ReferenceNo");
            int lastOR = 0;
            lastOR = Database.getLastID("BatchSalesSummary", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "' and MachineUsed='" + posname + "'", "ReferenceNo");
            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string cashiername = "-----------------";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode, posname);
            string petsa = DateTime.Now.ToShortDateString();
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT SUMMARY (Z)") + Environment.NewLine;
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("V896 CASHIER : " + cashiername) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            string oras = DateTime.Now.ToShortTimeString();

            string strLabel = "";
            double amount = 0.0, newtotalamount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string vattype = gridview.GetRowCellValue(i, "isVat").ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()) / 1.12, 2);
                    //vatableamount = amount / 1.12;
                    strLabel = "Vatable Sale";
                    newtotalamount += amount;
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()), 2);
                    strLabel = "Vat Exempt Sale";
                    newtotalamount += amount;
                }

                details += HelperFunction.PrintLeftRigthText(strLabel, HelperFunction.convertToNumericFormat(amount)) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", HelperFunction.convertToNumericFormat(newtotalamount)) + Environment.NewLine;
            double totalvatnew = Convert.ToDouble(totalvat) / 1.12;
            string totalvatnew1 = HelperFunction.convertToNumericFormat(totalvatnew);

            double salestotals = 0.0;
            salestotals = newtotalamount + Convert.ToDouble(totalvatnew1);

            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvatnew1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            string totalcashamount1 = HelperFunction.numericFormat(salestotals);
            details += HelperFunction.PrintLeftRigthText("GRAND TOTAL", totalcashamount1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
            //details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("B OR.#:" + beginOR.ToString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("L OR.#:" + lastOR.ToString()) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            string filename = "FullTransactionSales_" + date1.Replace("/", "-") + ".txt";
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + filename;
            StreamWriter writer = new StreamWriter(filepath + filename);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }

        public void printXReadReportFullTransactionSalesDevExZReset(string totalvat, string totalamount, GridView gridview, string date1, string date2, string brcode)
        {

            int beginOR = 0;
            beginOR = Database.getBeginningID("BatchSalesSummary2", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            int lastOR = 0;
            lastOR = Database.getLastID("BatchSalesSummary2", "CAST(Transdate as date)='" + date1 + "' AND BranchCode='" + brcode + "'", "ReferenceNo");
            //beginOR = Database.getSingleQuery("SELECT * FROM BatchSalesDetails")
            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            string cashiername = "-----------------";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            //details += HelperFunction.PrintLeftText("X-Read Report ") + date1 + " -> " + date2 + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT SUMMARY (Z)") + Environment.NewLine;
            //details += HelperFunction.PrintLeftText(petsa) + Environment.NewLine;
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("V896 CASHIER : " + cashiername) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("[ PAYABLE TO BIR ]") + Environment.NewLine;
            //details += HelperFunction.createEqualLine() + Environment.NewLine;

            //string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            //string trans = "Trans #: " + transcode;
            //string orderr = "Order #: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
            //string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();

            string strLabel = "";
            double amount = 0.0, newtotalamount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string vattype = gridview.GetRowCellValue(i, "isVat").ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()) / 1.12, 2);
                    //vatableamount = amount / 1.12;
                    strLabel = "Vatable Sale";
                    newtotalamount += amount;
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString()), 2);
                    strLabel = "Vat Exempt Sale";
                    newtotalamount += amount;
                }

                details += HelperFunction.PrintLeftRigthText(strLabel, amount.ToString()) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            //   details += HelperFunction.PrinttoRight("1000.00");

            double totalcashamount = 0.0;
            //details += HelperFunction.PrintLeftRigthText("TOTAL", totalamount) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL", HelperFunction.numericFormat(newtotalamount)) + Environment.NewLine;
            //  details += HelperFunction.createDottedLine() + Environment.NewLine;
            double totalvatnew = Convert.ToDouble(totalvat) / 1.12;
            string totalvatnew1 = HelperFunction.numericFormat(totalvatnew);

            double salestotals = 0.0;
            salestotals = newtotalamount + Convert.ToDouble(totalvatnew1);

            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvatnew1) + Environment.NewLine;
            //   details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvatnew1) + Environment.NewLine;
            //   details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            // string totalcashamount1 = HelperFunction.numericFormat(totalcashamount);
            string totalcashamount1 = HelperFunction.numericFormat(salestotals);
            details += HelperFunction.PrintLeftRigthText("v896", totalcashamount1) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
           // details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("B OR.#:" + beginOR.ToString()) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("L OR.#:" + lastOR.ToString()) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();
            //----------------------------------------------------------------------------------------------------
            //    If (Not System.IO.Directory.Exists(salefilelocation)) Then
            //    System.IO.Directory.CreateDirectory(salefilelocation)
            //End If

            //If System.IO.File.Exists(salefilelocation & "\" & batchcode & ".txt") = True Then
            //    System.IO.File.Delete(salefilelocation & "\" & batchcode & ".txt")
            //End If

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            string filename = "FullTransactionSales_" + date1.Replace("/", "-") + ".txt";
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + filename;
            StreamWriter writer = new StreamWriter(filepath + filename);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }
        //IS USED PRINT GROUP CATEGORY
        public void printXReadReportGroupSalesDevEx(string totalkilos, string totalamount, GridView gridview, string date1, string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\GroupSales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            details += Classes.ReceiptSetup.doTitle(POS.POSXreadReport.reportTitle + " - " + "GROUP CATEGORY SALES");
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //string cashiername = (isPerCashier == true) ? "positive" : "negative";

            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            details += Classes.ReceiptSetup.doTitle("GROUP CATEGORY SALES");
            details += HelperFunction.PrintLeftText(date1) + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("Report Type: ") + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            //  string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //details += HelperFunction.PrintLeftText(gridview.GetRowCellValue(i, "Description").ToString()) + Environment.NewLine;
                details += HelperFunction.PrintLeftText(gridview.GetRowCellValue(i, "Description").ToString()) + Environment.NewLine;
                string a = "  - " + gridview.GetRowCellValue(i, "TotalKilos").ToString() + " ";
                string b = " " + gridview.GetRowCellValue(i, "TotalAmount").ToString();
                double b1 = Convert.ToDouble(gridview.GetRowCellValue(i, "TotalAmount").ToString());
                details += HelperFunction.PrintLeftRigthText(a, HelperFunction.convertToNumericFormat(b1)) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //details += HelperFunction.PrinttoRight("1000.00");
            Label totalLabel = new Label();
            totalLabel.Text = HelperFunction.PrintLeftText("TOTAL");
            totalLabel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            details += HelperFunction.PrintLeftText(totalLabel.Text) + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Total Qty: "+totalkilos) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Total Amount: " + HelperFunction.convertToNumericFormat(Convert.ToDouble(totalamount))) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void printFinancialReport(string transcode,string ordercode,string total,string totalvoid,string vatablesale,string vatexemptsale,string vat,string cash,string change,DataGridView gridview)
        {
          
            String details = "";
            string filepath = "C:\\POSTransaction\\FinancialReport\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
           
            //string terminalno = "122";
            details += HelperFunction.PrintCenterText("YOUR TRADENAME HERE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("YOUR NAME HERE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("YOUR VAT REGISTERED") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintCenterText("TIN NO: 000-000-000-001") + Environment.NewLine + Environment.NewLine;
           
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;
            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";

            details += HelperFunction.PrintLeftText(dt.ToString(format)) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASHIER : ", Login.Fullname+" Terminal No#: 000") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FINANCIAL REPORT (Z)") + Environment.NewLine;
            details += HelperFunction.PrintLeftText(petsa) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftText("V896 CASHIER : "+Login.Fullname) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTALS [ Cashier-Reading ]") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("GRAND TOTAL ", total) + Environment.NewLine; //total sales
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASH :", total) + Environment.NewLine; //total sales
            details += HelperFunction.PrintLeftRigthText("CREDIT :", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("RETURN :", totalvoid) + Environment.NewLine; //total void
            details += HelperFunction.PrintCenterText("-----------------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("GROSS SALE ", total) + Environment.NewLine; //total sales
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASHIER'S AUDIT") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("No. of Item Sold: ", total) + Environment.NewLine; //numitemsold
            details += HelperFunction.PrintLeftRigthText("Beginning Invoice: ", total) + Environment.NewLine; //beginvoice
            details += HelperFunction.PrintLeftRigthText("Last OR Number: ", total) + Environment.NewLine; //lastornum
            details += HelperFunction.PrintLeftRigthText("Transaction Count: ", total) + Environment.NewLine; //numtranscunt
            details += HelperFunction.PrintLeftRigthText("Average Per Transaction: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Beg Transaction #: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Last Transaction #: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Refunds: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Refunds: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Cancelled Item: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Cancelled: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Tot ServiceFee: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Deleted/Voids: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Deleted/Voids: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of Discounts: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Discounts: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("No. of 12% VAT: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total 12% VAT: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Total Add On Amt: ", total) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PAYABLE TO BIR") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Vatable Amt: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Net Sales of Vat: ", total) + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Vat Amt: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("Less: Input Vat: ", total) + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ACC. PREV: ", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CURRENT: ", total) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ACC. TOTAL: ", total) + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H  C O U N T") + Environment.NewLine;
            details += HelperFunction.createAsteriskLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Terminal #: 000") + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASHIER: "+Login.Fullname) + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("[ QTY ] X [ DENOMIN ] = [ AMT ]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [    1000 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     500 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     200 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     100 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [      50 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [      20 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [      10 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [       5 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [       1 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("    0   X [     .25 ] = [ 0.00]") + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "---------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL     P ", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintRightToLeft(" ", "=========") + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createEqualLine() + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Certified Correct By : " + Login.Fullname) + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("C A S H I E R") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SUPERVISOR/Manager") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Signature Over Printed Name") + Environment.NewLine;
           // details += HelperFunction.PrintCenterText("v896 www.tanaytechnologies.com") + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("L OR.#:"+"last or") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("TRAN.#:" + "last or") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.createAsteriskLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Have a nice day!") + Environment.NewLine;

            details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            txtorder = "\\" + PointOfSale.refno + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();
            printTextFile(filetoprint);
        }

        public void ReprintReceipt(string transcode, string ordercode, string total, string vatablesale, string vatexemptsale, string vat, string cash, string change, DataGridView gridview)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            string terminalno = "122";
       
            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            details += Classes.ReceiptSetup.doHeaderDetails(ordercode, transcode, terminalno);
            details += Classes.ReceiptSetup.doTitle("R E P R I N T");
            details += Classes.ReceiptSetup.doTitle("SALES INVOICE");

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                string addV = "";
                if (Convert.ToBoolean(gridview.Rows[i].Cells["isVat"].Value.ToString()) == true)
                {
                    addV = "V";
                }
                else
                {
                    addV = "";
                }
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["QtySold"].Value + " @ " + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["TotalAmount"].Value + addV;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
            }
            details += HelperFunction.PrinttoRight("--------") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TOTAL.DUE", total) + Environment.NewLine;
            details += HelperFunction.PrinttoRight("========") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TENDERED:", cash) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CHANGE  :", change) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("VAT Sales", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT (12%)", vat) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("ZERO RATED", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT-EXEMPT SALES", vatexemptsale) + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += Classes.ReceiptSetup.doFooter();
            details += HelperFunction.LastPagePaper();
            //if (!Directory.Exists(filepath))
            //{
            //    Directory.CreateDirectory(filepath);
            //}
            txtorder = "\\" + ordercode + ".txt";
            //string filetoprint = filepath + txtorder;
            //StreamWriter writer = new StreamWriter(filepath + txtorder);
            //writer.Write(details);
            //writer.Close();
            string filetoprint = filepath + txtorder;
            printTextFile(filetoprint);
        }
        
        
        

        public void printRefundSales(string transcode, string ordercode, string vatablesale, string vatexemptsale, string vat, DataGridView gridview)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\RefundSales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Branch.getBranchAddress(Login.assignedBranch)) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PRMT#: xxxxxxxxx-xxxxxxx-xxxxxxx") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("MIN#: xxxxxxxxx   S/N: 000000000") + Environment.NewLine + Environment.NewLine;

            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                //  string qty = gridview.Rows[i].Cells["Qty"].Value.ToString();
                string a = gridview.Rows[i].Cells["QtySold"].Value + " Kg" + " x" + gridview.Rows[i].Cells["SellingPrice"].Value;
                string b = gridview.Rows[i].Cells["TotalAmount"].Value.ToString();

                details += HelperFunction.PrintLeftText(a) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(gridview.Rows[i].Cells["Description"].Value.ToString(), b) + Environment.NewLine;
                details += HelperFunction.PrintCenterText("REFUND") + Environment.NewLine;
            }

            details += Environment.NewLine;

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CASH");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a2 = totalChange.Text;

            //details += HelperFunction.PrintRightToMiddle(totalLabel.Text, total) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------
            //details += HelperFunction.PrintLeftRigthText("VAT Exempt Sales", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VATable Sale", vatablesale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Exempt Sale", vatexemptsale) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT", vat) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText("O.R No.", orderr) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Login.Fullname) + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Customer Name: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Address: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Business Style: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN: ") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Signature:_________________ ") + Environment.NewLine;

            details += HelperFunction.PrintCenterText("SPIRE IT SOLUTIONS INC.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("3720 Woodland Heights Banawa Cebu City") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("D.Issue: 01/01/2016 Valid 12/12/2016") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("ACCR#: XXX-XXXXXXXX-XXXXXXXXX") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS IS NOT AN OFFICIAL RECEIPT") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Thank You and Please Come Again!");
            details += HelperFunction.LastPagePaper();

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //StreamWriter writer = new StreamWriter(filepath + @"\rea.txt");
            txtorder = "\\" + ordercode + ".txt";
            //txtorder = "\\10009.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void printReceiptDailySales(string transcode, string ordercode, string total, string cash, string change, DataGridView gridview)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\DailySales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Branch.getBranchAddress(Login.assignedBranch)) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PRMT#: xxxxxxxxx-xxxxxxx-xxxxxxx") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("MIN#: xxxxxxxxx   S/N: 000000000") + Environment.NewLine + Environment.NewLine;

            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            
            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            string trans = "Trans #: " + transcode;
            string orderr = "Order #: " + ordercode;
            details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
           
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Particulars"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["Qty"].Value + " Kg" + " x" + gridview.Rows[i].Cells["UnitPrice"].Value;
                string b = " " + gridview.Rows[i].Cells["Amount"].Value;
                details += HelperFunction.PrintLeftRigthText(a, b) + Environment.NewLine;
            }

            details += Environment.NewLine;
           
            Label totalLabel = new Label();
            totalLabel.Text = HelperFunction.PrintLeftText("TOTAL");
            totalLabel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            Label totalChange = new Label();
            totalChange.Text = HelperFunction.PrintLeftText("CHANGE");
            totalChange.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            string a1 = totalLabel.Text;
            string a2 = totalChange.Text;

            details += HelperFunction.PrintRightToMiddle(totalLabel.Text, total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("CASH", cash) + Environment.NewLine;
            details += HelperFunction.PrintRightToMiddle(a2, change) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;

            //----------------------------------------------------------------------------------------------------
            details += HelperFunction.PrintLeftRigthText("VAT Exempt Sales", total) + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VATable Sales", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT Zero-Rated Sales", "0.00") + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("VAT", "0.00") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftText("Customer: Eulz Avancena") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Cashier: Paulo Pascual") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintLeftText("SOLD TO : ___________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("ADDRESS : ___________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN #   : ___________________") + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("SPIRE IT SOLUTIONS INC.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("3720 Woodland Heights Banawa Cebu City") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("D.Issue: 01/01/2016 Valid 12/12/2016") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("ACCR#: XXX-XXXXXXXX-XXXXXXXXX") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS IS NOT AN OFFICIAL RECEIPT") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Thank You and Please Come Again!");
            details += HelperFunction.LastPagePaper();
            //----------------------------------------------------------------------------------------------------

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            //StreamWriter writer = new StreamWriter(filepath + @"\rea.txt");
            txtorder = "\\" + PointOfSale.refno + ".txt";
            //txtorder = "\\10009.txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void printXReadReportGroupSales(string totalkilos, string totalamount, DataGridView gridview,string date1,string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\GroupSales";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            
            details += Classes.ReceiptSetup.doHeader(POSDevEx.POSXReadReportDevEx.brcode);
            details += Classes.ReceiptSetup.doTitle(POS.POSXreadReport.reportTitle + " - " + "GROUP CATEGORY SALES");
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
          
           string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                string a = "  - " + gridview.Rows[i].Cells["TotalKilos"].Value + " Kg";    
                string b = " " + gridview.Rows[i].Cells["TotalAmount"].Value;
                double b1 = Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value);
                details += HelperFunction.PrintLeftRigthText(a, b1.ToString()) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //details += HelperFunction.PrinttoRight("1000.00");
            Label totalLabel = new Label();
            totalLabel.Text = HelperFunction.PrintLeftText("TOTAL");
            totalLabel.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
         
            details += HelperFunction.PrintLeftText(totalLabel.Text) + Environment.NewLine;

            details += HelperFunction.PrintLeftRigthText(totalkilos, totalamount) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        

        public void printXReadReportFullTransactionSales(string totalvat, string totalamount, DataGridView gridview, string date1, string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\FullTransaction\\";
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";

            details += Classes.ReceiptSetup.doHeader(Login.assignedBranch);
            //details += HelperFunction.PrintLeftText("X-Read Report ") + date1 + " -> " + date2 + Environment.NewLine;

            details += Classes.ReceiptSetup.doTitle(POS.POSXreadReport.reportTitle + " - " + "AUTO FULL TRANSACTION");

            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            //string trans = "Trans #: " + transcode;
            //string orderr = "Order #: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
            //string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();

            string strLabel = "";
            double amount = 0.0;
            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {  
                string vattype = gridview.Rows[i].Cells["isVat"].Value.ToString();
                if (Convert.ToBoolean(vattype) == true)
                {
                    amount = Math.Round(Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                    strLabel = "Vatable Sale";
                }
                else
                {
                    amount = Math.Round(Convert.ToDouble(gridview.Rows[i].Cells["TotalAmount"].Value.ToString()),2);
                    strLabel = "Vat Exempt Sale";
                }
                details += HelperFunction.PrintLeftRigthText(strLabel, amount.ToString()) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //   details += HelperFunction.PrinttoRight("1000.00");

            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", totalamount) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            details += HelperFunction.PrintLeftRigthText("805", totalcashamount.ToString()) + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------
            //    If (Not System.IO.Directory.Exists(salefilelocation)) Then
            //    System.IO.Directory.CreateDirectory(salefilelocation)
            //End If

            //If System.IO.File.Exists(salefilelocation & "\" & batchcode & ".txt") = True Then
            //    System.IO.File.Delete(salefilelocation & "\" & batchcode & ".txt")
            //End If

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        

        public void printXReadReportRefundSales(string totalvat, string totalamount, DataGridView gridview, string date1, string date2)
        {

            String details = "";
            string filepath = "C:\\POSTransaction\\Refund\\";//" + Login.assignedBranch + "\\" + Login.Fullname;
            details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += HelperFunction.PrintCenterText("SPIRE BUSINESS SOLUTIONS") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(Branch.getBranchAddress(Login.assignedBranch)) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("VAT REG TIN: 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PRMT#: xxxxxxxxx-xxxxxxx-xxxxxxx") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("MIN#: xxxxxxxxx   S/N: 000000000") + Environment.NewLine + Environment.NewLine;
            details += HelperFunction.PrintLeftText("X-Read Report ") + date1 + " -> " + date2 + Environment.NewLine;
           // details += HelperFunction.PrintLeftText("Auto Full Transaction") + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();

            //details += HelperFunction.PrintLeftRigthText(petsa, oras) + Environment.NewLine;
            //string trans = "Trans #: " + transcode;
            //string orderr = "Order #: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(trans, orderr) + Environment.NewLine + Environment.NewLine;
            //string mark = gridview.Rows[gridview.CurrentCellAddress.Y].Cells["Description"].Value.ToString();

            for (int i = 0; i <= gridview.RowCount - 1; i++)
            {
                //details += HelperFunction.PrintLeftText(gridview.Rows[i].Cells["Description"].Value.ToString()) + Environment.NewLine;
                // string vatable = "  - " + gridview.Rows[i].Cells["TotalKilos"].Value + " Kg";
                string vatexempt = " " + gridview.Rows[i].Cells["TotalAmount"].Value;
                string vardesc = gridview.Rows[i].Cells["TotalAmount"].Value.ToString();
                string vattype = gridview.Rows[i].Cells["isVat"].Value.ToString();
                //details += HelperFunction.PrintLeftRigthText("Vatable Sale", vatable) + Environment.NewLine;
                details += HelperFunction.PrintLeftRigthText(vattype, vatexempt) + Environment.NewLine;
            }

            details += Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            //   details += HelperFunction.PrinttoRight("1000.00");

            double totalcashamount = 0.0;
            details += HelperFunction.PrintLeftRigthText("TOTAL", totalamount) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("12% VAT", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftRigthText("TAX TOTAL", totalvat) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SALES TOTAL") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("CASH") + Environment.NewLine;
            totalcashamount = Convert.ToDouble(totalvat) + Convert.ToDouble(totalamount);
            details += HelperFunction.PrintLeftRigthText("805", totalcashamount.ToString()) + Environment.NewLine;
            //----------------------------------------------------------------------------------------------------
            //    If (Not System.IO.Directory.Exists(salefilelocation)) Then
            //    System.IO.Directory.CreateDirectory(salefilelocation)
            //End If

            //If System.IO.File.Exists(salefilelocation & "\" & batchcode & ".txt") = True Then
            //    System.IO.File.Delete(salefilelocation & "\" & batchcode & ".txt")
            //End If

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            DateTime dt = DateTime.Now;
            txtorder = "\\" + String.Format("{0:MM-dd-yyyy}", dt) + ".txt";
            string filetoprint = filepath + txtorder;
            StreamWriter writer = new StreamWriter(filepath + txtorder);
            writer.Write(details);
            writer.Close();

            printTextFile(filetoprint);
        }

        public void rePrintReceipt(string orderno)
        {
            string filepath = "C:\\POSTransaction\\Refund\\" + Login.assignedBranch + "\\" + Login.Fullname;
        }

        public void printTextFile(string location)
        {
            bool isprint = Database.checkifExist("Select isEnablePrinting FROM POSType where isEnablePrinting=1");
            if (location != "")
            {
                if(isprint)
                {
                    //string command = "/C print /d:LPT1: '"+location+"'\\mafi.txt";
                    string command = "/C print /d:LPT1: " + location + " ";
                    ProcessStartInfo apps = new System.Diagnostics.ProcessStartInfo("cmd.exe", command);
                    // Process myprocesses = new Process();
                    apps.WindowStyle = ProcessWindowStyle.Hidden;
                    Process myprocesses = Process.Start(apps);
                    myprocesses.WaitForExit();
                    myprocesses.Close();
                }
               
            }
        }

        public void printTextFile2(string location)
        {
            bool isprint = true;// Database.checkifExist("Select isEnablePrinting FROM POSType where isEnablePrinting=1");
            if (location != "")
            {
                if (isprint)
                {
                    //string command = "/C print /d:LPT1: '"+location+"'\\mafi.txt";
                    string command = "/C print /d:LPT2: " + location + " ";
                    ProcessStartInfo apps = new System.Diagnostics.ProcessStartInfo("cmd.exe", command);
                    // Process myprocesses = new Process();
                    apps.WindowStyle = ProcessWindowStyle.Hidden;
                    Process myprocesses = Process.Start(apps);
                    myprocesses.WaitForExit();
                    myprocesses.Close();
                }
            }
        }
    }
}
