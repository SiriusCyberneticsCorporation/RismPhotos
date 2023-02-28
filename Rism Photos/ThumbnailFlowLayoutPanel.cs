using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RismPhotos.DataClasses;
using System.Threading;

namespace RismPhotos
{
	public class ThumbnailFlowLayoutPanel : FlowLayoutPanel
	{
		public delegate void ThumbnailAddedDelegate(Photo newPhoto);
		public event ThumbnailAddedDelegate ThumbnailAdded;

		public Size ThumbnailSize
		{
			get { return m_thumbnailSize; }
			set { m_thumbnailSize = value; SetThumbnalSize(); }
		}

		private bool m_threadRunning = true;
		private bool m_getThumbnails = false;
		private Size m_thumbnailSize = new Size(96, 96);
		private Thread m_thumbnailThread = null;

		public ThumbnailFlowLayoutPanel()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			m_thumbnailThread = new Thread(ThumbnailProcessingThread);
			m_thumbnailThread.Start();
		}

		protected override void Dispose(bool disposing)
		{
			m_threadRunning = false;
		}

		public void ClearList()
		{
			m_getThumbnails = false;
			Controls.Clear();
		}

		public void FindThumbnails()
		{
			m_getThumbnails = true;
		}

		public void AddPhoto(Photo thumbnailPhoto)
		{
			ThumbnailPhotoUserControl newThumbnail = new ThumbnailPhotoUserControl(thumbnailPhoto, ThumbnailSize);
			Controls.Add(newThumbnail);
		}

		public void AddPhoto(FileData photoFile)
		{
			ThumbnailPhotoUserControl newThumbnail = new ThumbnailPhotoUserControl(photoFile, ThumbnailSize);
			Controls.Add(newThumbnail);
		}

		public void AddPhotos(IEnumerable<FileData> photos)
		{
			SuspendLayout();
			foreach (FileData photoFile in photos)
			{
				ThumbnailPhotoUserControl newThumbnail = new ThumbnailPhotoUserControl(photoFile, ThumbnailSize);
				Controls.Add(newThumbnail);
			}
			ResumeLayout();
			FindThumbnails();
		}

		private void SetThumbnalSize()
		{
			SuspendLayout();
			foreach (Control iControl in Controls)
			{
				((ThumbnailPhotoUserControl)iControl).ThumbnailSize = m_thumbnailSize;
			}
			ResumeLayout(true);
		}

		private void ThumbnailProcessingThread()
		{
			while (m_threadRunning)
			{
				if (m_getThumbnails)
				{
					bool allDone = true;

					foreach (Control iControl in Controls)
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
	}
}
