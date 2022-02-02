using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem
{
    class IDGenerator
    {
        //public static int getOrderNumber()
        //{
        //    int id = Database.getLastID("BatchSalesSummary","BranchCode='"+Login.assignedBranch+"'","ReferenceNo");
        //    if(id!=0)
        //        return ++id;
        //    else
        //        return 1;
        //}
        public static int getIDNumber(string tableName, string returnval, int beginningValue)
        {
            int id = Database.getLastID(tableName, returnval);
            if (id != 0)
                return ++id;
            else
                return beginningValue;
        }
        public static int getIDNumber(string tableName,string condition,string returnval,int beginningValue)
        {
            int id = Database.getLastID(tableName, condition, returnval);
            if (id != 0)
                return ++id;
            else
                return beginningValue;
        }
        public static int getIDNumber(string tableName, string condition, string returnval, int beginningValue,SqlConnection con)
        {
            int id = Database.getLastID(tableName, condition, returnval);
            if (id != 0)
                return ++id;
            else
                return beginningValue;
        }
        //NEW
        //public static int getPOSTransactionNumber()
        //{
        //    int id = Database.getLastID("POSTransaction", "TransactionNo <> ''", "TransactionNo");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;

        //} //NEW
        //public static int getPOSReturnTransactionNumber()
        //{
        //    int id = Database.getLastID("POSReturnTransaction", "BranchCode='" + Login.assignedBranch + "'", "ReturnTransactionNo");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;

        //}
        //public static int getPOSTransactionID()
        //{
        //    int id = Database.getLastID("POSTransaction", "BranchCode='" + Login.assignedBranch + "'", "TransactionNo");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}
        //public static int getPOSReprintCounter()
        //{
        //    int id = Database.getLastID("POSTransaction", "BranchCode='" + Login.assignedBranch + "' and ReprintCtr is not null AND MachineUsed='"+Environment.MachineName+"' and Type='REPRINT OR'", "ReprintCtr");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}
        //public static int getOrderNumberRestaurant()
        //{
        //    int id = Database.getLastID("RestaurantOrderSummary", "BranchCode='" + Login.assignedBranch + "'", "ReferenceNo",Database.getCustomizeConnection());
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10000;
        //}
       
        //public static int getReferenceNumberRestaurant()
        //{
        //    int id = Database.getLastID("OrderSummary", "OrderType<>''", "ReferenceNo");//, Database.getCustomizeConnection());
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10000;
        //}
       
        //public static int getServiceID() //
        //{
        //    int id = Database.getLastID("Services", "SRVC_ID");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10000;
        //}

        //public static int getNonTradeOrderNumber()
        //{
        //    int id = Database.getLastID("NonTradeOrder", "OrderNo");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10000;
        //}
        //public static int getExpenseReferenceNumber()
        //{
        //    int id = Database.getLastID("ExpenseMaster", "ReferenceNumber");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}

        //public static int getSupplierNumber()
        //{
        //    int id = Database.getLastID("Supplier", "SupplierID");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}

        //public static int getProductCategoryID() //
        //{
        //    int id = Database.getLastID("ProductCategory", "ProductCategoryID");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10;
        //}

        //public static int getDeliveryNumber()
        //{
        //    int id = Database.getLastID("DeliverySummary", "DeliveryNo"); //SELECT MAX(ReferenceNo)AS maxref FROM ChargeSalesSummary
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10000;
        //}

        //public static int getInvoiceNumber() //
        //{
        //    int id = Database.getLastID("BatchSalesSummary", "Invoice"); 
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10000;
        //}

        //public static int getTransactionCashReferenceNumber()
        //{
        //    int id = Database.getLastID("TransactionCash", "Reference");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}

        //public static int getTicketNumber()
        //{
        //    int id = Database.getLastID("TicketMaster", "TicketNumber");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}

        //public static int getCustCode()
        //{
        //    int id = Database.getLastID("Customers", "CustomerID");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}
        public static int getGuestID()
        {
            int id = Database.getLastID("GuestInfo", "GuestID <> ''","GuestID",Database.getCustomizeConnection());
            if (id != 0)
                return ++id;
            else
                return 10000;
        }
        public static int getReservationID()
        {
            int id = Database.getLastID("Reservation", "ReservationNo <> 0", "ReservationNo", Database.getCustomizeConnection());
            if (id != 0)
                return ++id;
            else
                return 10000;
        }
        public static int getBookingNo()
        {
            int id = Database.getLastID("CheckinGuest", "BookingNo <> 0", "BookingNo", Database.getCustomizeConnection());
            if (id != 0)
                return ++id;
            else
                return 10000;
        }

        //public static int getPurchaseNumber()
        //{
        //    int id = Database.getLastID("PurchaseOrderSummary", "PONumber");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 10000;

        //}

        //public static int getSalesTransactionID() //
        //{
        //    int id = Database.getLastID("SalesTransactionSummary","BranchCode='"+Login.assignedBranch+"'", "AccountCode");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}

        //public static int getLastTicketNumber()
        //{
        //    int id = Database.getLastID("TicketMaster", "TicketNumber");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;

        //}

        //public static int getLastInventoryCostID()
        //{
        //    int id = Database.getLastID("InventoryCost", "SequenceNumber");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;

        //}
        //public static int getInventoryItemCode()
        //{
        //    int id = Database.getLastID("GenInventoryItems", "ItemCode");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;

        //}

        //public static int getLastReferenceNumber()
        //{
        //    int id = Database.getLastID("ReferenceNumber", "ReferenceNumber");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;

        //}

        //public static int getLastBatchCode() //
        //{
        //    int id = Database.getLastID("Inventory","BatchCode");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;

        //}

        //public static int getSequenceNumber() //
        //{
        //    int id = Database.getLastID("TempInventory", "SequenceNumber");
        //    if (id != 0)
        //        return ++id;
        //    else
        //        return 1;
        //}

        public static string getIDNumberSP(string spName, string returnval)
        {
            string num = "";
            SqlConnection con = Database.getConnection();
            con.Open();
            try
            {
                string query = spName;
                SqlCommand com = new SqlCommand(query, con);
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = query;
                SqlDataReader reader = com.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        num = reader[returnval].ToString();
                    }
                }
            }
            catch(SqlException sqlex)
            {
                sqlex.Message.ToString();
            }
            finally
            {
                con.Close();
            }
            return num;
        }
        //public static String getPOSTransactionNoSP() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetPOSTransactionNo";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["TransactionNo"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}
        //public static String getShipmentNoSP() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetShipmentNo";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["ShipmentNo"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}

        public static String getIDbySP(string spname,string coloumn,SqlConnection con)
        {
            string num = "";
            con.Open();
            string query = spname;
            SqlCommand com = new SqlCommand(query, con);
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            SqlDataReader reader = com.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    num = reader[coloumn].ToString();
                }
            }
            con.Close();
            return num;
            //  con.Close();
        }

        //public static String getReferenceNumber()//
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetReferenceNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["ReferenceNumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}

        //public static String getBatchCode() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetBatchCode";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["BatchNumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}

        //public static String getTransferedNumber() //
        //{
        //    string str = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetTransNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            str = reader["BatchNumber"].ToString();
        //        }
        //    }
        //    return str;
        //    //con.Close();
        //}

        //public static String getPONumber() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetPurchaseOrderNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["PONumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}
        //public static String getExpenseNumber() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetExpenseNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["expenseno"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}

        //public static String getInventoryINNumber() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetInventoryINNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["InventoryID"].ToString();
        //        }
        //    }else
        //    {
        //        num = "1";
        //    }
        //    return num;
        //    //  con.Close();
        //}

        //public static String getDeliveryNumberSP() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetDeliveryNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["DeliveryNumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}

        //public static String getConversionNumber() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetConversionNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["conversionnumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}

        //public static String getTicketNumberSP() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetTicketNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["TicketNumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}

        //public static String getVoucherNumberSP() //
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetVoucherNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["TicketNumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}
        //public static String getLiquidationNumberSP()//
        //{
        //    string num = "";
        //    SqlConnection con = Database.getConnection();
        //    con.Open();
        //    string query = "sp_GetLiquidationNumber";
        //    SqlCommand com = new SqlCommand(query, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = query;
        //    SqlDataReader reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            num = reader["TicketNumber"].ToString();
        //        }
        //    }
        //    return num;
        //    //  con.Close();
        //}


    }
}
