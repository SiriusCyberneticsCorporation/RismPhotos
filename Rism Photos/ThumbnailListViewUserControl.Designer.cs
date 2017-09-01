namespace RismPhotos
{
	partial class ThumbnailListViewUserControl
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
			this.ThumbnailFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.ThumbnailBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.SuspendLayout();
			// 
			// ThumbnailFlowLayoutPanel
			// 
			this.ThumbnailFlowLayoutPanel.AutoScroll = true;
			this.ThumbnailFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ThumbnailFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.ThumbnailFlowLayoutPanel.Name = "ThumbnailFlowLayoutPanel";
			this.ThumbnailFlowLayoutPanel.Size = new System.Drawing.Size(527, 345);
			this.ThumbnailFlowLayoutPanel.TabIndex = 0;
			// 
			// ThumbnailBackgroundWorker
			// 
			this.ThumbnailBackgroundWorker.WorkerSupportsCancellation = true;
			this.ThumbnailBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ThumbnailBackgroundWorker_DoWork);
			// 
			// ThumbnailListViewUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ThumbnailFlowLayoutPanel);
			this.DoubleBuffered = true;
			this.Name = "ThumbnailListViewUserControl";
			this.Size = new System.Drawing.Size(527, 345);
			this.Load += new System.EventHandler(this.ThumbnailListViewUserControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel ThumbnailFlowLayoutPanel;
		private System.ComponentModel.BackgroundWorker ThumbnailBackgroundWorker;
	}
}
