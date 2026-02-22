namespace SalesInventorySystem.HOForms
{
    partial class AddPrimalCutInventory
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPrimalCutInventory));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtsrchprod = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.buttonSearchItems = new DevExpress.XtraEditors.SimpleButton();
            this.buttonPrintBarcode = new DevExpress.XtraEditors.SimpleButton();
            this.buttonGetWeight = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSaveAndTransfer = new DevExpress.XtraEditors.SimpleButton();
            this.buttonClear = new DevExpress.XtraEditors.SimpleButton();
            this.buttonCancel = new DevExpress.XtraEditors.SimpleButton();
            this.buttonAdd = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtweight = new System.Windows.Forms.TextBox();
            this.autoprintbarcode = new System.Windows.Forms.CheckBox();
            this.txtprodcat = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtshipmentno = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtbatchcode = new System.Windows.Forms.TextBox();
            this.txtavailableqty = new System.Windows.Forms.TextBox();
            this.txtskuno = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtports = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtprodcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.isprimalcuts = new System.Windows.Forms.CheckBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label10 = new System.Windows.Forms.Label();
            this.txtpalletno = new DevExpress.XtraEditors.TextEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.txtdispatchno = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtsrchprod.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpalletno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdispatchno.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtdispatchno);
            this.groupControl1.Controls.Add(this.label11);
            this.groupControl1.Controls.Add(this.txtpalletno);
            this.groupControl1.Controls.Add(this.label10);
            this.groupControl1.Controls.Add(this.txtsrchprod);
            this.groupControl1.Controls.Add(this.buttonSearchItems);
            this.groupControl1.Controls.Add(this.buttonPrintBarcode);
            this.groupControl1.Controls.Add(this.buttonGetWeight);
            this.groupControl1.Controls.Add(this.buttonSaveAndTransfer);
            this.groupControl1.Controls.Add(this.buttonClear);
            this.groupControl1.Controls.Add(this.buttonCancel);
            this.groupControl1.Controls.Add(this.buttonAdd);
            this.groupControl1.Controls.Add(this.pictureBox1);
            this.groupControl1.Controls.Add(this.button2);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.txtweight);
            this.groupControl1.Controls.Add(this.autoprintbarcode);
            this.groupControl1.Controls.Add(this.txtprodcat);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.txtshipmentno);
            this.groupControl1.Controls.Add(this.checkBox1);
            this.groupControl1.Controls.Add(this.txtbatchcode);
            this.groupControl1.Controls.Add(this.txtavailableqty);
            this.groupControl1.Controls.Add(this.txtskuno);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.txtports);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.txtprodcode);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.isprimalcuts);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2366, 486);
            this.groupControl1.TabIndex = 39;
            this.groupControl1.Text = "Process To Primal Cuts";
            this.groupControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl1_Paint);
            // 
            // txtsrchprod
            // 
            this.txtsrchprod.Location = new System.Drawing.Point(648, 194);
            this.txtsrchprod.Margin = new System.Windows.Forms.Padding(4);
            this.txtsrchprod.Name = "txtsrchprod";
            this.txtsrchprod.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.8F);
            this.txtsrchprod.Properties.Appearance.Options.UseFont = true;
            this.txtsrchprod.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtsrchprod.Properties.NullText = "";
            this.txtsrchprod.Properties.PopupView = this.searchLookUpEdit1View;
            this.txtsrchprod.Size = new System.Drawing.Size(638, 52);
            this.txtsrchprod.TabIndex = 57;
            this.txtsrchprod.EditValueChanged += new System.EventHandler(this.txtsrchprod_EditValueChanged);
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.DetailHeight = 547;
            this.searchLookUpEdit1View.FixedLineWidth = 3;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // buttonSearchItems
            // 
            this.buttonSearchItems.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.buttonSearchItems.Appearance.Options.UseFont = true;
            this.buttonSearchItems.Location = new System.Drawing.Point(1296, 131);
            this.buttonSearchItems.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSearchItems.Name = "buttonSearchItems";
            this.buttonSearchItems.Size = new System.Drawing.Size(291, 50);
            this.buttonSearchItems.TabIndex = 56;
            this.buttonSearchItems.Text = "Search Items";
            this.buttonSearchItems.Visible = false;
            this.buttonSearchItems.Click += new System.EventHandler(this.buttonSearchItems_Click);
            // 
            // buttonPrintBarcode
            // 
            this.buttonPrintBarcode.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.buttonPrintBarcode.Appearance.Options.UseFont = true;
            this.buttonPrintBarcode.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Print_32x32__2_;
            this.buttonPrintBarcode.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonPrintBarcode.Location = new System.Drawing.Point(1296, 256);
            this.buttonPrintBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrintBarcode.Name = "buttonPrintBarcode";
            this.buttonPrintBarcode.Size = new System.Drawing.Size(291, 116);
            this.buttonPrintBarcode.TabIndex = 55;
            this.buttonPrintBarcode.Text = "Print Barcode (F8)";
            this.buttonPrintBarcode.Click += new System.EventHandler(this.buttonPrintBarcode_Click_1);
            // 
            // buttonGetWeight
            // 
            this.buttonGetWeight.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.buttonGetWeight.Appearance.Options.UseFont = true;
            this.buttonGetWeight.Location = new System.Drawing.Point(1065, 256);
            this.buttonGetWeight.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGetWeight.Name = "buttonGetWeight";
            this.buttonGetWeight.Size = new System.Drawing.Size(220, 50);
            this.buttonGetWeight.TabIndex = 54;
            this.buttonGetWeight.Text = "Get Weight";
            this.buttonGetWeight.Click += new System.EventHandler(this.buttonGetWeight_Click);
            // 
            // buttonSaveAndTransfer
            // 
            this.buttonSaveAndTransfer.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.buttonSaveAndTransfer.Appearance.Options.UseFont = true;
            this.buttonSaveAndTransfer.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Save_16x16__5_;
            this.buttonSaveAndTransfer.Location = new System.Drawing.Point(1221, 394);
            this.buttonSaveAndTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSaveAndTransfer.Name = "buttonSaveAndTransfer";
            this.buttonSaveAndTransfer.Size = new System.Drawing.Size(222, 58);
            this.buttonSaveAndTransfer.TabIndex = 53;
            this.buttonSaveAndTransfer.Text = "Save && Transfer";
            this.buttonSaveAndTransfer.Click += new System.EventHandler(this.buttonSaveAndTransfer_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.buttonClear.Appearance.Options.UseFont = true;
            this.buttonClear.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Clear_16x16__2_;
            this.buttonClear.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonClear.Location = new System.Drawing.Point(1032, 394);
            this.buttonClear.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(180, 58);
            this.buttonClear.TabIndex = 52;
            this.buttonClear.Text = "Clear";
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.buttonCancel.Appearance.Options.UseFont = true;
            this.buttonCancel.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Cancel_16x16__2_;
            this.buttonCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonCancel.Location = new System.Drawing.Point(832, 394);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(190, 58);
            this.buttonCancel.TabIndex = 51;
            this.buttonCancel.Text = "Cancel Line";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 9.25F);
            this.buttonAdd.Appearance.Options.UseFont = true;
            this.buttonAdd.ImageOptions.Image = global::SalesInventorySystem.Properties.Resources.Add_16x16__2_;
            this.buttonAdd.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.buttonAdd.Location = new System.Drawing.Point(648, 394);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(176, 58);
            this.buttonAdd.TabIndex = 50;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 47);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(378, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 46;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SeaGreen;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(356, 379);
            this.button2.Margin = new System.Windows.Forms.Padding(6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(282, 72);
            this.button2.TabIndex = 27;
            this.button2.Text = "Upload(F10)";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(398, 261);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 39);
            this.label2.TabIndex = 10;
            this.label2.Text = "ACTUAL QTY:";
            // 
            // txtweight
            // 
            this.txtweight.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtweight.ForeColor = System.Drawing.Color.Blue;
            this.txtweight.Location = new System.Drawing.Point(648, 256);
            this.txtweight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtweight.MaxLength = 6;
            this.txtweight.Name = "txtweight";
            this.txtweight.Size = new System.Drawing.Size(406, 46);
            this.txtweight.TabIndex = 11;
            this.txtweight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtweight_KeyDown);
            this.txtweight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtweight_KeyPress);
            // 
            // autoprintbarcode
            // 
            this.autoprintbarcode.AutoSize = true;
            this.autoprintbarcode.Checked = true;
            this.autoprintbarcode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoprintbarcode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.autoprintbarcode.ForeColor = System.Drawing.Color.Black;
            this.autoprintbarcode.Location = new System.Drawing.Point(1612, 429);
            this.autoprintbarcode.Margin = new System.Windows.Forms.Padding(6);
            this.autoprintbarcode.Name = "autoprintbarcode";
            this.autoprintbarcode.Size = new System.Drawing.Size(269, 37);
            this.autoprintbarcode.TabIndex = 48;
            this.autoprintbarcode.Text = "Auto Print Barcode";
            this.autoprintbarcode.UseVisualStyleBackColor = true;
            this.autoprintbarcode.CheckedChanged += new System.EventHandler(this.autoprintbarcode_CheckedChanged);
            // 
            // txtprodcat
            // 
            this.txtprodcat.Enabled = false;
            this.txtprodcat.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprodcat.FormattingEnabled = true;
            this.txtprodcat.Location = new System.Drawing.Point(648, 131);
            this.txtprodcat.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtprodcat.Name = "txtprodcat";
            this.txtprodcat.Size = new System.Drawing.Size(636, 47);
            this.txtprodcat.TabIndex = 45;
            this.txtprodcat.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(398, 198);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 39);
            this.label4.TabIndex = 8;
            this.label4.Text = "PRODUCT:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(398, 325);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 39);
            this.label1.TabIndex = 13;
            this.label1.Text = "BARCODE:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(1606, 239);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(183, 33);
            this.label6.TabIndex = 33;
            this.label6.Text = "BATCH CODE:";
            // 
            // txtshipmentno
            // 
            this.txtshipmentno.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtshipmentno.FormattingEnabled = true;
            this.txtshipmentno.Location = new System.Drawing.Point(1866, 284);
            this.txtshipmentno.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtshipmentno.Name = "txtshipmentno";
            this.txtshipmentno.Size = new System.Drawing.Size(154, 41);
            this.txtshipmentno.TabIndex = 46;
            this.txtshipmentno.Click += new System.EventHandler(this.txtshipmentno_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Tahoma", 10F);
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(1612, 379);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(234, 37);
            this.checkBox1.TabIndex = 47;
            this.checkBox1.Text = "Auto GetWeight";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtbatchcode
            // 
            this.txtbatchcode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtbatchcode.ForeColor = System.Drawing.Color.Blue;
            this.txtbatchcode.Location = new System.Drawing.Point(1866, 234);
            this.txtbatchcode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtbatchcode.MaxLength = 6;
            this.txtbatchcode.Name = "txtbatchcode";
            this.txtbatchcode.ReadOnly = true;
            this.txtbatchcode.Size = new System.Drawing.Size(154, 40);
            this.txtbatchcode.TabIndex = 34;
            this.txtbatchcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbatchcode_KeyDown);
            // 
            // txtavailableqty
            // 
            this.txtavailableqty.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtavailableqty.ForeColor = System.Drawing.Color.Blue;
            this.txtavailableqty.Location = new System.Drawing.Point(1868, 53);
            this.txtavailableqty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtavailableqty.MaxLength = 6;
            this.txtavailableqty.Name = "txtavailableqty";
            this.txtavailableqty.Size = new System.Drawing.Size(154, 40);
            this.txtavailableqty.TabIndex = 44;
            this.txtavailableqty.Visible = false;
            // 
            // txtskuno
            // 
            this.txtskuno.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtskuno.ForeColor = System.Drawing.Color.Blue;
            this.txtskuno.Location = new System.Drawing.Point(648, 322);
            this.txtskuno.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtskuno.MaxLength = 40;
            this.txtskuno.Name = "txtskuno";
            this.txtskuno.Size = new System.Drawing.Size(636, 46);
            this.txtskuno.TabIndex = 14;
            this.txtskuno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtskuno_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(1610, 289);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 33);
            this.label9.TabIndex = 45;
            this.label9.Text = "SHIPMENT NO.:";
            // 
            // txtports
            // 
            this.txtports.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtports.FormattingEnabled = true;
            this.txtports.Location = new System.Drawing.Point(1866, 183);
            this.txtports.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtports.Name = "txtports";
            this.txtports.Size = new System.Drawing.Size(154, 41);
            this.txtports.TabIndex = 36;
            this.txtports.Text = "COM1";
            this.txtports.SelectedIndexChanged += new System.EventHandler(this.txtports_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(398, 139);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 39);
            this.label5.TabIndex = 44;
            this.label5.Text = "CATEGORY:";
            this.label5.Visible = false;
            // 
            // txtprodcode
            // 
            this.txtprodcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprodcode.ForeColor = System.Drawing.Color.Blue;
            this.txtprodcode.Location = new System.Drawing.Point(2127, 351);
            this.txtprodcode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtprodcode.MaxLength = 6;
            this.txtprodcode.Name = "txtprodcode";
            this.txtprodcode.Size = new System.Drawing.Size(152, 98);
            this.txtprodcode.TabIndex = 32;
            this.txtprodcode.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(1606, 191);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 33);
            this.label3.TabIndex = 37;
            this.label3.Text = "COM PORT:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(1606, 58);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(178, 33);
            this.label8.TabIndex = 44;
            this.label8.Text = "Available Qty:";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(390, 47);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(813, 83);
            this.label7.TabIndex = 40;
            this.label7.Text = "ENZO\'S MEAT MARKET";
            // 
            // isprimalcuts
            // 
            this.isprimalcuts.AutoSize = true;
            this.isprimalcuts.Font = new System.Drawing.Font("Tahoma", 10F);
            this.isprimalcuts.ForeColor = System.Drawing.Color.Black;
            this.isprimalcuts.Location = new System.Drawing.Point(1296, 202);
            this.isprimalcuts.Margin = new System.Windows.Forms.Padding(6);
            this.isprimalcuts.Name = "isprimalcuts";
            this.isprimalcuts.Size = new System.Drawing.Size(191, 37);
            this.isprimalcuts.TabIndex = 43;
            this.isprimalcuts.Text = "isPrimalCuts";
            this.isprimalcuts.UseVisualStyleBackColor = true;
            this.isprimalcuts.Visible = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 486);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(2366, 647);
            this.groupControl2.TabIndex = 40;
            this.groupControl2.Text = "Primal Cut Items";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.gridControl1.Location = new System.Drawing.Point(3, 45);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(2360, 599);
            this.gridControl1.TabIndex = 97;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.DetailHeight = 673;
            this.gridView1.FixedLineWidth = 3;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(1606, 139);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 33);
            this.label10.TabIndex = 58;
            this.label10.Text = "PALLETNO:";
            // 
            // txtpalletno
            // 
            this.txtpalletno.Location = new System.Drawing.Point(1866, 132);
            this.txtpalletno.Name = "txtpalletno";
            this.txtpalletno.Size = new System.Drawing.Size(154, 40);
            this.txtpalletno.TabIndex = 59;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(1610, 335);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(202, 33);
            this.label11.TabIndex = 60;
            this.label11.Text = "DISPATCH NO.:";
            // 
            // txtdispatchno
            // 
            this.txtdispatchno.Location = new System.Drawing.Point(1866, 332);
            this.txtdispatchno.Name = "txtdispatchno";
            this.txtdispatchno.Size = new System.Drawing.Size(154, 40);
            this.txtdispatchno.TabIndex = 61;
            // 
            // AddPrimalCutInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(2366, 1133);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "AddPrimalCutInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddPrimalCutInventory";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddPrimalCutInventory_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddPrimalCutInventory_FormClosed);
            this.Load += new System.EventHandler(this.AddPrimalCutInventory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtsrchprod.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpalletno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtdispatchno.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.Ports.SerialPort serialPort1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtweight;
        private System.Windows.Forms.CheckBox autoprintbarcode;
        private System.Windows.Forms.ComboBox txtprodcat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.ComboBox txtshipmentno;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtavailableqty;
        private System.Windows.Forms.TextBox txtskuno;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox txtports;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtprodcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox isprimalcuts;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton buttonSaveAndTransfer;
        private DevExpress.XtraEditors.SimpleButton buttonClear;
        private DevExpress.XtraEditors.SimpleButton buttonCancel;
        private DevExpress.XtraEditors.SimpleButton buttonAdd;
        private DevExpress.XtraEditors.SimpleButton buttonGetWeight;
        private DevExpress.XtraEditors.SimpleButton buttonPrintBarcode;
        private DevExpress.XtraEditors.SimpleButton buttonSearchItems;
        private DevExpress.XtraEditors.SearchLookUpEdit txtsrchprod;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        public System.Windows.Forms.TextBox txtbatchcode;
        private System.Windows.Forms.Label label10;
        private DevExpress.XtraEditors.TextEdit txtpalletno;
        public System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.TextEdit txtdispatchno;
    }
}