using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using RismPhotos.DataClasses;
using System.Threading.Tasks;

namespace RismPhotos
{
	public partial class ThumbnailListViewUserControl : UserControl
	{
		public delegate void ThumbnailAddedDelegate(Photo newPhoto);
		public event ThumbnailAddedDelegate ThumbnailAdded;

		public Size ThumbnailSize
		{
			get { return m_thumbnailSize; }
			set { m_thumbnailSize = value; SetThumbnalSize(); }
		}

		private bool m_getThumbnails = false;
		private Size m_thumbnailSize = new Size(96, 96);
		
		public ThumbnailListViewUserControl()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			InitializeComponent();
		}

		public void ClearList()
		{
			m_getThumbnails = false;
			ThumbnailFlowLayoutPanel.Controls.Clear();
		}

		public void SuspendListUpdate()
		{
			ThumbnailFlowLayoutPanel.SuspendLayout();
		}

		public void ResumeListUpdate()
		{
			ThumbnailFlowLayoutPanel.ResumeLayout();
		}

		public void FindThumbnails()
		{
			m_getThumbnails = true;
		}

		public void AddPhoto(Photo thumbnailPhoto)
		{
			ThumbnailPhotoUserControl newThumbnail = new ThumbnailPhotoUserControl(thumbnailPhoto, ThumbnailSize);
			ThumbnailFlowLayoutPanel.Controls.Add(newThumbnail);
		}

		public void AddPhoto(FileData photoFile)
		{
			ThumbnailPhotoUserControl newThumbnail = new ThumbnailPhotoUserControl(photoFile, ThumbnailSize);
			ThumbnailFlowLayoutPanel.Controls.Add(newThumbnail);
		}

		public void AddPhotos(IEnumerable<FileData> photos)
		{
			SuspendListUpdate();
			foreach (FileData photoFile in photos)
			{
				ThumbnailPhotoUserControl newThumbnail = new ThumbnailPhotoUserControl(photoFile, ThumbnailSize);
				ThumbnailFlowLayoutPanel.Controls.Add(newThumbnail);
			}
			ResumeListUpdate();
			FindThumbnails();
		}

		private void ThumbnailListViewUserControl_Load(object sender, EventArgs e)
		{
			if (ParentForm != null)
			{
				ParentForm.Closing += ParentForm_Closing;
			}

			ThumbnailBackgroundWorker.RunWorkerAsync();
		}

		private void SetThumbnalSize()
		{
			ThumbnailFlowLayoutPanel.SuspendLayout();
			foreach (Control iControl in ThumbnailFlowLayoutPanel.Controls)
			{
				((ThumbnailPhotoUserControl)iControl).ThumbnailSize = m_thumbnailSize;
			}
			ThumbnailFlowLayoutPanel.ResumeLayout(true);
		}

		private void ThumbnailBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			while (!ThumbnailBackgroundWorker.CancellationPending)
			{
				if (m_getThumbnails)
				{
					bool allDone = true;
					
					foreach (Control iControl in ThumbnailFlowLayoutPanel.Controls)
					{
						if (!((ThumbnailPhotoUserControl)iControl).ThumbnailSet)
						{
							Photo newPhoto = ((ThumbnailPhotoUserControl)iControl).FetchThumbnail();
							ThumbnailAdded?.Invoke(newPhoto);

							allDone = false;
							break;
						}
					}
										
					if (allDone)
					{
						m_getThumbnails = false;
					}
					Thread.Sleep(100);
				}
				else
				{
					Thread.Sleep(100);
				}
			}
		}
		
		void ParentForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ParentForm.Closing -= ParentForm_Closing;

			ThumbnailBackgroundWorker.CancelAsync();
		}
	}
}
