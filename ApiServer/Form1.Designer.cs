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
            SuspendLayout();
            // 
            // btnLoadDb
            // 
            btnLoadDb.Location = new Point(379, 88);
            btnLoadDb.Name = "btnLoadDb";
            btnLoadDb.Size = new Size(91, 42);
            btnLoadDb.TabIndex = 0;
            btnLoadDb.Text = "Load Db";
            btnLoadDb.UseVisualStyleBackColor = true;
            btnLoadDb.Click += btnLoadDb_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLoadDb);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoadDb;
    }
}
