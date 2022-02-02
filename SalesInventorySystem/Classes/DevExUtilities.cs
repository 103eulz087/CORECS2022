using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesInventorySystem.Classes
{
    class DevExUtilities
    {
        private static string strApplicationName="";

        public static bool checkEmptyTextBox(TextEdit txt, string strMessage)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                displayMessage("Fields with (*) are compulsory. \n" + strMessage, MessageBoxIcon.Exclamation);
                txt.Focus();
                return true;
            }
            else { return false; }

        }
        public static bool displayMessage(string strMessage)
        {
            DialogResult dialogResult = XtraMessageBox.Show(strMessage, strApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes) { return true; }
            else { return false; }
        }

        public static void displayMessage(string strMessage, MessageBoxIcon msgBoxIcon)
        {
            XtraMessageBox.Show(strMessage, strApplicationName, MessageBoxButtons.OK, msgBoxIcon);
        }

        //public static void ClearFields(DevExpress.XtraEditors.GroupControl ctrl)
        //{
        //    foreach (GroupControl ctl in ctrl.Controls)
        //    {
        //        if (ctl is TextEdit) { ctl.Text = string.Empty; }
        //        else if (ctl is ComboBoxEdit) { ((ComboBoxEdit)ctl).SelectedIndex = -1; }
        //        else if (ctl is DateTimePicker) { ((DateTimePicker)ctl).Value = DateTime.Today; }
        //        //else if (ctl is DateTimePicker){((DateTimePicker)ctl).Checked  = false;}
        //        else if (ctl is PictureEdit) { ((PictureEdit)ctl).Image = null; }
        //        else if (ctl is CheckBox) { ((CheckBox)ctl).Checked = false; }
        //        else if (ctl is ListView) { ((ListView)ctl).Items.Clear(); }
        //    }
        //}
    }
}
