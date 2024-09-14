namespace ApiServer.WindowsForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnRunMqtt = new Button();
            lblMain = new Label();
            btnCloseMqtt = new Button();
            btnStart = new Button();
            btnStop = new Button();
            SuspendLayout();
            // 
            // btnRunMqtt
            // 
            btnRunMqtt.Location = new Point(161, 88);
            btnRunMqtt.Name = "btnRunMqtt";
            btnRunMqtt.Size = new Size(100, 57);
            btnRunMqtt.TabIndex = 0;
            btnRunMqtt.Text = "Run MQTT";
            btnRunMqtt.UseVisualStyleBackColor = true;
            btnRunMqtt.Click += btnRunMqtt_Click;
            // 
            // lblMain
            // 
            lblMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMain.AutoSize = true;
            lblMain.Font = new Font("Segoe UI", 20F);
            lblMain.Location = new Point(41, 21);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(471, 37);
            lblMain.TabIndex = 1;
            lblMain.Text = "Server MQTT + ASP.NET Core Web API";
            // 
            // btnCloseMqtt
            // 
            btnCloseMqtt.Location = new Point(296, 88);
            btnCloseMqtt.Name = "btnCloseMqtt";
            btnCloseMqtt.Size = new Size(100, 57);
            btnCloseMqtt.TabIndex = 2;
            btnCloseMqtt.Text = "Close MQTT";
            btnCloseMqtt.UseVisualStyleBackColor = true;
            btnCloseMqtt.Click += btnCloseMqtt_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(161, 171);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(100, 57);
            btnStart.TabIndex = 3;
            btnStart.Text = "Sub";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(296, 171);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(100, 57);
            btnStop.TabIndex = 4;
            btnStop.Text = "uSub";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 318);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(btnCloseMqtt);
            Controls.Add(lblMain);
            Controls.Add(btnRunMqtt);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRunMqtt;
        private Label lblMain;
        private Button btnCloseMqtt;
        private Button btnStart;
        private Button btnStop;
    }
}
