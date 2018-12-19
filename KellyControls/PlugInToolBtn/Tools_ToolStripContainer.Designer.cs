namespace KellyControls.PlugInToolBtn
{
	partial class Tools_ToolStripContainer
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
			this.components = new System.ComponentModel.Container();
			this.ToolTimer = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.Generic_ToolStrip = new System.Windows.Forms.ToolStrip();
			this.GenericToolName = new System.Windows.Forms.ToolStripLabel();
			this.Generic_ToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// ToolTimer
			// 
			this.ToolTimer.Interval = 1;
			// 
			// Generic_ToolStrip
			// 
			this.Generic_ToolStrip.BackColor = System.Drawing.Color.White;
			this.Generic_ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.Generic_ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenericToolName});
			this.Generic_ToolStrip.Location = new System.Drawing.Point(0, 0);
			this.Generic_ToolStrip.Name = "Generic_ToolStrip";
			this.Generic_ToolStrip.Size = new System.Drawing.Size(1330, 25);
			this.Generic_ToolStrip.TabIndex = 20;
			// 
			// GenericToolName
			// 
			this.GenericToolName.BackColor = System.Drawing.SystemColors.ControlDark;
			this.GenericToolName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
			this.GenericToolName.Image = global::KellyControls.Properties.Resources.undefined;
			this.GenericToolName.Name = "GenericToolName";
			this.GenericToolName.Size = new System.Drawing.Size(93, 22);
			this.GenericToolName.Text = "Generic Tool";
			// 
			// Tools_ToolStripContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1330, 631);
			this.ControlBox = false;
			this.Controls.Add(this.Generic_ToolStrip);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Tools_ToolStripContainer";
			this.ShowInTaskbar = false;
			this.Generic_ToolStrip.ResumeLayout(false);
			this.Generic_ToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		public System.Windows.Forms.Timer ToolTimer;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStrip Generic_ToolStrip;
		private System.Windows.Forms.ToolStripLabel GenericToolName;
	}

}