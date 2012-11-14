namespace SimpleCrud
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.ListPeople = new System.Windows.Forms.Button();
			this.AddProductModel = new System.Windows.Forms.Button();
			this.AddChildProduct = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.DeleteChildProduct = new System.Windows.Forms.Button();
			this.InsertButton = new System.Windows.Forms.Button();
			this.UpdateButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.SelectButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(12, 12);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(635, 225);
			this.listBox1.TabIndex = 0;
			// 
			// ListPeople
			// 
			this.ListPeople.Location = new System.Drawing.Point(12, 243);
			this.ListPeople.Name = "ListPeople";
			this.ListPeople.Size = new System.Drawing.Size(75, 23);
			this.ListPeople.TabIndex = 1;
			this.ListPeople.Text = "List people";
			this.ListPeople.UseVisualStyleBackColor = true;
			this.ListPeople.Click += new System.EventHandler(this.ListPeople_Click);
			// 
			// AddProductModel
			// 
			this.AddProductModel.Location = new System.Drawing.Point(94, 244);
			this.AddProductModel.Name = "AddProductModel";
			this.AddProductModel.Size = new System.Drawing.Size(112, 23);
			this.AddProductModel.TabIndex = 2;
			this.AddProductModel.Text = "Add product model";
			this.AddProductModel.UseVisualStyleBackColor = true;
			this.AddProductModel.Click += new System.EventHandler(this.AddProductModel_Click);
			// 
			// AddChildProduct
			// 
			this.AddChildProduct.Location = new System.Drawing.Point(213, 244);
			this.AddChildProduct.Name = "AddChildProduct";
			this.AddChildProduct.Size = new System.Drawing.Size(112, 23);
			this.AddChildProduct.TabIndex = 3;
			this.AddChildProduct.Text = "Add child product";
			this.AddChildProduct.UseVisualStyleBackColor = true;
			this.AddChildProduct.Click += new System.EventHandler(this.AddChildProduct_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(331, 249);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "label1";
			// 
			// DeleteChildProduct
			// 
			this.DeleteChildProduct.Location = new System.Drawing.Point(13, 273);
			this.DeleteChildProduct.Name = "DeleteChildProduct";
			this.DeleteChildProduct.Size = new System.Drawing.Size(111, 23);
			this.DeleteChildProduct.TabIndex = 5;
			this.DeleteChildProduct.Text = "Delete child product";
			this.DeleteChildProduct.UseVisualStyleBackColor = true;
			this.DeleteChildProduct.Click += new System.EventHandler(this.DeleteChildProduct_Click);
			// 
			// InsertButton
			// 
			this.InsertButton.Location = new System.Drawing.Point(13, 303);
			this.InsertButton.Name = "InsertButton";
			this.InsertButton.Size = new System.Drawing.Size(75, 23);
			this.InsertButton.TabIndex = 6;
			this.InsertButton.Text = "Insert";
			this.InsertButton.UseVisualStyleBackColor = true;
			this.InsertButton.Click += new System.EventHandler(this.Insert_Click);
			// 
			// UpdateButton
			// 
			this.UpdateButton.Location = new System.Drawing.Point(94, 303);
			this.UpdateButton.Name = "UpdateButton";
			this.UpdateButton.Size = new System.Drawing.Size(75, 23);
			this.UpdateButton.TabIndex = 7;
			this.UpdateButton.Text = "Update";
			this.UpdateButton.UseVisualStyleBackColor = true;
			this.UpdateButton.Click += new System.EventHandler(this.Update_Click);
			// 
			// DeleteButton
			// 
			this.DeleteButton.Location = new System.Drawing.Point(175, 303);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteButton.TabIndex = 8;
			this.DeleteButton.Text = "Delete";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.Delete_Click);
			// 
			// SelectButton
			// 
			this.SelectButton.Location = new System.Drawing.Point(256, 303);
			this.SelectButton.Name = "SelectButton";
			this.SelectButton.Size = new System.Drawing.Size(75, 23);
			this.SelectButton.TabIndex = 9;
			this.SelectButton.Text = "Select";
			this.SelectButton.UseVisualStyleBackColor = true;
			this.SelectButton.Click += new System.EventHandler(this.Select_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(660, 332);
			this.Controls.Add(this.SelectButton);
			this.Controls.Add(this.DeleteButton);
			this.Controls.Add(this.UpdateButton);
			this.Controls.Add(this.InsertButton);
			this.Controls.Add(this.DeleteChildProduct);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.AddChildProduct);
			this.Controls.Add(this.AddProductModel);
			this.Controls.Add(this.ListPeople);
			this.Controls.Add(this.listBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button ListPeople;
		private System.Windows.Forms.Button AddProductModel;
		private System.Windows.Forms.Button AddChildProduct;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button DeleteChildProduct;
		private System.Windows.Forms.Button InsertButton;
		private System.Windows.Forms.Button UpdateButton;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button SelectButton;
	}
}

