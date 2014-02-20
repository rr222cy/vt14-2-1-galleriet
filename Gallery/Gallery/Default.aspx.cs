using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gallery.Models;
using System.IO;

namespace Gallery
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                if (PictureUpload.PostedFile != null)
                {
                    String filename = PictureUpload.PostedFile.FileName;
                    PictureUpload.PostedFile.SaveAs(Server.MapPath(@"~/galleryImages/"+filename));

                    System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(@"~/galleryImages/"+filename));
                    System.Drawing.Image thumbnail = image.GetThumbnailImage(150, 150, null, System.IntPtr.Zero);

                    thumbnail.Save(Server.MapPath(@"~/galleryImages/thumbnails/"+filename));
                }
            }
        }

        public IEnumerable<System.IO.FileInfo> GalleryThumbnailsRepeater_GetData()
        {
            var dir = new DirectoryInfo(Server.MapPath(@"~/galleryImages/thumbnails/"));
            return dir.GetFiles();
        }
    }
}