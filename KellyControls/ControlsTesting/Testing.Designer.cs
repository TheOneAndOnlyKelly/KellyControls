namespace ControlsTesting
{
	partial class Testing
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
			KellyControls.CommonClasses.HSL hsl4 = new KellyControls.CommonClasses.HSL();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Testing));
			KellyControls.CommonClasses.HSL hsl5 = new KellyControls.CommonClasses.HSL();
			KellyControls.CommonClasses.HSL hsl6 = new KellyControls.CommonClasses.HSL();
			this.ghostedTextBox1 = new KellyControls.GhostedTextBox();
			this.imageDropDown1 = new KellyControls.ImageDropDown();
			this.imageListBox1 = new KellyControls.ImageListBox();
			this.hslSlider1 = new KellyControls.HSLSlider();
			this.colorPanel1 = new KellyControls.ColorPanel();
			this.colorBox1 = new KellyControls.ColorBox();
			this.colorPicker1 = new KellyControls.ColorPicker();
			this.colorSelector1 = new KellyControls.ColorSelector();
			this.colorWheel1 = new KellyControls.ColorWheel();
			this.selectColorDialog1 = new KellyControls.SelectColorDlg.SelectColorDialog(this.components);
			this.treeViewScroll1 = new KellyControls.TreeViewScroll();
			this.richTextBoxPrintCtrl1 = new KellyControls.RichTextBoxPrintCtrl();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.colorGrid1 = new KellyControls.ColorGrid();
			this.label12 = new System.Windows.Forms.Label();
			this.dropDownControl1 = new KellyControls.DropDownControl();
			this.label13 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ghostedTextBox1
			// 
			this.ghostedTextBox1.GhostedText = "Test";
			this.ghostedTextBox1.Location = new System.Drawing.Point(12, 28);
			this.ghostedTextBox1.Name = "ghostedTextBox1";
			this.ghostedTextBox1.Size = new System.Drawing.Size(174, 20);
			this.ghostedTextBox1.TabIndex = 0;
			// 
			// imageDropDown1
			// 
			this.imageDropDown1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.imageDropDown1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.imageDropDown1.FormattingEnabled = true;
			this.imageDropDown1.Location = new System.Drawing.Point(12, 127);
			this.imageDropDown1.Name = "imageDropDown1";
			this.imageDropDown1.Size = new System.Drawing.Size(174, 21);
			this.imageDropDown1.TabIndex = 1;
			// 
			// imageListBox1
			// 
			this.imageListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.imageListBox1.FormattingEnabled = true;
			this.imageListBox1.Location = new System.Drawing.Point(211, 28);
			this.imageListBox1.Name = "imageListBox1";
			this.imageListBox1.Size = new System.Drawing.Size(160, 122);
			this.imageListBox1.TabIndex = 2;
			// 
			// hslSlider1
			// 
			this.hslSlider1.BackColor = System.Drawing.Color.Transparent;
			this.hslSlider1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			hsl4.Alpha = 1D;
			hsl4.H = 0.66666666666666663D;
			hsl4.Hue = 0.66666666666666663D;
			hsl4.L = 1D;
			hsl4.Luminance = 1D;
			hsl4.S = 1D;
			hsl4.Saturation = 1D;
			this.hslSlider1.HSL = hsl4;
			this.hslSlider1.IndicatorMarks = ((System.Collections.Generic.List<double>)(resources.GetObject("hslSlider1.IndicatorMarks")));
			this.hslSlider1.Location = new System.Drawing.Point(12, 169);
			this.hslSlider1.Name = "hslSlider1";
			this.hslSlider1.Size = new System.Drawing.Size(174, 30);
			this.hslSlider1.TabIndex = 4;
			// 
			// colorPanel1
			// 
			this.colorPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.colorPanel1.Color = System.Drawing.Color.Blue;
			this.colorPanel1.Location = new System.Drawing.Point(12, 75);
			this.colorPanel1.Name = "colorPanel1";
			this.colorPanel1.PaintColor = true;
			this.colorPanel1.Size = new System.Drawing.Size(174, 23);
			this.colorPanel1.TabIndex = 5;
			this.colorPanel1.Text = "colorPanel1";
			// 
			// colorBox1
			// 
			this.colorBox1.DrawStyle = KellyControls.ColorBox.eDrawStyle.Hue;
			hsl5.Alpha = 1D;
			hsl5.H = 0.66666666666666663D;
			hsl5.Hue = 0.66666666666666663D;
			hsl5.L = 1D;
			hsl5.Luminance = 1D;
			hsl5.S = 1D;
			hsl5.Saturation = 1D;
			this.colorBox1.HSL = hsl5;
			this.colorBox1.Location = new System.Drawing.Point(211, 169);
			this.colorBox1.Name = "colorBox1";
			this.colorBox1.RGB = System.Drawing.Color.Blue;
			this.colorBox1.Size = new System.Drawing.Size(160, 112);
			this.colorBox1.TabIndex = 6;
			// 
			// colorPicker1
			// 
			this.colorPicker1.Color = System.Drawing.Color.Blue;
			this.colorPicker1.ColorName = "Blue";
			this.colorPicker1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colorPicker1.Location = new System.Drawing.Point(572, 28);
			this.colorPicker1.Name = "colorPicker1";
			this.colorPicker1.RedOffset = -90D;
			this.colorPicker1.Size = new System.Drawing.Size(210, 320);
			this.colorPicker1.TabIndex = 7;
			// 
			// colorSelector1
			// 
			this.colorSelector1.AnchorSize = new System.Drawing.Size(174, 21);
			this.colorSelector1.BackColor = System.Drawing.Color.Transparent;
			this.colorSelector1.Color = System.Drawing.Color.Blue;
			this.colorSelector1.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.colorSelector1.Location = new System.Drawing.Point(12, 229);
			this.colorSelector1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.colorSelector1.Name = "colorSelector1";
			this.colorSelector1.ShowNoColor = false;
			this.colorSelector1.Size = new System.Drawing.Size(174, 21);
			this.colorSelector1.TabIndex = 8;
			// 
			// colorWheel1
			// 
			this.colorWheel1.Color = System.Drawing.Color.Blue;
			hsl6.Alpha = 1D;
			hsl6.H = 0.66666666666666663D;
			hsl6.Hue = 0.66666666666666663D;
			hsl6.L = 1D;
			hsl6.Luminance = 1D;
			hsl6.S = 1D;
			hsl6.Saturation = 1D;
			this.colorWheel1.HSL = hsl6;
			this.colorWheel1.Hue = 0.66666666666666663D;
			this.colorWheel1.Location = new System.Drawing.Point(398, 28);
			this.colorWheel1.Luminance = 1D;
			this.colorWheel1.MarkerSize = ((uint)(10u));
			this.colorWheel1.Name = "colorWheel1";
			this.colorWheel1.Red = ((byte)(0));
			this.colorWheel1.RedOffset = -90D;
			this.colorWheel1.Saturation = 1D;
			this.colorWheel1.Size = new System.Drawing.Size(150, 150);
			this.colorWheel1.TabIndex = 9;
			// 
			// selectColorDialog1
			// 
			this.selectColorDialog1.CancelButton_Image = null;
			this.selectColorDialog1.Color = System.Drawing.Color.Empty;
			this.selectColorDialog1.ColorName = null;
			this.selectColorDialog1.CustomColors = null;
			this.selectColorDialog1.OKButton_Image = null;
			this.selectColorDialog1.ShowPalette = false;
			this.selectColorDialog1.Title = null;
			// 
			// treeViewScroll1
			// 
			this.treeViewScroll1.Location = new System.Drawing.Point(12, 310);
			this.treeViewScroll1.Name = "treeViewScroll1";
			this.treeViewScroll1.Size = new System.Drawing.Size(174, 117);
			this.treeViewScroll1.TabIndex = 10;
			// 
			// richTextBoxPrintCtrl1
			// 
			this.richTextBoxPrintCtrl1.Location = new System.Drawing.Point(211, 311);
			this.richTextBoxPrintCtrl1.Name = "richTextBoxPrintCtrl1";
			this.richTextBoxPrintCtrl1.Size = new System.Drawing.Size(160, 116);
			this.richTextBoxPrintCtrl1.TabIndex = 11;
			this.richTextBoxPrintCtrl1.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Ghosted Text Box";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 62);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Color Panel";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 111);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 13);
			this.label3.TabIndex = 14;
			this.label3.Text = "Image Dropdown";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 153);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 15;
			this.label4.Text = "HSL Slider";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(208, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 13);
			this.label5.TabIndex = 16;
			this.label5.Text = "Image ListBox";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(208, 153);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 13);
			this.label6.TabIndex = 17;
			this.label6.Text = "Color Box";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(9, 213);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 13);
			this.label7.TabIndex = 18;
			this.label7.Text = "Color Selector";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(395, 12);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 13);
			this.label8.TabIndex = 19;
			this.label8.Text = "Color Wheel";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(569, 12);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(64, 13);
			this.label9.TabIndex = 20;
			this.label9.Text = "Color Picker";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 294);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(78, 13);
			this.label10.TabIndex = 21;
			this.label10.Text = "TreeViewScroll";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(208, 294);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(104, 13);
			this.label11.TabIndex = 22;
			this.label11.Text = "RichTextBoxPrintCtrl";
			// 
			// colorGrid1
			// 
			this.colorGrid1.CancelButtonImage = null;
			this.colorGrid1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.colorGrid1.Location = new System.Drawing.Point(398, 213);
			this.colorGrid1.Name = "colorGrid1";
			this.colorGrid1.OKButtonImage = null;
			this.colorGrid1.ParentUserControl = null;
			this.colorGrid1.SelectedIndex = 8;
			this.colorGrid1.Size = new System.Drawing.Size(150, 168);
			this.colorGrid1.TabIndex = 23;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(395, 197);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(53, 13);
			this.label12.TabIndex = 24;
			this.label12.Text = "Color Grid";
			// 
			// dropDownControl1
			// 
			this.dropDownControl1.BackColor = System.Drawing.Color.White;
			this.dropDownControl1.Location = new System.Drawing.Point(398, 400);
			this.dropDownControl1.Name = "dropDownControl1";
			this.dropDownControl1.Size = new System.Drawing.Size(150, 150);
			this.dropDownControl1.TabIndex = 25;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(395, 384);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(91, 13);
			this.label13.TabIndex = 26;
			this.label13.Text = "DropDownControl";
			// 
			// Testing
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(875, 621);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.dropDownControl1);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.colorGrid1);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.richTextBoxPrintCtrl1);
			this.Controls.Add(this.treeViewScroll1);
			this.Controls.Add(this.colorWheel1);
			this.Controls.Add(this.colorSelector1);
			this.Controls.Add(this.colorPicker1);
			this.Controls.Add(this.colorBox1);
			this.Controls.Add(this.colorPanel1);
			this.Controls.Add(this.hslSlider1);
			this.Controls.Add(this.imageListBox1);
			this.Controls.Add(this.imageDropDown1);
			this.Controls.Add(this.ghostedTextBox1);
			this.Name = "Testing";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private KellyControls.GhostedTextBox ghostedTextBox1;
		private KellyControls.ImageDropDown imageDropDown1;
		private KellyControls.ImageListBox imageListBox1;
		private KellyControls.HSLSlider hslSlider1;
		private KellyControls.ColorPanel colorPanel1;
		private KellyControls.ColorBox colorBox1;
		private KellyControls.ColorPicker colorPicker1;
		private KellyControls.ColorSelector colorSelector1;
		private KellyControls.ColorWheel colorWheel1;
		private KellyControls.SelectColorDlg.SelectColorDialog selectColorDialog1;
		private KellyControls.TreeViewScroll treeViewScroll1;
		private KellyControls.RichTextBoxPrintCtrl richTextBoxPrintCtrl1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private KellyControls.ColorGrid colorGrid1;
		private System.Windows.Forms.Label label12;
		private KellyControls.DropDownControl dropDownControl1;
		private System.Windows.Forms.Label label13;
	}
}

