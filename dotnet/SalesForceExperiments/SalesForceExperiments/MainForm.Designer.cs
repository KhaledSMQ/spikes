namespace SalesForceExperiments
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
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.AuthButton = new System.Windows.Forms.Button();
            this.ListVersionsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ListResourcesButton = new System.Windows.Forms.Button();
            this.ListObjectsButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ApiCallBox = new System.Windows.Forms.TextBox();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AccountsButton = new System.Windows.Forms.Button();
            this.ContactsButton = new System.Windows.Forms.Button();
            this.ContactsPerAccountButton = new System.Windows.Forms.Button();
            this.DescribeAccountsButton = new System.Windows.Forms.Button();
            this.DescribeContactsButton = new System.Windows.Forms.Button();
            this.ProductsButton = new System.Windows.Forms.Button();
            this.DescribeProductsButton = new System.Windows.Forms.Button();
            this.AssetsButton = new System.Windows.Forms.Button();
            this.DescribeAssetsButton = new System.Windows.Forms.Button();
            this.DescribeAssignedUsersButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(13, 43);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.OutputBox.Size = new System.Drawing.Size(770, 563);
            this.OutputBox.TabIndex = 0;
            // 
            // AuthButton
            // 
            this.AuthButton.Location = new System.Drawing.Point(789, 11);
            this.AuthButton.Name = "AuthButton";
            this.AuthButton.Size = new System.Drawing.Size(75, 23);
            this.AuthButton.TabIndex = 1;
            this.AuthButton.Text = "Authenticate";
            this.AuthButton.UseVisualStyleBackColor = true;
            this.AuthButton.Click += new System.EventHandler(this.AuthButton_Click);
            // 
            // ListVersionsButton
            // 
            this.ListVersionsButton.Location = new System.Drawing.Point(789, 107);
            this.ListVersionsButton.Name = "ListVersionsButton";
            this.ListVersionsButton.Size = new System.Drawing.Size(75, 23);
            this.ListVersionsButton.TabIndex = 2;
            this.ListVersionsButton.Text = "Versions";
            this.ListVersionsButton.UseVisualStyleBackColor = true;
            this.ListVersionsButton.Click += new System.EventHandler(this.ListVersionsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(789, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "User is not authenticated";
            // 
            // ListResourcesButton
            // 
            this.ListResourcesButton.Location = new System.Drawing.Point(870, 107);
            this.ListResourcesButton.Name = "ListResourcesButton";
            this.ListResourcesButton.Size = new System.Drawing.Size(75, 23);
            this.ListResourcesButton.TabIndex = 4;
            this.ListResourcesButton.Text = "Resources";
            this.ListResourcesButton.UseVisualStyleBackColor = true;
            this.ListResourcesButton.Click += new System.EventHandler(this.ListResourcesButton_Click);
            // 
            // ListObjectsButton
            // 
            this.ListObjectsButton.Location = new System.Drawing.Point(789, 136);
            this.ListObjectsButton.Name = "ListObjectsButton";
            this.ListObjectsButton.Size = new System.Drawing.Size(75, 23);
            this.ListObjectsButton.TabIndex = 5;
            this.ListObjectsButton.Text = "Objects";
            this.ListObjectsButton.UseVisualStyleBackColor = true;
            this.ListObjectsButton.Click += new System.EventHandler(this.ListObjectsButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "API call:";
            // 
            // ApiCallBox
            // 
            this.ApiCallBox.Location = new System.Drawing.Point(66, 13);
            this.ApiCallBox.Name = "ApiCallBox";
            this.ApiCallBox.Size = new System.Drawing.Size(636, 20);
            this.ApiCallBox.TabIndex = 7;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(708, 11);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 8;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(786, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "List all:";
            // 
            // AccountsButton
            // 
            this.AccountsButton.Location = new System.Drawing.Point(870, 136);
            this.AccountsButton.Name = "AccountsButton";
            this.AccountsButton.Size = new System.Drawing.Size(75, 23);
            this.AccountsButton.TabIndex = 10;
            this.AccountsButton.Text = "Accounts";
            this.AccountsButton.UseVisualStyleBackColor = true;
            this.AccountsButton.Click += new System.EventHandler(this.AccountsButton_Click);
            // 
            // ContactsButton
            // 
            this.ContactsButton.Location = new System.Drawing.Point(870, 165);
            this.ContactsButton.Name = "ContactsButton";
            this.ContactsButton.Size = new System.Drawing.Size(75, 23);
            this.ContactsButton.TabIndex = 11;
            this.ContactsButton.Text = "Contacts";
            this.ContactsButton.UseVisualStyleBackColor = true;
            this.ContactsButton.Click += new System.EventHandler(this.ContactsButton_Click);
            // 
            // ContactsPerAccountButton
            // 
            this.ContactsPerAccountButton.Location = new System.Drawing.Point(792, 497);
            this.ContactsPerAccountButton.Name = "ContactsPerAccountButton";
            this.ContactsPerAccountButton.Size = new System.Drawing.Size(153, 23);
            this.ContactsPerAccountButton.TabIndex = 12;
            this.ContactsPerAccountButton.Text = "Contacts per Account";
            this.ContactsPerAccountButton.UseVisualStyleBackColor = true;
            this.ContactsPerAccountButton.Click += new System.EventHandler(this.ContactsPerAccountButton_Click);
            // 
            // DescribeAccountsButton
            // 
            this.DescribeAccountsButton.Location = new System.Drawing.Point(792, 293);
            this.DescribeAccountsButton.Name = "DescribeAccountsButton";
            this.DescribeAccountsButton.Size = new System.Drawing.Size(153, 23);
            this.DescribeAccountsButton.TabIndex = 14;
            this.DescribeAccountsButton.Text = "Describe Accounts";
            this.DescribeAccountsButton.UseVisualStyleBackColor = true;
            this.DescribeAccountsButton.Click += new System.EventHandler(this.DescribeAccountsButton_Click);
            // 
            // DescribeContactsButton
            // 
            this.DescribeContactsButton.Location = new System.Drawing.Point(792, 323);
            this.DescribeContactsButton.Name = "DescribeContactsButton";
            this.DescribeContactsButton.Size = new System.Drawing.Size(153, 23);
            this.DescribeContactsButton.TabIndex = 15;
            this.DescribeContactsButton.Text = "Describe Contacts";
            this.DescribeContactsButton.UseVisualStyleBackColor = true;
            this.DescribeContactsButton.Click += new System.EventHandler(this.DescribeContactsButton_Click);
            // 
            // ProductsButton
            // 
            this.ProductsButton.Location = new System.Drawing.Point(789, 165);
            this.ProductsButton.Name = "ProductsButton";
            this.ProductsButton.Size = new System.Drawing.Size(75, 23);
            this.ProductsButton.TabIndex = 16;
            this.ProductsButton.Text = "Products";
            this.ProductsButton.UseVisualStyleBackColor = true;
            this.ProductsButton.Click += new System.EventHandler(this.ProductsButton_Click);
            // 
            // DescribeProductsButton
            // 
            this.DescribeProductsButton.Location = new System.Drawing.Point(792, 352);
            this.DescribeProductsButton.Name = "DescribeProductsButton";
            this.DescribeProductsButton.Size = new System.Drawing.Size(153, 23);
            this.DescribeProductsButton.TabIndex = 17;
            this.DescribeProductsButton.Text = "Describe Products";
            this.DescribeProductsButton.UseVisualStyleBackColor = true;
            this.DescribeProductsButton.Click += new System.EventHandler(this.DescribeProductsButton_Click);
            // 
            // AssetsButton
            // 
            this.AssetsButton.Location = new System.Drawing.Point(789, 194);
            this.AssetsButton.Name = "AssetsButton";
            this.AssetsButton.Size = new System.Drawing.Size(75, 23);
            this.AssetsButton.TabIndex = 18;
            this.AssetsButton.Text = "Assets";
            this.AssetsButton.UseVisualStyleBackColor = true;
            this.AssetsButton.Click += new System.EventHandler(this.AssetsButton_Click);
            // 
            // DescribeAssetsButton
            // 
            this.DescribeAssetsButton.Location = new System.Drawing.Point(792, 381);
            this.DescribeAssetsButton.Name = "DescribeAssetsButton";
            this.DescribeAssetsButton.Size = new System.Drawing.Size(153, 23);
            this.DescribeAssetsButton.TabIndex = 19;
            this.DescribeAssetsButton.Text = "Describe Assets";
            this.DescribeAssetsButton.UseVisualStyleBackColor = true;
            this.DescribeAssetsButton.Click += new System.EventHandler(this.DescribeAssetsButton_Click);
            // 
            // DescribeAssignedUsersButton
            // 
            this.DescribeAssignedUsersButton.Location = new System.Drawing.Point(792, 410);
            this.DescribeAssignedUsersButton.Name = "DescribeAssignedUsersButton";
            this.DescribeAssignedUsersButton.Size = new System.Drawing.Size(153, 23);
            this.DescribeAssignedUsersButton.TabIndex = 20;
            this.DescribeAssignedUsersButton.Text = "Describe Assigned Users";
            this.DescribeAssignedUsersButton.UseVisualStyleBackColor = true;
            this.DescribeAssignedUsersButton.Click += new System.EventHandler(this.DescribeAssignedUsersButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 618);
            this.Controls.Add(this.DescribeAssignedUsersButton);
            this.Controls.Add(this.DescribeAssetsButton);
            this.Controls.Add(this.AssetsButton);
            this.Controls.Add(this.DescribeProductsButton);
            this.Controls.Add(this.ProductsButton);
            this.Controls.Add(this.DescribeContactsButton);
            this.Controls.Add(this.DescribeAccountsButton);
            this.Controls.Add(this.ContactsPerAccountButton);
            this.Controls.Add(this.ContactsButton);
            this.Controls.Add(this.AccountsButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.ApiCallBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ListObjectsButton);
            this.Controls.Add(this.ListResourcesButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListVersionsButton);
            this.Controls.Add(this.AuthButton);
            this.Controls.Add(this.OutputBox);
            this.Name = "MainForm";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox OutputBox;
		private System.Windows.Forms.Button AuthButton;
		private System.Windows.Forms.Button ListVersionsButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button ListResourcesButton;
		private System.Windows.Forms.Button ListObjectsButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ApiCallBox;
		private System.Windows.Forms.Button ExecuteButton;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AccountsButton;
        private System.Windows.Forms.Button ContactsButton;
        private System.Windows.Forms.Button ContactsPerAccountButton;
        private System.Windows.Forms.Button DescribeAccountsButton;
        private System.Windows.Forms.Button DescribeContactsButton;
        private System.Windows.Forms.Button ProductsButton;
        private System.Windows.Forms.Button DescribeProductsButton;
        private System.Windows.Forms.Button AssetsButton;
        private System.Windows.Forms.Button DescribeAssetsButton;
        private System.Windows.Forms.Button DescribeAssignedUsersButton;
    }
}