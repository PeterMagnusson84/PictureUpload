using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Gallery
{
    public class Gallery
    {
        //Undersöka om en fil har en tillåten filändelse med hjälp av reguljärt uttryck.
        private static readonly Regex ApprovedExenstions;

        static Gallery()
        {
            ApprovedExenstions = new Regex(@"^.*\.(gif|jpg|png)$");
        }

        //Den fysiska sökvägen där bilderna sparas.
        public static string PhysicalUploadPath
        {
            get
            {
                return HttpContext.Current.Server.MapPath(@"Pictures/");
            }
            set
            {
                PhysicalUploadPath = value;
            }
        }

        //Metod för att spara en bild och miniatyr.
        public string SaveImage(Stream stream, string fileName )
        {
            int imageExist = 2;
            var image = System.Drawing.Image.FromStream(stream);
            var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);

            try
            {
                //Kollar så att det är rätt format.
                if (!IsValidImage(image))
                {
                    throw new InvalidDataException("");
                }
                //Säkerställer så att filnamnet är unikt.
                while (ImageExits(fileName))
                {
                    string orginalFileName = Path.GetFileNameWithoutExtension(String.Format("{0}{1}", PhysicalUploadPath, fileName));
                    string fileNameChange = Path.GetExtension(String.Format("{0}{1}", PhysicalUploadPath, fileName));

                    fileName = String.Format("{0}{1}{2}", orginalFileName, imageExist, fileNameChange);

                    imageExist++;
                }
                image.Save(PhysicalUploadPath + fileName);
                thumbnail.Save(PhysicalUploadPath + @"thumb/" + fileName);
            }
            catch (Exception)
            {

                throw new ArgumentException("De gick inte att spara bilden!");
            }

            return fileName;
        }

        private bool IsValidImage(Image image)
        {
            return
              image.RawFormat.Guid == ImageFormat.Gif.Guid ||
              image.RawFormat.Guid == ImageFormat.Png.Guid ||
              image.RawFormat.Guid == ImageFormat.Jpeg.Guid;
            
        }

        public bool ImageExits(string name)
        {
            return File.Exists(String.Format("{0}{1}", PhysicalUploadPath, name));
        }

        //Innehåller ett List-objekt med bildernas filnamn.
        public static List<string> GetImageNames()
        {
            string p = String.Format(PhysicalUploadPath + "thumb/");
            DirectoryInfo dirIn = new DirectoryInfo(p);
            FileInfo[] images = dirIn.GetFiles();
            List<string> list = new List<string>();


            for (int i = 0; i < images.Length; i++)
            {
                list.Add(images[i].Name);
            }

            return list;
        }
    }
}