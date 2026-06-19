using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SalesInventorySystem.AccountingDevEx
{
    public partial class BankReconItemForm : XtraForm
    {
        public string ItemType { get; set; }
        public string ReferenceNo { get; set; }
        public DateTime ItemDate { get; set; }
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public decimal BankStatementBal { get; set; }

        private static readonly Color C_CARD = Color.FromArgb(30, 35, 51);
        private static readonly Color C_TEXT = Color.FromArgb(232, 230, 224);
        private static readonly Color C_MUTED = Color.FromArgb(136, 145, 170);
        private static readonly Color C_GOLD = Color.FromArgb(201, 168, 76);
        private static readonly Font F_MONO = new Font("Courier New", 9f);

        private ComboBoxEdit cmbType;
        private TextEdit txtRef;
        private TextEdit txtPayee;
        private TextEdit txtAmount;
        private TextEdit txtRemarks;
        private DateEdit dtItemDate;

        public BankReconItemForm(bool isNew)
        {
            ItemType = "OC";
            ReferenceNo = "";
            ItemDate = DateTime.Today;
            Payee = "";
            Amount = 0m;
            Remarks = "";
            BankStatementBal = 0m;

            this.Text = isNew ? "Add Reconciling Item" : "Edit Reconciling Item";
            this.BackColor = Color.FromArgb(24, 28, 39);
            this.ForeColor = C_TEXT;
            this.ClientSize = new Size(400, 360);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            InitializeComponent();
            BuildDialog();
        }

        private void BuildDialog()
        {
            int y = 16;

            // Item Type
            cmbType = new ComboBoxEdit();
            cmbType.Font = F_MONO;
            cmbType.Properties.Appearance.BackColor = C_CARD;
            cmbType.Properties.Appearance.ForeColor = C_TEXT;
            cmbType.Properties.Items.AddRange(new object[]
            {
                "OC - Outstanding Check",
                "DIT - Deposit in Transit",
                "BCM - Bank Credit Memo",
                "BDM - Bank Debit Memo",
                "BC - Bank Charges",
                "NSF - NSF / Returned Check"
            });
            cmbType.SelectedIndex = 0;
            AddField("Item Type", cmbType, ref y);

            // Reference No
            txtRef = new TextEdit();
            txtRef.Font = F_MONO;
            StyleText(txtRef);
            AddField("Reference No (check/OR number)", txtRef, ref y);

            // Date
            dtItemDate = new DateEdit();
            dtItemDate.Font = F_MONO;
            dtItemDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            dtItemDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtItemDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            dtItemDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtItemDate.Properties.Appearance.BackColor = C_CARD;
            dtItemDate.Properties.Appearance.ForeColor = C_TEXT;
            dtItemDate.EditValue = DateTime.Today;
            AddField("Item Date", dtItemDate, ref y);

            // Payee
            txtPayee = new TextEdit();
            txtPayee.Font = F_MONO;
            StyleText(txtPayee);
            AddField("Payee / Depositor", txtPayee, ref y);

            // Amount
            txtAmount = new TextEdit();
            txtAmount.Font = F_MONO;
            StyleText(txtAmount);
            txtAmount.Text = "0.00";
            AddField("Amount (PHP)", txtAmount, ref y);

            // Remarks
            txtRemarks = new TextEdit();
            txtRemarks.Font = F_MONO;
            StyleText(txtRemarks);
            AddField("Remarks", txtRemarks, ref y);

            // Buttons
            SimpleButton btnOK = new SimpleButton();
            btnOK.Text = "Save";
            btnOK.DialogResult = DialogResult.None;
            btnOK.Bounds = new Rectangle(216, y, 80, 30);
            btnOK.Appearance.BackColor = C_GOLD;
            btnOK.Appearance.ForeColor = Color.FromArgb(15, 17, 23);
            btnOK.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

            SimpleButton btnCancel = new SimpleButton();
            btnCancel.Text = "Cancel";
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Bounds = new Rectangle(304, y, 72, 30);
            btnCancel.Appearance.BackColor = C_CARD;
            btnCancel.Appearance.ForeColor = C_MUTED;
            btnCancel.Appearance.BorderColor = Color.FromArgb(42, 48, 80);
            btnCancel.Font = new Font("Segoe UI", 9f);

            btnOK.Click += BtnOK_Click;

            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            // Populate edit values
            if (!string.IsNullOrEmpty(ItemType))
            {
                int i;
                for (i = 0; i < cmbType.Properties.Items.Count; i++)
                {
                    if (cmbType.Properties.Items[i].ToString().StartsWith(ItemType))
                    {
                        cmbType.SelectedIndex = i;
                        break;
                    }
                }

                txtRef.Text = ReferenceNo;
                dtItemDate.EditValue = ItemDate;
                txtPayee.Text = Payee;
                txtAmount.Text = Amount.ToString("N2");
                txtRemarks.Text = Remarks;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            decimal amt;

            if (string.IsNullOrWhiteSpace(txtRef.Text))
            {
                XtraMessageBox.Show("Reference No is required.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text.Replace(",", ""),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out amt) || amt <= 0)
            {
                XtraMessageBox.Show("Enter a valid positive amount.");
                return;
            }

            ItemType = cmbType.Text.Split(' ')[0];
            ReferenceNo = txtRef.Text.Trim();

            if (dtItemDate.EditValue != null && dtItemDate.EditValue is DateTime)
                ItemDate = (DateTime)dtItemDate.EditValue;
            else
                ItemDate = DateTime.Today;

            Payee = txtPayee.Text.Trim();
            Amount = Math.Round(amt, 2);
            Remarks = txtRemarks.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddField(string label, Control ctrl, ref int y)
        {
            LabelControl lbl = new LabelControl();
            lbl.Text = label.ToUpper();
            lbl.Font = new Font("Courier New", 7f, FontStyle.Bold);
            lbl.ForeColor = C_MUTED;
            lbl.AutoSizeMode = LabelAutoSizeMode.None;
            lbl.Bounds = new Rectangle(16, y, 360, 14);

            ctrl.Bounds = new Rectangle(16, y + 16, 360, 24);

            this.Controls.Add(lbl);
            this.Controls.Add(ctrl);

            y += 50;
        }

        private void StyleText(TextEdit ctl)
        {
            ctl.Properties.Appearance.BackColor = C_CARD;
            ctl.Properties.Appearance.ForeColor = C_TEXT;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "BankReconItemForm";
            this.ResumeLayout(false);
        }
    }
}
