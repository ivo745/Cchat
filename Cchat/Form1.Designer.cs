namespace Cchat
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
                streamWriter.Dispose();
                streamReader.Dispose();
                client.Close();
                binaryReader.Dispose();
                binaryWriter.Dispose();
                cefSettings.Dispose();
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
            this.button_start = new System.Windows.Forms.Button();
            this.button_connect = new System.Windows.Forms.Button();
            this.startServerIPBox = new System.Windows.Forms.TextBox();
            this.startServerPortBox = new System.Windows.Forms.TextBox();
            this.connectServerIPBox = new System.Windows.Forms.TextBox();
            this.connectServerPortBox = new System.Windows.Forms.TextBox();
            this.dataReceiver = new System.ComponentModel.BackgroundWorker();
            this.dataSender = new System.ComponentModel.BackgroundWorker();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(12, 41);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(273, 21);
            this.button_start.TabIndex = 1;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(12, 94);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(273, 21);
            this.button_connect.TabIndex = 2;
            this.button_connect.Text = "Connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // startServerIPBox
            // 
            this.startServerIPBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.startServerIPBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.startServerIPBox.Location = new System.Drawing.Point(12, 15);
            this.startServerIPBox.MaxLength = 15;
            this.startServerIPBox.Name = "startServerIPBox";
            this.startServerIPBox.ReadOnly = true;
            this.startServerIPBox.Size = new System.Drawing.Size(133, 20);
            this.startServerIPBox.TabIndex = 5;
            this.startServerIPBox.Text = "IPv4 Address";
            // 
            // startServerPortBox
            // 
            this.startServerPortBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.startServerPortBox.Location = new System.Drawing.Point(151, 15);
            this.startServerPortBox.MaxLength = 5;
            this.startServerPortBox.Name = "startServerPortBox";
            this.startServerPortBox.Size = new System.Drawing.Size(134, 20);
            this.startServerPortBox.TabIndex = 6;
            this.startServerPortBox.Text = "Port Number";
            // 
            // connectServerIPBox
            // 
            this.connectServerIPBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.connectServerIPBox.Location = new System.Drawing.Point(12, 68);
            this.connectServerIPBox.MaxLength = 15;
            this.connectServerIPBox.Name = "connectServerIPBox";
            this.connectServerIPBox.Size = new System.Drawing.Size(133, 20);
            this.connectServerIPBox.TabIndex = 8;
            this.connectServerIPBox.Text = "IPv4 Address";
            // 
            // connectServerPortBox
            // 
            this.connectServerPortBox.ForeColor = System.Drawing.SystemColors.GrayText;
            this.connectServerPortBox.Location = new System.Drawing.Point(151, 68);
            this.connectServerPortBox.MaxLength = 5;
            this.connectServerPortBox.Name = "connectServerPortBox";
            this.connectServerPortBox.Size = new System.Drawing.Size(134, 20);
            this.connectServerPortBox.TabIndex = 7;
            this.connectServerPortBox.Text = "Port Number";
            // 
            // dataReceiver
            // 
            this.dataReceiver.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataReceiver_DoWork);
            // 
            // dataSender
            // 
            this.dataSender.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataSender_DoWork);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Location = new System.Drawing.Point(12, 121);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(273, 21);
            this.button_disconnect.TabIndex = 9;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 509);
            this.textBox1.MaxLength = 100;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(248, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(12, 382);
            this.chatBox.MaxLength = 1024;
            this.chatBox.Multiline = true;
            this.chatBox.Name = "chatBox";
            this.chatBox.ReadOnly = true;
            this.chatBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatBox.Size = new System.Drawing.Size(273, 119);
            this.chatBox.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 201);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(273, 227);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 509);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(19, 20);
            this.button1.TabIndex = 15;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(233, 344);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 32);
            this.button2.TabIndex = 16;
            this.button2.Text = "Restart";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 541);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.connectServerIPBox);
            this.Controls.Add(this.connectServerPortBox);
            this.Controls.Add(this.startServerPortBox);
            this.Controls.Add(this.startServerIPBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.button_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Cchat";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.TextBox startServerIPBox;
        private System.Windows.Forms.TextBox startServerPortBox;
        private System.Windows.Forms.TextBox connectServerIPBox;
        private System.Windows.Forms.TextBox connectServerPortBox;
        private System.ComponentModel.BackgroundWorker dataReceiver;
        private System.ComponentModel.BackgroundWorker dataSender;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox chatBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
    }
}