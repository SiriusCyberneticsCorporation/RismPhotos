namespace RismPhotos
{
	partial class RismPhotosForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RismPhotosForm));
			this.PhotosMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addPhotoFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.PhotosToolStrip = new System.Windows.Forms.ToolStrip();
			this.PhotosTreeView = new System.Windows.Forms.TreeView();
			this.TreeViewImageList = new System.Windows.Forms.ImageList(this.components);
			this.PhotosMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// PhotosMenuStrip
			// 
			this.PhotosMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.PhotosMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.PhotosMenuStrip.Name = "PhotosMenuStrip";
			this.PhotosMenuStrip.Size = new System.Drawing.Size(907, 24);
			this.PhotosMenuStrip.TabIndex = 0;
			this.PhotosMenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPhotoFolderToolStripMenuItem,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// addPhotoFolderToolStripMenuItem
			// 
			this.addPhotoFolderToolStripMenuItem.Name = "addPhotoFolderToolStripMenuItem";
			this.addPhotoFolderToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.addPhotoFolderToolStripMenuItem.Text = "Add Photo Folder";
			this.addPhotoFolderToolStripMenuItem.Click += new System.EventHandler(this.addPhotoFolderToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// PhotosToolStrip
			// 
			this.PhotosToolStrip.Location = new System.Drawing.Point(0, 24);
			this.PhotosToolStrip.Name = "PhotosToolStrip";
			this.PhotosToolStrip.Size = new System.Drawing.Size(907, 25);
			this.PhotosToolStrip.TabIndex = 1;
			this.PhotosToolStrip.Text = "toolStrip1";
			// 
			// PhotosTreeView
			// 
			this.PhotosTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.PhotosTreeView.ImageIndex = 0;
			this.PhotosTreeView.ImageList = this.TreeViewImageList;
			this.PhotosTreeView.Location = new System.Drawing.Point(0, 49);
			this.PhotosTreeView.Name = "PhotosTreeView";
			this.PhotosTreeView.SelectedImageIndex = 0;
			this.PhotosTreeView.Size = new System.Drawing.Size(255, 473);
			this.PhotosTreeView.TabIndex = 2;
			this.PhotosTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.PhotosTreeView_BeforeExpand);
			// 
			// TreeViewImageList
			// 
			this.TreeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeViewImageList.ImageStream")));
			this.TreeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.TreeViewImageList.Images.SetKeyName(0, "Folder-icon[4].png");
			this.TreeViewImageList.Images.SetKeyName(1, "Folder-icon[4].png");
			this.TreeViewImageList.Images.SetKeyName(2, "Folder-icon[1].png");
			// 
			// RismPhotosForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(907, 522);
			this.Controls.Add(this.PhotosTreeView);
			this.Controls.Add(this.PhotosToolStrip);
			this.Controls.Add(this.PhotosMenuStrip);
			this.MainMenuStrip = this.PhotosMenuStrip;
			this.Name = "RismPhotosForm";
			this.Text = "Rism Photos";
			this.Load += new System.EventHandler(this.RismPhotosForm_Load);
			this.PhotosMenuStrip.ResumeLayout(false);
			this.PhotosMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip PhotosMenuStrip;
		private System.Windows.Forms.ToolStrip PhotosToolStrip;
		private System.Windows.Forms.TreeView PhotosTreeView;
		private System.Windows.Forms.ImageList TreeViewImageList;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addPhotoFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
	}
}

