using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LiteDB;
using RismPhotos.DataClasses;

namespace RismPhotos
{
	public partial class RismPhotosForm : Form
	{
		private LiteDatabase m_photoDatabase = null;
		private LiteCollection<Folder> m_photoFolders = null;

		public RismPhotosForm()
		{
			InitializeComponent();

			if(Properties.Settings.Default.PhotoDirectories == null)
			{
				Properties.Settings.Default.PhotoDirectories = new System.Collections.Specialized.StringCollection();
			}
		}

		private void RismPhotosForm_Load(object sender, EventArgs e)
		{
			m_photoDatabase = new LiteDatabase("RismPhotos.db");
			m_photoFolders = m_photoDatabase.GetCollection<Folder>();			

			DisplayPhotoFolders();
			/*
			//get a list of the drives
			string[] drives = Environment.GetLogicalDrives();

			foreach (string drive in drives)
			{
				DriveInfo di = new DriveInfo(drive);
				int driveImage;

				switch (di.DriveType)    //set the drive's icon
				{
					case DriveType.CDRom:
						driveImage = 3;
						break;
					case DriveType.Network:
						driveImage = 6;
						break;
					case DriveType.NoRootDirectory:
						driveImage = 8;
						break;
					case DriveType.Unknown:
						driveImage = 8;
						break;
					default:
						driveImage = 2;
						break;
				}

				TreeNode node = new TreeNode(drive.Substring(0, 1), driveImage, driveImage);
				node.Tag = drive;

				if (di.IsReady == true)
					node.Nodes.Add("*");

				PhotosTreeView.Nodes.Add(node);
			}
			*/
		}

		private void DisplayPhotoFolders()
		{
			foreach (string folder in Properties.Settings.Default.PhotoDirectories)
			{
				if (!m_photoFolders.Exists(p => p.FolderName == folder))
				{
					m_photoFolders.Insert(new Folder()
					{
						FolderName = folder,
						DateModified = Directory.GetLastWriteTime(folder),
						ParentFolder = null
					});
				}
				//TreeNode node = new TreeNode(Path.GetDirectoryName(folder), 0, 1);
				TreeNode node = new TreeNode(folder, 0, 1);
				node.Tag = folder;
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
						m_photoFolders.Insert(new Folder()
						{
							FolderName = dir.FullName,
							DateModified = dir.LastWriteTime,
							ParentFolder = m_photoFolders.FindOne(p => p.FolderName == node.Tag.ToString())
						});
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
	}
}
