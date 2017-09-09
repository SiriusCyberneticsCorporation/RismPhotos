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

//using FreeImageAPI;
using System.Windows.Media.Imaging;

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
		internal static Image ThumbnailFromImage(Image image, Size size, System.Drawing.Color backColor)
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

					using (System.Drawing.Brush brush = new SolidBrush(backColor))
					{
						g.FillRectangle(System.Drawing.Brushes.White, 0, 0, scaled.Width, scaled.Height);
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

		
		System.Drawing.Bitmap MakeNet2BitmapFromWPFBitmapSource(BitmapSource src)
		{
			try
			{
				MemoryStream TransportStream = new MemoryStream();
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create(src));
				enc.Save(TransportStream);
				return new System.Drawing.Bitmap(TransportStream);
			}
			catch
			{
				//MessageBox.Show("failed"); 
				return null; 
			}
		}

		/*
		public static System.Drawing.Bitmap BitmapSourceToBitmap2(BitmapSource srs)
		{
			int width = srs.PixelWidth;
			int height = srs.PixelHeight;
			int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
			IntPtr ptr = IntPtr.Zero;
			try
			{
				ptr = Marshal.AllocHGlobal(height * stride);
				srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
				using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr))
				{
					// Clone the bitmap so that we can dispose it and
					// release the unmanaged memory at ptr
					return new System.Drawing.Bitmap(btm);
				}
			}
			finally
			{
				if (ptr != IntPtr.Zero)
					Marshal.FreeHGlobal(ptr);
			}
		}
		*/

		private static BitmapSource GetThumbnail(string filename)
		{
			BitmapSource ret = null;
			BitmapMetadata meta = null;
			double angle = 0;

			try
			{
				//tentative de creation du thumbnail via Bitmap frame : très rapide et très peu couteux en mémoire !
				BitmapFrame frame = BitmapFrame.Create(new Uri(filename),BitmapCreateOptions.DelayCreation,BitmapCacheOption.None);

				if (frame.Thumbnail == null) //echec, on tente avec BitmapImage (plus lent et couteux en mémoire)
				{
					BitmapImage image = new BitmapImage();
					image.DecodePixelHeight = 128; 
					image.BeginInit();
					image.UriSource = new Uri(filename);
					image.CacheOption = BitmapCacheOption.OnLoad;
					image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile; //important pour les performances
					image.EndInit();

					if (image.CanFreeze) //pour éviter les memory leak
						image.Freeze();

					ret = image;
				}
				else
				{
					//récupération des métas de l'image
					meta = frame.Metadata as BitmapMetadata;
					ret = frame.Thumbnail;
				}

				/*
				if ((meta != null) && (ret != null)) //si on a des meta, tentative de récupération de l'orientation
				{
					if (meta.GetQuery("/app1/ifd/{ushort=274}") != null)
					{
						orientation = (ExifOrientations)Enum.Parse(typeof(ExifOrientations), meta.GetQuery("/app1/ifd/{ushort=274}").ToString());
					}

					switch (orientation)
					{
						case ExifOrientations.Rotate90:
							angle = -90;
							break;
						case ExifOrientations.Rotate180:
							angle = 180;
							break;
						case ExifOrientations.Rotate270:
							angle = 90;
							break;
					}

					if (angle != 0) //on doit effectuer une rotation de l'image
					{
						ret = new TransformedBitmap(ret.Clone(), new RotateTransform(angle));
						ret.Freeze();
					}
				}
				*/
			}
			catch (Exception ex)
			{
			}

			return ret;
		}

		public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
		{
			Bitmap bitmap;
			using (var outStream = new MemoryStream())
			{
				BitmapEncoder enc = new BmpBitmapEncoder();
				enc.Frames.Add(BitmapFrame.Create(bitmapsource));
				enc.Save(outStream);
				bitmap = new Bitmap(outStream);
			}
			return bitmap;
		}

		public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
		{

			System.Drawing.Bitmap bmpOut = null;
			try
			{
				Bitmap loBMP = new Bitmap(lcFilename);
				ImageFormat loFormat = loBMP.RawFormat;

				decimal lnRatio;
				int lnNewWidth = 0;
				int lnNewHeight = 0;

				//*** If the image is smaller than a thumbnail just return it
				if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
					return loBMP;


				if (loBMP.Width > loBMP.Height)
				{
					lnRatio = (decimal)lnWidth / loBMP.Width;
					lnNewWidth = lnWidth;
					decimal lnTemp = loBMP.Height * lnRatio;
					lnNewHeight = (int)lnTemp;
				}
				else
				{
					lnRatio = (decimal)lnHeight / loBMP.Height;
					lnNewHeight = lnHeight;
					decimal lnTemp = loBMP.Width * lnRatio;
					lnNewWidth = (int)lnTemp;
				}

				// System.Drawing.Image imgOut = 
				//      loBMP.GetThumbnailImage(lnNewWidth,lnNewHeight,
				//                              null,IntPtr.Zero);

				// *** This code creates cleaner (though bigger) thumbnails and properly
				// *** and handles GIF files better by generating a white background for
				// *** transparent images (as opposed to black)
				bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
				Graphics g = Graphics.FromImage(bmpOut);
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
				g.FillRectangle(System.Drawing.Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
				g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

				loBMP.Dispose();
			}
			catch
			{
				return null;
			}

			return bmpOut;
		}

		/// <summary>
		/// Creates a thumbnail from the given image file.
		/// </summary>
		/// <param name="filename">The filename pointing to an image.</param>
		/// <param name="thumbnailSize">Requested image size.</param>
		/// <param name="backColor">Background color of returned thumbnail.</param>
		/// <returns>The image from the given file or null if an error occurs.</returns>
		internal static Image ThumbnailFromFile(string filename, Size thumbnailSize, out byte[] imageBytes, out RismExif exifData)
		{
			if (thumbnailSize.Width <= 0 || thumbnailSize.Height <= 0)
			{
				throw new ArgumentException();
			}

			//RismExif.ReadWLPGRegions(filename);

			Image source = null;
			Image thumb = null;
			imageBytes = null;
			exifData = null;

			try
			{
				string speeed = string.Empty;

				
				/*
				MemoryStream ms = new MemoryStream();
				System.Windows.Media.Imaging.BmpBitmapEncoder bbe = new BmpBitmapEncoder();
				bbe.Frames.Add(BitmapFrame.Create(new Uri(image.Source.ToString(), UriKind.RelativeOrAbsolute)));
				bbe.Save(ms);
				thumb = System.Drawing.Image.FromStream(ms);
				*/
				/*
				string speeed = string.Empty;
				System.Diagnostics.Stopwatch freeWatch = System.Diagnostics.Stopwatch.StartNew();
				FreeImageBitmap fib = new FreeImageBitmap(filename);
				Size sz1 = GetProportionalSize(thumbnailSize, fib.Size);
				fib.Rescale(sz1, FREE_IMAGE_FILTER.FILTER_BICUBIC);
				thumb = (Image)fib;
				exifData = new RismExif(thumb);
				imageBytes = Utility.ImageToByteArray(thumb);
				freeWatch.Stop();
				Console.WriteLine(freeWatch.ElapsedMilliseconds);
				speeed += "Free: " + freeWatch.ElapsedMilliseconds.ToString();
				*/

				/*
				System.Diagnostics.Stopwatch csWatch = System.Diagnostics.Stopwatch.StartNew();
				using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
				{
					using (Image img = Image.FromStream(stream, true, false))
					{
						Size sz = GetProportionalSize(thumbnailSize, img.Size);
						thumb = new Bitmap(sz.Width, sz.Height);
						using (Graphics g = Graphics.FromImage(thumb))
						{
							g.Clear(System.Drawing.Color.White);
							g.InterpolationMode = InterpolationMode.HighQualityBicubic;
							g.SmoothingMode = SmoothingMode.HighQuality;
							g.PixelOffsetMode = PixelOffsetMode.HighQuality;
							g.CompositingQuality = CompositingQuality.HighQuality;
							g.DrawImage(img, new Rectangle(Point.Empty, sz), new Rectangle(Point.Empty, img.Size), GraphicsUnit.Pixel);
						}

						exifData = new RismExif(img);
						imageBytes = Utility.ImageToByteArray(thumb);
					}
				}
				csWatch.Stop();
				Console.WriteLine(csWatch.ElapsedMilliseconds);
				speeed += ", CS: " + csWatch.ElapsedMilliseconds.ToString();
				*/

				System.Diagnostics.Stopwatch freeWatch = System.Diagnostics.Stopwatch.StartNew();


				//BitmapSource fred = GetThumbnail(filename);
				//thumb = BitmapFromSource(fred);

				thumb = CreateThumbnail(filename, 256, 256);

				/*
				Image tempimg = Image.FromFile(filename);
				Size sz = GetProportionalSize(thumbnailSize, tempimg.Size);
				thumb = tempimg.GetThumbnailImage(sz.Width, sz.Height, null, IntPtr.Zero);
				tempimg.Dispose();
				*/

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
