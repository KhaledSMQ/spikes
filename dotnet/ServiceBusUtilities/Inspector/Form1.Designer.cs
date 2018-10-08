namespace Inspector
{
	partial class FormInspector
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
			this.label1 = new System.Windows.Forms.Label();
			this.TextConnectionString = new System.Windows.Forms.TextBox();
			this.ButtonInspect = new System.Windows.Forms.Button();
			this.TextOutput = new System.Windows.Forms.RichTextBox();
			this.TextLog = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Connection string:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// TextConnectionString
			// 
			this.TextConnectionString.Location = new System.Drawing.Point(112, 15);
			this.TextConnectionString.Name = "TextConnectionString";
			this.TextConnectionString.Size = new System.Drawing.Size(568, 20);
			this.TextConnectionString.TabIndex = 1;
			// 
			// ButtonInspect
			// 
			this.ButtonInspect.Location = new System.Drawing.Point(686, 13);
			this.ButtonInspect.Name = "ButtonInspect";
			this.ButtonInspect.Size = new System.Drawing.Size(75, 23);
			this.ButtonInspect.TabIndex = 2;
			this.ButtonInspect.Text = "Inspect";
			this.ButtonInspect.UseVisualStyleBackColor = true;
			this.ButtonInspect.Click += new System.EventHandler(this.ButtonInspect_Click);
			// 
			// TextOutput
			// 
			this.TextOutput.Location = new System.Drawing.Point(17, 47);
			this.TextOutput.Name = "TextOutput";
			this.TextOutput.Size = new System.Drawing.Size(744, 297);
			this.TextOutput.TabIndex = 3;
			this.TextOutput.Text = "";
			// 
			// TextLog
			// 
			this.TextLog.Location = new System.Drawing.Point(17, 351);
			this.TextLog.Multiline = true;
			this.TextLog.Name = "TextLog";
			this.TextLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextLog.Size = new System.Drawing.Size(743, 131);
			this.TextLog.TabIndex = 4;
			// 
			// FormInspector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(778, 499);
			this.Controls.Add(this.TextLog);
			this.Controls.Add(this.TextOutput);
			this.Controls.Add(this.ButtonInspect);
			this.Controls.Add(this.TextConnectionString);
			this.Controls.Add(this.label1);
			this.Name = "FormInspector";
			this.Text = "Inspector";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TextConnectionString;
		private System.Windows.Forms.Button ButtonInspect;
		private System.Windows.Forms.RichTextBox TextOutput;
		private System.Windows.Forms.TextBox TextLog;
	}
}

