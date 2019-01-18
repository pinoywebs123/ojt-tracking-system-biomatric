namespace ojt
{
    partial class Form1
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
            this.picFPImg = new System.Windows.Forms.PictureBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.txtname = new System.Windows.Forms.Label();
            this.txtleft = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.btnAdminLogin = new System.Windows.Forms.Button();
            this.groupInfo = new System.Windows.Forms.GroupBox();
            this.btnDtr = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.btnEmergency = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).BeginInit();
            this.groupInfo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picFPImg
            // 
            this.picFPImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.picFPImg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picFPImg.Location = new System.Drawing.Point(15, 30);
            this.picFPImg.Name = "picFPImg";
            this.picFPImg.Size = new System.Drawing.Size(267, 282);
            this.picFPImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFPImg.TabIndex = 9;
            this.picFPImg.TabStop = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelStatus.Location = new System.Drawing.Point(357, 417);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(40, 13);
            this.labelStatus.TabIndex = 10;
            this.labelStatus.Text = "Status:";
            // 
            // txtname
            // 
            this.txtname.AutoSize = true;
            this.txtname.BackColor = System.Drawing.Color.Navy;
            this.txtname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtname.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtname.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtname.Location = new System.Drawing.Point(74, 118);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(76, 27);
            this.txtname.TabIndex = 11;
            this.txtname.Text = "Name:";
            // 
            // txtleft
            // 
            this.txtleft.AutoSize = true;
            this.txtleft.BackColor = System.Drawing.Color.Navy;
            this.txtleft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtleft.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtleft.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.txtleft.Location = new System.Drawing.Point(74, 182);
            this.txtleft.Name = "txtleft";
            this.txtleft.Size = new System.Drawing.Size(103, 27);
            this.txtleft.TabIndex = 12;
            this.txtleft.Text = "Time Left";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(201, 116);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(163, 29);
            this.txtFullName.TabIndex = 13;
            // 
            // txtTime
            // 
            this.txtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.Location = new System.Drawing.Point(201, 178);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(163, 29);
            this.txtTime.TabIndex = 14;
            // 
            // btnAdminLogin
            // 
            this.btnAdminLogin.BackColor = System.Drawing.Color.Navy;
            this.btnAdminLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminLogin.Location = new System.Drawing.Point(698, 408);
            this.btnAdminLogin.Name = "btnAdminLogin";
            this.btnAdminLogin.Size = new System.Drawing.Size(90, 30);
            this.btnAdminLogin.TabIndex = 15;
            this.btnAdminLogin.Text = "Sign-In";
            this.btnAdminLogin.UseVisualStyleBackColor = false;
            this.btnAdminLogin.Click += new System.EventHandler(this.btnAdminLogin_Click);
            // 
            // groupInfo
            // 
            this.groupInfo.Controls.Add(this.btnDtr);
            this.groupInfo.Controls.Add(this.txtTime);
            this.groupInfo.Controls.Add(this.txtFullName);
            this.groupInfo.Controls.Add(this.txtleft);
            this.groupInfo.Controls.Add(this.txtname);
            this.groupInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupInfo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupInfo.Location = new System.Drawing.Point(313, 16);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(428, 311);
            this.groupInfo.TabIndex = 16;
            this.groupInfo.TabStop = false;
            this.groupInfo.Text = "OJT Informations";
            // 
            // btnDtr
            // 
            this.btnDtr.BackColor = System.Drawing.Color.Navy;
            this.btnDtr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDtr.Location = new System.Drawing.Point(201, 239);
            this.btnDtr.Name = "btnDtr";
            this.btnDtr.Size = new System.Drawing.Size(163, 30);
            this.btnDtr.TabIndex = 20;
            this.btnDtr.Text = "View DTR";
            this.btnDtr.UseVisualStyleBackColor = false;
            this.btnDtr.Click += new System.EventHandler(this.btnDtr_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupInfo);
            this.panel1.Location = new System.Drawing.Point(12, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 355);
            this.panel1.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picFPImg);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(10, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 324);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thumb Print";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(626, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(39, 16);
            this.lblTime.TabIndex = 19;
            this.lblTime.Text = "Time";
            // 
            // btnEmergency
            // 
            this.btnEmergency.BackColor = System.Drawing.Color.Navy;
            this.btnEmergency.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmergency.Location = new System.Drawing.Point(12, 4);
            this.btnEmergency.Name = "btnEmergency";
            this.btnEmergency.Size = new System.Drawing.Size(113, 25);
            this.btnEmergency.TabIndex = 20;
            this.btnEmergency.Text = "Emergency Code";
            this.btnEmergency.UseVisualStyleBackColor = false;
            this.btnEmergency.Click += new System.EventHandler(this.btnEmergency_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(800, 451);
            this.Controls.Add(this.btnEmergency);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAdminLogin);
            this.Controls.Add(this.labelStatus);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OJT Tracking System";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picFPImg)).EndInit();
            this.groupInfo.ResumeLayout(false);
            this.groupInfo.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picFPImg;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label txtname;
        private System.Windows.Forms.Label txtleft;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Button btnAdminLogin;
        private System.Windows.Forms.GroupBox groupInfo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnDtr;
        private System.Windows.Forms.Button btnEmergency;
    }
}

