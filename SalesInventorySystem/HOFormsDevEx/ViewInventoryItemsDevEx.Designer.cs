namespace SalesInventorySystem.HOFormsDevEx
{
    partial class ViewInventoryItemsDevEx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewInventoryItemsDevEx));
            this.txtproducts = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtdesc = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtcost = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.txtshipmentno = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtproducts.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtproducts
            // 
            this.txtproducts.EditValue = "";
            this.txtproducts.Location = new System.Drawing.Point(222, 83);
            this.txtproducts.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtproducts.Name = "txtproducts";
            this.txtproducts.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtproducts.Properties.Appearance.Options.UseFont = true;
            this.txtproducts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtproducts.Properties.NullText = "";
            this.txtproducts.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtproducts.Size = new System.Drawing.Size(380, 46);
            this.txtproducts.TabIndex = 34;
            this.txtproducts.EditValueChanged += new System.EventHandler(this.txtproducts_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 673;
            this.searchLookUpEdit1View.FixedLineWidth = 4;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(24, 87);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(179, 33);
            this.labelControl1.TabIndex = 33;
            this.labelControl1.Text = "Select Product:";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(24, 138);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(142, 33);
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "Description:";
            // 
            // txtdesc
            // 
            this.txtdesc.Location = new System.Drawing.Point(222, 137);
            this.txtdesc.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtdesc.Properties.Appearance.Options.UseFont = true;
            this.txtdesc.Size = new System.Drawing.Size(380, 44);
            this.txtdesc.TabIndex = 36;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(24, 188);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(62, 33);
            this.labelControl3.TabIndex = 37;
            this.labelControl3.Text = "Cost:";
            // 
            // txtcost
            // 
            this.txtcost.Location = new System.Drawing.Point(222, 187);
            this.txtcost.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtcost.Name = "txtcost";
            this.txtcost.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtcost.Properties.Appearance.Options.UseFont = true;
            this.txtcost.Size = new System.Drawing.Size(380, 44);
            this.txtcost.TabIndex = 38;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.5F);
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(222, 235);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(380, 102);
            this.simpleButton1.TabIndex = 39;
            this.simpleButton1.Text = "Update Final Cost";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Location = new System.Drawing.Point(222, 33);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.txtshipmentno.Properties.Appearance.Options.UseFont = true;
            this.txtshipmentno.Size = new System.Drawing.Size(380, 44);
            this.txtshipmentno.TabIndex = 41;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(24, 35);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(150, 33);
            this.labelControl4.TabIndex = 40;
            this.labelControl4.Text = "Shipment #:";
            // 
            // ViewInventoryItemsDevEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 371);
            this.Controls.Add(this.txtshipmentno);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.txtcost);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtdesc);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtproducts);
            this.Controls.Add(this.labelControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "ViewInventoryItemsDevEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewInventoryItemsDevEx";
            this.Load += new System.EventHandler(this.ViewInventoryItemsDevEx_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtproducts.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtcost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtshipmentno.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.SearchLookUpEdit txtproducts;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtdesc;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtcost;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.TextEdit txtshipmentno;
    }
}