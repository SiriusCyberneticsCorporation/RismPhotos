using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RismPhotos
{
	public class RismExif
	{
		#region Public Exif Fields

		/// <summary>
		/// Orientation of the image.
		/// </summary>
		public ExifOrientation Orientation;

		/// <summary>
		/// The X and Y resolutions of the image, expressed in ResolutionUnit.
		/// </summary>
		public double XResolution;
		public double YResolution;

		/// <summary>
		/// Resolution unit of the image.
		/// </summary>
		public ExifUnit ResolutionUnit;

		/// <summary>
		/// Date at which the image was taken.
		/// </summary>
		public DateTime DateTaken;

		/// <summary>
		/// Description of the image.
		/// </summary>
		public string Description;
		/// <summary>
		/// Camera manufacturer.
		/// </summary>
		public string CameraMake;
		/// <summary>
		/// Camera model.
		/// </summary>
		public string CameraModel;
		/// <summary>
		/// Software used to create the image.
		/// </summary>
		public string SoftwareUsed;
		/// <summary>
		/// Image artist.
		/// </summary>
		public string Artist;
		/// <summary>
		/// Image copyright.
		/// </summary>
		public string Copyright;
		/// <summary>
		/// Image user comments.
		/// </summary>
		public string UserComment;
		/// <summary>
		/// Exposure time, in seconds.
		/// </summary>
		public double ExposureTime;
		public string ExposureTimeText { get { return ExposureTime >= 1 ? String.Format("{0} sec.", ExposureTime) : String.Format("1/{0} sec.", (int)1/ExposureTime); } }
		/// <summary>
		/// F-number (F-stop) of the camera lens when the image was taken.
		/// </summary>
		public double FNumber;
		public string FNumberText { get { return String.Format("f/{0:g3}", FNumber); } }

		public double FocalLength;
		public string FocalLengthText {  get { return FocalLength.ToString("G0") + " mm"; } }

		public Int16 ISO;
		public string ISOText {  get {  return String.Format("ISO-{0}", ISO); } }

		public Int16 MeteringMode;
		public string MeteringModeText {  get { return meteringModeMap.ContainsKey(MeteringMode) ? meteringModeMap[MeteringMode] : meteringModeMap[0]; } }

		public Int16 ExposureProgram;
		public string ExposureProgramText { get { return ExposureProgramMap.ContainsKey(ExposureProgram) ? ExposureProgramMap[ExposureProgram] : ExposureProgramMap[0]; } }
		
		public Int16 WhiteBalance;
		public string WhiteBalanceText {  get { return WhiteBalanceMap.ContainsKey(WhiteBalance) ? WhiteBalanceMap[WhiteBalance] : WhiteBalanceMap[0]; } }

		/// <summary>
		/// Flash settings of the camera when the image was taken.
		/// </summary>
		public ExifFlash Flash;

		/// <summary>
		/// GPS latitude reference (North, South).
		/// </summary>
		public ExifGpsLatitudeRef GpsLatitudeRef;
		/// <summary>
		/// GPS latitude (degrees, minutes, seconds).
		/// </summary>
		public double[] GpsLatitude = new double[3];
		/// <summary>
		/// GPS longitude reference (East, West).
		/// </summary>
		public ExifGpsLongitudeRef GpsLongitudeRef;
		/// <summary>
		/// GPS longitude (degrees, minutes, seconds).
		/// </summary>
		public double[] GpsLongitude = new double[3];

		#endregion Public Exif Fields

		private Image m_sourceImage = null;
		private List<int> m_propertyIdList = null;
		private Dictionary<Int16, string> meteringModeMap = new Dictionary<Int16, string>()
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
		private Dictionary<Int16, string> WhiteBalanceMap = new Dictionary<Int16, string>()
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
		private Dictionary<Int16, string> ExposureProgramMap = new Dictionary<Int16, string>()
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


		public RismExif(Image sourceImage)
		{
			m_sourceImage = sourceImage;
			m_propertyIdList = m_sourceImage.PropertyIdList.ToList();

			Orientation = GetOrientation(Property(PropertyTagId.PropertyTagOrientation));
			XResolution = GetDouble(Property(PropertyTagId.PropertyTagXResolution));
			YResolution = GetDouble(Property(PropertyTagId.PropertyTagYResolution));
			ResolutionUnit = GetResolutionUnit(Property(PropertyTagId.PropertyTagResolutionUnit));
			DateTaken = GetDateTaken(sourceImage);
			//Description = GetString(Property(PropertyTagId.PropertyTagImageDescription));
			CameraMake = GetString(Property(PropertyTagId.PropertyTagEquipMake));
			CameraModel = GetString(Property(PropertyTagId.PropertyTagEquipModel));
			SoftwareUsed = GetString(Property(PropertyTagId.PropertyTagSoftwareUsed));
			Artist = GetString(Property(PropertyTagId.PropertyTagArtist));
			Copyright = GetString(Property(PropertyTagId.PropertyTagCopyright));
			UserComment = GetString(Property(PropertyTagId.PropertyTagExifUserComment));
			ExposureTime = GetDouble(Property(PropertyTagId.PropertyTagExifExposureTime));
			FNumber = GetDouble(Property(PropertyTagId.PropertyTagExifFNumber));
			FocalLength = GetDouble(Property(PropertyTagId.PropertyTagExifFocalLength));
			ISO = GetInt16(Property(PropertyTagId.PropertyTagExifISOSpeed));
			MeteringMode = GetInt16(Property(PropertyTagId.PropertyTagExifMeteringMode));
			ExposureProgram = GetInt16(Property(PropertyTagId.PropertyTagExifExposureProg));
			//WhiteBalance = GetInt16(Property(PropertyTagId.PropertyTagExifWhiteBalance));
			WhiteBalance = GetInt16(Property(PropertyTagId.PropertyTagExifLightSource));

			Flash = GetFlash(Property(PropertyTagId.PropertyTagExifFlash));
			/*
			GpsLatitudeRef = GetGpsLatitudeRef(Property(PropertyTagId.PropertyTagGpsLatitudeRef));
			GpsLatitude = GetDoubleArray3(Property(PropertyTagId.PropertyTagGpsLatitude));
			GpsLongitudeRef = GetGpsLongitudeRef(Property(PropertyTagId.PropertyTagGpsLongitudeRef));
			GpsLongitude = GetDoubleArray3(Property(PropertyTagId.PropertyTagGpsLongitude));
			*/
		}

		private PropertyItem Property(PropertyTagId tag)
		{
			PropertyItem result = null;

			if (m_propertyIdList.Contains((int)tag))
			{
				result = m_sourceImage.GetPropertyItem((int)tag);
			}

			return result;
		}


		private string PropertyToString(PropertyTagId tag)
		{
			string result = null;
			PropertyItem item = Property(tag);

			if (item != null)
			{
				result = item.Value.ToString();
			}

			return result;
		}

		internal static RotateFlipType OrientationToFlipType(ExifOrientation orientation)
		{
			switch (orientation)
			{
				case ExifOrientation.TopLeft:
					return RotateFlipType.RotateNoneFlipNone;
				case ExifOrientation.TopRight:
					return RotateFlipType.RotateNoneFlipX;
				case ExifOrientation.BottomRight:
					return RotateFlipType.Rotate180FlipNone;
				case ExifOrientation.BottomLeft:
					return RotateFlipType.Rotate180FlipX;
				case ExifOrientation.LeftTop:
					return RotateFlipType.Rotate90FlipX;
				case ExifOrientation.RightTop:
					return RotateFlipType.Rotate90FlipNone;
				case ExifOrientation.RightBottom:
					return RotateFlipType.Rotate270FlipX;
				case ExifOrientation.LeftBottom:
					return RotateFlipType.Rotate270FlipNone;
				default:
					return RotateFlipType.RotateNoneFlipNone;
			}
		}

		private DateTime GetDateTaken(Image sourceImage)
		{
			int propertyToRead = 0;

			if (sourceImage.PropertyIdList.Contains((int)PropertyTagId.PropertyTagExifDTOrig))
			{
				propertyToRead = (int)PropertyTagId.PropertyTagExifDTOrig;
			}
			else if (sourceImage.PropertyIdList.Contains((int)PropertyTagId.PropertyTagDateTime))
			{
				propertyToRead = (int)PropertyTagId.PropertyTagDateTime;
			}

			if (propertyToRead <= 0)
			{
				return DateTime.MinValue;
			}

			PropertyItem dateProperty = sourceImage.GetPropertyItem(propertyToRead);
			ASCIIEncoding encoding = new ASCIIEncoding();
			string propValue = encoding.GetString(dateProperty.Value, 0, dateProperty.Len - 1);
			DateTime dateTaken = DateTime.ParseExact(propValue, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

			return dateTaken;
		}

		private ExifOrientation GetOrientation(PropertyItem item)
		{
			if (item == null)
			{
				return ExifOrientation.Unknown;
			}
			return NumberToEnum<ExifOrientation>((int)GetInt16(item));
		}

		private ExifUnit GetResolutionUnit(PropertyItem item)
		{
			if (item == null)
			{
				return ExifUnit.Undefined;
			}
			return NumberToEnum<ExifUnit>((int)GetInt16(item));
		}

		private ExifFlash GetFlash(PropertyItem item)
		{
			if (item == null)
			{
				return ExifFlash.No;
			}
			return NumberToEnum<ExifFlash>((int)GetInt16(item));
		}

		private ExifGpsLatitudeRef GetGpsLatitudeRef(PropertyItem item)
		{
			if (item == null)
			{
				return ExifGpsLatitudeRef.Unknown;
			}
			return NumberToEnum<ExifGpsLatitudeRef>((int)GetInt16(item));
		}

		private ExifGpsLongitudeRef GetGpsLongitudeRef(PropertyItem item)
		{
			if (item == null)
			{
				return ExifGpsLongitudeRef.Unknown;
			}
			return NumberToEnum<ExifGpsLongitudeRef>((int)GetInt16(item));
		}

		private T NumberToEnum<T>(int number)
		{
			return (T)Enum.ToObject(typeof(T), number);
		}

		private string GetString(PropertyItem item)
		{
			if (item == null)
			{
				return string.Empty;
			}

			string value = string.Empty;
			ASCIIEncoding encoding = new ASCIIEncoding();

			value = encoding.GetString(item.Value, 0, item.Len - 1);

			return value;
		}

		private Int16 GetInt16(PropertyItem item)
		{
			if (item == null) return 0;

			Int16 value = BitConverter.ToInt16(item.Value, 0);
			return value;
		}

		private Int32 GetInt32(PropertyItem item)
		{
			if (item == null) return 0;

			Int32 value = BitConverter.ToInt32(item.Value, 0);
			return value;
		}

		private double GetDouble(PropertyItem item)
		{
			if (item == null) return 0;

			double value = 0;
			UInt32 numberator = BitConverter.ToUInt32(item.Value, 0);
			UInt32 denominator = BitConverter.ToUInt32(item.Value, 4);

			try
			{
				value = (double)numberator / (double)denominator;

				if (value == double.NaN)
				{
					value = 0;
				}
			}
			catch (DivideByZeroException)
			{
				value = 0;
			}

			return value;
		}

		private double[] GetDoubleArray3(PropertyItem item)
		{
			if (item == null) return null;

			double[] doubleArray = new double[3];

			uint degreesNumerator = BitConverter.ToUInt32(item.Value, 0);
			uint degreesDenominator = BitConverter.ToUInt32(item.Value, 4);
			doubleArray[0] = degreesNumerator / (double)degreesDenominator;

			uint minutesNumerator = BitConverter.ToUInt32(item.Value, 8);
			uint minutesDenominator = BitConverter.ToUInt32(item.Value, 12);
			doubleArray[1] = minutesNumerator / (double)minutesDenominator;

			uint secondsNumerator = BitConverter.ToUInt32(item.Value, 16);
			uint secondsDenominator = BitConverter.ToUInt32(item.Value, 20);
			doubleArray[2] = secondsNumerator / (double)secondsDenominator;

			return doubleArray;
		}

	}
}
