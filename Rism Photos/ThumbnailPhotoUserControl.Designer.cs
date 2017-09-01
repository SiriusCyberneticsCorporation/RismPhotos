namespace RismPhotos
{
	partial class ThumbnailPhotoUserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThumbnailPhotoUserControl));
			this.PhotoPictureBox = new System.Windows.Forms.PictureBox();
			this.PhotoInfoFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.FilenameLabel = new System.Windows.Forms.Label();
			this.DateTakenLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.PhotoPictureBox)).BeginInit();
			this.PhotoInfoFlowLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// PhotoPictureBox
			// 
			this.PhotoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PhotoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("PhotoPictureBox.Image")));
			this.PhotoPictureBox.Location = new System.Drawing.Point(3, 3);
			this.PhotoPictureBox.Name = "PhotoPictureBox";
			this.PhotoPictureBox.Size = new System.Drawing.Size(144, 104);
			this.PhotoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.PhotoPictureBox.TabIndex = 0;
			this.PhotoPictureBox.TabStop = false;
			// 
			// PhotoInfoFlowLayoutPanel
			// 
			this.PhotoInfoFlowLayoutPanel.Controls.Add(this.FilenameLabel);
			this.PhotoInfoFlowLayoutPanel.Controls.Add(this.DateTakenLabel);
			this.PhotoInfoFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PhotoInfoFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.PhotoInfoFlowLayoutPanel.Location = new System.Drawing.Point(3, 107);
			this.PhotoInfoFlowLayoutPanel.Name = "PhotoInfoFlowLayoutPanel";
			this.PhotoInfoFlowLayoutPanel.Size = new System.Drawing.Size(144, 40);
			this.PhotoInfoFlowLayoutPanel.TabIndex = 1;
			this.PhotoInfoFlowLayoutPanel.WrapContents = false;
			this.PhotoInfoFlowLayoutPanel.SizeChanged += new System.EventHandler(this.PhotoInfoFlowLayoutPanel_SizeChanged);
			// 
			// FilenameLabel
			// 
			this.FilenameLabel.AutoEllipsis = true;
			this.FilenameLabel.Location = new System.Drawing.Point(3, 4);
			this.FilenameLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.FilenameLabel.Name = "FilenameLabel";
			this.FilenameLabel.Size = new System.Drawing.Size(144, 13);
			this.FilenameLabel.TabIndex = 0;
			this.FilenameLabel.Text = "label1";
			this.FilenameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// DateTakenLabel
			// 
			this.DateTakenLabel.AutoEllipsis = true;
			this.DateTakenLabel.Location = new System.Drawing.Point(3, 23);
			this.DateTakenLabel.Margin = new System.Windows.Forms.Padding(3);
			this.DateTakenLabel.Name = "DateTakenLabel";
			this.DateTakenLabel.Size = new System.Drawing.Size(144, 13);
			this.DateTakenLabel.TabIndex = 1;
			this.DateTakenLabel.Text = "label2";
			this.DateTakenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ThumbnailPhotoUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PhotoPictureBox);
			this.Controls.Add(this.PhotoInfoFlowLayoutPanel);
			this.DoubleBuffered = true;
			this.Name = "ThumbnailPhotoUserControl";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Load += new System.EventHandler(this.ThumbnailPhotoUserControl_Load);
			((System.ComponentModel.ISupportInitialize)(this.PhotoPictureBox)).EndInit();
			this.PhotoInfoFlowLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox PhotoPictureBox;
		private System.Windows.Forms.FlowLayoutPanel PhotoInfoFlowLayoutPanel;
		private System.Windows.Forms.Label FilenameLabel;
		private System.Windows.Forms.Label DateTakenLabel;
	}
}
