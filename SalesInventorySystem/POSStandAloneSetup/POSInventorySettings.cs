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
    public partial class POSInventorySettings : Form
    {
        public POSInventorySettings()
        {
            InitializeComponent();
        }

        private void POSInventorySettings_Load(object sender, EventArgs e)
        {
            display();
        }

        void display()
        {
            bool isrealtimededuction = Database.checkifExist("Select isRealTimeDeduction FROM InventorySettings WHERE isRealTimeDeduction=1");
           
            if (isrealtimededuction)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        void update()
        {
            bool isrealtime;
            
            if (checkBox1.Checked == true)
            {
                isrealtime = true;
            }
            else
            {
                isrealtime = false;
            }
            Database.ExecuteQuery("UPDATE InventorySettings Set isFIFO='1',isRealTimeDeduction='" + isrealtime + "' ", "Successfully Updated!");
            this.Dispose();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            update();
        }
    }
}
