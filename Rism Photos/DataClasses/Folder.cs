using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RismPhotos.DataClasses
{
	public class Folder
	{
		public int ID { get; set; }
		public Folder ParentFolder { get; set; }
		public string FolderName { get; set; }
		public DateTime DateModified { get; set; }
	}
}
