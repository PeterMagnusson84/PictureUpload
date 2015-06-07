using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Gallery
{

    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string i = Request.QueryString["img"];

                if (i != null)
                {
                    bigImgDisplay.Visible = true;
                    bigImg.ImageUrl = "~/Pictures/" + i;
                }
            }
 
            PicList.DataSource = Gallery.GetImageNames();
            PicList.DataBind();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var upload = PictureFileUpload.FileName;
                var stream = PictureFileUpload.FileContent;

                Gallery modelGallery = new Gallery();

                modelGallery.SaveImage(stream, upload);
                //Session["Uploadsuccses"] = true;
                Response.Redirect("Default.aspx?img=" + upload);



            }
        }
    }
}