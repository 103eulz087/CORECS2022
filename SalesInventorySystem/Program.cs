using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
//using System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SalesInventorySystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                DevExpress.UserSkins.BonusSkins.Register();
                foreach (Form form in Application.OpenForms) //HotelManagement.HotelFrmMain()
                {
                    if (form.GetType() == typeof(Login))
                    //if (form.GetType() == typeof(Practice))
                    {

                        form.Activate();
                        return;
                    }
                }

                String thisprocessname = Process.GetCurrentProcess().ProcessName;

                Thread.Sleep(1000);

                if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
                {
                    XtraMessageBox.Show("There is Already Active User Login in this System.. if you want to open using another account, please press logout button in the menu.");
                    return;
                }

                //Application.Run(new Practice());
                Application.Run(new Login());
                //Application.Run(new PivotPractice());
                //Application.Run(new Main());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message.ToString());
            }

            

        }
    }
}
