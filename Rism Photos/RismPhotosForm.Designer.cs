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
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.x24ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x32ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x48ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x64ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x96ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x128ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x256ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TreeViewImageList = new System.Windows.Forms.ImageList(this.components);
			this.MainResizableTableLayoutPanel = new RismPhotos.ResizableTableLayoutPanel();
			this.PhotosTreeView = new System.Windows.Forms.TreeView();
			this.ThumbnailList = new RismPhotos.ThumbnailListViewUserControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.PhotosMenuStrip.SuspendLayout();
			this.PhotosToolStrip.SuspendLayout();
			this.MainResizableTableLayoutPanel.SuspendLayout();
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
			this.PhotosToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
			this.PhotosToolStrip.Location = new System.Drawing.Point(0, 24);
			this.PhotosToolStrip.Name = "PhotosToolStrip";
			this.PhotosToolStrip.Size = new System.Drawing.Size(907, 25);
			this.PhotosToolStrip.TabIndex = 1;
			this.PhotosToolStrip.Text = "toolStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x24ToolStripMenuItem,
            this.x32ToolStripMenuItem,
            this.x48ToolStripMenuItem,
            this.x64ToolStripMenuItem,
            this.x96ToolStripMenuItem,
            this.x128ToolStripMenuItem,
            this.x256ToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(101, 22);
			this.toolStripDropDownButton1.Text = "Thumbnail Size";
			// 
			// x24ToolStripMenuItem
			// 
			this.x24ToolStripMenuItem.Name = "x24ToolStripMenuItem";
			this.x24ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.x24ToolStripMenuItem.Text = "24 x 24";
			this.x24ToolStripMenuItem.Click += new System.EventHandler(this.x24ToolStripMenuItem_Click);
			// 
			// x32ToolStripMenuItem
			// 
			this.x32ToolStripMenuItem.Name = "x32ToolStripMenuItem";
			this.x32ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.x32ToolStripMenuItem.Text = "32 x 32";
			this.x32ToolStripMenuItem.Click += new System.EventHandler(this.x32ToolStripMenuItem_Click);
			// 
			// x48ToolStripMenuItem
			// 
			this.x48ToolStripMenuItem.Name = "x48ToolStripMenuItem";
			this.x48ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.x48ToolStripMenuItem.Text = "48 x 48";
			this.x48ToolStripMenuItem.Click += new System.EventHandler(this.x48ToolStripMenuItem_Click);
			// 
			// x64ToolStripMenuItem
			// 
			this.x64ToolStripMenuItem.Name = "x64ToolStripMenuItem";
			this.x64ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.x64ToolStripMenuItem.Text = "64 x 64";
			this.x64ToolStripMenuItem.Click += new System.EventHandler(this.x64ToolStripMenuItem_Click);
			// 
			// x96ToolStripMenuItem
			// 
			this.x96ToolStripMenuItem.Name = "x96ToolStripMenuItem";
			this.x96ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.x96ToolStripMenuItem.Text = "96 x 96";
			this.x96ToolStripMenuItem.Click += new System.EventHandler(this.x96ToolStripMenuItem_Click);
			// 
			// x128ToolStripMenuItem
			// 
			this.x128ToolStripMenuItem.Name = "x128ToolStripMenuItem";
			this.x128ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.x128ToolStripMenuItem.Text = "128 x 128";
			this.x128ToolStripMenuItem.Click += new System.EventHandler(this.x128ToolStripMenuItem_Click);
			// 
			// x256ToolStripMenuItem
			// 
			this.x256ToolStripMenuItem.Name = "x256ToolStripMenuItem";
			this.x256ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			this.x256ToolStripMenuItem.Text = "256 x 256";
			this.x256ToolStripMenuItem.Click += new System.EventHandler(this.x256ToolStripMenuItem_Click);
			// 
			// TreeViewImageList
			// 
			this.TreeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeViewImageList.ImageStream")));
			this.TreeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.TreeViewImageList.Images.SetKeyName(0, "Folder-icon[4].png");
			this.TreeViewImageList.Images.SetKeyName(1, "Folder-icon[4].png");
			this.TreeViewImageList.Images.SetKeyName(2, "Folder-icon[1].png");
			// 
			// MainResizableTableLayoutPanel
			// 
			this.MainResizableTableLayoutPanel.ColumnCount = 3;
			this.MainResizableTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.MainResizableTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.MainResizableTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.MainResizableTableLayoutPanel.Controls.Add(this.PhotosTreeView, 0, 0);
			this.MainResizableTableLayoutPanel.Controls.Add(this.ThumbnailList, 1, 0);
			this.MainResizableTableLayoutPanel.Controls.Add(this.panel1, 2, 0);
			this.MainResizableTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainResizableTableLayoutPanel.Location = new System.Drawing.Point(0, 49);
			this.MainResizableTableLayoutPanel.Name = "MainResizableTableLayoutPanel";
			this.MainResizableTableLayoutPanel.RowCount = 1;
			this.MainResizableTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.MainResizableTableLayoutPanel.Size = new System.Drawing.Size(907, 473);
			this.MainResizableTableLayoutPanel.SplitterSize = 6;
			this.MainResizableTableLayoutPanel.TabIndex = 3;
			// 
			// PhotosTreeView
			// 
			this.PhotosTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PhotosTreeView.HideSelection = false;
			this.PhotosTreeView.ImageIndex = 0;
			this.PhotosTreeView.ImageList = this.TreeViewImageList;
			this.PhotosTreeView.Location = new System.Drawing.Point(3, 3);
			this.PhotosTreeView.Name = "PhotosTreeView";
			this.PhotosTreeView.SelectedImageIndex = 0;
			this.PhotosTreeView.Size = new System.Drawing.Size(244, 467);
			this.PhotosTreeView.TabIndex = 2;
			this.PhotosTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.PhotosTreeView_BeforeExpand);
			this.PhotosTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PhotosTreeView_AfterSelect);
			this.PhotosTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.PhotosTreeView_NodeMouseClick);
			// 
			// ThumbnailList
			// 
			this.ThumbnailList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ThumbnailList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ThumbnailList.Location = new System.Drawing.Point(253, 3);
			this.ThumbnailList.Name = "ThumbnailList";
			this.ThumbnailList.Size = new System.Drawing.Size(551, 467);
			this.ThumbnailList.TabIndex = 3;
			this.ThumbnailList.ThumbnailSize = new System.Drawing.Size(96, 96);
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(810, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(94, 467);
			this.panel1.TabIndex = 4;
			// 
			// RismPhotosForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(907, 522);
			this.Controls.Add(this.MainResizableTableLayoutPanel);
			this.Controls.Add(this.PhotosToolStrip);
			this.Controls.Add(this.PhotosMenuStrip);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.PhotosMenuStrip;
			this.Name = "RismPhotosForm";
			this.Text = "Rism Photos";
			this.Load += new System.EventHandler(this.RismPhotosForm_Load);
			this.StyleChanged += new System.EventHandler(this.RismPhotosForm_StyleChanged);
			this.PhotosMenuStrip.ResumeLayout(false);
			this.PhotosMenuStrip.PerformLayout();
			this.PhotosToolStrip.ResumeLayout(false);
			this.PhotosToolStrip.PerformLayout();
			this.MainResizableTableLayoutPanel.ResumeLayout(false);
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
		private ResizableTableLayoutPanel MainResizableTableLayoutPanel;
		private ThumbnailListViewUserControl ThumbnailList;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem x24ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x32ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x48ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x64ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x96ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x128ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x256ToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
	}
}

