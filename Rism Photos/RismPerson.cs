using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RismPhotos
{
	public class RismPerson
	{
		public string Name { get; set; }
		public RectangleF Location { get; set; }

		public RismPerson() { }

		public RismPerson(string personsName, string locationRectangle)
		{
			Name = personsName;

			string[] values = locationRectangle.Split(',');

			if (values.Length == 4)
			{
				Location = new RectangleF();
			}
		}
	}
}
