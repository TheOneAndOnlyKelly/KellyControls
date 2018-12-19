namespace KellyControls
{
    partial class ColorSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.pnlSelectedColor = new KellyControls.ColorPanel();
			this.pnlControls = new System.Windows.Forms.Panel();
			this.tabsOptions = new System.Windows.Forms.TabControl();
			this.tabCustom = new System.Windows.Forms.TabPage();
			this.ColorGrid = new KellyControls.ColorGrid();
			this.tabWeb = new System.Windows.Forms.TabPage();
			this.WebColorList = new KellyControls.ColorList.ColorList();
			this.tabSystem = new System.Windows.Forms.TabPage();
			this.SystemColorList = new KellyControls.ColorList.ColorList();
			this.cmdMoreSolidColors = new System.Windows.Forms.Button();
			this.cmdNoColor = new System.Windows.Forms.Button();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.pctNoColor = new System.Windows.Forms.PictureBox();
			this.pnlControls.SuspendLayout();
			this.tabsOptions.SuspendLayout();
			this.tabCustom.SuspendLayout();
			this.tabWeb.SuspendLayout();
			this.tabSystem.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pctNoColor)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlSelectedColor
			// 
			this.pnlSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSelectedColor.Color = System.Drawing.Color.Lime;
			this.pnlSelectedColor.Location = new System.Drawing.Point(0, 2);
			this.pnlSelectedColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.pnlSelectedColor.Name = "pnlSelectedColor";
			this.pnlSelectedColor.PaintColor = true;
			this.pnlSelectedColor.Size = new System.Drawing.Size(29, 16);
			this.pnlSelectedColor.TabIndex = 0;
			this.pnlSelectedColor.Click += new System.EventHandler(this.colorPanel1_Click);
			// 
			// pnlControls
			// 
			this.pnlControls.BackColor = System.Drawing.SystemColors.Control;
			this.pnlControls.Controls.Add(this.tabsOptions);
			this.pnlControls.Controls.Add(this.cmdMoreSolidColors);
			this.pnlControls.Controls.Add(this.cmdNoColor);
			this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlControls.Location = new System.Drawing.Point(0, 21);
			this.pnlControls.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.pnlControls.Name = "pnlControls";
			this.pnlControls.Size = new System.Drawing.Size(170, 236);
			this.pnlControls.TabIndex = 1;
			// 
			// tabsOptions
			// 
			this.tabsOptions.Controls.Add(this.tabCustom);
			this.tabsOptions.Controls.Add(this.tabWeb);
			this.tabsOptions.Controls.Add(this.tabSystem);
			this.tabsOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabsOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tabsOptions.Location = new System.Drawing.Point(0, 21);
			this.tabsOptions.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tabsOptions.Name = "tabsOptions";
			this.tabsOptions.SelectedIndex = 0;
			this.tabsOptions.Size = new System.Drawing.Size(170, 194);
			this.tabsOptions.TabIndex = 4;
			// 
			// tabCustom
			// 
			this.tabCustom.Controls.Add(this.ColorGrid);
			this.tabCustom.Location = new System.Drawing.Point(4, 21);
			this.tabCustom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tabCustom.Name = "tabCustom";
			this.tabCustom.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tabCustom.Size = new System.Drawing.Size(162, 169);
			this.tabCustom.TabIndex = 0;
			this.tabCustom.Text = "Custom";
			this.tabCustom.UseVisualStyleBackColor = true;
			// 
			// ColorGrid
			// 
			this.ColorGrid.CancelButtonImage = null;
			this.ColorGrid.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.ColorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ColorGrid.GridPadding = ((byte)(5));
			this.ColorGrid.GridSize = new System.Drawing.Size(16, 12);
			this.ColorGrid.Location = new System.Drawing.Point(2, 3);
			this.ColorGrid.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.ColorGrid.Name = "ColorGrid";
			this.ColorGrid.OKButtonImage = null;
			this.ColorGrid.ParentUserControl = null;
			this.ColorGrid.SelectedIndex = 0;
			this.ColorGrid.Size = new System.Drawing.Size(158, 150);
			this.ColorGrid.TabIndex = 3;
			this.ColorGrid.SelectedIndexChange += new System.EventHandler(this.colorGrid1_SelectedIndexChange);
			// 
			// tabWeb
			// 
			this.tabWeb.Controls.Add(this.WebColorList);
			this.tabWeb.Location = new System.Drawing.Point(4, 21);
			this.tabWeb.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tabWeb.Name = "tabWeb";
			this.tabWeb.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tabWeb.Size = new System.Drawing.Size(162, 169);
			this.tabWeb.TabIndex = 1;
			this.tabWeb.Text = "Web";
			this.tabWeb.UseVisualStyleBackColor = true;
			// 
			// WebColorList
			// 
			this.WebColorList.Color = System.Drawing.Color.Empty;
			this.WebColorList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebColorList.GridPadding = new System.Windows.Forms.Padding(4);
			this.WebColorList.GridSize = new System.Drawing.Size(40, 20);
			this.WebColorList.ListStyle = KellyControls.ColorList.ColorList.ListStyleEnum.Web;
			this.WebColorList.Location = new System.Drawing.Point(2, 3);
			this.WebColorList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.WebColorList.Name = "WebColorList";
			this.WebColorList.SelectedIndex = -1;
			this.WebColorList.Size = new System.Drawing.Size(158, 163);
			this.WebColorList.TabIndex = 0;
			// 
			// tabSystem
			// 
			this.tabSystem.Controls.Add(this.SystemColorList);
			this.tabSystem.Location = new System.Drawing.Point(4, 21);
			this.tabSystem.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.tabSystem.Name = "tabSystem";
			this.tabSystem.Size = new System.Drawing.Size(162, 169);
			this.tabSystem.TabIndex = 2;
			this.tabSystem.Text = "System";
			this.tabSystem.UseVisualStyleBackColor = true;
			// 
			// SystemColorList
			// 
			this.SystemColorList.BackColor = System.Drawing.SystemColors.Window;
			this.SystemColorList.Color = System.Drawing.Color.Empty;
			this.SystemColorList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SystemColorList.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SystemColorList.GridPadding = new System.Windows.Forms.Padding(4);
			this.SystemColorList.GridSize = new System.Drawing.Size(40, 20);
			this.SystemColorList.ListStyle = KellyControls.ColorList.ColorList.ListStyleEnum.System;
			this.SystemColorList.Location = new System.Drawing.Point(0, 0);
			this.SystemColorList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.SystemColorList.Name = "SystemColorList";
			this.SystemColorList.SelectedIndex = -1;
			this.SystemColorList.Size = new System.Drawing.Size(162, 169);
			this.SystemColorList.TabIndex = 1;
			// 
			// cmdMoreSolidColors
			// 
			this.cmdMoreSolidColors.BackColor = System.Drawing.SystemColors.Control;
			this.cmdMoreSolidColors.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.cmdMoreSolidColors.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdMoreSolidColors.Location = new System.Drawing.Point(0, 215);
			this.cmdMoreSolidColors.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.cmdMoreSolidColors.Name = "cmdMoreSolidColors";
			this.cmdMoreSolidColors.Size = new System.Drawing.Size(170, 21);
			this.cmdMoreSolidColors.TabIndex = 1;
			this.cmdMoreSolidColors.Text = "More Solid Colors...";
			this.cmdMoreSolidColors.UseVisualStyleBackColor = false;
			this.cmdMoreSolidColors.Click += new System.EventHandler(this.cmdMoreSolidColors_Click);
			this.cmdMoreSolidColors.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.cmdMoreSolidColors.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			// 
			// cmdNoColor
			// 
			this.cmdNoColor.BackColor = System.Drawing.SystemColors.Control;
			this.cmdNoColor.Dock = System.Windows.Forms.DockStyle.Top;
			this.cmdNoColor.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdNoColor.Location = new System.Drawing.Point(0, 0);
			this.cmdNoColor.Margin = new System.Windows.Forms.Padding(0);
			this.cmdNoColor.Name = "cmdNoColor";
			this.cmdNoColor.Size = new System.Drawing.Size(170, 21);
			this.cmdNoColor.TabIndex = 3;
			this.cmdNoColor.Text = "No Color";
			this.cmdNoColor.UseVisualStyleBackColor = false;
			this.cmdNoColor.Click += new System.EventHandler(this.cmdNoColor_Click);
			this.cmdNoColor.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
			this.cmdNoColor.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
			// 
			// pctNoColor
			// 
			this.pctNoColor.Image = global::KellyControls.Properties.Resources.not;
			this.pctNoColor.Location = new System.Drawing.Point(34, 3);
			this.pctNoColor.Name = "pctNoColor";
			this.pctNoColor.Size = new System.Drawing.Size(16, 16);
			this.pctNoColor.TabIndex = 2;
			this.pctNoColor.TabStop = false;
			this.pctNoColor.Visible = false;
			// 
			// ColorSelector
			// 
			this.AnchorSize = new System.Drawing.Size(170, 21);
			this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pctNoColor);
			this.Controls.Add(this.pnlControls);
			this.Controls.Add(this.pnlSelectedColor);
			this.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.Name = "ColorSelector";
			this.Size = new System.Drawing.Size(170, 257);
			this.pnlControls.ResumeLayout(false);
			this.tabsOptions.ResumeLayout(false);
			this.tabCustom.ResumeLayout(false);
			this.tabWeb.ResumeLayout(false);
			this.tabSystem.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pctNoColor)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private ColorPanel pnlSelectedColor;
        private System.Windows.Forms.Panel pnlControls;
		private System.Windows.Forms.Button cmdMoreSolidColors;
		private System.Windows.Forms.Button cmdNoColor;
		private System.Windows.Forms.TabControl tabsOptions;
		private System.Windows.Forms.TabPage tabCustom;
		private ColorGrid ColorGrid;
		private System.Windows.Forms.TabPage tabWeb;
		private System.Windows.Forms.TabPage tabSystem;
		private ColorList.ColorList WebColorList;
		private ColorList.ColorList SystemColorList;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.PictureBox pctNoColor;


    }
}
