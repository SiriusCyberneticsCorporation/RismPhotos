using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RismPhotos.DataClasses;

namespace RismPhotos
{
	public partial class ThumbnailPhotoUserControl : UserControl
	{
		public bool ThumbnailSet = false;

		public Size ThumbnailSize
		{
			get { return m_thumbnailSize; }
			set { m_thumbnailSize = value; SetSize(); }
		}

		public Photo ThumbnailPhoto
		{
			get { return m_photo; }
			set { m_photo = value; }
		}

		private object m_imageLock = new object();
		private Size m_thumbnailSize = new Size(96, 96);
		private FileData m_photoFile = null;
		private Photo m_photo = null;

		private static Size InternalThumbnailSize = new Size(256, 256);

		public ThumbnailPhotoUserControl()
		{
			InitializeComponent();
		}

		public ThumbnailPhotoUserControl(FileData photoFile, Size thumbnailSize)
		{
			InitializeComponent();

			m_photoFile = photoFile;
			m_thumbnailSize = thumbnailSize;

			FilenameLabel.Text = m_photoFile.Name;
			DateTakenLabel.Text = m_photoFile.CreationTime.ToString("dd-MM-yyyy HH:mm:ss");
		}

		public ThumbnailPhotoUserControl(Photo thumbnailPhoto, Size thumbnailSize)
		{
			InitializeComponent();

			m_photo = thumbnailPhoto;
			m_thumbnailSize = thumbnailSize;

			PhotoPictureBox.Image = Utility.ImageFromByteArray(m_photo.ThumbnailBytes); 
			FilenameLabel.Text = System.IO.Path.GetFileName(thumbnailPhoto.Filename);
			DateTakenLabel.Text = thumbnailPhoto.DateModified.ToString("dd-MM-yyyy HH:mm:ss");
			ThumbnailSet = true;
		}

		public Photo FetchThumbnail()
		{
			byte[] imageBytes;
			PhotoMetadata exifData;
			PhotoPictureBox.Image = Utility.ThumbnailFromFile(m_photoFile.Path, InternalThumbnailSize, out imageBytes, out exifData);
			ThumbnailSet = true;

			m_photo = new Photo();
			m_photo.DateModified = m_photoFile.LastWriteTime;
			m_photo.Filename = m_photoFile.Path;
			m_photo.ThumbnailBytes = imageBytes;
			m_photo.Metadata = exifData;

			return m_photo;
		}

		private void ThumbnailPhotoUserControl_Load(object sender, EventArgs e)
		{
			SetSize();
		}

		private void PhotoInfoFlowLayoutPanel_SizeChanged(object sender, EventArgs e)
		{
			PhotoInfoFlowLayoutPanel.SuspendLayout();
			foreach (Control ctrl in PhotoInfoFlowLayoutPanel.Controls)
			{
				if (ctrl is Button) ctrl.Width = PhotoInfoFlowLayoutPanel.ClientSize.Width;
			}
			PhotoInfoFlowLayoutPanel.ResumeLayout();
		}

		private void SetSize()
		{
			this.Size = new Size(m_thumbnailSize.Width + 6, m_thumbnailSize.Height + 50);
		}

		private void PhotoPictureBox_Click(object sender, EventArgs e)
		{

		}
	}
}
