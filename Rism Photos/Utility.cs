using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RismPhotos
{
	public class Utility
	{
		public static Image ImageFromByteArray(byte[] byteArrayIn)
		{
			return (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayIn));
		}

		public static byte[] ImageToByteArray(Image image)
		{
			if(image == null)
			{
				return null;
			}
			ImageConverter _imageConverter = new ImageConverter();
			byte[] xByte = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
			return xByte;
			/*
			using (var ms = new MemoryStream())
			{
				image.Save(ms, image.RawFormat);
				return ms.ToArray();
			}
			*/
		}

		/// <summary>
		/// Creates a thumbnail from the given image.
		/// </summary>
		/// <param name="image">The source image.</param>
		/// <param name="size">Requested image size.</param>
		/// <param name="backColor">Background color of returned thumbnail.</param>
		/// <returns>The image from the given file or null if an error occurs.</returns>
		internal static Image ThumbnailFromImage(Image image, Size size, Color backColor)
		{
			if (size.Width <= 0 || size.Height <= 0)
				throw new ArgumentException();

			Image thumb = null;
			try
			{
				Size scaled = GetSizedImageBounds(image, size);
				thumb = new Bitmap(scaled.Width, scaled.Height);
				using (Graphics g = Graphics.FromImage(thumb))
				{
					g.PixelOffsetMode = PixelOffsetMode.None;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;

					using (Brush brush = new SolidBrush(backColor))
					{
						g.FillRectangle(Brushes.White, 0, 0, scaled.Width, scaled.Height);
					}

					g.DrawImage(image, 0, 0, scaled.Width, scaled.Height);
				}
			}
			catch
			{
				if (thumb != null)
				{
					thumb.Dispose();
				}
				thumb = null;
			}

			return thumb;
		}

		/// <summary>
		/// Creates a thumbnail from the given image file.
		/// </summary>
		/// <param name="filename">The filename pointing to an image.</param>
		/// <param name="thumbnailSize">Requested image size.</param>
		/// <param name="backColor">Background color of returned thumbnail.</param>
		/// <returns>The image from the given file or null if an error occurs.</returns>
		internal static Image ThumbnailFromFile(string filename, Size thumbnailSize, out byte[] imageBytes)
		{
			if (thumbnailSize.Width <= 0 || thumbnailSize.Height <= 0)
			{
				throw new ArgumentException();
			}

			Image source = null;
			Image thumb = null;
			try
			{
				//using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(filename)))
				using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
				{
					using (Image img = Image.FromStream(stream, true, false))
					//using (Image img = Bitmap.FromFile(filename))
					//using (Image img = Image.FromFile(filename))
					//using (Image img = Image.FromStream(ms))
					{
						/*
						int thumbWidth = thumbnailSize.Width;
						int thumbHeight = thumbnailSize.Height;
						if (img.Width > img.Height)
						{
							thumbHeight = (int)((float)thumbnailSize.Height * (float)img.Height / (float)img.Width);
						}
						else if (img.Height > img.Width)
						{
							thumbWidth = (int)((float)thumbnailSize.Width * (float)img.Width / (float)img.Height);
						}
						//thumb = img.GetThumbnailImage(thumbWidth, thumbHeight, null, IntPtr.Zero);
						thumb = new Bitmap(img, thumbWidth, thumbHeight);
						*/
						
						Size sz = GetProportionalSize(thumbnailSize, img.Size);
						thumb = new Bitmap(sz.Width, sz.Height);
						using (Graphics g = Graphics.FromImage(thumb))
						{
							g.Clear(Color.White);
							g.InterpolationMode = InterpolationMode.HighQualityBicubic;
							g.SmoothingMode = SmoothingMode.HighQuality;
							g.PixelOffsetMode = PixelOffsetMode.HighQuality;
							g.CompositingQuality = CompositingQuality.HighQuality;
							g.DrawImage(img, new Rectangle(Point.Empty, sz), new Rectangle(Point.Empty, img.Size), GraphicsUnit.Pixel);
						}
					}
				}
			}
			catch
			{
				if (source != null)
				{
					source.Dispose();
				}
				source = null;
				if (thumb != null)
				{
					thumb.Dispose();
				}
				thumb = null;
			}

			imageBytes = Utility.ImageToByteArray(thumb);

			return thumb;
		}

		private static Size GetProportionalSize(Size szMax, Size szReal)
        {
            int nWidth;
            int nHeight;
            double sMaxRatio;
            double sRealRatio;
 
            if (szMax.Width< 1 || szMax.Height< 1 || szReal.Width< 1 || szReal.Height< 1)
                return Size.Empty;
 
            sMaxRatio = (double) szMax.Width / (double) szMax.Height;
            sRealRatio = (double) szReal.Width / (double) szReal.Height;
 
            if (sMaxRatio<sRealRatio)
            {
                nWidth = Math.Min(szMax.Width, szReal.Width);
                nHeight = (int) Math.Round(nWidth / sRealRatio);
            }
            else
            {
                nHeight = Math.Min(szMax.Height, szReal.Height);
                nWidth = (int) Math.Round(nHeight* sRealRatio);
            }
 
            return new Size(nWidth, nHeight);
        }

		/// <summary>
		/// Gets the scaled size of an image required to fit
		/// in to the given size keeping the image aspect ratio.
		/// </summary>
		/// <param name="image">The source image.</param>
		/// <param name="fit">The size to fit in to.</param>
		/// <returns></returns>
		internal static Size GetSizedImageBounds(Image image, Size fit)
		{
			float f = System.Math.Max((float)image.Width / (float)fit.Width, (float)image.Height / (float)fit.Height);
			if (f < 1.0f) f = 1.0f; // Do not upsize small images
			int width = (int)System.Math.Round((float)image.Width / f);
			int height = (int)System.Math.Round((float)image.Height / f);
			return new Size(width, height);
		}

		/// <summary>
		/// Gets the bounding rectangle of an image required to fit
		/// in to the given rectangle keeping the image aspect ratio.
		/// </summary>
		/// <param name="image">The source image.</param>
		/// <param name="fit">The rectangle to fit in to.</param>
		/// <param name="hAlign">Horizontal image aligment in percent.</param>
		/// <param name="vAlign">Vertical image aligment in percent.</param>
		/// <returns></returns>
		internal static Rectangle GetSizedImageBounds(Image image, Rectangle fit, float hAlign, float vAlign)
		{
			Size scaled = GetSizedImageBounds(image, fit.Size);
			int x = fit.Left + (int)(hAlign / 100.0f * (float)(fit.Width - scaled.Width));
			int y = fit.Top + (int)(vAlign / 100.0f * (float)(fit.Height - scaled.Height));

			return new Rectangle(x, y, scaled.Width, scaled.Height);
		}
	}
}
