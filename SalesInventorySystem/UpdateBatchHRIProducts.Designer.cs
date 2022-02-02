namespace SalesInventorySystem
{
    partial class UpdateBatchHRIProducts
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
            this.txtprod = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtsprice = new DevExpress.XtraEditors.SpinEdit();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtprod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsprice.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtprod
            // 
            this.txtprod.Location = new System.Drawing.Point(129, 12);
            this.txtprod.Name = "txtprod";
            this.txtprod.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtprod.Properties.Appearance.Options.UseFont = true;
            this.txtprod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtprod.Properties.DisplayMember = "SupplierName";
            this.txtprod.Properties.NullText = "";
            this.txtprod.Properties.ValueMember = "SupplierName";
            this.txtprod.Properties.View = this.gridView3;
            this.txtprod.Size = new System.Drawing.Size(280, 24);
            this.txtprod.TabIndex = 452;
            this.txtprod.EditValueChanged += new System.EventHandler(this.txtprod_EditValueChanged);
            // 
            // gridView3
            // 
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label17.Location = new System.Drawing.Point(12, 15);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 17);
            this.label17.TabIndex = 451;
            this.label17.Text = "Select Products:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 453;
            this.label1.Text = "Selling Price:";
            // 
            // txtsprice
            // 
            this.txtsprice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtsprice.Location = new System.Drawing.Point(129, 42);
            this.txtsprice.Name = "txtsprice";
            this.txtsprice.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.txtsprice.Properties.Appearance.Options.UseFont = true;
            this.txtsprice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsprice.Size = new System.Drawing.Size(129, 24);
            this.txtsprice.TabIndex = 454;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SeaGreen;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(129, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 36);
            this.button1.TabIndex = 455;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UpdateBatchHRIProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 119);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtsprice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtprod);
            this.Controls.Add(this.label17);
            this.Name = "UpdateBatchHRIProducts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UpdateBatchHRIProducts";
            this.Load += new System.EventHandler(this.UpdateBatchHRIProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtprod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtsprice.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit txtprod;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SpinEdit txtsprice;
        private System.Windows.Forms.Button button1;
    }
}