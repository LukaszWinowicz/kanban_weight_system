﻿namespace Server
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
            button1 = new Button();
            closeBtn = new Button();
            subBtn = new Button();
            cmdBtn = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(121, 39);
            button1.Name = "button1";
            button1.Size = new Size(97, 45);
            button1.TabIndex = 0;
            button1.Text = "Run MQTT";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // closeBtn
            // 
            closeBtn.Location = new Point(121, 118);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(97, 45);
            closeBtn.TabIndex = 1;
            closeBtn.Text = "Close";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += closeBtn_Click;
            // 
            // subBtn
            // 
            subBtn.Location = new Point(121, 198);
            subBtn.Name = "subBtn";
            subBtn.Size = new Size(97, 45);
            subBtn.TabIndex = 2;
            subBtn.Text = "Sub";
            subBtn.UseVisualStyleBackColor = true;
            subBtn.Click += subBtn_Click;
            // 
            // cmdBtn
            // 
            cmdBtn.Location = new Point(644, 39);
            cmdBtn.Name = "cmdBtn";
            cmdBtn.Size = new Size(97, 45);
            cmdBtn.TabIndex = 3;
            cmdBtn.Text = "CMD";
            cmdBtn.UseVisualStyleBackColor = true;
            cmdBtn.Click += cmdBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmdBtn);
            Controls.Add(subBtn);
            Controls.Add(closeBtn);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button closeBtn;
        private Button subBtn;
        private Button cmdBtn;
    }
}