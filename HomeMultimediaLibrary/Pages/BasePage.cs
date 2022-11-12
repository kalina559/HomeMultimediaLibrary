using HomeMultimediaLibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeMultimediaLibrary.Pages
{
    public class BasePage : System.Web.UI.Page
    {
        protected const string TYPE_ALL = "All";
        protected const string TYPE_BOOK = "Book";
        protected const string TYPE_MAGAZINE = "Magazine";
        protected const string TYPE_FILM = "Film";
        protected const string TYPE_ALBUM = "Album";
        protected void Page_PreInit(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(User.Identity.GetUserId());

            if (user?.Theme != null)
            {
                Page.Theme = user.Theme;
            } else
            {
                Page.Theme = "Light";
            }
        }
    }
}