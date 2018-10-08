namespace AadManualAuth
{
    partial class MainForm
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
            this.UsernameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AuthenticateButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.EnvironmentsGroupBox = new System.Windows.Forms.GroupBox();
            this.ProdCheckBox = new System.Windows.Forms.CheckBox();
            this.BetaCheckBox = new System.Windows.Forms.CheckBox();
            this.QaCheckBox = new System.Windows.Forms.CheckBox();
            this.DevTestCheckBox = new System.Windows.Forms.CheckBox();
            this.EnvironmentTabControl = new System.Windows.Forms.TabControl();
            this.DevTestTab = new System.Windows.Forms.TabPage();
            this.ResultsPanel = new System.Windows.Forms.Panel();
            this.CopyRefreshTokenButton = new System.Windows.Forms.Button();
            this.CopyAccessTokenButton = new System.Windows.Forms.Button();
            this.CopyResponseButton = new System.Windows.Forms.Button();
            this.CopyRefreshTokenAsHeaderButton = new System.Windows.Forms.Button();
            this.CopyAccessTokenAsHeaderButton = new System.Windows.Forms.Button();
            this.ResponseTextBox = new System.Windows.Forms.TextBox();
            this.RefreshTokenTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AccessTokenTextBox = new System.Windows.Forms.TextBox();
            this.ActivityTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.EnvironmentsGroupBox.SuspendLayout();
            this.ResultsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // UsernameTextBox
            // 
            this.UsernameTextBox.Location = new System.Drawing.Point(88, 12);
            this.UsernameTextBox.Name = "UsernameTextBox";
            this.UsernameTextBox.Size = new System.Drawing.Size(217, 20);
            this.UsernameTextBox.TabIndex = 0;
            this.UsernameTextBox.TextChanged += new System.EventHandler(this.UsernameTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // AuthenticateButton
            // 
            this.AuthenticateButton.Enabled = false;
            this.AuthenticateButton.Location = new System.Drawing.Point(230, 92);
            this.AuthenticateButton.Name = "AuthenticateButton";
            this.AuthenticateButton.Size = new System.Drawing.Size(75, 23);
            this.AuthenticateButton.TabIndex = 2;
            this.AuthenticateButton.Text = "Authenticate";
            this.AuthenticateButton.UseVisualStyleBackColor = true;
            this.AuthenticateButton.Click += new System.EventHandler(this.AuthenticateButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password:";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(88, 48);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(217, 20);
            this.PasswordTextBox.TabIndex = 1;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // EnvironmentsGroupBox
            // 
            this.EnvironmentsGroupBox.Controls.Add(this.ProdCheckBox);
            this.EnvironmentsGroupBox.Controls.Add(this.BetaCheckBox);
            this.EnvironmentsGroupBox.Controls.Add(this.QaCheckBox);
            this.EnvironmentsGroupBox.Controls.Add(this.DevTestCheckBox);
            this.EnvironmentsGroupBox.Location = new System.Drawing.Point(82, 87);
            this.EnvironmentsGroupBox.Name = "EnvironmentsGroupBox";
            this.EnvironmentsGroupBox.Size = new System.Drawing.Size(131, 119);
            this.EnvironmentsGroupBox.TabIndex = 3;
            this.EnvironmentsGroupBox.TabStop = false;
            this.EnvironmentsGroupBox.Text = "Environments:";
            // 
            // ProdCheckBox
            // 
            this.ProdCheckBox.AutoSize = true;
            this.ProdCheckBox.Location = new System.Drawing.Point(7, 89);
            this.ProdCheckBox.Name = "ProdCheckBox";
            this.ProdCheckBox.Size = new System.Drawing.Size(48, 17);
            this.ProdCheckBox.TabIndex = 3;
            this.ProdCheckBox.Tag = "prod";
            this.ProdCheckBox.Text = "Prod";
            this.ProdCheckBox.UseVisualStyleBackColor = true;
            this.ProdCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // BetaCheckBox
            // 
            this.BetaCheckBox.AutoSize = true;
            this.BetaCheckBox.Location = new System.Drawing.Point(7, 66);
            this.BetaCheckBox.Name = "BetaCheckBox";
            this.BetaCheckBox.Size = new System.Drawing.Size(48, 17);
            this.BetaCheckBox.TabIndex = 2;
            this.BetaCheckBox.Tag = "beta";
            this.BetaCheckBox.Text = "Beta";
            this.BetaCheckBox.UseVisualStyleBackColor = true;
            this.BetaCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // QaCheckBox
            // 
            this.QaCheckBox.AutoSize = true;
            this.QaCheckBox.Checked = true;
            this.QaCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.QaCheckBox.Location = new System.Drawing.Point(7, 43);
            this.QaCheckBox.Name = "QaCheckBox";
            this.QaCheckBox.Size = new System.Drawing.Size(41, 17);
            this.QaCheckBox.TabIndex = 1;
            this.QaCheckBox.Tag = "qa";
            this.QaCheckBox.Text = "QA";
            this.QaCheckBox.UseVisualStyleBackColor = true;
            this.QaCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // DevTestCheckBox
            // 
            this.DevTestCheckBox.AutoSize = true;
            this.DevTestCheckBox.Checked = true;
            this.DevTestCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DevTestCheckBox.Location = new System.Drawing.Point(7, 20);
            this.DevTestCheckBox.Name = "DevTestCheckBox";
            this.DevTestCheckBox.Size = new System.Drawing.Size(72, 17);
            this.DevTestCheckBox.TabIndex = 0;
            this.DevTestCheckBox.Tag = "dev";
            this.DevTestCheckBox.Text = "Dev/Test";
            this.DevTestCheckBox.UseVisualStyleBackColor = true;
            this.DevTestCheckBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // EnvironmentTabControl
            // 
            this.EnvironmentTabControl.Location = new System.Drawing.Point(334, 12);
            this.EnvironmentTabControl.Name = "EnvironmentTabControl";
            this.EnvironmentTabControl.SelectedIndex = 0;
            this.EnvironmentTabControl.Size = new System.Drawing.Size(604, 460);
            this.EnvironmentTabControl.TabIndex = 13;
            // 
            // DevTestTab
            // 
            this.DevTestTab.Location = new System.Drawing.Point(0, 0);
            this.DevTestTab.Name = "DevTestTab";
            this.DevTestTab.Size = new System.Drawing.Size(200, 100);
            this.DevTestTab.TabIndex = 0;
            // 
            // ResultsPanel
            // 
            this.ResultsPanel.Controls.Add(this.CopyRefreshTokenButton);
            this.ResultsPanel.Controls.Add(this.CopyAccessTokenButton);
            this.ResultsPanel.Controls.Add(this.CopyResponseButton);
            this.ResultsPanel.Controls.Add(this.CopyRefreshTokenAsHeaderButton);
            this.ResultsPanel.Controls.Add(this.CopyAccessTokenAsHeaderButton);
            this.ResultsPanel.Controls.Add(this.ResponseTextBox);
            this.ResultsPanel.Controls.Add(this.RefreshTokenTextBox);
            this.ResultsPanel.Controls.Add(this.label5);
            this.ResultsPanel.Controls.Add(this.label4);
            this.ResultsPanel.Controls.Add(this.label3);
            this.ResultsPanel.Controls.Add(this.AccessTokenTextBox);
            this.ResultsPanel.Location = new System.Drawing.Point(345, 40);
            this.ResultsPanel.Name = "ResultsPanel";
            this.ResultsPanel.Size = new System.Drawing.Size(580, 421);
            this.ResultsPanel.TabIndex = 19;
            // 
            // CopyRefreshTokenButton
            // 
            this.CopyRefreshTokenButton.Location = new System.Drawing.Point(472, 90);
            this.CopyRefreshTokenButton.Name = "CopyRefreshTokenButton";
            this.CopyRefreshTokenButton.Size = new System.Drawing.Size(75, 23);
            this.CopyRefreshTokenButton.TabIndex = 23;
            this.CopyRefreshTokenButton.Text = "Copy";
            this.CopyRefreshTokenButton.UseVisualStyleBackColor = true;
            this.CopyRefreshTokenButton.Click += new System.EventHandler(this.CopyRefreshTokenButton_Click);
            // 
            // CopyAccessTokenButton
            // 
            this.CopyAccessTokenButton.Location = new System.Drawing.Point(472, 14);
            this.CopyAccessTokenButton.Name = "CopyAccessTokenButton";
            this.CopyAccessTokenButton.Size = new System.Drawing.Size(75, 23);
            this.CopyAccessTokenButton.TabIndex = 22;
            this.CopyAccessTokenButton.Text = "Copy";
            this.CopyAccessTokenButton.UseVisualStyleBackColor = true;
            this.CopyAccessTokenButton.Click += new System.EventHandler(this.CopyAccessTokenButton_Click);
            // 
            // CopyResponseButton
            // 
            this.CopyResponseButton.Location = new System.Drawing.Point(472, 181);
            this.CopyResponseButton.Name = "CopyResponseButton";
            this.CopyResponseButton.Size = new System.Drawing.Size(75, 23);
            this.CopyResponseButton.TabIndex = 21;
            this.CopyResponseButton.Text = "Copy";
            this.CopyResponseButton.UseVisualStyleBackColor = true;
            this.CopyResponseButton.Click += new System.EventHandler(this.CopyResponseButton_Click);
            // 
            // CopyRefreshTokenAsHeaderButton
            // 
            this.CopyRefreshTokenAsHeaderButton.Location = new System.Drawing.Point(472, 119);
            this.CopyRefreshTokenAsHeaderButton.Name = "CopyRefreshTokenAsHeaderButton";
            this.CopyRefreshTokenAsHeaderButton.Size = new System.Drawing.Size(75, 23);
            this.CopyRefreshTokenAsHeaderButton.TabIndex = 11;
            this.CopyRefreshTokenAsHeaderButton.Text = "Copy header";
            this.CopyRefreshTokenAsHeaderButton.UseVisualStyleBackColor = true;
            this.CopyRefreshTokenAsHeaderButton.Click += new System.EventHandler(this.CopyRefreshTokenAsHeaderButton_Click);
            // 
            // CopyAccessTokenAsHeaderButton
            // 
            this.CopyAccessTokenAsHeaderButton.Location = new System.Drawing.Point(472, 43);
            this.CopyAccessTokenAsHeaderButton.Name = "CopyAccessTokenAsHeaderButton";
            this.CopyAccessTokenAsHeaderButton.Size = new System.Drawing.Size(75, 23);
            this.CopyAccessTokenAsHeaderButton.TabIndex = 5;
            this.CopyAccessTokenAsHeaderButton.Text = "Copy header";
            this.CopyAccessTokenAsHeaderButton.UseVisualStyleBackColor = true;
            this.CopyAccessTokenAsHeaderButton.Click += new System.EventHandler(this.CopyAccessTokenAsHeaderButton_Click);
            // 
            // ResponseTextBox
            // 
            this.ResponseTextBox.Location = new System.Drawing.Point(107, 175);
            this.ResponseTextBox.Multiline = true;
            this.ResponseTextBox.Name = "ResponseTextBox";
            this.ResponseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResponseTextBox.Size = new System.Drawing.Size(359, 235);
            this.ResponseTextBox.TabIndex = 20;
            // 
            // RefreshTokenTextBox
            // 
            this.RefreshTokenTextBox.Location = new System.Drawing.Point(107, 87);
            this.RefreshTokenTextBox.Multiline = true;
            this.RefreshTokenTextBox.Name = "RefreshTokenTextBox";
            this.RefreshTokenTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.RefreshTokenTextBox.Size = new System.Drawing.Size(359, 70);
            this.RefreshTokenTextBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Response:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Refresh token:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Access token:";
            // 
            // AccessTokenTextBox
            // 
            this.AccessTokenTextBox.Location = new System.Drawing.Point(107, 11);
            this.AccessTokenTextBox.Multiline = true;
            this.AccessTokenTextBox.Name = "AccessTokenTextBox";
            this.AccessTokenTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AccessTokenTextBox.Size = new System.Drawing.Size(359, 70);
            this.AccessTokenTextBox.TabIndex = 4;
            // 
            // ActivityTextBox
            // 
            this.ActivityTextBox.Location = new System.Drawing.Point(89, 218);
            this.ActivityTextBox.Multiline = true;
            this.ActivityTextBox.Name = "ActivityTextBox";
            this.ActivityTextBox.ReadOnly = true;
            this.ActivityTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ActivityTextBox.Size = new System.Drawing.Size(216, 243);
            this.ActivityTextBox.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 221);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Activity:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 507);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ActivityTextBox);
            this.Controls.Add(this.ResultsPanel);
            this.Controls.Add(this.EnvironmentTabControl);
            this.Controls.Add(this.EnvironmentsGroupBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.AuthenticateButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsernameTextBox);
            this.Name = "MainForm";
            this.Text = "AAD Auth";
            this.EnvironmentsGroupBox.ResumeLayout(false);
            this.EnvironmentsGroupBox.PerformLayout();
            this.ResultsPanel.ResumeLayout(false);
            this.ResultsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UsernameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AuthenticateButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.GroupBox EnvironmentsGroupBox;
        private System.Windows.Forms.CheckBox ProdCheckBox;
        private System.Windows.Forms.CheckBox BetaCheckBox;
        private System.Windows.Forms.CheckBox QaCheckBox;
        private System.Windows.Forms.CheckBox DevTestCheckBox;
        private System.Windows.Forms.TabControl EnvironmentTabControl;
        private System.Windows.Forms.TabPage DevTestTab;
        private System.Windows.Forms.Panel ResultsPanel;
        private System.Windows.Forms.TextBox ResponseTextBox;
        private System.Windows.Forms.TextBox RefreshTokenTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox AccessTokenTextBox;
        private System.Windows.Forms.TextBox ActivityTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button CopyResponseButton;
        private System.Windows.Forms.Button CopyRefreshTokenAsHeaderButton;
        private System.Windows.Forms.Button CopyAccessTokenAsHeaderButton;
        private System.Windows.Forms.Button CopyRefreshTokenButton;
        private System.Windows.Forms.Button CopyAccessTokenButton;
    }
}