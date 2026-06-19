
using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;

namespace SalesInventorySystem
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle(SkinStyle.Office2019White);

            // 1. Load your global cache first!
            GlobalCache.InitializeCompanyData();

            var app = new SingleInstanceApp();
            app.Run(args);
        }
    }
}
