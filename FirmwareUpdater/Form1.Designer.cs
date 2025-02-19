namespace FirmwareUpdater
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox_com_ports = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_file_status = new System.Windows.Forms.Label();
            this.label_firmware_size = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pic_connect = new System.Windows.Forms.PictureBox();
            this.pic_disconnect = new System.Windows.Forms.PictureBox();
            this.pic_firm_off = new System.Windows.Forms.PictureBox();
            this.pic_firm_g = new System.Windows.Forms.PictureBox();
            this.pic_firm_r = new System.Windows.Forms.PictureBox();
            this.pic_firm_y = new System.Windows.Forms.PictureBox();
            this.pic_update = new System.Windows.Forms.PictureBox();
            this.pic_firm_v = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_connect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_disconnect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_off)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_g)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_r)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_update)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_v)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox_com_ports
            // 
            this.comboBox_com_ports.BackColor = System.Drawing.Color.Gray;
            this.comboBox_com_ports.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox_com_ports.FormattingEnabled = true;
            this.comboBox_com_ports.Location = new System.Drawing.Point(12, 17);
            this.comboBox_com_ports.Name = "comboBox_com_ports";
            this.comboBox_com_ports.Size = new System.Drawing.Size(249, 21);
            this.comboBox_com_ports.TabIndex = 0;
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_file_status
            // 
            this.label_file_status.AutoSize = true;
            this.label_file_status.BackColor = System.Drawing.Color.Transparent;
            this.label_file_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_file_status.ForeColor = System.Drawing.Color.White;
            this.label_file_status.Location = new System.Drawing.Point(9, 181);
            this.label_file_status.Name = "label_file_status";
            this.label_file_status.Size = new System.Drawing.Size(120, 16);
            this.label_file_status.TabIndex = 77;
            this.label_file_status.Text = "No new firmware";
            // 
            // label_firmware_size
            // 
            this.label_firmware_size.AutoSize = true;
            this.label_firmware_size.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_firmware_size.ForeColor = System.Drawing.Color.White;
            this.label_firmware_size.Location = new System.Drawing.Point(9, 197);
            this.label_firmware_size.Name = "label_firmware_size";
            this.label_firmware_size.Size = new System.Drawing.Size(35, 13);
            this.label_firmware_size.TabIndex = 83;
            this.label_firmware_size.Text = "Size:";
            // 
            // timer2
            // 
            this.timer2.Interval = 3000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.Gray;
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.progressBar1.Location = new System.Drawing.Point(12, 156);
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(249, 15);
            this.progressBar1.TabIndex = 84;
            // 
            // pic_connect
            // 
            this.pic_connect.Image = ((System.Drawing.Image)(resources.GetObject("pic_connect.Image")));
            this.pic_connect.Location = new System.Drawing.Point(12, 49);
            this.pic_connect.Name = "pic_connect";
            this.pic_connect.Size = new System.Drawing.Size(190, 45);
            this.pic_connect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_connect.TabIndex = 85;
            this.pic_connect.TabStop = false;
            this.pic_connect.Click += new System.EventHandler(this.pic_connect_Click);
            // 
            // pic_disconnect
            // 
            this.pic_disconnect.Image = ((System.Drawing.Image)(resources.GetObject("pic_disconnect.Image")));
            this.pic_disconnect.Location = new System.Drawing.Point(12, 49);
            this.pic_disconnect.Name = "pic_disconnect";
            this.pic_disconnect.Size = new System.Drawing.Size(190, 45);
            this.pic_disconnect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_disconnect.TabIndex = 86;
            this.pic_disconnect.TabStop = false;
            this.pic_disconnect.Click += new System.EventHandler(this.pic_disconnect_Click);
            // 
            // pic_firm_off
            // 
            this.pic_firm_off.Image = ((System.Drawing.Image)(resources.GetObject("pic_firm_off.Image")));
            this.pic_firm_off.Location = new System.Drawing.Point(12, 100);
            this.pic_firm_off.Name = "pic_firm_off";
            this.pic_firm_off.Size = new System.Drawing.Size(249, 45);
            this.pic_firm_off.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_firm_off.TabIndex = 87;
            this.pic_firm_off.TabStop = false;
            this.pic_firm_off.Click += new System.EventHandler(this.pic_firm_off_Click);
            // 
            // pic_firm_g
            // 
            this.pic_firm_g.Image = ((System.Drawing.Image)(resources.GetObject("pic_firm_g.Image")));
            this.pic_firm_g.Location = new System.Drawing.Point(12, 100);
            this.pic_firm_g.Name = "pic_firm_g";
            this.pic_firm_g.Size = new System.Drawing.Size(249, 45);
            this.pic_firm_g.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_firm_g.TabIndex = 88;
            this.pic_firm_g.TabStop = false;
            this.pic_firm_g.Click += new System.EventHandler(this.pic_firm_g_Click);
            // 
            // pic_firm_r
            // 
            this.pic_firm_r.Image = ((System.Drawing.Image)(resources.GetObject("pic_firm_r.Image")));
            this.pic_firm_r.Location = new System.Drawing.Point(12, 100);
            this.pic_firm_r.Name = "pic_firm_r";
            this.pic_firm_r.Size = new System.Drawing.Size(249, 45);
            this.pic_firm_r.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_firm_r.TabIndex = 89;
            this.pic_firm_r.TabStop = false;
            this.pic_firm_r.Click += new System.EventHandler(this.pic_firm_r_Click);
            // 
            // pic_firm_y
            // 
            this.pic_firm_y.Image = ((System.Drawing.Image)(resources.GetObject("pic_firm_y.Image")));
            this.pic_firm_y.Location = new System.Drawing.Point(12, 100);
            this.pic_firm_y.Name = "pic_firm_y";
            this.pic_firm_y.Size = new System.Drawing.Size(249, 45);
            this.pic_firm_y.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_firm_y.TabIndex = 90;
            this.pic_firm_y.TabStop = false;
            // 
            // pic_update
            // 
            this.pic_update.Image = ((System.Drawing.Image)(resources.GetObject("pic_update.Image")));
            this.pic_update.Location = new System.Drawing.Point(208, 49);
            this.pic_update.Name = "pic_update";
            this.pic_update.Size = new System.Drawing.Size(53, 45);
            this.pic_update.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_update.TabIndex = 91;
            this.pic_update.TabStop = false;
            this.pic_update.Click += new System.EventHandler(this.pic_update_Click);
            // 
            // pic_firm_v
            // 
            this.pic_firm_v.Image = ((System.Drawing.Image)(resources.GetObject("pic_firm_v.Image")));
            this.pic_firm_v.Location = new System.Drawing.Point(12, 100);
            this.pic_firm_v.Name = "pic_firm_v";
            this.pic_firm_v.Size = new System.Drawing.Size(249, 45);
            this.pic_firm_v.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_firm_v.TabIndex = 92;
            this.pic_firm_v.TabStop = false;
            this.pic_firm_v.Click += new System.EventHandler(this.pic_firm_v_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(272, 221);
            this.Controls.Add(this.pic_firm_v);
            this.Controls.Add(this.pic_update);
            this.Controls.Add(this.comboBox_com_ports);
            this.Controls.Add(this.pic_firm_y);
            this.Controls.Add(this.pic_firm_r);
            this.Controls.Add(this.pic_firm_g);
            this.Controls.Add(this.pic_firm_off);
            this.Controls.Add(this.pic_disconnect);
            this.Controls.Add(this.pic_connect);
            this.Controls.Add(this.label_firmware_size);
            this.Controls.Add(this.label_file_status);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Firmware Updater";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_connect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_disconnect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_off)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_g)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_r)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_update)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_firm_v)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_com_ports;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_file_status;
        private System.Windows.Forms.Label label_firmware_size;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pic_connect;
        private System.Windows.Forms.PictureBox pic_disconnect;
        private System.Windows.Forms.PictureBox pic_firm_off;
        private System.Windows.Forms.PictureBox pic_firm_g;
        private System.Windows.Forms.PictureBox pic_firm_r;
        private System.Windows.Forms.PictureBox pic_firm_y;
        private System.Windows.Forms.PictureBox pic_update;
        private System.Windows.Forms.PictureBox pic_firm_v;
    }
}

