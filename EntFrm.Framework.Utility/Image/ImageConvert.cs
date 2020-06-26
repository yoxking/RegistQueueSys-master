using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EntFrm.Framework.Utility
{
    public class ImageConvert
    {
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public static string ToBaseString(Image image)
        {
            try
            {
                ImageCodecInfo myImageCodecInfo;
                Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                // Get an ImageCodecInfo object that represents the JPEG codec.
                myImageCodecInfo = GetEncoderInfo("image/jpeg");

                // Create an Encoder object based on the GUID

                // for the Quality parameter category.
                myEncoder = Encoder.Quality;

                // Create an EncoderParameters object.

                // An EncoderParameters object has an array of EncoderParameter

                // objects. In this case, there is only one

                // EncoderParameter object in the array.
                myEncoderParameters = new EncoderParameters(1);

                // Save the bitmap as a JPEG file with quality level 25.
                myEncoderParameter = new EncoderParameter(myEncoder, 300L);
                myEncoderParameters.Param[0] = myEncoderParameter;

                MemoryStream ms = new MemoryStream();
                image.Save(ms, myImageCodecInfo, myEncoderParameters);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                string pic = Convert.ToBase64String(arr);

                return pic;
            }
            catch (Exception)
            {
                return "Fail to change bitmap to string!";
            }
        }

        public static Image FromBaseString(string pic)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(pic);
                //读入MemoryStream对象
                MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
                memoryStream.Write(imageBytes, 0, imageBytes.Length);
                //转成图片
                Image image = Image.FromStream(memoryStream);

                return image;
            }
            catch (Exception)
            {
                Image image = null;
                return image;
            }
        }

        /// <summary>
        /// 将图片转换为Base64格式字符串
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ToBase64String(Image img)
        {
            return ToBase64String(img, ImageFormat.Png);
        }

        public static string ToBase64String(Image img, ImageFormat format)
        {
            if (img != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, format);
                    byte[] buffer = ms.ToArray();
                    return Convert.ToBase64String(buffer);
                }
            }
            return string.Empty;
        }

        public static string ToBase64HtmlString(Image img)
        {
            return ToBase64HtmlString(img, ImageFormat.Png);
        }

        public static string ToBase64HtmlString(Image img, ImageFormat format)
        {
            string type = "jpeg";
            if (format.Guid == ImageFormat.Png.Guid)
            {
                type = "png";
            }
            else if (format.Guid == ImageFormat.Gif.Guid)
            {
                type = "gif";
            }
            return string.Format("data:image/{0};base64,", type) + ToBase64String(img, format);
        }

        public static Image FromBase64String(string base64Str)
        {
            Bitmap bitmap = null;
            Image img = null;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] buffer = Convert.FromBase64String(base64Str);
                ms.Write(buffer, 0, buffer.Length);
                try
                {
                    img = Image.FromStream(ms);
                    if (img != null)
                    {
                        bitmap = new Bitmap(img.Width, img.Height);
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.DrawImage(img, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                        }
                    }
                }
                catch { }
            }
            return bitmap;
        }

        public static Image FromBase64HtmlString(string str)
        {
            string[] strs = str.Split(',');
            if (strs.Length > 0)
            {
                return FromBase64String(strs[strs.Length - 1]);
            }
            else
            {
                return FromBase64String(str);
            }
        }


    }
}
