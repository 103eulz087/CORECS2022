using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesInventorySystem.Classes
{
    public class ReceiptSetup
    {
       
        public static string doHeaderOrig(string branchcode)
        {
            var row = Database.getMultipleQuery("POSInfoDetails", "BranchCode='" + branchcode + "' ", "BusinessName" +
                 ",BusinessAddress   " +
                 ",TINNo   " +
                 ",MachineUsed       " +
                 ",AccreditationNo   " +
                 ",DateIssued        " +
                 ",SerialNo          " +
                 ",RegTransactionNo  " +
                 ",DateApplication   " +
                 ",PermitNumber      " +
                 ",MINNo             " +
                 ",DatePermitStart   " +
                 ",DatePermitEnd     ");

            string tradename = row["BusinessName"].ToString();
            string posname = row["MachineUsed"].ToString();
            string compaddress1 = row["BusinessAddress"].ToString();
            //string compaddress2 = row["Address2"].ToString();
            string comptinno = row["TINNo"].ToString();
            string compminno = row["MINNo"].ToString();
            string compbirpermitno = row["PermitNumber"].ToString();
            string compserialno = row["SerialNo"].ToString();

            String details = "";
            //details = "" + (Char)27 + (Char)112 + (Char)0 + (Char)25 + "";
            details += HelperFunction.PrintCenterText(tradename) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(compaddress1) + Environment.NewLine;
            //details += HelperFunction.PrintCenterText(compaddress2) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("TIN: " + comptinno) + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("MIN #: " + compminno) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("BIR PERMIT #: " + compbirpermitno) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("SERIAL #: " + compserialno) + Environment.NewLine + Environment.NewLine;

            return details;
        }
        public static string doHeader(string branchcode)
        {
            var row = Database.getMultipleQuery("POSInfoDetails", "BranchCode='" + branchcode + "' ", "BusinessName" +
                ",BusinessAddress   " +
                ",TINNo   " +
                ",MachineUsed       " +
                ",AccreditationNo   " +
                ",DateIssued        " +
                ",SerialNo          " +
                ",RegTransactionNo  " +
                ",DateApplication   " +
                ",PermitNumber      " +
                ",MINNo             " +
                ",DatePermitStart   " +
                ",DatePermitEnd     ");

            string tradename = row["BusinessName"].ToString();
            string posname = row["MachineUsed"].ToString();
            string compaddress1 = row["BusinessAddress"].ToString();
            //string compaddress2 = row["Address2"].ToString();
            string comptinno = row["TINNo"].ToString();
            string compminno = row["MINNo"].ToString();
            string compbirpermitno = row["PermitNumber"].ToString();
            string compserialno = row["SerialNo"].ToString();

            String details = "";
            string tinno = "VAT Reg TIN:" + comptinno;
            string sn = "S/N:" + compserialno;
            details += HelperFunction.PrintCenterText(tradename) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(compaddress1) + Environment.NewLine;
            //details += HelperFunction.PrintCenterText(compaddress2) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(tinno) + Environment.NewLine;

            details += HelperFunction.PrintCenterText(sn) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("MIN: " + compminno) + Environment.NewLine + Environment.NewLine;
            return details;
        }
        public static string doHeader(string branchcode, string machinename)
        {
            String details = "";
            try
            {
               
                var row = Database.getMultipleQuery("POSInfoDetails", "BranchCode = '"+ branchcode + "' " +
                //var row = Database.getMultipleQuery("POSInfoDetails", "BranchCode='" + branchcode + "' " +
                    "AND MachineUsed='" + machinename + "' ", "BusinessName" +
                   ",BusinessAddress   " +
                   ",TINNo   " +
                   ",MachineUsed       " +
                   ",AccreditationNo   " +
                   ",DateIssued        " +
                   ",SerialNo          " +
                   ",RegTransactionNo  " +
                   ",DateApplication   " +
                   ",PermitNumber      " +
                   ",MINNo             " +
                   ",DatePermitStart   " +
                   ",DatePermitEnd     ");

                string tradename = row["BusinessName"].ToString();
                string posname = row["MachineUsed"].ToString();
                string compaddress1 = row["BusinessAddress"].ToString();
                //string compaddress2 = row["Address2"].ToString();
                string comptinno = row["TINNo"].ToString();
                string compminno = row["MINNo"].ToString();
                string compbirpermitno = row["PermitNumber"].ToString();
                string compserialno = row["SerialNo"].ToString();
           


                string tinno = "TIN:" + comptinno;
                string sn = "S/N:" + compserialno;
                details += HelperFunction.PrintCenterText(tradename) + Environment.NewLine;
                details += HelperFunction.PrintCenterText(compaddress1) + Environment.NewLine;
                details += HelperFunction.PrintCenterText("VAT REG") + Environment.NewLine;
                details += HelperFunction.PrintCenterText("TIN: " + comptinno) + Environment.NewLine; 
                details += HelperFunction.PrintCenterText(sn) + Environment.NewLine;
                details += HelperFunction.PrintCenterText("MIN: " + compminno) + Environment.NewLine + Environment.NewLine;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            return details;
        }
        public static string doHeaderB2(string branchcode, string machinename)
        {
            String details = "";
            try
            {
               
                var row = Database.getMultipleQuery("POSInfoDetails", $"BranchCode = '{branchcode}' " +
                //var row = Database.getMultipleQuery("POSInfoDetails", "BranchCode='" + branchcode + "' " +
                    "AND MachineUsed='" + machinename + "' ", "BusinessName" +
                   ",BusinessAddress   " +
                   ",TINNo   " +
                   ",MachineUsed       " +
                   ",AccreditationNo   " +
                   ",DateIssued        " +
                   ",SerialNo          " +
                   ",RegTransactionNo  " +
                   ",DateApplication   " +
                   ",PermitNumber      " +
                   ",MINNo             " +
                   ",DatePermitStart   " +
                   ",DatePermitEnd     ");

                string tradename = row["BusinessName"].ToString();
                string posname = row["MachineUsed"].ToString();
                string compaddress1 = row["BusinessAddress"].ToString();
                //string compaddress2 = row["Address2"].ToString();
                string comptinno = row["TINNo"].ToString();
                string compminno = row["MINNo"].ToString();
                string compbirpermitno = row["PermitNumber"].ToString();
                string compserialno = row["SerialNo"].ToString();



                string tinno = "TIN:" + comptinno;
                string sn = "S/N:" + compserialno;
                details += HelperFunction.PrintCenterText(tradename) + Environment.NewLine;
                details += HelperFunction.PrintCenterText(compaddress1) + Environment.NewLine;
                //details += HelperFunction.PrintCenterText(compaddress2) + Environment.NewLine;
                details += HelperFunction.PrintCenterText("TIN: " + comptinno) + Environment.NewLine; 
                details += HelperFunction.PrintCenterText(sn) + Environment.NewLine;
                details += HelperFunction.PrintCenterText("MIN: " + compminno) + Environment.NewLine + Environment.NewLine;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }
            return details;
        }

        public static string doHeaderDetails(string ordercode, string transcode, string terminalno)
        {
            String details = "";
            string cashier = "CASHIER : " + Login.Fullname;
            string custno = "CUST #: " + PointOfSale.custcode;
            details += HelperFunction.PrintLeftRigthText(cashier, custno) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SI No.: " + ordercode) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Tran#: " + transcode) + Environment.NewLine;
         
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;

            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt"; 
            details += HelperFunction.PrintLeftText("Date:" + dt.ToString(format)) + Environment.NewLine + Environment.NewLine; 
            details += HelperFunction.PrintLeftText("NAME : _________________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("ADDRESS : ______________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN: _____________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Business Style : _______________") + Environment.NewLine + Environment.NewLine;
            return details;
        }
        public static string doHeaderDetails(string ordercode, string transcode, string terminalno, string name, string address, string tin, string businesstype)
        {
            String details = "";
            string cashier = "CASHIER : " + Login.Fullname;
            string custno = "CUST #: " + PointOfSale.custcode;
            details += HelperFunction.PrintLeftRigthText(cashier, custno) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SI No.: " + ordercode) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Tran#: " + transcode) + Environment.NewLine;
            //string trans = "TRAN#: " + transcode;
            //string orderr = "SI No: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(orderr, trans) + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;

            if (String.IsNullOrEmpty(name))
            {
                name = "__________________";
            }
            if (String.IsNullOrEmpty(address))
            {
                address = "__________________";
            }
            if (String.IsNullOrEmpty(tin))
            {
                tin = "__________________";
            }
            if (String.IsNullOrEmpty(businesstype))
            {
                businesstype = "__________________";
            }

            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";
            //string fulldate = String.Format("{dd-MMM-yyyy ddd hh:mm:ss tt}", dt);
            details += HelperFunction.PrintLeftText("Date:" + dt.ToString(format)) + Environment.NewLine + Environment.NewLine;
            // details += HelperFunction.PrintLeftText("v896 Terminal#: " + terminalno) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("NAME : " + name) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("ADDRESS : " + address) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN: " + tin) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Business Style : " + businesstype) + Environment.NewLine + Environment.NewLine;
            return details;
        }public static string doHeaderDetailsX(string cashiername,string ordercode, string terminalno, string name, string address, string tin, string businesstype,string dateBuy,string timeBuy)
        {
            String details = "";
            string cashier = "CASHIER : " + cashiername;// Login.Fullname;
            string custno = "CUST #: " + PointOfSale.custcode;
            details += HelperFunction.PrintLeftRigthText(cashier, custno) + Environment.NewLine;
            
            details += HelperFunction.PrintLeftText("SI No.: " + ordercode) + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("Tran#: "+ custno) + Environment.NewLine;
            //string trans = "TRAN#: " + transcode;
            //string orderr = "SI No: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(orderr, trans) + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            //string petsa = DateTime.Now.ToShortDateString();
            //string oras = DateTime.Now.ToShortTimeString();
            string fulldate1 = dateBuy;
            //string fulldate1 = dateBuy + ' ' + timeBuy;

            if (String.IsNullOrEmpty(name))
            {
                name = "__________________";
            }
            if (String.IsNullOrEmpty(address))
            {
                address = "__________________";
            }
            if (String.IsNullOrEmpty(tin))
            {
                tin = "__________________";
            }
            if (String.IsNullOrEmpty(businesstype))
            {
                businesstype = "__________________";
            }

            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";
            //string fulldate = String.Format("{dd-MMM-yyyy ddd hh:mm:ss tt}", dt);
            details += HelperFunction.PrintLeftText("Date:" + Convert.ToDateTime(dateBuy).ToString(format)) + Environment.NewLine + Environment.NewLine;
            // details += HelperFunction.PrintLeftText("v896 Terminal#: " + terminalno) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("NAME : " + name) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("ADDRESS : " + address) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN: " + tin) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Business Style : " + businesstype) + Environment.NewLine + Environment.NewLine;
            return details;
        }
        public static string doHeaderDetails(string ordercode, string transcode, string terminalno, string type)
        {
            String details = "";
            string cashier = "CASHIER : " + Login.Fullname;
            string custno = "CUST #: " + PointOfSale.custcode;
            details += HelperFunction.PrintLeftRigthText(cashier, custno) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SI No.: " + ordercode) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Tran#: " + transcode) + Environment.NewLine;
            //string trans = "TRAN#: " + transcode;
            //string orderr = type+" SI No: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(orderr, trans) + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;

            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";
            //string fulldate = String.Format("{dd-MMM-yyyy ddd hh:mm:ss tt}", dt);
            details += HelperFunction.PrintLeftText("Date:" + dt.ToString(format)) + Environment.NewLine + Environment.NewLine;
            // details += HelperFunction.PrintLeftText("v896 Terminal#: " + terminalno) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("NAME : _________________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("ADDRESS : ______________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("TIN: _____________________") + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Business Style : ______________________") + Environment.NewLine + Environment.NewLine;
            return details;
        }
        public static string doHeaderDetailsOrig(string ordercode, string transcode, string terminalno)
        {
            String details = "";
            string cashier = "CASHIER : " + Login.Fullname;
            string custno = "CUST #: " + PointOfSale.custcode;
            // details += HelperFunction.PrintLeftRigthText(cashier, custno) + Environment.NewLine;
            details += HelperFunction.PrintLeftText(cashier) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("SI No.: " + ordercode) + Environment.NewLine;
            details += HelperFunction.PrintLeftText("Tran#: " + transcode) + Environment.NewLine;
            //string trans = "TRAN#: " + transcode;
            //string orderr = "SI/OR#: " + ordercode;
            //details += HelperFunction.PrintLeftRigthText(orderr, trans) + Environment.NewLine;
            string petsa = DateTime.Now.ToShortDateString();
            string oras = DateTime.Now.ToShortTimeString();
            string fulldate1 = petsa + ' ' + oras;

            DateTime dt = DateTime.Now;
            string format = "dd-MMM-yyyy ddd hh:mm:ss tt";
            //string fulldate = String.Format("{dd-MMM-yyyy ddd hh:mm:ss tt}", dt);
            details += HelperFunction.PrintLeftText("Date:" + dt.ToString(format)) + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("v896 Terminal#: " + terminalno) + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("TIN/ACCT#: --------------------") + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("NAME : ------------------------") + Environment.NewLine;
            //details += HelperFunction.PrintLeftText("ADDRESS : ---------------------") + Environment.NewLine;
            return details;
        }

        public static string doTitle(string titlename)
        {
            String details = "";
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            details += HelperFunction.PrintCenterText(titlename) + Environment.NewLine;
            details += HelperFunction.createDottedLine() + Environment.NewLine;
            return details;
        }

        public static string doFooterOrig()
        {
            String details = "";
            details += HelperFunction.PrintCenterText("Tanay Technologies/E. Tanay") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("TIN : 192760374-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Ojeda St., Dayandang, Naga City") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Acc'd#:065-192760374-000304") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Date Issued : 2007-08-17") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("BIR Final PTU : 2017-10-26") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU Date Issued : 2016-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU Valid Until : 2021-01-01") + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("THANK YOU! Pls. Come Again.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("This serves as your.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("OFFICIAL RECEIPT") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("THIS RECEIPT/INVOICE SHALL BE VALID FOR") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FIVE (5) YEARS FROM DATE OF THE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PERMIT TO USE.") + Environment.NewLine + Environment.NewLine;
            return details;
        }
        public static string doFooter(string branchcode)
        {
            var row = Database.getMultipleQuery("ReceiptFooter", "BranchCode='" + branchcode + "' ", "FieldA,FieldB,FieldC,FieldD,FieldE,FieldF,FieldG,FieldH,FieldI,FieldJ,FieldK,FieldL,FieldM");
            string a, b, c, d, e, f, g, h, i, j, k, l, m;
            a = row["FieldA"].ToString();
            b = row["FieldB"].ToString();
            c = row["FieldC"].ToString();
            d = row["FieldD"].ToString();
            e = row["FieldE"].ToString();
            f = row["FieldF"].ToString();
            g = row["FieldG"].ToString();
            h = row["FieldH"].ToString();
            i = row["FieldI"].ToString();
            j = row["FieldJ"].ToString();
            k = row["FieldK"].ToString();
            l = row["FieldL"].ToString();
            m = row["FieldM"].ToString();

            String details = "";
            details += HelperFunction.PrintCenterText(a) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Operated By: Eulen Topacio Avancena") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(b) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(c) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Guadalupe Cebu City 6000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(d) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(e) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Valid Until: 2018-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(f) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(g) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(h) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(i) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(j) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(k) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(l) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(m) + Environment.NewLine;

            return details;
        }
        public static string doFooter(string branchcode,string type)
        {
            String details = "";
            var row = Database.getMultipleQuery("ReceiptFooter", "BranchCode='" + branchcode + "' ", "FieldA,FieldB,FieldC,FieldD,FieldE,FieldF,FieldG,FieldH,FieldI,FieldJ,FieldK,FieldL,FieldM");
            string a, b, c, d, e, f, g, h, i, j, k, l, m;
            a = row["FieldA"].ToString();
            b = row["FieldB"].ToString();
            c = row["FieldC"].ToString();
            d = row["FieldD"].ToString();
            e = row["FieldE"].ToString();
            f = row["FieldF"].ToString();
            g = row["FieldG"].ToString();
            h = row["FieldH"].ToString();
            i = row["FieldI"].ToString();
            j = row["FieldJ"].ToString().Replace("INVOICE", "DOCUMENT");
            k = row["FieldK"].ToString();
            l = row["FieldL"].ToString();
            m = row["FieldM"].ToString();
            string jj = j.Replace("INVOICE", "DOCUMENT");
            details += HelperFunction.PrintCenterText(a) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Operated By: Eulen Topacio Avancena") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(b) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(c) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Guadalupe Cebu City 6000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(d) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(e) + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Valid Until: 2018-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText(f) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(g) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(h) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(i) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(j) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(k) + Environment.NewLine;
            details += HelperFunction.PrintCenterText(l) + Environment.NewLine;
           
            details += HelperFunction.PrintCenterText("This Document is not valid for") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Claim of Input Tax") + Environment.NewLine;

            return details;
        }
        public static string doFooter()
        {
            String details = "";
            details += HelperFunction.PrintCenterText("Tanay Technologies/E. Tanay") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("TIN : 192760374-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Ojeda St., Dayandang, Naga City") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Acc'd#:065-192760374-000304") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Date Issued : 2007-08-17") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("BIR Final PTU : 2017-10-26") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU Date Issued : 2016-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU Valid Until : 2021-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("This serves as your OFFICIAL RECEIPT") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THIS RECEIPT/INVOICE SHALL BE VALID FOR") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FIVE (5) YEARS FROM DATE OF THE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PERMIT TO USE.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THANK YOU! Pls. Come Again.") + Environment.NewLine;
            return details;
        }
        public static string doFooterTest()
        {
            String details = "";
            details += HelperFunction.PrintCenterText("EULZ AVANCENA.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("TIN : 221-413-885-000") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Banawa St. Cebu City") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Acc'd#:123-123456789-123456") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("Date Issued : 2018-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("BIR Final PTU : 2018-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU Date Issued : 2018-01-01") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PTU Valid Until : 2018-01-01") + Environment.NewLine;

            //details += HelperFunction.PrintCenterText("THANK YOU! Pls. Come Again.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("This serves as your OFFICIAL RECEIPT") + Environment.NewLine;
            //details += HelperFunction.PrintCenterText("OFFICIAL RECEIPT") + Environment.NewLine + Environment.NewLine;

            details += HelperFunction.PrintCenterText("THIS RECEIPT/INVOICE SHALL BE VALID FOR") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("FIVE (5) YEARS FROM DATE OF THE") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("PERMIT TO USE.") + Environment.NewLine;
            details += HelperFunction.PrintCenterText("THANK YOU! Pls. Come Again.") + Environment.NewLine;
            return details;
        }
    }
}
