using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using LiteDB;
using RismPhotos.DataClasses;

namespace RismPhotos
{
	public partial class RismPhotosForm : Form
	{
		private object m_folderListLock = new object();
		private object m_photoListLock = new object();
		private Folder m_currentFolder = null;
		private LiteDatabase m_photoDatabase = null;
		private LiteCollection<Folder> m_photoFolders = null;
		private LiteCollection<Photo> m_photos = null;
		private List<Folder> m_foldersToUpdate = new List<Folder>();

		public RismPhotosForm()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			InitializeComponent();

			if (Properties.Settings.Default.PhotoDirectories == null)
			{
				Properties.Settings.Default.PhotoDirectories = new System.Collections.Specialized.StringCollection();
			}
		}

		private void RismPhotosForm_Load(object sender, EventArgs e)
		{
			Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;

			m_photoDatabase = new LiteDatabase("RismPhotos.db");
			m_photoFolders = m_photoDatabase.GetCollection<Folder>();
			m_photos = m_photoDatabase.GetCollection<Photo>();

			ThumbnailList.ThumbnailAdded += ThumbnailList_ThumbnailAdded;

			Thread displayThread = new Thread(new ThreadStart(DisplayPhotoFolders));
			displayThread.Start();
		}

		private void ThumbnailList_ThumbnailAdded(Photo newPhoto)
		{
			newPhoto.ParentFolder = m_currentFolder;

			lock (m_photoListLock)
			{
				Photo existingPhoto = m_photos.FindOne(p => p.Filename == newPhoto.Filename);

				if (existingPhoto == null)
				{
					m_photos.Insert(newPhoto);
				}
				else
				{
					existingPhoto.DateModified = newPhoto.DateModified;
					existingPhoto.ThumbnailBytes = newPhoto.ThumbnailBytes;
					existingPhoto.ExifData = newPhoto.ExifData;
					m_photos.Update(existingPhoto);
				}
			}
		}

		private void DisplayPhotoFolders()
		{
			foreach (string folderName in Properties.Settings.Default.PhotoDirectories)
			{
				if (!m_photoFolders.Exists(p => p.FolderName == folderName))
				{
					Folder newFolder = new Folder()
					{
						FolderName = folderName,
						DateModified = Directory.GetLastWriteTime(folderName),
						ParentFolder = null
					};

					m_photoFolders.Insert(newFolder);

					lock(m_folderListLock)
					{
						m_foldersToUpdate.Add(newFolder);
					}
				}

				AddRootNode(folderName);
			}
		}

		private void AddRootNode(string folderName)
		{
			if(InvokeRequired)
			{
				MethodInvoker del = delegate { AddRootNode(folderName); };
				Invoke(del);
			}
			else
			{
				TreeNode node = new TreeNode(folderName, 0, 1);
				node.Tag = folderName;
				node.Nodes.Add("*");
				PhotosTreeView.Nodes.Add(node);
			}
		}

		private void FillChildFolderNodes(TreeNode node)
		{
			try
			{
				node.ImageIndex = 1;
				DirectoryInfo dirs = new DirectoryInfo(node.Tag.ToString());
				foreach (DirectoryInfo dir in dirs.GetDirectories())
				{
					if (!m_photoFolders.Exists(p => p.FolderName == dir.FullName))
					{
						Folder newFolder = new Folder()
						{
							FolderName = dir.FullName,
							DateModified = dir.LastWriteTime,
							ParentFolder = m_photoFolders.FindOne(p => p.FolderName == node.Tag.ToString())
						};

						m_photoFolders.Insert(newFolder);

						lock (m_folderListLock)
						{
							m_foldersToUpdate.Add(newFolder);
						}
					}
					TreeNode newnode = new TreeNode(dir.Name, 0, 1);
					newnode.Tag = dir.FullName;
					node.Nodes.Add(newnode);
					newnode.Nodes.Add("*");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
		}

		private void PhotosTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Nodes[0].Text == "*")
			{
				e.Node.Nodes.Clear();
				FillChildFolderNodes(e.Node);
			}
		}

		private void PhotosTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			e.Node.Expand();
		}

		private void PhotosTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			m_currentFolder = m_photoFolders.FindOne(p => p.FolderName == e.Node.Tag.ToString());

			ThumbnailList.ClearList();
			//ThumbnailList.SuspendListUpdate();
			foreach (string fileType in new string[] { "*.jpg", "*.jpeg", "*.png", "*.raw" })
			{
				//ThumbnailList.AddPhotos(FastDirectoryEnumerator.EnumerateFiles(e.Node.Tag.ToString(), fileType));
				foreach (FileData photoFile in FastDirectoryEnumerator.EnumerateFiles(e.Node.Tag.ToString(), fileType))
				{
					Photo existingPhoto = null;
					lock(m_photoListLock)
					{
						existingPhoto = m_photos.FindOne(p => p.Filename == photoFile.Path);
					}
					if (existingPhoto != null && existingPhoto.ThumbnailBytes != null)
					{
						ThumbnailList.AddPhoto(existingPhoto);
					}
					else
					{
						ThumbnailList.AddPhoto(photoFile);
					}
				}
			}
			//ThumbnailList.ResumeListUpdate();
			ThumbnailList.FindThumbnails();
		}

		private void addPhotoFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog iFolderBrowserDialog = new FolderBrowserDialog();
			iFolderBrowserDialog.Description = "Select Photo Folder";
			iFolderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
			iFolderBrowserDialog.ShowNewFolderButton = true;

			if(iFolderBrowserDialog.ShowDialog(this) == DialogResult.OK)
			{
				Properties.Settings.Default.PhotoDirectories.Add(iFolderBrowserDialog.SelectedPath);
				Properties.Settings.Default.Save();
				DisplayPhotoFolders();
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void x24ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThumbnailList.ThumbnailSize = new Size(24, 24);
		}

		private void x32ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThumbnailList.ThumbnailSize = new Size(32, 32);
		}

		private void x48ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThumbnailList.ThumbnailSize = new Size(48, 48);
		}

		private void x64ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThumbnailList.ThumbnailSize = new Size(64, 64);
		}

		private void x96ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThumbnailList.ThumbnailSize = new Size(96, 96);
		}

		private void x128ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThumbnailList.ThumbnailSize = new Size(128, 128);
		}

		private void x256ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ThumbnailList.ThumbnailSize = new Size(256, 256);
		}

		private void RismPhotosForm_StyleChanged(object sender, EventArgs e)
		{
			//Application.DoEvents();

		}
	}
}
