using RismPhotos.DataClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RismPhotos
{
	public class Utility
	{
		public static T NumberToEnum<T>(int number)
		{
			return (T)Enum.ToObject(typeof(T), number);
		}

		public static Image ImageFromByteArray(byte[] byteArrayIn)
		{
			return (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayIn));
		}

		public static byte[] ImageToByteArray(Image sourceImage)
		{
			if(sourceImage == null)
			{
				return null;
			}
			ImageConverter converter = new ImageConverter();
			byte[] byteArrayOut = (byte[])converter.ConvertTo(sourceImage, typeof(byte[]));

			return byteArrayOut;
		}

		/// <summary>
		/// Creates a thumbnail from the given image.
		/// </summary>
		/// <param name="sourceImage">The source image.</param>
		/// <param name="thumbnailSize">Requested image size.</param>
		/// <param name="backColor">Background color of returned thumbnail.</param>
		/// <returns>The image from the given file or null if an error occurs.</returns>
		internal static Image ThumbnailFromImage(Image sourceImage, Size thumbnailSize, System.Drawing.Color backColor)
		{
			if (thumbnailSize.Width <= 0 || thumbnailSize.Height <= 0)
			{
				throw new ArgumentException();
			}

			Image thumb = null;
			try
			{
				Size scaled = GetSizedImageBounds(sourceImage, thumbnailSize);
				thumb = new Bitmap(scaled.Width, scaled.Height);
				using (Graphics g = Graphics.FromImage(thumb))
				{
					g.PixelOffsetMode = PixelOffsetMode.None;
					g.InterpolationMode = InterpolationMode.HighQualityBicubic;

					using (System.Drawing.Brush brush = new SolidBrush(backColor))
					{
						g.FillRectangle(System.Drawing.Brushes.White, 0, 0, scaled.Width, scaled.Height);
					}

					g.DrawImage(sourceImage, 0, 0, scaled.Width, scaled.Height);
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

		
		public static Bitmap CreateThumbnail(string filename, int thumbWidth, int thumbHeight)
		{
			Bitmap thumgnail = null;
			try
			{
				int newWidth = 0;
				int newHeight = 0;
				double widthHeightRatio = 1;
				Bitmap souceBitmap = new Bitmap(filename);

				// If the image is smaller than the required thumbnail then just return it.
				if (souceBitmap.Width < thumbWidth && souceBitmap.Height < thumbHeight)
				{
					return souceBitmap;
				}

				if (souceBitmap.Width > souceBitmap.Height)
				{
					widthHeightRatio = (double)thumbWidth / (double)souceBitmap.Width;
					newWidth = thumbWidth;
					newHeight = (int)(souceBitmap.Height* widthHeightRatio);
				}
				else
				{
					widthHeightRatio = (double)thumbHeight / (double)souceBitmap.Height;
					newWidth = (int)(souceBitmap.Width * widthHeightRatio);
					newHeight = thumbHeight;
				}

				thumgnail = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				Graphics g = Graphics.FromImage(thumgnail);

				g.CompositingQuality = CompositingQuality.HighSpeed;
				g.SmoothingMode = SmoothingMode.HighSpeed;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.PixelOffsetMode = PixelOffsetMode.Half;
				g.CompositingMode = CompositingMode.SourceCopy;

				g.FillRectangle(System.Drawing.Brushes.White, 0, 0, newWidth, newHeight);

				g.DrawImage(souceBitmap, 0, 0, newWidth, newHeight);

				foreach (PropertyItem item in souceBitmap.PropertyItems)
				{
					thumgnail.SetPropertyItem(item);
				}
				souceBitmap.Dispose();
			}
			catch
			{
				return null;
			}

			return thumgnail;
		}

		/// <summary>
		/// Creates a thumbnail from the given image file.
		/// </summary>
		/// <param name="filename">The filename pointing to an image.</param>
		/// <param name="thumbnailSize">Requested image size.</param>
		/// <param name="backColor">Background color of returned thumbnail.</param>
		/// <returns>The image from the given file or null if an error occurs.</returns>
		internal static Image ThumbnailFromFile(string filename, Size thumbnailSize, out byte[] imageBytes, out PhotoMetadata exifData)
		{
			if (thumbnailSize.Width <= 0 || thumbnailSize.Height <= 0)
			{
				throw new ArgumentException();
			}

			Image source = null;
			Image thumb = null;
			imageBytes = null;
			exifData = null;

			try
			{
				string speeed = string.Empty;
											
				System.Diagnostics.Stopwatch csWatch = System.Diagnostics.Stopwatch.StartNew();

				csWatch.Stop();
				Console.WriteLine(csWatch.ElapsedMilliseconds);
				speeed += ", CS: " + csWatch.ElapsedMilliseconds.ToString();				

				System.Diagnostics.Stopwatch freeWatch = System.Diagnostics.Stopwatch.StartNew();

				thumb = CreateThumbnail(filename, 256, 256);
				imageBytes = Utility.ImageToByteArray(thumb);
				exifData = new PhotoMetadata(filename);

				freeWatch.Stop();
				Console.WriteLine(freeWatch.ElapsedMilliseconds);
				speeed += ", Free: " + freeWatch.ElapsedMilliseconds.ToString();


				//GC.Collect();
			}
			catch (Exception ex)
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
                nHeight = (int) /*Math.Round*/(nWidth / sRealRatio);
            }
            else
            {
                nHeight = Math.Min(szMax.Height, szReal.Height);
                nWidth = (int) /*Math.Round*/(nHeight* sRealRatio);
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
