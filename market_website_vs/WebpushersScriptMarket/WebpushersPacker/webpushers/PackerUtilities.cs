using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace webpushers
{
    /// <summary>
    /// Utility functions for the script packager
    /// </summary>
    public sealed class PackerUtilities
    {
        private static System.Drawing.Imaging.ImageFormat DEFAULT_FORMAT = ImageFormat.Png;

        // code duplication but speed. fix later who cares.
        public static string ImageToBase64Encoded(Image img)
        {
            if (img == null)
            {
                ThrowError(new Exception("No image was provided!"));
            }

            // Encode image as base64 PNG
            MemoryStream stream = new MemoryStream();
            img.Save(stream, DEFAULT_FORMAT);

            byte[] image_bytes = stream.ToArray();

            // Convert it to Base64 and we're done here!
            return Convert.ToBase64String(image_bytes);
        }

        public static string FileToBase64Encoded(string image_path)
        {
            if (!File.Exists(image_path))
            {
                ThrowError(new Exception("No image was provided!"));
            }

            // try and load the image
            Image loaded_image = null;
            try
            {
                loaded_image = Image.FromFile(image_path);
            }
            catch (Exception e) // if we can't throw an error
            {
             //   PushLogger.Log("Image could not be loaded / bad format "+image_path);
                ThrowError(e);
            }

            if (loaded_image == null) return null;

            // Encode image as base64 PNG
            MemoryStream stream = new MemoryStream();
            loaded_image.Save(stream, DEFAULT_FORMAT);

            byte[] image_bytes = stream.ToArray();

            // Convert it to Base64 and we're done here!
            return Convert.ToBase64String(image_bytes);
        }

		public static string StringToBase64Encoded(string input)
		{
			byte[] bytes = System.Text.Encoding.Unicode.GetBytes(input);
			return Convert.ToBase64String(bytes);
		}

        public static void ThrowError(Exception e)
        {
    //        PushLogger.Logger.LogError(e);
            throw e;
        }

        private const int width = 48, height = 48;
        public static Image GetThumbnail(Image img)
        {
            Image thumbNail = new Bitmap(width, height, img.PixelFormat);

            Graphics g = Graphics.FromImage(thumbNail);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rect = new Rectangle(0, 0, width, height);
            g.DrawImage(img, rect);

            return thumbNail;
        }

    }
}

