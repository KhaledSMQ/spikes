using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ImageResizing
{
	class Program
	{
		static void Main(string[] args)
		{
			var image = Image.FromFile("source.jpg");
			var size = new Size(100, 150);
			var resized = ResizeImage(image, size);
			resized.Save("resized.jpg", ImageFormat.Jpeg);
		}

		private static Image ResizeImage(Image imageToResize, Size newSize)
		{
			var oldWidth = imageToResize.Width;
			var oldHeight = imageToResize.Height;

			var widthRatio = newSize.Width / (float)oldWidth;
			var heightRatio = newSize.Height/(float)oldHeight;
			var ratio = Math.Min(widthRatio, heightRatio);

			var newWidth = (int) (ratio * oldWidth);
			var newHeight = (int) (ratio * oldHeight);

			var b = new Bitmap(newWidth, newHeight);
			using (var g = Graphics.FromImage(b))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.DrawImage(imageToResize, 0, 0, newWidth, newHeight);
				g.Dispose();
			}

			return b;
		}
	}
}
