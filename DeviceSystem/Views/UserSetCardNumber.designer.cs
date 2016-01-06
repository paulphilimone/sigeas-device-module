namespace mz.betainteractive.sigeas.DeviceSystem.Views {

    partial class UserSetCardNumber {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSetCardNumber));
            this.picHand = new System.Windows.Forms.PictureBox();
            this.labMsg = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabelSerialNumber = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picHand)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picHand
            // 
            this.picHand.BackColor = System.Drawing.Color.Transparent;
            this.picHand.Image = global::mz.betainteractive.sigeas.Properties.Resources.credit_card_and_lock;
            this.picHand.Location = new System.Drawing.Point(14, 100);
            this.picHand.Name = "picHand";
            this.picHand.Size = new System.Drawing.Size(183, 144);
            this.picHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHand.TabIndex = 0;
            this.picHand.TabStop = false;
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
            this.labMsg.Text = "Passe o cartão no biométrico";
            this.labMsg.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Número do cartão";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(5, 258);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(258, 101);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SpringGreen;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.LabelSerialNumber);
            this.panel2.Location = new System.Drawing.Point(7, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(243, 28);
            this.panel2.TabIndex = 24;
            // 
            // LabelSerialNumber
            // 
            this.LabelSerialNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelSerialNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LabelSerialNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSerialNumber.Location = new System.Drawing.Point(0, 0);
            this.LabelSerialNumber.Name = "LabelSerialNumber";
            this.LabelSerialNumber.Size = new System.Drawing.Size(239, 24);
            this.LabelSerialNumber.TabIndex = 0;
            this.LabelSerialNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.labMsg);
            this.panel1.Location = new System.Drawing.Point(-2, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(335, 54);
            this.panel1.TabIndex = 20;
            // 
            // btOk
            // 
            this.btOk.Enabled = false;
            this.btOk.Location = new System.Drawing.Point(279, 266);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(95, 23);
            this.btOk.TabIndex = 22;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(279, 297);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(95, 23);
            this.btCancel.TabIndex = 23;
            this.btCancel.Text = "Cancelar";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // UserSetCardNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(386, 390);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.picHand);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserSetCardNumber";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Número de cartão";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSetFingerprint_FormClosing);            
            ((System.ComponentModel.ISupportInitialize)(this.picHand)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picHand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        public System.Windows.Forms.Label labMsg;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LabelSerialNumber;
    }
}