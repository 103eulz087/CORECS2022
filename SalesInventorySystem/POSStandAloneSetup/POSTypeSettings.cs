using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.POSStandAloneSetup
{
    public partial class POSTypeSettings : Form
    {
        public POSTypeSettings()
        {
            InitializeComponent();
        }

        private void POSTypeSettings_Load(object sender, EventArgs e)
        {
            display();
        }
        void display()
        {
            var rows = Database.getMultipleQuery("POSType", "PosType is not null", "POSType,DisplayPoolPort,isUsedDisplayPool,isEnablePrinting,SendEmailNotification," +
                "UploadPerShifting,DataUploading,EnableInvoiceLapsingTerm,EnableCreditLimit,isLinkedServer,linkedServerName,MachineName,EODEmailNotification,CashBeginAmount");
            string POSType = rows["POSType"].ToString();
            string DisplayPoolPort = rows["DisplayPoolPort"].ToString(); //not used
            string isUsedDisplayPool = rows["isUsedDisplayPool"].ToString(); //not used
            string isEnablePrinting = rows["isEnablePrinting"].ToString();
            string SendEmailNotification = rows["SendEmailNotification"].ToString();
            string UploadPerShifting = rows["UploadPerShifting"].ToString();
            string DataUploadingType = rows["DataUploading"].ToString();
            string EnableInvoiceLapsingTerm = rows["EnableInvoiceLapsingTerm"].ToString();
            string EnableCreditLimit = rows["EnableCreditLimit"].ToString();
            string isLinkedServer = rows["isLinkedServer"].ToString();
            string linkedServerName = rows["linkedServerName"].ToString();
            string MachineName = rows["MachineName"].ToString();
            string EODEmailNotification = rows["EODEmailNotification"].ToString();
            string CashBeginAmount = rows["CashBeginAmount"].ToString();

            if (Convert.ToBoolean(isEnablePrinting)) { chckisenableprinting.Checked = true; } else { chckisenableprinting.Checked = false; }
            if (Convert.ToBoolean(SendEmailNotification)) { chcksendemail.Checked = true; } else { chcksendemail.Checked = false; }
            if (Convert.ToBoolean(UploadPerShifting)) { chckuploadpershifting.Checked = true; } else { chckuploadpershifting.Checked = false; }
            if (Convert.ToBoolean(DataUploadingType)) { chckisdatauploading.Checked = true; } else { chckisdatauploading.Checked = false; }
            if (Convert.ToBoolean(EnableInvoiceLapsingTerm)) { chckinvoicelapsing.Checked = true; } else { chckinvoicelapsing.Checked = false; }
            if (Convert.ToBoolean(EnableCreditLimit)) { chckcreditlimit.Checked = true; } else { chckcreditlimit.Checked = false; }
            if (Convert.ToBoolean(isLinkedServer)) { chckislinkedserver.Checked = true; } else { chckislinkedserver.Checked = false; }
            if (Convert.ToBoolean(EODEmailNotification)) { chckeodnotification.Checked = true; } else { chckeodnotification.Checked = false; }

            txtmachinename.Text = MachineName;
            txtpostype.Text = POSType;
            txtlinkedservername.Text = linkedServerName;
            txtcashbegin.Text = CashBeginAmount;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtcashbegin.Text))
            {
                XtraMessageBox.Show("Cash Begin Must Not Empty");
            }
            else
            {
                update();
            }
           
        }

        void update()
        {
            bool isEnablePrinting, SendEmailNotification, UploadPerShifting, DataUploadingType, EnableInvoiceLapsingTerm, EnableCreditLimit, isLinkedServer, EODEmailNotification;

            if (chckisenableprinting.Checked == true) { isEnablePrinting = true; } else { isEnablePrinting = false; }
            if (chcksendemail.Checked == true) { SendEmailNotification = true; }else { SendEmailNotification = false; }
            if (chckuploadpershifting.Checked == true) { UploadPerShifting = true; }else { UploadPerShifting = false; }
            if (chckisdatauploading.Checked == true) { DataUploadingType = true; }else { DataUploadingType = false; }
            if (chckinvoicelapsing.Checked == true) { EnableInvoiceLapsingTerm = true; }else { EnableInvoiceLapsingTerm = false; }
            if (chckcreditlimit.Checked == true) { EnableCreditLimit = true; }else { EnableCreditLimit = false; }
            if (chckislinkedserver.Checked == true) { isLinkedServer = true; }else { isLinkedServer = false; } 
            if (chckeodnotification.Checked == true) { EODEmailNotification = true; }else { EODEmailNotification = false; } 

            Database.ExecuteQuery("UPDATE POSType Set PosType='"+txtpostype.Text+"'" +
                ",isEnablePrinting='" + isEnablePrinting + "'" +
                ",SendEmailNotification='"+ SendEmailNotification + "'" +
                ",UploadPerShifting='"+ UploadPerShifting + "'" +
                ",DataUploading='"+ DataUploadingType + "'" +
                ",EnableInvoiceLapsingTerm='"+ EnableInvoiceLapsingTerm + "'" +
                ",EnableCreditLimit='"+ EnableCreditLimit + "'" +
                ",isLinkedServer='"+ isLinkedServer + "'" +
                ",linkedServerName='"+txtlinkedservername.Text+"'" +
                ",MachineName='"+txtmachinename.Text+"'" +
                ",EODEmailNotification='" + EODEmailNotification + "'" +
                ",CashBeginAmount='" + txtcashbegin.Text + "'" +
                " ", "Successfully Updated!");
            this.Dispose();
        }

        private void txtcashbegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            HelperFunction.isEnableAlpha(e);
        }
    }
}

