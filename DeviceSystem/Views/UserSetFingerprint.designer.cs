namespace mz.betainteractive.sigeas.DeviceSystem.Views {
    partial class UserSetFingerprint {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSetFingerprint));
            this.picHand = new System.Windows.Forms.PictureBox();
            this.labMsg = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panelNotReg = new System.Windows.Forms.Panel();
            this.panelReg = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkBox = new System.Windows.Forms.CheckBox();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picHand)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picHand
            // 
            this.picHand.BackColor = System.Drawing.Color.Transparent;
            this.picHand.Image = ((System.Drawing.Image)(resources.GetObject("picHand.Image")));
            this.picHand.Location = new System.Drawing.Point(6, 215);
            this.picHand.Name = "picHand";
            this.picHand.Size = new System.Drawing.Size(257, 150);
            this.picHand.TabIndex = 0;
            this.picHand.TabStop = false;
            this.picHand.Paint += new System.Windows.Forms.PaintEventHandler(this.picHand_Paint);
            this.picHand.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picHand_MouseClick);
            this.picHand.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picHand_MouseMove);
            // 
            // labMsg
            // 
            this.labMsg.BackColor = System.Drawing.Color.Transparent;
            this.labMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labMsg.Font = new System.Drawing.Font("Gill Sans MT Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labMsg.ForeColor = System.Drawing.Color.Aquamarine;
            this.labMsg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labMsg.Location = new System.Drawing.Point(8, 11);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(257, 31);
            this.labMsg.TabIndex = 11;
            this.labMsg.Text = "Selecione o dedo";
            this.labMsg.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(48, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "Dedo não registrado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(48, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Dedo registrado";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.panelNotReg);
            this.groupBox3.Controls.Add(this.panelReg);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(9, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 107);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Fuchsia;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(21, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(23, 22);
            this.panel2.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(48, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Dedo selecionado";
            // 
            // panelNotReg
            // 
            this.panelNotReg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNotReg.Location = new System.Drawing.Point(21, 44);
            this.panelNotReg.Name = "panelNotReg";
            this.panelNotReg.Size = new System.Drawing.Size(23, 22);
            this.panelNotReg.TabIndex = 19;
            // 
            // panelReg
            // 
            this.panelReg.BackColor = System.Drawing.Color.SeaGreen;
            this.panelReg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelReg.Location = new System.Drawing.Point(21, 15);
            this.panelReg.Name = "panelReg";
            this.panelReg.Size = new System.Drawing.Size(23, 22);
            this.panelReg.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.labMsg);
            this.panel1.Location = new System.Drawing.Point(-2, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 54);
            this.panel1.TabIndex = 20;
            // 
            // chkBox
            // 
            this.chkBox.AutoSize = true;
            this.chkBox.BackColor = System.Drawing.Color.Transparent;
            this.chkBox.Location = new System.Drawing.Point(296, 198);
            this.chkBox.Name = "chkBox";
            this.chkBox.Size = new System.Drawing.Size(78, 17);
            this.chkBox.TabIndex = 21;
            this.chkBox.Text = "Rec Points";
            this.chkBox.UseVisualStyleBackColor = false;
            this.chkBox.Visible = false;
            // 
            // btOk
            // 
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(290, 313);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 22;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(290, 342);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 23;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // UserSetFingerprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(386, 390);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.chkBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.picHand);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserSetFingerprint";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Imprenssão digital";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSetFingerprint_FormClosing);            
            ((System.ComponentModel.ISupportInitialize)(this.picHand)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkBox;
        private System.Windows.Forms.Panel panelReg;
        private System.Windows.Forms.Panel panelNotReg;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        public System.Windows.Forms.Label labMsg;
    }
}