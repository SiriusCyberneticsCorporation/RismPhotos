using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RismPhotos.DataClasses
{
	public class Keyword
	{
		public int ID { get; set; }
		public int KeywordParentID { get; set; }
		public string KeywordText { get; set; }
	}
}
