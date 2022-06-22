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

namespace SalesInventorySystem
{
    public partial class SampleForm : DevExpress.XtraEditors.XtraForm
    {
        public SampleForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Classes.EmailSetup ema = new Classes.EmailSetup();
            ema.setupEmailParam("TEST", "ajsdhksajdhksjad", false);
        }
    }
}