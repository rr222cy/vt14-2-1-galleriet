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
        private GalleryClass _galleryClass;

        // Using lazy initialization of the GalleryClass object.
        private GalleryClass Gallery
        {
            get
            {
                return _galleryClass ?? (_galleryClass = new GalleryClass());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Gets the image out of the querystring.
            if (Request.QueryString["Picture"] != null)
            {
                SelectedImage.ImageUrl = String.Format("/galleryImages/{0}", Request.QueryString["Picture"]);              
            }

            // Prints out upload success message to user.
            if (Session["Success"] != null)
            {
                StatusMessage.Text = Session["Success"].ToString();
                Message.Visible = true;
                Session.Remove("Success");
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                // If a file is beeing uploaded, the galleryclass is contacted to validate and save the image, plus create thumbnails.
                if (PictureUpload.HasFile)
                {
                    try
                    {
                        Gallery.SaveImage(PictureUpload.FileContent, PictureUpload.FileName);
                        Session["Success"] = "Uppladdningen lyckades!";
                        Response.Redirect(String.Format("~/Default.aspx?Picture={0}", PictureUpload.FileName));                      
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Ett fel inträffade då bilden skulle laddas upp.");                       
                    }                    
                }
            }
        }

        // Gets all the thumbnails.
        public IEnumerable<GalleryClass> GalleryThumbnailsRepeater_GetData()
        {
            return Gallery.GetImageNames();
        }
    }
}