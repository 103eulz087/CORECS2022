namespace SalesInventorySystem.Orders
{
    partial class ReceivedSTSDetails
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gridControlMyReq = new DevExpress.XtraGrid.GridControl();
            this.gridViewMyReq = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMyReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMyReq)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gridControlMyReq);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(1578, 922);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // gridControlMyReq
            // 
            this.gridControlMyReq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlMyReq.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlMyReq.Location = new System.Drawing.Point(3, 20);
            this.gridControlMyReq.MainView = this.gridViewMyReq;
            this.gridControlMyReq.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControlMyReq.Name = "gridControlMyReq";
            this.gridControlMyReq.Size = new System.Drawing.Size(1572, 898);
            this.gridControlMyReq.TabIndex = 3;
            this.gridControlMyReq.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMyReq});
            // 
            // gridViewMyReq
            // 
            this.gridViewMyReq.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMyReq.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewMyReq.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridViewMyReq.Appearance.Row.Options.UseFont = true;
            this.gridViewMyReq.DetailHeight = 431;
            this.gridViewMyReq.GridControl = this.gridControlMyReq;
            this.gridViewMyReq.Name = "gridViewMyReq";
            this.gridViewMyReq.OptionsBehavior.Editable = false;
            this.gridViewMyReq.OptionsBehavior.ReadOnly = true;
            this.gridViewMyReq.OptionsView.ColumnAutoWidth = false;
            this.gridViewMyReq.OptionsView.RowAutoHeight = true;
            this.gridViewMyReq.OptionsView.ShowFooter = true;
            // 
            // ReceivedSTSDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 922);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReceivedSTSDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceivedSTSDetails";
            this.Load += new System.EventHandler(this.ReceivedSTSDetails_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMyReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMyReq)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        public DevExpress.XtraGrid.GridControl gridControlMyReq;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewMyReq;
    }
}