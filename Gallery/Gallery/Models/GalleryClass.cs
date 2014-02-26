using System;
using System.Collections.Generic;
using System.Drawing;
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
        public IEnumerable<string> GetImageNames()
        {
            throw new NotImplementedException();
        }

        // Method for checking if a image truly exists, returns true or false.
        public static bool ImageExists(string name)
        {
            throw new NotImplementedException();
        }

        private bool IsValidImage(Image image)
        {
            throw new NotImplementedException();
        }

        public string SaveImage(Stream stream, string fileName)
        {
            throw new NotImplementedException();
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