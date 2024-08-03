namespace ApiServer
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
            btnLoadDb = new Button();
            btnRunMqtt = new Button();
            btnCloseMqtt = new Button();
            btnSub = new Button();
            btnCmd = new Button();
            SuspendLayout();
            // 
            // btnLoadDb
            // 
            btnLoadDb.Location = new Point(633, 27);
            btnLoadDb.Name = "btnLoadDb";
            btnLoadDb.Size = new Size(91, 42);
            btnLoadDb.TabIndex = 0;
            btnLoadDb.Text = "Load Db";
            btnLoadDb.UseVisualStyleBackColor = true;
            btnLoadDb.Click += btnLoadDb_Click;
            // 
            // btnRunMqtt
            // 
            btnRunMqtt.Location = new Point(157, 27);
            btnRunMqtt.Name = "btnRunMqtt";
            btnRunMqtt.Size = new Size(91, 42);
            btnRunMqtt.TabIndex = 1;
            btnRunMqtt.Text = "Run MQTT";
            btnRunMqtt.UseVisualStyleBackColor = true;
            btnRunMqtt.Click += btnRunMqtt_Click;
            // 
            // btnCloseMqtt
            // 
            btnCloseMqtt.Location = new Point(157, 124);
            btnCloseMqtt.Name = "btnCloseMqtt";
            btnCloseMqtt.Size = new Size(91, 42);
            btnCloseMqtt.TabIndex = 2;
            btnCloseMqtt.Text = "Close MQTT";
            btnCloseMqtt.UseVisualStyleBackColor = true;
            btnCloseMqtt.Click += btnCloseMqtt_Click;
            // 
            // btnSub
            // 
            btnSub.Location = new Point(157, 208);
            btnSub.Name = "btnSub";
            btnSub.Size = new Size(91, 42);
            btnSub.TabIndex = 3;
            btnSub.Text = "Sub";
            btnSub.UseVisualStyleBackColor = true;
            btnSub.Click += btnSub_Click;
            // 
            // btnCmd
            // 
            btnCmd.Location = new Point(633, 208);
            btnCmd.Name = "btnCmd";
            btnCmd.Size = new Size(91, 42);
            btnCmd.TabIndex = 4;
            btnCmd.Text = "CMD";
            btnCmd.UseVisualStyleBackColor = true;
            btnCmd.Click += btnCmd_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnCmd);
            Controls.Add(btnSub);
            Controls.Add(btnCloseMqtt);
            Controls.Add(btnRunMqtt);
            Controls.Add(btnLoadDb);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoadDb;
        private Button btnRunMqtt;
        private Button btnCloseMqtt;
        private Button btnSub;
        private Button btnCmd;
    }
}
