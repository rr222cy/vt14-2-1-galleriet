using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Gallery.Models
{
    public class GalleryClass
    {
        // Fields that will be used.
        private readonly static Regex ApprovedExtensions;
        private readonly static Regex SantizePath;
        private readonly static string PhysicalUploadedImagesPath;
        private readonly static string PhysicalUploadedThumbnailsPath;

        // Fields for giving a highlight-border to image in focus.
        public string Class;
        public string Name;

        // Method for returning a list-object-reference to all images in given directory.
        public IEnumerable<GalleryClass> GetImageNames()
        {
            // Gets all the thumbnails.
            var dir = new DirectoryInfo(PhysicalUploadedThumbnailsPath);

            return (from files in dir.GetFiles()
                    select new Gallery.Models.GalleryClass
                    {
                        Name = files.Name,
                        Class = "imageBorder"
                    }).OrderBy(files => files.Name).ToList();          
        }

        // Method for checking if a image truly exists, returns true or false.
        public static bool ImageExists(string name)
        {
            return File.Exists(Path.Combine(PhysicalUploadedImagesPath, name));
        }

        // Method for checking if the file is truly a image of supported type, if yes true is returned, else, false.
        private bool IsValidImage(Image image)
        {
            if (image.RawFormat.Guid == ImageFormat.Gif.Guid || image.RawFormat.Guid == ImageFormat.Jpeg.Guid || image.RawFormat.Guid == ImageFormat.Png.Guid)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }

        // Method for saving the uploaded image, plus generate a thumbnail in the desired size.
        public string SaveImage(Stream stream, string fileName)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            // Checks if the file truly is a image, and a supported one.
            if (!IsValidImage(image))
            {
                throw new ArgumentException("The file is not a valid picture, .gif, .jpg, .png supported only.");
            }

            // Checks that the file has no illegal characters in filename.
            if (SantizePath.IsMatch(fileName))
            {
                throw new ArgumentException("The file has illegal characters in filename, please change.");
            }

            // Checks if a picture with the same name exists, if yes, a character will be added in a later loop.
            if (ImageExists(fileName))
            {
                // This loops checks if a picture with the same name exists, if yes, a character will be added.
                while (ImageExists(fileName))
                {
                    fileName = "(2)" + fileName;
                }
            }

            // Saves the image since it has passed all tests.
            image.Save(Path.Combine(PhysicalUploadedImagesPath, fileName));

            // Settings for the thumbnail to be created, 150x150px here - also saves the thumbnail.
            System.Drawing.Image thumbnail = image.GetThumbnailImage(150, 150, null, System.IntPtr.Zero);
            thumbnail.Save(Path.Combine(PhysicalUploadedThumbnailsPath, fileName));

            return fileName;
        }

        // Constructor.
        static GalleryClass()
        {
            ApprovedExtensions = new Regex(@"([^\s]+(\.(?i)(jpg|png|gif))$)", RegexOptions.IgnoreCase);
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));

            PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"galleryImages");
            PhysicalUploadedThumbnailsPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), @"galleryImages\thumbnails");
        }
    }
}