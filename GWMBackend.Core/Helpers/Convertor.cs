using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

namespace GWMBackend.Core.Helpers
{
    public class Convertor
    {
        public static dynamic JsonDeserialize(string value)
        {
            var json = JsonConvert.DeserializeObject<JObject>(value);
            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            dynamic dobj = jsonSerializer.Deserialize<dynamic>(json.ToString());
            return dobj;
        }

        public static string CombineValueWithDateTime(string value)
        {
            string result = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + value;
            return result;
        }

        public static string MiladiDateToJalaliDate(DateTime date)
        {
            PersianCalendar jc = new PersianCalendar();
            return string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(date), jc.GetMonth(date), jc.GetDayOfMonth(date));
        }

        public static string Base64ToImage(string base64String, string imagePath, string imageName)
        {
            try
            {
                var fileFormat = "." + base64String.Split(",")[0].Split("/")[1].Split(";")[0];
                //***Convert Image File to Base64 Encoded string***//

                //Read the uploaded file using BinaryReader and convert it to Byte Array.
                //BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream);
                //byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);

                //Convert the Byte Array to Base64 Encoded string.
                //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                //***Save Base64 Encoded string as Image File***//

                //Convert Base64 Encoded string to Byte Array.
                if (fileFormat.ToLower() == ".jpeg" || fileFormat.ToLower() == ".jpg" || fileFormat.ToLower() == ".png" || fileFormat.ToLower() == ".gif" || fileFormat.ToLower() == "mp4")
                {
                    byte[] imageBytes = Convert.FromBase64String(base64String.Split(",")[1]);

                    //Save the Byte Array as Image File.
                    string filePath = imagePath + imageName + fileFormat;
                    File.WriteAllBytes(filePath, imageBytes);
                    return imageName + fileFormat;
                }
                else
                {
                    return "InvalidFormat";
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return "default.jpeg";
            }
        }
        public static string Base64ToThumbnail(string base64String, string imagePath, string imageName)
        {
            try
            {
                var fileFormat = "." + base64String.Split(",")[0].Split("/")[1].Split(";")[0];
                byte[] bytes = Convert.FromBase64String(base64String.Split(",")[1]);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Bitmap thumb = new Bitmap(100, 100);
                    using (Image bmp = Image.FromStream(ms))
                    {
                        using (Graphics g = Graphics.FromImage(thumb))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.SmoothingMode = SmoothingMode.AntiAlias;                           
                            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            g.DrawImage(bmp, 0, 0, 100, 100);
                        }
                    }
                    //Save the Byte Array as Image File.
                    string filePath = imagePath + imageName + fileFormat;
                    MemoryStream stream = new MemoryStream();
                    thumb.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    Byte[] newBytes = stream.ToArray();
                    File.WriteAllBytes(filePath, newBytes);
                    return imageName + fileFormat;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return "crash";
            }
        }
        public static string Base64ToFile(string base64String, string filePath, string fileName)
        {
            try
            {
                var fileFormat = "." + base64String.Split(",")[0].Split("/")[1].Split(";")[0];
                //***Convert Image File to Base64 Encoded string***//

                //Read the uploaded file using BinaryReader and convert it to Byte Array.
                //BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream);
                //byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);

                //Convert the Byte Array to Base64 Encoded string.
                //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                //***Save Base64 Encoded string as Image File***//

                //Convert Base64 Encoded string to Byte Array.
                if (fileFormat.ToLower() == ".jpeg" || fileFormat.ToLower() == ".jpg" || fileFormat.ToLower() == ".png" || fileFormat.ToLower() == ".gif")
                {
                    byte[] imageBytes = Convert.FromBase64String(base64String.Split(",")[1]);

                    //Save the Byte Array as Image File.
                    string Path = filePath + fileName + fileFormat;
                    File.WriteAllBytes(Path, imageBytes);
                    return fileName + fileFormat;
                }
                else
                {
                    return "InvalidFormat";
                }

            }
            catch
            {
                return "ConvertError";
            }
        }
        public static string Base64ToFile(string base64String, string filePath, string fileName, string fileFormat)
        {
            try
            {
                //***Convert Image File to Base64 Encoded string***//

                //Read the uploaded file using BinaryReader and convert it to Byte Array.
                //BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream);
                //byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);

                //Convert the Byte Array to Base64 Encoded string.
                //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                //***Save Base64 Encoded string as Image File***//

                //Convert Base64 Encoded string to Byte Array.

                if (fileFormat.ToLower() == ".jpeg" || fileFormat.ToLower() == ".mp4" || fileFormat.ToLower() == ".gif")
                {
                    byte[] imageBytes = Convert.FromBase64String(base64String);

                    //Save the Byte Array as Image File.
                    string Path = filePath + fileName + fileFormat;
                    File.WriteAllBytes(Path, imageBytes);
                    return fileName + fileFormat;
                }
                else
                {
                    return "InvalidFormat";
                }

            }
            catch
            {
                return "ConvertError";
            }
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long DateTimeToTimestamp(DateTime value)
        {
            return (long)Math.Floor((value.ToUniversalTime() - UnixEpoch).TotalMilliseconds);
        }

        public static DateTime TimeStampToDateTime(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis).ToLocalTime();
        }
    }
}
