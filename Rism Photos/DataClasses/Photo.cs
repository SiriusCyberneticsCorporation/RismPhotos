using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace RismPhotos.DataClasses
{
	public class Photo
	{
		public int ID { get; set; }
		public Folder ParentFolder { get; set; }
		public string Filename { get; set; }
		public DateTime DateModified { get; set; }
		public byte[] ThumbnailBytes { get; set; }
	}
}
