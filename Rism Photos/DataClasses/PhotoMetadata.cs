using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RismPhotos.DataClasses
{
	public class PhotoMetadata
	{
		public int ID { get; set; }
		
		/// <summary>
		/// Image artist/creator.
		/// </summary>
		public string Author { get; set; }
		/// <summary>
		/// Camera manufacturer.
		/// </summary>
		public string CameraMake { get; set; }
		/// <summary>
		/// Camera model.
		/// </summary>
		public string CameraModel { get; set; }
		/// <summary>
		/// Image copyright.
		/// </summary>
		public string Copyright { get; set; }
		/// <summary>
		/// Date at which the image was taken.
		/// </summary>
		public DateTime DateTaken { get; set; }
		/// <summary>
		/// Cameras Exposure Program.
		/// </summary>
		public Int16 ExposureProgram { get; set; }
		public string ExposureProgramText { get { return ExposureProgramMap.ContainsKey(ExposureProgram) ? ExposureProgramMap[ExposureProgram] : ExposureProgramMap[0]; } }
		/// <summary>
		/// Exposure time, in seconds.
		/// </summary>
		public double ExposureTime { get; set; }
		public string ExposureTimeText { get { return ExposureTime >= 1 ? String.Format("{0} sec.", ExposureTime) : String.Format("1/{0} sec.", (int)1 / ExposureTime); } }
		/// <summary>
		/// F-number (F-stop) of the camera lens when the image was taken.
		/// </summary>
		public double FNumber { get; set; }
		public string FNumberText { get { return String.Format("f/{0:g3}", FNumber); } }
		/// <summary>
		/// Flash settings of the camera when the image was taken.
		/// </summary>
		public ExifFlash Flash { get; set; }
		/// <summary>
		/// Lens Focal Length
		/// </summary>
		public double FocalLength { get; set; }
		public string FocalLengthText { get { return FocalLength.ToString("G0") + " mm"; } }
		/// <summary>
		/// GPS Altitude.
		/// </summary>
		public double GpsAltitude { get; set; }
		/// <summary>
		/// GPS latitude (degrees, minutes, seconds).
		/// </summary>
		public double GpsLatitude { get; set; }
		/// <summary>
		/// GPS latitude reference (North, South).
		/// </summary>
		public ExifGpsLatitudeRef GpsLatitudeRef { get; set; }
		/// <summary>
		/// GPS longitude (degrees, minutes, seconds).
		/// </summary>
		public double GpsLongitude { get; set; }
		/// <summary>
		/// GPS longitude reference (East, West).
		/// </summary>
		public ExifGpsLongitudeRef GpsLongitudeRef { get; set; }
		/// <summary>
		/// Image ISO.
		/// </summary>
		public Int16 ISO { get; set; }
		public string ISOText { get { return String.Format("ISO-{0}", ISO); } }
		/// <summary>
		/// Should probably use GPS coordinates NOT THIS.
		/// </summary>
		public string Location;
		/// <summary>
		/// Camera Metering Mode.
		/// </summary>
		public Int16 MeteringMode { get; set; }
		public string MeteringModeText { get { return MeteringModeMap.ContainsKey(MeteringMode) ? MeteringModeMap[MeteringMode] : MeteringModeMap[0]; } }
		/// <summary>
		/// Orientation of the image.
		/// </summary>
		public ExifOrientation Orientation { get; set; }
		/// <summary>
		/// User applied image rating.
		/// </summary>
		public int Rating;
		/// <summary>
		/// Software used to create/edit the image.
		/// </summary>
		public string SoftwareUsed { get; set; }
		/// <summary>
		/// Image Subject.
		/// </summary>
		public string Subject { get; set; }
		/// <summary>
		/// Iamage Title.
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Camera White Balance setting.
		/// </summary>
		public Int16 WhiteBalance { get; set; }
		public string WhiteBalanceText { get { return WhiteBalanceMap.ContainsKey(WhiteBalance) ? WhiteBalanceMap[WhiteBalance] : WhiteBalanceMap[0]; } }
		/// <summary>
		/// The X and Y dimensions of the image.
		/// </summary>
		public double XPixels { get; set; }
		public double YPixels { get; set; }
		/// <summary>
		/// Image Keywords
		/// </summary>
		public List<string> Keywords { get; set; }
		/// <summary>
		/// People tagged in image.
		/// </summary>
		public List<RismPerson> People { get; set; }

		public PhotoMetadata() { }

		public PhotoMetadata(string filename)
		{
			string microsoftRegions = @"/xmp/RegionInfo/Regions";
			string microsoftPersonDisplayName = @"/PersonDisplayName";
			string microsoftRectangle = @"/Rectangle";
			//BitmapCreateOptions createOptions = BitmapCreateOptions.IgnoreImageCache | BitmapCreateOptions.IgnoreColorProfile;

			using (Stream sourceStream = File.Open(filename, FileMode.Open, FileAccess.Read))
			{
				BitmapDecoder sourceDecoder = BitmapDecoder.Create(sourceStream, BitmapCreateOptions.None/*createOptions*/, BitmapCacheOption.None);

				// Check source has valid frames
				if (sourceDecoder.Frames[0] != null && sourceDecoder.Frames[0].Metadata != null)
				{
					BitmapMetadata sourceMetadata = sourceDecoder.Frames[0].Metadata as BitmapMetadata;

					if (sourceMetadata.Author != null && sourceMetadata.Author[0] != null)
					{
						Author = sourceMetadata.Author[0];
					}
					CameraMake = sourceMetadata.CameraManufacturer;
					CameraModel = sourceMetadata.CameraModel;
					Copyright = sourceMetadata.Copyright;
					DateTaken = Convert.ToDateTime(sourceMetadata.DateTaken);
					ExposureProgram = Convert.ToInt16(sourceMetadata.GetQuery(@"System.Photo.ProgramMode"));
					ExposureTime = Convert.ToDouble(sourceMetadata.GetQuery(@"System.Photo.ExposureTime"));
					FNumber = Convert.ToDouble(sourceMetadata.GetQuery(@"System.Photo.FNumber"));
					Flash = Utility.NumberToEnum<ExifFlash>(Convert.ToInt16(sourceMetadata.GetQuery(@"System.Photo.Flash")));
					FocalLength = Convert.ToDouble(sourceMetadata.GetQuery(@"System.Photo.FocalLength"));

					GpsAltitude = ConvertToUnsignedRational(Convert.ToUInt64(sourceMetadata.GetQuery("/app1/ifd/gps/subifd:{ulong=6}")));
					ulong[] latitude = sourceMetadata.GetQuery("/app1/ifd/gps/subifd:{ulong=2}") as ulong[];
					ulong[] longitude = sourceMetadata.GetQuery("/app1/ifd/gps/subifd:{ulong=4}") as ulong[];
					GpsLatitude = ConvertCoordinate(latitude);
					GpsLongitude = ConvertCoordinate(longitude);



					if (sourceMetadata.Keywords != null)
					{
						Keywords = sourceMetadata.Keywords.ToList();
					}
					SoftwareUsed = sourceMetadata.ApplicationName;
					Rating = sourceMetadata.Rating;
					Location = sourceMetadata.Location;

					Orientation = Utility.NumberToEnum<ExifOrientation>(Convert.ToInt16(sourceMetadata.GetQuery(@"System.Photo.Orientation")));
					XPixels = sourceDecoder.Frames[0].PixelWidth;
					YPixels = sourceDecoder.Frames[0].PixelHeight;
					//ResolutionUnit = GetResolutionUnit(Property(PropertyTagId.PropertyTagResolutionUnit));
					//UserComment = GetString(Property(PropertyTagId.PropertyTagExifUserComment));
					ISO = Convert.ToInt16(sourceMetadata.GetQuery(@"System.Photo.ISOSpeed"));
					MeteringMode = Convert.ToInt16(sourceMetadata.GetQuery(@"System.Photo.MeteringMode"));
					WhiteBalance = Convert.ToInt16(sourceMetadata.GetQuery(@"System.Photo.WhiteBalance"));

					Title = sourceMetadata.Title;
					Subject = sourceMetadata.Subject;

					People = new List<RismPerson>();

					// Check there is a RegionInfo
					if (sourceMetadata.ContainsQuery(microsoftRegions))
					{
						BitmapMetadata regionsMetadata = sourceMetadata.GetQuery(microsoftRegions) as BitmapMetadata;

						// Loop through each Region
						foreach (string regionQuery in regionsMetadata)
						{
							string regionFullQuery = microsoftRegions + regionQuery;

							// Query for all the data for this region
							BitmapMetadata regionMetadata = sourceMetadata.GetQuery(regionFullQuery) as BitmapMetadata;

							if (regionMetadata != null)
							{
								if (regionMetadata.ContainsQuery(microsoftPersonDisplayName) &&
									regionMetadata.ContainsQuery(microsoftRectangle))
								{
									People.Add(new RismPerson(regionMetadata.GetQuery(microsoftPersonDisplayName).ToString(),
															  regionMetadata.GetQuery(microsoftRectangle).ToString()));
									Console.WriteLine(regionMetadata.GetQuery(microsoftRectangle).ToString());
									Console.WriteLine(regionMetadata.GetQuery(microsoftPersonDisplayName).ToString());
								}

							}
						}
					}
				}
			}
		}

		static double ConvertCoordinate(ulong[] coordinates)
		{
			if (coordinates == null)
				return 0;

			double degrees = ConvertToUnsignedRational(coordinates[0]);
			double minutes = ConvertToUnsignedRational(coordinates[1]);
			double seconds = ConvertToUnsignedRational(coordinates[2]);
			return degrees + (minutes / 60.0) + (seconds / 3600);

		}
		static double ConvertToUnsignedRational(ulong value)
		{
			return (value & 0xFFFFFFFFL) / (double)((value & 0xFFFFFFFF00000000L) >> 32);
		}

		private static Dictionary<Int16, string> MeteringModeMap = new Dictionary<Int16, string>()
		{
			{ 0, "Unknown" },
			{ 1, "Average" },
			{ 2, "Center-Weighted" },
			{ 3, "Spot" },
			{ 4, "Multi-Spot" },
			{ 5, "Pattern" },
			{ 6, "Partial" },
			{ 255, "Other" }
		};

		private static Dictionary<Int16, string> WhiteBalanceMap = new Dictionary<Int16, string>()
		{
			{ 0, "Unknown" },
			{ 1, "Daylight" },
			{ 2, "Fluorescent" },
			{ 3, "Tungsten (Incandescent)" },
			{ 4, "Flash" },
			{ 9, "Fine weather" },
			{ 10, "Cloudy" },
			{ 11, "Shade" },
			{ 12, "Daylight fluorescent" },
			{ 13, "Day white fluorescent" },
			{ 14, "Cool white fluorescent" },
			{ 15, "White fluorescent" },
			{ 16, "Warm White fluorescent" },
			{ 17, "Standard light A" },
			{ 18, "Standard light B" },
			{ 19, "Standard light C" },
			{ 20, "D55" },
			{ 21, "D65" },
			{ 22, "D75" },
			{ 23, "D50" },
			{ 24, "ISO studio tungsten" },
			{ 255, "Other light source" },
		};

		/// <summary>
		/// Map of exposure value names to their unsigned short representations
		/// </summary>
		private static Dictionary<Int16, string> ExposureProgramMap = new Dictionary<Int16, string>()
		{
			{ 0, "Not defined" },
			{ 1, "Manual" },
			{ 2, "Normal program" },
			{ 3, "Aperture priority" },
			{ 4, "Shutter priority" },
			{ 5, "Creative program" },
			{ 6, "Action program" },
			{ 7, "Portrait mode" },
			{ 8, "Landscape mode" }
		};
	}
}
