using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;

namespace SalesInventorySystem
{
    public partial class StartupForm : XtraForm
    {
        private readonly SingleInstanceApp _app;

        // IMPORTANT: constructor should be internal (or make SingleInstanceApp public)
        internal StartupForm(SingleInstanceApp app)
        {
            InitializeComponent();
            _app = app;

            ShowInTaskbar = false;
            Opacity = 0;
            WindowState = FormWindowState.Minimized;
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            try
            {
                SplashScreenManager.ShowForm(typeof(WaitForm1));
                SplashScreenManager.Default.SetWaitFormCaption("Starting...");
                SplashScreenManager.Default.SetWaitFormDescription("Loading config...");

                await Task.Run(() =>
                {
                    string filepath = Application.StartupPath + "\\checkVersion.txt";
                    GlobalConfig.Load(filepath);
                });

                SplashScreenManager.Default.SetWaitFormDescription("Opening login...");

                var login = new Login();

                // switch main form safely via wrapper method
                _app.SwitchMainForm(login);

                SplashScreenManager.CloseForm();

                login.Show();
                Close();
            }
            catch (Exception ex)
            {
                try { SplashScreenManager.CloseForm(); } catch { }
                XtraMessageBox.Show(ex.ToString(), "Startup error");
                Application.Exit();
            }
        }
    }
}