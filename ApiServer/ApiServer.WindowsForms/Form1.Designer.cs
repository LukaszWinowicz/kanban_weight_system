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
            SuspendLayout();
            // 
            // btnRunMqtt
            // 
            btnRunMqtt.Location = new Point(111, 97);
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
            lblMain.Location = new Point(186, 18);
            lblMain.Name = "lblMain";
            lblMain.Size = new Size(471, 37);
            lblMain.TabIndex = 1;
            lblMain.Text = "Server MQTT + ASP.NET Core Web API";
            // 
            // btnCloseMqtt
            // 
            btnCloseMqtt.Location = new Point(111, 182);
            btnCloseMqtt.Name = "btnCloseMqtt";
            btnCloseMqtt.Size = new Size(100, 57);
            btnCloseMqtt.TabIndex = 2;
            btnCloseMqtt.Text = "Close MQTT";
            btnCloseMqtt.UseVisualStyleBackColor = true;
            btnCloseMqtt.Click += btnCloseMqtt_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
    }
}
