using HomeMultimediaLibrary.Models;
using HomeMultimediaLibrary.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HomeMultimediaLibrary
{
    public partial class AddItem: BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> types = new List<String> { "Book", "Magazine", "Movie", "Album" };
            itemType.DataSource = types;
            itemType.DataBind();
        }
    }
}