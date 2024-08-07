namespace DFM_Server
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
            btnPortNumber = new Button();
            lblMainTitle = new Label();
            lblIps = new Label();
            SuspendLayout();
            // 
            // btnPortNumber
            // 
            btnPortNumber.Font = new Font("Segoe UI", 14F);
            btnPortNumber.Location = new Point(125, 338);
            btnPortNumber.Name = "btnPortNumber";
            btnPortNumber.Size = new Size(386, 62);
            btnPortNumber.TabIndex = 0;
            btnPortNumber.Text = Strings.mainConnection;
            btnPortNumber.UseVisualStyleBackColor = true;
            btnPortNumber.Click += btnPortNumber_Click;
            // 
            // lblMainTitle
            // 
            lblMainTitle.AutoSize = true;
            lblMainTitle.Font = new Font("Segoe UI", 14F);
            lblMainTitle.Location = new Point(247, 24);
            lblMainTitle.Name = "lblMainTitle";
            lblMainTitle.Size = new Size(141, 25);
            lblMainTitle.TabIndex = 2;
            lblMainTitle.Text = "Connected IPs :";
            // 
            // lblIps
            // 
            lblIps.AutoSize = true;
            lblIps.Font = new Font("Segoe UI", 12F);
            lblIps.Location = new Point(22, 67);
            lblIps.Name = "lblIps";
            lblIps.Size = new Size(0, 21);
            lblIps.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 427);
            Controls.Add(lblIps);
            Controls.Add(lblMainTitle);
            Controls.Add(btnPortNumber);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DFM Remote Control Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPortNumber;
        private Label lblMainTitle;
        private Label lblIps;
    }
}
