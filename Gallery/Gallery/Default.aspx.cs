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
            if (Request.QueryString["Picture"] != null)
            {
                SelectedImage.ImageUrl = String.Format("/galleryImages/{0}", Request.QueryString["Picture"]);              
            }

            if (Request.QueryString["Success"] != null)
            {
                Message.Visible = true;
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                if (PictureUpload.PostedFile != null)
                {
                    String filename = PictureUpload.PostedFile.FileName;

                    // This loops checks if a picture with the same name exists, if yes, a character will be added.
                    while (File.Exists(Server.MapPath(@"~/galleryImages/" + filename)))
                    {
                        filename = "(2)" + filename;
                    }

                    // Saves the image.
                    PictureUpload.PostedFile.SaveAs(Server.MapPath(@"~/galleryImages/"+filename));

                    // Creates a thumbnail with the size 150x150px.
                    System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(@"~/galleryImages/"+filename));
                    System.Drawing.Image thumbnail = image.GetThumbnailImage(150, 150, null, System.IntPtr.Zero);
                    thumbnail.Save(Server.MapPath(@"~/galleryImages/thumbnails/"+filename));

                    

                    Response.Redirect(String.Format("~/Default.aspx?Picture={0}&Success=Yes", filename));
                }
            }
        }

        public IEnumerable<GalleryClass> GalleryThumbnailsRepeater_GetData()
        {
            // Gets all the thumbnails.
            var dir = new DirectoryInfo(Server.MapPath(@"~/galleryImages/thumbnails/"));
            //return dir.GetFiles();

            return (from files in dir.GetFiles()
                    select new Gallery.Models.GalleryClass
                    {
                        Name = files.Name,
                        Class = "imageBorder"
                    }).AsEnumerable();
        }
    }
}