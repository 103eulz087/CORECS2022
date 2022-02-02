namespace SalesInventorySystem
{
    partial class TestSweepStakes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txta = new System.Windows.Forms.TextBox();
            this.txtb = new System.Windows.Forms.TextBox();
            this.txtc = new System.Windows.Forms.TextBox();
            this.txtd = new System.Windows.Forms.TextBox();
            this.txte = new System.Windows.Forms.TextBox();
            this.txtf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtavailable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txta
            // 
            this.txta.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.txta.Location = new System.Drawing.Point(12, 60);
            this.txta.MaxLength = 1;
            this.txta.Name = "txta";
            this.txta.Size = new System.Drawing.Size(70, 72);
            this.txta.TabIndex = 0;
            this.txta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txta.TextChanged += new System.EventHandler(this.txta_TextChanged);
            // 
            // txtb
            // 
            this.txtb.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.txtb.Location = new System.Drawing.Point(88, 60);
            this.txtb.MaxLength = 1;
            this.txtb.Name = "txtb";
            this.txtb.Size = new System.Drawing.Size(70, 72);
            this.txtb.TabIndex = 1;
            this.txtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb.TextChanged += new System.EventHandler(this.txtb_TextChanged);
            // 
            // txtc
            // 
            this.txtc.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.txtc.Location = new System.Drawing.Point(164, 60);
            this.txtc.MaxLength = 1;
            this.txtc.Name = "txtc";
            this.txtc.Size = new System.Drawing.Size(70, 72);
            this.txtc.TabIndex = 2;
            this.txtc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtc.TextChanged += new System.EventHandler(this.txtc_TextChanged);
            // 
            // txtd
            // 
            this.txtd.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.txtd.Location = new System.Drawing.Point(240, 60);
            this.txtd.MaxLength = 1;
            this.txtd.Name = "txtd";
            this.txtd.Size = new System.Drawing.Size(70, 72);
            this.txtd.TabIndex = 3;
            this.txtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtd.TextChanged += new System.EventHandler(this.txtd_TextChanged);
            // 
            // txte
            // 
            this.txte.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.txte.Location = new System.Drawing.Point(316, 60);
            this.txte.MaxLength = 1;
            this.txte.Name = "txte";
            this.txte.Size = new System.Drawing.Size(70, 72);
            this.txte.TabIndex = 4;
            this.txte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txte.TextChanged += new System.EventHandler(this.txte_TextChanged);
            // 
            // txtf
            // 
            this.txtf.Font = new System.Drawing.Font("Tahoma", 32F, System.Drawing.FontStyle.Bold);
            this.txtf.Location = new System.Drawing.Point(392, 60);
            this.txtf.MaxLength = 1;
            this.txtf.Name = "txtf";
            this.txtf.Size = new System.Drawing.Size(70, 72);
            this.txtf.TabIndex = 5;
            this.txtf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtf.TextChanged += new System.EventHandler(this.txtf_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 22.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(33, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(409, 46);
            this.label1.TabIndex = 6;
            this.label1.Text = "Digital Sweepstakes";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 200);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(450, 39);
            this.button1.TabIndex = 7;
            this.button1.Text = "ADD TICKET";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(12, 246);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(450, 275);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 528);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(450, 39);
            this.button2.TabIndex = 9;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 22.8F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 46);
            this.label2.TabIndex = 10;
            this.label2.Text = "Available:";
            // 
            // txtavailable
            // 
            this.txtavailable.AutoSize = true;
            this.txtavailable.Font = new System.Drawing.Font("Tahoma", 22.8F, System.Drawing.FontStyle.Bold);
            this.txtavailable.Location = new System.Drawing.Point(213, 143);
            this.txtavailable.Name = "txtavailable";
            this.txtavailable.Size = new System.Drawing.Size(44, 46);
            this.txtavailable.TabIndex = 11;
            this.txtavailable.Text = "0";
            // 
            // TestSweepStakes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 577);
            this.Controls.Add(this.txtavailable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtf);
            this.Controls.Add(this.txte);
            this.Controls.Add(this.txtd);
            this.Controls.Add(this.txtc);
            this.Controls.Add(this.txtb);
            this.Controls.Add(this.txta);
            this.Name = "TestSweepStakes";
            this.Text = "TestSweepStakes";
            this.Load += new System.EventHandler(this.TestSweepStakes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txta;
        private System.Windows.Forms.TextBox txtb;
        private System.Windows.Forms.TextBox txtc;
        private System.Windows.Forms.TextBox txtd;
        private System.Windows.Forms.TextBox txte;
        private System.Windows.Forms.TextBox txtf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtavailable;
    }
}