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
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SalesInventorySystem.HotelManagement
{
    public partial class HotelFrmMainDashBoard : DevExpress.XtraEditors.XtraForm
    {
        public static string id = "", bookingid = "";

        public HotelFrmMainDashBoard()
        {
            InitializeComponent();
        }

        private void HotelFrmMainDashBoard_Load(object sender, EventArgs e)
        {
            filtertab();
        }

        private void filtertab()
        {
            if (tabControl1.SelectedTab.Equals(ForCheckout))
            {
                Database.display("SELECT BookingNo,RoomNumber,RoomType,GuestName,CheckInDate,CheckOutDate FROM CheckinGuest WHERE CheckOutDate='" + DateTime.Now.ToShortDateString() + "' ", gridControlCheckout, gridViewCheckout, Database.getCustomizeConnection());
            }
            else if (tabControl1.SelectedTab.Equals(ForCleaning))
            {
                Database.display("SELECT RoomNumber,ExecuteBy,DateAdded FROM RoomCleanedReport WHERE Status='FOR CLEANING' ", gridControlCleaning, gridViewCleaning, Database.getCustomizeConnection());
            }
            else if (tabControl1.SelectedTab.Equals(Available))
            {
                Database.display("SELECT RoomNumber,RoomDescription,RoomType,RoomStatus FROM Rooms WHERE RoomStatus='Available'", gridControlAvailable, gridViewAvailable, Database.getCustomizeConnection());
            }
            else if (tabControl1.SelectedTab.Equals(Occupied))
            {
                Database.display("SELECT RoomNumber,RoomDescription,RoomType,RoomStatus FROM Rooms WHERE RoomStatus='Occupied'", gridControlOccupied, gridViewOccupied, Database.getCustomizeConnection());

            }
            else if (tabControl1.SelectedTab.Equals(ForCollection))
            {
                //Database.display("SELECT SequenceNumber,RoomNumber,RoomType,GuestID,GuestName,CheckInDate,CheckOutDate FROM CheckinGuest WHERE isDone='0' ", gridControlCollection, gridViewCollection, Database.getCustomizeConnection());
            }
            else if (tabControl1.SelectedTab.Equals(checkinAndReservation))
            {
                Database.display("SELECT BookingNo,RoomNumber,RoomType,RoomRate,GuestID,GuestName,CheckInDate,CheckOutDate,NumberOfDays,TotalAmount,InitialPayment FROM CheckinGuest WHERE isDone='0' ", gridControlCheckin, gridViewCheckin, Database.getCustomizeConnection());
                Database.display("SELECT * FROM Reservation WHERE Status='ONGOING'", gridControl7, gridView7, Database.getCustomizeConnection());
            }
            else if (tabControl1.SelectedTab.Equals(Arrival))
            {
                Database.display("SELECT ReservationNo,RoomNumber,GuestName,ContactNo,CheckInDate,DateReserved FROM Reservation WHERE Status='ONGOING' AND CAST(CheckInDate as date) = '"+DateTime.Now.ToShortDateString()+"' ", gridControlArrivalForToday, gridViewArrivalForToday, Database.getCustomizeConnection());
                Database.display("SELECT ReservationNo,RoomNumber,GuestName,ContactNo,CheckInDate,DateReserved FROM Reservation WHERE Status='ONGOING' AND CAST(CheckInDate as date) > '"+ DateTime.Now.ToShortDateString() + "' ", gridControl7, gridView7, Database.getCustomizeConnection());
            }
        }

        
        private void checkInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string roomnum = gridViewAvailable.GetRowCellValue(gridViewAvailable.FocusedRowHandle, "RoomNumber").ToString();
            string roomtype = gridViewAvailable.GetRowCellValue(gridViewAvailable.FocusedRowHandle, "RoomType").ToString();
            HotelFrmCheckin checkin = new HotelFrmCheckin();
            checkin.Show();
            Database.displaySearchlookupEdit("Select GuestID,FirstName,MiddleName,LastName FROM GuestInfo", checkin.txtid, "GuestID", "GuestID",Database.getCustomizeConnection());
            Database.displayDevComboBoxItems("SELECT * FROM Rates", "Rates",checkin.txtratetype,Database.getCustomizeConnection());
            
            checkin.txtroomnum.Text = roomnum;
            checkin.txtroomtype.Text = roomtype;
        }

        private void gridControlAvailable_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripAvailable.Show(gridControlAvailable, e.Location);
        }

        private void gridControl7_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuReservation.Show(gridControl7, e.Location);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string roomnum = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "RoomNumber").ToString();
            string resid = gridView7.GetRowCellValue(gridView7.FocusedRowHandle, "ReservationNo").ToString();
            HotelFrmCheckin checkin = new HotelFrmCheckin();
            checkin.Show();

            Database.displaySearchlookupEdit("Select GuestID,FirstName,MiddleName,LastName FROM GuestInfo", checkin.txtid, "GuestID", "GuestID", Database.getCustomizeConnection());
            Database.displayDevComboBoxItems("SELECT * FROM Rates", "Rates", checkin.txtratetype, Database.getCustomizeConnection());
            checkin.txtresid.Text = resid;
            checkin.txtroomnum.Text = roomnum;
            checkin.txtroomtype.Text = Database.getSingleQuery("Rooms", "RoomNumber='" + roomnum + "'", "RoomType", Database.getCustomizeConnection());
        }

        private void gridControlCheckin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripCheckin.Show(gridControlCheckin, e.Location);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {   
            string bookingno = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "BookingNo").ToString();
            bookingid = bookingno;
            string roomnum = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "RoomNumber").ToString();
            id = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestID").ToString();
            HotelCheckoutComputation checkin = new HotelCheckoutComputation();
            checkin.txtbookingno.Text = bookingno;
            checkin.Show();
            Database.display("SELECT * FROM Charges WHERE BookingNo='" + bookingno + "'", checkin.gridControlCharges, checkin.gridViewCharges, Database.getCustomizeConnection());
            Database.display("SELECT BookingNo,ReferenceNo,TotalItem,TotalAmount,CAST(TransDate as date) as DateOrder FROM BatchSalesSummary WHERE RoomNumber='" + roomnum + "' and isFloat=1 and Status='Pending' and BookingNo='" + bookingno + "'", checkin.gridControlOrders, checkin.gridViewOrders);
        }

        private void transferRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookingid = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "BookingNo").ToString();
            id = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestID").ToString();
            HotelFrmTransferRoom transferrm = new HotelFrmTransferRoom();
            transferrm.Show();

            Database.display("SELECT RoomNumber,RoomDescription,RoomType,RoomStatus FROM Rooms WHERE RoomStatus='Available'", transferrm.gridControlAvailable, transferrm.gridViewAvailable, Database.getCustomizeConnection());

        }

        private void addServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotelFrmAddCharges htlchrge = new HotelFrmAddCharges();
            htlchrge.Show();
            Database.displaySearchlookupEdit("SELECT Charges,Rate FROM ChargesRate", htlchrge.txtservices, "Charges", "Charges", Database.getCustomizeConnection());
            htlchrge.lblguestid.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestID").ToString();
            htlchrge.lblcheckindate.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "CheckInDate").ToString();
            htlchrge.lblguestname.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestName").ToString();
            htlchrge.lblroomno.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "RoomNumber").ToString();
            htlchrge.lblbookingid.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "BookingNo").ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtertab();
        }

        private void gridControlOccupied_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuOccupied.Show(gridControlOccupied, e.Location);
        }

        private void requestForCleaningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isExist = Database.checkifExist("SELECT * FROM RoomCleanedReport WHERE RoomNumber='" + gridViewAvailable.GetRowCellValue(gridViewAvailable.FocusedRowHandle, "RoomNumber").ToString() + "' and Status='FOR CLEANING'", Database.getCustomizeConnection());
            if (!isExist)
                Database.ExecuteQuery("INSERT INTO RoomCleanedReport VALUES('" + gridViewAvailable.GetRowCellValue(gridViewAvailable.FocusedRowHandle, "RoomNumber").ToString() + "','FOR CLEANING','" + Login.Fullname + "','','" + DateTime.Now.ToShortDateString() + "','','')", "Tag as For Cleaning", Database.getCustomizeConnection());
            else
                XtraMessageBox.Show("RoomNumber Already Tagged as FOR CLEANING..");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            bool isExist = Database.checkifExist("SELECT * FROM RoomCleanedReport WHERE RoomNumber='" + gridViewOccupied.GetRowCellValue(gridViewOccupied.FocusedRowHandle, "RoomNumber").ToString() + "' and Status='FOR CLEANING'", Database.getCustomizeConnection());
            if (!isExist)
                Database.ExecuteQuery("INSERT INTO RoomCleanedReport VALUES('" + gridViewOccupied.GetRowCellValue(gridViewOccupied.FocusedRowHandle, "RoomNumber").ToString() + "','FOR CLEANING','" + Login.Fullname + "','','" + DateTime.Now.ToShortDateString() + "','','')", "Tag as For Cleaning", Database.getCustomizeConnection());
            else
                XtraMessageBox.Show("RoomNumber Already Tagged as FOR CLEANING..");
        }

        private void requestForCleaningToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bool isExist = Database.checkifExist("SELECT * FROM RoomCleanedReport WHERE RoomNumber='" + gridViewOccupied.GetRowCellValue(gridViewOccupied.FocusedRowHandle, "RoomNumber").ToString() + "' and Status='FOR CLEANING'", Database.getCustomizeConnection());
            if (!isExist)
                Database.ExecuteQuery("INSERT INTO RoomCleanedReport VALUES('" + gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "RoomNumber").ToString() + "','FOR CLEANING','" + Login.Fullname + "','','" + DateTime.Now.ToShortDateString() + "','','')", "Tag as For Cleaning", Database.getCustomizeConnection());
            else
                XtraMessageBox.Show("RoomNumber Already Tagged as FOR CLEANING..");
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripCheckin.Show(gridControlCheckin, e.Location);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            HotelFrmUpdateForCleaning asd = new HotelFrmUpdateForCleaning();
            asd.txtroomno.Text = gridViewCleaning.GetRowCellValue(gridViewCleaning.FocusedRowHandle, "RoomNumber").ToString();
            Database.displaySearchlookupEdit("SELECT HouseKeeper FROM RoomAttendant", asd.txtattendant, "HouseKeeper", "HouseKeeper", Database.getCustomizeConnection());
            asd.ShowDialog(this);
            if(HotelFrmUpdateForCleaning.isdone == true)
            {
                asd.Dispose();
                filtertab();
                HotelFrmUpdateForCleaning.isdone = false;
            }
        }

        private void gridControlCleaning_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripForCleaning.Show(gridControlCleaning, e.Location);
        }

        private void clientInfoSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string guestid = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestID").ToString();
            string guestname, checkindate, address, bdate, mobileno, idpresented, idno, roomno, roomrate, noofstay, totalamountdue, amountpaid;
            guestname = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestName").ToString();
            checkindate = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "CheckInDate").ToString();
            address = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "Address", Database.getCustomizeConnection());
            bdate = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "DateOfBirth", Database.getCustomizeConnection());
            mobileno = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "ContactNo", Database.getCustomizeConnection());
            idpresented = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "PrimaryID", Database.getCustomizeConnection());
            idno = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "PrimaryIDNo", Database.getCustomizeConnection());
            roomno = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "RoomNumber").ToString();
            roomrate = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "RoomRate").ToString(); 
            noofstay = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "NumberOfDays").ToString(); 
            totalamountdue = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "TotalAmount").ToString(); 
            amountpaid = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "InitialPayment").ToString();

            HotelReportingFrms.ClientInfoSheet asdk = new HotelReportingFrms.ClientInfoSheet();
            asdk.lblguestname.Text = guestname;
            asdk.lblcheckindate.Text = checkindate;
            asdk.lbladdress.Text = address;
            asdk.lblbdate.Text = bdate;
            asdk.lblmobileno.Text = mobileno;
            asdk.lblidpresented.Text = idpresented;
            asdk.lblidno.Text = idno;
            asdk.lblroomno.Text = roomno;
            asdk.lblroomrate.Text = roomrate;
            asdk.lblnoofstay.Text = noofstay;
            asdk.lbltotalamountdue.Text = totalamountdue;
            asdk.lblamountpaid.Text = amountpaid;
            ReportPrintTool report = new ReportPrintTool(asdk);
            report.ShowRibbonPreviewDialog();
        }

        private void gridControlArrivalForToday_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStripArrivalForToday.Show(gridControlArrivalForToday, e.Location);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void viewBookingDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string bookingno = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "BookingNo").ToString();
            string guestid = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestID").ToString();
            string checkindate = Database.getSingleQuery("CheckinGuest", "BookingNo='" + bookingno + "' and isDone=0", "CheckInDate", Database.getCustomizeConnection());
            string checkoutdate  =Database.getSingleQuery("CheckinGuest", "BookingNo='" + bookingno + "' and isDone=0", "CheckOutDate", Database.getCustomizeConnection());
            HotelFrmViewBookingDetails askdh = new HotelFrmViewBookingDetails();
            askdh.lblgender.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "Gender", Database.getCustomizeConnection());
            askdh.lbldateofbirth.Text = Convert.ToDateTime(Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "DateofBirth", Database.getCustomizeConnection())).ToShortDateString();
            askdh.lblplaceofbirth.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "PlaceofBirth", Database.getCustomizeConnection());
            askdh.lblcitizenship.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "Citizenship", Database.getCustomizeConnection());
            askdh.lblnationality.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "Nationality", Database.getCustomizeConnection());
            askdh.lblcivilstatus.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "CivilStatus", Database.getCustomizeConnection());
            askdh.lblreligion.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "Religion", Database.getCustomizeConnection());
            askdh.lblcompany.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "Company", Database.getCustomizeConnection());
            askdh.lblprimaryid.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "PrimaryID", Database.getCustomizeConnection());
            askdh.lblprimaryidno.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "PrimaryIDNo", Database.getCustomizeConnection());
            askdh.lblsecondaryid.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "SecondaryID", Database.getCustomizeConnection());
            askdh.lblsecondaryidno.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "SecondaryIDNo", Database.getCustomizeConnection());

            askdh.lblbookingno.Text = bookingno;
            askdh.lblbookedby.Text = Database.getSingleQuery("CheckinGuest", "BookingNo='" + bookingno + "' and isDone=0", "AddedBy", Database.getCustomizeConnection());
            askdh.lblcheckindate.Text = Convert.ToDateTime(checkindate).ToShortDateString();
            askdh.lblroomtype.Text = Database.getSingleQuery("CheckinGuest", "BookingNo='" + bookingno + "' and isDone=0", "RoomType", Database.getCustomizeConnection());
            askdh.lblremarks.Text = "";
            askdh.lblcheckoutdate.Text = Convert.ToDateTime(checkoutdate).ToShortDateString();
            askdh.lblfullname.Text = Database.getSingleQuery("CheckinGuest", "BookingNo='" + bookingno + "' and isDone=0", "GuestName", Database.getCustomizeConnection());
            askdh.lbladdress.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "Address", Database.getCustomizeConnection());
            askdh.lblcontactno.Text = Database.getSingleQuery("GuestInfo", "GuestID='" + guestid + "'", "ContactNo", Database.getCustomizeConnection());

            askdh.lblroomno.Text = Database.getSingleQuery("CheckinGuest", "BookingNo='" + bookingno + "' and isDone=0", "RoomNumber", Database.getCustomizeConnection());
            askdh.lblrate.Text = Database.getSingleQuery("CheckinGuest", "BookingNo='" + bookingno + "' and isDone=0", "RoomRate", Database.getCustomizeConnection());
            askdh.lblcheckindatecheckoutdate.Text = Convert.ToDateTime(checkindate).ToShortDateString()+ " - " + Convert.ToDateTime(checkoutdate).ToShortDateString();
            SalesInventorySystem.Classes.Utilities.GetImage(askdh.pictureBox1, "GuestInfo", "GuestID='" + guestid + "'", "ImageID", Database.getCustomizeConnection());
            Database.display("SELECT * FROM Charges WHERE BookingNo='" + bookingno + "'", askdh.gridControlCharges, askdh.gridViewCharges, Database.getCustomizeConnection());
            Database.display("SELECT BookingNo,ReferenceNo,TotalItem,TotalAmount,CAST(TransDate as date) as DateOrder FROM BatchSalesSummary WHERE RoomNumber='" + gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "RoomNumber").ToString() + "' and isFloat=1 and Status='Pending' and BookingNo='" + bookingno + "'", askdh.gridControlOrders, askdh.gridViewOrders);

            askdh.ShowDialog(this);
           
        }

        private void addDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookingid = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "BookingNo").ToString();
            HotelFrmAddDiscount htlchrge = new HotelFrmAddDiscount();
            htlchrge.Show();
            htlchrge.lblguestid.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestID").ToString();
            htlchrge.lblbookingno.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "BookingNo").ToString();
            htlchrge.lblcheckindate.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "CheckInDate").ToString();
            htlchrge.lblguestname.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "GuestName").ToString();
            htlchrge.lblroomno.Text = gridViewCheckin.GetRowCellValue(gridViewCheckin.FocusedRowHandle, "RoomNumber").ToString();

        }
    }
}