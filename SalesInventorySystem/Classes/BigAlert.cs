using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace SalesInventorySystem.Classes
{
    public static class BigAlert
    {
        // 1. The original "OK" Button Alert
        public static DialogResult Show(string title, string message, MessageBoxIcon icon)
        {
            return ShowInternal(title, message, icon, MessageBoxButtons.OK);
        }

        // 2. The NEW "YES/NO" Confirmation Alert
        public static DialogResult Show(string title, string message, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return ShowInternal(title, message, icon, buttons);
        }

        // 3. The Core Engine that builds the massive form
        private static DialogResult ShowInternal(string title, string message, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            using (XtraForm alertForm = new XtraForm())
            {
                alertForm.Text = title;
                alertForm.StartPosition = FormStartPosition.CenterScreen;
                alertForm.Size = new Size(700, 400); // Massive touch-screen size
                alertForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                alertForm.MaximizeBox = false;
                alertForm.MinimizeBox = false;
                alertForm.ShowIcon = false;

                // Determine Background Color
                Color backColor = Color.White;
                Color textColor = Color.White;

                if (icon == MessageBoxIcon.Error)
                    backColor = Color.Firebrick;       // Dark Red for Errors
                else if (icon == MessageBoxIcon.Information)
                    backColor = Color.SeaGreen;        // Green for Success
                else if (icon == MessageBoxIcon.Warning)
                    backColor = Color.DarkOrange;      // Orange for Warnings
                else if (icon == MessageBoxIcon.Question)
                    backColor = Color.SteelBlue;       // Professional Blue for Questions

                alertForm.Appearance.BackColor = backColor;

                // Create the massive Text Label
                LabelControl lblMessage = new LabelControl();
                lblMessage.Text = message;
                lblMessage.Appearance.Font = new Font("Segoe UI", 24f, FontStyle.Bold);
                lblMessage.Appearance.ForeColor = textColor;
                lblMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                lblMessage.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                lblMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                lblMessage.AutoSizeMode = LabelAutoSizeMode.None;
                lblMessage.Dock = DockStyle.Fill;
                lblMessage.Padding = new Padding(30);

                alertForm.Controls.Add(lblMessage);

                // ==========================================
                // BUTTON GENERATION LOGIC
                // ==========================================
                if (buttons == MessageBoxButtons.YesNo)
                {
                    // Create a table to split the bottom perfectly in half (50% / 50%)
                    TableLayoutPanel buttonPanel = new TableLayoutPanel();
                    buttonPanel.ColumnCount = 2;
                    buttonPanel.RowCount = 1;
                    buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                    buttonPanel.Height = 90; // Slightly taller for Yes/No to prevent miss-clicks
                    buttonPanel.Dock = DockStyle.Bottom;
                    buttonPanel.Margin = new Padding(0);
                    buttonPanel.Padding = new Padding(0);

                    // YES Button (Green)
                    SimpleButton btnYes = new SimpleButton();
                    btnYes.Text = "YES";
                    btnYes.Appearance.Font = new Font("Segoe UI", 22f, FontStyle.Bold);
                    btnYes.Appearance.BackColor = Color.SeaGreen;
                    btnYes.Appearance.ForeColor = Color.White;
                    btnYes.Dock = DockStyle.Fill;
                    btnYes.DialogResult = DialogResult.Yes;

                    // NO Button (Red)
                    SimpleButton btnNo = new SimpleButton();
                    btnNo.Text = "NO";
                    btnNo.Appearance.Font = new Font("Segoe UI", 22f, FontStyle.Bold);
                    btnNo.Appearance.BackColor = Color.Firebrick;
                    btnNo.Appearance.ForeColor = Color.White;
                    btnNo.Dock = DockStyle.Fill;
                    btnNo.DialogResult = DialogResult.No;

                    // Add to the grid
                    buttonPanel.Controls.Add(btnYes, 0, 0);
                    buttonPanel.Controls.Add(btnNo, 1, 0);

                    alertForm.Controls.Add(buttonPanel);
                    System.Media.SystemSounds.Question.Play();
                }
                else
                {
                    // Standard single OK Button
                    SimpleButton btnOk = new SimpleButton();
                    btnOk.Text = "OK";
                    btnOk.Appearance.Font = new Font("Segoe UI", 20f, FontStyle.Bold);
                    btnOk.Appearance.BackColor = Color.FromArgb(50, 50, 50); // Dark Gray
                    btnOk.Appearance.ForeColor = Color.White;
                    btnOk.Size = new Size(0, 80);
                    btnOk.Dock = DockStyle.Bottom;
                    btnOk.DialogResult = DialogResult.OK;

                    alertForm.Controls.Add(btnOk);

                    if (icon == MessageBoxIcon.Error) System.Media.SystemSounds.Hand.Play();
                    else System.Media.SystemSounds.Asterisk.Play();
                }

                // Bring to front and freeze the UI until they click a button
                return alertForm.ShowDialog();
            }
        }
    }
}