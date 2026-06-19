using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using DevExpress.XtraEditors;

namespace SalesInventorySystem
{
    internal class SingleInstanceApp : WindowsFormsApplicationBase
    {
        public SingleInstanceApp()
        {
            IsSingleInstance = true;
        }

        protected override void OnCreateMainForm()
        {
            MainForm = new StartupForm(this);   // StartupForm is a Form
        }

        internal void SwitchMainForm(Form f)
        {
            MainForm = f; // allowed because MainForm is protected and we are inside derived class [3](https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.applicationservices.windowsformsapplicationbase.mainform?view=windowsdesktop-10.0)
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            eventArgs.BringToForeground = true; // bring existing instance forward
            XtraMessageBox.Show("Application is already running.");
            base.OnStartupNextInstance(eventArgs);
        }
    }
}