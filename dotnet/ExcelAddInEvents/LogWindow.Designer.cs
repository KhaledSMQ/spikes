namespace ExcelAddInEvents
{
	partial class LogWindow
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
			this.LogTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// LogTextBox
			// 
			this.LogTextBox.Location = new System.Drawing.Point(13, 13);
			this.LogTextBox.Multiline = true;
			this.LogTextBox.Name = "LogTextBox";
			this.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.LogTextBox.Size = new System.Drawing.Size(601, 698);
			this.LogTextBox.TabIndex = 0;
			// 
			// LogWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(626, 723);
			this.Controls.Add(this.LogTextBox);
			this.Name = "LogWindow";
			this.Text = "LogWindow";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox LogTextBox;
	}
}